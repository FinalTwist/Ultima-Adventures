using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a Samurai Dragon corpse" )]
	public class SamuraiDragon : BaseCreature
	{
        private DateTime last = DateTime.UtcNow;
        private TimeSpan delay = TimeSpan.FromSeconds(30);

        public DateTime Limit;
        public ArrayList Given;

		[Constructable]
		public SamuraiDragon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Samurai Dragon";
			Body = 197;
			BaseSoundID = 362;

			SetStr( 2600, 3000 );
			SetDex( 2060, 2380 );
			SetInt( 1060, 1140 );

			SetHits( 24000, 26000 );
			SetMana( 500 );

			SetDamage( 20, 40 );

			SetDamageType( ResistanceType.Physical, 50 );
                                                 SetDamageType(ResistanceType.Fire, 50);
			SetResistance( ResistanceType.Physical, 80, 85 );
                                                SetResistance(ResistanceType.Fire, 100);
                                                SetResistance(ResistanceType.Cold, 50, 60);
                                                SetResistance(ResistanceType.Poison, 80, 85);
                                                SetResistance(ResistanceType.Energy, 80, 85);

			SetSkill( SkillName.MagicResist, 120.0, 140.0 );
			SetSkill( SkillName.Tactics, 110.1, 120.0 );
			SetSkill( SkillName.Wrestling, 119.1, 125.0 );
                                                SetSkill
(SkillName.EvalInt, 110.2, 125.3);
                                                SetSkill (SkillName.Magery, 110.9, 125.5);
                                                SetSkill(SkillName.Meditation, 119.4, 130.0);
                                                SetSkill(SkillName.Meditation, 119.4, 130.0);
                                                SetSkill(SkillName.Anatomy, 118.7, 125.0);
                                                SetSkill(SkillName.DetectHidden, 120.0);


			Fame = 20000;
			Karma = -20000;

			VirtualArmor = 70;

			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 120;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public SamuraiDragon( Serial serial ) : base( serial )
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
                        this.Say("Guess you really can't fix stupid�!");
                        break;
                    }
                case 2:
                    {
                        this.Say("You won't like me when im angry");
                        break;
                    }
                case 3:
                    {
                        this.Say("Man they were scraping bottom of barrel when they sent you!");
                        break;
                    }
                case 4:
                    {
                        this.Say("*Dinner Time*!");
                        break;
                    }
                case 5:
                    {
                        this.Say("Humans the other white meat*.");
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