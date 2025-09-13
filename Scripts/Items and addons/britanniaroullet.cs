//Created by Doin Icemallet and Esperologist//
using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class BritanniaRoulette : Item
	{
		[Constructable]
		public BritanniaRoulette() : base( 0x171F )
		{
			Weight = 1.0;
			Movable = false;
			Name = "Britannia Roulette";
			Hue = 0x902;
		}

		public BritanniaRoulette( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
				return;

			int firstOne = Utility.Random( 1, 6 );
			int secondOne = Utility.Random( 1, 6 );

			if ((firstOne + secondOne) <= 5) {
				this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "{0} is dead!", from.Name, firstOne, secondOne) );
				from.BoltEffect( 0 );
				from.Kill();
			} else {
				this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "{0} survives!", from.Name, firstOne, secondOne) );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}