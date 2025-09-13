using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName("a giant snake corpse")]
	[TypeAlias( "Server.Mobiles.Silverserpant" )]
	public class GoldenSerpent : BaseCreature
	{
		[Constructable]
		public GoldenSerpent() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Body = 21;
			Hue = Utility.RandomList( 0xB54, 0xB1B, 0x99A, 0x993, 0x82B );
			Name = "a greater golden serpent";
			BaseSoundID = 219;

			SetStr( 191, 500 );
			SetDex( 191, 440 );
			SetInt( 41, 60 );

			SetHits( 147, 366 );

			SetDamage( 9, 32 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 5, 20 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 95.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 100.0 );

			Fame = 7900;
			Karma = -7400;

			VirtualArmor = 50;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.9;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}
		else
		{
			Body = 21;
			Hue = Utility.RandomList( 0xB54, 0xB1B, 0x99A, 0x993, 0x82B );
			Name = "a golden serpent";
			BaseSoundID = 219;

			SetStr( 191, 400 );
			SetDex( 191, 340 );
			SetInt( 41, 60 );

			SetHits( 147, 266 );

			SetDamage( 9, 26 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 95.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 95.0 );
			SetSkill( SkillName.Wrestling, 85.1, 100.0 );

			Fame = 7400;
			Karma = -7400;

			VirtualArmor = 50;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}
	}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			GoldenSerpentVenom ingut = new GoldenSerpentVenom();
   			ingut.Amount = Utility.RandomMinMax( 1, 5 );
   			c.DropItem(ingut);
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
		}

		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public GoldenSerpent(Serial serial) : base(serial)
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

			if ( BaseSoundID == -1 )
				BaseSoundID = 219;
		}
	}
}