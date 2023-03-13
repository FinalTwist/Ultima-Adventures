using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an aqueath corpse" )]
	public class AqueathSpawn : BaseCreature
	{
		[Constructable]
		public AqueathSpawn() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Aqueath Spawn";
			Body = 733;
			BaseSoundID = 0x553;
			CanSwim = true;

			SetStr( 196, 220 );
			SetDex( 186, 205 );
			SetInt( 136, 160 );

			SetHits( 100, 120 );

			SetDamage( 9, 15 );

            SetDamageType(ResistanceType.Poison, 100);

            SetResistance( ResistanceType.Physical, 55, 70 );
			SetResistance( ResistanceType.Fire, 25, 50 );
			SetResistance( ResistanceType.Cold, 25, 50 );
			SetResistance( ResistanceType.Poison, 10, 20 );

			SetSkill( SkillName.MagicResist, 35.1, 60.0 );
			SetSkill( SkillName.Tactics, 65.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 90.0 );

			Fame = 3500;
			Karma = 3500;

			VirtualArmor = 28;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 2; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public override int GetAttackSound(){ return 0x5FD; }	// A
		public override int GetDeathSound(){ return 0x5FE; }	// D
		public override int GetHurtSound(){ return 0x5FF; }		// H

		public AqueathSpawn( Serial serial ) : base( serial )
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