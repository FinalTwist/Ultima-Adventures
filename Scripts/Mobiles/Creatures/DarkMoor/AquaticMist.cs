using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an Aquatic Mist corpse" )]
	public class AquaticMist : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public AquaticMist () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an Aquatic Mist";
			Body = 16;
			BaseSoundID = 278;
            Hue = 100;
			
            SetStr( 126, 155 );
			SetDex( 66, 85 );
			SetInt( 101, 125 );

			SetHits( 76, 193 );

			SetDamage( 12, 15 );

			SetDamageType( ResistanceType.Physical, 50 );
            SetDamageType(ResistanceType.Cold, 50);

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 10, 25 );
			SetResistance( ResistanceType.Cold, 50, 55 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 60.1, 75.0 );
			SetSkill( SkillName.MagicResist, 100.1, 115.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 4500;
			Karma = 4500;

			VirtualArmor = 40;
			ControlSlots = 3;
			CanSwim = true;

			PackItem( new BlackPearl( 3 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 8 );
			AddLoot( LootPack.Meager, 10 );
			AddLoot( LootPack.Potions );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public AquaticMist( Serial serial ) : base( serial )
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