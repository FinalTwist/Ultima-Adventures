using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a corpse Of Mondains Mount " )]
	[TypeAlias( "Server.Mobiles.BrownHorse", "Server.Mobiles.DirtyHorse", "Server.Mobiles.GrayHorse", "Server.Mobiles.TanHorse" )]
	public class MondainsMount : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xCC, 0x3EA2
			};

		[Constructable]
		public MondainsMount() : this( "a Mondains Mount" )
		{
		}

		[Constructable]
		public MondainsMount( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = m_IDs[random * 2];
			ItemID = m_IDs[random * 2 + 1];
			BaseSoundID = 0xA8;
			Hue = 1176;

			SetStr( 800 );
                                               SetDex( 789, 812 );
                                               SetInt( 778, 800 );
                                               SetHits( 7500, 8000 );
                                               SetDamage( 50 );
                                               SetDamageType( ResistanceType.Physical, 100 );
                                               SetDamageType( ResistanceType.Cold, 98 );
                                               SetDamageType( ResistanceType.Fire, 98 );
                                               SetDamageType( ResistanceType.Energy, 98 );
                                               SetDamageType( ResistanceType.Poison, 98 );

                                               SetResistance( ResistanceType.Physical, 100 );
                                               SetResistance( ResistanceType.Cold, 80 );
                                               SetResistance( ResistanceType.Fire, 80 );
                                               SetResistance( ResistanceType.Energy, 80 );
                                               SetResistance( ResistanceType.Poison, 80 );


						SetSkill( SkillName.EvalInt, 150.4, 200.0 );
						SetSkill( SkillName.Magery, 130.4, 150.0 );
						SetSkill( SkillName.MagicResist, 135.3, 150.0 );
						SetSkill( SkillName.Tactics, 137.6, 158.0 );
						SetSkill( SkillName.Wrestling, 130.5, 152.5 );

                                               Fame = 1200;
                                               Karma = 1200;
                                               VirtualArmor = 60;
     
			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 110.0;
		}


		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );			
		}

		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public MondainsMount( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}