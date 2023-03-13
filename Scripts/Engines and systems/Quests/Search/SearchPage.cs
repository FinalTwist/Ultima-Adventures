using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using Server.Gumps;

namespace Server.Items
{
	public class SearchPage : Item
	{
		public Mobile owner;
		public string SearchMessage;
		public string SearchDungeon;
		public Map DungeonMap;
		public string SearchWorld;
		public string SearchType;
		public string SearchItem;
		public int LegendReal;
		public int LegendPercent;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[CommandProperty(AccessLevel.Owner)]
		public string Search_Message { get { return SearchMessage; } set { SearchMessage = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Search_Dungeon { get { return SearchDungeon; } set { SearchDungeon = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public Map Dungeon_Map { get { return DungeonMap; } set { DungeonMap = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Search_World { get { return SearchWorld; } set { SearchWorld = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Search_Type { get { return SearchType; } set { SearchType = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Search_Item { get { return SearchItem; } set { SearchItem = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Legend_Real { get { return LegendReal; } set { LegendReal = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Legend_Percent { get { return LegendPercent; } set { LegendPercent = value; InvalidateProperties(); } }

		[Constructable]
		public SearchPage( Mobile from, int LegendLore, string seekType, string seekName ) : base( 0x2159 )
		{
			SearchItem = seekName;
			SearchType = seekType;

			this.owner = from;
			Weight = 1.0;
			Hue = 0x995;
			Name = "a parchment";

			switch ( LegendLore )
			{
				case 1:	Name = "highly unlikely legend for " + from.Name;	break;
				case 2:	Name = "unlikely legend for " + from.Name;	break;
				case 3:	Name = "somewhat unlikely legend for " + from.Name;	break;
				case 4:	Name = "somewhat reliable legend for " + from.Name;	break;
				case 5:	Name = "reliable legend for " + from.Name;	break;
				case 6:	Name = "highly reliable legend for " + from.Name;	break;
			}

			/// CHECK TO SEE IF THE NOTE IS FALSE OR TRUE
			LegendLore = ( LegendLore * 10 ) + 10;
			LegendReal = 0;
			if ( LegendLore >= Utility.RandomMinMax( 1, 100 ) ){ LegendReal = 1; }
			LegendPercent = LegendLore;

			PickSearchLocation( this, "No Dungeon Yet", from );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, SearchItem);
            list.Add( 1049644, SearchDungeon);
        }

		public class SearchGump : Gump
		{
			public SearchGump( Mobile from, Item parchment ): base( 100, 100 )
			{
				SearchPage scroll = (SearchPage)parchment;
				string sText = scroll.SearchMessage;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(37, 28, 1249);
				AddHtml( 86, 72, 303, 237, @"<BODY><BASEFONT Color=#111111><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage( "This must be in your backpack to read." );
			}
			else
			{
				e.CloseGump( typeof( SearchGump ) );
				e.SendGump( new SearchGump( e, this ) );
				e.PlaySound( 0x249 );
			}
		}

		public static void PickSearchLocation( SearchPage scroll, string DungeonNow, Mobile from )
		{
			string thisWorld = "the Land of Sosaria";
			string thisPlace = "the Dungeon of Doom";
			Map thisMap = Map.Trammel;

			int aCount = 0;
			ArrayList targets = new ArrayList();
			foreach ( Item target in World.Items.Values )
			if ( target is SearchBase && ( MyServerSettings.GetDifficultyLevel( target.Location, target.Map ) <= GetPlayerInfo.GetPlayerDifficulty( from ) ) )
			{
				string tWorld = Worlds.GetMyWorld( target.Map, target.Location, target.X, target.Y );
				Region region = Region.Find( target.Location, target.Map );
				targets.Add( target );
				aCount++;
			}

			aCount = Utility.RandomMinMax( 1, aCount );

			int xCount = 0;
			for ( int i = 0; i < targets.Count; ++i )
			{
				xCount++;

				if ( xCount == aCount )
				{
					Item finding = ( Item )targets[ i ];
					thisWorld = Worlds.GetMyWorld( finding.Map, finding.Location, finding.X, finding.Y );
					thisMap = finding.Map;
					thisPlace = Server.Misc.Worlds.GetRegionName( finding.Map, finding.Location );
				}
			}

			string Word1 = "Legends";
			switch ( Utility.RandomMinMax( 1, 4 ) )
			{
				case 1:	Word1 = "Rumors"; break;
				case 2:	Word1 = "Myths"; break;
				case 3:	Word1 = "Tales"; break;
				case 4:	Word1 = "Stories"; break;
			}
			string Word2 = "lost";
			switch ( Utility.RandomMinMax( 1, 4 ) )
			{
				case 1:	Word2 = "kept"; break;
				case 2:	Word2 = "seen"; break;
				case 3:	Word2 = "taken"; break;
				case 4:	Word2 = "hidden"; break;
			}
			string Word3 = "deep in";
			switch ( Utility.RandomMinMax( 1, 4 ) )
			{
				case 1:	Word3 = "within"; break;
				case 2:	Word3 = "somewhere in"; break;
				case 3:	Word3 = "somehow in"; break;
				case 4:	Word3 = "far in"; break;
			}
			string Word4 = "centuries ago";
			switch ( Utility.RandomMinMax( 1, 4 ) )
			{
				case 1:	Word4 = "thousands of years ago"; break;
				case 2:	Word4 = "decades ago"; break;
				case 3:	Word4 = "millions of years ago"; break;
				case 4:	Word4 = "many years ago"; break;
			}

			string sMessage = 

            scroll.SearchDungeon = thisPlace;
            scroll.SearchWorld = thisWorld;
			scroll.DungeonMap = thisMap;

			string EntranceLocation = Worlds.GetAreaEntrance( scroll.SearchDungeon, scroll.DungeonMap );

			string OldMessage = "<br><br><br><br><br><br>" + scroll.SearchMessage;

			scroll.SearchMessage = scroll.SearchItem + "<br><br>" + Word1 + " tell of the " + scroll.SearchItem + " being " + Word2 + " " + Word3;
			scroll.SearchMessage = scroll.SearchMessage + " " + scroll.SearchDungeon + " " + Word4 + " by " + QuestCharacters.QuestGiver() + ".";
			scroll.SearchMessage = scroll.SearchMessage + " in " + scroll.SearchWorld + " at the below sextant coordinates.<br><br>" + EntranceLocation + OldMessage;

			scroll.InvalidateProperties();
		}

		public static void ArtifactQuestTimeAllowed( Mobile m )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
			DateTime TimeFinished = DateTime.UtcNow;
			DB.ArtifactQuestTime = Convert.ToString(TimeFinished);
		}

		public static int ArtifactQuestTimeNew( Mobile m )
		{
			int QuestTime = 90000;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string sTime = DB.ArtifactQuestTime;

			if ( sTime != null )
			{
				DateTime TimeThen = Convert.ToDateTime(sTime);
				DateTime TimeNow = DateTime.UtcNow;
				long ticksThen = TimeThen.Ticks;
				long ticksNow = TimeNow.Ticks;
				int minsThen = (int)TimeSpan.FromTicks(ticksThen).TotalMinutes;
				int minsNow = (int)TimeSpan.FromTicks(ticksNow).TotalMinutes;
				QuestTime = minsNow - minsThen;
			}

			return QuestTime;
		}

		public SearchPage(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.Write( (Mobile)owner);
            writer.Write( SearchMessage );
            writer.Write( SearchDungeon );
            writer.Write( DungeonMap );
            writer.Write( SearchWorld );
            writer.Write( SearchType );
            writer.Write( SearchItem );
            writer.Write( LegendReal );
            writer.Write( LegendPercent );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			SearchMessage = reader.ReadString();
			SearchDungeon = reader.ReadString();
			DungeonMap = reader.ReadMap();
			SearchWorld = reader.ReadString();
			SearchType = reader.ReadString();
			SearchItem = reader.ReadString();
			LegendReal = reader.ReadInt();
			LegendPercent = reader.ReadInt();
			ItemID = 0x2159;
			Hue = 0x995;
		}
	}
}