using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class moongates : Item
	{
		private bool m_Active, m_Creatures, m_CombatCheck;
		private Point3D m_PointDest;
		private Map m_MapDest;
		private bool m_SourceEffect;
		private bool m_DestEffect;
		private int m_SoundID;
		private TimeSpan m_Delay;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SourceEffect
		{
			get{ return m_SourceEffect; }
			set{ m_SourceEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool DestEffect
		{
			get{ return m_DestEffect; }
			set{ m_DestEffect = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SoundID
		{
			get{ return m_SoundID; }
			set{ m_SoundID = 0x1FE; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan Delay
		{
			get{ return m_Delay; }
			set{ m_Delay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set { m_Active = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest
		{
			get { return m_PointDest; }
			set { m_PointDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest
		{
			get { return m_MapDest; }
			set { m_MapDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Creatures
		{
			get { return m_Creatures; }
			set { m_Creatures = value; InvalidateProperties(); }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public bool CombatCheck
		{
			get { return m_CombatCheck; }
			set { m_CombatCheck = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return 1023948; } } // moongate

		[Constructable]
		public moongates() : this( new Point3D( 0, 0, 0 ), null, false )
		{
		}

		[Constructable]
		public moongates( Point3D pointDest, Map mapDest ) : this( pointDest, mapDest, false )
		{
		}

		[Constructable]
		public moongates( Point3D pointDest, Map mapDest, bool creatures ) : base( 0xF6C )
		{
			Movable = false;
			Visible = true;
			SoundID = 0x1FE;
			Light = LightType.Circle300;

			m_Active = true;
			m_PointDest = pointDest;
			m_MapDest = mapDest;
			m_Creatures = creatures;

			m_CombatCheck = false;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Active )
				list.Add( 1060742 ); // active
			else
				list.Add( 1060743 ); // inactive

			if ( m_MapDest != null )
				list.Add( 1060658, "Map\t{0}", m_MapDest );

			if ( m_PointDest != Point3D.Zero )
				list.Add( 1060659, "Coords\t{0}", m_PointDest );

			list.Add( 1060660, "Creatures\t{0}", m_Creatures ? "Yes" : "No" );
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Active )
			{
				if ( m_MapDest != null && m_PointDest != Point3D.Zero )
					LabelTo( from, "{0} [{1}]", m_PointDest, m_MapDest );
				else if ( m_MapDest != null )
					LabelTo( from, "[{0}]", m_MapDest );
				else if ( m_PointDest != Point3D.Zero )
					LabelTo( from, m_PointDest.ToString() );
			}
			else
			{
				LabelTo( from, "(inactive)" );
			}
		}

		public virtual void StartTeleport( Mobile m )
		{
			if ( m_Delay == TimeSpan.Zero )
				DoTeleport( m );
			else
				Timer.DelayCall( m_Delay, new TimerStateCallback( DoTeleport_Callback ), m );
		}

		private void DoTeleport_Callback( object state )
		{
			DoTeleport( (Mobile) state );
		}

		public virtual void DoTeleport( Mobile m )
		{
			Map map = m_MapDest;

			if ( map == null || map == Map.Internal )
				map = m.Map;

			Point3D p = m_PointDest;

			if ( p == Point3D.Zero )
				p = m.Location;

			Server.Mobiles.BaseCreature.TeleportPets( m, p, map );

			bool sendEffect = ( !m.Hidden || m.AccessLevel == AccessLevel.Player );

			if ( m_SourceEffect && sendEffect )
				Effects.SendLocationEffect( m.Location, m.Map, 0x3728, 10, 10 );

			m.MoveToWorld( p, map );

			if ( m_DestEffect && sendEffect )
				Effects.SendLocationEffect( m.Location, m.Map, 0x3728, 10, 10 );

			if ( m_SoundID > 0 && sendEffect )
				Effects.PlaySound( m.Location, m.Map, m_SoundID );
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m_Active )
			{
				if ( !m_Creatures && !m.Player )
					return true;
				else if ( m_CombatCheck && SpellHelper.CheckCombat( m ) )
				{
					m.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
					return true;				
				}

				StartTeleport( m );
				return false;
			}

			return true;
		}

		public moongates( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version

			writer.Write( (bool) m_CombatCheck );

			writer.Write( (bool) m_SourceEffect );
			writer.Write( (bool) m_DestEffect );
			writer.Write( (TimeSpan) m_Delay );
			writer.WriteEncodedInt( (int) m_SoundID );

			writer.Write( m_Creatures );

			writer.Write( m_Active );
			writer.Write( m_PointDest );
			writer.Write( m_MapDest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_CombatCheck = reader.ReadBool();
					goto case 2;
				}
				case 2:
				{
					m_SourceEffect = reader.ReadBool();
					m_DestEffect = reader.ReadBool();
					m_Delay = reader.ReadTimeSpan();
					m_SoundID = reader.ReadEncodedInt();

					goto case 1;
				}
				case 1:
				{
					m_Creatures = reader.ReadBool();

					goto case 0;
				}
				case 0:
				{
					m_Active = reader.ReadBool();
					m_PointDest = reader.ReadPoint3D();
					m_MapDest = reader.ReadMap();

					break;
				}
			}
		}
	}

	public class Skillmoongates : moongates
	{
		private SkillName m_Skill;
		private double m_Required;
		private string m_MessageString;
		private int m_MessageNumber;

		[CommandProperty( AccessLevel.GameMaster )]
		public SkillName Skill
		{
			get{ return m_Skill; }
			set{ m_Skill = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public double Required
		{
			get{ return m_Required; }
			set{ m_Required = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string MessageString
		{
			get{ return m_MessageString; }
			set{ m_MessageString = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MessageNumber
		{
			get{ return m_MessageNumber; }
			set{ m_MessageNumber = value; InvalidateProperties(); }
		}

		private void EndMessageLock( object state )
		{
			((Mobile)state).EndAction( this );
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( Active )
			{
				if ( !Creatures && !m.Player )
					return true;

				Skill sk = m.Skills[m_Skill];

				if ( sk == null || sk.Base < m_Required )
				{
					if ( m.BeginAction( this ) )
					{
						if ( m_MessageString != null )
							m.Send( new UnicodeMessage( Serial, ItemID, MessageType.Regular, 0x3B2, 3, "ENU", null, m_MessageString ) );
						else if ( m_MessageNumber != 0 )
							m.Send( new MessageLocalized( Serial, ItemID, MessageType.Regular, 0x3B2, 3, m_MessageNumber, null, "" ) );

						Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( EndMessageLock ), m );
					}

					return false;
				}

				StartTeleport( m );
				return false;
			}

			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			int skillIndex = (int)m_Skill;
			string skillName;

			if ( skillIndex >= 0 && skillIndex < SkillInfo.Table.Length )
				skillName = SkillInfo.Table[skillIndex].Name;
			else
				skillName = "(Invalid)";

			list.Add( 1060661, "{0}\t{1:F1}", skillName, m_Required );

			if ( m_MessageString != null )
				list.Add( 1060662, "Message\t{0}", m_MessageString );
			else if ( m_MessageNumber != 0 )
				list.Add( 1060662, "Message\t#{0}", m_MessageNumber );
		}

		[Constructable]
		public Skillmoongates()
		{
		}

		public Skillmoongates( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Skill );
			writer.Write( (double) m_Required );
			writer.Write( (string) m_MessageString );
			writer.Write( (int) m_MessageNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Skill = (SkillName)reader.ReadInt();
					m_Required = reader.ReadDouble();
					m_MessageString = reader.ReadString();
					m_MessageNumber = reader.ReadInt();

					break;
				}
			}
		}
	}

	public class Keywordmoongates : moongates
	{
		private string m_Substring;
		private int m_Keyword;
		private int m_Range;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Substring
		{
			get{ return m_Substring; }
			set{ m_Substring = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Keyword
		{
			get{ return m_Keyword; }
			set{ m_Keyword = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Range
		{
			get{ return m_Range; }
			set{ m_Range = value; InvalidateProperties(); }
		}

		public override bool HandlesOnSpeech{ get{ return true; } }

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && Active )
			{
				Mobile m = e.Mobile;

				if ( !Creatures && !m.Player )
					return;

				if ( !m.InRange( GetWorldLocation(), m_Range ) )
					return;

				bool isMatch = false;

				if ( m_Keyword >= 0 && e.HasKeyword( m_Keyword ) )
					isMatch = true;
				else if ( m_Substring != null && e.Speech.ToLower().IndexOf( m_Substring.ToLower() ) >= 0 )
					isMatch = true;

				if ( !isMatch )
					return;

				e.Handled = true;
				StartTeleport( m );
			}
		}

		public override bool OnMoveOver( Mobile m )
		{
			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060661, "Range\t{0}", m_Range );

			if ( m_Keyword >= 0 )
				list.Add( 1060662, "Keyword\t{0}", m_Keyword );

			if ( m_Substring != null )
				list.Add( 1060663, "Substring\t{0}", m_Substring );
		}

		[Constructable]
		public Keywordmoongates()
		{
			m_Keyword = -1;
			m_Substring = null;
		}

		public Keywordmoongates( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Substring );
			writer.Write( m_Keyword );
			writer.Write( m_Range );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Substring = reader.ReadString();
					m_Keyword = reader.ReadInt();
					m_Range = reader.ReadInt();

					break;
				}
			}
		}
	}
	public class NothingGate : moongates
	{
		private string m_Substring;
		private int m_Keyword;
		private int m_Range;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Substring
		{
			get{ return m_Substring; }
			set{ m_Substring = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Keyword
		{
			get{ return m_Keyword; }
			set{ m_Keyword = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Range
		{
			get{ return m_Range; }
			set{ m_Range = value; InvalidateProperties(); }
		}

		public override bool HandlesOnSpeech{ get{ return true; } }

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled && Active )
			{
				Mobile m = e.Mobile;

				if ( !Creatures && !m.Player )
					return;

				if ( !m.InRange( GetWorldLocation(), m_Range ) )
					return;

				bool isMatch = false;

				if ( m_Keyword >= 0 && e.HasKeyword( m_Keyword ) )
					isMatch = true;
				else if ( m_Substring != null && e.Speech.ToLower().IndexOf( m_Substring.ToLower() ) >= 0 )
					isMatch = true;

				if ( !isMatch )
					return;

				e.Handled = true;
				
				if (CheckNothing(m))
					StartTeleport( m );
			}
		}
		
		public bool CheckNothing(Mobile m)
		{
			if (m == null || !(m is PlayerMobile) || !m.Alive )
				return false;
			
			PlayerMobile mp = (PlayerMobile)m;
			bool something = false;
		
			Item magebook = mp.FindItemOnLayer( Layer.Talisman );
			Item tunic = mp.FindItemOnLayer( Layer.InnerTorso );
			Item shirt = mp.FindItemOnLayer( Layer.Shirt );
			Item robe = mp.FindItemOnLayer( Layer.OuterTorso );
			Item shoes = mp.FindItemOnLayer( Layer.Shoes );
			Item glove = mp.FindItemOnLayer( Layer.Gloves );
			Item pants = mp.FindItemOnLayer( Layer.Pants );
			Item skirt = mp.FindItemOnLayer( Layer.OuterLegs );
			Item belt = mp.FindItemOnLayer( Layer.Waist );
			Item sash = mp.FindItemOnLayer( Layer.MiddleTorso );
			Item neck = mp.FindItemOnLayer( Layer.Neck );
			Item arms = mp.FindItemOnLayer( Layer.Arms );
			Item cloak = mp.FindItemOnLayer( Layer.Cloak );
			Item helm = mp.FindItemOnLayer( Layer.Helm );
			Item ear = mp.FindItemOnLayer( Layer.Earrings );
			Item wrist = mp.FindItemOnLayer( Layer.Bracelet );
			Item finger = mp.FindItemOnLayer( Layer.Ring );
			Item twoHanded = mp.FindItemOnLayer( Layer.TwoHanded );
			Item oneHanded = mp.FindItemOnLayer( Layer.OneHanded );
			Item firstvalid = mp.FindItemOnLayer( Layer.FirstValid );
			
			if ( magebook != null || tunic != null || wrist != null || finger != null || ear != null || sash != null || belt != null || shirt != null || skirt != null || robe != null || shoes != null || glove != null || pants != null || neck != null || arms != null || cloak != null || twoHanded != null || oneHanded != null || firstvalid != null )
				something = true;
			
			foreach ( Item item in mp.Backpack.Items )
			{
				if (item.Visible )
					something = true;
			}
			
			if (m.Followers > 0)
				something = true;

			if (something)
			{
				m.SendMessage("You need to be completely naked and have no items in your pack to enter this gate.");
				return false;
			}
			
			return true;
		
		}

		public override bool OnMoveOver( Mobile m )
		{
			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( 1060661, "Range\t{0}", m_Range );

			if ( m_Keyword >= 0 )
				list.Add( 1060662, "Keyword\t{0}", m_Keyword );

			if ( m_Substring != null )
				list.Add( 1060663, "Substring\t{0}", m_Substring );
		}

		[Constructable]
		public NothingGate()
		{
			m_Keyword = -1;
			m_Substring = null;
		}

		public NothingGate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Substring );
			writer.Write( m_Keyword );
			writer.Write( m_Range );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Substring = reader.ReadString();
					m_Keyword = reader.ReadInt();
					m_Range = reader.ReadInt();

					break;
				}
			}
		}
	}	
	
}
