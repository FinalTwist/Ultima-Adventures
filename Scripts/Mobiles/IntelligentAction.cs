using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using System.Text;
using System.IO;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
	public class HeldLight : BaseEquipableLight
	{
		public override int LitItemID{ get { return 0xA22; } }
		public override int UnlitItemID{ get { return 0xA22; } }

		[Constructable]
		public HeldLight() : base( 0xA22 )
		{
			Name = "lantern";
			Duration = TimeSpan.Zero;
			Burning = true;
			Light = LightType.Circle300;
			Weight = 2.0;
			//LootType = LootType.Blessed;

			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: Name = "torch";		ItemID = 0xA12;		Light = LightType.Circle300;	break;
				case 1: Name = "candle";	ItemID = 0xA0F;		Light = LightType.Circle150;	break;
			}
		}

		public override bool DisplayLootType{ get{ return false; } }

		public HeldLight( Serial serial ) : base( serial )
		{
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

namespace Server.Misc
{
    class IntelligentAction
    {
    	public static int RogueHues() {
    		return Utility.RandomList( 0x973, 0x966, 0x96D, 0x972, 0x8A5, 0x979, 0x89F, 0x8AB, 0x66D, 0x8A8, 0x455, 0x851, 0x8FD, 0x8B0, 0x283, 0x227, 0x1C1, 0x8AC, 0x845, 0x851, 0x497, Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue() ); 
    	}	
		public static bool GetMyEnemies( Mobile m, Mobile me, bool checkDisguise )
		{
			if (AdventuresFunctions.IsPuritain((object)me) && me is BaseCreature && ((BaseCreature)me).midrace > 0)
				return RaceCheck (me, m);

			bool enemy = true;

			Region reg = Region.Find( me.Location, me.Map );

			if ( !m.Criminal && ( DisguiseTimers.IsDisguised( m ) || Server.Spells.Fifth.IncognitoSpell.m_Timers.Contains(m)))
				return false;

			if ( m is PlayerMobile && !m.Criminal && Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "Doom Gauntlet" )
				return false;

			if ( (me is BasePerson || me is BaseVendor || me is PlayerVendor) && ( (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "DarkMoor") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Temple of Praetoria") ))
			{
				
				if ( m is BaseCreature && ((BaseCreature)m).ControlMaster != null && m.Combatant != me)
					{
					Mobile owner = ((BaseCreature)m).ControlMaster;
					if ( owner is PlayerMobile && owner.Karma < 0)
						return false;
					else 
						return true;
					} 
				else if ( m.Karma < 0)
					return false;
			
				else if ( m.Karma >= 0)
					return true;
			}

			if ( m is PlayerMobile )
			{
				if (m.Criminal && me is BaseVendor)
					return true;
				
				if ( ((PlayerMobile)m).Profession == 1 )
				{
					m.Criminal = true;
					if ( m.Kills < 1 ){ m.Kills = 1; }
				}
			}

			if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && ( GetPlayerInfo.EvilPlayer( m ) || m is BaseCreature ) )
				return false;

			if ( !(me.CanSee( m )) || !(me.InLOS( m )) )
				return false;

			if ( m.AccessLevel > AccessLevel.Player )
				return false;

			if ( m is PlayerMobile && me is BaseVendor && (  (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Basement") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "The Pit") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Town of Skara Brae") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "Lamut County") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Ruins of Tenebrae") || reg.IsPartOf(typeof(UmbraRegion) ) || reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)) ) && ( m.Kills > 2 || m.Karma <=  -5000) || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "Arena Royale") )
				return false;

			if ( m is BasePerson || m is BaseVendor || m is PlayerVendor || m is BaseBlue || m is Townsperson || m is Citizens || m is PlayerBarkeeper )
				return false;

			if ( m is BaseCreature && ((BaseCreature)m).FightMode == FightMode.Evil ) 
				return false;

			if ( m.Region.IsPartOf( typeof( PublicRegion ) ) )
				return false;

			if ( m.Region.IsPartOf( typeof( StartRegion ) ) )
				return false;

			if ( m.Region.IsPartOf( typeof( SafeRegion ) ) )
				return false;

			if ( m.Region.IsPartOf( typeof( ProtectedRegion ) ) )
				return false;

			if ( m is PlayerMobile && !m.Criminal && m.Kills<1 )
				enemy = false;

			if ( ( DisguiseTimers.IsDisguised( m ) || Server.Spells.Fifth.IncognitoSpell.m_Timers.Contains(m)) && checkDisguise )
				enemy = false;

			if ( m is PlayerMobile && m.Karma <= -5000 && m.Skills[SkillName.Chivalry].Base >= 50 && !m.Region.IsPartOf(typeof(UmbraRegion)) && !m.Region.IsPartOf(typeof(NecromancerRegion)) )
				enemy = true; // DEATH KNIGHTS ARE NOT WELCOME AFTER THIS POINT...EXCEPT IN UMBRA OR RAVENDARK

			if ( m is PlayerMobile && m.Karma <= -5000 && m.Skills[SkillName.EvalInt].Base >= 50 && Server.Misc.GetPlayerInfo.isSyth(m, false) && !m.Region.IsPartOf(typeof(UmbraRegion)) && !m.Region.IsPartOf(typeof(NecromancerRegion)) )
				enemy = true; // SYTH ARE NOT WELCOME AFTER THIS POINT...EXCEPT IN UMBRA OR RAVENDARK

			if (m is BaseCreature)
			{
				BaseCreature c = (BaseCreature)m;
				if ( c.Controlled || c.FightMode == FightMode.Aggressor || c.FightMode == FightMode.None )
					enemy = false;
			}	

			return enemy;
		}

		public static void AssignMidlandRace(BaseCreature m)
		{
			if ( m.Map == null)
				return;

				
					Region reg = Region.Find( m.Location, m.Map );

					if ( reg.IsPartOf( "the city of Midkemia" ) || ( (m.X > 2464 && m.X < 2855 ) && (m.Y > 661 && m.Y < 1123 ) ) )
					{
						((BaseCreature)m).midrace = 1;
						if (m is MidlandVendor)
							((MidlandVendor)m).moneytype = 1;
					}
					//else if ( reg.IsPartOf( "Midkemia" )
					//	m.midrace = 2;
					//else if ( reg.IsPartOf( "Midkemia" )
					//	m.midrace = 3;
					else if ( reg.IsPartOf( "the port of Calypso" ))
					{
						((BaseCreature)m).midrace = 4;
						if (m is MidlandVendor)
							((MidlandVendor)m).moneytype = 4;
					}
					else if ( reg.IsPartOf( "Ha'ars Crag" ))
					{
						((BaseCreature)m).midrace = 5;
						if (m is MidlandVendor)
							((MidlandVendor)m).moneytype = 5;
					}
					else if ( reg.IsPartOf( "Freeport" ))
					{
						if (m is MidlandVendor)
						{
							((BaseCreature)m).midrace = Utility.RandomMinMax(1, 4);

							if (((BaseCreature)m).midrace == 1)
								((MidlandVendor)m).moneytype = 1;
							if (((BaseCreature)m).midrace == 2)
								((MidlandVendor)m).moneytype = 2;
							if (((BaseCreature)m).midrace == 3)
								((MidlandVendor)m).moneytype = 3;
							if (((BaseCreature)m).midrace == 4)
								((MidlandVendor)m).moneytype = 4;
						}
					}


		}

		public static bool RaceCheck(Mobile m, Mobile other)
		{

		//1 humans
		//2 gargoyles
		//3 lizards
		//4 pirates
		//5 orcs

			if (!(m is BaseCreature) ) // only BC should be sent here as m
				return false;
			
			if (((BaseCreature)m).midrace == 0) // this is not a racial mob
				return false;

			Region reg = Region.Find( m.Location, m.Map );

			if ( reg.IsPartOf( "Freeport" ) && !other.Criminal)
				return false;

			if (other is BaseCreature) // is other just a pet?
			{
				BaseCreature bc = (BaseCreature)m;
					if(bc.Summoned)
					{
						if(bc.SummonMaster != null)
							other = bc.SummonMaster;
					}
					else if(bc.Controlled)
					{
						if(bc.ControlMaster != null)
							other=bc.ControlMaster;
					}
					else if(bc.BardProvoked)
					{
						if(bc.BardMaster != null)
							other=bc.BardMaster;
					}
			}
			//now that real actors are identified, check reputation
			bool enemy = false;
			int otherreputation = 0;
			int otherrace = 0;

			if (other is PlayerMobile )//set variables
			{
				PlayerMobile pm = (PlayerMobile)other;
				if (pm.midrace != ((BaseCreature)m).midrace)
					enemy = true; // does not mean is enemy, may just indicate possible conflict

				if (((BaseCreature)m).midrace == 1)//human
					otherreputation = pm.midhumans;
				if (((BaseCreature)m).midrace == 2)//garg
					otherreputation = pm.midgargoyles;
				if (((BaseCreature)m).midrace == 3)//lizard
					otherreputation = pm.midlizards;
				if (((BaseCreature)m).midrace == 4)//pirates
					otherreputation = pm.midpirates;
				if (((BaseCreature)m).midrace == 5)//orc
					otherreputation = pm.midorcs;

				otherrace = pm.midrace;
			}
			else if (other is BaseCreature )//set variables
			{
				BaseCreature pm = (BaseCreature)other;
				if (pm.midrace != ((BaseCreature)m).midrace)
					enemy = true; // does not mean is enemy, may just indicate possible conflict

				otherrace = pm.midrace;
				otherreputation = -500;  // assumed other races hate each other
			}

			//factor in regional exceptions (freeport)
			Region region = Region.Find( m.Location, m.Map );
			if (region.IsPartOf( "Freeport" ) && m.Combatant != other )
			{
				if (other.Criminal)
					return true;
				else
					return false;
			}

			//do racial mortal enemies check
			if (((BaseCreature)m).midrace == 1 && otherrace == 2)//human vs gargs
					return true;
			if (((BaseCreature)m).midrace == 2 && otherrace == 1)//garg vs humans
					return true;
			if (((BaseCreature)m).midrace == 3 && otherrace == 4)//lizard vs pirates
					return true;
			if (((BaseCreature)m).midrace == 4 && otherrace == 3)//pirates vs lizards
					return true;

			//begin player reputation check
			if (other is PlayerMobile && otherrace != ((BaseCreature)m).midrace)
			{
				if (otherreputation <= -250) //bad reputation
					return true;
				else if (otherreputation >= 250) // good repuation
					return false;
				else if (otherreputation > -250 && otherrace < 0) // negative greyzone
				{
					enemy = false;
						if (Utility.RandomBool())//not to make this too frequent?
						{
							switch ( Utility.Random( 5 ) )
							{
								case 0: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*looks at " + other.Name + " disaprovingly*")); break;
								case 1: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*looks at " + other.Name + " and frowns*")); break;
								case 2: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*glances at " + other.Name + " and shakes its head*")); break;
								case 3: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*shakes its fist at " + other.Name + "*")); break;
								case 4: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*frowns at " + other.Name + "*")); break;
							}
						}
				}
				else if (otherreputation < 250 && otherrace > 0) // positive greyzone
				{
					enemy = false;
						if (Utility.RandomBool())//not to make this too frequent?
						{
							switch ( Utility.Random( 5 ) )
							{
								case 0: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*nods in " + other.Name + "'s direction*")); break;
								case 1: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*smiles at " + other.Name + "*")); break;
								case 2: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*waves to " + other.Name + "*")); break;
								case 3: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*looks at " + other.Name + " and nods*")); break;
								case 4: m.PublicOverheadMessage(MessageType.Regular, 0, false, string.Format ("*greets " + other.Name + "*")); break;
							}
						}
				}
			}


			if (enemy)
				return true;

			return false;
		}

		public static void MidlandRace(BaseCreature m)
		{
		//1 humans
		//2 gargoyles
		//3 lizards
		//4 pirates
		//5 orcs

			Item helm = m.FindItemOnLayer( Layer.Helm );
			if (helm != null && m.midrace > 1)
				helm.Delete();

			if (m.midrace == 1)
			{
				//humans, basic hues
				m.Hue = Server.Misc.RandomThings.GetRandomSkinColor();

				if ( !(m is MidlandVendor) && !(m is MidlandGuard) )
				{
					m.Title = "";

					if ( Utility.RandomDouble() < 0.20)
					{
						int f = 0;
						if (m.Female)
							f = 1;
						m.Title = RandomThings.GetBoyGirlJob(f) + " of the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom();
					}
				}
				if (m is MidlandVendor)
					((MidlandVendor)m).moneytype = 1;


			}
			else if (m.midrace == 2) // gargoyles
			{
				m.HairItemID = 0;
				m.FacialHairItemID = 0;

				int h = Utility.RandomMinMax(132, 140);
				m.Hue = h;

				Item wings = new WornHumanDeco();
				wings.Name = "gargoyle wings";
				wings.ItemID = 0x2FC5;
				wings.Hue = h;
				wings.Layer = Layer.Cloak;
				m.AddItem( wings );
				wings.Movable = false;

				GargoyleCowl mask = new GargoyleCowl();
				mask.Hue = h;
				mask.Layer = Layer.Helm;
				m.AddItem( mask );
				mask.Movable = false;

				if ( m.Female )
				{
					m.Name = NameList.RandomName( "gargoyle name" );
				}
				else
				{
					m.Name = NameList.RandomName( "gargoyle vendor" );
				}
				if (m is MidlandVendor)
					((MidlandVendor)m).moneytype = 2;
			}
			else if (m.midrace == 3) // lizards
			{
				m.HairItemID = 0;
				m.FacialHairItemID = 0;

				int h = Utility.RandomMinMax(456, 465);
				LizardCowl mask = new LizardCowl();
				mask.Hue = h;
				m.Hue = h;
				m.AddItem( mask );
				mask.Movable = false;

				if ( m.Female )
				{
					m.Name = NameList.RandomName( "lizardman" );
				}
				else
				{
					m.Name = NameList.RandomName( "lizardman" );
				}

				if (Utility.RandomDouble() < 0.20)
					m.Title = "Al Ah" + NameList.RandomName( "lizardman" );
				
				if (m is MidlandVendor)
					((MidlandVendor)m).moneytype = 3;

			}
			else if (m.midrace == 4)// pirates
			{
				m.HairItemID = 0;
				m.FacialHairItemID = 0;

				int h = Utility.RandomMinMax(440, 450);
				CalypsoCowl mask = new CalypsoCowl();
				mask.Hue = h;
				m.Hue = h;
				m.AddItem( mask );
				mask.Movable = false;

				m.Name = NameList.RandomName( "drakkul" );

				if (Utility.RandomDouble() < 0.20)
					m.Title = " from the " + RandomThings.GetRandomShipName( RandomThings.GetRandomWeapon(), 0 );

				if (m is MidlandVendor)
					((MidlandVendor)m).moneytype = 4;

			}
			else if (m.midrace == 5)// orcs
			{
				m.HairItemID = 0;
				m.FacialHairItemID = 0;

				int h = Utility.RandomMinMax(1126, 1133);
				Item mask = null;

				if (Utility.RandomBool())
					mask = new OrcCowl();
				else
					mask = new OrcHelm();

				mask.Hue = h;
				m.Hue = h;
				m.AddItem( mask );
				mask.Movable = false;

				m.BaseSoundID = 0x45A;

				if ( m.Female )
				{
					m.Name = NameList.RandomName( "ork_female" );
				}
				else
				{
					m.Name = NameList.RandomName( "ork_male" );
				}

				if (Utility.RandomDouble() < 0.20)
					m.Title = "of the " + RandomThings.GetRandomDisaster();

				if (m is MidlandVendor)
					((MidlandVendor)m).moneytype = 5;
			}			
		}

		public static bool GetMidlandEnemies( Mobile m, Mobile me, bool checkDisguise )
		{
			bool enemy = true;

			Region reg = Region.Find( me.Location, me.Map );

			if (me is BaseCreature && m is BaseCreature && ((BaseCreature)me).midrace == ((BaseCreature)m).midrace && me.Combatant != m && !m.Criminal)
				return false;

			if ( (me is MidlandVendor || me is MidlandGuard) && m is BaseVendor )
				return false;

			if ( (me is MidlandVendor || me is MidlandGuard) && m is TownHerald )
				return false;

			if ( (me is BasePerson || me is BaseVendor || me is PlayerVendor) && ( (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "DarkMoor") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Temple of Praetoria") ))
			{
				
				if ( m is BaseCreature && ((BaseCreature)m).ControlMaster != null && m.Combatant != me)
					{
					Mobile owner = ((BaseCreature)m).ControlMaster;
					if ( owner is PlayerMobile && owner.Karma < 0)
						return false;
					else 
						return true;
					} 
				else if ( m.Karma < 0)
					return false;
			
				else if ( m.Karma >= 0)
					return true;
			}

			if ( m is PlayerMobile )
			{
				if (m.Criminal && me is BaseVendor)
					return true;
				
				if ( ((PlayerMobile)m).Profession == 1 )
				{
					m.Criminal = true;
					if ( m.Kills < 1 ){ m.Kills = 1; }
				}
			}

			if ( reg.IsPartOf( typeof( NecromancerRegion ) ) && ( GetPlayerInfo.EvilPlayer( m ) || m is BaseCreature ) )
				return false;

			if ( !(me.CanSee( m )) || !(me.InLOS( m )) )
				return false;

			if ( m.AccessLevel > AccessLevel.Player )
				return false;

			if ( m is PlayerMobile && me is BaseVendor && (  (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Basement") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "The Pit") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Town of Skara Brae") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "Lamut County") || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "the Ruins of Tenebrae") || reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)) ) && ( m.Kills > 0 || m.Karma <=  -5000) || (Server.Misc.Worlds.GetRegionName( me.Map, me.Location ) == "Arena Royale") )
				return false;

			if ( m is BasePerson || m is BaseVendor || m is PlayerVendor || m is BaseBlue || m is Townsperson || m is Citizens || m is PlayerBarkeeper )
				return false;

			if ( m is BaseCreature && ((BaseCreature)m).FightMode == FightMode.Evil ) 
				return false;

			if ( m.Region.IsPartOf( typeof( PublicRegion ) ) )
				return false;

			if ( m.Region.IsPartOf( typeof( StartRegion ) ) )
				return false;

			if ( m.Region.IsPartOf( typeof( SafeRegion ) ) )
				return false;

			if ( m.Region.IsPartOf( typeof( ProtectedRegion ) ) )
				return false;

			if ( m is PlayerMobile && !m.Criminal && m.Kills<1 )
				enemy = false;

			if ( ( DisguiseTimers.IsDisguised( m ) || Server.Spells.Fifth.IncognitoSpell.m_Timers.Contains(m)) && checkDisguise )
				enemy = false;

			if ( m is PlayerMobile && m.Karma <= -5000 && m.Skills[SkillName.Chivalry].Base >= 50 && !m.Region.IsPartOf(typeof(UmbraRegion)) && !m.Region.IsPartOf(typeof(NecromancerRegion)) )
				enemy = true; // DEATH KNIGHTS ARE NOT WELCOME AFTER THIS POINT...EXCEPT IN UMBRA OR RAVENDARK

			if ( m is PlayerMobile && m.Karma <= -5000 && m.Skills[SkillName.EvalInt].Base >= 50 && Server.Misc.GetPlayerInfo.isSyth(m, false) && !m.Region.IsPartOf(typeof(UmbraRegion)) && !m.Region.IsPartOf(typeof(NecromancerRegion)) )
				enemy = true; // SYTH ARE NOT WELCOME AFTER THIS POINT...EXCEPT IN UMBRA OR RAVENDARK

			if (m is BaseCreature)
			{
				BaseCreature c = (BaseCreature)m;
				if ( c.Controlled || c.FightMode == FightMode.Aggressor || c.FightMode == FightMode.None )
					enemy = false;
			}	

			return enemy;
		}

		public static int GetCreatureLevel( Mobile m )
		{
			int fame = m.Fame;
				if ( fame > MyServerSettings.FameCap()){ fame = MyServerSettings.FameCap(); }

			int karma = m.Karma;
				if ( karma < 0 ){ karma = m.Karma * -1; }
				if ( karma > MyServerSettings.KarmaMax() ){ karma = MyServerSettings.KarmaMax(); }

			int skills = m.Skills.Total;
				if ( skills > 10000){ skills = 10000; }
				skills = (int)( 1.5 * skills );			// UP TO 15,000

			int stats = m.RawStr + m.RawDex + m.RawInt;
				if ( stats > 250){ stats = 250; }
				stats = 60 * stats;						// UP TO 15,000

			int level = (int)( ( fame + karma + skills + stats ) / 600 );
				level = (int)( ( level - 10 ) * 1.12 );

			if ( level < 1 ){ level = 1; }
			if ( level > 125 ){ level = 125; }

			return level;
		}

		public static void GiveTorch( Mobile m )
		{
			if ( m.FindItemOnLayer( Layer.TwoHanded ) == null )
			{
				if ( m.Region.IsPartOf( typeof( BardDungeonRegion ) ) || m.Region.IsPartOf( typeof( DungeonRegion ) ) || m.Region.IsPartOf( typeof( CaveRegion ) ) )
				{
					if ( Utility.RandomMinMax( 1, 3 ) != 1 )
					{
						m.AddItem( new HeldLight() );
					}
				}
			}
		}

		public static bool FameBasedEvent( Mobile m )
		{
			int events = (int)( m.Fame / 250 );

			if ( events <= 0 )
				return false;

			if ( events > 80 )
				events = 80;

			if ( events >= Utility.RandomMinMax( 1, 100 ) )
				return true;

			return false;
		}

		public static int FameBasedLevel( Mobile m )
		{
			int level = (int)( m.Fame / 3500 );

			if ( level <= 0 )
				return 1;

			if ( level > 6 )
				level = 6;

			return level;
		}

		public static void DoSpecialAbility( Mobile from, Mobile target )
		{
			if ( from == null || from.Deleted ) //sanity
				return;

			if ( Utility.RandomMinMax( 1, 20 ) == 1 && from.EmoteHue > 0 )
			{
				Map map = from.Map;

				if ( map == null )
					return;

				int monsters = 0;

				foreach ( Mobile m in from.GetMobilesInRange( 10 ) )
				{
					if ( from.EmoteHue == 1 )
					{
						if ( m is EvilBladeSpirits || m is Imp || m is Slime )
							++monsters;
					}
					else if ( from.EmoteHue == 2 )
					{
						if ( m is BloodWorm || m is BloodSnake || m is Viscera || m is BloodSpawn || m is GiantLeech )
							++monsters;
					}
					else if ( from.EmoteHue == 3 )
					{
						if ( m is LesserDemon || m is Imp || m is ShadowHound || m is Gargoyle || m is SoulWorm )
							++monsters;

						if ( m is LowerDemon )
						{
							++monsters;
							++monsters;
						}
					}
					else if ( from.EmoteHue == 4 )
					{
						if ( m is GarnetElemental || m is TopazElemental || m is QuartzElemental || m is SpinelElemental || m is StarRubyElemental || m is EarthElemental || m is AgapiteElemental || m is BronzeElemental || m is CopperElemental || m is DullCopperElemental || m is GoldenElemental || m is ShadowIronElemental || m is ValoriteElemental || m is VeriteElemental || m is WaterElemental )
							++monsters;

						if ( m is PoisonElemental || m is ToxicElemental || m is AirElemental || m is BloodElemental || m is FireElemental || m is ElectricalElemental )
						{
							++monsters;
							++monsters;
						}
					}
					else if ( from.EmoteHue == 5 )
					{
						if ( m is BoneKnight || m is BoneMagi || m is Ghoul || m is Mummy || m is Shade || m is SkeletalKnight || m is SkeletalMage || m is Skeleton || m is Spectre || m is Wraith || m is Phantom || m is Zombie )
							++monsters;
					}
					else if ( from.EmoteHue == 6 )
					{
						if ( m is WeedElemental || m is DireWolf || m is DireBear || m is DireBoar )
							++monsters;
					}
					else if ( from.EmoteHue == 7 )
					{
						if ( m is SnowElemental || m is IceSerpent || m is WinterWolf || m is IceElemental || m is FrostOoze || m is FrostSpider || m is IceGolem || m is IceToad || m is IceSerpent )
							++monsters;
					}
					else if ( from.EmoteHue == 8 )
					{
						if ( m is FireDemon || m is LavaPuddle || m is CinderElemental || m is FireBat || m is FireElemental || m is FireMephit )
							++monsters;
					}
					else if ( from.EmoteHue == 9 )
					{
						if ( m is GiantSerpent || m is GiantAdder || m is JungleViper || m is LargeSnake || m is Snake )
							++monsters;

						if ( m is SilverSerpent )
						{
							++monsters;
							++monsters;
						}
					}
					else if ( from.EmoteHue == 10 )
					{
						if ( m is WaterWeird || m is Typhoon || m is WaterElemental || m is StormCloud || m is WaterSpawn )
							++monsters;
					}
					else if ( from.EmoteHue == 11 || from.EmoteHue == 505 )
					{
						if ( m is EvilIcyVortex || m is EvilPlagueVortex || m is EvilEnergyVortex || m is EvilBladeSpirits || m is EvilScorchingVortex )
							++monsters;
					}
					else if ( from.EmoteHue == 12 )
					{
						if ( m is WineElemental || m is ManureGolem || m is Fairy )
							++monsters;
					}
					else if ( from.EmoteHue == 13 )
					{
						if ( m is GhostWarrior || m is WalkingCorpse || m is Wight || m is Spirit || m is Phantom || m is FrailSkeleton || m is Zombie || m is Skeleton || m is SkeletalKnight || m is BoneKnight || m is SkeletalWarrior )
							++monsters;
					}
					else if ( from.EmoteHue == 14 )
					{
						if ( m is DeathBear || m is DeathWolf || m is DarkReaper )
							++monsters;
					}
					else if ( from.EmoteHue == 15 )
					{
						if ( m is Bat || m is Ghoul || m is Wraith || m is WalkingCorpse || m is VampireBat || m is Zombie )
							++monsters;
					}
					else if ( from.EmoteHue == 16 )
					{
						if ( m is EvilIcyVortex || m is IceBladeSpirits )
							++monsters;
					}
					else if ( from.EmoteHue == 17 )
					{
						if ( m is Scorpion || m is SandVortex || m is SandSpider || m is DustElemental || m is GiantAdder )
							++monsters;
					}
				}

				if ( monsters < 3 && from.Mana > 39 )
				{
					BaseCreature monster = new Imp();

					from.PlaySound( 0x216 );

					from.Mana = from.Mana - 40;

					int newmonsters = Utility.RandomMinMax( 1, 3 );

					for ( int i = 0; i < newmonsters; ++i )
					{
						if ( from.EmoteHue == 1 )
						{
							switch ( Utility.Random( 5 ) )
							{
								default:
								case 0: monster = new EvilBladeSpirits(); break;
								case 1: monster = new Imp(); break;
								case 2: monster = new Imp(); break;
								case 3: monster = new Slime(); break;
								case 4: monster = new Slime(); break;
							}
						}
						else if ( from.EmoteHue == 2 )
						{
							switch ( Utility.Random( 5 ) )
							{
								default:
								case 0: monster = new BloodWorm(); break;
								case 1: monster = new BloodSnake(); break;
								case 2: monster = new Viscera(); break;
								case 3: monster = new BloodSpawn(); break;
								case 4: monster = new GiantLeech(); break;
							}
						}
						else if ( from.EmoteHue == 3 )
						{
							switch ( Utility.Random( 11 ) )
							{
								default:
								case 0: monster = new LesserDemon(); break;
								case 1: monster = new LowerDemon(); break;
								case 2: monster = new LesserDemon(); break;
								case 3: monster = new LowerDemon(); break;
								case 4: monster = new Imp(); break;
								case 5: monster = new ShadowHound(); break;
								case 7: monster = new Gargoyle(); break;
								case 9: monster = new SoulWorm(); break;
							}
						}
						else if ( from.EmoteHue == 4 )
						{
							switch ( Utility.Random( 31 ) )
							{
								default:
								case 0: monster = new GarnetElemental(); break;
								case 1: monster = new TopazElemental(); break;
								case 2: monster = new QuartzElemental(); break;
								case 3: monster = new SpinelElemental(); break;
								case 4: monster = new StarRubyElemental(); break;
								case 5: monster = new GarnetElemental(); break;
								case 6: monster = new TopazElemental(); break;
								case 7: monster = new QuartzElemental(); break;
								case 8: monster = new SpinelElemental(); break;
								case 9: monster = new StarRubyElemental(); break;
								case 10: monster = new GarnetElemental(); break;
								case 11: monster = new TopazElemental(); break;
								case 12: monster = new QuartzElemental(); break;
								case 13: monster = new SpinelElemental(); break;
								case 14: monster = new StarRubyElemental(); break;
								case 15: monster = new EarthElemental(); break;
								case 16: monster = new AgapiteElemental(); break;
								case 17: monster = new BronzeElemental(); break;
								case 18: monster = new CopperElemental(); break;
								case 19: monster = new DullCopperElemental(); break;
								case 20: monster = new GoldenElemental(); break;
								case 21: monster = new ShadowIronElemental(); break;
								case 22: monster = new ValoriteElemental(); break;
								case 23: monster = new VeriteElemental(); break;
								case 24: monster = new PoisonElemental(); break;
								case 25: monster = new ToxicElemental(); break;
								case 26: monster = new WaterElemental(); break;
								case 27: monster = new AirElemental(); break;
								case 28: monster = new BloodElemental(); break;
								case 29: monster = new FireElemental(); break;
								case 30: monster = new ElectricalElemental(); break;
							}
						}
						else if ( from.EmoteHue == 5 )
						{
							switch ( Utility.Random( 13 ) )
							{
								default:
								case 0: monster = new BoneKnight(); break;
								case 1: monster = new BoneMagi(); break;
								case 2: monster = new Ghoul(); break;
								case 3: monster = new Ghostly(); break;
								case 4: monster = new Mummy(); break;
								case 5: monster = new Shade(); break;
								case 6: monster = new SkeletalKnight(); break;
								case 7: monster = new SkeletalMage(); break;
								case 8: monster = new Skeleton(); break;
								case 9: monster = new Spectre(); break;
								case 10: monster = new Wraith(); break;
								case 11: monster = new Phantom(); break;
								case 12: monster = new Zombie(); break;
							}
						}
						else if ( from.EmoteHue == 6 )
						{
							switch ( Utility.Random( 4 ) )
							{
								default:
								case 0: monster = new DireBear(); break;
								case 1: monster = new DireBoar(); break;
								case 2: monster = new DireWolf(); break;
								case 3: monster = new WeedElemental(); break;
							}
						}
						else if ( from.EmoteHue == 7 )
						{
							switch ( Utility.Random( 9 ) )
							{
								default:
								case 0: monster = new SnowElemental(); break;
								case 1: monster = new IceSerpent(); break;
								case 2: monster = new IceElemental(); break;
								case 3: monster = new FrostOoze(); break;
								case 4: monster = new FrostSpider(); break;
								case 5: monster = new IceGolem(); break;
								case 6: monster = new IceToad(); break;
								case 7: monster = new IceSerpent(); break;
								case 8: monster = new WinterWolf(); break;
							}
						}
						else if ( from.EmoteHue == 8 )
						{
							switch ( Utility.Random( 9 ) )
							{
								default:
								case 0: monster = new FireDemon(); break;
								case 1: monster = new LavaPuddle(); break;
								case 2: monster = new CinderElemental(); break;
								case 3: monster = new FireElemental(); break;
								case 4: monster = new FireBat(); break;
								case 5: monster = new FireBat(); break;
								case 6: monster = new LavaPuddle(); break;
								case 7: monster = new FireMephit(); break;
								case 8: monster = new FireMephit(); break;
							}
						}
						else if ( from.EmoteHue == 9 )
						{
							switch ( Utility.Random( 11 ) )
							{
								default:
								case 0: monster = new GiantSerpent(); break;
								case 1: monster = new GiantAdder(); break;
								case 2: monster = new JungleViper(); break;
								case 3: monster = new LargeSnake(); break;
								case 4: monster = new Snake(); break;
								case 5: monster = new GiantSerpent(); break;
								case 6: monster = new GiantAdder(); break;
								case 7: monster = new JungleViper(); break;
								case 8: monster = new LargeSnake(); break;
								case 9: monster = new Snake(); break;
								case 10: monster = new SilverSerpent(); break;
							}
						}
						else if ( from.EmoteHue == 10 )
						{
							switch ( Utility.Random( 5 ) )
							{
								default:
								case 0: monster = new WaterWeird(); break;
								case 1: monster = new Typhoon(); break;
								case 2: monster = new WaterElemental(); break;
								case 3: monster = new StormCloud(); break;
								case 4: monster = new WaterSpawn(); break;
							}
						}
						else if ( from.EmoteHue == 11 || from.EmoteHue == 505 )
						{
							switch ( Utility.Random( 5 ) )
							{
								default:
								case 0: monster = new EvilIcyVortex(); break;
								case 1: monster = new EvilPlagueVortex(); break;
								case 2: monster = new EvilEnergyVortex(); break;
								case 3: monster = new EvilScorchingVortex(); break;
								case 4: monster = new EvilBladeSpirits(); break;
							}
						}
						else if ( from.EmoteHue == 12 )
						{
							switch ( Utility.Random( 3 ) )
							{
								default:
								case 0: monster = new WineElemental(); break;
								case 1: monster = new ManureGolem(); break;
								case 2: monster = new Fairy(); break;
							}
						}
						else if ( from.EmoteHue == 13 )
						{
							int MaxMonster = 2;
							if ( from.Fame >= 23000 ){		MaxMonster = 10; }
							else if ( from.Fame >= 12000 ){	MaxMonster = 6; }

							switch ( Utility.RandomMinMax( 0, MaxMonster ) )
							{
								default:
								case 0: monster = new FrailSkeleton(); break;
								case 1: monster = new Phantom(); break;
								case 2: monster = new Skeleton(); break;
								case 3: monster = new Zombie(); break;
								case 4: monster = new GhostWarrior(); break;
								case 5: monster = new Wight(); break;
								case 6: monster = new SkeletalWarrior(); break;
								case 7: monster = new WalkingCorpse(); break;
								case 8: monster = new SkeletalKnight(); break;
								case 9: monster = new BoneKnight(); break;
								case 10: monster = new Spirit(); break;
							}
						}
						else if ( from.EmoteHue == 14 )
						{
							switch ( Utility.Random( 5 ) )
							{
								default:
								case 0: case 1:	monster = new DeathWolf(); break;
								case 2: case 3:	monster = new DeathBear(); break;
								case 4:			monster = new DarkReaper(); break;
							}
						}
						else if ( from.EmoteHue == 15 )
						{
							int MaxMonster = 1;
							if ( from.Fame >= 24000 ){		MaxMonster = 5; }
							else if ( from.Fame >= 10500 ){	MaxMonster = 3; }

							switch ( Utility.RandomMinMax( 0, MaxMonster ) )
							{
								default:
								case 0: monster = new Bat(); break;
								case 1: monster = new Zombie(); break;
								case 2: monster = new Wraith(); break;
								case 3: monster = new Ghoul(); break;
								case 4: monster = new VampireBat(); break;
								case 5: monster = new WalkingCorpse(); break;
							}
						}
						else if ( from.EmoteHue == 16 )
						{
							switch ( Utility.Random( 2 ) )
							{
								default:
								case 0: monster = new EvilIcyVortex(); break;
								case 1: monster = new IceBladeSpirits(); break;
							}
						}
						else if ( from.EmoteHue == 17 )
						{
							switch ( Utility.Random( 7 ) )
							{
								default:
								case 0: monster = new Scorpion(); break;
								case 1: monster = new SandVortex(); break;
								case 2: monster = new SandVortex(); break;
								case 3: monster = new DustElemental(); break;
								case 4: monster = new DustElemental(); break;
								case 5: monster = new SandSpider(); break;
								case 6: monster = new GiantAdder(); break;
							}
						}

						((BaseCreature)monster).Team = ((BaseCreature)from).Team;

						bool validLocation = false;
						Point3D loc = from.Location;

						for ( int j = 0; !validLocation && j < 10; ++j )
						{
							int x = from.X + Utility.Random( 3 ) - 1;
							int y = from.Y + Utility.Random( 3 ) - 1;
							int z = map.GetAverageZ( x, y );

							if ( validLocation = map.CanFit( x, y, from.Z, 16, false, false ) )
								loc = new Point3D( x, y, from.Z );
							else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
								loc = new Point3D( x, y, z );
						}

						monster.ControlSlots = 666; // FOR EMERGENCY MONSTER CLEANUP
						monster.YellHue = from.Serial;
						monster.MoveToWorld( loc, map );
						monster.Combatant = target;
						Effects.SendLocationParticles( EffectItem.Create( monster.Location, monster.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						monster.PlaySound( 0x1FE );
						OnCreatureSpawned( monster );
					}

					from.Say( "" + NameList.RandomName( "magic words" ) + " " + NameList.RandomName( "magic words" ) + " " + NameList.RandomName( "magic words" ) + "!" );
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void OnCreatureSpawned( Mobile summoned )
		{
			if ( summoned.Backpack != null )
			{
				List<Item> belongings = new List<Item>();
				foreach( Item i in summoned.Backpack.Items )
				{
					belongings.Add(i);
				}
				foreach ( Item stuff in belongings )
				{
					stuff.Delete();
				}
			}

			((BaseCreature)summoned).NameColor();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void GiveBasicWepShld( Mobile m )
		{
			int CanHaveShield = 1;

			switch ( Utility.Random( 28 ) )
			{
				case 0: m.AddItem( new BattleAxe() ); CanHaveShield = 0; break;
				case 1: m.AddItem( new VikingSword() ); break;
				case 2: m.AddItem( new Halberd() ); CanHaveShield = 0; break;
				case 3: m.AddItem( new DoubleAxe() ); break;
				case 4: m.AddItem( new ExecutionersAxe() ); CanHaveShield = 0; break;
				case 5: m.AddItem( new WarAxe() ); break;
				case 6: m.AddItem( new TwoHandedAxe() ); CanHaveShield = 0; break;
				case 7: m.AddItem( new Cutlass() ); break;
				case 8: m.AddItem( new Katana() ); break;
				case 9: m.AddItem( new Kryss() ); break;
				case 10: m.AddItem( new Broadsword() ); break;
				case 11: m.AddItem( new Longsword() ); break;
				case 12: m.AddItem( new ThinLongsword() ); break;
				case 13: m.AddItem( new Scimitar() ); break;
				case 14: m.AddItem( new BoneHarvester() ); break;
				case 15: m.AddItem( new CrescentBlade() ); CanHaveShield = 0; break;
				case 16: m.AddItem( new DoubleBladedStaff() ); CanHaveShield = 0; break;
				case 17: m.AddItem( new Pike() ); CanHaveShield = 0; break;
				case 18: m.AddItem( new Scythe() ); CanHaveShield = 0; break;
				case 19: m.AddItem( new Pitchfork() ); CanHaveShield = 0; break;
				case 20: m.AddItem( new ShortSpear() ); CanHaveShield = 0; break;
				case 21: m.AddItem( new Spear() ); CanHaveShield = 0; break;
				case 22: m.AddItem( new Club() ); break;
				case 23: m.AddItem( new HammerPick() ); break;
				case 24: m.AddItem( new Mace() ); break;
				case 25: m.AddItem( new Maul() ); break;
				case 26: m.AddItem( new WarHammer() ); CanHaveShield = 0; break;
				case 27: m.AddItem( new WarMace() ); break;
			}

			if ( CanHaveShield == 1 && Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				switch ( Utility.Random( 12 ) )
				{
					case 0: m.AddItem( new BronzeShield() ); break;
					case 1: m.AddItem( new Buckler() ); break;
					case 2: m.AddItem( new MetalKiteShield() ); break;
					case 3: m.AddItem( new HeaterShield() ); break;
					case 4: m.AddItem( new WoodenKiteShield() ); break;
					case 5: m.AddItem( new MetalShield() ); break;
					case 6: m.AddItem( new GuardsmanShield() ); break;
					case 7: m.AddItem( new ElvenShield() ); break;
					case 8: m.AddItem( new DarkShield() ); break;
					case 9: m.AddItem( new CrestedShield() ); break;
					case 10: m.AddItem( new ChampionShield() ); break;
					case 11: m.AddItem( new JeweledShield() ); break;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void DressUpWizards( Mobile from )
		{
			int clothHue = Utility.RandomMinMax( 0, 12 );
			int hoodColor = 0;

					Item shirt = new FancyShirt();
					shirt.Hue = RandomThings.GetRandomColor( clothHue );
					hoodColor = shirt.Hue;
					from.AddItem( shirt ); 

			if ( Utility.Random( 2 ) == 1 )
			{ 
				Item robe = new Robe();
					robe.Delete();

					int wear = Utility.RandomMinMax( 0, 9 );
					if ( from.Body == 605 || from.Body == 606 ){ wear = Utility.RandomMinMax( 0, 7 ); } // ELVES CANNOT WEAR SORCERER ROBES
					
					switch ( wear )
					{
						case 0: robe = new AssassinRobe(); break;
						case 1: robe = new FancyRobe(); break;
						case 2: robe = new GildedRobe(); break;
						case 3: robe = new OrnateRobe(); break;
						case 4: robe = new MagistrateRobe(); break;
						case 5: robe = new RoyalRobe(); break;
						case 6: robe = new NecromancerRobe(); break;
						case 7: robe = new SpiderRobe(); break;
						case 8: robe = new SorcererRobe(); break;
						case 9: robe = new VagabondRobe(); break;
					}
					robe.Hue = RandomThings.GetRandomColor( clothHue );
					hoodColor = robe.Hue;
					from.AddItem( robe ); 


			}
			else if ( from.Body == 0x190 || from.Body == 605 )
			{
				Item robe = new Robe();
					robe.Hue = RandomThings.GetRandomColor( clothHue );
					hoodColor = robe.Hue;
					from.AddItem( robe ); 
			}
			else
			{
				Item dress = new PlainDress();
					dress.Hue = RandomThings.GetRandomColor( clothHue );
					hoodColor = dress.Hue;
					from.AddItem( dress ); 
			}

			if ( Utility.Random( 2 ) == 1 )
			{ 
				if ( Utility.Random( 2 ) == 1 )
				{ 
					int myHat = Utility.RandomMinMax( 0, 2 );
					if ( from.Body == 605 ){ myHat = 1; }
					Item hat = new WizardsHat();
						hat.Delete();
						switch ( myHat )
						{
							case 0: hat = new ClothCowl(); break;
							case 1: hat = new ClothHood(); break;
							case 2: hat = new FancyHood(); break;
						}
						hat.Hue = hoodColor;
						hoodColor = hat.Hue;
						from.AddItem( hat ); 
				}
				else if ( from.Body == 0x190 || from.Body == 605 )
				{
					WizardsHat hat = new WizardsHat( );
						hat.Hue = RandomThings.GetRandomColor( clothHue );
						from.AddItem( hat );
				}
				else
				{
					WitchHat hat = new WitchHat( );
						hat.Hue = RandomThings.GetRandomColor( clothHue );
						from.AddItem( hat );
				}
			}

			if ( Utility.Random( 4 ) == 1 )
			{ 
				Cloak cloak = new Cloak( );
					cloak.Hue = RandomThings.GetRandomColor( clothHue );
					if ( hoodColor > 0 )
					{
						cloak.Hue = hoodColor;
					}
					from.AddItem( cloak );
			}

			Item boots = new ThighBoots( );
				boots.Hue = Utility.RandomNeutralHue();
				boots.Name = "boots";
				if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ boots.ItemID = 12228; }
				from.AddItem( boots );
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void DressUpFighters( Mobile m, string race, bool isEnemy, int iArmor )
		{
			m.AddItem( new Boots( Utility.RandomNeutralHue() ) );
			int cloakColor = 0;
			if ( 1 == Utility.RandomMinMax( 1, 2 ) ){ cloakColor = RandomThings.GetRandomColor( 0 ); m.AddItem( new Cloak( cloakColor ) ); }

			int aHue = Utility.RandomList( 0x973, 0x966, 0x96D, 0x972, 0x8A5, 0x979, 0x89F, 0x8AB, 0x497, 0, Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue() );
			int lHue = Utility.RandomList( 0x66D, 0x8A8, 0x455, 0x851, 0x8FD, 0x8B0, 0x283, 0x227, 0x1C1, 0x8AC, 0x845, 0x851, 0x497, 0, Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue(), Utility.RandomMetalHue() );
			if (iArmor == 0) {
				 iArmor = Utility.RandomMinMax( 1, 6 );
			}	
			
			if ( race == "undead " && iArmor == 4 ){ iArmor = 1; }
			if ( iArmor == 5 && !isEnemy ){ iArmor = 1; }

			int hHue = aHue;

			int myTitle = Utility.RandomMinMax( 0, 8 );
			int myHelm = Utility.RandomMinMax( 0, 10 );

			if ( iArmor == 1 )
			{
				Item cloth1 = new PlateArms();
					cloth1.Hue = aHue;
					m.AddItem( cloth1 );
				Item cloth2 = new PlateGorget();
					cloth2.Hue = aHue;
					m.AddItem( cloth2 );
				Item cloth3 = new PlateLegs();
					cloth3.Hue = aHue;
					m.AddItem( cloth3 );
				Item cloth4 = new PlateChest();
					cloth4.Hue = aHue;
					m.AddItem( cloth4 );

				if ( Utility.RandomMinMax( 1, 2 ) == 2 || race == "undead " )
				{
					Item glove = new PlateGloves();
						glove.Hue = aHue;
						m.AddItem( glove );
				}
			}
			else if ( iArmor == 2 )
			{
				Item cloth1 = new ChainChest();
					cloth1.Hue = aHue;
					m.AddItem( cloth1 );
				Item cloth2 = new ChainLegs();
					cloth2.Hue = aHue;
					m.AddItem( cloth2 );
				Item cloth3 = new RingmailArms();
					cloth3.Hue = aHue;
					m.AddItem( cloth3 );
				Item cloth4 = new PlateGorget();
					cloth4.Hue = aHue;
					m.AddItem( cloth4 );

				if ( Utility.RandomMinMax( 1, 2 ) == 2 || race == "undead " )
				{
					Item glove = new RingmailGloves();
						glove.Hue = aHue;
						m.AddItem( glove );
				}
			}
			else if ( iArmor == 3 )
			{
				hHue = lHue;
				Item cloth1 = new StuddedChest();
					cloth1.Hue = lHue;
					m.AddItem( cloth1 );
				Item cloth2 = new StuddedArms();
					cloth2.Hue = lHue;
					m.AddItem( cloth2 );
				Item cloth3 = new StuddedLegs();
					cloth3.Hue = lHue;
					m.AddItem( cloth3 );
				Item cloth4 = new StuddedGorget();
					cloth4.Hue = lHue;
					m.AddItem( cloth4 );

				if ( Utility.RandomMinMax( 1, 2 ) == 2 || race == "undead " )
				{
					Item glove = new StuddedGloves();
						glove.Hue = lHue;
						m.AddItem( glove );
				}
			}
			else if ( iArmor == 4 )
			{
				myTitle = Utility.RandomMinMax( 9, 13 );
				myHelm = Utility.RandomMinMax( 6, 10 );

				hHue = lHue;

				if ( Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					Item cloth1 = new LoinCloth();
						cloth1.Hue = lHue;
						m.AddItem( cloth1 );
				}
				else
				{
					Item cloth2 = new LeatherSkirt();
						cloth2.Hue = lHue;
						m.AddItem( cloth2 );
				}

				if ( m.Body == 0x191 || m.Body == 606 )
				{
					Item cloth3 = new LeatherBustierArms();
						cloth3.Hue = lHue;
						m.AddItem( cloth3 );
				}

				if ( Utility.RandomMinMax( 1, 4 ) == 2 )
				{
					Item glove = new LeatherGloves();
						glove.Hue = lHue;
						m.AddItem( glove );
				}
			}
			else if ( iArmor == 5 )
			{
				if ( m.Body.IsFemale ){ m.Name = NameList.RandomName( "tokuno female" ); }
				else { m.Name = NameList.RandomName( "tokuno male" ); }

				myTitle = Utility.RandomMinMax( 14, 15 );

				hHue = lHue;

				SamuraiTabi cloth1 = new SamuraiTabi( );
					cloth1.Hue = lHue;
					m.AddItem( cloth1 );

				LeatherHiroSode cloth2 = new LeatherHiroSode( );
					cloth2.Hue = lHue;
					m.AddItem( cloth2 );

				LeatherDo cloth3 = new LeatherDo( );
					cloth3.Hue = lHue;
					m.AddItem( cloth3 );

				if ( Utility.RandomMinMax( 1, 2 ) == 2 || race == "undead " )
				{
					Item glove = new LeatherGloves();
						glove.Hue = lHue;
						m.AddItem( glove );
				}

				switch ( Utility.Random( 4 ) )
				{
					case 0: LightPlateJingasa cloth4 = new LightPlateJingasa( );	cloth4.Hue = lHue;	m.AddItem( cloth4 ); break;
					case 1: ChainHatsuburi cloth5 = new ChainHatsuburi( );	cloth5.Hue = lHue;	m.AddItem( cloth5 ); break;
					case 2: DecorativePlateKabuto cloth6 = new DecorativePlateKabuto( );	cloth6.Hue = lHue;	m.AddItem( cloth6 ); break;
					case 3: LeatherJingasa cloth7 = new LeatherJingasa( );	cloth7.Hue = lHue;	m.AddItem( cloth7 ); break;
				}

				switch ( Utility.Random( 3 ) )
				{
					case 0: StuddedHaidate cloth8 = new StuddedHaidate( );	cloth8.Hue = lHue;	m.AddItem( cloth8 ); break;
					case 1: LeatherSuneate cloth9 = new LeatherSuneate( );	cloth9.Hue = lHue;	m.AddItem( cloth9 ); break;
					case 2: PlateSuneate cloth0 = new PlateSuneate( );	cloth0.Hue = lHue;	m.AddItem( cloth0 ); break;
				}
			}
			else
			{
				hHue = lHue;
				Item cloth1 = new LeatherArms();
					cloth1.Hue = lHue;
					m.AddItem( cloth1 );
				Item cloth2 = new LeatherChest();
					cloth2.Hue = lHue;
					m.AddItem( cloth2 );
				Item cloth3 = new LeatherGorget();
					cloth3.Hue = lHue;
					m.AddItem( cloth3 );
				Item cloth4 = new LeatherLegs();
					cloth4.Hue = lHue;
					m.AddItem( cloth4 );

				if ( Utility.RandomMinMax( 1, 2 ) == 2 || race == "undead " )
				{
					Item glove = new LeatherGloves();
						glove.Hue = lHue;
						m.AddItem( glove );
				}
			}

			int CanHaveHelm = 0;

			if ( Utility.RandomMinMax( 1, 2 ) == 2 && iArmor != 5 ){ CanHaveHelm = 1; }
			if ( race == "undead " && m.Hue != 0x430 ){ CanHaveHelm = 1; }
			if ( race == "undead " && m.Hue == 0x430 ){ CanHaveHelm = 0; }

			if ( CanHaveHelm > 0 )
			{
				if ( cloakColor > 0 && Utility.Random( 20 ) == 1 )
				{
					switch ( Utility.RandomMinMax( 1, 3 ) )
					{
						case 1: m.AddItem( new ClothCowl( cloakColor ) ); break;
						case 2: m.AddItem( new ClothHood( cloakColor ) ); break;
						case 3: m.AddItem( new FancyHood( cloakColor ) ); break;
					}
				}
				else
				{
					switch ( myHelm )
					{
						case 0: Item helm1 = new PlateHelm(); helm1.Hue = hHue; m.AddItem( helm1 ); break;
						case 1: Item helm2 = new Bascinet();  helm2.Hue = hHue; m.AddItem( helm2 ); break;
						case 2: Item helm3 = new ChainCoif(); helm3.Hue = hHue; m.AddItem( helm3 ); break;
						case 3: Item helm4 = new CloseHelm(); helm4.Hue = hHue; m.AddItem( helm4 ); break;
						case 4: Item helm5 = new LeatherCap(); helm5.Hue = hHue; m.AddItem( helm5 ); break;
						case 5: Item helm6 = new Helmet(); helm6.Hue = hHue; m.AddItem( helm6 ); break;
						case 6: Item helm7 = new OrcHelm(); helm7.Hue = hHue; m.AddItem( helm7 ); break;
						case 7: Item helm8 = new NorseHelm(); helm8.Hue = hHue; m.AddItem( helm8 ); break;
						case 8: Item helm9 = new BoneHelm(); helm9.Hue = hHue; m.AddItem( helm9 ); break;
						case 9: Item helm10 = new OrcHelm(); helm10.Name = "great helm"; helm10.ItemID = 0x2645; helm10.Hue = hHue; m.AddItem( helm10 ); break;
						case 10: Item helm11 = new MagicJewelryCirclet(); helm11.Hue = hHue; m.AddItem( helm11 ); break;
					}
				}
			}

			int CanHaveShield = 1;

			if ( iArmor == 5 )
			{
				CanHaveShield = 0; 
				switch ( Utility.Random( 5 ) )
				{
					case 0: m.AddItem( new NoDachi() ); break;
					case 1: m.AddItem( new Halberd() ); break;
					case 2: m.AddItem( new Wakizashi() ); break;
					case 3: m.AddItem( new Longsword() ); break;
					case 4: m.AddItem( new Bokuto() ); break;
				}
			}
			else
			{
				switch ( Utility.Random( 28 ) )
				{
					case 0: m.AddItem( new BattleAxe() ); CanHaveShield = 0; break;
					case 1: m.AddItem( new VikingSword() ); break;
					case 2: m.AddItem( new Halberd() ); CanHaveShield = 0; break;
					case 3: m.AddItem( new DoubleAxe() ); break;
					case 4: m.AddItem( new ExecutionersAxe() ); CanHaveShield = 0; break;
					case 5: m.AddItem( new WarAxe() ); break;
					case 6: m.AddItem( new TwoHandedAxe() ); CanHaveShield = 0; break;
					case 7: m.AddItem( new Cutlass() ); break;
					case 8: m.AddItem( new Katana() ); break;
					case 9: m.AddItem( new Kryss() ); break;
					case 10: m.AddItem( new Broadsword() ); break;
					case 11: m.AddItem( new Longsword() ); break;
					case 12: m.AddItem( new ThinLongsword() ); break;
					case 13: m.AddItem( new Scimitar() ); break;
					case 14: m.AddItem( new BoneHarvester() ); break;
					case 15: m.AddItem( new CrescentBlade() ); CanHaveShield = 0; break;
					case 16: m.AddItem( new DoubleBladedStaff() ); CanHaveShield = 0; break;
					case 17: m.AddItem( new Pike() ); CanHaveShield = 0; break;
					case 18: m.AddItem( new Scythe() ); CanHaveShield = 0; break;
					case 19: m.AddItem( new Pitchfork() ); CanHaveShield = 0; break;
					case 20: m.AddItem( new ShortSpear() ); CanHaveShield = 0; break;
					case 21: m.AddItem( new Spear() ); CanHaveShield = 0; break;
					case 22: m.AddItem( new Club() ); break;
					case 23: m.AddItem( new HammerPick() ); break;
					case 24: m.AddItem( new Mace() ); break;
					case 25: m.AddItem( new Maul() ); break;
					case 26: m.AddItem( new WarHammer() ); CanHaveShield = 0; break;
					case 27: m.AddItem( new WarMace() ); break;
				}
			}

			if ( CanHaveShield == 1 && Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: Item shield1 = new BronzeShield(); shield1.Hue = hHue; m.AddItem( shield1 ); break;
					case 1: Item shield2 = new Buckler();  shield2.Hue = hHue; m.AddItem( shield2 ); break;
					case 2: Item shield3 = new MetalKiteShield(); shield3.Hue = hHue; m.AddItem( shield3 ); break;
					case 3: Item shield4 = new HeaterShield(); shield4.Hue = hHue; m.AddItem( shield4 ); break;
					case 4: Item shield5 = new WoodenKiteShield();   shield5.Hue = hHue; m.AddItem( shield5 ); break;
					case 5: Item shield6 = new MetalShield(); shield6.Hue = hHue; m.AddItem( shield6 ); break;
				}
			}

			switch ( myTitle )
			{
				case 0: m.Title = "the " + race + "fighter"; break;
				case 1: m.Title = "the " + race + "knight"; break;
				case 2: m.Title = "the " + race + "champion"; break;
				case 3: m.Title = "the " + race + "warrior"; break;
				case 4: m.Title = "the " + race + "soldier"; break;
				case 5: m.Title = "the " + race + "vanquisher"; break;
				case 6: m.Title = "the " + race + "battler"; break;
				case 7: m.Title = "the " + race + "gladiator"; break;
				case 8: m.Title = "the " + race + "mercenary"; break;
				case 9: m.Title = "the " + race + "nomad"; break;
				case 10: m.Title = "the " + race + "berserker"; break;
				case 11: m.Title = "the " + race + "barbarian"; if ( m.Female ){ m.Title = "the " + race + "amazon"; } break;
				case 12: m.Title = "the " + race + "pit fighter"; break;
				case 13: m.Title = "the " + race + "brute"; break;
				case 14: m.Title = "the " + race + "samurai"; break;
				case 15: m.Title = "the " + race + "ronin"; break;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void DressUpRogues( Mobile m, string race, bool isEnemy, int iArmor, string suit )
		{
			if (m.Map == null)
				return;
			
			Region reg = null;
			
			if (m is BaseCreature)
			{
				BaseCreature bc = (BaseCreature)m;
				reg = Region.Find( bc.Home, bc.Map );
			}
			else if (m is PlayerMobile)
				reg = ((PlayerMobile)m).Region;

			int clothHue = IntelligentAction.RogueHues();

			if (iArmor == 0) {
				iArmor = Utility.RandomMinMax( 1, 3 );
			}

			int myTitle = Utility.RandomMinMax( 0, 9 );
			int myHelm = Utility.RandomMinMax( 0, 6 );

			if ( reg == null)
				return;
			
			else if ( reg.IsPartOf( "the Ruins of the Black Blade" ) )
			{
				iArmor = 2;
			}
			else if ( reg.IsPartOf( "Stonegate Castle" ) )
			{
				iArmor = 2;
			}
			else if ( m is Brigand )
			{
				iArmor = 3;
			}

			if ( iArmor == 1 )
			{
				m.AddItem( new Boots( Utility.RandomNeutralHue() ) );
				if ( 1 == Utility.RandomMinMax( 1, 2 ) ){ m.AddItem( new Cloak( Utility.RandomNeutralHue() ) ); }

				Item cloth1 = new LeatherChest();
					cloth1.Hue = clothHue;
					m.AddItem( cloth1 );
				Item cloth2 = new LeatherArms();
					cloth2.Hue = clothHue;
					m.AddItem( cloth2 );
				Item cloth3 = new LeatherLegs();
					cloth3.Hue = clothHue;
					m.AddItem( cloth3 );
				Item cloth4 = new LeatherGorget();
					cloth4.Hue = clothHue;
					m.AddItem( cloth4 );

				if ( Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					Item glove = new LeatherGloves();
						glove.Hue = clothHue;
						m.AddItem( glove );
				}
			}
			else if ( iArmor == 2 )
			{
				m.YellHue = 1;
				if (suit.Length == 0) {
					switch ( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: m.Title = "the " + race + "assassin";	suit = "assassin"; m.YellHue = 2; break;
						case 1: m.Title = "the " + race + "hunter";		suit = "hunter"; break;
						case 2: m.Title = "the " + race + "ninja";		suit = "ninja"; myHelm = 6; break;
					}
				}

				m.EmoteHue = 1;

				int thief = Utility.RandomList(0,1,2);
					if ( !isEnemy ){ thief = Utility.RandomList(0,1); }			

				myTitle = 100;

				if ( suit == "ninja" && m.Body.IsFemale ){ m.Name = NameList.RandomName( "tokuno female" ); }
				else if ( suit == "ninja" ){ m.Name = NameList.RandomName( "tokuno male" ); }

				if ( Utility.RandomMinMax( 1, 2 ) == 1 || suit == "ninja" )
				{
					LeatherNinjaJacket jacket = new LeatherNinjaJacket( );
						jacket.Name = suit + " tunic";
						jacket.Hue = clothHue;
						m.AddItem( jacket );
				}
				else
				{
					LeatherChest jacket = new LeatherChest( );
						jacket.Name = suit + " tunic";
						jacket.Hue = clothHue;
						m.AddItem( jacket );
				}

				if ( Utility.RandomMinMax( 1, 2 ) == 1 || suit == "ninja" )
				{
					LeatherNinjaPants pants = new LeatherNinjaPants( );
						pants.Name = suit + " leggings";
						pants.Hue = clothHue;
						m.AddItem( pants );
				}
				else
				{
					LeatherLegs pants = new LeatherLegs( );
						pants.Name = suit + " leggings";
						pants.Hue = clothHue;
						m.AddItem( pants );
				}

				if ( Utility.RandomMinMax( 1, 2 ) == 1 || suit == "ninja" )
				{
					LeatherNinjaMitts gloves = new LeatherNinjaMitts( );
						gloves.Name = suit + " gloves";
						gloves.Hue = clothHue;
						m.AddItem( gloves );
				}
				else
				{
					LeatherGloves gloves = new LeatherGloves( );
						gloves.Name = suit + " gloves";
						gloves.Hue = clothHue;
						m.AddItem( gloves );
				}

				if ( Utility.RandomMinMax( 1, 2 ) == 1 || suit == "ninja" )
				{
					NinjaTabi boot = new NinjaTabi( );
						boot.Name = suit + " boots";
						boot.Hue = clothHue;
						m.AddItem( boot );
				}
				else
				{
					Boots boot = new Boots( );
						boot.Name = suit + " boots";
						boot.Hue = clothHue;
						m.AddItem( boot );
				}

				if ( Utility.RandomMinMax( 1, 5 ) == 1 && suit == "assassin" )
				{
					AssassinShroud robe = new AssassinShroud( );
						robe.Hue = clothHue;
						m.AddItem( robe );
				}
			}
			else
			{
				m.AddItem( new Boots( Utility.RandomNeutralHue() ) );
				if ( 1 == Utility.RandomMinMax( 1, 2 ) ){ m.AddItem( new Cloak( Utility.RandomNeutralHue() ) ); }

				Item cloth1 = new FancyShirt();
					cloth1.Hue = clothHue;
					m.AddItem( cloth1 );

				if ( ( m.Body == 0x191 || m.Body == 606 ) && Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					Item cloth2 = new Skirt();
						cloth2.Hue = clothHue;
						m.AddItem( cloth2 );
				}
				else
				{
					Item cloth3 = new LeatherLegs();
					cloth3.Hue = clothHue;
					m.AddItem( cloth3 );
				}

				if ( Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					LeatherChest jacket = new LeatherChest( );
						jacket.Name = suit + " tunic";
						jacket.Hue = clothHue;
						m.AddItem( jacket );
				}

				if ( Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					Item glove = new LeatherGloves();
						glove.Hue = clothHue;
						m.AddItem( glove );
				}
			}

			switch ( myTitle )
			{
				case 0: m.Title = "the " + race + "thief"; break;
				case 1: m.Title = "the " + race + "rogue"; break;
				case 2: m.Title = "the " + race + "outlaw"; break;
				case 3: m.Title = "the " + race + "bandit"; break;
				case 4: m.Title = "the " + race + "pickpocket"; break;
				case 5: m.Title = "the " + race + "burglar"; break;
				case 6: m.Title = "the " + race + "robber"; break;
				case 7: m.Title = "the " + race + "criminal"; break;
				case 8: m.Title = "the " + race + "prowler"; break;
				case 9: m.Title = "the " + race + "pilferer"; break;
			}

			if ( m is Brigand ){ m.Title = "the " + race + "brigand"; }

			if ( Utility.RandomMinMax( 1, 2 ) == 2 || m.Title == "the " + race + "ninja" )
			{
				int myHat = Utility.RandomMinMax( 0, 1 );
				if ( m.Body == 605 && myHelm == 7 ){ myHelm = 8; }

				switch ( myHelm )
				{
					case 0: Item helm1 = new SkullCap(); 			helm1.Hue = clothHue; m.AddItem( helm1 ); break;
					case 1: Item helm2 = new FloppyHat(); 			helm2.Hue = clothHue; m.AddItem( helm2 ); break;
					case 2: Item helm3 = new LeatherCap(); 			helm3.Hue = clothHue; m.AddItem( helm3 ); break;
					case 3: Item helm4 = new FeatheredHat(); 		helm4.Hue = clothHue; m.AddItem( helm4 ); break;
					case 4: Item helm5 = new MagicJewelryCirclet(); helm5.Hue = clothHue; m.AddItem( helm5 ); break;
					case 5: Item helm6 = new WideBrimHat(); 		helm6.Hue = clothHue; m.AddItem( helm6 ); break;
					case 6: Item helm7 = new LeatherNinjaHood(); 	helm7.Hue = clothHue; m.AddItem( helm7 ); break;
					case 7: Item helm8 = new ClothCowl(); 			helm8.Hue = clothHue; m.AddItem( helm8 ); break;
					case 8: Item helm9 = new ClothHood(); 			helm9.Hue = clothHue; m.AddItem( helm9 ); break;
				}
			}

			int CanHaveShield = 1;

			if ( iArmor == 2 )
			{
				CanHaveShield = 0; 
				switch ( Utility.Random( 7 ) )
				{
					case 0: m.AddItem( new Daisho() ); break;
					case 1: m.AddItem( new Nunchaku() ); break;
					case 2: m.AddItem( new Sai() ); break;
					case 3: m.AddItem( new Kama() ); break;
					case 4: m.AddItem( new NoDachi() ); break;
					case 5: m.AddItem( new Tekagi() ); break;
					case 6: m.AddItem( new Wakizashi() ); break;
				}
			}
			else
			{
				switch ( Utility.Random( 16 ) )
				{
					case 0: m.AddItem( new Cutlass() ); break;
					case 1: m.AddItem( new Katana() ); break;
					case 2: m.AddItem( new Kryss() ); break;
					case 3: m.AddItem( new Broadsword() ); break;
					case 4: m.AddItem( new Longsword() ); break;
					case 5: m.AddItem( new ThinLongsword() ); break;
					case 6: m.AddItem( new Scimitar() ); break;
					case 7: m.AddItem( new BoneHarvester() ); break;
					case 8: m.AddItem( new ShortSpear() ); CanHaveShield = 0; break;
					case 9: m.AddItem( new Club() ); break;
					case 10: m.AddItem( new Dagger() ); break;
					case 11: m.AddItem( new AssassinSpike() ); break;
					case 12: m.AddItem( new RuneBlade() ); CanHaveShield = 0; break;
					case 13: m.AddItem( new Leafblade() ); break;
					case 14: m.AddItem( new ElvenSpellblade() ); break;
					case 15: m.AddItem( new ElvenMachete() ); break;
				}
			}

			if ( CanHaveShield == 1 && Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				Item shield1 = new Buckler();
				shield1.Hue = clothHue; 
				m.AddItem( shield1 );
			}

			if ( reg.IsPartOf( "the Ruins of the Black Blade" ) )
			{
				MorphingTime.ColorMyClothes( m, 0x497 );
				m.Title = "the dark assassin";
			}
			else if ( reg.IsPartOf( "Stonegate Castle" ) )
			{
				MorphingTime.ColorMyClothes( m, 0x541 );
				m.Title = "the shadow assassin";
				m.Hue = 0x4001;
				m.HairItemID = 0;
				m.FacialHairItemID = 0;
				m.BaseSoundID = 0x482;
			}
			else if (m is BaseCreature)
			{
				GiveAdventureGear( (BaseCreature)m );
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void GiveAdventureGear( BaseCreature m )
		{
			if ( Utility.RandomMinMax( 1, 10 ) > 3 )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: m.PackItem( new BreadLoaf( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 1: m.PackItem( new CheeseWheel( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 2: m.PackItem( new Ribs( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 3: m.PackItem( new Apple( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 4: m.PackItem( new CookedBird( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 5: m.PackItem( new LambLeg( Utility.RandomMinMax( 1, 3 ) ) ); break;
				}
			}
			if ( Utility.RandomMinMax( 1, 10 ) > 3 )
			{
				switch ( Utility.RandomMinMax( 0, 4 ) )
				{
					case 0: m.PackItem( new BeverageBottle( BeverageType.Ale ) ); break;
					case 1: m.PackItem( new BeverageBottle( BeverageType.Wine ) ); break;
					case 2: m.PackItem( new BeverageBottle( BeverageType.Liquor ) ); break;
					case 3: m.PackItem( new Jug( BeverageType.Cider ) ); break;
					case 4: m.PackItem( new Waterskin() ); break;
				}
			}
			if ( Utility.RandomMinMax( 1, 10 ) > 3 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: m.PackItem( new Torch() ); break;
					case 1: m.PackItem( new Candle() ); break;
					case 2: m.PackItem( new Lantern() ); break;
				}
			}
			if ( Utility.RandomMinMax( 1, 10 ) > 3 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: m.PackItem( new Bandage( Utility.RandomMinMax( 5, 15 ) ) ); break;
					case 1: LesserCurePotion pot1 = new LesserCurePotion(); pot1.Amount = Utility.RandomMinMax( 1, 2 ); m.PackItem( pot1 ); break;
					case 2: LesserHealPotion pot2 = new LesserHealPotion(); pot2.Amount = Utility.RandomMinMax( 1, 2 ); m.PackItem( pot2 ); break;
				}
			}

			if ( Utility.RandomMinMax( 1, 10 ) > 3 )
			{
				switch ( Utility.RandomMinMax( 0, 8 ) )
				{
					case 0: TenFootPole pole = new TenFootPole(); pole.Charges = Utility.RandomMinMax( 1, 10 ); m.PackItem( pole ); break;
					case 1: m.PackItem( new Lockpick() ); break;
					case 2: m.PackItem( new SkeletonsKey() ); break;
					case 3: m.PackItem( new Bottle( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 4: m.PackItem( new Pouch() ); break;
					case 5: m.PackItem( new Bag() ); break;
					case 6: m.PackItem( new Bedroll() ); break;
					case 7: m.PackItem( new Kindling( Utility.RandomMinMax( 1, 3 ) ) ); break;
					case 8: m.PackItem( new BlueBook() ); break;
				}
			}
			GiveTorch( m );
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void BeforeMyDeath( Mobile from )
		{
			ArrayList targets = new ArrayList();

			foreach ( Mobile myPet in from.GetMobilesInRange( 30 ) )
			{
				if ( myPet is BaseCreature )
				{
					if ( ((BaseCreature)myPet).YellHue == from.Serial && ((BaseCreature)myPet).ControlSlots == 666 )
					{
						targets.Add( myPet );
					}
				}
			}

			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile pet = ( Mobile )targets[ i ];
				Effects.SendLocationParticles( EffectItem.Create( pet.Location, pet.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				pet.PlaySound( 0x1FE );
				pet.Delete();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static bool HealThySelf( Mobile from )
		{
			if ( from.Mana > 20 && Utility.RandomMinMax( 1, 4 ) == 1  )
			{
				from.Mana = from.Mana - 20;

				if ((from.HitsMax - from.Hits) <= 250)
					from.Hits = from.HitsMax;
				else
				{
					if (Utility.RandomBool())
						from.Hits += (int)((double)from.HitsMax * 0.25);
					else
						from.Hits += 250;
				}
				from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
				from.PlaySound( 0x202 );
				return true;
			}

			return false;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void MakeAssassinNote( Mobile from )
		{
			bool MakeNote = false;

			if ( from.Title.Contains(" assassin") ){ MakeNote = true; }
			else if ( from.Title.Contains(" hunter") ){ MakeNote = true; }
			else if ( from.Title.Contains(" ninja") ){ MakeNote = true; }

			if ( Utility.RandomMinMax( 1, 10 ) > 1 ){ MakeNote = false; }

			if ( MakeNote )
			{
				Mobile killer = from.LastKiller;

				Region reg = Region.Find( from.Location, from.Map );
				
				if (killer is BaseCreature)
				{
					BaseCreature bc_killer = (BaseCreature)killer;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							killer = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							killer=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							killer=bc_killer.BardMaster;
					}
				}

				if ( killer is PlayerMobile )
				{
					int gold = (int)((killer.RawStr + killer.RawDex + killer.RawInt) / 12 ) + Utility.RandomMinMax( 0, 5 );
					string a = "him";
					string b = "he";
					string c = "his";

					if ( killer.Body == 0x191 )
					{
						a = "her";
						b = "she";
						c = "her";
					} 

					string ScrollText = from.Name + ",<br><br>You have been given a task by " + RandomThings.GetRandomSociety() + ". You are to find " + killer.Name + " and make sure you kill " + a + " while " + b + " is in " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + ". When the deed is done, meet " + QuestCharacters.ParchmentWriter() + " in " + RandomThings.GetRandomCity() + " where you can collect your " + gold.ToString() + ",000 gold.<br><br> - " + QuestCharacters.ParchmentWriter();

					switch ( Utility.RandomMinMax( 0, 9 ) )
					{
						case 1:	ScrollText = "If we are going to carry out our plans, you need to kill " + killer.Name + " as we think " + b + " will become a problem for us. We heard from the " + RandomThings.GetRandomJob() + " in " + RandomThings.GetRandomCity() + " that " + b + " may be going to " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + ". Wait for " + a + " there and strike when the time is right. If we do not see you return to " + RandomThings.GetRandomCity() + " soon, we will assume you failed.<br><br> - " + RandomThings.GetRandomSociety();
							break;
						case 2:	ScrollText = QuestCharacters.ParchmentWriter() + ",<br><br>We all know who stole the " + gold.ToString() + ",000 gold from me in " + RandomThings.GetRandomCity() + ". I will make a deal with you. You find out where " + from.Name + " is hiding and tell them that I will overlook this incident if they do something for me. They need to go to " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + " and wait in the shadows for " + killer.Name + " to arrive. When " + b + " is spotted, they need to kill " + a + " before " + b + " finds what " + b + " is looking for. When the deed is done, the debt will be forgiven.<br><br> - " + QuestCharacters.ParchmentWriter();
							break;
						case 3:	ScrollText = from.Name + ",<br><br>The time is almost near, but there are some that fear of " + killer.Name + " causing a problem for us. We need to send " + a + " away from this life before " + b + " realizes what we are about to do. " + QuestCharacters.ParchmentWriter() + " has followed " + a + " to " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + " so you can probably find " + a + " there. Do not fail, as you would not want to face the judgement of " + RandomThings.GetRandomSociety() + ".";
							break;
						case 4:	ScrollText = QuestCharacters.ParchmentWriter() + ",<br><br>I have another problem for you to take care of. A " + RandomThings.GetRandomJob() + " in " + RandomThings.GetRandomCity() + " is paying us " + gold.ToString() + ",000 gold to assassinate the one who killed their friend. I think we should send " + from.Name + " to deal with " + a + ", and " + b + " is known as " + killer.Name + ". I believe they headed in the direction of " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + ". Have them wait for " + a + " there.<br><br> - " + QuestCharacters.ParchmentWriter();
							break;
						case 5:	ScrollText = from.Name + ",<br><br>I was in " + RandomThings.GetRandomCity() + " and I heard some whispers of a " + gold.ToString() + ",000 gold bounty on " + killer.Name + ". I also heard that " + RandomThings.GetRandomSociety() + " is the one offering the gold. I don't need to remind you that we have been trying to gain their trust so we can acquire " + Server.Misc.QuestCharacters.QuestItems( true ) + ". I paid the barkeep a few coins and found out where " + b + " might be. I will check out the place nearby. You head to " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + " and look for " + a + " there. We will meet each other in " + RandomThings.GetRandomCity() + " in a few days. This could be the chance we were waiting for.<br><br> - " + QuestCharacters.ParchmentWriter();
							break;
						case 6:	ScrollText = from.Name + ",<br><br>You are to slay the one they call " + killer.Name + ". They have been a little too curious of what lies within " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + ", and we do not need " + a + " getting in our way to find " + Server.Misc.QuestCharacters.QuestItems( true ) + ". If " + b + " finds it before we do, it could mean our very lives " + RandomThings.GetRandomSociety() + " will want. I will be going to " + RandomThings.GetRandomCity() + " to get some supplies, but I will return soon.<br><br> - " + QuestCharacters.ParchmentWriter();
							break;
						case 7:	ScrollText = from.Name + ",<br><br>Now that you eliminated " + QuestCharacters.ParchmentWriter() + ", it is time for your next target. This will be well worth your time as the fee being paid for this one is " + gold.ToString() + ",000 gold. They are known as " + killer.Name + ", and " + b + " has been talking about exploring " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + ". If this is true, then " + QuestCharacters.ParchmentWriter() + " would have to hide " + Server.Misc.QuestCharacters.QuestItems( true ) + " elsewhere. You can guess by the gold being offered, that they would rather not resort to such actions. Take care of " + a + " and we will split the gold in " + RandomThings.GetRandomCity() + ".";
							break;
						case 8:	ScrollText = QuestCharacters.ParchmentWriter() + ",<br><br>" + killer.Name + " has meddled in our plans for the last time. " + QuestCharacters.ParchmentWriter() + " claims that " + b + " doesn't even know of " + c + " participation in my annoyance, but the time to act is now. I think we should give this mission to " + from.Name + ", as they have yet to fail us. Send " + from.Name + " to " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) + " and kill " + a + " before " + b + " leaves the area. Bring back " + Server.Misc.QuestCharacters.QuestItems( true ) + " if " + b + " is found with it.<br><br> - " + QuestCharacters.ParchmentWriter();
							break;
						case 9:	ScrollText = from.Name + ",<br><br>I left that magic item in a dungeon chest for safe keeping, but " + killer.Name + " ended up taking it! Find " + a + " and kill " + a + "! See if " + b + " still has it. If you find it, head to " + RandomThings.GetRandomCity() + " and give it to the " + RandomThings.GetRandomJob() + ". They'll know what to do with it.";
							break;
					}

					AssassinNote letter = new AssassinNote();
					letter.LetterMessage = ScrollText;
					((BaseCreature)from).PackItem( letter );
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void ChooseMonk( Mobile m, string race )
		{
			BaseCreature bc = (BaseCreature)m;

			Region reg = Region.Find( bc.Home, m.Map );

			m.Title = "the " + race + "monk";

			int level = 0;
			int color = 0;
			string named = race + "monk robe";

			if ( reg.IsPartOf( "Mangar's Tower" ) || reg.IsPartOf( "Mangar's Chamber" ) )
			{
				level = 5;
				color = 0xB89;
				named = "ivory monk robe";
				m.Title = "the ivory monk";
			}
			else if ( reg.IsPartOf( "Kylearan's Tower" ) )
			{
				level = 4;
				color = 0x125;
				named = "azure monk robe";
				m.Title = "the azure monk";
			}
			else if ( reg.IsPartOf( "Harkyn's Castle" ) )
			{
				level = 3;
				color = 0x8A;
				named = "scarlet monk robe";
				m.Title = "the scarlet monk";
			}
			else if ( reg.IsPartOf( "the Catacombs" ) )
			{
				level = 3;
				color = 0xB95;
				named = "jade monk robe";
				m.Title = "the jade monk";
			}
			else if ( reg.IsPartOf( "the Lower Catacombs" ) )
			{
				level = 2;
				color = 0xB95;
				named = "jade monk robe";
				m.Title = "the jade monk";
			}
			else if ( reg.IsPartOf( "the Tower of Brass" ) )
			{
				color = 2413;
				named = "brass monk robe";
				m.Title = "the brass monk";
			}
			else if ( reg.IsPartOf( "the Ruins of the Black Blade" ) )
			{
				color = 0x497;
				named = "dark monk robe";
				m.Title = "the dark monk";
			}
			else if ( reg.IsPartOf( "Stonegate Castle" ) )
			{
				color = 0x541;
				named = "shadow monk robe";
				m.Title = "the shadow monk";
				m.Hue = 0x4001;
				m.BaseSoundID = 0x482;
				m.HairItemID = 0;
				m.FacialHairItemID = 0;
			}

			int myBonus = 10;
			int myMinDmg = 8;
			int myMaxDmg = 12;
			int myResist = 5;

			int RandomRoll = Utility.RandomMinMax( 1, 6 );
			if ( level > 0 ){ RandomRoll = level; }

			switch ( RandomRoll )
			{
				case 1: myBonus = 10; myMinDmg = 8;  myMaxDmg = 12; myResist = 5;  break;
				case 2: myBonus = 20; myMinDmg = 9;  myMaxDmg = 13; myResist = 10; break;
				case 3: myBonus = 30; myMinDmg = 10; myMaxDmg = 14; myResist = 15; break;
				case 4: myBonus = 40; myMinDmg = 11; myMaxDmg = 15; myResist = 20; break;
				case 5: myBonus = 50; myMinDmg = 12; myMaxDmg = 16; myResist = 25; break;
				case 6: myBonus = 60; myMinDmg = 13; myMaxDmg = 17; myResist = 30; break;
			}

			m.RawStr = m.RawStr + myBonus;
			m.RawDex = m.RawDex + myBonus;
			m.RawInt = m.RawInt + myBonus;

			m.Hits = m.HitsMax;
			m.Mana = m.ManaMax;
			m.Stam = m.StamMax;

			bc.DamageMin = myMinDmg;
			bc.DamageMax = myMaxDmg;

			bc.SetDamageType( ResistanceType.Physical, 100 );

			bc.SetResistance( ResistanceType.Physical, ( 10 + myResist ) );
			bc.SetResistance( ResistanceType.Fire, myResist );
			bc.SetResistance( ResistanceType.Cold, myResist );
			bc.SetResistance( ResistanceType.Poison, myResist );
			bc.SetResistance( ResistanceType.Energy, myResist );

			bc.SetSkill( SkillName.DetectHidden, ( 20.0 + myBonus ) );
			bc.SetSkill( SkillName.Anatomy, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.MagicResist, ( 20.0 + myBonus ) );
			bc.SetSkill( SkillName.Macing, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Fencing, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Wrestling, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Swords, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Tactics, ( 50.0 + myBonus ) );

			m.Fame = m.Fame * myBonus;
			m.Karma = m.Karma * myBonus;

			m.VirtualArmor = myResist;

			List<Item> belongings = new List<Item>();
			foreach( Item i in m.Backpack.Items )
			{
				belongings.Add(i);
			}
			foreach ( Item stuff in belongings )
			{
				stuff.Delete();
			}

			bc.GenerateLoot( true );

			int clothHue = RandomThings.GetRandomColor( 0 );
				if ( color > 0 ){ clothHue = color; }

			if ( Utility.RandomBool() )
			{
				Item robe = new VagabondRobe();
					robe.Hue = clothHue;
					robe.Name = named;
					m.AddItem( robe ); 
			}
			else if ( m.Body == 0x190 || m.Body == 605 )
			{
				Item robe = new Robe();
					robe.Hue = clothHue;
					robe.Name = named;
					m.AddItem( robe ); 
			}
			else
			{
				Item dress = new PlainDress();
					dress.Hue = clothHue;
					dress.Name = named;
					m.AddItem( dress ); 
			}

			Item sandals = new Sandals();
				sandals.Hue = Utility.RandomNeutralHue();
				m.AddItem( sandals ); 

			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				PugilistGlove glove = new PugilistGlove();
					glove.LootType = LootType.Blessed;
					glove.Movable = false;
					switch ( Utility.RandomMinMax( 1, 12 ) )
					{
						case 1: glove.WeaponAttributes.HitLeechHits = myBonus; break;
						case 2: glove.WeaponAttributes.HitLeechStam = myBonus; break;
						case 3: glove.WeaponAttributes.HitLeechMana = myBonus; break;
						case 4: glove.WeaponAttributes.HitMagicArrow = myBonus; break;
						case 5: glove.WeaponAttributes.HitHarm = myBonus; break;
						case 6: glove.WeaponAttributes.HitFireball = myBonus; break;
						case 7: glove.WeaponAttributes.HitLightning = myBonus; break;
						case 8: glove.WeaponAttributes.HitColdArea = myBonus; break;
						case 9: glove.WeaponAttributes.HitFireArea = myBonus; break;
						case 10: glove.WeaponAttributes.HitPoisonArea = myBonus; break;
						case 11: glove.WeaponAttributes.HitEnergyArea = myBonus; break;
						case 12: glove.WeaponAttributes.HitPhysicalArea = myBonus; break;
					}
					glove.Attributes.ReflectPhysical = myBonus;
					glove.Hue = Utility.RandomNeutralHue();
					glove.MinDamage = myMinDmg + 5;
					glove.MaxDamage = myMaxDmg + 5;
					glove.Name = "monk gloves";
					glove.Hue = m.Hue;
					m.AddItem( glove );
			}
			else
			{
				QuarterStaff ring = new QuarterStaff();
					ring.LootType = LootType.Blessed;
					ring.Movable = false;
					switch ( Utility.RandomMinMax( 1, 12 ) )
					{
						case 1: ring.WeaponAttributes.HitLeechHits = myBonus; break;
						case 2: ring.WeaponAttributes.HitLeechStam = myBonus; break;
						case 3: ring.WeaponAttributes.HitLeechMana = myBonus; break;
						case 4: ring.WeaponAttributes.HitMagicArrow = myBonus; break;
						case 5: ring.WeaponAttributes.HitHarm = myBonus; break;
						case 6: ring.WeaponAttributes.HitFireball = myBonus; break;
						case 7: ring.WeaponAttributes.HitLightning = myBonus; break;
						case 8: ring.WeaponAttributes.HitColdArea = myBonus; break;
						case 9: ring.WeaponAttributes.HitFireArea = myBonus; break;
						case 10: ring.WeaponAttributes.HitPoisonArea = myBonus; break;
						case 11: ring.WeaponAttributes.HitEnergyArea = myBonus; break;
						case 12: ring.WeaponAttributes.HitPhysicalArea = myBonus; break;
					}
					ring.Attributes.ReflectPhysical = myBonus;
					ring.Hue = Utility.RandomNeutralHue();
					ring.MinDamage = myMinDmg + 5;
					ring.MaxDamage = myMaxDmg + 5;
					ring.Name = "a monk staff";
					m.AddItem( ring );
			}

			Server.Misc.IntelligentAction.GiveAdventureGear( bc );
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void ChooseFighter( Mobile m, string race )
		{
			BaseCreature bc = (BaseCreature)m;

			Region reg = Region.Find( bc.Home, m.Map );

			int level = 0;
			int equip = 0;

			if ( reg.IsPartOf( "Mangar's Tower" ) || reg.IsPartOf( "Mangar's Chamber" ) )
			{
				level = 5;
				equip = 1;
			}
			else if ( reg.IsPartOf( "Kylearan's Tower" ) )
			{
				level = 4;
			}
			else if ( reg.IsPartOf( "Harkyn's Castle" ) || reg.IsPartOf( "the Catacombs" ) )
			{
				level = 3;
			}
			else if ( reg.IsPartOf( "the Lower Catacombs" ) || reg.IsPartOf( "the Sewers" ) )
			{
				level = 2;
			}
			else if ( reg.IsPartOf( "the Mines" ) || reg.IsPartOf( "the Cellar" ) )
			{
				level = 1;
			}
			else if ( reg.IsPartOf( "the Vault of the Black Knight" ) )
			{
				level = 4;
				equip = 2;
			}
			else if ( reg.IsPartOf( "the Tomb of Kazibal" ) )
			{
				equip = 3;
			}
			else if ( reg.IsPartOf( "Stonegate Castle" ) )
			{
				level = 4;
				equip = 4;
			}
			else if ( reg.IsPartOf( "the Azure Castle" ) )
			{
				level = 3;
				equip = 5;
			}

			int myBonus = 10;
			int myMinDmg = 8;
			int myMaxDmg = 18;
			int myResist = 5;

			int RandomRoll = Utility.RandomMinMax( 1, 6 );
			if ( level > 0 ){ RandomRoll = level; }

			switch ( RandomRoll )
			{
				case 1: myBonus = 10; myMinDmg = 8;  myMaxDmg = 18; myResist = 5;  break;
				case 2: myBonus = 20; myMinDmg = 9;  myMaxDmg = 19; myResist = 10; break;
				case 3: myBonus = 30; myMinDmg = 10; myMaxDmg = 20; myResist = 15; break;
				case 4: myBonus = 40; myMinDmg = 11; myMaxDmg = 21; myResist = 20; break;
				case 5: myBonus = 50; myMinDmg = 12; myMaxDmg = 22; myResist = 25; break;
				case 6: myBonus = 60; myMinDmg = 13; myMaxDmg = 23; myResist = 30; break;
			}

			m.RawStr = m.RawStr + myBonus;
			m.RawDex = m.RawDex + myBonus;
			m.RawInt = m.RawInt + myBonus;

			m.Hits = m.HitsMax;
			m.Mana = m.ManaMax;
			m.Stam = m.StamMax;

			bc.DamageMin = myMinDmg;
			bc.DamageMax = myMaxDmg;

			bc.SetDamageType( ResistanceType.Physical, 100 );

			bc.SetResistance( ResistanceType.Physical, ( 10 + myResist ) );
			bc.SetResistance( ResistanceType.Fire, myResist );
			bc.SetResistance( ResistanceType.Cold, myResist );
			bc.SetResistance( ResistanceType.Poison, myResist );
			bc.SetResistance( ResistanceType.Energy, myResist );

			bc.SetSkill( SkillName.DetectHidden, ( 20.0 + myBonus ) );
			bc.SetSkill( SkillName.Anatomy, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.MagicResist, ( 20.0 + myBonus ) );
			bc.SetSkill( SkillName.Macing, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Fencing, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Wrestling, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Swords, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.Tactics, ( 50.0 + myBonus ) );
			bc.SetSkill( SkillName.DetectHidden, ( 50.0 + myBonus ) );

			m.Fame = m.Fame * myBonus;
			m.Karma = m.Karma * myBonus;

			m.VirtualArmor = myResist;

			List<Item> belongings = new List<Item>();
			foreach( Item i in m.Backpack.Items )
			{
				belongings.Add(i);
			}
			foreach ( Item stuff in belongings )
			{
				stuff.Delete();
			}

			bc.GenerateLoot( true );

			if ( equip == 1 ) // MANGAR'S TOWER
			{
				m.Hue = 0x497;
				m.Title = "of Mangar's guard";
				m.HairHue = 0x47E;
				m.FacialHairHue = 0x47E;

				BoneArms piece1 = new BoneArms( );
					piece1.Hue = 0x497;
					piece1.Name = "dark guard arms";
					m.AddItem( piece1 );

				BoneChest piece2 = new BoneChest( );
					piece2.Hue = 0x497;
					piece2.Name = "dark guard tunic";
					m.AddItem( piece2 );

				BoneGloves piece3 = new BoneGloves( );
					piece3.Hue = 0x497;
					piece3.Name = "dark guard gloves";
					m.AddItem( piece3 );

				BoneLegs piece4 = new BoneLegs( );
					piece4.Hue = 0x497;
					piece4.Name = "dark guard leggings";
					m.AddItem( piece4 );

				BoneHelm piece5 = new BoneHelm( );
					piece5.Hue = 0x497;
					piece5.Name = "dark guard helm";
					m.AddItem( piece5 );

				RoyalBoots piece6 = new RoyalBoots( );
					piece6.Hue = 0x497;
					piece6.Name = "dark guard boots";
					m.AddItem( piece6 );

				Halberd piece7 = new Halberd( );
					piece7.Hue = 0x497;
					piece7.Name = "dark guard halberd";
					m.AddItem( piece7 );

					bc.SetSkill( SkillName.DetectHidden, 90, 120 );

			}
			else if ( equip == 2 ) // BLACK KNIGHT'S VAULT
			{
				m.Title = "the black guard";
				m.AddItem( new Boots( ) );

				ChaosShield shield = new ChaosShield( );

				switch ( Utility.Random( 10 ) )
				{
					case 0: BattleAxe weapon1 = new BattleAxe( ); weapon1.Hue = 0x497; m.AddItem( weapon1 ); break;
					case 1: Halberd weapon2 = new Halberd( ); weapon2.Hue = 0x497; m.AddItem( weapon2 ); break;
					case 2: DoubleAxe weapon3 = new DoubleAxe( ); weapon3.Hue = 0x497; m.AddItem( weapon3 ); break;
					case 3: ExecutionersAxe weapon4 = new ExecutionersAxe( ); weapon4.Hue = 0x497; m.AddItem( weapon4 ); break;
					case 4: WarAxe weapon5 = new WarAxe( ); weapon5.Hue = 0x497; m.AddItem( weapon5 ); break;
					case 5: TwoHandedAxe weapon6 = new TwoHandedAxe( ); weapon6.Hue = 0x497; m.AddItem( weapon6 ); break;
					case 6: VikingSword weapon7 = new VikingSword( ); weapon7.Hue = 0x497; m.AddItem( weapon7 ); if ( Utility.Random( 3 ) == 1 ){ shield.Hue = 0x497; m.AddItem( shield ); } break;
					case 7: ThinLongsword weapon8 = new ThinLongsword( ); weapon8.Hue = 0x497; m.AddItem( weapon8 ); if ( Utility.Random( 3 ) == 1 ){ shield.Hue = 0x497; m.AddItem( shield ); } break;
					case 8: Longsword weapon9 = new Longsword( ); weapon9.Hue = 0x497; m.AddItem( weapon9 ); if ( Utility.Random( 3 ) == 1 ){ shield.Hue = 0x497; m.AddItem( shield ); } break;
					case 9: Broadsword weapon0 = new Broadsword( ); weapon0.Hue = 0x497; m.AddItem( weapon0 ); if ( Utility.Random( 3 ) == 1 ){ shield.Hue = 0x497; m.AddItem( shield ); } break;
				}

				Robe robe = new Robe( );
					robe.Name = "black guard robe";
					robe.Hue = 0x497;
					m.AddItem( robe );

				LeatherGloves gloves = new LeatherGloves( );
					gloves.Name = "black guard gloves";
					gloves.Hue = 0x497;
					m.AddItem( gloves );

				LeatherCap cap = new LeatherCap( );
					cap.Name = "black guard cap";
					cap.Hue = 0x497;
					m.AddItem( cap );
			}
			else if ( equip == 3 ) // TOMB OF KAZIBAL
			{
				m.Hue = 0x8420;
				m.Name = "a kaztec warrior";
				m.Title = null;
				m.HairItemID = 0;
				m.FacialHairItemID = 0;

				BoneArms piece1 = new BoneArms( );
					piece1.Hue = 0x83B;
					piece1.Name = "kaztec arms";
					m.AddItem( piece1 );

				BoneGloves piece3 = new BoneGloves( );
					piece3.Hue = 0x83B;
					piece3.Name = "kaztec gloves";
					m.AddItem( piece3 );

				BoneLegs piece4 = new BoneLegs( );
					piece4.Hue = 0x83B;
					piece4.Name = "kaztec leggings";
					m.AddItem( piece4 );

				TribalMask piece5 = new TribalMask( );
					piece5.Hue = 0x83B;
					piece5.Name = "kaztec mask";
					m.AddItem( piece5 );

				if ( m.Body == 0x191 || m.Body == 606 )
				{
					LeatherBustierArms piece2 = new LeatherBustierArms( );
						piece2.Hue = 0x83B;
						piece2.Name = "kaztec bustier";
						m.AddItem( piece2 );
				}

				if ( Utility.RandomMinMax( 1, 2 ) == 2 )
				{
					Item cloth1 = new LoinCloth();
						cloth1.Hue = 0x83B;
						cloth1.Name = "kaztec loin cloth";
						m.AddItem( cloth1 );
				}
				else
				{
					Item cloth2 = new LeatherSkirt();
						cloth2.Hue = 0x83B;
						cloth2.Name = "kaztec skirt";
						cloth2.Layer = Layer.Waist;
						m.AddItem( cloth2 );
				}

				Spear piece7 = new Spear( );
					piece7.Name = "kaztec spear";
					m.AddItem( piece7 );
			}
			else if ( equip == 4 ) // STONEGATE CASTLE
			{
				m.Title = "the shadow guard";
				m.Hue = 0x4001;
				m.BaseSoundID = 0x482;
				m.HairItemID = 0;
				m.FacialHairItemID = 0;

				GiveBasicWepShld( m );

				Item helm = new OrcHelm();
					helm.Name = "great helm";
					helm.ItemID = 0x2645;
					m.AddItem( helm );

				m.AddItem( new PlateArms() );
				m.AddItem( new PlateGorget() );
				m.AddItem( new PlateLegs() );
				m.AddItem( new PlateChest() );
				m.AddItem( new PlateGloves() );

				MorphingTime.ColorMyClothes( m, 0x541 );
			}
			else if ( equip == 5 ) // AZURE CASTLE
			{
				m.Title = "the azure guard";

				GiveBasicWepShld( m );

				Item helm = new OrcHelm();
					helm.Name = "great helm";
					helm.ItemID = 0x2645;
					m.AddItem( helm );

				m.AddItem( new PlateArms() );
				m.AddItem( new PlateGorget() );
				m.AddItem( new PlateLegs() );
				m.AddItem( new PlateChest() );
				m.AddItem( new PlateGloves() );

				MorphingTime.ColorMyClothes( m, 0x538 );
			}
			else if ( race == "undead " )
			{
				Server.Misc.IntelligentAction.DressUpFighters( m, race, true, 0);
			}
			else 
			{
				Server.Misc.IntelligentAction.DressUpFighters( m, race, true, 0 );
				Server.Misc.IntelligentAction.GiveAdventureGear( bc );
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void PunchStun( Mobile m )
		{
			if ( ( 100 - m.PhysicalResistance ) < Utility.RandomMinMax( 1, 100 ) && Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				int duration = Utility.RandomMinMax(4, 12);
				m.Paralyze(TimeSpan.FromSeconds(duration));
				m.Warmode = false;
				m.LocalOverheadMessage( MessageType.Emote, 0xB1F, true, "You are hit with a stunning punch!" );
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void PoisonVictim( Mobile m, Mobile from )
		{
			bool CanPoison = false;

			int itSicks = 1;
			if ( from.Fame >= 3000 ){ itSicks = 5; }
			else if ( from.Fame >= 2500 ){ itSicks = 4; }
			else if ( from.Fame >= 2000 ){ itSicks = 3; }
			else if ( from.Fame >= 1500 ){ itSicks = 2; }

			if ( from.Hue == 0x430 && ( from is Urk || from is UrkShaman ) )
			{
				CanPoison = true;
				itSicks = 1;
			}
			else if ( from.EmoteHue == 1 )
			{
				CanPoison = true;
			}

			if ( !(Server.Items.HiddenTrap.SavingThrow( m, "Poison", false )) && Utility.RandomMinMax( 1, 5 ) == 1 && CanPoison == true )
			{
				switch( Utility.RandomMinMax( 1, itSicks ) )
				{
					case 1: m.ApplyPoison( m, Poison.Lesser );	break;
					case 2: m.ApplyPoison( m, Poison.Regular );	break;
					case 3: m.ApplyPoison( m, Poison.Greater );	break;
					case 4: m.ApplyPoison( m, Poison.Deadly );	break;
					case 5: m.ApplyPoison( m, Poison.Lethal );	break;
				}

				Effects.SendLocationEffect( m.Location, m.Map, 0x11A8 - 2, 16, 3, 0, 0 );
				Effects.PlaySound( m, m.Map, 0x201 );
				m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You have been poisoned!");
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void LeapToAttacker( Mobile from, Mobile m )
		{
			if ( from != null && from.Hits > 0 && Utility.RandomMinMax( 1, 5 ) == 1 )
			{
				Region myReg = Region.Find( from.Location, from.Map );
				Region foeReg = Region.Find( m.Location, m.Map );

				bool isNearby = false;
				foreach ( Mobile foe in from.GetMobilesInRange( 1 ) )
				{
					if ( foe == m )
					{
						isNearby = true;
					}
				}

				if ( isNearby == false && myReg == foeReg )
				{
					Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( from, from.Map, 0x201 );
					from.Location = m.Location;
					from.Combatant = m;
					from.Warmode = true;
					Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( from, from.Map, 0x201 );
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void HideStealMove( Mobile from, Mobile m )
		{
			int IHide = 0;

			if ( m != null && m.Hits > 0 )
			{
				int hits = m.HitsMax / 5;
				if ( m.Skills[SkillName.Hiding].Value >= Utility.RandomMinMax( 1, 120 ) && m.Hits <= hits && Utility.RandomMinMax( 1, 5 ) == 1 )
				{
					Map map = m.Map;
					bool validLocation = false;
					Point3D loc = m.Location;

					for ( int j = 0; !validLocation && j < 20; ++j )
					{
						int x = m.X + Utility.Random( 6 ) + 6;
							if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ x = m.X - Utility.Random( 6 ) - 6; }

						int y = m.Y + Utility.Random( 6 ) + 6;
							if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ y = m.Y - Utility.Random( 6 ) - 6; }

						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, m.Z, 16, false, false ) )
							loc = new Point3D( x, y, m.Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}
					
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( m, m.Map, 0x201 );
					m.Combatant = null;
					m.Warmode = false;
					m.CantWalk = true;
					m.Location = loc;
					m.Hidden = true;
					IHide = 1;
				}
			}

			if ( m != null && from != null && m.Hits > 0 && from is PlayerMobile && m.InRange( from, 2 ) && m.Map == from.Map )
			{
				if ( m.Skills[SkillName.Stealing].Value >= Utility.RandomMinMax( 1, 125 ) && m.Skills[SkillName.Snooping].Value >= Utility.RandomMinMax( 1, 100 ) )
				{
					if ( Utility.RandomMinMax( 1, 5 ) == 1 )
					{
						int c = 0;

						List<Item> belongings = new List<Item>();
						foreach( Item i in from.Backpack.Items )
						{
							if ( i.LootType != LootType.Ensouled && i.LootType != LootType.Blessed && i.TotalItems == 0 )
							{ belongings.Add(i); c++; }
						}

						int o = Utility.RandomMinMax( 0, c );

						foreach ( Item stuff in belongings )
						{
							o++;
							if ( c == o ){ ((BaseCreature)m).PackItem( stuff ); from.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, m.Name + " stole something from you!"); }
						}
					}
				}
			}

			if ( IHide > 0 && Utility.RandomMinMax( 1, 5 ) == 5 && m.Skills[SkillName.Stealth].Value >= Utility.RandomMinMax( 1, 125 ) )
			{
				m.Delete();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void HideFromOthers( Mobile m )
		{
			int IHide = 0;

			if ( m != null && m.Hits > 0 )
			{
				int hits = m.HitsMax / 5;
				if ( m.Skills[SkillName.Hiding].Value >= Utility.RandomMinMax( 1, 120 ) && m.Hits <= hits && Utility.RandomMinMax( 1, 4 ) == 1 )
				{
					Map map = m.Map;
					bool validLocation = false;
					Point3D loc = m.Location;

					for ( int j = 0; !validLocation && j < 20; ++j )
					{
						int x = m.X + Utility.Random( 6 ) + 6;
							if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ x = m.X - Utility.Random( 6 ) - 6; }

						int y = m.Y + Utility.Random( 6 ) + 6;
							if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ y = m.Y - Utility.Random( 6 ) - 6; }

						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, m.Z, 16, false, false ) )
							loc = new Point3D( x, y, m.Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}
					
					Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( m, m.Map, 0x201 );
					m.Combatant = null;
					m.Warmode = false;
					m.CantWalk = true;
					m.Location = loc;
					m.Hidden = true;
					IHide = 1;
				}
			}

			if ( IHide > 0 && Utility.RandomMinMax( 1, 5 ) == 5 && m.Skills[SkillName.Stealth].Value >= Utility.RandomMinMax( 1, 125 ) )
			{
				m.Delete();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void CryOut( Mobile from )
		{
			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: from.PlaySound( from.Female ? 0x14B : 0x154 ); break;
					case 1: from.PlaySound( from.Female ? 0x14C : 0x155 ); break;
					case 2: from.PlaySound( from.Female ? 0x14D : 0x156 ); break;
					case 3: from.PlaySound( from.Female ? 0x14E : 0x157 ); break;
					case 4: from.PlaySound( from.Female ? 0x14F : 0x158 ); break;
					case 5: from.PlaySound( from.Female ? 0x14F : 0x159 ); break;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static bool TestForReagent( Mobile from, string job )
		{
			if ( job == "necromancer" && from.Skills[SkillName.Necromancy].Value >= Utility.RandomMinMax( 25, 125 ) ){ return true; }
			if ( job == "alchemist" && from.Skills[SkillName.Alchemy].Value >= Utility.RandomMinMax( 25, 125 ) ){ return true; }
			if ( job == "undertaker" && from.Skills[SkillName.Forensics].Value >= Utility.RandomMinMax( 25, 125 ) ){ return true; }
			if ( job == "mixologist" && from.Skills[SkillName.Alchemy].Value >= Utility.RandomMinMax( 25, 125 ) && 
										from.Skills[SkillName.Cooking].Value >= Utility.RandomMinMax( 25, 125 ) ){ return true; }
			if ( job == "wizard" && from.Skills[SkillName.Magery].Value >= Utility.RandomMinMax( 25, 125 ) ){ return true; }
			if ( job == "herbalist" && from.Skills[SkillName.Cooking].Value >= Utility.RandomMinMax( 25, 125 ) && 
										from.Skills[SkillName.AnimalLore].Value >= Utility.RandomMinMax( 25, 125 ) ){ return true; }

			return false;
		}

		public static void DropReagent( Mobile player, BaseCreature monster )
		{
			if (monster.Backpack == null || monster.Map == null || monster is BaseUndead || monster is Zombiex )
				return;

			SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );
			SlayerEntry exorcism = SlayerGroup.GetEntryByName( SlayerName.Exorcism );
			SlayerEntry plants = SlayerGroup.GetEntryByName( SlayerName.WeedRuin );
			SlayerEntry gargoyle = SlayerGroup.GetEntryByName( SlayerName.GargoylesFoe );
			SlayerEntry poisoner = SlayerGroup.GetEntryByName( SlayerName.ElementalHealth );
			SlayerEntry rocks = SlayerGroup.GetEntryByName( SlayerName.EarthShatter );
			SlayerEntry flame = SlayerGroup.GetEntryByName( SlayerName.FlameDousing );
			SlayerEntry water = SlayerGroup.GetEntryByName( SlayerName.NeptunesBane );

			int DropThisMuch = Server.Misc.IntelligentAction.FameBasedLevel( monster );

			int amount = Utility.RandomMinMax( DropThisMuch, ( DropThisMuch * 3 ) );

			if ( undead.Slays(monster) && ( TestForReagent( player, "necromancer" ) || TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new GraveDust( amount ) );
			}
			if ( gargoyle.Slays(monster) && ( TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new GargoyleEar( Utility.RandomMinMax( 1, 2 ) ) );
			}
			if ( monster is PoisonElemental && ( TestForReagent( player, "necromancer" ) || TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new NoxCrystal( amount ) );
			}
			if ( rocks.Slays(monster) && ( TestForReagent( player, "necromancer" ) || TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new PigIron( amount ) );
			}
			if ( flame.Slays(monster) && ( TestForReagent( player, "mixologist" ) || TestForReagent( player, "alchemist" ) || TestForReagent( player, "wizard" ) ) )
			{
				int pick = 1; if ( TestForReagent( player, "mixologist" ) ){ pick = Utility.RandomMinMax( 0, 1 ); }
				switch ( pick ) 
				{
					case 0: monster.PackItem( new Brimstone( amount ) ); break;
					case 1: monster.PackItem( new SulfurousAsh( amount ) ); break;
				}
			}
			if ( water.Slays(monster) && ( TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new SeaSalt( amount ) );
			}
			if ( Server.Misc.Worlds.GetMyWorld( player.Map, player.Location, player.X, player.Y ) == "the Moon of Luna" && monster is Wisp && ( TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new MoonCrystal( amount ) );
			}
			if ( (monster.Name).Contains("beetle") && ( TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new BeetleShell( 1 ) );
			}
			if ( ( (monster.Name).Contains("frog") || (monster.Name).Contains("toad") ) && ( TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new EyeOfToad( Utility.RandomMinMax( 1, 2 ) ) );
			}
			if ( ( monster is Pixie || monster is Sprite || monster is Faerie ) && ( TestForReagent( player, "mixologist" ) ) )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) ) 
				{
					case 0: monster.PackItem( new FairyEgg( Utility.RandomMinMax( 1, 2 ) ) ); break;
					case 1: monster.PackItem( new PixieSkull( 1 ) ); break;
					case 2: monster.PackItem( new ButterflyWings( Utility.RandomMinMax( 1, 2 ) ) ); break;
				}
			}
			if ( (monster.Name).Contains("spider") && ( TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new SilverWidow( 1 ) );
			}
			if ( rocks.Slays(monster) && ( TestForReagent( player, "necromancer" ) || TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new PigIron( amount ) );
			}
			if ( ( monster is BloodElemental || exorcism.Slays(monster) ) && ( TestForReagent( player, "necromancer" ) || TestForReagent( player, "mixologist" ) ) )
			{
				monster.PackItem( new DaemonBlood( amount ) );
			}
			if ( plants.Slays(monster) && ( TestForReagent( player, "herbalist" ) || TestForReagent( player, "alchemist" ) || TestForReagent( player, "wizard" ) || TestForReagent( player, "mixologist" ) ) )
			{
				int pick = Utility.RandomMinMax( 0, 1 );
				if ( TestForReagent( player, "mixologist" ) ){ pick = Utility.RandomMinMax( 0, 3 ); }
				if ( TestForReagent( player, "herbalist" ) ){ pick = Utility.RandomMinMax( 4, 9 ); }
				switch ( pick ) 
				{
					case 0: monster.PackItem( new MandrakeRoot( amount ) ); break;
					case 1: monster.PackItem( new Nightshade( amount ) ); break;
					case 2: monster.PackItem( new SwampBerries( amount ) ); break;
					case 3: monster.PackItem( new RedLotus( amount ) ); break;
					case 4: Item plant1 = new PlantHerbalism_Leaf(); plant1.Amount = amount; monster.PackItem( plant1 ); break;
					case 5: Item plant2 = new PlantHerbalism_Flower(); plant2.Amount = amount; monster.PackItem( plant2 ); break;
					case 6: Item plant3 = new PlantHerbalism_Mushroom(); plant3.Amount = amount; monster.PackItem( plant3 ); break;
					case 7: Item plant4 = new PlantHerbalism_Lilly(); plant4.Amount = amount; monster.PackItem( plant4 ); break;
					case 8: Item plant5 = new PlantHerbalism_Cactus(); plant5.Amount = amount; monster.PackItem( plant5 ); break;
					case 9: Item plant6 = new PlantHerbalism_Grass(); plant6.Amount = amount; monster.PackItem( plant6 ); break;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void DropItem( Mobile from, Mobile killer )
		{
			if (	from is BoneMagi || from is SkeletalMage || from is SkeletalWizard || from is Lich || from is Vordo || from is Nazghoul || from is LichLord || from is DemiLich || 
					from is AncientLich || from is Surtaz || from is LichKing || from is UndeadDruid )
			{
				if ( from.Fame > Utility.Random( 40000 ) )
				{
					EvilSkull skull = new EvilSkull();
						skull.Name = "the skull of " + from.Name;
							if ( from.Title != null && from.Title != "" ){ skull.Name = skull.Name + " " + from.Title; skull.Hue = from.Hue; }
						from.AddItem( skull );
				}
			}

			if ( from is Lich || from is Nazghoul || from is LichLord || from is AncientLich || from is Surtaz || from is LichKing || from is DemiLich || from is UndeadDruid )
			{
				if ( killer != null )
				{
					string gear = "an old";
					int Magic = 1;
					int Mgear = 3;
					int MagicHue = Utility.RandomNeutralHue();
					int MgearHue = Utility.RandomNeutralHue();

					if ( killer is BaseCreature )
						killer = ((BaseCreature)killer).GetMaster();

					if ( killer is PlayerMobile )
					{
						if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
						{
							if ( from is Lich )
							{
								gear = "a lich";
								Magic = 1;
								Mgear = 3;
							}
							else if ( from is LichLord )
							{
								gear = "a lich lord";
								Magic = 3;
								Mgear = 5;
							}
							else if ( from is Nazghoul )
							{
								gear = "a nazghoul";
								Magic = 3;
								Mgear = 5;
							}
							else if ( from is AncientLich )
							{
								gear = "an ancient";
								Magic = 5;
								Mgear = 7;
							}
							else if ( from is Surtaz )
							{
								gear = "Surtaz's";
								Magic = 7;
								Mgear = 9;
							}
							else if ( from is LichKing )
							{
								gear = "a dreaded";
								Magic = 7;
								Mgear = 9;
								MagicHue = 1150;
							}
							else if ( from is DemiLich )
							{
								gear = "a demilich";
								MagicHue = from.Hunger;
								MgearHue = from.Thirst;
								Magic = 3;
								Mgear = 5;

								if ( from.Title == "the crypt thing" ){		gear = "a crypt";	Magic = 5; }
								else if ( from.Title == "the dark lich" ){	gear = "a dark";	Magic = 7; }
							}
							else if ( from is UndeadDruid )
							{
								gear = "a dark druid";
								Magic = 3;
								Mgear = 5;
								MagicHue = 0x497;
								MgearHue = 0x497;
							}

							switch( Utility.RandomMinMax( 0, 1 ) )
							{
								case 0: 
									NecromancerRobe robe = new NecromancerRobe();
										robe.Name = gear + " robe";
										robe.Hue = MagicHue;
										robe.Attributes.CastRecovery = Magic;
										robe.Attributes.CastSpeed = Magic;
										robe.Attributes.LowerManaCost = 4 + Magic;
										robe.Attributes.LowerRegCost = 4 + Magic;
										robe.Attributes.SpellDamage = 2 + Magic;
										from.AddItem( robe );
									break;
								case 1: 
									QuarterStaff staff = new QuarterStaff();
										staff.Name = gear + " staff";
										staff.ItemID = Utility.RandomList( 0xDF0, 0x13F8, 0xE89, 0x2D25 );
										staff.Hue = MgearHue;
										staff.WeaponAttributes.HitHarm = 5 * Mgear;
										staff.MaxHitPoints = 100;
										staff.HitPoints = 100;
										staff.MinDamage = staff.MinDamage + Mgear;
										staff.MaxDamage = staff.MaxDamage + Mgear;
										staff.SkillBonuses.SetValues( 0, SkillName.Macing, (2*Mgear) );
										((BaseCreature)from).PackItem( staff );
									break;
							}
						}
					}
				}
			}
			else if ( from is VampireLord || from is Vampire || from is VampirePrince || from is Dracula )
			{
				if ( killer != null )
				{
					string gear = "a vampire";
					int Magic = 1;
					int Mgear = 3;
					int MagicHue = 0x497;
					int MgearHue = 0x485;

					if ( killer is BaseCreature )
						killer = ((BaseCreature)killer).GetMaster();

					if ( killer is PlayerMobile )
					{
						if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 4 ) == 1 )
						{
							if ( from is Vampire )
							{
								gear = "a vampire";
								Magic = 1;
								Mgear = 3;
							}
							else if ( from is VampireLord )
							{
								gear = "a vampire lord";
								Magic = 3;
								Mgear = 5;
							}
							else if ( from is VampirePrince )
							{
								gear = "a vampire prince";
								Magic = 5;
								Mgear = 7;
							}
							else if ( from is Dracula )
							{
								gear = "Dracula's";
								Magic = 7;
								Mgear = 9;
							}

							switch( Utility.RandomMinMax( 0, 1 ) )
							{
								case 0: 
									VampireRobe robe = new VampireRobe();
										robe.Name = gear + " robe";
										robe.Hue = MagicHue;
										robe.Resistances.Cold = 3 + Mgear;
										robe.Attributes.DefendChance = 3 + Mgear;
										robe.Attributes.BonusStr = 1 + Mgear;
										robe.Attributes.NightSight = 1;
										robe.Attributes.RegenHits = 1 + Mgear;
										from.AddItem( robe );
									break;
								case 1: 
									MagicCloak cloak = new MagicCloak();
										cloak.Name = gear + " cloak";
										cloak.Hue = MgearHue;
										cloak.Resistances.Cold = 3 + Magic;
										cloak.Attributes.DefendChance = 3 + Magic;
										cloak.Attributes.BonusStr = 1 + Magic;
										cloak.Attributes.NightSight = 1;
										cloak.Attributes.RegenHits = 1 + Magic;
										from.AddItem( cloak );
									break;
							}
						}
					}
				}
			}
			else if ( from.EmoteHue == 11 && from.Title == "the mad archmage" )
			{
				if ( killer != null )
				{
					switch( Utility.RandomMinMax( 0, 1 ) )
					{
						case 0: 
							Robe robe = new Robe( );
								robe.Hue = 0xA2A;
								robe.Name = "robe of the mad archmage";
								robe.Attributes.SpellDamage = 35;
								robe.Attributes.CastRecovery = 1;
								robe.Attributes.CastSpeed = 1;
								robe.Attributes.LowerManaCost = 30;
								robe.Attributes.LowerRegCost = 30;
								from.AddItem( robe );
							break;
						case 1: 
							WizardsHat hat = new WizardsHat( );
								hat.Hue = 0xA2A;
								hat.Name = "hat of the mad archmage";
								hat.Attributes.SpellDamage = 25;
								hat.Attributes.CastRecovery = 1;
								hat.Attributes.CastSpeed = 1;
								hat.Attributes.LowerManaCost = 20;
								hat.Attributes.LowerRegCost = 20;
								from.AddItem( hat );
							break;
					}
				}
			}
			else if ( from.EmoteHue == 16 )
			{
				if ( killer != null )
				{
					switch( Utility.RandomMinMax( 0, 1 ) )
					{
						case 0: 
							Robe robe = new Robe( );
								robe.Hue = 0x482;
								robe.Name = "ice queen robe";
								robe.Attributes.RegenMana = 5;
								robe.Attributes.ReflectPhysical = 20;
								robe.Attributes.SpellDamage = 35;
								from.AddItem( robe );
							break;
						case 1: 
							WizardsHat hat = new WizardsHat( );
								hat.Hue = 0x482;
								hat.Name = "ice queen hat";
								hat.Attributes.RegenMana = 3;
								hat.Attributes.ReflectPhysical = 10;
								hat.Attributes.SpellDamage = 15;
								from.AddItem( hat );
							break;
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void BeforeMyBirth( Mobile from )
		{
			int PackGear = 1;

			Region reg = Region.Find( from.Location, from.Map );

			if ( reg.IsPartOf( "the Ice Queen Fortress" ) && from is EvilMageLord )
			{
				from.EmoteHue = 16;
				PackGear = 0;
			}
			else if ( from.EmoteHue == 11 ){} // EVIL MAGE LORD SKIP
			else if ( from is DeadWizard )
			{
				from.EmoteHue = 0;
				PackGear = 0;
			}
			else if ( reg.IsPartOf( "Stonegate Castle" ) && ( from is EvilMage || from is ElfMage || from is OrkMage ) && from.X >= 6326 && from.Y >= 2377 && from.X <= 6509 && from.Y <= 2505 )
			{
				from.EmoteHue = 6;
			}
			else if ( reg.IsPartOf( "Stonegate Castle" ) && ( from is EvilMage || from is ElfMage || from is OrkMage ) )
			{
				from.EmoteHue = 0;
				PackGear = 0;
			}
			else if ( reg.IsPartOf( "the Hidden Valley" ) && ( from is EvilMage || from is ElfMage || from is OrkMage ) )
			{
				from.EmoteHue = 6;
				PackGear = 0;
			}
			else if ( reg.IsPartOf( "the Azure Castle" ) && ( from is EvilMage || from is ElfMage || from is OrkMage ) )
			{
				from.EmoteHue = 0;
				PackGear = 0;
			}
			else if ( from is BoneMagi || from is SkeletalMage || from is SkeletalWizard || from is Lich || from is Vordo || from is Nazghoul || from is LichLord || from is DemiLich || from is AncientLich || from is Surtaz || from is LichKing )
			{
				from.EmoteHue = 13;
				PackGear = 0;
			}
			else if ( from is UndeadDruid )
			{
				from.EmoteHue = 14;
				PackGear = 0;
			}
			else if ( from is VampirePrince || from is Vampire || from is VampireLord || from is Dracula )
			{
				from.EmoteHue = 15;
				PackGear = 0;
			}
			else if (	reg.IsPartOf( "the Ruins of the Black Blade" ) || 
						reg.IsPartOf( typeof( BardDungeonRegion ) ) )
			{
				from.EmoteHue = 0;
			}
			else if (	from.Map == Map.Tokuno || 
						reg.IsPartOf( "the Blood Temple" ) )
			{
				from.EmoteHue = 2;
				PackGear = 0;
			}
			else if (	reg.IsPartOf( "Dungeon Hythloth" ) )
			{
				from.EmoteHue = 3;
			}
			else if (
						( from.X >= 6362 && from.Y >= 3854 && from.X <= 6372 && from.Y <= 3864 && from.Map == Map.Felucca ) || 
						( from.X >= 6442 && from.Y >= 3821 && from.X <= 6452 && from.Y <= 3831 && from.Map == Map.Felucca ) || 
						reg.IsPartOf( "the Hall of the Mountain King" ) || 
						reg.IsPartOf( "Dungeon Shame" )
			)
			{
				from.EmoteHue = 4;
			}
			else if (
						( from.X >= 6312 && from.Y >= 3538 && from.X <= 6397 && from.Y <= 3628 && from.Map == Map.Trammel ) || 
						( from.X >= 6266 && from.Y >= 469 && from.X <= 6276 && from.Y <= 479 && from.Map == Map.Felucca ) || 
						( from.X >= 6272 && from.Y >= 534 && from.X <= 6282 && from.Y <= 544 && from.Map == Map.Felucca ) || 
						( from.X >= 6309 && from.Y >= 578 && from.X <= 6319 && from.Y <= 588 && from.Map == Map.Felucca ) || 
						( from.X >= 6203 && from.Y >= 661 && from.X <= 6213 && from.Y <= 671 && from.Map == Map.Trammel ) || 
						( from.X >= 6331 && from.Y >= 145 && from.X <= 6341 && from.Y <= 155 && from.Map == Map.Trammel ) || 
						( from.X >= 6284 && from.Y >= 3598 && from.X <= 6294 && from.Y <= 3608 && from.Map == Map.Felucca ) || 
						( from.X >= 28 && from.Y >= 3294 && from.X <= 101 && from.Y <= 3329 && from.Map == Map.TerMur ) || 
						Server.Misc.Worlds.IsCrypt( from.Location, from.Map )
			)
			{
				from.EmoteHue = 5;
			}
			else if (

						( from.X >= 6590 && from.Y >= 373 && from.X <= 6629 && from.Y <= 465 && from.Map == Map.Felucca ) || 
						( Utility.RandomMinMax( 1, 4 ) > 1 && Server.Misc.Worlds.TestTile ( from.Map, from.X, from.Y, "forest" ) ) || 
						reg.IsPartOf( "the Valley of Dark Druids" ) )
			{
				from.EmoteHue = 6;
			}
			else if (
						( from is EvilMage || from is ElfMage || from is OrkMage ) && 
						(
							( from.X >= 6177 && from.Y >= 256 && from.X <= 6224 && from.Y <= 297 && from.Map == Map.Trammel ) || 
							( from.X >= 6359 && from.Y >= 508 && from.X <= 6451 && from.Y <= 564 && from.Map == Map.Felucca ) || 
							Server.Misc.Worlds.TestTile ( from.Map, from.X, from.Y, "snow" ) || 
							Server.Misc.Worlds.IsIceDungeon( from.Location, from.Map )
						)
			)
			{
				from.EmoteHue = 7;
			}
			else if (
						( from.X >= 6184 && from.Y >= 496 && from.X <= 6208 && from.Y <= 520 && from.Map == Map.Trammel ) || 
						( from.X >= 6314 && from.Y >= 250 && from.X <= 6339 && from.Y <= 285 && from.Map == Map.Trammel ) || 
						( from.X >= 6459 && from.Y >= 460 && from.X <= 6481 && from.Y <= 477 && from.Map == Map.Trammel ) || 
						( from.X >= 3094 && from.Y >= 3582 && from.X <= 3118 && from.Y <= 3602 && from.Map == Map.Felucca ) || 
						Server.Misc.Worlds.IsFireDungeon( from.Location, from.Map ) || 
						reg.IsPartOf( "the Tower of Brass" ) 
			)
			{
				from.EmoteHue = 8;
			}
			else if (
						( from.X >= 6289 && from.Y >= 119 && from.X <= 6299 && from.Y <= 129 && from.Map == Map.Trammel ) || 
						( from.X >= 6312 && from.Y >= 125 && from.X <= 6326 && from.Y <= 133 && from.Map == Map.Trammel ) || 
						Server.Misc.Worlds.TestTile ( from.Map, from.X, from.Y, "swamp" ) || 
						reg.IsPartOf( "the Temple of Osirus" ) || 
						reg.IsPartOf( "the Dragon's Maw" ) ||
						reg.IsPartOf( "Dungeon Destard" ) 
			)
			{
				from.EmoteHue = 9;
			}
			else if ( Server.Misc.Worlds.IsSeaDungeon( from.Location, from.Map ) || 
						Server.Misc.Worlds.TestOcean ( from.Map, from.X, from.Y, 15 ) )
			{
				from.EmoteHue = 10;
			}
			else if ( reg.IsPartOf( "the Tomb of Kazibal" ) )
			{
				from.EmoteHue = 17;
			}
			else // RANDOM MAGE
			{
				switch ( Utility.RandomMinMax( 0, 10 ) )
				{
					case 0: from.EmoteHue = 0; break; // Mages with no Summoning
					case 1: from.EmoteHue = 1; break; // Traditional Mages
					case 2: from.EmoteHue = 3; break; // Demonologists
					case 3: from.EmoteHue = 4; break; // Elementalists
					case 4: from.EmoteHue = 5; break; // Necromancers
					case 5: from.EmoteHue = 6; break; // Druids
					case 6: from.EmoteHue = 7; break; // Ice Wizards
					case 7: from.EmoteHue = 8; break; // Fire Wizards
					case 8: from.EmoteHue = 9; break; // Serpent Mages
					case 9: from.EmoteHue = 10; break; // Water Wizards
					case 10: from.EmoteHue = 12; break; // Insane Wizards
				}
			}

			if ( from.EmoteHue == 0 && reg.IsPartOf( "Stonegate Castle" ) )
			{
				from.Title = "the shadow priest";
				from.Hue = 0x4001;
				from.HairItemID = 0;
				from.FacialHairItemID = 0;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
						item.Hue = 0x541;
				}
			}
			if ( reg.IsPartOf( "the Azure Castle" ) && ( from is OrkMage || from is DeadWizard ) )
			{
				from.Title = from.Title + " of azure";

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
						item.Hue = 0x538;
				}
			}
			else if ( from.EmoteHue == 2 )
			{
				from.Title = from.Title + " of blood";
				from.Hue = 0x4AA;
				from.HairHue = 0x96C;
				from.FacialHairHue = 0x96C;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is Hair || item is Beard )
						item.Hue = 0x96C;
					else if ( item is BaseClothing || item is BaseArmor )
						item.Hue = 0x84D;
				}
			}
			else if ( from.EmoteHue == 3 )
			{
				from.Title = from.Title + " of demons";
			}
			else if ( from.EmoteHue == 4 )
			{
				from.Title = from.Title + " of elements";
			}
			else if ( from.EmoteHue == 5 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: from.Title = from.Title + " of death"; break;
					case 1: from.Title = from.Title + " of the grave"; break;
					case 2: from.Title = from.Title + " of the dead"; break;
				}

				from.Hue = 0x83E8;
				from.HairHue = 0;
				from.FacialHairHue = 0;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is Hair || item is Beard )
						item.Hue = 0;
					else if ( item is BaseClothing || item is BaseArmor )
						item.Hue = 0x497;
				}
			}
			else if ( from.EmoteHue == 6 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: from.Title = from.Title + " of the woods"; break;
					case 1: from.Title = from.Title + " of the forest"; break;
					case 2: from.Title = from.Title + " of the glade"; break;
				}

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
						item.Hue = Utility.RandomYellowHue();
				}
			}
			else if ( from.EmoteHue == 7 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: from.Title = from.Title + " of ice"; break;
					case 1: from.Title = from.Title + " of frost"; break;
					case 2: from.Title = from.Title + " of the snow"; break;
				}

				from.Hue = 0x83E8;
				from.HairHue = 0;
				from.FacialHairHue = 0;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is Hair || item is Beard )
						item.Hue = 0;
					else if ( item is BaseClothing || item is BaseArmor )
						item.Hue = Utility.RandomList( 0xB78, 0xB33, 0xB34, 0xB35, 0xB36, 0xB37, 0xAF3 );
				}
			}
			else if ( from.EmoteHue == 8 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: from.Title = from.Title + " of fire"; break;
					case 1: from.Title = from.Title + " of the flame"; break;
					case 2: from.Title = from.Title + " of flame"; break;
				}

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
						item.Hue = Utility.RandomList( 0xB73, 0xB71, 0xB17, 0xAFA, 0xAC8, 0x986 );
				}

				from.AddItem( new LighterSource() );
			}
			else if ( from.EmoteHue == 9 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: from.Title = from.Title + " of snakes"; break;
					case 1: from.Title = from.Title + " of venom"; break;
					case 2: from.Title = from.Title + " of serpents"; break;
				}

				int HairColor = Utility.RandomGreenHue();
				from.Hue = Utility.RandomGreenHue();
				from.HairHue = HairColor;
				from.FacialHairHue = HairColor;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is Hair || item is Beard )
						item.Hue = HairColor;
					else if ( item is BaseClothing || item is BaseArmor )
						item.Hue = Utility.RandomGreenHue();
				}
			}
			else if ( from.EmoteHue == 10 )
			{
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: from.Title = from.Title + " of the sea"; break;
					case 1: from.Title = from.Title + " of the deep"; break;
					case 2: from.Title = from.Title + " of the lake"; break;
				}

				int HairColor = Utility.RandomBlueHue();
				from.Hue = Utility.RandomGreenHue();
				from.HairHue = HairColor;
				from.FacialHairHue = HairColor;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is Hair || item is Beard )
						item.Hue = HairColor;
					else if ( item is BaseClothing || item is BaseArmor )
						item.Hue = RandomThings.GetRandomColor( 2 );
				}
			}
			else if ( from.EmoteHue == 16 )
			{
				BaseCreature bsct = (BaseCreature)from;

				from.Body = 0x191; 
				from.Name = NameList.RandomName( "female" );
				from.Title = "the ice queen";

				from.Hue = 0x47E;
				Utility.AssignRandomHair( from );
				from.HairHue = 0x47F;
				from.FacialHairItemID = 0;

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
					{
						item.Hue = 0x482;
						item.LootType = LootType.Blessed;
					}
					else if ( item is BaseWeapon )
						item.Delete();
				}

				((BaseCreature)from).PackItem( new GlacialStaff() );

				bsct.RawStr = 305;
				bsct.RawDex = 115;
				bsct.RawInt = 1045;

				bsct.Hits = bsct.HitsMax;
				bsct.Stam = bsct.StamMax;
				bsct.Mana = bsct.ManaMax;

				bsct.DamageMin = 15;
				bsct.DamageMax = 27;

				bsct.PhysicalDamage = 20;
				bsct.ColdDamage = 40;
				bsct.EnergyDamage = 40;

				bsct.PhysicalResistanceSeed = 50;
				bsct.FireResistSeed = 0;
				bsct.ColdResistSeed = 90;
				bsct.PoisonResistSeed = 50;
				bsct.EnergyResistSeed = 10;

				for( int i = 0; i < bsct.Skills.Length; i++ )
				{
					Skill skill = (Skill)bsct.Skills[i];

					if ( skill.Base > 0.0 )
						skill.Base = 125.0;
				}

				bsct.Fame = 23000;
				bsct.Karma = -23000;

				from.VirtualArmor = 60;
			}
			else if ( from.EmoteHue == 12 )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: from.Title = from.Title + " of insanity"; break;
					case 1: from.Title = from.Title + " of dementia"; break;
					case 2: from.Title = from.Title + " of mania"; break;
					case 3: from.Title = from.Title + " of lunacy"; break;
					case 4: from.Title = from.Title + " of madness"; break;
					case 5: from.Title = from.Title + " of hysteria"; break;
				}

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
						item.Hue = RandomThings.GetRandomColor( 0 );
				}
			}
			else if ( from.EmoteHue == 17 )
			{
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: from.Title = from.Title + " of the sands"; break;
					case 1: from.Title = from.Title + " of the desert"; break;
					case 2: from.Title = from.Title + " of the wastes"; break;
					case 3: from.Title = from.Title + " of the barrens"; break;
					case 4: from.Title = from.Title + " of the wasteland"; break;
					case 5: from.Title = from.Title + " of the badlands"; break;
				}

				for ( int i = 0; i < from.Items.Count; ++i )
				{
					Item item = from.Items[i];

					if ( item is BaseClothing || item is BaseArmor )
						item.Hue = 0x83B;
				}
			}

			if ( PackGear == 1 ){ Server.Misc.IntelligentAction.GiveAdventureGear( (BaseCreature)from ); }

			WizardStaff caster = new WizardStaff();
				if ( from.FindItemOnLayer( Layer.OneHanded ) != null ) { from.FindItemOnLayer( Layer.OneHanded ).Delete(); }
				if ( from.FindItemOnLayer( Layer.TwoHanded ) != null ) { from.FindItemOnLayer( Layer.TwoHanded ).Delete(); }
				((BaseCreature)from).SetSkill( SkillName.Archery, from.Skills[SkillName.Magery].Value );
				if ( Utility.RandomBool() )
				{
					caster.Name = "staff";
					caster.ItemID = Utility.RandomList( 0xDF0, 0x13F8, 0xE89, 0x2D25, 0x26BC, 0x26C6, 0xDF2, 0xDF3, 0xDF4, 0xDF5, 0x269D, 0x269E );
					if ( caster.ItemID == 0x26BC || caster.ItemID == 0x26C6 ){ caster.Name = "scepter"; }
					if ( caster.ItemID == 0x269D || caster.ItemID == 0x269E ){ caster.Name = "skull scepter"; }
					if ( caster.ItemID == 0xDF2 || caster.ItemID == 0xDF3 || caster.ItemID == 0xDF4 || caster.ItemID == 0xDF5 ){ caster.Name = "magic wand"; }
				}
				else
				{
					caster.ItemID = 0x13C6;
					caster.Hue = from.Hue;
					caster.Name = "wizard gloves";
				}
				caster.LootType = LootType.Blessed;
				caster.Attributes.SpellChanneling = 1;
				caster.damageType = Utility.RandomMinMax( 0, 4 );
				if ( from.EmoteHue == 7 ){ caster.damageType = 2; } 		// Ice Wizards
				else if ( from.EmoteHue == 8 ){ caster.damageType = 1; } 	// Fire Wizards
				else if ( from.EmoteHue == 9 ){ caster.damageType = 4; } 	// Serpent Mages
				from.AddItem( caster );
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void BurnAway( Mobile from )
		{
			from.PlaySound( 0x208 );

			Point3D fire1 = new Point3D( ( from.X-1 ), ( from.Y ), from.Z );
			Point3D fire2 = new Point3D( ( from.X+1 ), ( from.Y ), from.Z );
			Point3D fire3 = new Point3D( ( from.X-1 ), ( from.Y-1 ), from.Z );
			Point3D fire4 = new Point3D( ( from.X+1 ), ( from.Y-1 ), from.Z );
			Point3D fire5 = new Point3D( ( from.X ), ( from.Y-1 ), from.Z );
			Point3D fire6 = new Point3D( ( from.X-1 ), ( from.Y+1 ), from.Z );
			Point3D fire7 = new Point3D( ( from.X+1 ), ( from.Y+1 ), from.Z );
			Point3D fire8 = new Point3D( ( from.X ), ( from.Y+1 ), from.Z );
			Point3D fire9 = new Point3D( ( from.X ), ( from.Y ), from.Z );

			Effects.SendLocationEffect( fire1, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire2, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire3, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire4, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire5, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire6, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire7, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire8, from.Map, 0x3709, 30, 10 );
			Effects.SendLocationEffect( fire9, from.Map, 0x3709, 30, 10 );
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void FizzleSpell( Mobile m )
		{
			m.LocalOverheadMessage( MessageType.Regular, 0x3B2, 502632 ); // The spell fizzles.

			if ( m.Player )
			{
				m.FixedParticles( 0x3735, 1, 30, 9503, EffectLayer.Waist );
				m.PlaySound( 0x5C );
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void SaySomethingWhenAttacking( Mobile from, Mobile m )
		{
			if ( from.Name != "a sailor" && from.Name != "a pirate" && from.Name != "a follower" && m != null && from.EmoteHue != 505 && ((BaseCreature)from).ControlSlots != 666 && ((BaseCreature)from).GetMaster() == null && Utility.Random( 5 ) == 1 )
			{
				if ( from.SpeechHue < 1 ){ from.SpeechHue = Server.Misc.RandomThings.GetSpeechHue(); }

				if ( m is BaseCreature )
					m = ((BaseCreature)m).GetMaster();

				if ( m is PlayerMobile )
				{
					if ( from is Exodus )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I will vanquish your existence from all time!"); break;
							case 1: from.Say("" + m.Name + ", prepare to meet your end!"); break;
							case 2: from.Say("You cannot stop the destruction I will soon unleash!"); break;
							case 3: from.Say("My diligence will be your ultimate doom!"); break;
						};
					}
					else if ( from is FleshGolem || from is AncientFleshGolem )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I am fearless, and therefore powerful!"); break;
							case 1: from.Say("I am a monster, cut off from all the world!"); break;
							case 2: from.Say("To be whole again, I must destroy you!"); break;
							case 3: from.Say("Fell the wrath of my master!"); break;
						};
					}
					else if ( from is BloodDemigod )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Foolish mortal!"); break;
							case 1: from.Say("I will summon your gore to crawl these halls!"); break;
							case 2: from.Say("Your life only feeds my own!"); break;
							case 3: from.Say("Let this be your final battle!"); break;
						};
					}
					else if ( from is Balron 
							|| from is Devil 
							|| from is BlackGateDemon 
							|| from is AbysmalDaemon 
							|| from is Xurtzar 
							|| from is TitanPyros 
							|| from is Marilith 
							|| from is Daemonic 
							|| from is Archfiend 
							|| from is Fiend 
							|| from is Daemon 
							|| from is DaemonTemplate 
							|| from is BloodDemon )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Your soul will be mine!"); break;
							case 1: from.Say("I will use your corpse to feed my minions!"); break;
							case 2: from.Say("Do you think you can slay one such as me?!"); break;
							case 3: from.Say("I look forward to torturing your soul, " + m.Name + "!"); break;
						};
					}
					else if ( from is DeepSeaDevil || from is DemonOfTheSea )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Your soul will soon be one with the deep!"); break;
							case 1: from.Say("You dare face the power of the sea?!"); break;
							case 2: from.Say("Are you ready to serve me in the depths, " + m.Name + "?!"); break;
							case 3: from.Say("I will drag your corpse into the sea!"); break;
						};
					}
					else if ( from is IceDevil )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Your soul will soon be encased in ice!"); break;
							case 1: from.Say("You dare face my glacial power?!"); break;
							case 2: from.Say("Are your bones cold yet, " + m.Name + "?!"); break;
							case 3: from.Say("I will freeze your blood and shatter your soul!"); break;
						};
					}
					else if ( from is Succubus )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Your blood smells sweet!"); break;
							case 1: from.Say("Are you ready to give yourself to me?!"); break;
							case 2: from.Say("Your life only feeds my own!"); break;
							case 3: from.Say("You will make me young again, " + m.Name + "!"); break;
						};
					}
					else if ( from is Satan )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Your soul will serve me well!"); break;
							case 1: from.Say("I will break a spirit such as yours!"); break;
							case 2: from.Say("Do you feel the power of hell on you?!"); break;
							case 3: from.Say("Your soul will be mine, " + m.Name + "!"); break;
						};
					}
					else if ( from is VampiricDragon )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I will drain every breath of life from you!"); break;
							case 1: from.Say("I can smell the blood from your wounds!"); break;
							case 2: from.Say("Fool...I cannot kill what is dead?!"); break;
							case 3: from.Say("Your corpse will rise and serve me, " + m.Name + "!"); break;
						};
					}
					else if ( from is ShadowWyrm )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I will take you from the light!"); break;
							case 1: from.Say("I can feel the darkness filling you!"); break;
							case 2: from.Say("Fool...you can never bring me to the light!"); break;
							case 3: from.Say("Your life will end in darkness, " + m.Name + "!"); break;
						};
					}
					else if ( from is AshDragon || from is VolcanicDragon )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I will leave your corpse as ashes!"); break;
							case 1: from.Say("I can smell your burning flesh!"); break;
							case 2: from.Say("Fool...you cannot survive the flames!"); break;
							case 3: from.Say("Cinders will be all that is left of you, " + m.Name + "!"); break;
						};
					}
					else if ( from is BottleDragon
						 || from is CaddelliteDragon
						 || from is CrystalDragon
						 || from is DragonKing
						 || from is SlasherOfVoid
						 || from is ElderDragon
						 || from is RadiationDragon
						 || from is VoidDragon
						 || from is PrimevalAbysmalDragon
						 || from is PrimevalAmberDragon
						 || from is PrimevalBlackDragon
						 || from is PrimevalDragon
						 || from is PrimevalFireDragon
						 || from is PrimevalGreenDragon
						 || from is PrimevalNightDragon
						 || from is PrimevalRedDragon
						 || from is PrimevalRoyalDragon
						 || from is PrimevalRunicDragon
						 || from is PrimevalSeaDragon
						 || from is PrimevalSilverDragon
						 || from is PrimevalStygianDragon
						 || from is PrimevalVolcanicDragon
						 || from is AncientWyrm )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I have slain those mightier than you, " + m.Name + "!"); break;
							case 1: from.Say("You will make me an excellent meal!"); break;
							case 2: from.Say("Many have died trying to take what is mine!"); break;
							case 3: from.Say("I will swallow you whole, " + m.Name + "!"); break;
						};
					}
					else if ( from is FireGargoyle
						 || from is Gargoyle
						 || from is GargoyleAmethyst
						 || from is GargoyleEmerald
						 || from is GargoyleOnyx
						 || from is GargoyleRuby
						 || from is GargoyleSapphire
						 || from is CodexGargoyleA
						 || from is CodexGargoyleB
						 || from is StoneGargoyle
						 || from is GargoyleWarrior
						 || from is StygianGargoyle
						 || from is StygianGargoyleLord
						 || from is AncientGargoyle
						 || from is MutantGargoyle
						 || from is CosmicGargoyle
						 || from is GargoyleMarble )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Esaeu lizz gia xes zes soth!"); break;
							case 1: from.Say("Dnadona qae zaaq esaeun doom!"); break;
							case 2: from.Say("I lizz raeq chq esaeu xaed za!"); break;
							case 3: from.Say("Dnadona qae gia, " + m.Name + "!"); break;
						};
					}
					else if ( from is ZornTheBlacksmith )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("You will never have the secrets of the ore!"); break;
							case 1: from.Say("You should leave before I crush you!"); break;
							case 2: from.Say("Feel the power of my hammer!"); break;
							case 3: from.Say("I will tell all on how I crushed " + m.Name + "!"); break;
						};
					}
					else if ( from is OrkDemigod )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Kneel before me you puny creature!"); break;
							case 1: from.Say("Feel the might of the orks!"); break;
							case 2: from.Say("I will become your new god, " + m.Name + "!"); break;
							case 3: from.Say("I have slain those more powerful than you!"); break;
						};
					}
					else if ( from is TrollWitchDoctor
						 || from is Troll
						 || from is SwampTroll
						 || from is SeaTroll
						 || from is FrostTrollShaman
						 || from is FrostTroll )
					{
						string organ = "spleen";
						switch ( Utility.Random( 4 ))		   
						{
							case 0: organ = "spleen"; break;
							case 1: organ = "heart"; break;
							case 2: organ = "liver"; break;
							case 3: organ = "guts"; break;
						};
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Me will be eating your " + organ + " me thinks!"); break;
							case 1: from.Say("Me see you living no longer!"); break;
							case 2: from.Say("You will be dead by me hand!"); break;
							case 3: from.Say("Me will be feasting on your bones soon!"); break;
						};
					}
					else if ( from is AncientEttin
						 || from is EttinShaman
						 || from is Ettin
						 || from is ArcticEttin )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I smash you to pulp!"); break;
							case 1: from.Say("I will smash you into dirt!"); break;
							case 2: from.Say("You will make great feast for us!"); break;
							case 3: from.Say("You leave our land now!"); break;
						};
					}
					else if ( from is Titan
						 || from is ElderTitan
						 || from is CloudGiant
						 || from is StormGiant )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Feel the wrath of the storm!"); break;
							case 1: from.Say("I unleash the storms on you!"); break;
							case 2: from.Say("This will be your final battle, " + m.Name + "!"); break;
							case 3: from.Say("You think you can defeat me?!"); break;
						};
					}
					else if ( from is Dragonogre
						 || from is TundraOgre
						 || from is OgreMagi
						 || from is OgreLord
						 || from is Ogre
						 || from is ArcticOgreLord
						 || from is AbysmalOgre
						 || from is Neanderthal
						 || from is HillGiant
						 || from is HillGiantShaman )
					{
						string organ = "arm";
						switch ( Utility.Random( 4 ))		   
						{
							case 0: organ = "arms"; break;
							case 1: organ = "legs"; break;
							case 2: organ = "bones"; break;
							case 3: organ = "corpse"; break;
						};
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Me hit, you die!"); break;
							case 1: from.Say("You no match for me!"); break;
							case 2: from.Say("Me make soup with your " + organ + "!"); break;
							case 3: from.Say("You weak, me strong!"); break;
						};
					}
					else if ( from is IceGiant )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Feel the cold of glacial ice!"); break;
							case 1: from.Say("You are nothing but an insect to me!"); break;
							case 2: from.Say("" + m.Name + ", you dare face me!"); break;
							case 3: from.Say("Your frozen corpse will decorate my halls!"); break;
						};
					}
					else if ( from is LavaGiant )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Feel the fires of volcanic might!"); break;
							case 1: from.Say("You are nothing but an insect to me!"); break;
							case 2: from.Say("" + m.Name + ", you dare face me!"); break;
							case 3: from.Say("You will soon be nothing but ashes!"); break;
						};
					}
					else if ( from is DeepSeaGiant
						 || from is SeaGiant )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Feel the strength of the sea!"); break;
							case 1: from.Say("You will soon will rot below the waves!"); break;
							case 2: from.Say("" + m.Name + ", your bones will lie with the crabs!"); break;
							case 3: from.Say("You are no match for the gods of the sea!"); break;
						};
					}
					else if ( from is MountainGiant
						 || from is AbyssGiant
						 || from is JungleGiant
						 || from is SandGiant
						 || from is StoneGiant
						 || from is FireGiant
						 || from is ForestGiant
						 || from is FrostGiant
						 || from is AncientCyclops
						 || from is ShamanicCyclops
						 || from is Cyclops )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("My foot will be the last thing you see!"); break;
							case 1: from.Say("I will crush you into the dirt!"); break;
							case 2: from.Say("" + m.Name + ", you will die!"); break;
							case 3: from.Say("I have defeated foes larger than you!"); break;
						};
					}
					else if ( from is TheAncientTree
						 || from is Ent
						 || from is EvilEnt
						 || from is AncientEnt )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("You...dare...fight...me?!"); break;
							case 1: from.Say("I...will...dispatch...of...you!"); break;
							case 2: from.Say("My...might...outweighs...yours!"); break;
							case 3: from.Say("You...will...die...in...this...fight!"); break;
						};
					}
					else if ( from is SwampThing )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Gri Gril Gestroy Groo!"); break;
							case 1: from.Say("Groo Gran Grever Gregreat Gre!"); break;
							case 2: from.Say("Grour Grones Gril Gray Grin Gry Grwamp!"); break;
							case 3: from.Say("Groo Grar Gro Gratch Gror Gre!"); break;
						};
					}
					else if ( from is Beholder )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("*blinks a red eye*"); break;
							case 1: from.Say("*blinks a blue eye*"); break;
							case 2: from.Say("*blinks a greed eye*"); break;
							case 3: from.Say("*blinks a yellow eye*"); break;
						};
					}
					else if ( from is Dracolich
						 || from is SkeletalDragon )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Your soul will make an excellent meal!"); break;
							case 1: from.Say("" + m.Name + ", do I frighten you?!"); break;
							case 2: from.Say("I have destroyed armies of things like you!"); break;
							case 3: from.Say("You dare invade my lair?!"); break;
						};
					}
					else if ( from is Vampire
						 || from is VampireLord
						 || from is VampirePrince
						 || from is VampireWoods )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("I can smell the blood from your wounds!"); break;
							case 1: from.Say("Look into my eyes..."); break;
							case 2: from.Say("Submit, and I will make it quick!"); break;
							case 3: from.Say("You think I have not faced mortals like you?!"); break;
						};
					}
					else if ( from is Dracula )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("" + m.Name + ", your blood will fill my glass tonight!"); break;
							case 1: from.Say("Look into my eyes, " + m.Name + "..."); break;
							case 2: from.Say("Your blood will decorate these walls!"); break;
							case 3: from.Say("You should be honored to be slain by me!"); break;
						};
					}
					else if ( from is Vordo )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("" + m.Name + ", you will join!"); break;
							case 1: from.Say("Look into my eyes, " + m.Name + "..."); break;
							case 2: from.Say("Your blood will decorate these walls!"); break;
							case 3: from.Say("You should be honored to be slain by me!"); break;
						};
					}
					else if ( from is AncientLich
						 || from is Lich
						 || from is LichKing
						 || from is TitanLich
						 || from is MummyGiant
						 || from is LichLord
						 || from is Nazghoul
						 || from is Surtaz
						 || from is UndeadDruid
						 || from is DemiLich )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("Feel the power of " + from.Name + "!"); break;
							case 1: from.Say("I will have a place for the bones of " + m.Name + "!"); break;
							case 2: from.Say("" + m.Name + ", you are a fool to face me!"); break;
							case 3: from.Say("My magic will decimate you!"); break;
						};
					}
					else if ( from is Executioner )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("" + m.Name + ", you are sentenced to death!"); break;
							case 1: from.Say("Your head will look good on the block!"); break;
							case 2: from.Say("My blade is eager to sever your head!"); break;
							case 3: from.Say("This will be your final fight!"); break;
						};
					}
					else if ( from is BlackKnight )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("" + m.Name + ", do you think you can defeat me?!"); break;
							case 1: from.Say("You will never gain entry to my vault!"); break;
							case 2: from.Say("Many have come here and all have perished!"); break;
							case 3: from.Say("Your treasure will help fill my vault!"); break;
						};
					}
					else if ( from is Archmage )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("" + m.Name + ", you have no hope against my power!"); break;
							case 1: from.Say("You will never leave this place alive!"); break;
							case 2: from.Say("You are no match for my magic!"); break;
							case 3: from.Say("All that have come here have perished!"); break;
						};
					}
					else if ( from is BombWorshipper || from is Psionicist )
					{
						switch ( Utility.Random( 9 ))		   
						{
							case 0: from.Say("I have converted others stonger than you, " + m.Name + "!"); break;
							case 1: from.Say("You will soon be one with the glow!"); break;
							case 2: from.Say("All will know that " + from.Name + " gave " + m.Name + " to the glow!"); break;
							case 3: from.Say("Maybe you should flee before it is too late!"); break;
							case 4: from.Say("Do you think you can beat me?!"); break;
							case 5: from.Say("No one desecrates the temple of the bomb!"); break;
							case 6: from.Say("Your life ends here!"); break;
							case 7: from.Say("Your life ends here, " + m.Name + "!"); break;
							case 8: from.Say("You will kneel before the bomb!"); break;
						};
					}
					else if ( from is Syth )
					{
						switch ( Utility.Random( 9 ))		   
						{
							case 0: from.Say("The Syth will be the last thing you see, " + m.Name + "!"); break;
							case 1: from.Say("You will submit to my dark power!"); break;
							case 2: from.Say("No one will find the bones of " + m.Name + "!"); break;
							case 3: from.Say("You should have fled but it is too late!"); break;
							case 4: from.Say("Do you think you can beat me?!"); break;
							case 5: from.Say("No one has faced a syth and lived!"); break;
							case 6: from.Say("Your life ends here!"); break;
							case 7: from.Say("Your life ends here, " + m.Name + "!"); break;
							case 8: from.Say("You will kneel before the Syth!"); break;
						};
					}
					else if ( from is ElfBerserker
						 || from is ElfRogue
						 || from is ElfMonks
						 || from is ElfMinstrel
						 || from is ElfMage
						 || from is BloodAssassin
						 || from is Berserker
						 || from is Bandit
						 || from is Rogue
						 || from is Monks
						 || from is Minstrel
						 || from is GolemController
						 || from is EvilMageLord
						 || from is EvilMage
						 || from is Brigand
						 || from is OrkMonks
						 || from is OrkMage
						 || from is OrkWarrior
						 || from is OrkRogue )
					{
						switch ( Utility.Random( 9 ))		   
						{
							case 0: from.Say("I have slain others better than you, " + m.Name + "!"); break;
							case 1: from.Say("Your riches will soon be mine!"); break;
							case 2: from.Say("All will know that " + from.Name + " defeated " + m.Name + "!"); break;
							case 3: from.Say("Maybe you should flee before it is too late!"); break;
							case 4: from.Say("Do you think you can best me?!"); break;
							case 5: from.Say("Let this be your final battle!"); break;
							case 6: from.Say("Your life ends here!"); break;
							case 7: from.Say("Your life ends here, " + m.Name + "!"); break;
							case 8: from.Say("All should fear " + from.Name + "!"); break;
						};
					}
					else if ( from is Adventurers || from is Jedi )
					{
						switch ( Utility.Random( 9 ))		   
						{
							case 0: from.Say("I have brought justice to others more vile than you, " + m.Name + "!"); break;
							case 1: from.Say("You will pay for your crimes!"); break;
							case 2: from.Say("All will know that " + from.Name + " brought " + m.Name + " to justice!"); break;
							case 3: from.Say("You should have fled this land long ago!"); break;
							case 4: from.Say("Do you think you can best me?!"); break;
							case 5: from.Say("Let this be your final battle!"); break;
							case 6: from.Say("Your life ends here!"); break;
							case 7: from.Say("Your life ends here, " + m.Name + "!"); break;
							case 8: from.Say("Your evil will be vanquished!"); break;
						};
					}
					else if ( Server.Mobiles.BasePirate.IsSailor( from ) )
					{
						switch ( Utility.Random( 9 ))		   
						{
							case 0: from.Say("" + m.Name + ", you will soon walk the plank!"); break;
							case 1: from.Say("I could beat you if I were three sheets to the wind!"); break;
							case 2: from.Say("I will splice the mainbrace over your corpse!"); break;
							case 3: from.Say("You will soon become shark bait!"); break;
							case 4: from.Say("You scurvy dog, do you think you can best me?!"); break;
							case 5: from.Say("I fought scallywags better than you!"); break;
							case 6: from.Say("No pray, no pay. Your riches will be mine!"); break;
							case 7: from.Say("You landlubber, prepare to die!"); break;
							case 8: from.Say("" + from.Name + ", you will soon feed the fish!"); break;
						};
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public static void SaySomethingOnDeath( Mobile from, Mobile m )
		{
			if ( from.Name != "a sailor" && from.Name != "a pirate" && from.Name != "a follower" && m != null && from.EmoteHue != 505 && ((BaseCreature)from).ControlSlots != 666 && ((BaseCreature)from).GetMaster() == null && Utility.Random( 5 ) == 1 )
			{
				if ( from.SpeechHue < 1 ){ from.SpeechHue = Server.Misc.RandomThings.GetSpeechHue(); }

				if ( m is BaseCreature )
					m = ((BaseCreature)m).GetMaster();

				if ( m is PlayerMobile )
				{
					if ( from is Exodus )
					{
						from.Say("You have not seen the last of me, " + m.Name + "!");
					}
					else if ( from is FleshGolem || from is AncientFleshGolem )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("No...I am eternal!"); break;
							case 1: from.Say("No...How can this be?!"); break;
							case 2: from.Say("Peace has finally found me..."); break;
							case 3: from.Say("I failed you my master..."); break;
						};
					}
					else if ( from is BloodDemigod )
					{
						from.Say("Some day your blood will fill these halls, " + m.Name + "!");
					}
					else if ( from is Balron 
							|| from is Devil 
							|| from is BlackGateDemon 
							|| from is AbysmalDaemon 
							|| from is Xurtzar 
							|| from is Marilith 
							|| from is Archfiend 
							|| from is Fiend 
							|| from is Daemon 
							|| from is BloodDemon )
					{
						switch ( Utility.Random( 5 ))		   
						{
							case 0: from.Say("No...I will not be vanquished!"); break;
							case 1: from.Say("I will return..."); break;
							case 2: from.Say("I hope the curses of hell fill your soul!"); break;
							case 3: from.Say("Death is only a distraction to me!"); break;
							case 4: from.Say("I will come for you, " + m.Name + "!"); break;
						};
					}
					else if ( from is DeepSeaDevil || from is DemonOfTheSea )
					{
						switch ( Utility.Random( 5 ))		   
						{
							case 0: from.Say("No...I will not be vanquished!"); break;
							case 1: from.Say("I will return..."); break;
							case 2: from.Say("I hope the blood of the sea drowns you!"); break;
							case 3: from.Say("Fool...I will rise again!"); break;
							case 4: from.Say("One day you will be taken by the sea, " + m.Name + "!"); break;
						};
					}
					else if ( from is IceDevil )
					{
						switch ( Utility.Random( 5 ))		   
						{
							case 0: from.Say("No...I will not be vanquished!"); break;
							case 1: from.Say("I will return..."); break;
							case 2: from.Say("The frost of death will find you!"); break;
							case 3: from.Say("Fool...you can never destroy me!"); break;
							case 4: from.Say("My cold heart will come for you, " + m.Name + "!"); break;
						};
					}
					else if ( from is Succubus )
					{
						from.Say("No...!");
					}
					else if ( from is Satan )
					{
						switch ( Utility.Random( 5 ))		   
						{
							case 0: from.Say("No...I cannot return to hell!"); break;
							case 1: from.Say("Hell will not be able to hold me..."); break;
							case 2: from.Say("" + m.Name + ", I will return for you!"); break;
							case 3: from.Say("Fool...I am eternal!"); break;
							case 4: from.Say("I will have your soul one day, " + m.Name + "!"); break;
						};
					}
					else if ( from is VampiricDragon )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("No...you cannot do this!"); break;
							case 1: from.Say("Curse you, " + m.Name + "!"); break;
						};
					}
					else if ( from is ShadowWyrm )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("No...you cannot have light without dark!"); break;
							case 1: from.Say("Let the shadows take you, " + m.Name + "!"); break;
						};
					}
					else if ( from is AshDragon || from is VolcanicDragon )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("No...this cannot be how it ends!"); break;
							case 1: from.Say("Let the mountain fires take you, " + m.Name + "!"); break;
						};
					}
					else if ( from is BottleDragon
						 || from is CaddelliteDragon
						 || from is CrystalDragon
						 || from is DragonKing
						 || from is ElderDragon
						 || from is RadiationDragon
						 || from is VoidDragon
						 || from is PrimevalAbysmalDragon
						 || from is PrimevalAmberDragon
						 || from is PrimevalBlackDragon
						 || from is PrimevalDragon
						 || from is PrimevalFireDragon
						 || from is PrimevalGreenDragon
						 || from is PrimevalNightDragon
						 || from is PrimevalRedDragon
						 || from is PrimevalRoyalDragon
						 || from is PrimevalRunicDragon
						 || from is PrimevalSeaDragon
						 || from is PrimevalSilverDragon
						 || from is PrimevalStygianDragon
						 || from is PrimevalVolcanicDragon
						 || from is AncientWyrm )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("No...this cannot be the end!"); break;
							case 1: from.Say("How...can...this...be..."); break;
							case 2: from.Say("No, " + m.Name + "!"); break;
							case 3: from.Say("What is this madness?!"); break;
						};
					}
					else if ( from is FireGargoyle
						 || from is Gargoyle
						 || from is GargoyleAmethyst
						 || from is GargoyleEmerald
						 || from is GargoyleOnyx
						 || from is GargoyleRuby
						 || from is GargoyleSapphire
						 || from is StoneGargoyle
						 || from is StygianGargoyle
						 || from is StygianGargoyleLord
						 || from is AncientGargoyle
						 || from is GargoyleMarble )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("Rae...sael yor yiz xa?"); break;
							case 1: from.Say("Zae zes hima ends sabbia!"); break;
						};
					}
					else if ( from is ZornTheBlacksmith )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("No...you will never get the ore!"); break;
							case 1: from.Say("You will never find the caddellite!"); break;
						};
					}
					else if ( from is OrkDemigod )
					{
						switch ( Utility.Random( 3 ))		   
						{
							case 0: from.Say("You cannot defeat the power of gods..."); break;
							case 1: from.Say("" + m.Name + ", you have bested me in battle..."); break;
							case 2: from.Say("No..."); break;
						};
					}
					else if ( from is TrollWitchDoctor
						 || from is Troll
						 || from is SwampTroll
						 || from is SeaTroll
						 || from is FrostTrollShaman
						 || from is FrostTroll )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("Me cannot lose!"); break;
							case 1: from.Say("Me curse you..."); break;
						};
					}
					else if ( from is AncientEttin
						 || from is EttinShaman
						 || from is Ettin
						 || from is ArcticEttin )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("Arrrggghhh..."); break;
							case 1: from.Say("You...fight...good..."); break;
						};
					}
					else if ( from is Titan
						 || from is ElderTitan
						 || from is StormGiant )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("By what thunder is this?"); break;
							case 1: from.Say("No, it cannot be..."); break;
							case 2: from.Say("You may have won this day, " + m.Name + "...but..."); break;
							case 3: from.Say("Arrgghhh..."); break;
						};
					}
					else if ( from is Dragonogre
						 || from is TundraOgre
						 || from is OgreMagi
						 || from is OgreLord
						 || from is Ogre
						 || from is ArcticOgreLord
						 || from is AbysmalOgre
						 || from is Neanderthal
						 || from is HillGiant
						 || from is HillGiantShaman )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("Me no feel good!"); break;
							case 1: from.Say("Arrgghhh..."); break;
						};
					}
					else if ( from is IceGiant )
					{
						switch ( Utility.Random( 3 ))		   
						{
							case 0: from.Say("By iceberg's might, how..."); break;
							case 1: from.Say("Arrgghhh..."); break;
							case 2: from.Say("No..."); break;
						};
					}
					else if ( from is LavaGiant )
					{
						switch ( Utility.Random( 3 ))		   
						{
							case 0: from.Say("By magma's might, how..."); break;
							case 1: from.Say("Arrgghhh..."); break;
							case 2: from.Say("No..."); break;
						};
					}
					else if ( from is DeepSeaGiant
						 || from is SeaGiant )
					{
						switch ( Utility.Random( 4 ))		   
						{
							case 0: from.Say("By Neptunes's might, how..."); break;
							case 1: from.Say("By Poseidon's wrath, how..."); break;
							case 2: from.Say("Arrgghhh..."); break;
							case 3: from.Say("No..."); break;
						};
					}
					else if ( from is MountainGiant
						 || from is AbyssGiant
						 || from is JungleGiant
						 || from is SandGiant
						 || from is StoneGiant
						 || from is FireGiant
						 || from is ForestGiant
						 || from is FrostGiant
						 || from is AncientCyclops
						 || from is ShamanicCyclops
						 || from is Cyclops )
					{
						string called = "fly";
						switch ( Utility.Random( 4 ))		   
						{
							case 0: called = "fly"; break;
							case 1: called = "wretch"; break;
							case 2: called = "toad"; break;
							case 3: called = "thing"; break;
						};
						switch ( Utility.Random( 3 ))		   
						{
							case 0: from.Say("You puny " + called + ", how..."); break;
							case 1: from.Say("Arrgghhh..."); break;
							case 2: from.Say("No..."); break;
						};
					}
					else if ( from is TheAncientTree
						 || from is Ent
						 || from is EvilEnt
						 || from is AncientEnt )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("How...did...you..."); break;
							case 1: from.Say("I...am...no...more..."); break;
						};
					}
					else if ( from is SwampThing )
					{
						switch ( Utility.Random( 2 ))		   
						{
							case 0: from.Say("Groo Grite Grood!"); break;
							case 1: from.Say("Grarrgh..."); break;
						};
					}
					else if ( from is Beholder )
					{
						// NOTHING
					}
					else if ( from is Dracolich
						 || from is SkeletalDragon )
					{
						switch ( Utility.Random( 3 ))		   
						{
							case 0: from.Say("My power is eternal!"); break;
							case 1: from.Say("" + m.Name + ", I will have my revenge..."); break;
							case 2: from.Say("No, how can this be?!"); break;
						};
					}
					else if ( from is AncientLich
						 || from is Lich
						 || from is LichKing
						 || from is LichLord
						 || from is Nazghoul
						 || from is TitanLich
						 || from is MummyGiant
						 || from is Surtaz
						 || from is UndeadDruid
						 || from is DemiLich )
					{
						switch ( Utility.Random( 3 ))		   
						{
							case 0: from.Say("My magic is eternal!"); break;
							case 1: from.Say("" + m.Name + ", I will have vengeance..."); break;
							case 2: from.Say("No...how can..."); break;
						};
					}
					else if ( from is Executioner
						 || from is BlackKnight
						 || from is Archmage
						 || from is ElfBerserker
						 || from is ElfRogue
						 || from is ElfMonks
						 || from is ElfMinstrel
						 || from is ElfMage
						 || from is BloodAssassin
						 || from is Berserker
						 || from is Adventurers
						 || from is Bandit
						 || from is Rogue
						 || from is Monks
						 || from is Minstrel
						 || from is GolemController
						 || from is EvilMageLord
						 || from is EvilMage
						 || from is Brigand
						 || from is OrkMonks
						 || from is OrkMage
						 || from is OrkWarrior
						 || from is OrkRogue
						 || Server.Mobiles.BasePirate.IsSailor( from ) )
					{
						switch ( Utility.Random( 5 ))		   
						{
							case 0: from.Say("No!"); break;
							case 1: from.Say("Argh!"); break;
							case 2: from.Say("Ahhh..."); break;
							case 3: from.Say("I...uh...uhhhhh..."); break;
							case 4: from.Say("Nooo..."); break;
						};
					}
				}
			}
		}
	}
}
