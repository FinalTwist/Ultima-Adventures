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
using Server.Multis;

namespace Server.Mobiles
{
	public class ChangeARumorMessagePrompt : Prompt
	{
		private RoomAttendant m_Roomattendant;
		private int m_RumorIndex;

		public ChangeARumorMessagePrompt( RoomAttendant roomattendant, int rumorIndex )
		{
			m_Roomattendant = roomattendant;
			m_RumorIndex = rumorIndex;
		}

		public override void OnCancel( Mobile from )
		{
			OnResponse( from, "" );
		}

		public override void OnResponse( Mobile from, string text )
		{
			if( text.Length > 130 )
				text = text.Substring( 0, 130 );

			m_Roomattendant.EndChangeRumor( from, m_RumorIndex, text );
		}
	}

	public class ChangeARumorKeywordPrompt : Prompt
	{
		private RoomAttendant m_Roomattendant;
		private int m_RumorIndex;

		public ChangeARumorKeywordPrompt( RoomAttendant roomattendant, int rumorIndex )
		{
			m_Roomattendant = roomattendant;
			m_RumorIndex = rumorIndex;
		}

		public override void OnCancel( Mobile from )
		{
			OnResponse( from, "" );
		}

		public override void OnResponse( Mobile from, string text )
		{
			if( text.Length > 130 )
				text = text.Substring( 0, 130 );

			m_Roomattendant.EndChangeKeyword( from, m_RumorIndex, text );
		}
	}

	public class ChangeATipMessagePrompt : Prompt
	{
		private RoomAttendant m_Roomattendant;

		public ChangeATipMessagePrompt( RoomAttendant roomattendant )
		{
			m_Roomattendant = roomattendant;
		}

		public override void OnCancel( Mobile from )
		{
			OnResponse( from, "" );
		}

		public override void OnResponse( Mobile from, string text )
		{
			if ( text.Length > 130 )
				text = text.Substring( 0, 130 );

			m_Roomattendant.EndChangeTip( from, text );
		}
	}

	public class RoomAttendantRumor
	{
		private string m_Message;
		private string m_Keyword;

		public string Message{ get{ return m_Message; } set{ m_Message = value; } }
		public string Keyword{ get{ return m_Keyword; } set{ m_Keyword = value; } }

		public RoomAttendantRumor( string message, string keyword )
		{
			m_Message = message;
			m_Keyword = keyword;
		}

		public static RoomAttendantRumor Deserialize( GenericReader reader )
		{
			if ( !reader.ReadBool() )
				return null;

			return new RoomAttendantRumor( reader.ReadString(), reader.ReadString() );
		}

		public static void Serialize( GenericWriter writer, RoomAttendantRumor rumor )
		{
			if ( rumor == null )
			{
				writer.Write( false );
			}
			else
			{
				writer.Write( true );
				writer.Write( rumor.m_Message );
				writer.Write( rumor.m_Keyword );
			}
		}
	}

	public class ManageRoomAttendantEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private RoomAttendant m_Roomattendant;

		public ManageRoomAttendantEntry( Mobile from, RoomAttendant roomattendant ) : base( 6151, 12 )
		{
			m_From = from;
			m_Roomattendant = roomattendant;
		}

		public override void OnClick()
		{
			m_Roomattendant.BeginManagement( m_From );
		}
	}

	public class RoomAttendant : BaseVendor
	{
		private Mobile m_Owner;
		private BaseHouse m_House;
		private string m_TipMessage;

		private RoomAttendantRumor[] m_Rumors;
		
		private static Hashtable s_Keywords;
		enum RoomAttendantCommands { None = 0, Room, Help, }		
		
		static RoomAttendant()
		{
			string [] keyWords = { " ", "room", "help", };

			s_Keywords = new Hashtable( keyWords.Length, StringComparer.OrdinalIgnoreCase );

			for ( int i = 0; i < keyWords.Length; ++i )
				s_Keywords[keyWords[i]] = i;
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		public BaseHouse House
		{
			get{ return m_House; }
			set
			{
				if ( m_House != null )
					m_House.PlayerBarkeepers.Remove( this );

				if ( value != null )
					value.PlayerBarkeepers.Add( this );

				m_House = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string TipMessage
		{
			get{ return m_TipMessage; }
			set{ m_TipMessage = value; }
		}

		public override bool IsActiveBuyer{ get{ return false; } }
        //public override bool IsActiveSeller{ get{ return ( m_SBInfos.Count > 0 ); } }

		public override bool DisallowAllMoves{ get{ return true; } }
		public override bool NoHouseRestrictions{ get{ return true; } }

		public RoomAttendantRumor[] Rumors{ get{ return m_Rumors; } }

		public override VendorShoeType ShoeType{ get{ return Utility.RandomBool() ? VendorShoeType.ThighBoots : VendorShoeType.Boots; } }

		public override bool GetGender()
		{
			return false; // always starts as male
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
			Container pack = this.Backpack;

			if ( pack != null )
				pack.Delete();
		}

		public override void InitBody()
		{
			base.InitBody();

			if ( BodyValue == 0x340 || BodyValue == 0x402 )
				Hue = 0;
			else
				Hue = 0x83F4; // hue is not random

			Container pack = this.Backpack;

			if ( pack != null )
				pack.Delete();
		}

		public RoomAttendant( Mobile owner, BaseHouse house ) : base( "the room attendant" )
		{
			Blessed = true;
			m_Owner = owner;
			House = house;
			m_Rumors = new RoomAttendantRumor[3];

			LoadSBInfo();
		}

		public override bool HandlesOnSpeech(Mobile from)
		{
			if ( InRange( from, 3 ) )
				return true;

			return base.HandlesOnSpeech (from);
		}

		private Timer m_NewsTimer;

		/*
        private void ShoutNews_Callback(object state)
		{
			object[] states = (object[])state;
			TownCrierEntry tce = (TownCrierEntry)states[0];
			
			int index = (int)states[1];

			if ( index < 0 || index >= tce.Lines.Length )
			{
				if ( m_NewsTimer != null )
					m_NewsTimer.Stop();

				m_NewsTimer = null;
			}
			else
			{
				PublicOverheadMessage( MessageType.Regular, 0x3B2, false, tce.Lines[index] );
				states[1] = index + 1;
			}
		}
		*/

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			House = null;
		}

		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() )
				return false;

			Item shoes = this.FindItemOnLayer( Layer.Shoes );

			if ( shoes is Sandals )
				shoes.Hue = 0;

			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;
			Container bank = from.FindBankNoCreate();

			object command = RoomAttendantCommands.None;
			
			if ( e.Speech.Length > "vendor ".Length && e.Speech.Substring( 0, "vendor ".Length ).ToLower() == "vendor " )
				command = s_Keywords[ e.Speech.Substring( "vendor ".Length ) ];
			else if ( e.Speech.Length > Name.Length + 1 && e.Speech.Substring( 0, Name.Length + 1 ).ToLower() == Name.ToLower() + ' ' )
				command = s_Keywords[ e.Speech.Substring( Name.Length + 1 ) ];
			else if ( e.Speech.Length > "attendant ".Length && e.Speech.Substring( 0, "attendant ".Length ).ToLower() == "attendant " )
				command = s_Keywords[ e.Speech.Substring( "attendant ".Length ) ];
			
			switch ( (command == null ? (int)RoomAttendantCommands.None : (int)command) )
			{
				case (int)RoomAttendantCommands.Room:
				{
                        if ((from.Backpack == null || from.Backpack.GetAmount(typeof(Gold)) < 300) && (bank == null || Banker.GetBalance(from) < 300))
					{
						SayTo( from, "Thou dost not have enough gold, not even in thy bank account." ); // Thou dost not have enough gold, not even in thy bank account.
					}
					else
					{
						SayTo( from, "I need three hundred gold for the carrier pigeon rental." );
						
						from.Target = new RoomTarget( this );
					}
					break;
				}
				case (int)RoomAttendantCommands.Help:
				{
					SayTo( from, "For a fee I can rent a carrier pigeon for you to call your squire with from their room." );
                        break;
                    }
				default:
				{
					base.OnSpeech( e );
					
					if ( !e.Handled && InRange( e.Mobile, 3 ) )
					{
					    /*
						if ( m_NewsTimer == null && e.HasKeyword( 0x30 ) ) // *news*
						{
							TownCrierEntry tce = GlobalTownCrierEntryList.Instance.GetRandomEntry();

							if ( tce == null )
							{
								PublicOverheadMessage( MessageType.Regular, 0x3B2, 1005643 ); // I have no news at this time.
							}
							else
							{
								m_NewsTimer = Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 3.0 ), new TimerStateCallback( ShoutNews_Callback ), new object[]{ tce, 0 } );

								PublicOverheadMessage( MessageType.Regular, 0x3B2, 502978 ); // Some of the latest news!
							}
						}
						*/

						for ( int i = 0; i < m_Rumors.Length; ++i )
						{
							RoomAttendantRumor rumor = m_Rumors[i];

							if ( rumor == null )
								continue;

							string keyword = rumor.Keyword;

							if ( keyword == null || (keyword = keyword.Trim()).Length == 0 )
								continue;

							if ( Insensitive.Equals( keyword, e.Speech ) )
							{
								string message = rumor.Message;

								if ( message == null || (message = message.Trim()).Length == 0 )
									continue;

								PublicOverheadMessage( MessageType.Regular, 0x3B2, false, message );
							}
						}
					}

					break;
				}
			}
		}

		public override bool CheckGold( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				Gold g = (Gold)dropped;

				if ( g.Amount > 50 )
				{
					PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "I cannot accept so large a tip!", from.NetState );
				}
				else
				{
					string tip = m_TipMessage;

					if ( tip == null || (tip = tip.Trim()).Length == 0 )
					{
						PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "It would not be fair of me to take your money and not offer you information in return.", from.NetState );
					}
					else
					{
						Direction = GetDirectionTo( from );
						PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, tip, from.NetState );

						g.Delete();
						return true;
					}
				}
			}

			return false;
		}

		public bool IsOwner( Mobile from )
		{
			if ( from == null || from.Deleted || this.Deleted )
				return false;

			if ( from.AccessLevel > AccessLevel.GameMaster )
				return true;

			return ( m_Owner == from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( IsOwner( from ) && from.InLOS( this ) )
				list.Add( new ManageRoomAttendantEntry( from, this ) );
		}

		public void BeginManagement( Mobile from )
		{
			if ( !IsOwner( from ) )
				return;

			from.SendGump( new RoomAttendantGump( from, this ) );
		}

		public void Dismiss()
		{
			Delete();
		}

		public void BeginChangeRumor( Mobile from, int index )
		{
			if ( index < 0 || index >= m_Rumors.Length )
				return;

			from.Prompt = new ChangeARumorMessagePrompt( this, index );
			PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "Say what news you would like me to tell our guests.", from.NetState );
		}

		public void EndChangeRumor( Mobile from, int index, string text )
		{
			if ( index < 0 || index >= m_Rumors.Length )
				return;

			if ( m_Rumors[index] == null )
				m_Rumors[index] = new RoomAttendantRumor( text, null );
			else
				m_Rumors[index].Message = text;

			from.Prompt = new ChangeARumorKeywordPrompt( this, index );
			PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "What keyword should a guest say to me to get this news?", from.NetState );
		}

		public void EndChangeKeyword( Mobile from, int index, string text )
		{
			if ( index < 0 || index >= m_Rumors.Length )
				return;

			if ( m_Rumors[index] == null )
				m_Rumors[index] = new RoomAttendantRumor( null, text );
			else
				m_Rumors[index].Keyword = text;

			PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "I'll pass on the message.", from.NetState );
		}

		public void RemoveRumor( Mobile from, int index )
		{
			if ( index < 0 || index >= m_Rumors.Length )
				return;

			m_Rumors[index] = null;
		}

		public void BeginChangeTip( Mobile from )
		{
			from.Prompt = new ChangeATipMessagePrompt( this );
			PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "Say what you want me to tell guests when they give me a good tip.", from.NetState );
		}

		public void EndChangeTip( Mobile from, string text )
		{
			m_TipMessage = text;
			PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "I'll say that to anyone who gives me a good tip.", from.NetState );
		}

		public void RemoveTip( Mobile from )
		{
			m_TipMessage = null;
		}

		public void BeginChangeAppearance( Mobile from )
		{
			from.CloseGump( typeof( PlayerVendorCustomizeGump ) );
			from.SendGump( new PlayerVendorCustomizeGump( this, from ) );
		}

		public void ChangeGender( Mobile from )
		{
			Female = !Female;

			if ( Female )
			{
				Body = 401;
				Name = NameList.RandomName( "female" );

				FacialHairItemID = 0;
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName( "male" );
			}
		}

		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		public override void InitSBInfo()
		{
			if ( Title == "the room attendant" )
			{
				if ( m_SBInfos.Count == 0 )
					m_SBInfos.Add( new SBRoomAttendant() );
			}
			else
			{
				m_SBInfos.Clear();
			}
		}

		public RoomAttendant( Serial serial ) : base( serial )
		{
		}
		
		private class RoomTarget : Target
		{
			private RoomAttendant m_RoomAttendant;

			public RoomTarget( RoomAttendant roomattendant ) : base( 12, false, TargetFlags.None )
			{
				m_RoomAttendant = roomattendant;
			}

			protected override void OnTarget( Mobile from, object s )
			{
				Container bank = from.FindBankNoCreate();
				
				if ( s == from )
				{
					m_RoomAttendant.Say( "Haha! Sorry! I thought we were rooming someone else... You may sleep here for free." );
				}
				else if ( s is PlayerMobile )
				{
					m_RoomAttendant.Say( "Haha! Sorry! I think the sir or madam may speak for themselves." );
				}
				else if ( !(s is Squire) )
				{
					m_RoomAttendant.Say( "That... Doesn't look like a squire to me." );
				}
				else
				{
					BaseCreature squi = ( BaseCreature )s;
					
					if ( squi.Combatant != null && squi.InRange( squi.Combatant, 12 ) && squi.Map == squi.Combatant.Map )
					{
						m_RoomAttendant.SayTo( from, "Your squire seems to be distracted..." );
					}
					else if ( !( squi.Controlled && squi.ControlMaster == from ) )
					{
						m_RoomAttendant.SayTo( from, "That is not your squire to room." );
					}
					//Uncomment this section if you don't want them to have items in their backpacks before storing them.
					/*else if ( squi is Squire && squi.Backpack != null && squi.Backpack.Items.Count > 0 )
					{
						m_RoomAttendant.SayTo( from, "Please unload your squire before sending them to a room." );
					}*/
					else if ( squi.Mounted == true )
					{
						m_RoomAttendant.SayTo( from, "This is not a stable, please have your squire dismount before I rent them a room." );
					}
					else if ( squi.Summoned )
					{
						m_RoomAttendant.SayTo( from, "How did you summon a squire with magic?" );
					}
					else if ( !squi.Alive )
					{
						m_RoomAttendant.SayTo( from, "I do not provide rooms for ghosts." );
					}
                    else if (squi is Squire && squi.Controlled == true && from == squi.ControlMaster && from.Backpack.ConsumeTotal(typeof(Gold), 300) || Banker.Withdraw(from, 300))
					{
						RoomAFunctions.Room( from, squi, true );
					}
					else
					{
						m_RoomAttendant.SayTo( from, "I will not room... That." ); // You can't stable that!
					}
				}
			}
		}
		
		private class RoomEntry : ContextMenuEntry
		{
			private RoomAttendant m_Attendant;
			private Mobile m_From;

			public RoomEntry( RoomAttendant roomattendant, Mobile from ) : base( 560, 12 )
			{
				m_Attendant = roomattendant;
				m_From = from;
			}

			public override void OnClick()
			{ // Fixed 1.9.4, uses the same gold check so that players don't get the wrong message when they do not have enough gold in their bank or inventory.
				Container bank = m_From.FindBankNoCreate();
                if ((m_From.Backpack == null || m_From.Backpack.GetAmount(typeof(Gold)) < 300) && (Banker.GetBalance(m_From) < 300))
				{
					m_Attendant.SayTo( m_From, "Thou dost not have enough gold, not even in thy bank account." ); // Thou dost not have enough gold, not even in thy bank account.
				}
				else
				{
					m_Attendant.SayTo( m_From, "I need three hundred gold for the carrier pigeon rental." );
						
					m_From.Target = new RoomTarget( m_Attendant );
				}
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

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version;

			writer.Write( (Item) m_House );

			writer.Write( (Mobile) m_Owner );

			writer.WriteEncodedInt( (int) m_Rumors.Length );

			for ( int i = 0; i < m_Rumors.Length; ++i )
				RoomAttendantRumor.Serialize( writer, m_Rumors[i] );

			writer.Write( (string) m_TipMessage );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					House = (BaseHouse) reader.ReadItem();

					goto case 0;
				}
				case 0:
				{
					m_Owner = reader.ReadMobile();

					m_Rumors = new RoomAttendantRumor[reader.ReadEncodedInt()];

					for ( int i = 0; i < m_Rumors.Length; ++i )
						m_Rumors[i] = RoomAttendantRumor.Deserialize( reader );

					m_TipMessage = reader.ReadString();

					break;
				}
			}

			if ( version < 1 )
				Timer.DelayCall( TimeSpan.Zero, new TimerCallback( UpgradeFromVersion0 ) );
		}

		private void UpgradeFromVersion0()
		{
			House = BaseHouse.FindHouseAt( this );
		}
	}

	public class RoomAttendantGump : Gump
	{
		private Mobile m_From;
		private RoomAttendant m_Roomattendant;

		public void RenderBackground()
		{
			AddPage( 0 );

			AddBackground( 30, 40, 585, 410, 5054 );

			AddImage( 30, 40, 9251 );
			AddImage( 180, 40, 9251 );
			AddImage( 30, 40, 9253 );
			AddImage( 30, 130, 9253 );
			AddImage( 598, 40, 9255 );
			AddImage( 598, 130, 9255 );
			AddImage( 30, 433, 9257 );
			AddImage( 180, 433, 9257 );
			AddImage( 30, 40, 9250 );
			AddImage( 598, 40, 9252 );
			AddImage( 598, 433, 9258 );
			AddImage( 30, 433, 9256 );

			AddItem( 30, 40, 6816 );
			AddItem( 30, 125, 6817 );
			AddItem( 30, 233, 6817 );
			AddItem( 30, 341, 6817 );
			AddItem( 580, 40, 6814 );
			AddItem( 588, 125, 6815 );
			AddItem( 588, 233, 6815 );
			AddItem( 588, 341, 6815 );

			AddBackground( 183, 25, 280, 30, 5054 );

			AddImage( 180, 25, 10460 );
			AddImage( 434, 25, 10460 );
			AddImage( 560, 20, 1417 );

			AddHtml( 223, 32, 200, 40, "ATTENDANT CUSTOMIZATION MENU", false, false );
			AddBackground( 243, 433, 150, 30, 5054 );

			AddImage( 240, 433, 10460 );
			AddImage( 375, 433, 10460 );
		}

		public void RenderCategories()
		{
			AddPage( 1 );

			AddButton( 130, 120, 4005, 4007, 0, GumpButtonType.Page, 2 );
			AddHtml( 170, 120, 200, 40, "Message Control", false, false );

			AddButton( 130, 200, 4005, 4007, 0, GumpButtonType.Page, 8 );
			AddHtml( 170, 200, 200, 40, "Customize your room attendant", false, false );

			AddButton( 130, 280, 4005, 4007, 0, GumpButtonType.Page, 3 );
			AddHtml( 170, 280, 200, 40, "Dismiss your room attendant", false, false );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Reply, 0 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 574, 43, 5360 );
		}

		public void RenderMessageManagement()
		{
			AddPage( 2 );

			AddButton( 130, 120, 4005, 4007, 0, GumpButtonType.Page, 4 );
			AddHtml( 170, 120, 380, 20, "Add or change a message and keyword", false, false );

			AddButton( 130, 200, 4005, 4007, 0, GumpButtonType.Page, 5 );
			AddHtml( 170, 200, 380, 20, "Remove a message and keyword from your room attendant", false, false );

			AddButton( 130, 280, 4005, 4007, 0, GumpButtonType.Page, 6 );
			AddHtml( 170, 280, 380, 20, "Add or change your room attendant's tip message", false, false );

			AddButton( 130, 360, 4005, 4007, 0, GumpButtonType.Page, 7 );
			AddHtml( 170, 360, 380, 20, "Delete your room attendant's tip message", false, false );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 1 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 580, 46, 4030 );
		}

		public void RenderDismissConfirmation()
		{
			AddPage( 3 );

			AddHtml( 170, 160, 380, 20, "Are you sure you want to dismiss your room attendant?", false, false );

			AddButton( 205, 280, 4005, 4007, GetButtonID( 0, 0 ), GumpButtonType.Reply, 0 );
			AddHtml( 240, 280, 100, 20,@"Yes", false, false );

			AddButton( 395, 280, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtml( 430, 280, 100, 20, "No", false, false );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 1 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 574, 43, 5360 );
			AddItem( 584, 34, 6579 );
		}

		public void RenderMessageManagement_Message_AddOrChange()
		{
			AddPage( 4 );

			AddHtml( 250, 60, 500, 25, "Add or change a message", false, false );

			RoomAttendantRumor[] rumors = m_Roomattendant.Rumors;

			for ( int i = 0; i < rumors.Length; ++i )
			{
				RoomAttendantRumor rumor = rumors[i];

				AddHtml( 100,  70 + (i * 120),  50, 20, "Message", false, false );
				AddHtml( 100,  90 + (i * 120), 450, 40, rumor == null ? "No current message" : rumor.Message, true, false );
				AddHtml( 100, 130 + (i * 120),  50, 20, "Keyword", false, false );
				AddHtml( 100, 150 + (i * 120), 450, 40, rumor == null ? "None" : rumor.Keyword, true, false );

				AddButton( 60, 90 + (i * 120), 4005, 4007, GetButtonID( 1, i ), GumpButtonType.Reply, 0 );
			}

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 2 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 580, 46, 4030 );
		}

		public void RenderMessageManagement_Message_Remove()
		{
			AddPage( 5 );

			AddHtml( 190, 60, 500, 25, "Choose the message you would like to remove", false, false );

			RoomAttendantRumor[] rumors = m_Roomattendant.Rumors;

			for ( int i = 0; i < rumors.Length; ++i )
			{
				RoomAttendantRumor rumor = rumors[i];

				AddHtml( 100,  70 + (i * 120),  50, 20, "Message", false, false );
				AddHtml( 100,  90 + (i * 120), 450, 40, rumor == null ? "No current message" : rumor.Message, true, false );
				AddHtml( 100, 130 + (i * 120),  50, 20, "Keyword", false, false );
				AddHtml( 100, 150 + (i * 120), 450, 40, rumor == null ? "None" : rumor.Keyword, true, false );

				AddButton( 60, 90 + (i * 120), 4005, 4007, GetButtonID( 2, i ), GumpButtonType.Reply, 0 );
			}

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 2 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 580, 46, 4030 );
		}

		private int GetButtonID( int type, int index )
		{
			return 1 + (index * 6) + type;
		}

		private void RenderMessageManagement_Tip_AddOrChange()
		{
			AddPage( 6 );

			AddHtml( 250, 95, 500, 20, "Change this tip message", false, false );
			AddHtml( 100, 190, 50, 20, "Message", false, false );
			AddHtml( 100, 210, 450, 40, m_Roomattendant.TipMessage == null ? "No current message" : m_Roomattendant.TipMessage, true, false );

			AddButton( 60, 210, 4005, 4007, GetButtonID( 3, 0 ), GumpButtonType.Reply, 0 );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 2 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 580, 46, 4030 );
		}

		private void RenderMessageManagement_Tip_Remove()
		{
			AddPage( 7 );

			AddHtml( 250, 95, 500, 20, "Remove this tip message", false, false );
			AddHtml( 100, 190, 50, 20, "Message", false, false );
			AddHtml( 100, 210, 450, 40, m_Roomattendant.TipMessage == null ? "No current message" : m_Roomattendant.TipMessage, true, false );

			AddButton( 60, 210, 4005, 4007, GetButtonID( 4, 0 ), GumpButtonType.Reply, 0 );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 2 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 580, 46, 4030 );
		}

		private void RenderAppearanceCategories()
		{
			AddPage( 8 );

			//AddButton( 130, 120, 4005, 4007, GetButtonID( 5, 0 ), GumpButtonType.Reply, 0 );
			//AddHtml( 170, 120, 120, 20, "Title", false, false );

            if (m_Roomattendant.BodyValue != 0x340 && m_Roomattendant.BodyValue != 0x402)
            {
				AddButton( 130, 200, 4005, 4007, GetButtonID( 5, 1 ), GumpButtonType.Reply, 0 );
				AddHtml( 170, 200, 120, 20, "Appearance", false, false );

				AddButton( 130, 280, 4005, 4007, GetButtonID( 5, 2 ), GumpButtonType.Reply, 0 );
				AddHtml( 170, 280, 120, 20, "Male / Female", false, false );

				AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 1 );
				AddHtml( 290, 440, 35, 40, "Back", false, false );
			}

			AddItem( 580, 44, 4033 );
		}

		public RoomAttendantGump( Mobile from, RoomAttendant roomattendant ) : base( 0, 0 )
		{
			m_From = from;
			m_Roomattendant = roomattendant;

			from.CloseGump( typeof( RoomAttendantGump ) );

			RenderBackground();
			RenderCategories();
			RenderMessageManagement();
			RenderDismissConfirmation();
			RenderMessageManagement_Message_AddOrChange();
			RenderMessageManagement_Message_Remove();
			RenderMessageManagement_Tip_AddOrChange();
			RenderMessageManagement_Tip_Remove();
			RenderAppearanceCategories();
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( !m_Roomattendant.IsOwner( m_From ) )
				return;

			int index = info.ButtonID - 1;

			if ( index < 0 )
				return;

			int type = index % 6;
			index /= 6;

			switch ( type )
			{
				case 0: // Controls
				{
					switch ( index )
					{
						case 0: // Dismiss
						{
							m_Roomattendant.Dismiss();
							break;
						}
					}

					break;
				}
				case 1: // Change message
				{
					m_Roomattendant.BeginChangeRumor( m_From, index );
					break;
				}
				case 2: // Remove message
				{
					m_Roomattendant.RemoveRumor( m_From, index );
					break;
				}
				case 3: // Change tip
				{
					m_Roomattendant.BeginChangeTip( m_From );
					break;
				}
				case 4: // Remove tip
				{
					m_Roomattendant.RemoveTip( m_From );
					break;
				}
				case 5: // Appearance category selection
				{
					switch ( index )
					{
						case 0: break;
						case 1: m_Roomattendant.BeginChangeAppearance( m_From ); break;
						case 2: m_Roomattendant.ChangeGender( m_From ); break;
					}

					break;
				}
			}
		}
	}
	
	public class RoomAFunctions 
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
}
