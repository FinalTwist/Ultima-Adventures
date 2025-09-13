//To enable RunUO 2.0 RC1 compatibility, uncomment the following line
//#define RunUO2RC1


using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;


namespace Solaris.BoardGames
{
	//a bomb - has a reference to a bomb candle that sits above it
	public class BombBag : Item
	{
		protected bool _Active;
		protected Mobile _Owner;
		
		public Mobile Owner
		{
			get{ return _Owner; }
			set
			{
				_Owner = value;
				
				//disable any speed boost from polymorph, so it's fair for all players
				#if RunUO2RC1
					Owner.Send( Server.Network.SpeedBoost.Disabled );
				#else
					Owner.Send( SpeedControl.Disable );
				#endif
				

			}
		}
		
		public int MaxBombs;
		public int BombStrength;
		
		protected bool _SpeedUpgraded;

		public BombDetonator Detonator;
		
		//this indicates if the bombs have unstoppable blasts
		public bool BaddaBoom;
		
		protected List<Bomb> _Bombs;
		
				
		public bool Active
		{
			get{ return _Active; }
			set
			{
				_Active = value;
				
				if( Detonator != null )
				{
					Detonator.Active = value;
				}
			}
		}

		
		public List<Bomb> Bombs
		{
			get
			{
				if( _Bombs == null )
				{
					_Bombs = new List<Bomb>();
				}
				return _Bombs;
			}
		}
		
		public BombermanControlItem ControlItem;
		
		//master constructor
		
		public BombBag( BombermanControlItem controlitem, int maxbombs, int bombstrength ) : base( 0xE76 )
		{
			ControlItem = controlitem;
			
			Hue = 1161;
			Name = "Bomb Bag";
			
			//locked down in backpack
			Movable = false;
			
			MaxBombs = maxbombs;
			BombStrength = bombstrength;
			Active = true;
		}
		
		//deserialize constructor
		public BombBag( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( Owner == null )
			{
				from.SendMessage( "You are now the owner of this bomb bag!" );
				Owner = from;
			}
			
			//make sure the person using the bomb bag is the owner, and that it's in their backpack
			if( from != Owner || !IsChildOf( from.Backpack ) || !Active )
			{
				from.SendMessage( "You cannot use that" );
				return;
			}
			
			if( from.Followers > 0 )
			{
				from.SendMessage( "You can't use this with pets!" );
				return;
			}
			
			//check if there's a bomb at feet already
			if( BombAtFeet( from ) )
			{
				from.SendMessage( "There is a bomb at your feet already!" );
				return;
			}
			
			
			if( Bombs.Count < MaxBombs )
			{
				Bomb newbomb = new Bomb( this );
				
				Owner.PlaySound( 0x42 );
				
				newbomb.MoveToWorld( new Point3D( Owner.X, Owner.Y, Owner.Z ), Owner.Map );
				
				Bombs.Add( newbomb );
				
				from.SendMessage( "Planting bomb!" );
			}
			else
			{
				from.SendMessage( "You have too many bombs on the field." );
			}
			
		}
		
		//boosts the player walking speed
		public void SpeedBoost()
		{
			_SpeedUpgraded = true;
			#if RunUO2RC1
				Owner.Send( Server.Network.SpeedBoost.Enabled );
			#else
				Owner.Send( SpeedControl.MountSpeed );
			#endif
		}
		
		public void DetonateFirstBomb()
		{
			if( Bombs.Count > 0 )
			{
				Bombs[0].Explode();
			}
		}
		
		//TODO: find a better place for this, like in the BombermanControlItem?
		public static bool BombAtFeet( Mobile player )
		{
			IPooledEnumerable ie = player.Map.GetItemsInRange( player.Location, 0 );
			
			bool founditem = false;
			foreach( Item item in ie )
			{
				if( item is Bomb )
				{
					founditem = true;
					break;
				}
				
			}
			ie.Free();
			
			return founditem;
		}
		
		public override void OnAfterDelete()
		{
			foreach( Bomb bomb in Bombs )
			{
				if( bomb != null )
				{
					bomb.Delete();
				}
			}
			
			if( Detonator != null && !Detonator.Deleted )
			{
				Detonator.Delete();
			}
			
			if( _SpeedUpgraded )
			{
				#if RunUO2RC1
					Owner.Send( Server.Network.SpeedBoost.Disabled );
				#else
					Owner.Send( SpeedControl.Disable );
				#endif
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );

			writer.Write( Owner );
			writer.Write( MaxBombs );
			writer.Write( BombStrength );
			
			writer.Write( _SpeedUpgraded );
			
			writer.Write( (Item)Detonator );
			writer.Write( (Item)ControlItem );
			
			writer.Write( Bombs.Count );
			
			foreach( Bomb bomb in Bombs )
			{
				writer.Write( (Item)bomb );	
			}
			
			writer.Write( Active );
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();

			_Owner = reader.ReadMobile();
			MaxBombs = reader.ReadInt();
			BombStrength = reader.ReadInt();
			
			_SpeedUpgraded = reader.ReadBool();
			
			Detonator = (BombDetonator)reader.ReadItem();
			ControlItem = (BombermanControlItem)reader.ReadItem();
			
			int count = reader.ReadInt();
			
			for( int i = 0; i < count; i++ )
			{
				Bombs.Add( (Bomb)reader.ReadItem() );
			}
			
			Active = reader.ReadBool();
		}
	}
	
}