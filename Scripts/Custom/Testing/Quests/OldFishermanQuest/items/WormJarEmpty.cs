using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{

	public class WormJarEmpty : Item
	{
		[Constructable]
		public WormJarEmpty() : this( null )
		{
		}

		[Constructable]
		public WormJarEmpty ( string name ) : base ( 0x1005 )
		{
			Name = "Bait Jar (It's Empty)";
			Hue = 1113;
		}

		public WormJarEmpty ( Serial serial ) : base ( serial )
		{
		}

      		
	public override void OnDoubleClick( Mobile m )

		{	
			Item [] a = m.Backpack.FindItemsByType( typeof( Worm ) );
			// are there at least 10 elements in the returned array?
			if ( a!= null && a.Length >= 10)
			{
				
			Item b = m.Backpack.FindItemByType( typeof( WormJarEmpty ) );
			if ( b != null )
			{	
			Item c = m.Backpack.FindItemByType( typeof(FertileDirt) );
			if ( c != null )
			{
				// delete the first 10 of them
				for(int i=0;i<10;i++) a[i].Delete();
				b.Delete();
				c.Delete();
				// and add the full jar of worms
				m.AddToBackpack( new WormJarFull() );
				m.SendMessage("The jar is full, hurry back to James for your reward!");
			}
			}						
			}
			else
			{
				m.SendMessage( "James wants this jar full of worms and fertile dirt. There is still room for more..." ); 
			}
		}
		
		
		
		public override void Serialize ( GenericWriter writer)
		{
			base.Serialize ( writer );

			writer.Write ( (int) 0);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize ( reader );

			int version = reader.ReadInt();
		}
	}
}
