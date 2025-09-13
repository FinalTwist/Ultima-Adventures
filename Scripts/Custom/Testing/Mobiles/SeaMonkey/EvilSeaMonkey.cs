using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fuzzy monkey corpse" )]
	public class EvilSeaMonkey : BaseCreature
	{
		[Constructable]
		public EvilSeaMonkey() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a evil sea monkey";
			Body = 0x1D;
			BaseSoundID = 0x9E;
			Hue = 1157;

			SetStr( 500, 600 );
			SetDex( 87, 135 );
			SetInt( 87, 155 );

			SetHits( 1400, 1500 );

			SetDamage( 36, 48 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 60;
			CanSwim = true;
			
			switch(Utility.Random(10))
			{
			case 0:{PackItem( new SeaMonkeyHeadBand () );break;}
			}

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

		public EvilSeaMonkey( Serial serial ) : base( serial )
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
