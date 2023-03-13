using System; 
using Server;
using System.Collections; 
using System.Collections.Generic;
using Server.Targeting;
using Server.Misc; 
using Server.Items; 
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Mobiles; 


namespace Server.Mobiles 
{ 
    public class CalypsoGuard : BaseBlue
    { 
	private bool m_Bandaging;
	public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
	public DateTime m_NextTalk;

	public override void OnMovement( Mobile m, Point3D oldLocation )
	{

	    if ( !( m is PlayerMobile ) )
			return;

		if (!CheckReputation(((PlayerMobile)m)))
			return;

	    if ((m is PlayerMobile) && (m.AccessLevel == AccessLevel.Player))
	    {
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
			{
				if ( !m.Hidden && DateTime.UtcNow >= m_NextResurrect && this.HealsYoungPlayers && m.Hits < (m.HitsMax/2) && m is PlayerMobile || DateTime.UtcNow >= m_NextResurrect && m.Hits < (m.HitsMax/2) && m is BaseBlue )
				{
				OfferHeal( (PlayerMobile) m );
				}
				else if ( !m.Hidden && this.Combatant == null && DateTime.UtcNow >= m_NextTalk ) // check if its time to talk
				{
				m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
				switch (Utility.Random(6))
				{
					case 0: Emote("Greets, " + m.Name + " "); break;
					case 1: Emote("" + m.Name + ""); break;
					case 2: Emote("Move along," + m.Name ); break;
					case 3: Emote("Behave here " + m.Name ); break;
					case 4: Emote("We keep the peace here."); break;
					case 5: Emote("*nods*"); break;
				}
				}
			}
	    }

	}


	[Constructable] 
	    public CalypsoGuard() : base( AIType.AI_Melee, FightMode.Closest, 25, 1, 0.4, 0.3 ) 
	{
		((BaseCreature)this).midrace =4;

			AIFullSpeedActive = true; // Force full speed
			AIFullSpeedPassive = false;		
	    
	    SetStr(400, 550);
	    SetDex(150, 200);
	    SetInt(60, 100);


	    SetHits(200, 300);

	    SetDamage(40, 75);

	    SetDamageType(ResistanceType.Physical, 100);

	    SetResistance(ResistanceType.Physical, 50, 70);
	    SetResistance(ResistanceType.Fire, 50, 70);
	    SetResistance(ResistanceType.Cold, 50, 70);
	    SetResistance(ResistanceType.Poison, 50, 70);
	    SetResistance(ResistanceType.Energy, 50, 70);

	    SetSkill(SkillName.Swords, 89.0, 120.0);
	    SetSkill(SkillName.Tactics, 89.0, 120.0);
	    SetSkill(SkillName.MagicResist, 89.0, 120.0);
	    SetSkill(SkillName.Tactics, 89.0, 120.0);
	    SetSkill(SkillName.Parry, 89.0, 120.0);
	    SetSkill(SkillName.Anatomy, 85.0, 120.0);
	    SetSkill(SkillName.Healing, 85.0, 120.0);
	    SetSkill(SkillName.Magery, 85.0, 120.0);
	    SetSkill(SkillName.EvalInt, 85.0, 120.0);

	    if (Utility.Random(1, 2) == 2) // 50% chance to have an OmniAI skill set
		OmniAI.SetRandomSkillSet(this, 70.0, 110.0);

		
	    Fame = 10000;
	    Karma = 7500;

	    VirtualArmor = 60;

	    Utility.AssignRandomHair( this );

	    for (int i = 0; i < 10; i++)
	    {
			PackItem( new GreaterCurePotion() );
			PackItem( new GreaterHealPotion() );
			PackItem( new TotalRefreshPotion() );
	    }

	    PackItem(new Bandage(Utility.RandomMinMax(20, 35)));
		

	}

	// + OmniAI support +
	protected override BaseAI ForcedAI
	{
	    get
	    {
		return new OmniAI(this);
	    }
	}
	// - OmniAI support -

	public override void OnAfterSpawn()
	{
	    base.OnAfterSpawn();
    		Region reg = Region.Find( this.Location, this.Map );

			string World = Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );

			int clothColor = 0;
			int shieldType = 0;
			int helmType = 0;
			int cloakColor = 0;

			Item weapon = new VikingSword(); weapon.Delete();


				Title = "[Calypso Guard]";				
				if ( Female = Utility.RandomBool() ) 
				{ 
					Body = 401; //606 for elf
					Name = NameList.RandomName( "female" );	
				}
				else 
				{ 
					Body = 400; 	//605 for elf		
					Name = NameList.RandomName( "male" ); 
				}	
			

			{
				clothColor = 0x455;		shieldType = 0x2FC9;	helmType = 0x140E;		cloakColor = 0x34;		weapon = new Cutlass();   
			}

			weapon.Movable = true;
			((BaseWeapon)weapon).MaxHitPoints = 250;
			((BaseWeapon)weapon).HitPoints = 100;
			((BaseWeapon)weapon).MinDamage = 10;
			((BaseWeapon)weapon).MaxDamage = 25;
			AddItem( weapon );

			AddItem( new PlateChest() );
			AddItem( new PlateArms() );
			AddItem( new PlateLegs() );
			AddItem( new PlateGorget() );
			AddItem( new PlateGloves() );
			AddItem( new Boots( ) );
            Item robe = new PirateRobe(); robe.Hue = 0x455; AddItem(robe);

            MorphingTime.ColorMyClothes( this, clothColor );

			if ( shieldType > 0 )
			{
				SmallPlateShield shield = new SmallPlateShield();
					shield.ItemID = shieldType;
					shield.Name = "shield";
					AddItem( shield );
			}

			if ( cloakColor > 0 )
			{
				Cloak cloak = new Cloak();
					cloak.Hue = cloakColor;
					AddItem( cloak );
			}

			IntelligentAction.MidlandRace((BaseCreature)this);
			

	}

	public override void GenerateLoot()
	{
	    AddLoot( LootPack.Average );
	}

	public override bool CanRummageCorpses{ get{ return true; } }
	public override bool CanHeal { get { return true; } }
			public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is PirateBounty )
			{
				if ( IntelligentAction.GetMyEnemies( from, this, false ) == true )
				{
					string sSay = "You shouldn't be carrying that around with you.";
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sSay, from.NetState);
				}
				else
				{
					PirateBounty bounty = (PirateBounty)dropped;
					int fame = (int)(bounty.BountyValue/5);
					int karma = -1*fame;
					int gold = bounty.BountyValue;
					string sMessage = "";
					string sReward = "Here is " + gold.ToString() + " gold for you.";

					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 0:	sReward = "Here is " + gold.ToString() + " gold for you.";							break;
						case 1:	sReward = "Take this " + gold.ToString() + " gold for your trouble.";				break;
						case 2:	sReward = "The reward is " + gold.ToString() + " gold.";							break;
						case 3:	sReward = "Here is " + gold.ToString() + " gold for the bounty.";					break;
						case 4:	sReward = "The bounty is " + gold.ToString() + " gold for this one.";				break;
						case 5:	sReward = "Here is your reward of " + gold.ToString() + " gold";					break;
						case 6:	sReward = "You can have this " + gold.ToString() + " gold for the bounty.";			break;
						case 7:	sReward = "There is a reward of " + gold.ToString() + " gold for this one.";		break;
						case 8:	sReward = "This one was worth " + gold.ToString() + " gold for their crimes.";		break;
						case 9:	sReward = "Their crimes called for a bounty of " + gold.ToString() + " gold.";		break;
					}

					Titles.AwardKarma( from, karma, true );
					Titles.AwardFame( from, fame, true );
					from.SendSound( 0x2E6 );
					from.AddToBackpack ( new Gold( gold ) );

					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 0:	sMessage = "We have been looking for this pirate. " + sReward;	break;
						case 1:	sMessage = "I have heard of this pirate before. " + sReward;	break;
						case 2:	sMessage = "I never thought I would see this pirate brought to justice. " + sReward;	break;
						case 3:	sMessage = "This pirate will plunder no more. " + sReward;	break;
						case 4:	sMessage = "Our galleons are safer now. " + sReward;	break;
						case 5:	sMessage = "The sea is safer because of you. " + sReward;	break;
						case 6:	sMessage = "The sailors at the docks will not believe this. " + sReward;	break;
						case 7:	sMessage = "I have only heard stories about this pirate. " + sReward;	break;
						case 8:	sMessage = "How did you come across this pirate? " + sReward;	break;
						case 9:	sMessage = "Where did you find this pirate? " + sReward;	break;
					}
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
					dropped.Delete();
					return true;
				}
			}
			else if ( dropped is Head && !from.Blessed )
			{
				if ( IntelligentAction.GetMyEnemies( from, this, false ) == true )
				{
					string sSay = "You shouldn't be carrying that around with you.";
					this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sSay, from.NetState);
				}
				else
				{
					Head head = (Head)dropped;
					int karma = 0;
					int gold = 0;
					string sMessage = "";
					string sReward = "Here is " + gold.ToString() + " gold for you.";

					if ( head.m_Job == "Thief" )
					{
						karma = Utility.RandomMinMax( 20, 30 );
						gold = Utility.RandomMinMax( 40, 60 );
					}
					else if ( head.m_Job == "Bandit" )
					{
						karma = Utility.RandomMinMax( 10, 15 );
						gold = Utility.RandomMinMax( 15, 30 );
					}
					else if ( head.m_Job == "Brigand" )
					{
						karma = Utility.RandomMinMax( 15, 20 );
						gold = Utility.RandomMinMax( 25, 50 );
					}
					else if ( head.m_Job == "Pirate" )
					{
						karma = Utility.RandomMinMax( 45, 55 );
						gold = Utility.RandomMinMax( 60, 85 );
					}
					else if ( head.m_Job == "Assassin" )
					{
						karma = Utility.RandomMinMax( 30, 175 );
						gold = Utility.RandomMinMax( 100, 150 );
					}
					else if ( head.m_Job == "Mounted Player Killer" )
					{
						karma = Utility.RandomMinMax( 100, 250 );
						gold = Utility.RandomMinMax( 375, 750 );
					}
					else if ( head.m_Job == "Player Killer" )
					{
						karma = Utility.RandomMinMax( 25, 125 );
						gold = Utility.RandomMinMax( 100, 250 );
					}
					else if ( head.m_Job == "Mage" )
					{
						karma = Utility.RandomMinMax( 25, 50 );
						gold = Utility.RandomMinMax( 40, 100 );
					}
					else if ( head.m_Job == "Bard" )
					{
						karma = Utility.RandomMinMax( 25, 50 );
						gold = Utility.RandomMinMax( 40, 100 );
					}
					else if ( head.m_Job == "MageLord" )
					{
						karma = Utility.RandomMinMax( 50, 100 );
						gold = Utility.RandomMinMax( 75, 175 );
					}
					else if ( head.m_Job == "Monk" )
					{
						karma = Utility.RandomMinMax( 100, 250 );
						gold = Utility.RandomMinMax( 25, 40 );
					}
					else if ( head.m_Job == "Executioner" )
					{
						karma = Utility.RandomMinMax( 50, 100 );
						gold = Utility.RandomMinMax( 50, 75 );
					}
					else if ( head.m_Job == "Warrior" )
					{
						karma = Utility.RandomMinMax( 25, 50 );
						gold = Utility.RandomMinMax( 25, 50 );
					}
					else if ( head.m_Job == "Knight" )
					{
						karma = Utility.RandomMinMax( 100, 250 );
						gold = Utility.RandomMinMax( 100, 150 );
					}
					else if ( head.m_Job == "Cultist" )
					{
						karma = Utility.RandomMinMax( 100, 250 );
						gold = Utility.RandomMinMax( 25, 50 );
					}
					else if ( head.m_Job == "Controller" )
					{
						karma = Utility.RandomMinMax( 100, 250 );
						gold = Utility.RandomMinMax( 100, 250 );
					}					

					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 0:	sReward = "Here is " + gold.ToString() + " gold for you.";							break;
						case 1:	sReward = "Take this " + gold.ToString() + " gold for your trouble.";				break;
						case 2:	sReward = "The reward is " + gold.ToString() + " gold.";							break;
						case 3:	sReward = "Here is " + gold.ToString() + " gold for the bounty.";					break;
						case 4:	sReward = "The bounty is " + gold.ToString() + " gold for this one.";				break;
						case 5:	sReward = "Here is your reward of " + gold.ToString() + " gold";					break;
						case 6:	sReward = "You can have this " + gold.ToString() + " gold for the bounty.";			break;
						case 7:	sReward = "There is a reward of " + gold.ToString() + " gold for this one.";		break;
						case 8:	sReward = "This one was worth " + gold.ToString() + " gold for their crimes.";		break;
						case 9:	sReward = "Their crimes called for a bounty of " + gold.ToString() + " gold.";		break;
					}


					if ( head.m_Job == "Thief" || head.m_Job == "Bandit" || head.m_Job == "Brigand" )
					{
						//if (head.m_killer != null && head.m_killer == from)
						{
							Titles.AwardKarma( from, karma, true );
							Titles.AwardFame( from, karma, true );
						}
						from.SendSound( 0x2E6 );
						from.AddToBackpack ( new Gold( gold ) );

						switch ( Utility.RandomMinMax( 0, 9 ) )
						{
							case 0:	sMessage = "We have been looking for this rogue. " + sReward;	break;
							case 1:	sMessage = "I have heard of this thief before. " + sReward;	break;
							case 2:	sMessage = "I never thought I would see this bandit brought to justice. " + sReward;	break;
							case 3:	sMessage = "This rouge will steal no more. " + sReward;	break;
							case 4:	sMessage = "Our gold purses are safer now. " + sReward;	break;
							case 5:	sMessage = "The land is safer because of you. " + sReward;	break;
							case 6:	sMessage = "The others at the guard house will not believe this. " + sReward;	break;
							case 7:	sMessage = "I have only heard stories about this rogue. " + sReward;	break;
							case 8:	sMessage = "How did you come across this thief? " + sReward;	break;
							case 9:	sMessage = "Where did you find this sneak? " + sReward;	break;
						}

						//if (head.m_killer != null && head.m_killer != from)
							sMessage = "Doesn't look like you delivered the final blow, but I can still give you the gold. " + sReward;

						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else if ( head.m_Job == "Pirate" )
					{
						//if (head.m_killer != null && head.m_killer == from)
						{
							Titles.AwardKarma( from, karma, true );
							Titles.AwardFame( from, karma, true );
						}
						from.SendSound( 0x2E6 );
						from.AddToBackpack ( new Gold( gold ) );

						switch ( Utility.RandomMinMax( 0, 9 ) )
						{
							case 0:	sMessage = "We have been looking for this pirate. " + sReward;	break;
							case 1:	sMessage = "I have heard of this pirate before. " + sReward;	break;
							case 2:	sMessage = "I never thought I would see this pirate brought to justice. " + sReward;	break;
							case 3:	sMessage = "This pirate will plunder no more. " + sReward;	break;
							case 4:	sMessage = "Our galleons are safer now. " + sReward;	break;
							case 5:	sMessage = "The sea is safer because of you. " + sReward;	break;
							case 6:	sMessage = "The sailors at the docks will not believe this. " + sReward;	break;
							case 7:	sMessage = "I have only heard stories about this pirate. " + sReward;	break;
							case 8:	sMessage = "How did you come across this pirate? " + sReward;	break;
							case 9:	sMessage = "Where did you find this pirate? " + sReward;	break;
						}

						//if (head.m_killer != null && head.m_killer != from)
							sMessage = "Doesn't look like you delivered the final blow, but I can still give you the gold. " + sReward;

						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else if ( head.m_Job == "Assassin" || head.m_Job == "Player Killer" || head.m_Job == "Mounted Player Killer" )
					{
						//if (head.m_killer != null && head.m_killer == from)
						{
							Titles.AwardKarma( from, karma, true );
							Titles.AwardFame( from, karma, true );
						}
						from.SendSound( 0x2E6 );
						from.AddToBackpack ( new Gold( gold ) );

						switch ( Utility.RandomMinMax( 0, 9 ) )
						{
							case 0:	sMessage = "We have been living in fear of this one. " + sReward;	break;
							case 1:	sMessage = "I have heard others speak of this assassin. " + sReward;	break;
							case 2:	sMessage = "I never thought this assassin existed. " + sReward;	break;
							case 3:	sMessage = "This assassin will kill no more. " + sReward;	break;
							case 4:	sMessage = "Our nobles are safer now. " + sReward;	break;
							case 5:	sMessage = "The shadows are less feared because of you. " + sReward;	break;
							case 6:	sMessage = "Those in the tavern will not believe this. " + sReward;	break;
							case 7:	sMessage = "I have only heard rumors about this assassin. " + sReward;	break;
							case 8:	sMessage = "It is good to see this assassin did not best you. " + sReward;	break;
							case 9:	sMessage = "How did you survive this assassin? " + sReward;	break;
						}

						//if (head.m_killer != null && head.m_killer != from)
							sMessage = "Doesn't look like you delivered the final blow, but I can still give you the gold. " + sReward;

						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
						dropped.Delete();
						return true;
					}
					else
					{
						this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I assume they done you harm. Let me rid you of this thing.", from.NetState);
						dropped.Delete();
						return true;
					}
				}
			}
			
		return base.OnDragDrop( from, dropped );			
		}
		
			public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

			public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The Duties Of The Guard", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Guard" ) ));
					}
				}

				ArrayList wanted = new ArrayList();
				int w = 0;
				if (mobile == null || mobile.BankBox == null) return;
				foreach ( Item item in mobile.BankBox.Items )
				{
					if ( item is CharacterDatabase )
					{
						CharacterDatabase DB = (CharacterDatabase)item;

						if ( DB.CharacterWanted != null && DB.CharacterWanted != "" )
						{
							wanted.Add( item );
							w++;
						}
					}
				}
				int wChoice = Utility.RandomMinMax( 1, w );
				int c = 0;
				for ( int i = 0; i < wanted.Count; ++i )
				{
					c++;
					if ( c == wChoice )
					{
						CharacterDatabase DB = ( CharacterDatabase )wanted[ i ];
						GuardNote note = new GuardNote();
						note.ScrollText = DB.CharacterWanted;
						m_Mobile.AddToBackpack( note );
						m_Giver.Say("Here is a note citizen. Be on the lookout.");
					}
				}
            }
        }	

		public bool CheckReputation(PlayerMobile from)
		{
			if (!from.Criminal)
				return true; // for now

			else
			{
				Say("Guards!! Guards!!");
				foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
				{
					if ( m is CalypsoGuard )
					{
						((BaseCreature)m).FocusMob = from;
						m.Combatant = from;
						m.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Stop! You there!" ) ); 
					}

				}				


			}
			return false;

		}

	public CalypsoGuard( Serial serial ) : base( serial ) 
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

		AIFullSpeedActive = true; // Force full speed
		AIFullSpeedPassive = false;
	} 

    } 
}   
  
