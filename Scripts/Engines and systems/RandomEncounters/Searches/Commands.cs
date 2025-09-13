//--------------------------------------------------------------------------------
// Copyright Joe Kraska, 2006. This file is restricted according to the GPL.
// Terms and conditions can be found in COPYING.txt.
//--------------------------------------------------------------------------------
using System;
//--------------------------------------------------------------------------------
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Searches;
//--------------------------------------------------------------------------------
//  
//--------------------------------------------------------------------------------
namespace Server.Scripts.Commands
{
    public class SearchCommands
    {
        public static void Initialize()
        { 
            Server.Commands.CommandSystem.Register( "search", AccessLevel.Player, new CommandEventHandler( OnCommand ) );
        }
 
        public static void OnCommand( CommandEventArgs e )
        {
            if( e.Length >= 1 )
            {
                switch( e.Arguments[0] )
                {
                    case "octant":

                        Octant( e );
                        break;

                    case "line":

                        Line( e );
                        break;

                    case "circle":

                        Circle( e );
                        break;

                    case "spiral":

                        Spiral( e );
                        break;
                }
            }
        }
        public static void Octant( CommandEventArgs e )
        {
            Tour tour = delegate( Map map, int x, int y )
            {
                int         xLoc = e.Mobile.Location.X + x;
                int         yLoc = e.Mobile.Location.Y + y;

                LandTile        landTile  = map.Tiles.GetLandTile( xLoc, yLoc );

                Effects.SendLocationParticles( 
                    EffectItem.Create( new Point3D( xLoc, yLoc, landTile.Z), map, EffectItem.DefaultDuration ), 
                    0x37CC, 1, 40, 96, 3, 9917, 0 
                    );

                if( Math.Abs( x ) > 12 ) return true; // stop searching
                if( Math.Abs( y ) > 12 ) return true; // stop searching

                return false;
            };

            Direction   dir  = (Direction)((int)(e.Mobile.Direction) & 0x0f);

            Search.Octant( 
                e.Mobile.Map, 
                dir,
                1, 
                SearchDirection.Outwards, 
                tour 
                );
        }
        public static void Line( CommandEventArgs e )
        {
            Tour tour = delegate( Map map, int x, int y )
            {
                LandTile        landTile  = map.Tiles.GetLandTile( x, y );

                Effects.SendLocationParticles( 
                    EffectItem.Create( new Point3D( x, y, landTile.Z), map, EffectItem.DefaultDuration ), 
                    0x37CC, 1, 40, 96, 3, 9917, 0 
                    );

                return false;
            };

            Search.Line( 
                e.Mobile.Map, 
                new Point2D( e.Mobile.Location.X-3, e.Mobile.Location.Y-3), 
                new Point2D( e.Mobile.Location.X+3, e.Mobile.Location.Y+3), 
                tour
                );
        }
        public static void Circle( CommandEventArgs e )
        {
            Tour tour = delegate( Map map, int x, int y )
            {
                int         xLoc = e.Mobile.Location.X + x;
                int         yLoc = e.Mobile.Location.Y + y;

                LandTile        landTile  = map.Tiles.GetLandTile( xLoc, yLoc );

                Effects.SendLocationParticles( 
                    EffectItem.Create( new Point3D( xLoc, yLoc, landTile.Z), map, EffectItem.DefaultDuration ), 
                    0x37CC, 1, 40, 96, 3, 9917, 0 
                    );

                return false;
            };

            int radius = 5;

            if( e.Length == 2 )
                int.TryParse( e.Arguments[1], out radius );

            Search.Circle( 
                e.Mobile.Map, 
                radius,
                tour
                );
        }
        public static void Spiral( CommandEventArgs e )
        {
            int i = 0;

            Tour tour = delegate( Map map, int x, int y )
            {
                int         xLoc = e.Mobile.Location.X + x;
                int         yLoc = e.Mobile.Location.Y + y;

                LandTile        landTile  = map.Tiles.GetLandTile( xLoc, yLoc );

                i++;

                Effects.SendLocationParticles( 
                    EffectItem.Create( new Point3D( xLoc, yLoc, landTile.Z), map, EffectItem.DefaultDuration ), 
                    0x37CC, 1, 40, i, 3, 9917, 0 
                    );

                if( Math.Abs( x ) > 12 ) return true; // stop searching
                if( Math.Abs( y ) > 12 ) return true; // stop searching

                return false;
            };
            Search.Spiral( 
                e.Mobile.Map, 
                1,
                SearchDirection.Outwards,
                false,
                tour
                );
        }
    }
}
