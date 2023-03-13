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
	public class CourierMail : Item
	{
		public Mobile owner;
		public string SearchMessage;
		public string SearchDungeon;
		public Map DungeonMap;
		public string SearchWorld;
		public string SearchItem;
		public string ForWho;
		public string ForWhere;
		public string ForAlignment;
		public int MsgComplete;
		public int MsgReward;

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
		public string Search_Item { get { return SearchItem; } set { SearchItem = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string For_Who { get { return ForWho; } set { ForWho = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string For_Where { get { return ForWhere; } set { ForWhere = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string For_Alignment { get { return ForAlignment; } set { ForAlignment = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Msg_Complete { get { return MsgComplete; } set { MsgComplete = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Msg_Reward { get { return MsgReward; } set { MsgReward = value; InvalidateProperties(); } }

		[Constructable]
		public CourierMail( Mobile from ) : base( 0x2159 )
		{
			Weight = 1.0;
			Hue = 0x9C4;
			Name = "Message for " + from.Name;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "From " + ForWho);
			if ( MsgComplete > 0 ){ list.Add( 1049644, "Complete"); }
        }

		public class SearchGump : Gump
		{
			public SearchGump( Mobile from, Item parchment ): base( 100, 100 )
			{
				CourierMail scroll = (CourierMail)parchment;
				string sText = scroll.SearchMessage;

				if ( scroll.MsgComplete > 0 ){ sText = "You have found the '" + scroll.SearchItem + "'. Return to " + scroll.ForWho + " and bring them this message.<br><br>" + sText; }

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(34, 32, 1247);
				AddHtml( 96, 68, 272, 227, @"<BODY><BASEFONT Color=#111111><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
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

		public CourierMail(Serial serial) : base(serial)
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
            writer.Write( SearchItem );
		    writer.Write( ForWho );
		    writer.Write( ForWhere );
		    writer.Write( ForAlignment );
		    writer.Write( MsgComplete );
		    writer.Write( MsgReward );
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
			SearchItem = reader.ReadString();
			ForWho = reader.ReadString();
			ForWhere = reader.ReadString();
			ForAlignment = reader.ReadString();
			MsgComplete = reader.ReadInt();
			MsgReward = reader.ReadInt();
			ItemID = 0x2159;
		}
	}
}