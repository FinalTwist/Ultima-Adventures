using System;
using Server;
using Server.Items;
namespace Server.Items
{
        
	public class Steel :  Item
	{


		
		
                

                                [Constructable]
		public Steel(): base( 0x1B56 )
		{
			Weight = 1.0; 
            		Name = "Steel"; 
            		Hue = 0;
                                ItemID = 7158;                                
                        
			}  


		public  Steel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}