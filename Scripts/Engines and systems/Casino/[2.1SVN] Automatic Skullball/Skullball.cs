////////////////////////////////////////////////////////////////////////////////////////////
// 					SKULLBALL.cs					  //
////////////////////////////////////////////////////////////////////////////////////////////
// 		Roth/Joeku: 	Original Idea and Project Skeleton			  //
////////////////////////////////////////////////////////////////////////////////////////////
// 		SHAMBAMPOW: 	Fully Automotatic features including: 			  //
// 	Score Reset, Team Reset, Further Arena Decoration, Staggered Positioning,	  //
//	Winners Handouts, End-Game Leave, Player Freeze, and 1-Click Setup Stone.	  //
////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Regions;
using Server;
using Server.Items;

namespace Server.Items
{
	public class Skullball : Item
	{
		private int m_silverscore = 0;
		private int m_goldscore = 0;
		private int m_101 = 0;
		private int m_102;
		private int m_Xp = 0;
		private int m_Yp = 0;
		private int m_Xn = 0;
		private int m_Yn = 0;
		private int m_silverb1 = 0;
		private int m_silverb2 = 0;
		private int m_silverb3 = 0;
		private int m_silverb4 = 0;
		private int m_goldb1 = 0;
		private int m_goldb2 = 0;
		private int m_goldb3 = 0;
		private int m_goldb4 = 0;
		private int m_HomeY = 0;
		private int m_HomeX = 0;
		private Mobile m_AllowedPlayer1;
		private Mobile m_AllowedPlayer2;
		private bool m_GiveReward = true;
		private bool m_FreezePlayers = true;
		private double m_FreezeTime = 3.0;
		private int m_GiveRewardMinTeamCount = 1;
		private bool m_DisplayDateOnReward = true;
		private bool m_DisplayNameOnReward = true;
		private bool m_DisplayTeamCountOnReward = true;


		[CommandProperty( AccessLevel.Administrator )]
		public int Yp{ get{ return m_Yp; } set{ m_Yp = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int Xp{ get{ return m_Xp; } set{ m_Xp = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int Yn{ get{ return m_Yn; } set{ m_Yn = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int Xn{ get{ return m_Xn; } set{ m_Xn = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int silverb1{ get{ return m_silverb1; } set{ m_silverb1 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int silverb2{ get{ return m_silverb2; } set{ m_silverb2 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int silverb3{ get{ return m_silverb3; } set{ m_silverb3 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int silverb4{ get{ return m_silverb4; } set{ m_silverb4 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int goldb1{ get{ return m_goldb1; } set{ m_goldb1 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int goldb2{ get{ return m_goldb2; } set{ m_goldb2 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int goldb3{ get{ return m_goldb3; } set{ m_goldb3 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int goldb4{ get{ return m_goldb4; } set{ m_goldb4 = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int HomeX{ get{ return m_HomeX; } set{ m_HomeX = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int HomeY{ get{ return m_HomeY; } set{ m_HomeY = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int silverscore{ get{ return m_silverscore; } set{ m_silverscore = value; } }
		[CommandProperty( AccessLevel.Administrator )]
		public int goldscore{ get{ return m_goldscore; } set{ m_goldscore = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile MobileSilverTeam{ get{ return m_AllowedPlayer1; } set{ m_AllowedPlayer1 = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile MobileGoldTeam{ get{ return m_AllowedPlayer2; } set{ m_AllowedPlayer2 = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool GiveReward{ get{ return m_GiveReward; } set{ m_GiveReward = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool FreezePlayers{ get{ return m_FreezePlayers; } set{ m_FreezePlayers = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public double FreezeTime{ get{ return m_FreezeTime; } set{ m_FreezeTime = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public int GiveRewardMinTeamCount{ get{ return m_GiveRewardMinTeamCount; } set{ m_GiveRewardMinTeamCount = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool DisplayDateOnReward{ get{ return m_DisplayDateOnReward; } set{ m_DisplayDateOnReward = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool DisplayNameOnReward{ get{ return m_DisplayNameOnReward; } set{ m_DisplayNameOnReward = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public bool DisplayTeamCountOnReward{ get{ return m_DisplayTeamCountOnReward; } set{ m_DisplayTeamCountOnReward = value; } }
		

		[Constructable]
		public Skullball() : base( 6880 )
		{
			Movable = false;
			Hue = 1152;
			Name = "a skullball";
			Light = LightType.Circle300;
		}

		public Skullball( Serial serial ) : base( serial )
		{
		}
		
		public void goldteamb()
		{
			this.X = m_HomeX;
			this.Y = m_HomeY + Utility.RandomMinMax(-1,1);	// Added this to offset the skullball, either 1 up, 1 down, or in the middle.

			if( m_goldscore >= 10)
			{
				int oldgoldscore;
				int oldsilverscore;
				oldgoldscore = m_goldscore;
				oldsilverscore = m_silverscore;	
				m_goldscore = 0;
				m_silverscore = 0;
				SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 

				if( m_AllowedPlayer2 != null )
					m_AllowedPlayer2.Say( "Gold won! The final score is "+ oldgoldscore +" to " + oldsilverscore + ".");
				if( m_AllowedPlayer1 != null )
					m_AllowedPlayer1.Say( "Gold won! The final score is "+ oldgoldscore +" to " + oldsilverscore + ".");

				return;
			}

			if( m_AllowedPlayer2 != null )
			{
				if( m_goldscore > m_silverscore )
				{
					SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 
					m_AllowedPlayer2.Say( "Gold is winning, the score is " + m_goldscore + " to " + m_silverscore + ".");
				}
				else if( m_goldscore < m_silverscore )
				{
					SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 
					m_AllowedPlayer2.Say( "Silver is winning, the score is " + m_silverscore + " to " + m_goldscore + ".");
				}
				else
				{
					SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 
					m_AllowedPlayer2.Say( "Both teams are tied, the score is " + m_goldscore + " to " + m_silverscore + ".");
				}
			}

			if( m_AllowedPlayer1 != null )
			{
				if( m_goldscore > m_silverscore )
					m_AllowedPlayer1.Say( "Gold is winning, the score is " + m_goldscore + " to " + m_silverscore + ".");
				else if( m_goldscore < m_silverscore )
					m_AllowedPlayer1.Say( "Silver is winning, the score is " + m_silverscore + " to " + m_goldscore + ".");
				else
					m_AllowedPlayer1.Say( "Both teams are tied, the score is " + m_goldscore + " to " + m_silverscore + ".");
			}
		}
		
		public void silverteamb()
		{
			this.X = m_HomeX;
			this.Y = m_HomeY + Utility.RandomMinMax(-1,1);	// Added this to offset the skullball, either 1 up, 1 down, or in the middle.
			
			if( m_silverscore >= 10)
			{
				int oldgoldscore;
				int oldsilverscore;
				oldgoldscore = m_goldscore;
				oldsilverscore = m_silverscore;	
				m_goldscore = 0;
				m_silverscore = 0;
				SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 

				if( m_AllowedPlayer2 != null )
					m_AllowedPlayer2.Say( "Silver won! The final score is "+ oldsilverscore +" to " + oldgoldscore + ".");
				if( m_AllowedPlayer1 != null )
					m_AllowedPlayer1.Say( "Silver won! The final score is "+ oldsilverscore +" to " + oldgoldscore + ".");

				return;
			}

			if( m_AllowedPlayer2 != null )
			{
				if( m_goldscore > m_silverscore )
				{
					SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 
					m_AllowedPlayer2.Say( "Gold is winning, the score is " + m_goldscore + " to " + m_silverscore + ".");
				}
				else if( m_goldscore < m_silverscore )
				{
					SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 
					m_AllowedPlayer2.Say( "Silver is winning, the score is " + m_silverscore + " to " + m_goldscore + ".");
				}
				else
				{
					SetHue( m_AllowedPlayer1.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer1, m_AllowedPlayer2.FindItemOnLayer( Layer.OuterTorso ), m_AllowedPlayer2, this ); 
					m_AllowedPlayer2.Say( "Both teams are tied, the score is " + m_goldscore + " to " + m_silverscore + ".");
				}
			}

			if( m_AllowedPlayer1 != null )
			{
				if( m_goldscore > m_silverscore )
					m_AllowedPlayer1.Say( "Gold is winning, the score is " + m_goldscore + " to " + m_silverscore + ".");
				else if( m_goldscore < m_silverscore )
					m_AllowedPlayer1.Say( "Silver is winning, the score is " + m_silverscore + " to " + m_goldscore + ".");
				else
					m_AllowedPlayer1.Say( "Both teams are tied, the score is " + m_goldscore + " to " + m_silverscore + ".");
			}
		}

		public virtual void StartMoveBack( Mobile m )
		{
			Timer.DelayCall( TimeSpan.FromSeconds( 0.0 ), new TimerStateCallback( MoveBack_Callback ), m );
		}

		private void MoveBack_Callback( object state )
		{
			MoveBack( (Mobile) state );
		}

		public virtual void MoveBack( Mobile m )
		{
			if( m is PlayerMobile && m != null )
			{
				Map map = this.Map;
	
				if ( map == null || map == Map.Internal )
					map = m.Map;

				if( m_goldscore == 0 && m_silverscore == 0 )		// This was set to 10 && 10 but it didn't work because the scores are reset prior to calling this method. It's ok though, this gets it done.
				{
					IPooledEnumerable eable = m.GetItemsInRange(30);

					foreach ( Item g in eable)
					{ 
						if( g is SkullballExitGate)
						{
							SkullballExitGate gate = (SkullballExitGate)g;
							gate.OnMoveOver(m);
						}
					}
								
							return;
				}
			
				if( m.SolidHueOverride == 2125 )	// Move gold players back to their side
				{
					Point3D p = new Point3D( (m_HomeX - Utility.RandomMinMax(7,8)) , (m_HomeY + Utility.RandomMinMax(-2,2)), this.Z );
					m.MoveToWorld( p, map );
					if( m_FreezePlayers )
					{
						m.CantWalk = true;
						StartUnfreezePlayers(m);
					}
					return;
				}
				else if( m.SolidHueOverride == 2101 )	// Move silver players back to their side
				{
					Point3D p = new Point3D( (m_HomeX + Utility.RandomMinMax(7,8)), (m_HomeY + Utility.RandomMinMax(-2,2)), this.Z );
					m.MoveToWorld( p, map );
					if( m_FreezePlayers )
					{
						m.CantWalk = true;
						StartUnfreezePlayers(m);
					}
					return;
				}
			}
		}

		public virtual void UnfreezePlayers( Mobile m )
		{
			if( m is PlayerMobile && m != null && m.CantWalk == true )
				m.CantWalk = false;
		}

		public virtual void StartUnfreezePlayers( Mobile m )
		{
			if( m_FreezeTime < 0.0 )	// Safety Min.
				m_FreezeTime = 0.0;
			if( m_FreezeTime > 60.0 )	// Safety Max.
				m_FreezeTime = 60.0;	
				
			Timer.DelayCall( TimeSpan.FromSeconds( m_FreezeTime ), new TimerStateCallback( UnfreezePlayers_Callback ), m );
		}

		private void UnfreezePlayers_Callback( object state )
		{
			UnfreezePlayers( (Mobile)state );
		}

		public void bc()
		{	
			ArrayList mobs = new ArrayList( World.Mobiles.Values ); 

			int goldteamcount = 0;
			int silverteamcount = 0;

			foreach ( Mobile ko in mobs )
			{
				if( ko is PlayerMobile && ( ko != null ) )
				{
					if ( ko.SolidHueOverride == 2101 )
					{
						silverteamcount++;	// count the number of players who are silver
					}
					if ( ko.SolidHueOverride == 2125 )
					{
						goldteamcount++;	// count the number of players who are gold
					}
				}
			}
			foreach ( Mobile ko in mobs ) 
			{
				if ( ( ko is PlayerMobile ) && ( ko != null )  )
				{
					if ( ko.SolidHueOverride == 2101 || ko.SolidHueOverride == 2125 )
					{
						if( m_goldscore >= 10)
						{
							if( m_AllowedPlayer2 != null )
								ko.SendMessage( ko.SolidHueOverride, "Gold won! The final score is "+ m_goldscore +" to " + m_silverscore + ".");
							if( m_AllowedPlayer1 != null )
								ko.SendMessage( ko.SolidHueOverride, "Gold won! The final score is "+ m_goldscore +" to " + m_silverscore + ".");

							if( ko.SolidHueOverride == 2125 && m_GiveReward )	//Gold Winners Reward
							{
								if( goldteamcount >= m_GiveRewardMinTeamCount && silverteamcount >= m_GiveRewardMinTeamCount )
								{
									Item prize = Loot.RandomStatue();
									prize.Hue = 2125;
									prize.Name = ( DisplayNameOnReward ? ko.Name + ": " : "" ) + "Skullball Winner *** GOLD TEAM *** " + ( DisplayTeamCountOnReward ? "[" + goldteamcount + "v" + silverteamcount + "] " : "" ) + ( DisplayDateOnReward ? DateTime.UtcNow.ToString() : "" );
									prize.LootType = LootType.Blessed;
									ko.AddToBackpack( prize );
								}
								else
								{
									ko.SendMessage( 33, "There was not enough players to qualify for a trophy." );
								}
							}
	
							return;
						}
						else if( m_silverscore >= 10)
						{
							if( m_AllowedPlayer2 != null )
								ko.SendMessage( ko.SolidHueOverride, "Silver won! The final score is "+ m_silverscore +" to " + m_goldscore + ".");
							if( m_AllowedPlayer1 != null )
								ko.SendMessage( ko.SolidHueOverride, "Silver won! The final score is "+ m_silverscore +" to " + m_goldscore + ".");

							if( ko.SolidHueOverride == 2101 && m_GiveReward )	// Silver Winners Reward
							{
								if( goldteamcount >= m_GiveRewardMinTeamCount && silverteamcount >= m_GiveRewardMinTeamCount )
								{
									Item prize = Loot.RandomStatue();
									prize.Hue = 2101;
									prize.Name = ( DisplayNameOnReward ? ko.Name + ": " : "" ) + "Skullball Winner *** SILVER TEAM *** " + ( DisplayTeamCountOnReward ? "[" + silverteamcount + "v" + goldteamcount + "] " : "" ) + ( DisplayDateOnReward ? DateTime.UtcNow.ToString() : "" );
									prize.LootType = LootType.Blessed;
									ko.AddToBackpack( prize );
								}
								else
								{
									ko.SendMessage( 33, "There was not enough players to qualify for a trophy." );
								}
							}


							return;
						}


						if( m_goldscore > m_silverscore )
							ko.SendMessage( ko.SolidHueOverride, "Gold is winning, the score is " + m_goldscore + " to " + m_silverscore + ".");
						else if( m_goldscore < m_silverscore )
							ko.SendMessage( ko.SolidHueOverride, "Silver is winning, the score is " + m_silverscore + " to " + m_goldscore + ".");
						else
							ko.SendMessage( ko.SolidHueOverride, "Both teams are tied, the score is " + m_goldscore + " to " + m_silverscore + ".");

					}
				}
			}
		}
		
		public static void SetHue(Item g, Mobile a, Item r, Mobile s, Skullball b)
		{
			if( b.goldscore > b.silverscore )
			{
				if( g != null )
					g.Hue = 2125;
				if( a != null )
					a.SpeechHue = 2125;
				if( r != null )
					r.Hue = 2125;
				if( s != null )
					s.SpeechHue = 2125;
				if( b != null )
					b.Hue = 2125;
			}
			else if( b.silverscore > b.goldscore )
			{
				if( g != null )
					g.Hue = 2101;
				if( a != null )
					a.SpeechHue = 2101;
				if( r != null )
					r.Hue = 2101;
				if( s != null )
					s.SpeechHue = 2101;
				if( b != null )
					b.Hue = 2101;
			}
			else
			{
				if( g != null )
					g.Hue = 1152;
				if( a != null )
					a.SpeechHue = 1153;
				if( r != null )
					r.Hue = 1152;
				if( s != null )
					s.SpeechHue = 1153;
				if( b != null )
					b.Hue = 1153;
			}
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m.SolidHueOverride == 2125 || m.SolidHueOverride == 2101  )
			{
				if ( m.Direction == Direction.East || (m.Direction & ~Direction.Running) == Direction.East )
				{
					if( ( m_goldb1 == this.Y ) && ( m_Xp == this.X ) || ( m_goldb2 == this.Y )  && ( m_Xp == this.X ) || ( m_goldb3 == this.Y )  && ( m_Xp == this.X )  || ( m_goldb4 == this.Y )  && ( m_Xp == this.X ) )
					{
						m_goldscore += 1;
						this.bc();
						this.goldteamb();
						
						foreach( Mobile mob in GetMobilesInRange(30) )
							StartMoveBack(mob);

					}
					else
					{
						if( m_Xp != this.X )
							X += 1;
						else
							X -= 1;
					}
				}
				else if ( m.Direction == Direction.West || (m.Direction & ~Direction.Running) == Direction.West )
				{
					if( ( m_silverb1 == this.Y ) && ( m_Xn == this.X ) || ( m_silverb2 == this.Y )  && ( m_Xn == this.X )|| ( m_silverb3 == this.Y )  && ( m_Xn == this.X )|| (m_silverb4 == this.Y )  && ( m_Xn == this.X ))
					{
						m_silverscore += 1;
						this.bc();
						this.silverteamb();

						foreach( Mobile mob in GetMobilesInRange(30) )
							StartMoveBack(mob);
					}
					else
					{
						if( m_Xn != this.X )
							X -= 1;
						else
							X += 1;
					}
				}
				else if ( m.Direction == Direction.South || (m.Direction & ~Direction.Running) == Direction.South )
				{
					if( m_Yp != this.Y )
						Y += 1;
					else
						Y -= 1;
				}
				else if ( m.Direction == Direction.North || m.Direction == Direction.Running )
				{
					if( m_Yn != this.Y )
						Y -= 1;
					else
						Y += 1;
				}
				else if ( m.Direction == Direction.Down || (m.Direction & ~Direction.Running) == Direction.Down )
				{	
					if( ( m_goldb2 == this.Y )  && ( m_Xp == this.X ) || ( m_goldb3 == this.Y )  && ( m_Xp == this.X )  || ( m_goldb4 == this.Y )  && ( m_Xp == this.X ) )
					{	
						m_goldscore += 1;
						this.bc();
						this.goldteamb();

						foreach( Mobile mob in GetMobilesInRange(30) )
							StartMoveBack(mob);
					}
					else
					{
						if( m_Yp != this.Y )
							Y += 1;
						else
							Y -= 1;
						if( m_Xp != this.X )
							X += 1;
						else
							X -= 1;
					}
				}
				else if ( m.Direction == Direction.Mask || m.Direction == Direction.ValueMask )
				{
					if( ( m_silverb1 == this.Y ) && ( m_Xn == this.X ) || ( m_silverb2 == this.Y )  && ( m_Xn == this.X )|| ( m_silverb3 == this.Y )  && ( m_Xn == this.X ))
					{
						m_silverscore += 1;
						this.bc();
						this.silverteamb();

						foreach( Mobile mob in GetMobilesInRange(30) )
							StartMoveBack(mob);

					}
					else
					{
						if( m_Yn != this.Y )
							Y -= 1;
						else
							Y += 1;
						if( m_Xn != this.X )
							X -= 1;
						else
							X += 1;
					}
				}
				else if ( m.Direction == Direction.Left || (m.Direction & ~Direction.Running) == Direction.Left )
				{
					if( ( m_silverb2 == this.Y )  && ( m_Xn == this.X )|| ( m_silverb3 == this.Y )  && ( m_Xn == this.X )|| (m_silverb4 == this.Y )  && ( m_Xn == this.X ))
					{
						m_silverscore += 1;
						this.bc();
						this.silverteamb();

						foreach( Mobile mob in GetMobilesInRange(30) )
							StartMoveBack(mob);

					}
					else
					{
						if( m_Yp != this.Y )
							Y += 1;
						else
							Y -= 1;
						if( m_Xn != this.X )
							X -= 1;
						else
							X += 1;
					}
				}
				else if ( m.Direction == Direction.Right || (m.Direction & ~Direction.Running) == Direction.Right )
				{
					if( ( m_goldb1 == this.Y ) && ( m_Xp == this.X ) || ( m_goldb2 == this.Y )  && ( m_Xp == this.X ) || ( m_goldb3 == this.Y )  && ( m_Xp == this.X ) )
					{
						m_goldscore += 1;
						this.bc();
						this.goldteamb();

						foreach( Mobile mob in GetMobilesInRange(30) )
							StartMoveBack(mob);

					}
					else
					{
						if( m_Yn != this.Y )
							Y -= 1;
						else
							Y += 1;
						
						if( m_Xp != this.X )
							X += 1;
						else
							X -= 1;
					}
				}

           			switch (Utility.Random( 5 ))  
				{ 
					case 0: this.ItemID = 6880; 
						break;
					case 1: this.ItemID = 6881; 
						break;
					case 2: this.ItemID = 6882; 
						break;
					case 3: this.ItemID = 6883; 
						break;
					case 4: this.ItemID = 6884; 
						break;
				}
			}

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
	
			writer.Write( m_silverscore );
			writer.Write( m_goldscore );
			writer.Write( m_Xp );
			writer.Write( m_Yp );
			writer.Write( m_Xn );
			writer.Write( m_Yn );
			writer.Write( m_silverb1 );
			writer.Write( m_silverb2 );
			writer.Write( m_silverb3 );
			writer.Write( m_silverb4 );
			writer.Write( m_goldb1 );
			writer.Write( m_goldb2 );
			writer.Write( m_goldb3 );
			writer.Write( m_goldb4 );
			writer.Write( m_HomeY );
			writer.Write( m_HomeX );
			writer.Write( m_AllowedPlayer1 );
			writer.Write( m_AllowedPlayer2 );
			writer.Write( m_GiveReward );
			writer.Write( m_FreezePlayers );
			writer.Write( m_FreezeTime );
			writer.Write( m_GiveRewardMinTeamCount );
			writer.Write( m_DisplayDateOnReward );
			writer.Write( m_DisplayNameOnReward );
			writer.Write( m_DisplayTeamCountOnReward );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_silverscore = reader.ReadInt();
			m_goldscore = reader.ReadInt();
			m_Xp = reader.ReadInt();
			m_Yp = reader.ReadInt(); 
			m_Xn = reader.ReadInt();
			m_Yn = reader.ReadInt();
			m_silverb1 = reader.ReadInt();
			m_silverb2 = reader.ReadInt();
			m_silverb3 = reader.ReadInt();
			m_silverb4 = reader.ReadInt();
			m_goldb1 = reader.ReadInt();
			m_goldb2 = reader.ReadInt();
			m_goldb3 = reader.ReadInt();
			m_goldb4 = reader.ReadInt();
			m_HomeY = reader.ReadInt();
			m_HomeX = reader.ReadInt();
			m_AllowedPlayer1 = reader.ReadMobile();
			m_AllowedPlayer2 = reader.ReadMobile();
			m_GiveReward = reader.ReadBool();
			m_FreezePlayers = reader.ReadBool();
			m_FreezeTime = reader.ReadDouble();
			m_GiveRewardMinTeamCount = reader.ReadInt();
			m_DisplayDateOnReward = reader.ReadBool();
			m_DisplayNameOnReward = reader.ReadBool();
			m_DisplayTeamCountOnReward = reader.ReadBool();
		}
	}

	public enum Team
	{
		Gold,
		Silver
	}

	public class SkullballEnterGate : Item
	{
		private Team m_Team;
		private Point3D m_PointDest;
		private Map m_MapDest;

		[CommandProperty( AccessLevel.GameMaster )]
		public Team Team{ get{ return m_Team; } set{ m_Team = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest{ get{ return m_PointDest; } set{ m_PointDest = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest{ get{ return m_MapDest; } set{ m_MapDest = value; } }

		[Constructable]
		public SkullballEnterGate() : base( 0xF6C )
		{
			Movable = false;
			Name = "Enter Skullball";
			Light = LightType.Circle300;
		}

		public SkullballEnterGate( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if( !(m is PlayerMobile) || m == null )
				return true;		
	
			Point3D p = m_PointDest;
			Map map = m_MapDest;
			if ( map == null || map == Map.Internal )
				map = m.Map;

			if( p == Point3D.Zero )
			{
				m.SendMessage( "There is no destination set for this gate. Please contact a GM." );
				return true;
			}

			if( m.SolidHueOverride == 2125 || m.SolidHueOverride == 2101 )
			{
				m.SendMessage( "You are already in a Skullball game!" );
				return true;
			}

			if( m.Mounted )
			{
				m.SendMessage( "Please dismount before entering the Skullball field." );
				return true;
			}

			if ( m_Team == Team.Gold )
			{
				m.MoveToWorld( p, map );
				m.Blessed = true;
				m.SolidHueOverride = 2125;

				ArrayList mobs = new ArrayList( World.Mobiles.Values ); 
				foreach ( Mobile ko in mobs ) 
				{
					if ( ( ko is PlayerMobile ) && ( ko != null )  )
					{
						if ( ko.SolidHueOverride == 2101 || ko.SolidHueOverride == 2125 )
							ko.SendMessage( ko.SolidHueOverride, m.Name + " has joined the Skullball game." );
					}
				}	
			}
			else
			{
				m.MoveToWorld( p, map );
				m.Blessed = true;
				m.SolidHueOverride = 2101;
				
				ArrayList mobs = new ArrayList( World.Mobiles.Values ); 
				foreach ( Mobile ko in mobs ) 
				{
					if ( ( ko is PlayerMobile ) && ( ko != null )  )
					{
						if ( ko.SolidHueOverride == 2101 || ko.SolidHueOverride == 2125 )
							ko.SendMessage( ko.SolidHueOverride, m.Name + " has joined the Skullball game." );
					}
				}
			}
			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
	
			writer.Write( (int) m_Team );
			writer.Write( m_PointDest );
			writer.Write( m_MapDest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Team = (Team)reader.ReadInt();
			m_PointDest = reader.ReadPoint3D();
			m_MapDest = reader.ReadMap();
		}
	}

	public class SkullballExitGate : Item
	{
		private Point3D m_PointDest;
		private Map m_MapDest;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest{ get{ return m_PointDest; } set{ m_PointDest = value; } }
		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest{ get{ return m_MapDest; } set{ m_MapDest = value; } }

		[Constructable]
		public SkullballExitGate() : base( 0xF6C )
		{
			Movable = false;
			Name = "Exit Skullball";
			Light = LightType.Circle300;
		}

		public SkullballExitGate( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if( !(m is PlayerMobile) || m == null )
				return false;

			Point3D p = m_PointDest;
			Map map = m_MapDest;
			if ( map == null || map == Map.Internal )
				map = m.Map;

			if( p == Point3D.Zero )
			{
				m.SendMessage( "There is no destination set for this gate. Please contact a GM." );
				return false;
			}

			if( m.SolidHueOverride != 2125 && m.SolidHueOverride != 2101 )
			{
				m.MoveToWorld( p, map );
				m.Blessed = false;

				return false;
			}

			ArrayList mobs = new ArrayList( World.Mobiles.Values ); 
			foreach ( Mobile ko in mobs ) 
			{
				if ( ( ko is PlayerMobile ) && ( ko != null )  )
				{
					if ( ko.SolidHueOverride == 2101 || ko.SolidHueOverride == 2125 )
					{
						if( ko.Location == this.Location )	// Only send the leave message if they use the gate. (we call this wehen the game is over, don't need message then).
							ko.SendMessage( ko.SolidHueOverride, m.Name + " has left the Skullball game." );
					}
				}
			}

			m.MoveToWorld( p, map );
			m.Blessed = false;
			m.SolidHueOverride = -1;	

			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
	
			writer.Write( m_PointDest );
			writer.Write( m_MapDest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_PointDest = reader.ReadPoint3D();
			m_MapDest = reader.ReadMap();
		}
	}
	
	public class SkullballSetupStone : Item
	{	
		[Constructable]
		public SkullballSetupStone() : base( 4483 )
		{
			Movable = false;
			Name = "Skullball Setup Stone";
			Hue = 33;
			Light = LightType.Circle300;
		}	

		public SkullballSetupStone( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{	
			///////////////////////////////////////////////////////////////////  body
			for ( int Yy = this.Y; Yy <= 9 + this.Y; ++Yy )
			for ( int Xx = this.X; Xx <= 20 + this.X; ++Xx )
			new Server.Items.Static( 6014 ).MoveToWorld( new Point3D( Xx, Yy, this.Z ), this.Map );
			////////////////////////////////////////////////////////////////////
			
			////////////////////////////////////////////////////////////////////// out side area 
			//for ( int Yy = this.Y; Yy <= 9 + this.Y; ++Yy )
			for ( int Xx = this.X; Xx <= 22 + this.X; ++Xx )
			{
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( Xx - 1, this.Y - 1, this.Z ), this.Map );
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( Xx - 1, this.Y + 10, this.Z ), this.Map );
			}
			
			for ( int Yy = this.Y; Yy <= 2 + this.Y; ++Yy )
			{
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X - 1, Yy, this.Z ), this.Map );
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 21, Yy, this.Z ), this.Map );
				
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X - 1, Yy + 7, this.Z ), this.Map );
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 21, Yy + 7, this.Z ), this.Map );
				
			}
			
			for ( int Yy = this.Y; Yy <= 5 + this.Y; ++Yy )
			{
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X - 2, Yy + 2, this.Z ), this.Map );
				new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 22, Yy + 2, this.Z ), this.Map );
			}
			////////////////////////////////////////////////////////////////////
			
			////////////////////////////////////////////////////////////////////// out side area 2
			//for ( int Yy = this.Y; Yy <= 9 + this.Y; ++Yy )
			for ( int Xx = this.X; Xx <= 24 + this.X; ++Xx )
			{
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( Xx - 2, this.Y - 2, this.Z ), this.Map );
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( Xx - 2, this.Y + 11, this.Z ), this.Map );
			}
			
			for ( int Yy = this.Y; Yy <= 2 + this.Y; ++Yy )
			{
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X - 2, Yy -1, this.Z ), this.Map );
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X + 22, Yy -1, this.Z ), this.Map );
				
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X - 2, Yy + 8, this.Z ), this.Map );
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X + 22, Yy + 8, this.Z ), this.Map );
				
			}
			
			for ( int Yy = this.Y; Yy <= 7 + this.Y; ++Yy )
			{
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X - 3, Yy + 1, this.Z ), this.Map );
				new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X + 23, Yy + 1, this.Z ), this.Map );
			}
			////////////////////////////////////////////////////////////////////




			//////////////////////////////// GOALS /////////////////////////////

			////////////////////////////// GOLD SIDE ///////////////////////////
			Item wall1 = new Server.Items.Static( 9350 ); wall1.Hue = 2125; wall1.MoveToWorld( new Point3D( this.X - 2, this.Y + 6, this.Z ), this.Map ); // back wall
			Item wall2 = new Server.Items.Static( 9350 ); wall2.Hue = 2125; wall2.MoveToWorld( new Point3D( this.X - 2, this.Y + 5, this.Z ), this.Map );	// back wall
			Item wall3 = new Server.Items.Static( 9350 ); wall3.Hue = 2125; wall3.MoveToWorld( new Point3D( this.X - 2, this.Y + 4, this.Z ), this.Map );	// back wall
			Item wall4 = new Server.Items.Static( 9350 ); wall4.Hue = 2125; wall4.MoveToWorld( new Point3D( this.X - 2, this.Y + 3, this.Z ), this.Map );	// back wall
			Item wall5 = new Server.Items.Static( 9354 ); wall5.Hue = 2125; wall5.MoveToWorld( new Point3D( this.X - 2, this.Y + 2, this.Z ), this.Map );	// corner post
			Item wall6 = new Server.Items.Static( 9351 ); wall6.Hue = 2125; wall6.MoveToWorld( new Point3D( this.X - 1, this.Y + 2, this.Z ), this.Map );	// goal side 1
			Item wall7 = new Server.Items.Static( 9351 ); wall7.Hue = 2125; wall7.MoveToWorld( new Point3D( this.X - 1, this.Y + 6, this.Z ), this.Map ); // goal side 2
			new Blocker().MoveToWorld( new Point3D( this.X - 2, this.Y + 7, this.Z ), this.Map ); // blocker

			// GOALIE BOX
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 1, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 2, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 2, this.Y + 3, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 2, this.Y + 4, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 2, this.Y + 5, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 2, this.Y + 6, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 2, this.Y + 7, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 1, this.Y + 7, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X , this.Y + 7, this.Z ), this.Map ); // stone pavern

			///////////////////////////// SILVER SIDE //////////////////////////
			Item wall8 = new Server.Items.Static( 9350 ); wall8.Hue = 2101; wall8.MoveToWorld( new Point3D( this.X + 21, this.Y + 5, this.Z ), this.Map ); // back wall
			Item wall9 = new Server.Items.Static( 9350 ); wall9.Hue = 2101; wall9.MoveToWorld( new Point3D( this.X + 21, this.Y + 4, this.Z ), this.Map ); // back wall
			Item wall10 = new Server.Items.Static( 9350 ); wall10.Hue = 2101; wall10.MoveToWorld( new Point3D( this.X + 21, this.Y + 3, this.Z ), this.Map ); // back wall
			Item wall11 = new Server.Items.Static( 9351 ); wall11.Hue = 2101; wall11.MoveToWorld( new Point3D( this.X + 21, this.Y + 2, this.Z ), this.Map ); // goal side 1
			Item wall12 = new Server.Items.Static( 9349 ); wall12.Hue = 2101; wall12.MoveToWorld( new Point3D( this.X + 21, this.Y + 6, this.Z ), this.Map ); // goal side 2 & back wall/corner
			new Blocker().MoveToWorld( new Point3D( this.X + 22, this.Y + 2, this.Z ), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 22, this.Y + 3, this.Z ), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 22, this.Y + 4, this.Z ), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 22, this.Y + 5, this.Z ), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 22, this.Y + 6, this.Z ), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 22, this.Y + 7, this.Z ), this.Map ); // blocker

			// GOALIE BOX
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 20, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 19, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 18, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 18, this.Y + 3, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 18, this.Y + 4, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 18, this.Y + 5, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 18, this.Y + 6, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 18, this.Y + 7, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 19, this.Y + 7, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 20, this.Y + 7, this.Z ), this.Map ); // stone pavern

			// CENTER LINE
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 1, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 3, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 4, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 5, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 6, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 7, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 8, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 9, this.Z ), this.Map ); // stone pavern

			// CENTER CIRCLE
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 11, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 9, this.Y + 2, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 12, this.Y + 3, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 8, this.Y + 3, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 13, this.Y + 4, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 7, this.Y + 4, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 13, this.Y + 5, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 7, this.Y + 5, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 12, this.Y + 6, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 8, this.Y + 6, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 11, this.Y + 7, this.Z ), this.Map ); // stone pavern
			new Server.Items.Static( 1313 ).MoveToWorld( new Point3D( this.X + 9, this.Y + 7, this.Z ), this.Map ); // stone pavern

			////////////////////////// GATE DIVIDE BLOCK ///////////////////////
			new Server.Items.Static( 1848 ).MoveToWorld( new Point3D( this.X + 10, this.Y + 12, this.Z ), this.Map );
			new Blocker().MoveToWorld( new Point3D( this.X + 10, this.Y + 12, this.Z + 5), this.Map ); // blocker

			new Blocker().MoveToWorld( new Point3D( this.X + 10, this.Y - 2, this.Z + 5), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 11, this.Y - 2, this.Z + 5), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X + 9, this.Y - 2, this.Z + 5), this.Map ); // blocker

			new Blocker().MoveToWorld( new Point3D( this.X - 1, this.Y - 2, this.Z + 5), this.Map ); // blocker
			new Blocker().MoveToWorld( new Point3D( this.X - 2, this.Y - 2, this.Z + 5), this.Map ); // blocker

			////////////////////////// EXIT GATE STAIRS ////////////////////////
			new Server.Items.Static( 1856 ).MoveToWorld( new Point3D( this.X + 21, this.Y - 1, this.Z ), this.Map );

			///////////////////////////// EXIT GATE ////////////////////////////
			SkullballExitGate exitgate = new SkullballExitGate();
			exitgate.PointDest = new Point3D( this.X + 10, this.Y + 13, this.Z );
			exitgate.MapDest = this.Map;
			exitgate.Hue = 33;
			exitgate.MoveToWorld( new Point3D( this.X + 22, this.Y - 2, this.Z + 5 ), this.Map );
			new Blocker().MoveToWorld( new Point3D( this.X + 21, this.Y - 2, this.Z + 5 ), this.Map ); // blocker

			////////////////////////// ENTRANCE GATES //////////////////////////
			SkullballEnterGate entergateG = new SkullballEnterGate();	// Gold
			entergateG.PointDest = new Point3D( this.X + 1, this.Y + 5, this.Z );
			entergateG.MapDest = this.Map;
			entergateG.Team = Team.Gold;
			entergateG.Hue = 2125;
			entergateG.ItemID = 14170;
			entergateG.MoveToWorld( new Point3D( this.X + 9, this.Y + 12, this.Z ), this.Map );

			SkullballEnterGate entergateS = new SkullballEnterGate(); // Silver
			entergateS.PointDest = new Point3D( this.X + 18, this.Y + 5, this.Z );
			entergateS.MapDest = this.Map;
			entergateS.Team = Team.Silver;
			entergateS.Hue = 2101;
			entergateS.ItemID = 14170;
			entergateS.MoveToWorld( new Point3D( this.X + 11, this.Y + 12, this.Z ), this.Map );


			////////////////////////// Fire Columns ///////////////////////////
			// 1
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 2, this.Z ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 2, this.Z + 5 ), this.Map );
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 2, this.Z + 10 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 2, this.Z + 11 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 1, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 1, this.Z + 5), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 1, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 1, this.Z + 15 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X - 3, this.Y - 1, this.Z + 16 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y, this.Z + 5), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y, this.Z + 15 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X - 3, this.Y, this.Z + 20 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X - 3, this.Y, this.Z + 21 ), this.Map );
			// 2
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 2, this.Z ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 2, this.Z + 5 ), this.Map );
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 2, this.Z + 10 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 2, this.Z + 11 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 1, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 1, this.Z + 5), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 1, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 1, this.Z + 15 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X + 23, this.Y - 1, this.Z + 16 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y, this.Z + 5 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y, this.Z + 15 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X + 23, this.Y, this.Z + 20 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X + 23, this.Y, this.Z + 21 ), this.Map );
			// 3
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 11, this.Z ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 11, this.Z + 5 ), this.Map );
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 11, this.Z + 10 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 11, this.Z + 11 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 10, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 10, this.Z + 5 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 10, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 10, this.Z + 15 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 10, this.Z + 16 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 9, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 9, this.Z + 5), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 9, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 9, this.Z + 15 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 9, this.Z + 20 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X + 23, this.Y + 9, this.Z + 21 ), this.Map );
			// 4
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 11, this.Z ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 11, this.Z + 5 ), this.Map );
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 11, this.Z + 10 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 11, this.Z + 11 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 10, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 10, this.Z + 5 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 10, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 10, this.Z + 15 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 10, this.Z + 16 ), this.Map );
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 9, this.Z ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 9, this.Z + 5 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 9, this.Z + 10 ), this.Map ); 
			new Server.Items.Static( 933 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 9, this.Z + 15 ), this.Map ); 
			new Server.Items.Static( 6587 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 9, this.Z + 20 ), this.Map );
			new Server.Items.Static( 6571 ).MoveToWorld( new Point3D( this.X - 3, this.Y + 9, this.Z + 21 ), this.Map );



			
			Static static1 = new Static( 1179 );	// Gold Goal Floor
			static1.Hue = 2125;
			static1.X = this.X -1;
			static1.Y = this.Y +3;
			static1.Z = this.Z;
			static1.Map = this.Map;
			Static static2 = new Static( 1179 );
			static2.Hue = static1.Hue;
			static2.X = static1.X;
			static2.Y = static1.Y +1;
			static2.Z = this.Z;
			static2.Map = this.Map;
			Static static3 = new Static( 1179 );
			static3.Hue = static1.Hue;
			static3.X = static1.X;
			static3.Y = static1.Y +2;
			static3.Z = this.Z;
			static3.Map = this.Map;
			Static static4 = new Static( 1179 );
			static4.Hue = static1.Hue;
			static4.X = static1.X;
			static4.Y = static1.Y +3;
			static4.Z = this.Z;
			static4.Map = this.Map;
			
			Static static5 = new Static( 1179 );	// Silver Goal Floor
			static5.Hue = 2101;
			static5.X = this.X +21;
			static5.Y = this.Y +3;
			static5.Z = this.Z;
			static5.Map = this.Map;
			Static static6 = new Static( 1179 );
			static6.Hue = static5.Hue;
			static6.X = static5.X;
			static6.Y = static5.Y +1;
			static6.Z = this.Z;
			static6.Map = this.Map;
			Static static7 = new Static( 1179 );
			static7.Hue = static5.Hue;
			static7.X = static5.X;
			static7.Y = static5.Y +2;
			static7.Z = this.Z;
			static7.Map = this.Map;
			Static static8 = new Static( 1179 );
			static8.Hue = static5.Hue;
			static8.X = static5.X;
			static8.Y = static5.Y +3;
			static8.Z = this.Z;
			static8.Map = this.Map;

			
			Skullball ball = new Skullball();
			ball.Yn = this.Y;
			ball.Yp = this.Y + 9;
			
			ball.Xn = this.X;
			ball.Xp = this.X + 20;
			
			ball.goldb4 = this.Y + 3;
			ball.goldb3 = this.Y + 4;
			ball.goldb2 = this.Y + 5;
			ball.goldb1 = this.Y + 6;
			
			ball.silverb4 = this.Y + 3;
			ball.silverb3 = this.Y + 4;
			ball.silverb2 = this.Y + 5;
			ball.silverb1 = this.Y + 6;
			
			ball.HomeX = this.X + 10;
			ball.HomeY = this.Y + 4;
			
			
			
			Referee ref1 = new Referee();
			ball.MobileSilverTeam = ref1;
			ref1.Direction = Direction.South;
			ref1.MoveToWorld( new Point3D( this.X + 10, this.Y - 2, this.Z + 5 ), this.Map );
			
			Referee ref2 = new Referee();
			ball.MobileGoldTeam = ref2;
			ref2.Direction = Direction.North;
			ref2.MoveToWorld( new Point3D( this.X + 10, this.Y + 11, this.Z + 5 ), this.Map );
			
			ball.MoveToWorld( new Point3D( this.X + 10, this.Y + 4, this.Z + 2 ), this.Map );
			
			this.Delete();
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

namespace Server.Mobiles
{
	[CorpseName( "a referee corpse" )]
	public class Referee : Mobile
	{
		[Constructable]
		public Referee() : base()
		{
			Name = "a referee";
			Body = 400;
			Hue = 1153;
			Blessed = true;
			SpeechHue = 1153;
			CantWalk = true;

			GMRobe robe = new GMRobe();
			robe.Hue = 1153;
			robe.Name = "Referee's Robe";
			AddItem( robe );
		}

		public Referee( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}