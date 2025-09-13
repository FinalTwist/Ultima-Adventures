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
	public class TradesmanMiner : Citizens
	{
		[Constructable]
		public TradesmanMiner()
		{
			CitizenType = 7;
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
					if ( anvil is RockHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null && !(this.FindItemOnLayer( Layer.FirstValid ) is Pickaxe) ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null && !(this.FindItemOnLayer( Layer.OneHanded ) is Pickaxe) ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ){ this.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
						RockHit smith = (RockHit)anvil;
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
			Item hammer = new Pickaxe();
			hammer.Movable = false;
			AddItem( hammer );
		}

		public TradesmanMiner( Serial serial ) : base( serial )
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
	public class RockHit : Item
	{
		[Constructable]
		public RockHit() : base( 0x1775 )
		{
			Name = "rock";
			Visible = false;
			Movable = false;
		}

		public RockHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) is BaseWeapon && from is Citizens )
			{
				BaseWeapon weapon = ( BaseWeapon )( from.FindItemOnLayer( Layer.OneHanded ) );
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				weapon.PlaySwingAnimation( from );
				from.PlaySound( Utility.RandomList( 0x125, 0x126 ) );
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
	public class CrateOfOre : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfOre() : base( 0x5095 )
		{
			Name = "crate of ore";
			Weight = 10;
		}

		public CrateOfOre( Serial serial ) : base( serial )
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

				if ( CrateItem == "dull copper" ){ from.AddToBackpack ( new DullCopperOre( CrateQty ) ); }
				else if ( CrateItem == "shadow iron" ){ from.AddToBackpack ( new ShadowIronOre( CrateQty ) ); }
				else if ( CrateItem == "copper" ){ from.AddToBackpack ( new CopperOre( CrateQty ) ); }
				else if ( CrateItem == "bronze" ){ from.AddToBackpack ( new BronzeOre( CrateQty ) ); }
				else if ( CrateItem == "gold" ){ from.AddToBackpack ( new GoldOre( CrateQty ) ); }
				else if ( CrateItem == "agapite" ){ from.AddToBackpack ( new AgapiteOre( CrateQty ) ); }
				else if ( CrateItem == "verite" ){ from.AddToBackpack ( new VeriteOre( CrateQty ) ); }
				else if ( CrateItem == "valorite" ){ from.AddToBackpack ( new ValoriteOre( CrateQty ) ); }
				else if ( CrateItem == "nepturite" ){ from.AddToBackpack ( new NepturiteOre( CrateQty ) ); }
				else if ( CrateItem == "obsidian" ){ from.AddToBackpack ( new ObsidianOre( CrateQty ) ); }
				else if ( CrateItem == "mithril" ){ from.AddToBackpack ( new MithrilOre( CrateQty ) ); }
				else if ( CrateItem == "xormite" ){ from.AddToBackpack ( new XormiteOre( CrateQty ) ); }
				else if ( CrateItem == "dwarven" ){ from.AddToBackpack ( new DwarvenOre( CrateQty ) ); }
				else { from.AddToBackpack ( new IronOre( CrateQty ) ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the ore into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + " Ore");
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