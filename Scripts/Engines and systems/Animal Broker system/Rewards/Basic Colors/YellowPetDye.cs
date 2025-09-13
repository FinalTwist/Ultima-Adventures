using Server.Targeting; 
using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 
using Server.Mobiles; 
using System.Collections; 

namespace Server.Items 
{ 
   	public class YellowPetDye : Item 
   	{ 
    
      		[Constructable] 
      		public YellowPetDye() : base( 0xE2B ) 
      		{ 
         		Weight = 1.0;  
         		Movable = true;
			Hue = 53;
         		Name="pet dye (Yellow)"; 
          	} 

      		public YellowPetDye( Serial serial ) : base( serial ) 
      		{ 
          
      
      		} 
      		public override void OnDoubleClick( Mobile from ) 
     	 	{ 

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if( from.InRange( this.GetWorldLocation(), 1 ) ) 
		        {
				from.SendMessage( "What do you wish to dye?" ); 
           			from.Target = new YellowDyeTarget( this );
		        } 
		        else 
		        { 
		            from.SendLocalizedMessage( 500446 ); // That is too far away. 
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


  		private class YellowDyeTarget : Target 
      		{ 
         		private Mobile m_Owner; 
      
         		private YellowPetDye m_Powder; 

         		public YellowDyeTarget( YellowPetDye charge ) : base ( 10, false, TargetFlags.None ) 
         		{ 
            			m_Powder=charge; 
         		} 
          
         		protected override void OnTarget( Mobile from, object target ) 
         		{ 

            			if ( target == from ) 
               				from.SendMessage( "This can only be used on pets." );

				else if ( target is PlayerMobile )
					from.SendMessage( "You cannot dye them." );

				else if ( target is Item )
					from.SendMessage( "You cannot dye that." );

          			else if ( target is BaseCreature ) 
          			{ 
          				BaseCreature c = (BaseCreature)target;	
					if ( c.BodyValue == 400 || c.BodyValue == 401 && c.Controlled == false )
					{
						from.SendMessage( "You cannot dye them." );
					}
					else if ( c.ControlMaster != from && c.Controlled == false )
					{
						from.SendMessage( "This is not your pet." );
					}
					else if ( c.Controlled == true && c.ControlMaster == from)
					{
						c.Hue = 53;
						from.SendMessage( 53, "Your pet has now been dyed." );
						from.PlaySound( 0x23E );
						m_Powder.Delete();
					}
  
            			}
         		} 
      		} 
   	} 
} 
