using System;
using Server;

namespace Server.Items
{
	public class LeprechaunShirt : FancyShirt
	{
		[Constructable]
		public LeprechaunShirt()
		{
			Hue = 1150;
			Name = "leprechaun shirt";
			Attributes.Luck = 40;
			LootType = LootType.Blessed;
		}

		public LeprechaunShirt( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "St. Patrick's Day\t2005" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LeprechaunPants : LongPants
	{
		[Constructable]
		public LeprechaunPants()
		{
			Hue = 1436;
			Name = "leprechaun pants";
			Attributes.Luck = 40;
			LootType = LootType.Blessed;
		}

		public LeprechaunPants( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "St. Patrick's Day\t2005" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LeprechaunBoots : Boots
	{
		[Constructable]
		public LeprechaunBoots()
		{
			Hue = 1175;
			Name = "leprechaun boots";
			Attributes.Luck = 40;
			LootType = LootType.Blessed;
		}

		public LeprechaunBoots( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "St. Patrick's Day\t2005" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LeprechaunGloves : LeatherGloves
	{
		[Constructable]
		public LeprechaunGloves()
		{
			Hue = 1436;
			Name = "leprechaun gloves";
			Attributes.Luck = 40;
			LootType = LootType.Blessed;
			Identified = true;
		}

		public LeprechaunGloves( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "St. Patrick's Day\t2005" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LeprechaunHat : FeatheredHat
	{
		[Constructable]
		public LeprechaunHat()
		{
			Hue = 1436;
			Name = "leprechaun hat";
			Attributes.Luck = 40;
			LootType = LootType.Blessed;
		}

		public LeprechaunHat( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060662, "St. Patrick's Day\t2005" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}