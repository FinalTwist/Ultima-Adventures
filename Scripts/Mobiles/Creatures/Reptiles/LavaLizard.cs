using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a lava lizard corpse" )]
	[TypeAlias( "Server.Mobiles.Lavalizard" )]
	public class LavaLizard : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public LavaLizard() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater lava lizard";
			Body = 206;
			Hue = 0xB73;
			BaseSoundID = 0x289;

			SetStr( 126, 250 );
			SetDex( 56, 175 );
			SetInt( 11, 20 );

			SetHits( 76, 190 );
			SetMana( 0 );

			SetDamage( 6, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 30, 55 );
			SetResistance( ResistanceType.Poison, 25, 45 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3500;
			Karma = -3000;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 85.7;

			PackItem( new SulfurousAsh( Utility.Random( 4, 10 ) ) );
			AddItem( new LightSource() );
		}
		else
		{
			Name = "a lava lizard";
			Body = 206;
			Hue = 0xB73;
			BaseSoundID = 0x289;

			SetStr( 126, 150 );
			SetDex( 56, 75 );
			SetInt( 11, 20 );

			SetHits( 76, 90 );
			SetMana( 0 );

			SetDamage( 6, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 80.7;

			PackItem( new SulfurousAsh( Utility.Random( 4, 10 ) ) );
			AddItem( new LightSource() );
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Volcanic; } }

		public LavaLizard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}