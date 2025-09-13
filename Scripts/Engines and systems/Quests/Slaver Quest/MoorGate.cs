using System;
using Server;

namespace Server.Items
{
	public class MoorGate : Moongate
	{
		private bool m_Decays;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		public override int LabelNumber{ get{ return 1049498; } } // dark moongate

		[Constructable]
		public MoorGate( Point3D loc, Map map ) : this()
		{
			MoveToWorld( loc, map );
			Effects.PlaySound( loc, map, 0x20E );

		}

		[Constructable]
		public MoorGate() : base( new Point3D( 603, 709, -38 ), Map.Ilshenar )
		{
			Dispellable = false;
			ItemID = 0x1FD4;

		}

		public MoorGate( Serial serial ) : base( serial )
		{
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
