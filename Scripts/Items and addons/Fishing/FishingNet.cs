using System;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class FishingNet : Item
	{
		private bool m_InUse;

		[Constructable]
		public FishingNet() : base( 0x0DCA )
		{
			Name = "fishing net";
			Weight = 1.0;
			Hue = Utility.RandomList( m_Hues );
		}

		private static int[] m_Hues = new int[]
			{
				0x09B,
				0x0CD,
				0x0D3,
				0x14D,
				0x1DD,
				0x1E9,
				0x1F4,
				0x373,
				0x451,
				0x47F,
				0x489,
				0x492,
				0x4B5,
				0x8AA
			};

		public FishingNet( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Use On The High Seas To Search For Something");
			list.Add( 1049644, "Requires 30 Fishing");
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
			writer.Write( m_InUse );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 1:
				{
					m_InUse = reader.ReadBool();

					if ( m_InUse )
						Delete();

					break;
				}
			}

			Stackable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_InUse )
			{
				from.SendLocalizedMessage( 1010483 ); // Someone is already using that net!
			}
			else if ( from.Skills[SkillName.Fishing].Value < 30.0 )
			{
				from.SendMessage("You are not skilled enough at fishing to use this net.");
			}
			else if ( Worlds.IsOnBoat( from ) == false )
			{
				from.SendMessage("You'll need to be on your boat to use this net.");
			}
			else if ( Worlds.BoatToCloseToTown( from ) == true )
			{
				from.SendMessage("You'll need to go out to deeper waters to use this net.");
			}
			else if ( IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1010484 ); // Where do you wish to use the net?
				from.BeginTarget( -1, true, TargetFlags.None, new TargetCallback( OnTarget ) );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public void OnTarget( Mobile from, object obj )
		{
			if ( Deleted || m_InUse )
				return;

			IPoint3D p3D = obj as IPoint3D;

			if ( p3D == null )
				return;

			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			int x = p3D.X, y = p3D.Y;

			LandTile landTile = map.Tiles.GetLandTile( x, y );
			StaticTile[] tiles = map.Tiles.GetStaticTiles( x, y, true );

			bool hasWater = false;

			if ( landTile.Z == p3D.Z && Server.Misc.Worlds.IsWaterTile( landTile.ID, 0 ) )
				hasWater = true;

			for ( int i = 0; i < tiles.Length; ++i )
			{
				StaticTile tile = tiles[i];

				if ( tile.Z == p3D.Z && Server.Misc.Worlds.IsWaterTile( tile.ID, 0 ) )
					hasWater = true;
			}

			if ( !from.InRange( p3D, 6 ) )
			{
				from.SendLocalizedMessage( 500976 ); // You need to be closer to the water to fish!
			}
			else if ( hasWater )
			{
				Point3D p = new Point3D( x, y, map.GetAverageZ( x, y ) );

				this.ItemID = 0x0DCA;

				m_InUse = true;
				Movable = false;
				MoveToWorld( p, map );

				from.Animate( 12, 5, 1, true, false, 0 );

				Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), TimeSpan.FromSeconds( 1.0 ), 20, new TimerStateCallback( DoEffect ), new object[]{ p, 0, from } );

				from.SendLocalizedMessage( 1010487 ); // You plunge the net into the sea...
			}
			else
			{
				from.SendLocalizedMessage( 1010485 ); // You can only use this net in deep water!
			}
		}

		private void DoEffect( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;

			Point3D p = (Point3D)states[0];
			int index = (int)states[1];
			Mobile from = (Mobile)states[2];

			states[1] = ++index;

			if ( index == 1 )
			{
				Effects.SendLocationEffect( p, Map, 0x352D, 16, 4 );
				Effects.PlaySound( p, Map, 0x364 );
			}
			else if ( index <= 10 || index == 20 )
			{
				for ( int i = 0; i < 3; ++i )
				{
					int x, y;

					switch ( Utility.Random( 8 ) )
					{
						default:
						case 0: x = -1; y = -1; break;
						case 1: x = -1; y =  0; break;
						case 2: x = -1; y = +1; break;
						case 3: x =  0; y = -1; break;
						case 4: x =  0; y = +1; break;
						case 5: x = +1; y = -1; break;
						case 6: x = +1; y =  0; break;
						case 7: x = +1; y = +1; break;
					}

					Effects.SendLocationEffect( new Point3D( p.X + x, p.Y + y, p.Z ), Map, 0x352D, 16, 4 );
				}

				Effects.PlaySound( p, Map, 0x364 );

				if ( index == 20 )
					FinishEffect( p, Map, from );
				else
					this.Z -= 1;
			}
		}

		protected void Spawn( Point3D p, Map map, BaseCreature spawn, int onBoat )
		{
			if ( map == null )
			{
				spawn.Delete();
				return;
			}

			int x = p.X, y = p.Y;

			if ( onBoat != 1 )
			{
				for ( int j = 0; j < 20; ++j )
				{
					int tx = p.X - 2 + Utility.Random( 5 );
					int ty = p.Y - 2 + Utility.Random( 5 );

					LandTile t = map.Tiles.GetLandTile( tx, ty );

					if ( t.Z == p.Z && Server.Misc.Worlds.IsWaterTile( t.ID, 0 ) && !Spells.SpellHelper.CheckMulti( new Point3D( tx, ty, p.Z ), map ) )
					{
						x = tx;
						y = ty;
						break;
					}
				}
			}
			spawn.MoveToWorld( new Point3D( x, y, p.Z ), map );
		}

		protected virtual void FinishEffect( Point3D p, Map map, Mobile from )
		{
			from.RevealingAction();
			Server.Engines.Harvest.Fishing.FishingSkill( from, 5 );

			int count = Utility.RandomMinMax( 1, 6 );
			int onBoat = 0;
			string monster = "";
			Point3D SpawnAt = p;
			switch ( Utility.Random( 12 ) )
			{
				case 0: monster = "AquaticGhoul"; SpawnAt = from.Location; onBoat = 1; break;
				case 1: monster = "SeaWeeder"; SpawnAt = from.Location; onBoat = 1; break;
				case 2: monster = "SeaSnake"; break;
				case 3: monster = "WaterBeetleRiding"; break;
				case 4: monster = "WaterStrider"; break;
				case 5: monster = "OilSlick"; break;
				case 6: monster = "FloatingEye"; break;
				case 7: monster = "SeaTroll"; SpawnAt = from.Location; onBoat = 1; break;
				case 8: monster = "WaterElemental"; break;
				case 9: monster = "GiantCrab"; break;
				case 10: monster = "GiantLamprey"; break;
				case 11: monster = "Locathah"; SpawnAt = from.Location; onBoat = 1; break;
			}

			for ( int i = 0; map != null && i < count; ++i )
			{
				BaseCreature spawn = new AquaticGhoul();

				if ( monster == "AquaticGhoul" ){ spawn = new AquaticGhoul(); }
				else if ( monster == "SeaWeeder" ){ spawn = new SeaWeeder(); }
				else if ( monster == "SeaSnake" ){ spawn = new SeaSnake(); }
				else if ( monster == "WaterBeetleRiding" ){ spawn = new WaterBeetleRiding(); }
				else if ( monster == "WaterStrider" ){ spawn = new WaterStrider(); }
				else if ( monster == "OilSlick" ){ spawn = new OilSlick(); }
				else if ( monster == "FloatingEye" ){ spawn = new FloatingEye(); }
				else if ( monster == "SeaTroll" ){ spawn = new SeaTroll(); }
				else if ( monster == "WaterElemental" ){ spawn = new WaterElemental(); }
				else if ( monster == "GiantCrab" ){ spawn = new GiantCrab(); }
				else if ( monster == "GiantLamprey" ){ spawn = new GiantLamprey(); }
				else if ( monster == "Locathah" ){ spawn = new Locathah(); }

				Spawn( SpawnAt, map, spawn, onBoat );

				spawn.WhisperHue = 999; // SO TASK MANAGER DELETES THEM EVENTUALLY
				spawn.Combatant = from;
			}

			Delete();
		}
	}
}