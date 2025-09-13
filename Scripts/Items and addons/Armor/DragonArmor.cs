using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class MagicDragonLegs : DragonLegs /////////////////////////////////////////////////////////
	{
		public string DragonFrom;
		public string DragonKiller;

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_From { get { return DragonFrom; } set { DragonFrom = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_Killer { get { return DragonKiller; } set { DragonKiller = value; InvalidateProperties(); } }

		[Constructable]
		public MagicDragonLegs()
		{
			Name = "scalemail leggings";

			string dColor = "red";

			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Resource = CraftResource.RedScales;		dColor = "red";		break;
				case 1: Resource = CraftResource.YellowScales;	dColor = "yellow"; 	break;
				case 2: Resource = CraftResource.BlackScales;	dColor = "black"; 	break;
				case 3: Resource = CraftResource.GreenScales;	dColor = "green"; 	break;
				case 4: Resource = CraftResource.WhiteScales;	dColor = "white"; 	break;
				case 5: Resource = CraftResource.BlueScales;	dColor = "blue"; 	break;
			}

			if ( DragonFrom == null ){ DragonFrom = DragonTypes.GetDragonType( dColor ); }
			if ( DragonKiller == null ){ DragonKiller = "Slain by " + ContainerFunctions.GetOwner( "property" ); }
		}

		public MagicDragonLegs( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, DragonFrom );
            list.Add( 1049644, DragonKiller );
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( DragonFrom );
            writer.Write( DragonKiller );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            DragonFrom = reader.ReadString();
            DragonKiller = reader.ReadString();
		}
	}
	public class MagicDragonGloves : DragonGloves /////////////////////////////////////////////////////
	{
		public string DragonFrom;
		public string DragonKiller;

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_From { get { return DragonFrom; } set { DragonFrom = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_Killer { get { return DragonKiller; } set { DragonKiller = value; InvalidateProperties(); } }

		[Constructable]
		public MagicDragonGloves()
		{
			Name = "scalemail gloves";

			string dColor = "red";

			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Resource = CraftResource.RedScales;		dColor = "red";		break;
				case 1: Resource = CraftResource.YellowScales;	dColor = "yellow"; 	break;
				case 2: Resource = CraftResource.BlackScales;	dColor = "black"; 	break;
				case 3: Resource = CraftResource.GreenScales;	dColor = "green"; 	break;
				case 4: Resource = CraftResource.WhiteScales;	dColor = "white"; 	break;
				case 5: Resource = CraftResource.BlueScales;	dColor = "blue"; 	break;
			}

			if ( DragonFrom == null ){ DragonFrom = DragonTypes.GetDragonType( dColor ); }
			if ( DragonKiller == null ){ DragonKiller = "Slain by " + ContainerFunctions.GetOwner( "property" ); }
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, DragonFrom );
            list.Add( 1049644, DragonKiller );
        }

		public MagicDragonGloves( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( DragonFrom );
            writer.Write( DragonKiller );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            DragonFrom = reader.ReadString();
            DragonKiller = reader.ReadString();
		}
	}
	public class MagicDragonArms : DragonArms /////////////////////////////////////////////////////////
	{
		public string DragonFrom;
		public string DragonKiller;

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_From { get { return DragonFrom; } set { DragonFrom = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_Killer { get { return DragonKiller; } set { DragonKiller = value; InvalidateProperties(); } }

		[Constructable]
		public MagicDragonArms()
		{
			Name = "scalemail arms";

			string dColor = "red";

			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Resource = CraftResource.RedScales;		dColor = "red";		break;
				case 1: Resource = CraftResource.YellowScales;	dColor = "yellow"; 	break;
				case 2: Resource = CraftResource.BlackScales;	dColor = "black"; 	break;
				case 3: Resource = CraftResource.GreenScales;	dColor = "green"; 	break;
				case 4: Resource = CraftResource.WhiteScales;	dColor = "white"; 	break;
				case 5: Resource = CraftResource.BlueScales;	dColor = "blue"; 	break;
			}

			if ( DragonFrom == null ){ DragonFrom = DragonTypes.GetDragonType( dColor ); }
			if ( DragonKiller == null ){ DragonKiller = "Slain by " + ContainerFunctions.GetOwner( "property" ); }
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, DragonFrom );
            list.Add( 1049644, DragonKiller );
        }

		public MagicDragonArms( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( DragonFrom );
            writer.Write( DragonKiller );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            DragonFrom = reader.ReadString();
            DragonKiller = reader.ReadString();
		}
	}
	public class MagicDragonChest : DragonChest ///////////////////////////////////////////////////////
	{
		public string DragonFrom;
		public string DragonKiller;

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_From { get { return DragonFrom; } set { DragonFrom = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_Killer { get { return DragonKiller; } set { DragonKiller = value; InvalidateProperties(); } }

		[Constructable]
		public MagicDragonChest()
		{
			Name = "scalemail tunic";

			string dColor = "red";

			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Resource = CraftResource.RedScales;		dColor = "red";		break;
				case 1: Resource = CraftResource.YellowScales;	dColor = "yellow"; 	break;
				case 2: Resource = CraftResource.BlackScales;	dColor = "black"; 	break;
				case 3: Resource = CraftResource.GreenScales;	dColor = "green"; 	break;
				case 4: Resource = CraftResource.WhiteScales;	dColor = "white"; 	break;
				case 5: Resource = CraftResource.BlueScales;	dColor = "blue"; 	break;
			}

			if ( DragonFrom == null ){ DragonFrom = DragonTypes.GetDragonType( dColor ); }
			if ( DragonKiller == null ){ DragonKiller = "Slain by " + ContainerFunctions.GetOwner( "property" ); }
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, DragonFrom );
            list.Add( 1049644, DragonKiller );
        }

		public MagicDragonChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( DragonFrom );
            writer.Write( DragonKiller );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            DragonFrom = reader.ReadString();
            DragonKiller = reader.ReadString();
		}
	}
	public class MagicDragonHelm : DragonHelm /////////////////////////////////////////////////////////
	{
		public string DragonFrom;
		public string DragonKiller;

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_From { get { return DragonFrom; } set { DragonFrom = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Dragon_Killer { get { return DragonKiller; } set { DragonKiller = value; InvalidateProperties(); } }

		[Constructable]
		public MagicDragonHelm()
		{
			Name = "scalemail helm";

			string dColor = "red";

			switch( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Resource = CraftResource.RedScales;		dColor = "red";		break;
				case 1: Resource = CraftResource.YellowScales;	dColor = "yellow"; 	break;
				case 2: Resource = CraftResource.BlackScales;	dColor = "black"; 	break;
				case 3: Resource = CraftResource.GreenScales;	dColor = "green"; 	break;
				case 4: Resource = CraftResource.WhiteScales;	dColor = "white"; 	break;
				case 5: Resource = CraftResource.BlueScales;	dColor = "blue"; 	break;
			}

			if ( DragonFrom == null ){ DragonFrom = DragonTypes.GetDragonType( dColor ); }
			if ( DragonKiller == null ){ DragonKiller = "Slain by " + ContainerFunctions.GetOwner( "property" ); }
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, DragonFrom );
            list.Add( 1049644, DragonKiller );
        }

		public MagicDragonHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( DragonFrom );
            writer.Write( DragonKiller );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            DragonFrom = reader.ReadString();
            DragonKiller = reader.ReadString();
		}
	}
}

namespace Server.Misc
{
    class DragonTypes
    {
		public static string GetDragonType( string color )
		{
			string sNumber = Utility.RandomMinMax( 3, 12 ).ToString();

			string[] dType = new string[] {"Dragon", "Wyrm", "Drake"};
				string wType = dType[Utility.RandomMinMax( 0, (dType.Length-1) )];

			string[] dAge = new string[] {"Ancient ", "Elder ", ""};
				string wAge = dAge[Utility.RandomMinMax( 0, (dAge.Length-1) )];

			string wColor = "red";

			if ( color == "red" )
			{
				switch( Utility.RandomMinMax( 1, 5 ) )
				{
					case 1: wColor = "red"; break;
					case 2: wColor = "fire"; break;
					case 3: wColor = "lava"; break;
					case 4: wColor = "ruby"; break;
					case 5: wColor = "blood"; break;
				}
			}
			else if ( color == "yellow" )
			{
				switch( Utility.RandomMinMax( 1, 4 ) )
				{
					case 1: wColor = "sun"; break;
					case 2: wColor = "gold"; break;
					case 3: wColor = "amber"; break;
					case 4: wColor = "sand"; break;
				}
			}
			else if ( color == "black" )
			{
				switch( Utility.RandomMinMax( 1, 6 ) )
				{
					case 1: wColor = "black"; break;
					case 2: wColor = "night"; break;
					case 3: wColor = "shadow"; break;
					case 4: wColor = "deep"; break;
					case 5: wColor = "dark"; break;
					case 6: wColor = "onyx"; break;
				}
			}
			else if ( color == "green" )
			{
				switch( Utility.RandomMinMax( 1, 5 ) )
				{
					case 1: wColor = "green"; break;
					case 2: wColor = "forest"; break;
					case 3: wColor = "emerald"; break;
					case 4: wColor = "jade"; break;
					case 5: wColor = "jungle"; break;
				}
			}
			else if ( color == "white" )
			{
				switch( Utility.RandomMinMax( 1, 5 ) )
				{
					case 1: wColor = "frost"; break;
					case 2: wColor = "snow"; break;
					case 3: wColor = "winter"; break;
					case 4: wColor = "ice"; break;
					case 5: wColor = "white"; break;
				}
			}
			else if ( color == "blue" )
			{
				switch( Utility.RandomMinMax( 1, 5 ) )
				{
					case 1: wColor = "blue"; break;
					case 2: wColor = "sea"; break;
					case 3: wColor = "deep sea"; break;
					case 4: wColor = "sky"; break;
					case 5: wColor = "sappphire"; break;
				}
			}

			string sDragon = "From " + NameList.RandomName( "dragon" ) + " the " + wAge + wColor + " " + wType;

			return sDragon;
		}
	}
}