using System;
using Server;
using Server.Items;
namespace Server.Items
{
        
	public class PirateAle :  Item
	{


		
		
                

                                [Constructable]
		public PirateAle(): base( 0x99F )
		{
			Weight = 1.0; 
            		Name = "Pirate Ale"; 
            		
                        ItemID = 2463;                                
                        
			}  


		public PirateAle( Serial serial ) : base( serial )
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