using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;

namespace Server.Items
{
	[Flipable( 0x2811, 0x2812 )]
	public class TrashChest : Container
	{
		public override int DefaultMaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight

		public override bool IsDecoContainer
		{
			get{ return false; }
		}

		[Constructable]
		public TrashChest() : base( 0x2811 )
		{
			Name = "donation box";
			Movable = false;
		}

		public TrashChest( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Empties Every 24 Hours");
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
			if ( Items.Count > 0 )
			{
				m_Timer = new EmptyTimer( this );
				m_Timer.Start();
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{

			if (dropped is TombStone || dropped is Corpse || dropped is CorpseItem)
				return false;
				
			if ( !base.OnDragDrop( from, dropped ) )
				return false;

			if ( !base.TryDropItem( from, dropped, false ) )
				return false;

			if ( m_Timer != null )
				m_Timer.Stop();
			else
				m_Timer = new EmptyTimer( this );

			m_Timer.Start();

			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{

			if (item is TombStone || item is Corpse || item is CorpseItem)
				return false;

			if ( !base.OnDragDropInto( from, item, p ) )
				return false;

			if ( !base.TryDropItem( from, item, false ) )
				return false;

			if ( m_Timer != null )
				m_Timer.Stop();
			else
				m_Timer = new EmptyTimer( this );

			m_Timer.Start();

			return true;
		}

		public void Empty( int message )
		{
			List<Item> items = this.Items;

			if ( items.Count > 0 )
			{
				PublicOverheadMessage( Network.MessageType.Regular, 0x3B2, message, "" );

				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;

					items[i].Delete();
				}
			}

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private Timer m_Timer;

		private class EmptyTimer : Timer
		{
			private TrashChest m_Chest;

			public EmptyTimer( TrashChest chest ) : base( TimeSpan.FromHours( 24.0 ) )
			{
				m_Chest = chest;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Chest.Empty( 501479 ); // Emptying the trashcan!
			}
		}
	}
}