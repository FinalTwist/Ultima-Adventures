//------------------------------------------------------------------------------
// <summary>
//   
//  </summary>
//------------------------------------------------------------------------------
using System;
using System.Collections;
//------------------------------------------------------------------------------
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using Server.Misc;
using Server.Searches;
//------------------------------------------------------------------------------
namespace Server.Misc
{
    //--------------------------------------------------------------------------
    public class SpawnFinder
    {
        //----------------------------------------------------------------------
        //  Here, we are going to spiral inwards towards the center point,
        //  picking the first spawnable location at the outer most edge of
        //  the spiral that we encounter; a random starting position on the
        //  spiral ensures no bias towards particular positions
        //----------------------------------------------------------------------
        public static bool FindInwards(
            PlayerMobile    pm,
            Point3D         centerPoint,
            ref Point3D     spawnPoint,
            LandType        landType,
            int             distance,
            EffectType      effectType,
            int             effectHue
            )
        {
            return FindSpiral( pm, centerPoint, ref spawnPoint, landType, distance, true, effectType, effectHue );
        }
        //----------------------------------------------------------------------
        //  Opposite of the above; we want the first spawn location closest to
        //  the center -- no closer than the initial distance
        //----------------------------------------------------------------------
        public static bool FindOutwards(
            PlayerMobile    pm,
            Point3D         centerPoint,
            ref Point3D     spawnPoint,
            LandType        landType,
            int             distance,
            EffectType      effectType,
            int             effectHue
            )
        {
            return FindSpiral( pm, centerPoint, ref spawnPoint, landType, distance, false, effectType, effectHue );
        }
        //----------------------------------------------------------------------
        //  Here, we are going to search the quadrant that it is ahead of the player, because
        //  he is running
        //----------------------------------------------------------------------
        public static bool FindAhead(
            PlayerMobile    pm,
            Point3D         centerPoint,
            ref Point3D     spawnPoint,
            LandType        landType,
            int             distance,
            EffectType      effectType,
            int             effectHue
            )
        {
            Direction dir = (Direction) ( (int) pm.Direction & 0x0f );

            Point3D         foundPoint      = new Point3D();

            Tour tour = delegate( Map map, int x, int y )
            {
                if( Utility.RandomDouble ( ) < .5 ) return false; // break it up a little.

                Point2D currentPoint = new Point2D( pm.Location.X+x, pm.Location.Y+y );

                if  ( FindSpawnTileInternal(
                        pm,
                        centerPoint,
                        currentPoint,
                        ref foundPoint,
                        landType,
                        effectType,
                        effectHue
                        )
                    )
                {
                    return true;
                }

                return false;
            };

            bool found = Search.Octant( 
                pm.Map, 
                dir,
                distance, 
                SearchDirection.Inwards, 
                tour 
                );

            if( found )
            {
                spawnPoint.X = foundPoint.X;
                spawnPoint.Y = foundPoint.Y;
                spawnPoint.Z = foundPoint.Z;

                return true;
            }

            return false;
        }
        //----------------------------------------------------------------------
        //  Implementation of the find spiral pattern; supports inwards from
        //    distance, and outwards from distance searches
        //----------------------------------------------------------------------
        private static bool FindSpiral(
            PlayerMobile    pm,
            Point3D         centerPoint,
            ref Point3D     spawnPoint,
            LandType        landType,
            int             distance,
            bool            inwards,
            EffectType      effectType,
            int             effectHue
            )
        {
            Point3D         foundPoint      = new Point3D();

            Tour tour = delegate( Map map, int x, int y )
            {
                Point2D currentPoint = new Point2D( pm.Location.X+x, pm.Location.Y+y );

                if  ( FindSpawnTileInternal(
                        pm,
                        centerPoint,
                        currentPoint,
                        ref foundPoint,
                        landType,
                        effectType,
                        effectHue
                        )
                    )
                {
                    return true;
                }

                return false;
            };

            bool found = Search.Spiral( 
                pm.Map, 
                distance,
                inwards ? SearchDirection.Inwards: SearchDirection.Outwards,
                true,
                tour
                );

            if( found )
            {
                spawnPoint.X = foundPoint.X;
                spawnPoint.Y = foundPoint.Y;
                spawnPoint.Z = foundPoint.Z;

                return true;
            }

            return false;
        }
        //----------------------------------------------------------------------
        //  Here we do the actual work of studying tiles to see if they are suitable
        //  for spawn; if something is wrong with the spawn finder, it's probably
        //  here
        //----------------------------------------------------------------------
        private static bool FindSpawnTileInternal( 
            PlayerMobile    pm,
            Point3D         centerPoint,
            Point2D         currentPoint,
            ref Point3D     spawnPoint,
            LandType        landType,
            EffectType      effectType,
            int             effectHue
            )
        {
            Map         map             = pm.Map;
            Region      sourceRegion    = pm.Region;

            LandTile        landTile        = map.Tiles.GetLandTile(    centerPoint.X, centerPoint.Y );
            LandTile        spawnTile       = map.Tiles.GetLandTile(    currentPoint.X, currentPoint.Y );
            StaticTile[]    staticTiles     = map.Tiles.GetStaticTiles( currentPoint.X, currentPoint.Y, true );

            Region r = Region.Find( new Point3D( currentPoint.X, currentPoint.Y, landTile.Z ), map );

            if( r.GetType() != sourceRegion.GetType() ) 
            {
                //Console.WriteLine( "player.Location="+pm.Location );
                //Console.WriteLine( "rquery={0},{1}", currentPoint.X, currentPoint.Y );
                //Console.WriteLine( "sourceRegion="+sourceRegion.GetType() );
                //Console.WriteLine( "r="+r.GetType() );
                return false;
            }

            if( r is HouseRegion ) return false;

            //--------------------------------------------------------------
            //  WATER LOCATION:
            //--------------------------------------------------------------
            if ((TileData.LandTable[spawnTile.ID & 0x3FFF].Flags & TileFlag.Wet) == TileFlag.Wet)
            {
                if( landType != LandType.Water ) return false; // water tile, isn't a water encounter

                //----------------------------------------------------------
                // Instead of the CanSpawn function we use can fit, as it allows us to not require
                // a surface (which defaults to true otherwise)
                //----------------------------------------------------------

                if (map.CanFit( currentPoint.X, currentPoint.Y, spawnTile.Z, 16, true, true, false ))
                {
                    spawnPoint.X = currentPoint.X;
                    spawnPoint.Y = currentPoint.Y;
                    spawnPoint.Z = spawnTile.Z;

                    return true;
                }
                //----------------------------------------------------------
                // XXX -- note; there is a degenerate case where the static and not the land tile
                //        determines water. This is along coastlines, and in rivers and the like;
                //        may be useful later, as we can take "Wet" cells to generally be "at sea".
                //  For now, though, we are ignoring these static wet tiles. One reason for
                //        this is that if we don't we could end up getting dolphins in small
                //        inland lakes.
                //----------------------------------------------------------
            }
            //--------------------------------------------------------------
            //  LAND LOCATION:
            //--------------------------------------------------------------
            else
            {
                if( landType == LandType.Water ) return false; // land tile, isn't a land encounter

                //  search for wet static tiles; these are coastal cells; 
                //  if we we want land, we don't want to spawn in cells like these:

                bool foundWet = false;

                foreach( StaticTile staticTile in staticTiles ) 
                {
                    if ((TileData.ItemTable[staticTile.ID & 0x3FFF].Flags & TileFlag.Wet) == TileFlag.Wet) 
                    {
                        foundWet=true;
                        break;
                    }
                }

                if( foundWet ) 
                {
                    return false;
                }

//if( RandomEncounterEngine.DebugEffect )
//Effects.SendLocationParticles( 
//EffectItem.Create( new Point3D( currentPoint.X, currentPoint.Y, centerPoint.Z), map, EffectItem.DefaultDuration ), 
//0x37CC, 1, 40, 36, 3, 9917, 0 
//);

                // we will probe highest to lowest if the player is above ground level (e.g, they are in  a tower),
                // or lowest to highest otherwise:

                ArrayList staticTileArray = new ArrayList( staticTiles );
                staticTileArray.Sort( new TileComparer() );
                if( centerPoint.Z > landTile.Z ) staticTileArray.Reverse();

                foreach( StaticTile staticTile in staticTileArray )
                {
                    if (map.CanSpawnMobile( currentPoint.X, currentPoint.Y, staticTile.Z))
                    {
                        spawnPoint.X = currentPoint.X;
                        spawnPoint.Y = currentPoint.Y;
                        spawnPoint.Z = staticTile.Z;

                        if( RandomEncounterEngine.DebugEffect )
                            Effects.SendLocationParticles( 
                                EffectItem.Create( new Point3D( spawnPoint.X, spawnPoint.Y, spawnPoint.Z), map, EffectItem.DefaultDuration ), 
                                0x37CC, 1, 40, 96, 3, 9917, 0 
                                );
                        else if( effectType != EffectType.None )
                        {
                            EffectEntry effect = EffectEntry.Lookup[(int)effectType];

                            Effects.SendLocationParticles( 
                                EffectItem.Create( new Point3D( spawnPoint.X, spawnPoint.Y, spawnPoint.Z), map, EffectItem.DefaultDuration ), 
                                effect.Animation, effect.Speed, effect.Duration, effectHue, effect.RenderMode, effect.Effect, 0 
                                );
                        }
                        //Console.WriteLine("Found good point at static.Z");
                        return true;
                    }

//if( RandomEncounterEngine.DebugEffect )
//Effects.SendLocationParticles( 
//EffectItem.Create( new Point3D( currentPoint.X, currentPoint.Y, staticTile.Z), map, EffectItem.DefaultDuration ), 
//0x37CC, 1, 40, 36, 3, 9917, 0 
//);
                }

                // probe spawn tile Z
                if (map.CanSpawnMobile( currentPoint.X, currentPoint.Y, spawnTile.Z))
                {
                    spawnPoint.X = currentPoint.X;
                    spawnPoint.Y = currentPoint.Y;
                    spawnPoint.Z = spawnTile.Z;

                    if( RandomEncounterEngine.DebugEffect )
                        Effects.SendLocationParticles( 
                            EffectItem.Create( new Point3D( spawnPoint.X, spawnPoint.Y, spawnPoint.Z), map, EffectItem.DefaultDuration ), 
                            0x37CC, 1, 40, 36, 3, 9917, 0 
                            );
                    else if( effectType != EffectType.None )
                    {
                        EffectEntry effect = EffectEntry.Lookup[(int)effectType];

                        Effects.SendLocationParticles( 
                            EffectItem.Create( new Point3D( spawnPoint.X, spawnPoint.Y, spawnPoint.Z), map, EffectItem.DefaultDuration ), 
                            effect.Animation, effect.Speed, effect.Duration, effectHue, effect.RenderMode, effect.Effect, 0 
                            );
                    }
//Console.WriteLine("Found good point at search point.Z");
                    return true;
                }

                // probe player tile Z
                if (map.CanSpawnMobile( currentPoint.X, currentPoint.Y, centerPoint.Z ))
                {
                    spawnPoint.X = currentPoint.X;
                    spawnPoint.Y = currentPoint.Y;
                    spawnPoint.Z = centerPoint.Z;

                    if( RandomEncounterEngine.DebugEffect )
                        Effects.SendLocationParticles( 
                            EffectItem.Create( new Point3D( spawnPoint.X, spawnPoint.Y, spawnPoint.Z), map, EffectItem.DefaultDuration ), 
                            0x37CC, 1, 40, 167, 3, 9917, 0 
                            );
                    else if( effectType != EffectType.None )
                    {
                        EffectEntry effect = EffectEntry.Lookup[(int)effectType];

                        Effects.SendLocationParticles( 
                            EffectItem.Create( new Point3D( spawnPoint.X, spawnPoint.Y, spawnPoint.Z), map, EffectItem.DefaultDuration ), 
                            effect.Animation, effect.Speed, effect.Duration, effectHue, effect.RenderMode, effect.Effect, 0 
                            );
                    }
//Console.WriteLine("Found good point at center point.Z");
                    return true;
                }

//if( RandomEncounterEngine.DebugEffect )
//Effects.SendLocationParticles( 
//EffectItem.Create( new Point3D( currentPoint.X, currentPoint.Y, spawnTile.Z), map, EffectItem.DefaultDuration ), 
//0x37CC, 1, 40, 36, 3, 9917, 0 
//);
            }

            return false;
        }

        public class TileComparer : IComparer
        {
            public int Compare( object o1, object o2 )
            {
                StaticTile tile1 = (StaticTile) o1;
                StaticTile tile2 = (StaticTile) o2;

                if ( tile1.Z < tile2.Z ) return -1;
                if ( tile1.Z > tile2.Z ) return 1;

                else return 0;
            }
        }
    }
}
