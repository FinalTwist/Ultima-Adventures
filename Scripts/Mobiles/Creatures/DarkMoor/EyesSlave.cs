      //////////////////////////////////////////////////
     //////////////////////////////////////////////////
    /////       Created By: 	 Murrer.    //////////
   /////       Scripted By:	     Murrer.   //////////
  //////////////////////////////////////////////////
 //////////////////////////////////////////////////


using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class EyesSlave : BaseCreature 
	{ 
		[Constructable] 
		public EyesSlave() : base( AIType.AI_Archer, FightMode.Aggressor, 10, 1, 0.2, 0.4 ) 
		{ 
			InitStats( 100, 125, 25 ); 
			Title = "The Slave Of Eyeball"; 

			SpeechHue = Utility.RandomDyedHue(); 

			Hue = Utility.RandomSkinHue(); 

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 0x191; 
				Name = NameList.RandomName( "female" ); 
			} 
			else 
			{ 
				Body = 0x190; 
				Name = NameList.RandomName( "male" ); 
			} 

			
			SlaveCollar Gorget = new SlaveCollar(); 
			Gorget.Hue = 0x966; 
			AddItem( Gorget ); 
			SlavePants legs = new SlavePants(); 
			legs.Hue = 0x966; 
			AddItem( legs ); 
			


			PackItem( new Apple( 5 ) );
			PackGold( 250, 500 );

			
			Skills[SkillName.Tactics].Base = 120.0; 
			Skills[SkillName.Wrestling].Base = 120.0; 
			Skills[SkillName.Anatomy].Base = 120.0; 
			

		} 

		public EyesSlave( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 
        public override bool AlwaysMurderer{ get{ return true; } }
		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
}