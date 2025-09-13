using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;


namespace Server.Items
{

	public class SeafarerToolKit : Item
	{
		[Constructable]
		public SeafarerToolKit() : this( null )
		{
		}

		[Constructable]
		public SeafarerToolKit ( string name ) : base ( 0x1EBA )
		{
			Name = "Empty Seafarer's Tool Kit";
			LootType = LootType.Blessed;
			Hue = 456;
		}

		public SeafarerToolKit ( Serial serial ) : base ( serial )
		{
		}

      		
		public override void OnDoubleClick( Mobile m )

		{
			Item a = m.Backpack.FindItemByType( typeof(ReinforcedHinge) );
			if ( a != null )
			{	
			Item b = m.Backpack.FindItemByType( typeof(SturdyAxle) );
			if ( b != null )
			{
			Item c = m.Backpack.FindItemByType( typeof(SteelGears) );
			if ( c != null )
			{
			Item d = m.Backpack.FindItemByType( typeof(GemOfControl) );
			if ( d != null )
			{						
				m.AddToBackpack( new CompletedSeafarerToolKit() );
				a.Delete();
				b.Delete();
				c.Delete();
				d.Delete();				
				m.SendMessage( "You place the items into the tool kit for a Completed Seafarer's Tool Kit!" );
				this.Delete();
			}
			}
			}
			}			
				else
			{
				m.SendMessage( "You are missing some parts." );
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