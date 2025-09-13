using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomberman obstacle defines the object types
	public class DestructableWall : BombermanObstacle
	{
		public override bool Destructable{ get{ return true; } }
		
		public DestructableWall( BombermanStyle style ) : base( BombermanSettings.GetDestructableWallID( style ), "Bombable Wall" )
		{
			if( style == BombermanStyle.Default )
			{
				Hue = 0x3BB;
			}
		}
		
		
		//deserialize constructor
		public DestructableWall( Serial serial ) : base( serial )
		{
		}
		
		public override void Destroy()
		{
			//spawn powerup
			if( Utility.RandomDouble() < BombermanSettings.UPGRADE_SPAWN_CHANCE )
			{
				if( BoardGameControlItem.State == BoardGameState.Active )
				{
					BombermanUpgrade upgrade = BombermanUpgrade.GetRandomUpgrade();
					upgrade.RegisterToBoardGameControlItem( BoardGameControlItem, new Point3D( Offset.X, Offset.Y, Offset.Z + 3 ) );
				}
			}
			
			base.Destroy();
			
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