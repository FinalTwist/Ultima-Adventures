using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Items
{
	public class MagicFurLegs : FurLegs /////////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicFurLegs()
		{
			FurArmors.FurType( this, "fur leggings" );
		}

		public MagicFurLegs( Serial serial ) : base( serial )
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

	public class MagicFurArms : FurArms /////////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicFurArms()
		{
			FurArmors.FurType( this, "fur arms" );
		}

		public MagicFurArms( Serial serial ) : base( serial )
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
	public class MagicFurChest : FurTunic ///////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicFurChest()
		{
			FurArmors.FurType( this, "fur tunic" );
		}

		public MagicFurChest( Serial serial ) : base( serial )
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
namespace Server.Misc
{
    class FurArmors
    {
		public static void FurType( Item i, string name )
		{
			string color = "brown";

			switch( Utility.RandomMinMax( 0, 24 ) )
			{
				case 0: color = "black"; i.Name = "kodiak " + name; break;
				case 1: color = "black"; i.Name = "black bear " + name; break;
				case 2: color = "black"; i.Name = "nightmare " + name; break;
				case 3: color = "black"; i.Name = "panther " + name; break;
				case 4: color = "black"; i.Name = "black wolf " + name; break;
				case 5: color = "white"; i.Name = "polar bear " + name; break;
				case 6: color = "white"; i.Name = "white wolf " + name; break;
				case 7: color = "white"; i.Name = "unicorn " + name; break;
				case 8: color = "white"; i.Name = "pegasus " + name; break;
				case 9: color = "white"; i.Name = "white fox " + name; break;
				case 10: color = "white"; i.Name = "goat " + name; break;
				case 11: i.Name = "grizzly bear " + name; break;
				case 12: i.Name = "wolf " + name; break;
				case 13: i.Name = "lion " + name; break;
				case 14: i.Name = "cougar " + name; break;
				case 15: i.Name = "timber wolf " + name; break;
				case 16: i.Name = "stag " + name; break;
				case 17: i.Name = "deer " + name; break;
				case 18: i.Name = "rabbit " + name; break;
				case 19: i.Name = "bear " + name; break;
				case 20: i.Name = "minotaur " + name; break;
				case 21: i.Name = "bugbear " + name; break;
				case 22: i.Name = "werewolf " + name; break;
				case 23: i.Name = "owlbear " + name; break;
				case 24: i.Name = "jackal " + name; break;
			}

			if ( color == "black" ){ i.Hue = Utility.RandomMinMax( 0x763, 0x774 ); }
			else if ( color == "white" ){ i.Hue = Utility.RandomList( 0x47E, 0x481, 0xBB4, 0xB78, 0xB62 ); }
			else { i.Hue = Utility.RandomMinMax( 0x709, 0x73E ); }
		}
	}
}