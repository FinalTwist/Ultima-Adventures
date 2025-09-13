/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/30/2005
 * Time: 5:32 PM
 * 
 * Santas Coal
 */
using System;
using Server;

namespace Server.Items
{
	public class SantasCoal : Item
	{
		[Constructable]
		public SantasCoal() : this( 1 )
		{
		}

		[Constructable]
		public SantasCoal( int amount ) : base( 0x1366 )
		{
			Name = "A Lump of Magical Coal";
			LootType = LootType.Blessed;
			Hue = 962;
			Stackable = false;
			Weight = 5.0;
			Amount = amount;
		}
		 public override void GetProperties( ObjectPropertyList list )
	         {
	  	    base.GetProperties( list );

		    list.Add( 1041052 ); 
    	     }
		
        public override void OnDoubleClick( Mobile from )
        {

        	if ( !IsChildOf (from.Backpack))
        		{
				from.SendMessage( "In order to make a wish and crush the coal with your hands, it must be in your backpack." );
        	    }
        	else
        	{
        	
        	Effects.PlaySound( from, from.Map, 0x2E3 );
        	if( Utility.Random( 100 ) < 100 ) 
			       
         			switch ( Utility.Random( 3 ) )
			{ 

				case 0: from.AddToBackpack( new Diamond(Utility.RandomList(   6 ,  7 , 8 , 8 , 9 , 10 ) ) );
				{
				from.SendMessage( "You use all your brute strength - and turn the Magical Coal into Diamonds." );
				}
					break;
				case 1: from.AddToBackpack( new SantasElvenRobe() ); 
				{
				from.SendMessage( "You promise to be good next year - and the Magical Coal hears your words and gives you a gift." );
				}
				    break;
				case 2: from.AddToBackpack( new SantasFancyElvenRobe() ); 
				{
				from.SendMessage( "You promise to be EXTRA good next year - and the Magical Coal hears your words and gives you a gift." );
				}
					break;	
			}			
				this.Delete();
        	}
		}

		public SantasCoal( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new SantasCoal( amount ), amount );
		//}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
