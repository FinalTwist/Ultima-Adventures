using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class Barbarian : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Barbarian() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Barbarian";
			Hue = Utility.RandomSkinHue();

            
			if (this.Female)
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( Utility.RandomNeutralHue() ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}

			SetStr( 90, 110 );
			SetDex( 75, 95 );
			SetInt( 60, 70 );

			SetDamage( 15, 23 );

			SetSkill( SkillName.Fencing, 60.0, 80.0 );
			SetSkill( SkillName.Macing, 60.0, 80.0 );
			SetSkill( SkillName.MagicResist, 60.0, 80.0 );
			SetSkill( SkillName.Swords, 60.0, 80.0 );
			SetSkill( SkillName.Tactics, 60.0, 80.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );

			Fame = 1000;
			Karma = -1000;

			AddItem( new FurBootsofTheBarbarian());
			AddItem( new BearMaskofTheBarbarian());
			AddItem( new FurCapeofTheBarbarian());
			AddItem( new FurLoinclothofTheBarbarian());
			AddItem( new OrcBoneArmsofTheBarbarian());
			AddItem( new OrcBoneGlovesofTheBarbarian());
			AddItem( new OrcSkinGorgetofTheBarbarian());

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
			}

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public Barbarian( Serial serial ) : base( serial )
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
