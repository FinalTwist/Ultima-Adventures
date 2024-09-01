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
	public class TradesmanAlchemist : Citizens
	{
		[Constructable]
		public TradesmanAlchemist()
		{
			CitizenType = 9;
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
				foreach ( Item pot in this.GetItemsInRange( 2 ) )
				{
					if ( pot is CauldronHit )
					{
						if ( this.FindItemOnLayer( Layer.FirstValid ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.TwoHanded ) != null ) { this.Delete(); }
						else if ( this.FindItemOnLayer( Layer.OneHanded ) != null ) { this.Delete(); }
						CauldronHit chemist = (CauldronHit)pot;
						chemist.OnDoubleClick( this );
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

		public TradesmanAlchemist( Serial serial ) : base( serial )
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
	public class CauldronHit : Item
	{
		[Constructable]
		public CauldronHit() : base( 0x227D )
		{
			Name = "cauldron";
			Movable = false;
		}

		public CauldronHit( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is Citizens )
			{
				from.Direction = from.GetDirectionTo( GetWorldLocation() );
				from.Animate( 230, 5, 1, true, false, 0 );
				ItemID = Utility.RandomList( 0x227D, 0x2284, 0x228B, 0x2292 );
				from.PlaySound( Utility.RandomList( 0x020, 0x025, 0x04E, 0x5AF ) );
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
	public class CrateOfReagents : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfReagents() : base( 0x0E3C )
		{
			Name = "crate of reagents";
			Weight = 10;
		}
		public CrateOfReagents( Serial serial ) : base( serial )
		{
		}

		public static CrateOfReagents CreateRandom()
		{
			CrateOfReagents crate = new CrateOfReagents();
			string reagent = null;
			switch (Utility.RandomMinMax(0, 24))
			{
				case 0: crate.ItemID = 0x508E; reagent = "bloodmoss"; break;
				case 1: crate.ItemID = 0x508F; reagent = "black pearl"; break;
				case 2: crate.ItemID = 0x5098; reagent = "garlic"; break;
				case 3: crate.ItemID = 0x5099; reagent = "ginseng"; break;
				case 4: crate.ItemID = 0x509A; reagent = "mandrake root"; break;
				case 5: crate.ItemID = 0x509B; reagent = "nightshade"; break;
				case 6: crate.ItemID = 0x509C; reagent = "sulfurous ash"; break;
				case 7: crate.ItemID = 0x509D; reagent = "spider silk"; break;
				case 8: reagent = "swamp berry"; break;
				case 9: reagent = "bat wing"; break;
				case 10: reagent = "beetle shell"; break;
				case 11: reagent = "brimstone"; break;
				case 12: reagent = "butterfly"; break;
				case 13: reagent = "daemon blood"; break;
				case 14: reagent = "toad eyes"; break;
				case 15: reagent = "fairy eggs"; break;
				case 16: reagent = "gargoyle ears"; break;
				case 17: reagent = "grave dust"; break;
				case 18: reagent = "moon crystals"; break;
				case 19: reagent = "nox crystal"; break;
				case 20: reagent = "silver widow"; break;
				case 21: reagent = "pig iron"; break;
				case 22: reagent = "pixie skull"; break;
				case 23: reagent = "red lotus"; break;
				case 24: reagent = "sea salt"; break;
			}

			crate.Crate_Item = reagent;
			crate.Name = "crate of " + reagent + "";

			return crate;
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendMessage("This must be in your backpack to open.");
				return;
			}
			else
			{
				from.PlaySound(0x02D);
				from.AddToBackpack(new LargeCrate());

				Item item = null;
				if (CrateItem == "bloodmoss") item = new Bloodmoss(CrateQty);
				if (CrateItem == "black pearl") item = new BlackPearl(CrateQty);
				if (CrateItem == "garlic") item = new Garlic(CrateQty);
				if (CrateItem == "ginseng") item = new Ginseng(CrateQty);
				if (CrateItem == "mandrake root") item = new MandrakeRoot(CrateQty);
				if (CrateItem == "nightshade") item = new Nightshade(CrateQty);
				if (CrateItem == "sulfurous ash") item = new SulfurousAsh(CrateQty);
				if (CrateItem == "spider silk") item = new SpidersSilk(CrateQty);
				if (CrateItem == "swamp berry") item = new SwampBerries(CrateQty);
				if (CrateItem == "bat wing") item = new BatWing(CrateQty);
				if (CrateItem == "beetle shell") item = new BeetleShell(CrateQty);
				if (CrateItem == "brimstone") item = new Brimstone(CrateQty);
				if (CrateItem == "butterfly") item = new ButterflyWings(CrateQty);
				if (CrateItem == "daemon blood") item = new DaemonBlood(CrateQty);
				if (CrateItem == "toad eyes") item = new EyeOfToad(CrateQty);
				if (CrateItem == "fairy eggs") item = new FairyEgg(CrateQty);
				if (CrateItem == "gargoyle ears") item = new GargoyleEar(CrateQty);
				if (CrateItem == "grave dust") item = new GraveDust(CrateQty);
				if (CrateItem == "moon crystals") item = new MoonCrystal(CrateQty);
				if (CrateItem == "nox crystal") item = new NoxCrystal(CrateQty);
				if (CrateItem == "silver widow") item = new SilverWidow(CrateQty);
				if (CrateItem == "pig iron") item = new PigIron(CrateQty);
				if (CrateItem == "pixie skull") item = new PixieSkull(CrateQty);
				if (CrateItem == "red lotus") item = new RedLotus(CrateQty);
				if (CrateItem == "sea salt") item = new SeaSalt(CrateQty);

				if (item == null)
				{
					from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the reagents into your backpack", from.NetState);
					return;
				}

				from.AddToBackpack(item);

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the reagents into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + "");
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

	public class CrateOfPotions : Item
	{
		public int CrateQty;
		[CommandProperty(AccessLevel.Owner)]
		public int Crate_Qty { get { return CrateQty; } set { CrateQty = value; InvalidateProperties(); } }

		public string CrateItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Crate_Item { get { return CrateItem; } set { CrateItem = value; InvalidateProperties(); } }

		[Constructable]
		public CrateOfPotions() : base( 0x55DF )
		{
			Name = "crate of potions";
			Weight = 10;
		}

		public CrateOfPotions( Serial serial ) : base( serial )
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

				if ( Name == "crate of nightsight potions" ){ Item pot = new NightSightPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser cure potions" ){ Item pot = new LesserCurePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of cure potions" ){ Item pot = new CurePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater cure potions" ){ Item pot = new GreaterCurePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of agility potions" ){ Item pot = new AgilityPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater agility potions" ){ Item pot = new GreaterAgilityPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of strength potions" ){ Item pot = new StrengthPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater strength potions" ){ Item pot = new GreaterStrengthPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser poison potions" ){ Item pot = new LesserPoisonPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of poison potions" ){ Item pot = new PoisonPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater poison potions" ){ Item pot = new GreaterPoisonPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of deadly poison potions" ){ Item pot = new DeadlyPoisonPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lethal poison potions" ){ Item pot = new LethalPoisonPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of refresh potions" ){ Item pot = new RefreshPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of total refresh potions" ){ Item pot = new TotalRefreshPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser heal potions" ){ Item pot = new LesserHealPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of heal potions" ){ Item pot = new HealPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater heal potions" ){ Item pot = new GreaterHealPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser explosion potions" ){ Item pot = new LesserExplosionPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of explosion potions" ){ Item pot = new ExplosionPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater explosion potions" ){ Item pot = new GreaterExplosionPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser invisibility potions" ){ Item pot = new LesserInvisibilityPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of invisibility potions" ){ Item pot = new InvisibilityPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater invisibility potions" ){ Item pot = new GreaterInvisibilityPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser rejuvenate potions" ){ Item pot = new LesserRejuvenatePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of rejuvenate potions" ){ Item pot = new RejuvenatePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater rejuvenate potions" ){ Item pot = new GreaterRejuvenatePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of lesser mana potions" ){ Item pot = new LesserManaPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of mana potions" ){ Item pot = new ManaPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater mana potions" ){ Item pot = new GreaterManaPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of conflagration potions" ){ Item pot = new ConflagrationPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater conflagration potions" ){ Item pot = new GreaterConflagrationPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of confusion blast potions" ){ Item pot = new ConfusionBlastPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater confusion blast potions" ){ Item pot = new GreaterConfusionBlastPotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of frostbite potions" ){ Item pot = new FrostbitePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of greater frostbite potions" ){ Item pot = new GreaterFrostbitePotion(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }
				else if ( Name == "crate of acid bottles" ){ Item pot = new BottleOfAcid(); pot.Amount = CrateQty; from.AddToBackpack ( pot ); }

				from.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "You separate the potions into your backpack", from.NetState);
				this.Delete();
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Contains " + CrateQty + " " + CrateItem + "");
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