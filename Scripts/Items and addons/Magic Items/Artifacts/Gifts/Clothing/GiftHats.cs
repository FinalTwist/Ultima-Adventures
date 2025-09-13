using System;
using Server.Engines.Craft;

namespace Server.Items
{
	public abstract class BaseGiftHat : BaseGiftClothing
	{
		public BaseGiftHat( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftHat( int itemID, int hue ) : base( itemID, Layer.Helm, hue )
		{
		}

		public BaseGiftHat( Serial serial ) : base( serial )
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

		public override int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality)quality;

			if( Quality == ClothingQuality.Exceptional )
				DistributeBonuses( (tool is BaseRunicTool ? 6 : (Core.SE ? 15 : 14)) );	//BLAME OSI. (We can't confirm it's an OSI bug yet.)

			return base.OnCraft( quality, makersMark, from, craftSystem, typeRes, tool, craftItem, resHue );
		}

	}
	[Flipable( 0x2798, 0x27E3 )]
	public class GiftKasa : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftKasa() : this( 0 )
		{
		}

		[Constructable]
		public GiftKasa( int hue ) : base( 0x2798, hue )
		{
			Weight = 3.0;
		}

        public GiftKasa(Serial serial)
            : base(serial)
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

	[Flipable( 0x278F, 0x27DA )]
	public class GiftClothNinjaHood : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftClothNinjaHood() : this( 0 )
		{
		}

		[Constructable]
		public GiftClothNinjaHood( int hue ) : base( 0x278F, hue )
		{
			Weight = 2.0;
		}

        public GiftClothNinjaHood(Serial serial)
            : base(serial)
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

	[Flipable( 0x2306, 0x2305 )]
	public class GiftFlowerGarland : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftFlowerGarland() : this( 0 )
		{
		}

		[Constructable]
		public GiftFlowerGarland( int hue ) : base( 0x2306, hue )
		{
			Weight = 1.0;
		}

        public GiftFlowerGarland(Serial serial)
            : base(serial)
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

	public class GiftFloppyHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftFloppyHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftFloppyHat( int hue ) : base( 0x1713, hue )
		{
			Weight = 1.0;
		}

        public GiftFloppyHat(Serial serial)
            : base(serial)
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

	public class GiftWideBrimHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftWideBrimHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftWideBrimHat( int hue ) : base( 0x1714, hue )
		{
			Weight = 1.0;
		}

        public GiftWideBrimHat(Serial serial)
            : base(serial)
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

	public class GiftCap : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftCap() : this( 0 )
		{
		}

		[Constructable]
		public GiftCap( int hue ) : base( 0x1715, hue )
		{
			Weight = 1.0;
		}

        public GiftCap(Serial serial)
            : base(serial)
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

	public class GiftSkullCap : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 7; } }
		public override int InitMaxHits{ get{ return 12; } }

		[Constructable]
		public GiftSkullCap() : this( 0 )
		{
		}

		[Constructable]
		public GiftSkullCap( int hue ) : base( 0x1544, hue )
		{
			Weight = 1.0;
		}

        public GiftSkullCap(Serial serial)
            : base(serial)
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

	public class GiftBandana : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftBandana() : this( 0 )
		{
		}

		[Constructable]
		public GiftBandana( int hue ) : base( 0x1540, hue )
		{
			Weight = 1.0;
		}

        public GiftBandana(Serial serial)
            : base(serial)
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

	public class GiftBearMask : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftBearMask() : this( 0 )
		{
		}

		[Constructable]
		public GiftBearMask( int hue ) : base( 0x1545, hue )
		{
			Name = "bear mask";
			Weight = 5.0;
		}

        public GiftBearMask(Serial serial)
            : base(serial)
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

	public class GiftWolfMask : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftWolfMask() : this( 0 )
		{
		}

		[Constructable]
		public GiftWolfMask( int hue ) : base( 0x2B6D, hue )
		{
			Name = "wolf mask";
			Weight = 5.0;
		}

        public GiftWolfMask(Serial serial)
            : base(serial)
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

	public class GiftDeerMask : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftDeerMask() : this( 0 )
		{
		}

		[Constructable]
		public GiftDeerMask( int hue ) : base( 0x1547, hue )
		{
			Name = "deer mask";
			Weight = 4.0;
		}

        public GiftDeerMask(Serial serial)
            : base(serial)
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

	public class GiftHornedTribalMask : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftHornedTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public GiftHornedTribalMask( int hue ) : base( 0x1549, hue )
		{
			Weight = 2.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

        public GiftHornedTribalMask(Serial serial)
            : base(serial)
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

	public class GiftTribalMask : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public GiftTribalMask( int hue ) : base( 0x154B, hue )
		{
			Weight = 2.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

        public GiftTribalMask(Serial serial)
            : base(serial)
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

	public class GiftTallStrawHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftTallStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftTallStrawHat( int hue ) : base( 0x1716, hue )
		{
			Weight = 1.0;
		}

        public GiftTallStrawHat(Serial serial)
            : base(serial)
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

	public class GiftStrawHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftStrawHat( int hue ) : base( 0x1717, hue )
		{
			Weight = 1.0;
		}

        public GiftStrawHat(Serial serial)
            : base(serial)
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

	public class GiftWizardsHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftWizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftWizardsHat( int hue ) : base( 0x1718, hue )
		{
			Weight = 1.0;
		}

        public GiftWizardsHat(Serial serial)
            : base(serial)
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

	public class GiftWitchHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftWitchHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftWitchHat( int hue ) : base( 0x2FC3, hue )
		{
			Name = "witch hat";
			Weight = 1.0;
		}

        public GiftWitchHat(Serial serial)
            : base(serial)
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

	public class GiftBonnet : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftBonnet() : this( 0 )
		{
		}

		[Constructable]
		public GiftBonnet( int hue ) : base( 0x1719, hue )
		{
			Weight = 1.0;
		}

        public GiftBonnet(Serial serial)
            : base(serial)
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

	public class GiftFeatheredHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftFeatheredHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftFeatheredHat( int hue ) : base( 0x171A, hue )
		{
			Weight = 1.0;
		}

        public GiftFeatheredHat(Serial serial)
            : base(serial)
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

	public class GiftTricorneHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftTricorneHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftTricorneHat( int hue ) : base( 0x171B, hue )
		{
			Weight = 1.0;
		}

        public GiftTricorneHat(Serial serial)
            : base(serial)
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

	public class GiftPirateHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftPirateHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftPirateHat( int hue ) : base( 0x2FBC, hue )
		{
			Name = "pirate hat";
			Weight = 1.0;
		}

        public GiftPirateHat(Serial serial)
            : base(serial)
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

	public class GiftJesterHat : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftJesterHat() : this( 0 )
		{
		}

		[Constructable]
		public GiftJesterHat( int hue ) : base( 0x171C, hue )
		{
			Weight = 1.0;
		}

        public GiftJesterHat(Serial serial)
            : base(serial)
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

	public class GiftCowl : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftCowl() : this( 0 )
		{
		}

		[Constructable]
		public GiftCowl( int hue ) : base( 0x3176, hue )
		{
			Name = "cowl";
			Weight = 1.0;
		}

        public GiftCowl(Serial serial)
            : base(serial)
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

	public class GiftHood : BaseGiftHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 30; } }

		[Constructable]
		public GiftHood() : this( 0 )
		{
		}

		[Constructable]
		public GiftHood( int hue ) : base( 0x2B71, hue )
		{
			Name = "hood";
			Weight = 1.0;
		}

        public GiftHood(Serial serial)
            : base(serial)
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