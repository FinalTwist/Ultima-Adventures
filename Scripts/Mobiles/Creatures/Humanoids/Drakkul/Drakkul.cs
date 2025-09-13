using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a drakkul corpse" )]
	public class Drakkul : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public Drakkul() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "drakkul" );
			Title = "the drakkul";
			Body = 668;
			BaseSoundID = 357;

			SetStr( 246, 275 );
			SetDex( 176, 195 );
			SetInt( 181, 205 );

			SetHits( 188, 205 );

			SetDamage( 9, 18 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 55 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 45, 55 );

			SetSkill( SkillName.MagicResist, 50.1, 65.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 50.1, 90.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 42;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 2; } }
		public override int Scales{ get{ return 2; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Black ); } }

		public Drakkul( Serial serial ) : base( serial )
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