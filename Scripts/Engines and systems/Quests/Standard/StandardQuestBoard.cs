using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	[Flipable(0x1E5F, 0x1E5E)]
	public class StandardQuestBoard : Item
	{
		[Constructable]
		public StandardQuestBoard() : base(0x1E5E)
		{
			Weight = 1.0;
			Name = "Seeking Brave Adventurers";
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from ) );
			list.Add( new StandardQuestEntry( from ) );
			list.Add( new StandardQuestComplete( from ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			
			public SpeechGumpEntry( Mobile from ) : base( 1024, 3 )
			{
				m_Mobile = from;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "Seeking Brave Adventurers", SpeechFunctions.SpeechText( m_Mobile.Name, m_Mobile.Name, "QuestBoard" ) ));
					}
				}
            }
        }

		public class StandardQuestEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			
			public StandardQuestEntry( Mobile from ) : base( 6120, 12 )
			{
				m_Mobile = from;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				string myQuest = CharacterDatabase.GetQuestInfo( m_Mobile, "StandardQuest" );

				int nAllowedForAnotherQuest = StandardQuestFunctions.QuestTimeNew( m_Mobile );
				int nServerQuestTimeAllowed = MyServerSettings.GetTimeBetweenQuests();
				int nWhenForAnotherQuest = nServerQuestTimeAllowed - nAllowedForAnotherQuest;
				string sAllowedForAnotherQuest = nWhenForAnotherQuest.ToString();

				if ( CharacterDatabase.GetQuestState( m_Mobile, "StandardQuest" ) )
				{
					m_Mobile.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You are already on a quest. Return here when you are done.", m_Mobile.NetState);
				}
				else if ( nWhenForAnotherQuest > 0 )
				{
					m_Mobile.PrivateOverheadMessage(MessageType.Regular, 1150, false, "There are no quests at the moment. Check back in " + sAllowedForAnotherQuest + " minutes.", m_Mobile.NetState);
				}
				else
				{
					int nFame = m_Mobile.Fame * 2;
						nFame = Utility.RandomMinMax( 0, nFame )+2000;

					StandardQuestFunctions.FindTarget( m_Mobile, nFame );

					string TellQuest = StandardQuestFunctions.QuestStatus( m_Mobile ) + ".";
					m_Mobile.PrivateOverheadMessage(MessageType.Regular, 1150, false, TellQuest, m_Mobile.NetState);
				}
            }
        }

		public class StandardQuestComplete : ContextMenuEntry
		{
			private Mobile m_Mobile;
			
			public StandardQuestComplete( Mobile from ) : base( 548, 12 )
			{
				m_Mobile = from;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				string myQuest = CharacterDatabase.GetQuestInfo( m_Mobile, "StandardQuest" );

				int nSucceed = StandardQuestFunctions.DidQuest( m_Mobile );

				if ( nSucceed > 0 )
				{
					StandardQuestFunctions.PayAdventurer( m_Mobile );
				}
				else if ( myQuest.Length > 0 )
				{
					if ( ! m_Mobile.HasGump( typeof( SpeechGump ) ) )
					{
						m_Mobile.SendGump(new SpeechGump( "Your Reputation Is At Stake", SpeechFunctions.SpeechText( m_Mobile.Name, m_Mobile.Name, "QuestBoardFail" ) ));
					}
				}
				else
				{
					m_Mobile.PrivateOverheadMessage(MessageType.Regular, 1150, false, "You are not currently on a quest.", m_Mobile.NetState);
				}
            }
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				int nPenalty = StandardQuestFunctions.QuestFailure( from );

				if ( dropped.Amount == nPenalty )
				{
					CharacterDatabase.ClearQuestInfo( from, "StandardQuest" );
					from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Someone else will eventually take care of this.", from.NetState);
					dropped.Delete();
				}
				else
				{
					from.AddToBackpack ( dropped );
				}
			}
			else
			{
				from.AddToBackpack ( dropped );
			}
			return true;
		}

		public StandardQuestBoard(Serial serial) : base(serial)
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