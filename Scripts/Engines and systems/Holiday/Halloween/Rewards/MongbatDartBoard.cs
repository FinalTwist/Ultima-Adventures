using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class MongbatDartBoard : AddonComponent
	{
		public bool East{ get{ return this.ItemID == 0x1953; } }

		[Constructable]
		public MongbatDartBoard() : this( true )
		{
		}

		[Constructable]
		public MongbatDartBoard( bool east ) : base( east ? 0x1953 : 0x1950 )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			Direction dir;
			if ( from.Location != this.Location )
				dir = from.GetDirectionTo( this );
			else if ( this.East )
				dir = Direction.West;
			else
				dir = Direction.North;

			from.Direction = dir;

			bool canThrow = true;

			if ( !from.InRange( this, 4 ) || !from.InLOS( this ) )
				canThrow = false;
			else if ( this.East )
				canThrow = ( dir == Direction.Left || dir == Direction.West || dir == Direction.Up );
			else
				canThrow = ( dir == Direction.Up || dir == Direction.North || dir == Direction.Right );

			if ( canThrow )
				Throw( from );
			else
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
		}

		public void Throw( Mobile from )
		{
			BaseKnife knife = from.Weapon as BaseKnife;

			if ( knife == null )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 500751 ); // Try holding a knife...
				return;
			}

			from.Animate( from.Mounted ? 26 : 9, 7, 1, true, false, 0 );
			from.MovingEffect( this, knife.ItemID, 7, 1, false, false );
			from.PlaySound( 0x238 );

			double rand = Utility.RandomDouble();

			int message;
			int nAnimate = 0;


			if ( rand < 0.05 ){
				nAnimate = 1;
				message = 500752; // BULLSEYE! 50 Points!
			}
			else if ( rand < 0.20 ){
				nAnimate = 1;
				message = 500753; // Just missed the center! 20 points.
			}
			else if ( rand < 0.45 ){
				nAnimate = 1;
				message = 500754; // 10 point shot.
			}
			else if ( rand < 0.70 )
				message = 500755; // 5 pointer.
			else if ( rand < 0.85 )
				message = 500756; // 1 point.  Bad throw.
			else
				message = 500757; // Missed.

			if ( ( nAnimate == 1 ) && ( ( this.ItemID == 0x1953 ) || ( this.ItemID == 0x1954 ) ) ) { ItemID = 0x1954; }
			else if ( ( nAnimate == 1 ) && ( ( this.ItemID == 0x1951 ) || ( this.ItemID == 0x1950 ) ) ) { ItemID = 0x1951; }

			if ( nAnimate == 1 ) { Timer.DelayCall( TimeSpan.FromSeconds( 0.7 ), new TimerCallback( OnMongbatReset ) ); }

			PublicOverheadMessage( MessageType.Regular, 0x3B2, message );
		}

		public virtual void OnMongbatReset()
		{
			if ( ( ItemID == 0x1954 ) || ( ItemID == 0x1953 ) ) { ItemID = 0x1953; }
			else { ItemID = 0x1950; }
		}

		public MongbatDartBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class MongbatDartBoardEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new MongbatDartBoardEastDeed(); } }

		public MongbatDartBoardEastAddon()
		{
			AddComponent( new MongbatDartBoard( true ), 0, 0, 0 );
		}

		public MongbatDartBoardEastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class MongbatDartBoardEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new MongbatDartBoardEastAddon(); } }

		[Constructable]
		public MongbatDartBoardEastDeed()
		{
			Name = "Mongbat Dark Board (east)";
			Hue = 0x96C;
		}

		public MongbatDartBoardEastDeed( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Happy Halloween" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class MongbatDartBoardSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new MongbatDartBoardSouthDeed(); } }

		public MongbatDartBoardSouthAddon()
		{
			AddComponent( new MongbatDartBoard( false ), 0, 0, 0 );
		}

		public MongbatDartBoardSouthAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class MongbatDartBoardSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new MongbatDartBoardSouthAddon(); } }

		[Constructable]
		public MongbatDartBoardSouthDeed()
		{
			Name = "Mongbat Dark Board (south)";
			Hue = 0x96C;
		}

		public MongbatDartBoardSouthDeed( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Happy Halloween" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}