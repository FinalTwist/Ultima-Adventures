using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
	public class WetClothes : Item
	{
		[Constructable]
		public WetClothes() : base( 0x1B72 )
		{
			string sAdjective = "wet";

			switch ( Utility.RandomMinMax( 0, 6 ) )
			{
				case 0: sAdjective = "soggy"; break;
				case 1:	sAdjective = "wet"; break;
				case 2:	sAdjective = "soaked"; break;
				case 3:	sAdjective = "sopping"; break;
				case 4:	sAdjective = "dripping"; break;
				case 5:	sAdjective = "waterlogged"; break;
				case 6:	sAdjective = "drenched"; break;
			}

			Name = "clothes";
			ItemID = 0x1B72;
			Weight = 2.0;

			switch ( Utility.RandomMinMax( 0, 32 ) )
			{
				case 0:		Name = "shirt";				ItemID = Utility.RandomList( 0x1517, 0x1518 );	break; // Shirt
				case 1:		Name = "fancy shirt";		ItemID = Utility.RandomList( 0x1efd, 0x1efe );	break; // FancyShirt
				case 2:		Name = "pants";				ItemID = Utility.RandomList( 0x152e, 0x152f );	break; // ShortPants
				case 3:		Name = "long pants";		ItemID = Utility.RandomList( 0x1539, 0x153a );	break; // LongPants
				case 4:		Name = "doublet";			ItemID = Utility.RandomList( 0x1f7b, 0x1f7c );	break; // Doublet
				case 5:		Name = "surcoat";			ItemID = Utility.RandomList( 0x1ffd, 0x1ffe );	break; // Surcoat
				case 6:		Name = "tunic";				ItemID = Utility.RandomList( 0x1fa1, 0x1fa2 );	break; // Tunic
				case 7:		Name = "formal shirt";		ItemID = Utility.RandomList( 0x2310, 0x230F );	break; // FormalShirt
				case 8:		Name = "jester suit";		ItemID = Utility.RandomList( 0x1f9f, 0x1fa0 );	break; // JesterSuit
				case 9:		Name = "boots";				ItemID = Utility.RandomList( 0x170b, 0x170c );	break; // Boots
				case 10:	Name = "thigh boots";		ItemID = Utility.RandomList( 0x1711, 0x1712 );	break; // ThighBoots
				case 11:	Name = "shoes";				ItemID = Utility.RandomList( 0x170f, 0x1710 );	break; // Shoes
				case 12:	Name = "sandals";			ItemID = Utility.RandomList( 0x170d, 0x170e );	break; // Sandals
				case 13:	Name = "cloak";				ItemID = Utility.RandomList( 0x1515, 0x1530 );	break; // Cloak
				case 14:	Name = "skirt";				ItemID = Utility.RandomList( 0x1516, 0x1531 );	break; // Skirt
				case 15:	Name = "kilt";				ItemID = Utility.RandomList( 0x1537, 0x1538 );	break; // Kilt
				case 16:	Name = "dress";				ItemID = Utility.RandomList( 0x230E, 0x230D );	break; // GildedDress
				case 17:	Name = "dress";				ItemID = Utility.RandomList( 0x1F00, 0x1EFF );	break; // FancyDress
				case 18:	Name = "robe";				ItemID = Utility.RandomList( 0x1F03, 0x1F04 );	break; // Robe
				case 19:	Name = "dress";				ItemID = Utility.RandomList( 0x1f01, 0x1f02 );	break; // PlainDress
				case 20:	Name = "executioner hood";	ItemID = Utility.RandomList( 0x278F, 0x27DA );	break; // ClothNinjaHood
				case 21:	Name = "floppy hat";		ItemID = 0x1713;	break; // FloppyHat
				case 22:	Name = "wide brim hat";		ItemID = 0x1714;	break; // WideBrimHat
				case 23:	Name = "cap";				ItemID = 0x1715;	break; // Cap
				case 24:	Name = "skullcap";			ItemID = 0x1544;	break; // SkullCap
				case 25:	Name = "bandana";			ItemID = 0x1540;	break; // Bandana
				case 26:	Name = "tall straw hat";	ItemID = 0x1716;	break; // TallStrawHat
				case 27:	Name = "straw hat";			ItemID = 0x1717;	break; // StrawHat
				case 28:	Name = "wizard hat";		ItemID = 0x1718;	break; // WizardsHat
				case 29:	Name = "bonnet";			ItemID = 0x1719;	break; // Bonnet
				case 30:	Name = "feathered hat";		ItemID = 0x171A;	break; // FeatheredHat
				case 31:	Name = "pirate hat";		ItemID = 0x171B;	break; // TricorneHat
				case 32:	Name = "jester hat";		ItemID = 0x171C;	break; // JesterHat
			}

			Name = sAdjective + " " + Name;
			Hue = Utility.RandomList( 0xB97, 0xB98, 0xB99, 0xB9A, 0xB88 );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Squeeze Out Water To Dry");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("You squeeze out the water.");
			from.PlaySound( 0x026 );

			if ( this.ItemID == 0x1517 || this.ItemID == 0x1518 ){ from.AddToBackpack( new Shirt( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1efd || this.ItemID == 0x1efe ){ from.AddToBackpack( new FancyShirt( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x152e || this.ItemID == 0x152f ){ from.AddToBackpack( new ShortPants( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1539 || this.ItemID == 0x153a ){ from.AddToBackpack( new LongPants( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1f7b || this.ItemID == 0x1f7c ){ from.AddToBackpack( new Doublet( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1ffd || this.ItemID == 0x1ffe ){ from.AddToBackpack( new Surcoat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1fa1 || this.ItemID == 0x1fa2 ){ from.AddToBackpack( new Tunic( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x2310 || this.ItemID == 0x230F ){ from.AddToBackpack( new FormalShirt( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1f9f || this.ItemID == 0x1fa0 ){ from.AddToBackpack( new JesterSuit( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x170b || this.ItemID == 0x170c ){ from.AddToBackpack( new Boots( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1711 || this.ItemID == 0x1712 ){ from.AddToBackpack( new ThighBoots( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x170f || this.ItemID == 0x1710 ){ from.AddToBackpack( new Shoes( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x170d || this.ItemID == 0x170e ){ from.AddToBackpack( new Sandals( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1515 || this.ItemID == 0x1530 ){ from.AddToBackpack( new Cloak( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1516 || this.ItemID == 0x1531 ){ from.AddToBackpack( new Skirt( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1537 || this.ItemID == 0x1538 ){ from.AddToBackpack( new Kilt( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x230E || this.ItemID == 0x230D ){ from.AddToBackpack( new GildedDress( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1F00 || this.ItemID == 0x1EFF ){ from.AddToBackpack( new FancyDress( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1F03 || this.ItemID == 0x1F04 ){ from.AddToBackpack( new Robe( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1f01 || this.ItemID == 0x1f02 ){ from.AddToBackpack( new PlainDress( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x278F || this.ItemID == 0x27DA ){ from.AddToBackpack( new ClothNinjaHood( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1713 ){ from.AddToBackpack( new FloppyHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1714 ){ from.AddToBackpack( new WideBrimHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1715 ){ from.AddToBackpack( new Cap( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1544 ){ from.AddToBackpack( new SkullCap( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1540 ){ from.AddToBackpack( new Bandana( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1716 ){ from.AddToBackpack( new TallStrawHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1717 ){ from.AddToBackpack( new StrawHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1718 ){ from.AddToBackpack( new WizardsHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x1719 ){ from.AddToBackpack( new Bonnet( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x171A ){ from.AddToBackpack( new FeatheredHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x171B ){ from.AddToBackpack( new TricorneHat( Utility.RandomDyedHue() ) ); }
			else if ( this.ItemID == 0x171C ){ from.AddToBackpack( new JesterHat( Utility.RandomDyedHue() ) ); }

			this.Delete();
		}

		public WetClothes(Serial serial) : base(serial)
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