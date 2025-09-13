using System;
using Server;

namespace UltimaLive.LumberHarvest
{
	public class FadingLeaf : Item
	{
		[Constructable]
		public FadingLeaf() : this( Utility.RandomList( 0x1B1F, 0x1B20, 0x1B21, 0x1B22, 0x1B23, 0x1B24, 0x1B25 ))
		{
		}

		[Constructable]
		public FadingLeaf( int itemID ) : base( itemID )
		{
			Movable = false;
      Hue = 1436;
			new InternalTimer( this ).Start();
		}

		public FadingLeaf( Serial serial ) : base( serial )
		{
			new InternalTimer( this ).Start();
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

		private class InternalTimer : Timer
		{
			private Item m_Leaf;

			public InternalTimer( Item leaf ) : base( TimeSpan.FromSeconds( 5.0 ) )
			{
				Priority = TimerPriority.OneSecond;

				m_Leaf = leaf;
			}

			protected override void OnTick()
			{
				m_Leaf.Delete();
			}
		}
	}
}
