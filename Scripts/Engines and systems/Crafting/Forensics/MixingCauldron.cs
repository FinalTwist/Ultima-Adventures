using System;
using Server.Network;
using Server.Items;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class MixingCauldron : Item
	{
		public string MyIngredient1; // FIRST JAR FROM FORENSICS
		public string MyIngredient2; // SECOND JAR FROM FORENSICS
		public string MyIngredient3; // SOME REAGENT
		public string MyIngredient4; // BODY PART
		public int MyReagent; // HOW RARE IS THE REAGENT
		public int MyWater; // NEEDS A WATERSKIN
		public string MyPotion; // WHICH POTION DID IT MAKE
		public int MyFill; // HOW MANY JARS DID THEY FILL FROM THE RESULTS
		public string MyCauldron; // WHAT DID IT MAKE

		public string MyRecipe01;
		public string MyRecipe02;
		public string MyRecipe03;
		public string MyRecipe04;
		public string MyRecipe05;
		public string MyRecipe06;
		public string MyRecipe07;
		public string MyRecipe08;
		public string MyRecipe09;
		public string MyRecipe10;
		public string MyRecipe11;
		public string MyRecipe12;
		public string MyRecipe13;
		public string MyRecipe14;
		public string MyRecipe15;
		public string MyRecipe16;

		[CommandProperty(AccessLevel.Owner)]
		public string My_Ingredient1 { get { return MyIngredient1; } set { MyIngredient1 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Ingredient2 { get { return MyIngredient2; } set { MyIngredient2 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Ingredient3 { get { return MyIngredient3; } set { MyIngredient3 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Ingredient4 { get { return MyIngredient4; } set { MyIngredient4 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int My_Reagent { get { return MyReagent; } set { MyReagent = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int My_Water { get { return MyWater; } set { MyWater = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int My_Fill { get { return MyFill; } set { MyFill = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Cauldron { get { return MyCauldron; } set { MyCauldron = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe01 { get { return MyRecipe01; } set { MyRecipe01 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe02 { get { return MyRecipe02; } set { MyRecipe02 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe03 { get { return MyRecipe03; } set { MyRecipe03 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe04 { get { return MyRecipe04; } set { MyRecipe04 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe05 { get { return MyRecipe05; } set { MyRecipe05 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe06 { get { return MyRecipe06; } set { MyRecipe06 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe07 { get { return MyRecipe07; } set { MyRecipe07 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe08 { get { return MyRecipe08; } set { MyRecipe08 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe09 { get { return MyRecipe09; } set { MyRecipe09 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe10 { get { return MyRecipe10; } set { MyRecipe10 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe11 { get { return MyRecipe11; } set { MyRecipe11 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe12 { get { return MyRecipe12; } set { MyRecipe12 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe13 { get { return MyRecipe13; } set { MyRecipe13 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe14 { get { return MyRecipe14; } set { MyRecipe14 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe15 { get { return MyRecipe15; } set { MyRecipe15 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string My_Recipe16 { get { return MyRecipe16; } set { MyRecipe16 = value; InvalidateProperties(); } }

		[Constructable]
		public MixingCauldron() : base( 0x269C )
		{
			Weight = 20.0;
			Name = "mixing cauldron";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			int Known = 0;

			if ( MyRecipe01 != "" && MyRecipe01 != null ){ Known = Known + 1; }
			if ( MyRecipe02 != "" && MyRecipe02 != null ){ Known = Known + 1; }
			if ( MyRecipe03 != "" && MyRecipe03 != null ){ Known = Known + 1; }
			if ( MyRecipe04 != "" && MyRecipe04 != null ){ Known = Known + 1; }
			if ( MyRecipe05 != "" && MyRecipe05 != null ){ Known = Known + 1; }
			if ( MyRecipe06 != "" && MyRecipe06 != null ){ Known = Known + 1; }
			if ( MyRecipe07 != "" && MyRecipe07 != null ){ Known = Known + 1; }
			if ( MyRecipe08 != "" && MyRecipe08 != null ){ Known = Known + 1; }
			if ( MyRecipe09 != "" && MyRecipe09 != null ){ Known = Known + 1; }
			if ( MyRecipe10 != "" && MyRecipe10 != null ){ Known = Known + 1; }
			if ( MyRecipe11 != "" && MyRecipe11 != null ){ Known = Known + 1; }
			if ( MyRecipe12 != "" && MyRecipe12 != null ){ Known = Known + 1; }
			if ( MyRecipe13 != "" && MyRecipe13 != null ){ Known = Known + 1; }
			if ( MyRecipe14 != "" && MyRecipe14 != null ){ Known = Known + 1; }
			if ( MyRecipe15 != "" && MyRecipe15 != null ){ Known = Known + 1; }
			if ( MyRecipe16 != "" && MyRecipe16 != null ){ Known = Known + 1; }

			if ( Known == 1 ){ list.Add( 1049644, "Seasoned With " + Known.ToString() + " Recipe" ); }
			else if ( Known > 1 ){ list.Add( 1049644, "Seasoned With " + Known.ToString() + " Recipes" ); }
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001);
			}
			else if ( MyIngredient1 == "" || MyIngredient1 == null )
			{
				from.SendMessage( "Choose an ingredient you obtained with your surgeons knife, to put in the cauldron." );
				from.Target = new IngredientTarget( this, 1 );
			}
			else if ( MyIngredient2 == "" || MyIngredient2 == null )
			{
				from.SendMessage( "Choose a different ingredient you obtained with your surgeons knife, to put in the cauldron." );
				from.Target = new IngredientTarget( this, 2 );
			}
			else if ( MyIngredient3 == "" || MyIngredient3 == null )
			{
				from.SendMessage( "Choose a magical reagent to put in the cauldron." );
				from.Target = new IngredientTarget( this, 3 );
			}
			else if ( MyIngredient4 == "" || MyIngredient4 == null )
			{
				from.SendMessage( "Choose a body part or bones to put in the cauldron." );
				from.Target = new IngredientTarget( this, 4 );
			}
			else if ( MyWater == 0 )
			{
				from.SendMessage( "Choose a waterskin to dump into the cauldron." );
				from.Target = new IngredientTarget( this, 5 );
			}
			else if ( MyFill == 0 )
			{
				from.SendMessage( "Choose a cauldron mixer to stir the liquid with." );
				from.Target = new IngredientTarget( this, 6 );
			}
			else if ( MyFill > 0 )
			{
				from.SendMessage( "Choose a jar to fill from the cauldron." );
				from.Target = new IngredientTarget( this, 7 );
			}
		}

		public static void CheckFlame( Mobile from, int range, out bool burning )
		{
			burning = false;
			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				bool isFlame = ( ( item.ItemID >= 0xDE3 && item.ItemID <= 0xDE9 ) || ( item.ItemID >= 0x461 && item.ItemID <= 0x48E ) || ( item.ItemID >= 0x92B && item.ItemID <= 0x96C ) || ( item.ItemID == 0xFAC ) || ( item.ItemID >= 0x184A && item.ItemID <= 0x184C ) || ( item.ItemID >= 0x184E && item.ItemID <= 0x1850 ) || ( item.ItemID >= 0x398C && item.ItemID <= 0x399F ) );

				if ( isFlame )
				{
					if ( (from.Z + 16) < item.Z || (item.Z + 16) < from.Z || !from.InLOS( item ) )
						continue;

					burning = burning || isFlame;

					if ( burning )
						break;
				}
			}

			eable.Free();

			for ( int x = -range; (!burning) && x <= range; ++x )
			{
				for ( int y = -range; (!burning) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; (!burning) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID;

						bool isFlame = ( ( id >= 0xDE3 && id <= 0xDE9 ) || ( id >= 0x461 && id <= 0x48E ) || ( id >= 0x92B && id <= 0x96C ) || ( id == 0xFAC ) || ( id >= 0x184A && id <= 0x184C ) || ( id >= 0x184E && id <= 0x1850 ) || ( id >= 0x398C && id <= 0x399F ) );

						if ( isFlame )
						{
							if ( (from.Z + 16) < tiles[i].Z || (tiles[i].Z + 16) < from.Z || !from.InLOS( new Point3D( from.X+x, from.Y+y, tiles[i].Z + (tiles[i].Height/2) + 1 ) ) )
								continue;

							burning = burning || isFlame;
						}
					}
				}
			}
		}

		public MixingCauldron( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( MyIngredient1 );
			writer.Write( MyIngredient2 );
			writer.Write( MyIngredient3 );
			writer.Write( MyIngredient4 );
			writer.Write( MyReagent );
			writer.Write( MyWater );
			writer.Write( MyPotion );
			writer.Write( MyFill );
			writer.Write( MyCauldron );
			writer.Write( MyRecipe01 );
			writer.Write( MyRecipe02 );
			writer.Write( MyRecipe03 );
			writer.Write( MyRecipe04 );
			writer.Write( MyRecipe05 );
			writer.Write( MyRecipe06 );
			writer.Write( MyRecipe07 );
			writer.Write( MyRecipe08 );
			writer.Write( MyRecipe09 );
			writer.Write( MyRecipe10 );
			writer.Write( MyRecipe11 );
			writer.Write( MyRecipe12 );
			writer.Write( MyRecipe13 );
			writer.Write( MyRecipe14 );
			writer.Write( MyRecipe15 );
			writer.Write( MyRecipe16 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			MyIngredient1 = reader.ReadString();
			MyIngredient2 = reader.ReadString();
			MyIngredient3 = reader.ReadString();
			MyIngredient4 = reader.ReadString();
			MyReagent = reader.ReadInt();
			MyWater = reader.ReadInt();
			MyPotion = reader.ReadString();
			MyFill = reader.ReadInt();
			MyCauldron = reader.ReadString();
			MyRecipe01 = reader.ReadString();
			MyRecipe02 = reader.ReadString();
			MyRecipe03 = reader.ReadString();
			MyRecipe04 = reader.ReadString();
			MyRecipe05 = reader.ReadString();
			MyRecipe06 = reader.ReadString();
			MyRecipe07 = reader.ReadString();
			MyRecipe08 = reader.ReadString();
			MyRecipe09 = reader.ReadString();
			MyRecipe10 = reader.ReadString();
			MyRecipe11 = reader.ReadString();
			MyRecipe12 = reader.ReadString();
			MyRecipe13 = reader.ReadString();
			MyRecipe14 = reader.ReadString();
			MyRecipe15 = reader.ReadString();
			MyRecipe16 = reader.ReadString();

			ItemID = 0x269C;
		}

		public class NecroticGump : Gump
		{
			public NecroticGump( Mobile from, MixingCauldron bowl ): base( 25, 25 )
			{
				int Known = 0;
				string sKnown = "";
				if ( bowl.MyRecipe01 != "" && bowl.MyRecipe01 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe02 != "" && bowl.MyRecipe02 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe03 != "" && bowl.MyRecipe03 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe04 != "" && bowl.MyRecipe04 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe05 != "" && bowl.MyRecipe05 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe06 != "" && bowl.MyRecipe06 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe07 != "" && bowl.MyRecipe07 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe08 != "" && bowl.MyRecipe08 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe09 != "" && bowl.MyRecipe09 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe10 != "" && bowl.MyRecipe10 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe11 != "" && bowl.MyRecipe11 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe12 != "" && bowl.MyRecipe12 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe13 != "" && bowl.MyRecipe13 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe14 != "" && bowl.MyRecipe14 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe15 != "" && bowl.MyRecipe15 != null ){ Known = Known + 1; }
				if ( bowl.MyRecipe16 != "" && bowl.MyRecipe16 != null ){ Known = Known + 1; }

				if ( Known == 1 ){ sKnown = "Seasoned With " + Known.ToString() + " Recipe"; }
				else if ( Known > 1 ){ sKnown = "Seasoned With " + Known.ToString() + " Recipes"; }
				else { sKnown = "No Recipes Seasoned Yet"; }

				string MyInfo = "" + sKnown + "<br><br>";
				int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";

				if ( bowl.MyRecipe01 != "" && bowl.MyRecipe01 != null )
				{
					string[] words = bowl.MyRecipe01.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe02 != "" && bowl.MyRecipe02 != null )
				{
					string[] words = bowl.MyRecipe02.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe03 != "" && bowl.MyRecipe03 != null )
				{
					string[] words = bowl.MyRecipe03.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe04 != "" && bowl.MyRecipe04 != null )
				{
					string[] words = bowl.MyRecipe04.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe05 != "" && bowl.MyRecipe05 != null )
				{
					string[] words = bowl.MyRecipe05.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe06 != "" && bowl.MyRecipe06 != null )
				{
					string[] words = bowl.MyRecipe06.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe07 != "" && bowl.MyRecipe07 != null )
				{
					string[] words = bowl.MyRecipe07.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe08 != "" && bowl.MyRecipe08 != null )
				{
					string[] words = bowl.MyRecipe08.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe09 != "" && bowl.MyRecipe09 != null )
				{
					string[] words = bowl.MyRecipe09.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe10 != "" && bowl.MyRecipe10 != null )
				{
					string[] words = bowl.MyRecipe10.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe11 != "" && bowl.MyRecipe11 != null )
				{
					string[] words = bowl.MyRecipe11.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe12 != "" && bowl.MyRecipe12 != null )
				{
					string[] words = bowl.MyRecipe12.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe13 != "" && bowl.MyRecipe13 != null )
				{
					string[] words = bowl.MyRecipe13.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe14 != "" && bowl.MyRecipe14 != null )
				{
					string[] words = bowl.MyRecipe14.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe15 != "" && bowl.MyRecipe15 != null )
				{
					string[] words = bowl.MyRecipe15.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}
				cycle=0; sR1=""; sR2=""; sR3=""; sR4=""; sR5="";
				if ( bowl.MyRecipe16 != "" && bowl.MyRecipe16 != null )
				{
					string[] words = bowl.MyRecipe16.Split('|');
					foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
					MyInfo = MyInfo + "To make " + sR5 + ", you will need a " + sR1 + ", a " + sR2 + ", a " + sR3 + ", a waterskin, and some " + sR4 + ".<br><br>";
				}

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 154);
				AddImage(300, 0, 154);
				AddImage(0, 300, 154);
				AddImage(300, 300, 154);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 297, 129);
				AddImage(197, 46, 132);
				AddImage(136, 554, 132);
				AddImage(8, 516, 139);
				AddImage(7, 7, 133);
				AddImage(541, 7, 131);
				AddImage(521, 201, 159);
				AddImage(498, 43, 143);
				AddHtml( 113, 138, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + MyInfo + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
				AddHtml( 114, 89, 397, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>NECROTIC ALCHEMY</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(41, 499, 7719);
				AddItem(29, 460, 10287);
				AddItem(36, 469, 10287);
				AddItem(46, 460, 10287);
				AddItem(45, 380, 9884);
				AddItem(45, 424, 8787, 2117);
				AddImage(379, 516, 134);
			}
		}

		private class IngredientTarget : Target
		{
			private MixingCauldron m_Pot;
			private int m_Stage;

			public IngredientTarget( MixingCauldron pot, int nStage ) : base( 1, false, TargetFlags.None )
			{
				m_Pot = pot;
				m_Stage = nStage;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Pot.Deleted )
					return;

				object obj = targeted;

				bool burning;
				CheckFlame( from, 2, out burning );

				if ( targeted == from )
				{
					if ( m_Stage > 1 )
					{
						from.SendMessage( "You abandon your efforts and dump out the cauldron." );
						from.PlaySound( 0x026 );
					}
					else
					{
						from.SendMessage( "This cauldron is used for necrotic alchemy." );
					}
					m_Pot.ItemID = 0x269C;
					m_Pot.MyIngredient1 = "";
					m_Pot.MyIngredient2 = "";
					m_Pot.MyIngredient3 = "";
					m_Pot.MyIngredient4 = "";
					m_Pot.MyReagent = 0;
					m_Pot.MyWater = 0;
					m_Pot.MyPotion = "";
					m_Pot.MyFill = 0;
					m_Pot.Hue = 0;
					m_Pot.MyCauldron = "";
					m_Pot.Name = "mixing cauldron";

					return;
				}
				if ( !(targeted is Item) )
				{
					from.SendMessage( "You should get a Necrotic Alchemy book from a sage or necromancer." );
					return;
				}

				Item itemz = (Item)targeted;

				if ( targeted == m_Pot )
				{
					from.SendGump( new NecroticGump( from, m_Pot ) );
				}
				else if ( !itemz.IsChildOf( from.Backpack ) ) 
				{
					from.SendMessage( "That must be in your backpack to use." );
				}
				else if ( ( from.Skills[SkillName.Forensics].Value < 40 ) && ( m_Stage != 7 ) )
				{
					from.SendMessage( "You need at least a 40 skill in forensic evaluation to concoct anything with this!" ); 
				}
				else if ( ( !burning ) && ( m_Stage != 7 ) )
				{
					from.SendMessage( "You need a heat source to cook with this cauldron!" ); 
				}
				else if ( m_Stage == 1 && obj is BottleOfParts )
				{
					BottleOfParts jar = (BottleOfParts)obj;
					m_Pot.MyIngredient1 = jar.Name;
					if ( jar.Amount > 1 ){ jar.Amount = jar.Amount - 1; } else { jar.Delete(); }
					from.AddToBackpack( new Jar() );
					from.SendMessage( "You pour the " + jar.Name + " into the cauldron." );
					from.PlaySound( 0x04E );
					m_Pot.ItemID = 0x269F;
					m_Pot.Hue = RandomThings.GetRandomColor(0);
				}
				else if ( m_Stage == 2 && obj is BottleOfParts )
				{
					BottleOfParts jar = (BottleOfParts)obj;
					if ( m_Pot.MyIngredient1 == jar.Name )
					{
						from.SendMessage( "You already put that in there. Try a different ingredient." );
					}
					else
					{
						m_Pot.MyIngredient2 = jar.Name;
						if ( jar.Amount > 1 ){ jar.Amount = jar.Amount - 1; } else { jar.Delete(); }
						from.AddToBackpack( new Jar() );
						from.SendMessage( "You pour the " + jar.Name + " into the cauldron." );
						from.PlaySound( 0x04E );
						m_Pot.Hue = RandomThings.GetRandomColor(0);
					}
				}
				else if ( m_Stage == 3 )
				{
					if ( obj is BatWing || obj is DaemonBlood || obj is NoxCrystal || obj is PigIron || obj is GraveDust || obj is Ginseng || obj is SulfurousAsh || obj is Garlic || obj is MandrakeRoot || obj is Nightshade || obj is SpidersSilk || obj is Bloodmoss || obj is BlackPearl )
					{
						string AlchemyIngredient = "";
						if ( obj is BatWing ){ AlchemyIngredient = "bat wing"; }
						else if ( obj is DaemonBlood ){ AlchemyIngredient = "daemon blood"; }
						else if ( obj is NoxCrystal ){ AlchemyIngredient = "nox crystal"; }
						else if ( obj is PigIron ){ AlchemyIngredient = "pig iron"; }
						else if ( obj is GraveDust ){ AlchemyIngredient = "grave dust"; }
						else if ( obj is Ginseng ){ AlchemyIngredient = "ginseng"; }
						else if ( obj is SulfurousAsh ){ AlchemyIngredient = "sulfurous ash"; }
						else if ( obj is Garlic ){ AlchemyIngredient = "garlic"; }
						else if ( obj is MandrakeRoot ){ AlchemyIngredient = "mandrake root"; }
						else if ( obj is Nightshade ){ AlchemyIngredient = "nightshade"; }
						else if ( obj is SpidersSilk ){ AlchemyIngredient = "spiders silk"; }
						else if ( obj is Bloodmoss ){ AlchemyIngredient = "bloodmoss"; }
						else if ( obj is BlackPearl ){ AlchemyIngredient = "black pearl"; }
						else if ( obj is EyeOfToad ){ AlchemyIngredient = "eye of toad"; }
						else if ( obj is FairyEgg ){ AlchemyIngredient = "fairy egg"; }
						else if ( obj is GargoyleEar ){ AlchemyIngredient = "gargoyle ear"; }
						else if ( obj is BeetleShell ){ AlchemyIngredient = "beetle shell"; }
						else if ( obj is MoonCrystal ){ AlchemyIngredient = "moon crystal"; }
						else if ( obj is PixieSkull ){ AlchemyIngredient = "pixie skull"; }
						else if ( obj is RedLotus ){ AlchemyIngredient = "red lotus"; }
						else if ( obj is SeaSalt ){ AlchemyIngredient = "sea salt"; }
						else if ( obj is SilverWidow ){ AlchemyIngredient = "silver widow"; }
						else if ( obj is SwampBerries ){ AlchemyIngredient = "swamp berries"; }
						else if ( obj is Brimstone ){ AlchemyIngredient = "brimstone"; }
						else if ( obj is ButterflyWings ){ AlchemyIngredient = "butterfly wings"; }

						Item jar = (Item)obj;
						m_Pot.MyIngredient3 = AlchemyIngredient;
						m_Pot.MyReagent = 1;
						if ( jar.Amount > 1 ){ jar.Amount = jar.Amount - 1; } else { jar.Delete(); }
						from.SendMessage( "You put the " + AlchemyIngredient + " into the cauldron." );
						from.PlaySound( 0x04E );
						m_Pot.Hue = RandomThings.GetRandomColor(0);
					}
					else if ( obj is SilverSerpentVenom || obj is DragonBlood || obj is EnchantedSeaweed || obj is DragonTooth || obj is GoldenSerpentVenom || obj is LichDust || obj is PegasusFeather || obj is PhoenixFeather || obj is DemonClaw || obj is UnicornHorn || obj is GhostlyDust || obj is DemigodBlood )
					{
						string AlchemyIngredient = "";
						if ( obj is SilverSerpentVenom ){ AlchemyIngredient = "silver serpent venom"; }
						else if ( obj is DragonBlood ){ AlchemyIngredient = "dragon blood"; }
						else if ( obj is EnchantedSeaweed ){ AlchemyIngredient = "enchanted seaweed"; }
						else if ( obj is DragonTooth ){ AlchemyIngredient = "dragon tooth"; }
						else if ( obj is GoldenSerpentVenom ){ AlchemyIngredient = "golden serpent venom"; }
						else if ( obj is LichDust ){ AlchemyIngredient = "lich dust"; }
						else if ( obj is DemonClaw ){ AlchemyIngredient = "demon claw"; }
						else if ( obj is PegasusFeather ){ AlchemyIngredient = "pegasus feather"; }
						else if ( obj is PhoenixFeather ){ AlchemyIngredient = "phoenix feather"; }
						else if ( obj is UnicornHorn ){ AlchemyIngredient = "unicorn horn"; }
						else if ( obj is GhostlyDust ){ AlchemyIngredient = "ghostly dust"; }
						else if ( obj is DemigodBlood ){ AlchemyIngredient = "demigod blood"; }

						Item jar = (Item)obj;
						m_Pot.MyIngredient3 = AlchemyIngredient;
						m_Pot.MyReagent = 2;
						if ( jar.Amount > 1 ){ jar.Amount = jar.Amount - 1; } else { jar.Delete(); }
						from.SendMessage( "You put the " + AlchemyIngredient + " into the cauldron." );
						from.PlaySound( 0x04E );
						m_Pot.Hue = RandomThings.GetRandomColor(0);
					}
					else
					{
						from.SendMessage( "That is not a magical reagent!" );
					}
				}
				else if ( m_Stage == 4 )
				{
					int MiscBodyPart = 0;
					if ( obj is Item )
					{
						Item gut = (Item)obj;

						if ( gut.ItemID == 0x1B11 || gut.ItemID == 0x1B12 || gut.ItemID == 0x1B13 || gut.ItemID == 0x1B14 || gut.ItemID == 0x1B15 || gut.ItemID == 0x1B16 || gut.ItemID == 0x1B19 || gut.ItemID == 0x1B1A || gut.ItemID == 0x1AE0 || gut.ItemID == 0x1AE1 || gut.ItemID == 0x1AE2 || gut.ItemID == 0x1AE3 || gut.ItemID == 0x1AE4 || gut.ItemID == 0x1B17 || gut.ItemID == 0x1B18 || gut.ItemID == 0x1B1B || gut.ItemID == 0x1B1C || gut.ItemID == 0x1B09 || gut.ItemID == 0x1B0A || gut.ItemID == 0x1B0B || gut.ItemID == 0x1B0C || gut.ItemID == 0x1B0D || gut.ItemID == 0x1B0E || gut.ItemID == 0x1B0F || gut.ItemID == 0x1B10 || gut.ItemID == 0xECA || gut.ItemID == 0xECB || gut.ItemID == 0xECC || gut.ItemID == 0xECD || gut.ItemID == 0xECE || gut.ItemID == 0xECF || gut.ItemID == 0xED0 || gut.ItemID == 0xED1 || gut.ItemID == 0xED2 )
						{
							MiscBodyPart = 1;
						}
						else if ( gut.ItemID == 0x1CF0 || gut.ItemID == 0x1CEF || gut.ItemID == 0x1CEE || gut.ItemID == 0x1CED || gut.ItemID == 0x1CE9 || gut.ItemID == 0x1DA0 || gut.ItemID == 0x1DAE || gut.ItemID == 0x1CEC || gut.ItemID == 0x1CE5 || gut.ItemID == 0x1CE2 || gut.ItemID == 0x1CDD || gut.ItemID == 0x1AE4 || gut.ItemID == 0x1DA1 || gut.ItemID == 0x1DA2 || gut.ItemID == 0x1DA4 || gut.ItemID == 0x1DAF || gut.ItemID == 0x1DB0 || gut.ItemID == 0x1DB1 || gut.ItemID == 0x1DB2 || gut.ItemID == 0x1CE8 || gut.ItemID == 0x1CE0 || gut.ItemID == 0x1D9F || gut.ItemID == 0x1DAD )
						{
							MiscBodyPart = 2;
						}
					}

					if ( obj is Bones || obj is BonePile || obj is Bone || obj is RibCage || MiscBodyPart == 1 )
					{
						Item jar = (Item)obj;
						m_Pot.MyIngredient4 = "bones";
						jar.Delete();
						from.SendMessage( "You put the bones into the cauldron." );
						from.PlaySound( 0x04E );
						m_Pot.Hue = RandomThings.GetRandomColor(0);
					}
					else if ( obj is Head || obj is RightArm || obj is LeftArm || obj is Torso || obj is RightLeg || obj is LeftLeg || MiscBodyPart == 2 )
					{
						Item jar = (Item)obj;
						m_Pot.MyIngredient4 = "body part";
						jar.Delete();
						from.SendMessage( "You put the body part into the cauldron." );
						from.PlaySound( 0x04E );
						m_Pot.Hue = RandomThings.GetRandomColor(0);
					}
					else
					{
						from.SendMessage( "That is not a bone or body part!" );
					}
				}
				else if ( m_Stage == 5 )
				{
					Item jar = (Item)obj;

					if ( obj is WaterVial || obj is WaterFlask || obj is Waterskin || obj is DirtyWaterskin )
					{
						if ( obj is DirtyWaterskin ){ from.AddToBackpack( new Waterskin() ); jar.Delete(); }
						else if ( obj is Waterskin )
						{
							if ( jar.ItemID == 0x48E4 || jar.ItemID == 0x4971 )
							{
								jar.ItemID = 0x4971;
								jar.Name = "empty canteen";
							}
							else
							{
								jar.ItemID = 0xA21;
								jar.Name = "empty waterskin";
							}
							jar.Weight = 1.0;
							jar.InvalidateProperties();
						}
						else { jar.Delete(); }
						m_Pot.MyWater = 1;
						from.SendMessage( "You pour the water into the cauldron." );
						from.PlaySound( 0x04E );
						m_Pot.Hue = RandomThings.GetRandomColor(0);
					}
					else
					{
						from.SendMessage( "You need to put water into the cauldron!" );
					}
				}
				else if ( m_Stage == 6 && obj is MixingSpoon )
				{
					MixingSpoon spoon = (MixingSpoon)obj;
					spoon.m_Charges = spoon.m_Charges - 1;
					spoon.InvalidateProperties();
						if ( spoon.m_Charges < 1 ){ from.SendMessage( "Your cauldron mixer has corroded away." ); spoon.Delete(); }

					from.PlaySound( 0x020 );
					if ( from.CheckSkill( SkillName.Alchemy, 0, 100 ) )
					{
						int nFilled = (int)( (from.Skills[SkillName.Alchemy].Value + from.Skills[SkillName.Forensics].Value + from.Skills[SkillName.Necromancy].Value)/30 );
						if ( nFilled < 1 ){ nFilled = 1; }
						int nHigh = (int)( from.Skills[SkillName.Alchemy].Value + from.Skills[SkillName.Forensics].Value );
						if ( from.CheckSkill( SkillName.Necromancy, 0, 100 ) ) { nHigh = nHigh + (int)from.Skills[SkillName.Necromancy].Value; }
						nHigh = nHigh / 7;
						if ( nHigh > 38 ){ nHigh = 38; }
						int nLow = 0;
						if ( m_Pot.MyReagent > 1 ){ nLow = (int)(nHigh/2); }
						int potionType = Utility.RandomMinMax( nLow, nHigh )+0;
						string potionName = "";
						bool sMk1 = false; bool sMk2 = false; bool sMk3 = false; bool sMk4 = false; bool sMk5 = false; bool sMk6 = false; bool sMk7 = false; bool sMk8 = false; bool sMk9 = false; bool sMk10 = false; bool sMk11 = false; bool sMk12 = false; bool sMk13 = false; bool sMk14 = false; bool sMk15 = false; bool sMk16 = false; 

						if ( m_Pot.MyRecipe01 != "" && m_Pot.MyRecipe01 != null )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe01.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk1 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe02 != "" && m_Pot.MyRecipe02 != null && sMk1 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe02.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk2 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe03 != "" && m_Pot.MyRecipe03 != null && sMk2 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe03.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk3 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe04 != "" && m_Pot.MyRecipe04 != null && sMk3 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe04.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk4 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe05 != "" && m_Pot.MyRecipe05 != null && sMk4 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe05.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk5 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe06 != "" && m_Pot.MyRecipe06 != null && sMk5 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe06.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk6 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe07 != "" && m_Pot.MyRecipe07 != null && sMk6 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe07.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk7 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe08 != "" && m_Pot.MyRecipe08 != null && sMk7 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe08.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk8 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe09 != "" && m_Pot.MyRecipe09 != null && sMk8 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe09.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk9 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe10 != "" && m_Pot.MyRecipe10 != null && sMk9 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe10.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk10 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe11 != "" && m_Pot.MyRecipe11 != null && sMk10 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe11.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk11 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe12 != "" && m_Pot.MyRecipe12 != null && sMk11 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe12.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk12 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe13 != "" && m_Pot.MyRecipe13 != null && sMk12 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe13.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk13 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe14 != "" && m_Pot.MyRecipe14 != null && sMk13 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe14.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk14 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe15 != "" && m_Pot.MyRecipe15 != null && sMk14 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe15.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk15 = true; potionName = sR5; }
						}
						else if ( m_Pot.MyRecipe16 != "" && m_Pot.MyRecipe16 != null && sMk15 == false )
						{
							int cycle=0; string sR1=""; string sR2=""; string sR3=""; string sR4=""; string sR5="";
							string[] words = m_Pot.MyRecipe16.Split('|');
							foreach (string word in words){ cycle = cycle + 1; if ( cycle == 1 ){ sR1 = word; } else if ( cycle == 2 ){ sR2 = word; } else if ( cycle == 3 ){ sR3 = word; } else if ( cycle == 4 ){ sR4 = word; } else if ( cycle == 5 ){ sR5 = word; } }
							if ( ( ( m_Pot.MyIngredient1 == sR1 ) || ( m_Pot.MyIngredient1 == sR2 ) ) && ( ( m_Pot.MyIngredient2 == sR1 ) || ( m_Pot.MyIngredient2 == sR2 ) ) && ( m_Pot.MyIngredient3 == sR3 ) && ( m_Pot.MyIngredient4 == sR4 ) ){ sMk16 = true; potionName = sR5; }
						}

						if ( potionName != "" )
						{
							from.SendMessage("You made a batch of " + potionName + ".");
							m_Pot.MyCauldron = "cauldron of " + potionName + " (" + nFilled.ToString() + ")";
							m_Pot.Name = m_Pot.MyCauldron;
							m_Pot.MyPotion = potionName;
							m_Pot.MyFill = nFilled;

							if ( potionName == "eyes of the dead mixture" ){ m_Pot.Hue = 0x491; }
							else if ( potionName == "tomb raiding concoction" ){ m_Pot.Hue = 0x493; }
							else if ( potionName == "disease draught" ){ m_Pot.Hue = 0x4F8; }
							else if ( potionName == "phantasm elixir" ){ m_Pot.Hue = 0x6DE; }
							else if ( potionName == "retched air elixir" ){ m_Pot.Hue = 0xA97; }
							else if ( potionName == "lich leech mixture" ){ m_Pot.Hue = 0xB87; }
							else if ( potionName == "wall of spikes draught" ){ m_Pot.Hue = 0xB8F; }
							else if ( potionName == "disease curing concoction" ){ m_Pot.Hue = 0x8A2; }
							else if ( potionName == "blood pact elixir" ){ m_Pot.Hue = 0x5B5; }
							else if ( potionName == "spectre shadow elixir" ){ m_Pot.Hue = 0x17E; }
							else if ( potionName == "ghost phase concoction" ){ m_Pot.Hue = 0x47E; }
							else if ( potionName == "demonic fire ooze" ){ m_Pot.Hue = 0x54F; }
							else if ( potionName == "ghostly images draught" ){ m_Pot.Hue = 0xBF; }
							else if ( potionName == "hellish branding ooze" ){ m_Pot.Hue = 0x54C; }
							else if ( potionName == "black gate draught" ){ m_Pot.Hue = 0x2EA; }
							else if ( potionName == "vampire blood draught" ){ m_Pot.Hue = 0xB85; }
						}
						else
						{
							int WhichPotion = 0;
							int PotionRegDifficult = 0;
							int PotHue = 0;

							if ( m_Pot.MyRecipe01 == "" || m_Pot.MyRecipe01 == null ){ potionName = "eyes of the dead mixture"; WhichPotion = 1; PotHue = 0x491; }
							else if ( m_Pot.MyRecipe02 == "" || m_Pot.MyRecipe02 == null ){ potionName = "tomb raiding concoction"; WhichPotion = 2; PotHue = 0x493; }
							else if ( m_Pot.MyRecipe03 == "" || m_Pot.MyRecipe03 == null ){ potionName = "disease draught"; WhichPotion = 3; PotHue = 0x4F8; }
							else if ( m_Pot.MyRecipe04 == "" || m_Pot.MyRecipe04 == null ){ potionName = "phantasm elixir"; WhichPotion = 4; PotHue = 0x6DE; }
							else if ( m_Pot.MyRecipe05 == "" || m_Pot.MyRecipe05 == null ){ potionName = "retched air elixir"; WhichPotion = 5; PotHue = 0xA97; }
							else if ( m_Pot.MyRecipe06 == "" || m_Pot.MyRecipe06 == null ){ potionName = "lich leech mixture"; WhichPotion = 6; PotHue = 0xB87; }
							else if ( m_Pot.MyRecipe07 == "" || m_Pot.MyRecipe07 == null ){ potionName = "wall of spikes draught"; WhichPotion = 7; PotHue = 0xB8F; }
							else if ( m_Pot.MyRecipe08 == "" || m_Pot.MyRecipe08 == null ){ potionName = "disease curing concoction"; WhichPotion = 8; PotHue = 0x8A2; }
							else if ( m_Pot.MyRecipe09 == "" || m_Pot.MyRecipe09 == null ){ potionName = "blood pact elixir"; WhichPotion = 9; PotHue = 0x5B5; }
							else if ( m_Pot.MyRecipe10 == "" || m_Pot.MyRecipe10 == null ){ potionName = "spectre shadow elixir"; WhichPotion = 10; PotionRegDifficult = 1; PotHue = 0x17E; }
							else if ( m_Pot.MyRecipe11 == "" || m_Pot.MyRecipe11 == null ){ potionName = "ghost phase concoction"; WhichPotion = 11; PotionRegDifficult = 1; PotHue = 0x47E; }
							else if ( m_Pot.MyRecipe12 == "" || m_Pot.MyRecipe12 == null ){ potionName = "demonic fire ooze"; WhichPotion = 12; PotionRegDifficult = 1; PotHue = 0x54F; }
							else if ( m_Pot.MyRecipe13 == "" || m_Pot.MyRecipe13 == null ){ potionName = "ghostly images draught"; WhichPotion = 13; PotionRegDifficult = 1; PotHue = 0xBF; }
							else if ( m_Pot.MyRecipe14 == "" || m_Pot.MyRecipe14 == null ){ potionName = "hellish branding ooze"; WhichPotion = 14; PotionRegDifficult = 1; PotHue = 0x54C; }
							else if ( m_Pot.MyRecipe15 == "" || m_Pot.MyRecipe15 == null ){ potionName = "black gate draught"; WhichPotion = 15; PotionRegDifficult = 1; PotHue = 0x2EA; }
							else if ( m_Pot.MyRecipe16 == "" || m_Pot.MyRecipe16 == null ){ potionName = "vampire blood draught"; WhichPotion = 16; PotionRegDifficult = 1; PotHue = 0xB85; }

							if ( m_Pot.MyReagent < 2 && PotionRegDifficult > 0 )
							{
								from.SendMessage( "You will have to find a rare magical reagent to mix anything new, so you dump out the cauldron." );
								m_Pot.ItemID = 0x269C;
								m_Pot.MyIngredient1 = "";
								m_Pot.MyIngredient2 = "";
								m_Pot.MyIngredient3 = "";
								m_Pot.MyIngredient4 = "";
								m_Pot.MyReagent = 0;
								m_Pot.MyWater = 0;
								m_Pot.MyPotion = "";
								m_Pot.MyFill = 0;
								m_Pot.Hue = 0;
								m_Pot.MyCauldron = "";
								m_Pot.Name = "mixing cauldron";
							}
							else if ( potionName != "" )
							{
								from.SendMessage("You somehow made a batch of " + potionName + ".");
								m_Pot.MyCauldron = "cauldron of " + potionName;
								m_Pot.Name = m_Pot.MyCauldron + " (" + nFilled.ToString() + ")";
								m_Pot.MyPotion = potionName;
								m_Pot.MyFill = nFilled;
								m_Pot.Hue = PotHue;

								string NewRecipe = m_Pot.MyIngredient1 + "|" + m_Pot.MyIngredient2 + "|" + m_Pot.MyIngredient3 + "|" + m_Pot.MyIngredient4 + "|" + potionName;

								if ( WhichPotion == 1){ m_Pot.MyRecipe01 = NewRecipe; }
								else if ( WhichPotion == 2){ m_Pot.MyRecipe02 = NewRecipe; }
								else if ( WhichPotion == 3){ m_Pot.MyRecipe03 = NewRecipe; }
								else if ( WhichPotion == 4){ m_Pot.MyRecipe04 = NewRecipe; }
								else if ( WhichPotion == 5){ m_Pot.MyRecipe05 = NewRecipe; }
								else if ( WhichPotion == 6){ m_Pot.MyRecipe06 = NewRecipe; }
								else if ( WhichPotion == 7){ m_Pot.MyRecipe07 = NewRecipe; }
								else if ( WhichPotion == 8){ m_Pot.MyRecipe08 = NewRecipe; }
								else if ( WhichPotion == 9){ m_Pot.MyRecipe09 = NewRecipe; }
								else if ( WhichPotion == 10){ m_Pot.MyRecipe10 = NewRecipe; }
								else if ( WhichPotion == 11){ m_Pot.MyRecipe11 = NewRecipe; }
								else if ( WhichPotion == 12){ m_Pot.MyRecipe12 = NewRecipe; }
								else if ( WhichPotion == 13){ m_Pot.MyRecipe13 = NewRecipe; }
								else if ( WhichPotion == 14){ m_Pot.MyRecipe14 = NewRecipe; }
								else if ( WhichPotion == 15){ m_Pot.MyRecipe15 = NewRecipe; }
								else if ( WhichPotion == 16){ m_Pot.MyRecipe16 = NewRecipe; }

								m_Pot.InvalidateProperties();
							}
							else
							{
								from.SendMessage( "Having discovered all available deathly elixirs, you dump out the cauldron." );
								m_Pot.ItemID = 0x269C;
								m_Pot.MyIngredient1 = "";
								m_Pot.MyIngredient2 = "";
								m_Pot.MyIngredient3 = "";
								m_Pot.MyIngredient4 = "";
								m_Pot.MyReagent = 0;
								m_Pot.MyWater = 0;
								m_Pot.MyPotion = "";
								m_Pot.MyFill = 0;
								m_Pot.Hue = 0;
								m_Pot.MyCauldron = "";
								m_Pot.Name = "mixing cauldron";
							}
						}
					}
					else
					{
						from.SendMessage( "Failing to mix anything of use, you dump out the cauldron." );
						m_Pot.ItemID = 0x269C;
						m_Pot.MyIngredient1 = "";
						m_Pot.MyIngredient2 = "";
						m_Pot.MyIngredient3 = "";
						m_Pot.MyIngredient4 = "";
						m_Pot.MyReagent = 0;
						m_Pot.MyWater = 0;
						m_Pot.MyPotion = "";
						m_Pot.MyFill = 0;
						m_Pot.Hue = 0;
						m_Pot.MyCauldron = "";
						m_Pot.Name = "mixing cauldron";
					}
				}
				else if ( m_Stage == 7 && obj is Jar )
				{
					Item jar = (Item)obj;
					from.PlaySound( 0x240 );
					m_Pot.MyFill = m_Pot.MyFill - 1;
					if ( jar.Amount > 1 ){ jar.Amount = jar.Amount - 1; } else { jar.Delete(); }

					if ( m_Pot.MyPotion == "eyes of the dead mixture" ){ from.AddToBackpack( new UndeadEyesScroll() ); }
					else if ( m_Pot.MyPotion == "tomb raiding concoction" ){ from.AddToBackpack( new NecroUnlockScroll() ); }
					else if ( m_Pot.MyPotion == "disease draught" ){ from.AddToBackpack( new NecroPoisonScroll() ); }
					else if ( m_Pot.MyPotion == "phantasm elixir" ){ from.AddToBackpack( new PhantasmScroll() ); }
					else if ( m_Pot.MyPotion == "retched air elixir" ){ from.AddToBackpack( new RetchedAirScroll() ); }
					else if ( m_Pot.MyPotion == "lich leech mixture" ){ from.AddToBackpack( new ManaLeechScroll() ); }
					else if ( m_Pot.MyPotion == "wall of spikes draught" ){ from.AddToBackpack( new WallOfSpikesScroll() ); }
					else if ( m_Pot.MyPotion == "disease curing concoction" ){ from.AddToBackpack( new NecroCurePoisonScroll() ); }
					else if ( m_Pot.MyPotion == "blood pact elixir" ){ from.AddToBackpack( new BloodPactScroll() ); }
					else if ( m_Pot.MyPotion == "spectre shadow elixir" ){ from.AddToBackpack( new SpectreShadowScroll() ); }
					else if ( m_Pot.MyPotion == "ghost phase concoction" ){ from.AddToBackpack( new GhostPhaseScroll() ); }
					else if ( m_Pot.MyPotion == "demonic fire ooze" ){ from.AddToBackpack( new HellsGateScroll() ); }
					else if ( m_Pot.MyPotion == "ghostly images draught" ){ from.AddToBackpack( new GhostlyImagesScroll() ); }
					else if ( m_Pot.MyPotion == "hellish branding ooze" ){ from.AddToBackpack( new HellsBrandScroll() ); }
					else if ( m_Pot.MyPotion == "black gate draught" ){ from.AddToBackpack( new GraveyardGatewayScroll() ); }
					else if ( m_Pot.MyPotion == "vampire blood draught" ){ from.AddToBackpack( new VampireGiftScroll() ); }

					if ( m_Pot.MyFill < 1 )
					{
						from.SendMessage( "You fill a bottle with the last of the liquid." );
						m_Pot.ItemID = 0x269C;
						m_Pot.MyIngredient1 = "";
						m_Pot.MyIngredient2 = "";
						m_Pot.MyIngredient3 = "";
						m_Pot.MyIngredient4 = "";
						m_Pot.MyReagent = 0;
						m_Pot.MyWater = 0;
						m_Pot.MyPotion = "";
						m_Pot.MyFill = 0;
						m_Pot.Hue = 0;
						m_Pot.MyCauldron = "";
						m_Pot.Name = "mixing cauldron";
					}
					else
					{
						from.SendMessage( "You fill a bottle with the liquid." );
						m_Pot.Name = m_Pot.MyCauldron + " (" + m_Pot.MyFill.ToString() + ")";
					}
				}
				else
				{
					from.SendMessage( "You should get a Necrotic Alchemy book from a sage or necromancer." );
				}

				Server.Gumps.MReagentGump.XReagentGump( from );
			}
		}
	}
///////////////////////////////////////////////////////////////////////////////////////////
	public class MixingSpoon : Item
	{
		public int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public MixingSpoon() : base( 0x1E27 )
		{
			Weight = 3.0;
			Hue = 0x979;
			Name = "cauldron mixer";
			Charges = 20;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

		public MixingSpoon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
		}
	}
}