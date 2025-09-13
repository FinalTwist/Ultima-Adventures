//Amherst Script
using System; 
using Server.Network; 
using Server.Prompts; 
using Server.Items; 
using Server.Mobiles;
using Server.Targeting; 
using Server.Misc;

namespace Server.Items 
{ 
   	public class PetControlTarget : Target 
   	{ 
      	private PetControlDeed m_Deed; 

      	public PetControlTarget( PetControlDeed deed ) : base( 1, false, TargetFlags.None ) 
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
							from.SendMessage( "You have no chance of controlling this animal..." );
					} 
		
				else if ( k.ControlMaster != null ) 
					{ 
						from.SendMessage( "That is alrady tamed!" ); 
					} 
					
				else  
				 
					{ 

							if (k.Map != null)
							{
								int Heat = MyServerSettings.GetDifficultyLevel( k.Location, k.Map );
								if (Heat > 0 )
									Server.Mobiles.BaseCreature.BeefDown(k, Heat); //final beefdown to adjust for beefup in dungeons
							}

					k.Controlled = true;
					k.SetControlMaster( from );
					k.ControlOrder = OrderType.Follow; 
					from.SendMessage( "You instantly tame the pet!" );
					m_Deed.Delete(); // Delete the deed 
					} 

			} 
         else 
         { 
            from.SendMessage( "That is not a valid traget." );  
         } 
      } 
   } 

   	public class PetControlDeed : Item
   	{ 
      		[Constructable] 
      		public PetControlDeed() : base( 0x14F0 ) 
      		{ 
			Weight = 1.0; 
			Name = "a pet control deed";
			Hue = 598;
      		} 

      	public PetControlDeed( Serial serial ) : base( serial ) 
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
            		from.SendMessage( "Choose the pet you wish to control." );  
            		from.Target = new PetControlTarget( this ); 
          	} 
      	}    
} 
}