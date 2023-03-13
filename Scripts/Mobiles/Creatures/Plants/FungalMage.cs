using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fungal corpse" )]
	public class FungalMage : BaseCreature
	{
		[Constructable]
		public FungalMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fungal mage";
			Body = 342;

			SetStr( 181, 205 );
			SetDex( 191, 215 );
			SetInt( 96, 120 );

			SetHits( 109, 123 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 25 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 30;

			PackReg( 10 );

			AddItem( new FoodToadStool() );
			AddItem( new FoodToadStool() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Potions );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				int goo = 0;

				foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == "fungal slime" ){ goo++; } }

				if ( goo == 0 )
				{
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "fungal slime", 0x4FC, 0 );
				}
			}
		}

		public override bool CanRummageCorpses{ get{ return true; } }

		public override int GetAngerSound()
		{
			return 0x451;
		}

		public override int GetIdleSound()
		{
			return 0x452;
		}

		public override int GetAttackSound()
		{
			return 0x453;
		}

		public override int GetHurtSound()
		{
			return 0x454;
		}

		public override int GetDeathSound()
		{
			return 0x455;
		}

		public FungalMage( Serial serial ) : base( serial )
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