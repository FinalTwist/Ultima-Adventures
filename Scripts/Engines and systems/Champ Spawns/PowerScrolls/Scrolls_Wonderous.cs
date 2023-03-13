using System;
using Server;
using Server.Engines.Craft;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class DJ_SW_Alchemy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Alchemy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Alchemy(): base(SkillName.Alchemy, 105)
		{
		}
		[Constructable]
		public DJ_SW_Alchemy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Alchemy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Alchemy(Serial serial): base(serial)
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

	public class DJ_SW_Hiding : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Hiding
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Hiding(): base(SkillName.Hiding, 105)
		{
		}
		[Constructable]
		public DJ_SW_Hiding(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Hiding";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Hiding(Serial serial): base(serial)
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


	public class DJ_SW_AnimalLore : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.AnimalLore
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_AnimalLore(): base(SkillName.AnimalLore, 105)
		{
		}
		[Constructable]
		public DJ_SW_AnimalLore(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Animal Lore";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_AnimalLore(Serial serial): base(serial)
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


	public class DJ_SW_AnimalTaming : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.AnimalTaming
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_AnimalTaming(): base(SkillName.AnimalTaming, 105)
		{
		}
		[Constructable]
		public DJ_SW_AnimalTaming(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Animal Taming";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_AnimalTaming(Serial serial): base(serial)
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


	public class DJ_SW_Archery : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Archery
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Archery(): base(SkillName.Archery, 105)
		{
		}
		[Constructable]
		public DJ_SW_Archery(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Archery";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Archery(Serial serial): base(serial)
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


	public class DJ_SW_ArmsLore : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.ArmsLore
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_ArmsLore(): base(SkillName.ArmsLore, 105)
		{
		}
		[Constructable]
		public DJ_SW_ArmsLore(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Arms Lore";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_ArmsLore(Serial serial): base(serial)
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


	public class DJ_SW_Blacksmith : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Blacksmith
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Blacksmith(): base(SkillName.Blacksmith, 105)
		{
		}
		[Constructable]
		public DJ_SW_Blacksmith(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Blacksmithing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Blacksmith(Serial serial): base(serial)
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


	public class DJ_SW_Bushido : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Bushido
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Bushido(): base(SkillName.Bushido, 105)
		{
		}
		[Constructable]
		public DJ_SW_Bushido(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Bushido";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Bushido(Serial serial): base(serial)
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


	public class DJ_SW_Carpentry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Carpentry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Carpentry(): base(SkillName.Carpentry, 105)
		{
		}
		[Constructable]
		public DJ_SW_Carpentry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Carpentry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Carpentry(Serial serial): base(serial)
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


	public class DJ_SW_Cartography : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Cartography
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Cartography(): base(SkillName.Cartography, 105)
		{
		}
		[Constructable]
		public DJ_SW_Cartography(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Cartography";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Cartography(Serial serial): base(serial)
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


	public class DJ_SW_Chivalry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Chivalry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Chivalry(): base(SkillName.Chivalry, 105)
		{
		}
		[Constructable]
		public DJ_SW_Chivalry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Chivalry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Chivalry(Serial serial): base(serial)
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


	public class DJ_SW_Cooking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Cooking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Cooking(): base(SkillName.Cooking, 105)
		{
		}
		[Constructable]
		public DJ_SW_Cooking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Cooking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Cooking(Serial serial): base(serial)
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


	public class DJ_SW_DetectHidden : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.DetectHidden
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_DetectHidden(): base(SkillName.DetectHidden, 105)
		{
		}
		[Constructable]
		public DJ_SW_DetectHidden(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Detect Hidden";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_DetectHidden(Serial serial): base(serial)
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


	public class DJ_SW_Discordance : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Discordance
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Discordance(): base(SkillName.Discordance, 105)
		{
		}
		[Constructable]
		public DJ_SW_Discordance(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Discordance";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Discordance(Serial serial): base(serial)
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


	public class DJ_SW_EvalInt : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.EvalInt
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_EvalInt(): base(SkillName.EvalInt, 105)
		{
		}
		[Constructable]
		public DJ_SW_EvalInt(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Evaluate Intelligence";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_EvalInt(Serial serial): base(serial)
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


	public class DJ_SW_Fencing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fencing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Fencing(): base(SkillName.Fencing, 105)
		{
		}
		[Constructable]
		public DJ_SW_Fencing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Fencing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Fencing(Serial serial): base(serial)
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


	public class DJ_SW_Fletching : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fletching
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Fletching(): base(SkillName.Fletching, 105)
		{
		}
		[Constructable]
		public DJ_SW_Fletching(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Fletching";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Fletching(Serial serial): base(serial)
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


	public class DJ_SW_Fishing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fishing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Fishing(): base(SkillName.Fishing, 105)
		{
		}
		[Constructable]
		public DJ_SW_Fishing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Fishing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Fishing(Serial serial): base(serial)
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


	public class DJ_SW_Healing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Healing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Healing(): base(SkillName.Healing, 105)
		{
		}
		[Constructable]
		public DJ_SW_Healing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Healing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Healing(Serial serial): base(serial)
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


	public class DJ_SW_Inscribe : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Inscribe
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Inscribe(): base(SkillName.Inscribe, 105)
		{
		}
		[Constructable]
		public DJ_SW_Inscribe(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Inscription";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Inscribe(Serial serial): base(serial)
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


	public class DJ_SW_Lockpicking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Lockpicking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Lockpicking(): base(SkillName.Lockpicking, 105)
		{
		}
		[Constructable]
		public DJ_SW_Lockpicking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Lockpicking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Lockpicking(Serial serial): base(serial)
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


	public class DJ_SW_Lumberjacking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Lumberjacking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Lumberjacking(): base(SkillName.Lumberjacking, 105)
		{
		}
		[Constructable]
		public DJ_SW_Lumberjacking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Lumberjacking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Lumberjacking(Serial serial): base(serial)
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


	public class DJ_SW_Macing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Macing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Macing(): base(SkillName.Macing, 105)
		{
		}
		[Constructable]
		public DJ_SW_Macing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Mace Fighting";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Macing(Serial serial): base(serial)
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


	public class DJ_SW_Magery : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Magery
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Magery(): base(SkillName.Magery, 105)
		{
		}
		[Constructable]
		public DJ_SW_Magery(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Magery";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Magery(Serial serial): base(serial)
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


	public class DJ_SW_MagicResist : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.MagicResist
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_MagicResist(): base(SkillName.MagicResist, 105)
		{
		}
		[Constructable]
		public DJ_SW_MagicResist(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Magic Resist";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_MagicResist(Serial serial): base(serial)
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


	public class DJ_SW_Meditation : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Meditation
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Meditation(): base(SkillName.Meditation, 105)
		{
		}
		[Constructable]
		public DJ_SW_Meditation(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Meditation";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Meditation(Serial serial): base(serial)
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


	public class DJ_SW_Mining : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Mining
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Mining(): base(SkillName.Mining, 105)
		{
		}
		[Constructable]
		public DJ_SW_Mining(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Mining";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Mining(Serial serial): base(serial)
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


	public class DJ_SW_Musicianship : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Musicianship
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Musicianship(): base(SkillName.Musicianship, 105)
		{
		}
		[Constructable]
		public DJ_SW_Musicianship(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Musicianship";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Musicianship(Serial serial): base(serial)
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


	public class DJ_SW_Necromancy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Necromancy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Necromancy(): base(SkillName.Necromancy, 105)
		{
		}
		[Constructable]
		public DJ_SW_Necromancy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Necromancy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Necromancy(Serial serial): base(serial)
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


	public class DJ_SW_Ninjitsu : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Ninjitsu
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Ninjitsu(): base(SkillName.Ninjitsu, 105)
		{
		}
		[Constructable]
		public DJ_SW_Ninjitsu(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Ninjitsu";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Ninjitsu(Serial serial): base(serial)
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


	public class DJ_SW_Parry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Parry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Parry(): base(SkillName.Parry, 105)
		{
		}
		[Constructable]
		public DJ_SW_Parry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Parry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Parry(Serial serial): base(serial)
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


	public class DJ_SW_Peacemaking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Peacemaking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Peacemaking(): base(SkillName.Peacemaking, 105)
		{
		}
		[Constructable]
		public DJ_SW_Peacemaking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Peacemaking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Peacemaking(Serial serial): base(serial)
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


	public class DJ_SW_Poisoning : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Poisoning
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Poisoning(): base(SkillName.Poisoning, 105)
		{
		}
		[Constructable]
		public DJ_SW_Poisoning(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Poisoning";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Poisoning(Serial serial): base(serial)
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


	public class DJ_SW_Provocation : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Provocation
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Provocation(): base(SkillName.Provocation, 105)
		{
		}
		[Constructable]
		public DJ_SW_Provocation(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Provocation";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Provocation(Serial serial): base(serial)
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


	public class DJ_SW_RemoveTrap : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.RemoveTrap
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_RemoveTrap(): base(SkillName.RemoveTrap, 105)
		{
		}
		[Constructable]
		public DJ_SW_RemoveTrap(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Remove Trap";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_RemoveTrap(Serial serial): base(serial)
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


	public class DJ_SW_Snooping : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Snooping
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Snooping(): base(SkillName.Snooping, 105)
		{
		}
		[Constructable]
		public DJ_SW_Snooping(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Snooping";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Snooping(Serial serial): base(serial)
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


	public class DJ_SW_SpiritSpeak : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.SpiritSpeak
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_SpiritSpeak(): base(SkillName.SpiritSpeak, 105)
		{
		}
		[Constructable]
		public DJ_SW_SpiritSpeak(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Spirit Speak";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_SpiritSpeak(Serial serial): base(serial)
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


	public class DJ_SW_Stealing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Stealing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Stealing(): base(SkillName.Stealing, 105)
		{
		}
		[Constructable]
		public DJ_SW_Stealing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Stealing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Stealing(Serial serial): base(serial)
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


	public class DJ_SW_Stealth : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Stealth
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Stealth(): base(SkillName.Stealth, 105)
		{
		}
		[Constructable]
		public DJ_SW_Stealth(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Stealth";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Stealth(Serial serial): base(serial)
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


	public class DJ_SW_Swords : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Swords
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Swords(): base(SkillName.Swords, 105)
		{
		}
		[Constructable]
		public DJ_SW_Swords(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Sword Fighting";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Swords(Serial serial): base(serial)
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


	public class DJ_SW_Tactics : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tactics
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Tactics(): base(SkillName.Tactics, 105)
		{
		}
		[Constructable]
		public DJ_SW_Tactics(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Tactics";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Tactics(Serial serial): base(serial)
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


	public class DJ_SW_Tailoring : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tailoring
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Tailoring(): base(SkillName.Tailoring, 105)
		{
		}
		[Constructable]
		public DJ_SW_Tailoring(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Tailoring";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Tailoring(Serial serial): base(serial)
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


	public class DJ_SW_Tinkering : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tinkering
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Tinkering(): base(SkillName.Tinkering, 105)
		{
		}
		[Constructable]
		public DJ_SW_Tinkering(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Tinkering";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Tinkering(Serial serial): base(serial)
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


	public class DJ_SW_Tracking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tracking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Tracking(): base(SkillName.Tracking, 105)
		{
		}
		[Constructable]
		public DJ_SW_Tracking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Tracking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Tracking(Serial serial): base(serial)
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


	public class DJ_SW_Veterinary : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Veterinary
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Veterinary(): base(SkillName.Veterinary, 105)
		{
		}
		[Constructable]
		public DJ_SW_Veterinary(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Veterinary";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Veterinary(Serial serial): base(serial)
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


	public class DJ_SW_Wrestling : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Wrestling
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Wrestling(): base(SkillName.Wrestling, 105)
		{
		}
		[Constructable]
		public DJ_SW_Wrestling(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Wrestling";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Wrestling(Serial serial): base(serial)
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

	public class DJ_SW_Anatomy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Anatomy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Anatomy(): base(SkillName.Anatomy, 105)
		{
		}
		[Constructable]
		public DJ_SW_Anatomy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Anatomy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Anatomy(Serial serial): base(serial)
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

	public class DJ_SW_Focus : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Focus
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SW_Focus(): base(SkillName.Focus, 105)
		{
		}
		[Constructable]
		public DJ_SW_Focus(SkillName skill, int value): base(0x14F0)
		{
			Name = "Wonderous Scroll of Focus";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SW_Focus(Serial serial): base(serial)
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
