//FinalTwist, 2022-2023
using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
    public class AetherGlobe : Item
    {
		public static int BalanceLevel; // This is a global property of the game
		public static List<AetherGlobe> WorldGlobes = new List<AetherGlobe>();

		public static SortedDictionary<int, string> ChaosAvatars = new SortedDictionary<int, string>(
       		new ReverseComparer<int>(Comparer<int>.Default));
		public static SortedDictionary<int, string> OrderAvatars = new SortedDictionary<int, string>(
       		new ReverseComparer<int>(Comparer<int>.Default));

		public sealed class ReverseComparer<T> : IComparer<T>
		{
			private readonly IComparer<T> original;

			public ReverseComparer(IComparer<T> original)
			{
				// TODO: Validation
				this.original = original;
			}

			public int Compare(T left, T right)
			{
				return original.Compare(right, left);
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public static int Multiplier { get; set; }
		public static int DoubleMultiplier { get; set; }

		private static DateTime lastchanged;
		public static double delta1;
		public static double delta2;
		public static double delta3;
		public static double rateofchange;
		public static int changeint;
		public static int VendorCurse;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public static int oldbalancelevel { get; set; }

		public static bool killbonus = false;
		public static double rateofreturn;
		public static Mobile EvilChamp;
		public static Mobile GoodChamp;

		// infected invasion variables
		[CommandProperty(AccessLevel.GameMaster)]
		public static double intensity { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public static int invasionstage { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public static String carrier { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public static String general { get; set; }

		[CommandProperty( AccessLevel.GameMaster )]
		public int BalanceLevelLevel
		{
			get
			{
			    return BalanceLevel; 
			}
			set
			{ 
			    BalanceLevel = value; 
			    AetherGlobe.UpdateColor(); 
			    InvalidateProperties(); 
			}
		}

		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		[Constructable]
		public AetherGlobe () : base( 0x115F ) //115f shows issue with hover over not showing
		{
			Movable = false;
			Name = "AEther Globe";
			Visible = true;
			AetherGlobe.BalanceLevel = 0;
			Multiplier = 0;
			DoubleMultiplier = 0;
			lastchanged = DateTime.UtcNow;
			oldbalancelevel = 0;
			rateofchange = 0;
			delta1 = 0;
			delta2 = 0;
			delta3 = 0;
			rateofreturn = 0;
			EvilChamp = null;
			GoodChamp = null;
			VendorCurse = BalanceLevel;

			carrier = null;
			general = null;
			invasionstage = 0;
			intensity = 0;

			WorldGlobes.Add( this );
			AetherGlobe.UpdateColor();
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
		
			if (BalanceLevel > 90000)
				list.Add( "Chaos has taken over the lands" ); 
			else if (BalanceLevel > 55000)
				list.Add( "Chaos is winning" ); 
			else if (BalanceLevel > 45000)
				list.Add( "The balance is levelled equally" ); 
			else if (BalanceLevel > 20000)
				list.Add( "Order is winning" ); 	
			else 
				list.Add( "Order is everpresent in the lands" );
			
			int evilpts = BalanceLevel - 50000;
			int goodpts = 50000 - BalanceLevel;

			if (evilpts > 0)
				list.Add("The balance is leaning towards chaos by " + evilpts + " points.");
			else
				list.Add("The balance is leaning towards order by " + goodpts + " points.");

			if (AetherGlobe.BalanceLevel > 51000) //balance is evil
			{
				int bal = (int)( ( ( ((double)AetherGlobe.BalanceLevel - 50000) / 50000) * 0.17) * 100);
				list.Add( "Evil Avatars will do " + bal + "% additional damage." );
			}
			else if (AetherGlobe.BalanceLevel < 49000) //balance is good
			{
				int balgood = (int)( ( ( (double)( 50000 - AetherGlobe.BalanceLevel ) / 50000) * 0.17) * 100);
				list.Add( "Good Avatars will do " + balgood + "% additional damage." );
			}
			else
				list.Add( "The balance is neutral." );

			if (OneRing.wearer != null)
				list.Add(OneRing.wearer.Name + " controls the power of the One Ring.");

			list.Add("Pledged Avatars can double click to see their standing.");

			list.Add("Balance load: " + Core.AverageCPS.ToString("N2"));

			/*if (Multiplier != 0)
			{
				int percent = Math.Abs(Multiplier) * 10;
				if (Multiplier > 0)
					list.Add( "The forces of evil have a " + percent + "% advantage to Balance Influence." );
				if (Multiplier < 0)
					list.Add( "The forces of good have a " + percent + "% advantage to Balance Influence." );
			}*/

		}
		
		public override void OnDoubleClick( Mobile from )		
		{

/*
			if (from is PlayerMobile && from != null)
			{
				if (((PlayerMobile)from).BalanceEffect == 0)
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You are neutral to the balance."  ) );

				else if (((PlayerMobile)from).BalanceEffect > 0)
				{
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You've tipped the balance towards Evil by " + ((PlayerMobile)from).BalanceEffect + " points."  ) );
					if ( AetherGlobe.EvilChamp == from)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Your ruthless aura casts a shadow over these lands."  ) );
					else if (AetherGlobe.EvilChamp != null)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You feel " + AetherGlobe.EvilChamp.Name + " " + AetherGlobe.EvilChamp.Title + " casts a shadow over you." ) );
				}
				else if (((PlayerMobile)from).BalanceEffect < 0)
				{
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You've tipped the balance towards Good by " + Math.Abs(((PlayerMobile)from).BalanceEffect) + " points."  ) );
					if ( AetherGlobe.GoodChamp == from)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You are leading the charge against evil and corruption!"  ) );
					else if (AetherGlobe.GoodChamp != null)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Your champion is " + AetherGlobe.GoodChamp.Name + " " + AetherGlobe.GoodChamp.Title  ) );
				}
					
			}*/

			bool order = true;

				if (from is PlayerMobile && ((PlayerMobile)from).BalanceStatus == 0)
				{
					from.SendMessage("You must be pledged to Order or Chaos to see the leaderboard.  Seek the Time Lord.");
					return;
				}
				if (from is PlayerMobile && ((PlayerMobile)from).BalanceStatus < 0)	
					order = false;

				from.CloseGump(typeof(BalanceRatings));
				from.SendGump(new BalanceRatings(order));

		}

		public AetherGlobe(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{			

			base.Serialize( writer );
			writer.Write( (int) 4 ); // version
			
			writer.Write( (int) AetherGlobe.BalanceLevel);
			writer.Write( (int) Multiplier);
			writer.Write( (DateTime)lastchanged );
			writer.Write( (double) AetherGlobe.delta1 );
			writer.Write( (double) AetherGlobe.delta2 );
			writer.Write( (double) AetherGlobe.delta3 );
			writer.Write( (double) AetherGlobe.rateofchange );
			writer.Write( (int) AetherGlobe.oldbalancelevel );
			writer.Write( (int) AetherGlobe.changeint );
			
			writer.Write( (double) AetherGlobe.rateofreturn );
			writer.Write( (Mobile) AetherGlobe.EvilChamp );
			writer.Write( (Mobile) AetherGlobe.GoodChamp );

			writer.Write( (int) invasionstage );
			writer.Write( (double) intensity );
			writer.Write( (String) carrier );
			writer.Write( (String) general );
			writer.Write( (int) DoubleMultiplier );

		}

		public override void Deserialize( GenericReader reader )
		{
						
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			AetherGlobe.BalanceLevel = reader.ReadInt();
			AetherGlobe.Multiplier = reader.ReadInt();
			lastchanged = reader.ReadDateTime();
			AetherGlobe.delta1 = reader.ReadDouble();
			AetherGlobe.delta2 = reader.ReadDouble();	
			AetherGlobe.delta3 = reader.ReadDouble();
			AetherGlobe.rateofchange = reader.ReadDouble();	
			AetherGlobe.oldbalancelevel = reader.ReadInt();		
			AetherGlobe.changeint = reader.ReadInt();
			
			rateofreturn = reader.ReadDouble();
			EvilChamp = reader.ReadMobile();
			GoodChamp = reader.ReadMobile();

			if (!WorldGlobes.Contains( this ))
			    WorldGlobes.Add( this );

			AetherGlobe.UpdateColor();

			if (version >= 2)
			{
				invasionstage = reader.ReadInt();
				intensity = reader.ReadDouble();
				carrier = reader.ReadString();
			}
			if (version >= 3)
			{
				general = reader.ReadString();
			}
			if (version >= 4)
			{
				DoubleMultiplier = reader.ReadInt();
			}
		}

		public override void OnAfterSpawn()
		{
			if (!WorldGlobes.Contains( this ))
			    WorldGlobes.Add( this );

			base.OnAfterSpawn();
		}

		public override void OnDelete()
		{
			if (WorldGlobes.Contains( this ))
			    WorldGlobes.Remove( this );

			base.OnDelete();
		}


		public static void UpdateColor() // updates color of all aetherglobes based on AetherGlobe.BalanceLevel level
		{
			foreach( AetherGlobe globe in WorldGlobes )
			{
				//maximum blue
			    if (AetherGlobe.BalanceLevel <= 10000) 
				globe.Hue = 1195; 
			    else if (AetherGlobe.BalanceLevel <= 20000)
				globe.Hue = 93;				
			    else if (AetherGlobe.BalanceLevel <= 30000)
				globe.Hue = 94;	
			    else if (AetherGlobe.BalanceLevel <= 40000)
				globe.Hue = 95;	
			    else if (AetherGlobe.BalanceLevel <= 45000)
				globe.Hue = 96;	

				//45-55 is neutral
				else if (AetherGlobe.BalanceLevel <= 55000)
				globe.Hue = 1487;	


				//trending towards red
			    else if (AetherGlobe.BalanceLevel <= 60000)
				globe.Hue = 148;	
			    else if (AetherGlobe.BalanceLevel <= 70000)
				globe.Hue = 144;	
			    else if (AetherGlobe.BalanceLevel <= 80000)
				globe.Hue = 139;	
			    else if (AetherGlobe.BalanceLevel <= 90000)
				globe.Hue = 138;			
			    else if (AetherGlobe.BalanceLevel <= 100000) // red
				globe.Hue = 1793;		

			    globe.InvalidateProperties();
			}
		}

		public static void ApplyCurse(Mobile m, Map frommap, Map tomap, int type)
		{

			if ( !(m is PlayerMobile) || m == null )
				return;

			if (((PlayerMobile)m).sbmaster)
				return;
				
			double cursechance;
			int balance = 0;
			
			if ( m.Karma >=0)
				balance = AetherGlobe.BalanceLevel;
			else if ( m.Karma < 0 )
				balance = 100000 - AetherGlobe.BalanceLevel;
			
			if ( ((double)balance / 125000.0) <= ((double)MyServerSettings.curseincrease() / 100000) )
				balance = MyServerSettings.curseincrease();

			cursechance = (( (double)balance / 100000 ) * ( (double)m.Karma / 15000)) /1.5   ;

			if (frommap != tomap) // recalling/gating to another facet is dangerous
			cursechance *= 5;

			if (type == 1) // recall
			cursechance *= 0.90;

			if (type == 2) // gate 
			cursechance /= 2;

			if (type == 3) // chivalry sacred journey
			cursechance *= 0.75;

			cursechance *= (1+ Utility.RandomDouble()); // randomize it a bit

			if (cursechance <= 0)
			cursechance = 0;

			if (cursechance >= 0.91)
			cursechance = 0.91;

			if (Utility.RandomDouble() <= cursechance) // check if curse is applied
			{
				
				
				if ( AetherGlobe.GoodChamp == m || AetherGlobe.EvilChamp == m )
					return;
				
					double loosing = 0;
					
					if (m.Karma >= 0)
						loosing = ( ( (double)AetherGlobe.BalanceLevel / 200000.0 ) * ( ((double)m.Karma / 15000) ) ) ;
					else 
						loosing = ( ( (double)(100000-AetherGlobe.BalanceLevel) / 200000.0) * ( ((double)Math.Abs(m.Karma) / 15000) ) ) ;
	
					if ( m.Fame > 0 ) // FAME LOSS
					{
						int amount = (int)((double)m.Fame * loosing );
						if ( m.Fame - amount < 0 ){ amount = m.Fame; }
						if ( m.Fame < 1 ){ m.Fame = 0; }
						Misc.Titles.AwardFame( m, -amount, true );
					}

					//karma loss
						int amounts = (int)((double)m.Karma * loosing);
						if ( m.Karma - amounts < 0 ){ amounts = m.Karma; }
						if ( m.Karma +1 == 0 || m.Karma -1 == 0){ m.Karma = 0; }
						Misc.Titles.AwardKarma( m, -amounts, true );
										
				
					if ( !(((PlayerMobile)m).Avatar) )
					{
						m.SendMessage( "You reappear, but shaken." );
						return;
					}				
				
				if (Utility.RandomBool())
					m.SendMessage( "Crossing the voids of the worlds has weakened you...  " );					
				else
					m.SendMessage( "The Aether's curse corrupts your being...  " );	

				for( int s = 0; s < m.Skills.Length; s++ )
				{
					if ( Utility.RandomDouble() <= (cursechance ) &&  ( m.Skills[s].Base * ( (100 - (cursechance/0.5) ) / 100) )  > 35 )
						m.Skills[s].Base *= ( (100 - (cursechance/0.5) ) / 100);
				}
			}
		}
		
		public static void QuestEffect( int gold, Mobile from, bool Good)
		{
			if (from == null || gold == null || gold == 0 || !(from is PlayerMobile))
				return;
			
			PlayerMobile pm = (PlayerMobile)from;
			
			if ( !pm.Avatar )
				return;
			
			double effect = (double)gold / 1000;
			
			if ( pm.BalanceStatus == 0 )
				effect /= 2;

			if ( !pm.SoulBound )
				effect *= 0.75;
			
			if (effect <1 && Utility.RandomDouble()> 0.66) // for smaller gold rewards, 33% chance of 1 point 
				effect = 1;
			else if (effect <1)
				return;
			
			if (Good)
				effect = -(effect); // good quests reduce the balance		
							
			from.SendMessage("You have done a quest and have had " + ((int)effect).ToString() + " effect on the balance." );

			AetherGlobe.ChangeCurse( (int)effect);
			pm.BalanceEffect += (int)effect;
			
		}
				
				
		public static void ChangeCurse( int change ) // will be called on a daily basis in taskmanager
		{

			if (change == 0 ) // change==0 means it is the daily task manager (running every 24 hours)
			{
				if ( DateTime.UtcNow > ( lastchanged + TimeSpan.FromHours( 23.0 )) ) 
				{		
					if (AetherGlobe.BalanceLevel <= 0) // balancelevel can be 0, divide by 0 check
						AetherGlobe.BalanceLevel = 1;
					
					if (AetherGlobe.BalanceLevel > 100000)
							AetherGlobe.BalanceLevel = 99000;
					
					AetherGlobe.UpdateRateOfReturn(); // updating the rate of return before making changes
					
					if (AetherGlobe.oldbalancelevel == 0 && AetherGlobe.BalanceLevel == 1 && AetherGlobe.rateofchange == 0) //system starting out / first time being run
					{
						AetherGlobe.BalanceLevel += Utility.RandomMinMax( (MyServerSettings.curseincrease() / 2), MyServerSettings.curseincrease() );						
						AetherGlobe.rateofchange = AetherGlobe.BalanceLevel;
						AetherGlobe.oldbalancelevel = AetherGlobe.BalanceLevel;
					}
					else 
					{
						
						rateofchange = 0; //resets rateofchange from previous calculation.
						
						if (DoubleMultiplier < 0 ) // shouldn't happen
							DoubleMultiplier = 0;
							
						double olddelta = (delta1 + delta2 + delta3) / 3; // claculate average previous 3 days of rate of changes
						int oldchangeint = AetherGlobe.changeint; // set previous amount the curse went up/down by
						AetherGlobe.changeint = AetherGlobe.BalanceLevel - AetherGlobe.oldbalancelevel; // the amount the curse changed by (neg or positive)
							
						if (AetherGlobe.changeint == 0) // divide by 0 check
							AetherGlobe.changeint = 1;
						
						AetherGlobe.delta3 = delta2; // changing the deltas 
						AetherGlobe.delta2 = delta1;
						
						Console.WriteLine( "step 1 rateofchange " + rateofchange); // debug console writeline
						AetherGlobe.delta1 = (double)AetherGlobe.changeint / 100000; // to determine new delta	

						if (AetherGlobe.oldbalancelevel == 0)
							AetherGlobe.oldbalancelevel = MyServerSettings.curseincrease(); // divide by 0 check	
						//end

						if ( AetherGlobe.BalanceLevel <= 3000 && AetherGlobe.changeint <= 0) //curse was brought to 0, evil needs a boost
						{
								AetherGlobe.Multiplier += 5;
								AetherGlobe.rateofchange += Math.Abs(AetherGlobe.changeint) + MyServerSettings.curseincrease() * 2;	// sets the initial increase higher some						
						}

						else if ( AetherGlobe.BalanceLevel > 97000 && AetherGlobe.changeint > 0) //curse was brought to max, good needs a boost
						{
								AetherGlobe.Multiplier -= 5;
								AetherGlobe.rateofchange -= Math.Abs(AetherGlobe.changeint)  + MyServerSettings.curseincrease() * 2;	// sets the initial increase higher some						
						}

						else if ( ((Math.Abs((double)AetherGlobe.changeint) / 100000) < ((double)MyServerSettings.curseincrease() / 100000) ) && ( (Math.Abs(oldchangeint) / AetherGlobe.oldbalancelevel) < ((double)MyServerSettings.curseincrease() / 50000) ) )	// curse changed by a small amount, not much movement seen last 2 days
						{
							if (AetherGlobe.BalanceLevel > 70000 && Utility.RandomBool() )
								AetherGlobe.Multiplier --; // encourage good side since balance is evil
							else if (AetherGlobe.BalanceLevel < 30000 && Utility.RandomBool() )
								AetherGlobe.Multiplier ++; // encourage evil side since balance is good
							else // balance is neutral-ish, so reduce multiplier
							{
								if (AetherGlobe.Multiplier > 0 ) 
									AetherGlobe.Multiplier --;
								else if (AetherGlobe.Multiplier < 0 )
									AetherGlobe.Multiplier ++;
								else if (AetherGlobe.Multiplier == 0) // scale it to one side if the multiplier is neutral, and balance is neutral
								{
									if (AetherGlobe.changeint > 0)
										AetherGlobe.Multiplier --;
									else if (AetherGlobe.changeint < 0)
										AetherGlobe.Multiplier ++;
								}
							}
						}

						else if ( ( Math.Abs(AetherGlobe.changeint) > (MyServerSettings.curseincrease()*2) ) && ( (Math.Abs(oldchangeint) / AetherGlobe.oldbalancelevel) > ((double)MyServerSettings.curseincrease() / 50000) )) // changed by a lot, ensure multiplier isn't helping too much
						{
								
							if (AetherGlobe.Multiplier > 0 && AetherGlobe.changeint > 0) // tone down the multiplier since there was a large move on the multiplier side
								AetherGlobe.Multiplier -= 2;
							else if (AetherGlobe.Multiplier < 0 && AetherGlobe.changeint < 0) // tone down the multiplier since there was a large move on the multiplier side
								AetherGlobe.Multiplier += 2;
								
							if (AetherGlobe.BalanceLevel > 70000 && Utility.RandomBool() )
								AetherGlobe.Multiplier --; // encourage good side since balance is evil
							if (AetherGlobe.BalanceLevel < 30000 && Utility.RandomBool() )
								AetherGlobe.Multiplier ++; // encourage evil side since balance is good
						}	
						else 
						{
							if (AetherGlobe.BalanceLevel > 70000 && Utility.RandomBool() )
								AetherGlobe.Multiplier --; // encourage good side since balance is evil
							if (AetherGlobe.BalanceLevel < 30000 && Utility.RandomBool() )
								AetherGlobe.Multiplier ++; // encourage evil side since balance is good
						}
						
						if ( AetherGlobe.BalanceLevel > 30000 && AetherGlobe.BalanceLevel < 70000) // this multiplier makes it harder to players to game the system and keep it neutral
							DoubleMultiplier += 2;
						else if (DoubleMultiplier > 0)
							DoubleMultiplier --;
						
						AetherGlobe.oldbalancelevel = AetherGlobe.BalanceLevel; // records this value for next cycle	
						AetherGlobe.BalanceLevel += (int)AetherGlobe.rateofchange;
						

						Console.WriteLine( "step 6" + rateofchange);
						
					}
					lastchanged = DateTime.UtcNow;
					Console.WriteLine( "rateofreturn is " + AetherGlobe.rateofreturn + " and balancelevel changed by " + AetherGlobe.rateofchange + " and is now " + AetherGlobe.BalanceLevel );	
					LoggingFunctions.LogServer( "rateofreturn is" + AetherGlobe.rateofreturn + " and balancelevel is " + AetherGlobe.BalanceLevel );
				}
				else
					return;
			}
			else
			{
				if (change > 0 && AetherGlobe.Multiplier > 0 )
					AetherGlobe.BalanceLevel += (int)((double)change *  (1 + ( (double)AetherGlobe.Multiplier /10 ) ) *  (1 + ( (double)AetherGlobe.DoubleMultiplier /10 ) )  );
				else if (change < 0 && AetherGlobe.Multiplier < 0 )
					AetherGlobe.BalanceLevel += (int)((double)change *  (1 + ( (double)Math.Abs(AetherGlobe.Multiplier) /10 ) ) *  (1 + ( (double)AetherGlobe.DoubleMultiplier /10 ) )  );
				else
					AetherGlobe.BalanceLevel += (int)((double)change *  (1 + ( (double)AetherGlobe.DoubleMultiplier /10 ) ) );	
			}

			if (AetherGlobe.BalanceLevel > 100000)
			    AetherGlobe.BalanceLevel = 99000;

			if (AetherGlobe.BalanceLevel <= 0) // divide by 0 check
			    AetherGlobe.BalanceLevel = 1;

			AetherGlobe.UpdateColor();
			
			

		}
		
		public static void UpdateRateOfReturn() // updates the rate of return
		{

			Console.WriteLine( "Updating Rate of return" );
			rateofreturn = 0;
			double risklevel = AetherGlobe.BalanceLevel / 200000; // lower is better
			double multiplier = 1;
			rateofreturn =  ( (double)AetherGlobe.oldbalancelevel  - (double)AetherGlobe.BalanceLevel) / 300000; // base everything on how much the curse changed by
				
			// system calculates the rate of return for good investment bags.  evil bags just have the inverse of the return applied.
			
			// First case, Randomize the return a bit
			rateofreturn *= Utility.RandomDouble();
			
			//Second change if the balance is low, give bonus to good players if balance is high, lower good returns
			if ( Utility.RandomDouble() > risklevel )
				rateofreturn *= 1 + Utility.RandomDouble();
			else if (Utility.RandomDouble() < risklevel )
				rateofreturn /= 1 + Utility.RandomDouble();

			// Third, Calculate jackpot returns for good with a 1/1000 chance
			if ( Utility.RandomMinMax(1, 1000) == 69 )
				rateofreturn *= 5;
			
			//no jackpot, so see if returns should be flipped!  4% chance
			else if ( Utility.RandomDouble() < 0.05 )
			{
				if ( rateofreturn > 0)
					rateofreturn = -(rateofreturn); // failed, flip return negative
				else if ( rateofreturn < 0)
					rateofreturn = Math.Abs(rateofreturn); // in case returns are negative, increase them
			}
			
			// Finally, chance returns are just nill
			else if ( Utility.RandomDouble() < 0.05 )
				rateofreturn = 0;
				
			if (rateofreturn > 2) // sanity check
				rateofreturn = 2;
			if (rateofreturn < -2)
				rateofreturn = -2;


		}

		public static void UpdateAvatars()
		{
			AetherGlobe.ChaosAvatars.Clear();
			AetherGlobe.OrderAvatars.Clear();

			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is PlayerMobile && ((PlayerMobile)m).Avatar)
                {
                    PlayerMobile pm = (PlayerMobile)m;
					int bp = Math.Abs(pm.BalanceEffect);

					if (pm.BalanceStatus < 0)
					{
						bool check = true;
						while ( check )
						{
							bool repeat = false;
							foreach( var de in AetherGlobe.ChaosAvatars )
											{
												if (de.Key == bp)								
												{
													repeat = true;

													bp++;
												}
											}
							if (!repeat)
								check = false;
						}
						AetherGlobe.ChaosAvatars.Add(bp, pm.Name );
					}
					else if (pm.BalanceStatus > 0)
					{
						bool check = true;
						while ( check )
						{
							bool repeat = false;
							foreach( var de in AetherGlobe.OrderAvatars )
											{
												if (de.Key == bp)								
												{
													repeat = true;

													bp++;
												}
											}
							if (!repeat)
								check = false;
						}
						AetherGlobe.OrderAvatars.Add( bp, pm.Name );
					}
				}
			}

		}	
    }
}
