using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a pile goo" )]
	public class CreepingFungus : BaseCreature
	{
		[Constructable]
		public CreepingFungus () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a creeping fungus";
			Body = 367;
			Hue = 0xB79;

			SetStr( 162, 184 );
			SetDex( 56, 71 );
			SetInt( 56, 70 );

			SetHits( 170, 200 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Poison, 80 );
			SetResistance( ResistanceType.Energy, 80 );

			SetSkill( SkillName.Poisoning, 36.0, 49.1 );
			SetSkill(SkillName.Anatomy, 0);
			SetSkill( SkillName.MagicResist, 15.9, 18.9 );
			SetSkill( SkillName.Tactics, 24.6, 26.1 );
			SetSkill( SkillName.Wrestling, 24.9, 26.1 );

			Fame = 1200;
			Karma = -1200;

			VirtualArmor = 32;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Rich );
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
					MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, "fungal slime", 0xB79, 0 );
				}
			}
		}

        public override int GetAngerSound(){ return 0x581; }
        public override int GetIdleSound(){ return 0x582; }
        public override int GetAttackSound(){ return 0x580; }
        public override int GetHurtSound(){ return 0x583; }
        public override int GetDeathSound(){ return 0x584; }

		public override Poison PoisonImmune { get { return Poison.Lethal; } }
		public override Poison HitPoison { get { return Poison.Deadly; } }
		public override bool BleedImmune{ get{ return true; } }

		public CreepingFungus( Serial serial ) : base( serial )
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