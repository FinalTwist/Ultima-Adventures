using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Items
{
	public class MagicBoneLegs : BoneLegs /////////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicBoneLegs()
		{
			BoneArmors.BoneType( this, "leggings" );
		}

		public MagicBoneLegs( Serial serial ) : base( serial )
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
	public class MagicBoneGloves : BoneGloves /////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicBoneGloves()
		{
			BoneArmors.BoneType( this, "gauntlets" );
		}

		public MagicBoneGloves( Serial serial ) : base( serial )
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
	public class MagicBoneArms : BoneArms /////////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicBoneArms()
		{
			BoneArmors.BoneType( this, "arms" );
		}

		public MagicBoneArms( Serial serial ) : base( serial )
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
	public class MagicBoneChest : BoneChest ///////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicBoneChest()
		{
			BoneArmors.BoneType( this, "tunic" );
		}

		public MagicBoneChest( Serial serial ) : base( serial )
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
	public class MagicBoneHelm : BoneHelm /////////////////////////////////////////////////////////
	{
		[Constructable]
		public MagicBoneHelm()
		{
			BoneArmors.BoneType( this, "helm" );
		}

		public MagicBoneHelm( Serial serial ) : base( serial )
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
	public class MagicBoneSkirt : BoneSkirt /////////////////////////////////////////////////////////
	{

		[Constructable]
		public MagicBoneSkirt()
		{
			BoneArmors.BoneType( this, "skirt" );
		}

		public MagicBoneSkirt( Serial serial ) : base( serial )
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
    class BoneArmors
    {
		public static void BoneType( Item i, string name )
		{
			switch( Utility.RandomMinMax( 0, 35 ) )
			{
				case 0: i.Hue = 0x430; i.Name = "lich bone " + name; break;
				case 1: i.Hue = 0x485; i.Name = "demon bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = Utility.RandomList( 0x2645, 0x2646 ); } break;
				case 2: i.Hue = 0x5B2; i.Name = "ogre bone " + name; break;
				case 3: i.Hue = 0x961; i.Name = "troll bone " + name; break;
				case 4: i.Hue = 0xB97; i.Name = "zombie bone " + name; break;
				case 5: i.Hue = 0xB8C; i.Name = "whale bone " + name; break;
				case 6: i.Hue = 0xB89; i.Name = "dracolich bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = Utility.RandomList( 0x2645, 0x2646 ); } break;
				case 7: i.Hue = 0xB85; i.Name = "vampire bone " + name; break;
				case 8: i.Hue = 0x846; i.Name = "devil bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = Utility.RandomList( 0x2645, 0x2646 ); } break;
				case 9: i.Hue = 0x89C; i.Name = "gargoyle bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = 0x1F0B; } break;
				case 10: i.Hue = 0x96D; i.Name = "centaur bone " + name; break;
				case 11: i.Hue = 0xB8E; i.Name = "satyr bone " + name; break;
				case 12: i.Hue = 0x978; i.Name = "ettin bone " + name; break;
				case 13: i.Hue = 0x504; i.Name = "sea giant bone " + name; break;
				case 14: i.Hue = 0x4A9; i.Name = "forest giant bone " + name; break;
				case 15: i.Hue = 0x495; i.Name = "frost giant bone " + name; break;
				case 16: i.Hue = 0x48E; i.Name = "fire giant bone " + name; break;
				case 17: i.Hue = 0x6EE; i.Name = "dragon bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = Utility.RandomList( 0x2645, 0x2646 ); } break;
				case 18: i.Hue = 0xB8B; i.Name = "ancient bone " + name; break;
				case 19: i.Hue = 0xB8F; i.Name = "dinosaur bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = Utility.RandomList( 0x2645, 0x2646 ); } break;
				case 20: i.Hue = 0x430; i.Name = "lich bone " + name; break;
				case 21: i.Hue = 0xB90; i.Name = "mastadon bone " + name; break;
				case 22: i.Hue = 0xB90; i.Name = "mammoth bone " + name; break;
				case 23: i.Hue = 0xB90; i.Name = "elephant bone " + name; break;
				case 24: i.Hue = 0x5B7; i.Name = "griffon bone " + name; break;
				case 25: i.Hue = 0x482; i.Name = "hydra bone " + name; break;
				case 26: i.Hue = 0x969; i.Name = "werewolf bone " + name; break;
				case 27: i.Hue = 0x83F; i.Name = "minotaur bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = 0x1F0B; } break;
				case 28: i.Hue = 0x48C; i.Name = "medusa bone " + name; break;
				case 29: i.Hue = 0x497; i.Name = "nightmare bone " + name; break;
				case 30: i.Hue = 0x47E; i.Name = "pegasus bone " + name; break;
				case 31: i.Hue = 0x47F; i.Name = "shark bone " + name; break;
				case 32: i.Hue = 0x481; i.Name = "unicorn bone " + name; break;
				case 33: i.Hue = 0x490; i.Name = "fairy bone " + name; break;
				case 34: i.Hue = 0x48F; i.Name = "ancient lich bone " + name; break;
				case 35: i.Hue = 0x489; i.Name = "imp bone " + name; if ( i is MagicBoneHelm ){ i.ItemID = 0x1F0B; } break;
			}
		}
	}
}