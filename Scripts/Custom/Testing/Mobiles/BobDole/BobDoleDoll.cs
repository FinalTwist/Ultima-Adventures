using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class BobDoleDoll : Item
	{
		[Constructable]
		public BobDoleDoll() : this( 1 )
		{
		}
		
		[Constructable]
		public BobDoleDoll( int amount ) : base( 0x2106 )
		{
			Name = "Bob Dole Doll";
            Weight = 1.0;
			Hue = 602;
		}

		public BobDoleDoll( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new BobDoleDoll( amount ), amount );
		//}
		
		public override void OnDoubleClick( Mobile from )
		
		{ 
			switch ( Utility.Random( 5 ) )
			{
				default:
				case  0: this.PublicOverheadMessage( MessageType.Regular, 602, true,"Bob Dole Is Hungry" ); break;
				case  1: this.PublicOverheadMessage( MessageType.Regular, 602, true,"Bob Dole Likes Youre Style Kid" ); break;	
				case  2: this.PublicOverheadMessage( MessageType.Regular, 602, true,"Bob Dole" ); break;
				case  3: this.PublicOverheadMessage( MessageType.Regular, 602, true,"Bob Dole Is Lonely, George Bush Wont Stop Telling Knock Knock Jokes!" ); break;
				case  4: this.PublicOverheadMessage( MessageType.Regular, 602, true,"Bob Dole Bob Dole Bob Dole bob dole bob... dole.... bob dole.... *Sleeps*" ); break;
			}
		}
	}
}
