using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Mobiles 
{
	[CorpseName( "a familiar corpse" )] 
	public class HenchmanFamiliar : BaseCreature
	{
		private DateTime m_NextTalking;
		public DateTime NextTalking{ get{ return m_NextTalking; } set{ m_NextTalking = value; } }
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.UtcNow >= m_NextTalking && InRange( m, 20 ) )
			{
				this.Loyalty = 100;
				m_NextTalking = (DateTime.UtcNow + TimeSpan.FromSeconds( 300 ));
			}
		}

		[Constructable] 
		public HenchmanFamiliar( ) : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			m_NextTalking = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));

			Name = "a familiar";
			Body = 0x3A;
			ControlSlots = 5;
			Blessed = true;
			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;
			SetStr( 500 );
			Container pack = Backpack;

			if ( pack != null )
				pack.Delete();

			pack = new StrongBackpack();
			pack.Movable = false;

			AddItem( pack );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return false; } }
		public override bool InitialInnocent{ get{ return true; } }
		public override bool DeleteOnRelease{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override bool IsDispellable { get { return false; } }
		public override bool IsBondable{ get{ return false; } }
		public override bool CanBeRenamedBy( Mobile from ){ return true; }

		public HenchmanFamiliar( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); // version
			Loyalty = 100;
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();

			LeaveNowTimer thisTimer = new LeaveNowTimer( this ); 
			thisTimer.Start(); 
		} 

		public override bool IsSnoop( Mobile from )
		{
			return false;
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( PackAnimal.CheckAccess( this, from ) )
			{
				AddToBackpack( item );
				return true;
			}

			return base.OnDragDrop( from, item );
		}

		public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override bool CheckNonlocalLift( Mobile from, Item item )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			PackAnimal.TryPackOpen( this, from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			PackAnimal.GetContextMenuEntries( this, from, list );
		}
	}

	public class LeaveNowTimer : Timer 
	{ 
		private Mobile i_me;
		public LeaveNowTimer( Mobile me ) : base( TimeSpan.FromSeconds( 10.0 ) ) 
		{ 
			Priority = TimerPriority.OneSecond; 
			i_me = me; 
		} 

		protected override void OnTick() 
		{ 
			if (( i_me != null ) && ( !i_me.Deleted )) 
				((BaseCreature)i_me).AIObject.DoOrderRelease();
		} 
	} 
}