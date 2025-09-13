using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("a naga corpse")]
	public class FireNaga : BaseCreature
	{
		[Constructable]
		public FireNaga() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace = 3;
			Body = 261;
			Name = "a fire naga";
			BaseSoundID = 634;

			SetStr( 161, 360 );
			SetDex( 151, 300 );
			SetInt( 21, 40 );

			SetHits( 112, 250 );

			SetDamage( 6, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 95.1, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 100.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 45;

			PackItem( new SulfurousAsh( 10 ) );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
			AddLoot( LootPack.MedScrolls, 1 );
		}

		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Hellish; } }
		public override int Meat{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

		public FireNaga(Serial serial) : base(serial)
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