using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a pile of dust" )]
	public class GraveDustElemental : BaseCreature
	{
		[Constructable]
		public GraveDustElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a grave dust elemental";
			Body = 142;
			BaseSoundID = 268;
			Hue = 1150;

			SetStr( 256, 385 );
			SetDex( 196, 215 );
			SetInt( 221, 242 );

			SetHits( 194, 211 );

			SetDamage( 18, 29 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 40.5, 90.0 );
			SetSkill( SkillName.Magery, 40.5, 90.0 );
			SetSkill( SkillName.MagicResist, 60.1, 110.0 );
			SetSkill( SkillName.Tactics, 100.1, 130.0 );
			SetSkill( SkillName.Wrestling, 90.1, 120.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 60;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( this.Body == 13 && willKill == false && Utility.Random( 4 ) == 1 )
			{
				this.Body = 14;
				this.BaseSoundID = 268;
			}
			else if ( willKill == false && Utility.Random( 4 ) == 1 )
			{
				this.Body = 13;
				this.BaseSoundID = 768;
			}

			base.OnDamage( amount, from, willKill );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			this.BaseSoundID = 768;
			return base.OnBeforeDeath();
		}

		public override void GenerateLoot()
		{
			PackItem( new GraveDust( Utility.RandomMinMax( 100, 200 ) ) );
			AddLoot( LootPack.Average, 1 );
			AddLoot( LootPack.Rich, 1 );
		}
		public override bool BleedImmune{ get{ return true; } }

		public GraveDustElemental( Serial serial ) : base( serial )
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
