using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class SpartanSage : BaseRanger
	{ 

		[Constructable] 
		public SpartanSage() : base( AIType.AI_Mage, FightMode.Weakest, 10, 5, 0.1, 0.2 ) 
		{ 
			Title = "a Spartan Sage";

            AddItem( new Sandals());
            AddItem( new Bandana(33) );
            AddItem( new Cloak(33));
			AddItem( new BodySash(33) );
            AddItem( new BoneArms());
            AddItem( new BoneLegs());

            SetStr(86, 100);
            SetDex(81, 100);
            SetInt(350, 500);

            SetDamage(18, 26);

            SetHits(150, 250);

            SetSkill( SkillName.MagicResist, 90.0,120.0 );
			SetSkill( SkillName.Magery, 100.0, 120.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 90.0,120.0 );
            Fame = 10000;
            Karma = 10000;

            SetResistance(ResistanceType.Physical, 70, 80);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 25, 35);
			
			VirtualArmor = 70;

            if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				AddItem( new FemaleLeatherChest() );
				


			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 
				
				AddItem(new LeatherChest() );
				AddItem(new LeatherArms() );
			}    

			Utility.AssignRandomHair( this );
		
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.MedScrolls, 2);
        }




		public SpartanSage( Serial serial ) : base( serial ) 
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