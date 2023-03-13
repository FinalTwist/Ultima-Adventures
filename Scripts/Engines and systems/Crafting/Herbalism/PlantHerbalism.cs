using System;
using Server.Items;
using Server;

namespace Server.Items
{
	public class PlantHerbalism_Leaf : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlantHerbalism_Leaf() : base( 0xF0E )
		{
			switch ( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: ItemID = 0x2FE0; Name = "dragon berry"; Hue = 0x845; break;
				case 1: ItemID = 0x2FE0; Name = "winter berry"; Hue = 0x47E; break;
				case 2: ItemID = 0xF88; Name = "earth stem"; Hue = 0x7D6; break;
				case 3: ItemID = 0x2066; Name = "tangle leaf"; Hue = 0x8A4; break;
				case 4: ItemID = 0x2066; Name = "eldritch leaf"; Hue = 0x48F; break;
			}
			Stackable = true;
		}

		public PlantHerbalism_Leaf( Serial serial ) : base( serial )
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

			if ( Name == "dragon berry" ){ Hue = 0x845; ItemID = 0x2FE0; }
			else if ( Name == "winter berry" ){ Hue = 0x47E; ItemID = 0x2FE0; }
			else if ( Name == "earth stem" ){ Hue = 0x7D6; ItemID = 0xF88; }
			else if ( Name == "tangle leaf" ){ Hue = 0x8A4; ItemID = 0x2066; }
			else if ( Name == "eldritch leaf" ){ Hue = 0x48F; ItemID = 0x2066; }
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class PlantHerbalism_Flower : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlantHerbalism_Flower() : base( 0xF0E )
		{
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: ItemID = 0x2FE8; Name = "lotus petal"; Hue = 0x1A0; break;
				case 1: ItemID = 0xF85; Name = "life root"; Hue = 0x8A4; break;
				case 2: ItemID = 0xF88; Name = "snake weed"; Hue = 0x89F; break;
			}
			Stackable = true;
		}

		public PlantHerbalism_Flower( Serial serial ) : base( serial )
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

			if ( Name == "lotus petal" ){ Hue = 0x1A0; ItemID = 0x2FE8; }
			else if ( Name == "life root" ){ Hue = 0x8A4; ItemID = 0xF85; }
			else if ( Name == "snake weed" ){ Hue = 0x89F; ItemID = 0xF88; }
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class PlantHerbalism_Mushroom : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlantHerbalism_Mushroom() : base( 0xB4D )
		{
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: ItemID = 0xB4D; Name = "white mushroom"; Hue = 0x47E; break;
				case 1: ItemID = 0xB4D; Name = "dark toadstool"; Hue = 0x96A; break;
				case 2: ItemID = 0xB4D; Name = "purple fungus"; Hue = 0x13C; break;
			}
			Stackable = true;
		}

		public PlantHerbalism_Mushroom( Serial serial ) : base( serial )
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

			if ( Name == "white mushroom" ){ Hue = 0x47E; ItemID = 0xB4D; }
			else if ( Name == "dark toadstool" ){ Hue = 0x96A; ItemID = 0xB4D; }
			else if ( Name == "purple fungus" ){ Hue = 0x13C; ItemID = 0xB4D; }
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class PlantHerbalism_Lilly : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlantHerbalism_Lilly() : base( 0xF0E )
		{
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: ItemID = 0x2066; Name = "frog bed leaf"; Hue = 0x89F; break;
				case 1: ItemID = 0x2FE8; Name = "lilly flower petal"; Hue = 0x47E; break;
				case 2: ItemID = 0x0A96; Name = "deep water stem"; Hue = 0x60; break;
			}
			Stackable = true;
		}

		public PlantHerbalism_Lilly( Serial serial ) : base( serial )
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

			if ( Name == "frog bed leaf" ){ Hue = 0x89F; ItemID = 0x2066; }
			else if ( Name == "lilly flower petal" ){ Hue = 0x47E; ItemID = 0x2FE8; }
			else if ( Name == "deep water stem" ){ Hue = 0x60; ItemID = 0x0A96; }
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class PlantHerbalism_Cactus : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlantHerbalism_Cactus() : base( 0xF0E )
		{
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: ItemID = 0xF86; Name = "desert root"; Hue = 0x54E; break;
				case 1: ItemID = 0x2065; Name = "cactus sponge"; Hue = 0x89F; break;
				case 2: ItemID = 0x2069; Name = "vampire thorn"; Hue = 0x485; break;
			}
			Stackable = true;
		}

		public PlantHerbalism_Cactus( Serial serial ) : base( serial )
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

			if ( Name == "desert root" ){ Hue = 0x54E; ItemID = 0xF86; }
			else if ( Name == "cactus sponge" ){ Hue = 0x89F; ItemID = 0x2065; }
			else if ( Name == "vampire thorn" ){ Hue = 0x485; ItemID = 0x2069; }
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class PlantHerbalism_Grass : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public PlantHerbalism_Grass() : base( 0xF0E )
		{
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: ItemID = 0x2064; Name = "forest hair"; Hue = 0x7D1; break;
				case 1: ItemID = 0x2067; Name = "fey seed"; Hue = 0x7D6; break;
				case 2: ItemID = 0x0A96; Name = "druidic blade"; Hue = 0x7D6; break;
			}
			Stackable = true;
		}

		public PlantHerbalism_Grass( Serial serial ) : base( serial )
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

			if ( Name == "forest hair" ){ Hue = 0x7D1; ItemID = 0x2064; }
			else if ( Name == "fey seed" ){ Hue = 0x7D6; ItemID = 0x2067; }
			else if ( Name == "druidic blade" ){ Hue = 0x7D6; ItemID = 0x0A96; }
		}
	}
}