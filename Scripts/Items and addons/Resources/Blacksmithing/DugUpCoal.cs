using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;

namespace Server.Items
{
	public class DugUpCoal : Item
	{
		[Constructable]
		public DugUpCoal() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 2.0; }
		}

		[Constructable]
		public DugUpCoal( int amount ) : base( 0x19B9 )
		{
			Name = "coal";
			Stackable = true;
			Hue = 0x497;
			Amount = amount;
		}

		public DugUpCoal( Serial serial ) : base( serial )
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

		public static bool CheckForDugUpCoal( Mobile from, int qty, bool remove )
		{
			bool pass = false;
			int carry = 0;

			if ( qty > 0 )
			{
				List<Item> belongings = new List<Item>();
				foreach( Item i in from.Backpack.Items )
				{
					if ( i is IronOre ){ carry = carry + i.Amount; }
				}

				if ( carry >= qty )
				{
					pass = true;
					Container pack = from.Backpack;
					if ( remove == true ){ pack.ConsumeTotal(typeof(IronOre), qty) ; }
				}
			}

			return pass;
		}
	}
}