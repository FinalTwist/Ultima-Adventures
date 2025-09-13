using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;


namespace Solaris.BoardGames
{
	//a detonator allows the player to select when their bombs explode
	public class BombDetonator : Item
	{
		public bool Active;
		public Mobile Owner;
		
		protected BombBag _BombBag;

		//master constructor
		public BombDetonator( BombBag bag ) : base( 0xFC1 )
		{
			_BombBag = bag;
			Hue = 1161;
			Name = "Detonator";
			
			//locked down in backpack
			Movable = false;
			Active = true;
		}
		
		//deserialize constructor
		public BombDetonator( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( Owner == null )
			{
				Owner = from;
			}
			
			//make sure the person using the detonator is the owner, and that it's in their backpack
			if( from != Owner || !IsChildOf( from.Backpack ) || !Active )
			{
				from.SendMessage( "You cannot use that" );
				return;
			}
			
			if( _BombBag != null )
			{
				_BombBag.DetonateFirstBomb();
			}
		}
		
		public override void OnAfterDelete()
		{
			if( _BombBag != null && !_BombBag.Deleted )
			{
				_BombBag.Delete();
			}
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );

			writer.Write( (Item)_BombBag );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();

			_BombBag = (BombBag)reader.ReadItem();
		}
	}
	
}