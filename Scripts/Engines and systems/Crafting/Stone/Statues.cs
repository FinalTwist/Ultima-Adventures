using System;

namespace Server.Items
{
	[Flipable( 0x320B, 0x3219 )]
	public class StatueElvenKnight : BaseStatue
	{
		[Constructable]
		public StatueElvenKnight() : base( 0x320B )
		{
			Name = "statue of an elven knight";
			Weight = 60;
		}

		public StatueElvenKnight(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x320C, 0x3212 )]
	public class StatueElvenWarrior : BaseStatue
	{
		[Constructable]
		public StatueElvenWarrior() : base( 0x320C )
		{
			Name = "statue of an elven warrior";
			Weight = 60;
		}

		public StatueElvenWarrior(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x321F, 0x3225 )]
	public class StatueElvenSorceress : BaseStatue
	{
		[Constructable]
		public StatueElvenSorceress() : base( 0x321F )
		{
			Name = "statue of an elven sorceress";
			Weight = 60;
		}

		public StatueElvenSorceress(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x322B, 0x3235 )]
	public class StatueElvenPriestess : BaseStatue
	{
		[Constructable]
		public StatueElvenPriestess() : base( 0x322B )
		{
			Name = "statue of an elven priestess";
			Weight = 60;
		}

		public StatueElvenPriestess(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x1224, 0x139A )]
	public class SmallStatueWoman : BaseStatue
	{
		[Constructable]
		public SmallStatueWoman() : base( 0x1224 )
		{
			Name = "small statue of a woman";
			Weight = 10;
		}

		public SmallStatueWoman(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x4B1D, 0x4B1C )]
	public class SmallStatueDragon : BaseStatue
	{
		[Constructable]
		public SmallStatueDragon() : base( 0x4B1D )
		{
			Name = "small statue of a dragon";
			Weight = 10;
		}

		public SmallStatueDragon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	public class SmallStatueNoble : BaseStatue
	{
		[Constructable]
		public SmallStatueNoble() : base( 0x1225 )
		{
			Name = "small statue of a noble";
			Weight = 10;
		}

		public SmallStatueNoble(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x1226, 0x139B )]
	public class SmallStatueAngel : BaseStatue
	{
		[Constructable]
		public SmallStatueAngel() : base( 0x1226 )
		{
			Name = "small statue of an angel";
			Weight = 10;
		}

		public SmallStatueAngel(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x1227, 0x139C )]
	public class SmallStatueMan : BaseStatue
	{
		[Constructable]
		public SmallStatueMan() : base( 0x1227 )
		{
			Name = "small statue of a man";
			Weight = 10;
		}

		public SmallStatueMan(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x1228, 0x139D )]
	public class SmallStatuePegasus : BaseStatue
	{
		[Constructable]
		public SmallStatuePegasus() : base( 0x1228 )
		{
			Name = "small statue of a pegasus";
			Weight = 10;
		}

		public SmallStatuePegasus(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	public class SmallStatueSkull : BaseStatue
	{
		[Constructable]
		public SmallStatueSkull() : base( 0x1F18 )
		{
			Name = "statue of a skull";
			Weight = 10;
		}

		public SmallStatueSkull(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x12D8, 0x12D9 )]
	public class StatueWomanWarriorPillar : BaseStatue
	{
		[Constructable]
		public StatueWomanWarriorPillar() : base( 0x12D8 )
		{
			Name = "statue of a woman warrior";
			Weight = 50;
		}

		public StatueWomanWarriorPillar(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31C1, 0x31C2 )]
	public class LargePegasusStatue : BaseStatue
	{
		[Constructable]
		public LargePegasusStatue() : base( 0x31C1 )
		{
			Name = "pegasus statue";
			Weight = 75;
		}

		public LargePegasusStatue(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x40BC, 0x3F3D )]
	public class MedusaStatue : BaseStatue
	{
		[Constructable]
		public MedusaStatue() : base( 0x40BC )
		{
			Name = "medusa statue";
			Weight = 50;
		}

		public MedusaStatue(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	public class GargoyleStatue : BaseStatue
	{
		[Constructable]
		public GargoyleStatue() : base( 0x42BB )
		{
			Name = "gargoyle statue";
			Weight = 25;
		}

		public GargoyleStatue(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31C7, 0x31C8 )]
	public class StatueFighter : BaseStatue
	{
		[Constructable]
		public StatueFighter() : base( 0x31C7 )
		{
			Name = "statue of a fighter";
			Weight = 60;
		}

		public StatueFighter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31C9, 0x31CA )]
	public class StatueSwordsman : BaseStatue
	{
		[Constructable]
		public StatueSwordsman() : base( 0x31C9 )
		{
			Name = "statue of a swordsman";
			Weight = 60;
		}

		public StatueSwordsman(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31CB, 0x31CC )]
	public class StatueNoble : BaseStatue
	{
		[Constructable]
		public StatueNoble() : base( 0x31CB )
		{
			Name = "statue of a noble";
			Weight = 60;
		}

		public StatueNoble(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31CD, 0x31CE )]
	public class StatueWizard : BaseStatue
	{
		[Constructable]
		public StatueWizard() : base( 0x31CD )
		{
			Name = "statue of a wizard";
			Weight = 60;
		}

		public StatueWizard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31CF, 0x31D0 )]
	public class StatueDruid : BaseStatue
	{
		[Constructable]
		public StatueDruid() : base( 0x31CF )
		{
			Name = "statue of a druid";
			Weight = 60;
		}

		public StatueDruid(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31D1, 0x31D2 )]
	public class StatuePriest : BaseStatue
	{
		[Constructable]
		public StatuePriest() : base( 0x31D1 )
		{
			Name = "statue of a priest";
			Weight = 60;
		}

		public StatuePriest(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31D3, 0x31D4 )]
	public class StatueAdventurer : BaseStatue
	{
		[Constructable]
		public StatueAdventurer() : base( 0x31D3 )
		{
			Name = "statue of an adventurer";
			Weight = 60;
		}

		public StatueAdventurer(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31FC, 0x31FD )]
	public class StatueDesertGod : BaseStatue
	{
		[Constructable]
		public StatueDesertGod() : base( 0x31FC )
		{
			Name = "statue of a god";
			Weight = 75;
		}

		public StatueDesertGod(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x31FE, 0x31FF )]
	public class StatueAmazon : BaseStatue
	{
		[Constructable]
		public StatueAmazon() : base( 0x31FE )
		{
			Name = "statue of an amazon";
			Weight = 60;
		}

		public StatueAmazon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x12CA, 0x12CB )]
	public class StatueBust : BaseStatue
	{
		[Constructable]
		public StatueBust() : base( 0x12CA )
		{
			Name = "bust of a man";
			Weight = 10;
		}

		public StatueBust(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	public class StatueGargoyleBust : BaseStatue
	{
		[Constructable]
		public StatueGargoyleBust() : base( 0x42BC )
		{
			Name = "bust of a gargoyle";
			Weight = 15;
		}

		public StatueGargoyleBust(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	public class StatueGargoyleTall : BaseStatue
	{
		[Constructable]
		public StatueGargoyleTall() : base( 0x42C5 )
		{
			Name = "statue of a gargoyle";
			Weight = 60;
		}

		public StatueGargoyleTall(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x42C3, 0x3F3E )]
	public class StatueWolfWinged : BaseStatue
	{
		[Constructable]
		public StatueWolfWinged() : base( 0x42C3 )
		{
			Name = "statue of a winged wolf";
			Weight = 50;
		}

		public StatueWolfWinged(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x4578, 0x4579 )]
	public class StatueSeaHorse : BaseStatue
	{
		[Constructable]
		public StatueSeaHorse() : base( 0x4578 )
		{
			Name = "statue of a sea horse";
			Weight = 60;
		}

		public StatueSeaHorse(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x457A, 0x457B )]
	public class StatueMermaid : BaseStatue
	{
		[Constructable]
		public StatueMermaid() : base( 0x457A )
		{
			Name = "statue of a mermaid";
			Weight = 60;
		}

		public StatueMermaid(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x457C, 0x457D )]
	public class StatueGryphon : BaseStatue
	{
		[Constructable]
		public StatueGryphon() : base( 0x457C )
		{
			Name = "statue of a gryphon";
			Weight = 60;
		}

		public StatueGryphon(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x3A98, 0x3A9A )]
	public class StatueDwarf : BaseStatue
	{
		[Constructable]
		public StatueDwarf() : base( 0x3A98 )
		{
			Name = "statue of a dwarf";
			Weight = 60;
		}

		public StatueDwarf(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x39A0, 0x39A1 )]
	public class StoneWizardTable : BaseStatue
	{
		[Constructable]
		public StoneWizardTable() : base( 0x39A0 )
		{
			Name = "stone wizard table";
			Weight = 60;
		}

		public StoneWizardTable(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x39BD, 0x39BE )]
	public class GargoyleFlightStatue : BaseStatue
	{
		[Constructable]
		public GargoyleFlightStatue() : base( 0x39BD )
		{
			Name = "statue of a gargoyle";
			Weight = 50;
		}

		public GargoyleFlightStatue(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	[Flipable( 0x3A07, 0x3A08 )]
	public class SphinxStatue : BaseStatue
	{
		[Constructable]
		public SphinxStatue() : base( 0x3A07 )
		{
			Name = "statue of a sphinx";
			Weight = 50;
		}

		public SphinxStatue(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}