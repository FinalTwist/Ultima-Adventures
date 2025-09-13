using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a skellot corpse" )]
	public class Skellot : BaseCreature
	{
		[Constructable]
		public Skellot () : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a skellot";
			Body = 652;
			BaseSoundID = 0x388;

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

			Fame = 0;
			Karma = 0;

			VirtualArmor = 16;

			Item Venom = new VenomSack();
				Venom.Name = "lethal venom sack";
				AddItem( Venom );
		}

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.6; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public Skellot( Serial serial ) : base( serial )
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