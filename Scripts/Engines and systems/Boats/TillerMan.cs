using System;
using Server;
using Server.Multis;
using Server.Network;
using Server.Gumps;
using Server.Misc;

namespace Server.Items
{
	public class TillerMan : Item
	{
		private BaseBoat m_Boat;

		public TillerMan( BaseBoat boat ) : base( 0x3E4E )
		{
			m_Boat = boat;
			Movable = false;


			if ( BaseBoat.isCarpet( m_Boat ) )
			{
				ItemID = 0x5439;
				Name = "magic lamp";
		}
		}


		public TillerMan( Serial serial ) : base(serial)
		{
		}

		public void SetFacing( Direction dir )
		{
			if ( BaseBoat.isCarpet( m_Boat ) )
			{
				switch ( dir )
				{
					case Direction.South: ItemID = 0x5421; break;
					case Direction.North: ItemID = 0x5439; break;
					case Direction.West:  ItemID = 0x5420; break;
					case Direction.East:  ItemID = 0x5421; break;
				}
			}
			else
			{
			switch ( dir )
			{
				case Direction.South: ItemID = 0x3E4B; break;
				case Direction.North: ItemID = 0x3E4E; break;
				case Direction.West:  ItemID = 0x3E50; break;
				case Direction.East:  ItemID = 0x3E55; break;
			}
		}
		}


		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( m_Boat.Status );
		}

		public void Say( int number )
		{
			PublicOverheadMessage( MessageType.Regular, 0x3B2, number );
		}

		public void Say( int number, string args )
		{
			PublicOverheadMessage( MessageType.Regular, 0x3B2, number, args );
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( m_Boat != null && m_Boat.ShipName != null )
				list.Add( BaseBoat.translateText( m_Boat, 1042884 ), m_Boat.ShipName ); // the tiller man of the ~1_SHIP_NAME~
			else
				base.AddNameProperty( list );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( m_Boat != null && m_Boat.ShipName != null )
				LabelTo( from, BaseBoat.translateText( m_Boat, 1042884 ), m_Boat.ShipName ); // the tiller man of the ~1_SHIP_NAME~
			else
				base.OnSingleClick( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_Boat != null )
			{
				if( m_Boat.Owner == from || from.AccessLevel >= AccessLevel.Administrator )
				{
					if( m_Boat.Contains( from ) )
					{
						from.CloseGump( typeof( TillerManGump ) );
						from.SendGump( new TillerManGump( from, m_Boat, false ) ); //m_Boat.BeginRename( from );
					}
					else if ( DockSearch.NearDock(from) == false && !BaseBoat.isCarpet( m_Boat ) )
					{
						from.SendMessage( "You must be near a dock to dry dock your ship!" );
					}
					else
					{
						m_Boat.BeginDryDock( from, m_Boat.Hue );
					}
				}
				else from.SendLocalizedMessage( 501023 ); // You must be the owner to use this item
			}
		}

        public override void OnDoubleClickDead( Mobile from )
        {
			if ( m_Boat != null )
			{
				if( m_Boat.Owner == from || from.AccessLevel >= AccessLevel.Administrator )
				{
					if( m_Boat.Contains( from ) )
					{
						from.CloseGump( typeof( TillerManGump ) );
						from.SendGump( new TillerManGump( from, m_Boat, false ) ); //m_Boat.BeginRename( from );
					}
					else if ( DockSearch.NearDock(from) == false && !BaseBoat.isCarpet( m_Boat ) )
					{
						from.SendMessage( "You must be near a dock to dry dock your ship!" );
					}
					else
					{
						m_Boat.BeginDryDock( from, m_Boat.Hue );
					}
				}
				else from.SendLocalizedMessage( 501023 ); // You must be the owner to use this item
			}
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is MapItem && m_Boat != null && m_Boat.CanCommand( from ) && m_Boat.Contains( from ) )
			{
				m_Boat.AssociateMap( (MapItem) dropped );
			}

			return false;
		}

		public override void OnAfterDelete()
		{
			if ( m_Boat != null )
				m_Boat.Delete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );//version

			writer.Write( m_Boat );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Boat = reader.ReadItem() as BaseBoat;

					if ( m_Boat == null )
						Delete();

					break;
				}
			}
		}
	}
}