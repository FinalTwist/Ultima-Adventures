using System;
using Server;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class DragonRider : Citizens
	{
		public override bool PlayerRangeSensitive { get { return false; } }

		[Constructable]
		public DragonRider()
		{
			if ( Backpack != null ){ Backpack.Delete(); }
			CitizenRumor = "";
			CitizenType = 0;
			CitizenCost = 0;
			CitizenService = 0;
			CitizenPhrase = "";
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
		}

		public DragonRider( Serial serial ) : base( serial )
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

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			return false;
		}
	}
}