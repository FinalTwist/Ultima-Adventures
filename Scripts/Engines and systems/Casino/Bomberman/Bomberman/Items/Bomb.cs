using System;
using Server;
using Server.Items;

namespace Solaris.BoardGames
{
	//a bomb - has a reference to a bomb candle that sits above it
	public class Bomb : GamePiece
	{
		protected BombCandle _Candle;
		protected DetonatorReceiver _Detonator;
		protected BaddaBoom _BaddaBoom;
		
		public BombBag BombBag;
		public Mobile Planter;
		
		protected FuseTimer _FuseTimer;
		protected int _Strength;
		
		//master constructor
		public Bomb( BombBag bombbag ) : base( 0x2256, bombbag == null ? "Bomb" : bombbag.Owner.Name + "'s Bomb" )
		{
			Hue = 1;
			
			//link this bomb up to the bomb bag it came from
			BombBag = bombbag;
			Planter = BombBag.Owner;
			
			_Strength = BombBag.BombStrength;
			
			
			
			if( BombBag.BaddaBoom )
			{
				_BaddaBoom = new BaddaBoom( this );
			}
			else
			{
				_Candle = new BombCandle( this );
			}
			
			if( BombBag.Detonator != null )
			{
				_Detonator = new DetonatorReceiver( this );
			}
			else
			{
				StartFuse();
			}
		}
		
		//deserialize constructor
		public Bomb( Serial serial ) : base( serial )
		{
		}
		
		//start the timer for the explosion
		public void StartFuse()
		{
			_FuseTimer = new FuseTimer( this );
			_FuseTimer.Start();
		}
		
		public void Explode()
		{
			Explode( BlastDirection.None );
		}
		
		public void Explode( BlastDirection inhibitdirection )
		{

			if( _FuseTimer != null )
			{
				_FuseTimer.Stop();
				_FuseTimer = null;
			}
			
			if( BombBag != null )
			{
				BombBag.Bombs.Remove( this );
			}

			//sound effect of explosion
			Effects.PlaySound( Location, Map, Utility.RandomList( 0x11B, 0x305, 0x306, 0x307, 0x11C, 0x308, 0x11D, 0x309, 0x4CF, 0x11E, 0x207  ) );
			
			//bomb explosion graphics effect: 0x36CB
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z ), Map, 0x36CB, 10 );
			
			//set off fire blowout
			foreach( int blastdirection in Enum.GetValues( typeof( BlastDirection ) ) )
			{
				BlastDirection curdirection = (BlastDirection)blastdirection;
				
				if( curdirection != BlastDirection.None && curdirection != inhibitdirection )
				{
					BombBlast blast = new BombBlast( Location, Map, curdirection, BombBag, Planter, _Strength - 1, _BaddaBoom != null );
				}
				
			}
			//check for damagable at spot
			if( BombBag != null && !BombBag.Deleted )
			{
				BombBag.ControlItem.CheckForMobileVictims( Location, Map, BombBag );
			}
			
			Delete();
		}
		
		
		public override void OnLocationChange( Point3D old )
		{
			if ( _Candle != null )
			{
				_Candle.Location = new Point3D( X, Y, Z + 3 );
			}
			
			if ( _Detonator != null )
			{
				_Detonator.Location = new Point3D( X, Y, Z + 2 );
			}
			
			if( _BaddaBoom != null )
			{
				_BaddaBoom.Location = new Point3D( X, Y, Z + 2 );
			}
		}

		public override void OnMapChange()
		{
			if ( _Candle != null )
			{
				_Candle.Map = Map;
			}

			if ( _Detonator != null )
			{
				_Detonator.Map = Map;
			}
			
			if( _BaddaBoom != null )
			{
				_BaddaBoom.Map = Map;
			}
		}
		
		
		public override void Delete()
		{
			//stop the fuse first, before beginning the delete process!
			if( _FuseTimer != null )
			{
				_FuseTimer.Stop();
				_FuseTimer = null;
			}
			
			base.Delete();
		}
		
		public override void OnAfterDelete()
		{

			if( _Candle != null && !_Candle.Deleted )
			{
				_Candle.Delete();
			}
			
			if( _Detonator != null && !_Detonator.Deleted )
			{
				_Detonator.Delete();
			}
			
			if( _BaddaBoom != null && !_BaddaBoom.Deleted )
			{
				_BaddaBoom.Delete();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );

			writer.Write( (Item)BombBag );
			
			writer.Write( _Strength );
			writer.Write( (Item)_Candle );
			
			writer.Write( (Item)_Detonator );
			
			writer.Write( (Item)_BaddaBoom );
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			BombBag = (BombBag)reader.ReadItem();
			
			_Strength = reader.ReadInt();
			_Candle = (BombCandle)reader.ReadItem();
			_Detonator = (DetonatorReceiver)reader.ReadItem();

			_BaddaBoom = (BaddaBoom)reader.ReadItem();
			
			
			if( _Detonator == null )
			{
				StartFuse();
			}

		}
		
		
		protected class FuseTimer : Timer
		{
			private Bomb _Bomb;

			public FuseTimer( Bomb bomb ) : base( TimeSpan.FromSeconds( BombermanSettings.EXPLODE_DELAY ), TimeSpan.FromSeconds( 1.0 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;
				_Bomb = bomb;
			}

			protected override void OnTick()
			{
				if( !_Bomb.Deleted && _Bomb.Map != null )
				{
					_Bomb.Explode();
				}
			}
		}

		
	}
	
	public class BombCandle : GamePiece
	{
		Bomb _Bomb;
		public BombCandle( Bomb bomb ) : base( 0x1430, bomb.Name )
		{
			Hue = 1;
			
			_Bomb = bomb;
			
		}
		
		public BombCandle( Serial serial ) : base( serial )
		{
		}
		
		public override void OnAfterDelete()
		{
			if ( _Bomb != null && !_Bomb.Deleted )
			{
				_Bomb.Delete();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			writer.Write( (Item)_Bomb );

		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			_Bomb = (Bomb)reader.ReadItem();
		}
	}
	
	public class DetonatorReceiver : GamePiece
	{
		Bomb _Bomb;
		public DetonatorReceiver( Bomb bomb ) : base( 0xF13, bomb.Name )
		{
			_Bomb = bomb;
			
		}
		
		public DetonatorReceiver( Serial serial ) : base( serial )
		{
		}
		
		public override void OnAfterDelete()
		{
			if ( _Bomb != null && !_Bomb.Deleted )
			{
				_Bomb.Delete();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			writer.Write( (Item)_Bomb );

		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			_Bomb = (Bomb)reader.ReadItem();
		}
	}	
	
	//this bomb upgrade makes the bomb blasts tear thru breakable obstacles
	public class BaddaBoom : GamePiece
	{
		Bomb _Bomb;
		
		public BaddaBoom( Bomb bomb ) : base( 0x1858, "If you can read this, you are probably going to die..." )
		{
			Hue = 1161;
			_Bomb = bomb;
			
		}
		
		public BaddaBoom( Serial serial ) : base( serial )
		{
		}
		
		public override void OnAfterDelete()
		{
			if ( _Bomb != null && !_Bomb.Deleted )
			{
				_Bomb.Delete();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			writer.Write( (Item)_Bomb );

		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			_Bomb = (Bomb)reader.ReadItem();
		}
	}
	
}