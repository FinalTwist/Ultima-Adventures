using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
	public class CharacterStatueGump : Gump
	{
		private Item m_Maker;
		private CharacterStatue m_Statue;
		private Timer m_Timer;
		private Mobile m_Owner;
	
		private enum Buttons
		{
			Close,
			Sculpt,			
			PosePrev,
			PoseNext,
			DirPrev,
			DirNext,
			MatPrev,
			MatNext,
			Restore
		}
	
		public CharacterStatueGump( Item maker, CharacterStatue statue, Mobile owner ) : base( 60, 36 )
		{
			m_Maker = maker;
			m_Statue = statue;
			m_Owner = owner;
			
			if ( m_Statue == null )
				return;
			
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;
		
			AddPage( 0 );

			AddImage(30, 22, 1140);
			AddHtml( 91, 71, 270, 26, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>Character Statue Carving</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 92, 110, 104, 19, @"<BODY><BASEFONT Color=#111111><BIG>Direction</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 92, 135, 104, 19, @"<BODY><BASEFONT Color=#111111><BIG>" + GetDirectionNumber( m_Statue.Direction ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(93, 165, 4014, 4014, (int)Buttons.DirNext, GumpButtonType.Reply, 0);
			AddButton(130, 165, 4005, 4005, (int)Buttons.DirPrev, GumpButtonType.Reply, 0);

			AddHtml( 255, 110, 104, 19, @"<BODY><BASEFONT Color=#111111><BIG>Material</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 255, 135, 104, 19, @"<BODY><BASEFONT Color=#111111><BIG>" + GetMaterialNumber( m_Statue.StatueType, m_Statue.Material ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(294, 165, 4014, 4014, (int)Buttons.MatNext, GumpButtonType.Reply, 0);
			AddButton(331, 165, 4005, 4005, (int)Buttons.MatPrev, GumpButtonType.Reply, 0);

			AddButton(66, 232, 241, 243, (int)Buttons.Close, GumpButtonType.Reply, 0);
			AddButton(319, 232, 247, 248, (int)Buttons.Sculpt, GumpButtonType.Reply, 0);

			// restore			
			if ( m_Maker is CharacterStatueDeed )
			{
				AddButton(197, 219, 2322, 2324, (int)Buttons.Restore, GumpButtonType.Reply, 0);
			}

			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 2.5 ), TimeSpan.FromSeconds( 2.5 ), new TimerCallback( CheckOnline ) );
		}

		private void CheckOnline()
		{
			if ( m_Owner != null && m_Owner.NetState == null )
			{
				if ( m_Timer != null )
					m_Timer.Stop();

				if ( m_Statue != null && !m_Statue.Deleted )
					m_Statue.Delete();
			}
		}
		
		private string GetMaterialNumber( StatueType type, StatueMaterial material )
		{
			switch ( material )
			{
				case StatueMaterial.Antique:
					
					switch ( type )
					{
						case StatueType.Bronze: return "Bronze";
						case StatueType.Jade: return "Jade";
						case StatueType.Marble: return "Marble";
					}

					return "Bronze";
				case StatueMaterial.Dark: 
					
					if ( type == StatueType.Marble )
						return "Dark";

					return "Dark";
				case StatueMaterial.Medium: return "Medium";
				case StatueMaterial.Light: return "Light";	
				default: return "Bronze";
			}
		}
		
		private string GetDirectionNumber( Direction direction )
		{
			switch ( direction )
			{
				case Direction.North: return "North";
				case Direction.Right: return "Right";
				case Direction.East: return "East";
				case Direction.Down: return "Down";	
				case Direction.South: return "South";
				case Direction.Left: return "Left";
				case Direction.West: return "West";
				case Direction.Up: return "Up";
				default: return "South";
			}
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{		
			if ( m_Statue == null || m_Statue.Deleted )
				return;
				
			bool sendGump = false;
				
			if ( info.ButtonID == (int) Buttons.Sculpt )
			{					
				if ( m_Maker is CharacterStatueDeed )
				{
					CharacterStatue backup = ( (CharacterStatueDeed) m_Maker ).Statue;
					
					if ( backup != null )
						backup.Delete();
				}
					
				if ( m_Maker != null )
					m_Maker.Delete();
					
				m_Statue.Sculpt( state.Mobile );
			}
			else if ( info.ButtonID == (int) Buttons.PosePrev )
			{
				m_Statue.Pose = (StatuePose) ( ( (int) m_Statue.Pose + 5 ) % 6 );
				sendGump = true;
			}
			else if ( info.ButtonID == (int) Buttons.PoseNext )
			{
				m_Statue.Pose = (StatuePose) ( ( (int) m_Statue.Pose + 1 ) % 6 );
				sendGump = true;
			}
			else if ( info.ButtonID == (int) Buttons.DirPrev )
			{
				m_Statue.Direction = (Direction) ( ( (int) m_Statue.Direction + 7 ) % 8 );
				m_Statue.InvalidatePose();
				sendGump = true;
			}
			else if ( info.ButtonID == (int) Buttons.DirNext )
			{
				m_Statue.Direction = (Direction) ( ( (int) m_Statue.Direction + 1 ) % 8 );
				m_Statue.InvalidatePose();
				sendGump = true;
			}
			else if ( info.ButtonID == (int) Buttons.MatPrev )
			{
				m_Statue.Material = (StatueMaterial) ( ( (int) m_Statue.Material + 3 ) % 4 );
				sendGump = true;
			}
			else if ( info.ButtonID == (int) Buttons.MatNext )
			{
				m_Statue.Material = (StatueMaterial) ( ( (int) m_Statue.Material + 1 ) % 4 );
				sendGump = true;
			}
			else if ( info.ButtonID == (int) Buttons.Restore )
			{
				if ( m_Maker is CharacterStatueDeed )
				{
					CharacterStatue backup = ( (CharacterStatueDeed) m_Maker ).Statue;
					
					if ( backup != null )
						m_Statue.Restore( backup );
				}
				
				sendGump = true;
			}
			else
			{
				m_Statue.Delete();
			}

			if ( sendGump )
				state.Mobile.SendGump( new CharacterStatueGump( m_Maker, m_Statue, m_Owner ) );
			
			if ( m_Timer != null )
				m_Timer.Stop();
		}

		public override void OnServerClose( NetState owner )
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			if ( m_Statue != null && !m_Statue.Deleted )
				m_Statue.Delete();
		}
	}
}
