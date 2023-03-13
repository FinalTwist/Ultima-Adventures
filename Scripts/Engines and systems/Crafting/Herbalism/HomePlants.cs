using System;
using Server;

namespace Server.Items
{
	public class HomePlants_Flower : Item
	{
		public int HomePlantID;
		public string HomePlantName;
		
		[CommandProperty(AccessLevel.Owner)]
		public int HomePlant_ID { get { return HomePlantID; } set { HomePlantID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string HomePlant_Name { get { return HomePlantName; } set { HomePlantName = value; InvalidateProperties(); } }

		[Constructable]
		public HomePlants_Flower() : base( 0x4210 )
		{
			Weight = 2.0;
			Movable = true;

			if ( HomePlantID > 0 )
			{
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
			else
			{
				switch ( Utility.RandomMinMax( 0, 6 ) )
				{
					case 0: HomePlantName = "campion flower"; HomePlantID = Utility.RandomList( 0xC83, 0xC87, 0xC89 ); break;
					case 1: HomePlantName = "foxglove flower"; HomePlantID = Utility.RandomList( 0xC84, 0xC8A, 0x11CA ); break;
					case 2: HomePlantName = "orfluer flower"; HomePlantID = Utility.RandomList( 0xC85, 0xCC0, 0xCC1, 0x11CB ); break;
					case 3: HomePlantName = "red poppies"; HomePlantID = Utility.RandomList( 0xC86, 0xCBE, 0xCBF ); break;
					case 4: HomePlantName = "snowdrop"; HomePlantID = Utility.RandomList( 0xC88, 0xC8E ); break;
					case 5: HomePlantName = "white flower"; HomePlantID = Utility.RandomList( 0xC8B, 0xC8C ); break;
					case 6: HomePlantName = "white poppies"; HomePlantID = 0xC8D; break;
				}

				ItemID = HomePlantID;
				Name = HomePlantName;
			}
		}

		public HomePlants_Flower(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( HomePlantID );
            writer.Write( HomePlantName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            HomePlantID = reader.ReadInt();
            HomePlantName = reader.ReadString();
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class HomePlants_Leaf : Item
	{
		public int HomePlantID;
		public string HomePlantName;
		
		[CommandProperty(AccessLevel.Owner)]
		public int HomePlant_ID { get { return HomePlantID; } set { HomePlantID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string HomePlant_Name { get { return HomePlantName; } set { HomePlantName = value; InvalidateProperties(); } }

		[Constructable]
		public HomePlants_Leaf() : base( 0x4210 )
		{
			Weight = 2.0;
			Movable = true;

			if ( HomePlantID > 0 )
			{
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
			else
			{
				switch ( Utility.RandomMinMax( 0, 7 ) )
				{
					case 0: HomePlantName = "blade plant"; HomePlantID = 0xC93; break;
					case 1: HomePlantName = "bulrush"; HomePlantID = Utility.RandomList( 0xC94, 0xCA7 ); break;
					case 2: HomePlantName = "elephant ear plant"; HomePlantID = 0xC97; break;
					case 3: HomePlantName = "fan plant"; HomePlantID = 0xC98; break;
					case 4: HomePlantName = "snake plant"; HomePlantID = 0xCA9; break;
					case 5: HomePlantName = "fern"; HomePlantID = Utility.RandomList( 0xC9F, 0xCA0, 0xCA1, 0xCA2, 0xCA3, 0xCA4 ); break;
					case 6: HomePlantName = "pampas grass"; HomePlantID = 0xCA5; break;
					case 7: HomePlantName = "reeds"; HomePlantID = 0xD05; break;
				}

				ItemID = HomePlantID;
				Name = HomePlantName;
			}
		}

		public HomePlants_Leaf(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( HomePlantID );
            writer.Write( HomePlantName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            HomePlantID = reader.ReadInt();
            HomePlantName = reader.ReadString();
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class HomePlants_Cactus : Item
	{
		public int HomePlantID;
		public string HomePlantName;
		
		[CommandProperty(AccessLevel.Owner)]
		public int HomePlant_ID { get { return HomePlantID; } set { HomePlantID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string HomePlant_Name { get { return HomePlantName; } set { HomePlantName = value; InvalidateProperties(); } }

		[Constructable]
		public HomePlants_Cactus() : base( 0x4210 )
		{
			Weight = 2.0;
			Movable = true;

			if ( HomePlantID > 0 )
			{
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
			else
			{
				HomePlantName = "cactus"; HomePlantID = Utility.RandomList( 0xD25, 0xD26, 0xD27, 0xD28, 0xD2A, 0xD2B, 0xD2C, 0xD2E );

				ItemID = HomePlantID;
				Name = HomePlantName;
			}
		}

		public HomePlants_Cactus(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( HomePlantID );
            writer.Write( HomePlantName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            HomePlantID = reader.ReadInt();
            HomePlantName = reader.ReadString();
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class HomePlants_Lilly : Item
	{
		public int HomePlantID;
		public string HomePlantName;
		
		[CommandProperty(AccessLevel.Owner)]
		public int HomePlant_ID { get { return HomePlantID; } set { HomePlantID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string HomePlant_Name { get { return HomePlantName; } set { HomePlantName = value; InvalidateProperties(); } }

		[Constructable]
		public HomePlants_Lilly() : base( 0x4210 )
		{
			Weight = 2.0;
			Movable = true;

			if ( HomePlantID > 0 )
			{
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
			else
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: HomePlantName = "blade plant"; HomePlantID = 0xC93; break;
					case 1: HomePlantName = "snake plant"; HomePlantID = 0xCA9; break;
					case 2: HomePlantName = "water plant"; HomePlantID = 0xD04; break;
					case 3: HomePlantName = "cattails"; HomePlantID = Utility.RandomList( 0xCB7, 0xCB8 ); break;
					case 4: HomePlantName = "reeds"; HomePlantID = 0xD05; break;
					case 5: HomePlantName = "lily pad"; HomePlantID = Utility.RandomList( 0xD06, 0xD07, 0xD08, 0xD09, 0xD0A, 0xD0B, 0xDBC, 0xDBE, 0xDC1, 0xDC2, 0xDC3 ); break;
				}

				ItemID = HomePlantID;
				Name = HomePlantName;
			}
		}

		public HomePlants_Lilly(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( HomePlantID );
            writer.Write( HomePlantName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            HomePlantID = reader.ReadInt();
            HomePlantName = reader.ReadString();
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class HomePlants_Mushroom : Item
	{
		public int HomePlantID;
		public string HomePlantName;
		
		[CommandProperty(AccessLevel.Owner)]
		public int HomePlant_ID { get { return HomePlantID; } set { HomePlantID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string HomePlant_Name { get { return HomePlantName; } set { HomePlantName = value; InvalidateProperties(); } }

		[Constructable]
		public HomePlants_Mushroom() : base( 0x4210 )
		{
			Weight = 2.0;
			Movable = true;

			if ( HomePlantID > 0 )
			{
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
			else
			{
				HomePlantName = "mushroom"; HomePlantID = Utility.RandomList( 0xD0C, 0xD0D, 0xD0E, 0xD0F, 0xD10, 0xD11, 0xD12, 0xD13, 0xD14, 0xD15, 0xD16, 0xD17, 0xD18, 0xD19 );
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 1: HomePlantName = "large mushroom"; HomePlantID = Utility.RandomList( 0x222E, 0x222F, 0x2230, 0x2231 ); break;
				}

				ItemID = HomePlantID;
				Name = HomePlantName;
			}
		}

		public HomePlants_Mushroom(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( HomePlantID );
            writer.Write( HomePlantName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            HomePlantID = reader.ReadInt();
            HomePlantName = reader.ReadString();
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class HomePlants_Grass : Item
	{
		public int HomePlantID;
		public string HomePlantName;
		
		[CommandProperty(AccessLevel.Owner)]
		public int HomePlant_ID { get { return HomePlantID; } set { HomePlantID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string HomePlant_Name { get { return HomePlantName; } set { HomePlantName = value; InvalidateProperties(); } }

		[Constructable]
		public HomePlants_Grass() : base( 0x4210 )
		{
			Weight = 2.0;
			Movable = true;

			if ( HomePlantID > 0 )
			{
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
			else
			{
				HomePlantName = "grass"; HomePlantID = Utility.RandomList( 0xCBB, 0xCBA, 0xCB9, 0xCC6, 0xD32, 0xD33 );
				ItemID = HomePlantID;
				Name = HomePlantName;
			}
		}

		public HomePlants_Grass(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( HomePlantID );
            writer.Write( HomePlantName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            HomePlantID = reader.ReadInt();
            HomePlantName = reader.ReadString();
		}
	}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable( 0x113F, 0x1150 )]
	public class HangingPlantA : Item
	{
		[Constructable]
		public HangingPlantA() : base( 0x113F )
		{
			Weight = 10;
			Name = "hanging plant";
		}

		public HangingPlantA(Serial serial) : base(serial)
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
		}
	}

	[Furniture]
	[Flipable( 0x1151, 0x1152 )]
	public class HangingPlantB : Item
	{
		[Constructable]
		public HangingPlantB() : base( 0x1151 )
		{
			Weight = 10;
			Name = "hanging plant";
		}

		public HangingPlantB(Serial serial) : base(serial)
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
		}
	}

	[Furniture]
	[Flipable( 0x1164, 0x118A )]
	public class HangingPlantC : Item
	{
		[Constructable]
		public HangingPlantC() : base( 0x1164 )
		{
			Weight = 10;
			Name = "hanging plant";
		}

		public HangingPlantC(Serial serial) : base(serial)
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
		}
	}
}