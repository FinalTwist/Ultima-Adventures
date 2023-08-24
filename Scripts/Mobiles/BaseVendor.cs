using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Misc;
using Server.Engines.BulkOrders;
using Server.Regions;
using Server.Custom;
using Server.Multis;

namespace Server.Mobiles
{
	public enum VendorShoeType
	{
		None,
		Shoes,
		Boots,
		Sandals,
		ThighBoots
	}

	public abstract class BaseVendor : BaseConvo, IVendor
	{
		#region Make Vendors Talk
        private static bool m_Talked;
        string[] VendorSay = new string[] 
		{ 
			"Greetings",
            "Hello there",
            "I have what ye needs.",
            "Look here!",
            "Shop ye here!",
			"Hey there what can I do for you?",
			"I might have that thing you were looking for...",
			"Ahoy!",
			"Best prices... I think",
			"yes?",
			"Quick, I'm busy",
			"People used to say Vendor this and that... I have a name",
			"My inventory refreshed just now!",
			"Welcome to my shoppe",
			"*Grumble*",
			"Weather's acting up lately",
			"Nice armor, is it for sale?"
		};

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false && !(Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Doom Gauntlet") && !(Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "Doom")  )
            {
                if (m.InRange(this, 3) && m is PlayerMobile && !m.Hidden && m.Alive)
                {
                    m_Talked = true;

					if (this is Carpenter && Map == Map.Trammel)
					{
						string sRegion = Worlds.GetMyRegion( Map, Location );
						if (sRegion == "the City of Britain")
						{
							if (Utility.RandomBool())
								Say("Help Sire!  Someone keeps stealing my SawMill!");
							else
								Say("Welp!  There's a Sawmill thief around!");
						}
					}
					else
                    	SayRandom(VendorSay, this);
                    this.Move(GetDirectionTo(m.Location));
                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }
        }

        private class SpamTimer : Timer
        {
            public SpamTimer()
                : base(TimeSpan.FromMinutes(10))
            {
                Priority = TimerPriority.OneMinute;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }
		
        #endregion
		private const int MaxSell = 1000;

		protected abstract List<SBInfo> SBInfos { get; }

		private ArrayList m_ArmorBuyInfo = new ArrayList();
		private ArrayList m_ArmorSellInfo = new ArrayList();

		private DateTime m_LastRestock;

		private bool m_pricesadjusted;

		private bool m_sellingpriceadjusted;

		public override bool CanTeach { get { return true; } }

		public override bool BardImmune { get { return false; } }

		public override bool PlayerRangeSensitive { get { return true; } }

		public virtual bool IsActiveVendor { get { return true; } }
		public virtual bool IsActiveBuyer { get { return IsActiveVendor; } } // response to vendor SELL
		public virtual bool IsActiveSeller { get { return IsActiveVendor; } } // repsonse to vendor BUY

		public virtual NpcGuild NpcGuild { get { return NpcGuild.None; } }

		public virtual bool IsInvulnerable { get { return false; } }
		public override bool Unprovokable { get { return false; } }
		public override bool Uncalmable{ get{ return false; } }

		public override bool ShowFameTitle { get { return false; } }

		protected override void GetConvoFragments(ArrayList list)
		{
			list.Add( (int)JobFragment.shopkeep );
			base.GetConvoFragments (list);
		}


		public virtual bool IsValidBulkOrder( Item item )
		{
			return false;
		}

		public virtual Item CreateBulkOrder( Mobile from, bool fromContextMenu )
		{
			return null;
		}

		public virtual bool SupportsBulkOrders( Mobile from )
		{
			return false;
		}

		public virtual TimeSpan GetNextBulkOrder( Mobile from )
		{
			return TimeSpan.FromMinutes(10); // was .Zero
		}

		public virtual void OnSuccessfulBulkOrderReceive( Mobile from )
		{
		}

		public override void OnThink()
		{
			base.OnThink();

			if ( PlayersInSight() )
				this.CantWalk = true;
			else
				this.CantWalk = false;
			
		}

		private bool PlayersInSight()
		{
			int players = 0;
			bool enemies = false;

			foreach ( Mobile player in GetMobilesInRange( 6 ) )
			{
				if (player != null)
				{
					if ( player is PlayerMobile )
					{
						if ( CanSee( player ) && InLOS( player ) ){ ++players; }
					}

					if ( IsEnemy( player ) )
					{
						enemies = true;
						
						if (player.Criminal && !player.Hidden && player is PlayerMobile)
						{
							this.Combatant = player;
							return false;
						}
					}
				}
			}

			if ( players > 0 && !enemies )
				return true;

			return false;
		}

		#region Faction
		public virtual int GetPriceScalar()
		{
			return 100;
		}

		public void UpdateBuyInfo()
		{
			int priceScalar = GetPriceScalar();

			IBuyItemInfo[] buyinfo = (IBuyItemInfo[])m_ArmorBuyInfo.ToArray( typeof( IBuyItemInfo ) );

			if ( buyinfo != null )
			{
				foreach ( IBuyItemInfo info in buyinfo )
					info.PriceScalar = priceScalar;
			}
		}
		#endregion

		private class BulkOrderInfoEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private BaseVendor m_Vendor;

			public BulkOrderInfoEntry( Mobile from, BaseVendor vendor )
				: base( 6152 )
			{
				m_From = from;
				m_Vendor = vendor;
			}

			public override void OnClick()
			{
				if ( m_Vendor.SupportsBulkOrders( m_From ) )
				{
					TimeSpan ts = m_Vendor.GetNextBulkOrder( m_From );

					int totalSeconds = (int)ts.TotalSeconds;
					int totalHours = ( totalSeconds + 3599 ) / 3600;
					int totalMinutes = ( totalSeconds + 59 ) / 60;

					if ( ( ( Core.SE ) ? totalMinutes == 0 : totalHours == 0 ) )
					{
						m_From.SendLocalizedMessage( 1049038 ); // You can get an order now.

						if ( Core.AOS )
						{
							Item bulkOrder = m_Vendor.CreateBulkOrder( m_From, true );

							if ( bulkOrder is LargeBOD )
								m_From.SendGump( new LargeBODAcceptGump( m_From, (LargeBOD)bulkOrder ) );
							else if ( bulkOrder is SmallBOD )
								m_From.SendGump( new SmallBODAcceptGump( m_From, (SmallBOD)bulkOrder ) );
						}
					}
					else
					{
						int oldSpeechHue = m_Vendor.SpeechHue;
						m_Vendor.SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

							m_Vendor.SayTo( m_From, 1072058, totalMinutes.ToString() ); // An offer may be available in about ~1_minutes~ minutes.

						m_Vendor.SpeechHue = oldSpeechHue;
					}
				}
			}
		}

		public BaseVendor( string title ) : base( AIType.AI_Vendor, FightMode.Closest, 15, 1, 0.1, 0.2 ) // WIZARD
		{
			LoadSBInfo();

			this.Title = title;

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			InitBody();
			InitOutfit();

			Container pack;
			//these packs MUST exist, or the client will crash when the packets are sent
			pack = new Backpack();
			pack.Layer = Layer.ShopBuy;
			pack.Movable = false;
			pack.Visible = false;
			AddItem( pack );

			pack = new Backpack();
			pack.Layer = Layer.ShopResale;
			pack.Movable = false;
			pack.Visible = false;
			AddItem( pack );

			m_LastRestock = DateTime.UtcNow;
			LoadSBInfo();

				SetStr(50, 200);
				SetDex(30, 150);
				SetInt(50, 180);

				SetHits(100, 300);

				SetDamage(5, 35);

				SetDamageType(ResistanceType.Physical, 100);

				SetResistance(ResistanceType.Physical, 30, 70);
				SetResistance(ResistanceType.Fire, 30, 70);
				SetResistance(ResistanceType.Cold, 30, 70);
				SetResistance(ResistanceType.Poison, 30, 70);
				SetResistance(ResistanceType.Energy, 30, 70);

				SetSkill(SkillName.Swords, 50.0, 100.0);
				SetSkill(SkillName.Fencing, 50.0, 100.0);
				SetSkill(SkillName.Macing, 50.0, 100.0);
				SetSkill(SkillName.Tactics, 50.0, 100.0);				
				SetSkill(SkillName.Tactics, 50.0, 100.0);
				SetSkill(SkillName.MagicResist, 50.0, 100.0);
				SetSkill(SkillName.Tactics, 50.0, 100.0);
				SetSkill(SkillName.Parry, 50.0, 100.0);
				SetSkill(SkillName.Anatomy, 50.0, 100.0);
				SetSkill(SkillName.Healing, 50.0, 100.0);
				SetSkill(SkillName.Magery, 50.0, 100.0);
				SetSkill(SkillName.EvalInt, 50.0, 100.0);
				SetSkill(SkillName.DetectHidden, 10.0, 100.0);

				m_pricesadjusted = false;
				m_sellingpriceadjusted = false;

	   //if (Utility.Random(1, 2) == 2) // 50% chance to have an OmniAI skill set
		//OmniAI.SetRandomSkillSet(this, 70.0, 110.0);
		}

		public BaseVendor( Serial serial ): base( serial )
		{
		}
/*
		// + OmniAI support +
		protected override BaseAI ForcedAI
		{
			get
			{
			return new OmniAI(this);
			}
		}
		// - OmniAI support -
*/
		public static int BeggingPose( Mobile from ) // LET US SEE IF THEY ARE BEGGING
		{
			int beggar = 0;
			if ( from is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

				if ( DB.CharacterBegging > 0 )
				{
					beggar = (int)from.Skills[SkillName.Begging].Value;
				}
			}
			return beggar;
		}
		
		public static int BeggingKarma( Mobile from ) // LET US SEE IF THEY ARE BEGGING
		{
			int charisma = 0;
			if ( from.Karma > -2459 ){ charisma = 40; }
			//from.CheckSkill( SkillName.Begging, 0, 100 );
			return charisma;
		}

		public static string BeggingWords() // LET US SEE IF THEY ARE BEGGING
		{
			string sSpeak = "Please give me a good price as I am so poor.";
			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: sSpeak = "Please give me a good price as I am so poor."; break;
				case 1: sSpeak = "I have very little gold so whatever you can give..."; break;
				case 2: sSpeak = "I have not eaten in days so your gold will surely help."; break;
				case 3: sSpeak = "Will thou give a poor soul more for these?"; break;
				case 4: sSpeak = "I have fallen on hard times, will thou be kind?"; break;
				case 5: sSpeak = "Whatever you can give for these will surely help."; break;
			}
			return sSpeak;
		}

		public DateTime LastRestock
		{
			get
			{
				return m_LastRestock;
			}
			set
			{
				m_LastRestock = value;
			}
		}

		public virtual TimeSpan RestockDelay
		{
			get
			{
				return TimeSpan.FromHours( 2 );
			}
		}

		public virtual TimeSpan RestockDelayFull
		{
			get
			{
				return TimeSpan.FromHours( 2 );
			}
		}

		public Container BuyPack
		{
			get
			{
				Container pack = FindItemOnLayer( Layer.ShopBuy ) as Container;

				if ( pack == null )
				{
					pack = new Backpack();
					pack.Layer = Layer.ShopBuy;
					pack.Visible = false;
					AddItem( pack );
				}

				return pack;
			}
		}

		public abstract void InitSBInfo();

		protected void LoadSBInfo()
		{
			m_LastRestock = DateTime.UtcNow;

			for ( int i = 0; i < m_ArmorBuyInfo.Count; ++i )
			{
				GenericBuyInfo buy = m_ArmorBuyInfo[i] as GenericBuyInfo;

				if ( buy != null )
					buy.DeleteDisplayEntity();
			}

			SBInfos.Clear();

			InitSBInfo();

			m_ArmorBuyInfo.Clear();
			m_ArmorSellInfo.Clear();

			for ( int i = 0; i < SBInfos.Count; i++ )
			{
				SBInfo sbInfo = (SBInfo)SBInfos[i];
				m_ArmorBuyInfo.AddRange( sbInfo.BuyInfo ); //+++ Can change price here
				m_ArmorSellInfo.Add( sbInfo.SellInfo );
			}
		}

		public virtual bool GetGender()
		{
			return Utility.RandomBool();
		}

		public virtual void InitBody()
		{
			InitStats( 100, 100, 25 );

			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( IsInvulnerable && !Core.AOS )
				NameHue = 0x35;

			if ( this is Roscoe || this is Garth )
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}
			else if ( this is KungFu )
			{
				if ( Female = GetGender() )
				{
					Body = 0x191;
					Name = NameList.RandomName( "tokuno female" );
				}
				else
				{
					Body = 0x190;
					Name = NameList.RandomName( "tokuno male" );
				}
			}
			else if ( Female = GetGender() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}
		}

		public virtual int GetRandomHue()
		{
			switch ( Utility.Random( 5 ) )
			{
				default:
				case 0: return Utility.RandomBlueHue();
				case 1: return Utility.RandomGreenHue();
				case 2: return Utility.RandomRedHue();
				case 3: return Utility.RandomYellowHue();
				case 4: return Utility.RandomNeutralHue();
			}
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

		public virtual int RandomBrightHue()
		{
			if ( 0.1 > Utility.RandomDouble() )
				return Utility.RandomList( 0x62, 0x71 );

			return Utility.RandomList( 0x03, 0x0D, 0x13, 0x1C, 0x21, 0x30, 0x37, 0x3A, 0x44, 0x59 );
		}

		public override void OnAfterSpawn()
		{
			if (this.Home.X == 0 && this.Home.Y == 0 && this.Home.Z == 0)
			    this.Home = this.Location;

			Region reg = Region.Find( this.Location, this.Map );

			if ( !Server.Items.EssenceBase.ColorCitizen( this ) )
			{
				Server.Misc.MorphingTime.CheckMorph( this );
			}

			Server.Mobiles.PremiumSpawner.SpreadOut( this );

			if (AdventuresFunctions.IsPuritain((object)this))
			{
				if ( reg.Name == "Midkemia"  )
				{
					((BaseCreature)this).midrace = 1;
				}
				if ( reg.Name == "Calypso"  )
				{
					((BaseCreature)this).midrace = 4;	
				}	
				IntelligentAction.MidlandRace(this);			
			}

			if ( ( reg.Name == "the Dungeon Room" || reg.Name == "the Camping Tent" ) && this is Provisioner )
			{
				this.Title = "the merchant";
			}
			else if ( reg.Name == "the Forgotten Lighthouse" || reg.Name == "Savage Sea Docks" || reg.Name == "Serpent Sail Docks" || reg.Name == "Anchor Rock Docks" || reg.Name == "Kraken Reef Docks" || reg.Name == "the Port" )
			{
				if ( this is Provisioner && reg.Name != "the Port" ){ this.Title = "the dock worker"; if ( Utility.RandomBool() ){ this.Title = "the merchant"; } }
				else if ( this is Fisherman && reg.Name != "the Port" ){ this.Title = "the sailor"; }
				else if ( this is Carpenter && reg.Name != "the Port" ){ this.Title = "the cooper"; }
				else if ( this is Waiter ){ this.Title = "the cabin boy"; if ( this.Female ){ this.Title = "the serving wench"; } }
				else if ( this is Weaponsmith && reg.Name != "the Port" ){ this.Title = "the master-at-arms"; }
				else if ( this is Ranger )
				{
					this.Title = "the harpooner";
					if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.FindItemOnLayer( Layer.OneHanded ).Delete(); }
					if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
					if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
					if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
					if ( Utility.RandomBool() ){ this.AddItem( new SkullCap( Utility.RandomDyedHue() ) ); }
					this.AddItem( new Harpoon() );
				}
				else if ( this is Shipwright && reg.Name != "the Port" ){ this.Title = "the boatswain"; }

				if ( !(this is Shipwright && reg.Name == "the Port" ) )
				{
					if ( !(this is Jester) && !(this is Druid) && !(this is VarietyDealer) )
					{
						if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
						if ( Utility.RandomBool() ){ this.AddItem( new SkullCap() ); }
						else { this.AddItem( new Bandana() ); }
						MorphingTime.SailorMyClothes( this );
					}
				}
			}
			else if ( reg.Name == "the Thieves Guild" && this is Provisioner )
			{
				this.Title = "the fence";
			}
			else if ( reg.Name == "the Ship's Lower Deck" && !(this is Jester) )
			{
				if ( this is Provisioner  ){ this.Title = "the quartermaster"; }
				else if ( this is Waiter ){ this.Title = "the cabin boy"; if ( this.Female ){ this.Title = "the serving wench"; } }
				if ( this.FindItemOnLayer( Layer.Helm ) != null ) { this.FindItemOnLayer( Layer.Helm ).Delete(); }
				if ( Utility.RandomBool() ){ this.AddItem( new SkullCap() ); }
				else { this.AddItem( new Bandana() ); }
				MorphingTime.SailorMyClothes( this );
			}
			else if ( reg.Name == "the Wizards Guild" && this is Waiter && this.Body == 400 )
			{
				this.Title = "the butler";
			}
			else if ( reg.Name == "the Wizards Guild" && this is Waiter && this.Body == 401 )
			{
				this.Title = "the maid";
			}
			else if ( reg.Name == "The Pit" )
			{
				this.Kills = Utility.RandomMinMax(5, 10);
				this.Karma = -500;
			}		
			else if ( reg.Name == "the Ruins of Tenebrae")
			{ 
				this.Hue = Utility.RandomMinMax( 496, 510 );
				if (this.Female)
					this.Body = 606;
				else
					this.Body = 605;
				
				if ( this.FindItemOnLayer( Layer.Shirt ) != null ) { this.FindItemOnLayer( Layer.Shirt ).Delete(); }
				if ( this.FindItemOnLayer( Layer.InnerTorso ) != null ) { this.FindItemOnLayer( Layer.InnerTorso ).Delete(); }
			}
			else if ( reg.Name == "the Temple of Praetoria")
			{ 
				this.Hue = 1489 ;
				if (this.Female)
					this.Body = 606;
				else
					this.Body = 605;
				this.Karma = -1;
				
				if ( this.FindItemOnLayer( Layer.Shirt ) != null ) { this.FindItemOnLayer( Layer.Shirt ).Delete(); }
				if ( this.FindItemOnLayer( Layer.InnerTorso ) != null ) { this.FindItemOnLayer( Layer.InnerTorso ).Delete(); }
			}
			
			base.OnAfterSpawn();
		}

		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );

			if ( !Server.Items.EssenceBase.ColorCitizen( this ) )
			{
				Server.Misc.MorphingTime.CheckMorph( this );
			}

			LoadSBInfo();
		}

		public virtual int GetHairHue()
		{
			return Utility.RandomHairHue();
		}

		public virtual void InitOutfit()
		{
			switch ( Utility.Random( 3 ) )
			{
				case 0: AddItem( new FancyShirt( GetRandomHue() ) ); break;
				case 1: AddItem( new Doublet( GetRandomHue() ) ); break;
				case 2: AddItem( new Shirt( GetRandomHue() ) ); break;
			}

			switch ( ShoeType )
			{
				case VendorShoeType.Shoes: AddItem( new Shoes( GetShoeHue() ) ); break;
				case VendorShoeType.Boots: AddItem( new Boots( GetShoeHue() ) ); break;
				case VendorShoeType.Sandals: AddItem( new Sandals( GetShoeHue() ) ); break;
				case VendorShoeType.ThighBoots: AddItem( new ThighBoots( GetShoeHue() ) ); break;
			}

			int hairHue = GetHairHue();

			Utility.AssignRandomHair( this, hairHue );
			Utility.AssignRandomFacialHair( this, hairHue );

			if ( Female )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: AddItem( new ShortPants( GetRandomHue() ) ); break;
					case 1:
					case 2: AddItem( new Kilt( GetRandomHue() ) ); break;
					case 3:
					case 4:
					case 5: AddItem( new Skirt( GetRandomHue() ) ); break;
				}
			}
			else
			{
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				FacialHairHue = hairHue;

				switch ( Utility.Random( 2 ) )
				{
					case 0: AddItem( new LongPants( GetRandomHue() ) ); break;
					case 1: AddItem( new ShortPants( GetRandomHue() ) ); break;
				}
			}

				int money1 = 30;
				int money2 = 120;

				double w1 = money1 * (MyServerSettings.GetGoldCutRate( this, null ) * .01);
				double w2 = money2 * (MyServerSettings.GetGoldCutRate( this, null ) * .01);

				money1 = (int)w1;
				money2 = (int)w2;

			PackGold( money1, money2 );
		}

		public virtual void Restock()
		{
			m_LastRestock = DateTime.UtcNow;

			LoadSBInfo();

			Container cont = this.Backpack;
			if ( cont != null )
			{
				Gold m_Gold = (Gold)this.Backpack.FindItemByType( typeof( Gold ) );
				int m_Amount = this.Backpack.GetAmount( typeof( Gold ) );
				cont.ConsumeTotal( typeof( Gold ), m_Amount );

					int money1 = 30;
					int money2 = 120;

					double w1 = money1 * (MyServerSettings.GetGoldCutRate( this, null ) * .01);
					double w2 = money2 * (MyServerSettings.GetGoldCutRate( this, null ) * .01);

					money1 = (int)w1;
					money2 = (int)w2;

				this.PackGold( money1, money2 );
			}
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.MorphingTime.TurnToSomethingOnDeath( this );

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
				Region reg = Region.Find( this.Location, this.Map );
				if (reg.Name == "The Pit") // no one gets kills from vendors in the pit
				{
				}
				else if ( this.Map == Map.Ilshenar && this.X <= 1007 && this.Y <= 1280) // darkmoor
				{
					if (this.Karma < 0 ) // evil killing evil vendors in darkmoor
					{
						killer.Criminal = true;
						killer.Kills = killer.Kills + 1;
						Titles.AwardKarma( killer, 300, true );
						Server.Items.DisguiseTimers.RemoveDisguise( killer );
					}
					
				}
				else
				{
					killer.Criminal = true;
					killer.Kills = killer.Kills + 1;
					Titles.AwardKarma( killer, -(300), true );
					Server.Items.DisguiseTimers.RemoveDisguise( killer );
				}


			}

			if ( !base.OnBeforeDeath() )
				return false;

			string bSay = "Help! Guards!";
			this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( bSay ) ); 

			return true;
		}

		public override bool IsEnemy( Mobile m )
		{
			if (m.Criminal);
				return true;

			if ( IntelligentAction.GetMyEnemies( m, this, true ) == false )
				return false;

			if ( m.Region != this.Region && !(m is PlayerMobile) )
				return false;

		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			switch ( Utility.Random( 4 ))		   
			{
				case 0: Say("Leave this place!"); break;
				case 1: Say("" + defender.Name + ", we have heard of you!"); break;
				case 2: Say("We have been told to watch for you, " + defender.Name + "!"); break;
				case 3: Say("Guards, " + defender.Name + " is here!"); break;
			};
		}

		private static TimeSpan InventoryDecayTime = TimeSpan.FromHours( 2.0 );

		public int AdjustPrices( int money, bool good)
		{

			int curse = AetherGlobe.VendorCurse;

			if (!good) // bad vendor, invert measurement
				curse = 100000 - curse;

			double pricemod = 1;
			
			if (curse < 10000)
			{
				pricemod = ( .55 * (double)money );
			}	
			else if (curse < 20000)
			{
				pricemod = ( .60 * (double)money );
			}	
			else if (curse < 30000)
			{
				pricemod = ( .65 * (double)money );
			}	
			else if (curse < 40000)
			{
				pricemod = ( .70 * (double)money );
			}		
			else if (curse < 50000)
			{
				pricemod = ( .75 * (double)money );
			}	
			else if (curse < 60000)
			{
				pricemod = ( 0.80 * (double)money );
			}	
			else if (curse < 70000)
			{
				pricemod = ( 0.85* (double)money );
			}	
			else if (curse < 80000)
			{
				pricemod = ( 0.90 * (double)money );
			}	
			else if (curse < 90000)
			{
				pricemod = ( 0.95 * (double)money );
			}	
			else if (curse < 100000)
			{
				pricemod = ( 1.00 * (double)money );
			}				
				
			if ( pricemod < 1 )
				pricemod = 1;

			return Convert.ToInt32(pricemod);
			
		}

		public static int AdjustAmount( int amount, bool good)
		{

			int curse = AetherGlobe.VendorCurse;

			if (!good) // bad vendor, invert
				curse = 100000 - curse;

			double pricemod = 1;
			
			if (curse < 10000)
			{
				pricemod = ( 1.45 * (double)amount );
			}	
			else if (curse < 20000)
			{
				pricemod = ( 1.35 * (double)amount );
			}	
			else if (curse < 30000)
			{
				pricemod = ( 1.25 * (double)amount );
			}	
			else if (curse < 40000)
			{
				pricemod = ( 1.15 * (double)amount );
			}		
			else if (curse < 50000)
			{
				pricemod = ( 1.05 * (double)amount );
			}	
			else if (curse < 60000)
			{
				pricemod = ( 0.95 * (double)amount );
			}	
			else if (curse < 70000)
			{
				pricemod = ( 0.85 * (double)amount );
			}	
			else if (curse < 80000)
			{
				pricemod = ( 0.75 * (double)amount );
			}	
			else if (curse < 90000)
			{
				pricemod = ( 0.65 * (double)amount );
			}	
			else if (curse < 100000)
			{
				pricemod = ( 0.55 * (double)amount );
			}				
				
			if ( pricemod < 1 )
				pricemod = 1;

			return Convert.ToInt32(pricemod);
			
		}

		public virtual void VendorBuy( Mobile from )
		{
			if ( !IsActiveSeller )
				return;

			if ( !from.CheckAlive() )
				return;

			if ( !CheckVendorAccess( from ) )
			{
				//Say( 501522 ); // I shall not treat with scum like thee!
				this.Say( "I have no business with you." );
				return;
			}

			if (AdventuresFunctions.IsPuritain((object)this))
			{
				this.Say( "Apologies but I don't sell anything M'lord." );
				return;
			}	

			if (	DateTime.UtcNow - m_LastRestock > RestockDelay || 
					( from.Region.IsPartOf( typeof( PublicRegion ) ) && DateTime.UtcNow - m_LastRestock > RestockDelayFull ) || 
					( this is BaseGuildmaster && DateTime.UtcNow - m_LastRestock > RestockDelayFull ) 
			)
			
			Restock();

			UpdateBuyInfo();

			int count = 0;
			List<BuyItemState> list; //+++
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			IShopSellInfo[] sellInfo = this.GetSellInfo();

            list = new List<BuyItemState>( buyInfo.Length );
			Container cont = this.BuyPack;

			List<ObjectPropertyList> opls = null;

			for ( int idx = 0; idx < buyInfo.Length; idx++ )
			{
				IBuyItemInfo buyItem = (IBuyItemInfo)buyInfo[idx];

				if ( buyItem.Amount <= 0 || list.Count >= 250 )
					continue;

				// NOTE: Only GBI supported; if you use another implementation of IBuyItemInfo, this will crash
				GenericBuyInfo gbi = (GenericBuyInfo)buyItem;
				IEntity disp = gbi.GetDisplayEntity();

				int money = buyItem.Price;
				int amount = buyItem.Amount;

				if (!m_pricesadjusted)
				{
					if (this.Karma > 0 ) //good vendor
						money = AdjustPrices( money, true);
					else if (this.Karma < 0 )
						money = AdjustPrices( money, false);
					else if (this.Karma == 0)
					{
						if (from.Karma >= 0)
							money = AdjustPrices( money, true);
						else
							money = AdjustPrices( money, false);
					}

					buyItem.Price = money;

					
					if (this.Karma > 0 ) //good vendor
						amount = AdjustAmount( amount, true);
					else if (this.Karma < 0 )
						amount = AdjustAmount( amount, false);
					else if (this.Karma == 0)
					{
						if (from.Karma >= 0)
							amount = AdjustAmount( amount, true);
						else
							amount = AdjustAmount( amount, false);
					}

					buyItem.Amount = amount;

					m_pricesadjusted = true;
				}

				list.Add( new BuyItemState( buyItem.Name, cont.Serial, disp == null ? (Serial)0x7FC0FFEE : disp.Serial, buyItem.Price, buyItem.Amount, buyItem.ItemID, buyItem.Hue ) );
				count++;

				if ( opls == null ) {
					opls = new List<ObjectPropertyList>();
				}

				if ( disp is Item ) {
					opls.Add( ( ( Item ) disp ).PropertyList );
				} else if ( disp is Mobile ) {
					opls.Add( ( ( Mobile ) disp ).PropertyList );
				}
			}

			List<Item> playerItems = cont.Items;

			for ( int i = playerItems.Count - 1; i >= 0; --i )
			{
				if ( i >= playerItems.Count )
					continue;

				Item item = playerItems[i];

				if ( ( item.LastMoved + InventoryDecayTime ) <= DateTime.UtcNow )
					item.Delete();
			}

			for ( int i = 0; i < playerItems.Count; ++i )
			{
				Item item = playerItems[i];

				int price = 0;
				string name = null;

				foreach ( IShopSellInfo ssi in sellInfo )
				{
					if ( ssi.IsSellable( item ) )
					{
						price = ssi.GetBuyPriceFor( item );
						name = ssi.GetNameFor( item );
						break;
					}
				}

				if ( name != null && list.Count < 250 )
				{
					list.Add( new BuyItemState( name, cont.Serial, item.Serial, price, item.Amount, item.ItemID, item.Hue ) );
					count++;

					if ( opls == null ) {
						opls = new List<ObjectPropertyList>();
					}

					opls.Add( item.PropertyList );
				}
			}

			//one (not all) of the packets uses a byte to describe number of items in the list.  Osi = dumb.
			//if ( list.Count > 255 )
			//	Console.WriteLine( "Vendor Warning: Vendor {0} has more than 255 buy items, may cause client errors!", this );

			if ( list.Count > 0 )
			{
				list.Sort( new BuyItemStateComparer() );

				SendPacksTo( from );

				NetState ns = from.NetState;

				if ( ns == null )
					return;

				if ( ns.ContainerGridLines )
					from.Send( new VendorBuyContent6017( list ) );
				else
					from.Send( new VendorBuyContent( list ) );

				from.Send( new VendorBuyList( this, list ) );

				if ( ns.HighSeas )
					from.Send( new DisplayBuyListHS( this ) );
				else
					from.Send( new DisplayBuyList( this ) );

				from.Send( new MobileStatusExtended( from ) );//make sure their gold amount is sent

				if ( opls != null ) {
					for ( int i = 0; i < opls.Count; ++i ) {
						from.Send( opls[i] );
					}
				}

				SayTo( from, 500186 ); // Greetings.  Have a look around.
			}
		}

		public virtual void SendPacksTo( Mobile from )
		{
			Item pack = FindItemOnLayer( Layer.ShopBuy );

			if ( pack == null )
			{
				pack = new Backpack();
				pack.Layer = Layer.ShopBuy;
				pack.Movable = false;
				pack.Visible = false;
				AddItem( pack );
			}

			from.Send( new EquipUpdate( pack ) );

			pack = FindItemOnLayer( Layer.ShopSell );

			if ( pack != null )
				from.Send( new EquipUpdate( pack ) );

			pack = FindItemOnLayer( Layer.ShopResale );

			if ( pack == null )
			{
				pack = new Backpack();
				pack.Layer = Layer.ShopResale;
				pack.Movable = false;
				pack.Visible = false;
				AddItem( pack );
			}

			from.Send( new EquipUpdate( pack ) );
		}

		public virtual void VendorSell( Mobile from )
		{
			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
			{
				from.Say( BeggingWords() );
			}

			if ( !IsActiveBuyer )
				return;

			if ( !from.CheckAlive() )
				return;

			if ( !CheckVendorAccess( from ) )
			{
				//Say( 501522 ); // I shall not treat with scum like thee!
				this.Say( "I have no business with you." );
				return;
			}

			if (AdventuresFunctions.IsPuritain((object)this))
			{
				this.Say( "Apologies but I don't sell anything M'lord." );
				return;
			}				

			Container pack = from.Backpack;

			if ( pack != null )
			{
				IShopSellInfo[] info = GetSellInfo();

				//Hashtable table = new Hashtable();
				Dictionary<Item, SellItemState> table = new Dictionary<Item, SellItemState>();

				foreach ( IShopSellInfo ssi in info )
				{
					Item[] items = pack.FindItemsByType( ssi.Types );

					foreach ( Item item in items )
					{
						LockableContainer parentcon = item.ParentEntity as LockableContainer;

						if ( item is Container && ( (Container)item ).Items.Count != 0 )
							continue;

						if ( parentcon != null && parentcon.Locked == true )
							continue;

						if ( item.IsStandardLoot() && item.Movable && ssi.IsSellable( item ) )
						{
							PlayerMobile pm = (PlayerMobile)from;

							int barter = (int)from.Skills[SkillName.ItemID].Value;

							int GuildMember = 0;

							if ( barter < 100 && this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ barter = 100; GuildMember = 1; } // FOR GUILD MEMBERS

							if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
							{
								Titles.AwardKarma( from, -BeggingKarma( from ), true );
								barter = (int)from.Skills[SkillName.Begging].Value;
							}

							//this.Say("getsellprice is "+ssi.GetSellPriceFor( item, barter ));
							int money = ssi.GetSellPriceFor( item, barter );

							if (!m_sellingpriceadjusted)
							{
								if (this.Karma > 0 ) //good vendor
									money = AdjustPrices( money, true);
								else if (this.Karma < 0 )
									money = AdjustPrices( money, false);
								else if (this.Karma == 0)
								{
									if (from.Karma >= 0)
										money = AdjustPrices( money, true);
									else
										money = AdjustPrices( money, false);
								}

								m_sellingpriceadjusted = true;
							}

							table[item] = new SellItemState( item, money, ssi.GetNameFor( item ) ); // +++ getsellprice needs to be changed
						}
					}
				}

				if ( table.Count > 0 )
				{
					SendPacksTo( from );

					from.Send(new VendorSellList(this, table.Values));
				}
				else
				{
					Say( true, "You have nothing I would be interested in." );
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( from.Blessed )
			{
				string sSay = "I cannot deal with you while you are in that state.";
				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sSay, from.NetState);
				return false;
			}
			else if ( IntelligentAction.GetMyEnemies( from, this, false ) == true )
			{
				string sSay = "I don't think I should accept that from you.";
				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sSay, from.NetState);
				return false;
			}
			else if (from is PlayerMobile)
			{
				PlayerMobile pm = (PlayerMobile)from;

				int RelicValue = 0;

				if ( Server.Misc.RelicItems.RelicValue( dropped, this ) > 0 )
				{
					RelicValue = Server.Misc.RelicItems.RelicValue( dropped, this );
				}
				else if ( dropped is GolemManual && ( this is Tinker || this is TinkerGuildmaster ) )
				{
					Server.Items.GolemManual.ProcessGolemBook( from, this, dropped );
				}
				else if ( dropped is OrbOfTheAbyss && ( this is Tinker || this is TinkerGuildmaster ) )
				{
					Server.Items.OrbOfTheAbyss.ChangeOrb( from, this, dropped );
				}
				else if ( dropped is RobotSchematics && ( this is Tinker || this is TinkerGuildmaster ) )
				{
					Server.Items.RobotSchematics.ProcessRobotBook( from, this, dropped );
				}
				else if ( dropped is AlienEgg && ( this is AnimalTrainer || this is Veterinarian ) )
				{
					Server.Items.AlienEgg.ProcessAlienEgg( from, this, dropped );
				}
                else if (dropped is EarthyEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.EarthyEgg.ProcessEarthyEgg(from, this, dropped);
                }
                else if (dropped is CorruptedEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.CorruptedEgg.ProcessCorruptedEgg(from, this, dropped);
                }
                else if (dropped is FeyEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.FeyEgg.ProcessFeyEgg(from, this, dropped);
                }
                else if (dropped is PrehistoricEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.PrehistoricEgg.ProcessPrehistoricEgg(from, this, dropped);
                }
                else if (dropped is ReptilianEgg && (this is AnimalTrainer || this is Veterinarian))
                {
                    Server.Items.ReptilianEgg.ProcessReptilianEgg(from, this, dropped);
                }
                else if ( dropped is DragonEgg && ( this is AnimalTrainer || this is Veterinarian ) )
				{
					Server.Items.DragonEgg.ProcessDragonEgg( from, this, dropped );
				}
				else if ( dropped is DracolichSkull && ( this is NecromancerGuildmaster ) )
				{
					Server.Items.DracolichSkull.ProcessDracolichSkull( from, this, dropped );
				}
				else if ( dropped is DemonPrison && ( this is NecromancerGuildmaster || this is MageGuildmaster || this is Mage || this is NecroMage || this is Necromancer || this is Witches ) )
				{
					Server.Items.DemonPrison.ProcessDemonPrison( from, this, dropped );
				}
				else if (this is Waiter && dropped is CorpseItem)
				{
					string corpsename = dropped.Name;
					if (Insensitive.Contains(corpsename, "coffee") || Insensitive.Contains(corpsename, "espresso") || Insensitive.Contains(corpsename, "coffee") || Insensitive.Contains(corpsename, "americano") || Insensitive.Contains(corpsename, "cappucino"))
					{
						dropped.Delete();
						Coffee cof = new Coffee();
						from.Backpack.DropItem(cof);
						this.Say("Ohhh... yes... this will brew well!");
						return true;
					}	
				}

				if ( RelicValue > 0 )
				{
					int GuildMember = 0;

					int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * RelicValue ) / 100), 0);

					if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.00 * RelicValue ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS

					if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
					{
						Titles.AwardKarma( from, -BeggingKarma( from ), true );
						from.Say( BeggingWords() );
						gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * RelicValue ) / 100), 0);
					}
					gBonus = gBonus + RelicValue;
					from.SendSound( 0x3D );
					from.AddToBackpack ( new Gold( gBonus ) );
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 0:	sMessage = "I have been looking for something like this. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 1:	sMessage = "I have heard of this item before. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 2:	sMessage = "I never thought I would see one of these. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 3:	sMessage = "I have never seen one of these. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 4:	sMessage = "What a rare item. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 5:	sMessage = "This is quite rare. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 6:	sMessage = "This will go nicely in my collection. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 7:	sMessage = "I have only heard tales about such items. Here is " + gBonus.ToString() + " gold for you.";		break;
						case 8:	sMessage = "How did you come across this? Here is " + gBonus.ToString() + " gold for you.";		break;
						case 9:	sMessage = "Where did you find this? Here is " + gBonus.ToString() + " gold for you.";		break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( dropped is Cargo )
				{
					Server.Items.Cargo.GiveCargo( (Cargo)dropped, this, from );
				}
				else if ( dropped is Museums && this is VarietyDealer )
				{
					Server.Items.Museums.GiveAntique( (Museums)dropped, this, from );
				}
				else if ( Server.Multis.BaseBoat.isRolledCarpet( dropped ) && ( this is Tailor || this is TailorGuildmaster ) )
				{
					Item carpet = null;

					if ( dropped is MagicDockedCarpetA || dropped is MagicCarpetADeed ){ carpet = new MagicCarpetBDeed(); }
					else if ( dropped is MagicDockedCarpetB || dropped is MagicCarpetBDeed ){ carpet = new MagicCarpetCDeed(); }
					else if ( dropped is MagicDockedCarpetC || dropped is MagicCarpetCDeed ){ carpet = new MagicCarpetDDeed(); }
					else if ( dropped is MagicDockedCarpetD || dropped is MagicCarpetDDeed ){ carpet = new MagicCarpetEDeed(); }
					else if ( dropped is MagicDockedCarpetE || dropped is MagicCarpetEDeed ){ carpet = new MagicCarpetFDeed(); }
					else if ( dropped is MagicDockedCarpetF || dropped is MagicCarpetFDeed ){ carpet = new MagicCarpetGDeed(); }
					else if ( dropped is MagicDockedCarpetG || dropped is MagicCarpetGDeed ){ carpet = new MagicCarpetHDeed(); }
					else if ( dropped is MagicDockedCarpetH || dropped is MagicCarpetHDeed ){ carpet = new MagicCarpetIDeed(); }
					else if ( dropped is MagicDockedCarpetI || dropped is MagicCarpetIDeed ){ carpet = new MagicCarpetADeed(); }

					dropped.Delete();
					carpet.Hue = dropped.Hue;
					from.AddToBackpack( carpet );
					SayTo(from, "I altered your magic carpet.");
					Effects.PlaySound(from.Location, from.Map, 0x248);
				}
				else if ( dropped is Gold && this is Mapmaker )
				{
					if ( dropped.Amount == 1000 )
					{
						if ( from.Map == Map.Trammel && from.X>5124 && from.Y>3041 && from.X<6147 && from.Y<4092 )
							from.AddToBackpack ( new WorldMapAmbrosia() );
						else if ( from.Map == Map.Trammel && from.X>859 && from.Y>3181 && from.X<2133 && from.Y<4092 )
							from.AddToBackpack ( new WorldMapUmber() );
						else if ( from.Map == Map.Malas && from.X<1870 )
							from.AddToBackpack ( new WorldMapSerpent() );
						else if ( from.Map == Map.Tokuno )
							from.AddToBackpack ( new WorldMapIslesOfDread() );
						else if ( from.Map == Map.TerMur && from.X>132 && from.Y>4 && from.X<1165 && from.Y<1798 )
							from.AddToBackpack ( new WorldMapSavage() );
						else if ( from.Map == Map.Trammel && from.X<6125 && from.Y<824 && from.X<7175 && from.Y<2746 )
							from.AddToBackpack ( new WorldMapBottle() );
						else if ( from.Map == Map.Felucca && from.X<5420 && from.Y<4096)
							from.AddToBackpack ( new WorldMapLodor() );
						else
							from.AddToBackpack ( new WorldMapSosaria() );

						string sMessage = "Thank you. Here is your world map.";
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					return false;
				}
				else if ( dropped is DugUpCoal && this is Blacksmith && Server.Misc.Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Savaged Empire" )
				{
					string sMessage = "";

					if ( !(Server.Items.DugUpCoal.CheckForDugUpCoal( from, dropped.Amount, false ) ) )
					{
						sMessage = "You don't have enought iron ore for me to make steel from this.";
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
					}
					else
					{
						sMessage = "Let's turn this into bars of steel you can use.";
						Server.Items.DugUpCoal.CheckForDugUpCoal( from, dropped.Amount, true );
						from.AddToBackpack ( new SteelIngot( dropped.Amount ) );
						dropped.Delete();
						from.PlaySound( 0x208 );
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						return true;
					}

					return false;
				}
				else if ( dropped is DugUpZinc && this is Blacksmith && Server.Misc.Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) == "the Island of Umber Veil" )
				{
					string sMessage = "";

					if ( !(Server.Items.DugUpZinc.CheckForDugUpZinc( from, dropped.Amount, false ) ) )
					{
						sMessage = "You don't have enought iron ore for me to make brass from this.";
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
					}
					else
					{
						sMessage = "Let's turn this into bars of brass you can use.";
						Server.Items.DugUpZinc.CheckForDugUpZinc( from, dropped.Amount, true );
						from.AddToBackpack ( new BrassIngot( dropped.Amount ) );
						dropped.Delete();
						from.PlaySound( 0x208 );
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						return true;
					}

					return false;
				}
				else if (	( dropped is DDCopper || dropped is DDSilver ) && 
							( this is Minter || this is Banker )	)
				{
					int nRate = 5;
					string sCoin = "silver";
					if ( dropped is DDCopper ){ nRate = 10; sCoin = "copper";}

					int nCoins = dropped.Amount;
					int nGold = (int)Math.Floor((decimal)(dropped.Amount / nRate));
					int nChange = dropped.Amount - ( nGold * nRate );

					string sMessage = "Sorry, you do not have enough here to exchange for even a single gold coin.";

					if ( ( nGold > 0 ) && ( nChange > 0 ) )
					{
						sMessage = "Here is " + nGold.ToString() + " gold for you, and " + nChange.ToString() + " " + sCoin + " back in change.";
						from.AddToBackpack ( new Gold( nGold ) );
					}
					else if ( nGold > 0 )
					{
						sMessage = "Here is " + nGold.ToString() + " gold for you.";
						from.AddToBackpack ( new Gold( nGold ) );
					}

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					if ( ( nChange > 0 ) && ( dropped is DDCopper ) ){ from.AddToBackpack ( new DDCopper( nChange ) ); }
					else if ( ( nChange > 0 ) && ( dropped is DDSilver ) ){ from.AddToBackpack ( new DDSilver( nChange ) ); }

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDXormite ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * 3;

					string sMessage = "Here is " + nGold.ToString() + " gold for you.";
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is Crystals ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * 5;

					string sMessage = "Here is " + nGold.ToString() + " gold for you.";
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDJewels ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * 2;

					string sMessage = "Here is " + nGold.ToString() + " gold for you.";
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDGemstones ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount * 2;

					string sMessage = "Here is " + nGold.ToString() + " gold for you.";
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( ( dropped is DDGoldNuggets ) && ( this is Minter || this is Banker ) )
				{
					int nGold = dropped.Amount;

					string sMessage = "Here is " + nGold.ToString() + " gold for you.";
					from.AddToBackpack ( new Gold( nGold ) );

					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					dropped.Delete();
					return true;
				}
				else if ( dropped is BottleOfParts && ( this is Alchemist || this is Witches ) )
				{
					int GuildMember = 0;

					int iForensics = (int)from.Skills[SkillName.Forensics].Value / 3;
					int iAnatomy = (int)from.Skills[SkillName.Anatomy].Value / 3;
					int nBottle = Utility.RandomMinMax( 2, 10 ) + Utility.RandomMinMax( 0, iForensics ) + Utility.RandomMinMax( 0, iAnatomy );

					int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * nBottle ) / 100), 0);

					if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.0 * nBottle ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS

					if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
					{
						Titles.AwardKarma( from, -BeggingKarma( from ), true );
						from.Say( BeggingWords() );
						gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * nBottle ) / 100), 0);
					}
					gBonus = (gBonus + nBottle) * dropped.Amount;
					from.AddToBackpack ( new Gold( gBonus ) );
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 0:	sMessage = "Hmmmm...I needed some of this. Here is " + gBonus.ToString() + " gold for you.";						break;
						case 1:	sMessage = "I'll take that. Here is " + gBonus.ToString() + " gold for you.";										break;
						case 2:	sMessage = "I assume this is fresh? Here is " + gBonus.ToString() + " gold for you.";								break;
						case 3:	sMessage = "You are better than some of the undertakers I know. Here is " + gBonus.ToString() + " gold for you.";	break;
						case 4:	sMessage = "This is a good bottle you found here. Here is " + gBonus.ToString() + " gold for you.";					break;
						case 5:	sMessage = "Keep this up and my lab will be stocked. Here is " + gBonus.ToString() + " gold for you.";				break;
						case 6:	sMessage = "How did you manage to get this bottle? Here is " + gBonus.ToString() + " gold for you.";				break;
						case 7:	sMessage = "You seem to be good with a surgeons knife. Here is " + gBonus.ToString() + " gold for you.";			break;
						case 8:	sMessage = "I have seen bottles like this before. Here is " + gBonus.ToString() + " gold for you.";					break;
						case 9:	sMessage = "I have never seen such a nice bottle of this before. Here is " + gBonus.ToString() + " gold for you.";	break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( this is ThiefGuildmaster && pm.NpcGuild == NpcGuild.ThievesGuild && ( // TOMB RAIDING
					dropped is RockArtifact || 
					dropped is SkullCandleArtifact || 
					dropped is BottleArtifact || 
					dropped is DamagedBooksArtifact || 
					dropped is StretchedHideArtifact || 
					dropped is BrazierArtifact || 
					dropped is LampPostArtifact || 
					dropped is BooksNorthArtifact || 
					dropped is BooksWestArtifact || 
					dropped is BooksFaceDownArtifact || 
					dropped is StuddedLeggingsArtifact || 
					dropped is EggCaseArtifact || 
					dropped is SkinnedGoatArtifact || 
					dropped is GruesomeStandardArtifact || 
					dropped is BloodyWaterArtifact || 
					dropped is TarotCardsArtifact || 
					dropped is BackpackArtifact || 
					dropped is StuddedTunicArtifact || 
					dropped is CocoonArtifact || 
					dropped is SkinnedDeerArtifact || 
					dropped is SaddleArtifact || 
					dropped is LeatherTunicArtifact || 
					dropped is RuinedPaintingArtifact ) )
				{
					int TombRaid = 8000;

					if ( dropped is RockArtifact || dropped is SkullCandleArtifact || dropped is BottleArtifact || dropped is DamagedBooksArtifact ){ TombRaid = 5000; }
					else if ( dropped is StretchedHideArtifact || dropped is BrazierArtifact ){ TombRaid = 6000; }
					else if ( dropped is LampPostArtifact || dropped is BooksNorthArtifact || dropped is BooksWestArtifact || dropped is BooksFaceDownArtifact ){ TombRaid = 7000; }
					else if ( dropped is StuddedTunicArtifact || dropped is CocoonArtifact ){ TombRaid = 9000; }
					else if ( dropped is SkinnedDeerArtifact ){ TombRaid = 10000; }
					else if ( dropped is SaddleArtifact || dropped is LeatherTunicArtifact ){ TombRaid = 11000; }
					else if ( dropped is RuinedPaintingArtifact ){ TombRaid = 12000; }

					from.AddToBackpack ( new Gold( TombRaid ) );
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 0:	sMessage = "Hmmmm...someone has been busy. Here is " + TombRaid.ToString() + " gold for you.";						break;
						case 1:	sMessage = "I'll take that. Here is " + TombRaid.ToString() + " gold for you.";										break;
						case 2:	sMessage = "I assume the traps were well avoided? Here is " + TombRaid.ToString() + " gold for you.";				break;
						case 3:	sMessage = "You are better than some of the thieves I have met. Here is " + TombRaid.ToString() + " gold for you.";	break;
						case 4:	sMessage = "This is a good one you stole here. Here is " + TombRaid.ToString() + " gold for you.";					break;
						case 5:	sMessage = "Keep this up and we will both be rich. Here is " + TombRaid.ToString() + " gold for you.";				break;
						case 6:	sMessage = "How did you manage to steal this one? Here is " + TombRaid.ToString() + " gold for you.";				break;
						case 7:	sMessage = "You seem to be avoiding the dangers out there. Here is " + TombRaid.ToString() + " gold for you.";		break;
						case 8:	sMessage = "I haven't seen one like this before. Here is " + TombRaid.ToString() + " gold for you.";				break;
						case 9:	sMessage = "Why earn when you can take? Here is " + TombRaid.ToString() + " gold for you.";							break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);

					Titles.AwardFame( from, TombRaid/25, true );
					//Titles.AwardKarma( from, -TombRaid/25, true );

					dropped.Delete();
					return true;
				}
				else if ( dropped is Item && this is Thief )
				{
					int GuildMember = 0;

					int iAmThief = (int)from.Skills[SkillName.Stealing].Value;

					if ( iAmThief < 10 ){ this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I only deal with fellow thieves.", from.NetState); }
					else if ( dropped is StealBox || dropped is StealMetalBox || dropped is StealBag )
					{
						int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * 500 ) / 100), 0);
						if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.00 * 500 ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS
						if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
						{
							Titles.AwardKarma( from, -BeggingKarma( from ), true );
							from.Say( BeggingWords() );
							gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * 500 ) / 100), 0);
						}
						gBonus = gBonus + 500;
						from.AddToBackpack ( new Gold( gBonus ) );
						string sMessage = "";
						switch ( Utility.RandomMinMax( 0, 9 ) )
						{
							case 0:	sMessage = "Hmmmm...someone has been busy. Here is " + gBonus.ToString() + " gold for you.";						break;
							case 1:	sMessage = "I'll take that. Here is " + gBonus.ToString() + " gold for you.";										break;
							case 2:	sMessage = "I assume the traps were well avoided? Here is " + gBonus.ToString() + " gold for you.";					break;
							case 3:	sMessage = "You are better than some of the thieves I have met. Here is " + gBonus.ToString() + " gold for you.";	break;
							case 4:	sMessage = "This is a good one you stole here. Here is " + gBonus.ToString() + " gold for you.";					break;
							case 5:	sMessage = "Keep this up and we will both be rich. Here is " + gBonus.ToString() + " gold for you.";				break;
							case 6:	sMessage = "How did you manage to steal this one? Here is " + gBonus.ToString() + " gold for you.";					break;
							case 7:	sMessage = "You seem to be avoiding the dangers out there. Here is " + gBonus.ToString() + " gold for you.";		break;
							case 8:	sMessage = "I have seen one like this before. Here is " + gBonus.ToString() + " gold for you.";						break;
							case 9:	sMessage = "Why earn when you can take? Here is " + gBonus.ToString() + " gold for you.";							break;
						}
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else if ( dropped is StolenChest )
					{
						StolenChest sRipoff = (StolenChest)dropped;
						int vRipoff = sRipoff.ContainerValue;
						int gBonus = (int)Math.Round((( from.Skills[SkillName.ItemID].Value * vRipoff ) / 100), 0);
						if ( this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ gBonus = gBonus + (int)Math.Round((( 100.00 * vRipoff ) / 100), 0); GuildMember = 1; } // FOR GUILD MEMBERS

						if ( BeggingPose(from) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
						{
							Titles.AwardKarma( from, -BeggingKarma( from ), true );
							from.Say( BeggingWords() );
							gBonus = (int)Math.Round((( from.Skills[SkillName.Begging].Value * vRipoff ) / 100), 0);
						}
						gBonus = gBonus + vRipoff;
						from.AddToBackpack ( new Gold( gBonus ) );
						string sMessage = "";
						switch ( Utility.RandomMinMax( 0, 9 ) )
						{
							case 0:	sMessage = "Hmmmm...someone has been busy. Here is " + gBonus.ToString() + " gold for you.";						break;
							case 1:	sMessage = "I'll take that. Here is " + gBonus.ToString() + " gold for you.";										break;
							case 2:	sMessage = "I assume the traps were well avoided? Here is " + gBonus.ToString() + " gold for you.";					break;
							case 3:	sMessage = "You are better than some of the thieves I have met. Here is " + gBonus.ToString() + " gold for you.";	break;
							case 4:	sMessage = "This is a good one you stole here. Here is " + gBonus.ToString() + " gold for you.";					break;
							case 5:	sMessage = "Keep this up and we will both be rich. Here is " + gBonus.ToString() + " gold for you.";				break;
							case 6:	sMessage = "How did you manage to steal this one? Here is " + gBonus.ToString() + " gold for you.";					break;
							case 7:	sMessage = "You seem to be avoiding the dangers out there. Here is " + gBonus.ToString() + " gold for you.";		break;
							case 8:	sMessage = "I have seen one like this before. Here is " + gBonus.ToString() + " gold for you.";						break;
							case 9:	sMessage = "Why earn when you can take? Here is " + gBonus.ToString() + " gold for you.";							break;
						}
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
				}
				else if ( ( dropped is HenchmanFighterItem || dropped is HenchmanArcherItem || dropped is HenchmanWizardItem ) && ( this is InnKeeper || this is TavernKeeper || this is Barkeeper ) )
				{
					int fairTrade = 1;
					string sMessage = "";
					switch ( Utility.RandomMinMax( 0, 7 ) )
					{
						case 0:	sMessage = "So, this follower is not working out for you?"; break;
						case 1:	sMessage = "Looking for a replacement henchman eh?"; break;
						case 2:	sMessage = "Well...this one is looking for fame and fortune."; break;
						case 3:	sMessage = "Maybe this one will be a better fit in your group."; break;
						case 4:	sMessage = "Not all relationships work out."; break;
						case 5:	sMessage = "At you least you parted ways amiably."; break;
						case 6:	sMessage = "This one has been hanging out around here."; break;
						case 7:	sMessage = "This one also seeks great treasure.";		break;
					}
					if ( dropped is HenchmanFighterItem )
					{
						HenchmanFighterItem myFollower = (HenchmanFighterItem)dropped;
						if ( myFollower.HenchDead > 0 ){ fairTrade = 0; } else
						{
							HenchmanFighterItem newFollower = new HenchmanFighterItem();
							newFollower.HenchTimer = myFollower.HenchTimer;
							newFollower.HenchBandages = myFollower.HenchBandages;
							from.AddToBackpack ( newFollower );
						}
					}
					else if ( dropped is HenchmanWizardItem )
					{
						HenchmanWizardItem myFollower = (HenchmanWizardItem)dropped;
						if ( myFollower.HenchDead > 0 ){ fairTrade = 0; } else
						{
							HenchmanWizardItem newFollower = new HenchmanWizardItem();
							newFollower.HenchTimer = myFollower.HenchTimer;
							newFollower.HenchBandages = myFollower.HenchBandages;
							from.AddToBackpack ( newFollower );
						}
					}
					else if ( dropped is HenchmanArcherItem )
					{
						HenchmanArcherItem myFollower = (HenchmanArcherItem)dropped;
						if ( myFollower.HenchDead > 0 ){ fairTrade = 0; } else
						{
							HenchmanArcherItem newFollower = new HenchmanArcherItem();
							newFollower.HenchTimer = myFollower.HenchTimer;
							newFollower.HenchBandages = myFollower.HenchBandages;
							from.AddToBackpack ( newFollower );
						}
					}
					if ( fairTrade == 1 )
					{
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else { this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "This is not a graveyard! Bury them somewhere else!", from.NetState); }
				}
				else if ( dropped is BookBox && ( this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "The curse has been lifted from the books.", from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( dropped is CurseItem && ( this is Mage || this is KeeperOfChivalry || this is Witches || this is Necromancer || this is MageGuildmaster || this is HolyMage || this is NecroMage ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					string curseName = dropped.Name;
						if ( curseName == ""){ curseName = "item"; }
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "The curse has been lifted from the " + curseName + ".", from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( ( dropped is SewageItem || dropped is SlimeItem ) && ( this is InnKeeper || this is TavernKeeper || this is Barkeeper || this is Waiter ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "The item has been cleaned.", from.NetState);
					dropped.Delete();
					return true;
				}
				else if ( dropped is WeededItem && ( this is Alchemist || this is Herbalist ) )
				{
					Container pack = (Container)dropped;
						List<Item> items = new List<Item>();
						foreach (Item item in pack.Items)
						{
							items.Add(item);
						}
						foreach (Item item in items)
						{
							from.AddToBackpack ( item );
						}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "The weeds have been removed.", from.NetState);
					dropped.Delete();
					return true;
				}

				//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				/* TODO: Thou art giving me? and fame/karma for gold gifts */

				if ( dropped is SmallBOD || dropped is LargeBOD )
				{
					if( Core.ML )
					{
						if( ((PlayerMobile)from).NextBODTurnInTime > DateTime.UtcNow )
						{
							SayTo( from, 1079976 );	//
							return false;
						}
					}

					if ( !IsValidBulkOrder( dropped ) || !SupportsBulkOrders( from ) )
					{
						SayTo( from, 1045130 ); // That order is for some other shopkeeper.
						return false;
					}
					else if ( ( dropped is SmallBOD && !( (SmallBOD)dropped ).Complete ) || ( dropped is LargeBOD && !( (LargeBOD)dropped ).Complete ) )
					{
						SayTo( from, 1045131 ); // You have not completed the order yet.
						return false;
					}
					
					int creds = 0;

					if ((dropped is LargeSmithBOD || dropped is SmallSmithBOD) && from is PlayerMobile)
					{
						if (dropped is LargeSmithBOD)
							creds = SmithRewardCalculator.Instance.ComputePoints( (LargeBOD)dropped );
						else 
							creds = SmithRewardCalculator.Instance.ComputePoints( (SmallBOD)dropped )/3;

						((PlayerMobile)from).BlacksmithBOD += creds; 
					}
					else if ((dropped is LargeTailorBOD || dropped is SmallTailorBOD) && from is PlayerMobile)
					{
						if (dropped is LargeTailorBOD)
							creds = TailorRewardCalculator.Instance.ComputePoints( (LargeBOD)dropped );		
						else
							creds = TailorRewardCalculator.Instance.ComputePoints( (SmallBOD)dropped ) /3;
							
						((PlayerMobile)from).TailorBOD += creds; 
					}
					
					from.SendSound( 0x3D );

					//SayTo( from, 1045132 ); // Thank you so much!  Here is a reward for your effort.
					SayTo( from, "Thank you!  You've earned " + (creds) + " credits with the Guild." );
					SayTo( from, "You can always ask me about your credits total or to redeem them." );

					OnSuccessfulBulkOrderReceive( from );

					((PlayerMobile)from).NextBODTurnInTime = DateTime.Now + TimeSpan.FromSeconds( 10.0 );

					dropped.Delete();
					return true;
				}
			}

			return base.OnDragDrop( from, dropped );
		}

		private GenericBuyInfo LookupDisplayObject( object obj )
		{
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();

			for ( int i = 0; i < buyInfo.Length; ++i ) {
				GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[i];

				if ( gbi.GetDisplayEntity() == obj )
					return gbi;
			}

			return null;
		}
		
		public void ClaimCredits(Mobile from, int amount)
		{
			if (!from.Player || from.Backpack == null)
				return;

			if ( ( this is Tailor || this is TailorGuildmaster) && ((PlayerMobile)from).TailorBOD <= 0)
				return;
				
			if ( ( this is Blacksmith || this is BlacksmithGuildmaster) && ((PlayerMobile)from).BlacksmithBOD <= 0)
				return;
		
			this.Say("Thank you for your help!");
			this.Say("Let me see what I can find for you... ");
			
			Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( LookForReward ), new object[]{ from, amount }  );

		}
		
		public void LookForReward( object state )
		{
			if (this.Deleted || this == null)
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			int amount = (int)states[1];
			
			if (!(from is PlayerMobile) || from == null)
				return;
				
			switch (Utility.Random(5))
			{
				case 0: Emote("I have this... "); break;
				case 1: Emote("Perhaps I could part with that there..."); break;
				case 2: Emote("Points to an item on a shelf and changes his mind."); break;
				case 3: Emote("I can't give that soo...."); break;
				case 4: Emote("ah... maybe you could use this."); break;
			}
			
			if (Utility.RandomDouble() > 0.75)
				GiveReward(from, amount);
			else 
				Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerStateCallback ( LookForReward ), new object[]{ from, amount } );
		}
				
		
		private void GiveReward(Mobile from, int amount)
		{ 
			//determine the type of credit rewards
			int credittype = 1; // backsmith
			if (this is Tailor || this is TailorGuildmaster)
				credittype = 2;
			
			int credits = 0;
			if (amount >= 1)
   			{
	  			if (credittype == 1)
      				{
	  				if (amount > ((PlayerMobile)from).BlacksmithBOD)
       						return;
	     
	  				credits = amount;
      					((PlayerMobile)from).BlacksmithBOD -= credits;
	   			}
	   			else if (credittype == 2)
       				{
	   				if (amount > ((PlayerMobile)from).TailorBOD)
       						return;

      					credits = amount;
       					((PlayerMobile)from).TailorBOD -= credits;
	    			}
	    		}
      
			else if (credittype == 1)
   			{
				credits = ((PlayerMobile)from).BlacksmithBOD;
    				((PlayerMobile)from).BlacksmithBOD -= credits;
			}
			else //tailor
   			{
				credits = ((PlayerMobile)from).TailorBOD;
    				((PlayerMobile)from).TailorBOD -= credits;
    			}

			Titles.AwardFame( from, (credits/50), true );
			
			this.Say("Here you go.");
			from.SendMessage("You have redeemed "+ credits+ " credits from the Guild.");
		
			Item reward = null;
			int gold = 0;
			
			//add variability
			if (Utility.RandomDouble() < 33)
				credits = (int)((double)credits * 0.85);
			else if (Utility.RandomDouble() > 0.80)
				credits = (int)((double)credits * 1.15);
			
			AdventuresFunctions.DiminishingReturns( credits, 12000 );
			
			if (credittype == 1)//smith
			{
				int basecred = 20; // variable which makes editing all reward values much easier for balancing
				
				if (credits <= (basecred *4))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new SturdyShovel(); break;
						case 1: reward = new SturdyPickaxe(); break;
						case 2: reward = new LeatherGlovesOfMining( 1 ); break;
					}
				}
				else if (credits <= (basecred *10))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new GargoylesPickaxe(); break;
						case 1: reward = new ProspectorsTool(); break;
						case 2: reward = new StuddedGlovesOfMining( 3 ); break;
					}
				}
				else if (credits <= (basecred *20))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new ColoredAnvil(); break;
						case 1: reward = new ProspectorsTool(); break;
						case 2: reward = new RingmailGlovesOfMining( 5 ); break;
					}
				}
				else if (credits <= (basecred *34))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 1, ( 55 - (1*5) ) ); break;
						case 1: reward = new ProspectorsTool(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 2, ( 55 - (2*5) ) ); break;
					}
				}
				else if (credits <= (basecred *70))//+18
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 2, ( 55 - (2*5) ) ); break;
						case 1: reward = new ColoredAnvil(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 3, ( 55 - (3*5) ) ); break;
					}
				}
				else if (credits <= (basecred *92)) //+22
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 2, ( 55 - (2*5) ) ); break;
						case 1: reward = new ColoredAnvil(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 3, ( 55 - (3*5) ) ); break;
					}
				}
				else if (credits <= (basecred *118)) //+26
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 3, ( 55 - (3*5) ) ); break;
						case 1: reward = new ColoredAnvil(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 4, ( 55 - (4*5) ) ); break;
					}
				}
				else if (credits <= (basecred *148)) //+30
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicHammer( CraftResource.Iron + 4, ( 55 - (4*5) ) ); break;
						case 1: reward = new DwarvenForge(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred *172)) //+34
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 10 ); break;
						case 1: reward = new DwarvenForge(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred *200)) //+38
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 15 ); break;
						case 1: reward = new DwarvenForge(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred *230)) //+38
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 15 ); break;
						case 1: reward = new ElvenForgeDeed(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 5, ( 55 - (5*5) ) ); break;
					}
				}
				else if (credits <= (basecred *262)) //+42
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 20 ); break;
						case 1: reward = new ElvenForgeDeed(); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 6, ( 55 - (6*5) ) ); break;
					}
				}
				else if (credits <= (basecred *308)) //+46
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 20 ); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, 100 + 5 ); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 7, ( 55 - (7*5) ) ); break;
					}
				}
				else if (credits <= (basecred *358)) //+50
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new AncientSmithyHammer( 25 ); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, 100 + 5 ); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 8, ( 55 - (8*5) ) ); break;
					}
				}
				else if (credits <= (basecred *412)) //+54
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new OneHandedDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, 100 + 10 ); break;
						case 2: reward = new RunicHammer( CraftResource.Iron + 8, ( 55 - (8*5) ) ); break;
					}
				}
				else if (credits <= (basecred *470)) //+58
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new OneHandedDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Blacksmith, 100 + 10 ); break;
						case 2: reward = new ItemBlessDeed(); break;
					}
				}
				else if (credits > (basecred *470)) //only powerscrolls from here
				{
					double odds = Utility.RandomDouble();
					if (odds >= 0.97)
						reward = new PowerScroll( SkillName.Blacksmith, 100 + 25 );
					else if (odds >= 90)
						reward = new PowerScroll( SkillName.Blacksmith, 100 + 20 );
					else if (odds >= 75)
						reward = new PowerScroll( SkillName.Blacksmith, 100 + 15 );
					else if (odds >= 20)
						reward = new PowerScroll( SkillName.Blacksmith, 100 + 10 );
					else 
						reward = new PowerScroll( SkillName.Blacksmith, 100 + 5 );
				}
			
			}
			else if (credittype == 2)//tailor
			{
				int basecred = 20; // variable which makes editing all reward values much easier for balancing
				
				if (credits <= (basecred *4))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new SmallStretchedHideEastDeed(); break;
						case 1: reward = new SmallStretchedHideSouthDeed(); break;
						case 2: reward = new TallBannerEast(); break;
					}
				}
				else if (credits <= (basecred *10))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new MediumStretchedHideEastDeed(); break;
						case 1: reward = new MediumStretchedHideSouthDeed(); break;
						case 2: reward = new TallBannerNorth(); break;
					}
				}
				else if (credits <= (basecred *20))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new LightFlowerTapestryEastDeed(); break;
						case 1: reward = new LightFlowerTapestrySouthDeed(); break;
						case 2: reward = new SalvageBag(); break;
					}
				}
				else if (credits <= (basecred *34))
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new BrownBearRugEastDeed(); break;
						case 1: reward = new BrownBearRugSouthDeed(); break;
						case 2: reward = new SalvageBag(); break;
					}
				}
				else if (credits <= (basecred *70))//+18
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new PolarBearRugEastDeed(); break;
						case 1: reward = new PolarBearRugSouthDeed(); break;
						case 2: reward = new SalvageBag(); break;
					}
				}
				else if (credits <= (basecred *92)) //+22
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new DarkFlowerTapestryEastDeed(); break;
						case 1: reward = new DarkFlowerTapestrySouthDeed(); break;
						case 2: reward = new RunicSewingKit( CraftResource.RegularLeather + 1, 60 - (1*15) ); break;
					}
				}
				else if (credits <= (basecred *118)) //+26
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new MagicScissors(); break;
						case 1: reward = new TallBannerEast(); break;
						case 2: reward = new GuildedTallBannerEast(); break;
					}
				}
				else if (credits <= (basecred *148)) //+30
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new GuildedTallBannerNorth(); break;
						case 1: reward = new TallBannerNorth(); break;
						case 2: reward = new MagicScissors(); break;
					}
				}
				else if (credits <= (basecred *182)) //+34
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new EnhancementDeed(); break;
						case 1: reward = new GuildedTallBannerEast(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred *220)) //+38
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, 60 - (2*15) ); break;
						case 1: reward = new GuildedTallBannerNorth(); break;
						case 2: reward = new EnhancementDeed(); break;
					}
				}
				else if (credits <= (basecred *220)) //+38
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new EnhancementDeed(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, 60 - (2*15) ); break;
						case 2: reward = new CathedralWindow1(); break;
					}
				}
				else if (credits <= (basecred *262)) //+42
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new CathedralWindow3(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, 60 - (2*15) ); break;
						case 2: reward = new CathedralWindow2(); break;
					}
				}
				else if (credits <= (basecred *308)) //+46
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new CathedralWindow4(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 2, 60 - (2*15) ); break;
						case 2: reward = new CathedralWindow5(); break;
					}
				}
				else if (credits <= (basecred *358)) //+50
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new LegsOfMusicalPanache(); break;
						case 1: reward = new RunicSewingKit( CraftResource.RegularLeather + 3, 60 - (3*15) ); break;
						case 2: reward = new SkirtOfPower(); break;
					}
				}
				else if (credits <= (basecred *412)) //+54
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new ClothingBlessDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Tailoring, 100 + 5 ); break;
						case 2: reward = new RunicSewingKit( CraftResource.RegularLeather + 3, 60 - (3*15) ); break;
					}
				}
				else if (credits <= (basecred *470)) //+58
				{
					switch (Utility.Random(3))
					{
						case 0: reward = new ItemBlessDeed(); break;
						case 1: reward = new PowerScroll( SkillName.Tailoring, 100 + 5 ); break;
						case 2: reward = new RunicSewingKit( CraftResource.RegularLeather + 3, 60 - (3*15) );; break;
					}
				}
				else if (credits > (basecred *470)) //only powerscrolls from here
				{
					double odds = Utility.RandomDouble();
					if (odds >= 0.97)
						reward = new PowerScroll( SkillName.Tailoring, 100 + 25 );
					else if (odds >= 90)
						reward = new PowerScroll( SkillName.Tailoring, 100 + 20 );
					else if (odds >= 75)
						reward = new PowerScroll( SkillName.Tailoring, 100 + 15 );
					else if (odds >= 20)
						reward = new PowerScroll( SkillName.Tailoring, 100 + 10 );
					else 
						reward = new PowerScroll( SkillName.Tailoring, 100 + 5 );
				}
			}
						
			if ( reward != null )
			{
				from.AddToBackpack( reward );
			}
				
			gold = credits * Utility.RandomMinMax(4,7);
					
			if (AdventuresFunctions.IsPuritain((object)this))
				gold /= 2;

			if ( gold > 2500 )
				Banker.Deposit(from, gold );
			else if ( gold > 0 )
				from.AddToBackpack( new Gold( gold ) );	
				
		}

        private void ProcessSinglePurchase( BuyItemResponse buy, IBuyItemInfo bii, List<BuyItemResponse> validBuy, ref int controlSlots, ref bool fullPurchase, ref int totalCost )
		{
			int amount = buy.Amount;

			if ( amount > bii.Amount )
				amount = bii.Amount;

			if ( amount <= 0 )
				return;

			int slots = bii.ControlSlots * amount;

			if ( controlSlots >= slots )
			{
				controlSlots -= slots;
			}
			else
			{
				fullPurchase = false;
				return;
			}

			totalCost += bii.Price * amount;
			validBuy.Add( buy );
		}

		private void ProcessValidPurchase( int amount, IBuyItemInfo bii, Mobile buyer, Container cont )
		{
			if ( amount > bii.Amount )
				amount = bii.Amount;

			if ( amount < 1 )
				return;

			bii.Amount -= amount;

			IEntity o = bii.GetEntity();

			if ( o is Item )
			{
				Item item = (Item)o;

				if ( item.Stackable )
				{
					item.Amount = amount;

					if ( cont == null || !cont.TryDropItem( buyer, item, false ) )
						item.MoveToWorld( buyer.Location, buyer.Map );
				}
				else
				{
					item.Amount = 1;

					if ( cont == null || !cont.TryDropItem( buyer, item, false ) )
						item.MoveToWorld( buyer.Location, buyer.Map );

					for ( int i = 1; i < amount; i++ )
					{
						item = bii.GetEntity() as Item;

						if ( item != null )
						{
							item.Amount = 1;

							if ( cont == null || !cont.TryDropItem( buyer, item, false ) )
								item.MoveToWorld( buyer.Location, buyer.Map );
						}
					}
				}
			}
			else if ( o is Mobile )
			{
				Mobile m = (Mobile)o;

				m.Direction = (Direction)Utility.Random( 8 );
				m.MoveToWorld( buyer.Location, buyer.Map );
				m.PlaySound( m.GetIdleSound() );

				if ( m is BaseCreature )
				{
					( (BaseCreature)m ).SetControlMaster( buyer );
					( (BaseCreature)m ).Tamable = true;
					( (BaseCreature)m ).MinTameSkill = 29.1;
				}

				for ( int i = 1; i < amount; ++i )
				{
					m = bii.GetEntity() as Mobile;

					if ( m != null )
					{
						m.Direction = (Direction)Utility.Random( 8 );
						m.MoveToWorld( buyer.Location, buyer.Map );

						if ( m is BaseCreature )
						{
							( (BaseCreature)m ).SetControlMaster( buyer );
							( (BaseCreature)m ).Tamable = true;
							( (BaseCreature)m ).MinTameSkill = 29.1;
						}
					}
				}
			}
		}

        public virtual bool OnBuyItems( Mobile buyer, List<BuyItemResponse> list )
		{
			if ( !IsActiveSeller )
				return false;

			if ( !buyer.CheckAlive() )
				return false;

			if ( !CheckVendorAccess( buyer ) )
			{
				//Say( 501522 ); // I shall not treat with scum like thee!
				this.Say( "I have no business with you." );
				return false;
			}

			UpdateBuyInfo();

			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			IShopSellInfo[] info = GetSellInfo();
			int totalCost = 0;
            List<BuyItemResponse> validBuy = new List<BuyItemResponse>( list.Count );
			Container cont;
			bool bought = false;
			bool fromBank = false;
			bool fullPurchase = true;
			int controlSlots = buyer.FollowersMax - buyer.Followers;

			foreach ( BuyItemResponse buy in list )
			{
				Serial ser = buy.Serial;
				int amount = buy.Amount;

				if ( ser.IsItem )
				{
					Item item = World.FindItem( ser );

					if ( item == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( item );

					if ( gbi != null )
					{
						ProcessSinglePurchase( buy, gbi, validBuy, ref controlSlots, ref fullPurchase, ref totalCost );
					}
					else if ( item != this.BuyPack && item.IsChildOf( this.BuyPack ) )
					{
						if ( amount > item.Amount )
							amount = item.Amount;

						if ( amount <= 0 )
							continue;

						foreach ( IShopSellInfo ssi in info )
						{
							if ( ssi.IsSellable( item ) )
							{
								if ( ssi.IsResellable( item ) )
								{
									totalCost += ssi.GetBuyPriceFor( item ) * amount;
									validBuy.Add( buy );
									break;
								}
							}
						}
					}
				}
				else if ( ser.IsMobile )
				{
					Mobile mob = World.FindMobile( ser );

					if ( mob == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( mob );

					if ( gbi != null )
						ProcessSinglePurchase( buy, gbi, validBuy, ref controlSlots, ref fullPurchase, ref totalCost );
				}
			}//foreach

			if ( fullPurchase && validBuy.Count == 0 )
				SayTo( buyer, 500190 ); // Thou hast bought nothing!
			else if ( validBuy.Count == 0 )
				SayTo( buyer, 500187 ); // Your order cannot be fulfilled, please try again.

			if ( validBuy.Count == 0 )
				return false;

			bought = ( buyer.AccessLevel >= AccessLevel.GameMaster );

			cont = buyer.Backpack;
			if ( !bought && cont != null )
			{
				if ( cont.ConsumeTotal( typeof( Gold ), totalCost ) )
					bought = true;
				else if ( totalCost < 2000 )
					SayTo( buyer, 500192 );//Begging thy pardon, but thou casnt afford that.
			}

			if ( !bought && totalCost >= 2000 )
			{
				cont = buyer.FindBankNoCreate();
				if ( cont != null && cont.ConsumeTotal( typeof( Gold ), totalCost ) )
				{
					bought = true;
					fromBank = true;
				}
				else
				{
					SayTo( buyer, 500191 ); //Begging thy pardon, but thy bank account lacks these funds.
				}
			}

			if ( !bought )
				return false;
			else
				buyer.PlaySound( 0x32 );

			cont = buyer.Backpack;
			if ( cont == null )
				cont = buyer.BankBox;

			foreach ( BuyItemResponse buy in validBuy )
			{
				Serial ser = buy.Serial;
				int amount = buy.Amount;

				if ( amount < 1 )
					continue;

				if ( ser.IsItem )
				{
					Item item = World.FindItem( ser );

					if ( item == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( item );

					if ( gbi != null )
					{
						ProcessValidPurchase( amount, gbi, buyer, cont );
					}
					else
					{
						if ( amount > item.Amount )
							amount = item.Amount;

						foreach ( IShopSellInfo ssi in info )
						{
							if ( ssi.IsSellable( item ) )
							{
								if ( ssi.IsResellable( item ) )
								{
									Item buyItem;
									if ( amount >= item.Amount )
									{
										buyItem = item;
									}
									else
									{
										buyItem = Mobile.LiftItemDupe( item, item.Amount - amount );

										if ( buyItem == null )
											buyItem = item;
									}

									if ( cont == null || !cont.TryDropItem( buyer, buyItem, false ) )
										buyItem.MoveToWorld( buyer.Location, buyer.Map );

									break;
								}
							}
						}
					}
				}
				else if ( ser.IsMobile )
				{
					Mobile mob = World.FindMobile( ser );

					if ( mob == null )
						continue;

					GenericBuyInfo gbi = LookupDisplayObject( mob );

					if ( gbi != null )
						ProcessValidPurchase( amount, gbi, buyer, cont );
				}
			}//foreach

			if ( fullPurchase )
			{
				if ( buyer.AccessLevel >= AccessLevel.GameMaster )
					SayTo( buyer, true, "I would not presume to charge thee anything.  Here are the goods you requested." );
				else if ( fromBank )
					SayTo( buyer, true, "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.", totalCost );
				else
					SayTo( buyer, true, "The total of thy purchase is {0} gold.  My thanks for the patronage.", totalCost );
			}
			else
			{
				if ( buyer.AccessLevel >= AccessLevel.GameMaster )
					SayTo( buyer, true, "I would not presume to charge thee anything.  Unfortunately, I could not sell you all the goods you requested." );
				else if ( fromBank )
					SayTo( buyer, true, "The total of thy purchase is {0} gold, which has been withdrawn from your bank account.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.", totalCost );
				else
					SayTo( buyer, true, "The total of thy purchase is {0} gold.  My thanks for the patronage.  Unfortunately, I could not sell you all the goods you requested.", totalCost );
			}

			return true;
		}

		public virtual bool CheckVendorAccess( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( from.Blessed )
				return false;

			if ( IntelligentAction.GetMyEnemies( from, this, false ) == true )
				return false;

			if ( this is BaseGuildmaster && this.NpcGuild != pm.NpcGuild ) // ONLY GUILD MEMBERS CAN BUY FROM GUILD MASTERS
				return false;

			return true;
		}

        public virtual bool OnSellItems( Mobile seller, List<SellItemResponse> list )
		{
			if ( !IsActiveBuyer )
				return false;

			if ( !seller.CheckAlive() )
				return false;

			if ( !CheckVendorAccess( seller ) )
			{
				//Say( 501522 ); // I shall not treat with scum like thee!
				this.Say( "I have no business with you." );
				return false;
			}

			seller.PlaySound( 0x32 );

			IShopSellInfo[] info = GetSellInfo();
			IBuyItemInfo[] buyInfo = this.GetBuyInfo();
			int GiveGold = 0;
			int Sold = 0;
			Container cont;

			foreach ( SellItemResponse resp in list )
			{
				if ( resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() || !resp.Item.Movable || ( resp.Item is Container && ( (Container)resp.Item ).Items.Count != 0 ) )
					continue;

				foreach ( IShopSellInfo ssi in info )
				{
					if ( ssi.IsSellable( resp.Item ) )
					{
						Sold++;
						break;
					}
				}
			}

			if ( Sold > MaxSell )
			{
				SayTo( seller, true, "You may only sell {0} items at a time!", MaxSell );
				return false;
			}
			else if ( Sold == 0 )
			{
				return true;
			}

			foreach ( SellItemResponse resp in list )
			{
				if ( resp.Item.RootParent != seller || resp.Amount <= 0 || !resp.Item.IsStandardLoot() || !resp.Item.Movable || ( resp.Item is Container && ( (Container)resp.Item ).Items.Count != 0 ) )
					continue;

				if ( BeggingPose(seller) > 0 ) // LET US SEE IF THEY ARE BEGGING
				{
					Titles.AwardKarma( seller, -BeggingKarma( seller ), true );
				}

				PlayerMobile pm = (PlayerMobile)seller;
				int GuildMember = 0;

				foreach ( IShopSellInfo ssi in info )
				{
					if ( ssi.IsSellable( resp.Item ) )
					{
						int amount = resp.Amount;

						if ( amount > resp.Item.Amount )
							amount = resp.Item.Amount;

						if ( ssi.IsResellable( resp.Item ) )
						{
							bool found = false;

							foreach ( IBuyItemInfo bii in buyInfo )
							{
								if ( bii.Restock( resp.Item, amount ) )
								{
									resp.Item.Consume( amount );
									found = true;

									break;
								}
							}

							if ( !found )
							{
								cont = this.BuyPack;

								if ( amount < resp.Item.Amount )
								{
									Item item = Mobile.LiftItemDupe( resp.Item, resp.Item.Amount - amount );

									if ( item != null )
									{
										item.SetLastMoved();
										cont.DropItem( item );
										//resp.item.Delete();
									}
									else
									{
										resp.Item.SetLastMoved();
										cont.DropItem( item );
										//resp.item.Delete();
									}
								}
								else
								{
									resp.Item.SetLastMoved();
									cont.DropItem( resp.Item );
									//resp.item.Delete();
								}
							}
						}
						else
						{
							if ( amount < resp.Item.Amount )
								resp.Item.Amount -= amount;
							else
								resp.Item.Delete();
						}

						int barter = (int)seller.Skills[SkillName.ItemID].Value;
						if ( barter < 100 && this.NpcGuild != NpcGuild.None && this.NpcGuild == pm.NpcGuild ){ barter = 100; GuildMember = 1; } // FOR GUILD MEMBERS

						if ( BeggingPose(seller) > 0 && GuildMember == 0 ) // LET US SEE IF THEY ARE BEGGING
						{
							seller.CheckSkill( SkillName.Begging, 0, 100 );
							barter = (int)seller.Skills[SkillName.Begging].Value;
						}
							//this.Say("Getsellprice is " + ssi.GetSellPriceFor( resp.Item, barter ));
							int money = ssi.GetSellPriceFor( resp.Item, barter );

							if (!m_sellingpriceadjusted)
							{
								//this.Say("adjusting prices");
								if (this.Karma > 0 ) //good vendor
									money = AdjustPrices( money, true);
								else if (this.Karma < 0 )
									money = AdjustPrices( money, false);
								else if (this.Karma == 0)
								{
									if (seller.Karma >= 0)
										money = AdjustPrices( money, true);
									else
										money = AdjustPrices( money, false);
								}

								m_sellingpriceadjusted = true;
							}

						GiveGold += money * amount;
						//this.Say("givegold is " + GiveGold);
						break;
					}
				}
			}

			if ( GiveGold > 0 )
			{
/*				while ( GiveGold > 30000 )
				{
					seller.AddToBackpack( new Gold( 30000 ) );
					GiveGold -= 60000;
				}*/
				if ( GiveGold > 10000 )
					seller.AddToBackpack( new BankCheck( GiveGold ) );
				else
					seller.AddToBackpack( new Gold( GiveGold ) );

				seller.PlaySound( 0x0037 );//Gold dropping sound

				if ( SupportsBulkOrders( seller ) )
				{
					Item bulkOrder = CreateBulkOrder( seller, false );

					if ( bulkOrder is LargeBOD )
						seller.SendGump( new LargeBODAcceptGump( seller, (LargeBOD)bulkOrder ) );
					else if ( bulkOrder is SmallBOD )
						seller.SendGump( new SmallBODAcceptGump( seller, (SmallBOD)bulkOrder ) );
				}
			}

			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string sTitle = GetPlayerInfo.GetNPCGuild( this );
			if ( sTitle != "" ){ list.Add( Utility.FixHtml( sTitle ) ); }

			if (this is Blacksmith || this is BlacksmithGuildmaster || this is Tailor || this is TailorGuildmaster)
			{
				list.Add("Bulk orders give credits, say 'credits' to see how many you have.");
				list.Add("To redeem your credits, say 'claim'.");
			}

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 ); // version

			List<SBInfo> sbInfos = this.SBInfos;

			for ( int i = 0; sbInfos != null && i < sbInfos.Count; ++i )
			{
				SBInfo sbInfo = sbInfos[i];
				List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

				for ( int j = 0; buyInfo != null && j < buyInfo.Count; ++j )
				{
					GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[j];

					int maxAmount = gbi.MaxAmount;
					int doubled = 0;

					if ( doubled > 0 )
					{
						writer.WriteEncodedInt( 1 + ( ( j * sbInfos.Count ) + i ) );
						writer.WriteEncodedInt( doubled );
					}
				}
			}

			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			LoadSBInfo();

			List<SBInfo> sbInfos = this.SBInfos;

			switch ( version )
			{
				case 1:
					{
						int index;

						while ( ( index = reader.ReadEncodedInt() ) > 0 )
						{
							int doubled = reader.ReadEncodedInt();

							if ( sbInfos != null )
							{
								index -= 1;
								int sbInfoIndex = index % sbInfos.Count;
								int buyInfoIndex = index / sbInfos.Count;

								if ( sbInfoIndex >= 0 && sbInfoIndex < sbInfos.Count )
								{
									SBInfo sbInfo = sbInfos[sbInfoIndex];
									List<GenericBuyInfo> buyInfo = sbInfo.BuyInfo;

									if ( buyInfo != null && buyInfoIndex >= 0 && buyInfoIndex < buyInfo.Count )
									{
										GenericBuyInfo gbi = (GenericBuyInfo)buyInfo[buyInfoIndex];

										int amount = 20;

										gbi.Amount = gbi.MaxAmount = amount;
									}
								}
							}
						}

						break;
					}
			}

			if ( IsParagon )
				IsParagon = false;

			if ( Core.AOS && NameHue == 0x35 )
				NameHue = -1;
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && IsActiveVendor )
			{
				if ( SupportsBulkOrders( from ) )
					list.Add( new BulkOrderInfoEntry( from, this ) );
				
				if ( MyServerSettings.buysellcontext() )
				{
					if ( IsActiveSeller )
						list.Add( new VendorBuyEntry( from, this ) );

					if ( IsActiveBuyer )
						list.Add( new VendorSellEntry( from, this ) );
				}
			}

			if (
				( from.Skills[SkillName.Forensics].Value >= 50 && ( this is NecroMage || this is Witches || this is Necromancer || this is NecromancerGuildmaster ) ) || 
				( from.Skills[SkillName.Alchemy].Value >= 40 && from.Skills[SkillName.Cooking].Value >= 40 && from.Skills[SkillName.AnimalLore].Value >= 40 && ( this is Herbalist || this is DruidTree || this is Druid || this is DruidGuildmaster ) ) || 
				( from.Skills[SkillName.Alchemy].Value >= 50 && ( this is Alchemist || this is AlchemistGuildmaster ) ) || 
				( from.Skills[SkillName.Blacksmith].Value >= 50 && ( this is Blacksmith || this is BlacksmithGuildmaster ) ) || 
				( from.Skills[SkillName.Fletching].Value >= 50 && ( this is Bowyer || this is ArcherGuildmaster ) ) || 
				( from.Skills[SkillName.Carpentry].Value >= 50 && ( this is Carpenter || this is CarpenterGuildmaster ) ) || 
				( from.Skills[SkillName.Cartography].Value >= 50 && ( this is Mapmaker || this is CartographersGuildmaster ) ) || 
				( from.Skills[SkillName.Cooking].Value >= 50 && ( this is Cook || this is Baker || this is CulinaryGuildmaster ) ) || 
				( from.Skills[SkillName.Inscribe].Value >= 50 && ( this is Scribe || this is Sage || this is LibrarianGuildmaster ) ) || 
				( from.Skills[SkillName.Tailoring].Value >= 50 && ( this is Weaver || this is Tailor || this is LeatherWorker || this is TailorGuildmaster ) ) || 
				( from.Skills[SkillName.Tinkering].Value >= 50 && ( this is Tinker || this is TinkerGuildmaster ) ) 
			)
			{
				list.Add( new SetupShoppeEntry( from, this ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		public virtual IShopSellInfo[] GetSellInfo()
		{
			return (IShopSellInfo[])m_ArmorSellInfo.ToArray( typeof( IShopSellInfo ) );
		}

		public virtual IBuyItemInfo[] GetBuyInfo()
		{
			return (IBuyItemInfo[])m_ArmorBuyInfo.ToArray( typeof( IBuyItemInfo ) );
		}

		public override bool CanBeDamaged()
		{
			return !IsInvulnerable;
		}
	}
}

namespace Server.ContextMenus
{
	public class SetupShoppeEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;
		private Mobile m_From;
		
		public SetupShoppeEntry( Mobile from, BaseVendor vendor ) : base( 6164, 3 )
		{
			m_Vendor = vendor;
			m_From = from;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			if ( !m_From.HasGump( typeof( Server.Items.ExplainShopped ) ) )
			{
				m_From.SendGump( new Server.Items.ExplainShopped( m_From, m_Vendor ) );
			}
		}
	}

	public class VendorBuyEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;

		public VendorBuyEntry( Mobile from, BaseVendor vendor ) : base( 6103, 8 )
		{
			m_Vendor = vendor;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			m_Vendor.VendorBuy( this.Owner.From );
		}
	}

	public class VendorSellEntry : ContextMenuEntry
	{
		private BaseVendor m_Vendor;

		public VendorSellEntry( Mobile from, BaseVendor vendor ) : base( 6104, 8 )
		{
			m_Vendor = vendor;
			Enabled = vendor.CheckVendorAccess( from );
		}

		public override void OnClick()
		{
			m_Vendor.VendorSell( this.Owner.From );
		}
	}
}

namespace Server
{
	public interface IShopSellInfo
	{
		//get display name for an item
		string GetNameFor( Item item );

		//get price for an item which the player is selling
		int GetSellPriceFor( Item item, int barter );

		//get price for an item which the player is buying
		int GetBuyPriceFor( Item item );

		//can we sell this item to this vendor?
		bool IsSellable( Item item );

		//What do we sell?
		Type[] Types { get; }

		//does the vendor resell this item?
		bool IsResellable( Item item );
	}

	public interface IBuyItemInfo
	{
		//get a new instance of an object (we just bought it)
		IEntity GetEntity();

		int ControlSlots { get; }

		int PriceScalar { get; set; }

		//display price of the item
		int Price { get; set; }

		//display name of the item
		string Name { get; }

		//display hue
		int Hue { get; }

		//display id
		int ItemID { get; }

		//amount in stock
		int Amount { get; set; }

		//max amount in stock
		int MaxAmount { get; }

		//Attempt to restock with item, (return true if restock sucessful)
		bool Restock( Item item, int amount );

		//called when its time for the whole shop to restock
		void OnRestock();
	}
}
