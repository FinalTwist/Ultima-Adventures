using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Network;
using Server.Misc;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public interface IGiftable
	{
		string Gifter{ get; set; }
		string How{ get; set; }
		Mobile Owner{ get; set; }
		int Points{ get; set; }
	}
}

namespace Server.ContextMenus
{
	public class GiftInfoEntry : ContextMenuEntry
	{
		private Item m_Item;
		private Mobile m_From;
		private GiftAttributeCategory m_Cat;

		public GiftInfoEntry( Mobile from, Item item, GiftAttributeCategory cat ) : base( 132, 3 )
		{
			m_From = from;
			m_Item = item;
			m_Cat = cat;
		}

		public override void OnClick()
		{
			Owner.From.CloseGump( typeof( GiftGump ) );
			Owner.From.SendGump( new GiftGump( m_From, m_Item, m_Cat ) ); 
		}
	}
}