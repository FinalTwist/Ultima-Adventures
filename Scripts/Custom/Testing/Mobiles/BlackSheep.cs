using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a sheep corpse" )]
	public class BlackSheep : BaseCreature
	{
		[Constructable]
		public BlackSheep() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a sheep";
			Body = 0xCF;
			BaseSoundID = 0xD6;
            Hue = 1907;

            Item blackcloth = new Cloth();
            blackcloth.Movable = true;
            blackcloth.Hue = 2019;
            AddItem(blackcloth);

			SetStr( 19 );
			SetDex( 25 );
			SetInt( 5 );

            SetHits(25, 35);
			SetMana( 0 );

            SetDamage(2, 4);

            VirtualArmor = 5;

            SetSkill( SkillName.Tactics, 100.0, 100.0 );
            SetSkill( SkillName.MagicResist, 5.0 );
            SetSkill( SkillName.Wrestling, 20.0, 25.0 );

			Fame = 300;
			Karma = 0;			

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 11.1;
		}

        public override void GenerateLoot()
        {
            
        }

		public override int Meat{ get{ return 3; } }
		public override MeatType MeatType{ get{ return MeatType.LambLeg; } }


        public BlackSheep( Serial serial ) : base( serial )
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