using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x232A, 0x232B )]
	public class SantasGiftBox2010 : GiftBox
	{
		public int offset;
		
		[Constructable]
		public SantasGiftBox2010()
		{
			Name = "A Gift from Santa - 2010";
			offset = Utility.Random( 0, 10 );
			
             switch ( Utility.Random( 9 ) )
             {
             	case 0:
             		DropItem( new  HolidayBell2010() );break;
             	case 1:
             		DropItem( new  SantasChairAddonDeed()  );break;
             	case 2:
             		DropItem( new ChristmasCandle2010() );break;
             	case 3:
             		DropItem( new SantasCoal() ); break;
             	case 4:
             		DropItem( new SantasElfBoots() ); break;
             	//case 5:
             		//DropItem( new SantasMistletoe2010Deed() ); //break;
             	case 6:
             		DropItem( new Snowpile2020() ); break;
             	case 7:
             		DropItem( new SantasTimepiece() ); break;
		case 8:
             		DropItem( new RadioactiveXMasTreeAddonDeed() ); break;

			}

		}

		public SantasGiftBox2010( Serial serial ) : base( serial )
		{
		}
		
		public override void GetProperties( ObjectPropertyList list )
	         {
	  	    base.GetProperties( list );

		    list.Add( 1007149 + offset ); 
    	     }

		public override void Serialize( GenericWriter writer ) 
	         {
	            base.Serialize( writer ); 

	            writer.Write( (int) 0 ); 
	            
	            writer.Write( (int) offset );
	         }
	
	         public override void Deserialize( GenericReader reader ) 
	         {
	            base.Deserialize( reader ); 

	            int version = reader.ReadInt(); 

		        offset = reader.ReadInt();
	         }
	}
}
