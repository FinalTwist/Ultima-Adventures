using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BaseStatueAddon : BaseAddon
	{
		public int StatueID;
		public int StatueColor;
		public string StatueMaterial;
		public string StatueMaker;
		public string StatueName;

		[CommandProperty(AccessLevel.Owner)]
		public int Statue_ID { get { return StatueID; } set { StatueID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Statue_Color { get { return StatueColor; } set { StatueColor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Material { get { return StatueMaterial; } set { StatueMaterial = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Maker { get { return StatueMaker; } set { StatueMaker = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Name { get { return StatueName; } set { StatueName = value; InvalidateProperties(); } }
 
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BaseStatueDeed( StatueID, StatueColor, StatueMaterial, StatueMaker, StatueName );
			}
		}

		[Constructable]
		public BaseStatueAddon() : this( 1, 0xB8E, "Granite", "", "statue" )
		{
		}

		[ Constructable ]
		public BaseStatueAddon( int vStatueID, int vStatueColor, string vStatueMaterial, string vStatueMaker, string vStatueName )
		{
			string name = Statues.BuiltName( vStatueName );

			if ( vStatueID == 1 )
			{
				AddComplexComponent( (BaseAddon) this, 12777, -1, 1, 5, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12776, 0, 0, 5, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12775, 1, -1, 5, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 2 )
			{
				AddComplexComponent( (BaseAddon) this, 12778, -1, 1, 5, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12779, 0, 0, 5, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12780, 1, -1, 5, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 3 )
			{
				AddComplexComponent( (BaseAddon) this, 12986, -1, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12985, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12983, 1, -1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12984, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12987, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 4 )
			{
				AddComplexComponent( (BaseAddon) this, 12978, -1, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12979, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12981, 1, -1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12980, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12982, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 5 )
			{
				AddComplexComponent( (BaseAddon) this, 12741, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12742, 1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 6 )
			{
				AddComplexComponent( (BaseAddon) this, 12740, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12739, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 7 )
			{
				AddComplexComponent( (BaseAddon) this, 0x139E, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x139F, -1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x13A0, 0, -1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 8 )
			{
				AddComplexComponent( (BaseAddon) this, 0x129F, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x12A0, 0, -1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x12A1, -1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 9 )
			{
				AddComplexComponent( (BaseAddon) this, 12989, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12988, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 10 )
			{
				AddComplexComponent( (BaseAddon) this, 12994, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 13003, 1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 11 )
			{
				AddComplexComponent( (BaseAddon) this, 7624, 0, -1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7625, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7623, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 12 )
			{
				AddComplexComponent( (BaseAddon) this, 7627, -1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7628, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7626, 1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 13 )
			{
				AddComplexComponent( (BaseAddon) this, 7624, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7623, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 14 )
			{
				AddComplexComponent( (BaseAddon) this, 7627, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7626, 1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 15 )
			{
				AddComplexComponent( (BaseAddon) this, 7612, 0, -1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7613, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7611, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 16 )
			{
				AddComplexComponent( (BaseAddon) this, 7615, -1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7616, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7614, 1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 17 )
			{
				AddComplexComponent( (BaseAddon) this, 7612, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7611, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 18 )
			{
				AddComplexComponent( (BaseAddon) this, 7615, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 7614, 1, 0, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 19 )
			{
				AddComplexComponent( (BaseAddon) this, 12769, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12771, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12770, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 20 )
			{
				AddComplexComponent( (BaseAddon) this, 12762, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12760, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12761, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 21 )
			{
				AddComplexComponent( (BaseAddon) this, 12759, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12757, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12758, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 22 )
			{
				AddComplexComponent( (BaseAddon) this, 12766, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12768, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12767, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 23 )
			{
				AddComplexComponent( (BaseAddon) this, 12774, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12772, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12773, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 24 )
			{
				AddComplexComponent( (BaseAddon) this, 12765, 0, 1, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12763, 1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 12764, 1, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 25 )
			{
				AddComplexComponent( (BaseAddon) this, 0x313D, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 26 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3140, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 27 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3141, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 28 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3142, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 29 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3143, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 30 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3144, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 31 )
			{
				AddComplexComponent( (BaseAddon) this, 0x313E, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 32 )
			{
				AddComplexComponent( (BaseAddon) this, 0x313F, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 33 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3149, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 34 )
			{
				AddComplexComponent( (BaseAddon) this, 0x314A, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 35 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3200, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 36 )
			{
				AddComplexComponent( (BaseAddon) this, 0x31F3, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 37 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3202, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 38 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3201, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 39 )
			{
				AddComplexComponent( (BaseAddon) this, 0x306F, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x3070, -1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x3071, 0, -1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 40 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3072, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x3073, 0, -1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 41 )
			{
				AddComplexComponent( (BaseAddon) this, 0x3074, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x3075, -1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x3076, 0, -1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 42 )
			{
				AddComplexComponent( (BaseAddon) this, 0x306C, 0, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x306D, -1, 0, 0, vStatueColor, -1, name, 1);
				AddComplexComponent( (BaseAddon) this, 0x306E, 0, -1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 43 )
			{
				AddComplexComponent( (BaseAddon) this, 0x2104, 0, 1, 0, vStatueColor, -1, name, 1);
			}
			else if ( vStatueID == 44 )
			{
				AddComplexComponent( (BaseAddon) this, 0x2105, 0, 1, 0, vStatueColor, -1, name, 1);
			}

			StatueID = vStatueID;
			StatueColor = vStatueColor;
			StatueMaterial = vStatueMaterial;
			StatueMaker = vStatueMaker;
			StatueName = vStatueName;
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public BaseStatueAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( StatueID );
            writer.Write( StatueColor );
            writer.Write( StatueMaterial );
            writer.Write( StatueMaker );
            writer.Write( StatueName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			StatueID = reader.ReadInt();
			StatueColor = reader.ReadInt();
			StatueMaterial = reader.ReadString();
			StatueMaker = reader.ReadString();
			StatueName = reader.ReadString();
		}
	}

	[Furniture]
	[Flipable( 0x32F0, 0x32F0 )]
	public class BaseStatueDeed : BaseAddonDeed
	{
		public int StatueID;
		public int StatueColor;
		public string StatueMaterial;
		public string StatueMaker;
		public string StatueName;

		[CommandProperty(AccessLevel.Owner)]
		public int Statue_ID { get { return StatueID; } set { StatueID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Statue_Color { get { return StatueColor; } set { StatueColor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Material { get { return StatueMaterial; } set { StatueMaterial = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Maker { get { return StatueMaker; } set { StatueMaker = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Name { get { return StatueName; } set { StatueName = value; InvalidateProperties(); } }

		public override BaseAddon Addon
		{
			get
			{
				return new BaseStatueAddon( StatueID, StatueColor, StatueMaterial, StatueMaker, StatueName );
			}
		}

		[Constructable]
		public BaseStatueDeed() : this( 0, 0xB8E, "Granite", "", "statue" )
		{
		}

		[Constructable]
		public BaseStatueDeed( int vStatueID, int vStatueColor, string vStatueMaterial, string vStatueMaker, string vStatueName )
		{
			Name = "statue";
			ItemID = 0x32F0;
			Weight = 10;

			if ( StatueID < 1 )
			{
				StatueID = vStatueID;
				StatueColor = vStatueColor;
				StatueMaterial = vStatueMaterial;
				StatueMaker = vStatueMaker;
				StatueName = vStatueName;
				Hue = StatueColor;
				Name = StatueName;
			}

			Statues.DirectionStatue( this, StatueID );
			StatueName = Name;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( StatueMaker != null )
				list.Add( 1050043, StatueMaker ); // crafted by ~1_NAME~
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Made From " + StatueMaterial + "");
            list.Add( 1049644, "Double Click To Build In Your Home");
        }

		public override void OnDoubleClick( Mobile from )
		{
			StatueColor = this.Hue;
            base.OnDoubleClick(from);
		}

		public BaseStatueDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( StatueID );
            writer.Write( StatueColor );
            writer.Write( StatueMaterial );
            writer.Write( StatueMaker );
            writer.Write( StatueName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			StatueID = reader.ReadInt();
			StatueColor = reader.ReadInt();
			StatueMaterial = reader.ReadString();
			StatueMaker = reader.ReadString();
			StatueName = reader.ReadString();
		}
	}

    class Statues
    {
		public static void SetStatue( BaseStatueDeed statue, int StatueID, int StatueColor, string StatueMaterial, string StatueMaker, string StatueName )
		{
			statue.StatueID = StatueID;
			statue.StatueColor = StatueColor;
			statue.StatueMaterial = StatueMaterial;
			statue.StatueMaker = StatueMaker;
			statue.StatueName = StatueName;
			statue.Hue = StatueColor;
			statue.Name = StatueName;
			statue.Weight = 10;
		}

		public static void FlipStatue( BaseStatueDeed statue, int StatueID )
		{
			if ( IsOdd( StatueID ) ){ statue.StatueID = statue.StatueID + 1; }
			else { statue.StatueID = statue.StatueID - 1; }

			Statues.DirectionStatue( statue, statue.StatueID );
		}

		public static void DirectionStatue( BaseStatueDeed statue, int StatueID )
		{
			if ( IsOdd( StatueID ) ){ statue.Name = (statue.Name).Replace("(south)", "(east)"); }
			else { statue.Name = (statue.Name).Replace("(east)", "(south)"); }

			statue.InvalidateProperties(); 
		}

		public static string BuiltName( string statue )
		{
			statue = statue.Replace(" (east)", "");
			statue = statue.Replace(" (south)", "");

			return statue;
		}

		public static bool IsOdd( int value )
		{
			return value % 2 != 0;
		}
	}
}