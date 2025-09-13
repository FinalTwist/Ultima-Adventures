using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Commands;

namespace Server.Items
{
	public class QuestTeleporter : Item
	{
		public int TeleporterOpen;
		public int TeleporterSound;
		public int TeleporterItem;
		public string TeleporterMessage;
		public string TeleporterFail;
		public string TeleporterQuest;
		public string TeleporterLock;
		public string TeleporterLockMsg;
		public Point3D TeleporterPointDest;
		public Map TeleporterMapDest;

		[CommandProperty(AccessLevel.Owner)]
		public int Teleporter_Open { get { return TeleporterOpen; } set { TeleporterOpen = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Teleporter_Sound { get { return TeleporterSound; } set { TeleporterSound = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Teleporter_Item { get { return TeleporterItem; } set { TeleporterItem = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Teleporter_Message { get { return TeleporterMessage; } set { TeleporterMessage = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Teleporter_Fail { get { return TeleporterFail; } set { TeleporterFail = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Teleporter_Quest { get { return TeleporterQuest; } set { TeleporterQuest = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Teleporter_Lock { get { return TeleporterLock; } set { TeleporterLock = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Teleporter_LockMsg { get { return TeleporterLockMsg; } set { TeleporterLockMsg = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest { get { return TeleporterPointDest; } set { TeleporterPointDest = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest { get { return TeleporterMapDest; } set { TeleporterMapDest = value; InvalidateProperties(); } }

		[Constructable]
		public QuestTeleporter() : base( 0x1BC3 )
		{
			Name = "Quest Teleporter";
			Weight = 10;
			Movable = false;
		}

		public void CloseQuestTeleporter()
		{
			TeleporterOpen = 0;
		}

		public void DoQuestTeleporter( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				if ( CharacterDatabase.GetBardsTaleQuest( m, "BardsTaleWin" ) && this.Name == "a mysterious crystal ball" && this.X == 2830 && this.Y == 1875 )
				{
					m.PrivateOverheadMessage(MessageType.Regular, 1150, false, TeleporterLockMsg, m.NetState);
				}
				else if ( TeleporterOpen == 1 || TeleporterQuest == "blank" )
				{
					if ( TeleporterQuest != "blank" ){ CharacterDatabase.SetBardsTaleQuest( m, TeleporterQuest, true ); }
					BaseCreature.TeleportPets( m, TeleporterPointDest, TeleporterMapDest, false );
					m.MoveToWorld ( TeleporterPointDest, TeleporterMapDest );
					m.PlaySound( TeleporterSound );
					m.PrivateOverheadMessage(MessageType.Regular, 1150, false, TeleporterMessage, m.NetState);
				}
				else if ( CharacterDatabase.GetKeys( m, TeleporterQuest ) || CharacterDatabase.GetBardsTaleQuest( m, TeleporterQuest ) )
				{
					TeleporterOpen = 1;
					BaseCreature.TeleportPets( m, TeleporterPointDest, TeleporterMapDest, false );
					m.MoveToWorld ( TeleporterPointDest, TeleporterMapDest );
					m.PlaySound( TeleporterSound );
					Timer.DelayCall( TimeSpan.FromMinutes( 2.0 ), new TimerCallback( CloseQuestTeleporter ) );
					m.PrivateOverheadMessage(MessageType.Regular, 1150, false, TeleporterMessage, m.NetState);
				}
				else
				{
					m.PrivateOverheadMessage(MessageType.Regular, 1150, false, TeleporterFail, m.NetState);
				}
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoQuestTeleporter( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
        {
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoQuestTeleporter( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
        }

		public QuestTeleporter( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( TeleporterOpen );
            writer.Write( TeleporterSound );
            writer.Write( TeleporterItem );
            writer.Write( TeleporterMessage );
            writer.Write( TeleporterFail );
            writer.Write( TeleporterQuest );
            writer.Write( TeleporterLock );
            writer.Write( TeleporterLockMsg );
			writer.Write( TeleporterPointDest );
			writer.Write( TeleporterMapDest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            TeleporterOpen = reader.ReadInt();
            TeleporterSound = reader.ReadInt();
            TeleporterItem = reader.ReadInt();
            TeleporterMessage = reader.ReadString();
            TeleporterFail = reader.ReadString();
            TeleporterQuest = reader.ReadString();
            TeleporterLock = reader.ReadString();
            TeleporterLockMsg = reader.ReadString();
			TeleporterPointDest = reader.ReadPoint3D();
			TeleporterMapDest = reader.ReadMap();
			CloseQuestTeleporter();
		}
	}
}