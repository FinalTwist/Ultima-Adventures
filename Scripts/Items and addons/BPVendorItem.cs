using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class BPVendorItem : Item // BPVendorItem needs to be changed to "BPVendorXXXYYY" where XXXYYY is the name/type of the item
	{
    		private int cost;
		private Type itm; 
		private int amnt;
    
		public override string DefaultName
		{
			get { return "Bandage"; } //what is displayed on ctrl-shift
		}

		[Constructable]
		public BPVendorItem() : base( 0x14F0 ) //change BPVENDORITEM and 0x14f0 is the itemid that the item will display when placed.
		{
			base.Weight = 1.0;
      			Movable = false;
      			cost = 0; //set cost to whatever you want here (in BP)
			itm = typeof(Bandage);// Change this bandage to describe the item this is the ITEM NAME (e.g. ctrl-shift name so should be short)
			amnt = 1; // enter the amount to be sold per purchase here 1, 10, whatever
		}
		
		public override void AddNameProperties( ObjectPropertyList list ) // the ""text below are for the hover over.  you have have as many lines as you want, just copy one more line 
		{
			base.AddNameProperties( list );

			list.Add( "A new set of whatever"); 
			list.Add( "Cost: " + cost + " balance influence.");
      //feel free to add more

		}

		public BPVendorItem( Serial serial ) : base( serial ) //change BPVENDORITEM here
		{
		}

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

		public override void OnDoubleClick( Mobile from )
		{
			if (from is PlayerMobile)
			{
				if ( ((PlayerMobile)from).Avatar )
				{
          if ( Math.Abs(((PlayerMobile)from).BalanceEffect) > cost)
          {
            from.SendMessage("You buy the item.");
            if ( ((PlayerMobile)from).BalanceEffect > 0 )
              ((PlayerMobile)from).BalanceEffect -= cost;
            else if ( ((PlayerMobile)from).BalanceEffect < 0)
              ((PlayerMobile)from).BalanceEffect += cost;
	     int howmany = amnt;
	     while (howmany >0)
	     {
	     ((PlayerMobile)from).Backpack.DropItem((Item)Activator.CreateInstance(itm));
	     howmany --;
	     }
            
	 }
           else
               from.SendMessage("You Lack sufficient influence with the balance.");
				}
				else 
				{
					from.SendMessage("Only Avatars of the Balance can use this item.");
				}
					
			}
				
		}
	}
}
