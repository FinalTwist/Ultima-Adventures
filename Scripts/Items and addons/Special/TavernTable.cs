using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using System.Collections;

namespace Server.Items
{
	[Flipable( 0x55D9, 0x55DA )]
	public class TavernTable : Item
	{
		[Constructable]
		public TavernTable() : base( 0x55D9 )
		{
			Name = "tavern table";
			Weight = 20.0;
			Light = LightType.Circle225;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "For Adventurers To Drink in Your Home");
            list.Add( 1049644, "Use the Table to Configure it");
        } 

		public override void OnDoubleClick( Mobile from )
		{
			if ( Movable )
			{
				from.SendMessage( "This must be secured down in a home to use." );
			}
			else if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendMessage( "You will have to get closer to use that." );
			}
			else
			{
				from.PlaySound( 0x4A );
				from.CloseGump( typeof( TavernGump ) );
				from.SendGump( new TavernGump( this ) );
			}

			return;
		}

		public TavernTable( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( PatronNorth );
			writer.Write( PatronSouth );
			writer.Write( PatronEast );
			writer.Write( PatronWest );
			writer.Write( (Mobile)DrinkerNorth );
			writer.Write( (Mobile)DrinkerSouth );
			writer.Write( (Mobile)DrinkerEast );
			writer.Write( (Mobile)DrinkerWest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			PatronNorth = reader.ReadInt();
			PatronSouth = reader.ReadInt();
			PatronEast = reader.ReadInt();
			PatronWest = reader.ReadInt();
			DrinkerNorth = reader.ReadMobile();
			DrinkerSouth = reader.ReadMobile();
			DrinkerEast = reader.ReadMobile();
			DrinkerWest = reader.ReadMobile();
		}

		public int PatronNorth;
		[CommandProperty(AccessLevel.Owner)]
		public int Patron_North{ get { return PatronNorth; } set { PatronNorth = value; InvalidateProperties(); } }

		public int PatronSouth;
		[CommandProperty(AccessLevel.Owner)]
		public int Patron_South{ get { return PatronSouth; } set { PatronSouth = value; InvalidateProperties(); } }

		public int PatronEast;
		[CommandProperty(AccessLevel.Owner)]
		public int Patron_East{ get { return PatronEast; } set { PatronEast = value; InvalidateProperties(); } }

		public int PatronWest;
		[CommandProperty(AccessLevel.Owner)]
		public int Patron_West{ get { return PatronWest; } set { PatronWest = value; InvalidateProperties(); } }

		public Mobile DrinkerNorth;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Drinker_North { get{ return DrinkerNorth; } set{ DrinkerNorth = value; } }

		public Mobile DrinkerSouth;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Drinker_South { get{ return DrinkerSouth; } set{ DrinkerSouth = value; } }

		public Mobile DrinkerEast;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Drinker_East { get{ return DrinkerEast; } set{ DrinkerEast = value; } }

		public Mobile DrinkerWest;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Drinker_West { get{ return DrinkerWest; } set{ DrinkerWest = value; } }

		public override void OnLocationChange( Point3D oldLocation )
		{
			RemovePatrons( this );
		}

		public override bool OnDragLift( Mobile from )
		{
			RemovePatrons( this );
			return true;
		}

		public override void OnDelete()
		{
			RemovePatrons( this );
			base.OnDelete();
		}

		public static void AddPatrons( TavernTable table )
		{
			RemovePatrons( table );
			Point3D location = new Point3D(0,0,0);
			Direction direction = Direction.East;
			Mobile patron = null;

			if ( table.PatronNorth > 0 && !table.Movable )
			{
				location = new Point3D( table.X, table.Y-1, table.Z );
				if ( (table.Map).CanSpawnMobile( table.X, table.Y-1, table.Z ) )
				{
					direction = Direction.South;
					patron = new HouseVisitor();
					patron.MoveToWorld( location, table.Map );
					patron.Direction = direction;
					table.DrinkerNorth = patron;
					Server.Misc.TavernPatrons.RemoveSomeGear( patron, true );
				}
				else { table.PatronNorth = 0; }
			}
			if ( table.PatronSouth > 0 && !table.Movable )
			{
				location = new Point3D( table.X, table.Y+1, table.Z );
				if ( (table.Map).CanSpawnMobile( table.X, table.Y+1, table.Z ) )
				{
					direction = Direction.North;
					patron = new HouseVisitor();
					patron.MoveToWorld( location, table.Map );
					patron.Direction = direction;
					table.DrinkerSouth = patron;
					Server.Misc.TavernPatrons.RemoveSomeGear( patron, true );
				}
				else { table.PatronSouth = 0; }
			}
			if ( table.PatronEast > 0 && !table.Movable )
			{
				location = new Point3D( table.X+1, table.Y, table.Z );
				if ( (table.Map).CanSpawnMobile( table.X+1, table.Y, table.Z ) )
				{
					direction = Direction.West;
					patron = new HouseVisitor();
					patron.MoveToWorld( location, table.Map );
					patron.Direction = direction;
					table.DrinkerEast = patron;
					Server.Misc.TavernPatrons.RemoveSomeGear( patron, true );
				}
				else { table.PatronEast = 0; }
			}
			if ( table.PatronWest > 0 && !table.Movable )
			{
				location = new Point3D( table.X-1, table.Y, table.Z );
				if ( (table.Map).CanSpawnMobile( table.X-1, table.Y, table.Z ) )
				{
					direction = Direction.East;
					patron = new HouseVisitor();
					patron.MoveToWorld( location, table.Map );
					patron.Direction = direction;
					table.DrinkerWest = patron;
					Server.Misc.TavernPatrons.RemoveSomeGear( patron, true );
				}
				else { table.PatronWest = 0; }
			}
		}

		public static void RemovePatrons( TavernTable table )
		{
			List<Mobile> targets = new List<Mobile>();

			foreach ( Mobile m in table.GetMobilesInRange( 1 ) )
			{
				if ( m.Map == table.Map && m is HouseVisitor && ( m == table.DrinkerNorth || m == table.DrinkerSouth || m == table.DrinkerEast || m == table.DrinkerWest ) )
					targets.Add( m );
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = targets[i];
				m.Delete();
			}
		}

		public static int CountPatrons( Mobile m )
		{
			int count = 0;
			foreach ( Item i in m.GetItemsInRange( 1 ) )
			{
				if ( i.Map == m.Map && !( i.Movable ) && i is TavernTable && ( m == ((TavernTable)i).DrinkerNorth || m == ((TavernTable)i).DrinkerSouth || m == ((TavernTable)i).DrinkerEast || m == ((TavernTable)i).DrinkerWest ) )
				{
					if ( ((TavernTable)i).PatronNorth > 0 )
						count++;

					if ( ((TavernTable)i).PatronSouth > 0 )
						count++;

					if ( ((TavernTable)i).PatronEast > 0 )
						count++;

					if ( ((TavernTable)i).PatronWest > 0 )
						count++;
				}
			}
			return count;
		}

		public static void PopulateHomes()
		{
			ArrayList patrons = new ArrayList();
			foreach ( Mobile patron in World.Mobiles.Values )
			{
				if ( patron is HouseVisitor )
				{
					patrons.Add( patron );
				}
			}
			for ( int i = 0; i < patrons.Count; ++i )
			{
				Mobile person = ( Mobile )patrons[ i ];
				person.Delete();
			}

			ArrayList tables = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is TavernTable && !item.Movable )
				{
					tables.Add( item );
				}
			}
			for ( int i = 0; i < tables.Count; ++i )
			{
				Item item = ( Item )tables[ i ];
				TavernTable table = (TavernTable)item;
				AddPatrons( table );
			}
		}
	}

    public class TavernGump : Gump
    {
        private TavernTable m_Table;

        public TavernGump( TavernTable table ): base( 25, 25 )
        {
            m_Table = table;

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(0, 300, 155);
			AddImage(300, 300, 155);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(5, 6, 133);
			AddImage(234, 45, 132);
			AddImage(414, 536, 130);
			AddImage(25, 365, 128);
			AddImage(600, 0, 155);
			AddImage(600, 300, 155);
			AddImage(598, 2, 129);
			AddImage(598, 298, 129);
			AddImage(670, 38, 149);
			AddImage(431, 45, 132);
			AddImage(571, 536, 130);
			AddImage(851, 531, 143);
			AddItem(788, 407, 21977);

			int button = 3609;

			// EAST
			if ( m_Table.PatronEast > 0 ){ button = 4017; } else { button = 3609; }
			AddButton(841, 468, button, button, 1, GumpButtonType.Reply, 0);

			// SOUTH
			if ( m_Table.PatronSouth > 0 ){ button = 4017; } else { button = 3609; }
			AddButton(751, 468, button, button, 2, GumpButtonType.Reply, 0);

			// NORTH
			if ( m_Table.PatronNorth > 0 ){ button = 4017; } else { button = 3609; }
			AddButton(841, 398, button, button, 3, GumpButtonType.Reply, 0);

			// WEST
			if ( m_Table.PatronWest > 0 ){ button = 4017; } else { button = 3609; }
			AddButton(751, 398, button, button, 4, GumpButtonType.Reply, 0);

			AddItem(753, 206, 21534);
			AddHtml( 117, 103, 606, 324, @"<BODY><BIG><BASEFONT Color=#FCFF00>Tavern tables are small drinking tables that you can place in your home. When these are properly locked down, you can configure the table for the number of patrons that will stand next to the table. These patrons will be random adventurers, temporarily seeking refuge in your home. They provide no benefit other than offer their company and brief or vague tales of adventure. Every so often, the adventurers will move on and others will take their place to rest. Simply check the boxes by the table icon, where you want patrons to stand.<br><br>Remember, these patrons will not aid you on your quests like the citizens you encounter in the villages and towns. They will not repair your items, sell you unusual items, or randomly tell others where a special relic or character is located. They simply provide your home with a bit of atmosphere. Placing many tables with many patrons will not aid you in finding the location of Exodus or the last known location of the Candle of Love. They will not give you clues on where you can find museum relics or items needed for a quest you have taken from a dead adventurer's journal. So place them sparingly in your home so you can best see what they may be talking about with each other. Lastly, a lone patron at a table will have no one to talk to so if you want them to chatter then make sure you have at least two patrons per table.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 273, 67, 382, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>TAVERN TABLES</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(751, 270, 19717);
			AddItem(450, 450, 21978);
			AddItem(650, 450, 21977);
			AddItem(550, 450, 21978);
			AddItem(826, 328, 3011);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 1 )
			{
				from.SendSound( 0x4A );
				if ( m_Table.PatronEast > 0 ){ m_Table.PatronEast = 0; } else { m_Table.PatronEast = 1; }
				Server.Items.TavernTable.AddPatrons( m_Table );
				from.SendGump( new TavernGump( m_Table ) );
			}
			else if ( info.ButtonID == 2 )
			{
				from.SendSound( 0x4A );
				if ( m_Table.PatronSouth > 0 ){ m_Table.PatronSouth = 0; } else { m_Table.PatronSouth = 1; }
				Server.Items.TavernTable.AddPatrons( m_Table );
				from.SendGump( new TavernGump( m_Table ) );
			}
			else if ( info.ButtonID == 3 )
			{
				from.SendSound( 0x4A );
				if ( m_Table.PatronNorth > 0 ){ m_Table.PatronNorth = 0; } else { m_Table.PatronNorth = 1; }
				Server.Items.TavernTable.AddPatrons( m_Table );
				from.SendGump( new TavernGump( m_Table ) );
			}
			else if ( info.ButtonID == 4 )
			{
				from.SendSound( 0x4A );
				if ( m_Table.PatronWest > 0 ){ m_Table.PatronWest = 0; } else { m_Table.PatronWest = 1; }
				Server.Items.TavernTable.AddPatrons( m_Table );
				from.SendGump( new TavernGump( m_Table ) );
			}
			else
			{
				from.SendSound( 0x4A );
			}
		}
	}
}