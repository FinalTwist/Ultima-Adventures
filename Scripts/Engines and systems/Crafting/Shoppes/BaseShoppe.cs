using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0x3CF5, 0x3CF6 )]
	public class BaseShoppe : Item, ISecurable
	{
		[Constructable]
		public BaseShoppe() : base( 0x3CF5 )
		{
			Name = "Work Shoppe";
			Weight = 20.0;
			m_Level = SecureLevel.Anyone;
			SetupShelf();
		}

		public override void AddNameProperties(ObjectPropertyList list)
		{
			base.AddNameProperties(list);
			list.Add( 1049644, "Contains: " + ShoppeGold.ToString() + " Gold");
			if ( ShoppeOwner.Name == null ){ list.Add( 1070722, "Owner: " + ShoppeName + "" ); }
			else { list.Add( 1070722, "Owner: " + ShoppeOwner.Name + "" ); }
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			SetSecureLevelEntry.AddTo( from, this, list );
			if ( !this.Movable && CheckAccess( from ) )
			{
				list.Add( new CashOutEntry( from, this ) );
			}
		}

		public static string MakeTask( BaseShoppe shoppe )
		{
			string task = null;

			if ( shoppe is MorticianShoppe ){ task = Server.Items.MorticianShoppe.MakeThisTask(); }
			else if ( shoppe is HerbalistShoppe ){ task = Server.Items.HerbalistShoppe.MakeThisTask(); }
			else if ( shoppe is AlchemistShoppe ){ task = Server.Items.AlchemistShoppe.MakeThisTask(); }
			else if ( shoppe is BlacksmithShoppe ){ task = Server.Items.BlacksmithShoppe.MakeThisTask(); }
			else if ( shoppe is BowyerShoppe ){ task = Server.Items.BowyerShoppe.MakeThisTask(); }
			else if ( shoppe is CarpentryShoppe ){ task = Server.Items.CarpentryShoppe.MakeThisTask(); }
			else if ( shoppe is CartographyShoppe ){ task = Server.Items.CartographyShoppe.MakeThisTask(); }
			else if ( shoppe is BakerShoppe ){ task = Server.Items.BakerShoppe.MakeThisTask(); }
			else if ( shoppe is LibrarianShoppe ){ task = Server.Items.LibrarianShoppe.MakeThisTask(); }
			else if ( shoppe is TailorShoppe ){ task = Server.Items.TailorShoppe.MakeThisTask(); }
			else if ( shoppe is TinkerShoppe ){ task = Server.Items.TinkerShoppe.MakeThisTask(); }

			if ( task == null || task == "" ){ task = "Craft that item they need for their upcoming journey"; }

			return task;
		}

		public void SetupShelf() 
		{
			ShoppeGold = 0;
			ShoppeTools = 0;
			ShoppeResources = 0;
			ShoppeReputation = 0;
			Customer01 = "";
			Customer02 = "";
			Customer03 = "";
			Customer04 = "";
			Customer05 = "";
			Customer06 = "";
			Customer07 = "";
			Customer08 = "";
			Customer09 = "";
			Customer10 = "";
			Customer11 = "";
			Customer12 = "";
		} 

		public static void GiveNewShoppe( Mobile from, Mobile merchant )
		{
			Item shoppe = null;

			if ( merchant is NecroMage || merchant is Witches || merchant is Necromancer || merchant is NecromancerGuildmaster ){ shoppe = new MorticianShoppe(); }
			else if ( merchant is Herbalist || merchant is DruidTree || merchant is Druid || merchant is DruidGuildmaster ){ shoppe = new HerbalistShoppe(); }
			else if ( merchant is Alchemist || merchant is AlchemistGuildmaster ){ shoppe = new AlchemistShoppe(); }
			else if ( merchant is Blacksmith || merchant is BlacksmithGuildmaster ){ shoppe = new BlacksmithShoppe(); }
			else if ( merchant is Bowyer || merchant is ArcherGuildmaster ){ shoppe = new BowyerShoppe(); }
			else if ( merchant is Carpenter || merchant is CarpenterGuildmaster ){ shoppe = new CarpentryShoppe(); }
			else if ( merchant is Mapmaker || merchant is CartographersGuildmaster ){ shoppe = new CartographyShoppe(); }
			else if ( merchant is Cook || merchant is Baker || merchant is CulinaryGuildmaster ){ shoppe = new BakerShoppe(); }
			else if ( merchant is Scribe || merchant is Sage || merchant is LibrarianGuildmaster ){ shoppe = new LibrarianShoppe(); }
			else if ( merchant is Weaver || merchant is Tailor || merchant is LeatherWorker || merchant is TailorGuildmaster ){ shoppe = new TailorShoppe(); }
			else if ( merchant is Tinker || merchant is TinkerGuildmaster ){ shoppe = new TinkerShoppe(); }

			int gold = from.TotalGold;
			int fee = 10000;
			bool begging = false;

			if ( Server.Mobiles.BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
			{
				int cut = (int)(from.Skills[SkillName.Begging].Value * 25 );
					if ( cut > 3000 ){ cut = 3000; }
				fee = fee - cut;
				begging = true;
			}
			else if ( AlreadyHasShoppe( from, shoppe ) )
			{
				merchant.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Good luck with your shoppe." ) ); 
			}
			else if ( gold >= fee && shoppe != null )
			{
				Container cont = from.Backpack;
				cont.ConsumeTotal( typeof( Gold ), fee );
				BaseShoppe store = (BaseShoppe)shoppe;
				from.PlaySound( 0x23D );
				store.ShoppeOwner = from;
				store.ShoppeName = from.Name;
				from.AddToBackpack( store );
				Server.Misc.Customers.CustomerCycle( from, store );
				
				if ( begging )
					merchant.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Since you are begging, this shoppe is only " + fee + " gold." ) ); 
				else
					merchant.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Good luck with your shoppe." ) ); 
			}
			else
				merchant.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Sorry, but you do not have enough gold to start a shoppe." ) ); 
		}

		public static bool AlreadyHasShoppe( Mobile from, Item shelf )
		{
			BaseShoppe shoppe = (BaseShoppe)shelf;
			bool HasShoppe = false;

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is MorticianShoppe && item != shoppe && shoppe is MorticianShoppe ){ targets.Add( item ); }
				else if ( item is HerbalistShoppe && item != shoppe && shoppe is HerbalistShoppe ){ targets.Add( item ); }
				else if ( item is AlchemistShoppe && item != shoppe && shoppe is AlchemistShoppe ){ targets.Add( item ); }
				else if ( item is BlacksmithShoppe && item != shoppe && shoppe is BlacksmithShoppe ){ targets.Add( item ); }
				else if ( item is BowyerShoppe && item != shoppe && shoppe is BowyerShoppe ){ targets.Add( item ); }
				else if ( item is CarpentryShoppe && item != shoppe && shoppe is CarpentryShoppe ){ targets.Add( item ); }
				else if ( item is CartographyShoppe && item != shoppe && shoppe is CartographyShoppe ){ targets.Add( item ); }
				else if ( item is BakerShoppe && item != shoppe && shoppe is BakerShoppe ){ targets.Add( item ); }
				else if ( item is LibrarianShoppe && item != shoppe && shoppe is LibrarianShoppe ){ targets.Add( item ); }
				else if ( item is TailorShoppe && item != shoppe && shoppe is TailorShoppe ){ targets.Add( item ); }
				else if ( item is TinkerShoppe && item != shoppe && shoppe is TinkerShoppe ){ targets.Add( item ); }
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];

				if ( item is BaseShoppe )
				{
					BaseShoppe store = (BaseShoppe)item;

					if ( store.ShoppeOwner == from )
					{
						HasShoppe = true; 

						from.PlaySound( 0x23D );

						shoppe.ShoppeOwner = store.ShoppeOwner;
						shoppe.ShoppeName = store.ShoppeName;
						shoppe.ShoppeGold = store.ShoppeGold;
						shoppe.ShoppeTools = store.ShoppeTools;
						shoppe.ShoppeResources = store.ShoppeResources;
						shoppe.ShoppeReputation = store.ShoppeReputation;
						shoppe.ShoppePage = store.ShoppePage;
						shoppe.ShelfTitle = store.ShelfTitle;
						shoppe.ShelfItem = store.ShelfItem;
						shoppe.ShelfSkill = store.ShelfSkill;
						shoppe.ShelfGuild = store.ShelfGuild;
						shoppe.ShelfTools = store.ShelfTools;
						shoppe.ShelfResources = store.ShelfResources;
						shoppe.ShelfSound = store.ShelfSound;
						shoppe.Customer01 = store.Customer01;
						shoppe.Customer02 = store.Customer02;
						shoppe.Customer03 = store.Customer03;
						shoppe.Customer04 = store.Customer04;
						shoppe.Customer05 = store.Customer05;
						shoppe.Customer06 = store.Customer06;
						shoppe.Customer07 = store.Customer07;
						shoppe.Customer08 = store.Customer08;
						shoppe.Customer09 = store.Customer09;
						shoppe.Customer10 = store.Customer10;
						shoppe.Customer11 = store.Customer11;
						shoppe.Customer12 = store.Customer12;
						from.AddToBackpack( shelf );
						store.Delete();
					}
				}
			}

			return HasShoppe;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Movable )
			{
				from.SendMessage( "This must be secured down in a home to use." );
			}
			else if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendMessage( "You will have to get closer to use that." );
			}
			//else if ( from.Kills > 0 )
			//{
            //    from.SendMessage("This is useless since no one deals with murderers!");
			//}
			else if ( ShoppeOwner != from )
			{
				from.SendMessage ("This is not your shoppe.");
			}
			else
			{
				ShoppeName = from.Name;
				from.PlaySound( 0x2F );
				from.CloseGump( typeof( Server.Items.ShoppeGump ) );
				from.SendGump( new Server.Items.ShoppeGump( this, from ) );
				if (m_Timer == null)
				{
					TimeSpan delay = TimeSpan.FromHours(Utility.RandomMinMax(2, 4));
					if (GetDelay())
						delay = TimeSpan.FromMinutes(Utility.RandomMinMax(20, 40));

					m_Timer = new CustomerTimer( this, delay ); 
					m_Timer.Start();
				}
			}

			return;
		}

		public static void ProgressSkill( Mobile from, BaseShoppe shoppe, int difficulty )
		{
			if ( shoppe is MorticianShoppe ){ from.CheckSkill( SkillName.Forensics, (difficulty-10), (difficulty + 30) ); from.CheckSkill( SkillName.Necromancy, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is HerbalistShoppe ){ from.CheckSkill( SkillName.Alchemy, (difficulty -10), (difficulty + 30) ); from.CheckSkill( SkillName.AnimalLore, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is AlchemistShoppe ){ from.CheckSkill( SkillName.Alchemy, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is BlacksmithShoppe ){ from.CheckSkill( SkillName.Blacksmith, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is BowyerShoppe ){ from.CheckSkill( SkillName.Fletching, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is CarpentryShoppe ){ from.CheckSkill( SkillName.Carpentry, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is CartographyShoppe ){ from.CheckSkill( SkillName.Cartography, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is BakerShoppe ){ from.CheckSkill( SkillName.Cooking, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is LibrarianShoppe ){ from.CheckSkill( SkillName.Inscribe, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is TailorShoppe ){ from.CheckSkill( SkillName.Tailoring, (difficulty -10), (difficulty + 30) ); }
			else if ( shoppe is TinkerShoppe ){ from.CheckSkill( SkillName.Tinkering, (difficulty -10), (difficulty + 30) ); }
		}

		public class CashOutEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private BaseShoppe m_Shoppe;
	
			public CashOutEntry( Mobile from, BaseShoppe shelf ) : base( 6113, 3 )
			{
				m_Mobile = from;
				m_Shoppe = shelf;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( m_Shoppe.ShoppeGold > 0 )
					{
						int barter = (int)m_Mobile.Skills[SkillName.ItemID].Value;
						if ( mobile.NpcGuild == NpcGuild.MerchantsGuild ){ barter = barter + 25; } // FOR GUILD MEMBERS

						Titles.AwardFame( m_Mobile, (int)((double)m_Shoppe.ShoppeGold/30), true );

						int cash = (int)( m_Shoppe.ShoppeGold + (m_Shoppe.ShoppeGold * (barter / 100) ) );

						if (mobile.Avatar)
						{
							if (mobile.BalanceStatus != 0)					
								m_Mobile.SendMessage("Your dedication to crafting affects the balance!" );
							else
								m_Mobile.SendMessage("Your dedication to crafting affects the balance, but you are not pledged to a side." );

							if (mobile.Karma >= 0)
							{
								Titles.AwardKarma( m_Mobile, (int)((double)cash/30), true );
								AetherGlobe.QuestEffect( cash, m_Mobile, true);
							}
							else
							{
								Titles.AwardKarma( m_Mobile, -((int) ((double)cash / 30)), true );
								AetherGlobe.QuestEffect( cash, m_Mobile, false);
							}
						}

						m_Mobile.AddToBackpack( new BankCheck( cash ) );
						m_Mobile.SendMessage("You now have a check for " + cash.ToString() + " gold.");
						m_Shoppe.ShoppeGold = 0;
						m_Shoppe.InvalidateProperties();
					}
					else
					{
						m_Mobile.SendMessage("There is no gold in this shoppe!");
					}
				}
            }
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
			bool procResource = false;
			bool procTools = false;

			if ( ( dropped.ItemID >= 0x1BE3 && dropped.ItemID <= 0x1BFA ) && this is BlacksmithShoppe ){ procResource = true; }
			else if ( ( dropped.ItemID >= 0x1BE3 && dropped.ItemID <= 0x1BFA ) && this is TinkerShoppe ){ procResource = true; }
			else if ( ( dropped is BoltOfCloth || 
						dropped is Cloth || 
						dropped is UncutCloth ) && this is TailorShoppe ){ procResource = true; }
			else if ( ( dropped is BottleOfParts ) && this is MorticianShoppe ){ procResource = true; }
			else if ( ( dropped is BlankScroll ) && this is LibrarianShoppe ){ procResource = true; }
			else if ( ( dropped is Dough ) && this is BakerShoppe ){ procResource = true; }
			else if ( ( dropped is BlankScroll || 
						dropped is BlankMap ) && this is CartographyShoppe ){ procResource = true; }
			else if ( ( dropped is BaseLog || 
						dropped is BaseWoodBoard ) && this is CarpentryShoppe ){ procResource = true; }
			else if ( ( dropped is BaseLog || 
						dropped is BaseWoodBoard ) && this is BowyerShoppe ){ procResource = true; }
			else if ( ( dropped is PlantHerbalism_Leaf || 
						dropped is PlantHerbalism_Mushroom || 
						dropped is PlantHerbalism_Lilly || 
						dropped is PlantHerbalism_Cactus || 
						dropped is PlantHerbalism_Grass || 
						dropped is PlantHerbalism_Flower ) && this is HerbalistShoppe ){ procResource = true; }
			else if ( Server.Misc.MaterialInfo.IsReagent( dropped ) && this is AlchemistShoppe ){ procResource = true; }

			else if ( ( dropped is GodSmithing || 
						dropped is SledgeHammer || 
						dropped is SmithHammer || 
						dropped is Tongs || 
						dropped is AncientSmithyHammer ) && this is BlacksmithShoppe ){ procTools = true; }
			else if ( dropped is TinkerTools && this is TinkerShoppe ){ procTools = true; }
			else if ( ( dropped is GodSewing || 
						dropped is SewingKit ) && this is TailorShoppe ){ procTools = true; }
			else if ( dropped is ScribesPen && this is LibrarianShoppe ){ procTools = true; }
			else if ( dropped is MapmakersPen && this is CartographyShoppe ){ procTools = true; }
			else if ( dropped is FletcherTools && this is BowyerShoppe ){ procTools = true; }
			else if ( ( dropped is MortarPestle || 
						dropped is GodBrewing ) && this is AlchemistShoppe ){ procTools = true; }
			else if ( ( dropped is RollingPin || 
						dropped is Skillet ) && this is BakerShoppe ){ procTools = true; }
			else if ( ( dropped is DovetailSaw || 
						dropped is DrawKnife || 
						dropped is Hammer || 
						dropped is Froe || 
						dropped is Inshave || 
						dropped is WoodworkingTools || 
						dropped is JointingPlane || 
						dropped is MouldingPlane || 
						dropped is Nails || 
						dropped is Saw || 
						dropped is Scorp || 
						dropped is SmoothingPlane ) && this is CarpentryShoppe ){ procTools = true; }

			if ( Movable )
			{
				from.SendMessage( "This must be secured down in a home to use." );
			}
			//else if ( from.Kills > 0 )
			//{
           //     from.SendMessage("This is useless since no one deals with murderers!");
			//}
			else if ( dropped is SurgeonsKnife || dropped is GardenTool )
			{
				if ( ShoppeTools >= 1000 )
				{
					ShoppeTools = 1000;
					from.SendMessage( "Your shoppe is already full of tools." );
				}
				else
				{
					ShoppeTools = ShoppeTools + 50;
					if ( ShoppeTools >= 1000 )
					{
						ShoppeTools = 1000;
						from.SendMessage( "You add another tool but now your shoppe is full." );
					}
					from.PlaySound( 0x42 );
					dropped.Delete();
					return true;
				}
			}
			else if ( procResource )
			{
				if ( ShoppeResources >= 5000 )
				{
					ShoppeResources = 5000;
					from.SendMessage( "Your shoppe is already full of resources." );
				}
				else
				{
					ShoppeResources = ShoppeResources + dropped.Amount;
					if ( ShoppeResources >= 5000 )
					{
						ShoppeResources = 5000;
						from.SendMessage( "You add more resources but now your shoppe is full." );
					}
					from.PlaySound( 0x42 );
					dropped.Delete();
					return true;
				}
			}
			else if ( procTools )
			{
				if ( ShoppeTools >= 1000 )
				{
					ShoppeTools = 1000;
					from.SendMessage( "Your shoppe is already full of tools." );
				}
				else
				{
					BaseTool tool = (BaseTool)dropped;
					ShoppeTools = ShoppeTools + tool.UsesRemaining;
					if ( ShoppeTools >= 1000 )
					{
						ShoppeTools = 1000;
						from.SendMessage( "You add another tool but now your shoppe is full." );
					}
					from.PlaySound( 0x42 );
					dropped.Delete();
					return true;
				}
			}

			return false;
		}

		public void Customers()
		{
			if ( ShoppeOwner != null )
			{
				if (!this.Movable)
				{
					Server.Misc.Customers.CustomerCycle( ShoppeOwner, this );
				}

				if ( m_Timer != null )
					m_Timer.Stop();

					TimeSpan delay = TimeSpan.FromHours(Utility.RandomMinMax(2, 4));
					if (GetDelay())
						delay = TimeSpan.FromMinutes(Utility.RandomMinMax(20, 40));

					m_Timer = new CustomerTimer( this, delay ); 
				
				m_Timer.Start();
			}
			else
			{
				this.Delete();
			}
		}


		public bool GetDelay()
		{

			if ( !(ShoppeOwner is PlayerMobile) )
				return false;

			bool test = false;

			PlayerMobile pm = (PlayerMobile)ShoppeOwner;

			if ( pm.NpcGuild == NpcGuild.BlacksmithsGuild && this is BlacksmithShoppe )
			{
				test = true;
			}

			else if ( pm.NpcGuild == NpcGuild.NecromancersGuild && this is MorticianShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.DruidsGuild && this is HerbalistShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.CartographersGuild && this is CartographyShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.MerchantsGuild && Utility.RandomDouble() > 0.85 )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.TailorsGuild && this is TailorShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.CarpentersGuild && this is CarpentryShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.CulinariansGuild && this is BakerShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.TinkersGuild && this is TinkerShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.ArchersGuild && this is BowyerShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.AlchemistsGuild && this is AlchemistShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.RangersGuild && this is BowyerShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.LibrariansGuild && this is LibrarianShoppe )
				test = true;

			else if ( pm.NpcGuild == NpcGuild.MinersGuild && this is BlacksmithShoppe )
				test = true;

			return test;


		}
		public bool CheckAccess(Mobile m)
		{
			BaseHouse house = BaseHouse.FindHouseAt(this);

			if (house != null && (house.Public ? house.IsBanned(m) : !house.HasAccess(m)))
				return false;

			return (house != null && house.HasSecureAccess(m, m_Level));
		}

		public BaseShoppe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write((int)m_Level);
			writer.Write( (Mobile)ShoppeOwner);
			writer.Write( ShoppeName );
			writer.Write( ShoppeGold );
			writer.Write( ShoppeTools );
			writer.Write( ShoppeResources );
			writer.Write( ShoppeReputation );
			writer.Write( ShoppePage );
			writer.Write( ShelfTitle );
			writer.Write( ShelfItem );
			writer.Write( ShelfSkill );
			writer.Write( (int) ShelfGuild );
			writer.Write( ShelfTools );
			writer.Write( ShelfResources );
			writer.Write( ShelfSound );
			writer.Write( Customer01 );
			writer.Write( Customer02 );
			writer.Write( Customer03 );
			writer.Write( Customer04 );
			writer.Write( Customer05 );
			writer.Write( Customer06 );
			writer.Write( Customer07 );
			writer.Write( Customer08 );
			writer.Write( Customer09 );
			writer.Write( Customer10 );
			writer.Write( Customer11 );
			writer.Write( Customer12 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel)reader.ReadInt();
			ShoppeOwner = reader.ReadMobile();
			ShoppeName = reader.ReadString();
			ShoppeGold = reader.ReadInt();
			ShoppeTools = reader.ReadInt();
			ShoppeResources = reader.ReadInt();
			ShoppeReputation = reader.ReadInt();
			ShoppePage = reader.ReadInt();
			ShelfTitle = reader.ReadString();
			ShelfItem = reader.ReadInt();
			ShelfSkill = reader.ReadInt();
			ShelfGuild = (NpcGuild)reader.ReadInt();
			ShelfTools = reader.ReadString();
			ShelfResources = reader.ReadString();
			ShelfSound = reader.ReadInt();
			Customer01 = reader.ReadString();
			Customer02 = reader.ReadString();
			Customer03 = reader.ReadString();
			Customer04 = reader.ReadString();
			Customer05 = reader.ReadString();
			Customer06 = reader.ReadString();
			Customer07 = reader.ReadString();
			Customer08 = reader.ReadString();
			Customer09 = reader.ReadString();
			Customer10 = reader.ReadString();
			Customer11 = reader.ReadString();
			Customer12 = reader.ReadString();

			QuickTimer thisTimer = new QuickTimer( this ); 
			thisTimer.Start();
		}

		private Timer m_Timer;

		private class CustomerTimer : Timer
		{
			private BaseShoppe m_Shoppe;


			public CustomerTimer( BaseShoppe shoppe, TimeSpan delay ) : base( delay ) 
			{
				m_Shoppe = shoppe;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Shoppe.Customers();
			}
		}

		private class QuickTimer : Timer
		{
			private BaseShoppe m_Shoppe;

			public QuickTimer( BaseShoppe shoppe ) : base( TimeSpan.FromSeconds( 60.0 ) )
			{
				m_Shoppe = shoppe;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Shoppe.Customers();
			}
		}

        public SecureLevel m_Level;
        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level { get { return m_Level; } set { m_Level = value; } }

		// ------------------------------------------------------------------------------------------------------------------------------------------

		public Mobile ShoppeOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Shoppe_Owner { get{ return ShoppeOwner; } set{ ShoppeOwner = value; InvalidateProperties(); } }

		public string ShoppeName;
		[CommandProperty( AccessLevel.GameMaster )]
		public string Shoppe_Name { get{ return ShoppeName; } set{ ShoppeName = value; InvalidateProperties(); } }

		public int ShoppeGold;
		[CommandProperty(AccessLevel.Owner)]
		public int Shoppe_Gold{ get { return ShoppeGold; } set { ShoppeGold = value; InvalidateProperties(); } }

		public int ShoppeTools;
		[CommandProperty(AccessLevel.Owner)]
		public int Shoppe_Tools{ get { return ShoppeTools; } set { ShoppeTools = value; InvalidateProperties(); } }

		public int ShoppeResources;
		[CommandProperty(AccessLevel.Owner)]
		public int Shoppe_Resources{ get { return ShoppeResources; } set { ShoppeResources = value; InvalidateProperties(); } }

		public int ShoppeReputation;
		[CommandProperty(AccessLevel.Owner)]
		public int Shoppe_Reputation{ get { return ShoppeReputation; } set { ShoppeReputation = value; InvalidateProperties(); } }

		public int ShoppePage;
		[CommandProperty(AccessLevel.Owner)]
		public int Shoppe_Page{ get { return ShoppePage; } set { ShoppePage = value; InvalidateProperties(); } }

		// ------------------------------------------------------------------------------------------------------------------------------------------

		public string ShelfTitle;
		[CommandProperty(AccessLevel.Owner)]
		public string Shelf_Title{ get { return ShelfTitle; } set { ShelfTitle = value; InvalidateProperties(); } }

		public int ShelfItem;
		[CommandProperty(AccessLevel.Owner)]
		public int Shelf_Item{ get { return ShelfItem; } set { ShelfItem = value; InvalidateProperties(); } }

		public int ShelfSkill;
		[CommandProperty(AccessLevel.Owner)]
		public int Shelf_Skill{ get { return ShelfSkill; } set { ShelfSkill = value; InvalidateProperties(); } }

		public NpcGuild ShelfGuild;
		[CommandProperty(AccessLevel.Owner)]
		public NpcGuild Shelf_Guild{ get { return ShelfGuild; } set { ShelfGuild = value; InvalidateProperties(); } }

		public string ShelfTools;
		[CommandProperty(AccessLevel.Owner)]
		public string Shelf_Tools{ get { return ShelfTools; } set { ShelfTools = value; InvalidateProperties(); } }

		public string ShelfResources;
		[CommandProperty(AccessLevel.Owner)]
		public string Shelf_Resources{ get { return ShelfResources; } set { ShelfResources = value; InvalidateProperties(); } }

		public int ShelfSound;
		[CommandProperty(AccessLevel.Owner)]
		public int Shelf_Sound{ get { return ShelfSound; } set { ShelfSound = value; InvalidateProperties(); } }

		// ------------------------------------------------------------------------------------------------------------------------------------------

		public string Customer01;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_01{ get { return Customer01; } set { Customer01 = value; InvalidateProperties(); } }

		public string Customer02;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_02{ get { return Customer02; } set { Customer02 = value; InvalidateProperties(); } }

		public string Customer03;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_03{ get { return Customer03; } set { Customer03 = value; InvalidateProperties(); } }

		public string Customer04;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_04{ get { return Customer04; } set { Customer04 = value; InvalidateProperties(); } }

		public string Customer05;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_05{ get { return Customer05; } set { Customer05 = value; InvalidateProperties(); } }

		public string Customer06;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_06{ get { return Customer06; } set { Customer06 = value; InvalidateProperties(); } }

		public string Customer07;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_07{ get { return Customer07; } set { Customer07 = value; InvalidateProperties(); } }

		public string Customer08;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_08{ get { return Customer08; } set { Customer08 = value; InvalidateProperties(); } }

		public string Customer09;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_09{ get { return Customer09; } set { Customer09 = value; InvalidateProperties(); } }

		public string Customer10;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_10{ get { return Customer10; } set { Customer10 = value; InvalidateProperties(); } }

		public string Customer11;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_11{ get { return Customer11; } set { Customer11 = value; InvalidateProperties(); } }

		public string Customer12;
		[CommandProperty(AccessLevel.Owner)]
		public string Customer_12{ get { return Customer12; } set { Customer12 = value; InvalidateProperties(); } }

		public static int GetSkillValue( int job, Mobile from )
		{
			SkillName skill = SkillName.Alchemy;

			if ( job > 0 )
			{
				if ( job == 1 ){ skill = SkillName.Alchemy; }
				else if ( job == 2 ){ skill = SkillName.Anatomy; }
				else if ( job == 3 ){ skill = SkillName.AnimalLore; }
				else if ( job == 4 ){ skill = SkillName.AnimalTaming; }
				else if ( job == 5 ){ skill = SkillName.Archery; }
				else if ( job == 6 ){ skill = SkillName.ArmsLore; }
				else if ( job == 7 ){ skill = SkillName.Begging; }
				else if ( job == 8 ){ skill = SkillName.Blacksmith; }
				else if ( job == 9 ){ skill = SkillName.Bushido; }
				else if ( job == 10 ){ skill = SkillName.Camping; }
				else if ( job == 11 ){ skill = SkillName.Carpentry; }
				else if ( job == 12 ){ skill = SkillName.Cartography; }
				else if ( job == 13 ){ skill = SkillName.Chivalry; }
				else if ( job == 14 ){ skill = SkillName.Cooking; }
				else if ( job == 15 ){ skill = SkillName.DetectHidden; }
				else if ( job == 16 ){ skill = SkillName.Discordance; }
				else if ( job == 17 ){ skill = SkillName.EvalInt; }
				else if ( job == 18 ){ skill = SkillName.Fencing; }
				else if ( job == 19 ){ skill = SkillName.Fishing; }
				else if ( job == 20 ){ skill = SkillName.Fletching; }
				else if ( job == 21 ){ skill = SkillName.Focus; }
				else if ( job == 22 ){ skill = SkillName.Forensics; }
				else if ( job == 23 ){ skill = SkillName.Healing; }
				else if ( job == 24 ){ skill = SkillName.Herding; }
				else if ( job == 25 ){ skill = SkillName.Hiding; }
				else if ( job == 26 ){ skill = SkillName.Inscribe; }
				else if ( job == 27 ){ skill = SkillName.ItemID; }
				else if ( job == 28 ){ skill = SkillName.Lockpicking; }
				else if ( job == 29 ){ skill = SkillName.Lumberjacking; }
				else if ( job == 30 ){ skill = SkillName.Macing; }
				else if ( job == 31 ){ skill = SkillName.Magery; }
				else if ( job == 32 ){ skill = SkillName.MagicResist; }
				else if ( job == 33 ){ skill = SkillName.Meditation; }
				else if ( job == 34 ){ skill = SkillName.Mining; }
				else if ( job == 35 ){ skill = SkillName.Musicianship; }
				else if ( job == 36 ){ skill = SkillName.Necromancy; }
				else if ( job == 37 ){ skill = SkillName.Ninjitsu; }
				else if ( job == 38 ){ skill = SkillName.Parry; }
				else if ( job == 39 ){ skill = SkillName.Peacemaking; }
				else if ( job == 40 ){ skill = SkillName.Poisoning; }
				else if ( job == 41 ){ skill = SkillName.Provocation; }
				else if ( job == 42 ){ skill = SkillName.RemoveTrap; }
				else if ( job == 43 ){ skill = SkillName.Snooping; }
				else if ( job == 44 ){ skill = SkillName.SpiritSpeak; }
				else if ( job == 45 ){ skill = SkillName.Stealing; }
				else if ( job == 46 ){ skill = SkillName.Stealth; }
				else if ( job == 47 ){ skill = SkillName.Swords; }
				else if ( job == 48 ){ skill = SkillName.Tactics; }
				else if ( job == 49 ){ skill = SkillName.Tailoring; }
				else if ( job == 50 ){ skill = SkillName.TasteID; }
				else if ( job == 51 ){ skill = SkillName.Tinkering; }
				else if ( job == 52 ){ skill = SkillName.Tracking; }
				else if ( job == 53 ){ skill = SkillName.Veterinary; }
				else if ( job == 54 ){ skill = SkillName.Wrestling; }
				else if ( job == 55 ){ return (int)((from.Skills[SkillName.Alchemy].Value + from.Skills[SkillName.Cooking].Value + from.Skills[SkillName.AnimalLore].Value)/2); }
				else if ( job == 56 ){ return (int)((from.Skills[SkillName.Alchemy].Value + from.Skills[SkillName.Forensics].Value + from.Skills[SkillName.Necromancy].Value)/2); }

				return (int)(from.Skills[skill].Value);
			}

			return 0;
		}
	}

    public class ExplainShopped : Gump
    {
        private Mobile m_From;
        private Mobile m_Merchant;

        public ExplainShopped( Mobile from, Mobile merchant ): base( 25, 25 )
        {
            m_From = from;
            m_Merchant = merchant;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(0, 143, 155);
			AddImage(300, 143, 155);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 141, 129);
			AddImage(298, 141, 129);
			AddImage(7, 7, 133);
			AddImage(201, 46, 132);
			AddImage(362, 7, 138);
			AddItem(94, 84, 15587);
			AddHtml( 143, 104, 347, 257, @"<BODY><BIG><BASEFONT Color=#FCFF00>So you want to setup a Shoppe of your own. In order to do that, you would need your own home built somewhere in the land. These Shoppes usually demand you to part with 10,000 gold, but they can quickly pay for themselves if you are good at your craft. You may only have one type of this Shoppe at any given time. You will be the only one to use the Shoppe, but you may give permission to others to transfer the gold out into a bank check for themselves. Shoppes require to be stocked with tools and resources, and the Shoppe will indicate what those are. Simply drop such things onto your Shoppe to amass an inventory. When you drop tools onto your Shoppe, the number of tool uses will add to the Shoppe's tool count. A Shoppe may only hold 1,000 tools and 5,000 resources. These will get used up as you perform tasks for others. After a set period of time, customers will make requests of you which you can fulfill or refuse. Each request will display the task, who it is for, the amount of tools needed, the amount of resources required, your chance to fulfill the request (based on the difficulty and your skill), and the amount of reputation your Shoppe will acquire if you are successful.<br><br>If you fail to perform a selected task, or refuse to do it, your Shoppe's reputation will drop by that same value you would have been rewarded with. Word of mouth travels fast in the land and you will have less prestigious work if your reputation is low. Any gold earned will stay within the Shoppe until you single click the Shoppe and Transfer the funds out of it. Your Shoppe can have no more than 500,000 gold at a time, and you will not be able to conduct any more business in it until you withdraw the funds so it can amass more. The reputation for the Shoppe cannot go below 0, and it cannot go higher than 10,000. Again, the higher the reputation, the more lucrative work you will be asked to do. If you are a member of the associated crafting guild, your reputation will have a bonus toward it based on your crafting skill. If you have enough gold in your pack, do you want me to build a Shoppe for you?</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			AddHtml( 143, 74, 347, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>SETTING UP SHOPPE</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(145, 383, 4005, 4005, 1, GumpButtonType.Reply, 0);
			AddButton(411, 383, 4020, 4020, 0, GumpButtonType.Reply, 0);
			AddHtml( 185, 383, 61, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>Yes</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 451, 383, 61, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>No</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			if ( info.ButtonID > 0 )
			{
				Server.Items.BaseShoppe.GiveNewShoppe( m_From, m_Merchant );
			}
		}
	}

    public class ShoppeGump : Gump
    {
        private BaseShoppe m_Shop;
        private Mobile m_From;

        public ShoppeGump( BaseShoppe shoppe, Mobile from ): base( 25, 25 )
        {
            m_Shop = shoppe;
            m_From = from;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			if ( shoppe.ShoppePage == 3 )
			{
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(600, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 300, 155);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(0, 425, 155);
				AddImage(300, 425, 155);
				AddImage(600, 425, 155);
				AddImage(598, 298, 129);
				AddImage(300, 298, 129);
				AddImage(2, 298, 129);
				AddImage(7, 8, 133);
				AddImage(2, 423, 129);
				AddImage(298, 423, 129);
				AddImage(598, 423, 129);
				AddImage(8, 478, 142);
				AddImage(236, 47, 132);
				AddImage(536, 47, 132);
				AddImage(331, 686, 140);
				AddImage(331, 687, 140);
				AddImage(558, 687, 140);
				AddImage(848, 688, 143);
				AddImage(565, 47, 132);
				AddImage(848, 44, 143);
				AddItem(41, 384, 15389);
				AddHtml( 219, 70, 489, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>ABOUT SHOPPES</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(857, 13, 4017, 4017, 9, GumpButtonType.Reply, 0);
				AddImage(577, 323, 130);
				AddImage(377, 323, 130);
				AddImage(103, 323, 130);
				AddItem(102, 346, 3823);
				AddItem(113, 493, 10283);
				AddItem(114, 378, 10174);
				AddItem(102, 436, 4030);
				AddItem(102, 468, 10922);
				AddItem(94, 403, 3710);

				AddHtml( 153, 348, 717, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>Shows your shoppe's gold at the top, or for each individual contract.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 153, 378, 717, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>Shows your shoppe's tool count at the top, or tools needed for each individual contract.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 153, 408, 717, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>Shows your shoppe's resource count at the top, or resources needed for each individual contract.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 153, 438, 717, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>Shows your reputation bonus if you are a member of the associated guild.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 153, 468, 717, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>Shows your chance to successfully fulfill a contract.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 153, 498, 717, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>Shows your shoppe's reputation at the top, or for each individual contract.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 110, 112, 759, 200, @"<BODY><BIG><BASEFONT Color=#FCFF00>The world is filled with opportunity, where adventurers seek the help of other in order to achieve their goals. With filled coin purses, they seek experts in various crafts to acquire their skills. Some would need armor repaired, maps deciphered, potions concocted, scrolls translated, clothing fixed, or many other things. The merchants, in the cities and villages, often cannot keep up with the demand of these requests. This provides opportunity for those that practice a trade and have their own home from which to conduct business. Seek out a tradesman and see if they have an option for you to have them build you a Shoppe of your own. These Shoppes usually demand you to part with 10,000 gold, but they can quickly pay for themselves if you are good at your craft. You may only have one type of each Shoppe at any given time. So if you are skilled in two different types of crafts, then you can have a Shoppe for each. You will be the only one to use the Shoppe, but you may give permission to others to transfer the gold out into a bank check for themselves. Shoppes require to be stocked with tools and resources, and the Shoppe will indicate what those are at the bottom. Simply drop such things onto your Shoppe to amass an inventory. When you drop tools onto your Shoppe, the number of tool uses will add to the Shoppe's tool count. A Shoppe may only hold 1,000 tools and 5,000 resources. After a set period of time, customers will make requests of you which you can fulfill or refuse. Each request will display the task, who it is for, the amount of tools needed, the amount of resources required, your chance to fulfill the request (based on the difficulty and your skill), and the amount of reputation your Shoppe will acquire if you are successful.<br><br>If you fail to perform a selected task, or refuse to do it, your Shoppe's reputation will drop by that same value you would have been rewarded with. Word of mouth travels fast in the land and you will have less prestigious work if your reputation is low. If you find yourself reaching the lows of becoming a murderer, your Shoppe will be useless as no one deals with murderers. Any gold earned will stay within the Shoppe until you single click the Shoppe and Transfer the funds out of it. Your Shoppe can have no more than 500,000 gold at a time, and you will not be able to conduct any more business in it until you withdraw the funds so it can amass more. The reputation for the Shoppe cannot go below 0, and it cannot go higher than 10,000. Again, the higher the reputation, the more lucrative work you will be asked to do. If you are a member of the associated crafting guild, your reputation will have a bonus toward it based on your crafting skill.<br><br>If you want to earn more gold from your home, see the local provisioner and see if you can buy a merchant crate. These crates allow you to craft items, place them in the crate, and the Merchants Guild will pick up your wares after a set period of time. If you decide you want something back from the crate, make sure to take it out before the guild shows up.</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				AddImage(108, 538, 4023);
				AddImage(108, 594, 4020);
				AddHtml( 153, 536, 717, 40, @"<BODY><BIG><BASEFONT Color=#FFA200>This will attempt to fulfill the contract. If you fail, you will lose materials and reputation. If you succeed, you will gain reputation and gold, as well as using up the appropriate materials.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 153, 595, 717, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>This will refuse the request, but you will take a reduction in your shoppe's reputation by doing so.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			}
			else
			{
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(600, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 300, 155);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(0, 425, 155);
				AddImage(300, 425, 155);
				AddImage(600, 425, 155);
				AddImage(598, 298, 129);
				AddImage(300, 298, 129);
				AddImage(2, 298, 129);
				AddImage(7, 8, 133);
				AddImage(2, 423, 129);
				AddImage(298, 423, 129);
				AddImage(598, 423, 129);
				AddImage(8, 478, 142);
				AddImage(236, 47, 132);
				AddImage(536, 47, 132);
				AddImage(331, 686, 140);
				AddImage(331, 687, 140);
				AddImage(558, 687, 140);
				AddImage(848, 688, 143);
				AddImage(565, 47, 132);
				AddImage(848, 44, 143);

				// ------------------------------------------------------------------------------------

				AddButton(837, 13, 3610, 3610, 3, GumpButtonType.Reply, 0); // HELP

				if ( shoppe.ShoppePage == 1 )
				{
					AddButton(182, 69, 4014, 4014, 1, GumpButtonType.Reply, 0); // LEFT ARROW
				}
				else
				{
					AddButton(715, 70, 4005, 4005, 2, GumpButtonType.Reply, 0); // RIGHT ARROW
				}

				AddHtml( 219, 70, 489, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>" + shoppe.ShelfTitle + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(41, 384, shoppe.ShelfItem); // SHELF

				// ------------------------------------------------------------------------------------

				AddItem(93, 99, 3823);
				AddHtml( 132, 100, 113, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + shoppe.ShoppeGold + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // TOTAL GOLD

				AddItem(328, 99, 10174);
				AddHtml( 358, 100, 48, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + shoppe.ShoppeTools + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // TOTAL TOOLS

				AddItem(476, 96, 3710);
				AddHtml( 521, 100, 48, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + shoppe.ShoppeResources + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // TOTAL RESOURCES

				int guildBonus = 0;
				if ( ((PlayerMobile)from).NpcGuild == shoppe.ShelfGuild ){ guildBonus = 500 + (int)(Server.Items.BaseShoppe.GetSkillValue( shoppe.ShelfSkill, from ) * 5 ); }
				AddItem(631, 97, 10922);
				AddHtml( 682, 100, 48, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + guildBonus + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // GUILD BONUS 

				AddItem(808, 97, 10283);
				AddHtml( 833, 100, 48, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + shoppe.ShoppeReputation + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // TOTAL REPUTATION

				// ------------------------------------------------------------------------------------

				AddHtml( 261, 636, 123, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>Shoppe Owner:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 393, 635, 485, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + shoppe.ShoppeName + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(334, 667, 10174);
				AddHtml( 364, 667, 209, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>" + shoppe.ShelfTools + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // NEEDED TOOLS

				AddItem(597, 664, 3710);
				AddHtml( 640, 669, 209, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>" + shoppe.ShelfResources + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // NEEDED RESOURCES

				// ------------------------------------------------------------------------------------

				int entries = 6;
				int line = 0; if ( shoppe.ShoppePage == 1 ){ line = 6; }
				string customer = shoppe.Customer01;

				string taskJob = "";
				string taskWho = "";
				int taskStatus = 0;
				int taskGold = 0;
				int taskTools = 0;
				int taskResources = 0;
				int taskDifficulty = 0;
				int taskReputation = 0;
				int y = 175;

				while ( entries > 0 )
				{
					entries--;
					line++;

					int no = 100 + line;
					int yes = 200 + line;

					if ( line == 1 ){ customer = shoppe.Customer01; }					else if ( line == 2 ){ customer = shoppe.Customer02; }
					else if ( line == 3 ){ customer = shoppe.Customer03; }				else if ( line == 4 ){ customer = shoppe.Customer04; }
					else if ( line == 5 ){ customer = shoppe.Customer05; }				else if ( line == 6 ){ customer = shoppe.Customer06; }
					else if ( line == 7 ){ customer = shoppe.Customer07; }				else if ( line == 8 ){ customer = shoppe.Customer08; }
					else if ( line == 9 ){ customer = shoppe.Customer09; }				else if ( line == 10 ){ customer = shoppe.Customer10; }
					else if ( line == 11 ){ customer = shoppe.Customer11; }				else if ( line == 12 ){ customer = shoppe.Customer12; }

					taskJob = Server.Misc.Customers.GetDataElement( customer, 1 );
					taskWho = Server.Misc.Customers.GetDataElement( customer, 2 );
					taskStatus = Convert.ToInt32( Server.Misc.Customers.GetDataElement( customer, 3 ) );
					taskGold = Convert.ToInt32( Server.Misc.Customers.GetDataElement( customer, 4 ) ); //FINAL payout is in customers.cs 
					taskTools = Convert.ToInt32( Server.Misc.Customers.GetDataElement( customer, 5 ) );
					taskResources = Convert.ToInt32( Server.Misc.Customers.GetDataElement( customer, 6 ) );
					taskDifficulty = Server.Misc.Customers.GetChance( Server.Items.BaseShoppe.GetSkillValue( shoppe.ShelfSkill, from ), Convert.ToInt32( Server.Misc.Customers.GetDataElement( customer, 7 ) ) );
					taskReputation = Convert.ToInt32( Server.Misc.Customers.GetDataElement( customer, 8 ) );

					AddImage(104, y-49, 130);
					AddImage(371, y-49, 130);
					AddImage(584, y-49, 130);

					if ( customer != "" )
					{
						if ( taskDifficulty > 0 && shoppe.ShoppeTools >= taskTools && shoppe.ShoppeResources >= taskResources && ( shoppe.ShoppeGold + taskGold ) < 100001 )
						{
							AddButton(105, y, 4005, 4005, yes, GumpButtonType.Reply, 0); // WILL FIX IT
						}

						AddHtml( 104, y-30, 780, 20, @"<BODY><BIG><BASEFONT Color=#FCFF00>" + taskJob + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						AddHtml( 146, y, 319, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>for " + taskWho + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

						AddButton(854, y, 4020, 4020, no, GumpButtonType.Reply, 0); // WILL NOT FIX IT

						AddItem(462, y-1, 3823);
						AddHtml( 501, y, 48, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + taskGold + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // PAYMENT

						AddItem(561, y, 10174);
						AddHtml( 587, y, 30, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + taskTools + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // RESOURCES NEEDED

						AddItem(614, y-3, 3710);
						AddHtml( 659, y, 30, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + taskResources + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // RESOURCES NEEDED

						AddItem(691, y-2, 4030);
						AddHtml( 733, y, 48, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + taskDifficulty + "%</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // CHANCE TO SUCCEED

						AddItem(786, y-5, 10283);
						AddHtml( 811, y, 30, 20, @"<BODY><BIG><BASEFONT Color=#FFA200>" + taskReputation + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false); // REPUTATION GAINED
					}
					else
					{
						AddHtml( 104, y-30, 780, 20, @"<BODY><BIG><BASEFONT Color=#284F9F>Done - Awaiting Next Customer</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					}

					// ------------------------------------------------------------------------------------

					y=y+80;
				}
			}
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 1 )
			{
				m_Shop.ShoppePage = 0;
				from.SendGump( new ShoppeGump( m_Shop, m_From ) );
			}
			else if ( info.ButtonID == 2 )
			{
				from.SendSound( 0x4A );
				m_Shop.ShoppePage = 1;
				from.SendGump( new ShoppeGump( m_Shop, m_From ) );
			}
			else if ( info.ButtonID == 3 )
			{
				from.SendSound( 0x4A );
				m_Shop.ShoppePage = 3;
				from.SendGump( new ShoppeGump( m_Shop, m_From ) );
			}
			else if ( m_Shop.ShoppePage == 3 )
			{
				from.SendSound( 0x4A );
				m_Shop.ShoppePage = 0;
				from.SendGump( new ShoppeGump( m_Shop, m_From ) );
			}
			else if ( info.ButtonID > 200 )
			{
				Server.Misc.Customers.FillOrder( m_From, m_Shop, (info.ButtonID-200) );
				from.SendGump( new ShoppeGump( m_Shop, m_From ) );
			}
			else if ( info.ButtonID > 100 )
			{
				Server.Misc.Customers.CancelOrder( m_From, m_Shop, (info.ButtonID-100) );
				from.SendGump( new ShoppeGump( m_Shop, m_From ) );
			}
			else
			{
				from.SendSound( 0x4A );
			}
		}
	}
}