using System;
using Server;
using Server.Items;
namespace Server.Items
{
        
	public class StrongLeatherLace :  Item
	{


		
		
                

                                [Constructable]
		public StrongLeatherLace (): base( 0x1535 )
		{
			Weight = 1.0; 
            		Name = "Strong Leather Lace"; 
            		Hue = 1842;
                        ItemID = 5429;                                
                        
			}  


		public StrongLeatherLace ( Serial serial ) : base( serial )
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