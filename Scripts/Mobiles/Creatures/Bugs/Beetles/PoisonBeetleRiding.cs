using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a beetle corpse" )]
	public class PoisonBeetleRiding : BaseMount
	{
		[Constructable]
		public PoisonBeetleRiding() : this( "a poisonous beetle" )
		{
		}

		[Constructable]
		public PoisonBeetleRiding( string name ) : base( name, 0xA9, 0x3E95, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.25, 0.5 )
		{
			BaseSoundID = 0x388;
			Hue = 1167;

			SetStr( 96, 120 );
			SetDex( 86, 105 );
			SetInt( 6, 10 );

			SetHits( 80, 110 );

			SetDamage( 3, 10 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.Tactics, 55.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 75.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 16;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 39.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.6; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public PoisonBeetleRiding( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 1170;
		}
	}
}