using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.BulkOrders
{
	public class SmallBODGump : Gump
	{
		private SmallBOD m_Deed;
		private Mobile m_From;

		public SmallBODGump( Mobile from, SmallBOD deed ) : base( 25, 25 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeBODGump ) );
			m_From.CloseGump( typeof( SmallBODGump ) );

			AddPage( 0 );

			AddBackground( 50, 10, 455, 260, 5054 );
			AddImageTiled( 58, 20, 438, 241, 2624 );
			AddAlphaRegion( 58, 20, 438, 241 );

			AddImage( 45, 5, 10460 );
			AddImage( 480, 5, 10460 );
			AddImage( 45, 245, 10460 );
			AddImage( 480, 245, 10460 );

			AddHtmlLocalized( 225, 25, 120, 20, 1045133, 0x7FFF, false, false ); // A bulk order

			AddHtmlLocalized( 75, 48, 250, 20, 1045138, 0x7FFF, false, false ); // Amount to make:
			AddLabel( 275, 48, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 275, 76, 200, 20, 1045153, 0x7FFF, false, false ); // Amount finished:
			AddHtmlLocalized( 75, 72, 120, 20, 1045136, 0x7FFF, false, false ); // Item requested:

			AddItem( 410, 72, deed.Graphic );

			AddHtmlLocalized( 75, 96, 210, 20, deed.Number, 0x7FFF, false, false );
			AddLabel( 275, 96, 0x480, deed.AmountCur.ToString() );

			if ( deed.RequireExceptional || deed.Material != BulkMaterialType.None )
				AddHtmlLocalized( 75, 120, 200, 20, 1045140, 0x7FFF, false, false ); // Special requirements to meet:

			if ( deed.RequireExceptional )
				AddHtmlLocalized( 75, 144, 300, 20, 1045141, 0x7FFF, false, false ); // All items must be exceptional.

			if ( deed.Material != BulkMaterialType.None )
                AddHtml( 75, deed.RequireExceptional ? 168 : 144, 300, 20, "<basefont color=#FF0000>All items must be crafted with " + SmallBODGump.GetMaterialStringFor(deed.Material), false, false );

			AddButton( 125, 192, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 192, 300, 20, 1045154, 0x7FFF, false, false ); // Combine this deed with the item requested.

			AddButton( 125, 216, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 216, 120, 20, 1011441, 0x7FFF, false, false ); // EXIT
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Deed.Deleted || !m_Deed.IsChildOf( m_From.Backpack ) )
				return;

			if ( info.ButtonID == 2 ) // Combine
			{
				m_From.SendGump( new SmallBODGump( m_From, m_Deed ) );
				m_Deed.BeginCombine( m_From );
			}
		}

        public static string GetMaterialStringFor(BulkMaterialType material)
        {
            string result = "UNKNOWN";
            switch (material)
            {
                case BulkMaterialType.DullCopper: result = "dull copper ingots"; break;
                case BulkMaterialType.ShadowIron: result = "shadow iron ingots"; break;
                case BulkMaterialType.Copper: result = "copper ingots"; break;
                case BulkMaterialType.Bronze: result = "bronze ingots"; break;
                case BulkMaterialType.Gold: result = "gold ingots"; break;
                case BulkMaterialType.Agapite: result = "agapite ingots"; break;
                case BulkMaterialType.Verite: result = "verite ingots"; break;
                case BulkMaterialType.Valorite: result = "valorite ingots"; break;
				case BulkMaterialType.Nepturite: result = "nepturite ingots"; break;
				case BulkMaterialType.Obsidian: result = "obsidian ingots"; break;
				case BulkMaterialType.Steel: result = "steel ingots"; break;
				case BulkMaterialType.Brass: result = "brass ingots"; break;
				case BulkMaterialType.Mithril: result = "mithril ingots"; break;
				case BulkMaterialType.Xormite: result = "xormite ingots"; break;
				case BulkMaterialType.Dwarven: result = "dwarven ingots"; break;

				case BulkMaterialType.Horned: result = "lizard leather"; break;
				case BulkMaterialType.Barbed: result = "serpent leather"; break;
				case BulkMaterialType.Necrotic: result = "necrotic leather"; break;
				case BulkMaterialType.Volcanic: result = "volcanic leather"; break;
				case BulkMaterialType.Frozen: result = "frozen leather"; break;
				case BulkMaterialType.Spined: result = "deep sea leather"; break;
				case BulkMaterialType.Goliath: result = "goliath leather"; break;
				case BulkMaterialType.Draconic: result = "draconic leather"; break;
				case BulkMaterialType.Hellish: result = "hellish leather"; break;
				case BulkMaterialType.Dinosaur: result = "dinosaur leather"; break;
				case BulkMaterialType.Alien: result = "alien leather"; break;

				case BulkMaterialType.Ash: result = "ash wood"; break;
				case BulkMaterialType.Cherry: result = "cherry wood"; break;
				case BulkMaterialType.Ebony: result = "ebony wood"; break;
				case BulkMaterialType.GoldenOak: result = "golden oak wood"; break;
				case BulkMaterialType.Hickory: result = "hickory wood"; break;
				case BulkMaterialType.Mahogany: result = "mahogany wood"; break;
				case BulkMaterialType.Oak: result = "oak wood"; break;
				case BulkMaterialType.Pine: result = "pine wood"; break;
				case BulkMaterialType.Ghost: result = "ghost wood"; break;
				case BulkMaterialType.Rosewood: result = "rosewood"; break;
				case BulkMaterialType.Walnut: result = "walnut wood"; break;
				case BulkMaterialType.Petrified: result = "petrified wood"; break;
				case BulkMaterialType.Driftwood: result = "driftwood"; break;
				case BulkMaterialType.Elven: result = "elven wood"; break;
            }
			
            return result;
        }
	}
}