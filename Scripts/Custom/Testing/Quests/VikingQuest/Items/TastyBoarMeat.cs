using System;
using Server;
using Server.Items;
namespace Server.Items
{
        
	public class TastyBoarMeat :  Item
	{


		
		
                

         [Constructable]
		public TastyBoarMeat(): base( 0xB7 )
		{
			Weight = 1.0; 
            		Name = "Tasty Boar Meat"; 
            		
                        ItemID = 2487;                                
                        
			}


                                public TastyBoarMeat(Serial serial)
                                    : base(serial)
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