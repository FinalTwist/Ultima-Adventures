using System;

namespace Server.Items
{
	[Flipable( 0x2B6B, 0x3162 )]
	public class JokerRobe : BaseOuterTorso
	{
		[Constructable]
		public JokerRobe() : this( 0 )
		{
		}

		[Constructable]
		public JokerRobe( int hue ) : base( 0x2B6B, hue )
		{
			Name = "jester coat";
			Weight = 3.0;
		}

		public JokerRobe( Serial serial ) : base( serial )
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

	public class PirateRobe : BaseOuterTorso
	{
		[Constructable]
		public PirateRobe() : this( 0 )
		{
		}

		[Constructable]
		public PirateRobe( int hue ) : base( 0x567E, hue )
		{
			Name = "pirate coat";
			Weight = 3.0;
		}

		public PirateRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2B69, 0x3160 )]
	public class AssassinRobe : BaseOuterTorso
	{
		[Constructable]
		public AssassinRobe() : this( 0 )
		{
		}

		[Constructable]
		public AssassinRobe( int hue ) : base( 0x2B69, hue )
		{
			Name = "assassin robe";
			Weight = 3.0;
			if ( Hue == 0 ){ Hue = 0x96B; }
		}

		public AssassinRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x201D, 0x201E )]
	public class VampireRobe : BaseOuterTorso
	{
		[Constructable]
		public VampireRobe() : this( 0 )
		{
		}

		[Constructable]
		public VampireRobe( int hue ) : base( 0x201D, hue )
		{
			Name = "vampire robe";
			Weight = 3.0;
		}

		public VampireRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x201B, 0x201C )]
	public class DragonRobe : BaseOuterTorso
	{
		[Constructable]
		public DragonRobe() : this( 0 )
		{
		}

		[Constructable]
		public DragonRobe( int hue ) : base( 0x201B, hue )
		{
			Name = "dragon robe";
			Weight = 3.0;
		}

		public DragonRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x201F, 0x2020 )]
	public class ChaosRobe : BaseOuterTorso
	{
		[Constructable]
		public ChaosRobe() : this( 0 )
		{
		}

		[Constructable]
		public ChaosRobe( int hue ) : base( 0x201F, hue )
		{
			Name = "chaos robe";
			Weight = 3.0;
		}

		public ChaosRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2B6A, 0x3161 )]
	public class FancyRobe : BaseOuterTorso
	{
		[Constructable]
		public FancyRobe() : this( 0 )
		{
		}

		[Constructable]
		public FancyRobe( int hue ) : base( 0x2B6A, hue )
		{
			Name = "fancy robe";
			Weight = 3.0;
		}

		public FancyRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2B6C, 0x3163 )]
	public class GildedRobe : BaseOuterTorso
	{
		[Constructable]
		public GildedRobe() : this( 0 )
		{
		}

		[Constructable]
		public GildedRobe( int hue ) : base( 0x2B6C, hue )
		{
			Name = "gilded robe";
			Weight = 3.0;
			if ( Hue == 0 ){ Hue = 0x967; }
		}

		public GildedRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2B6E, 0x3165 )]
	public class OrnateRobe : BaseOuterTorso
	{
		[Constructable]
		public OrnateRobe() : this( 0 )
		{
		}

		[Constructable]
		public OrnateRobe( int hue ) : base( 0x2B6E, hue )
		{
			Name = "ornate robe";
			Weight = 3.0;
		}

		public OrnateRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2B70, 0x3167 )]
	public class MagistrateRobe : BaseOuterTorso
	{
		[Constructable]
		public MagistrateRobe() : this( 0 )
		{
		}

		[Constructable]
		public MagistrateRobe( int hue ) : base( 0x2B70, hue )
		{
			Name = "magistrate robe";
			Weight = 3.0;
			if ( Hue == 0 ){ Hue = 0xA4B; }
		}

		public MagistrateRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2B73, 0x316A )]
	public class RoyalRobe : BaseOuterTorso
	{
		[Constructable]
		public RoyalRobe() : this( 0 )
		{
		}

		[Constructable]
		public RoyalRobe( int hue ) : base( 0x2B73, hue )
		{
			Name = "royal robe";
			Weight = 3.0;
		}

		public RoyalRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x3175, 0x3178 )]
	public class SorcererRobe : BaseOuterTorso
	{
		[Constructable]
		public SorcererRobe() : this( 0 )
		{
		}

		[Constructable]
		public SorcererRobe( int hue ) : base( 0x3175, hue )
		{
			Name = "sorcerer robe";
			Weight = 3.0;
		}

		public SorcererRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2FB9, 0x3173 )]
	public class AssassinShroud : BaseOuterTorso
	{
		[Constructable]
		public AssassinShroud() : this( 0 )
		{
		}

		[Constructable]
		public AssassinShroud( int hue ) : base( 0x2FB9, hue )
		{
			Name = "assassin shroud";
			Weight = 3.0;
			if ( Hue == 0 ){ Hue = 0x96B; }
		}

		public AssassinShroud( Serial serial ) : base( serial )
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

	[Flipable( 0x2FBA, 0x3174 )]
	public class NecromancerRobe : BaseOuterTorso
	{
		[Constructable]
		public NecromancerRobe() : this( 0 )
		{
		}

		[Constructable]
		public NecromancerRobe( int hue ) : base( 0x2FBA, hue )
		{
			Name = "necromancer robe";
			Weight = 3.0;
		}

		public NecromancerRobe( Serial serial ) : base( serial )
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

	[Flipable( 0x2FC6, 0x2FC7 )]
	public class SpiderRobe : BaseOuterTorso
	{
		[Constructable]
		public SpiderRobe() : this( 0 )
		{
		}

		[Constructable]
		public SpiderRobe( int hue ) : base( 0x2FC6, hue )
		{
			Name = "spider robe";
			Weight = 3.0;
			if ( Hue == 0 ){ Hue = 0x96B; }
		}

		public SpiderRobe( Serial serial ) : base( serial )
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

	public class VagabondRobe : BaseOuterTorso
	{
		[Constructable]
		public VagabondRobe() : this( 0 )
		{
		}

		[Constructable]
		public VagabondRobe( int hue ) : base( 0x567D, hue )
		{
			Name = "vagabond robe";
			Weight = 3.0;
		}

		public VagabondRobe( Serial serial ) : base( serial )
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

	public class PirateCoat : BaseOuterTorso
	{
		[Constructable]
		public PirateCoat() : this( 0 )
		{
		}

		[Constructable]
		public PirateCoat( int hue ) : base( 0x567E, hue )
		{
			Name = "pirate coat";
			Weight = 3.0;
		}

		public PirateCoat( Serial serial ) : base( serial )
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
	
	public class KnightsRobe : BaseOuterTorso // final
	{
		[Constructable]
		public KnightsRobe() : this( 0 )
		{
		}

		[Constructable]
		public KnightsRobe( int hue ) : base( 0x55BA, hue )
		{
			Name = "Knight's Robe";
			Weight = 3.0;
		}

		public KnightsRobe( Serial serial ) : base( serial )
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
	
	public class GladiatorSash : BaseOuterTorso // final
	{
		[Constructable]
		public GladiatorSash() : this( 0 )
		{
		}

		[Constructable]
		public GladiatorSash( int hue ) : base( 0x55BB, hue )
		{
			Name = "Gladiator Laniard";
			Weight = 3.0;
		}

		public GladiatorSash( Serial serial ) : base( serial )
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

	public class GladiatorClasp : BaseOuterTorso // final
	{
		[Constructable]
		public GladiatorClasp() : this( 0 )
		{
		}

		[Constructable]
		public GladiatorClasp( int hue ) : base( 0x55BC, hue )
		{
			Name = "Gladiator Clasp";
			Weight = 3.0;
		}

		public GladiatorClasp( Serial serial ) : base( serial )
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

	public class ThiefPaulettes : BaseOuterTorso // final
	{
		[Constructable]
		public ThiefPaulettes() : this( 0 )
		{
		}

		[Constructable]
		public ThiefPaulettes( int hue ) : base( 0x55BD, hue )
		{
			Name = "Thief's Eppaulettes";
			Weight = 3.0;
		}

		public ThiefPaulettes( Serial serial ) : base( serial )
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

	public class JesterGarb : BaseOuterTorso
	{
		[Constructable]
		public JesterGarb() : this( 0 )
		{
		}

		[Constructable]
		public JesterGarb( int hue ) : base( 0x4C16, hue )
		{
			Name = "jester garb";
			Weight = 3.0;
		}

		public JesterGarb( Serial serial ) : base( serial )
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

	public class FoolsCoat : BaseOuterTorso
	{
		[Constructable]
		public FoolsCoat() : this( 0 )
		{
		}

		[Constructable]
		public FoolsCoat( int hue ) : base( 0x4C17, hue )
		{
			Name = "fool's coat";
			Weight = 3.0;
		}

		public FoolsCoat( Serial serial ) : base( serial )
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


	public class ExquisiteRobe : BaseOuterTorso
	{
		[Constructable]
		public ExquisiteRobe() : this( 0 )
		{
		}

		[Constructable]
		public ExquisiteRobe( int hue ) : base( 0x283, hue )
		{
			Name = "exquisite robe";
			Weight = 3.0;
		}

		public ExquisiteRobe( Serial serial ) : base( serial )
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
	public class ProphetRobe : BaseOuterTorso
	{
		[Constructable]
		public ProphetRobe() : this( 0 )
		{
		}

		[Constructable]
		public ProphetRobe( int hue ) : base( 0x284, hue )
		{
			Name = "prophet robe";
			Weight = 3.0;
		}

		public ProphetRobe( Serial serial ) : base( serial )
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
	public class ElegantRobe : BaseOuterTorso
	{
		[Constructable]
		public ElegantRobe() : this( 0 )
		{
		}

		[Constructable]
		public ElegantRobe( int hue ) : base( 0x285, hue )
		{
			Name = "elegant robe";
			Weight = 3.0;
		}

		public ElegantRobe( Serial serial ) : base( serial )
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
	public class FormalRobe : BaseOuterTorso
	{
		[Constructable]
		public FormalRobe() : this( 0 )
		{
		}

		[Constructable]
		public FormalRobe( int hue ) : base( 0x286, hue )
		{
			Name = "formal robe";
			Weight = 3.0;
		}

		public FormalRobe( Serial serial ) : base( serial )
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
	public class ArchmageRobe : BaseOuterTorso
	{
		[Constructable]
		public ArchmageRobe() : this( 0 )
		{
		}

		[Constructable]
		public ArchmageRobe( int hue ) : base( 0x287, hue )
		{
			Name = "archmage robe";
			Weight = 3.0;
		}

		public ArchmageRobe( Serial serial ) : base( serial )
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
	public class PriestRobe : BaseOuterTorso
	{
		[Constructable]
		public PriestRobe() : this( 0 )
		{
		}

		[Constructable]
		public PriestRobe( int hue ) : base( 0x288, hue )
		{
			Name = "priest robe";
			Weight = 3.0;
		}

		public PriestRobe( Serial serial ) : base( serial )
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
	public class CultistRobe : BaseOuterTorso
	{
		[Constructable]
		public CultistRobe() : this( 0 )
		{
		}

		[Constructable]
		public CultistRobe( int hue ) : base( 0x289, hue )
		{
			Name = "cultist robe";
			Weight = 3.0;
		}

		public CultistRobe( Serial serial ) : base( serial )
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
	public class GildedDarkRobe : BaseOuterTorso
	{
		[Constructable]
		public GildedDarkRobe() : this( 0 )
		{
		}

		[Constructable]
		public GildedDarkRobe( int hue ) : base( 0x28A, hue )
		{
			Name = "gilded dark robe";
			Weight = 3.0;
		}

		public GildedDarkRobe( Serial serial ) : base( serial )
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
	public class GildedLightRobe : BaseOuterTorso
	{
		[Constructable]
		public GildedLightRobe() : this( 0 )
		{
		}

		[Constructable]
		public GildedLightRobe( int hue ) : base( 0x301, hue )
		{
			Name = "gilded light robe";
			Weight = 3.0;
		}

		public GildedLightRobe( Serial serial ) : base( serial )
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
	public class SageRobe : BaseOuterTorso
	{
		[Constructable]
		public SageRobe() : this( 0 )
		{
		}

		[Constructable]
		public SageRobe( int hue ) : base( 0x302, hue )
		{
			Name = "sage robe";
			Weight = 3.0;
		}

		public SageRobe( Serial serial ) : base( serial )
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