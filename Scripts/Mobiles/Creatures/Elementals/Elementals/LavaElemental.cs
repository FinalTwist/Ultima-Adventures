using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an elemental corpse" )]
	public class LavaElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public LavaElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lava elemental";
			Body = 698;
			BaseSoundID = 268;

			SetStr( 126, 155 );
			SetDex( 166, 185 );
			SetInt( 101, 125 );

			SetHits( 76, 93 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 75 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 60.1, 75.0 );
			SetSkill( SkillName.MagicResist, 75.2, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 70.1, 100.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;
			ControlSlots = 4;

			PackItem( new SulfurousAsh( 10 ) );

			AddItem( new LighterSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public LavaElemental( Serial serial ) : base( serial )
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
