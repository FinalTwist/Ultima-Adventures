using System;
using Server;

namespace Server.Items
{
	public class InvestmentBook : Item
	{

		[Constructable]
		public InvestmentBook( ) : base( 0xFF2 )
		{
		}
/*
		public override void OnDoubleClick( Mobile from )		
		{


			AetherGlobe.oldbalancelevel = 0;
			AetherGlobe.BalanceLevel = 0;

		}
		
		public override void OnSingleClick( Mobile from )		
		{
			AetherGlobe.UpdateRateOfReturn();
			AetherGlobe.ChangeCurse( 0 );
		}
		*/
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
		
			list.Add( "Doomcurse is " + AetherGlobe.BalanceLevel ); 
			list.Add( "rateofchange is " + AetherGlobe.rateofchange ); 		
			list.Add( "rateofreturn is " + AetherGlobe.rateofreturn ); 				
			list.Add( "changeint is " + AetherGlobe.changeint ); 
			list.Add( "oldbalancelevel is " + AetherGlobe.oldbalancelevel ); 			
			list.Add( "delta1 is " + AetherGlobe.delta1 ); 
			list.Add( "delta2 is " + AetherGlobe.delta2 );	
			list.Add( "delta3 is " + AetherGlobe.delta3 );	
			list.Add( "multiplier is " + AetherGlobe.Multiplier );			

		}


		public InvestmentBook( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}
}
