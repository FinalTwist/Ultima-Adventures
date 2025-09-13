using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a gamepiece behaves much like an addon component
	public class GamePiece : Item
	{
		//offset from the boardgame control item
		public Point3D Offset;
		
		//reference to the LOSBlocker used to block line of sight thru this gamepiece
		public LOSBlocker _Blocker;
		
		//reference to the boardgame control item that this piece belongs to
		public BoardGameControlItem BoardGameControlItem;
				
		//randomize itemid constructor
		public GamePiece( int itemidmin, int itemidmax, string name ) : this( Utility.RandomMinMax( itemidmin, itemidmax ), name )
		{
		}
		
		//randomize itemid constructor
		public GamePiece( int itemidmin, int itemidmax, string name, bool blocklos ) : this( Utility.RandomMinMax( itemidmin, itemidmax ), name, blocklos )
		{
		}
		
		//default no block los constructor
		public GamePiece( int itemid, string name ) : this( itemid, name, false )
		{
		}
		
		//master constructor
		public GamePiece( int itemid, string name, bool blocklos ) : base( itemid )
		{
			Movable = false;
			Name = name;
			
			if( blocklos )
			{
				_Blocker = new LOSBlocker();
			}
		}
		
		//deserialize constructor
		public GamePiece( Serial serial ) : base( serial )
		{
		}
		
		public void RegisterToBoardGameControlItem( BoardGameControlItem boardgamecontrolitem, Point3D offset )
		{
			BoardGameControlItem = boardgamecontrolitem;
			Offset = offset;
			
			UpdatePosition();
		}

		
		//move the item based on its position with respect to the boardgame control item
		public void UpdatePosition()
		{
			if( BoardGameControlItem != null )
			{
				MoveToWorld( new Point3D( BoardGameControlItem.X + BoardGameControlItem.BoardOffset.X + Offset.X, BoardGameControlItem.Y + BoardGameControlItem.BoardOffset.Y + Offset.Y, BoardGameControlItem.Z + BoardGameControlItem.BoardOffset.Z + Offset.Z ), BoardGameControlItem.BoardMap );
			}
			else
			{
				Delete();
			}
		}

		public override void OnLocationChange( Point3D old )
		{
			if( BoardGameControlItem != null )
			{
				BoardGameControlItem.Location = new Point3D( X - BoardGameControlItem.BoardOffset.X - Offset.X, Y - BoardGameControlItem.BoardOffset.Y - Offset.Y, Z - BoardGameControlItem.BoardOffset.Z - Offset.Z );
			}
			
			if( _Blocker != null )
			{
				_Blocker.MoveToWorld( Location, Map );
			}
		}

		public override void OnMapChange()
		{
			if( BoardGameControlItem != null && BoardGameControlItem.BoardMap != Map )
			{
				BoardGameControlItem.BoardMap = Map;
			}
			
			if( _Blocker != null )
			{
				_Blocker.MoveToWorld( Location, Map );
			}
		}
		
		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if( BoardGameControlItem != null )
			{
				BoardGameControlItem.Delete();
			}
			
			if( _Blocker != null )
			{
				_Blocker.Delete();
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 1 );
			
			writer.Write( (Item)_Blocker );

			writer.Write( (Item)BoardGameControlItem );
						
			writer.Write( Offset.X );
			writer.Write( Offset.Y );
			writer.Write( Offset.Z );
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				default:
				case 1:
				{
					_Blocker = (LOSBlocker)reader.ReadItem();
					goto case 0;
				}
				case 0:
				{
					
			
					BoardGameControlItem = (BoardGameControlItem)reader.ReadItem();
			
					Offset.X = reader.ReadInt();
					Offset.Y = reader.ReadInt();
					Offset.Z = reader.ReadInt();
					break;
				}
			}
		}
	}
}