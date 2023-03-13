/*
GD13 Sickness System
Script By : GD13
Script Version : v2.0.0
Script Updated : 07/08/06
*/
using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items 
{ 
  	public class ItemRemovalTimer : Timer 
   	{ 
      		private Item i_item; 

      		public ItemRemovalTimer( Item item ) : base( TimeSpan.FromSeconds( 2.0 ) ) 
      		{ 
         		Priority = TimerPriority.OneSecond; 
         		i_item = item; 
      		} 

      		protected override void OnTick() 
      		{ 
         		if (( i_item != null ) && ( !i_item.Deleted )) 
            		i_item.Delete(); 
      		} 
   	} 

   	public class BloodyPuke : Item 
   	{ 	
      		[Constructable] 
      		public BloodyPuke() : base( Utility.RandomList( 0xf3b, 0xf3c ) ) 
      		{ 
         		Name = "A Pile of Bloody Puke"; 
         		Hue = 2118; 
         		Movable = false; 

        	 	ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
         		thisTimer.Start(); 
		} 

      		public override void OnSingleClick( Mobile from ) 
      		{ 
         		this.LabelTo( from, this.Name ); 
      		} 
  
      		public BloodyPuke( Serial serial ) : base( serial ) 
      		{ 
      		} 

      		public override void Serialize(GenericWriter writer) 
      		{ 
         		base.Serialize( writer ); 
		        writer.Write( (int) 0 ); 
      		} 

      		public override void Deserialize(GenericReader reader) 
      		{ 
         		base.Deserialize( reader ); 
         		int version = reader.ReadInt(); 

         		this.Delete(); // none when the world starts 
      		} 
   	} 
}

