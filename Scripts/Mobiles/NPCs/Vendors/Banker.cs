using System;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;

namespace Server.Mobiles
{
	public class Banker : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }

		[Constructable]
		public Banker() : base( "the banker" )
		{
			Job = JobFragment.banker;
			Karma = Utility.RandomMinMax( 13, -45 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBanker() );
		}

		public static int GetBalance( Mobile from )
		{
			Item[] gold, checks;

			return GetBalance( from, out gold, out checks );
		}

		public static int GetBalance( Mobile from, out Item[] gold, out Item[] checks )
		{
			int balance = 0;

			Container bank = from.FindBankNoCreate();

			if ( bank != null )
			{
				gold = bank.FindItemsByType( typeof( Gold ) );
				checks = bank.FindItemsByType( typeof( BankCheck ) );

				for ( int i = 0; i < gold.Length; ++i )
					balance += gold[i].Amount;

				for ( int i = 0; i < checks.Length; ++i )
					balance += ((BankCheck)checks[i]).Worth;
			}
			else
			{
				gold = checks = new Item[0];
			}

			return balance;
		}

		public static bool Withdraw( Mobile from, int amount )
		{
			if (!AdventuresFunctions.IsPuritain((object)from))
			{
				Item[] gold, checks;
				int balance = GetBalance( from, out gold, out checks );

				if ( balance < amount )
					return false;

				for ( int i = 0; amount > 0 && i < gold.Length; ++i )
				{
					if ( gold[i].Amount <= amount )
					{
						amount -= gold[i].Amount;
						gold[i].Delete();
					}
					else
					{
						gold[i].Amount -= amount;
						amount = 0;
					}
				}

				for ( int i = 0; amount > 0 && i < checks.Length; ++i )
				{
					BankCheck check = (BankCheck)checks[i];

					if ( check.Worth <= amount )
					{
						amount -= check.Worth;
						check.Delete();
					}
					else
					{
						check.Worth -= amount;
						amount = 0;
					}
				}
				return true;
			}

			else if (AdventuresFunctions.IsPuritain((object)from) && from is PlayerMobile && ((PlayerMobile)from).midrace != 0 );
			{
				PlayerMobile pm = (PlayerMobile)from;
				//Backpack pack = (Backpack)pm.Backpack;

				if (pm.midrace == 1)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Sovereign ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midhumanacc >= amount)
						pm.midhumanacc -= amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 2)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Drachma ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midgargoyleacc >= amount)
						pm.midgargoyleacc -= amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 3)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Sslit ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midlizardacc >= amount)
						pm.midlizardacc -= amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 4)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Dubloon ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midpirateacc >= amount)
						pm.midpirateacc -= amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 5)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Skaal ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midorcacc >= amount)
						pm.midorcacc -= amount;
					else
					{
						return false;
					}
				}
				return true;			
			
			}
			return false;
			
		}

		public static bool Deposit( Mobile from, int amount )
		{
			if (AdventuresFunctions.IsPuritain((object)from) && from is PlayerMobile && ((PlayerMobile)from).midrace == 0 )
			{
				PlayerMobile pm = (PlayerMobile)from;
				//Backpack pack = (Backpack)pm.Backpack;

				if (pm.midrace == 1)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Sovereign ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midhumanacc != null)
						pm.midhumanacc += amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 2)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Drachma ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midgargoyleacc != null)
						pm.midgargoyleacc += amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 3)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Sslit ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midlizardacc != null)
						pm.midlizardacc += amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 4)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Dubloon ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midpirateacc != null)
						pm.midpirateacc += amount;
					else
					{
						return false;
					}
				}
				else if (pm.midrace == 5)
				{
					/*Item money = pm.Backpack.FindItemByType( typeof ( Skaal ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;
					}
					else */if (pm.midorcacc != null)
						pm.midorcacc += amount;
					else
					{
						return false;
					}
				}
				return true;			
			
			}

				BankBox box = from.FindBankNoCreate();
				if ( box == null )
					return false;

				List<Item> items = new List<Item>();

				while ( amount > 0 )
				{
					Item item;
					if ( amount < 5000 )
					{
						item = new Gold( amount );
						amount = 0;
					}
					else if ( amount <= 1000000 )
					{
						item = new BankCheck( amount );
						amount = 0;
					}
					else
					{
						item = new BankCheck( 1000000 );
						amount -= 1000000;
					}

					if ( box.TryDropItem( from, item, false ) )
					{
						items.Add( item );
					}
					else
					{
						item.Delete();
						foreach ( Item curItem in items )
						{
							curItem.Delete();
						}

						return false;
					}
				}

				return true;

		}

		public static int DepositUpTo( Mobile from, int amount )
		{
			BankBox box = from.FindBankNoCreate();
			if ( box == null )
				return 0;

			int amountLeft = amount;
			while ( amountLeft > 0 )
			{
				Item item;
				int amountGiven;

				if ( amountLeft < 5000 )
				{
					item = new Gold( amountLeft );
					amountGiven = amountLeft;
				}
				else if ( amountLeft <= 1000000 )
				{
					item = new BankCheck( amountLeft );
					amountGiven = amountLeft;
				}
				else
				{
					item = new BankCheck( 1000000 );
					amountGiven = 1000000;
				}

				if ( box.TryDropItem( from, item, false ) )
				{
					amountLeft -= amountGiven;
				}
				else
				{
					item.Delete();
					break;
				}
			}

			return amount - amountLeft;
		}

		public static void Deposit( Container cont, int amount )
		{
			while ( amount > 0 )
			{
				Item item;

				if ( amount < 5000 )
				{
					item = new Gold( amount );
					amount = 0;
				}
				else if ( amount <= 1000000 )
				{
					item = new BankCheck( amount );
					amount = 0;
				}
				else
				{
					item = new BankCheck( 1000000 );
					amount -= 1000000;
				}

				cont.DropItem( item );
			}
		}

		public Banker( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 12 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if (  Insensitive.Contains( e.Speech, "deposit" ) )
			{
				this.Say("To deposit sir, simply give me the gold.  You can trust me.");
			}

			if ( !e.Handled && e.Mobile.InRange( this.Location, 12 ) )
			{
				for ( int i = 0; i < e.Keywords.Length; ++i )
				{
					int keyword = e.Keywords[i];

					switch ( keyword )
					{
						case 0x0000: // *withdraw*
						{
							e.Handled = true;

							string[] split = e.Speech.Split( ' ' );

							if ( split.Length >= 2 )
							{
								int amount;

								Container pack = e.Mobile.Backpack;

								if ( !int.TryParse( split[1], out amount ) )
									break;

								if ( (!Core.ML && amount > 5000) || (Core.ML && amount > 60000) )
								{
									this.Say( 500381 ); // Thou canst not withdraw so much at one time!
								}
								else if (pack == null || pack.Deleted || !(pack.TotalWeight < pack.MaxWeight) || !(pack.TotalItems < pack.MaxItems))
								{
									this.Say(1048147); // Your backpack can't hold anything else.
								}
								else if (amount > 0)
								{
									BankBox box = e.Mobile.FindBankNoCreate();

									if (box == null || !box.ConsumeTotal(typeof(Gold), amount))
									{
										this.Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold!
									}
									else
									{
										pack.DropItem(new Gold(amount));

										Server.Gumps.WealthBar.RefreshWealthBar( e.Mobile );

										this.Say(1010005); // Thou hast withdrawn gold from thy account.
									}
								}
							}

							break;
						}
						case 0x0001: // *balance*
						{
							e.Handled = true;

							BankBox box = e.Mobile.FindBankNoCreate();

							if ( box != null )
								this.Say( 1042759, box.TotalGold.ToString() ); // Thy current bank balance is ~1_AMOUNT~ gold.
							else
								this.Say( 1042759, "0" ); // Thy current bank balance is ~1_AMOUNT~ gold.

							break;
						}
						case 0x0002: // *bank*
						{
							e.Handled = true;
							if (AdventuresFunctions.IsPuritain((object)this))
							{
								if (Utility.RandomBool())
									this.Say("Aye sir, it is a bank, you can deposit, withdraw or check the balance of your account.");
								else
									this.Say("Yes, you are correct, this is a bank!  Did you want to deposit, withdraw or check the balance of your account?");
							}
							else
							{
								BankBox box = e.Mobile.BankBox;
								if (box != null)
								{
									//box.GumpID = BaseContainer.BankGump( e.Mobile, box );
									box.Open();
								}
							}

							break;
						}
						case 0x0003: // *check*
						{
							e.Handled = true;

							string[] split = e.Speech.Split( ' ' );

							if ( split.Length >= 2 )
							{
								int amount;

                                if ( !int.TryParse( split[1], out amount ) )
                                    break;

								if ( amount < 5000 )
								{
									this.Say( 1010006 ); // We cannot create checks for such a paltry amount of gold!
								}
								else if ( amount > 10000000 )
								{
									this.Say( 1010007 ); // Our policies prevent us from creating checks worth that much!
								}
								else
								{
									BankCheck check = new BankCheck( amount );

									BankBox box = e.Mobile.BankBox;

									if ( !box.TryDropItem( e.Mobile, check, false ) )
									{
										this.Say( 500386 ); // There's not enough room in your bankbox for the check!
										check.Delete();
									}
									else if ( !box.ConsumeTotal( typeof( Gold ), amount ) )
									{
										this.Say( 500384 ); // Ah, art thou trying to fool me? Thou hast not so much gold!
										check.Delete();
									}
									else
									{
										this.Say( 1042673, AffixType.Append, amount.ToString(), "" ); // Into your bank box I have placed a check in the amount of:
									}
									Server.Gumps.WealthBar.RefreshWealthBar( e.Mobile );
								}
							}

							break;
						}
					}
				}
			} 
			base.OnSpeech( e );
		}


		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (!from.Hidden && dropped is Gold  && from is PlayerMobile)
			{
				BankBox box = from.FindBankNoCreate();

				if ( box != null )
				{
					this.Say("Very well, I have deposited " + dropped.Amount.ToString() + " into your account wish us.");
					if (AdventuresFunctions.IsPuritain((object)this))
					{
						// charge a fee!  banks charge fees!
						int fee = 0;
						if (dropped.Amount > 10)
							fee = (int)((double)dropped.Amount * 0.01);
						this.Say("Thank you for your patronage, " + fee.ToString() + " gold has been charged as a modest fee.");
						dropped.Amount -= fee;
					}
					
					Deposit( box, dropped.Amount );
					dropped.Delete();

				}
			}
			return base.OnDragDrop(from, dropped);
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && from.Kills < 1 && from.Criminal == false )
				list.Add( new OpenBankEntry( from, this ) );

			base.AddCustomContextEntries( from, list );
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Copper and Silver Coins", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Banker" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

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
}
