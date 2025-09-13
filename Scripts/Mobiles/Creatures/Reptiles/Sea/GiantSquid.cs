using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a squid corpse" )]
	public class GiantSquid : BaseCreature
	{
		[Constructable]
		public GiantSquid() : base( AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant squid";
			Body = 77;
			BaseSoundID = 353;
			Hue = 0x8AB;

			SetStr( 556, 580 );
			SetDex( 126, 145 );
			SetInt( 26, 40 );

			SetHits( 554, 668 );
			SetMana( 0 );

			SetDamage( 30, 60 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 60;

			CanSwim = true;
			CantWalk = true;
			
			RangeFight = 3;
			
			//AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public GiantSquid( Serial serial ) : base( serial )
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
			
			//AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}
	}
}
