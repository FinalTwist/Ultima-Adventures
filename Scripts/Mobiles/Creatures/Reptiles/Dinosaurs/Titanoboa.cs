using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a dinosaur corpse" )]
	public class Titanoboa : BaseCreature
	{
		[Constructable]
		public Titanoboa() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.86)
		{
			Name = "a greater titanoboa";
			Body = 0x15;
			Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );

			BaseSoundID = 219;

			SetStr( 286, 415 );
			SetDex( 56, 180 );
			SetInt( 66, 85 );

			SetHits( 212, 329 );
			SetMana( 0 );

			SetDamage( 9, 27 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 60 );

			SetResistance( ResistanceType.Physical, 30, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 30 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Poisoning, 70.1, 100.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4000;
			Karma = -3500;

			VirtualArmor = 40;

			Item Venom = new VenomSack();
				Venom.Name = "greater venom sack";
				AddItem( Venom );
		}
		else
		{
			Name = "a titanoboa";
			Body = 0x15;
			Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );

			BaseSoundID = 219;

			SetStr( 286, 315 );
			SetDex( 56, 80 );
			SetInt( 66, 85 );

			SetHits( 212, 229 );
			SetMana( 0 );

			SetDamage( 9, 19 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 60 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Poisoning, 70.1, 100.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;

			Item Venom = new VenomSack();
				Venom.Name = "greater venom sack";
				AddItem( Venom );
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }
		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Scales{ get{ return 2; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Dinosaur; } }

		public Titanoboa(Serial serial) : base(serial)
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