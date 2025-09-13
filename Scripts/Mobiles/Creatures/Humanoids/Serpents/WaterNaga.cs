using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName("a naga corpse")]
	public class WaterNaga : BaseCreature
	{
		[Constructable]
		public WaterNaga() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 66;
			Name = "a water naga";
			BaseSoundID = 644;
			CanSwim = true;

			SetStr( 161, 360 );
			SetDex( 151, 300 );
			SetInt( 21, 40 );

			SetHits( 112, 250 );

			SetDamage( 6, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 95.1, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 100.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 45;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
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
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Meat{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool BleedImmune{ get{ return true; } }

		public WaterNaga(Serial serial) : base(serial)
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