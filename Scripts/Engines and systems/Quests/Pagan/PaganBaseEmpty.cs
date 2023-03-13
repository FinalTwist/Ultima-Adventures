using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class PaganBaseEmpty : Item
	{
		public int ItemType;

		[CommandProperty(AccessLevel.Owner)]
		public int Item_Type { get { return ItemType; } set { ItemType = value; InvalidateProperties(); } }

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromMinutes( (double)(Utility.RandomMinMax( 7200, 15840 )) ); } }

		[Constructable]
		public PaganBaseEmpty() : base( 13042 )
		{
			Name = "Pedestal";
			Movable = false;

			RefreshDecay( true );
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckAddComponents ) );
		}

		public void CheckAddComponents()
		{
			if( Deleted )
				return;
		}

		public void RemovePedestal()
		{
			Item Pedul = new PaganBase( ItemType );
			Pedul.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
			this.Delete();
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( RemovePedestal ) );
		}

		public PaganBaseEmpty( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteDeltaTime( m_DecayTime );
            writer.Write( ItemType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
            ItemType = reader.ReadInt();
		}
	}
}