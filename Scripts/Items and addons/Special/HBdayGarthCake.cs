/*
 * Created by GreyWolf79
 * Created on 06/27/2008
 * Created for Garthavan's 30th Birthday
 *      - 08/15/2020
*/

using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class BdayCake : Food
	{

		[Constructable]
		public BdayCake() : this( 1 )
		{	
		}
		
		[Constructable]
		public BdayCake( int amount ) : base( 0xF8F, amount )
		{
			this.Hue = 2634;
			this.Name = "A Happy 30th Birthday to Garthavan cake";
			this.Movable = true;
			this.ItemID = 2537;
			this.Amount = amount;
			this.FillFactor = 20;
			this.Weight = 0;
			this.Stackable = true;
		}

		public BdayCake( Serial serial ) : base( serial )
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
	}
}