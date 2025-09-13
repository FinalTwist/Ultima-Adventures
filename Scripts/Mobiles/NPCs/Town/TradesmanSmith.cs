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
	public class TradesmanSmith : Citizens
	{
		[Constructable]
		public TradesmanSmith()
		{
			CitizenType = 4;
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
				foreach ( Item anvil in this.GetItemsInRange( 1 ) )
				{
					if ( anvil is AnvilHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null && !(this.FindItemOnLayer( Layer.FirstValid ) is Club) ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null && !(this.FindItemOnLayer( Layer.OneHanded ) is Club) ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ){ this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
						AnvilHit smith = (AnvilHit)anvil;
						smith.OnDoubleClick( this );
						m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 2, 5 ) ));
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
			Item hammer = new Club();
			hammer.Name = "hammer";
			hammer.ItemID = 0x0FB4;
			hammer.Movable = false;
			AddItem( hammer );
		}

		public TradesmanSmith( Serial serial ) : base( serial )
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
	[FlipableAttribute( 0xFAF, 0xFB0 )]
	public class AnvilHit : Item
	{
		[Constructable]
		public AnvilHit() : base( 0xFAF )
		{
			Name = "anvil";
			Movable = false;
		}

		public AnvilHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) is BaseWeapon && from is Citizens )
			{
				BaseWeapon weapon = ( BaseWeapon )( from.FindItemOnLayer( Layer.OneHanded ) );
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				if ( this.X == from.X ){ this.ItemID = 0xFB0; } else { this.ItemID = 0xFAF; }
				weapon.PlaySwingAnimation( from );
				from.PlaySound( Utility.RandomList( 0x541, 0x2A, 0x2A, 0x2A ) );
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
	public class CrateOfMetal : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfMetal() : base( 0x5095 )
		{
			Name = "crate of ingots";
			Weight = 10;
		}

		public CrateOfMetal( Serial serial ) : base( serial )
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

				if ( CrateItem == "dull copper" ){ from.AddToBackpack ( new DullCopperIngot( CrateQty ) ); }
				else if ( CrateItem == "shadow iron" ){ from.AddToBackpack ( new ShadowIronIngot( CrateQty ) ); }
				else if ( CrateItem == "copper" ){ from.AddToBackpack ( new CopperIngot( CrateQty ) ); }
				else if ( CrateItem == "bronze" ){ from.AddToBackpack ( new BronzeIngot( CrateQty ) ); }
				else if ( CrateItem == "gold" ){ from.AddToBackpack ( new GoldIngot( CrateQty ) ); }
				else if ( CrateItem == "agapite" ){ from.AddToBackpack ( new AgapiteIngot( CrateQty ) ); }
				else if ( CrateItem == "verite" ){ from.AddToBackpack ( new VeriteIngot( CrateQty ) ); }
				else if ( CrateItem == "valorite" ){ from.AddToBackpack ( new ValoriteIngot( CrateQty ) ); }
				else if ( CrateItem == "nepturite" ){ from.AddToBackpack ( new NepturiteIngot( CrateQty ) ); }
				else if ( CrateItem == "obsidian" ){ from.AddToBackpack ( new ObsidianIngot( CrateQty ) ); }
				else if ( CrateItem == "steel" ){ from.AddToBackpack ( new SteelIngot( CrateQty ) ); }
				else if ( CrateItem == "brass" ){ from.AddToBackpack ( new BrassIngot( CrateQty ) ); }
				else if ( CrateItem == "mithril" ){ from.AddToBackpack ( new MithrilIngot( CrateQty ) ); }
				else if ( CrateItem == "xormite" ){ from.AddToBackpack ( new XormiteIngot( CrateQty ) ); }
				else if ( CrateItem == "dwarven" ){ from.AddToBackpack ( new DwarvenIngot( CrateQty ) ); }
				else { from.AddToBackpack ( new IronIngot( CrateQty ) ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the ingots into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + " Ingots");
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