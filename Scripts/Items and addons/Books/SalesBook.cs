using System;
using Server; 
using Server.Network;
using System.Collections;
using System.Globalization;
using Server.Items;
using Server.Misc;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class SalesBook : Item
	{
		public static SalesBook m_Book;

		[Constructable]
		public SalesBook() : base( 0x2254 )
		{
			Weight = 1.0;
			Movable = false;
			Hue = 0x515;
			Name = "Steel Crafted Items";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendSound( 0x55 );
			from.CloseGump( typeof( SalesBookGump ) );
			from.SendGump( new SalesBookGump( from, this, 0 ) );
		}

		public class SalesBookGump : Gump
		{
			public SalesBookGump( Mobile from, SalesBook wikipedia, int page ): base( 100, 100 )
			{
				m_Book = wikipedia;
				SalesBook pedia = (SalesBook)wikipedia;

				int NumberOfsellings = 0;	// SEE LISTING BELOW AND MAKE SURE IT MATCHES THE AMOUNT
											// DO THIS NUMBER+1 IN THE OnResponse SECTION BELOW

				string BookTitle = "";

				if ( m_Book.Name == "Steel Crafted Items" )
				{
					NumberOfsellings = 108;
					BookTitle = "Steel Crafted";
				}
				else if ( m_Book.Name == "Mithril Crafted Items" )
				{
					NumberOfsellings = 108;
					BookTitle = "Mithril Crafted";
				}
				else if ( m_Book.Name == "Brass Crafted Items" )
				{
					NumberOfsellings = 108;
					BookTitle = "Brass Crafted";
				}

				decimal PageCount = NumberOfsellings / 16;
				int TotalBookPages = ( 100000 ) + ( (int)Math.Ceiling( PageCount ) );

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				int subItem = page * 16;

				int showItem1 = subItem + 1;
				int showItem2 = subItem + 2;
				int showItem3 = subItem + 3;
				int showItem4 = subItem + 4;
				int showItem5 = subItem + 5;
				int showItem6 = subItem + 6;
				int showItem7 = subItem + 7;
				int showItem8 = subItem + 8;
				int showItem9 = subItem + 9;
				int showItem10 = subItem + 10;
				int showItem11 = subItem + 11;
				int showItem12 = subItem + 12;
				int showItem13 = subItem + 13;
				int showItem14 = subItem + 14;
				int showItem15 = subItem + 15;
				int showItem16 = subItem + 16;

				int page_prev = ( 100000 + page ) - 1;
					if ( page_prev < 100000 ){ page_prev = TotalBookPages; }
				int page_next = ( 100000 + page ) + 1;
					if ( page_next > TotalBookPages ){ page_next = 100000; }

				AddImage(40, 36, 1054);

				AddHtml( 162, 64, 200, 34, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>" + BookTitle + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 444, 64, 180, 34, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>" + BookTitle + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(93, 53, 1055, 1055, page_prev, GumpButtonType.Reply, 0);
				AddButton(625, 53, 1056, 1056, page_next, GumpButtonType.Reply, 0);

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 126, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem1, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem2, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem3, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem4, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem5, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem6, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem7, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem8, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetSalesForBook( m_Book.Name, showItem1, 1, from ) != "" ){ AddHtml( 328, 112, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem1, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem2, 1, from ) != "" ){ AddHtml( 328, 148, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem2, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem3, 1, from ) != "" ){ AddHtml( 328, 184, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem3, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem4, 1, from ) != "" ){ AddHtml( 328, 220, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem4, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem5, 1, from ) != "" ){ AddHtml( 328, 256, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem5, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem6, 1, from ) != "" ){ AddHtml( 328, 292, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem6, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem7, 1, from ) != "" ){ AddHtml( 328, 328, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem7, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem8, 1, from ) != "" ){ AddHtml( 328, 364, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem8, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }

				if ( GetSalesForBook( m_Book.Name, showItem1, 1, from ) != "" ){ AddButton(104, 115, 30008, 30008, showItem1, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem2, 1, from ) != "" ){ AddButton(104, 151, 30008, 30008, showItem2, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem3, 1, from ) != "" ){ AddButton(104, 187, 30008, 30008, showItem3, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem4, 1, from ) != "" ){ AddButton(104, 223, 30008, 30008, showItem4, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem5, 1, from ) != "" ){ AddButton(104, 259, 30008, 30008, showItem5, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem6, 1, from ) != "" ){ AddButton(104, 295, 30008, 30008, showItem6, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem7, 1, from ) != "" ){ AddButton(104, 331, 30008, 30008, showItem7, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem8, 1 , from) != "" ){ AddButton(104, 367, 30008, 30008, showItem8, GumpButtonType.Reply, 0); }

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 443, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem9, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem10, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem11, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem12, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem13, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem14, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem15, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem16, 1, from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetSalesForBook( m_Book.Name, showItem9, 1, from ) != "" ){ AddHtml( 645, 112, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem9, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem10, 1, from ) != "" ){ AddHtml( 645, 148, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem10, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem11, 1, from ) != "" ){ AddHtml( 645, 184, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem11, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem12, 1, from ) != "" ){ AddHtml( 645, 220, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem12, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem13, 1, from ) != "" ){ AddHtml( 645, 256, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem13, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem14, 1, from ) != "" ){ AddHtml( 645, 292, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem14, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem15, 1, from ) != "" ){ AddHtml( 645, 328, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem15, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }
				if ( GetSalesForBook( m_Book.Name, showItem16, 1, from ) != "" ){ AddHtml( 645, 364, 70, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetSalesForBook( m_Book.Name, showItem16, 3, from ) + "G</BIG></BASEFONT></BODY>", (bool)false, (bool)false); }

				if ( GetSalesForBook( m_Book.Name, showItem9, 1, from ) != "" ){ AddButton(421, 115, 30008, 30008, showItem9, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem10, 1, from ) != "" ){ AddButton(421, 151, 30008, 30008, showItem10, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem11, 1, from ) != "" ){ AddButton(421, 187, 30008, 30008, showItem11, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem12, 1, from ) != "" ){ AddButton(421, 223, 30008, 30008, showItem12, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem13, 1, from ) != "" ){ AddButton(421, 259, 30008, 30008, showItem13, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem14, 1, from ) != "" ){ AddButton(421, 295, 30008, 30008, showItem14, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem15, 1, from ) != "" ){ AddButton(421, 331, 30008, 30008, showItem15, GumpButtonType.Reply, 0); }
				if ( GetSalesForBook( m_Book.Name, showItem16, 1, from ) != "" ){ AddButton(421, 367, 30008, 30008, showItem16, GumpButtonType.Reply, 0); }
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 
				Container pack = from.Backpack;
				from.SendSound( 0x55 );
				int NumItemsPlusOne = 0;

				if ( m_Book.Name == "Steel Crafted Items" )
				{
					NumItemsPlusOne = 104;
				}
				else if ( m_Book.Name == "Mithril Crafted Items" )
				{
					NumItemsPlusOne = 104;
				}
				else if ( m_Book.Name == "Brass Crafted Items" )
				{
					NumItemsPlusOne = 104;
				}

				if ( info.ButtonID >= 100000 )
				{
					int page = info.ButtonID - 100000;
					from.SendGump( new SalesBookGump( from, m_Book, page ) );
				}
				else if ( info.ButtonID < NumItemsPlusOne )
				{
					string sType = GetSalesForBook( m_Book.Name, info.ButtonID, 2, from );
					string sName = GetSalesForBook( m_Book.Name, info.ButtonID, 1, from );
					int cost = Int32.Parse( GetSalesForBook( m_Book.Name, info.ButtonID, 3, from ) );
					string spentMessage = "You pay a total of " + cost.ToString() + " gold.";

					if ( Server.Mobiles.BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
					{
						cost = cost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * cost ); if ( cost < 1 ){ cost = 1; }
						spentMessage = "You only pay a total of " + cost.ToString() + " gold because of your begging.";
					}

					bool nearBook = false;
					foreach ( Item tome in from.GetItemsInRange( 10 ) )
					{
						if ( tome == m_Book ){ nearBook = true; }
					}

					if ( sName != "" && nearBook == true )
					{
						if ( from.TotalGold >= cost )
						{
							Item item = null;
							Type itemType = ScriptCompiler.FindTypeByName( sType );
							item = (Item)Activator.CreateInstance(itemType);

							pack.ConsumeTotal(typeof(Gold), cost);
							from.SendMessage( spentMessage );

							if ( m_Book.Name == "Steel Crafted Items" )
							{
								if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.Steel; }
								else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.Steel; }
							}
							else if ( m_Book.Name == "Mithril Crafted Items" )
							{
								if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.Mithril; }
								else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.Mithril; }
							}
							else if ( m_Book.Name == "Brass Crafted Items" )
							{
								if ( item is BaseWeapon ){ BaseWeapon weapon = (BaseWeapon)item; weapon.Resource = CraftResource.Brass; }
								else if ( item is BaseArmor ){ BaseArmor armor = (BaseArmor)item; armor.Resource = CraftResource.Brass; }
							}

							from.AddToBackpack ( item );
							if ( Server.Mobiles.BaseVendor.BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -Server.Mobiles.BaseVendor.BeggingKarma( from ), true ); } // DO ANY KARMA LOSS

							int OneSay = 0;

							foreach ( Mobile who in from.GetMobilesInRange( 10 ) )
							{
								if ( ( who is IronWorker || who is Weaponsmith || who is Armorer  || who is Blacksmith ) && OneSay == 0 && m_Book.Name == "Steel Crafted Items" )
								{
									who.PlaySound( 0x2A );
									
									switch( Utility.Random( 2 ) )
									{
										case 0: who.Say( "I have spent years learning the art of steel." ); 	break;
										case 1: who.Say( "Let me see what I can make here." );					break;
										case 2: who.Say( "People come from afar for orkish steel." ); 			break;
										case 3: who.Say( "You won't see many items like this." );				break;
										case 4: who.Say( "I think I can forge that for you." );					break;
										case 5: who.Say( "The fires are hot so I am ready to forge steel." );	break;
									}

									OneSay = 1;
								}
								else if ( ( who is IronWorker || who is Weaponsmith || who is Armorer  || who is Blacksmith ) && OneSay == 0 && m_Book.Name == "Mithril Crafted Items" )
								{
									who.PlaySound( 0x2A );
									
									switch( Utility.Random( 2 ) )
									{
										case 0: who.Say( "I have spent years learning the art of mithril." ); 	break;
										case 1: who.Say( "Let me see what I can make here." );					break;
										case 2: who.Say( "People find their way here for our mithril." ); 		break;
										case 3: who.Say( "You won't see many items like this." );				break;
										case 4: who.Say( "I think I can forge that for you." );					break;
										case 5: who.Say( "The fires are hot so I am ready to forge mithril." );	break;
									}

									OneSay = 1;
								}
								else if ( ( who is IronWorker || who is Weaponsmith || who is Armorer  || who is Blacksmith ) && OneSay == 0 && m_Book.Name == "Brass Crafted Items" )
								{
									who.PlaySound( 0x2A );
									
									switch( Utility.Random( 2 ) )
									{
										case 0: who.Say( "I have spent years learning the art of brass." ); 	break;
										case 1: who.Say( "Let me see what I can make here." );					break;
										case 2: who.Say( "People find their way here for our brass." ); 		break;
										case 3: who.Say( "You won't see many items like this." );				break;
										case 4: who.Say( "I think I can forge that for you." );					break;
										case 5: who.Say( "The fires are hot so I am ready to forge brass." );	break;
									}

									OneSay = 1;
								}
							}
						}
						else
						{
							int NoGold = 0;

							foreach ( Mobile who in from.GetMobilesInRange( 10 ) )
							{
								if ( ( who is IronWorker || who is Weaponsmith || who is Armorer  || who is Blacksmith ) && NoGold == 0 && m_Book.Name == "Steel Crafted Items" )
								{
									who.Say( "You don't seem to have enough gold for me to make that." );
									NoGold = 1;
								}
								else if ( ( who is IronWorker || who is Weaponsmith || who is Armorer  || who is Blacksmith ) && NoGold == 0 && m_Book.Name == "Mithril Crafted Items" )
								{
									who.Say( "You don't seem to have enough gold for me to make that." );
									NoGold = 1;
								}
								else if ( ( who is IronWorker || who is Weaponsmith || who is Armorer  || who is Blacksmith ) && NoGold == 0 && m_Book.Name == "Brass Crafted Items" )
								{
									who.Say( "You don't seem to have enough gold for me to make that." );
									NoGold = 1;
								}
							}
						}
					}
				}
			}
		}

		public SalesBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public static string GetSalesForBook( string book, int selling, int part, Mobile player )
		{
			double barter = player.Skills[SkillName.ItemID].Value * 0.001;

			string item = "";
			string name = "";
			int cost = 0;

			int sales = 1;
			int rate = 4; // STANDARD MARKUP

			double markup = 1;

			if ( m_Book.Name == "Steel Crafted Items" )
			{
				markup = 3.00 * rate;
			}
			else if ( m_Book.Name == "Brass Crafted Items" )
			{
				markup = 6.00 * rate;
			}
			else if ( m_Book.Name == "Mithril Crafted Items" )
			{
				markup = 9.00 * rate;
			}

			markup = markup - ( markup * barter );

			if ( book == "Steel Crafted Items" || book == "Mithril Crafted Items" || book == "Brass Crafted Items" )
			{
				if ( selling == sales ) { name="AssassinSpike"; item="Assassin Dagger"; cost = 21; } sales++;
				if ( selling == sales ) { name="ElvenSpellblade"; item="Assassin Sword"; cost = 33; } sales++;
				if ( selling == sales ) { name="Axe"; item="Axe"; cost = 40; } sales++;
				if ( selling == sales ) { name="OrnateAxe"; item="Barbarian Axe"; cost = 42; } sales++;
				if ( selling == sales ) { name="VikingSword"; item="Barbarian Sword"; cost = 55; } sales++;
				if ( selling == sales ) { name="Bardiche"; item="Bardiche"; cost = 60; } sales++;
				if ( selling == sales ) { name="Bascinet"; item="Bascinet"; cost = 18; } sales++;
				if ( selling == sales ) { name="BattleAxe"; item="Battle Axe"; cost = 26; } sales++;
				if ( selling == sales ) { name="DiamondMace"; item="Battle Mace"; cost = 31; } sales++;
				if ( selling == sales ) { name="BladedStaff"; item="Bladed Staff"; cost = 40; } sales++;
				if ( selling == sales ) { name="Broadsword"; item="Broadsword"; cost = 35; } sales++;
				if ( selling == sales ) { name="BronzeShield"; item="Round Shield"; cost = 66; } sales++;
				if ( selling == sales ) { name="Buckler"; item="Buckler"; cost = 50; } sales++;
				if ( selling == sales ) { name="ButcherKnife"; item="Butcher Knife"; cost = 14; } sales++;
				if ( selling == sales ) { name="ChainChest"; item="Chain Chest"; cost = 143; } sales++;
				if ( selling == sales ) { name="ChainCoif"; item="Chain Coif"; cost = 17; } sales++;
				if ( selling == sales ) { name="ChainHatsuburi"; item="Chain Hatsuburi"; cost = 76; } sales++;
				if ( selling == sales ) { name="ChainLegs"; item="Chain Legs"; cost = 149; } sales++;
				if ( selling == sales ) { name="ChampionShield"; item="Champion Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="ChaosShield"; item="Chaos Shield"; cost = 241; } sales++;
				if ( selling == sales ) { name="Cleaver"; item="Cleaver"; cost = 15; } sales++;
				if ( selling == sales ) { name="CloseHelm"; item="Close Helm"; cost = 18; } sales++;
				if ( selling == sales ) { name="CrescentBlade"; item="Crescent Blade"; cost = 37; } sales++;
				if ( selling == sales ) { name="CrestedShield"; item="Crested Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="Cutlass"; item="Cutlass"; cost = 24; } sales++;
				if ( selling == sales ) { name="Dagger"; item="Dagger"; cost = 21; } sales++;
				if ( selling == sales ) { name="Daisho"; item="Daisho"; cost = 66; } sales++;
				if ( selling == sales ) { name="DarkShield"; item="Dark Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="DecorativePlateKabuto"; item="Decorative Plate Kabuto"; cost = 95; } sales++;
				if ( selling == sales ) { name="DoubleAxe"; item="Double Axe"; cost = 52; } sales++;
				if ( selling == sales ) { name="DoubleBladedStaff"; item="Double Bladed Staff"; cost = 35; } sales++;
				if ( selling == sales ) { name="DreadHelm"; item="Dread Helm"; cost = 21; } sales++;
				if ( selling == sales ) { name="ElvenShield"; item="Elven Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="ExecutionersAxe"; item="Executioners Axe"; cost = 30; } sales++;
				if ( selling == sales ) { name="RadiantScimitar"; item="Falchion"; cost = 35; } sales++;
				if ( selling == sales ) { name="FemalePlateChest"; item="Female Plate Chest"; cost = 113; } sales++;
				if ( selling == sales ) { name="GuardsmanShield"; item="Guardsman Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="Halberd"; item="Halberd"; cost = 42; } sales++;
				if ( selling == sales ) { name="HammerPick"; item="Hammer Pick"; cost = 26; } sales++;
				if ( selling == sales ) { name="Harpoon"; item="Harpoon"; cost = 40; } sales++;
				if ( selling == sales ) { name="HeaterShield"; item="Heater Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="HeavyPlateJingasa"; item="Heavy Plate Jingasa"; cost = 76; } sales++;
				if ( selling == sales ) { name="Helmet"; item="Helmet"; cost = 18; } sales++;
				if ( selling == sales ) { name="OrcHelm"; item="Horned Helm"; cost = 24; } sales++;
				if ( selling == sales ) { name="JeweledShield"; item="Jeweled Shield"; cost = 231; } sales++;
				if ( selling == sales ) { name="Kama"; item="Kama"; cost = 61; } sales++;
				if ( selling == sales ) { name="Katana"; item="Katana"; cost = 33; } sales++;
				if ( selling == sales ) { name="Kryss"; item="Kryss"; cost = 32; } sales++;
				if ( selling == sales ) { name="Lajatang"; item="Lajatang"; cost = 108; } sales++;
				if ( selling == sales ) { name="Lance"; item="Lance"; cost = 34; } sales++;
				if ( selling == sales ) { name="LargeBattleAxe"; item="Large Battle Axe"; cost = 33; } sales++;
				if ( selling == sales ) { name="LightPlateJingasa"; item="Light Plate Jingasa"; cost = 56; } sales++;
				if ( selling == sales ) { name="Longsword"; item="Longsword"; cost = 55; } sales++;
				if ( selling == sales ) { name="Mace"; item="Mace"; cost = 28; } sales++;
				if ( selling == sales ) { name="ElvenMachete"; item="Machete"; cost = 35; } sales++;
				if ( selling == sales ) { name="Maul"; item="Maul"; cost = 21; } sales++;
				if ( selling == sales ) { name="MetalKiteShield"; item="Metal Kite Shield"; cost = 123; } sales++;
				if ( selling == sales ) { name="MetalShield"; item="Metal Shield"; cost = 121; } sales++;
				if ( selling == sales ) { name="NoDachi"; item="NoDachi"; cost = 82; } sales++;
				if ( selling == sales ) { name="NorseHelm"; item="Norse Helm"; cost = 18; } sales++;
				if ( selling == sales ) { name="OrderShield"; item="Order Shield"; cost = 241; } sales++;
				if ( selling == sales ) { name="OrnateAxe"; item="Barbarian Axe"; cost = 241; } sales++;
				if ( selling == sales ) { name="Pike"; item="Pike"; cost = 39; } sales++;
				if ( selling == sales ) { name="Pitchfork"; item="Trident"; cost = 19; } sales++;
				if ( selling == sales ) { name="PlateArms"; item="Plate Arms"; cost = 188; } sales++;
				if ( selling == sales ) { name="PlateBattleKabuto"; item="Plate Battle Kabuto"; cost = 94; } sales++;
				if ( selling == sales ) { name="PlateChest"; item="Plate Chest"; cost = 243; } sales++;
				if ( selling == sales ) { name="PlateDo"; item="Plate Do"; cost = 310; } sales++;
				if ( selling == sales ) { name="PlateGloves"; item="Plate Gloves"; cost = 155; } sales++;
				if ( selling == sales ) { name="PlateGorget"; item="Plate Gorget"; cost = 104; } sales++;
				if ( selling == sales ) { name="PlateHaidate"; item="Plate Haidate"; cost = 235; } sales++;
				if ( selling == sales ) { name="PlateHatsuburi"; item="Plate Hatsuburi"; cost = 76; } sales++;
				if ( selling == sales ) { name="PlateHelm"; item="Plate Helm"; cost = 21; } sales++;
				if ( selling == sales ) { name="PlateHiroSode"; item="Plate Hiro Sode"; cost = 222; } sales++;
				if ( selling == sales ) { name="PlateLegs"; item="Plate Legs"; cost = 218; } sales++;
				if ( selling == sales ) { name="PlateMempo"; item="Plate Mempo"; cost = 76; } sales++;
				if ( selling == sales ) { name="PlateSuneate"; item="Plate Suneate"; cost = 224; } sales++;
				if ( selling == sales ) { name="RingmailArms"; item="Ringmail Arms"; cost = 85; } sales++;
				if ( selling == sales ) { name="RingmailChest"; item="Ringmail Chest"; cost = 121; } sales++;
				if ( selling == sales ) { name="RingmailGloves"; item="Ringmail Gloves"; cost = 93; } sales++;
				if ( selling == sales ) { name="RingmailLegs"; item="Ringmail Legs"; cost = 90; } sales++;
				if ( selling == sales ) { name="RoyalArms"; item="Royal Arms"; cost = 188; } sales++;
				if ( selling == sales ) { name="RoyalBoots"; item="Royal Boots"; cost = 40; } sales++;
				if ( selling == sales ) { name="RoyalChest"; item="Royal Chest"; cost = 242; } sales++;
				if ( selling == sales ) { name="RoyalGloves"; item="Royal Gloves"; cost = 144; } sales++;
				if ( selling == sales ) { name="RoyalGorget"; item="Royal Gorget"; cost = 104; } sales++;
				if ( selling == sales ) { name="RoyalHelm"; item="Royal Helm"; cost = 20; } sales++;
				if ( selling == sales ) { name="RoyalShield"; item="Royal Shield"; cost = 230; } sales++;
				if ( selling == sales ) { name="RoyalsLegs"; item="Royal Legs"; cost = 218; } sales++;
				if ( selling == sales ) { name="RoyalSword"; item="Royal Sword"; cost = 55; } sales++;
				if ( selling == sales ) { name="Sai"; item="Sai"; cost = 56; } sales++;
				if ( selling == sales ) { name="Scepter"; item="Scepter"; cost = 39; } sales++;
				if ( selling == sales ) { name="Sceptre"; item="Sceptre"; cost = 38; } sales++;
				if ( selling == sales ) { name="Scimitar"; item="Scimitar"; cost = 36; } sales++;
				if ( selling == sales ) { name="Scythe"; item="Scythe"; cost = 39; } sales++;
				if ( selling == sales ) { name="ShortSpear"; item="Short Spear"; cost = 23; } sales++;
				if ( selling == sales ) { name="BoneHarvester"; item="Sickle"; cost = 35; } sales++;
				if ( selling == sales ) { name="SkinningKnife"; item="Skinning Knife"; cost = 14; } sales++;
				if ( selling == sales ) { name="SmallPlateJingasa"; item="Small Plate Jingasa"; cost = 66; } sales++;
				if ( selling == sales ) { name="Spear"; item="Spear"; cost = 31; } sales++;
				if ( selling == sales ) { name="StandardPlateKabuto"; item="Standard Plate Kabuto"; cost = 74; } sales++;
				if ( selling == sales ) { name="WizardStaff"; item="Stave"; cost = 40; } sales++;
				if ( selling == sales ) { name="Tekagi"; item="Tekagi"; cost = 55; } sales++;
				if ( selling == sales ) { name="Tessen"; item="Tessen"; cost = 83; } sales++;
				if ( selling == sales ) { name="Tetsubo"; item="Tetsubo"; cost = 43; } sales++;
				if ( selling == sales ) { name="ThinLongsword"; item="Thin Longsword"; cost = 27; } sales++;
				if ( selling == sales ) { name="TwoHandedAxe"; item="Two Handed Axe"; cost = 32; } sales++;
				if ( selling == sales ) { name="Wakizashi"; item="Wakizashi"; cost = 38; } sales++;
				if ( selling == sales ) { name="WarAxe"; item="War Axe"; cost = 29; } sales++;
				if ( selling == sales ) { name="RuneBlade"; item="War Blades"; cost = 55; } sales++;
				if ( selling == sales ) { name="WarCleaver"; item="War Cleaver"; cost = 25; } sales++;
				if ( selling == sales ) { name="Leafblade"; item="War Dagger"; cost = 21; } sales++;
				if ( selling == sales ) { name="WarFork"; item="War Fork"; cost = 32; } sales++;
				if ( selling == sales ) { name="WarHammer"; item="War Hammer"; cost = 24; } sales++;
				if ( selling == sales ) { name="WarMace"; item="War Mace"; cost = 31; } sales++;
			}

			if ( part == 2 ){ item = name; }
			else if ( part == 3 ){ item = ((int)(cost*markup)).ToString(); }

			return item;
		}
	}
}