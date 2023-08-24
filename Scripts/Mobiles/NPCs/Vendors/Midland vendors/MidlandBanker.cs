using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;

namespace Server.Mobiles
{

	public class MidlandBanker : MidlandVendor
	{


		private int m_moneytype;
		[CommandProperty( AccessLevel.GameMaster )]
        public int moneytype
        {
            get{ return m_moneytype; }
            set{ m_moneytype = value; }
        }

		[Constructable]
		public MidlandBanker() : base(  )
		{
			Job = JobFragment.banker;
			Title = "the banker";

		}

		public int GetBalance( Mobile from )
		{
			int balance = 0;

			if (!AdventuresFunctions.IsPuritain((object)this) || !AdventuresFunctions.IsPuritain((object)from) || !(from is PlayerMobile) || ((PlayerMobile)from).midrace == 0 )
				return 0;

			PlayerMobile pm = (PlayerMobile)from;
			if (pm.midrace == 1 && this.midrace == 1)
				return pm.midhumanacc;
			else if (pm.midrace == 2 && this.midrace == 2)
				return pm.midgargoyleacc;
			else if (pm.midrace == 3 && this.midrace == 3)
				return pm.midlizardacc;
			else if (pm.midrace == 4 && this.midrace == 4)
				return pm.midpirateacc;
			else if (pm.midrace == 5 && this.midrace == 5)
				return pm.midorcacc;
			else if (pm.midrace != this.midrace)
				this.Say("We don't serve your kind here.");

			return balance;
		}


		public bool Deposit( Mobile from, int amount)
		{

			if (!AdventuresFunctions.IsPuritain((object)this) || !AdventuresFunctions.IsPuritain((object)from) || !(from is PlayerMobile) || ((PlayerMobile)from).midrace == 0 )
				return false;

			PlayerMobile pm = (PlayerMobile)from;
			Backpack pack = (Backpack)pm.Backpack;

				if (m_moneytype == 1 && this.midrace == 1)
				{
					Item money = pm.Backpack.FindItemByType( typeof ( Sovereign ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;

						pm.midhumanacc += amount;
					}
					else
					{
						return false;
					}
				}
				else if (m_moneytype == 2 && this.midrace == 2)
				{
					Item money = pm.Backpack.FindItemByType( typeof ( Drachma ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;

						pm.midgargoyleacc += amount;
					}
					else
					{
						return false;
					}
				}
				else if (m_moneytype == 3 && this.midrace == 3)
				{
					Item money = pm.Backpack.FindItemByType( typeof ( Sslit ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;

						pm.midlizardacc += amount;
					}
					else
					{
						return false;
					}
				}
				else if (m_moneytype == 4 && this.midrace == 4)
				{
					Item money = pm.Backpack.FindItemByType( typeof ( Dubloon ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;

						pm.midpirateacc += amount;
					}
					else
					{
						return false;
					}
				}
				else if (m_moneytype == 5 && this.midrace == 5)
				{
					Item money = pm.Backpack.FindItemByType( typeof ( Skaal ) );
					if (money != null && money.Amount >= amount)
					{
						if (money.Amount == amount)
							money.Delete();
						else
							money.Amount -= amount;

						pm.midorcacc += amount;
					}
					else
					{
						return false;
					}
				}
				else if (m_moneytype != this.midrace)
				{
					this.Say("We don't deal with your type here.");
					return false;
				}

			return true;

		}

		public bool Withdraw( Mobile from, int amount )
		{
			int balance = GetBalance( from );

			if (balance == 0 || !(from is PlayerMobile)) // not a playermobile or wrong race
				return false;

			if ( balance < amount )
				return false;

			PlayerMobile pm = (PlayerMobile)from;
			Backpack pack = (Backpack)pm.Backpack;

			if ( balance > amount )
			{
				if (pm.midrace == 1 && this.midrace == 1)
				{
					pm.midhumanacc -= amount;
					pm.AddToBackpack( new Sovereign( amount ) );	
				}
				else if (pm.midrace == 2 && this.midrace == 2)
				{
					pm.midgargoyleacc -= amount;
					pm.AddToBackpack( new Drachma( amount ) );
				}
				else if (pm.midrace == 3 && this.midrace == 3)
				{
					pm.midlizardacc -= amount;
					pm.AddToBackpack( new Sslit( amount ) );
				}
				else if (pm.midrace == 4 && this.midrace == 4)
				{
					pm.midpirateacc -= amount;
					pm.AddToBackpack( new Dubloon( amount ) );
				}
				else if (pm.midrace == 5 && this.midrace == 5)
				{
					pm.midorcacc -= amount;
					pm.AddToBackpack( new Skaal( amount ) );
				}
				else if (pm.midrace != this.midrace)
					return false;

			}
			return true;
		}



		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 12 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			if (  Insensitive.Contains( e.Speech, "deposit" ) && e.Mobile.InRange( this.Location, 6 )  )
			{
				string[] split = e.Speech.Split( ' ' );

				if ( split.Length >= 2 )
				{
					int amount;

					if ( !int.TryParse( split[1], out amount ) )
						this.Say("To deposit, simply tell me you wish to deposit and the amount or give me the gold.  You can trust me.");

					
					if ( amount > 0 && Deposit(e.Mobile, amount) )
						this.Say("You have deposited " + amount.ToString() + " to your account.");
					else
						this.Say("You don't appear to have enough to deposit that amount.");
				}
				else
					this.Say("To deposit, simply tell me you wish to deposit and the amount or give me the gold.  You can trust me.");
					
			}
			if ( !e.Handled && e.Mobile.InRange( this.Location, 6 ) )
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

									if (!Withdraw( e.Mobile, amount))
									{
										this.Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold!
									}
									else
									{
										this.Say("You've withdrawn " + amount + " from your account.");
									}
								}
							}

							break;
						}
						case 0x0001: // *balance*
						{
							e.Handled = true;

							int amt = GetBalance(e.Mobile);

							if ( amt > 0 )
								this.Say( "Your current balance with us is " + amt.ToString() ); 
							else
								this.Say( "You don't appear to have an account with us." ); 

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

							break;
						}
						/* no checks yet
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
								else if ( amount > 1000000 )
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
						*/
					}
				}
			} 
			base.OnSpeech( e );
		}


		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (!from.Hidden && from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;
				if (dropped is Sovereign && this.midrace == 1 && pm.midrace == 1)
				{
					pm.midhumanacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Drachma && this.midrace == 2 && pm.midrace == 2)
				{
					pm.midgargoyleacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Sslit && this.midrace == 3 && pm.midrace == 3)
				{
					pm.midlizardacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Dubloon && this.midrace == 4 && pm.midrace == 4)
				{
					pm.midpirateacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (dropped is Skaal && this.midrace == 5 && pm.midrace == 5)
				{
					pm.midorcacc += dropped.Amount;
					dropped.Delete();
					return true;
				}
				else if (this.midrace != pm.midrace)
				{
					this.Say("We don't deal your kind here.");
					return false;
				}
				else 
				{
					this.Say("That thing isn't money.");
					return false;
				}
			}

			return false;

		}

		public MidlandBanker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			good1 = typeof(TinkerTools);

		}

	}
}