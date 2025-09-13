//Amherst Script
using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles;
using Server.Targeting; 

namespace Server.Items 
{ 
   	public class PetEasingTarget : Target 
   	{ 
      	private PetEasingDeed m_Deed; 

      	public PetEasingTarget( PetEasingDeed deed ) : base( 1, false, TargetFlags.None ) 
      	{ 
         	m_Deed = deed; 
      	} 

      	protected override void OnTarget( Mobile from, object target ) 
      	{ 
         	if ( target is BaseCreature ) 
			{ 
				BaseCreature k = ( BaseCreature ) target; 

				if ( !k.Tamable )
					{ 
							from.SendMessage( "That pet is not Tamable..." );
					} 
					
				else if ( from.Skills[SkillName.AnimalTaming].Base  < k.MinTameSkill )
					{ 
							from.SendMessage( "You have no chance of affecting this animal..." );
					} 
		
				else if ( k.ControlMaster != from ) 
					{ 
						from.SendMessage( "This is not your pet" ); 
					} 
					
				else  
				 
					{ 

					k.MinTameSkill -= 10;
					from.SendMessage( "The Pet seems easier to control!" );
					m_Deed.Delete(); // Delete the deed 
					} 

			} 
         else 
         { 
            from.SendMessage( "That is not a valid traget." );  
         } 
      } 
   } 

   	public class PetEasingDeed : Item
   	{ 
      		[Constructable] 
      		public PetEasingDeed() : base( 0x14F0 ) 
      		{ 
			Weight = 1.0; 
			Name = "a pet easing deed";
			Hue = 589;
      		} 

      	public PetEasingDeed( Serial serial ) : base( serial ) 
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
 
         	//LootType = LootType.Blessed; 

         	int version = reader.ReadInt(); 
      	} 

      	public override bool DisplayLootType{ get{ return false; } } 

      	public override void OnDoubleClick( Mobile from ) 
      	{ 
		if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack 
		{ 
             		from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it. 
         	} 
         	else 
         	{ 
            		from.SendMessage( "Choose the pet you wish to ease." );  
            		from.Target = new PetEasingTarget( this ); 
          	} 
      	}    
} 
}