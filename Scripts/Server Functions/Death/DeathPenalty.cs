using Server;
using Server.Mobiles;
using Server.Items;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Server.Misc
{
    class Death
    {
		public static void Penalty( Mobile from, bool allPenalty )
        { 
			Penalty(from, allPenalty, false);
		}

		public static void Penalty( Mobile from, bool allPenalty, bool ankh )
        {
			if ( !(from is PlayerMobile) || from == null)
				return;

				if ( ((PlayerMobile)from).SoulBound )
				{
					((PlayerMobile)from).ResetPlayer( from );
					return;
				}

				if (AdventuresFunctions.IsPuritain(from))
					return;

				double val1 = 0.05; // karma loss
				
				if ( !((PlayerMobile)from).Avatar )
					val1 *= 2; // normal mode players now 10%
				
				double val2 = 0; // skill loss

				double val3 = 0.05; // balance effect 
				
				int karma = Math.Abs(from.Karma);
				if ( karma < 1000)
					karma = 1000; // sets minimum value for balance effect

				if (from.Karma >= 0) // how much skill is lost is based on the balance for unrestricted players
					val2 = ( (100 - ( ( ((double)AetherGlobe.BalanceLevel / 100000.0) * ( ((double)karma / 15000) ) ) / 1.5)  ) / 100 ) ; // ranges from .9947 to .999999
				else 
					val2 = ( (100 - ( ( ((double)(100000-AetherGlobe.BalanceLevel) / 100000.0) * ( ((double)karma / 15000) ) )  / 1.5) ) / 100 ) ;
				
				if (val2 >= 0.999) // sanity check to make sure skills are not INCREASED if above goes wrong and to set minimum 0.1 skill loss
					val2 = 0.999;

				if ( allPenalty ) // they either insta-ressed or don't have gold, etc
				{
					if ( !((PlayerMobile)from).Avatar )
						val1 *= 4; // max fame/karma is now 40% for them
					else 
						val1 *= 2;// max fame/karma is now 10% for them
					
					val2 = 1-((1-val2)*3); // skill loss changed to max .9847 (1.5% loss)

					val3 *= 3; // max 15%
				}
				else if ( !((PlayerMobile)from).Avatar )
					val1 *= 2; // penalty for easymode players just doubles here to 20%
					
				if ( (from.RawStr + from.RawDex + from.RawInt) < 125  )
				{
					val2 = 1.0; // no skill loss
					val1 /= 2; // half fame/karma loss
				}
				
				if (ankh )
				{
					val1 *= 2; // fame/karma can now range from 10% to 80% loss, ankh doubles it because less/no gold
				}	

				if (val1 < 0.05) // sanity check
					val1 = 0.05;
				
				if( from.Fame > 0 ) // FAME LOSS
				{
					int amount = (int)((double)from.Fame * val1);
					if ( from.Fame - amount < 0 ){ amount = from.Fame; }
					if ( from.Fame < 1 ){ from.Fame = 0; }
					Misc.Titles.AwardFame( from, -amount, true );
				}

					int amounts = (int)((double)from.Karma * val1);
					if ( from.Karma > 0 && (from.Karma - amounts) < 0 ){ amounts = from.Karma; }
					else if ( from.Karma < 0 && (from.Karma - amounts) > 0 ){ amounts = from.Karma; }
					if ( from.Karma -1 == 0 || from.Karma +1 == 0){ from.Karma = 0; }
					Misc.Titles.AwardKarma( from, -amounts, true );


				if (  AetherGlobe.EvilChamp == from || AetherGlobe.GoodChamp == from || (from.RawStr + from.RawDex + from.RawInt) < 125 || !((PlayerMobile)from).Avatar)
				{
					if ((from.RawStr + from.RawDex + from.RawInt) < 125 && ((PlayerMobile)from).Avatar && (AetherGlobe.EvilChamp != from || AetherGlobe.GoodChamp != from) )
						from.SendMessage( "You would have lost skills here were you not so weak." );
					else if ( (AetherGlobe.EvilChamp == from || AetherGlobe.GoodChamp == from ) && ((PlayerMobile)from).Avatar )
					{
						from.SendMessage( "Your influence on the balance overcomes the perils of death!" );
						double lost = (double)((PlayerMobile)from).BalanceEffect * val3;
						((PlayerMobile)from).BalanceEffect -= (int)lost;
						from.SendMessage( "But your influence was reduced by " + (int)lost + " from your untimely demise." );
					}
					else if ( !((PlayerMobile)from).Avatar)
						from.SendMessage( "You avoid any serious penalty for your death since your actions do not influence the balance." );
					return;
				}

				double loss = val2;

				if( from.RawStr * loss > 10 )
				{
					if (allPenalty)
						from.RawStr = (int)(from.RawStr * loss);
					else if ( Utility.RandomDouble() < 0.33 )
						from.RawStr = (int)(from.RawStr * loss);
				}
				if ( from.RawStr < 10 ){ from.RawStr = 10; }
				
				if( from.RawInt * loss > 10 )
				{
					if (allPenalty)
						from.RawInt = (int)(from.RawInt * loss);
					else if ( Utility.RandomDouble() < 0.33 )
						from.RawInt = (int)(from.RawInt * loss);
				}					
				if ( from.RawInt < 10 ){ from.RawInt = 10; }
			
				if( from.RawDex * loss > 10 )
				{
					if (allPenalty)
						from.RawDex = (int)(from.RawDex * loss);
					else if ( Utility.RandomDouble() < 0.33 )				
						from.RawDex = (int)(from.RawDex * loss);
				}					
				if ( from.RawDex < 10 ){ from.RawDex = 10; }
						
				if ( allPenalty  )
				{
					for( int s = 0; s < from.Skills.Length; s++ )
					{
						if( Utility.RandomDouble() > 0.35 && (from.Skills[s].Base * loss) > 35 )
							from.Skills[s].Base *= loss;
	
					}	
					from.SendMessage( "Your body revives... but much weaker. " );
				}
				else 
				{
					for( int s = 0; s < from.Skills.Length; s++ )
					{
						if (Utility.RandomDouble() > 0.65)
						{
							if (from.Karma >= 0 && (from.Skills[s].Base * loss > 35) && (Utility.RandomDouble() < (((double)AetherGlobe.BalanceLevel / 200000.0)) * ( 1+ ((double)from.Karma / 15000)))) 
								from.Skills[s].Base *= loss;
							else if ( (from.Skills[s].Base * loss > 35) && (Utility.RandomDouble() < (((100000-(double)AetherGlobe.BalanceLevel) / 200000.0)) * ( 1+ ((double)Math.Abs(from.Karma) / 15000))))
								from.Skills[s].Base *= loss;
						}
					}	
					from.SendMessage( "Your body revives... but a little weaker. " );
				}
						
			
		}
	}
}