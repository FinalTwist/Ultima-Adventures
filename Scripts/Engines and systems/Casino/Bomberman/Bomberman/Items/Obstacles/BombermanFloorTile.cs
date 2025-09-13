using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class BombermanFloorTile : BombermanObstacle
	{
		public BombermanFloorTile( BombermanStyle style ) : base( BombermanSettings.GetFloorTileID( style ), "Floor" )
		{
			if( style == BombermanStyle.Default )
			{
				Hue = 0x237;
			}
		}
		
		
		//deserialize constructor
		public BombermanFloorTile( Serial serial ) : base( serial )
		{
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