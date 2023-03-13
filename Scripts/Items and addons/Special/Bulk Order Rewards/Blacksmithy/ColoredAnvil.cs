using System;
using Server;
using Server.Misc;
using Server.Engines.Craft;

namespace Server.Items
{
	[Anvil, Flipable( 0xFAF, 0xFB0 )]
	public class ColoredAnvil : Item
	{
		[Constructable]
		public ColoredAnvil() : base( 0xFAF )
		{
			Server.Misc.MaterialInfo.ColorPlainMetal( this );
			Weight = 100;
		}

		public ColoredAnvil( Serial serial ) : base( serial )
		{
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

	[Anvil, Flipable( 0xFAF, 0xFB0 )]
	public class RareAnvil : Item
	{
		[Constructable]
		public RareAnvil() : base( 0xFAF )
		{
			Name = "anvil";
			Server.Misc.MaterialInfo.ColorPlainMetal( this );
			Weight = 100;

			string adjective = "mighty";

			switch ( Utility.RandomMinMax( 0, 17 ) )
			{
				case 0:		adjective = "mighty";			break;
				case 1:		adjective = "lost";				break;
				case 2:		adjective = "legendary";		break;
				case 3:		adjective = "fabled";			break;
				case 4:		adjective = "mystical";			break;
				case 5:		adjective = "mythical";			break;
				case 6:		adjective = "superb";			break;
				case 7:		adjective = "marvelous";		break;
				case 8:		adjective = "magnificent";		break;
				case 9:		adjective = "extraordinary";	break;
				case 10:	adjective = "strange";			break;
				case 11:	adjective = "unique";			break;
				case 12:	adjective = "unusual";			break;
				case 13:	adjective = "mysterious";		break;
				case 14:	adjective = "missing";			break;
				case 15:	adjective = "ancient";			break;
				case 16:	adjective = "secret";			break;
				case 17:	adjective = "unknown";			break;
			}

			Name = "the " + adjective + " " + Name + " of " + NameList.RandomName( "dwarf" );
		}

		public RareAnvil( Serial serial ) : base( serial )
		{
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