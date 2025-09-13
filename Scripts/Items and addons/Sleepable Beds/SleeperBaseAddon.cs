// Script Package: Sleepable Beds
// Version: 1.0
// Author: Oak
// Servers: RunUO 2.0
// Date: 7/7/2006
// History: 
//  Written for RunUO 1.0 shard, Sylvan Dreams,  in February 2005. Based largely on work by David on his Sleepable NPCs scripts.
//  Modified for RunUO 2.0, removed shard specific customizations (wing layers, etc.)
//  Deedable code by Zardoz. 7/8/2006
//  Modified to version 1 serialization by Eni/Shanira, changes include:
//   - Option to change the message hue (MessageHue variable)
//   - New option for non lethal awakening attempts, but can still use old style (ShockTreatment variable)
//   - Option to let anyone use any bed anywhere, or only friends/(co-)owners in houses (HouseOnly variable)
//   - Several changes to the sleep/awakening code:
//      - Freezes the character, so you can't cast spells while sleeping. To prevent exploits you can only
//        use the bed while not frozen
//      - Range check for using the bed so you can't port across the room onto the bed.
//      - Blesses the character and returns them to prior bless status, this is to prevent someone attacking
//        a sleeping player, who will be frozen and defenseless.
//      - Changed body value to 0, making the character completely invisible, this is done so people can't
//        unhide the supposedly gone sleeping player with spells or skills. Returns to normal afterwards.
//        Done also for player immersion.
//      - Made a check to prevent polymorphed characters from sleeping. This is done because of the invisibility
//        which cannot be accomplished by editing BodyMod, and thus polymorphed bed use could create undesirable
//        results.
//      - Made bed use move the character onto the bed when used, so the camera moves as well, for immersion.
//  Version 2 by Eni/Shanira, changes include:
//   - Put reused bed code in SleeperBaseAddon.cs class
//   - Added more serialization code to ensure that old style sleepers will load and convert to the new system
//     seamlessly. Make sure to check the Serialization comments if you have custom serialization code.
//   - The various component classes still exist for backwards compatibility but behaviour is handled by the
//     base SleeperPiece class. They don't serialize (as this would cause load errors)
//   - Added an OnDelete method that punts whoever's on the bed if it's chopped or otherwise removed.
//   - Used new hair system so sleeping people now have hair.
//   - Added a variable to SleeperBedBody.cs; AlwaysSleep. True is old style always sleep, false is clickable
//     corpse to sleep/unsleep while on the bed


using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Multis;

namespace Server.Items
{
	// version 1.1.1 Bed coordinates of 0,0,0 will cause npc to sleep and wake at it's current location.
	// version 1.0 initial release.
	public class SleeperBaseAddon: BaseAddon, IChopable
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return null;
			}
		}
	
		public SleeperBaseAddon( Serial serial ) : base( serial )
		{
		}
	
		public SleeperBaseAddon()
		{
			Visible = false;
			Name = "Sleeper";
		}
		
		protected bool OldStyleSleepers;
		
		private SleeperBaseAddon m_Sleeper;
		
		private SleeperBedBody m_SleeperBedBody;
		private bool m_Active = false;
		private Mobile m_Player;
		private Point3D m_Location;
		private bool m_Sleeping = false;
		private bool m_Debug = false;
		private Mobile m_Owner;
		
		// Eni's stuff
		private Point3D m_LastLocation;
		private int m_LastBody;
		private bool m_WasBlessed;
		private InternalTimer m_Timer;
		
		// Configuration stuff
		// You can obviously set these individually for subclasses if you don't want them to use global
		// configuration.
		protected bool ShockTreatment = false; // set to true for old style zapping on wake-up tries
		protected int MessageHue = 951; // change to 0x33 for old style color messages
		protected bool HouseOnly = false; // false means anyone can use it, true means friends & (co-)owners in a house
		protected bool AlwaysSleep = false; // true is old style always sleep, false is clickable corpse to sleep/unsleep while on the bed
		
		//wtry is the "wake try" counter. After two attempts to wake someone else up, you get zapped
		private int wtry;
	
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Bed
		{
			get{ return m_Location; }
			set{ m_Location = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Debug
		{
			get{ return m_Debug; }
			set{ m_Debug = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get{ return m_Active; }
			set{ m_Active = value; InvalidateProperties(); }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Asleep
		{
			get{ return m_Sleeping; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Mobile
		{
			get{ return m_Player; }
			set
			{ 
				if( value == null )
				m_Active = false;
				else
				m_Active = true;
				
				m_Player = value; 
				
				InvalidateProperties();
			}
		}
		
		public SleeperBedBody SleeperBedBody { get { return m_SleeperBedBody; } }		
		
		[CommandProperty( AccessLevel.GameMaster )]
		public SleeperBaseAddon Sleeper
		{
			get{ return m_Sleeper; }
			set{}
		}
		
		private void Sleep()
		{
			if( m_Sleeping ) return;
		}
		
		public override void OnDelete()
		{
			try {
				if ( m_Sleeping && m_Player != null )
					UseBed(m_Player, new Point3D(0,0,0), Direction.North);
			}
			finally
			{
				m_Sleeping = false;
			}	
		}
		
		public new virtual void OnDoubleClick( Mobile from )
		{
			UseBed(from, new Point3D(0,0,0), Direction.North);
		}

		public virtual void UseBed( Mobile from, Point3D sleepLocation, Direction sleepDirection ) 
		{ 
			if (from == null)
				return;
		
			// For if player is logged off
			bool LoggedOff = false;
			Map PlayerMap = from.Map;
			if ( m_Sleeping && m_Owner == from && from.Map != this.Map ) {
				LoggedOff = true;
			}
			
			// Eni stuff below
			if ( !m_Sleeping ) {
				if (!from.CanSee( this ) || !from.InLOS( this ) || !from.InRange( this, 3 ) )
				{
					from.SendLocalizedMessage( 1019045 );
					return;
				}
			}
			// End of Eni stuff
			
			if (from.Mounted && !m_Sleeping )
			{
				from.LocalOverheadMessage( MessageType.Regular, MessageHue, 1010097 ); // You cannot use this while mounted.
				return;
			}

			if(from.CantWalk && !m_Sleeping)
			{
				from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You are already sleeping somewhere!" );
			}
			else
			{
				if( !m_Sleeping ) 
				{	
					// Eni, cannot use a bed if you're frozen in place.
					// Here and not up there because otherwise you can't get off again!
					if (from.Frozen) {
						from.SendLocalizedMessage( 500111 ); // you are frozen and cannot move
						return;
					}
					
					BaseHouse m_house = BaseHouse.FindHouseAt( from );
					BaseHouse this_house = BaseHouse.FindHouseAt ( this );
					if (m_house!= null && (m_house != this_house && !this_house.IsFriend(from) ))
					{
						from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You can not sleep in someone elses bed! Get a bed of your own." );
						return;
					}
					
					// To make choosable for house checks
					bool HouseChecks;

					if ( HouseOnly ) {
						HouseChecks = ( m_house != null && (m_house.IsOwner(from) || m_house.IsCoOwner(from) || m_house.IsFriend( from )));
					} else { HouseChecks = true; }
					
					// Eni addition at start of if to prevent polymorphed bed use conflicts
					if ( !from.IsBodyMod && HouseChecks )
					{
						wtry=0;
						m_Player = from;
						m_Owner = m_Player;
						m_Player.Hidden = true;
						m_Player.CantWalk = true;
						m_Sleeping = true;
			
						if (m_Player is PlayerMobile)
							((PlayerMobile)m_Player).SetFlag( PlayerFlag.InBed, true );
						
						m_SleeperBedBody = new SleeperBedBody( m_Player, false, AlwaysSleep, this );
						Point3D m_Location = sleepLocation;
						m_SleeperBedBody.Direction=sleepDirection;
						m_SleeperBedBody.MoveToWorld( m_Location, this.Map );
						
						// Eni - additions
						m_Player.Frozen = true; // freeze so spells can't be cast
						m_WasBlessed = m_Player.Blessed;
						m_Player.Blessed = true; // blessed so can't be killed while helpless

						// below changes the camera to the sleeping body position for realism.
						m_LastLocation = m_Player.Location;
						m_Player.MoveToWorld( m_Location, this.Map );

						m_LastBody = m_Player.Body;
						m_Player.BodyValue = 0; // set body to nothing, hiding can be unhidden by skills/spells
					}
					else
					{
						// Eni anti polymorph addition
						if ( from.IsBodyMod ) {
							from.LocalOverheadMessage( MessageType.Regular, MessageHue, 1061627 ); // can't do that while polymorphed
							return;
						} else {
							from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You must be in the house and be the owner, co-owner or friend of the house this bed is in to sleep in it." );
							return;
						}
					}
				}
				else 
				{
					if(m_Owner == from)
					{
						m_Player = from;
						m_Sleeping = false;
						m_Player.Hidden = false;
						m_Player.CantWalk = false;
						
						// Eni
						m_Player.Frozen = false;
						m_Player.Blessed = m_WasBlessed;
						m_Player.BodyValue = m_LastBody;
						
						if (m_Player is PlayerMobile)
							((PlayerMobile)m_Player).SetFlag( PlayerFlag.InBed, false );
						
						m_Player.MoveToWorld( m_LastLocation, this.Map );
						
						if (LoggedOff)
							m_Player.Map = PlayerMap;
						
						if( m_SleeperBedBody != null )
							m_SleeperBedBody.Delete();
						m_SleeperBedBody = null;
						switch( Utility.RandomMinMax( 1, 3 ) )
						{
						case 1:
							m_Player.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You wake up and feel rested and strong." );
							break;
						case 2:
							m_Player.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You spring out of bed, ready for another day!" );
							break;
						case 3:
							m_Player.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You fall out of bed and blearily reach for the coffee pot." );
							break;
						}
					}
					else
					{
						// Eni - if ShockTreatment is false do the non lethal messages, otherwise shock them using old method
						if ( !ShockTreatment )
						{
							switch( Utility.RandomMinMax( 1, 3 ) )
							{
							case 1:
								from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "Something tells you you shouldn't do that." );
								break;
							case 2: 
								from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You really shouldn't bother someone who's sleeping." );
								break;
							case 3:
								from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "Have you never heard the phrase; let sleeping dogs lie?" );
								break;
							}
						}
						else
						{
							switch (wtry)
							{
							case 0:
								from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "Shhh, don't wake them up. They really need their beauty rest!" );
								wtry=wtry+1;
								break;
							case 1: 
								from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You really should NOT bother someone that is sleeping. Bad things might happen." );
								wtry=wtry+1;
								break;
							case 2:
								from.LocalOverheadMessage( MessageType.Regular, MessageHue, true, "You were warned! Now leave them alone." );
								from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.Head );
								from.PlaySound( 0x208 );
								from.Hits=m_Player.Hits-40;
								break;
							}
						}
					}
				}
			}
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			string tmp = String.Format( "{0}: {1}", this.Name, ( m_Player != null ? m_Player.Name : "unassigned" ) );
			list.Add( tmp );
			
			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // new version by Eni, previously 0
			// version increase to 2 only signifies that this is a new style script world
			// if it's a version below 2 the serialization will try to load it as the old scripts
			// should work unless the old scripts were modified, if so editing the serialization
			// here to make new style a newer version (and add one's own serialization) will be
			// needed.
			
			writer.Write( (Item)m_SleeperBedBody );
			writer.Write( (Mobile)m_Player );
			writer.Write( m_Active );
			writer.Write( m_Location );
			writer.Write( m_Sleeping );
			writer.Write( (Mobile)m_Owner );
			
			// version 1, Eni's stuff
			writer.Write( m_LastLocation );
			writer.Write( m_LastBody );
			writer.Write( m_WasBlessed );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_SleeperBedBody = (SleeperBedBody)reader.ReadItem();
			m_Player = reader.ReadMobile();
			m_Active = reader.ReadBool();
			m_Location = reader.ReadPoint3D();
			m_Sleeping = reader.ReadBool();
			m_Owner = reader.ReadMobile();
			
			// Eni's stuff
			if (version >= 1) {
				m_LastLocation = reader.ReadPoint3D();
				m_LastBody = reader.ReadInt();
				m_WasBlessed = reader.ReadBool();
			}
			else // version 0, original script distro
			{ // to avoid errors in the wakeup routine
				if (m_Player != null) {
					m_LastLocation = m_Player.Location;
					m_LastBody = m_Player.Body;
					m_WasBlessed = m_Player.Blessed;
					if (m_Sleeping)
						m_Player.Frozen = true;
				} else {
					m_LastLocation = m_Location;
					m_LastBody = 401;
					m_WasBlessed = false;
				}
			}
			
			try {
				// just something to check if m_Player is actually a valid reference
				// in case someone silly deletes their char while on a bed
				bool testBless = m_Player.Blessed;
			} catch {
				m_Player = null;
				m_Sleeping = false;
			}
			
			// Backwards compatibility between old and new script, see comments in Serialize
			if (version >= 2)
				OldStyleSleepers = false;
			else
				OldStyleSleepers = true;
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Player;
			private Map m_Map;
		
			public InternalTimer( Mobile m, Map map ) : base( TimeSpan.FromSeconds(1) )
			{
				m_Player = m;
				m_Map = map;
			}

			protected override void OnTick() 
			{ 
				m_Player.Map = m_Map;
				Stop();
			} 
		}
	}
	
	public class SleeperPiece : AddonComponent
	{
		private SleeperBaseAddon m_Sleeper;
	
		public SleeperPiece( SleeperBaseAddon sleeper, int itemid ) : base( itemid )
		{
			m_Sleeper = sleeper;
		}
	
		public override void OnDoubleClick( Mobile from ) 
		{ 
			m_Sleeper.OnDoubleClick(from);
		}
	
		public SleeperPiece( Serial serial ) : base( serial )
		{
		}
	
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Item)m_Sleeper );
		}
	
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Sleeper = (SleeperBaseAddon)reader.ReadItem();
		}
	}
}
