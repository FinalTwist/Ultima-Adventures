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
		public CrateOfReagents() : base( 0x5095 )
		{
			Name = "crate of reagents";
			Weight = 10;
		}

		public CrateOfReagents( Serial serial ) : base( serial )
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

				if ( ItemID == 0x508E ){ from.AddToBackpack ( new Bloodmoss( CrateQty ) ); }
				else if ( ItemID == 0x508F ){ from.AddToBackpack ( new BlackPearl( CrateQty ) ); }
				else if ( ItemID == 0x5098 ){ from.AddToBackpack ( new Garlic( CrateQty ) ); }
				else if ( ItemID == 0x5099 ){ from.AddToBackpack ( new Ginseng( CrateQty ) ); }
				else if ( ItemID == 0x509A ){ from.AddToBackpack ( new MandrakeRoot( CrateQty ) ); }
				else if ( ItemID == 0x509B ){ from.AddToBackpack ( new Nightshade( CrateQty ) ); }
				else if ( ItemID == 0x509C ){ from.AddToBackpack ( new SulfurousAsh( CrateQty ) ); }
				else if ( ItemID == 0x509D ){ from.AddToBackpack ( new SpidersSilk( CrateQty ) ); }
				else if ( ItemID == 0x568A ){ from.AddToBackpack ( new SwampBerries( CrateQty ) ); }
				else if ( ItemID == 0x55E0 ){ from.AddToBackpack ( new BatWing( CrateQty ) ); }
				else if ( ItemID == 0x55E1 ){ from.AddToBackpack ( new BeetleShell( CrateQty ) ); }
				else if ( ItemID == 0x55E2 ){ from.AddToBackpack ( new Brimstone( CrateQty ) ); }
				else if ( ItemID == 0x55E3 ){ from.AddToBackpack ( new ButterflyWings( CrateQty ) ); }
				else if ( ItemID == 0x55E4 ){ from.AddToBackpack ( new DaemonBlood( CrateQty ) ); }
				else if ( ItemID == 0x55E5 ){ from.AddToBackpack ( new EyeOfToad( CrateQty ) ); }
				else if ( ItemID == 0x55E6 ){ from.AddToBackpack ( new FairyEgg( CrateQty ) ); }
				else if ( ItemID == 0x55E7 ){ from.AddToBackpack ( new GargoyleEar( CrateQty ) ); }
				else if ( ItemID == 0x55E8 ){ from.AddToBackpack ( new GraveDust( CrateQty ) ); }
				else if ( ItemID == 0x55E9 ){ from.AddToBackpack ( new MoonCrystal( CrateQty ) ); }
				else if ( ItemID == 0x55EA ){ from.AddToBackpack ( new NoxCrystal( CrateQty ) ); }
				else if ( ItemID == 0x55EB ){ from.AddToBackpack ( new SilverWidow( CrateQty ) ); }
				else if ( ItemID == 0x55EC ){ from.AddToBackpack ( new PigIron( CrateQty ) ); }
				else if ( ItemID == 0x55ED ){ from.AddToBackpack ( new PixieSkull( CrateQty ) ); }
				else if ( ItemID == 0x55EE ){ from.AddToBackpack ( new RedLotus( CrateQty ) ); }
				else if ( ItemID == 0x55EF ){ from.AddToBackpack ( new SeaSalt( CrateQty ) ); }

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