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
   	public class RepairKitTarget : Target 
   	{ 
      	private HarvesterRepairKit m_Deed; 

      	public RepairKitTarget( HarvesterRepairKit deed ) : base( 1, false, TargetFlags.None ) 
      	{ 
         	m_Deed = deed; 
      	} 

      	protected override void OnTarget( Mobile from, object target ) 
      	{ 
			if (target == null)
				return;
			
         	if ( target is BaseHarvester ) 
			{ 
				BaseHarvester k = ( BaseHarvester ) target; 

				if ( !k.disabled )
					{ 
							from.SendMessage( "You've turned off the device, double click the device to make it movable again." );
							k.disabled = true;
							return;

					} 
				if ( k.owner != from )
					{ 
							from.SendMessage( "That device can only be fixed by its builder." );
					} 
					
				else  
				 
					{ 
						k.disabled = false;
						m_Deed.Delete();
						return;
					}

					

			} 
         else 
         { 
            from.SendMessage( "That's not the type of device this can fix." );  
         } 
      } 
   } 

   	public class HarvesterRepairKit : Item
   	{ 
      		[Constructable] 
      		public HarvesterRepairKit() : base( 0x4C2C ) 
      		{ 
			Weight = 15.0; 
			Name = "a harvester repair kit";
			Movable = true;
			//Hue = 598;
      		} 

      	public HarvesterRepairKit( Serial serial ) : base( serial ) 
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
            		from.SendMessage( "Choose the device you wish to repair." );  
            		from.Target = new RepairKitTarget( this ); 
          	} 
      	}    
} 
}