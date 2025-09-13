using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;


namespace Server.Items
{

	public class VikingRaiderBasket : Item
	{
		[Constructable]
		public VikingRaiderBasket() : this( null )
		{
		}

		[Constructable]
		public VikingRaiderBasket ( string name ) : base ( 0x24D5 )
		{
			Name = "Viking Raider Basket";
                  		LootType = LootType.Blessed;
			Hue = 0;
		}

		public VikingRaiderBasket ( Serial serial ) : base ( serial )
		{
		}

      		
		public override void OnDoubleClick( Mobile m )

		{
			Item a = m.Backpack.FindItemByType( typeof(StrongLeatherLace) );
			if ( a != null )
			{	
			Item b = m.Backpack.FindItemByType( typeof(WarmWool) );
			if ( b != null )
			{
			Item c = m.Backpack.FindItemByType( typeof(HeavyLeather) );
			if ( c != null )
			{
			
				m.AddToBackpack( new WarmPirateJacket() );
				a.Delete();
				b.Delete();
				c.Delete();
				m.SendMessage( "You have created a wonderful Warm Jacket" );
				this.Delete();
			}
			}
				else
			{
				m.SendMessage( "You are missing something..." );
		}
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