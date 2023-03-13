using System;
using Server;

namespace Server.Items
{
	public class WidowGate : Moongate
	{
		private bool m_Decays;
		private DateTime m_DecayTime;
		private Timer m_Timer;

		public override int LabelNumber{ get{ return 1049498; } } // dark moongate

		[Constructable]
		public WidowGate( Point3D loc, Map map ) : this()
		{
			m_Decays = false;
			Effects.PlaySound( loc, map, 0x20E );
		}

		[Constructable]
		public WidowGate() : base( new Point3D( 1173, 1524, 0 ), Map.Ilshenar )
		{
			Dispellable = false;
			ItemID = 0x1FD4;

		}

		public WidowGate( Serial serial ) : base( serial )
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
