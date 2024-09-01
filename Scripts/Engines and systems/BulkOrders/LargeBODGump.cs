using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.BulkOrders
{
	public class LargeBODGump : Gump
	{
		private LargeBOD m_Deed;
		private Mobile m_From;

		public LargeBODGump( Mobile from, LargeBOD deed ) : base( 25, 25 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeBODGump ) );
			m_From.CloseGump( typeof( SmallBODGump ) );

			LargeBulkEntry[] entries = deed.Entries;

			AddPage( 0 );

			AddBackground( 50, 10, 455, 236 + (entries.Length * 24), 5054 );

			AddImageTiled( 58, 20, 438, 217 + (entries.Length * 24), 2624 );
			AddAlphaRegion( 58, 20, 438, 217 + (entries.Length * 24) );

			AddImage( 45, 5, 10460 );
			AddImage( 480, 5, 10460 );
			AddImage( 45, 221 + (entries.Length * 24), 10460 );
			AddImage( 480, 221 + (entries.Length * 24), 10460 );

			AddHtmlLocalized( 225, 25, 120, 20, 1045134, 0x7FFF, false, false ); // A large bulk order

			AddHtmlLocalized( 75, 48, 250, 20, 1045138, 0x7FFF, false, false ); // Amount to make:
			AddLabel( 275, 48, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 75, 72, 120, 20, 1045137, 0x7FFF, false, false ); // Items requested:
			AddHtmlLocalized( 275, 76, 200, 20, 1045153, 0x7FFF, false, false ); // Amount finished:

			int y = 96;

			for ( int i = 0; i < entries.Length; ++i )
			{
				LargeBulkEntry entry = entries[i];
				SmallBulkEntry details = entry.Details;

				AddHtmlLocalized( 75, y, 210, 20, details.Number, 0x7FFF, false, false );
				AddLabel( 275, y, 0x480, entry.Amount.ToString() );

				y += 24;
			}

			if ( deed.RequireExceptional || deed.Material != BulkMaterialType.None )
			{
				AddHtmlLocalized( 75, y, 200, 20, 1045140, 0x7FFF, false, false ); // Special requirements to meet:
				y += 24;
			}

			if ( deed.RequireExceptional )
			{
				AddHtmlLocalized( 75, y, 300, 20, 1045141, 0x7FFF, false, false ); // All items must be exceptional.
				y += 24;
			}

			if ( deed.Material != BulkMaterialType.None )
                AddHtml( 75, y, 300, 20, "<basefont color=#FF0000>All items must be crafted with " + SmallBODGump.GetMaterialStringFor(deed.Material), false, false );

			AddButton( 125, 168 + (entries.Length * 24), 4005, 4007, 2, GumpButtonType.Reply, 0 );
            AddLabel( 160, 168 + (entries.Length * 24), 1152, "Combine this deed with each item requested." );

            AddButton( 125, 192 + (entries.Length * 24), 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 192 + (entries.Length * 24), 120, 20, 1011441, 0x7FFF, false, false ); // EXIT
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Deed.Deleted || !m_Deed.IsChildOf( m_From.Backpack ) )
				return;

			if ( info.ButtonID == 2 ) // Combine
			{
				m_From.SendGump( new LargeBODGump( m_From, m_Deed ) );
				m_Deed.BeginCombine( m_From );
			}
		}
	}
}