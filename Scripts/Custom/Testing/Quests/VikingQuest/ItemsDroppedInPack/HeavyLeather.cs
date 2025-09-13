using System;
using Server;
using Server.Items;
namespace Server.Items
{
        
	public class HeavyLeather :  Item
	{


		
		
                

                                [Constructable]
		public HeavyLeather (): base( 0x1067 )
		{
			Weight = 1.0; 
            		Name = "Heavy Leather"; 
            		Hue = 1810;
                        ItemID = 4199;                                
                        
			}  


		public HeavyLeather( Serial serial ) : base( serial )
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