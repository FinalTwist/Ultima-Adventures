// created by BondD
using System;
using Server; 
using Server.Misc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Net;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Guilds;
using Server.Items;
using Server.Gumps;
using Server.Commands;

namespace Server.Items
{
	[Flipable(0x1E5E, 0x1E5F)]
	public class StatusBoard : Item
	{
		[Constructable]
		public StatusBoard( ) : base( 0x1E5E )
		{
			Weight = 1.0;
			Name = "Status Board";
			Hue = 0xB98;
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( StatusGump ) );
				e.SendGump( new StatusGump( e, 0, null, null ) );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public StatusBoard(Serial serial) : base(serial)
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

namespace Server.Gumps
{
   public class StatusGump : Gump
   {
      public void AddBlackAlpha( int x, int y, int width, int height )
      {
         AddImageTiled( x, y, width, height, 2624 );
         AddAlphaRegion( x, y, width, height );
      }

      public static string FormatTimeSpan( TimeSpan ts )
      {
         return String.Format( "{0:D2}:{1:D2}:{2:D2}:{3:D2}", ts.Days, ts.Hours % 24, ts.Minutes % 60, ts.Seconds % 60 );
      }

      public static string FormatByteAmount( long totalBytes )
      {
         if ( totalBytes > 1000000000 )
            return String.Format( "{0:F1} GB", (double)totalBytes / 1073741824 );

         if ( totalBytes > 1000000 )
            return String.Format( "{0:F1} MB", (double)totalBytes / 1048576 );

         if ( totalBytes > 1000 )
            return String.Format( "{0:F1} KB", (double)totalBytes / 1024 );

         return String.Format( "{0} Bytes", totalBytes );
      }

      private ArrayList m_List;
      private int m_ListPage;
      private ArrayList m_CountList;

      public StatusGump( Mobile from, int listPage, ArrayList list, ArrayList count ) : base( 140, 80 )
      {
         from.CloseGump( typeof( StatusGump ) );

         m_List = list;
         m_ListPage = listPage;
         m_CountList = count;
         AddPage( 0 );

         AddBackground( 0, 0, 800, 600, 0x53 );

         AddImageTiled( 15, 15, 770, 17, 5154 );
         AddHtml( 15, 15, 770, 17, "<div align=\"center\" color=\"2100\">"+ ServerList.ServerName +"</div>", false, false );

		int t = NetState.Instances.Count;
		if (t != 0)
		{
			ArrayList KList = new ArrayList( NetState.Instances );
			for( int s = 0; s < t; ++s)
			{
				NetState nsk = KList[s] as NetState;
				if ( nsk == null )
					continue;
				Mobile mk = nsk.Mobile;
				if ( mk == null )
				{
					continue;
				}
				else
				{
					if ((from.AccessLevel == AccessLevel.Player && (( mk.AccessLevel == AccessLevel.Counselor && mk.Hidden ) || mk.AccessLevel >= AccessLevel.GameMaster )) || (from.AccessLevel >= AccessLevel.Player && mk.AccessLevel > from.AccessLevel))
					{
						--t;
					}
				}
			}
		}

		AddImageTiled( 15, 37, 190, 17, 5154 );
		AddLabel( 17, 36, 0x25, "Online :" );
		AddHtml( 160, 37, 30, 17, "<div align=\"right\" color=\"2100\">"+ t.ToString() +"</div>", false, false );

		AddImageTiled( 210, 37, 190, 17, 5154 );
		AddLabel( 212, 36, 0x68, "Accounts :" );
		AddHtml( 357, 37, 30, 17, "<div align=\"right\" color=\"2100\">"+ Accounts.Count.ToString() +"</div>", false, false );

		AddImageTiled( 405, 37, 190, 17, 5154 );
		AddLabel( 407, 36, 2100, "Uptime :" );
		AddHtml( 485, 37, 109, 17, "<div align=\"right\" color=\"2100\">"+ FormatTimeSpan( DateTime.UtcNow - Clock.ServerStart) +"</div>", false, false );

		AddImageTiled( 600, 37, 185, 17, 5154 );
		AddLabel( 602, 36, 2100, "RAM in use :" );
		AddHtml( 700, 37, 75, 17, "<div align=\"right\" color=\"2100\">"+ FormatByteAmount( GC.GetTotalMemory( false ) ) +"</div>", false, false );
		// A3C BB8 DAC E10 13BE 13EC 1400 1432 23F0 238C 23BE 2422 242C 2436 2454 2486 24A4 24AE 24B8 24EA 251C 254E 2557 2560 ?2776?
		AddBackground( 15, 59, 770, 526, 0x2454);
		AddBlackAlpha( 18, 62, 763, 520);
		AddLabelCropped(  20, 60, 220, 20, 2100, "Name" );
		AddLabelCropped( 222, 60, 209, 20, 2100, "Guild" );
		AddLabelCropped( 453, 60, 60, 20, 2100, "Stats" );
		AddLabelCropped( 515, 60, 60, 20, 2100, "Skills" );
		AddLabelCropped( 577, 60, 60, 20, 2100, "Karma" );
		AddLabelCropped( 639, 60, 60, 20, 2100, "Fame" );
		AddLabelCropped( 701, 60, 60, 20, 2100, "Kills" );

         if ( m_List == null )
            m_List = new ArrayList( NetState.Instances );

         if ( m_CountList == null )
            m_CountList = new ArrayList();

         if ( listPage > 0 )
            AddButton( 744, 62, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );
         else
            AddImage( 744, 62, 0x25EA );

         if ( (listPage + 1) * 25 < m_List.Count )
            AddButton( 761, 62, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 );
         else
            AddImage( 761, 62, 0x25E6 );

         if ( m_List.Count == 0 )
            AddLabel( 20, 80, 0x25, "There are no clients to display." );

         int k = 0;

         if ( listPage > 0 )
         {
            for ( int z = 0; z < ( listPage - 1 ); ++z )
            {
               k = k + Convert.ToInt32(m_CountList[z]);
            }
         }

         for ( int i = 0, j = 0, index=((listPage*25)+k) ; i < 25 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index )
         {
            NetState ns = m_List[index] as NetState;

            if ( ns == null )
               continue;

            Mobile m = ns.Mobile;

            int offset = 80 + (i * 20);

            if ( m == null )
            {
               if ( RemoteAdmin.AdminNetwork.IsAuth( ns ) )
                  AddLabelCropped( 20, offset, 220, 20, 2100, "(remote admin)" );
               else
                  AddLabelCropped( 20, offset, 220, 20, 2100, "(logging in)" );
            }
            else
            {
               if ((from.AccessLevel == AccessLevel.Player && (( m.AccessLevel == AccessLevel.Counselor && m.Hidden ) || m.AccessLevel >= AccessLevel.GameMaster )) || (from.AccessLevel >= AccessLevel.Player && m.AccessLevel > from.AccessLevel))
               {
                  --i;
               }
                else
                {
                  AddLabelCropped(  20, offset, 220, 20, 2100, m.Name );

				  string title = "";
                  Guild g = m.Guild as Guild;

                  if ( g != null )
                  {
					title = m.GuildTitle;
					if ( title != null )
					{
						title = title.Trim();
						if ( title.Length > 0 )
						{
							title = title + ", ";
						}
						title = title + g.Abbreviation + "";
					}
					else
					{
						title = g.Name + " [" + g.Abbreviation + "]";
					}
					AddLabelCropped( 222, offset, 209, 20, 2100, title );
                  }
				  else if ( GetPlayerInfo.GetStatusGuild( m ) != "" )
				  {
                     AddLabelCropped( 222, offset, 209, 20, 2100, GetPlayerInfo.GetStatusGuild( m ) );
				  }

                  AddLabelCropped( 453, offset,  60, 20, 2100, m.RawStatTotal.ToString() );
                  AddLabelCropped( 515, offset,  60, 20, 2100, m.SkillsTotal.ToString() );
                  AddLabelCropped( 577, offset,  60, 20, 2100, m.Karma.ToString() );
                  AddLabelCropped( 639, offset,  60, 20, 2100, m.Fame.ToString() );
                  AddLabelCropped( 701, offset,  60, 20, 2100, m.Kills.ToString() );
               }
            }
            if ( i == 25 )
            {
               m_CountList[listPage] = (j - 25);
            }

         }
      }
	public override void OnResponse( NetState sender, RelayInfo info )
	{
		Mobile from = sender.Mobile;
		if ( info.ButtonID == 0 ) // Cancel
			return;
		else if ( from.Deleted || from.Map == null || from == null )
			return;


		switch ( info.ButtonID )
		{
			case 1:
			{
				if ( m_List != null && m_ListPage > 0 )
					from.SendGump( new StatusGump( from, m_ListPage - 1, m_List, m_CountList));

				break;
			}
			case 2:
			{
				if ( m_List != null && ( (m_ListPage + 1) * 25 < m_List.Count ) )
					from.SendGump( new StatusGump( from, m_ListPage + 1, m_List, m_CountList));

				break;
			}
			default:
			{
				break;
			}
		}
	}
   }
}