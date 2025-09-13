using System;
using Server;

namespace Solaris.BoardGames
{
	public enum BlastDirection
	{
		None = 0x0,
		West = 0x1,
		North = 0x2,
		East = 0x4,
		South = 0x8
	}
	
	public class BombBlast
	{
		protected Point3D _Location;
		protected Map _Map;
		protected int _DeltaX;
		protected int _DeltaY;
		protected BlastDirection _Direction;
		protected Mobile _Planter;
		
		protected bool _BaddaBoom;
		
		protected BombBag _BombBag;
		
		protected int _Strength;
		
		protected BlastTimer _BlastTimer;
		
		public BombBlast( Point3D location, Map map, BlastDirection direction, BombBag bombbag, Mobile planter, int strength, bool baddaboom )
		{
			_Direction = direction;
			_DeltaX = ( _Direction == BlastDirection.West ? -1 : 0 ) + ( _Direction == BlastDirection.East ? 1 : 0 );
			_DeltaY = ( _Direction == BlastDirection.North ? -1 : 0 ) + ( _Direction == BlastDirection.South ? 1 : 0 );
			
			_Location = new Point3D( location.X + _DeltaX, location.Y + _DeltaY, location.Z );
			_Map = map;
			
			_BombBag = bombbag;
			_Planter = planter;
			
			_Strength = strength;
			_BaddaBoom = baddaboom;
			
			_BlastTimer = new BlastTimer( this );
			_BlastTimer.Start();
			
			//check for any victims of the blast
			if( _BombBag != null && !_BombBag.Deleted )
			{
				_BombBag.ControlItem.CheckForMobileVictims( _Location, _Map, _BombBag );
			}
		}
		
		public void Explode()
		{
			if( _BlastTimer != null )
			{
				_BlastTimer.Stop();
				_BlastTimer = null;
			}
			
			if( _Map == null )
			{
				return;
			}
			
			IPooledEnumerable ie = _Map.GetItemsInRange( _Location, 0 );
			
			bool hitwall = false;
			
			foreach( Item item in ie )
			{
				if( item is BombermanObstacle && !( item is BombermanFloorTile ) )
				{
					
					BombermanObstacle obstacle = (BombermanObstacle)item;
					
					if( obstacle.Destructable )
					{
						obstacle.Destroy();
					}
					else
					{
						hitwall = true;
						_Strength = 0;
					}
					
					//stop the fires here if you don't have a baddaboom bomb
					if( !_BaddaBoom )
					{
						_Strength = 0;
					}
					break;
				}
				
				if( item is Bomb )
				{
					Bomb bomb = (Bomb)item;
					
					//reassign who planted it so that the bomb who originally exploded will get credit for any kills
					bomb.Planter = _BombBag.Owner;
					
					bomb.Explode( ReverseDirection( _Direction ) );
					
					//stop the fires here
					_Strength = 0;
					break;
				}
				
			}
			
			ie.Free();
			
			if( !hitwall )
			{
				RenderBlast();
			}
			
			//check for any victims of the blast
			if( _BombBag != null && !_BombBag.Deleted )
			{
				_BombBag.ControlItem.CheckForMobileVictims( _Location, _Map, _BombBag );
			}
			
			
			if( !hitwall )
			{
				
			}
			
			if( _Strength > 0 )
			{
				
				BombBlast newblast = new BombBlast( _Location, _Map, _Direction, _BombBag, _Planter, _Strength - 1, _BaddaBoom );
			}
		}
		
		protected void RenderBlast()
		{
			Effects.SendLocationEffect( new Point3D( _Location.X + 1, _Location.Y + 1, _Location.Z ), _Map, Utility.RandomList( 0x36CB, 0x36BD, 0x36B0 ), 10 );
		}
		
		//this determines the opposite direction to the specified blast direction
		protected BlastDirection ReverseDirection( BlastDirection direction )
		{
			switch( direction )
			{
				case BlastDirection.West:
				{
					return( BlastDirection.East );
				}
				case BlastDirection.East:
				{
					return( BlastDirection.West );
				}
				case BlastDirection.North:
				{
					return( BlastDirection.South );
				}
				case BlastDirection.South:
				{
					return( BlastDirection.North );
				}
			}
			
			return BlastDirection.None;
		}
		
		protected class BlastTimer : Timer
		{
			private BombBlast _BombBlast;

			public BlastTimer( BombBlast bombblast ) : base( TimeSpan.FromMilliseconds( BombermanSettings.BLAST_DELAY ), TimeSpan.FromSeconds( 1.0 ) )
			{
				_BombBlast = bombblast;
			}

			protected override void OnTick()
			{
				_BombBlast.Explode();
			}
		}
		
		
	}
	
}