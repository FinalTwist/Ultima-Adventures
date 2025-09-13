using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	/// <summary>
	/// Lockpickable Door v1.0  
	/// Original script created b Carding. Modified by PitHelvit
	/// </summary>
	public class PickableDoor : DarkWoodDoor, ILockpickable
	{
		
		private int m_LockLevel, m_MaxLockLevel, m_RequiredSkill;
		private Mobile m_Picker;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Picker
		{
			get	{ return m_Picker; }
			set { m_Picker = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int LockLevel
		{
			get { return m_LockLevel; }
			set { m_LockLevel = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxLockLevel
		{
			get { return m_MaxLockLevel; }
			set { m_MaxLockLevel = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int RequiredSkill
		{
			get { return m_RequiredSkill; }
			set { m_RequiredSkill = value; }
		}
		
		public virtual void LockPick( Mobile from )
		{
			Picker = from;
			Locked = false;
            m_Unlocked = DateTime.UtcNow;		
		}
		
		[Constructable]
		public PickableDoor( DoorFacing facing ) : base( facing )
		{
// default values		
			m_LockLevel = 80;
			m_MaxLockLevel = 110; 
			m_RequiredSkill = 100;
		}

		private DateTime m_Unlocked;

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public DateTime Unlocked
		{
			get { return m_Unlocked; }
			set { m_Unlocked = value; }
		}

		private string m_Message = null;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Message
		{
			get{ return m_Message; }
			set{ m_Message = value; }
		}
// determines how long the door remains unlocked
		private TimeSpan m_RelockTime = TimeSpan.FromMinutes( 5.0 );

		[ CommandProperty( AccessLevel.GameMaster ) ]
		public TimeSpan RelockTime
		{
			get { return m_RelockTime; }
			set { m_RelockTime = value; }
		}

		public override void Use( Mobile from )
		{
			if ( DateTime.UtcNow > m_Unlocked + m_RelockTime )
			{
				Locked = true;
				from.SendMessage( "This door is locked." );
				return;
			}
			if ( m_Message != null && m_Message.Length > 0 )
				from.SendMessage( m_Message );
			
			base.Use( from );
		}

		public PickableDoor( Serial serial ) : base( serial )
		{
			
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 );
			
			writer.Write( m_Unlocked );
			writer.Write( m_RelockTime );
			writer.Write( m_Message );
			writer.Write( (int) m_RequiredSkill );
			writer.Write( (int) m_MaxLockLevel );
			writer.Write( (int) m_LockLevel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			m_Unlocked = reader.ReadDateTime();
			m_RelockTime = reader.ReadTimeSpan();
			m_Message = reader.ReadString();
			m_RequiredSkill = reader.ReadInt();
			m_MaxLockLevel = reader.ReadInt();
			m_LockLevel = reader.ReadInt();
		}
	}
}
