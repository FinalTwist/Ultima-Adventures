
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Commands;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "Lady Luck's Corpse" )]
	public class LadyLuck : Mobile
	{
		public static void Initialize() 
		{ 
		//Server.Commands.Register( "Lottery", AccessLevel.Player, new CommandEventHandler( Lottery_OnCommand ) ); 
			CommandSystem.Register( "Lottery", AccessLevel.Player, new CommandEventHandler( Lottery_OnCommand ) ); 
		}    
      
		[Usage( "Lottery" )] 
		[Description( "Check if the next drawing need to be hold and anounce the next drawing if it isn't due yet." )] 

		public static void Lottery_OnCommand( CommandEventArgs e ) 
		{ 
			if ( DateTime.UtcNow >= dt_NextDrawing )
				DrawingTime();
			
			if (e.Mobile.AccessLevel >= AccessLevel.GameMaster)
				World.Broadcast( 66, true, "Current reward for drawing number {0} that will take place at {1} stands at {2} gold.", i_Drawing, dt_NextDrawing, i_Reward );
			else
				e.Mobile.SendMessage(32, "Current reward for drawing number {0} that will take place at {1} stands at {2} gold.", i_Drawing, dt_NextDrawing, i_Reward );
			
			if ( i_Drawing > 0 && i_Winner > 0 )
			{
				if (s_WinnerName == "Not claimed")
					e.Mobile.SendMessage(1073, "Ticket number {0} won {1} gold in drawing number {2}.", i_Winner, i_WinnerReward, (i_Drawing - 1));
				else
					e.Mobile.SendMessage(1073, "{0} won {1} gold at drawing number {2}.", s_WinnerName, i_WinnerReward, (i_Drawing - 1));
			}
		}    

		private static bool bEnableLottery = true;
		[CommandProperty(AccessLevel.Administrator)]
		public static bool EnableLottery { get { return bEnableLottery; } set { bEnableLottery = value; } }
		
		//private static bool bEnableExchange = true;
		//[CommandProperty(AccessLevel.Administrator)]
		//public static bool EnableExchange { get { return bEnableExchange; } set { bEnableExchange = value; } }

		//private static int iTokenCostInGold = 500;
		//[CommandProperty(AccessLevel.Administrator)]
		//public static int TokenCostInGold { get { return iTokenCostInGold; } set { iTokenCostInGold = value; } }

		private static DateTime dt_NextDrawing;

		[CommandProperty(AccessLevel.Administrator)]
		public DateTime NextDrawing { get { return dt_NextDrawing; } set { dt_NextDrawing = value; InvalidateProperties(); } }

		private static string s_WinnerName = "Not claimed";

		[CommandProperty(AccessLevel.GameMaster)]
		public string WinnerName { get { return s_WinnerName; } }
		//public string WinnerName { get { return s_WinnerName; } set { s_WinnerName = value; InvalidateProperties(); } }

		private static int i_Drawing, i_Ticket = 1, i_Winner = 0, i_TicketCost = 1000, i_DrawingDelay = 168, i_WinnerReward = 0;
		private static int i_Reward = Utility.RandomMinMax(500000, 5000000);
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int Drawing { get { return i_Drawing; } }
		//public int Drawing { get { return i_Drawing; } set { i_Drawing = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Reward { get { return i_Reward; } }
		//public int Reward { get { return i_Reward; } set { i_Reward = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.Administrator)]
		public int TicketCost { get { return i_TicketCost; } set { i_TicketCost = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Administrator)]
		public int Ticket { get { return i_Ticket; } set { i_Ticket = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int Winner { get { return i_Winner; } }
		//public int Winner { get { return i_Winner; } set { i_Winner = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Administrator)]
		public int DrawingDelay { get { return i_DrawingDelay; } set { i_DrawingDelay = value; InvalidateProperties(); } }
		
		[CommandProperty(AccessLevel.Administrator)]
		public int WinnerReward { get { return i_WinnerReward; } }
		//public int WinnerReward { get { return i_WinnerReward; } set { i_WinnerReward = value; InvalidateProperties(); } }
		
		[Constructable]
		public LadyLuck()
		{
			Name = "Lucky Evelyn";
			Title = "the lottery ticket vendor";
			Body = 0x191;
			Frozen = true;
			CantWalk = true;
			Hue = 0x83F8;
			Direction = Direction.East;

			AddItem( new Server.Items.Sandals(2213) );
			AddItem( new Server.Items.FloppyHat( 2213 ) );
			AddItem( new Server.Items.FancyDress( 2213 ) );
			
			AddItem( new LongHair( 1153 ) );
			Blessed = true;

			if (i_Drawing == 0)
			{
				dt_NextDrawing = DateTime.UtcNow + TimeSpan.FromHours( i_DrawingDelay );
				i_Drawing = 1;
			}
		}
		
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 5 ) && !InRange( oldLocation, 5 ) )
			{
				if ( m is PlayerMobile && !m.Hidden ) 
				{
                           		switch (Utility.Random(1))
                           		{
                                		case 0: Say("Step Riiight up! Lottery takes place weekly!  You might be the lucky winner!"); break;
                        	     	 	case 1: Say("Try your luck in the weekly lottery! [lottery for details!"); break;
                         	 	}
				
				}
			}

		}
		
      public override bool HandlesOnSpeech( Mobile from ) 
      { 
         return true; 
      } 

      public override void OnSpeech( SpeechEventArgs e ) 
      {
      	if( e.Mobile.InRange( this, 5 ))
      	{
	    if ( ( e.Speech.ToLower() == "buy" ) )//was sellpet
	    {
				if( !( e.Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile from = (PlayerMobile) e.Mobile;
				from.CloseGump( typeof( LadyLuckSellingGump ) ); 
				from.SendGump( new LadyLuckSellingGump( from ) );
	    }
	    else 
	    { 
		base.OnSpeech( e ); 
	    }
      	}
      
      } 

		public LadyLuck( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			if (bEnableLottery)
				list.Add( new SellTicketEntry( from ) );
				//list.Add( new SellTicketEntry( from ) ); 
			//if (bEnableExchange)
			//	list.Add( new ExchangeEntry( from ) );
		} 

		public virtual bool IsInvulnerable{ get{ return true; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 );

			//version 2
			writer.Write( (bool) bEnableLottery );
		//	writer.Write( (bool) bEnableExchange );
		//	writer.Write( (int) iTokenCostInGold );
			//version 1
			writer.Write( (int) i_WinnerReward );
			writer.Write( (string) s_WinnerName );
			//version 0
			writer.Write( (int) i_Drawing );
			writer.Write( (int) i_Reward );
			writer.Write( (int) i_Ticket );
			writer.Write( (int) i_Winner );
			writer.Write( (int) i_TicketCost );
			writer.Write( (DateTime)dt_NextDrawing );

			if (i_Reward == 0)
				i_Reward = Utility.RandomMinMax(5000, 1000000);

			CheckDrawingTime();
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch (version)
			{
				case 2:
				{
					bEnableLottery = reader.ReadBool();
			//		bEnableExchange = reader.ReadBool();
			//		iTokenCostInGold = reader.ReadInt();
					goto case 1;
				}
				case 1:
				{
					i_WinnerReward = reader.ReadInt();
					s_WinnerName = reader.ReadString();
					goto case 0;
				}
				case 0:
				{
					i_Drawing = reader.ReadInt();
					i_Reward = reader.ReadInt();
					i_Ticket = reader.ReadInt();
					i_Winner = reader.ReadInt();
					i_TicketCost = reader.ReadInt();
					dt_NextDrawing = reader.ReadDateTime();
					break;
				}
			}
			Frozen = true;
			CheckDrawingTime();
		}

		public static void CheckDrawingTime()
		{
			if ( DateTime.UtcNow >= dt_NextDrawing )
				DrawingTime();
		}
		
		public static void DrawingTime()
		{
			if (i_Ticket == 1) //nobody bought a ticket
			{
				dt_NextDrawing = DateTime.UtcNow + TimeSpan.FromHours( i_DrawingDelay );
				World.Broadcast( 38, true, "Since nobody bought any ticket for drawing {0} the drawing will continue.", i_Drawing);
			}
			else
			{
//+++
				s_WinnerName = "Not claimed";
				
				// calculate odds based on amount being drawn
				
				double mixit = Utility.RandomDouble();
				if ( mixit > 0.90 ) mixit = 0.90;
				if ( mixit < 0.50 ) mixit = 0.50;
				
				double oddsdouble = ( (i_Reward / (i_TicketCost*4)) * mixit ) + i_Ticket;
				int odds = Convert.ToInt32(oddsdouble);
				
				i_Winner = Utility.RandomMinMax(1, odds);

				i_WinnerReward = i_Reward;
				dt_NextDrawing = DateTime.UtcNow + TimeSpan.FromHours( i_DrawingDelay );
				World.Broadcast( 38, true, "And the lucky ticket that won {0} gold for drawing number {1} is {2}.", i_WinnerReward, i_Drawing, i_Winner);				
				i_Reward = Utility.RandomMinMax(500000, 5000000);
				i_Drawing++;
				i_Ticket = 1;
				World.Broadcast( 89, true, "If a player has the winning ticket, hand it in to Lady Luck!" );
				World.Broadcast( 66, true, "The next drawing will take place at {0}.", dt_NextDrawing );
			}
		}

		public class SellTicketEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Player;
			
			public SellTicketEntry( Mobile from ) : base( 6103, 3 )
			{
				m_Mobile = from;
			}

			public override void OnClick()
			{
				if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile from = (PlayerMobile) m_Mobile;
				from.CloseGump( typeof( LadyLuckSellingGump ) ); 
				from.SendGump( new LadyLuckSellingGump( from ) );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
			CheckDrawingTime();
			Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is LotteryTicket )
				{
					LotteryTicket ticket = dropped as LotteryTicket;
					if ( ((LotteryTicket)ticket).DrawingNumber == ( i_Drawing - 1 ) ) //last drawing
					{
						if ( ((LotteryTicket)ticket).StartTicketNumber <= i_Winner && i_Winner <= ((LotteryTicket)ticket).EndTicketNumber) //winner
						{
							switch (Utility.Random(3))
							{
								case 0: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I am very Pleased to Inform you that you have won!!!", mobile.NetState ); break;
								case 1: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Congratulations!!! You have won!!!", mobile.NetState ); break;
								case 2: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "YAY! We have a winner!", mobile.NetState ); break;
							}
							LoggingFunctions.LogWin( m, i_WinnerReward );
							m.AddToBackpack(new BankCheck(i_WinnerReward));
							m.AddToBackpack(new FireworksWand());
							if (m.Name == null)
								s_WinnerName = "John Doe";
							else
								s_WinnerName = m.Name;
							World.Broadcast( 88, true, "{0} claimed {1} gold from drawing {2},  Congratulations.", s_WinnerName, i_WinnerReward, (i_Drawing - 1) );
							World.Broadcast( 66, true, "Drawing number {0} will take place at {1}.", i_Drawing, dt_NextDrawing );
							new Celebration( m.Map, m ).Start();
						}
						else //looser
						{
							LoggingFunctions.LogLose( m, i_WinnerReward );
							switch (Utility.Random(3))
							{
								case 0: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Sorry, Luck is not with you today.", mobile.NetState ); break;
								case 1: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "You know what they say... You big looser!", mobile.NetState ); break;
								case 2: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Perhaps next time!", mobile.NetState ); break;
							}
						}
						dropped.Delete();
						return true;
					}
					else if ( ((LotteryTicket)ticket).DrawingNumber == i_Drawing ) //drowing isn't over
					{
						switch (Utility.Random(3))
						{
							case 0: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I am sorry, the drawing is not yet closed.", mobile.NetState ); break;
							case 1: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "This drawing is not over yet.", mobile.NetState ); break;
							case 2: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Please come back at the end of drawing to see if you have won.", mobile.NetState ); break;
						}
					}
					else //old drawings
					{
						switch (Utility.Random(3))
						{
							case 0: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I am very sorry, That ticket has expired.", mobile.NetState ); break;
							case 1: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That Ticket is no longer any good.", mobile.NetState ); break;
							case 2: this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Do not try to fool me With outdated Tickets!!!", mobile.NetState ); break;
						}
						dropped.Delete();
					}
					return false;
				}
				else
				{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I have no need of this!", mobile.NetState );
				}
			}
			return false;
		}

		private class Celebration : Timer
		{
			private Map m_Map;
			private int m_Count;
			Mobile m_From;

			public Celebration( Map map, Mobile from ) : base( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( 1 ) )
			{
				m_Map = map;
				m_Count = 30;
				m_From = from;
			}

			protected override void OnTick()
			{
				if (m_Count > 0)
				{
					Point3D ourLoc = m_From.Location;
					Point3D startLoc = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
					Point3D endLoc = new Point3D( startLoc.X + Utility.RandomMinMax( -2, 2 ), startLoc.Y + Utility.RandomMinMax( -2, 2 ), startLoc.Z + 20 );
					Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc, m_Map ), new Entity( Serial.Zero, endLoc, m_Map ), 0x36E4, 5, 0, false, false );
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishCelebration ), new object[]{ m_From, endLoc, m_Map } );
					m_Count--;
				}
				else Stop();
			}
		}
		private static void FinishCelebration( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			int hue = Utility.Random( 40 );

			if ( hue < 8 )
				hue = 0x66D;
			else if ( hue < 10 )
				hue = 0x482;
			else if ( hue < 12 )
				hue = 0x47E;
			else if ( hue < 16 )
				hue = 0x480;
			else if ( hue < 20 )
				hue = 0x47F;
			else
				hue = 0;

			if ( Utility.RandomBool() )
				hue = Utility.RandomList( 0x47E, 0x47F, 0x480, 0x482, 0x66D );

			int renderMode = Utility.RandomList( 0, 2, 3, 4, 5, 7 );

			Effects.PlaySound( endLoc, map, Utility.Random( 0x11B, 4 ) );
			Effects.SendLocationEffect( endLoc, map, 0x373A + (0x10 * Utility.Random( 4 )), 16, 10, hue, renderMode );
		}

		public class LadyLuckSellingGump : Gump
		{
			private Mobile m_From;
			private int i_BuyTickets;
			
			public LadyLuckSellingGump( Mobile from ) : base(0, 0)
			{
				m_From = from;
				if (!(m_From is PlayerMobile))
					return;
				
				Resizable=false;
				AddPage(0);
				AddImage(5, 5, 1140);
				AddLabel(80, 65, 2213, @"Each lottery ticket costs " + i_TicketCost + " gold.");
				AddLabel(88, 90, 89, @"How many tickets you want to buy ?");
				AddLabel(80, 130, 67, @"Buy:");
				AddLabel(215, 130, 67, @"tickets.");
				AddTextEntry(120, 132, 80, 20, 2113, 1, "1");
				AddButton(275, 130, 2644, 2645, 2, GumpButtonType.Reply, 0);
			}
			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				if ( m_From.Deleted ) 
					return; 
				if ( info.ButtonID == 2 )
				{
					TextRelay tr_BuyTickets = info.GetTextEntry( 1 );
					if(tr_BuyTickets != null)
					{
						int i_BuyTickets = 0;
						try
						{
							i_BuyTickets = Convert.ToInt32(tr_BuyTickets.Text,10);
						} 
						catch
						{
							m_From.SendMessage("Please make sure you wrote only numbers.");
							m_From.SendGump( new LadyLuckSellingGump( m_From ) );
						}
						if ( i_BuyTickets <= 0 ) 
							return;
						else if ( i_BuyTickets >= 1000000 )
						{
							m_From.SendMessage(32, "you can't buy more then 999,999 tickets at the same time");
							return;
						}
						else 
						{
//+++
							int i_Bank;
							int i_Total;
							i_Bank = Banker.GetBalance( m_From );
							i_Total = i_BuyTickets * i_TicketCost;
							
							Container bank = m_From.FindBankNoCreate();
							if ( ( m_From.Backpack != null && m_From.Backpack.ConsumeTotal( typeof( Gold ), i_Total ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), i_Total ) ) )
							//if ( i_Bank > i_Total )
							{
								
			
								LoggingFunctions.LogLottery( m_From, i_Total );
								//m_From.BankBox.ConsumeTotal(typeof(Gold), i_Total);
								m_From.SendLocalizedMessage( 1060398, i_Total.ToString() ); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
								//m_From.SendLocalizedMessage( 1060022, Banker.GetBalance( m_From ).ToString() ); // You have ~1_AMOUNT~ gold in cash remaining in your bank box.
												
								LotteryTicket lottery = new LotteryTicket();
								lottery.DrawingNumber = i_Drawing;
								lottery.StartTicketNumber = i_Ticket;
								lottery.EndTicketNumber = (i_Ticket + i_BuyTickets - 1);
								m_From.AddToBackpack( lottery );
												
								i_Ticket = (i_Ticket + i_BuyTickets);
								m_From.CloseGump( typeof( LadyLuckSellingGump ) );
								m_From.SendMessage("You bought {0} lottery tickets.", i_BuyTickets);
								i_Reward = ( i_Reward + ( i_BuyTickets * (i_TicketCost/3) ) );
							}
							else
							{
								m_From.PlaySound(1069); //play Hey!! sound
								m_From.SendMessage("You don't have enough Money!!!!");
								m_From.SendMessage("Please make sure you have enough gold (coins) in your bank.");
								m_From.SendGump( new LadyLuckSellingGump( m_From ) );
							}
							
							return;
						}
					}
				}
			}
		}
	}

	public class LotteryTicket : Item
	{
		private int i_DrawingNumber, i_StartTicketNumber, i_EndTicketNumber;
		
		//[CommandProperty(AccessLevel.Administrator)]
		//[CommandProperty(AccessLevel.Administrator)]
		public int DrawingNumber { get { return i_DrawingNumber; } set { i_DrawingNumber = value; InvalidateProperties(); } }
		
		//[CommandProperty(AccessLevel.Administrator)]
		//[CommandProperty(AccessLevel.Administrator)]
		public int StartTicketNumber { get { return i_StartTicketNumber; } set { i_StartTicketNumber = value; InvalidateProperties(); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//[CommandProperty(AccessLevel.Administrator)]
		public int EndTicketNumber { get { return i_EndTicketNumber; } set { i_EndTicketNumber = value; InvalidateProperties(); } }
		
		public LotteryTicket() : base(5359)
		{
			Name = "Lottery Ticket";
			Hue = 3;
			Weight = 1.0;
			Stackable = false;
			//LootType = LootType.Blessed;
		}

		public LotteryTicket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			//version 0
			writer.Write( i_DrawingNumber );
			writer.Write( i_StartTicketNumber );
			writer.Write( i_EndTicketNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			//version 0
			i_DrawingNumber = reader.ReadInt();
			i_StartTicketNumber = reader.ReadInt();
			i_EndTicketNumber = reader.ReadInt();
			
			LootType = LootType.Blessed;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			if ( i_StartTicketNumber == i_EndTicketNumber )
				list.Add( 1060663, "Drawing\t{0}, Ticket: {1}", i_DrawingNumber, i_StartTicketNumber );
			else
				list.Add( 1060659, "Drawing\t{0}, Tickets: {1} through {2}", i_DrawingNumber, i_StartTicketNumber, i_EndTicketNumber );
		}
	}
}
