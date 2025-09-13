using System;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly essence" )]
	public class LostKnight : BaseCreature
	{
		[Constructable]
		public LostKnight() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "barb_male" );
			Title = "the lost knight";
			BaseSoundID = 412;
			Hue = 1;
			Body = 0x190;

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 200, 300 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 100.0 );
			SetSkill( SkillName.Anatomy, 125.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 83.5, 92.5 );
			SetSkill( SkillName.Swords, 125.0 );
			SetSkill( SkillName.Tactics, 125.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 20;

			Item armor1 = new PlateChest();
				AddItem( armor1 );
			Item armor2 = new PlateArms();
				AddItem( armor2 );
			Item armor3 = new PlateLegs();
				AddItem( armor3 );
			Item armor4 = new PlateGorget();
				AddItem( armor4 );
			Item armor5 = new PlateGloves();
				AddItem( armor5 );
			Item armor6 = new PlateHelm();
				AddItem( armor6 );
			Item armor7 = new Longsword();
				AddItem( armor7 );
			Item armor8 = new OrderShield();
				AddItem( armor8 );

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, 0x47E );

			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				switch( Utility.RandomMinMax( 0, 7 ) )
				{
					case 0: armor1.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor1, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
					case 1: armor2.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor2, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
					case 2: armor3.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor3, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
					case 3: armor4.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor4, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
					case 4: armor5.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor5, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
					case 5: armor6.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor6, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
					case 6: armor7.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor7, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("weapons") ); break;
					case 7: armor8.LootType = LootType.Regular; MorphingItem.MorphMyItem( armor8, "IGNORED", "Spectral", "IGNORED", MorphingTemplates.TemplateLostKnight("armors") ); break;
				}
			}

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Rich );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			this.Hue = 0x47E;
			return base.OnBeforeDeath();
		}

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public LostKnight( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}