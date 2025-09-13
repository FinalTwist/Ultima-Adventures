using System;
using Server.Misc;

namespace Server.Items
{
    class Tinkering
    {
		public static void TinkerMagic( Item item, int magic, int min, int max )
		{
			if ( magic >= Utility.RandomMinMax( 1, 100 ) ){ BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, false, 0, 1, min, max ); }
		}
	}

	public class AgapiteAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); } }

		[Constructable]
		public AgapiteAmulet()
		{
			if ( Name != "agapite amulet" ){ Name = "agapite amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AgapiteAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "agapite amulet";
		}
	}

	public class AgapiteBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); } }

		[Constructable]
		public AgapiteBracelet()
		{
			if ( Name != "agapite bracelet" ){ Name = "agapite bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AgapiteBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "agapite bracelet";
		}
	}

	public class AgapiteRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); } }

		[Constructable]
		public AgapiteRing()
		{
			if ( Name != "agapite ring" ){ Name = "agapite ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AgapiteRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "agapite ring";
		}
	}

	public class AgapiteEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); } }

		[Constructable]
		public AgapiteEarrings()
		{
			if ( Name != "agapite earrings" ){ Name = "agapite earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AgapiteEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "agapite earrings";
		}
	}

	public class AmberAmulet : GoldNecklace
	{
		public override int Hue{ get { return 1359; } }

		[Constructable]
		public AmberAmulet()
		{
			if ( Name != "amber amulet" ){ Name = "amber amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmberAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amber amulet";
		}
	}

	public class AmberBracelet : GoldBracelet
	{
		public override int Hue{ get { return 1359; } }

		[Constructable]
		public AmberBracelet()
		{
			if ( Name != "amber bracelet" ){ Name = "amber bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmberBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amber bracelet";
		}
	}

	public class AmberRing : GoldRing
	{
		public override int Hue{ get { return 1359; } }

		[Constructable]
		public AmberRing()
		{
			if ( Name != "amber ring" ){ Name = "amber ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmberRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amber ring";
		}
	}

	public class AmberEarrings : GoldEarrings
	{
		public override int Hue{ get { return 1359; } }

		[Constructable]
		public AmberEarrings()
		{
			if ( Name != "amber earrings" ){ Name = "amber earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmberEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amber earrings";
		}
	}

	public class AmethystAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 ); } }

		[Constructable]
		public AmethystAmulet()
		{
			if ( Name != "amethyst amulet" ){ Name = "amethyst amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmethystAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amethyst amulet";
		}
	}

	public class AmethystBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 ); } }

		[Constructable]
		public AmethystBracelet()
		{
			if ( Name != "amethyst bracelet" ){ Name = "amethyst bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmethystBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amethyst bracelet";
		}
	}

	public class AmethystRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 ); } }

		[Constructable]
		public AmethystRing()
		{
			if ( Name != "amethyst ring" ){ Name = "amethyst ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmethystRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amethyst ring";
		}
	}

	public class AmethystEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 ); } }

		[Constructable]
		public AmethystEarrings()
		{
			if ( Name != "amethyst earrings" ){ Name = "amethyst earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public AmethystEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "amethyst earrings";
		}
	}

	public class BrassAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); } }

		[Constructable]
		public BrassAmulet()
		{
			if ( Name != "brass amulet" ){ Name = "brass amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public BrassAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "brass amulet";
		}
	}

	public class BrassBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); } }

		[Constructable]
		public BrassBracelet()
		{
			if ( Name != "brass bracelet" ){ Name = "brass bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public BrassBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "brass bracelet";
		}
	}

	public class BrassRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); } }

		[Constructable]
		public BrassRing()
		{
			if ( Name != "brass ring" ){ Name = "brass ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public BrassRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "brass ring";
		}
	}

	public class BrassEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); } }

		[Constructable]
		public BrassEarrings()
		{
			if ( Name != "brass earrings" ){ Name = "brass earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public BrassEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "brass earrings";
		}
	}

	public class BronzeAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); } }

		[Constructable]
		public BronzeAmulet()
		{
			if ( Name != "bronze amulet" ){ Name = "bronze amulet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public BronzeAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "bronze amulet";
		}
	}

	public class BronzeBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); } }

		[Constructable]
		public BronzeBracelet()
		{
			if ( Name != "bronze bracelet" ){ Name = "bronze bracelet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public BronzeBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "bronze bracelet";
		}
	}

	public class BronzeRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); } }

		[Constructable]
		public BronzeRing()
		{
			if ( Name != "bronze ring" ){ Name = "bronze ring"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public BronzeRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "bronze ring";
		}
	}

	public class BronzeEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); } }

		[Constructable]
		public BronzeEarrings()
		{
			if ( Name != "bronze earrings" ){ Name = "bronze earrings"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public BronzeEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "bronze earrings";
		}
	}

	public class CaddelliteAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "caddellite", "classic", 0 ); } }

		[Constructable]
		public CaddelliteAmulet()
		{
			if ( Name != "caddellite amulet" ){ Name = "caddellite amulet"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public CaddelliteAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "caddellite amulet";
		}
	}

	public class CaddelliteBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "caddellite", "classic", 0 ); } }

		[Constructable]
		public CaddelliteBracelet()
		{
			if ( Name != "caddellite bracelet" ){ Name = "caddellite bracelet"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public CaddelliteBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "caddellite bracelet";
		}
	}

	public class CaddelliteRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "caddellite", "classic", 0 ); } }

		[Constructable]
		public CaddelliteRing()
		{
			if ( Name != "caddellite ring" ){ Name = "caddellite ring"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public CaddelliteRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "caddellite ring";
		}
	}

	public class CaddelliteEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "caddellite", "classic", 0 ); } }

		[Constructable]
		public CaddelliteEarrings()
		{
			if ( Name != "caddellite earrings" ){ Name = "caddellite earrings"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public CaddelliteEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "caddellite earrings";
		}
	}

	public class CopperAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); } }

		[Constructable]
		public CopperAmulet()
		{
			if ( Name != "copper amulet" ){ Name = "copper amulet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public CopperAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "copper amulet";
		}
	}

	public class CopperBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); } }

		[Constructable]
		public CopperBracelet()
		{
			if ( Name != "copper bracelet" ){ Name = "copper bracelet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public CopperBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "copper bracelet";
		}
	}

	public class CopperRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); } }

		[Constructable]
		public CopperRing()
		{
			if ( Name != "copper ring" ){ Name = "copper ring"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public CopperRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "copper ring";
		}
	}

	public class CopperEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); } }

		[Constructable]
		public CopperEarrings()
		{
			if ( Name != "copper earrings" ){ Name = "copper earrings"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public CopperEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "copper earrings";
		}
	}

	public class DiamondAmulet : GoldNecklace
	{
		public override int Hue{ get { return 2657; } }

		[Constructable]
		public DiamondAmulet()
		{
			if ( Name != "diamond amulet" ){ Name = "diamond amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 3, 6 ); }
		}

		public DiamondAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "diamond amulet";
		}
	}

	public class DiamondBracelet : GoldBracelet
	{
		public override int Hue{ get { return 2657; } }

		[Constructable]
		public DiamondBracelet()
		{
			if ( Name != "diamond bracelet" ){ Name = "diamond bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 3, 6 ); }
		}

		public DiamondBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "diamond bracelet";
		}
	}

	public class DiamondRing : GoldRing
	{
		public override int Hue{ get { return 2657; } }

		[Constructable]
		public DiamondRing()
		{
			if ( Name != "diamond ring" ){ Name = "diamond ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 3, 6 ); }
		}

		public DiamondRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "diamond ring";
		}
	}

	public class DiamondEarrings : GoldEarrings
	{
		public override int Hue{ get { return 2657; } }

		[Constructable]
		public DiamondEarrings()
		{
			if ( Name != "diamond earrings" ){ Name = "diamond earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 3, 6 ); }
		}

		public DiamondEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "diamond earrings";
		}
	}

	public class DullCopperAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); } }

		[Constructable]
		public DullCopperAmulet()
		{
			if ( Name != "dull copper amulet" ){ Name = "dull copper amulet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public DullCopperAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dull copper amulet";
		}
	}

	public class DullCopperBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); } }

		[Constructable]
		public DullCopperBracelet()
		{
			if ( Name != "dull copper bracelet" ){ Name = "dull copper bracelet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public DullCopperBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dull copper bracelet";
		}
	}

	public class DullCopperRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); } }

		[Constructable]
		public DullCopperRing()
		{
			if ( Name != "dull copper ring" ){ Name = "dull copper ring"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public DullCopperRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dull copper ring";
		}
	}

	public class DullCopperEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); } }

		[Constructable]
		public DullCopperEarrings()
		{
			if ( Name != "dull copper earrings" ){ Name = "dull copper earrings"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public DullCopperEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dull copper earrings";
		}
	}

	public class EmeraldAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); } }

		[Constructable]
		public EmeraldAmulet()
		{
			if ( Name != "emerald amulet" ){ Name = "emerald amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 2, 4 ); }
		}

		public EmeraldAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "emerald amulet";
		}
	}

	public class EmeraldBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); } }

		[Constructable]
		public EmeraldBracelet()
		{
			if ( Name != "emerald bracelet" ){ Name = "emerald bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 2, 4 ); }
		}

		public EmeraldBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "emerald bracelet";
		}
	}

	public class EmeraldRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); } }

		[Constructable]
		public EmeraldRing()
		{
			if ( Name != "emerald ring" ){ Name = "emerald ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 2, 4 ); }
		}

		public EmeraldRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "emerald ring";
		}
	}

	public class EmeraldEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); } }

		[Constructable]
		public EmeraldEarrings()
		{
			if ( Name != "emerald earrings" ){ Name = "emerald earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 2, 4 ); }
		}

		public EmeraldEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "emerald earrings";
		}
	}

	public class GarnetAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "garnet", "classic", 0 ); } }

		[Constructable]
		public GarnetAmulet()
		{
			if ( Name != "garnet amulet" ){ Name = "garnet amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public GarnetAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "garnet amulet";
		}
	}

	public class GarnetBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "garnet", "classic", 0 ); } }

		[Constructable]
		public GarnetBracelet()
		{
			if ( Name != "garnet bracelet" ){ Name = "garnet bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public GarnetBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "garnet bracelet";
		}
	}

	public class GarnetRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "garnet", "classic", 0 ); } }

		[Constructable]
		public GarnetRing()
		{
			if ( Name != "garnet ring" ){ Name = "garnet ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public GarnetRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "garnet ring";
		}
	}

	public class GarnetEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "garnet", "classic", 0 ); } }

		[Constructable]
		public GarnetEarrings()
		{
			if ( Name != "garnet earrings" ){ Name = "garnet earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public GarnetEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "garnet earrings";
		}
	}

	public class GoldenAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "gold", "", 0 ); } }

		[Constructable]
		public GoldenAmulet()
		{
			if ( Name != "gold amulet" ){ Name = "gold amulet"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public GoldenAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "gold amulet";
		}
	}

	public class GoldenBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "gold", "", 0 ); } }

		[Constructable]
		public GoldenBracelet()
		{
			if ( Name != "gold bracelet" ){ Name = "gold bracelet"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public GoldenBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "gold bracelet";
		}
	}

	public class GoldenRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "gold", "", 0 ); } }

		[Constructable]
		public GoldenRing()
		{
			if ( Name != "gold ring" ){ Name = "gold ring"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public GoldenRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "gold ring";
		}
	}

	public class GoldenEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "gold", "", 0 ); } }

		[Constructable]
		public GoldenEarrings()
		{
			if ( Name != "gold earrings" ){ Name = "gold earrings"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public GoldenEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "gold earrings";
		}
	}

	public class JadeAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "jade", "classic", 0 ); } }

		[Constructable]
		public JadeAmulet()
		{
			if ( Name != "jade amulet" ){ Name = "jade amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public JadeAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "jade amulet";
		}
	}

	public class JadeBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "jade", "classic", 0 ); } }

		[Constructable]
		public JadeBracelet()
		{
			if ( Name != "jade bracelet" ){ Name = "jade bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public JadeBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "jade bracelet";
		}
	}

	public class JadeRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "jade", "classic", 0 ); } }

		[Constructable]
		public JadeRing()
		{
			if ( Name != "jade ring" ){ Name = "jade ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public JadeRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "jade ring";
		}
	}

	public class JadeEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "jade", "classic", 0 ); } }

		[Constructable]
		public JadeEarrings()
		{
			if ( Name != "jade earrings" ){ Name = "jade earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public JadeEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "jade earrings";
		}
	}

	public class MithrilAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "mithril", "", 0 ); } }

		[Constructable]
		public MithrilAmulet()
		{
			if ( Name != "mithril amulet" ){ Name = "mithril amulet"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public MithrilAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "mithril amulet";
		}
	}

	public class MithrilBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "mithril", "", 0 ); } }

		[Constructable]
		public MithrilBracelet()
		{
			if ( Name != "mithril bracelet" ){ Name = "mithril bracelet"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public MithrilBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "mithril bracelet";
		}
	}

	public class MithrilRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "mithril", "", 0 ); } }

		[Constructable]
		public MithrilRing()
		{
			if ( Name != "mithril ring" ){ Name = "mithril ring"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public MithrilRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "mithril ring";
		}
	}

	public class MithrilEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "mithril", "", 0 ); } }

		[Constructable]
		public MithrilEarrings()
		{
			if ( Name != "mithril earrings" ){ Name = "mithril earrings"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public MithrilEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "mithril earrings";
		}
	}

	public class DwarvenAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); } }

		[Constructable]
		public DwarvenAmulet()
		{
			if ( Name != "dwarven amulet" ){ Name = "dwarven amulet"; Server.Items.Tinkering.TinkerMagic( this, 90, 8, 18 ); }
		}

		public DwarvenAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dwarven amulet";
		}
	}

	public class DwarvenBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); } }

		[Constructable]
		public DwarvenBracelet()
		{
			if ( Name != "dwarven bracelet" ){ Name = "dwarven bracelet"; Server.Items.Tinkering.TinkerMagic( this, 90, 8, 18 ); }
		}

		public DwarvenBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dwarven bracelet";
		}
	}

	public class DwarvenRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); } }

		[Constructable]
		public DwarvenRing()
		{
			if ( Name != "dwarven ring" ){ Name = "dwarven ring"; Server.Items.Tinkering.TinkerMagic( this, 90, 8, 18 ); }
		}

		public DwarvenRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dwarven ring";
		}
	}

	public class DwarvenEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); } }

		[Constructable]
		public DwarvenEarrings()
		{
			if ( Name != "dwarven earrings" ){ Name = "dwarven earrings"; Server.Items.Tinkering.TinkerMagic( this, 90, 8, 18 ); }
		}

		public DwarvenEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "dwarven earrings";
		}
	}

	public class NepturiteAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); } }

		[Constructable]
		public NepturiteAmulet()
		{
			if ( Name != "nepturite amulet" ){ Name = "nepturite amulet"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public NepturiteAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "nepturite amulet";
		}
	}

	public class NepturiteBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); } }

		[Constructable]
		public NepturiteBracelet()
		{
			if ( Name != "nepturite bracelet" ){ Name = "nepturite bracelet"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public NepturiteBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "nepturite bracelet";
		}
	}

	public class NepturiteRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); } }

		[Constructable]
		public NepturiteRing()
		{
			if ( Name != "nepturite ring" ){ Name = "nepturite ring"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public NepturiteRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "nepturite ring";
		}
	}

	public class NepturiteEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); } }

		[Constructable]
		public NepturiteEarrings()
		{
			if ( Name != "nepturite earrings" ){ Name = "nepturite earrings"; Server.Items.Tinkering.TinkerMagic( this, 60, 4, 9 ); }
		}

		public NepturiteEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "nepturite earrings";
		}
	}

	public class ObsidianAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); } }

		[Constructable]
		public ObsidianAmulet()
		{
			if ( Name != "obsidian amulet" ){ Name = "obsidian amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public ObsidianAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "obsidian amulet";
		}
	}

	public class ObsidianBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); } }

		[Constructable]
		public ObsidianBracelet()
		{
			if ( Name != "obsidian bracelet" ){ Name = "obsidian bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public ObsidianBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "obsidian bracelet";
		}
	}

	public class ObsidianRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); } }

		[Constructable]
		public ObsidianRing()
		{
			if ( Name != "obsidian ring" ){ Name = "obsidian ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public ObsidianRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "obsidian ring";
		}
	}

	public class ObsidianEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); } }

		[Constructable]
		public ObsidianEarrings()
		{
			if ( Name != "obsidian earrings" ){ Name = "obsidian earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public ObsidianEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "obsidian earrings";
		}
	}

	public class OnyxAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); } }

		[Constructable]
		public OnyxAmulet()
		{
			if ( Name != "onyx amulet" ){ Name = "onyx amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public OnyxAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "onyx amulet";
		}
	}

	public class OnyxBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); } }

		[Constructable]
		public OnyxBracelet()
		{
			if ( Name != "onyx bracelet" ){ Name = "onyx bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public OnyxBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "onyx bracelet";
		}
	}

	public class OnyxRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); } }

		[Constructable]
		public OnyxRing()
		{
			if ( Name != "onyx ring" ){ Name = "onyx ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public OnyxRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "onyx ring";
		}
	}

	public class OnyxEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); } }

		[Constructable]
		public OnyxEarrings()
		{
			if ( Name != "onyx earrings" ){ Name = "onyx earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public OnyxEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "onyx earrings";
		}
	}

	public class PearlAmulet : GoldNecklace
	{
		public override int Hue{ get { return 1150; } }

		[Constructable]
		public PearlAmulet()
		{
			if ( Name != "pearl amulet" ){ Name = "pearl amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public PearlAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "pearl amulet";
		}
	}

	public class PearlBracelet : GoldBracelet
	{
		public override int Hue{ get { return 1150; } }

		[Constructable]
		public PearlBracelet()
		{
			if ( Name != "pearl bracelet" ){ Name = "pearl bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public PearlBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "pearl bracelet";
		}
	}

	public class PearlRing : GoldRing
	{
		public override int Hue{ get { return 1150; } }

		[Constructable]
		public PearlRing()
		{
			if ( Name != "pearl ring" ){ Name = "pearl ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public PearlRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "pearl ring";
		}
	}

	public class PearlEarrings : GoldEarrings
	{
		public override int Hue{ get { return 1150; } }

		[Constructable]
		public PearlEarrings()
		{
			if ( Name != "pearl earrings" ){ Name = "pearl earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public PearlEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "pearl earrings";
		}
	}

	public class QuartzAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "quartz", "classic", 0 ); } }

		[Constructable]
		public QuartzAmulet()
		{
			if ( Name != "quartz amulet" ){ Name = "quartz amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public QuartzAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "quartz amulet";
		}
	}

	public class QuartzBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "quartz", "classic", 0 ); } }

		[Constructable]
		public QuartzBracelet()
		{
			if ( Name != "quartz bracelet" ){ Name = "quartz bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public QuartzBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "quartz bracelet";
		}
	}

	public class QuartzRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "quartz", "classic", 0 ); } }

		[Constructable]
		public QuartzRing()
		{
			if ( Name != "quartz ring" ){ Name = "quartz ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public QuartzRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "quartz ring";
		}
	}

	public class QuartzEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "quartz", "classic", 0 ); } }

		[Constructable]
		public QuartzEarrings()
		{
			if ( Name != "quartz earrings" ){ Name = "quartz earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public QuartzEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "quartz earrings";
		}
	}

	public class RubyAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); } }

		[Constructable]
		public RubyAmulet()
		{
			if ( Name != "ruby amulet" ){ Name = "ruby amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public RubyAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "ruby amulet";
		}
	}

	public class RubyBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); } }

		[Constructable]
		public RubyBracelet()
		{
			if ( Name != "ruby bracelet" ){ Name = "ruby bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public RubyBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "ruby bracelet";
		}
	}

	public class RubyRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); } }

		[Constructable]
		public RubyRing()
		{
			if ( Name != "ruby ring" ){ Name = "ruby ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public RubyRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "ruby ring";
		}
	}

	public class RubyEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); } }

		[Constructable]
		public RubyEarrings()
		{
			if ( Name != "ruby earrings" ){ Name = "ruby earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public RubyEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "ruby earrings";
		}
	}

	public class SapphireAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); } }

		[Constructable]
		public SapphireAmulet()
		{
			if ( Name != "sapphire amulet" ){ Name = "sapphire amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public SapphireAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "sapphire amulet";
		}
	}

	public class SapphireBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); } }

		[Constructable]
		public SapphireBracelet()
		{
			if ( Name != "sapphire bracelet" ){ Name = "sapphire bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public SapphireBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "sapphire bracelet";
		}
	}

	public class SapphireRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); } }

		[Constructable]
		public SapphireRing()
		{
			if ( Name != "sapphire ring" ){ Name = "sapphire ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public SapphireRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "sapphire ring";
		}
	}

	public class SapphireEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); } }

		[Constructable]
		public SapphireEarrings()
		{
			if ( Name != "sapphire earrings" ){ Name = "sapphire earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public SapphireEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "sapphire earrings";
		}
	}

	public class ShadowIronAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); } }

		[Constructable]
		public ShadowIronAmulet()
		{
			if ( Name != "shadow iron amulet" ){ Name = "shadow iron amulet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public ShadowIronAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "shadow iron amulet";
		}
	}

	public class ShadowIronBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); } }

		[Constructable]
		public ShadowIronBracelet()
		{
			if ( Name != "shadow iron bracelet" ){ Name = "shadow iron bracelet"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public ShadowIronBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "shadow iron bracelet";
		}
	}

	public class ShadowIronRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); } }

		[Constructable]
		public ShadowIronRing()
		{
			if ( Name != "shadow iron ring" ){ Name = "shadow iron ring"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public ShadowIronRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "shadow iron ring";
		}
	}

	public class ShadowIronEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); } }

		[Constructable]
		public ShadowIronEarrings()
		{
			if ( Name != "shadow iron earrings" ){ Name = "shadow iron earrings"; Server.Items.Tinkering.TinkerMagic( this, 35, 2, 6 ); }
		}

		public ShadowIronEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "shadow iron earrings";
		}
	}

	public class SilveryAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); } }

		[Constructable]
		public SilveryAmulet()
		{
			if ( Name != "silver amulet" ){ Name = "silver amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SilveryAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "silver amulet";
		}
	}

	public class SilveryBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); } }

		[Constructable]
		public SilveryBracelet()
		{
			if ( Name != "silver bracelet" ){ Name = "silver bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SilveryBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "silver bracelet";
		}
	}

	public class SilveryRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); } }

		[Constructable]
		public SilveryRing()
		{
			if ( Name != "silver ring" ){ Name = "silver ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SilveryRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "silver ring";
		}
	}

	public class SilveryEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); } }

		[Constructable]
		public SilveryEarrings()
		{
			if ( Name != "silver earrings" ){ Name = "silver earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SilveryEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "silver earrings";
		}
	}

	public class SpinelAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "spinel", "classic", 0 ); } }

		[Constructable]
		public SpinelAmulet()
		{
			if ( Name != "spinel amulet" ){ Name = "spinel amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SpinelAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "spinel amulet";
		}
	}

	public class SpinelBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "spinel", "classic", 0 ); } }

		[Constructable]
		public SpinelBracelet()
		{
			if ( Name != "spinel bracelet" ){ Name = "spinel bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SpinelBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "spinel bracelet";
		}
	}

	public class SpinelRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "spinel", "classic", 0 ); } }

		[Constructable]
		public SpinelRing()
		{
			if ( Name != "spinel ring" ){ Name = "spinel ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SpinelRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "spinel ring";
		}
	}

	public class SpinelEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "spinel", "classic", 0 ); } }

		[Constructable]
		public SpinelEarrings()
		{
			if ( Name != "spinel earrings" ){ Name = "spinel earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SpinelEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "spinel earrings";
		}
	}

	public class StarRubyAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "star ruby", "classic", 0 ); } }

		[Constructable]
		public StarRubyAmulet()
		{
			if ( Name != "star ruby amulet" ){ Name = "star ruby amulet"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public StarRubyAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star ruby amulet";
		}
	}

	public class StarRubyBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "star ruby", "classic", 0 ); } }

		[Constructable]
		public StarRubyBracelet()
		{
			if ( Name != "star ruby bracelet" ){ Name = "star ruby bracelet"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public StarRubyBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star ruby bracelet";
		}
	}

	public class StarRubyRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "star ruby", "classic", 0 ); } }

		[Constructable]
		public StarRubyRing()
		{
			if ( Name != "star ruby ring" ){ Name = "star ruby ring"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public StarRubyRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star ruby ring";
		}
	}

	public class StarRubyEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "star ruby", "classic", 0 ); } }

		[Constructable]
		public StarRubyEarrings()
		{
			if ( Name != "star ruby earrings" ){ Name = "star ruby earrings"; Server.Items.Tinkering.TinkerMagic( this, 35, 3, 7 ); }
		}

		public StarRubyEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star ruby earrings";
		}
	}

	public class StarSapphireAmulet : GoldNecklace
	{
		public override int Hue{ get { return 1266; } }

		[Constructable]
		public StarSapphireAmulet()
		{
			if ( Name != "star sapphire amulet" ){ Name = "star sapphire amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public StarSapphireAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star sapphire amulet";
		}
	}

	public class StarSapphireBracelet : GoldBracelet
	{
		public override int Hue{ get { return 1266; } }

		[Constructable]
		public StarSapphireBracelet()
		{
			if ( Name != "star sapphire bracelet" ){ Name = "star sapphire bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public StarSapphireBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star sapphire bracelet";
		}
	}

	public class StarSapphireRing : GoldRing
	{
		public override int Hue{ get { return 1266; } }

		[Constructable]
		public StarSapphireRing()
		{
			if ( Name != "star sapphire ring" ){ Name = "star sapphire ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public StarSapphireRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star sapphire ring";
		}
	}

	public class StarSapphireEarrings : GoldEarrings
	{
		public override int Hue{ get { return 1266; } }

		[Constructable]
		public StarSapphireEarrings()
		{
			if ( Name != "star sapphire earrings" ){ Name = "star sapphire earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public StarSapphireEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "star sapphire earrings";
		}
	}

	public class SteelAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); } }

		[Constructable]
		public SteelAmulet()
		{
			if ( Name != "steel amulet" ){ Name = "steel amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SteelAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "steel amulet";
		}
	}

	public class SteelBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); } }

		[Constructable]
		public SteelBracelet()
		{
			if ( Name != "steel bracelet" ){ Name = "steel bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SteelBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "steel bracelet";
		}
	}

	public class SteelRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); } }

		[Constructable]
		public SteelRing()
		{
			if ( Name != "steel ring" ){ Name = "steel ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SteelRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "steel ring";
		}
	}

	public class SteelEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); } }

		[Constructable]
		public SteelEarrings()
		{
			if ( Name != "steel earrings" ){ Name = "steel earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public SteelEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "steel earrings";
		}
	}

	public class TopazAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "topaz", "classic", 0 ); } }

		[Constructable]
		public TopazAmulet()
		{
			if ( Name != "topaz amulet" ){ Name = "topaz amulet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public TopazAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "topaz amulet";
		}
	}

	public class TopazBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "topaz", "classic", 0 ); } }

		[Constructable]
		public TopazBracelet()
		{
			if ( Name != "topaz bracelet" ){ Name = "topaz bracelet"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public TopazBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "topaz bracelet";
		}
	}

	public class TopazRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "topaz", "classic", 0 ); } }

		[Constructable]
		public TopazRing()
		{
			if ( Name != "topaz ring" ){ Name = "topaz ring"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public TopazRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "topaz ring";
		}
	}

	public class TopazEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "topaz", "classic", 0 ); } }

		[Constructable]
		public TopazEarrings()
		{
			if ( Name != "topaz earrings" ){ Name = "topaz earrings"; Server.Items.Tinkering.TinkerMagic( this, 50, 3, 9 ); }
		}

		public TopazEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "topaz earrings";
		}
	}

	public class TourmalineAmulet : GoldNecklace
	{
		public override int Hue{ get { return 1360; } }

		[Constructable]
		public TourmalineAmulet()
		{
			if ( Name != "tourmaline amulet" ){ Name = "tourmaline amulet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public TourmalineAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "tourmaline amulet";
		}
	}

	public class TourmalineBracelet : GoldBracelet
	{
		public override int Hue{ get { return 1360; } }

		[Constructable]
		public TourmalineBracelet()
		{
			if ( Name != "tourmaline bracelet" ){ Name = "tourmaline bracelet"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public TourmalineBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "tourmaline bracelet";
		}
	}

	public class TourmalineRing : GoldRing
	{
		public override int Hue{ get { return 1360; } }

		[Constructable]
		public TourmalineRing()
		{
			if ( Name != "tourmaline ring" ){ Name = "tourmaline ring"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public TourmalineRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "tourmaline ring";
		}
	}

	public class TourmalineEarrings : GoldEarrings
	{
		public override int Hue{ get { return 1360; } }

		[Constructable]
		public TourmalineEarrings()
		{
			if ( Name != "tourmaline earrings" ){ Name = "tourmaline earrings"; Server.Items.Tinkering.TinkerMagic( this, 25, 1, 3 ); }
		}

		public TourmalineEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "tourmaline earrings";
		}
	}

	public class ValoriteAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); } }

		[Constructable]
		public ValoriteAmulet()
		{
			if ( Name != "valorite amulet" ){ Name = "valorite amulet"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public ValoriteAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "valorite amulet";
		}
	}

	public class ValoriteBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); } }

		[Constructable]
		public ValoriteBracelet()
		{
			if ( Name != "valorite bracelet" ){ Name = "valorite bracelet"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public ValoriteBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "valorite bracelet";
		}
	}

	public class ValoriteRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); } }

		[Constructable]
		public ValoriteRing()
		{
			if ( Name != "valorite ring" ){ Name = "valorite ring"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public ValoriteRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "valorite ring";
		}
	}

	public class ValoriteEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); } }

		[Constructable]
		public ValoriteEarrings()
		{
			if ( Name != "valorite earrings" ){ Name = "valorite earrings"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public ValoriteEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "valorite earrings";
		}
	}

	public class VeriteAmulet : GoldNecklace
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); } }

		[Constructable]
		public VeriteAmulet()
		{
			if ( Name != "verite amulet" ){ Name = "verite amulet"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public VeriteAmulet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "verite amulet";
		}
	}

	public class VeriteBracelet : GoldBracelet
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); } }

		[Constructable]
		public VeriteBracelet()
		{
			if ( Name != "verite bracelet" ){ Name = "verite bracelet"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public VeriteBracelet ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "verite bracelet";
		}
	}

	public class VeriteRing : GoldRing
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); } }

		[Constructable]
		public VeriteRing()
		{
			if ( Name != "verite ring" ){ Name = "verite ring"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public VeriteRing ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "verite ring";
		}
	}

	public class VeriteEarrings : GoldEarrings
	{
		public override int Hue{ get { return MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); } }

		[Constructable]
		public VeriteEarrings()
		{
			if ( Name != "verite earrings" ){ Name = "verite earrings"; Server.Items.Tinkering.TinkerMagic( this, 45, 3, 8 ); }
		}

		public VeriteEarrings ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			Name = "verite earrings";
		}
	}
}
