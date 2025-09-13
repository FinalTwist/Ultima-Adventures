using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class BritainMage : BaseRanger 
	{ 

		[Constructable] 
		public BritainMage() : base( AIType.AI_Mage, FightMode.Weakest, 10, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Mage[Expeditionary Forces]"; 

			AddItem( new Boots() );
			AddItem( new WizardsHat(38) );
			AddItem( new Cloak(38) );
			AddItem( new LeatherGloves() );
			AddItem( new BodySash(941) );
			AddItem( new LeatherLegs());

            Fame = 10000;
            Karma = 10000;

            SetStr(86, 100);
            SetDex(81, 100);
            SetInt(100, 120);

            SetDamage(18, 26);

            SetHits(100, 152);

            SetResistance(ResistanceType.Physical, 70, 80);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 60, 65);

            SetSkill( SkillName.MagicResist, 120.0,120.0 );
			SetSkill( SkillName.Magery, 120.0, 120.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 120.0,120.0 );

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
				
				AddItem( new LeatherChest());
				AddItem( new LeatherArms());
				


			}

			Utility.AssignRandomHair( this );
		}


		public BritainMage( Serial serial ) : base( serial ) 
		{ 
		}
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
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