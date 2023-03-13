using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class MagicRobe : GoldRing
	{
		[Constructable]
		public MagicRobe()
		{
			Resource = CraftResource.None;
			Name = "robe";
			Hue = RandomThings.GetRandomColor(0);
			if ( Utility.RandomBool() )
				Hue = RandomThings.GetRandomSpecialColor();
			Layer = Layer.OuterTorso;
			Weight = 3.0;

			switch( Utility.RandomMinMax( 1, 32 ) )
			{
				case 1: ItemID = Utility.RandomList( 0x230E, 0x230D ); Name = "gilded dress"; break;
				case 2: ItemID = Utility.RandomList( 0x1F00, 0x1EFF ); Name = "fancy dress"; break;
				case 3: ItemID = Utility.RandomList( 0x1F01, 0x1F02 ); Name = "dress"; break;
				case 4: ItemID = Utility.RandomList( 0x1F03, 0x1F04, 0x26AE ); Name = "robe"; break;
				case 5: ItemID = Utility.RandomList( 0x204E ); Name = "shroud"; break;
				case 6: ItemID = Utility.RandomList( 0x2B69, 0x3160 ); Name = "assassin robe"; break;
				case 7: ItemID = Utility.RandomList( 0x2B70, 0x3167 ); Name = "magistrate robe"; break;
				case 8: ItemID = Utility.RandomList( 0x2B6C, 0x3163 ); Name = "gilded robe"; break;
				case 9: ItemID = Utility.RandomList( 0x2FB9, 0x3173 ); Name = "assassin shroud"; break;
				case 10: ItemID = Utility.RandomList( 0x2FBA, 0x3174 ); Name = "necromancer robe"; break;
				case 11: ItemID = Utility.RandomList( 0x3175, 0x3178 ); Name = "sorcerer robe"; break;
				case 12: ItemID = Utility.RandomList( 0x2B6A, 0x3161 ); Name = "fancy robe"; break;
				case 13: ItemID = Utility.RandomList( 0x2B6E, 0x3165 ); Name = "ornate robe"; break;
				case 14: ItemID = Utility.RandomList( 0x2B73, 0x316A ); Name = "royal robe"; break;
				case 15: ItemID = Utility.RandomList( 0x2FC6, 0x2FC7 ); Name = "spider robe"; break;
				case 16: ItemID = Utility.RandomList( 0x2B6B, 0x3162 ); Name = "jester coat";
					switch( Utility.RandomMinMax( 0, 2 ) )
					{
						case 1: ItemID = 0x4C16; Name = "jester garb"; break;
						case 2: ItemID = 0x4C17; Name = "fool's coat"; break;

					}
					break;
				case 17: ItemID = Utility.RandomList( 0x201B, 0x201C ); Name = "dragon robe"; break;
				case 18: ItemID = Utility.RandomList( 0x201F, 0x2020 ); Name = "chaos robe"; break;
				case 19: ItemID = Utility.RandomList( 0x201D, 0x201E ); Name = "vampire robe"; break;
				case 20: ItemID = 0x567E; Name = "pirate coat"; break;
				case 21: ItemID = 0x567D; Name = "vagabond robe"; break;
				case 22: ItemID = Utility.RandomList( 0x2799, 0x27E4 ); 
					switch( Utility.RandomMinMax( 1, 5 ) )
					{
						case 1: Name = "sorcerer robe"; break;
						case 2: Name = "magician robe"; break;
						case 3: Name = "conjurer robe"; break;
						case 4: Name = "mage robe"; break;
						case 5: Name = "warlock robe"; break;
					}
					break;
				case 23: ItemID = 0x283;	Name = "exquisite robe"; break;
				case 24: ItemID = 0x284;	Name = "prophet robe"; break;
				case 25: ItemID = 0x285;	Name = "elegant robe"; break;
				case 26: ItemID = 0x286;	Name = "formal robe"; break;
				case 27: ItemID = 0x287;	Name = "archmage robe"; break;
				case 28: ItemID = 0x288;	Name = "priest robe"; break;
				case 29: ItemID = 0x289;	Name = "cult robe"; break;
				case 30: ItemID = 0x28A;	Name = "gilded dark robe"; break;
				case 31: ItemID = 0x301;	Name = "gilded light robe"; break;
				case 32: ItemID = 0x302;	Name = "sage robe"; break;
			}

			//Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;
		}

		public MagicRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}