using System;
using Server.Prompts;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable(2967,2968)]
	public class house_sign_sign_post_a : Item
	{
		[Constructable]
		public house_sign_sign_post_a() : base(2967)
		{
			Weight = 1.0;
			Name = "signpost";
			Movable = true;
		}

		public house_sign_sign_post_a(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable(2970,2969)]
	public class house_sign_sign_post_b : Item
	{
		[Constructable]
		public house_sign_sign_post_b() : base(2970)
		{
			Weight = 1.0;
			Name = "signpost";
			Movable = true;
		}

		public house_sign_sign_post_b(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable(3082,3081)]
	public class house_sign_sign_merc : Item
	{
		[Constructable]
		public house_sign_sign_merc() : base(3082)
		{
			Weight = 1.0;
			Name = "Merchant - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_merc(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_merc m_Sign;

			public RenamePrompt( house_sign_sign_merc sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3008,3007)]
	public class house_sign_sign_armor : Item
	{
		[Constructable]
		public house_sign_sign_armor() : base(3008)
		{
			Weight = 1.0;
			Name = "Armourer - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_armor(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_armor m_Sign;

			public RenamePrompt( house_sign_sign_armor sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2980,2979)]
	public class house_sign_sign_bake : Item
	{
		[Constructable]
		public house_sign_sign_bake() : base(2980)
		{
			Weight = 1.0;
			Name = "Bakery - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_bake(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_bake m_Sign;

			public RenamePrompt( house_sign_sign_bake sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3084,3083)]
	public class house_sign_sign_bank : Item
	{
		[Constructable]
		public house_sign_sign_bank() : base(3084)
		{
			Weight = 1.0;
			Name = "Gold - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_bank(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_bank m_Sign;

			public RenamePrompt( house_sign_sign_bank sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3004,3003)]
	public class house_sign_sign_bard : Item
	{
		[Constructable]
		public house_sign_sign_bard() : base(3004)
		{
			Weight = 1.0;
			Name = "Bard - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_bard(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_bard m_Sign;

			public RenamePrompt( house_sign_sign_bard sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3016,3015)]
	public class house_sign_sign_smith : Item
	{
		[Constructable]
		public house_sign_sign_smith() : base(3016)
		{
			Weight = 1.0;
			Name = "Blacksmith - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_smith(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_smith m_Sign;

			public RenamePrompt( house_sign_sign_smith sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3022,3021)]
	public class house_sign_sign_bow : Item
	{
		[Constructable]
		public house_sign_sign_bow() : base(3022)
		{
			Weight = 1.0;
			Name = "Bowyer - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_bow(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_bow m_Sign;

			public RenamePrompt( house_sign_sign_bow sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2998,2997)]
	public class house_sign_sign_ship : Item
	{
		[Constructable]
		public house_sign_sign_ship() : base(2998)
		{
			Weight = 1.0;
			Name = "Shipwright - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_ship(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_ship m_Sign;

			public RenamePrompt( house_sign_sign_ship sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3006,3005)]
	public class house_sign_sign_fletch : Item
	{
		[Constructable]
		public house_sign_sign_fletch() : base(3006)
		{
			Weight = 1.0;
			Name = "Fletcher - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_fletch(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_fletch m_Sign;

			public RenamePrompt( house_sign_sign_fletch sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2988,2987)]
	public class house_sign_sign_heal : Item
	{
		[Constructable]
		public house_sign_sign_heal() : base(2988)
		{
			Weight = 1.0;
			Name = "Healer - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_heal(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_heal m_Sign;

			public RenamePrompt( house_sign_sign_heal sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2996,2995)]
	public class house_sign_sign_inn : Item
	{
		[Constructable]
		public house_sign_sign_inn() : base(2996)
		{
			Weight = 1.0;
			Name = "Inn - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_inn(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_inn m_Sign;

			public RenamePrompt( house_sign_sign_inn sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3010,3009)]
	public class house_sign_sign_gem : Item
	{
		[Constructable]
		public house_sign_sign_gem() : base(3010)
		{
			Weight = 1.0;
			Name = "Jeweler - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_gem(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_gem m_Sign;

			public RenamePrompt( house_sign_sign_gem sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2966,2965)]
	public class house_sign_sign_book : Item
	{
		[Constructable]
		public house_sign_sign_book() : base(2966)
		{
			Weight = 1.0;
			Name = "Library - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_book(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_book m_Sign;

			public RenamePrompt( house_sign_sign_book sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2990,2989)]
	public class house_sign_sign_mage : Item
	{
		[Constructable]
		public house_sign_sign_mage() : base(2990)
		{
			Weight = 1.0;
			Name = "Wizard - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_mage(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_mage m_Sign;

			public RenamePrompt( house_sign_sign_mage sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3020,3019)]
	public class house_sign_sign_supply : Item
	{
		[Constructable]
		public house_sign_sign_supply() : base(3020)
		{
			Weight = 1.0;
			Name = "Supplies - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_supply(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_supply m_Sign;

			public RenamePrompt( house_sign_sign_supply sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3014,3013)]
	public class house_sign_sign_herb : Item
	{
		[Constructable]
		public house_sign_sign_herb() : base(3014)
		{
			Weight = 1.0;
			Name = "Reagents - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_herb(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_herb m_Sign;

			public RenamePrompt( house_sign_sign_herb sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3000,2999)]
	public class house_sign_sign_pen : Item
	{
		[Constructable]
		public house_sign_sign_pen() : base(3000)
		{
			Weight = 1.0;
			Name = "Stables - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_pen(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_pen m_Sign;

			public RenamePrompt( house_sign_sign_pen sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2982,2981)]
	public class house_sign_sign_sew : Item
	{
		[Constructable]
		public house_sign_sign_sew() : base(2982)
		{
			Weight = 1.0;
			Name = "Tailor - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_sew(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_sew m_Sign;

			public RenamePrompt( house_sign_sign_sew sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(3012,3011)]
	public class house_sign_sign_tavern : Item
	{
		[Constructable]
		public house_sign_sign_tavern() : base(3012)
		{
			Weight = 1.0;
			Name = "Tavern - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_tavern(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_tavern m_Sign;

			public RenamePrompt( house_sign_sign_tavern sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2984,2983)]
	public class house_sign_sign_tinker : Item
	{
		[Constructable]
		public house_sign_sign_tinker() : base(2984)
		{
			Weight = 1.0;
			Name = "Tinker - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_tinker(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_tinker m_Sign;

			public RenamePrompt( house_sign_sign_tinker sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2992,2991)]
	public class house_sign_sign_wood : Item
	{
		[Constructable]
		public house_sign_sign_wood() : base(2992)
		{
			Weight = 1.0;
			Name = "Carpenter - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_wood(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_wood m_Sign;

			public RenamePrompt( house_sign_sign_wood sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(2811,2812)]
	public class house_sign_sign_necro : Item
	{
		[Constructable]
		public house_sign_sign_necro() : base(2811)
		{
			Weight = 1.0;
			Name = "Necromancer - Double Click To Rename";
			Movable = true;
		}

		public house_sign_sign_necro(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Sign");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private house_sign_sign_necro m_Sign;

			public RenamePrompt( house_sign_sign_necro sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
			}
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
			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
////////////////////////////////////////////////////////////////////////
}