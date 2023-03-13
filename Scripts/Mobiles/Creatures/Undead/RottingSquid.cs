using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a squid corpse" )]
	public class RottingSquid : BaseCreature
	{
		[Constructable]
		public RottingSquid() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a rotting squid";
			Body = 77;
			BaseSoundID = 353;
			Hue = 0xB97;

			SetStr( 556, 580 );
			SetDex( 126, 145 );
			SetInt( 26, 40 );

			SetHits( 354, 368 );
			SetMana( 0 );

			SetDamage( 10, 20 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 30;

			CanSwim = true;
			CantWalk = true;
			
			RangeFight = 3;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.FilthyRich, 1 );
		}

		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Necrotic; } }

		public RottingSquid( Serial serial ) : base( serial )
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
