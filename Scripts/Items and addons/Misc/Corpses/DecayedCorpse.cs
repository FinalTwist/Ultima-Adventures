using System;
using Server;

namespace Server.Items
{
	public class DecayedCorpse : Container
	{
		private Timer m_DecayTimer;
		private DateTime m_DecayTime;

		private static TimeSpan m_DefaultDecayTime = TimeSpan.FromMinutes( 7.0 );

		public DecayedCorpse( string name ) : base( Utility.Random( 0xECA, 9 ) )
		{
			Movable = false;
			Name = name;
			GumpID = 0x2A73;
			DropSound = 0x48;

			BeginDecay( m_DefaultDecayTime );
		}

		public void BeginDecay( TimeSpan delay )
		{
			if ( m_DecayTimer != null )
				m_DecayTimer.Stop();

			m_DecayTime = DateTime.UtcNow + delay;

			m_DecayTimer = new InternalTimer( this, delay );
			m_DecayTimer.Start();
		}

		public override void OnAfterDelete()
		{
			if ( m_DecayTimer != null )
				m_DecayTimer.Stop();

			m_DecayTimer = null;
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{		

			if (dropped is TombStone || dropped is Corpse || dropped is CorpseItem)
				return false;
				
			return base.TryDropItem(from, dropped, sendFullMessage );
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if (item is TombStone || item is Corpse || item is CorpseItem)
				return false;
			
			return base.OnDragDropInto(from, item, p);
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{

			if (dropped is TombStone || dropped is Corpse || dropped is CorpseItem)
				return false;
			
			return base.OnDragDrop(from, dropped);
		}

		private class InternalTimer : Timer
		{
			private DecayedCorpse m_Corpse;

			public InternalTimer( DecayedCorpse c, TimeSpan delay ) : base( delay )
			{
				m_Corpse = c;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Corpse.Delete();
			}
		}

		// Do not display (x items, y stones)
		public override bool CheckContentDisplay( Mobile from )
		{
			return false;
		}

		// Do not display (x items, y stones)
		public override bool DisplaysContent{ get{ return false; } }

		public override void AddNameProperty( ObjectPropertyList list )
		{
			list.Add( 1046414, Name ); // the remains of ~1_NAME~
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, 1046414, Name ); // the remains of ~1_NAME~
		}

		public DecayedCorpse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_DecayTimer != null );

			if ( m_DecayTimer != null )
				writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					BeginDecay( m_DefaultDecayTime );

					break;
				}
				case 1:
				{
					if ( reader.ReadBool() )
						BeginDecay( reader.ReadDeltaTime() - DateTime.UtcNow );

					break;
				}
			}
			GumpID = 0x2A73;
			DropSound = 0x48;
		}
	}
}
