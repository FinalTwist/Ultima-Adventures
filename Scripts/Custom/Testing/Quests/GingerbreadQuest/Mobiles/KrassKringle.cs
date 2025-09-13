/*Created by Hammerhand*/

using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("the Anti-Claus")] 
	public class KrassKringle : BaseCreature
	{
        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }

        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        private static bool m_Talked; // flag to prevent spam 

        string[] kfcsay = new string[] // things to say while greeting 
      { 
			"HoHoHo??? Ha Ha  HA!!!!",
            "So you think you're GOOD huh?",
            "I'm going to feed you to my reindeer!",
      };

		[Constructable]
		public KrassKringle() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
        {

		Name = "Krass Kringle";
        Title = "The Anti Claus";
		Female = false; 
		Body = 0x190; 
		Hue = 1873; 

		NameHue = 1272;
        FancyShirt shirt = new FancyShirt();
        shirt.Hue = 1;
        shirt.Movable = false;
        AddItem(shirt);
        Surcoat surcoat = new Surcoat();
        surcoat.Hue = 1;
        surcoat.Movable = false;
        AddItem(surcoat);
        LongPants longpants = new LongPants();
        longpants.Hue = 1;
        longpants.Movable = false;
        AddItem(longpants);
        Boots boots = new Boots();
        boots.Hue = 1;
        boots.Movable = false;
        AddItem(boots);
        WizardsHat hat = new WizardsHat();
        hat.Hue = 1;
        hat.Movable = false;
        AddItem(hat);
        LeatherGloves gloves = new LeatherGloves();
        gloves.Hue = 1;
        gloves.Movable = false;
        AddItem(gloves);
                  	
        HairItemID = 0x203C;
      	HairHue = 1153;
      
		FacialHairItemID = 0x204B;
      	FacialHairHue = 1153;
            {
	
          }

			SetStr( 410, 580 );
			SetDex( 185, 210 );
			SetInt( 300, 450 );

            SetDamage(40, 75);

            SetHits(5000, 10000);
            SetMana(5000);

            SetResistance(ResistanceType.Physical, 65, 75);
            SetResistance(ResistanceType.Fire, 35, 45);
            SetResistance(ResistanceType.Cold, 100, 110);
            SetResistance(ResistanceType.Poison, 75, 90);
            SetResistance(ResistanceType.Energy, 60, 75);


			SetSkill( SkillName.Fencing, 96.0, 117.5 );
			SetSkill( SkillName.Macing, 95.0, 107.5 );
			SetSkill( SkillName.MagicResist, 85.0, 107.5 );
			SetSkill( SkillName.Swords, 85.0, 119.5 );
			SetSkill( SkillName.Tactics, 95.0, 119.5 );
			SetSkill( SkillName.Wrestling, 75.0, 97.5 );
            SetSkill(SkillName.Anatomy, 90.1, 100.0);
            SetSkill(SkillName.MagicResist, 140.1, 150.0);
            SetSkill(SkillName.Necromancy, 100.5, 110.9);
            SetSkill(SkillName.Healing, 70.2, 79.4);

			Fame = 1000;
			Karma = -15000;
            VirtualArmor = 72;


			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
			}
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment10());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
            
        
		public override int GetIdleSound()
		{
            return 0x2F8;
		}

        public override int GetAttackSound()
        {
            return 0x2C8;
        }
        public override int GetHurtSound()
        {
            return 0x2D1;
        }

		public override int GetDeathSound()
		{
            return 0x2F7;
		}

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {
                if (m.InRange(this, 4))
                {
                    m_Talked = true;
                    SayRandom(kfcsay, this);
                    this.Move(GetDirectionTo(m.Location));
                    // Start timer to prevent spam 
                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }

        }
      private class SpamTimer : Timer 
      { 
         public SpamTimer() : base( TimeSpan.FromSeconds( 8 ) ) 
         { 
            Priority = TimerPriority.OneSecond; 
         } 

         protected override void OnTick() 
         { 
            m_Talked = false; 
         } 
      } 

      private static void SayRandom( string[] say, Mobile m ) 
      { 
         m.Say( say[Utility.Random( say.Length )] ); 
      }


        public KrassKringle(Serial serial)
            : base(serial)
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