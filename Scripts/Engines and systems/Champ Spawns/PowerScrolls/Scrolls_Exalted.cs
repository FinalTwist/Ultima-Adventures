using System;
using Server;
using Server.Engines.Craft;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class DJ_SE_Anatomy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Anatomy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Anatomy(): base(SkillName.Anatomy, 110)
		{
		}
		[Constructable]
		public DJ_SE_Anatomy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Anatomy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Anatomy(Serial serial): base(serial)
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

	public class DJ_SE_Alchemy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Alchemy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Alchemy(): base(SkillName.Alchemy, 110)
		{
		}
		[Constructable]
		public DJ_SE_Alchemy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Alchemy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Alchemy(Serial serial): base(serial)
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

	public class DJ_SE_Focus : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Focus
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Focus(): base(SkillName.Focus, 110)
		{
		}
		[Constructable]
		public DJ_SE_Focus(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Focus";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Focus(Serial serial): base(serial)
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

	public class DJ_SE_Hiding : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Hiding
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Hiding(): base(SkillName.Hiding, 110)
		{
		}
		[Constructable]
		public DJ_SE_Hiding(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Hiding";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Hiding(Serial serial): base(serial)
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



	public class DJ_SE_AnimalLore : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.AnimalLore
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_AnimalLore(): base(SkillName.AnimalLore, 110)
		{
		}
		[Constructable]
		public DJ_SE_AnimalLore(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Animal Lore";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_AnimalLore(Serial serial): base(serial)
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


	public class DJ_SE_AnimalTaming : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.AnimalTaming
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_AnimalTaming(): base(SkillName.AnimalTaming, 110)
		{
		}
		[Constructable]
		public DJ_SE_AnimalTaming(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Animal Taming";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_AnimalTaming(Serial serial): base(serial)
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


	public class DJ_SE_Archery : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Archery
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Archery(): base(SkillName.Archery, 110)
		{
		}
		[Constructable]
		public DJ_SE_Archery(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Archery";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Archery(Serial serial): base(serial)
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


	public class DJ_SE_ArmsLore : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.ArmsLore
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_ArmsLore(): base(SkillName.ArmsLore, 110)
		{
		}
		[Constructable]
		public DJ_SE_ArmsLore(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Arms Lore";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_ArmsLore(Serial serial): base(serial)
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


	public class DJ_SE_Blacksmith : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Blacksmith
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Blacksmith(): base(SkillName.Blacksmith, 110)
		{
		}
		[Constructable]
		public DJ_SE_Blacksmith(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Blacksmithing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Blacksmith(Serial serial): base(serial)
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


	public class DJ_SE_Bushido : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Bushido
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Bushido(): base(SkillName.Bushido, 110)
		{
		}
		[Constructable]
		public DJ_SE_Bushido(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Bushido";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Bushido(Serial serial): base(serial)
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


	public class DJ_SE_Carpentry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Carpentry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Carpentry(): base(SkillName.Carpentry, 110)
		{
		}
		[Constructable]
		public DJ_SE_Carpentry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Carpentry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Carpentry(Serial serial): base(serial)
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


	public class DJ_SE_Cartography : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Cartography
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Cartography(): base(SkillName.Cartography, 110)
		{
		}
		[Constructable]
		public DJ_SE_Cartography(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Cartography";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Cartography(Serial serial): base(serial)
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


	public class DJ_SE_Chivalry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Chivalry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Chivalry(): base(SkillName.Chivalry, 110)
		{
		}
		[Constructable]
		public DJ_SE_Chivalry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Chivalry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Chivalry(Serial serial): base(serial)
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


	public class DJ_SE_Cooking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Cooking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Cooking(): base(SkillName.Cooking, 110)
		{
		}
		[Constructable]
		public DJ_SE_Cooking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Cooking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Cooking(Serial serial): base(serial)
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


	public class DJ_SE_DetectHidden : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.DetectHidden
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_DetectHidden(): base(SkillName.DetectHidden, 110)
		{
		}
		[Constructable]
		public DJ_SE_DetectHidden(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Detect Hidden";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_DetectHidden(Serial serial): base(serial)
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


	public class DJ_SE_Discordance : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Discordance
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Discordance(): base(SkillName.Discordance, 110)
		{
		}
		[Constructable]
		public DJ_SE_Discordance(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Discordance";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Discordance(Serial serial): base(serial)
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


	public class DJ_SE_EvalInt : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.EvalInt
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_EvalInt(): base(SkillName.EvalInt, 110)
		{
		}
		[Constructable]
		public DJ_SE_EvalInt(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Evaluate Intelligence";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_EvalInt(Serial serial): base(serial)
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


	public class DJ_SE_Fencing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fencing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Fencing(): base(SkillName.Fencing, 110)
		{
		}
		[Constructable]
		public DJ_SE_Fencing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Fencing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Fencing(Serial serial): base(serial)
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


	public class DJ_SE_Fletching : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fletching
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Fletching(): base(SkillName.Fletching, 110)
		{
		}
		[Constructable]
		public DJ_SE_Fletching(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Fletching";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Fletching(Serial serial): base(serial)
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


	public class DJ_SE_Fishing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Fishing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Fishing(): base(SkillName.Fishing, 110)
		{
		}
		[Constructable]
		public DJ_SE_Fishing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Fishing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Fishing(Serial serial): base(serial)
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


	public class DJ_SE_Healing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Healing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Healing(): base(SkillName.Healing, 110)
		{
		}
		[Constructable]
		public DJ_SE_Healing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Healing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Healing(Serial serial): base(serial)
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


	public class DJ_SE_Inscribe : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Inscribe
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Inscribe(): base(SkillName.Inscribe, 110)
		{
		}
		[Constructable]
		public DJ_SE_Inscribe(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Inscription";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Inscribe(Serial serial): base(serial)
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


	public class DJ_SE_Lockpicking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Lockpicking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Lockpicking(): base(SkillName.Lockpicking, 110)
		{
		}
		[Constructable]
		public DJ_SE_Lockpicking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Lockpicking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Lockpicking(Serial serial): base(serial)
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


	public class DJ_SE_Lumberjacking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Lumberjacking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Lumberjacking(): base(SkillName.Lumberjacking, 110)
		{
		}
		[Constructable]
		public DJ_SE_Lumberjacking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Lumberjacking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Lumberjacking(Serial serial): base(serial)
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


	public class DJ_SE_Macing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Macing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Macing(): base(SkillName.Macing, 110)
		{
		}
		[Constructable]
		public DJ_SE_Macing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Mace Fighting";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Macing(Serial serial): base(serial)
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


	public class DJ_SE_Magery : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Magery
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Magery(): base(SkillName.Magery, 110)
		{
		}
		[Constructable]
		public DJ_SE_Magery(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Magery";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Magery(Serial serial): base(serial)
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


	public class DJ_SE_MagicResist : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.MagicResist
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_MagicResist(): base(SkillName.MagicResist, 110)
		{
		}
		[Constructable]
		public DJ_SE_MagicResist(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Magic Resist";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_MagicResist(Serial serial): base(serial)
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


	public class DJ_SE_Meditation : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Meditation
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Meditation(): base(SkillName.Meditation, 110)
		{
		}
		[Constructable]
		public DJ_SE_Meditation(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Meditation";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Meditation(Serial serial): base(serial)
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


	public class DJ_SE_Mining : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Mining
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Mining(): base(SkillName.Mining, 110)
		{
		}
		[Constructable]
		public DJ_SE_Mining(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Mining";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Mining(Serial serial): base(serial)
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


	public class DJ_SE_Musicianship : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Musicianship
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Musicianship(): base(SkillName.Musicianship, 110)
		{
		}
		[Constructable]
		public DJ_SE_Musicianship(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Musicianship";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Musicianship(Serial serial): base(serial)
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


	public class DJ_SE_Necromancy : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Necromancy
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Necromancy(): base(SkillName.Necromancy, 110)
		{
		}
		[Constructable]
		public DJ_SE_Necromancy(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Necromancy";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Necromancy(Serial serial): base(serial)
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


	public class DJ_SE_Ninjitsu : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Ninjitsu
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Ninjitsu(): base(SkillName.Ninjitsu, 110)
		{
		}
		[Constructable]
		public DJ_SE_Ninjitsu(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Ninjitsu";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Ninjitsu(Serial serial): base(serial)
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


	public class DJ_SE_Parry : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Parry
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Parry(): base(SkillName.Parry, 110)
		{
		}
		[Constructable]
		public DJ_SE_Parry(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Parry";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Parry(Serial serial): base(serial)
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


	public class DJ_SE_Peacemaking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Peacemaking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Peacemaking(): base(SkillName.Peacemaking, 110)
		{
		}
		[Constructable]
		public DJ_SE_Peacemaking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Peacemaking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Peacemaking(Serial serial): base(serial)
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


	public class DJ_SE_Poisoning : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Poisoning
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Poisoning(): base(SkillName.Poisoning, 110)
		{
		}
		[Constructable]
		public DJ_SE_Poisoning(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Poisoning";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Poisoning(Serial serial): base(serial)
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


	public class DJ_SE_Provocation : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Provocation
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Provocation(): base(SkillName.Provocation, 110)
		{
		}
		[Constructable]
		public DJ_SE_Provocation(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Provocation";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Provocation(Serial serial): base(serial)
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


	public class DJ_SE_RemoveTrap : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.RemoveTrap
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_RemoveTrap(): base(SkillName.RemoveTrap, 110)
		{
		}
		[Constructable]
		public DJ_SE_RemoveTrap(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Remove Trap";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_RemoveTrap(Serial serial): base(serial)
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


	public class DJ_SE_Snooping : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Snooping
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Snooping(): base(SkillName.Snooping, 110)
		{
		}
		[Constructable]
		public DJ_SE_Snooping(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Snooping";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Snooping(Serial serial): base(serial)
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


	public class DJ_SE_SpiritSpeak : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.SpiritSpeak
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_SpiritSpeak(): base(SkillName.SpiritSpeak, 110)
		{
		}
		[Constructable]
		public DJ_SE_SpiritSpeak(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Spirit Speak";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_SpiritSpeak(Serial serial): base(serial)
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


	public class DJ_SE_Stealing : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Stealing
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Stealing(): base(SkillName.Stealing, 110)
		{
		}
		[Constructable]
		public DJ_SE_Stealing(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Stealing";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Stealing(Serial serial): base(serial)
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


	public class DJ_SE_Stealth : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Stealth
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Stealth(): base(SkillName.Stealth, 110)
		{
		}
		[Constructable]
		public DJ_SE_Stealth(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Stealth";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Stealth(Serial serial): base(serial)
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


	public class DJ_SE_Swords : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Swords
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Swords(): base(SkillName.Swords, 110)
		{
		}
		[Constructable]
		public DJ_SE_Swords(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Sword Fighting";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Swords(Serial serial): base(serial)
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


	public class DJ_SE_Tactics : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tactics
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Tactics(): base(SkillName.Tactics, 110)
		{
		}
		[Constructable]
		public DJ_SE_Tactics(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Tactics";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Tactics(Serial serial): base(serial)
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


	public class DJ_SE_Tailoring : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tailoring
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Tailoring(): base(SkillName.Tailoring, 110)
		{
		}
		[Constructable]
		public DJ_SE_Tailoring(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Tailoring";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Tailoring(Serial serial): base(serial)
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


	public class DJ_SE_Tinkering : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tinkering
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Tinkering(): base(SkillName.Tinkering, 110)
		{
		}
		[Constructable]
		public DJ_SE_Tinkering(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Tinkering";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Tinkering(Serial serial): base(serial)
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


	public class DJ_SE_Tracking : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Tracking
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Tracking(): base(SkillName.Tracking, 110)
		{
		}
		[Constructable]
		public DJ_SE_Tracking(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Tracking";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Tracking(Serial serial): base(serial)
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


	public class DJ_SE_Veterinary : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Veterinary
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Veterinary(): base(SkillName.Veterinary, 110)
		{
		}
		[Constructable]
		public DJ_SE_Veterinary(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Veterinary";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Veterinary(Serial serial): base(serial)
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


	public class DJ_SE_Wrestling : PowerScroll
	{
		private SkillName m_Skill;
		private int m_Value;
		private static SkillName[] m_Skills = new SkillName[]
		{
			SkillName.Wrestling
		};
		public static SkillName[] Skills { get { return m_Skills; } }
		[Constructable]
		public DJ_SE_Wrestling(): base(SkillName.Wrestling, 110)
		{
		}
		[Constructable]
		public DJ_SE_Wrestling(SkillName skill, int value): base(0x14F0)
		{
			Name = "Exalted Scroll of Wrestling";
			Hue = 0x481;
			Weight = 1.0;
			LootType = LootType.Cursed;
			m_Skill = skill;
			m_Value = value;
		}

		public DJ_SE_Wrestling(Serial serial): base(serial)
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
