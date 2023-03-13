using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;
using Server.Accounting;


namespace Server.Items
{
	public class ThiefNote : Item
	{
		public Mobile NoteOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Note_Owner { get{ return NoteOwner; } set{ NoteOwner = value; } }

		public string NoteItemCategory;
		[CommandProperty(AccessLevel.Owner)]
		public string Note_ItemCategory { get { return NoteItemCategory; } set { NoteItemCategory = value; InvalidateProperties(); } }

		public string NoteItem;
		[CommandProperty(AccessLevel.Owner)]
		public string Note_Item { get { return NoteItem; } set { NoteItem = value; InvalidateProperties(); } }

		public int NoteItemGot;
		[CommandProperty(AccessLevel.Owner)]
		public int Note_ItemGot { get { return NoteItemGot; } set { NoteItemGot = value; InvalidateProperties(); } }

		public string NoteItemArea;
		[CommandProperty(AccessLevel.Owner)]
		public string Note_ItemArea { get { return NoteItemArea; } set { NoteItemArea = value; InvalidateProperties(); } }

		public string NoteItemPerson;
		[CommandProperty(AccessLevel.Owner)]
		public string Note_ItemPerson { get { return NoteItemPerson; } set { NoteItemPerson = value; InvalidateProperties(); } }

		public int NoteDeliverType;
		[CommandProperty(AccessLevel.Owner)]
		public int Note_DeliverType { get { return NoteDeliverType; } set { NoteDeliverType = value; InvalidateProperties(); } }

		public string NoteDeliverTo;
		[CommandProperty(AccessLevel.Owner)]
		public string Note_DeliverTo { get { return NoteDeliverTo; } set { NoteDeliverTo = value; InvalidateProperties(); } }

		public int NoteReward;
		[CommandProperty(AccessLevel.Owner)]
		public int Note_Reward { get { return NoteReward; } set { NoteReward = value; InvalidateProperties(); } }

		public string NoteStory;
		[CommandProperty(AccessLevel.Owner)]
		public string Note_Story { get { return NoteStory; } set { NoteStory = value; InvalidateProperties(); } }
		
		public int Consecutive;
		[CommandProperty(AccessLevel.Owner)]
		public int Con_secutive { get { return Consecutive; } set { Consecutive = value; InvalidateProperties(); } }

		[Constructable]
		public ThiefNote() : base( 0x2DD )
		{
			Name = "secret note";
			Weight = 1.0;
			Consecutive = 0;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( NoteOwner != null ){ list.Add( 1049644, "Coded for " + NoteOwner.Name + "" ); }
			if ( Consecutive > 0 ){ list.Add( 1070722, "Consecutive missions: " + Consecutive ); }
			if ( NoteItemGot > 0 ){ list.Add( 1070722, "Stolen " + NoteItem ); }
			
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( ThiefAllowed( from ) != null )
			{
				from.SendMessage( "You need a break from the last job, so read this note in about " + ThiefAllowed( from ) + " minutes." );
			}
			else if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( NoteOwner != from )
			{
				from.SendMessage( "This note is written in a code you don't understand so you throw it away!" );
				bool remove = true;
				foreach ( Account a in Accounts.GetAccounts() )
				{
					if (a == null)
						break;

					int index = 0;

					for (int i = 0; i < a.Length; ++i)
					{
						Mobile m = a[i];

						if (m == null)
							continue;

						if ( m == NoteOwner )
						{
							m.AddToBackpack( this );
							remove = false;
						}

						++index;
					}
				}
				if ( remove )
				{
					this.Delete();
				}
			}
			else
			{
				from.PlaySound( 0x249 );
				from.CloseGump( typeof( NoteGump ) );
				from.SendGump( new NoteGump( this ) );
			}
		}

		public static void ThiefTimeAllowed( Mobile m )
		{
			DateTime TimeFinished = DateTime.UtcNow;
			string sFinished = Convert.ToString(TimeFinished);
			CharacterDatabase.SetQuestInfo( m, "ThiefQuest", sFinished );
		}

		public static int ThiefTimeNew( Mobile m )
		{
			int ThiefTime = 10000;
			string sTime = CharacterDatabase.GetQuestInfo( m, "ThiefQuest" );

			if ( sTime.Length > 0 && !( CharacterDatabase.GetQuestState( m, "ThiefQuest" ) ) )
			{
				DateTime TimeThen = Convert.ToDateTime(sTime);
				DateTime TimeNow = DateTime.UtcNow;
				long ticksThen = TimeThen.Ticks;
				long ticksNow = TimeNow.Ticks;
				int minsThen = (int)TimeSpan.FromTicks(ticksThen).TotalMinutes;
				int minsNow = (int)TimeSpan.FromTicks(ticksNow).TotalMinutes;
				ThiefTime = minsNow - minsThen;
			}
			return ThiefTime;
		}

		public static string ThiefAllowed( Mobile from )
		{
			int nAllowedForAnotherQuest = ThiefTimeNew( from );
			int nServerQuestTimeAllowed = MyServerSettings.GetTimeBetweenQuests();
			int nWhenForAnotherQuest = nServerQuestTimeAllowed - nAllowedForAnotherQuest;
			string sAllowedForAnotherQuest = nWhenForAnotherQuest.ToString();

			if ( nWhenForAnotherQuest > 0 ){ return sAllowedForAnotherQuest; }

			return null;
		}

		public ThiefNote(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);

			writer.Write( (Mobile)NoteOwner);
			writer.Write( NoteItemCategory );
			writer.Write( NoteItem );
			writer.Write( NoteItemGot );
			writer.Write( NoteItemArea );
			writer.Write( NoteItemPerson );
			writer.Write( NoteDeliverType );
			writer.Write( NoteDeliverTo );
			writer.Write( NoteReward );
			writer.Write( NoteStory );
			writer.Write( Consecutive );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			NoteOwner = reader.ReadMobile();
			NoteItemCategory = reader.ReadString();
			NoteItem = reader.ReadString();
			NoteItemGot = reader.ReadInt();
			NoteItemArea = reader.ReadString();
			NoteItemPerson = reader.ReadString();
			NoteDeliverType = reader.ReadInt();
			NoteDeliverTo = reader.ReadString();
			NoteReward = reader.ReadInt();
			NoteStory = reader.ReadString();
			Consecutive = reader.ReadInt();
		}

		public class NoteGump : Gump
		{
			private ThiefNote m_Note;

			public NoteGump( ThiefNote note ) : base( 25, 25 )
			{
				m_Note = note;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(114, 0, 9391);
				AddImage(0, 140, 9396);
				AddImage(285, 140, 9398);
				AddImage(171, 0, 9391);
				AddImage(228, 0, 9391);
				AddImage(285, 0, 9392);
				AddImage(114, 140, 9397);
				AddImage(171, 140, 9397);
				AddImage(228, 140, 9397);

				string describe = "<br><br>Keep this note with you at all times if you are going to pursue this job. If you are to steal something from a dungeon, you need to find the pedestal with the bag or box that a thief would normally attempt to steal. Use the bag or box on the pedestal to see if you successfully steal the item. If a town merchant is someone you need to pilfer from, find their coffer use your stealing skill on it to see if you steal the item from that. Be warned, you may be flagged a criminal and the guards will surely dispatch of you if caught. If you manage to escape with you life, and the item sought, then bring this note to the location specified in these instructions. If you lose this secret note, then find the thief guildmaster and they will give you a copy of the message.";

				AddHtml( 28, 40, 342, 201, @"<BODY><BASEFONT Color=#111111><BIG>" + m_Note.NoteStory + describe + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
		}

		public static Item GetMyCurrentJob( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				if (m.Backpack == null)
					return null;

				foreach ( Item item in m.Backpack.Items )
				if ( item is ThiefNote )
				{
					if ( ((ThiefNote)item).NoteOwner == m ){ return item; }
				}

			}
			return null;
		}

		public static void SetupNote( ThiefNote note, Mobile m )
		{
			SetupNote( note, m, 0);
		}

		public static void SetupNote( ThiefNote note, Mobile m, int consecutive )
		{
			note.Hue = Utility.RandomList( 0x95E, 0x95D, 0x95B, 0x952, 0x957, 0x94D, 0x944, 0x945, 0x940, 0x93D, 0xB79 );
			note.ItemID = Utility.RandomList( 0x2DD, 0x201A );

			note.NoteOwner = m;

			note.NoteItemCategory = "";
			note.NoteItem = QuestCharacters.QuestItems( true );
			note.NoteItemGot = 0;
			note.NoteItemPerson = ContainerFunctions.GetOwner( "Pilfer" );
			note.NoteDeliverType = Utility.RandomMinMax( 1, 2 );

			if ( Utility.RandomBool() ) // STEAL FROM TOWN
			{
				int rewardMod = 1;
				string searchLocation = "the Land of Sosaria";
				switch ( Utility.RandomMinMax( 0, 14 ) )
				{
					case 0:		searchLocation = "the Land of Sosaria";			break;
					case 1:		searchLocation = "the Land of Sosaria";			break;
					case 2:		searchLocation = "the Land of Sosaria";			break;
					case 3:		searchLocation = "the Land of Lodoria";			rewardMod = 2;	if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 4:		searchLocation = "the Land of Lodoria";			rewardMod = 2;	if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 5:		searchLocation = "the Land of Lodoria";			rewardMod = 2;	if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 6:		searchLocation = "the Serpent Island";			rewardMod = 3;	if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 7:		searchLocation = "the Serpent Island";			rewardMod = 3;	if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 8:		searchLocation = "the Serpent Island";			rewardMod = 3;	if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 9:		searchLocation = "the Isles of Dread";			rewardMod = 4;	if ( !( CharacterDatabase.GetDiscovered( m, "the Isles of Dread" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 10:	searchLocation = "the Savaged Empire";			rewardMod = 5;	if ( !( CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 11:	searchLocation = "the Savaged Empire";			rewardMod = 5;	if ( !( CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 12:	searchLocation = "the Island of Umber Veil";	rewardMod = 2;	if ( !( CharacterDatabase.GetDiscovered( m, "the Island of Umber Veil" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 13:	searchLocation = "the Bottle World of Kuldar";	rewardMod = 4;	if ( !( CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 14:	searchLocation = "DarkMoor";					rewardMod = 3;	if ( !( CharacterDatabase.GetDiscovered( m, "DarkMoor" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
				}

				if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" ) ) && searchLocation == "the Land of Sosaria" )
				{
					if ( m.Skills.Cap == 11000 ){ searchLocation = "the Savaged Empire"; }
					else { searchLocation = "the Land of Lodoria"; }
				}

				if ( searchLocation == "the Land of Sosaria" ){ rewardMod = 1; }

				int aCount = 0;
				ArrayList targets = new ArrayList();
				foreach ( Item target in World.Items.Values )
				if ( target is Coffer && Worlds.GetMyWorld( target.Map, target.Location, target.X, target.Y ) == searchLocation )
				{
					targets.Add( target ); aCount++;
				}

				aCount = Utility.RandomMinMax( 1, aCount );

				int xCount = 0;
				for ( int i = 0; i < targets.Count; ++i )
				{
					xCount++;

					if ( xCount == aCount )
					{
						Item finding = ( Item )targets[ i ];
						Coffer coffer = (Coffer)finding;
						note.NoteItemArea = coffer.CofferTown;
						note.NoteItemCategory = coffer.CofferType;
						note.Consecutive = consecutive;
						int modded = Server.Misc.AdventuresFunctions.DiminishingReturns(consecutive, 200);
						note.NoteReward = (int)((double)( ( rewardMod * (int)(m.Skills[SkillName.Stealing].Value * 10) ) + ( Utility.RandomMinMax( 1, 15 ) * rewardMod * (int)(m.Skills[SkillName.Stealing].Value * 2) ) ) * (1+ ((double)modded/100)));
						note.NoteReward = (int)( ((double)Server.Misc.MyServerSettings.QuestRewardModifier(note.NoteOwner, null) * 0.01) * note.NoteReward );
					}
				}
			}
			else // STEAL FROM DUNGEON
			{
				string searchLocation = "the Land of Sosaria";
				switch ( Utility.RandomMinMax( 0, 15 ) )
				{
					case 0:		searchLocation = "the Land of Sosaria";			break;
					case 1:		searchLocation = "the Land of Sosaria";			break;
					case 2:		searchLocation = "the Land of Sosaria";			break;
					case 3:		searchLocation = "the Land of Lodoria";			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 4:		searchLocation = "the Land of Lodoria";			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 5:		searchLocation = "the Land of Lodoria";			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 6:		searchLocation = "the Serpent Island";			if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 7:		searchLocation = "the Serpent Island";			if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 8:		searchLocation = "the Serpent Island";			if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 9:		searchLocation = "the Isles of Dread";			if ( !( CharacterDatabase.GetDiscovered( m, "the Isles of Dread" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 10:	searchLocation = "the Savaged Empire";			if ( !( CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 11:	searchLocation = "the Savaged Empire";			if ( !( CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 12:	searchLocation = "the Island of Umber Veil";	if ( !( CharacterDatabase.GetDiscovered( m, "the Island of Umber Veil" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 13:	searchLocation = "the Bottle World of Kuldar";	if ( !( CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
					case 14:	searchLocation = "the Underworld";				if ( !( CharacterDatabase.GetDiscovered( m, "the Underworld" ) ) ){ searchLocation = "the Underworld"; } break;
					case 15:	searchLocation = "the Land of Ambrosia";		if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Ambrosia" ) ) ){ searchLocation = "the Land of Sosaria"; } break;
				}

				if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" ) ) && searchLocation == "the Land of Sosaria" )
				{
					if ( m.Skills.Cap == 11000 ){ searchLocation = "the Savaged Empire"; }
					else { searchLocation = "the Land of Lodoria"; }
				}

				int aCount = 0;
				ArrayList targets = new ArrayList();
				foreach ( Item target in World.Items.Values )
				if ( target is StealBase && Worlds.GetMyWorld( target.Map, target.Location, target.X, target.Y ) == searchLocation )
				{
					targets.Add( target ); aCount++;
				}

				aCount = Utility.RandomMinMax( 1, aCount );

				int xCount = 0;
				for ( int i = 0; i < targets.Count; ++i )
				{
					xCount++;

					if ( xCount == aCount )
					{
						Item finding = ( Item )targets[ i ];
						note.NoteItemArea = Server.Misc.Worlds.GetRegionName( finding.Map, finding.Location );
						int difficult = Server.Misc.MyServerSettings.GetDifficultyLevel( finding.Location, finding.Map ) + 2;
							if ( difficult < 2 ){ difficult = 2; }
						//note.NoteReward = ( difficult * 500 ) + ( Utility.RandomMinMax( 0, 10 ) * 50 ); Final increased and made dependent on skill level too.
						note.Consecutive = consecutive;
						int modded = Server.Misc.AdventuresFunctions.DiminishingReturns(consecutive, 200);
						note.NoteReward = (int)((double)(( difficult * (int)(m.Skills[SkillName.Stealing].Value * 10) ) + ( Utility.RandomMinMax( 0, 30 ) * (int)(m.Skills[SkillName.Stealing].Value * 2) )) * (1+ ((double)modded/100)));
						note.NoteReward = (int)( ((double)Server.Misc.MyServerSettings.QuestRewardModifier(note.NoteOwner, null) * 0.01) * note.NoteReward );
					}
				}
			}

			string dropLocation = "the Land of Sosaria";
			switch ( Utility.RandomMinMax( 0, 14 ) )
			{
				case 0:		dropLocation = "the Land of Sosaria";			break;
				case 1:		dropLocation = "the Land of Sosaria";			break;
				case 2:		dropLocation = "the Land of Sosaria";			break;
				case 3:		dropLocation = "the Land of Lodoria";			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 4:		dropLocation = "the Land of Lodoria";			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 5:		dropLocation = "the Land of Lodoria";			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 6:		dropLocation = "the Serpent Island";			if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 7:		dropLocation = "the Serpent Island";			if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 8:		dropLocation = "the Serpent Island";			if ( !( CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 9:		dropLocation = "the Isles of Dread";			if ( !( CharacterDatabase.GetDiscovered( m, "the Isles of Dread" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 10:	dropLocation = "the Savaged Empire";			if ( !( CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 11:	dropLocation = "the Savaged Empire";			if ( !( CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 12:	dropLocation = "the Island of Umber Veil";		if ( !( CharacterDatabase.GetDiscovered( m, "the Island of Umber Veil" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 13:	dropLocation = "the Bottle World of Kuldar";	if ( !( CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
				case 14:	dropLocation = "DarkMoor";						if ( !( CharacterDatabase.GetDiscovered( m, "DarkMoor" ) ) ){ dropLocation = "the Land of Sosaria"; } break;
			}

			if ( !( CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" ) ) && dropLocation == "the Land of Sosaria" )
			{
				if ( m.Skills.Cap == 11000 ){ dropLocation = "the Savaged Empire"; }
				else { dropLocation = "the Land of Lodoria"; }
			}

			int dCount = 0;
			ArrayList drops = new ArrayList();
			foreach ( Item target in World.Items.Values )
			if ( ( (note.NoteDeliverType == 1 && target is HollowStump) || (note.NoteDeliverType == 2 && target is HayCrate) ) && Worlds.GetMyWorld( target.Map, target.Location, target.X, target.Y ) == dropLocation )
			{
				drops.Add( target ); dCount++;
			}

			dCount = Utility.RandomMinMax( 1, dCount );

			int sCount = 0;
			for ( int i = 0; i < drops.Count; ++i )
			{
				sCount++;

				if ( sCount == dCount )
				{
					Item finding = ( Item )drops[ i ];

					if ( finding is HayCrate )
					{
						HayCrate hay = (HayCrate)finding;
						if (hay.HayTown != null && hay.HayTown != "")
							note.NoteDeliverTo = hay.HayTown;
						else 
							note.NoteDeliverTo = "the City of Britain";
							
					}
					else if ( finding is HollowStump )
					{
						HollowStump stump = (HollowStump)finding;
						if (stump.StumpTown != null && stump.StumpTown != "")
							note.NoteDeliverTo = stump.StumpTown;
						else 
							note.NoteDeliverTo = "the City of Britain";
					}
				}
			}

			string action = "recover";
				switch( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: action = "recover"; break;
					case 1: action = "steal"; break;
					case 2: action = "acquire"; break;
					case 3: action = "find"; break;
					case 4: action = "get"; break;
				}

			string drop = "drop";
				switch( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: drop = "leave"; break;
					case 1: drop = "place"; break;
					case 2: drop = "set"; break;
					case 3: drop = "put"; break;
					case 4: drop = "drop"; break;
				}

			string container = "crate of hay in";
			if ( note.NoteDeliverType == 1 ){ container = "hollow stump near"; }

			string location = note.NoteItemArea;
				if ( note.NoteItemCategory != "" && note.NoteItemCategory != null ){ location = "the " + note.NoteItemCategory + " in " + note.NoteItemArea; }

			note.NoteStory = note.NoteItemPerson + " wants you to " + action + " " + note.NoteItem + " from " + location + ".";
			note.NoteStory = note.NoteStory + " Once you have it, " + drop + " it in the " + container + " " + note.NoteDeliverTo + ".";
			note.NoteStory = note.NoteStory + " There you will also find your payment of " + note.NoteReward + " gold and instructions for your next job.";

			note.InvalidateProperties();
		}
	}
}
