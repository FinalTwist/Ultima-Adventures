using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a drakkul corpse" )]
	public class DrakkulChief : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public DrakkulChief() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "drakkul" );
			Title = "the drakkul chieftain";
			Body = 670;
			BaseSoundID = 357;

			SetStr( 446, 475 );
			SetDex( 276, 295 );
			SetInt( 181, 205 );

			SetHits( 388, 405 );

			SetDamage( 14, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 55 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 45, 55 );

			SetSkill( SkillName.MagicResist, 80.1, 95.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 8500;
			Karma = -8500;

			VirtualArmor = 54;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems, Utility.RandomMinMax( 1, 4 ) );
		}

		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 4; } }
		public override int Scales{ get{ return 4; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Black ); } }

		public DrakkulChief( Serial serial ) : base( serial )
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