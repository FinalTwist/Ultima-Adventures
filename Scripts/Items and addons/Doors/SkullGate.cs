using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class SkullGate : Item
	{
		[Constructable]
		public SkullGate() : base(0x4228)
		{
			Movable = false;
			Visible = false;
			Name = "skull gate";
		}

		public SkullGate(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( !(m is PlayerMobile) )
				return false;
			
			Point3D coord = new Point3D( 6730, 3315, 0 );
			Map map = Map.Felucca;

			if ( this.Map == Map.Felucca )
			{
				coord = new Point3D( 2672, 3235, 0 );
				map = Map.Trammel;
			}

			if ( CharacterDatabase.GetKeys( m, "SkullGate" ) )
			{
				BaseCreature.TeleportPets( m, coord, map, false );
				m.MoveToWorld ( coord, map );
				m.PlaySound( 0x658 );
				return false;
			}
			else
			{
				Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x36B0, 1, 14, 63, 7, 9915, 0 );
				Effects.PlaySound( m.Location, m.Map, 0x229 );
				m.ApplyPoison( m, Poison.Lethal );
				m.SendMessage( "Not knowing the secret of the skull gate, you suffer the effects." );
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