using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a gargoyle corpse" )]
	public class CosmicGargoyle : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 30; } }
		public override int BreathFireDamage{ get{ return 30; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 0; } }
		public override int BreathEnergyDamage{ get{ return 40; } }
		public override int BreathEffectHue{ get{ return 0; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathEffectSound{ get{ return 0x54A; } }
		public override int BreathEffectItemID{ get{ return 0x28EF; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 0 ); }

		[Constructable]
		public CosmicGargoyle() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace =2;
			Name = NameList.RandomName( "drakkul" );
			Title = "the cosmic gargoyle";
			Body = 127;
			BaseSoundID = 0x174;

			SetStr( 360, 550 );
			SetDex( 102, 150 );
			SetInt( 152, 200 );

			SetHits( 282, 385 );

			SetDamage( 7, 14 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 120.4, 160.0 );
			SetSkill( SkillName.Anatomy, 50.5, 100.0 );
			SetSkill( SkillName.Swords, 90.1, 100.0 );
			SetSkill( SkillName.Macing, 90.1, 100.0 );
			SetSkill( SkillName.Fencing, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 50;

			bool keepSword = true;
				if ( Utility.RandomMinMax( 1, 20 ) == 1 ){ keepSword = false; }

			if ( Utility.RandomBool() ){ Item sword = new LightSword(); if ( keepSword ){ sword.LootType = LootType.Blessed; } AddItem( sword ); }
			else { Item swords = new DoubleLaserSword(); if ( keepSword ){ swords.LootType = LootType.Blessed; } AddItem( swords ); }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.Gems, 3 );
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 3; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }


		public CosmicGargoyle( Serial serial ) : base( serial )
		{
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