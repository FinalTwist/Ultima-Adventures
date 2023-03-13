using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a mongbat corpse" )]
	public class MeanLookingMongbat : BaseCreature
	{
        private DateTime last = DateTime.UtcNow;
        private TimeSpan delay = TimeSpan.FromSeconds(30);

        public DateTime Limit;
        public ArrayList Given;

		[Constructable]
		public MeanLookingMongbat() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Meanlookin Mongbat";
			Body = 39;
			BaseSoundID = 422;
                        Hue = Utility.RandomMinMax(70,2520);
			SetStr( 600, 1000 );
			SetDex( 260, 380 );
			SetInt( 60, 140 );

			SetHits( 400, 600 );
			SetMana( 0 );

			SetDamage( 20, 40 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 100 );

			SetSkill( SkillName.MagicResist, 50.0, 140.0 );
			SetSkill( SkillName.Tactics, 50.1, 100.0 );
			SetSkill( SkillName.Wrestling, 50.1, 100.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 90;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 110;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public MeanLookingMongbat( Serial serial ) : base( serial )
		{
		}


             public override void OnThink()
        {
            if ((last + delay) > DateTime.UtcNow)
                return;
            else
                last = DateTime.UtcNow;

            SayRandom();
        }

        public void SayRandom()
        {
            switch (Utility.Random(1, 5))
            {
                case 1:
                    {
                        this.Say("Im about to open up a big can o�Whopass on yall!");
                        break;
                    }
                case 2:
                    {
                        this.Say("What you say about my mama!?");
                        break;
                    }
                case 3:
                    {
                        this.Say("Im�a get medival on yo ass!");
                        break;
                    }
                case 4:
                    {
                        this.Say("*bite, bite, bite* Crush, Kill, Destroy");
                        break;
                    }
                case 5:
                    {
                        this.Say("You are terminated! *Robotic voice*");
                        break;
                    }
            }
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