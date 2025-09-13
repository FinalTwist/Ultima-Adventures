using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;


namespace Server.Items
{

	public class SeaChest : Item
	{
		[Constructable]
		public SeaChest() : this( null )
		{
		}

		[Constructable]
		public SeaChest ( string name ) : base ( 0xE41 )
		{
			Name = "A Sea Chest";
			LootType = LootType.Blessed;
			Hue = 288;
		}

		public SeaChest ( Serial serial ) : base ( serial )
		{
		}

      		
		public override void OnDoubleClick( Mobile m )

		{
			Item a = m.Backpack.FindItemByType( typeof(EnchantedRope) );
			if ( a != null )
			{	
			Item b = m.Backpack.FindItemByType( typeof(GlowingShipModel) );
			if ( b != null )
			{
			Item c = m.Backpack.FindItemByType( typeof(SacredAnchor) );
			if ( c != null )
			{
			Item d = m.Backpack.FindItemByType( typeof(SpecialSeaMap) );
			if ( d != null )
			{						
				m.AddToBackpack( new MasterOfTheSeaChest() );
				a.Delete();
				b.Delete();
				c.Delete();
				d.Delete();				
				m.SendMessage( "You place the artifacts into the Sea Chest for a Full Master of the Sea Chest!" );
				this.Delete();
			}
			}
			}
			}			
				else
			{
				m.SendMessage( "You are missing some artifacts." );
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