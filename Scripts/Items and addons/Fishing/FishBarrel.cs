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
	public class FishBarrel : Item
	{
		[Constructable]
		public FishBarrel() : base(0x4CCF)
		{
			Weight = 100.0;
			Name = "Exotic Fish Barrel";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( !( from is PlayerMobile ) )
			return;
			
			if ( !from.HasGump( typeof( SpeechGump ) ) )
			{
				from.SendGump(new SpeechGump( "Fish In A Barrel", SpeechFunctions.SpeechText( from.Name, from.Name, "Aquarium" ) ));
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is NewFish )
			{
				PlayerMobile pc = (PlayerMobile)from;
				NewFish fishy = (NewFish)dropped;
				int nPay = fishy.FishGoldValue;
				if ( pc.NpcGuild == NpcGuild.FishermensGuild ){ nPay = nPay*2; }
				from.AddToBackpack ( new Gold( nPay ) );
				from.SendMessage("You are paid " + nPay.ToString() + " gold.");
				from.PlaySound( 0x026 );
				dropped.Delete();
			}
			else
			{
				from.AddToBackpack ( dropped );
			}
			return true;
		}

		public FishBarrel(Serial serial) : base(serial)
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