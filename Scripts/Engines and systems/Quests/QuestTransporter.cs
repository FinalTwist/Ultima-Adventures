using System;
using Server;
using Server.Network;
using Server.Spells;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;

namespace Server.Items
{
	public class QuestTransporter : Teleporter
	{
		private string m_TeleportName;
		private string m_Required;
		private string m_MessageString;

		[CommandProperty( AccessLevel.GameMaster )]
		public string TeleportName
		{
			get{ return m_TeleportName; }
			set{ m_TeleportName = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Required
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

				if ( m is PlayerMobile )
				{
					if ( !( CharacterDatabase.GetKeys( m, m_TeleportName ) ) && Required == "yes" )
					{
						if ( m.BeginAction( this ) )
						{
							if ( m_MessageString != null )
								m.Send( new UnicodeMessage( Serial, ItemID, MessageType.Regular, 0x3B2, 3, "ENU", null, m_MessageString ) );

							Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( EndMessageLock ), m );
						}

						return false;
					}
				}
				StartTeleport( m );
				return false;
			}

			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060661, "Key\t{0}", m_TeleportName );
			if ( m_MessageString != null )
				list.Add( 1060662, "Message\t{0}", m_MessageString );
		}

		[Constructable]
		public QuestTransporter()
		{
		}

		public QuestTransporter( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (string) m_TeleportName );
			writer.Write( (string) m_Required );
			writer.Write( (string) m_MessageString );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TeleportName = reader.ReadString();
					m_Required = reader.ReadString();
					m_MessageString = reader.ReadString();

					break;
				}
			}
		}
	}
}