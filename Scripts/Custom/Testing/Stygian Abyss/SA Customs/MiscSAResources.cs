using System;

namespace Server.Items
{
	public class AbyssalCloth : Item
	{
	        public override int LabelNumber { get { return 1113350; } } // abyssal cloth

		[Constructable]
		public AbyssalCloth()
			: this( 1 )
		{
		}

		[Constructable]
		public AbyssalCloth( int amount )
			: base( 0x3183 )
		{
			Stackable = true;
			Amount = amount;
		}

		public AbyssalCloth( Serial serial )
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


	public class ArcanicRuneStone : Item
	{
	        public override int LabelNumber { get { return 1113352; } } // arcanic rune stone

		[Constructable]
		public ArcanicRuneStone()
			: this( 1 )
		{
		}

		[Constructable]
		public ArcanicRuneStone( int amount )
			: base( 0x573C )
		{
			Stackable = true;
			Amount = amount;
		}

		public ArcanicRuneStone( Serial serial )
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

	public class BottleIchor : Item
	{
	        public override int LabelNumber { get { return 1113361; } } // bottle of ichor

		[Constructable]
		public BottleIchor()
			: this( 1 )
		{
		}


		[Constructable]
		public BottleIchor( int amount )
			: base( 0x5748 )
		{
			Stackable = true;
			Amount = amount;
		}

		public BottleIchor( Serial serial )
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


	public class ChagaMushroom : Item
	{
	        public override int LabelNumber { get { return 1113356; } } // chaga mushroom

		[Constructable]
		public ChagaMushroom()
			: this( 1 )
		{
		}

		[Constructable]
		public ChagaMushroom( int amount )
			: base( 0x5743 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ChagaMushroom( Serial serial )
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

	public class CrushedGlass : Item
	{
	        public override int LabelNumber { get { return 1113351; } } // crushed glass

		[Constructable]
		public CrushedGlass()
			: this( 1 )
		{
		}

		[Constructable]
		public CrushedGlass( int amount )
			: base( 0x573B )
		{
			Stackable = true;
			Amount = amount;
		}

		public CrushedGlass( Serial serial )
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

	public class CrystalShards : Item
	{
	        public override int LabelNumber { get { return 1113347; } } // crystal shards

		[Constructable]
		public CrystalShards()
			: this( 1 )
		{
		}

		[Constructable]
		public CrystalShards( int amount )
			: base( 0x5738 )
		{
			Stackable = true;
			Amount = amount;
		}

		public CrystalShards( Serial serial )
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

	public class CrystallineBlackrock : Item
	{
	        public override int LabelNumber { get { return 1113344; } } // crystalline blackrock

		[Constructable]
		public CrystallineBlackrock()
			: this( 1 )
		{
		}


		[Constructable]
		public CrystallineBlackrock( int amount )
			: base( 0x5732 )
		{
			Stackable = true;
			Amount = amount;
		}

		public CrystallineBlackrock( Serial serial )
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

	public class DaemonClaw : Item
	{
	        public override int LabelNumber { get { return 1113330; } } // daemon claw

		[Constructable]
		public DaemonClaw()
			: this( 1 )
		{
		}

		[Constructable]
		public DaemonClaw( int amount )
			: base( 0x5721 )
		{
			Stackable = true;
			Amount = amount;
		}

		public DaemonClaw( Serial serial )
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

	public class DelicateScales : Item
	{
	        public override int LabelNumber { get { return 1113349; } } // delicate scales

		[Constructable]
		public DelicateScales()
			: this( 1 )
		{
		}

		[Constructable]
		public DelicateScales( int amount )
			: base( 0x573A )
		{
			Stackable = true;
			Amount = amount;
		}

		public DelicateScales( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class ElvenFletchings : Item
	{
	        public override int LabelNumber { get { return 1113346; } } // elven fletching

		[Constructable]
		public ElvenFletchings()
			: this( 1 )
		{
		}

		[Constructable]
		public ElvenFletchings( int amount )
			: base( 0x5737 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ElvenFletchings( Serial serial )
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

	public class EssencePrecision : Item
	{
	        public override int LabelNumber { get { return 1113327; } } // essence of precision

		[Constructable]
		public EssencePrecision()
			: this( 1 )
		{
		}

		[Constructable]
		public EssencePrecision( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssencePrecision( Serial serial )
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

	public class EssenceAchievement : Item
	{
	        public override int LabelNumber { get { return 1113325; } } // essence of achievement

		[Constructable]
		public EssenceAchievement()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceAchievement( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceAchievement( Serial serial )
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

	public class EssenceBalance : Item
	{
	        public override int LabelNumber { get { return 1113324; } } // essence of balance

		[Constructable]
		public EssenceBalance()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceBalance( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceBalance( Serial serial )
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

	public class EssenceControl : Item
	{
	        public override int LabelNumber { get { return 1113340; } } // essence of control

		[Constructable]
		public EssenceControl()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceControl( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceControl( Serial serial )
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

	public class EssenceDiligence : Item
	{
	        public override int LabelNumber { get { return 1113338; } } // essence of diligence

		[Constructable]
		public EssenceDiligence()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceDiligence( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceDiligence( Serial serial )
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

	public class EssenceDirection : Item
	{
	        public override int LabelNumber { get { return 1113328; } } // essence of direction

		[Constructable]
		public EssenceDirection()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceDirection( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceDirection( Serial serial )
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

	public class EssenceFeeling : Item
	{
	        public override int LabelNumber { get { return 1113339; } } // essence of feeling

		[Constructable]
		public EssenceFeeling()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceFeeling( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceFeeling( Serial serial )
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

	public class EssenceOrder : Item
	{
	        public override int LabelNumber { get { return 1113342; } } // essence of order

		[Constructable]
		public EssenceOrder()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceOrder( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
		}

		public EssenceOrder( Serial serial )
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

	public class EssencePassion : Item
	{
	        public override int LabelNumber { get { return 1113326; } } // essence of passion

		[Constructable]
		public EssencePassion()
			: this( 1 )
		{
		}

		[Constructable]
		public EssencePassion( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
			Hue = 150;
		}

		public EssencePassion( Serial serial )
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

	public class EssencePersistence : Item
	{
	        public override int LabelNumber { get { return 1113343; } } // essence of persistence

		[Constructable]
		public EssencePersistence()
			: this( 1 )
		{
		}

		[Constructable]
		public EssencePersistence( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
			Hue = 883;
		}

		public EssencePersistence( Serial serial )
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

	public class EssenceSingularity : Item
	{
	        public override int LabelNumber { get { return 1113341; } } // essence of singularity

		[Constructable]
		public EssenceSingularity()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceSingularity( int amount )
			: base( 0x571C )
		{
			Stackable = true;
			Amount = amount;
			Hue = 731;
		}

		public EssenceSingularity( Serial serial )
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

	public class FeyWings : Item
	{
	        public override int LabelNumber { get { return 1113332; } } // fey wings

		[Constructable]
		public FeyWings()
			: base( 0x5726 )
		{
			
		}

		public FeyWings( Serial serial )
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

	public class FaeryDust : Item
	{
	        public override int LabelNumber { get { return 1113358; } } // faery dust

		[Constructable]
		public FaeryDust()
			: base( 0x5745 )
		{
			
		}

		public FaeryDust( Serial serial )
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

	public class Fur : Item
	{
		[Constructable]
		public Fur()
			: base( 0x1875 )
		{
			
		}

		public Fur( Serial serial )
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

	public class GoblinBlood : Item
	{
	        public override int LabelNumber { get { return 1113335; } } // goblin blood

		[Constructable]
		public GoblinBlood()
			: this( 1 )
		{
		}

		[Constructable]
		public GoblinBlood( int amount )
			: base( 0x572C )
		{
			Stackable = true;
			Amount = amount;
		}

		public GoblinBlood( Serial serial )
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

	public class HornAbyssalInferno : Item
	{
	        public override int LabelNumber { get { return 1031703; } } // Horn of Abyssal Infernal

		[Constructable]
		public HornAbyssalInferno()
			: base( 0x2dB7 )
		{
			
		}

		public HornAbyssalInferno( Serial serial )
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

	public class KepetchWax : Item
	{
	        public override int LabelNumber { get { return 1112412; } } // kepetch wax

		[Constructable]
		public KepetchWax()
			: base( 0x5745 )
		{
			
		}

		public KepetchWax( Serial serial )
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

	public class LavaSerpenCrust : Item
	{
	        public override int LabelNumber { get { return 1113336; } } // lava serpent crust

		[Constructable]
		public LavaSerpenCrust()
			: this( 1 )
		{
		}

		[Constructable]
		public LavaSerpenCrust( int amount )
			: base( 0x572D )
		{
			Stackable = true;
			Amount = amount;
		}

		public LavaSerpenCrust( Serial serial )
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

	public class Lodestone : Item
	{
	        public override int LabelNumber { get { return 1113348; } } // lodestone

		[Constructable]
		public Lodestone()
			: base( 0x5739 )
		{
			
		}

		public Lodestone( Serial serial )
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

	public class MedusaBlood : Item
	{
	        public override int LabelNumber { get { return 1031702; } } // Medusa Blood

		[Constructable]
		public MedusaBlood()
			: this( 1 )
		{
		}

		[Constructable]
		public MedusaBlood( int amount )
			: base( 0x2DB6 )
		{
			Stackable = true;
			Amount = amount;
		}

		public MedusaBlood( Serial serial )
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

	public class PlantClippings : Item
	{
	        public override int LabelNumber { get { return 1112131; } } // plant clippings

		[Constructable]
		public PlantClippings()
			: this( 1 )
		{
		}

		[Constructable]
		public PlantClippings( int amount )
			: base( 0x4022 )
		{
			Stackable = true;
			Amount = amount;
		}

		public PlantClippings( Serial serial )
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

	public class PowderedIron : Item
	{
	        public override int LabelNumber { get { return 1113353; } } // powdered iron

		[Constructable]
		public PowderedIron()
			: this( 1 )
		{
		}

		[Constructable]
		public PowderedIron( int amount )
			: base( 0x573D )
		{
			Stackable = true;
			Amount = amount;
		}

		public PowderedIron( Serial serial )
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

	public class PrimalLichDust : Item
	{
	        public override int LabelNumber { get { return 1031701; } } // Primeval Lich Dust

		[Constructable]
		public PrimalLichDust()
			: this( 1 )
		{
		}

		[Constructable]
		public PrimalLichDust( int amount )
			: base( 0x2DB5 )
		{
			Stackable = true;
			Amount = amount;
		}

		public PrimalLichDust( Serial serial )
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

	public class RaptorTeeth : Item
	{
	        public override int LabelNumber { get { return 1113360; } } // raptor teeth

		[Constructable]
		public RaptorTeeth()
			: this( 1 )
		{
		}

		[Constructable]
		public RaptorTeeth( int amount )
			: base( 0x5747 )
		{
			Stackable = true;
			Amount = amount;
		}

		public RaptorTeeth( Serial serial )
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

	public class ReflectiveWolfEye : Item
	{
	        public override int LabelNumber { get { return 1113362; } } // reflective wolf eye

		[Constructable]
		public ReflectiveWolfEye()
			: this( 1 )
		{
		}

		[Constructable]
		public ReflectiveWolfEye( int amount )
			: base( 0x5749 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ReflectiveWolfEye( Serial serial )
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

	public class SeedRenewal : Item
	{
	        public override int LabelNumber { get { return 1113345; } } // seed of renewal

		[Constructable]
		public SeedRenewal()
			: this( 1 )
		{
		}

		[Constructable]
		public SeedRenewal( int amount )
			: base( 0x5736 )
		{
			Stackable = true;
			Amount = amount;
		}

		public SeedRenewal( Serial serial )
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

	public class ShimmeringCrystal : Item
	{
	        public override int LabelNumber { get { return 1075095; } } // Shimmering Crystals

		[Constructable]
		public ShimmeringCrystal()
			: this( 1 )
		{
		}

		[Constructable]
		public ShimmeringCrystal( int amount )
			: base( 0x5736 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ShimmeringCrystal( Serial serial )
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

	public class SilverSnakeSkin : Item
	{
	        public override int LabelNumber { get { return 1113357; } } // silver snake skin

		[Constructable]
		public SilverSnakeSkin()
			: this( 1 )
		{
		}

		[Constructable]
		public SilverSnakeSkin( int amount )
			: base( 0x5744 )
		{
			Stackable = true;
			Amount = amount;
		}

		public SilverSnakeSkin( Serial serial )
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

	public class SlithEye : Item
	{
	        public override int LabelNumber { get { return 1112396; } } // slith's eye

		[Constructable]
		public SlithEye()
			: this( 1 )
		{
		}

		[Constructable]
		public SlithEye( int amount )
			: base( 0x5746 )
		{
			Stackable = true;
			Amount = amount;
		}

		public SlithEye( Serial serial )
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

	public class SlithTongue : Item
	{
	        public override int LabelNumber { get { return 1113359; } } // slith tongue

		[Constructable]
		public SlithTongue()
			: this( 1 )
		{
		}

		[Constructable]
		public SlithTongue( int amount )
			: base( 0x5746 )
		{
			Stackable = true;
			Amount = amount;
		}

		public SlithTongue( Serial serial )
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

	public class SpiderCarapace : Item
	{
	        public override int LabelNumber { get { return 1113329; } } // spider carapace

		[Constructable]
		public SpiderCarapace()
			: this( 1 )
		{
		}

		[Constructable]
		public SpiderCarapace( int amount )
			: base( 0x5720 )
		{
			Stackable = true;
			Amount = amount;
		}

		public SpiderCarapace( Serial serial )
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

	public class ScouringToxin : Item
	{
	        public override int LabelNumber { get { return 1112292; } } // scouring toxin

		[Constructable]
		public ScouringToxin()
			: this( 1 )
		{
		}

		[Constructable]
		public ScouringToxin( int amount )
			: base( 0x4005 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ScouringToxin( Serial serial )
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

	public class ToxicVenomSac : Item
	{
	        public override int LabelNumber { get { return 1112291; } } // toxic venom sac

		[Constructable]
		public ToxicVenomSac()
			: this( 1 )
		{
		}

		[Constructable]
		public ToxicVenomSac( int amount )
			: base( 0x4005 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ToxicVenomSac( Serial serial )
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

	public class UndyingFlesh : Item
	{
	        public override int LabelNumber { get { return 1113337; } } // undying flesh

		[Constructable]
		public UndyingFlesh()
			: this( 1 )
		{
		}

		[Constructable]
		public UndyingFlesh( int amount )
			: base( 0x5731 )
		{
			Stackable = true;
			Amount = amount;
		}

		public UndyingFlesh( Serial serial )
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

	public class VialVitirol : Item
	{
	        public override int LabelNumber { get { return 1113331; } } // vial vitirol

		[Constructable]
		public VialVitirol()
			: this( 1 )
		{
		}

		[Constructable]
		public VialVitirol( int amount )
			: base( 0x5722 )
		{
			Stackable = true;
			Amount = amount;
		}

		public VialVitirol( Serial serial )
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

	public class VileTentacles : Item
	{
	        public override int LabelNumber { get { return 1113333; } } // vile tentacles

		[Constructable]
		public VileTentacles()
			: this( 1 )
		{
		}

		[Constructable]
		public VileTentacles( int amount )
			: base( 0x5727 )
		{
			Stackable = true;
			Amount = amount;
		}

		public VileTentacles( Serial serial )
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

	public class VoidCore : Item
	{
	        public override int LabelNumber { get { return 1113334; } } // void core

		[Constructable]
		public VoidCore()
			: this( 1 )
		{
		}

		[Constructable]
		public VoidCore( int amount )
			: base( 0x5728 )
		{
			Stackable = true;
			Amount = amount;
		}

		public VoidCore( Serial serial )
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

	public class VoidEssence : Item
	{
	        public override int LabelNumber { get { return 1112327; } } // void essence

		[Constructable]
		public VoidEssence()
			: this( 1 )
		{
		}

		[Constructable]
		public VoidEssence( int amount )
			: base( 0x4007 )
		{
			Stackable = true;
			Amount = amount;
		}

		public VoidEssence( Serial serial )
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

	public class VoidOrb : Item
	{
	        public override int LabelNumber { get { return 1113354; } } // void orb

		[Constructable]
		public VoidOrb()
			: this( 1 )
		{
		}

		[Constructable]
		public VoidOrb( int amount )
			: base( 0x573E )
		{
			Stackable = true;
			Amount = amount;
		}

		public VoidOrb( Serial serial )
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
