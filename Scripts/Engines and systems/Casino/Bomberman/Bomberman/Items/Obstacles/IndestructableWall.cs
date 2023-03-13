using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class IndestructableWall : BombermanObstacle
	{
		public IndestructableWall( BombermanStyle style, bool blocklos ) : base( BombermanSettings.GetIndestructableWallID( style ), "Wall", blocklos )
		{
			if( style == BombermanStyle.Default )
			{
				Hue = 0x3E4;
			}
		}
		
		
		//deserialize constructor
		public IndestructableWall( Serial serial ) : base( serial )
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