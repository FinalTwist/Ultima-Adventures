using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class SerpentPillars : Item
	{
		[Constructable]
		public SerpentPillars() : base(0x4228)
		{
			Movable = false;
			Visible = false;
			Name = "serpent pillar";
		}

		public SerpentPillars(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Point3D coord = new Point3D( 3485, 2452, 0 );
			Map map = Map.Felucca;

			if ( this.Map == Map.Felucca )
			{
				coord = new Point3D( 4333, 2316, 0 );
				map = Map.Trammel;
			}

			if ( m != null && m is PlayerMobile && CharacterDatabase.GetKeys( m, "SerpentPillars" ))
			{
				BaseCreature.TeleportPets( m, coord, map, false );
				m.MoveToWorld ( coord, map );
				m.PlaySound( 0x658 );
				return false;
			}
			else if (m != null && m is PlayerMobile && m.Map != null)
			{
				Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z+10 );
				Effects.SendLocationEffect( blast, m.Map, 0x2A4E, 30, 10, 0, 0 );
				m.PlaySound( 0x029 );
				m.Hits = 1;
				m.SendMessage( "Not knowing the secret of the Serpent Pillars, you suffer Poseidon's wrath." );
			}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
