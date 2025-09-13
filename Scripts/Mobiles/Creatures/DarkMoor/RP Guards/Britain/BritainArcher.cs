using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class BritainArcher : BaseRanger
	{ 


		[Constructable] 
		public BritainArcher() : base( AIType.AI_Archer, FightMode.Weakest, 15, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Archer [Expeditionary Forces]"; 

			AddItem( new Bow() );
			AddItem( new Boots() );
			AddItem( new Bandana(38) );
			AddItem( new Cloak(38) );
			AddItem( new StuddedGloves() );
			AddItem( new BodySash(941) );

            Fame = 10000;
            Karma = 10000;

            SetStr(86, 100);
            SetDex(81, 100);
            SetInt(72, 85);

            SetDamage(18, 26);

            SetHits(100, 152);

            SetResistance(ResistanceType.Physical, 70, 80);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 80, 90);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 25, 35);

            SetSkill( SkillName.Anatomy, 120.0, 120.0 );
			SetSkill( SkillName.Archery, 120.0, 120.0 );
			SetSkill( SkillName.Tactics, 120.0, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0, 120.0 );

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				AddItem( new LeatherSkirt() );
				AddItem( new FemaleStuddedChest() );
				
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" );

				AddItem( new StuddedChest());
				AddItem( new StuddedLegs());
				AddItem( new StuddedArms());

			}
			
			Utility.AssignRandomHair( this );
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average);
        }

        public BritainArcher( Serial serial ) : base( serial ) 
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