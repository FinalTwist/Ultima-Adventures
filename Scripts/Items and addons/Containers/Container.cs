using System;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Items
{
	public abstract class BaseContainer : Container
	{
		public override int DefaultMaxWeight
		{
			get
			{
				if ( IsSecure || this is Corpse )
					return 0;

				return base.DefaultMaxWeight;
			}
		}

		public BaseContainer( int itemID ) : base( itemID )
		{
		}

		public override bool IsAccessibleTo( Mobile m )
		{
			if ( !BaseHouse.CheckAccessible( m, this ) )
				return false;

			return base.IsAccessibleTo( m );
		}

		public override void OnAfterSpawn()
		{
			Server.Mobiles.PremiumSpawner.SpreadItems( this );
			base.OnAfterSpawn();
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( this.IsSecure && !BaseHouse.CheckHold( m, this, item, message, checkItems, plusItems, plusWeight ) )
				return false;

			return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );
		}

		public override bool CheckItemUse( Mobile from, Item item )
		{
			if ( IsDecoContainer && item is BaseBook )
				return true;

			return base.CheckItemUse( from, item );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
			RenameContainer.AddTo( from, this, list );
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{		

			if ( !CheckHold( from, dropped, sendFullMessage, true ) )
				return false;

			if ((dropped is CorpseItem || dropped is TombStone || dropped is Corpse) && !(this is Backpack && this.ParentEntity is PlayerMobile && this.Layer == Layer.Backpack))
			{
				return false;
			}
			
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsLockedDown( this ) )
			{
				if ( dropped is VendorRentalContract || ( dropped is Container && ((Container)dropped).FindItemByType( typeof( VendorRentalContract ) ) != null ) )
				{
					from.SendLocalizedMessage( 1062492 ); // You cannot place a rental contract in a locked down container.
					return false;
				}

				if ( !house.LockDown( from, dropped, false ) )
					return false;
			}

			List<Item> list = this.Items;

			for ( int i = 0; i < list.Count; ++i )
			{
				Item item = list[i];

				if ( !(item is Container) && item.StackWith( from, dropped, false ) )
					return true;
			}

			DropItem( dropped );

			return true;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{

			if ((dropped is CorpseItem || dropped is TombStone || dropped is Corpse) && !(this is Backpack && this.ParentEntity is PlayerMobile && this.Layer == Layer.Backpack))
			{
				return false;
			}

			return base.OnDragDrop( from, dropped);
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{

			if ( !CheckHold( from, item, true, true ) )
				return false;

			if ((item is CorpseItem || item is TombStone || item is Corpse) && !(this is Backpack && this.ParentEntity is PlayerMobile && this.Layer == Layer.Backpack))
			{
				return false;
			}

			if ((item is CorpseItem || item is TombStone || item is Corpse) && this is BankBox )
				return false;

			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsLockedDown( this ) )
			{
				if ( item is VendorRentalContract || ( item is Container && ((Container)item).FindItemByType( typeof( VendorRentalContract ) ) != null ) )
				{
					from.SendLocalizedMessage( 1062492 ); // You cannot place a rental contract in a locked down container.
					return false;
				}

				if ( !house.LockDown( from, item, false ) )
					return false;
			}

			item.Location = new Point3D( p.X, p.Y, 0 );
			AddItem( item );

			from.SendSound( GetDroppedSound( item ), GetWorldLocation() );
	
			return true;
		}

        private static System.Timers.Timer invalidateTimer = new System.Timers.Timer(500);
        private static bool isInvalidating = false;
        private static Mobile mobToInvalidate = null;

		public bool Renamable()
		{
			BaseHouse house = BaseHouse.FindHouseAt( this);

			if ( house == null )
				return false;

			if (house.IsLockedDown( this ))
				return true;
			if (house.IsSecure( this))
				return true;

			return false;
		}

        public override void UpdateTotal( Item sender, TotalType type, int delta )
        {
            base.UpdateTotal( sender, type, delta );

            /*if ( type == TotalType.Weight && RootParent is Mobile )
			{
				((Mobile)RootParent).InvalidateProperties();
			}*/
            if ( type == TotalType.Weight && RootParent is Mobile && !isInvalidating ) {

                isInvalidating = true;
                mobToInvalidate = (Mobile)RootParent;
                invalidateTimer.Elapsed += OnInvalidateEvent;
                invalidateTimer.AutoReset = true;
                invalidateTimer.Enabled = true;
            }
        }
        private static void OnInvalidateEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (mobToInvalidate != null)
                mobToInvalidate.InvalidateProperties();
            isInvalidating = false;
            invalidateTimer.Elapsed -= OnInvalidateEvent;
            invalidateTimer.Enabled = false;
        }

		public override void OnDoubleClick( Mobile from )
		{
			
			if ( from.AccessLevel > AccessLevel.Player || this.RootParent is PlayerVendor || this.RootParentEntity == from )
			{
				Open( from );
			}
			else if (from.InRange( this.GetWorldLocation(), 2 ))
			{
				BaseHouse house = BaseHouse.FindHouseAt( Location, Map, 2 );
				if (house != null && house.Owner != null && !BaseHouse.CheckLockedDownOrSecured(this))
				{
					from.SendMessage("A container must be secured or locked down to be used in a house.");
				}
				else
					Open( from );
			}
			else
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
		}

		public virtual void Open( Mobile from )
		{
			DisplayTo( from );
		}


		public BaseContainer( Serial serial ) : base( serial )
		{
		}

		/* Note: base class insertion; we cannot serialize anything here */
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			if ( ItemID == 0x0E75 || ItemID == 0x09B2 || ItemID == 0x53D5 || ItemID == 0x27BE || ItemID == 0x27D7 || ItemID == 0x4C53 || ItemID == 0x4C54 || ItemID == 0x1C10 || ItemID == 0x1CC6 || ItemID == 0x3582 || ItemID == 0x3583 || ItemID == 0x35AD || ItemID == 0x3868 || ItemID == 0x4B5A || ItemID == 0x4B5B || ItemID == 0x4B5C || ItemID == 0x4B5D || ItemID == 0x4B5E || ItemID == 0x4B5F || ItemID == 0x4B60 || ItemID == 0x4B61 || ItemID == 0x4B62 || ItemID == 0x4B63 || ItemID == 0x4B64 || ItemID == 0x4B65 || ItemID == 0x4B66 || ItemID == 0x4B67 || ItemID == 0x4B68 || ItemID == 0x4B69 || ItemID == 0x4B6A || ItemID == 0x4B6B || ItemID == 0x4B6C || ItemID == 0x4B6D || ItemID == 0x4B6E || ItemID == 0x4B6F || ItemID == 0x4B70 || ItemID == 0x4B71 || ItemID == 0x4B72 || ItemID == 0x4B73 || ItemID == 0x4B74 || ItemID == 0x4B75 || ItemID == 0x4B76 || ItemID == 0x4B77 || ItemID == 0x4B78 || ItemID == 0x4B79 || ItemID == 0x4B7A || ItemID == 0x4B7B || ItemID == 0x4B7C || ItemID == 0x4B7D || ItemID == 0x4B7E || ItemID == 0x4B7F || ItemID == 0x4B80 || ItemID == 0x4B81 || ItemID == 0x4B82 || ItemID == 0x4B83 || ItemID == 0x4B84 || ItemID == 0x4B85 || ItemID == 0x4B86 || ItemID == 0x4B87 || ItemID == 0x4B88 || ItemID == 0x4B89 || ItemID == 0x4B8A || ItemID == 0x4B8B || ItemID == 0x4B8C || ItemID == 0x4B8D || ItemID == 0x4B8E || ItemID == 0x4B8F || ItemID == 0x4B90 || ItemID == 0x4B91 || ItemID == 0x4B92 || ItemID == 0x4B93 || ItemID == 0x4B94 || ItemID == 0x4B95 || ItemID == 0x4B96 || ItemID == 0x4B97 || ItemID == 0x4B98 || ItemID == 0x4B99 || ItemID == 0x4B9A || ItemID == 0x4B9B || ItemID == 0x4B9C || ItemID == 0x4B9D || ItemID == 0x4B9E || ItemID == 0x4B9F || ItemID == 0x4BA0 || ItemID == 0x4BA1 || ItemID == 0x4BA2 || ItemID == 0x4BA3 || ItemID == 0x4BA4 || ItemID == 0x4BA5 || ItemID == 0x4BA6 || ItemID == 0x4BA7 || ItemID == 0x4BA8 || ItemID == 0x4BA9 || ItemID == 0x4BAA || ItemID == 0x4BAB )
			{
				if ( GumpID >= 0x415 && GumpID <= 0x41C ){ /* DO NOTHING */ }
				else { GumpID = 0x3C; }
				DropSound = 0x48;
			}
			else if ( ItemID == 0x2006 ){ GumpID = 0x9; DropSound = 0x42; }
			else if ( ItemID == 0xE76 || ItemID == 0x2256 || ItemID == 0x2257 || ItemID == 0x5777 || ItemID == 0x5776 || ItemID == 0x1E3F || ItemID == 0x1E52 || ItemID == 0x55DD || ItemID == 0x577E || ItemID == 0x1248 || ItemID == 0x1264 || ItemID == 0x541E || ItemID == 0x541F ){ GumpID = 0x3D; DropSound = 0x48; }
			else if ( ItemID == 0xE77 || ItemID == 0xE7F || ItemID == 0xFAE || ItemID == 0xE83 || ItemID == 0x4D05 || ItemID == 0x4D06 || ItemID == 0x50AF || ItemID == 0x50B0 || ItemID == 0x50B1 || ItemID == 0x50B2 || ItemID == 0x50B4 || ItemID == 0x50B6 || ItemID == 0x50B7 || ItemID == 0x50B8 || ItemID == 0x50BC || ItemID == 0x50BD || ItemID == 0x50BE || ItemID == 0x50BF || ItemID == 0x50C0 || ItemID == 0x50C1 || ItemID == 0x50C2 || ItemID == 0x50C3 || ItemID == 0x0C0F || ItemID == 0x0DB6 ){ GumpID = 0x3E; DropSound = 0x42; }
			else if ( ItemID == 0xE7A || ItemID == 0x24D5 || ItemID == 0x24D6 || ItemID == 0x24D9 || ItemID == 0x24DA ){ GumpID = 0x3F; DropSound = 0x4F; }
			else if ( ItemID == 0x990 || ItemID == 0x9AC || ItemID == 0x9B1 || ItemID == 0x24D7 || ItemID == 0x24D8 || ItemID == 0x24DD ){ GumpID = 0x41; DropSound = 0x4F; }
			else if ( ItemID == 0x52E2 || ItemID == 0x52E3 || ItemID == 0xE40 || ItemID == 0xE41 || ItemID == 0x3125 || ItemID == 0x3126 || ItemID == 0x312B || ItemID == 0x312C || ItemID == 0x3131 || ItemID == 0x3132 || ItemID == 0x3133 || ItemID == 0x3134 || ItemID == 0x3135 || ItemID == 0x3136 || ItemID == 0x3137 || ItemID == 0x3138 || ItemID == 0x3139 || ItemID == 0x313A || ItemID == 0x313B || ItemID == 0x313C || ItemID == 0x3330 || ItemID == 0x3331 || ItemID == 0x3332 || ItemID == 0x3333 || ItemID == 0x3334 || ItemID == 0x3335 || ItemID == 0x3336 || ItemID == 0x3337 || ItemID == 0x1A0F || ItemID == 0x1A10 || ItemID == 0x1A11 || ItemID == 0x1A12 || ItemID == 0x1A13 || ItemID == 0x1A14 || ItemID == 0x1A15 || ItemID == 0x1A16 || ItemID == 0x4FE1 || ItemID == 0x4FE2 || ItemID == 0x4FF4 || ItemID == 0x4FF5 ){ GumpID = 0x42; DropSound = 0x42; }
			else if ( ItemID == 0xE7D || ItemID == 0x9AA ){ GumpID = 0x43; DropSound = 0x42; }
			else if ( ItemID == 0xE7E || ItemID == 0x9A9 || ItemID == 0xE3C || ItemID == 0xE3D || ItemID == 0xE3E || ItemID == 0xE3F || ItemID == 0x531C || ItemID == 0x531D || ItemID == 0x5534 || ItemID == 0x5535 || ItemID == 0x4F86 || ItemID == 0x50B5 || ItemID == 0x4F87 || ItemID == 0x4F88 || ItemID == 0x4F89 || ItemID == 0x4F8A || ItemID == 0x4F8B || ItemID == 0x4F8C || ItemID == 0x4F8D || ItemID == 0x4F8E || ItemID == 0x4F8F || ItemID == 0x4F90 || ItemID == 0x4F91 || ItemID == 0x4F92 || ItemID == 0x4F93 || ItemID == 0x4F94 || ItemID == 0x4F95 || ItemID == 0x4F96 || ItemID == 0x4F97 || ItemID == 0x4F98 || ItemID == 0x4F99 || ItemID == 0x4F9A || ItemID == 0x4F9B || ItemID == 0x4F9C || ItemID == 0x4F9D || ItemID == 0x4F9E || ItemID == 0x5082 || ItemID == 0x545F || ItemID == 0x5460 || ItemID == 0x5083 || ItemID == 0x5084 || ItemID == 0x5085 || ItemID == 0x5086 || ItemID == 0x5087 || ItemID == 0x5088 || ItemID == 0x5089 || ItemID == 0x508A || ItemID == 0x508B || ItemID == 0x508C || ItemID == 0x508D || ItemID == 0x508E || ItemID == 0x508F || ItemID == 0x5090 || ItemID == 0x5091 || ItemID == 0x5092 || ItemID == 0x5093 || ItemID == 0x5094 || ItemID == 0x5095 || ItemID == 0x5096 || ItemID == 0x5097 || ItemID == 0x5098 || ItemID == 0x5099 || ItemID == 0x509A || ItemID == 0x509B || ItemID == 0x509C || ItemID == 0x509D || ItemID == 0x509E || ItemID == 0x509F || ItemID == 0x50A0 || ItemID == 0x50A1 || ItemID == 0x50A2 || ItemID == 0x50A3 || ItemID == 0x50A4 || ItemID == 0x50A5 || ItemID == 0x50A6 || ItemID == 0x50A7 || ItemID == 0x50A8 || ItemID == 0x50A9 || ItemID == 0x50AA || ItemID == 0x50AB || ItemID == 0x50AC || ItemID == 0x50AD || ItemID == 0x50AE || ItemID == 0x50B3 || ItemID == 0x50B9 || ItemID == 0x50BA || ItemID == 0x50BB || ItemID == 0x50C4 || ItemID == 0x50C5 || ItemID == 0x50C6 || ItemID == 0x50C7  || ItemID == 0x568A || ItemID == 0x55E0 || ItemID == 0x55E1 || ItemID == 0x55E2 || ItemID == 0x55E3 || ItemID == 0x55E4 || ItemID == 0x55E5 || ItemID == 0x55E6 || ItemID == 0x55E7 || ItemID == 0x55E8 || ItemID == 0x55E9 || ItemID == 0x55EA || ItemID == 0x55EB || ItemID == 0x55EC || ItemID == 0x55ED || ItemID == 0x55EE || ItemID == 0x55EF || ItemID == 0x55DF ){ GumpID = 0x44; DropSound = 0x42; }
			else if ( ItemID == 0xA30 || ItemID == 0xA38 || ItemID == 0x544F || ItemID == 0x5450 || ItemID == 0x5451 || ItemID == 0x5452 || ItemID == 0x5453 || ItemID == 0x5454 || ItemID == 0x5455 || ItemID == 0x5456 || ItemID == 0x5457 || ItemID == 0x5458 || ItemID == 0x5459 || ItemID == 0x545A || ItemID == 0x545B || ItemID == 0x545C || ItemID == 0x545D || ItemID == 0x545E ){ GumpID = 0x48; DropSound = 0x42; }
			else if ( ItemID == 0x5718 || ItemID == 0x5719 || ItemID == 0x571A || ItemID == 0x571B || ItemID == 0x5752 || ItemID == 0x5753 || ItemID == 0xE42 || ItemID == 0xE43 || ItemID == 0x4104 || ItemID == 0x4102 || ItemID == 0x4109 || ItemID == 0x4106 || ItemID == 0x4910 || ItemID == 0x4911 || ItemID == 0x4C2B || ItemID == 0x4C2C || ItemID == 0x141E || ItemID == 0x141F || ItemID == 0x1C0E || ItemID == 0x1C0F ){ GumpID = 0x49; DropSound = 0x42; }
			else if ( ItemID == 0xE7C || ItemID == 0x9AB ){ GumpID = 0x4A; DropSound = 0x42; }
			else if ( ItemID == 0xE80 || ItemID == 0x9A8 ){ GumpID = 0x4B; DropSound = 0x42; }
			else if ( ItemID == 0x3E65 || ItemID == 0x3E93 || ItemID == 0x3EAE || ItemID == 0x3EB9 || ItemID == 0x2299 || ItemID == 0x229A || ItemID == 0x229B || ItemID == 0x229C || ItemID == 0x229D || ItemID == 0x229E || ItemID == 0x229F || ItemID == 0x22A0 || ItemID == 0x507C || ItemID == 0x507D || ItemID == 0x5186 || ItemID == 0x5199 ){ GumpID = 0x4C; DropSound = 0x42; }
			else if ( ItemID == 0xA97 || ItemID == 0xA98 || ItemID == 0xA99 || ItemID == 0xA9A || ItemID == 0xA9B || ItemID == 0xA9C || ItemID == 0xA9D || ItemID == 0xA9E ){ GumpID = 0x4D; DropSound = 0x42; }
			else if ( ItemID == 0xA4D || ItemID == 0xA4C || ItemID == 0xA50 || ItemID == 0xA51 ){ GumpID = 0x4E; DropSound = 0x42; }
			else if ( ItemID == 0xA4E || ItemID == 0xA4F || ItemID == 0xA52 || ItemID == 0xA53 ){ GumpID = 0x4F; DropSound = 0x42; }
			else if ( ItemID == 0xA2C || ItemID == 0xA34 || ItemID == 0xC24 || ItemID == 0xC25 ){ GumpID = 0x51; DropSound = 0x42; }
			else if ( ItemID == 0x1E5E ){ GumpID = 0x52; DropSound = 0x42; }
			else if ( ItemID == 0x2B02 || ItemID == 0x2B03 || ItemID == 0x5770 ){ GumpID = 0x2648; DropSound = 0x48; }
			else if ( ItemID == 0x232A || ItemID == 0x232B ){ GumpID = 0x102; DropSound = 0x42; }
			else if ( ItemID == 0x2857 || ItemID == 0x2858 ){ GumpID = 0x105; DropSound = 0x42; }
			else if ( ItemID == 0x285B || ItemID == 0x285C || ItemID == 0xC12 || ItemID == 0xC13 ){ GumpID = 0x106; DropSound = 0x42; }
			else if ( ItemID == 0x285D || ItemID == 0x285E || ItemID == 0x2859 || ItemID == 0x285A ){ GumpID = 0x107; DropSound = 0x42; }
			else if ( ItemID == 0x24DB || ItemID == 0x24DC ){ GumpID = 0x108; DropSound = 0x4F; }
			else if ( ItemID == 0x280B || ItemID == 0x280C ){ GumpID = 0x109; DropSound = 0x42; }
			else if ( ItemID == 0x280F || ItemID == 0x2810 ){ GumpID = 0x10A; DropSound = 0x42; }
			else if ( ItemID == 0x280D || ItemID == 0x280E ){ GumpID = 0x10B; DropSound = 0x42; }
			else if ( ItemID == 0x2811 || ItemID == 0x2812 || ItemID == 0x2815 || ItemID == 0x2816 || ItemID == 0x2817 || ItemID == 0x2818 ){ GumpID = 0x10C; DropSound = 0x42; }
			else if ( ItemID == 0x2813 || ItemID == 0x2814 ){ GumpID = 0x10D; DropSound = 0x42; }
			else if ( ItemID == 0x27E0 || ItemID == 0x280A || ItemID == 0x2802 || ItemID == 0x2803 ){ GumpID = 0x1D; DropSound = 0x22B; }
			else if ( ItemID == 0x5329 || ItemID == 0x532A || ItemID == 0x4FE3 || ItemID == 0x4FE4 || ItemID == 0x281D || ItemID == 0x281E || ItemID == 0x0436 || ItemID == 0x0437 || ItemID == 0x507E || ItemID == 0x507F || ItemID == 0x5080 || ItemID == 0x5081 ){ GumpID = 0x975; DropSound = 0x42; }
			else if ( ItemID == 0x10EA || ItemID == 0x10EB || ItemID == 0x10EC || ItemID == 0x10ED || ItemID == 0x3564 || ItemID == 0x3565 ){ GumpID = 0x976; DropSound = 0x42; }
			else if ( ItemID == 0x1AFC || ItemID == 0x1AFD || ItemID == 0x1AFE || ItemID == 0x1AFF || ItemID == 0x398B || ItemID == 0x39A2 || ItemID == 0x4B59 || ItemID == 0x4C2A ){ GumpID = 0x13B1; DropSound = 0x22B; }
			else if ( ItemID == 0x4FDB || ItemID == 0x4FDC || ItemID == 0x3BF0 || ItemID == 0x3BF1 || ItemID == 0x3BF2 || ItemID == 0x3BF3 || ItemID == 0x3BF4 || ItemID == 0x3BF9 || ItemID == 0x3BFA || ItemID == 0x3BFB || ItemID == 0x3BFC || ItemID == 0x3BFD || ItemID == 0x3BFE || ItemID == 0x3BFF || ItemID == 0x3C00 || ItemID == 0x3C15 || ItemID == 0x3C16 || ItemID == 0x3C17 || ItemID == 0x3C18 || ItemID == 0x3C19 || ItemID == 0x3C1A || ItemID == 0x3C1B || ItemID == 0x3C1C || ItemID == 0x3C1D || ItemID == 0x3C1E || ItemID == 0x3C21 || ItemID == 0x3C22 || ItemID == 0x3C23 || ItemID == 0x3C24 || ItemID == 0x3C25 || ItemID == 0x3C26 || ItemID == 0x3C27 || ItemID == 0x3C28 || ItemID == 0x3C29 || ItemID == 0x3C2A || ItemID == 0x3C2B || ItemID == 0x3C2C || ItemID == 0x3C2D || ItemID == 0x3C2E || ItemID == 0x3C2F || ItemID == 0x3C30 || ItemID == 0x3C31 || ItemID == 0x3C32 || ItemID == 0x3C33 || ItemID == 0x3C34 || ItemID == 0x3C35 || ItemID == 0x3C36 || ItemID == 0x3C37 || ItemID == 0x3C38 || ItemID == 0x3C39 || ItemID == 0x3C3A || ItemID == 0x3C3B || ItemID == 0x3C3C || ItemID == 0x3C3D || ItemID == 0x3C3E || ItemID == 0x3C3F || ItemID == 0x3C40 || ItemID == 0x3C41 || ItemID == 0x3C42 || ItemID == 0x3C49 || ItemID == 0x3C4A || ItemID == 0x3C4B || ItemID == 0x3C4C || ItemID == 0x3C4D || ItemID == 0x3C4E || ItemID == 0x3C4F || ItemID == 0x3C50 || ItemID == 0x3C51 || ItemID == 0x3C52 || ItemID == 0x3C53 || ItemID == 0x3C54 || ItemID == 0x3C55 || ItemID == 0x3C56 || ItemID == 0x3C57 || ItemID == 0x3C58 || ItemID == 0x3C59 || ItemID == 0x3C5A || ItemID == 0x3C5B || ItemID == 0x3C5C || ItemID == 0x3C5D || ItemID == 0x3C5E || ItemID == 0x3C5F || ItemID == 0x3C60 || ItemID == 0x3C61 || ItemID == 0x3C62 || ItemID == 0x3C63 || ItemID == 0x3C64 || ItemID == 0x3C65 || ItemID == 0x3C66 || ItemID == 0x3C67 || ItemID == 0x3C68 || ItemID == 0x3C69 || ItemID == 0x3C6A || ItemID == 0x3C6B || ItemID == 0x3C6C || ItemID == 0x3C6D || ItemID == 0x3C6E || ItemID == 0x3C6F || ItemID == 0x3C70 || ItemID == 0x3C71 || ItemID == 0x3C72 || ItemID == 0x3C73 || ItemID == 0x3C74 || ItemID == 0x3C75 || ItemID == 0x3C76 || ItemID == 0x3C77 || ItemID == 0x3C78 || ItemID == 0x3C79 || ItemID == 0x3C7A || ItemID == 0x3C7B || ItemID == 0x3C7C || ItemID == 0x3C7D || ItemID == 0x3C7E || ItemID == 0x3C9B || ItemID == 0x3C9C || ItemID == 0x3C9D || ItemID == 0x3C9E || ItemID == 0x3C9F || ItemID == 0x3CA0 || ItemID == 0x3CA1 || ItemID == 0x3CA2 || ItemID == 0x3CA3 || ItemID == 0x3CA4 || ItemID == 0x3CA5 || ItemID == 0x3CA6 || ItemID == 0x3CA7 || ItemID == 0x3CA8 || ItemID == 0x3CAD || ItemID == 0x3CAE || ItemID == 0x3CAF || ItemID == 0x3CB0 || ItemID == 0x3CB1 || ItemID == 0x3CB2 || ItemID == 0x3CB3 || ItemID == 0x3CB4 || ItemID == 0x3CBF || ItemID == 0x3CC0 || ItemID == 0x3CC1 || ItemID == 0x3CC2 || ItemID == 0x3CC3 || ItemID == 0x3CC4 || ItemID == 0x3CC5 || ItemID == 0x3CC6 || ItemID == 0x3CC7 || ItemID == 0x3CC8 || ItemID == 0x3CD7 || ItemID == 0x3CD8 || ItemID == 0x3CD9 || ItemID == 0x3CDA || ItemID == 0x3CDB || ItemID == 0x3CDC || ItemID == 0x3CDD || ItemID == 0x3CDE || ItemID == 0x3CDF || ItemID == 0x3CE0 || ItemID == 0x3CE1 || ItemID == 0x3CE2 || ItemID == 0x3CE3 || ItemID == 0x3CE4 || ItemID == 0x3CE5 || ItemID == 0x3CE6 || ItemID == 0x3CE7 || ItemID == 0x3CE8 || ItemID == 0x3CE9 || ItemID == 0x3CEA || ItemID == 0x3CEB || ItemID == 0x3CEC || ItemID == 0x3CED || ItemID == 0x3CEE || ItemID == 0x3CEF || ItemID == 0x3CF0 || ItemID == 0x3CF1 || ItemID == 0x3CF2 || ItemID == 0x3CF3 || ItemID == 0x3CF4 || ItemID == 0x3CF5 || ItemID == 0x3CF6 || ItemID == 0x3CF7 || ItemID == 0x3CF8 || ItemID == 0x3CF9 || ItemID == 0x3CFA || ItemID == 0x3CFB || ItemID == 0x3CFC || ItemID == 0x3CFD || ItemID == 0x3CFE || ItemID == 0x3CFF || ItemID == 0x3D00 || ItemID == 0x3D01 || ItemID == 0x3D02 || ItemID == 0x3D03 || ItemID == 0x3D04 || ItemID == 0x3D05 || ItemID == 0x3D06 || ItemID == 0x3D07 || ItemID == 0x3D08 || ItemID == 0x3D09 || ItemID == 0x3D0A || ItemID == 0x19FF || ItemID == 0x1A00 || ItemID == 0xC14 || ItemID == 0xC15 || ItemID == 0x38B || ItemID == 0x38C || ItemID == 0x38D || ItemID == 0x38E || ItemID == 0x4FFE || ItemID == 0x4FFF || ItemID == 0x5000 || ItemID == 0x5001 || ItemID == 0x5002 || ItemID == 0x5003 || ItemID == 0x5004 || ItemID == 0x5005 || ItemID == 0x5006 || ItemID == 0x5007 || ItemID == 0x5008 || ItemID == 0x5009 || ItemID == 0x500A || ItemID == 0x500B || ItemID == 0x500C || ItemID == 0x500D || ItemID == 0x500E || ItemID == 0x500F || ItemID == 0x5010 || ItemID == 0x5011 || ItemID == 0x5012 || ItemID == 0x5013 || ItemID == 0x5014 || ItemID == 0x5015 || ItemID == 0x501A || ItemID == 0x501B || ItemID == 0x501C || ItemID == 0x501D || ItemID == 0x501E || ItemID == 0x501F || ItemID == 0x5020 || ItemID == 0x5021 || ItemID == 0x5022 || ItemID == 0x5023 || ItemID == 0x5024 || ItemID == 0x5025 || ItemID == 0x5026 || ItemID == 0x5027 || ItemID == 0x5028 || ItemID == 0x5029 || ItemID == 0x502A || ItemID == 0x502B || ItemID == 0x502C || ItemID == 0x502D || ItemID == 0x502E || ItemID == 0x502F || ItemID == 0x5030 || ItemID == 0x5031 || ItemID == 0x5032 || ItemID == 0x5033 || ItemID == 0x5034 || ItemID == 0x5035 || ItemID == 0x5038 || ItemID == 0x5039 || ItemID == 0x503A || ItemID == 0x503B || ItemID == 0x5064 || ItemID == 0x5065 || ItemID == 0x5066 || ItemID == 0x5067 || ItemID == 0x5068 || ItemID == 0x5069 || ItemID == 0x506A || ItemID == 0x506B || ItemID == 0x506C || ItemID == 0x506D || ItemID == 0x5070 || ItemID == 0x5071 ){ GumpID = 0x987; DropSound = 0x42; }
			else if ( ItemID == 0x3C43 || ItemID == 0x3C44 || ItemID == 0x3C45 || ItemID == 0x3C46 || ItemID == 0x3C47 || ItemID == 0x3C48 || ItemID == 0x3C7F || ItemID == 0x3C80 || ItemID == 0x3C81 || ItemID == 0x3C82 || ItemID == 0x3C83 || ItemID == 0x3C84 || ItemID == 0x3C85 || ItemID == 0x3C86 || ItemID == 0x3C87 || ItemID == 0x3C88 || ItemID == 0x3C89 || ItemID == 0x3C8A || ItemID == 0x3C8B || ItemID == 0x3C8C || ItemID == 0x3C8D || ItemID == 0x3C8E || ItemID == 0x3CB5 || ItemID == 0x3CB6 || ItemID == 0x3CB7 || ItemID == 0x3CB8 || ItemID == 0x3CB9 || ItemID == 0x3CBA || ItemID == 0x3CBB || ItemID == 0x3CBC || ItemID == 0x3CBD || ItemID == 0x3CBE || ItemID == 0x3CC9 || ItemID == 0x3CCA || ItemID == 0x3CCB || ItemID == 0x3CCC || ItemID == 0x3CCD || ItemID == 0x3CCE || ItemID == 0x3D0B || ItemID == 0x3D0C || ItemID == 0x3D20 || ItemID == 0x3D21 || ItemID == 0x3D22 || ItemID == 0x3D23 || ItemID == 0x3D24 || ItemID == 0x3D25 || ItemID == 0x3D26 || ItemID == 0x3D27 || ItemID == 0x4FF8 || ItemID == 0x4FF9 || ItemID == 0x4FFA || ItemID == 0x4FFB || ItemID == 0x4FFC || ItemID == 0x4FFD || ItemID == 0x5016 || ItemID == 0x5017 || ItemID == 0x5018 || ItemID == 0x5019 || ItemID == 0x5036 || ItemID == 0x5037 || ItemID == 0x503C || ItemID == 0x503D || ItemID == 0x503E || ItemID == 0x503F || ItemID == 0x5040 || ItemID == 0x5041 || ItemID == 0x5042 || ItemID == 0x5043 || ItemID == 0x5044 || ItemID == 0x5045 || ItemID == 0x5046 || ItemID == 0x5047 || ItemID == 0x5048 || ItemID == 0x5049 || ItemID == 0x504A || ItemID == 0x504B || ItemID == 0x504C || ItemID == 0x504D || ItemID == 0x504E || ItemID == 0x504F || ItemID == 0x5050 || ItemID == 0x5051 || ItemID == 0x5052 || ItemID == 0x5053 || ItemID == 0x5054 || ItemID == 0x5055 || ItemID == 0x5056 || ItemID == 0x5057 || ItemID == 0x5058 || ItemID == 0x5059 || ItemID == 0x505A || ItemID == 0x505B || ItemID == 0x505C || ItemID == 0x505D || ItemID == 0x505E || ItemID == 0x505F || ItemID == 0x5060 || ItemID == 0x5061 || ItemID == 0x5062 || ItemID == 0x5063 ){ GumpID = 0x989; DropSound = 0x42; }
			else if ( ItemID == 0x52FB || ItemID == 0x52FD || ItemID == 0x281F || ItemID == 0x2820 || ItemID == 0x2821 || ItemID == 0x2822 || ItemID == 0x2823 || ItemID == 0x2824 || ItemID == 0x2825 || ItemID == 0x2826 || ItemID == 0x4FE6 || ItemID == 0x4FE7 ){ GumpID = 0x2810; DropSound = 0x22B; }
			else if ( ItemID == 0x2800 || ItemID == 0x2801 || ItemID == 0x27E9 || ItemID == 0x27EA ){ GumpID = 0x41D; DropSound = 0x42; }
			else if ( ItemID == 0x0ECA || ItemID == 0x0ECB || ItemID == 0x0ECC || ItemID == 0x0ECD || ItemID == 0x0ECE || ItemID == 0x0ECF || ItemID == 0x0ED0 || ItemID == 0x0ED1 || ItemID == 0x0ED2 || ItemID == 0x1236 || ItemID == 0x123F ){ GumpID = 0x2A73; DropSound = 0x48; }

		}
	}







	public class CreatureBackpack : Backpack	//Used on BaseCreature
	{
		[Constructable]
		public CreatureBackpack( string name )
		{
			Name = name;
			Layer = Layer.Backpack;
			Hue = 5;
			Weight = 3.0;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Name != null )
				list.Add( 1075257, Name ); // Contents of ~1_PETNAME~'s pack.
			else
				base.AddNameProperty( list );
		}

		public override void OnItemRemoved( Item item )
		{
			if ( Items.Count == 0 )
				this.Delete();

			base.OnItemRemoved( item );
		}

		public override bool OnDragLift( Mobile from )
		{
			
			if ( from.AccessLevel > AccessLevel.Player )
				return true;

			Mobile bobo = null;
			
			if (this.RootParent is Mobile)
				bobo = this.RootParent as Mobile;

			if ( bobo != null && bobo is PlayerMobile )
			{
				PlayerMobile root = bobo as PlayerMobile;
				if (root.caught)
					return true;
			}

			if (bobo != null && bobo is BaseChild && ((BaseChild)bobo).stole == from )
				return true;

			BaseHouse house = BaseHouse.FindHouseAt( this.Location, Map, 2 );
			if (house != null)
			{
				from.Say("container");
				if (house.IsCoOwner( from ))
				{
					return true;
				}
			}

			from.SendLocalizedMessage( 500169 ); // You cannot pick that up.
			return false;
		
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			return false;
		}



		public CreatureBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 )
				Weight = 13.0;
		}
	}

	public class StrongBackpack : Backpack	//Used on Pack animals
	{
		[Constructable]
		public StrongBackpack()
		{
			Layer = Layer.Backpack;
			Weight = 13.0;
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			return base.CheckHold( m, item, false, checkItems, plusItems, plusWeight );
		}

		public override int DefaultMaxWeight{ get{ return 2400; } }

		public override int DefaultMaxItems {
			get {
					Mobile m = ParentEntity as Mobile;
					if ( m != null && m is PlayerVendor && m.Backpack == this ) {
                        return 250; // too high causes lag on player death and corpse loot
                    } else {
						return base.DefaultMaxItems;
					}
				}
		}
		public override bool CheckContentDisplay( Mobile from )
		{
			object root = this.RootParent;

			if ( root is BaseCreature && ((BaseCreature)root).Controlled && ((BaseCreature)root).ControlMaster == from )
				return true;

			return base.CheckContentDisplay( from );
		}

		public StrongBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 )
				Weight = 13.0;
		}
	}

	public class Backpack : BaseContainer, IDyable
	{
		[Constructable]
		public Backpack() : base( 0xE75 )
		{
			Layer = Layer.Backpack;
			Weight = 3.0;
		}

		public override int DefaultMaxWeight {
			get {
					Mobile m = ParentEntity as Mobile;
					if ( m != null && (m.Player || m is PlayerVendor) && m.Backpack == this ) {
                        return 800; //increased from 550
                    } else {
						return base.DefaultMaxWeight;
					}
			}
		}

		public override int DefaultMaxItems {
			get {
					Mobile m = ParentEntity as Mobile;
					if ( m != null && (m.Player || m is PlayerVendor) && m.Backpack == this ) {
                        return 250; // too high causes lag on player death and corpse loot
                    } else {
						return base.DefaultMaxItems;
					}
				}
		}

		public Backpack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && ItemID == 0x9B2 )
				ItemID = 0xE75;
		}
	}

	public class Pouch : TrapableContainer
	{
		[Constructable]
		public Pouch() : base( 0xE79 )
		{
			Weight = 1.0;
		}

		public Pouch( Serial serial ) : base( serial )
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

	public abstract class BaseBagBall : BaseContainer, IDyable
	{
		public BaseBagBall( int itemID ) : base( itemID )
		{
			Weight = 1.0;
		}

		public BaseBagBall( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

	public class SmallBagBall : BaseBagBall
	{
		[Constructable]
		public SmallBagBall() : base( 0x2256 )
		{
		}

		public SmallBagBall( Serial serial ) : base( serial )
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

	public class LargeBagBall : BaseBagBall
	{
		[Constructable]
		public LargeBagBall() : base( 0x2257 )
		{
		}

		public LargeBagBall( Serial serial ) : base( serial )
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

	public class Bag : BaseContainer, IDyable
	{
		[Constructable]
		public Bag() : base( 0xE76 )
		{
			Weight = 2.0;
			Hue = 0xABE;
		}

		public Bag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			if ( Hue == 0 ){ Hue = 0xABE; }

			return true;
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

			if ( Hue == 0 ){ Hue = 0xABE; }
		}
	}

	[Flipable( 0x1E3F, 0x1E52 )]
	public class LargeBag : BaseContainer, IDyable
	{
		[Constructable]
		public LargeBag() : base( 0x1E3F )
		{
			Name = "large bag";
			Weight = 2.0;
			GumpID = 0x3D;
		}

		public LargeBag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
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

	[Flipable( 0x1248, 0x1264 )]
	public class GiantBag : BaseContainer, IDyable
	{
		[Constructable]
		public GiantBag() : base( 0x1248 )
		{
			Name = "giant bag";
			Weight = 4.0;
			GumpID = 0x3D;
		}

		public GiantBag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
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

	[Flipable( 0x1C10, 0x1CC6 )]
	public class LargeSack : BaseContainer, IDyable
	{
		[Constructable]
		public LargeSack() : base( 0x1C10 )
		{
			Name = "large rucksack";
			Weight = 3.0;
			GumpID = 0x2A74;
		}

		public LargeSack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x27BE, 0x27D7 )]
	public class RuggedBackpack : BaseContainer, IDyable
	{
		[Constructable]
		public RuggedBackpack() : base( 0x27BE )
		{
			Name = "rugged backpack";
			Weight = 3.0;
			GumpID = 0x2A74;
		}

		public RuggedBackpack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;
			Hue = sender.DyedHue;
			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class Barrel : BaseContainer
	{
		[Constructable]
		public Barrel() : base( 0xE77 )
		{
			Weight = 25.0;
		}

		public Barrel( Serial serial ) : base( serial )
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

			if ( Weight == 0.0 )
				Weight = 25.0;
		}
	}

	public class Keg : BaseContainer
	{
		[Constructable]
		public Keg() : base( 0xE7F )
		{
			Weight = 15.0;
		}

		public Keg( Serial serial ) : base( serial )
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

	public class PicnicBasket : BaseContainer
	{
		[Constructable]
		public PicnicBasket() : base( 0xE7A )
		{
			Weight = 2.0; // Stratics doesn't know weight
		}

		public PicnicBasket( Serial serial ) : base( serial )
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

	public class Basket : BaseContainer
	{
		[Constructable]
		public Basket() : base( 0x990 )
		{
			Weight = 1.0; // Stratics doesn't know weight
		}

		public Basket( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x24D9, 0x24DA )]
	public class OrientBasket1 : BaseContainer
	{
		[Constructable]
		public OrientBasket1() : base( 0x24D9 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket1( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x24D5, 0x24D6 )]
	public class OrientBasket2 : BaseContainer
	{
		[Constructable]
		public OrientBasket2() : base( 0x24D5 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket2( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x24DB, 0x24DC )]
	public class OrientBasket3 : BaseContainer
	{
		[Constructable]
		public OrientBasket3() : base( 0x24DB )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket3( Serial serial ) : base( serial )
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

	public class OrientBasket4 : BaseContainer
	{
		[Constructable]
		public OrientBasket4() : base( 0x24D7 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket4( Serial serial ) : base( serial )
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

	public class OrientBasket5 : BaseContainer
	{
		[Constructable]
		public OrientBasket5() : base( 0x24D8 )
		{
			Weight = 1.0;
			Name = "basket";
		}

		public OrientBasket5( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x9AA, 0xE7D )]
	public class WoodenBox : LockableContainer
	{
		[Constructable]
		public WoodenBox() : base( 0x9AA )
		{
			Weight = 4.0;
		}

		public WoodenBox( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0x9A9, 0xE7E )]
	public class SmallCrate : LockableContainer
	{
		[Constructable]
		public SmallCrate() : base( 0x9A9 )
		{
			Weight = 2.0;
		}

		public SmallCrate( Serial serial ) : base( serial )
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

			if ( Weight == 4.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0xE3F, 0xE3E )]
	public class MediumCrate : LockableContainer
	{
		[Constructable]
		public MediumCrate() : base( 0xE3F )
		{
			Weight = 2.0;
		}

		public MediumCrate( Serial serial ) : base( serial )
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

			if ( Weight == 6.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0xE3D, 0xE3C )]
	public class LargeCrate : LockableContainer
	{
		[Constructable]
		public LargeCrate() : base( 0xE3D )
		{
			Weight = 1.0;
		}

		public LargeCrate( Serial serial ) : base( serial )
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

			if ( Weight == 8.0 )
				Weight = 1.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x436, 0x437 )]
	public class MetalSafe : LockableContainer
	{
		[Constructable]
		public MetalSafe() : base( 0x436 )
		{
			Name = "metal safe";
			GumpID = 0x975;
			Weight = 25.0;
		}

		public MetalSafe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x5329, 0x532A )]
	public class IronSafe : LockableContainer
	{
		[Constructable]
		public IronSafe() : base( 0x5329 )
		{
			Name = "iron safe";
			GumpID = 0x975;
			Weight = 20.0;
		}

		public IronSafe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x4FE3, 0x4FE4 )]
	public class MetalVault : LockableContainer
	{
		[Constructable]
		public MetalVault() : base( 0x4FE3 )
		{
			Name = "metal safe";
			GumpID = 0x975;
			Weight = 25.0;
		}

		public MetalVault( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x4D05, 0x4D06 )]
	public class ArmsBarrel : LockableContainer
	{
		[Constructable]
		public ArmsBarrel() : base( 0x4D05 )
		{
			Name = "arms barrel";
			GumpID = 0x2A78;
			Server.Misc.MaterialInfo.ColorPlainMetal( this );
			Weight = 25.0;
		}

		public ArmsBarrel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x0C0F, 0x0DB6 )]
	public class NecromancerBarrel : LockableContainer
	{
		[Constructable]
		public NecromancerBarrel() : base( 0x0C0F )
		{
			Name = "necromancer barrel";
			GumpID = 0x2A78;
			Weight = 25.0;
		}

		public NecromancerBarrel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[DynamicFliping]
	[Flipable( 0x9A8, 0xE80 )]
	public class MetalBox : LockableContainer
	{
		[Constructable]
		public MetalBox() : base( 0x9A8 )
		{
		}

		public MetalBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 3 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x9AB, 0xE7C )]
	public class MetalChest : LockableContainer
	{
		[Constructable]
		public MetalChest() : base( 0x9AB )
		{
		}

		public MetalChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x281D, 0x281E )]
	public class StoneCoffer : LockableContainer
	{
		[Constructable]
		public StoneCoffer() : base( 0x281D )
		{
			Name = "stone coffer";
			GumpID = 0x2810;
		}

		public StoneCoffer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x52FB, 0x52FD )]
	public class VirtueStoneChest : LockableContainer
	{
		[Constructable]
		public VirtueStoneChest() : base( 0x52FB )
		{
			Name = "chest of virtue";
			GumpID = 0x2810;
			Light = LightType.Circle225;
		}

		public VirtueStoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x281F, 0x2820 )]
	public class GildedStoneChest : LockableContainer
	{
		[Constructable]
		public GildedStoneChest() : base( 0x281F )
		{
			Name = "gilded stone chest";
			GumpID = 0x2810;
		}

		public GildedStoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2821, 0x2822 )]
	public class FancyStoneChest : LockableContainer
	{
		[Constructable]
		public FancyStoneChest() : base( 0x2821 )
		{
			Name = "fancy stone chest";
			GumpID = 0x2810;
		}

		public FancyStoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2825, 0x2826 )]
	public class StoneStrongbox : LockableContainer
	{
		[Constructable]
		public StoneStrongbox() : base( 0x2825 )
		{
			Name = "stone strongbox";
			GumpID = 0x2810;
		}

		public StoneStrongbox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x2823, 0x2824 )]
	public class StoneChest : LockableContainer
	{
		[Constructable]
		public StoneChest() : base( 0x2823 )
		{
			Name = "stone chest";
			GumpID = 0x2810;
		}

		public StoneChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3330, 0x3331 )]
	public class SilverChest : LockableContainer
	{
		[Constructable]
		public SilverChest() : base( 0x3330 )
		{
			Name = "silver chest";
		}

		public SilverChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3332, 0x3333 )]
	public class RustyChest : LockableContainer
	{
		[Constructable]
		public RustyChest() : base( 0x3332 )
		{
			Name = "rusty chest";
		}

		public RustyChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3334, 0x3335 )]
	public class BronzeChest : LockableContainer
	{
		[Constructable]
		public BronzeChest() : base( 0x3334 )
		{
			Name = "bronze chest";
		}

		public BronzeChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x3336, 0x3337 )]
	public class IronChest : LockableContainer
	{
		[Constructable]
		public IronChest() : base( 0x3336 )
		{
			Name = "iron chest";
		}

		public IronChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x10EC, 0x10ED )]
	public class SpaceChest : LockableContainer
	{
		[Constructable]
		public SpaceChest() : base( 0x10EC )
		{
			Name = "metal trunk";
			GumpID = 0x976;
		}

		public SpaceChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	public class SpaceCrate : LockableContainer
	{
		[Constructable]
		public SpaceCrate() : base( 0x10EA )
		{
			Name = "metal crate";
			GumpID = 0x976;
		}

		public SpaceCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	public class HazardCrate : LockableContainer
	{
		[Constructable]
		public HazardCrate() : base( 0x10EB )
		{
			Name = "containment crate";
			GumpID = 0x976;
		}

		public HazardCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0xE41, 0xE40 )]
	public class MetalGoldenChest : LockableContainer
	{
		[Constructable]
		public MetalGoldenChest() : base( 0xE41 )
		{
		}

		public MetalGoldenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0xe43, 0xe42 )]
	public class WoodenChest : LockableContainer
	{
		[Constructable]
		public WoodenChest() : base( 0xe43 )
		{
			Weight = 2.0;
			Hue = 0x724;
		}

		public WoodenChest( Serial serial ) : base( serial )
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

			if ( Weight == 15.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0x280B, 0x280C )]
	public class PlainWoodenChest : LockableContainer
	{
		[Constructable]
		public PlainWoodenChest() : base( 0x280B )
		{
		}

		public PlainWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x280D, 0x280E )]
	public class OrnateWoodenChest : LockableContainer
	{
		[Constructable]
		public OrnateWoodenChest() : base( 0x280D )
		{
		}

		public OrnateWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x280F, 0x2810 )]
	public class GildedWoodenChest : LockableContainer
	{
		[Constructable]
		public GildedWoodenChest() : base( 0x280F )
		{
		}

		public GildedWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x2811, 0x2812 )]
	public class WoodenFootLocker : LockableContainer
	{
		[Constructable]
		public WoodenFootLocker() : base( 0x2811 )
		{
			GumpID = 0x10C;
		}

		public WoodenFootLocker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
			
			GumpID = 0x10C;
		}
	}

	[Furniture]
	[Flipable( 0x2800, 0x2801 )]
	public class WoodenCoffin : LockableContainer
	{
		[Constructable]
		public WoodenCoffin() : base( 0x2800 )
		{
			Name = "coffin";
			GumpID = 0x41D;
		}

		public WoodenCoffin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x27E9, 0x27EA )]
	public class WoodenCasket : LockableContainer
	{
		[Constructable]
		public WoodenCasket() : base( 0x27E9 )
		{
			Name = "coffin";
			GumpID = 0x41D;
		}

		public WoodenCasket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x27E0, 0x280A )]
	public class StoneCoffin : LockableContainer
	{
		[Constructable]
		public StoneCoffin() : base( 0x27E0 )
		{
			Name = "sarcophagus";
			Weight = 100.0;
			GumpID = 0x1D;
		}

		public StoneCoffin( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x2802, 0x2803 )]
	public class StoneCasket : LockableContainer
	{
		[Constructable]
		public StoneCasket() : base( 0x2802 )
		{
			Name = "sarcophagus";
			Weight = 100.0;
			GumpID = 0x1D;
		}

		public StoneCasket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x1AFC, 0x1AFD )]
	public class RockUrn : LockableContainer
	{
		[Constructable]
		public RockUrn() : base( 0x1AFC )
		{
			Name = "urn";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public RockUrn( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x1AFE, 0x1AFF )]
	public class RockVase : LockableContainer
	{
		[Constructable]
		public RockVase() : base( 0x1AFE )
		{
			Name = "vase";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public RockVase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	public class StoneOrnateUrn : LockableContainer
	{
		[Constructable]
		public StoneOrnateUrn() : base( 0x39A2 )
		{
			Name = "ornate urn";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public StoneOrnateUrn( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	public class StoneOrnateTallVase : LockableContainer
	{
		[Constructable]
		public StoneOrnateTallVase() : base( 0x398B )
		{
			Name = "ornate vase";
			Weight = 20.0;
			GumpID = 0x13B1;
		}

		public StoneOrnateTallVase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x2813, 0x2814 )]
	public class FinishedWoodenChest : LockableContainer
	{
		[Constructable]
		public FinishedWoodenChest() : base( 0x2813 )
		{
		}

		public FinishedWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	public class HugeCrate : LockableContainer
	{
		[Constructable]
		public HugeCrate() : base( 0x4F86 )
		{
			Name = "huge crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public HugeCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class StableCrate : LockableContainer
	{
		[Constructable]
		public StableCrate() : base( 0x4F87 )
		{
			Name = "stable crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public StableCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class FletcherCrate : LockableContainer
	{
		[Constructable]
		public FletcherCrate() : base( 0x4F88 )
		{
			Name = "fletcher crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public FletcherCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class ButcherCrate : LockableContainer
	{
		[Constructable]
		public ButcherCrate() : base( 0x4F89 )
		{
			Name = "butcher crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public ButcherCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class CarpenterCrate : LockableContainer
	{
		[Constructable]
		public CarpenterCrate() : base( 0x4F8A )
		{
			Name = "carpenter crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public CarpenterCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class JewelerCrate : LockableContainer
	{
		[Constructable]
		public JewelerCrate() : base( 0x4F8B )
		{
			Name = "jeweler crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public JewelerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class WizardryCrate : LockableContainer
	{
		[Constructable]
		public WizardryCrate() : base( 0x4F8C )
		{
			Name = "wizardry crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public WizardryCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BlacksmithCrate : LockableContainer
	{
		[Constructable]
		public BlacksmithCrate() : base( 0x4F8D )
		{
			Name = "blacksmith crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BlacksmithCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class ProvisionerCrate : LockableContainer
	{
		[Constructable]
		public ProvisionerCrate() : base( 0x4F8E )
		{
			Name = "provisioner crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public ProvisionerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TailorCrate : LockableContainer
	{
		[Constructable]
		public TailorCrate() : base( 0x4F8F )
		{
			Name = "tailor crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TailorCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TinkerCrate : LockableContainer
	{
		[Constructable]
		public TinkerCrate() : base( 0x4F90 )
		{
			Name = "tinker crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TinkerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class AlchemyCrate : LockableContainer
	{
		[Constructable]
		public AlchemyCrate() : base( 0x4F91 )
		{
			Name = "alchemy crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public AlchemyCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BakerCrate : LockableContainer
	{
		[Constructable]
		public BakerCrate() : base( 0x4F92 )
		{
			Name = "baker crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BakerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TreasureCrate : LockableContainer
	{
		[Constructable]
		public TreasureCrate() : base( 0x4F93 )
		{
			Name = "treasure crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TreasureCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class MusicianCrate : LockableContainer
	{
		[Constructable]
		public MusicianCrate() : base( 0x4F94 )
		{
			Name = "musician crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public MusicianCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BeekeeperCrate : LockableContainer
	{
		[Constructable]
		public BeekeeperCrate() : base( 0x4F95 )
		{
			Name = "beekeeper crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BeekeeperCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class LibrarianCrate : LockableContainer
	{
		[Constructable]
		public LibrarianCrate() : base( 0x4F96 )
		{
			Name = "librarian crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public LibrarianCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class BowyerCrate : LockableContainer
	{
		[Constructable]
		public BowyerCrate() : base( 0x4F97 )
		{
			Name = "bowyer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public BowyerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class HealerCrate : LockableContainer
	{
		[Constructable]
		public HealerCrate() : base( 0x4F98 )
		{
			Name = "healer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public HealerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class TavernCrate : LockableContainer
	{
		[Constructable]
		public TavernCrate() : base( 0x4F99 )
		{
			Name = "tavern crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public TavernCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class NecromancerCrate : LockableContainer
	{
		[Constructable]
		public NecromancerCrate() : base( 0x4F9A )
		{
			Name = "necromancer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public NecromancerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class AdventurerCrate : LockableContainer
	{
		[Constructable]
		public AdventurerCrate() : base( 0x4F9B )
		{
			Name = "adventurer crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public AdventurerCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class SailorCrate : LockableContainer
	{
		[Constructable]
		public SailorCrate() : base( 0x4F9C )
		{
			Name = "sailor crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public SailorCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class SupplyCrate : LockableContainer
	{
		[Constructable]
		public SupplyCrate() : base( 0x4F9D )
		{
			Name = "supply crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public SupplyCrate( Serial serial ) : base( serial )
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

	[Furniture]
	public class ArmsCrate : LockableContainer
	{
		[Constructable]
		public ArmsCrate() : base( 0x4F9E )
		{
			Name = "arms crate";
			GumpID = 0x2A77;
			Weight = 10.0;
			Hue = 0xABE;
		}

		public ArmsCrate( Serial serial ) : base( serial )
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

	public class RenameContainer : ContextMenuEntry
	{
		private BaseContainer m_Item;
		private bool m_Renamable;
		private Mobile m_from;

		public RenameContainer( BaseContainer item, Mobile from ) : base( 1111680, 4 )
		{
			m_Item = item;
			m_Renamable = ((BaseContainer)m_Item).Renamable();
			m_from = from;
		}

		public static void AddTo( Mobile from, BaseContainer item, List<ContextMenuEntry> list )
		{
            BaseHouse house = BaseHouse.FindHouseAt(item);

            if (house == null)
                return;

            if (house.IsLockedDown(item) || house.IsSecure(item))
                list.Add(new RenameContainer(item, from));
		}

		public override void OnClick()
		{
			if (!m_Renamable)
				return;

			m_from.CloseGump(typeof(ContainerNameGump));
			m_from.SendGump(new ContainerNameGump(m_from, (BaseContainer)m_Item));
		}
	}
}
