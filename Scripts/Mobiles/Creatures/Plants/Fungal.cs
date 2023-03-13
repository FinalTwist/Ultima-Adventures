using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a fungal corpse" )]
	public class Fungal : BaseCreature
	{
		[Constructable]
		public Fungal () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fungal";
			Body = 341;

			SetStr( 166, 195 );
			SetDex( 46, 65 );
			SetInt( 46, 70 );

			SetHits( 100, 117 );
			SetMana( 0 );

			SetDamage( 9, 11 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 25 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 70.1, 80.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 32;

			AddItem( new FoodToadStool() );
			AddItem( new FoodToadStool() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
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

		public Fungal( Serial serial ) : base( serial )
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