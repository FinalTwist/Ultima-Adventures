using System;
using Server;
using Server.Guilds;
using Server.Network;


namespace Server.Gumps
{
	public class GuildAdminCandidatesGump : GuildMobileListGump
	{
		public GuildAdminCandidatesGump( Mobile from, Guild guild ) : base( from, guild, true, guild.Candidates )
		{
		}

		protected override void Design()
		{
			AddHtmlLocalized( 20, 10, 400, 35, 1013075, false, false ); // Accept or Refuse candidates for membership

			AddButton( 20, 400, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 55, 400, 245, 30, 1013076, false, false ); // Accept

			AddButton( 300, 400, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 335, 400, 100, 35, 1013077, false, false ); // Refuse
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( GuildGump.BadLeader( m_Mobile, m_Guild ) )
				return;

			switch ( info.ButtonID )
			{
				case 0:
				{
					GuildGump.EnsureClosed( m_Mobile );
					m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );

					break;
				}
				case 1: // Accept
				{
					int[] switches = info.Switches;

					if ( switches.Length > 0 )
					{
						int index = switches[0];

						if ( index >= 0 && index < m_List.Count )
						{
							Mobile m = (Mobile)m_List[index];

							if ( m != null && !m.Deleted )
							{

								m_Guild.Candidates.Remove( m );
								m_Guild.Accepted.Add( m );

								GuildGump.EnsureClosed( m_Mobile );

								if ( m_Guild.Candidates.Count > 0 )
									m_Mobile.SendGump( new GuildAdminCandidatesGump( m_Mobile, m_Guild ) );
								else
									m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );
							}
						}
					}

					break;
				}
				case 2: // Refuse
				{
					int[] switches = info.Switches;

					if ( switches.Length > 0 )
					{
						int index = switches[0];

						if ( index >= 0 && index < m_List.Count )
						{
							Mobile m = (Mobile)m_List[index];

							if ( m != null && !m.Deleted )
							{
								m_Guild.Candidates.Remove( m );

								GuildGump.EnsureClosed( m_Mobile );

								if ( m_Guild.Candidates.Count > 0 )
									m_Mobile.SendGump( new GuildAdminCandidatesGump( m_Mobile, m_Guild ) );
								else
									m_Mobile.SendGump( new GuildmasterGump( m_Mobile, m_Guild ) );
							}
						}
					}

					break;
				}
			}
		}
	}
}