using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class NewPlayerTeleport : Item
	{
		[Constructable]
		public NewPlayerTeleport() : base(0x1B72)
		{
			Movable = false;
			Visible = false;
			Name = "chasm exit";
		}

		public NewPlayerTeleport(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				Mobile director = null;
				foreach ( Mobile mob in World.Mobiles.Values )
				{
					if ( mob is PlayDirectorNewChar )
					{
						director = mob;
					}
				}
				bool bus = false;
				if (director != null)
					bus = ((PlayDirectorNewChar)director).BusyCheck();
							
				if (!bus)
					m.MoveToWorld( new Point3D(1961, 1318, 0), Map.Malas );
				else 
				{
					m.SendMessage( "Something is blocking the light. You might need to wait a bit more.");
					PlayDirectorNewChar.CheckWaiting( m, true);
				}				

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