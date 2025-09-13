using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Rudolph : BaseReindeer
	{
		[Constructable]
		public Rudolph() 
		{
			Name = "Rudolph";			
			Hue = 1810;
		}
				
		public Rudolph(Serial serial) : base(serial)
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
		

	public class Dasher : BaseReindeer
	{
		[Constructable]
		public Dasher() 
		{
			Name = "Dasher";			
			Hue = 1811;
		}
						
		public Dasher(Serial serial) : base(serial)
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
	
	public class Dancer : BaseReindeer
	{
		[Constructable]
		public Dancer() 
		{
			Name = "Dancer";			
			Hue = 1812;
		}
			
			
		public Dancer(Serial serial) : base(serial)
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
	
	public class Prancer : BaseReindeer
	{
		[Constructable]
		public Prancer() 
		{
			Name = "Prancer";		
			Hue = 1813;
		}
					
		public Prancer(Serial serial) : base(serial)
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
	
	public class Vixen : BaseReindeer
	{
		[Constructable]
		public Vixen() 
		{
			Name = "Vixen";		
			Hue = 1814;
		}
						
		public Vixen(Serial serial) : base(serial)
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
	
	public class Comet : BaseReindeer
	{
		[Constructable]
		public Comet() 
		{
			Name = "Comet";			
			Hue = 1815;
		}
						
		public Comet(Serial serial) : base(serial)
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
	
	public class Cupid : BaseReindeer
	{
		[Constructable]
		public Cupid() 
		{
			Name = "Cupid";		
			Hue = 1816;
		}
			
		public Cupid(Serial serial) : base(serial)
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
	
	public class Donner : BaseReindeer
	{
		[Constructable]
		public Donner() 
		{
			Name = "Donner";
			Hue = 1817;
		}			
		public Donner(Serial serial) : base(serial)
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
	
	public class Blitzen : BaseReindeer
	{
		[Constructable]
		public Blitzen() 
		{
			Name = "Blitzen";		
			Hue = 1818;
		}			
		public Blitzen(Serial serial) : base(serial)
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
