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
	public class GrundulVarg : BaseCreature
	{
		[Constructable]
		public GrundulVarg() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Grundul Varg";
			Title = "the Slayer of Men";
			BaseSoundID = 412;
			Hue = 1150;
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

			AddItem( new PlateChest() );
			AddItem( new PlateArms() );
			AddItem( new PlateLegs() );
			AddItem( new PlateGorget() );
			AddItem( new PlateGloves() );
			AddItem( new OrcHelm() );
			AddItem( new RoyalSword() );
			AddItem( new ChaosShield() );

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, 1167 );

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
			this.Hue = 1167;
			return base.OnBeforeDeath();
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						switch( Utility.RandomMinMax( 0, 8 ) )
						{
							case 0: Item loot1 = new PlateChest(); loot1.Name = "tunic"; MorphingItem.MorphMyItem( loot1, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot1 ); break;
							case 1: Item loot2 = new PlateArms(); loot2.Name = "arms"; MorphingItem.MorphMyItem( loot2, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot2 ); break;
							case 2: Item loot3 = new PlateLegs(); loot3.Name = "leggings"; MorphingItem.MorphMyItem( loot3, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot3 ); break;
							case 3: Item loot4 = new PlateGorget(); loot4.Name = "gorget"; MorphingItem.MorphMyItem( loot4, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot4 ); break;
							case 4: Item loot5 = new PlateGloves(); loot5.Name = "guantlets"; MorphingItem.MorphMyItem( loot5, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot5 ); break;
							case 5: Item loot6 = new OrcHelm(); loot6.Name = "helm"; MorphingItem.MorphMyItem( loot6, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot6 ); break;
							case 6: Item loot7 = new RoyalSword(); loot7.Name = "sword"; MorphingItem.MorphMyItem( loot7, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("weapons") ); c.DropItem( loot7 ); break;
							case 7: Item loot8 = new ChaosShield(); loot8.Name = "shield"; MorphingItem.MorphMyItem( loot8, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("armors") ); c.DropItem( loot8 ); break;
							case 8: Item loot9 = Loot.RandomJewelry(); MorphingItem.MorphMyItem( loot9, "IGNORED", "Grundul Varg's", "IGNORED", MorphingTemplates.TemplateGrundulVarg("misc") ); c.DropItem( loot9 ); break;
						}
					}
				}
			}
		}

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 3; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public GrundulVarg( Serial serial ) : base( serial )
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