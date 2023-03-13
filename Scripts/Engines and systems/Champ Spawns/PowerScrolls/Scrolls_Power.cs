using System;
using Server;
using Server.Engines.Craft;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class DJ_SP_Alchemy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Alchemy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Alchemy(): base(SkillName.Alchemy, 125)
		{
		}
		[Constructable]
		public DJ_SP_Alchemy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Alchemy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Alchemy(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}

	public class DJ_SP_Hiding : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Hiding
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Hiding(): base(SkillName.Hiding, 125)
		{
		}
		[Constructable]
		public DJ_SP_Hiding(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Hiding";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Hiding(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}

	public class DJ_SP_AnimalLore : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.AnimalLore
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_AnimalLore(): base(SkillName.AnimalLore, 125)
		{
		}
		[Constructable]
		public DJ_SP_AnimalLore(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Animal Lore";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_AnimalLore(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_AnimalTaming : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.AnimalTaming
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_AnimalTaming(): base(SkillName.AnimalTaming, 125)
		{
		}
		[Constructable]
		public DJ_SP_AnimalTaming(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Animal Taming";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_AnimalTaming(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Archery : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Archery
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Archery(): base(SkillName.Archery, 125)
		{
		}
		[Constructable]
		public DJ_SP_Archery(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Archery";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Archery(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_ArmsLore : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.ArmsLore
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_ArmsLore(): base(SkillName.ArmsLore, 125)
		{
		}
		[Constructable]
		public DJ_SP_ArmsLore(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Arms Lore";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_ArmsLore(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Blacksmith : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Blacksmith
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Blacksmith(): base(SkillName.Blacksmith, 125)
		{
		}
		[Constructable]
		public DJ_SP_Blacksmith(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Blacksmithing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Blacksmith(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Bushido : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Bushido
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Bushido(): base(SkillName.Bushido, 125)
		{
		}
		[Constructable]
		public DJ_SP_Bushido(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Bushido";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Bushido(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Carpentry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Carpentry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Carpentry(): base(SkillName.Carpentry, 125)
		{
		}
		[Constructable]
		public DJ_SP_Carpentry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Carpentry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Carpentry(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Cartography : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Cartography
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Cartography(): base(SkillName.Cartography, 125)
		{
		}
		[Constructable]
		public DJ_SP_Cartography(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Cartography";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Cartography(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Chivalry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Chivalry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Chivalry(): base(SkillName.Chivalry, 125)
		{
		}
		[Constructable]
		public DJ_SP_Chivalry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Chivalry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Chivalry(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Cooking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Cooking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Cooking(): base(SkillName.Cooking, 125)
		{
		}
		[Constructable]
		public DJ_SP_Cooking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Cooking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Cooking(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_DetectHidden : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.DetectHidden
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_DetectHidden(): base(SkillName.DetectHidden, 125)
		{
		}
		[Constructable]
		public DJ_SP_DetectHidden(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Detect Hidden";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_DetectHidden(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Discordance : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Discordance
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Discordance(): base(SkillName.Discordance, 125)
		{
		}
		[Constructable]
		public DJ_SP_Discordance(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Discordance";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Discordance(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_EvalInt : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.EvalInt
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_EvalInt(): base(SkillName.EvalInt, 125)
		{
		}
		[Constructable]
		public DJ_SP_EvalInt(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Evaluate Intelligence";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_EvalInt(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Fencing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fencing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Fencing(): base(SkillName.Fencing, 125)
		{
		}
		[Constructable]
		public DJ_SP_Fencing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Fencing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Fencing(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Fletching : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fletching
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Fletching(): base(SkillName.Fletching, 125)
		{
		}
		[Constructable]
		public DJ_SP_Fletching(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Fletching";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Fletching(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Fishing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fishing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Fishing(): base(SkillName.Fishing, 125)
		{
		}
		[Constructable]
		public DJ_SP_Fishing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Fishing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Fishing(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Healing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Healing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Healing(): base(SkillName.Healing, 125)
		{
		}
		[Constructable]
		public DJ_SP_Healing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Healing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Healing(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Inscribe : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Inscribe
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Inscribe(): base(SkillName.Inscribe, 125)
		{
		}
		[Constructable]
		public DJ_SP_Inscribe(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Inscription";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Inscribe(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Lockpicking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Lockpicking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Lockpicking(): base(SkillName.Lockpicking, 125)
		{
		}
		[Constructable]
		public DJ_SP_Lockpicking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Lockpicking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Lockpicking(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Lumberjacking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Lumberjacking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Lumberjacking(): base(SkillName.Lumberjacking, 125)
		{
		}
		[Constructable]
		public DJ_SP_Lumberjacking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Lumberjacking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Lumberjacking(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Macing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Macing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Macing(): base(SkillName.Macing, 125)
		{
		}
		[Constructable]
		public DJ_SP_Macing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Mace Fighting";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Macing(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Magery : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Magery
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Magery(): base(SkillName.Magery, 125)
		{
		}
		[Constructable]
		public DJ_SP_Magery(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Magery";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Magery(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_MagicResist : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.MagicResist
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_MagicResist(): base(SkillName.MagicResist, 125)
		{
		}
		[Constructable]
		public DJ_SP_MagicResist(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Magic Resist";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_MagicResist(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Meditation : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Meditation
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Meditation(): base(SkillName.Meditation, 125)
		{
		}
		[Constructable]
		public DJ_SP_Meditation(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Meditation";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Meditation(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Mining : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Mining
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Mining(): base(SkillName.Mining, 125)
		{
		}
		[Constructable]
		public DJ_SP_Mining(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Mining";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Mining(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Musicianship : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Musicianship
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Musicianship(): base(SkillName.Musicianship, 125)
		{
		}
		[Constructable]
		public DJ_SP_Musicianship(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Musicianship";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Musicianship(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Necromancy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Necromancy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Necromancy(): base(SkillName.Necromancy, 125)
		{
		}
		[Constructable]
		public DJ_SP_Necromancy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Necromancy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Necromancy(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Ninjitsu : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Ninjitsu
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Ninjitsu(): base(SkillName.Ninjitsu, 125)
		{
		}
		[Constructable]
		public DJ_SP_Ninjitsu(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Ninjitsu";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Ninjitsu(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Parry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Parry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Parry(): base(SkillName.Parry, 125)
		{
		}
		[Constructable]
		public DJ_SP_Parry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Parry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Parry(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Peacemaking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Peacemaking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Peacemaking(): base(SkillName.Peacemaking, 125)
		{
		}
		[Constructable]
		public DJ_SP_Peacemaking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Peacemaking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Peacemaking(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Poisoning : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Poisoning
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Poisoning(): base(SkillName.Poisoning, 125)
		{
		}
		[Constructable]
		public DJ_SP_Poisoning(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Poisoning";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Poisoning(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Provocation : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Provocation
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Provocation(): base(SkillName.Provocation, 125)
		{
		}
		[Constructable]
		public DJ_SP_Provocation(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Provocation";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Provocation(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_RemoveTrap : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.RemoveTrap
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_RemoveTrap(): base(SkillName.RemoveTrap, 125)
		{
		}
		[Constructable]
		public DJ_SP_RemoveTrap(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Remove Trap";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_RemoveTrap(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Snooping : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Snooping
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Snooping(): base(SkillName.Snooping, 125)
		{
		}
		[Constructable]
		public DJ_SP_Snooping(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Snooping";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Snooping(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_SpiritSpeak : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.SpiritSpeak
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_SpiritSpeak(): base(SkillName.SpiritSpeak, 125)
		{
		}
		[Constructable]
		public DJ_SP_SpiritSpeak(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Spirit Speak";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_SpiritSpeak(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Stealing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Stealing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Stealing(): base(SkillName.Stealing, 125)
		{
		}
		[Constructable]
		public DJ_SP_Stealing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Stealing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Stealing(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Stealth : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Stealth
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Stealth(): base(SkillName.Stealth, 125)
		{
		}
		[Constructable]
		public DJ_SP_Stealth(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Stealth";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Stealth(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Swords : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Swords
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Swords(): base(SkillName.Swords, 125)
		{
		}
		[Constructable]
		public DJ_SP_Swords(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Sword Fighting";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Swords(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Tactics : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tactics
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Tactics(): base(SkillName.Tactics, 125)
		{
		}
		[Constructable]
		public DJ_SP_Tactics(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Tactics";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Tactics(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Tailoring : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tailoring
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Tailoring(): base(SkillName.Tailoring, 125)
		{
		}
		[Constructable]
		public DJ_SP_Tailoring(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Tailoring";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Tailoring(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Tinkering : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tinkering
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Tinkering(): base(SkillName.Tinkering, 125)
		{
		}
		[Constructable]
		public DJ_SP_Tinkering(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Tinkering";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Tinkering(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Tracking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tracking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Tracking(): base(SkillName.Tracking, 125)
		{
		}
		[Constructable]
		public DJ_SP_Tracking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Tracking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Tracking(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Veterinary : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Veterinary
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Veterinary(): base(SkillName.Veterinary, 125)
		{
		}
		[Constructable]
		public DJ_SP_Veterinary(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Veterinary";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Veterinary(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}


	public class DJ_SP_Wrestling : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Wrestling
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Wrestling(): base(SkillName.Wrestling, 125)
		{
		}
		[Constructable]
		public DJ_SP_Wrestling(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Wrestling";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Wrestling(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}

	public class DJ_SP_Anatomy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Anatomy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Anatomy(): base(SkillName.Anatomy, 125)
		{
		}
		[Constructable]
		public DJ_SP_Anatomy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Anatomy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Anatomy(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}

	public class DJ_SP_Focus : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Focus
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SP_Focus(): base(SkillName.Focus, 125)
		{
		}
		[Constructable]
		public DJ_SP_Focus(SkillName skill, int value): base(0x14F0)
		{
			Name = "Power Scroll of Focus";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SP_Focus(Serial serial): base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int)0); // version
			writer.Write((int)m_Skill);
			writer.Write((int)m_Value);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Value = reader.ReadInt();
					break;
				}
			}
			if (LootType != LootType.Cursed)
				LootType = LootType.Cursed;
			
				
		}
	}

}
