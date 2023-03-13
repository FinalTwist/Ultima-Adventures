using System;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	

	public class Elevator : Item
	{
	
		private int mZa;
		private Timer m_XPlus;
		private Timer m_XMinus;
		private Timer m_RecordLocationTimer;
	
		[CommandProperty(AccessLevel.GameMaster)]
		public int Za
		{
			get { return mZa; }
			set { mZa = value; }
		}
		
		

	
	
		[Constructable]
		public Elevator() : base( 1313 )
		{
			Weight = 100.0;
			Hue = 0;
			Name = "Elevator";
			
			this.m_RecordLocationTimer = new RecordLocationTimer (this);
			this.m_RecordLocationTimer.Start();
		}
		
		
		
		public Elevator( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write(mZa);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
			
			mZa = reader.ReadInt();
			
			this.m_XMinus = new XMinus (this);
			this.m_XMinus.Start();
		}
		
///////////////////////		
		private class RecordLocationTimer  : Timer
		{
			private Elevator m_Elevator;
			
		
			public RecordLocationTimer ( Elevator lift ) : base( TimeSpan.Zero )
			{
				m_Elevator = lift;
			}
			
			public void RecordLocation()
			{
				Point3D loc = m_Elevator.GetWorldLocation();
				m_Elevator.Za = loc.Z;
			}

			protected override void OnTick()
			{
				RecordLocation();
				Stop();
				
				m_Elevator.m_XPlus = new XPlus (m_Elevator);
				m_Elevator.m_XPlus.Start();
			}
		}
		
	///////////////////
		private class XPlus  : Timer
		{
			private Elevator m_Elevator;
			
		

			public XPlus ( Elevator lift ) : base( TimeSpan.FromSeconds( 0.5 ) )
			{
				Priority = TimerPriority.FiftyMS;

				m_Elevator = lift;
				
			}
	
			protected override void OnTick()
			{
				
				if( (m_Elevator.Za + 5) > m_Elevator.Z )
					{
						
						
						
						List<Mobile> list = new List<Mobile>();
						foreach( Mobile mob in m_Elevator.GetMobilesInRange( 0 ) ) 
						{
							if (mob is Mobile & mob.Alive)
							list.Add(mob);
						}
						
						foreach (Mobile mob in list)
						{
							mob.Z++;
						}
						m_Elevator.Z++;
						
						Start();
						
					}
				else
				{
					Stop();
					m_Elevator.m_XMinus = new XMinus (m_Elevator);
					m_Elevator.m_XMinus.Start();
				}
				
			}
		}
		
	///////////////////////////////
		private class XMinus  : Timer
		{
			private Elevator m_Elevator;

			public XMinus ( Elevator lift ) : base( TimeSpan.FromSeconds( 0.5 ) )
			{
				Priority = TimerPriority.FiftyMS;

				m_Elevator = lift;
			}
			
			

			protected override void OnTick()
			{
				
					if( m_Elevator.Z != m_Elevator.Za )
					{
						
						//0x3e5
						
						List<Mobile> list = new List<Mobile>();
						foreach( Mobile mob in m_Elevator.GetMobilesInRange( 0 ) ) 
						{
							if (mob is Mobile & mob.Alive)
							list.Add(mob);
						}
						
						foreach (Mobile mob in list)
						{
							mob.Z--;
						}
						m_Elevator.Z--;
						
						Start();
						
					}
					
				else
				{
					Stop();
					m_Elevator.m_XPlus = new XPlus (m_Elevator);
					m_Elevator.m_XPlus.Start();
				}
				
			}
		}	// end of XMinus Timer
		
	}	// end of class Elevator
}	// end of namespace



