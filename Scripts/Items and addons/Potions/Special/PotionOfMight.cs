using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;


namespace Server.Items
{
	public class PotionOfMight : Item
	{
		[Constructable]
		public PotionOfMight() : this( 1 )
		{
		}


		[Constructable]
		public PotionOfMight( int amount ) : base( 0x2827 )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Hue = 0xB9E;
			Name = "potion of might";
		}


		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( from.StatCap > ( from.RawStatTotal ) )
			{
				int up = 1;


				int chance = (int)( Utility.RandomMinMax( 1, 100 ) + ( Server.Items.BasePotion.EnhancePotions( from ) / 2 ) );


				if ( chance >= 98 ){ up = AvailPoints( from, 5 ); }
				else if ( chance >= 87 ){ up = AvailPoints( from, 4 ); }
				else if ( chance >= 75 ){ up = AvailPoints( from, 3 ); }
				else if ( chance >= 50 ){ up = AvailPoints( from, 2 ); }


				from.RawStr = from.RawStr + up;
				from.SendMessage( "This potions makes you feel stronger!" );
				Server.Items.BasePotion.PlayDrinkEffect( from );
				this.Consume();
			}
			else
			{
				from.SendMessage( "This potion would have no effect on you." );
			}
		}


		public static int AvailPoints( Mobile from, int val )
		{
			int points = from.StatCap - ( from.RawStr + from.RawInt + from.RawDex );


			if ( val > points ){ val = points; }


			return val;
		}


		public PotionOfMight( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
