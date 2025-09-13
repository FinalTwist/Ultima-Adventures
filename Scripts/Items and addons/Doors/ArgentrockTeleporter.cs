using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class ArgentrockTeleporter : Item
	{
		[Constructable]
		public ArgentrockTeleporter() : base(0x4228)
		{
			Movable = false;
			Visible = false;
			Name = "maze teleporter";
		}

		public ArgentrockTeleporter(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			Point3D p = new Point3D(6270, 647, 0);

			switch ( Utility.RandomMinMax( 0, 11 ) )
			{
				case 1:		p = new Point3D(5808, 786, -22);	break; // ENTER A
				case 2:		p = new Point3D(5887, 856, -22);	break; // ENTER B1
				case 3:		p = new Point3D(5807, 937, -22);	break; // ENTER B2
				case 4:		p = new Point3D(5808, 786, -22);	break; // ENTER A
				case 5:		p = new Point3D(5887, 856, -22);	break; // ENTER B1
				case 6:		p = new Point3D(5807, 937, -22);	break; // ENTER B2
				case 7:		p = new Point3D(5808, 786, -22);	break; // ENTER A
				case 8:		p = new Point3D(5887, 856, -22);	break; // ENTER B1
				case 9:		p = new Point3D(5807, 937, -22);	break; // ENTER B2
				case 10:	p = new Point3D(5977, 486, 0);		break; // START
				case 11:	p = new Point3D(6270, 647, 0);		break; // FINISH
			}

			Server.Mobiles.BaseCreature.TeleportPets( m, p, this.Map );
			m.MoveToWorld( p, this.Map );
			Effects.PlaySound( m.Location, m.Map, 0x5C0 );

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