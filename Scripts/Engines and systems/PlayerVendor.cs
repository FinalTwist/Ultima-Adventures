using System.Net;
using System.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Prompts;
using Server.Targeting;
using Server.Misc;
using Server.Multis;
using Server.ContextMenus;

namespace Server.Mobiles
{
	[AttributeUsage( AttributeTargets.Class )]
	public class PlayerVendorTargetAttribute : Attribute
	{
		public PlayerVendorTargetAttribute()
		{
		}
	}

	public class VendorItem
	{
		private Item m_Item;
		private int m_Price;
		private string m_Description;
		private DateTime m_Created;

		private bool m_Valid;

		public Item Item{ get{ return m_Item; } }
		public int Price{ get{ return m_Price; } }

		public string FormattedPrice
		{
			get
			{
				if ( Core.ML )
					return m_Price.ToString( "N0", CultureInfo.GetCultureInfo( "en-US" ) );

				return m_Price.ToString();
			}
		}

		public string Description
		{
			get{ return m_Description; }
			set
			{
				if ( value != null )
					m_Description = value;
				else
					m_Description = "";

				if ( Valid )
					Item.InvalidateProperties();
			}
		}

		public DateTime Created{ get{ return m_Created; } }

		public bool IsForSale{ get{ return Price >= 0; } }
		public bool IsForFree{ get{ return Price == 0; } }

		public bool Valid{ get{ return m_Valid; } }

		public VendorItem( Item item, int price, string description, DateTime created )
		{
			m_Item = item;
			m_Price = price;

			if ( description != null )
				m_Description = description;
			else
				m_Description = "";

			m_Created = created;

			m_Valid = true;
		}

		public void Invalidate()
		{
			m_Valid = false;
		}
	}

	public class VendorBackpack : Backpack
	{
		public VendorBackpack()
		{
			Layer = Layer.Backpack;
			Weight = 1.0;
		}

		public override int DefaultMaxWeight{ get{ return 0; } }

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( !base.CheckHold( m, item, message, checkItems, plusItems, plusWeight ) )
				return false;


			if ( !BaseHouse.NewVendorSystem && Parent is PlayerVendor )
			{
				BaseHouse house = ((PlayerVendor)Parent).House;

				if ( house != null && house.IsAosRules && !house.CheckAosStorage( 1 + item.TotalItems + plusItems ) )
				{
					if ( message )
						m.SendLocalizedMessage( 1061839 ); // This action would exceed the secure storage limit of the house.

					return false;
				}
			}

			return true;
		}

		public override bool IsAccessibleTo( Mobile m )
		{
			return true;
		}

		public override bool CheckItemUse( Mobile from, Item item )
		{
			if ( !base.CheckItemUse( from, item ) )
				return false;

			if ( item is Container || item is Engines.BulkOrders.BulkOrderBook )
				return true;

			from.SendLocalizedMessage( 500447 ); // That is not accessible.
			return false;
		}

		public override bool CheckTarget( Mobile from, Target targ, object targeted )
		{
			if ( !base.CheckTarget( from, targ, targeted ) )
				return false;

			if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			return targ.GetType().IsDefined( typeof( PlayerVendorTargetAttribute ), false );
		}

		public override void GetChildContextMenuEntries( Mobile from, List<ContextMenuEntry> list, Item item )
		{
			base.GetChildContextMenuEntries( from, list, item );

			PlayerVendor pv = RootParent as PlayerVendor;

			if ( pv == null || pv.IsOwner( from ) )
				return;

			VendorItem vi = pv.GetVendorItem( item );

			if ( vi != null )
				list.Add( new BuyEntry( item ) );
		}

		private class BuyEntry : ContextMenuEntry
		{
			private Item m_Item;

			public BuyEntry( Item item ) : base( 6103 )
			{
				m_Item = item;
			}

			public override bool NonLocalUse{ get{ return true; } }

			public override void OnClick()
			{
				if ( m_Item.Deleted )
					return;

				PlayerVendor.TryToBuy( m_Item, Owner.From );
			}
		}

		public override void GetChildNameProperties( ObjectPropertyList list, Item item )
		{
			base.GetChildNameProperties( list, item );

			PlayerVendor pv = RootParent as PlayerVendor;

			if ( pv == null )
				return;

			VendorItem vi = pv.GetVendorItem( item );

			if ( vi == null )
				return;

			if ( !vi.IsForSale )
				list.Add( 1043307 ); // Price: Not for sale.
			else if ( vi.IsForFree )
				list.Add( 1043306 ); // Price: FREE!
			else
				list.Add( 1043304, vi.FormattedPrice ); // Price: ~1_COST~
		}

		public override void GetChildProperties( ObjectPropertyList list, Item item )
		{
			base.GetChildProperties( list, item );

			PlayerVendor pv = RootParent as PlayerVendor;

			if ( pv == null )
				return;

			VendorItem vi = pv.GetVendorItem( item );

			if ( vi != null && vi.Description != null && vi.Description.Length > 0 )
				list.Add( 1043305, vi.Description ); // <br>Seller's Description:<br>"~1_DESC~"
		}

		public override void OnSingleClickContained( Mobile from, Item item )
		{
			if ( RootParent is PlayerVendor )
			{
				PlayerVendor vendor = (PlayerVendor)RootParent;

				VendorItem vi = vendor.GetVendorItem( item );

				if ( vi != null )
				{
					if ( !vi.IsForSale )
						item.LabelTo( from, 1043307 ); // Price: Not for sale.
					else if ( vi.IsForFree )
						item.LabelTo( from, 1043306 ); // Price: FREE!
					else
						item.LabelTo( from, 1043304, vi.FormattedPrice ); // Price: ~1_COST~

					if ( !String.IsNullOrEmpty( vi.Description ) )
					{
						// The localized message (1043305) is no longer valid - <br>Seller's Description:<br>"~1_DESC~"
						item.LabelTo( from, "Description: {0}", vi.Description );
					}
				}
			}

			base.OnSingleClickContained( from, item );
		}

		public VendorBackpack( Serial serial ) : base( serial )
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
		}
	}

	public class PlayerVendor : Mobile
	{
		private Hashtable m_SellItems;

		private Mobile m_Owner;
		private BaseHouse m_House;

		private int m_BankAccount;
		private int m_HoldGold;

		private string m_ShopName;

		private Timer m_PayTimer;
		private DateTime m_NextPayTime;

		private Timer m_RandBuyTimer;
		private Timer m_AdTimer;

		private List<string> m_RecentSales = new List<string>();

		private PlayerVendorPlaceholder m_Placeholder;

		private static bool EnableSimulatedSales( Mobile vendor)
		{
			return MyServerSettings.EnableSimulatedPVSales( vendor ); // master toggle for simulated sales
		}
		private static bool EnableRandomAds( Mobile vendor)
		{ return MyServerSettings.EnableRandomPVAdvertisements( vendor );} // if true, items for sale will be advertised on AdTimer tick
		private static bool ChargeForServicePerUODay(Mobile vendor) 
		{ return MyServerSettings.PVChargeForServicePerUODay( vendor ); }// if true, the vendor will charge money for its service every UO day (and not every real day)
		private static bool EnableSpeech = true; // if enabled, this NPC will respond to certain keywords when talked to

		// how often and how rigorously to perform a sales check
		private static int MaxIterationsPerCheck = 8; // was originally set to 10
		private static int MinCheckPeriodInMinutes = 15;
		private static int MaxCheckPeriodInMinutes = 75;

		private static double AdPeriodInSeconds = 55.0; // how often to advertise the sales
		private static int NumRecentSalesToRemember = 50; // how many sales will be remembered and reported on "<NAME> report" or "<NAME> report N", where N is the number of sales

		private static bool LowPriceBoost = false; // may make it easier to game the system on low sell price (1 gp) items
		private static bool HarderBagSale = true; // if enabled, makes it harder to get a decent price on items sold en masse in bags
		private static int BarterValue = 33;
		private static int RichSuckerChance = 997;
		private static int ImprovedPriceModChance = 97;
		private static int MaxImprovedPriceMod = 4;
		private static int MinImprovedPriceMod = 2;
		private static int MinAttrsMultiplier = 50; // %
		private static int MaxAttrsMultiplier = 200; // %
		private static int RichSuckerMinPrice = 100;
		private static int RichSuckerMinPriceMultiplier = 3;
		private static int RichSuckerMaxPriceMultiplier = 10;
		private static int MinPriceModifier = 2;
		private static int MaxPriceModifier = 10;
		private static bool AnnounceSalesOnlyIfClose = true;
		private static int DistToAnnounceSales = 15;
		private static int MinimalPriceMaxBoost = 4;
		private static int SBListMaxRandom = 10;
		private static int SBListMaxFixed = 10;
		private static int PriceThresholdForAttributeCheck = 50000; // set to a low value (100-200) to only do this to cheap items
		private static bool IncreasePriceBasedOnNumberOfProps = true; // if true, items with many beneficial props will sell for more money
		private static int AttrsMod1Or2Props = 1; // price multiplier if the item has 1-2 beneficial props
		private static int AttrsMod3Or4Props = 2;
		private static int AttrsMod5Or6Props = 5;
		private static int AttrsMod7Or8Props = 10;
		private static int AttrsMod9OrMoreProps = 20; // price multiplier if the item has 9+ beneficial props
		private static int AttrsIntensityThreshold = 10; // threshold for attribute intensity % to count toward the number of beneficial props (0 = any intensity, otherwise needs to be greater than the percentage specified)
		private static int IntensityPercentile = 20; // for each N% intensity, give a payout bonus equal to intensity multiplied by the multiplier below
		private static int IntensityMultiplier = 2; // for each N% intensity, give an additional intensity multiplier

		private static int PriceCutOnMaxDurability25 = 90; // %
		private static int PriceCutOnMaxDurability20 = 75; // %
		private static int PriceCutOnMaxDurability15 = 50; // %
		private static int PriceCutOnMaxDurability10 = 25; // %
		private static int PriceCutOnMaxDurability5 = 5; // %
		private static int PriceCutOnMaxDurability3 = 1; // %

		private static int FinalPriceModifier = 24; // % - the final price after all bonuses will be modified to this percentage (e.g. the final price of 1000 will be set to 800 if the 80% modifier is applied); use this to fine tune the prices without affecting the overall balance above
		
		public PlayerVendor( Mobile owner, BaseHouse house )
		{
			Owner = owner;
			House = house;

			if ( BaseHouse.NewVendorSystem )
			{
				m_BankAccount = 0;
				m_HoldGold = 4;
			}
			else
			{
				m_BankAccount = 1000;
				m_HoldGold = 0;
			}

			ShopName = "Shop Not Yet Named";

			m_SellItems = new Hashtable();

			CantWalk = true;

			if ( !Core.AOS )
				NameHue = 0x35;

			InitStats( 200, 200, 200 ); 
			InitBody();
			InitOutfit();

			TimeSpan delay = PayTimer.GetInterval(this);

			m_PayTimer = new PayTimer( this, delay );
			m_PayTimer.Start();

			m_NextPayTime = DateTime.UtcNow + delay;

			m_RandBuyTimer = new RandomBuyTimer( this, RandomBuyTimer.GetInterval() );
			m_RandBuyTimer.Start();

			m_AdTimer = new RandomAdTimer( this, RandomAdTimer.GetInterval((Mobile)this) );
			m_AdTimer.Start();
		}

		public PlayerVendor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( m_RecentSales.Count ); // recent sales
			for (int i = 0; i < m_RecentSales.Count; i++)
			    writer.Write( m_RecentSales[i] );

			writer.Write( (bool) BaseHouse.NewVendorSystem );
			writer.Write( (string) m_ShopName );
			writer.WriteDeltaTime( (DateTime) m_NextPayTime );
			writer.Write( (Item) House );

			writer.Write( (Mobile) m_Owner );
			writer.Write( (int) m_BankAccount );
			writer.Write( (int) m_HoldGold );

			writer.Write( (int) m_SellItems.Count );
			foreach ( VendorItem vi in m_SellItems.Values )
			{
				writer.Write( (Item) vi.Item );
				writer.Write( (int) vi.Price );
				writer.Write( (string) vi.Description );

				writer.Write( (DateTime) vi.Created );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			bool newVendorSystem = false;

			switch ( version )
			{
				case 2:
					int cnt = reader.ReadInt();
					m_RecentSales.Clear();
					for (int i = 0; i < cnt; i++)
					    m_RecentSales.Add( reader.ReadString() );
					goto case 1;
				case 1:
				{
					newVendorSystem = reader.ReadBool();
					m_ShopName = reader.ReadString();
					m_NextPayTime = reader.ReadDeltaTime();
					House = (BaseHouse) reader.ReadItem();

					goto case 0;
				}
				case 0:
				{
					m_Owner = reader.ReadMobile();
					m_BankAccount = reader.ReadInt();
					m_HoldGold = reader.ReadInt();

					m_SellItems = new Hashtable();

					int count = reader.ReadInt();
					for ( int i = 0; i < count; i++ )
					{
						Item item = reader.ReadItem();

						int price = reader.ReadInt();
						if ( price > 100000000 )
							price = 100000000;

						string description = reader.ReadString();

						DateTime created = version < 1 ? DateTime.UtcNow : reader.ReadDateTime();

						if ( item != null )
						{
							SetVendorItem( item, version < 1 && price <= 0 ? -1 : price, description, created );
						}
					}

					break;	
				}
			}

			bool newVendorSystemActivated = BaseHouse.NewVendorSystem && !newVendorSystem;

			if ( version < 1 || newVendorSystemActivated )
			{
				if ( version < 1 )
				{
					m_ShopName = "Shop Not Yet Named";
					Timer.DelayCall( TimeSpan.Zero, new TimerStateCallback( UpgradeFromVersion0 ), newVendorSystemActivated );
				}
				else
				{
					Timer.DelayCall( TimeSpan.Zero, new TimerCallback( FixDresswear ) );
				}

				m_NextPayTime = DateTime.UtcNow + PayTimer.GetInterval(this);

				if ( newVendorSystemActivated )
				{
					m_HoldGold += m_BankAccount;
					m_BankAccount = 0;
				}
			}

			TimeSpan delay = m_NextPayTime - DateTime.UtcNow;

			m_PayTimer = new PayTimer( this, delay > TimeSpan.Zero ? delay : TimeSpan.Zero );
			m_PayTimer.Start();

			m_RandBuyTimer = new RandomBuyTimer( this, RandomBuyTimer.GetInterval() );
			m_RandBuyTimer.Start();

			m_AdTimer = new RandomAdTimer( this, RandomAdTimer.GetInterval((Mobile)this) );
			m_AdTimer.Start();

			Blessed = false;

			if ( Core.AOS && NameHue == 0x35 )
				NameHue = -1;
		}

		private void UpgradeFromVersion0( object newVendorSystem )
		{
			List<Item> toRemove = new List<Item>();

			foreach ( VendorItem vi in m_SellItems.Values )
				if ( !CanBeVendorItem( vi.Item ) )
					toRemove.Add( vi.Item );
				else
					vi.Description = Utility.FixHtml( vi.Description );

			foreach ( Item item in toRemove )
				RemoveVendorItem( item );

			House = BaseHouse.FindHouseAt( this );

			if ( (bool) newVendorSystem )
				ActivateNewVendorSystem();
		}

		private void ActivateNewVendorSystem()
		{
			FixDresswear();

			if ( House != null && !House.IsOwner( Owner ) )
				Destroy( false );
		}

		public void InitBody()
		{
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			if ( !Core.AOS )
				NameHue = 0x35;

			if ( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
			}
		}

		public virtual void InitOutfit()
		{
			Item item = new FancyShirt( Utility.RandomNeutralHue() );
			item.Layer = Layer.InnerTorso;
			AddItem( item );
			AddItem( new LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new BodySash( Utility.RandomNeutralHue() ) );
			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new Cloak( Utility.RandomNeutralHue() ) );

			Utility.AssignRandomHair( this );

			Container pack = new VendorBackpack();
			pack.Movable = false;
			AddItem( pack );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BankAccount
		{
			get{ return m_BankAccount; }
			set{ m_BankAccount = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HoldGold
		{
			get{ return m_HoldGold; }
			set{ m_HoldGold = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string ShopName
		{
			get{ return m_ShopName; }
			set
			{
				if ( value == null )
					m_ShopName = "";
				else
					m_ShopName = value;

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextPayTime
		{
			get{ return m_NextPayTime; }
		}

		public PlayerVendorPlaceholder Placeholder
		{
			get{ return m_Placeholder; } 
			set{ m_Placeholder = value; }
		}

		public BaseHouse House
		{
			get{ return m_House; }
			set
			{
				if ( m_House != null )
					m_House.PlayerVendors.Remove( this );

				if ( value != null )
					value.PlayerVendors.Add( this );

				m_House = value;
			}
		}

		public int ChargePerDay
		{
			get
			{ 
				if (!EnableSimulatedSales(this))
				{
					return 1;
				}
				if ( BaseHouse.NewVendorSystem )
				{
					return ChargePerRealWorldDay / 12;
				}
				else
				{
					long total = 0;
					foreach ( VendorItem vi in m_SellItems.Values )
					{
						total += vi.Price;
					}

					total -= 500;

					if ( total < 0 )
						total = 0;

					return (int)( 20 + (total / 500) );
				}
			}
		}

		public int ChargePerRealWorldDay
		{
			get
			{
				if (!EnableSimulatedSales(this))
				{
					return 1;
				}
				if ( BaseHouse.NewVendorSystem )
				{
					long total = 0;
					foreach ( VendorItem vi in m_SellItems.Values )
					{
						total += vi.Price;
					}

					return (int)( 60 + (total / 500) * 3 );
				}
				else
				{
					return ChargePerDay * 12;
				}
			}
		}

		public virtual bool IsOwner( Mobile m )
		{
			if ( m.AccessLevel >= AccessLevel.GameMaster )
				return true;

			if ( BaseHouse.NewVendorSystem && House != null )
			{
				return House.IsOwner( m );
			}
			else
			{
				return m == Owner;
			}
		}

		protected List<Item> GetItems()
		{
			List<Item> list = new List<Item>();

			foreach ( Item item in this.Items )
				if ( item.Movable && item != this.Backpack && item.Layer != Layer.Hair && item.Layer != Layer.FacialHair )
					list.Add( item );

			if ( this.Backpack != null )
				list.AddRange( this.Backpack.Items );

			return list;
		}

		public virtual void Destroy( bool toBackpack )
		{
			Return();

			if ( !BaseHouse.NewVendorSystem )
				FixDresswear();

			/* Possible cases regarding item return:
			 * 
			 * 1. No item must be returned
			 *       -> do nothing.
			 * 2. ( toBackpack is false OR the vendor is in the internal map ) AND the vendor is associated with a AOS house
			 *       -> put the items into the moving crate or a vendor inventory,
			 *          depending on whether the vendor owner is also the house owner.
			 * 3. ( toBackpack is true OR the vendor isn't associated with any AOS house ) AND the vendor isn't in the internal map
			 *       -> put the items into a backpack.
			 * 4. The vendor isn't associated with any house AND it's in the internal map
			 *       -> do nothing (we can't do anything).
			 */

			List<Item> list = GetItems();

			if ( list.Count > 0 || HoldGold > 0 ) // No case 1
			{
				if ( ( !toBackpack || this.Map == Map.Internal ) && House != null && House.IsAosRules ) // Case 2
				{
					if ( House.IsOwner( Owner ) ) // Move to moving crate
					{
						if ( House.MovingCrate == null )
							House.MovingCrate = new MovingCrate( House );

						if ( HoldGold > 0 )
							Banker.Deposit( House.MovingCrate, HoldGold );

						foreach ( Item item in list )
						{
							House.MovingCrate.DropItem( item );
						}
					}
					else // Move to vendor inventory
					{
						VendorInventory inventory = new VendorInventory( House, Owner, Name, ShopName );
						inventory.Gold = HoldGold;

						foreach ( Item item in list )
						{
							inventory.AddItem( item );
						}

						House.VendorInventories.Add( inventory );
					}
				}
				else if ( ( toBackpack || House == null || !House.IsAosRules ) && this.Map != Map.Internal ) // Case 3 - Move to backpack
				{
					Container backpack = new Backpack();

					if ( HoldGold > 0 )
						Banker.Deposit( backpack, HoldGold );

					foreach ( Item item in list )
					{
						backpack.DropItem( item );
					}

					backpack.MoveToWorld( this.Location, this.Map );
				}
			}

			Delete();
		}

		private void FixDresswear()
		{
			for ( int i = 0; i < Items.Count; ++i )
			{
				Item item = Items[i] as Item;

				if ( item is BaseHat )
					item.Layer = Layer.Helm;
				else if ( item is BaseMiddleTorso )
					item.Layer = Layer.MiddleTorso;
				else if ( item is BaseOuterLegs )
					item.Layer = Layer.OuterLegs;
				else if ( item is BaseOuterTorso )
					item.Layer = Layer.OuterTorso;
				else if ( item is BasePants )
					item.Layer = Layer.Pants;
				else if ( item is BaseShirt )
					item.Layer = Layer.Shirt;
				else if ( item is BaseWaist )
					item.Layer = Layer.Waist;
				else if ( item is BaseShoes )
				{
					if ( item is Sandals )
						item.Hue = 0;

					item.Layer = Layer.Shoes;
				}
			}
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			m_PayTimer.Stop();
			m_RandBuyTimer.Stop();
			m_AdTimer.Stop();

			House = null;

			if ( Placeholder != null )
				Placeholder.Delete();
		}

		public override bool IsSnoop( Mobile from )
		{
			return false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( BaseHouse.NewVendorSystem )
			{
				list.Add( 1062449, ShopName ); // Shop Name: ~1_NAME~
			}
		}

		public VendorItem GetVendorItem( Item item )
		{
			return (VendorItem) m_SellItems[item];
		}

		private VendorItem SetVendorItem( Item item, int price, string description )
		{
			return SetVendorItem( item, price, description, DateTime.UtcNow );
		}

		private VendorItem SetVendorItem( Item item, int price, string description, DateTime created )
		{
			RemoveVendorItem( item );

			VendorItem vi = new VendorItem( item, price, description, created );
			m_SellItems[item] = vi;

			item.InvalidateProperties();

			return vi;
		}

		private void RemoveVendorItem( Item item )
		{
			VendorItem vi = GetVendorItem( item );

			if ( vi != null )
			{
				vi.Invalidate();
				m_SellItems.Remove( item );

				foreach ( Item subItem in item.Items )
				{
					RemoveVendorItem( subItem );
				}

				item.InvalidateProperties();
			}
		}

		private bool CanBeVendorItem( Item item )
		{
			Item parent = item.Parent as Item;

			if ( parent == this.Backpack )
				return true;

			if ( parent is Container )
			{
				VendorItem parentVI = GetVendorItem( parent );

				if ( parentVI != null )
					return !parentVI.IsForSale;
			}

			return false;
		}

		public override void OnSubItemAdded( Item item )
		{
			base.OnSubItemAdded( item );

			if ( GetVendorItem( item ) == null && CanBeVendorItem( item ) )
			{
				// TODO: default price should be dependent to the type of object
				SetVendorItem( item, 999, "" );
			}
		}

		public override void OnSubItemRemoved( Item item )
		{
			base.OnSubItemRemoved( item );

			if ( item.GetBounce() == null )
				RemoveVendorItem( item );
		}

		public override void OnSubItemBounceCleared( Item item )
		{
			base.OnSubItemBounceCleared( item );

			if ( !CanBeVendorItem( item ) )
				RemoveVendorItem( item );
		}

		public override void OnItemRemoved( Item item )
		{
			base.OnItemRemoved( item );

			if ( item == this.Backpack )
			{
				foreach ( Item subItem in item.Items )
				{
					RemoveVendorItem( subItem );
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( !IsOwner( from ) )
			{
				SayTo( from, 503209 ); // I can only take item from the shop owner.
				return false;
			}

			if ( item is Gold )
			{
				if ( BaseHouse.NewVendorSystem )
				{
					if ( this.HoldGold < 1000000 )
					{
						SayTo( from, 503210 ); // I'll take that to fund my services.

						this.HoldGold += item.Amount;
						item.Delete();

						return true;
					}
					else
					{
						from.SendLocalizedMessage( 1062493 ); // Your vendor has sufficient funds for operation and cannot accept this gold.

						return false;
					}
				}
				else
				{
					if ( this.BankAccount < 1000000 )
					{
						SayTo( from, 503210 ); // I'll take that to fund my services.

						this.BankAccount += item.Amount;
						item.Delete();

						return true;
					}
					else
					{
						from.SendLocalizedMessage( 1062493 ); // Your vendor has sufficient funds for operation and cannot accept this gold.

						return false;
					}
				}
			}
			else
			{
				bool newItem = ( GetVendorItem( item ) == null );

				if ( this.Backpack != null && this.Backpack.TryDropItem( from, item, false ) )
				{
					if ( newItem )
						OnItemGiven( from, item );

					return true;
				}
				else
				{
					SayTo( from, 503211 ); // I can't carry any more.
					return false;
				}
			}
		}

		public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
		{
			if ( IsOwner( from ) )
			{
				if ( GetVendorItem( item ) == null )
				{
					// We must wait until the item is added
					Timer.DelayCall( TimeSpan.Zero, new TimerStateCallback( NonLocalDropCallback ), new object[] { from, item } );
				}

				return true;
			}
			else
			{
				SayTo( from, 503209 ); // I can only take item from the shop owner.
				return false;
			}
		}

		private void NonLocalDropCallback( object state )
		{
			object[] aState = (object[]) state;

			Mobile from = (Mobile) aState[0];
			Item item = (Item) aState[1];

			OnItemGiven( from, item );
		}

		private void OnItemGiven( Mobile from, Item item )
		{
			VendorItem vi = GetVendorItem( item );

			if ( vi != null )
			{
				string name;
				if ( !String.IsNullOrEmpty( item.Name ) )
					name = item.Name;
				else
					name = "#" + item.LabelNumber.ToString();

				from.SendLocalizedMessage( 1043303, name ); // Type in a price and description for ~1_ITEM~ (ESC=not for sale)
				from.Prompt = new VendorPricePrompt( this, vi );
			}
		}

		public override bool AllowEquipFrom( Mobile from )
		{
			if ( BaseHouse.NewVendorSystem && IsOwner( from ) )
				return true;

			return base.AllowEquipFrom( from );
		}

		public override bool CheckNonlocalLift( Mobile from, Item item )
		{
			if ( item.IsChildOf( this.Backpack ) )
			{
				if ( IsOwner( from ) )
				{
					return true;
				}
				else
				{
					SayTo( from, 503223 ); // If you'd like to purchase an item, just ask.
					return false;
				}
			}
			else if ( BaseHouse.NewVendorSystem && IsOwner( from ) )
			{
				return true;
			}

			return base.CheckNonlocalLift( from, item );
		}

		public bool CanInteractWith( Mobile from, bool ownerOnly )
		{
			if ( !from.CanSee( this ) || !Utility.InUpdateRange( from, this ) || !from.CheckAlive() )
				return false;

			if ( ownerOnly )
				return IsOwner( from );

			if ( House != null && House.IsBanned( from ) && !IsOwner( from ) )
			{
				from.SendLocalizedMessage( 1062674 ); // You can't shop from this home as you have been banned from this establishment.
				return false;
			}

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsOwner( from ) )
			{
				SendOwnerGump( from );
			}
			else if ( CanInteractWith( from, false ) )
			{
				OpenBackpack( from );
			}
		}

		public override void DisplayPaperdollTo( Mobile m )
		{
			if ( BaseHouse.NewVendorSystem )
			{
				base.DisplayPaperdollTo( m );
			}
			else if ( CanInteractWith( m, false ) )
			{
				OpenBackpack( m );
			}
		}

		public void SendOwnerGump( Mobile to )
		{
			if ( BaseHouse.NewVendorSystem )
			{
				to.CloseGump( typeof( NewPlayerVendorOwnerGump ) );
				to.CloseGump( typeof( NewPlayerVendorCustomizeGump ) );

				to.SendGump( new NewPlayerVendorOwnerGump( this ) );
			}
			else
			{
				to.CloseGump( typeof( PlayerVendorOwnerGump ) );
				to.CloseGump( typeof( PlayerVendorCustomizeGump ) );

				to.SendGump( new PlayerVendorOwnerGump( this ) );
			}
		}

		public void OpenBackpack( Mobile from )
		{
			if ( this.Backpack != null )
			{
				SayTo( from, IsOwner( from ) ? 1010642 : 503208 ); // Take a look at my/your goods.

				this.Backpack.DisplayTo( from );
			}
		}

		public static void TryToBuy( Item item, Mobile from )
		{
			PlayerVendor vendor = item.RootParent as PlayerVendor;

			if ( vendor == null || !vendor.CanInteractWith( from, false ) )
				return;

			if ( vendor.IsOwner( from ) )
			{
				vendor.SayTo( from, 503212 ); // You own this shop, just take what you want.
				return;
			}

			VendorItem vi = vendor.GetVendorItem( item );

			if ( vi == null )
			{
				vendor.SayTo( from, 503216 ); // You can't buy that.
			}
			else if ( !vi.IsForSale )
			{
				vendor.SayTo( from, 503202 ); // This item is not for sale.
			}
			else if ( vi.Created + TimeSpan.FromMinutes( 1.0 ) > DateTime.UtcNow )
			{
				from.SendMessage( "You cannot buy this item right now.  Please wait one minute and try again." );
			}
			else
			{
				from.CloseGump( typeof( PlayerVendorBuyGump ) );
				from.SendGump( new PlayerVendorBuyGump( vendor, vi ) );
			}
		}

		public void CollectGold( Mobile to )
		{
			if ( HoldGold > 0 )
			{
				SayTo( to, "How much of the {0} that I'm holding would you like?", HoldGold.ToString() );
				to.SendMessage( "Enter the amount of gold you wish to withdraw (ESC = CANCEL):" );

				to.Prompt = new CollectGoldPrompt( this );
			}
			else
			{
				SayTo( to, 503215 ); // I am holding no gold for you.
			}
		}

		public int GiveGold( Mobile to, int amount )
		{
			if ( amount <= 0 )
				return 0;

			if ( amount > HoldGold )
			{
				SayTo( to, "I'm sorry, but I'm only holding {0} gold for you.", HoldGold.ToString() );
				return 0;
			}

			int amountGiven = Banker.DepositUpTo( to, amount );
			HoldGold -= amountGiven;

			if ( amountGiven > 0 )
			{
				to.SendLocalizedMessage( 1060397, amountGiven.ToString() ); // ~1_AMOUNT~ gold has been deposited into your bank box.
			}

			if ( amountGiven == 0 )
			{
				SayTo( to, 1070755 ); // Your bank box cannot hold the gold you are requesting.  I will keep the gold until you can take it.
			}
			else if ( amount > amountGiven )
			{
				SayTo( to, 1070756 ); // I can only give you part of the gold now, as your bank box is too full to hold the full amount.
			}
			else if ( HoldGold > 0 )
			{
				SayTo( to, 1042639 ); // Your gold has been transferred.
			}
			else
			{
				SayTo( to, 503234 ); // All the gold I have been carrying for you has been deposited into your bank account.
			}

			return amountGiven;
		}

		public void Dismiss( Mobile from )
		{
			Container pack = this.Backpack;

			if ( pack != null && pack.Items.Count > 0 )
			{
				SayTo( from, 1038325 ); // You cannot dismiss me while I am holding your goods.
				return;
			}

			if ( HoldGold > 0 )
			{
				GiveGold( from, HoldGold );

				if ( HoldGold > 0 )
					return;
			}

			Destroy( true );
		}

		public void Rename( Mobile from )
		{
			from.SendLocalizedMessage( 1062494 ); // Enter a new name for your vendor (20 characters max):

			from.Prompt = new VendorNamePrompt( this );
		}

		public void RenameShop( Mobile from )
		{
			from.SendLocalizedMessage( 1062433 ); // Enter a new name for your shop (20 chars max):

			from.Prompt = new ShopNamePrompt( this );
		}

		public bool CheckTeleport( Mobile to )
		{
			if ( Deleted || !IsOwner( to ) || House == null || this.Map == Map.Internal )
				return false;

			if ( House.IsInside( to ) || to.Map != House.Map || !House.InRange( to, 5 ) )
				return false;

			if ( Placeholder == null )
			{
				Placeholder = new PlayerVendorPlaceholder( this );
				Placeholder.MoveToWorld( this.Location, this.Map );

				this.MoveToWorld( to.Location, to.Map );

				to.SendLocalizedMessage( 1062431 ); // This vendor has been moved out of the house to your current location temporarily.  The vendor will return home automatically after two minutes have passed once you are done managing its inventory or customizing it.
			}
			else
			{
				Placeholder.RestartTimer();

				to.SendLocalizedMessage( 1062430 ); // This vendor is currently temporarily in a location outside its house.  The vendor will return home automatically after two minutes have passed once you are done managing its inventory or customizing it.
			}

			return true;
		}

		public void Return()
		{
			if ( Placeholder != null )
				Placeholder.Delete();
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && Placeholder != null && IsOwner( from ) )
			{
				list.Add( new ReturnVendorEntry( this ) );
			}

			base.GetContextMenuEntries( from, list );
		}

		private class ReturnVendorEntry : ContextMenuEntry
		{
			private PlayerVendor m_Vendor;

			public ReturnVendorEntry( PlayerVendor vendor ) : base( 6214 )
			{
				m_Vendor = vendor;
			}

			public override void OnClick()
			{
				Mobile from = Owner.From;

				if ( !m_Vendor.Deleted && m_Vendor.IsOwner( from ) && from.CheckAlive() )
					m_Vendor.Return();
			}
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			return ( from.Alive && from.GetDistanceToSqrt( this ) <= 3 );
		}

		public bool WasNamed( string speech )
		{
			return this.Name != null && Insensitive.StartsWith( speech, this.Name );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( e.Handled || !from.Alive || from.GetDistanceToSqrt( this ) > 3 )
				return;

			if ( e.HasKeyword( 0x3C ) || (e.HasKeyword( 0x171 ) && WasNamed( e.Speech ))  ) // vendor buy, *buy*
			{
				if ( IsOwner( from ) )
				{
					SayTo( from, 503212 ); // You own this shop, just take what you want.
				}
				else if ( House == null || !House.IsBanned( from ) )
				{
					from.SendLocalizedMessage( 503213 ); // Select the item you wish to buy.
					from.Target = new PVBuyTarget();

					e.Handled = true;
				}
			} 
			else if ( e.HasKeyword( 0x3D ) || (e.HasKeyword( 0x172 ) && WasNamed( e.Speech )) ) // vendor browse, *browse
			{
				if ( House != null && House.IsBanned( from ) && !IsOwner( from ) )
				{
					SayTo( from, 1062674 ); // You can't shop from this home as you have been banned from this establishment.
				}
				else
				{
					if ( WasNamed( e.Speech ) )
						OpenBackpack( from );
					else
					{
						IPooledEnumerable mobiles = e.Mobile.GetMobilesInRange( 2 );
						
						foreach ( Mobile m in mobiles )
							if ( m is PlayerVendor && m.CanSee( e.Mobile ) && m.InLOS( e.Mobile ) )
								((PlayerVendor)m).OpenBackpack( from );
						
						mobiles.Free();
					}
					
					e.Handled = true;
				}
			}
			else if ( e.HasKeyword( 0x3E ) || (e.HasKeyword( 0x173 ) && WasNamed( e.Speech )) ) // vendor collect, *collect
			{
				if ( IsOwner( from ) )
				{
					CollectGold( from );

					e.Handled = true;
				}
			}
			else if ( e.HasKeyword( 0x3F ) || (e.HasKeyword( 0x174 ) && WasNamed( e.Speech )) ) // vendor status, *status
			{
				if ( IsOwner( from ) )
				{
					SendOwnerGump( from );

					e.Handled = true;
				}
				else
				{
					SayTo( from, 503226 ); // What do you care? You don't run this shop.	
				}
			}
			else if ( e.HasKeyword( 0x40 ) || (e.HasKeyword( 0x175 ) && WasNamed( e.Speech )) ) // vendor dismiss, *dismiss
			{
				if ( IsOwner( from ) )
				{
					Dismiss( from );

					e.Handled = true;
				}
			}
			else if ( e.HasKeyword( 0x41 ) || (e.HasKeyword( 0x176 ) && WasNamed( e.Speech )) ) // vendor cycle, *cycle
			{
				if ( IsOwner( from ) )
				{
					this.Direction = this.GetDirectionTo( from );

					e.Handled = true;
				}
			}

			// BaseConvo-like speech
			string title = from.Female ? "milady" : "milord";
			if (!e.Handled && EnableSimulatedSales(this) && (WasNamed(e.Speech) || Insensitive.StartsWith(e.Speech, "vendor ")) && Insensitive.Contains(e.Speech, "report") || Insensitive.Contains(e.Speech, "recent") || Insensitive.Contains(e.Speech, "sales"))
			{
				if (IsOwner( from ))
				{
					if (m_RecentSales.Count > 0)
					{
						int reported = 0;
						int numToReport = 5;
						
						String requestNumber = Regex.Match(e.Speech, @"\d+").Value;
						int requestNumberInt = 0;
						int.TryParse(requestNumber, out requestNumberInt);
						if (requestNumberInt > 0) numToReport = requestNumberInt;

						int startPos = m_RecentSales.Count - numToReport;
						if (startPos < 0)
						    startPos = 0;

						for (int i = startPos; i < startPos + numToReport && i < m_RecentSales.Count; i++)
						{
						    SayTo( from, m_RecentSales[i] );
						}
					}
					else
					{
						SayTo( from, "There were no recent sales, " + title + ".");
					}
				}
				else
				{
					SayTo( from, "Sorry, " + title + ", I only respond to the shop owner." );
				}
				e.Handled = true;
			}
			if (!e.Handled && EnableSimulatedSales(this) && EnableSpeech && IsOwner( from ) && (WasNamed( e.Speech ) || Insensitive.StartsWith(e.Speech, "vendor ")))
			{
				if (Insensitive.Contains(e.Speech, "forget") || Insensitive.Contains(e.Speech, "wipe") || Insensitive.Contains(e.Speech, "clear") || Insensitive.Contains(e.Speech, "new"))
				{
				    SayTo( from, "Very well, " + title + ", I will start a new list of sales." );
				    m_RecentSales.Clear();
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "job") || Insensitive.Contains(e.Speech, "work") || Insensitive.Contains(e.Speech, "profession") || Insensitive.Contains(e.Speech, "occupation") || Insensitive.Contains(e.Speech, "explain"))
				{
					int r = Utility.RandomMinMax(1, 3);
					switch(r)
					{
					    case 1:
						SayTo( from, "I will do my best to sell thy goods for the price that you specify. Thou canst also provide an optional item description." );
						break;
					    case 2:
						SayTo( from, "I sell thy goods to the passing adventurers, " + title + ", at the minimum price that thou specify." );

						break;
					    case 3:
						SayTo( from, "I am thy personal merchant, in charge of advertising and selling goods to passing adventurers for the price that thou specify." );
						break;
					    default:
						break;
					}
					e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "sell") || Insensitive.Contains(e.Speech, "item"))
				{
				    SayTo( from, "To put the item up for sale, put it in my inventory and specify the price. You can also specify an optional description." );
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "price"))
				{
				    SayTo( from, "Make sure the price for the item is reasonable, " + title + ". If the item doesn't sell, thou should consider reducing the price." );
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "description"))
				{
				    SayTo( from, "Thou can specify an item description after a space when you specify the price. I will use it in my advertisements." );
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "free"))
				{
				    SayTo( from, "Thou can give an item away for free to the first interested adventurer if you specify the price of zero." );
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "bag") || Insensitive.Contains(e.Speech, "container") || Insensitive.Contains(e.Speech, "chest"))
				{
					int r = Utility.RandomMinMax(1, 2);
					switch(r)
					{
					    case 1:
						SayTo( from, "Items inside bags and other containers will be sold too, if you specify the price of -1, just don't put bags inside bags, adventurers have problems finding those." );
						break;
					    case 2:
						SayTo( from, "Thou can also try to sell the bag and its contents as a package deal if you specify the price for the container itself." );
						break;
					    default:
						break;
					}
					e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "payment") || Insensitive.Contains(e.Speech, "charge"))
				{
				    SayTo( from, "I charge thee a small sum of gold for my services every day. If I don't receive my payment on time, I will leave your premises.");
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "rob") || Insensitive.Contains(e.Speech, "steal") || Insensitive.Contains(e.Speech, "thief"))
				{
					int r = Utility.RandomMinMax(1, 2);
					switch(r)
					{
					    case 1:
						SayTo( from, "Thy goods are safe with me, " + title + ". No one can steal them.");
						break;
					    case 2:
						SayTo( from, "A thief could kill me and I still couldn't give any of thy goods away." );
						break;
					    default:
						break;
					}
					e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "coin") || Insensitive.Contains(e.Speech, "money") || Insensitive.Contains(e.Speech, "currency"))
				{
				    SayTo( from, "Thou can give me some gold to cover the daily service payments." );
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "copper") || Insensitive.Contains(e.Speech, "jewel"))
				{
				    SayTo( from, "Sorry, I only accept gold for my services." );
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "name"))
				{
				    SayTo( from, "Why, I'm " + this.Name + ", " + title + ".");
				    e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "greetings") || Insensitive.Contains(e.Speech, "hail") || Insensitive.Contains(e.Speech, "hello"))
				{
					int r = Utility.RandomMinMax(1, 2);
					switch(r)
					{
					    case 1:
						SayTo( from, "Greetings, " + title + "." );
						break;
					    case 2:
						SayTo( from, "Hail, " + title + "." );
						break;
					    default:
						break;
					}
					e.Handled = true;
				}
				else if (Insensitive.Contains(e.Speech, "goodbye") || Insensitive.Contains(e.Speech, "farewell") || Insensitive.Contains(e.Speech, "bye"))
				{
					int r = Utility.RandomMinMax(1, 2);
					switch(r)
					{
					    case 1:
						SayTo( from, "Farewell, " + title + "." );
						break;
					    case 2:
						SayTo( from, "Safe travels, " + title + "." );
						break;
					    default:
						break;
					}
					e.Handled = true;
				}
			}
		}

		private static string GetItemName( Item item )
		{
		    string Label = item.Name;
		    TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
		    if ( Label != null && Label != "" ){} else { Label = MorphingItem.AddSpacesToSentence( (item.GetType()).Name ); }
		    if ( Server.Misc.MaterialInfo.GetMaterialName( item ) != "" ){ Label = Server.Misc.MaterialInfo.GetMaterialName( item ) + " " + item.Name; }
		    Label = cultInfo.ToTitleCase(Label);

		    return Label;
		}

		private static TimeSpan GetRandomBuyInterval()
		{
		    int nextPurchase = Utility.RandomMinMax(MinCheckPeriodInMinutes, MaxCheckPeriodInMinutes);

		    return TimeSpan.FromMinutes((double)nextPurchase);
		}

		private class PayTimer : Timer
		{
			public static TimeSpan GetInterval(Mobile vendor)
			{
				if ( !ChargeForServicePerUODay((Mobile)vendor))
					return TimeSpan.FromDays( 1.0 );
				else
					return TimeSpan.FromMinutes( Clock.MinutesPerUODay );
			}

			private PlayerVendor m_Vendor;

			public PayTimer( PlayerVendor vendor, TimeSpan delay ) : base( delay, GetInterval(vendor) )
			{
				m_Vendor = vendor;

				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Vendor.m_NextPayTime = DateTime.UtcNow + this.Interval;

				int pay;
				int totalGold;
				if ( !ChargeForServicePerUODay((Mobile)m_Vendor) )
				{
					pay = m_Vendor.ChargePerRealWorldDay;
					totalGold = m_Vendor.BankAccount + m_Vendor.HoldGold;
					//totalGold = m_Vendor.HoldGold;
				}
				else
				{
					pay = m_Vendor.ChargePerDay;
					totalGold = m_Vendor.BankAccount + m_Vendor.HoldGold;
				}

				if (AdventuresFunctions.IsPuritain((object)this))
					pay /= 5;

				if ( pay > totalGold )
				{
					m_Vendor.Destroy( !BaseHouse.NewVendorSystem );
				}
				else
				{
					if ( !BaseHouse.NewVendorSystem )
					{
						if ( m_Vendor.BankAccount >= pay )
						{
							m_Vendor.BankAccount -= pay;
							pay = 0;
						}
						else
						{
							pay -= m_Vendor.BankAccount;
							m_Vendor.BankAccount = 0;
						}
					}

					m_Vendor.HoldGold -= pay;
				}
			}
		}

		private class RandomAdTimer : Timer
		{
		    private PlayerVendor m_Vendor;

		    public RandomAdTimer( PlayerVendor vendor, TimeSpan delay ) : base( delay, GetInterval((Mobile)vendor) )
		    {
			m_Vendor = vendor;
			Priority = TimerPriority.FiveSeconds;
		    }

		    public static TimeSpan GetInterval(Mobile vendor)
		    {
			return TimeSpan.FromSeconds(AdPeriodInSeconds);
		    }

		    protected override void OnTick()
		    {
			if (m_Vendor == null || m_Vendor.Backpack == null || !EnableRandomAds((Mobile)m_Vendor) || !Utility.RandomBool())
			    return;

			List<String> list = new List<String>();

			foreach ( Item item in m_Vendor.Backpack.Items )
			{
			    VendorItem vi = m_Vendor.GetVendorItem( item );
			    bool banned = item is BankCheck || item is Gold || item is DDCopper || item is DDSilver || item is DDJewels || item is DDXormite || item is DDGemstones || item is DDGoldNuggets;

			    if ( vi != null && (vi.IsForSale || vi.IsForFree) && vi.Price != 999 && !banned && vi.Description != "notnpc")
			    {
				string Desc = vi.Description;
				string Label = GetItemName(item);

				if (Desc != String.Empty || Label != String.Empty)
				    list.Add(Desc != String.Empty ? Desc : Label);
			    }

			    if ( item is Container && vi != null && vi.Price < 0 ) // containers not for sale
			    {
				foreach( Item ins in item.Items )
				{
				    if (!(ins is Container))
				    {
					VendorItem vii = m_Vendor.GetVendorItem( ins );
					bool insBanned = ins is BankCheck || ins is Gold || ins is DDCopper || ins is DDSilver || ins is DDJewels || ins is DDXormite || ins is DDGemstones || ins is DDGoldNuggets;
					if ( vii != null && (vii.IsForSale || vii.IsForFree) && vii.Price != 999 && !insBanned && vi.Description != "notnpc")
					{
					    string Desc = vii.Description;
					    string Label = GetItemName(ins);

					    if (Desc != String.Empty || Label != String.Empty)
						list.Add(Desc != String.Empty ? Desc : Label);
					}
				    }
				}
			    }
			}

			if (list.Count == 0)
			    return;

			String AdTarget = list[Utility.Random(list.Count)];

			// advertise stuff

			if (AdTarget.StartsWith("@") && AdTarget.Length > 1)
			{
			    // custom advertisement
			    AdTarget = AdTarget.Substring(1);
			    m_Vendor.Say(AdTarget);
			    return;
			}

			switch(Utility.RandomMinMax( 0, 4 ))
			{
			    case 0:
				m_Vendor.Say("Hurry, hurry! "+AdTarget+" for sale!");
				break;
			    case 1:
				m_Vendor.Say("Step right up, we have "+AdTarget+"!");
				break;
			    case 2:
				m_Vendor.Say(AdTarget+" for sale");
				break;
			    case 3:
			    default:
				m_Vendor.Say(AdTarget+" for a great price!");
				break;
			}
		    }
		}

		private class RandomBuyTimer : Timer
		{
		    private PlayerVendor m_Vendor;

		    public RandomBuyTimer( PlayerVendor vendor, TimeSpan delay ) : base( delay, GetInterval() )
		    {
			m_Vendor = vendor;
			Priority = TimerPriority.OneMinute;
		    }

		    public static TimeSpan GetInterval()
		    {
			return GetRandomBuyInterval();
		    }

		    private void SetupSBList( ref List<SBInfo> sbList )
		    {
			sbList.Clear();
			sbList.Add(new SBBuyArtifacts());
			for (int i = 0; i < Utility.RandomMinMax( 1, SBListMaxRandom ) + SBListMaxFixed; i++)
			{
			    int sbListID = Utility.Random( 127 );
			    switch(sbListID)
			    {
				case 0: { sbList.Add(new SBElfRares()); break; }
				case 1: { sbList.Add(new SBChainmailArmor()); break; }
				case 2: { sbList.Add(new SBHelmetArmor()); break; }
				case 3: { sbList.Add(new SBLeatherArmor()); break; }
				case 4: { sbList.Add(new SBMetalShields()); break; }
				case 5: { sbList.Add(new SBPlateArmor()); break; }
				case 6: { sbList.Add(new SBLotsOfArrows()); break; }
				case 7: { sbList.Add(new SBRingmailArmor()); break; }
				case 8: { sbList.Add(new SBStuddedArmor()); break; }
				case 9: { sbList.Add(new SBWoodenShields()); break; }
				case 10: { sbList.Add(new SBSEArmor()); break; }
				case 11: { sbList.Add(new SBSEBowyer()); break; }
				case 12: { sbList.Add(new SBSECarpenter()); break; }
				case 13: { sbList.Add(new SBSEFood()); break; }
				case 14: { sbList.Add(new SBSELeatherArmor()); break; }
				case 15: { sbList.Add(new SBSEWeapons()); break; }
				case 16: { sbList.Add(new SBAxeWeapon()); break; }
				case 17: { sbList.Add(new SBKnifeWeapon()); break; }
				case 18: { sbList.Add(new SBMaceWeapon()); break; }
				case 19: { sbList.Add(new SBPoleArmWeapon()); break; }
				case 20: { sbList.Add(new SBRangedWeapon()); break; }
				case 21: { sbList.Add(new SBSpearForkWeapon()); break; }
				case 22: { sbList.Add(new SBStavesWeapon()); break; }
				case 23: { sbList.Add(new SBSwordWeapon()); break; }
				case 24: { sbList.Add(new SBElfWizard()); break; }
				case 25: { sbList.Add(new SBElfHealer()); break; }
				case 26: { sbList.Add(new SBUndertaker()); break; }
				case 27: { sbList.Add(new SBAlchemist()); break; }
				case 28: { sbList.Add(new SBMixologist()); break; }
				case 29: { sbList.Add(new SBAnimalTrainer()); break; }
				case 30: { sbList.Add(new SBHumanAnimalTrainer()); break; }
				case 31: { sbList.Add(new SBGargoyleAnimalTrainer()); break; }
				case 32: { sbList.Add(new SBElfAnimalTrainer()); break; }
				case 33: { sbList.Add(new SBBarbarianAnimalTrainer()); break; }
				case 34: { sbList.Add(new SBOrkAnimalTrainer()); break; }
				case 35: { sbList.Add(new SBArchitect()); break; }
				case 36: { sbList.Add(new SBSailor()); break; }
				case 37: { sbList.Add(new SBKungFu()); break; }
				case 38: { sbList.Add(new SBBaker()); break; }
				case 39: { sbList.Add(new SBBanker()); break; }
				case 40: { sbList.Add(new SBBard()); break; }
				case 41: { sbList.Add(new SBBarkeeper()); break; }
				case 42: { sbList.Add(new SBBeekeeper()); break; }
				case 43: { sbList.Add(new SBBlacksmith()); break; }
				case 44: { sbList.Add(new SBBowyer()); break; }
				case 45: { sbList.Add(new SBButcher()); break; }
				case 46: { sbList.Add(new SBCarpenter()); break; }
				case 47: { sbList.Add(new SBCobbler()); break; }
				case 48: { sbList.Add(new SBCook()); break; }
				case 49: { sbList.Add(new SBFarmer()); break; }
				case 50: { sbList.Add(new SBFisherman()); break; }
				case 51: { sbList.Add(new SBFortuneTeller()); break; }
				case 52: { sbList.Add(new SBFurtrader()); break; }
				case 53: { sbList.Add(new SBGlassblower()); break; }
				case 54: { sbList.Add(new SBHairStylist()); break; }
				case 55: { sbList.Add(new SBHealer()); break; }
				case 56: { sbList.Add(new SBDruid()); break; }
				case 57: { sbList.Add(new SBDruidTree()); break; }
				case 58: { sbList.Add(new SBHerbalist()); break; }
				case 59: { sbList.Add(new SBHolyMage()); break; }
				case 60: { sbList.Add(new SBRuneCasting()); break; }
				case 61: { sbList.Add(new SBEnchanter()); break; }
				case 62: { sbList.Add(new SBHouseDeed()); break; }
				case 63: { sbList.Add(new SBInnKeeper()); break; }
				case 64: { sbList.Add(new SBJewel()); break; }
				case 65: { sbList.Add(new SBKeeperOfChivalry()); break; }
				case 66: { sbList.Add(new SBLeatherWorker()); break; }
				case 67: { sbList.Add(new SBMapmaker()); break; }
				case 68: { sbList.Add(new SBMiller()); break; }
				case 69: { sbList.Add(new SBMiner()); break; }
				case 70: { sbList.Add(new SBMonk()); break; }
				case 71: { sbList.Add(new SBPlayerBarkeeper()); break; }
				case 72: { sbList.Add(new SBProvisioner()); break; }
				case 73: { sbList.Add(new SBRancher()); break; }
				case 74: { sbList.Add(new SBRanger()); break; }
				case 75: { sbList.Add(new SBRealEstateBroker()); break; }
				case 76: { sbList.Add(new SBScribe()); break; }
				case 77: { sbList.Add(new SBSage()); break; }
				case 78: { sbList.Add(new SBSECook()); break; }
				case 79: { sbList.Add(new SBSEHats()); break; }
				case 80: { sbList.Add(new SBShipwright()); break; }
				case 81: { sbList.Add(new SBDevon()); break; }
				case 82: { sbList.Add(new SBSmithTools()); break; }
				case 83: { sbList.Add(new SBStoneCrafter()); break; }
				case 84: { sbList.Add(new SBTailor()); break; }
				case 85: { sbList.Add(new SBJester()); break; }
				case 86: { sbList.Add(new SBTanner()); break; }
				case 87: { sbList.Add(new SBTavernKeeper()); break; }
				case 88: { sbList.Add(new SBThief()); break; }
				case 89: { sbList.Add(new SBTinker()); break; }
				case 90: { sbList.Add(new SBVagabond()); break; }
				case 91: { sbList.Add(new SBVarietyDealer()); break; }
				case 92: { sbList.Add(new SBVeterinarian()); break; }
				case 93: { sbList.Add(new SBWaiter()); break; }
				case 94: { sbList.Add(new SBWeaponSmith()); break; }
				case 95: { sbList.Add(new SBWeaver()); break; }
				case 96: { sbList.Add(new SBNecroMage()); break; }
				case 97: { sbList.Add(new SBNecromancer()); break; }
				case 98: { sbList.Add(new SBWitches()); break; }
				case 99: { sbList.Add(new SBMortician()); break; }
				case 100: { sbList.Add(new SBMage()); break; }
				case 101: { sbList.Add(new SBGodlySewing()); break; }
				case 102: { sbList.Add(new SBGodlySmithing()); break; }
				case 103: { sbList.Add(new SBGodlyBrewing()); break; }
				case 104: { sbList.Add(new SBMazeStore()); break; }
				case 105: { sbList.Add(new SBBuyArtifacts()); break; }
				case 106: { sbList.Add(new SBGemArmor()); break; }
				case 107: { sbList.Add(new SBRoscoe()); break; }
				case 108: { sbList.Add(new SBTinkerGuild()); break; }
				case 109: { sbList.Add(new SBThiefGuild()); break; }
				case 110: { sbList.Add(new SBTailorGuild()); break; }
				case 111: { sbList.Add(new SBMinerGuild()); break; }
				case 112: { sbList.Add(new SBMageGuild()); break; }
				case 113: { sbList.Add(new SBHealerGuild()); break; }
				case 114: { sbList.Add(new SBSailorGuild()); break; }
				case 115: { sbList.Add(new SBBlacksmithGuild()); break; }
				case 116: { sbList.Add(new SBBardGuild()); break; }
				case 117: { sbList.Add(new SBHolidayXmas()); break; }
				case 118: { sbList.Add(new SBHolidayHalloween()); break; }
				case 119: { sbList.Add(new SBNecroGuild()); break; }
				case 120: { sbList.Add(new SBArcherGuild()); break; }
				case 121: { sbList.Add(new SBAlchemistGuild()); break; }
				case 122: { sbList.Add(new SBLibraryGuild()); break; }
				case 123: { sbList.Add(new SBDruidGuild()); break; }
				case 124: { sbList.Add(new SBCarpenterGuild()); break; }
				case 125: { sbList.Add(new SBAssassin()); break; }
				case 126: { sbList.Add(new SBCartographer()); break; }
				case 127: { sbList.Add(new SBBuyArtifacts()); break; }
				default: break;
			    }
			}
		    }

		    // The lists below must correspond to the enum definitions in AOS.cs. The number of elements
		    // must strictly correspond to the number of elements in the AOS enums, or the game will crash.
		    private int[] AosAttributeIntensities = {
			10, // RegenHits
			10, // RegenStam
			10, // RegenMana
			25, // DefendChance
			25, // AttackChance
			25, // BonusStr
			25, // BonusDex
			25, // BonusInt
			25, // BonusHits
			25, // BonusStam
			25, // BonusMana
			50, // WeaponDamage
			50, // WeaponSpeed
			50, // SpellDamage
			3, // CastRecovery
			3, // CastSpeed
			25, // LowerManaCost
			25, // LowerRegCost
			50, // ReflectPhysical
			50, // EnhancePotions,
			150, // Luck
			1, // SpellChanneling
			1 // NightSight
		    };

		    private int[] AosWeaponAttributeIntensities = {
			50, // LowerStatReq
			5, // SelfRepair
			50, // HitLeechHits
			50, // HitLeechStam
			50, // HitLeechMana
			50, // HitLowerAttack
			50, // HitLowerDefend
			50, // HitMagicArrow
			50, // HitHarm
			50, // HitFireball
			50, // HitLightning
			50, // HitDispel
			50, // HitColdArea
			50, // HitFireArea
			50, // HitPoisonArea
			50, // HitEnergyArea
			50, // HitPhysicalArea
			15, // ResistPhysicalBonus
			15, // ResistFireBonus
			15, // ResistColdBonus
			15, // ResistPoisonBonus
			15, // ResistEnergyBonus
			1, // UseBestSkill
			1, // MageWeapon
			100 // DurabilityBonus
		    };

		    private int[] AosArmorAttributeIntensities = {
			50, // LowerStatReq
			5, // SelfRepair
			1, // MageArmor
			100 // DurabilityBonus
		    };

		    private int[] AosElementAttributeIntensities = {
			1100, // Physical - avoid overvaluing it since most stuff is at 100% physical
			200, // Fire
			200, // Cold
			200, // Poison
			200, // Energy
			25, // Chaos
			25, // Direct
		    };

		    private int MaxSkillIntensity = 15; // FIXME: 12?
		    private int MaxResistanceIntensity = 25;
		    private int ResistanceIntensityCountsAsProp = 80; // %

		    private enum IntensityMode
		    {
			AosAttribute,
			AosWeaponAttribute,
			AosArmorAttribute,
			AosElementAttribute,
			SkillBonus,
			ResistanceBonus,
			RunicToolProperties
		    }

		    private void AddSkillBonuses(Item ii, double skill1, double skill2, double skill3, double skill4, double skill5, ref int attrsMod, ref int props)
		    {
			int MaxRealIntensity = ii is BaseMagicStaff ? 50 : MaxSkillIntensity;

			int NormalizedSkillBonus1 = (int)skill1 * 100 / MaxRealIntensity;
			int NormalizedSkillBonus2 = (int)skill2 * 100 / MaxRealIntensity;
			int NormalizedSkillBonus3 = (int)skill3 * 100 / MaxRealIntensity;
			int NormalizedSkillBonus4 = (int)skill4 * 100 / MaxRealIntensity;
			int NormalizedSkillBonus5 = (int)skill5 * 100 / MaxRealIntensity;

			if(NormalizedSkillBonus1 > 0 && NormalizedSkillBonus1 >= AttrsIntensityThreshold) ++props;
			if(NormalizedSkillBonus2 > 0 && NormalizedSkillBonus2 >= AttrsIntensityThreshold) ++props;
			if(NormalizedSkillBonus3 > 0 && NormalizedSkillBonus3 >= AttrsIntensityThreshold) ++props;
			if(NormalizedSkillBonus4 > 0 && NormalizedSkillBonus4 >= AttrsIntensityThreshold) ++props;
			if(NormalizedSkillBonus5 > 0 && NormalizedSkillBonus5 >= AttrsIntensityThreshold) ++props;

			attrsMod += (int)skill1 * (NormalizedSkillBonus1 / 2);
			attrsMod += (int)skill2 * (NormalizedSkillBonus2 / 2);
			attrsMod += (int)skill3 * (NormalizedSkillBonus3 / 2);
			attrsMod += (int)skill4 * (NormalizedSkillBonus4 / 2);
			attrsMod += (int)skill5 * (NormalizedSkillBonus5 / 2);
		    }

		    private void AddResistanceBonuses(int physical, int fire, int cold, int poison, int energy, ref int attrsMod, ref int props)
		    {
			int NormalizedPhysicalResistance = physical * 100 / MaxResistanceIntensity;
			int NormalizedFireResistance = fire * 100 / MaxResistanceIntensity;
			int NormalizedColdResistance = cold * 100 / MaxResistanceIntensity;
			int NormalizedPoisonResistance = poison * 100 / MaxResistanceIntensity;
			int NormalizedEnergyResistance = energy * 100 / MaxResistanceIntensity;

			if (NormalizedPhysicalResistance >= ResistanceIntensityCountsAsProp) ++props;
			if (NormalizedFireResistance >= ResistanceIntensityCountsAsProp) ++props;
			if (NormalizedColdResistance >= ResistanceIntensityCountsAsProp) ++props;
			if (NormalizedPoisonResistance >= ResistanceIntensityCountsAsProp) ++props;
			if (NormalizedEnergyResistance >= ResistanceIntensityCountsAsProp) ++props;

			attrsMod += physical * (NormalizedPhysicalResistance / 10);
			attrsMod += fire * (NormalizedFireResistance / 10);
			attrsMod += cold * (NormalizedColdResistance / 10);
			attrsMod += poison * (NormalizedPoisonResistance / 10);
			attrsMod += energy * (NormalizedEnergyResistance / 10);
		    }

		    private void ScalePriceOnDurability(Item item, ref int price)
		    {
			int cur_dur = 0;
			int max_dur = 0;

			if (item is BaseWeapon)
			{
			    cur_dur = ((BaseWeapon)item).HitPoints;
			    max_dur = ((BaseWeapon)item).MaxHitPoints;
			}
			else if (item is BaseArmor)
			{
			    cur_dur = ((BaseArmor)item).HitPoints;
			    max_dur = ((BaseArmor)item).MaxHitPoints;
			}
			else if (item is BaseClothing)
			{
			    cur_dur = ((BaseClothing)item).HitPoints;
			    max_dur = ((BaseClothing)item).MaxHitPoints;
			}
			else if (item is BaseShield)
			{
			    cur_dur = ((BaseShield)item).HitPoints;
			    max_dur = ((BaseShield)item).MaxHitPoints;
			}
			else if (item is BaseJewel)
			{
			    cur_dur = ((BaseJewel)item).HitPoints;
			    max_dur = ((BaseJewel)item).MaxHitPoints;
			}

			if (cur_dur > 0 && max_dur > 0)
			{
			    if (max_dur <= 3)
				price = price * PriceCutOnMaxDurability3 / 100;
			    if (max_dur <= 5)
				price = price * PriceCutOnMaxDurability5 / 100;
			    if (max_dur <= 10)
				price = price * PriceCutOnMaxDurability10 / 100;
			    if (max_dur <= 15)
				price = price * PriceCutOnMaxDurability15 / 100;
			    if (max_dur <= 20)
				price = price * PriceCutOnMaxDurability20 / 100;
			    else if (max_dur <= 25)
				price = price * PriceCutOnMaxDurability25 / 100;
			}
		    }

		    // BaseWeapon
		    private void AddNormalizedBonuses(BaseWeapon bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.AosWeaponAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosWeaponAttribute ) ) ) 
			    {
				int MaxWeaponIntensity = AosWeaponAttributeIntensities[id++];
				int NormalizedWeaponAttribute = bw.WeaponAttributes[ (AosWeaponAttribute)i ] * 100 / MaxWeaponIntensity;
				if ( NormalizedWeaponAttribute > 0 && NormalizedWeaponAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxWeaponIntensity > 1 )
				    attrsMod += (int)(NormalizedWeaponAttribute * ( (double)NormalizedWeaponAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedWeaponAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			}
			else if (mode == IntensityMode.AosElementAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosElementAttribute ) ) ) 
			    {
				int MaxElemIntensity = AosElementAttributeIntensities[id++];
				int NormalizedElementalAttribute = bw.AosElementDamages[ (AosElementAttribute)i ] * 100 / MaxElemIntensity;
				if ( NormalizedElementalAttribute > 0 && NormalizedElementalAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxElemIntensity > 1 )
				    attrsMod += (int)(NormalizedElementalAttribute * ( (double)NormalizedElementalAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedElementalAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			}
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for weapon: " + mode);
			}
		    }

		    // BaseArmor
		    private void AddNormalizedBonuses(BaseArmor bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.AosArmorAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) ) 
			    {
				int MaxArmorIntensity = AosArmorAttributeIntensities[id++];
				int NormalizedArmorAttribute = bw.ArmorAttributes[ (AosArmorAttribute)i ] * 100 / MaxArmorIntensity;
				if ( NormalizedArmorAttribute > 0 && NormalizedArmorAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxArmorIntensity > 1 )
				    attrsMod += (int)(NormalizedArmorAttribute * ( (double)NormalizedArmorAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedArmorAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			}
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else if (mode == IntensityMode.ResistanceBonus)
			{
			    AddResistanceBonuses(bw.PhysicalBonus, bw.FireBonus, bw.ColdBonus, bw.PoisonBonus, bw.EnergyBonus,
				    ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for armor: " + mode);
			}
		    }

		    // BaseShield
		    private void AddNormalizedBonuses(BaseShield bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.AosArmorAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) ) 
			    {
				int MaxArmorIntensity = AosArmorAttributeIntensities[id++];
				int NormalizedArmorAttribute = bw.ArmorAttributes[ (AosArmorAttribute)i ] * 100 / MaxArmorIntensity;
				if ( NormalizedArmorAttribute > 0 && NormalizedArmorAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxArmorIntensity > 1 )
				    attrsMod += (int)(NormalizedArmorAttribute * ( (double)NormalizedArmorAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedArmorAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			}
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else if (mode == IntensityMode.ResistanceBonus)
			{
			    AddResistanceBonuses(bw.PhysicalBonus, bw.FireBonus, bw.ColdBonus, bw.PoisonBonus, bw.EnergyBonus,
				    ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for shield: " + mode);
			}
		    }

		    // BaseClothing
		    private void AddNormalizedBonuses(BaseClothing bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.AosArmorAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosArmorAttribute ) ) ) 
			    {
				int MaxArmorIntensity = AosArmorAttributeIntensities[id++];
				int NormalizedArmorAttribute = bw.ClothingAttributes[ (AosArmorAttribute)i ] * 100 / MaxArmorIntensity;
				if ( NormalizedArmorAttribute > 0 && NormalizedArmorAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxArmorIntensity > 1 )
				    attrsMod += (int)(NormalizedArmorAttribute * ( (double)NormalizedArmorAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedArmorAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			}
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for clothing: " + mode);
			}
		    }

		    // BaseJewel
		    private void AddNormalizedBonuses(BaseJewel bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.AosElementAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosElementAttribute ) ) ) 
			    {
				int MaxElemIntensity = AosElementAttributeIntensities[id++];
				int NormalizedElementalAttribute = bw.Resistances[ (AosElementAttribute)i ] * 100 / MaxElemIntensity;
				if ( NormalizedElementalAttribute > 0 && NormalizedElementalAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxElemIntensity > 1 )
				    attrsMod += (int)(NormalizedElementalAttribute * ( (double)NormalizedElementalAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedElementalAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			}
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else if (mode == IntensityMode.ResistanceBonus)
			{
			    AddResistanceBonuses(bw.PhysicalResistance, bw.FireResistance, bw.ColdResistance, bw.PoisonResistance, bw.EnergyResistance,
				    ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for jewel: " + mode);
			}
		    }

		    // BaseQuiver
		    private void AddNormalizedBonuses(BaseQuiver bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else
			{
			    Console.WriteLine("Unexpected mode for quiver: " + mode);
			}
		    }

		    // BaseInstrument
		    private void AddNormalizedBonuses(BaseInstrument bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else if (mode == IntensityMode.ResistanceBonus)
			{
			    AddResistanceBonuses(bw.PhysicalResistance, bw.FireResistance, bw.ColdResistance, bw.PoisonResistance, bw.EnergyResistance,
				    ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for instrument: " + mode);
			}
		    }

		    // Spellbook
		    private void AddNormalizedBonuses(Spellbook bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			int id = 0;

			if (mode == IntensityMode.AosAttribute)
			{
			    foreach( int i in Enum.GetValues(typeof( AosAttribute ) ) )
			    {
				int MaxIntensity = AosAttributeIntensities[id++];
				int NormalizedAttribute = bw.Attributes[ (AosAttribute)i ] * 100 / MaxIntensity;
				if ( NormalizedAttribute > 0 && NormalizedAttribute >= AttrsIntensityThreshold ) ++props;

				if ( MaxIntensity > 1 )
				    attrsMod += (int)(NormalizedAttribute * ( (double)NormalizedAttribute / IntensityPercentile * IntensityMultiplier ));
				else if ( NormalizedAttribute > 0 )
				    attrsMod += Utility.RandomMinMax(50, 100);
			    }
			} 
			else if (mode == IntensityMode.SkillBonus)
			{
			    AddSkillBonuses(bw, bw.SkillBonuses.Skill_1_Value, bw.SkillBonuses.Skill_2_Value, bw.SkillBonuses.Skill_3_Value,
				    bw.SkillBonuses.Skill_4_Value, bw.SkillBonuses.Skill_5_Value, ref attrsMod, ref props);
			}
			else if (mode == IntensityMode.ResistanceBonus)
			{
			    AddResistanceBonuses(bw.PhysicalResistance, bw.FireResistance, bw.ColdResistance, bw.PoisonResistance, bw.EnergyResistance,
				    ref attrsMod, ref props);
			}
			else
			{
			    Console.WriteLine("Unexpected mode for spellbook: " + mode);
			}
		    }

		    // BaseRunicTool
		    private void AddNormalizedBonuses(BaseRunicTool bw, IntensityMode mode, ref int attrsMod, ref int props)
		    {
			if (mode == IntensityMode.RunicToolProperties)
			{
			    attrsMod += 1000;

			    switch ( bw.Resource )
			    {
				case CraftResource.DullCopper: attrsMod = (int)( attrsMod * 1.25 ); break;
				case CraftResource.ShadowIron: attrsMod = (int)( attrsMod * 1.5 ); break;
				case CraftResource.Copper: attrsMod = (int)( attrsMod * 1.75 ); break;
				case CraftResource.Bronze: attrsMod = (int)( attrsMod * 2 ); break;
				case CraftResource.Gold: attrsMod = (int)( attrsMod * 2.25 ); break;
				case CraftResource.Agapite: attrsMod = (int)( attrsMod * 2.50 ); break;
				case CraftResource.Verite: attrsMod = (int)( attrsMod * 2.75 ); break;
				case CraftResource.Valorite: attrsMod = (int)( attrsMod * 3 ); break;
				case CraftResource.Nepturite: attrsMod = (int)( attrsMod * 3.10 ); break;
				case CraftResource.Obsidian: attrsMod = (int)( attrsMod * 3.10 ); break;
				case CraftResource.Steel: attrsMod = (int)( attrsMod * 3.25 ); break;
				case CraftResource.Brass: attrsMod = (int)( attrsMod * 3.5 ); break;
				case CraftResource.Mithril: attrsMod = (int)( attrsMod * 3.75 ); break;
				case CraftResource.Xormite: attrsMod = (int)( attrsMod * 3.75 ); break;
				case CraftResource.Dwarven: attrsMod = (int)( attrsMod * 7.50 ); break;
				case CraftResource.SpinedLeather: attrsMod = (int)( attrsMod * 1.5 ); break;
				case CraftResource.HornedLeather: attrsMod = (int)( attrsMod * 1.75 ); break;
				case CraftResource.BarbedLeather: attrsMod = (int)( attrsMod * 2.0 ); break;
				case CraftResource.NecroticLeather: attrsMod = (int)( attrsMod * 2.25 ); break;
				case CraftResource.VolcanicLeather: attrsMod = (int)( attrsMod * 2.5 ); break;
				case CraftResource.FrozenLeather: attrsMod = (int)( attrsMod * 2.75 ); break;
				case CraftResource.GoliathLeather: attrsMod = (int)( attrsMod * 3.0 ); break;
				case CraftResource.DraconicLeather: attrsMod = (int)( attrsMod * 3.25 ); break;
				case CraftResource.HellishLeather: attrsMod = (int)( attrsMod * 3.5 ); break;
				case CraftResource.DinosaurLeather: attrsMod = (int)( attrsMod * 3.75 ); break;
				case CraftResource.AlienLeather: attrsMod = (int)( attrsMod * 3.75 ); break;
				case CraftResource.RedScales: attrsMod = (int)( attrsMod * 1.25 ); break;
				case CraftResource.YellowScales: attrsMod = (int)( attrsMod * 1.25 ); break;
				case CraftResource.BlackScales: attrsMod = (int)( attrsMod * 1.5 ); break;
				case CraftResource.GreenScales: attrsMod = (int)( attrsMod * 1.5 ); break;
				case CraftResource.WhiteScales: attrsMod = (int)( attrsMod * 1.5 ); break;
				case CraftResource.BlueScales: attrsMod = (int)( attrsMod * 1.5 ); break;
				case CraftResource.AshTree: attrsMod = (int)( attrsMod * 1.25 ); break;
				case CraftResource.CherryTree: attrsMod = (int)( attrsMod * 1.45 ); break;
				case CraftResource.EbonyTree: attrsMod = (int)( attrsMod * 1.65 ); break;
				case CraftResource.GoldenOakTree: attrsMod = (int)( attrsMod * 1.85 ); break;
				case CraftResource.HickoryTree: attrsMod = (int)( attrsMod * 2.05 ); break;
				case CraftResource.MahoganyTree: attrsMod = (int)( attrsMod * 2.25 ); break;
				case CraftResource.DriftwoodTree: attrsMod = (int)( attrsMod * 2.25 ); break;
				case CraftResource.OakTree: attrsMod = (int)( attrsMod * 2.45 ); break;
				case CraftResource.PineTree: attrsMod = (int)( attrsMod * 2.65 ); break;
				case CraftResource.GhostTree: attrsMod = (int)( attrsMod * 2.65 ); break;
				case CraftResource.RosewoodTree: attrsMod = (int)( attrsMod * 2.85 ); break;
				case CraftResource.WalnutTree: attrsMod = (int)( attrsMod * 3 ); break;
				case CraftResource.ElvenTree: attrsMod = (int)( attrsMod * 6 ); break;
				case CraftResource.PetrifiedTree: attrsMod = (int)( attrsMod * 3.25 ); break;
			    }

			    attrsMod -= (50 - bw.UsesRemaining) * 30;
			    if (attrsMod < 0)
				attrsMod = 0;
			}
			else
			{
			    Console.WriteLine("Unexpected mode for runic tool: " + mode);
			}
		    }

		    private int GetAttrsMod( Item ii )
		    {
			if (ii == null) { return 0; }

			int attrsMod = 0;
			int props = 0;

			if (ii is BaseWeapon)
			{
			    BaseWeapon bw = ii as BaseWeapon;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.AosWeaponAttribute, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.AosElementAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);

			    if(bw.Slayer != SlayerName.None) ++props;
			    if(bw.Slayer2 != SlayerName.None) ++props;

			    if (bw.Slayer != SlayerName.None) 
			    {
				attrsMod += 100;
				props++;
			    }
			    if (bw.Slayer2 != SlayerName.None) 
			    {
				attrsMod += 100;
				props++;
			    }

			    if (props >= 3 && (bw.WeaponAttributes.MageWeapon > 0 || bw.Attributes.SpellChanneling > 0))
				attrsMod = (int)((double)attrsMod * 1.3);
			}
			else if (ii is BaseArmor)
			{
			    BaseArmor bw = ii as BaseArmor;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.AosArmorAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);

			    if (props >= 3 && bw.ArmorAttributes.MageArmor > 0 || bw.Attributes.SpellChanneling > 0)
				attrsMod = (int)((double)attrsMod * 1.3);

			}
			else if (ii is BaseClothing)
			{
			    BaseClothing bw = ii as BaseClothing;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.AosArmorAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);

			    if (props >= 3 && bw.ClothingAttributes.MageArmor > 0 || bw.Attributes.SpellChanneling > 0)
				attrsMod = (int)((double)attrsMod * 1.3);
			}
			else if (ii is BaseJewel)
			{
			    BaseJewel bw = ii as BaseJewel;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.AosElementAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);
			}
			else if (ii is BaseShield)
			{
			    BaseShield bw = ii as BaseShield;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.AosArmorAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);

			    if (props >= 3 && bw.ArmorAttributes.MageArmor > 0 || bw.Attributes.SpellChanneling > 0)
				attrsMod = (int)((double)attrsMod * 1.3);
			}
			else if (ii is BaseQuiver)
			{
			    BaseQuiver bw = ii as BaseQuiver;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);
			}
			else if (ii is BaseInstrument)
			{
			    BaseInstrument bw = ii as BaseInstrument;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);
			}
			else if (ii is BaseRunicTool)
			{
			    BaseRunicTool bw = ii as BaseRunicTool;
			    
			    AddNormalizedBonuses(bw, IntensityMode.RunicToolProperties, ref attrsMod, ref props);
			}
			else if (ii is Spellbook)
			{
			    Spellbook bw = ii as Spellbook;

			    AddNormalizedBonuses(bw, IntensityMode.AosAttribute, ref attrsMod, ref props);

			    AddNormalizedBonuses(bw, IntensityMode.SkillBonus, ref attrsMod, ref props);
			    AddNormalizedBonuses(bw, IntensityMode.ResistanceBonus, ref attrsMod, ref props);

			    if(bw.Slayer != SlayerName.None) ++props;
			    if(bw.Slayer2 != SlayerName.None) ++props;

			    if (bw.SpellCount > 0)
			    {
				    attrsMod += bw.SpellCount * 20; // TODO: make the higher circle spells cost more
			    }
			    if (bw.Slayer != SlayerName.None)
			    {
				    attrsMod += 100;
			    }
			    if (bw.Slayer2 != SlayerName.None)
			    {
				    attrsMod += 100;
			    }
			}

			if (IncreasePriceBasedOnNumberOfProps)
			{
			    if (props == 1 || props == 2) { attrsMod *= AttrsMod1Or2Props; }
			    else if (props == 3 || props == 4) { attrsMod *= AttrsMod3Or4Props; }
			    else if (props == 5 || props == 6) { attrsMod *= AttrsMod5Or6Props; }
			    else if (props == 7 || props == 8) { attrsMod *= AttrsMod7Or8Props; }
			    else if (props >= 9) { attrsMod *= AttrsMod9OrMoreProps; }
			}

			return attrsMod;
		    }


		    private int PredictPrice( Item item )
		    {
			int price = 0;
			List<SBInfo> sbList = new List<SBInfo>();
			List<Item> items = new List<Item>();

			SetupSBList(ref sbList);

			items.Add(item);
			if (item is Container)
			{
			    Container c = item as Container;
			    foreach(Item it in c.Items)
			    {
				bool banned = it is BankCheck || it is Gold || it is DDCopper || it is DDSilver || it is DDJewels || it is DDXormite || it is DDGemstones || it is DDGoldNuggets;

				if (!banned)
				    items.Add( it );
			    }
			}

			int barterValue = Utility.Random(BarterValue);
			int attrsMultiplier = Utility.RandomMinMax( MinAttrsMultiplier, MaxAttrsMultiplier );
			bool isRichSucker = Utility.RandomMinMax( 0, 1000 ) > RichSuckerChance;
			float modifier = (float)Utility.RandomMinMax( MinPriceModifier, MaxPriceModifier );
			if (Utility.Random(100) > ImprovedPriceModChance && Utility.RandomBool())
			    modifier *= Utility.RandomMinMax( MinImprovedPriceMod, MaxImprovedPriceMod ); // big luck, improved mod

			foreach(Item ii in items)
			{ 
			    if (HarderBagSale)
			    {
				// reroll for each item in the bag, makes it progressively harder to get a decent price
				// SetupSBList(ref sbList); // enable to make things even harder for bag sales
				barterValue = Utility.Random(BarterValue);
				attrsMultiplier = Utility.RandomMinMax( MinAttrsMultiplier, MaxAttrsMultiplier );
				modifier = (float)Utility.RandomMinMax( MinPriceModifier, MaxPriceModifier );
				if (Utility.Random(100) > ImprovedPriceModChance && Utility.RandomBool())
				    modifier *= Utility.RandomMinMax( MinImprovedPriceMod, MaxImprovedPriceMod );
			    }

			    int itemPrice = 0;
			    foreach(SBInfo priceInfo in sbList)
			    {
				int estimate = priceInfo.SellInfo.GetSellPriceFor(ii, barterValue);
				if (itemPrice < estimate)
				    itemPrice = estimate;
			    }

			    ScalePriceOnDurability(ii, ref price);

			    price += itemPrice;

			    if (price < PriceThresholdForAttributeCheck)
			    {
				int attrsMod = GetAttrsMod(ii);
				attrsMod *= (int)((float)attrsMultiplier / 100);

				ScalePriceOnDurability(ii, ref attrsMod);

				price += attrsMod;
			    }

			    if (price == 1 && LowPriceBoost)
			    {
				price = Utility.RandomMinMax( 1, MinimalPriceMaxBoost );
			    }
			    else if (price > RichSuckerMinPrice && isRichSucker)
			    {
				price *= Utility.RandomMinMax( RichSuckerMinPriceMultiplier, RichSuckerMaxPriceMultiplier ); // rich sucker
			    }

			    price += (int)((float)price / modifier);
			}

			return price;
		    }

		    protected override void OnTick()
		    {
			if (!EnableSimulatedSales((Mobile)m_Vendor) || m_Vendor.Backpack == null)
			    return;

			//Debug(); // uncomment to enable debug runs on every tick

			int iterations = MaxIterationsPerCheck; //Math.Min(MaxIterationsPerCheck, m_Vendor.Backpack.Items.Count);

			for (int i = 0; i < iterations; i++)
			{
			    List<Item> list = new List<Item>();
			    string BuyerName = Utility.RandomBool() ? NameList.RandomName("male") : NameList.RandomName("female");

			    foreach ( Item item in m_Vendor.Backpack.Items )
			    {
				VendorItem vi = m_Vendor.GetVendorItem( item );
				bool banned = item is BankCheck || item is Gold || item is DDCopper || item is DDSilver || item is DDJewels || item is DDXormite || item is DDGemstones || item is DDGoldNuggets;

				if ( vi != null && (vi.IsForSale || vi.IsForFree) && vi.Price != 999 && !banned && vi.Description != "notnpc")
				    list.Add( item );

				if ( item is Container && vi.Price < 0 ) // containers not for sale
				{
				    foreach( Item ins in item.Items )
				    {
					if (!(ins is Container))
					{
					    VendorItem vii = m_Vendor.GetVendorItem( ins );
					    bool insBanned = ins is BankCheck || ins is Gold || ins is DDCopper || ins is DDSilver || ins is DDJewels || ins is DDXormite || ins is DDGemstones || ins is DDGoldNuggets;
					    if ( vii != null && (vii.IsForSale || vii.IsForFree) && vii.Price != 999 && !insBanned && vi.Description != "notnpc")
						list.Add( ins );
					}
				    }
				}
			    }

			    if ( list.Count > 0 )
			    {
				Item randItem = list[Utility.Random(list.Count)];
				VendorItem viToSell = m_Vendor.GetVendorItem( randItem );

				string Desc = viToSell.Description;
				string Label = GetItemName(randItem);
				int TotalPrice = viToSell.Price;

				string Fmt = BuyerName+" ";
				if (TotalPrice != 0)
				    Fmt += "bought "+Label+" ";
				else
				    Fmt += "took "+Label+" ";
				if (randItem.Amount > 1)
				    Fmt += "("+randItem.Amount+"x) ";
				if (Desc != "" && !Desc.StartsWith("@"))
				    Fmt += "["+Desc+"] ";
				if (TotalPrice != 0)
				    Fmt += "for "+TotalPrice+" gold.";
				else
				    Fmt += "for free.";

				// projected price
				int ProjPrice = PredictPrice(randItem) * randItem.Amount;
				if (ProjPrice > 1)
				    ProjPrice = ProjPrice * FinalPriceModifier / 100;

				//m_Vendor.SayTo( m_Vendor.m_Owner, $"Projected price: {ProjPrice} for {Label}.");

				if (TotalPrice <= ProjPrice)
				{
				    if (!AnnounceSalesOnlyIfClose || m_Vendor.m_Owner.GetDistanceToSqrt( m_Vendor ) <= DistToAnnounceSales)
					m_Vendor.SayTo( m_Vendor.m_Owner, Fmt );

				    m_Vendor.HoldGold += TotalPrice;

				    m_Vendor.m_RecentSales.Add(Fmt);
				    if (m_Vendor.m_RecentSales.Count > NumRecentSalesToRemember)
				    {
					m_Vendor.m_RecentSales.RemoveAt(0);
				    }

				    if (randItem is Container)
				    {
					Container ct = randItem as Container;
					for (int d = ct.Items.Count - 1; d >= 0; d-- )
					{
					    ct.Items[d].Delete();
					}
				    }
				    randItem.Delete();
				}
			    }
			}

			// Set the new interval here
			this.Interval = GetRandomBuyInterval();
			// Console.WriteLine(m_Vendor.Name + " - next purchase in " + this.Interval);
		    }

		    private void Debug()
		    {
			const int NUM_ITERATIONS = 100;
			List<Item> list = new List<Item>();

			Console.WriteLine("Predicting prices over " + NUM_ITERATIONS + " iterations...");

			foreach ( Item item in m_Vendor.Backpack.Items )
			{
			    VendorItem vi = m_Vendor.GetVendorItem( item );
			    bool banned = item is BankCheck || item is Gold || item is DDCopper || item is DDSilver || item is DDJewels || item is DDXormite || item is DDGemstones || item is DDGoldNuggets;

			    if ( vi != null && (vi.IsForSale || vi.IsForFree) && vi.Price != 999 && !banned && vi.Description != "notnpc")
				list.Add( item );

			    if ( item is Container && vi.Price < 0 ) // containers not for sale
			    {
				foreach( Item ins in item.Items )
				{
				    if (!(ins is Container))
				    {
					VendorItem vii = m_Vendor.GetVendorItem( ins );
					bool insBanned = ins is BankCheck || ins is Gold || ins is DDCopper || ins is DDSilver || ins is DDJewels || ins is DDXormite || ins is DDGemstones || ins is DDGoldNuggets;
					if ( vii != null && (vii.IsForSale || vii.IsForFree) && vii.Price != 999 && !insBanned && vi.Description != "notnpc")
					    list.Add( ins );
				    }
				}
			    }
			}

			if ( list.Count > 0 )
			{
			    foreach( Item item in list)
			    {
				List<int> Prices = new List<int>();

				VendorItem viToSell = m_Vendor.GetVendorItem( item );
				string Label = GetItemName(item);

				int min = int.MaxValue;
				int max = 1;
				int avg = 0;

				for (int i = 0; i < NUM_ITERATIONS; i++)
				{
				    int ProjPrice = PredictPrice(item) * item.Amount;
				    if (ProjPrice > 1)
						ProjPrice = ProjPrice * FinalPriceModifier / 100 ; // +++
				    if (ProjPrice > 1 && ProjPrice < min)
						min = ProjPrice;
				    if (ProjPrice > max)
						max = ProjPrice;
				    if (ProjPrice > 1)
						Prices.Add(ProjPrice);
				}

				if (min == int.MaxValue) { min = 1; }
				if (Prices.Count > 0)
				    avg = (int)Prices.Average();

				Console.WriteLine("Prices for " + Label + ": min = " + min + ", max = " + max + ", avg = " + avg);
			    }
			}
		    }
		}

		[PlayerVendorTarget]
		private class PVBuyTarget : Target
		{
			public PVBuyTarget() : base( 3, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					TryToBuy( (Item) targeted, from );
				}
			}
		}

		private class VendorPricePrompt : Prompt
		{
			private PlayerVendor m_Vendor;
			private VendorItem m_VI;

			public VendorPricePrompt( PlayerVendor vendor, VendorItem vi )
			{
				m_Vendor = vendor;
				m_VI = vi;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( !m_VI.Valid || !m_Vendor.CanInteractWith( from, true ) )
					return;

				string firstWord;

				int sep = text.IndexOfAny( new char[] { ' ', ',' } );
				if ( sep >= 0 )
					firstWord = text.Substring( 0, sep );
				else
					firstWord = text;

				int price;
				string description;

				if ( int.TryParse( firstWord, out price ) )
				{
					if ( sep >= 0 )
						description = text.Substring( sep + 1 ).Trim();
					else
						description = "";
				}
				else
				{
					price = -1;
					description = text.Trim();
				}

				SetInfo( from, price, Utility.FixHtml( description ) );
			}

			public override void OnCancel( Mobile from )
			{
				if ( !m_VI.Valid || !m_Vendor.CanInteractWith( from, true ) )
					return;

				SetInfo( from, -1, "" );
			}

			private void SetInfo( Mobile from, int price, string description )
			{
				Item item = m_VI.Item;

				bool setPrice = false;

				if ( price < 0 ) // Not for sale
				{
					price = -1;

					if ( item is Container )
					{
						if ( item is LockableContainer && ((LockableContainer)item).Locked )
							m_Vendor.SayTo( from, 1043298 ); // Locked items may not be made not-for-sale.
						else if ( item.Items.Count > 0 )
							m_Vendor.SayTo( from, 1043299 ); // To be not for sale, all items in a container must be for sale.
						else
							setPrice = true;
					}
					else if ( item is BaseBook || item is Engines.BulkOrders.BulkOrderBook )
					{
						setPrice = true;
					}
					else
					{
						m_Vendor.SayTo( from, 1043301 ); // Only the following may be made not-for-sale: books, containers, keyrings, and items in for-sale containers.
					}
				}
				else
				{
					if ( price > 100000000 )
					{
						price = 100000000;
						from.SendMessage( "You cannot price items above 100,000,000 gold.  The price has been adjusted." );
					}

					setPrice = true;
				}

				if ( setPrice )
				{
					m_Vendor.SetVendorItem( item, price, description );
				}
				else
				{
					m_VI.Description = description;
				}
			}
		}

		private class CollectGoldPrompt : Prompt
		{
			private PlayerVendor m_Vendor;

			public CollectGoldPrompt( PlayerVendor vendor )
			{
				m_Vendor = vendor;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( !m_Vendor.CanInteractWith( from, true ) )
					return;

				text = text.Trim();

				int amount;

				if ( !int.TryParse( text, out amount ) )
					amount = 0;

				GiveGold( from, amount );
			}

			public override void OnCancel( Mobile from )
			{
				if ( !m_Vendor.CanInteractWith( from, true ) )
					return;

				GiveGold( from, 0 );
			}

			private void GiveGold( Mobile to, int amount )
			{
				if ( amount <= 0 )
				{
					m_Vendor.SayTo( to, "Very well. I will hold on to the money for now then." );
				}
				else
				{
					m_Vendor.GiveGold( to, amount );
				}
			}
		}

		private class VendorNamePrompt : Prompt
		{
			private PlayerVendor m_Vendor;

			public VendorNamePrompt( PlayerVendor vendor )
			{
				m_Vendor = vendor;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( !m_Vendor.CanInteractWith( from, true ) )
					return;

				string name = text.Trim();

				//if ( !NameVerification.Validate( name, 1, 20, true, true, true, 0, NameVerification.Empty ) )
				//{
				//	m_Vendor.SayTo( from, "That name is unacceptable." );
				//	return;
				//}

				m_Vendor.Name = Utility.FixHtml( name );

				from.SendLocalizedMessage( 1062496 ); // Your vendor has been renamed.

				from.SendGump( new NewPlayerVendorOwnerGump( m_Vendor ) );
			}
		}

		private class ShopNamePrompt : Prompt
		{
			private PlayerVendor m_Vendor;

			public ShopNamePrompt( PlayerVendor vendor )
			{
				m_Vendor = vendor;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( !m_Vendor.CanInteractWith( from, true ) )
					return;

				string name = text.Trim();

				if ( !NameVerification.Validate( name, 1, 20, true, true, true, 0, NameVerification.Empty ) )
				{
					m_Vendor.SayTo( from, "That name is unacceptable." );
					return;
				}

				m_Vendor.ShopName = Utility.FixHtml( name );

				from.SendGump( new NewPlayerVendorOwnerGump( m_Vendor ) );
			}
		}

		public override bool CanBeDamaged()
		{
			return false;
		}

	}

	public class PlayerVendorPlaceholder : Item
	{
		private PlayerVendor m_Vendor;
		private ExpireTimer m_Timer;

		[CommandProperty( AccessLevel.GameMaster )]
		public PlayerVendor Vendor{ get{ return m_Vendor; } }

		public PlayerVendorPlaceholder( PlayerVendor vendor ) : base( 0x1F28 )
		{
			Hue = 0x672;
			Movable = false;

			m_Vendor = vendor;

			m_Timer = new ExpireTimer( this );
			m_Timer.Start();
		}

		public PlayerVendorPlaceholder( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Vendor != null )
				list.Add( 1062498, m_Vendor.Name ); // reserved for vendor ~1_NAME~
		}

		public void RestartTimer()
		{
			m_Timer.Stop();
			m_Timer.Start();
		}

		private class ExpireTimer : Timer
		{
			private PlayerVendorPlaceholder m_Placeholder;

			public ExpireTimer( PlayerVendorPlaceholder placeholder ) : base( TimeSpan.FromMinutes( 2.0 ) )
			{
				m_Placeholder = placeholder;

				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Placeholder.Delete();
			}
		}

		public override void OnDelete()
		{
			if ( m_Vendor != null && !m_Vendor.Deleted )
			{
				m_Vendor.MoveToWorld( this.Location, this.Map );
				m_Vendor.Placeholder = null;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 );

			writer.Write( (Mobile) m_Vendor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Vendor = (PlayerVendor) reader.ReadMobile();

			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( Delete ) );
		}
	}
}
