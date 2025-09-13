using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.SkillHandlers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server.Targeting;
using Server.Prompts;
using Server.ContextMenus;

namespace Server.Mobiles 
{ 
	public class SS_InnKeeper : BaseVendor 
	{ 
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		private static Hashtable s_Keywords;
		enum SS_InnKeeperCommands { None = 0, Room, Help, }	//1.8 Renamed Commands to SS_InnKeeperCommands to account for possible confusion.	
		
		static SS_InnKeeper()
		{
			string [] keyWords = { " ", "room", "help", };

			s_Keywords = new Hashtable( keyWords.Length, StringComparer.OrdinalIgnoreCase );

			for ( int i = 0; i < keyWords.Length; ++i )
				s_Keywords[keyWords[i]] = i;
		}
		
		[Constructable]
		public SS_InnKeeper() : base( "the innkeeper" ) 
		{ 
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBSS_InnKeeper() ); 
		} 

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Sandals : VendorShoeType.Shoes; }
		}

		public SS_InnKeeper( Serial serial ) : base( serial ) 
		{ 
		} 
		
		//Begin Montegro Edits
		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			if ( InRange( from, 7 ) )
				return true;

			return base.HandlesOnSpeech (from);
		}
		
		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;
			Container bank = from.FindBankNoCreate();

			object command = SS_InnKeeperCommands.None;
			
			if ( e.Speech.Length > "vendor ".Length && e.Speech.Substring( 0, "vendor ".Length ).ToLower() == "vendor " )
				command = s_Keywords[ e.Speech.Substring( "vendor ".Length ) ];
			else if ( e.Speech.Length > Name.Length + 1 && e.Speech.Substring( 0, Name.Length + 1 ).ToLower() == Name.ToLower() + ' ' )
				command = s_Keywords[ e.Speech.Substring( Name.Length + 1 ) ];
			else if ( e.Speech.Length > "keeper ".Length && e.Speech.Substring( 0, "keeper ".Length ).ToLower() == "keeper " )
				command = s_Keywords[ e.Speech.Substring( "keeper ".Length ) ];
			else if ( e.Speech.Length > "innkeeper ".Length && e.Speech.Substring( 0, "innkeeper ".Length ).ToLower() == "innkeeper " )
				command = s_Keywords[ e.Speech.Substring( "innkeeper ".Length ) ];
			
			switch ( (command == null ? (int)SS_InnKeeperCommands.None : (int)command) )
			{
				case (int)SS_InnKeeperCommands.Room:
				{
                        if ((from.Backpack == null || from.Backpack.GetAmount(typeof(Gold)) < 300) && (bank == null || Banker.GetBalance(from) < 300))
					{
						SayTo( from, "Thou dost not have enough gold, not even in thy bank account." ); // Thou dost not have enough gold, not even in thy bank account.
					}
					else
					{
						SayTo( from, "I will board your squire for now, this three hundred will cover their initial room fees." );
						SayTo( from, "Should they stay here an extended period of time, they will work to pay for themselves." );
						
						from.Target = new RoomTarget( this );
					}
					break;
				}
				case (int)SS_InnKeeperCommands.Help:
				{
					SayTo( from, "For a fee I can rent a room to your squire, they will work if additional fees occur, and I will hand you a Carrier Pigeon to call them back." );
					break;
				}
				default:
				{
					base.OnSpeech( e );
					break;
				}
			}
		}
		
		private class RoomTarget : Target
		{
			private SS_InnKeeper m_InnKeeper;

			public RoomTarget( SS_InnKeeper innkeeper ) : base( 12, false, TargetFlags.None )
			{
				m_InnKeeper = innkeeper;
			}

			protected override void OnTarget( Mobile from, object s )
			{
				Container bank = from.FindBankNoCreate();
				
				if ( s == from )
				{
					m_InnKeeper.Say( "Haha! Sorry! I thought we were rooming someone else... You may sleep here for free." );
				}
				else if ( s is PlayerMobile )
				{
					m_InnKeeper.Say( "Haha! Sorry! I think the sir or madam may speak for themselves." );
				}
				else if ( !(s is Squire) )
				{
					m_InnKeeper.Say( "That... Doesn't look like a squire to me." );
				}
				else
				{
					BaseCreature squi = ( BaseCreature )s;
					
					if ( squi.Combatant != null && squi.InRange( squi.Combatant, 12 ) && squi.Map == squi.Combatant.Map )
					{
						m_InnKeeper.Say( "Your squire seems to be distracted..." );
					}
					else if ( !( squi.Controlled && squi.ControlMaster == from ) )
					{
						m_InnKeeper.Say( "That is not your squire to room." );
					}
					//Uncomment this section if you don't want them to have items in their backpacks before storing them.
					/*else if ( squi is Squire && squi.Backpack != null && squi.Backpack.Items.Count > 0 )
					{
						m_InnKeeper.Say( "Please unload your squire before sending them to a room." );
					}*/
					else if ( squi.Mounted == true )
					{
						m_InnKeeper.Say( "This is not a stable, please have your squire dismount before I rent them a room." );
					}
					else if ( squi.Summoned )
					{
						m_InnKeeper.Say( "How did you summon a squire with magic?" );
					}
					else if ( !squi.Alive )
					{
						m_InnKeeper.Say( "I do not provide rooms for ghosts." );
					}
                    else if (squi is Squire && squi.Controlled == true && from == squi.ControlMaster && from.Backpack.ConsumeTotal(typeof(Gold), 300) || Banker.Withdraw(from, 300))
					{
						RoomFunctions.Room( from, squi, true );
					}
					else
					{
						m_InnKeeper.Say( "I will not room... That." ); // You can't stable that!
					}
				}
			}
		}
		
		private class RoomEntry : ContextMenuEntry
		{
			private SS_InnKeeper m_Keeper;
			private Mobile m_From;

			public RoomEntry( SS_InnKeeper innkeeper, Mobile from ) : base( 560, 12 )
			{
				m_Keeper = innkeeper;
				m_From = from;
			}

			public override void OnClick()
			{
				m_From.Target = new RoomTarget( m_Keeper );
			}
		}
		
		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive )
			{
				list.Add( new RoomEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}
		//End Montegro Edit

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
	
	//Begin Montegro Edit
	public class RoomFunctions 
	{
		public static bool Room( Mobile from, object squi, bool restricted )
		{
			Squire s = ( Squire )squi;
			
			CarrierPigeon carrierPigeon = new CarrierPigeon( s );

			if ( from != null )
			{
				from.SendMessage( "Your squire marches over to their inn room." );
				if ( !from.AddToBackpack ( carrierPigeon ) )
				{
					carrierPigeon.MoveToWorld( new Point3D( from.X, from.Y, from.Z ), from.Map );
					from.SendMessage( "The carrier pigeon falls to the ground, as your backpack is too full." );
				}
			}
			else
			{
				carrierPigeon.MoveToWorld( new Point3D( s.X, s.Y, s.Z ), s.Map );
			}
			
			s.Controlled = true;
				
			GoToRoom( s );

			return true;
		}

		public static bool Room( Mobile from, object squi )
		{	
			return Room( from, squi, true );
		}

		public static bool Room( object squi )
		{
			return Room( null, squi, false );
		}

		private static void GoToRoom( Squire squire )
		{
			squire.SetControlMaster( null );
			squire.SummonMaster = null;
			squire.Internalize();
			squire.Controlled = true;
		}
	}
	//End Montegro Edit
}