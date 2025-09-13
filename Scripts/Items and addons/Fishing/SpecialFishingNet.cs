using System;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class SpecialFishingNet : Item
	{
		private bool m_InUse;

		[Constructable]
		public SpecialFishingNet() : base( 0x1EA3 )
		{
			Name = "strong fishing net";
			Weight = 5.0;
			ItemID = Utility.RandomList( 0x1EA3, 0x1EA4 );
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

		public SpecialFishingNet( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Use This On The High Seas");
			list.Add( 1049644, "Requires 60 Fishing");
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
			else if ( from.Skills[SkillName.Fishing].Value < 60.0 )
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
			Server.Engines.Harvest.Fishing.FishingSkill( from, 11 );

			int count = Utility.RandomMinMax( 1, 3 );
			int onBoat = 0;
			string monster = "";
			Point3D SpawnAt = p;
			switch ( Utility.Random( 17 ) )
			{
				case 0: monster = "SeaHag"; SpawnAt = from.Location; onBoat = 1; break;
				case 1: monster = "WaterWeird"; break;
				case 2: monster = "SeaweedElemental"; break;
				case 3: monster = "Kraken"; break;
				case 4: monster = "GiantEel"; break;
				case 5: monster = "GiantSquid"; break;
				case 6: monster = "SeaSerpent"; break;
				case 7: monster = "SeaDrake"; break;
				case 8: monster = "EyeOfTheDeep"; break;
				case 9: monster = "Typhoon"; break;
				case 10: monster = "SteamElemental"; break;
				case 11: monster = "BoneSailor"; SpawnAt = from.Location; onBoat = 1; break;
				case 12: monster = "Trituns"; SpawnAt = from.Location; onBoat = 1; break;
				case 13: monster = "Shark"; break;
				case 14: monster = "GreatWhite"; break;
				case 15: monster = "Megalodon"; break;
				case 16: monster = "Calamari"; break;
			}

			for ( int i = 0; map != null && i < count; ++i )
			{
				BaseCreature spawn = new SeaHag();

				if ( monster == "SeaHag" ){ spawn = new SeaHag(); }
				else if ( monster == "WaterWeird" ){ spawn = new WaterWeird(); }
				else if ( monster == "SeaweedElemental" ){ spawn = new SeaweedElemental(); }
				else if ( monster == "Kraken" ){ spawn = new Kraken(); }
				else if ( monster == "GiantEel" ){ spawn = new GiantEel(); }
				else if ( monster == "GiantSquid" ){ spawn = new GiantSquid(); }
				else if ( monster == "SeaSerpent" ){ spawn = new SeaSerpent(); }
				else if ( monster == "SeaDrake" ){ spawn = new SeaDrake(); }
				else if ( monster == "EyeOfTheDeep" ){ spawn = new EyeOfTheDeep(); }
				else if ( monster == "Typhoon" ){ spawn = new Typhoon(); }
				else if ( monster == "SteamElemental" ){ spawn = new SteamElemental(); }
				else if ( monster == "BoneSailor" ){ spawn = new BoneSailor(); }
				else if ( monster == "Shark" ){ spawn = new Shark(); }
				else if ( monster == "GreatWhite" ){ spawn = new GreatWhite(); }
				else if ( monster == "Megalodon" ){ spawn = new Megalodon(); }
				else if ( monster == "Calamari" ){ spawn = new Calamari(); }
				else if ( monster == "Trituns" )
				{
					switch ( Utility.Random( 2 ) )
					{
						case 0: spawn = new TritunMage(); break;
						case 1: spawn = new Tritun(); break;
					} 
				}

				Spawn( SpawnAt, map, spawn, onBoat );

				spawn.WhisperHue = 999; // SO TASK MANAGER DELETES THEM EVENTUALLY
				spawn.Combatant = from;
			}

			Delete();
		}
	}
}