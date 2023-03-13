using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class LargeFireworksStand : Item
	{
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}


		[Constructable]
		public LargeFireworksStand() : base( 0x24BE )
		{
			Name = "Large Fireworksstand";
			Hue = 1355;
			m_Charges = 20;
		}

		public LargeFireworksStand( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Charges < 1 )
			{
			from.SendMessage("You try to light the Fireworksstand again, but all you manage is to burn it down completely!");
			this.Delete();
			return;
			}
			else if ( this.Parent is Container )
			{
			from.SendMessage("You can't light fireworks if they are not placed on the ground in the open!");
			return;
			}
			else if ( this.Hue == 1161 )
			{
				if( from.InRange( this.GetWorldLocation(), 1 ) )
				{
				from.SendMessage("This fireworksstand is allready burning!");
				return;
				}
				else
				{
				from.SendMessage("That is too far away!");
				return;
				}
			}
			else
			{
				if( from.InRange( this.GetWorldLocation(), 1 ) )
				{
				this.Movable = false;
				this.Hue = 1161;
				this.Name = "Burning Fireworksstand";
				this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*{0} lights the Fireworksstand*", from.Name ) );
				Timer.DelayCall( TimeSpan.FromSeconds( 0.1 ), new TimerStateCallback( BeginLaunch ), new object[]{ from } );
				}
				else
				{
				from.SendMessage("That is too far away!");
				return;
				}
			}
		}

		public void BeginLaunch( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Map map = from.Map;

			if ( map == null || map == Map.Internal || from == null )
				return;

			if ( this.Deleted )
			{
			return;
			}

				if ( Charges > 0 )
				{
					--Charges;
				}
				else
				{
					this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "*The Fireworksstand is burned down*" ) );
					this.Movable = true;
					this.Hue = 1175;
					this.Name = "Burned out Fireworksstand";
					return;
				}

			Point3D ourLoc = GetWorldLocation();

			Point3D startLoc = new Point3D( ourLoc.X, ourLoc.Y, ourLoc.Z + 10 );
			Point3D endLoc = new Point3D( startLoc.X + Utility.RandomMinMax( -2, 2 ), startLoc.Y + Utility.RandomMinMax( -2, 2 ), startLoc.Z + 32 );

			Effects.SendMovingEffect( new Entity( Serial.Zero, startLoc, map ), new Entity( Serial.Zero, endLoc, map ),
				0x36E4, 5, 0, false, false );

			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( FinishLaunch ), new object[]{ from, endLoc, map } );
		}

		private void FinishLaunch( object state )
		{
			object[] states = (object[])state;

			Mobile from = (Mobile)states[0];
			Point3D endLoc = (Point3D)states[1];
			Map map = (Map)states[2];

			int hue = Utility.Random( 40 );

			if ( hue < 8 )
				hue = 0x66D;
			else if ( hue < 10 )
				hue = 0x482;
			else if ( hue < 12 )
				hue = 0x47E;
			else if ( hue < 16 )
				hue = 0x480;
			else if ( hue < 20 )
				hue = 0x47F;
			else
				hue = 0;

			if ( Utility.RandomBool() )
				hue = Utility.RandomList( 0x47E, 0x47F, 0x480, 0x482, 0x66D );

			int renderMode = Utility.RandomList( 0, 2, 3, 4, 5, 7 );

			Effects.PlaySound( endLoc, map, Utility.Random( 0x11B, 4 ) );
			Effects.SendLocationEffect( endLoc, map, 0x373A + (0x10 * Utility.Random( 4 )), 16, 10, hue, renderMode );
			Timer.DelayCall( TimeSpan.FromSeconds( 4.0 ), new TimerStateCallback( BeginLaunch ), new object[]{ from } );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Charges = reader.ReadInt();
					break;
				}
			}
		}
	}
}