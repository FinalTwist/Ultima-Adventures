using System; 
using System.Collections; 
using System.Collections.Generic;
using Server;
using Server.Misc; 
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.OneTime;
using Server.SkillHandlers;

namespace Server.Mobiles 
{ 
	public class BaseChild : BaseCreature, IOneTime
	{
       // public int OneTimeType = 3; //second : 3 = second, 4 = minute, 5 = hour, 6 = day (Pick a time interval 3-6)

		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

        public override bool ReacquireOnMovement{ get { return false; } }
		public override TimeSpan ReacquireDelay{ get { return TimeSpan.FromSeconds( 1.0 ); } }

		private int talkingtimer;
		private int body;
		private bool stealingtimer;
		private int stealingcount;
		private bool action;
		private int actioncount;
		private int freedcount;
		private int annoyed;

		private Mobile m_stole;
		[CommandProperty( AccessLevel.GameMaster )]
        public Mobile stole
        {
            get{ return m_stole; }
            set{ m_stole = value; }
        }

		private Mobile m_childtarget;
		[CommandProperty( AccessLevel.GameMaster )]
        public Mobile childtarget
        {
            get{ return m_childtarget; }
            set{ m_childtarget = value; }
        }

		private bool m_freedom;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool freedom
        {
            get{ return m_freedom; }
            set{ m_freedom = value; }
        }

		private bool m_talk;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool talk
        {
            get{ return m_talk; }
            set{ m_talk = value; }
        }

		private int m_type;
		[CommandProperty( AccessLevel.GameMaster )]
        public int type
        {
            get{ return m_type; }
            set{ m_type = value; }
        }
		
		public override bool CanMoveOverObstacles { get { return true; } }
		public override bool CanDestroyObstacles { get { return true; } }
		
		public override bool HandlesOnSpeech( Mobile from ) 
		{ 
			return true; 
		} 
		
		
		public BaseChild(AIType ai, FightMode fm, int PR, int FR, double AS, double PS) : base( ai, fm, PR, FR, AS, PS )
        {
            SpeechHue = Utility.RandomDyedHue(); 
			RangePerception = BaseCreature.DefaultRangePerception*2;
			Criminal = false;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
			m_OneTimeType = 3;
			Karma = 5;
			talkingtimer = 0;
			stealingcount = 0;
			m_talk = true;
			stealingtimer = true;
			m_childtarget = null;
			m_freedom = false;
			freedcount = 0;
			m_stole = this;
			annoyed = Utility.RandomMinMax(20,100);
			
			
			if (Title == "the Orphan" && m_type != 5)
				m_type = 5;
			
			else
			{
				if (Utility.RandomDouble() > 0.95)
					m_type = 4; // thief
				else
				{
					switch (Utility.Random(3))
					{
						case 0: m_type = 1; break; //standard
						case 1: m_type = 2; break; // annoying
						case 2: m_type = 3; break; // beggar
					}					
				}

			}
			switch (Utility.Random(4)) //randomize body type using the 4 animations on servuo
			{
				case 0: body = 1; Body = 0xA; Female = true; Name = NameList.RandomName( "Female" ); break;
				case 1: body = 2; Body = 0x24; Female = true; Name = NameList.RandomName( "Female" ); break; 
				case 2: body = 3; Body = 0x26; Female = false; Name = NameList.RandomName( "Male" ); break; 
				case 3: body = 4; Body = 0x28; Female = false; Name = NameList.RandomName( "Male" ); break; 
			}
			SetSkill(SkillName.DetectHidden, 0.0, 50.0);
			if (m_type == 4)
			{
				SetSkill(SkillName.Stealing, 30.0, 75.0);
				SetSkill(SkillName.Snooping, 30.0, 75.0);
			}
		}

		public override bool IsEnemy( Mobile m )
		{
		    return false;
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if (!(m is PlayerMobile) || m.Map == null || m == null || this.Map == null || this == null || m_childtarget != null || this.Controlled || m_type == null || m_talk == null) 
				return;

			if (m is PlayerMobile && m.AccessLevel == AccessLevel.Player && m_childtarget == null && ((Mobile)this).Combatant == null)
			{
				if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && !m.Hidden && m.Alive )
				{			
					if ((m_type == 2 || m_type == 3 || m_type == 4 ) && Utility.RandomDouble() > 0.85  ) // look to acquire a new target
							m_childtarget = m;									
								
					else if ( m_talk )
					{

						switch (Utility.Random(15) )
						{
									case 0: Say("Wow!  A knight!"); break;
									case 1: Say("I wanna be like him one day."); break;
									case 2: Say("It's " + m.Name + "!"); break;
									case 3: Say("You wear strange clothes. "  ); break;
									case 4: Say("An adventurer! An adventurer!"); break;
									case 5: Say("Daddy says you are a wimp, " + m.Name + "."); break;
									case 6: Say("When I grow up, ill be just like " + m.Name + "."); break;
									case 7: Say( m.Name + " is shorter than I imagined..."); break;
									case 8: Say("I want armor like that!"); break;
						}
							
						m_talk = false;
					}
				}
			}
		}				
	


		public override void OnThink()
		{
			if ( (m_type == 1 || m_type == 2 || m_type == 3) && annoyed == 0)
				m_type = 4; // turn them to a thief
			
			if (m_type == 4 && annoyed >= 20 && Utility.RandomDouble() < ( (double)annoyed/100))
				m_type = Utility.RandomMinMax(1,3);

			if (m_type == 0)
			{
				if (this.Title == "the Orphan")
					m_type = 5;
				else
					m_type = Utility.RandomMinMax(1,4);
			}
				

			if (m_type <= 4)
			{
				if (m_childtarget != null && m_childtarget is PlayerMobile ) // has a target
				{	
					if ( !InLOS( m_childtarget) || (int)this.GetDistanceToSqrt(m_childtarget) >  10 || m_childtarget.Hidden ) // lost the target
					{		
						if (m_childtarget.Hidden && (this.Skills[SkillName.DetectHidden].Value / 120) >= Utility.RandomDouble())	
						{
							m_childtarget.RevealingAction();
							this.Say("Found you!");
						}	
						else		
							m_childtarget = null;
					}
					if (m_childtarget != null && !m_childtarget.Alive)
						m_childtarget = null;
					
					if (m_childtarget != null && !this.Controlled)
					{
						((BaseCreature)this).Controlled = true;
						((BaseCreature)this).ControlOrder = OrderType.Follow;
						((BaseCreature)this).ControlTarget = m_childtarget;
					}
					
					if ( m_childtarget != null && (m_type == 1 || m_type == 4 && this.action))
						BeAnnoying(m_childtarget);
					if ( m_childtarget != null && (m_type == 2 && this.action)) // beggar
						SBegging(m_childtarget);
					if ( m_childtarget != null && (m_type == 4 && this.stealingtimer && Utility.RandomBool() && this.action )) // thief
						SSteal(m_childtarget);
						
				}

				else if(m_childtarget == null && this.Controlled )//no longer has a target, reset
				{
						((BaseCreature)this).Controlled = false;
						((BaseCreature)this).ControlOrder = OrderType.None;
						((BaseCreature)this).ControlTarget = null;				
				}
			}
			else if (m_type == 0 && (this.Title == null || this.Title == ""))
			{
				switch (Utility.Random(4) )
				{
					case 0: m_type = 1; break; //standard
					case 1: m_type = 2; break; // annoying
					case 2: m_type = 3; break; // beggar
					case 3: m_type = 4; break; //thief
				}
			}
			else if (m_type == 0 && this.Title != null && this.Title == "the Orphan")
			{
				m_type = 5;
				this.Title = "the Orphan";
			}

			if (m_type == 5  && this.m_freedom && this is Orphan ) // orphans
			{
				if ( ((BaseCreature)this).ControlMaster != null && ((Orphan)this).captured)
				{
					Mobile master = ((BaseCreature)this).ControlMaster;
					double freetest =  Math.Abs( ( ((Mobile)master).Karma - 15000) / 30000);
					
					if ( (!InRange( (Mobile)master, 6 ) && !InLOS( (Mobile)master )) || ((Mobile)master).Hidden) 
						freetest /= 1.25;
					
					if ( ((Mobile)master).Karma > 0)
						freetest /= 1+ (((Mobile)master).Karma / 15000);
					
					if (Utility.RandomDouble() > freetest) // test if gains freedom
					{
						((Orphan)this).captured = false;
						((BaseCreature)this).Controlled = false;
						((BaseCreature)this).SetControlMaster( null );
						((BaseCreature)this).ControlOrder = OrderType.None; 
						((BaseCreature)this).AIFullSpeedActive = true;
						((BaseCreature)this).AIFullSpeedPassive = true;	
						this.Say ("No... I must try to escape!");
					}					
					
					this.m_freedom = false;
				}
			}
			base.OnThink();
					
		}

		public void SBegging( Mobile target)
		{
			
			if ( m_talk && target != null && target is PlayerMobile && target.Name != null )
			{
				if (Utility.RandomBool())
					((BaseChild)this).annoyed -=1;

				switch (Utility.Random(12))
				{
							case 0: Say("Please!  Please give me some food!"); break;
							case 1: Say("A few coins Sire?"); break;
							case 2: Say("Please " + target.Name + ", im hungry!"); break;
							case 3: Say("a few coins will go a long way for me and my family, " + target.Name ); break;
							case 4: Say("I didn't eat yesterday...."); break;
							case 5: Say("Please " + target.Name + ", a few coins."); break;
							case 6: Say("My shoes are old and full of holes, " + target.Name + "."); break;
							case 7: Say("I have nothing, " + target.Name + "."); break;
							case 8: Say("Give me a few coins please."); break;
							case 9: Say("Why do you look so funny?"); break;
							case 10: Say("My mother is sick and needs medicine, help me..."); break;
							case 11: Say("I hear coins in that big backpack of yours."); break;
				}
					
				m_talk = false;
				((BaseChild)this).action = false;
				
			}			
			
			
		}
		
		public void SSteal( Mobile target)
		{
			// include random item stealing routine here from target's pack
			stealingtimer = false;
			Item stolen = null;
			bool caught = false;

			if ( target is PlayerMobile && InRange( target, 1 ) && InLOS( target ) )
			{

				if ( this.Skills[SkillName.Stealing].Value >= Utility.RandomMinMax( 1, 125 ) && this.Skills[SkillName.Snooping].Value >= Utility.RandomMinMax( 1, 100 ) && this.Skills[SkillName.Stealing].Value >= Utility.RandomMinMax( 1, (int)target.Skills[SkillName.Stealing].Value ))
				{
					if ( Utility.RandomMinMax( 1, 5 ) == 1 )
					{
						int c = 0;

						List<Item> belongings = new List<Item>();
						foreach( Item i in target.Backpack.Items )
						{
							if (  i.LootType != LootType.Blessed && i.TotalItems == 0 )
							{ belongings.Add(i); c++; }
						}

						int o = Utility.RandomMinMax( 0, c );

						foreach ( Item stuff in belongings )
						{
							o++;
							if ( c == o )
							{ 
								((BaseCreature)this).PackItem( stuff ); 
								m_childtarget = null; // they walk away after stealing an item
								if (Utility.RandomBool()) // later: add doby check for femal/male
                    				Name = NameList.RandomName("female");
								else 
                    				Name = NameList.RandomName("male");
								m_stole = target;
								switch (Utility.Random(6))
								{
											case 0: Say("You're boring."); break;
											case 1: Say("Bye!"); break;
											case 2: Say("Have a great day Sire!"); break;
											case 3: Say("Oh I thought you were my parent "); break;
											case 4: Say("Oh look over there!"); break;
											case 5: Say("You're no fun."); break;
								}
							}
						}
					}
				}
			}
			((BaseChild)this).action = false;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{  
			if (dropped == null || from == null || !(from is PlayerMobile) )
				return false;

			if (dropped is ChocolateMonster || dropped is WrappedCandy)
			{
				Item thing = null;
				foreach (Item item in this.Items)
				{
                    if (item != null)
					{
						thing = item;
					}
				}
				if (thing != null && from.Backpack != null && Utility.RandomBool())
				{
					from.Backpack.DropItem(thing);
					this.Say("Thank you Mister!  Here is something I found!");
				}
				else
					this.Say("Thank you Mister!");
				
				if (m_childtarget != null)
						m_childtarget = null; // leaves player alone

				dropped.Delete();
				return true;

			}

			if (m_type != null)
			{
				bool reward = false;
				int yay = 0;

						if (dropped is Gold  && dropped.Amount >5)
						{
							yay += dropped.Amount/5;
							if (Utility.RandomDouble() < ((double)dropped.Amount/7500))
								reward = true;
						}
						else if ( dropped is DDSilver && dropped.Amount >= 10)
						{
							yay += dropped.Amount/10;
							if (Utility.RandomDouble() < ((double)dropped.Amount/15000))
								reward = true;
						}
						else if ( (dropped is DDCopper || dropped.Amount >= 50 ) && dropped.Amount >= 50 )
						{
							yay += dropped.Amount/50;
							if (Utility.RandomDouble() < ((double)dropped.Amount/50000))
								reward = true;
						}
						else
						{
							yay += Utility.RandomMinMax(5,20);
							if (Utility.RandomDouble() < 0.05)
								reward = true;
						}

				if (reward)
				{
					Say("Thank you Sire!"); // success
					if (m_childtarget != null)
						m_childtarget = null; // leaves player alone

					if (m_childtarget == null) //charity, gets karma
						Titles.AwardKarma( m_childtarget, Utility.RandomMinMax(1,20), true );

					if (m_type == 4 && this.Backpack != null ) // thief, give something back
					{
						Item give = null;
						Container gab = this.Backpack;
						List<Item> list = gab.Items;
						if (list.Count >0)
						{
							int rng = Utility.RandomMinMax(1, list.Count);
							for ( int i = 0; i < list.Count; ++i )
							{
								if ((i+1) == rng)
									give = list[i];
							}
						}
						if (give != null)
						{
							Say("Here, I found this - you can have it!");
							Container newpack = from.Backpack;
							newpack.DropItem(give);
							from.SendMessage("The child nimbly slips something in your pack.");
						}
						else
							Say("I don't have anything to give you though.");

					}
				}
				else
				{
						if (dropped is Gold)
							Say("Oh look, " + dropped.Amount + " Gold!  I want more!"); // failure
						else if (dropped is Food)
							Say("I'm still hungry!");
						else if (dropped.Name != null)
							Say("Oh, " + dropped.Name + ", can I have something else?");
				}
				this.annoyed += yay;
	
				dropped.Delete();
				return true;
			}
			
			return false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.RevealingAction(); 
            if ( (from.AccessLevel > AccessLevel.Player || m_stole == from) && this.Backpack != null)
			{

				if ( from.InRange(this, 1) && InLOS( from ))
				{
					from.SendMessage("You grab the thief and look through his posessions!");
					this.Backpack.DisplayTo( from );
					int time = Utility.RandomMinMax(3,5);
					this.Paralyze( TimeSpan.FromSeconds( time ) );
					from.PlaySound( 0x204 );
				}

			}
			else if (Utility.RandomBool() && from.InRange(this, 1) && InLOS( from ) )
			{
				switch (Utility.Random(6))
								{
											case 0: Say("Help! this person's trying to molest me!"); break;
											case 1: Say("This person tried to touch my private parts!"); break;
											case 2: Say("AAAAAAAAAAAH!"); break;
											case 3: Say("Help! Help!"); break;
											case 4: Say("A stranger touched me!!"); break;
											case 5: Say("aah!"); break;
								}
				from.CriminalAction( true );
			}
		}

		public void BeAnnoying( Mobile target)
		{
			if (target == null)
				return;
			
			if ( m_talk && target.Name != null)
			{
				switch (Utility.Random(18))
				{
							case 0: Say("Why are you wearing that?"); break;
							case 1: Say("What does this do?"); break;
							case 2: Say("I bet you die a lot, " + target.Name + "."); break;
							case 3: Say("Why did you do that, " + target.Name + "??" ); break;
							case 4: Say("You have a funny hat!  It's ugly!"); break;
							case 5: Say("My father could have at thee, " + target.Name + ".  He's stronger!"); break;
							case 6: Say("You stink like an old cow,  " + target.Name + "."); break;
							case 7: Say("you're not that strong, " + target.Name + "."); break;
							case 8: Say("You stink!"); break;
							case 9: Say("You're ugly!"); break;
							case 10: Say("I bet you can't kill a mongbat!"); break;
							case 11: Say("Was your father a Mongbat?"); break;
							case 12: Say("An Imp is better looking than you - I've seen one."); break;
							case 13: Say("Why aren't you talking to me?"); break;
							case 14: Say("GIMME GIMME GIMME"); break;
							case 15: Say("Your mom was a Bog Thing and your dad was a slime!"); break;
							case 16: Say("I want something!"); break;
							case 17: Say("What is that you're wearing there?"); break;
							
				}
				if (Utility.RandomBool())
					annoyed -= 1;
					
				m_talk = false;
			}
			
			if (Utility.RandomDouble() > 0.80 && this.InRange( target, 1 ) )  // trip them
				target.Direction = target.GetDirectionTo( this );
				
			//else if (Utility.RandomDouble() > 0.80 && this.InRange( target, 1 ) ) // push them
			//	target.WalkRandom( 0, 3, 1); // BaseAI method -- nocompile
				
			// affect their pet (command.Stay)
			
			// make them unwear an item on a layer
			
			// swap gold for copper
			
			// other ideas?
			
			((BaseChild)this).action = false;
			
		}
		
    public override void OnSpeech( SpeechEventArgs e ) 
    {
      	if( e.Mobile.InRange( this, 4 ))
      	{
			if (m_type == 2 || (Utility.RandomBool() && m_type == 4) )
			{

				if ( ( e.Speech.ToLower() == "follow" ) )
				{
					Say("Follow me  me me me me me me me me !" );
				}
				else if ( ( e.Speech.ToLower() == "buy" ) )
				{
					Yell("Buy something for me!! Buy something for me!! Buy something for me!! Buy something for me!! Buy something for me!!" );
					Yell("NOOOOOW!!");
				}
				else if ( ( e.Speech.ToLower() == "sell" ) )
				{
					Yell("SELL!  SELL! SELL! WAAAAH NO DON'T SELL IT!");
				}
				else if ( ( e.Speech.ToLower() == "sell" ) )
				{
					Yell("NO!  I DON'T WANT TO STAY HERE!  NO NO NO NO NO!");
				}
				else if ( ( e.Speech.ToLower() == "go away" ) )
				{
					Yell("NO! I'm gonna follow you forever!");
				}
				else if ( ( e.Speech.ToLower() == "fuck you" ) )
				{
					Yell("No! Fuck YOU!");
				}
				else
				{
					switch (Utility.Random(7))
					{
								case 0: this.Say( "why " + e.Speech + "?" ); break;
								case 1: this.Say( e.Speech ); break;
								case 2: this.Yell(" Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah "); break;
								case 3: this.Yell(" Not " + e.Speech + " !"); break;
								case 4: this.Yell(" *high pitch voice* " + e.Speech + " !"); break;
								case 5: this.Yell( e.Speech + e.Speech +e.Speech +e.Speech +e.Speech +e.Speech ); break;
								case 6: this.Yell("I'll tell a guard you told said " + e.Speech + " !"); break;
					}

				}
				m_talk = false;	
			}						
      	}
    } 
		
		public override void OnDamagedBySpell(Mobile attacker)
        {
			// let them say sad things
			
			if (Utility.RandomDouble() > 0.80) //they hide
				this.Hidden = true;
			else if (this.Controlled)
			{
					m_childtarget = null;
				//this.FocusMob = attacker;
				//((BaseCreature)this).ControlOrder = OrderType.Attack;
				//BaseAI.DoActionFlee(); not working
			}
				
			base.OnDamagedBySpell(attacker);

		}
		
        public override void OnGotMeleeAttack(Mobile attacker)
        {
	
			// let them say sad things
			
			if (Utility.RandomDouble() > 0.80) //they hide
				this.Hidden = true;
				
			else if (this.Controlled)
			{	
					m_childtarget = null;
				//this.FocusMob = attacker;
				//DoActionFlee(); not working
				//((BaseCreature)this).ControlOrder = OrderType.Attack;
			}

			base.OnGotMeleeAttack(attacker);
		}

        public void OneTimeTick()
        {

			
			if (((BaseChild)this).talkingtimer >= 3 && !((BaseChild)this).talk ) // talking timer
			{
				((BaseChild)this).talk = true;
				((BaseChild)this).talkingtimer = 0;
				

			}
			else if ( !((BaseChild)this).talk && ((BaseChild)this).talkingtimer <= 10)
				((BaseChild)this).talkingtimer += 1;
			
			if ( !((BaseChild)this).stealingtimer && ((BaseChild)this).stealingcount >= Utility.RandomMinMax(5, 10))
			{
					((BaseChild)this).stealingtimer = true;
					((BaseChild)this).stealingcount = 0;
			}
			else if ( !((BaseChild)this).stealingtimer && ((BaseChild)this).stealingcount <= 20)
				((BaseChild)this).stealingcount += 1;

			if (((BaseChild)this).actioncount >= 3 && !((BaseChild)this).action ) // talking timer
			{
				((BaseChild)this).action = true;
				((BaseChild)this).actioncount = 0;
			}
			else if ( !((BaseChild)this).action && ((BaseChild)this).actioncount <= 10)
				((BaseChild)this).actioncount += 1;
			
			if ( this.m_type == 5 && ((BaseChild)this).freedcount >= 25 && !((BaseChild)this).freedom ) // freedom test for orphans
			{
				((BaseChild)this).freedom = true;
				((BaseChild)this).freedcount = 0;
			}
			else if ( !((BaseChild)this).freedom && ((BaseChild)this).freedcount <= 30)
				((BaseChild)this).freedcount += 1;

		}

 
		public override bool OnBeforeDeath()
		{
			if (Utility.RandomDouble() < 0.70)
			{
				// add heart?
			}
			
			return base.OnBeforeDeath();

		}
		
		
		public BaseChild( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 2 ); // version 
			writer.Write( (int) m_type );
			writer.Write( (int) body );
			writer.Write( (int) annoyed);

			if (m_stole == null)
				m_stole = this;
			
			writer.Write( (Mobile) m_stole );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
			int m_type = reader.ReadInt();
			int body = reader.ReadInt();
			
			if (version >= 1)
				annoyed = reader.ReadInt();
			
			if (version >= 2)
				m_stole = reader.ReadMobile();

			if (body == 0 || body == null)
				body = Utility.RandomMinMax(1,4);

			switch (body)
			{
				case 1: this.Body = 0xA; break; 
				case 2: this.Body = 0x24; break; 
				case 3: this.Body = 0x26; break; 
				case 4: this.Body = 0x28; break; 
			}

			if (this.Title == "the Orphan" && m_type != 5)
				m_type = 5;

			else if (m_type == 0)
			{
				m_type = Utility.RandomMinMax(1, 4);
			}
			
			m_OneTimeType = 3;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

	}
}

