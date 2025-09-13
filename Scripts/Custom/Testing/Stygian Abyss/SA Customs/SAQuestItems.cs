using System;

namespace Server.Items
{
	public class AcidSac : Item
	{
	        public override int LabelNumber { get { return 1111654; } } // acid sac

		[Constructable]
		public AcidSac()
			: this( 1 )
		{
		}

		[Constructable]
		public AcidSac( int amount )
			: base( 0x3191 )
		{
			Stackable = true;
			Amount = amount;
		}

		public AcidSac( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class AncientPotteryFragments : Item
	{
	        public override int LabelNumber { get { return 1112990; } } // Ancient Pottery fragments

		[Constructable]
		public AncientPotteryFragments()
			: this( 1 )
		{
		}

		[Constructable]
		public AncientPotteryFragments( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public AncientPotteryFragments( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class BouraPelt : Item
	{
	        public override int LabelNumber { get { return 1113355; } } // boura pelt

		[Constructable]
		public BouraPelt()
			: this( 1 )
		{
		}

		[Constructable]
		public BouraPelt( int amount )
			: base( 0x5742 )
		{
			Stackable = true;
			Amount = amount;
		}

		public BouraPelt( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class ClawSlasherVeils : Item
	{
	        public override int LabelNumber { get { return 1031704; } } // Claw of Slasher of Veils

		[Constructable]
		public ClawSlasherVeils()
			: this( 1 )
		{
		}

		[Constructable]
		public ClawSlasherVeils( int amount )
			: base( 0x2DB8 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ClawSlasherVeils( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class CongealedSlugAcid : Item
	{
	        public override int LabelNumber { get { return 1112901; } } // Congealed Slug Acid

		[Constructable]
		public CongealedSlugAcid()
			: this( 1 )
		{
		}

		[Constructable]
		public CongealedSlugAcid( int amount )
			: base( 0x5742 )
		{
			Stackable = true;
			Amount = amount;
		}

		public CongealedSlugAcid( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class EnchantEssence : Item
	{
	        public override int LabelNumber { get { return 1031698; } } // Enchaned Essence

		[Constructable]
		public EnchantEssence()
			: this( 1 )
		{
		}

		[Constructable]
		public EnchantEssence( int amount )
			: base( 0x2DB2 )
		{
			Stackable = true;
			Amount = amount;
		}

		public EnchantEssence( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class FairyDragonWing : Item
	{
	        public override int LabelNumber { get { return 1112899; } } // Fairy Dragon Wing

		[Constructable]
		public FairyDragonWing()
			: this( 1 )
		{
		}

		[Constructable]
		public FairyDragonWing( int amount )
			: base( 0x5726 )
		{
			Stackable = true;
			Amount = amount;
		}

		public FairyDragonWing( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LeatherWolfSkin : Item
	{
	        public override int LabelNumber { get { return 1112906; } } // leather wolf skin

		[Constructable]
		public LeatherWolfSkin()
			: this( 1 )
		{
		}

		[Constructable]
		public LeatherWolfSkin( int amount )
			: base( 0x3189 )
		{
			Stackable = true;
			Amount = amount;
		}

		public LeatherWolfSkin( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LuckyCoin : Item
	{
	        public override int LabelNumber { get { return 1113366; } } // lucky coin

		[Constructable]
		public LuckyCoin()
			: this( 1 )
		{
		}

		[Constructable]
		public LuckyCoin( int amount )
			: base( 0x3189 )
		{
			Stackable = true;
			Amount = amount;
		}

		public LuckyCoin( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class MagicalResidue : Item
	{
	        public override int LabelNumber { get { return 1031697; } } // Magical Residue

		[Constructable]
		public MagicalResidue()
			: this( 1 )
		{
		}

		[Constructable]
		public MagicalResidue( int amount )
			: base( 0x2DB1 )
		{
			Stackable = true;
			Amount = amount;
		}

		public MagicalResidue( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class PileInspectedIngots : Item
	{
		[Constructable]
		public PileInspectedIngots()
			: this( 1 )
		{
		}

		[Constructable]
		public PileInspectedIngots( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public PileInspectedIngots( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class RelicFragment : Item
	{
	        public override int LabelNumber { get { return 1031699; } } // Relic Fragment

		[Constructable]
		public RelicFragment()
			: this( 1 )
		{
		}

		[Constructable]
		public RelicFragment( int amount )
			: base( 0x2DB3 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RelicFragment( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SearedFireAntGoo : Item
	{
	        public override int LabelNumber { get { return 1112902; } } // Seared Fire Ant Goo
	
		[Constructable]
		public SearedFireAntGoo()
			: this( 1 )
		{
		}

		[Constructable]
		public SearedFireAntGoo( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public SearedFireAntGoo( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class StygianDragonHead : Item
	{
	        public override int LabelNumber { get { return 1031700; } } // Stygian Dragon Head

		[Constructable]
		public StygianDragonHead()
			: this( 1 )
		{
		}

		[Constructable]
		public StygianDragonHead( int amount )
			: base( 0x2DB4 )
		{
			Stackable = true;
			Amount = amount;
		}

		public StygianDragonHead( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class TatteredAncientScroll : Item
	{
	        public override int LabelNumber { get { return 1112991; } } // Tattered Remnants of an Ancient Scroll

		[Constructable]
		public TatteredAncientScroll()
			: this( 1 )
		{
		}

		[Constructable]
		public TatteredAncientScroll( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public TatteredAncientScroll( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class UndamagedIronBeetleScale : Item
	{
	        public override int LabelNumber { get { return 1112905; } } // Undamaged Iron Beetle Scale
	
		[Constructable]
		public UndamagedIronBeetleScale()
			: this( 1 )
		{
		}

		[Constructable]
		public UndamagedIronBeetleScale( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public UndamagedIronBeetleScale( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class UndeadGargHorn : Item
	{
	        public override int LabelNumber { get { return 1112903; } } // Undamaged Undead Gargoyle Horns
	
		[Constructable]
		public UndeadGargHorn()
			: this( 1 )
		{
		}

		[Constructable]
		public UndeadGargHorn( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public UndeadGargHorn( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class UndeadGargMedallion : Item
	{
	        public override int LabelNumber { get { return 1112907; } } // Undead Gargoyle Medallion
	
		[Constructable]
		public UndeadGargMedallion()
			: this( 1 )
		{
		}

		[Constructable]
		public UndeadGargMedallion( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public UndeadGargMedallion( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class UntransTome : Item
	{
	        public override int LabelNumber { get { return 1112992; } } // Untranslated Ancient Tome

		[Constructable]
		public UntransTome()
			: this( 1 )
		{
		}

		[Constructable]
		public UntransTome( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public UntransTome( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}