using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Gumps;
using Server.OneTime;
using Server.Regions;
using System.Text.RegularExpressions;

namespace Server.Mobiles
{
	[CorpseName( "an vendor corpse" )]
	public class MidlandVendor : BaseConvo, IOneTime
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

		private int m_good1inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good1inventory
        {
            get{ return m_good1inventory; }
            set{ m_good1inventory = value; }
        }

		private Type m_good1;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good1
        {
            get{ return m_good1; }
            set{ m_good1 = value; }
        }

		private string m_good1name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good1name
        {
            get{ return m_good1name; }
            set{ m_good1name = value; }
        }
		private int m_good1price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good1price
        {
            get{ return m_good1price; }
            set{ m_good1price = value; }
        }

		private double m_good1adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good1adjust
        {
            get{ return m_good1adjust; }
            set{ m_good1adjust = value; }
        }

		private int m_good2inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good2inventory
        {
            get{ return m_good2inventory; }
            set{ m_good2inventory = value; }
        }
		private Type m_good2;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good2
        {
            get{ return m_good2; }
            set{ m_good2 = value; }
        }	

		private string m_good2name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good2name
        {
            get{ return m_good2name; }
            set{ m_good2name = value; }
        }
		private int m_good2price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good2price
        {
            get{ return m_good2price; }
            set{ m_good2price = value; }
        }

		private double m_good2adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good2adjust
        {
            get{ return m_good2adjust; }
            set{ m_good2adjust = value; }
        }

		private int m_good3inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good3inventory
        {
            get{ return m_good3inventory; }
            set{ m_good3inventory = value; }
        }
		private Type m_good3;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good3
        {
            get{ return m_good3; }
            set{ m_good3 = value; }
        }

		private string m_good3name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good3name
        {
            get{ return m_good3name; }
            set{ m_good3name = value; }
        }	
		private int m_good3price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good3price
        {
            get{ return m_good3price; }
            set{ m_good3price = value; }
        }

		private double m_good3adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good3adjust
        {
            get{ return m_good3adjust; }
            set{ m_good3adjust = value; }
        }

		private int m_good4inventory;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good4inventory
        {
            get{ return m_good4inventory; }
            set{ m_good4inventory = value; }
        }
		private Type m_good4;
		[CommandProperty( AccessLevel.GameMaster )]
        public Type good4
        {
            get{ return m_good4; }
            set{ m_good4 = value; }
        }

		private string m_good4name;
		[CommandProperty( AccessLevel.GameMaster )]
        public string good4name
        {
            get{ return m_good4name; }
            set{ m_good4name = value; }
        }
		private int m_good4price;
		[CommandProperty( AccessLevel.GameMaster )]
        public int good4price
        {
            get{ return m_good4price; }
            set{ m_good4price = value; }
        }

		private double m_good4adjust;
		[CommandProperty( AccessLevel.GameMaster )]
        public double good4adjust
        {
            get{ return m_good4adjust; }
            set{ m_good4adjust = value; }
        }

		private int m_moneytype;
		[CommandProperty( AccessLevel.GameMaster )]
        public int moneytype
        {
            get{ return m_moneytype; }
            set{ m_moneytype = value; }
        }

		private bool buying;
		private string buyingwhat;
		private Mobile buyer;

		private int buyingamount;

		private bool selling;
		private string sellingwhat;
		private Mobile seller;

		private int sellingamount;

		private int saletick;

		// + OmniAI support +
		protected override BaseAI ForcedAI
		{
			get
			{
			return new OmniAI(this);
			}
		}
		// - OmniAI support -


		[Constructable]
		public MidlandVendor() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_OneTimeType = 3;

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			

			if ( Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}


			Karma = Utility.RandomMinMax( 13, -45 );

			SetStr( 100, 300 );
			SetDex( 100, 300 );
			SetInt( 100, 300 );

			SetHits( 100,300 );
			SetDamage( 15, 70 );

			VirtualArmor = 70;

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetDamageType( ResistanceType.Energy, 60 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 35, 40 );

			SetSkill( SkillName.EvalInt, 90, 120 );
			SetSkill( SkillName.Magery, 90, 120 );
			SetSkill( SkillName.Meditation, 90, 120 );
			SetSkill( SkillName.Poisoning, 90, 120 );
			SetSkill( SkillName.MagicResist, 90, 120 );
			SetSkill( SkillName.Tactics, 90, 120 );
			SetSkill( SkillName.Wrestling, 90, 120 );
			SetSkill( SkillName.Macing, 90, 120 );

			OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

			buying = false;
			buyingwhat = "";
			buyingamount = 0;
			saletick = 0;
			selling = false;
			sellingwhat = "";
			sellingamount = 0;

			good1price = 0;
			good1name = "";
			good1inventory = 0;

			good2price = 0;
			good2name = "";
			good2inventory = 0;

			good3price = 0;
			good3name = "";
			good3inventory = 0;

			good4price = 0;
			good4name = "";
			good4inventory = 0;

			AdjustPrice();
			CantWalk = true;

		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public virtual bool IsInvulnerable { get { return false; } }
		public override bool Unprovokable { get { return true; } }
		public override bool Uncalmable{ get{ return true; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			switch ( Utility.Random( 4 ))  
			{
				case 0: Say("Guards!  Guards!"); break;
				case 1: Say("Vendor Buy Bank Guards!."); break;
				case 2: Say("Where are you Guards???"); break;
				case 3: Say("To me!!! Guards!"); break;
			};
		}

		public override bool IsEnemy( Mobile m )
		{
			if (m.Criminal)
				return true;

			if ( IntelligentAction.GetMidlandEnemies( m, this, true ) == false )
				return false;

			return true;
		}

      public override bool HandlesOnSpeech( Mobile from ) 
      { 
         return (from != null && from.Player && from.Alive && (int)GetDistanceToSqrt( from ) < 4 && from != ControlMaster); 
      } 

		public override void OnSpeech( SpeechEventArgs e ) 
		{

			base.OnSpeech(e);

			if (e.Mobile == null)
				return;

			string speech = e.Speech;

			Mobile from = (Mobile)e.Mobile;

			if (from is PlayerMobile && IntelligentAction.RaceCheck( this, from) )
				return;

			this.Direction = this.GetDirectionTo( e.Mobile.Location );

			string mn = GetCurrency();

			if( from.InRange( this, 5 ))
			{
				if (  Insensitive.Contains( speech, "inventory" ) || Insensitive.Contains( speech, "list" ) || Insensitive.Contains( speech, "you have" ) || Insensitive.Contains( speech, "stock" ) || Insensitive.Contains( speech, "for sale" ))
				{
					if (this is MidlandBanker)
					{
						Say("I'm a banker... We provide accounts and keep your money safe.");
						return;
					}
				
					AdjustPrice();

					Say("Of course, Let's see what I have for sale...");
					bool g = false;
					string nm = GetCurrency();

					if (good1inventory > 0 && good1name != null)
					{
						Say(good1inventory.ToString() + " " + good1name + " at " + good1price + " " + nm + "." );
						g = true;
					}
					if (good2inventory > 0 && good2name != null)
					{
						Say(good2inventory.ToString() + " " + good2name + " at " + good2price + " " + nm + ".");
						g = true;
					}
					if (good3inventory > 0 && good3name != null)
					{
						Say(good3inventory.ToString() + " " + good3name + " at " + good3price + " " + nm + ".");
						g = true;
					}
					if (good4inventory > 0 && good4name != null)
					{
						Say(good4inventory.ToString() + " " + good4name + " at " + good4price + " " + nm + ".");
						g = true;
					}

					if (g)
						Say("I charge a small 1% fee for my services.");

					if (!g)
					{
						Say("I'm sorry M'Lord, my stocks are empty... perhaps you'd like to sell to me?");
						Say("I deal in " + good1name + " " + good2name + " " + good3name + " " + good4name);
						Say("You can ask me for my prices");
					}
						
					return;

				}
				if (  Insensitive.Contains( speech, "price" ) || Insensitive.Contains( speech, "prices" ) || Insensitive.Contains( speech, "cost" ))
				{

					if (this is MidlandBanker)
					{
						Say("Well, the bank does take a small fee, but it is minimal.");
						return;
					}

					AdjustPrice();
					string nm = GetCurrency();

					Say("My prices?  Of course...");
					if (good1name != null && good1name != "")
					{
						Say( "I buy " + good1name + " at " + good1price + " " + nm + "." );
					}
					if (good2name != null && good2name != "")
					{
						Say("I buy " + good2name + " at " + good2price + " " + nm + ".");
					}
					if (good3name != null && good3name != "")
					{
						Say("I buy " + good3name + " at " + good3price + " " + nm + ".");
					}
					if (good4name != null && good4name != "")
					{
						Say("I buy " + good4name + " at " + good4price + " " + nm + ".");
					}
						
					return;
				}
				//text parser

				if (  Insensitive.Contains( speech, "buy" ) && !buying)
				{

					if (this is MidlandBanker)
					{
						Say("You cannot buy a banker!");
						return;
					}

					buyingwhat = ""; //reset from previous orders
					buyingamount = 0;

					if (Insensitive.Contains( speech, good1name ))
						buyingwhat = good1name;
					else if (Insensitive.Contains( speech, good2name ))
						buyingwhat = good2name;
					else if (Insensitive.Contains( speech, good3name ))
						buyingwhat = good3name;
					else if (Insensitive.Contains( speech, good4name ))
						buyingwhat = good4name;

					if (buyingwhat != "" )
					{
						buying = true;
						buyer = e.Mobile;
					}
				}
				else if (  Insensitive.Contains( speech, "sell" ) && !selling)
				{

					if (this is MidlandBanker)
					{
						Say("Unless you are selling risk free investments, we are not interested.");
						return;
					}

					sellingwhat = ""; //reset from previous orders
					sellingamount = 0;

					if (Insensitive.Contains( speech, good1name ))
						sellingwhat = good1name;
					else if (Insensitive.Contains( speech, good2name ))
						sellingwhat = good2name;
					else if (Insensitive.Contains( speech, good3name ))
						sellingwhat = good3name;
					else if (Insensitive.Contains( speech, good4name ))
						sellingwhat = good4name;

					if (sellingwhat != "" )
					{
						selling = true;
						seller = e.Mobile;
					}
				}


				if ( (buying && from == buyer) || (selling && from == seller))
				{
					
					String number = Regex.Match(e.Speech, @"\d+").Value;

					int amount = 0;
					if (number != null)
						int.TryParse(number, out amount);
					if (amount >= 1)
					{
							Console.WriteLine("found amount " + amount);//debug
							if (buying && !CheckInventory(buyingwhat, amount)) //checks amount of item on hand
							{
								Say("I don't have that many " + buyingwhat + " on hand at the moment, Sire.");
							}
							else if (buying)
								ProcessBuy(buyer, buyingwhat, amount);
							else if (selling)
							{
								ProcessSell(seller, sellingwhat, amount);
							}
					}
					else if (buying)//amount not received
							Say("How many " + buyingwhat + " would you like to buy?");
					else if (selling)
							Say("How many " + sellingwhat + " would you like to sell sire?");

				}
				else if ((selling && from != seller) || (buying && from != buyer))
				{
					Console.WriteLine("different name error");
					Say("I'm sorry " + from.Name + ", I can only deal with one customer at a time.");
				}

			} 
		} 

		public bool CheckInventory(string what, int amount)
		{
			bool instock = false;
			if (what == good1name && amount <= good1inventory)
				instock = true;
			if (what == good2name && amount <= good2inventory)
				instock = true;
			if (what == good3name && amount <= good3inventory)
				instock = true;
			if (what == good4name && amount <= good4inventory)
				instock = true;

			if (instock)
				return true;
			
			Say("Sorry Sire, I don't have enough " + what + " to sell you that many.");
			return false;
		}

		public void ProcessBuy(Mobile buyer, string what, int amount)
		{
 			// processes sale, takes gold, gives items, resets buyer and buyingwhat vars

			AdjustPrice();

			Item ii = null;
			Type a = null;
			// give the goods
			if (what == good1name)
			{
				a = good1;
				ii = (Item)Activator.CreateInstance(a);
			}
			if (what == good2name)
			{
				a = good2;
				ii = (Item)Activator.CreateInstance(a);
			}
			if (what == good3name)
			{
				a = good3;
				ii = (Item)Activator.CreateInstance(a);
			}
			if (what == good4name)
			{
				a = good4;
				ii = (Item)Activator.CreateInstance(a);
			}


			if (a == null )
			{
				buyer.SendMessage("there was a problem with the sale, let an amin know.");
				this.buying = false;
				this.buyingamount = 0;
				this.buyingwhat = "";
				this.buyer = null;
				return;
			}

			int price = CalculatePrice(what, amount, false);
			price = (int)((double)price*1.01); //buying, price is 10% higher

			if (!GetMoney(buyer, price))
			{
				AdjustInventory(what, amount, true);
				Say("Do you have this much in your pack or your account at the local bank?");
				return;
			}

			if (amount > 0 && buyer is PlayerMobile)
				((PlayerMobile)buyer).AdjustReputation(price/50, ((BaseCreature)this).midrace, true);

			buyer.Backpack.DropItem(ii);
			amount -=1;
			if ( !(ii is Container) && amount > 1 && ii.StackWith( buyer, (Item)Activator.CreateInstance(a), false ))
			{
				amount -= 1;
				ii.Amount += amount;
			}
			else 
			{
				for (int i = 0; i < amount; ++i)
				{
					buyer.Backpack.DropItem((Item)Activator.CreateInstance(a));
				}
			}
			Say("Thank you for your business, Sire.");			


			this.buying = false;
			this.buyingamount = 0;
			this.buyingwhat = "";
			this.buyer = null;
			
		}

		public void ProcessSell(Mobile buyer, string what, int amount)
		{
 			
			AdjustPrice();

			Type ii = null;
			// give the goods
			if (what == good1name)
			{
				ii =  good1;
			}
			if (what == good2name)
			{
				ii =  good2;
			}
			if (what == good3name)
			{
				ii = good3;
			}
			if (what == good4name)
			{
				ii =  good4;
			} 

			// check player has enough
			bool check = false;
			int aamount = amount;

			List<Server.Item> listy = buyer.Backpack.Items;

			for ( int i = 0; i < listy.Count; ++i )
			{
				Item item = listy[i];
				if (item.GetType() == ii)
				{
					if (item.Amount >= aamount)
						check = true;
					else 
					{
						aamount -= 1;
						if (aamount == 0)
						{
							check = true;
						}
					}
				}
			}
			if (!check)
			{
				Say ("Sire, do you not appear to have " + amount + " " + what + " on you.");
			}
			else
			{
				aamount = amount;
				int price = CalculatePrice(what, amount, true);
				price = (int)((double)price*0.99);

				if (!GiveMoney(buyer, price))
					return;
					
				if (price == 0)
					buyer.SendMessage("The total was 0 gold so no money was given.");


			if (price > 0 && buyer is PlayerMobile)
				((PlayerMobile)buyer).AdjustReputation(price/50, ((BaseCreature)this).midrace, true);
				
				for ( int i = 0; i < listy.Count; ++i )
				{
					Item item = listy[i];
					if (item.GetType() == ii)
					{
						if (item.Amount > aamount)
						{
							item.Amount -= aamount;
						}
						else if (item.Amount <= aamount)
						{
							aamount -= item.Amount;
							item.Delete();
						}
						else 
						{
							aamount -= 1;
							item.Delete();
							if (aamount == 0)
							{
								break;
							}
						}
					}
				}
				Say("Pleasure doing business, Sire!");

				this.selling = false;
				this.sellingamount = 0;
				this.sellingwhat = "";
				this.seller = null;

			}

		}


		public bool GiveMoney( Mobile from, int amount)
		{

			if (!AdventuresFunctions.IsPuritain((object)this) || !AdventuresFunctions.IsPuritain((object)from) || !(from is PlayerMobile) || ((PlayerMobile)from).midrace == 0 )
				return false;

			if (amount < 0 )
				return false; // something wrong happened here

			PlayerMobile pm = (PlayerMobile)from;
			Backpack pack = (Backpack)pm.Backpack;

				if (m_moneytype == 1 && this.midrace == 1)
				{
					if (amount < 5000)
						pm.AddToBackpack( new Sovereign( amount ) );
					else
						pm.midhumanacc += amount;
				}
				else if (m_moneytype == 2 && this.midrace == 2)
				{
					if (amount < 5000)
						pm.AddToBackpack( new Drachma( amount ) );
					else
						pm.midgargoyleacc += amount;
				}
				else if (m_moneytype == 3 && this.midrace == 3)
				{
					if (amount < 5000)
						pm.AddToBackpack( new Sslit( amount ) );
					else
						pm.midlizardacc += amount;
				}
				else if (m_moneytype == 4 && this.midrace == 4)
				{
					if (amount < 5000)
						pm.AddToBackpack( new Dubloon( amount ) );
					else
						pm.midpirateacc += amount;
				}
				else if (m_moneytype == 5 && this.midrace == 5)
				{
					if (amount < 5000)
						pm.AddToBackpack( new Skaal( amount ) );
					else
						pm.midorcacc += amount;
				}
				else if (m_moneytype != this.midrace)
				{
					this.Say("We don't deal with your type here.");
					return false;
				}

			return true;

		}

		public bool GetMoney( Mobile from, int amount)
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
					}
					else if (pm.midhumanacc >= amount)
						pm.midhumanacc -= amount;
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
					}
					else if (pm.midgargoyleacc >= amount)
						pm.midgargoyleacc -= amount;
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
					}
					else if (pm.midlizardacc >= amount)
						pm.midlizardacc -= amount;
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
					}
					else if (pm.midpirateacc >= amount)
						pm.midpirateacc -= amount;
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
					}
					else if (pm.midorcacc >= amount)
						pm.midorcacc -= amount;
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

		public int CalculatePrice (string what, int amount, bool increase)
		{
			int price = 0;
			int total = 0;

			if (what == good1name)
			{
				price = good1price;
			}
			if (what == good2name)
			{
				price = good2price;
			}
			if (what == good3name)
			{
				price = good3price;
			}
			if (what == good4name)
			{
				price = good4price;
			}			

			if (amount > 20)
			{
				int a = amount;
				
				while ( a != 0 )
				{
					if (a>=10)
					{
						total += (10*price);
						a -= 10;
						AdjustInventory(what, 10, increase);
					}
					else
					{
						total += (a*price);
						a = 0;
						AdjustInventory(what, a, increase);						
					}
				}
			}
			else
				total = amount * price;

			return total;
		}

		public void AdjustInventory( string what, int amount, bool increase)
		{
			if (what == good1name)
			{
				if (increase)
					good1inventory += amount;
				else
					good1inventory -= amount;
			}
			if (what == good2name)
			{
				if (increase)
					good2inventory += amount;
				else
					good2inventory -= amount;
			}
			if (what == good3name)
			{
				if (increase)
					good3inventory += amount;
				else
					good3inventory -= amount;
			}
			if (what == good4name)
			{
				if (increase)
					good4inventory += amount;
				else
					good4inventory -= amount;
			}	
			AdjustPrice();		
		}

		public void AdjustPrice()
		{

				if (good1inventory >0)
					good1price = 1 + ( (int)(100*good1adjust) - (int)(((100*good1adjust)/(5000/good1adjust)) * (double)good1inventory) );
				else
					good1price = (int)(100*good1adjust);

				if (good2inventory >0)
					good2price = 1 + ( (int)(100*good2adjust) - (int)(((100*good2adjust)/(5000/good2adjust)) * (double)good2inventory) );
				else
					good2price = (int)(100*good2adjust);

				if (good3inventory >0)
					good3price = 1 + ( (int)(100*good3adjust) - (int)(((100*good3adjust)/(5000/good3adjust)) * (double)good3inventory) );
				else
					good3price = (int)(100*good3adjust);

				if (good4inventory >0)
					good4price = 1 + ( (int)(100*good4adjust) - (int)(((100*good4adjust)/(5000/good4adjust)) * (double)good4inventory) );
				else
					good4price = (int)(100*good4adjust);
				
		}

		public string GetCurrency()
		{
			if (m_moneytype == 1)
				return "Sovereign";
			if (m_moneytype == 2)
				return "Drachma";
			if (m_moneytype == 3)
				return "Sslits";
			if (m_moneytype == 4)
				return "Dubloons";
			if (m_moneytype == 5)
				return "Skaals";
				
			return "";

		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			base.OnMovement(m, oldLocation);

			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 5 ) && InLOS( m ) )
				{ 
					if (((BaseCreature)this).midrace == 1)
					{
						switch ( Utility.Random( 6 ))
						{
							case 0: Say("Only the finest goods here!"); break;
							case 1: Say("Adventurer, do you have any goods to sell?  Ask me what I buy."); break;
							case 2: Say("You can buy from me, just ask me what I have."); break;
							case 3: Say("You look in a spending mood, sir!  Ask me what I have for sale!"); break;
							case 4: Say("I buy from adventuers, and sell back to other adventurers - what a life!"); break;
							case 5: Say("Greetings, Sire - How can I assist?"); break;
						};
					}
					else if (((BaseCreature)this).midrace == 2) // gargoyle (needs to be edited)
					{
						switch ( Utility.Random( 6 ))
						{
							case 0: Say("Only the finest goods here!"); break;
							case 1: Say("Adventurer, do you have any goods to sell?  Ask me what I buy."); break;
							case 2: Say("You can buy from me, just ask me what I have."); break;
							case 3: Say("You look in a spending mood, sir!  Ask me what I have for sale!"); break;
							case 4: Say("I buy from adventuers, and sell back to other adventurers - what a life!"); break;
							case 5: Say("Greetings, Sire - How can I assist?"); break;
						};
					}
					else if (((BaseCreature)this).midrace == 3) // Lizards (needs to be edited)
					{
						switch ( Utility.Random( 6 ))
						{
							case 0: Say("Only the finest goods here!"); break;
							case 1: Say("Adventurer, do you have any goods to sell?  Ask me what I buy."); break;
							case 2: Say("You can buy from me, just ask me what I have."); break;
							case 3: Say("You look in a spending mood, sir!  Ask me what I have for sale!"); break;
							case 4: Say("I buy from adventuers, and sell back to other adventurers - what a life!"); break;
							case 5: Say("Greetings, Sire - How can I assist?"); break;
						};
					}
					else if (((BaseCreature)this).midrace == 4) // pirates (needs to be edited)
					{
						switch ( Utility.Random( 6 ))
						{
							case 0: Say("Only the finest goods here!"); break;
							case 1: Say("Adventurer, do you have any goods to sell?  Ask me what I buy."); break;
							case 2: Say("You can buy from me, just ask me what I have."); break;
							case 3: Say("You look in a spending mood, sir!  Ask me what I have for sale!"); break;
							case 4: Say("I buy from adventuers, and sell back to other adventurers - what a life!"); break;
							case 5: Say("Greetings, Sire - How can I assist?"); break;
						};
					}
					else if (((BaseCreature)this).midrace == 5) // orcs (needs to be edited)
					{
						switch ( Utility.Random( 6 ))
						{
							case 0: Say("Only the finest goods here!"); break;
							case 1: Say("Adventurer, do you have any goods to sell?  Ask me what I buy."); break;
							case 2: Say("You can buy from me, just ask me what I have."); break;
							case 3: Say("You look in a spending mood, sir!  Ask me what I have for sale!"); break;
							case 4: Say("I buy from adventuers, and sell back to other adventurers - what a life!"); break;
							case 5: Say("Greetings, Sire - How can I assist?"); break;
						};
					}

					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
			base.OnMovement(m, oldLocation );
			
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (dropped == null || from == null)
				return false;

			bool sale = false;
			string what = "";

			if (dropped.GetType() == good1)
			{
				sale = true;
				what = good1name;
			}
			if (dropped.GetType() == good2)
			{
				sale = true;
				what = good2name;
			}
			if (dropped.GetType() == good3)
			{
				sale = true;
				what = good3name;
			}
			if (dropped.GetType() == good4)
			{
				sale = true;
				what = good4name;
			}

			if (!sale )
			{
				Say("Pardon me Sire, I don't trade in that.");
				return false;
			}
			else
			{
				int money = CalculatePrice(what, dropped.Amount, true);

				if (!GiveMoney(from, money))
				{
					Say("I don't think so.");
					return false;
				}
				Say("Thank you Sire, I bought " + dropped.Amount + " " + what + " for a total of " + money + " " + GetCurrency() + ".");
				if (money > 5000)
					Say("This was over 5,000 so I sent the funds to your bank account.");

				dropped.Delete();
				return true;
			}

			return base.OnDragDrop( from, dropped );
		}

		public void OneTimeTick()
        {
			if (saletick == 0 && (buying || selling))
			{
				saletick = 20;
			}
			else if (saletick == 1)
			{
				if (selling)
				{		
					this.selling = false;
					this.sellingamount = 0;
					this.sellingwhat = "";
					this.seller = null;
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,"*goes back to his business*");
				}
				if (buying)
				{
					this.buying = false;
					this.buyingamount = 0;
					this.buyingwhat = "";
					this.buyer = null;
					this.PublicOverheadMessage(MessageType.Regular, 0x3B2, false,"*goes back to his business*");
				}
			}
			saletick -=1;

			if (Utility.RandomMinMax(1, 500) == 69)
			{
				if (Utility.RandomBool() && good1inventory > 3)
					good1inventory -= Utility.RandomMinMax(1, 3);
				if (Utility.RandomBool() && good2inventory > 3)
					good2inventory -= Utility.RandomMinMax(1, 3);
					if (Utility.RandomBool() && good3inventory > 3)
					good3inventory -= Utility.RandomMinMax(1, 3);
					if (Utility.RandomBool() && good4inventory > 3)
					good4inventory -= Utility.RandomMinMax(1, 3);
			}

		}

		public Type FindType(string what)
		{
			if (what == m_good1name)
			{
				return m_good1;
			}
			if (what == m_good2name)
			{
				return m_good2;
			}
			if (what == m_good3name)
			{
				return m_good3;
			}
			if (what == m_good4name)
			{
				return m_good4;
			} 
			return null;
		}

		public void Dress()
		{
			if (((BaseCreature)this).midrace == 1)
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: AddItem( new FancyShirt( RandomThings.GetRandomColor(0) ) ); break;
					case 1: AddItem( new Doublet( RandomThings.GetRandomColor(0) ) ); break;
					case 2: AddItem( new Shirt( RandomThings.GetRandomColor(0) ) ); break;
				}

				switch ( ShoeType )
				{
					case VendorShoeType.Shoes: AddItem( new Shoes( GetShoeHue() ) ); break;
					case VendorShoeType.Boots: AddItem( new Boots( GetShoeHue() ) ); break;
					case VendorShoeType.Sandals: AddItem( new Sandals( GetShoeHue() ) ); break;
					case VendorShoeType.ThighBoots: AddItem( new ThighBoots( GetShoeHue() ) ); break;
				}

				int hairHue =  RandomThings.GetRandomHairColor();

				Utility.AssignRandomHair( this, hairHue );
				Utility.AssignRandomFacialHair( this, hairHue );

				if ( Female )
				{
					switch ( Utility.Random( 6 ) )
					{
						case 0: AddItem( new ShortPants( RandomThings.GetRandomColor(0) ) ); break;
						case 1:
						case 2: AddItem( new Kilt( RandomThings.GetRandomColor(0) ) ); break;
						case 3:
						case 4:
						case 5: AddItem( new Skirt( RandomThings.GetRandomColor(0) ) ); break;
					}
				}
				else
				{
					FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
					FacialHairHue = hairHue;

					switch ( Utility.Random( 2 ) )
					{
						case 0: AddItem( new LongPants( RandomThings.GetRandomColor(0) ) ); break;
						case 1: AddItem( new ShortPants( RandomThings.GetRandomColor(0) ) ); break;
					}
				}


			}
			if (((BaseCreature)this).midrace == 4)
			{
			    AddItem( new ElvenBoots( 0x83A ) );
            	Item armor = new LeatherChest(); armor.Hue = 0x83A; AddItem( armor );
                Item robe = new PirateRobe(); robe.Hue = 0x455; AddItem(robe);
                AddItem( new FancyShirt( 0 ) );	

				switch ( Utility.Random( 2 ))
				{
					case 0: AddItem( new LongPants ( 0xBB4 ) ); break;
					case 1: AddItem( new ShortPants ( 0xBB4 ) ); break;
				}	
			}

            if (((BaseCreature)this).midrace == 5)
            {
                Item boots = new FurBoots();
                boots.Name = "Ugg Boots";
                boots.Hue = 357;
                AddItem(boots);

                Item cape = new FurCape();
                cape.Name = "Pelt Cape";
                cape.Hue = 351;
                AddItem(cape);

                Item skirt = new StuddedSkirt();
                skirt.Name = "Reed Skirt";
                skirt.Hue = 357;
                AddItem(skirt);

                Item gloves = new StuddedGloves();
                gloves.Hue = 357;
                AddItem(gloves);

                Item fetish = new SilverNecklace();
                fetish.Name = "Orkish Fetish";
                fetish.Hue = 357;
                AddItem(fetish);

                Item arms = new DragonArms();
                arms.Name = "Reed Armlets";
                arms.Hue = 357;
                AddItem(arms);

                Item malechest = new StuddedDo();
                malechest.Name = "Reed Chest";
                malechest.Hue = 357;

                Item femalechest = new FemaleStuddedChest();
                femalechest.Name = "Reed Chest";
                femalechest.Hue = 351;

                if (Female)
                {

                    AddItem(femalechest);
                }
                else
                {
                    AddItem(malechest);
                }

            }


			//PackGold( money1, money2 ); need to add respective curency for thieves/murderers
		}

		public virtual int GetShoeHue()
		{
			if ( 0.1 > Utility.RandomDouble() )
				return 0;

			return Utility.RandomNeutralHue();
		}

		public virtual VendorShoeType ShoeType
		{
			get { return VendorShoeType.Shoes; }
		}

		public override bool OnBeforeDeath()
		{
			Mobile killer = this.LastKiller;

			if (killer is BaseCreature)
			{
				BaseCreature bc_killer = (BaseCreature)killer;
				if(bc_killer.Summoned)
				{
					if(bc_killer.SummonMaster != null)
						killer = bc_killer.SummonMaster;
				}
				else if(bc_killer.Controlled)
				{
					if(bc_killer.ControlMaster != null)
						killer=bc_killer.ControlMaster;
				}
				else if(bc_killer.BardProvoked)
				{
					if(bc_killer.BardMaster != null)
						killer=bc_killer.BardMaster;
				}
			}

			if ( killer is PlayerMobile )
			{
				killer.Criminal = true;
				killer.Kills = killer.Kills + 1;
				((PlayerMobile)killer).AdjustReputation( this );
			}

			string bSay = "Help!";
			if (((BaseCreature)this).midrace == 1)
			{
				switch ( Utility.Random( 5 ))		   
				{
					case 0: bSay = "Guards!"; break;
					case 1: bSay = "There will be no place for you to hide!"; break;
					case 2: bSay = "Noooo!"; break;
					case 3: bSay = "Vile rogue!"; break;
					case 4: bSay = "Aarrgh!"; break;
				};
			}

			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) );

			if ( !base.OnBeforeDeath() )
				return false;

			return true;
		}

		public override void OnAfterSpawn()
		{
			IntelligentAction.AssignMidlandRace( (BaseCreature)this );
			IntelligentAction.MidlandRace((BaseCreature)this);
			Dress();
			base.OnAfterSpawn();	
		}

		public MidlandVendor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version

			writer.Write( m_good1inventory);
			writer.Write( m_good1name);
			writer.Write( m_good1adjust);

			writer.Write( m_good2inventory);
			writer.Write( m_good2name);
			writer.Write( m_good2adjust);

			writer.Write( m_good3inventory);
			writer.Write( m_good3name);
			writer.Write( m_good3adjust);

			writer.Write( m_good4inventory);
			writer.Write( m_good4name);
			writer.Write( m_good4adjust);

			writer.Write(m_moneytype);


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_good1inventory = reader.ReadInt();
			m_good1name = reader.ReadString();
			m_good1adjust = reader.ReadDouble();
		
			m_good2inventory = reader.ReadInt();
			m_good2name = reader.ReadString();
			m_good2adjust = reader.ReadDouble();

			m_good3inventory = reader.ReadInt();
			m_good3name = reader.ReadString();
			m_good3adjust = reader.ReadDouble();

			m_good4inventory = reader.ReadInt();
			m_good4name = reader.ReadString();
			m_good4adjust = reader.ReadDouble();

			m_moneytype = reader.ReadInt();

			m_OneTimeType = 3;

			AdjustPrice();
		}
	}
} 
