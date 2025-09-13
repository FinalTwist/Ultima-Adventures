using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;

namespace Server.Items
{
	[Flipable(0x2128, 0x2145)]
	public class MagicMirror : Item
	{
		public int MirrorDestination;
		[CommandProperty(AccessLevel.Owner)]
		public int Mirror_Destination { get { return MirrorDestination; } set { MirrorDestination = value; InvalidateProperties(); } }

		public int MirrorX;
		[CommandProperty(AccessLevel.Owner)]
		public int Mirror_X { get { return MirrorX; } set { MirrorX = value; InvalidateProperties(); } }

		public int MirrorY;
		[CommandProperty(AccessLevel.Owner)]
		public int Mirror_Y { get { return MirrorY; } set { MirrorY = value; InvalidateProperties(); } }

		public int MirrorZ;
		[CommandProperty(AccessLevel.Owner)]
		public int Mirror_Z { get { return MirrorZ; } set { MirrorZ = value; InvalidateProperties(); } }

		public Map MirrorMap;
		[CommandProperty(AccessLevel.Owner)]
		public Map Mirror_Map { get { return MirrorMap; } set { MirrorMap = value; InvalidateProperties(); } }

		[Constructable]
		public MagicMirror() : base( 0x2128 )
		{
			Name = "mirror";
			Weight = 1.0;
			Visible = false;
			Movable = false;
			NameMirror( this );
			Light = LightType.Circle150;
		}

		public MagicMirror( Serial serial ) : base( serial )
		{
		}

		public static void NameMirror( Item mirror )
		{
			string word = "magic";
			switch( Utility.RandomMinMax( 0, 20 ) )
			{
				case 0: word = "exotic"; 			break;
				case 1: word = "mysterious"; 		break;
				case 2: word = "enchanted"; 		break;
				case 3: word = "marvelous"; 		break;
				case 4: word = "amazing"; 			break;
				case 5: word = "astonishing"; 		break;
				case 6: word = "mystical"; 			break;
				case 7: word = "astounding"; 		break;
				case 8: word = "magical"; 			break;
				case 9: word = "divine"; 			break;
				case 10: word = "excellent"; 		break;
				case 11: word = "magnificent"; 		break;
				case 12: word = "phenomenal"; 		break;
				case 13: word = "fantastic"; 		break;
				case 14: word = "incredible"; 		break;
				case 15: word = "extraordinary"; 	break;
				case 16: word = "fabulous"; 		break;
				case 17: word = "ornate"; 			break;
				case 18: word = "wonderful"; 		break;
				case 19: word = "magic"; 			break;
				case 20: word = "unusual"; 			break;
			}

			mirror.Name = word + " mirror";
		}

		public static void SetMirrors()
		{
			int MirrorTrammel = 0;
			int MirrorFelucca = 0;
			int MirrorMalas = 0;
			int MirrorTerMur = 0;
			int MirrorTokuno = 0;

			ArrayList mirrors = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is MagicMirror )
			{
				if ( item != null )
				{
					mirrors.Add( item );
				}
			}
			for ( int i = 0; i < mirrors.Count; ++i )
			{
				Item item = ( Item )mirrors[ i ];

				MagicMirror mirror = (MagicMirror)item;
				item.Visible = false;
				Server.Items.MagicMirror.NameMirror( item );
				mirror.MirrorDestination = Utility.RandomList( 0, 0, 1 );

				string world = Worlds.GetMyWorld( item.Map, item.Location, item.X, item.Y );
				Point3D p = Worlds.GetRandomLocation( world, "land" );
				Map map = Worlds.GetMyDefaultMap( world );
					mirror.MirrorX = p.X;
					mirror.MirrorY = p.Y;
					mirror.MirrorZ = p.Z;
					mirror.MirrorMap = map;

				if ( Utility.RandomMinMax( 1, 5 ) == 1 )
				{
					if ( world == "the Isles of Dread" && MirrorTokuno < 2 )
					{
						MirrorTokuno++;
						item.Visible = true;
					}
					else if ( world == "the Savaged Empire" && MirrorTerMur < 2 )
					{
						MirrorTerMur++;
						item.Visible = true;
					}
					else if ( world == "the Land of Lodoria" && MirrorFelucca < 2 )
					{
						MirrorFelucca++;
						item.Visible = true;
					}
					else if ( world == "the Serpent Island" && MirrorMalas < 2 )
					{
						MirrorMalas++;
						item.Visible = true;
					}
					else if ( MirrorTrammel < 2 )
					{
						MirrorTrammel++;
						item.Visible = true;
					}
				}
			}

			ArrayList mirrorz = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is MagicMirror )
			{
				if ( item != null && item.Visible == false )
				{
					mirrorz.Add( item );
				}
			}
			for ( int i = 0; i < mirrorz.Count; ++i )
			{
				Item item = ( Item )mirrorz[ i ];
				string world = Worlds.GetMyWorld( item.Map, item.Location, item.X, item.Y );
				MagicMirror mirror = (MagicMirror)item;

				if ( Utility.RandomMinMax( 1, 3 ) == 1 )
				{
					if ( world == "the Isles of Dread" && MirrorTokuno < 2 )
					{
						MirrorTokuno++;
						item.Visible = true;
					}
					else if ( world == "the Savaged Empire" && MirrorTerMur < 2 )
					{
						MirrorTerMur++;
						item.Visible = true;
					}
					else if ( world == "the Land of Lodoria" && MirrorFelucca < 2 )
					{
						MirrorFelucca++;
						item.Visible = true;
					}
					else if ( world == "the Serpent Island" && MirrorMalas < 2 )
					{
						MirrorMalas++;
						item.Visible = true;
					}
					else if ( MirrorTrammel < 2 )
					{
						MirrorTrammel++;
						item.Visible = true;
					}
				}
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoTeleport( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoTeleport( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public virtual void DoTeleport( Mobile m )
		{
			bool FoundSpot = false;
			Point3D p = new Point3D(this.X, (this.Y+1), this.Z);
				if ( ItemID == 0x2145 ){ p = new Point3D((this.X+1), this.Y, this.Z); }

			Map map = this.Map;

			if ( MirrorDestination < 1 )
			{
				foreach ( Item item in World.Items.Values )
				if ( item is MagicMirror && item != this && item.Visible && Worlds.GetMyWorld( item.Map, item.Location, item.X, item.Y ) == Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) )
				{
					p = new Point3D(item.X, (item.Y+1), item.Z);
						if ( item.ItemID == 0x2145 ){ p = new Point3D((item.X+1), item.Y, item.Z); }

					map = item.Map;
					FoundSpot = true;
				}
			}

			if ( MirrorX > 0 && MirrorY > 0 && ( MirrorDestination > 0 || !FoundSpot ) )
			{
				p = new Point3D( MirrorX, MirrorY, MirrorZ );
				map = MirrorMap;
				FoundSpot = true;
			}

			if ( FoundSpot )
			{
				Server.Mobiles.BaseCreature.TeleportPets( m, p, map );
				m.MoveToWorld( p, map );
				m.SendMessage( "The magical mirror draws you in and you end up elsewhere." );
				m.PlaySound( 0x5C0 );
			}
			else
			{
				m.SendMessage( "You see yourself staring back at you." );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( MirrorDestination );
            writer.Write( MirrorX );
            writer.Write( MirrorY );
            writer.Write( MirrorZ );
            writer.Write( MirrorMap );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            MirrorDestination = reader.ReadInt();
            MirrorX = reader.ReadInt();
            MirrorY = reader.ReadInt();
            MirrorZ = reader.ReadInt();
            MirrorMap = reader.ReadMap();
		}
	}
}