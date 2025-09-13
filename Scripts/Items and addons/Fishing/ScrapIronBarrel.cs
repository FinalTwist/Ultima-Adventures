using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	public class ScrapIronBarrel : Item
	{
		[Constructable]
		public ScrapIronBarrel() : base( 0x4D05 )
		{
			ItemID = Utility.RandomList( 0x4D05, 0x4D06 );
			Weight = 10.0;
			Name = "Scrap Iron Barrel";
			Hue = 0xACF;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is RustyJunk )
			{
				PlayerMobile pc = (PlayerMobile)from;
				int nPay = (int)(dropped.Weight*5);
				if ( pc.NpcGuild == NpcGuild.BlacksmithsGuild ){ nPay = (int)(dropped.Weight*10); }
				from.AddToBackpack ( new Gold( nPay ) );
				from.SendMessage("You are paid " + nPay.ToString() + " gold.");
				from.PlaySound( 0x042 );
				dropped.Delete();
			}
			else
			{
				from.AddToBackpack ( dropped );
			}
			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( !( from is PlayerMobile ) )
			return;

			if ( !from.HasGump( typeof( SpeechGump ) ) )
			{
				from.SendGump(new SpeechGump( "Rusty Gold", SpeechFunctions.SpeechText( from.Name, from.Name, "ScrapMetal" ) ));
			}
		}

		public ScrapIronBarrel(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}