using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class TeleportTile : Item
	{
		[Constructable]
		public TeleportTile() : base(0x4228)
		{
			Movable = false;
			Visible = false;
			Name = "teleport";
		}

		public TeleportTile(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			string world = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );
			Point3D p = Worlds.GetRandomLocation( world, "land" );
			Map map = Worlds.GetMyDefaultMap( world );

			if ( p != Point3D.Zero && m is PlayerMobile )
			{
				Server.Mobiles.BaseCreature.TeleportPets( m, p, map );
				m.MoveToWorld( p, map );
				Effects.PlaySound( m.Location, m.Map, 0x1FC );
				m.LocalOverheadMessage(MessageType.Emote, 1150, true, "You " + this.Name + ", teleporting you away from here!");
				LoggingFunctions.LogVoid( m, this.Name );
			}

			return false;
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