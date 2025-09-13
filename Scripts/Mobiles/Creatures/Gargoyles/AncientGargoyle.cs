using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a gargoyle corpse" )]
	public class AncientGargoyle : BaseCreature
	{
		[Constructable]
		public AncientGargoyle() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace =2;
			Name = "an ancient gargoyle";
			Body = 102;
			BaseSoundID = 0x174;

			SetStr( 360, 550 );
			SetDex( 102, 150 );
			SetInt( 152, 200 );

			SetHits( 282, 385 );

			SetDamage( 7, 14 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

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

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new GargoylesPickaxe() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Gems, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 3; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }

		public override void OnDamagedBySpell( Mobile from )
		{
			if( from != null && from.Alive && 0.4 > Utility.RandomDouble() )
			{
				ThrowHatchet( from );
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if( attacker != null && attacker.Alive && attacker.Weapon is BaseRanged && 0.4 > Utility.RandomDouble() )
			{
				ThrowHatchet( attacker );
			}
		}

		public void ThrowHatchet( Mobile to )
		{
			int damage = 50;
			this.MovingEffect( to, 0xF43, 10, 0, false, false );
			this.DoHarmful( to );
			AOS.Damage( to, this, damage, 100, 0, 0, 0, 0 );
		}

		public AncientGargoyle( Serial serial ) : base( serial )
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