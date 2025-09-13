using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class BombermanObstacle : GamePiece
	{
		public virtual bool Destructable{ get{ return false; } }
		
		//randomize itemid constructors
		public BombermanObstacle( int itemidmin, int itemidmax, string name ) : this( Utility.RandomMinMax( itemidmin, itemidmax ), name )
		{
		}
		
		public BombermanObstacle( int itemidmin, int itemidmax, string name, bool blocklos ) : this( Utility.RandomMinMax( itemidmin, itemidmax ), name, blocklos )
		{
		}
		
		//default no LOS blocker constructor
		public BombermanObstacle( int itemid, string name ) : this( itemid, name, false )
		{
		}
		
		//master constructor
		public BombermanObstacle( int itemid, string name, bool blocklos ) : base( itemid, name, blocklos )
		{
		}
		
		//deserialize constructor
		public BombermanObstacle( Serial serial ) : base( serial )
		{
		}
		
		public virtual void Destroy()
		{
			if( BoardGameControlItem != null )
			{
				BoardGameControlItem.BackgroundItems.Remove( this );
				BoardGameControlItem = null;
			}
			
			Delete();
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}