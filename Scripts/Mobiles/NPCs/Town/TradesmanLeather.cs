using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class TradesmanLeather : Citizens
	{
		[Constructable]
		public TradesmanLeather()
		{
			CitizenType = 6;
			SetupCitizen();
			Blessed = true;
			CantWalk = true;
			AI = AIType.AI_Melee;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		public override void OnThink()
		{
			if ( DateTime.UtcNow >= m_NextTalk )
			{
				foreach ( Item skin in this.GetItemsInRange( 1 ) )
				{
					if ( skin is LeatherHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.Delete(); }
						LeatherHit hide = (LeatherHit)skin;
						hide.OnDoubleClick( this );
						m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 6, 12 ) ));
					}
				}
			}
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.TavernPatrons.RemoveSomeGear( this, false );
			Server.Misc.MorphingTime.CheckNecromancer( this );
			Server.Items.EssenceBase.ColorCitizen( this );
		}

		public TradesmanLeather( Serial serial ) : base( serial )
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
}

namespace Server.Items
{
	[FlipableAttribute( 0x1069, 0x107A )]
	public class LeatherHit : Item
	{
		[Constructable]
		public LeatherHit() : base( 0x13 )
		{
			Name = "stretched hide";
			Movable = false;
		}

		public LeatherHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is Citizens )
			{
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				from.Animate( 230, 5, 1, true, false, 0 ); 
				if ( this.X == from.X && ( this.ItemID == 0x1069 || this.ItemID == 0x106A ) )
				{
					this.ItemID = 0x107A;
				}
				else if ( this.Y == from.Y && ( this.ItemID == 0x107A || this.ItemID == 0x107B ) )
				{
					this.ItemID = 0x1069;
				}
				if ( ItemID == 0x1069 || ItemID == 0x106A ){ ItemID = Utility.RandomList( 0x1069, 0x106A ); }
				else { ItemID = Utility.RandomList( 0x107A, 0x107B ); }
				from.PlaySound( Utility.RandomList( 0x059, 0x057 ) );
			}
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
}

namespace Server.Items
{
	public class CrateOfLeather : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfLeather() : base( 0x5095 )
		{
			Name = "crate of leather";
			Weight = 10;
		}

		public CrateOfLeather( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to open." );
				return;
			}
			else
			{
				from.PlaySound( 0x02D );
				from.AddToBackpack ( new LargeCrate() );

				if ( CrateItem == "deep sea" ){ from.AddToBackpack ( new SpinedLeather( CrateQty ) ); }
				else if ( CrateItem == "lizard" ){ from.AddToBackpack ( new HornedLeather( CrateQty ) ); }
				else if ( CrateItem == "serpent" ){ from.AddToBackpack ( new BarbedLeather( CrateQty ) ); }
				else if ( CrateItem == "necrotic" ){ from.AddToBackpack ( new NecroticLeather( CrateQty ) ); }
				else if ( CrateItem == "volcanic" ){ from.AddToBackpack ( new VolcanicLeather( CrateQty ) ); }
				else if ( CrateItem == "frozen" ){ from.AddToBackpack ( new FrozenLeather( CrateQty ) ); }
				else if ( CrateItem == "goliath" ){ from.AddToBackpack ( new GoliathLeather( CrateQty ) ); }
				else if ( CrateItem == "draconic" ){ from.AddToBackpack ( new DraconicLeather( CrateQty ) ); }
				else if ( CrateItem == "hellish" ){ from.AddToBackpack ( new HellishLeather( CrateQty ) ); }
				else if ( CrateItem == "dinosaur" ){ from.AddToBackpack ( new DinosaurLeather( CrateQty ) ); }
				else if ( CrateItem == "alien" ){ from.AddToBackpack ( new AlienLeather( CrateQty ) ); }
				else { from.AddToBackpack ( new Leather( CrateQty ) ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the leather into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + " Leather");
			list.Add( 1049644, "Open to Remove them from the Crate");
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( CrateQty );
            writer.Write( CrateItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            CrateQty = reader.ReadInt();
            CrateItem = reader.ReadString();
		}
	}
}