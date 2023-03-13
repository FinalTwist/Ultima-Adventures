using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.SkillHandlers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Server.Targeting;
using Server.ContextMenus;
using Server.HuePickers;

namespace Server.Gumps
{
	public enum SquireTitlePage
	{
		PageOne,
		PageTwo,
		PageThree,
		PageFour, // Added 1.9.7
		Server
	}
	
	public class SquireTitleGump : Gump
	{
		private int SkillID;
		private SquireTitlePage m_Page;
		
		public int GetButtonID( int type, int index )
		{
			return 1 + (index * 15) + type;
		}
		
		private const int LabelColor = 0x7FFF;
		private Mobile m_Squire;
		private Mobile m_From;
		
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonID = info.ButtonID - 1;

			int index = buttonID / 15;
			int type = buttonID % 15;
			
			BaseCreature c = ((BaseCreature)m_Squire);
			
			switch( type )
			{
				default:
				{
					m_From.CloseGump( typeof( SquireTitleGump ) );
					break;
				}
				case 1:
				{
					m_From.CloseGump( typeof( SquireTitleGump ) );
					switch( index )
					{
						default:
						{
							m_From.CloseGump( typeof( SquireTitleGump ) );
							break;
						}
						case 1:
						{
							m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageOne ) );
						}
						break;
						case 2:
						{
							m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageTwo ) );
						}
						break;
						case 3:
						{
							m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageThree ) );
						}
						break;
						case 4: // Added 1.9.7
						{
							m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageFour ) );
						}
						break;
					}
					break;
				}
				case 2:
				{
					switch( index )
					{
						default:
						{
							m_From.CloseGump( typeof( SquireTitleGump ) );
							break;
						}
						case 1:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Anatomy].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Anatomy].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Biologist";
							break;
						}
						case 2:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.AnimalLore].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.AnimalLore].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Naturalist";
							break;
						}
						case 3:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Parry].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Parry].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Duelist";
							break;
						}
						case 4:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Peacemaking].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Peacemaking].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Pacifier";
							break;
						}
						case 5:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Discordance].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Discordance].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Demoralizer";
							break;
						}
						case 6:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Healing].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Healing].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Healer";
							break;
						}
						case 7:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Hiding].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Hiding].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Shade";
							break;
						}
						case 8:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Provocation].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Provocation].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Rouser";
							break;
						}
						case 9:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Lockpicking].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Lockpicking].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Infiltrator";
							break;
						}
						case 10:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Tactics].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Tactics].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Tactician";
							break;
						}
						case 11:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Musicianship].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Musicianship].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Bard";
							break;
						}
						case 12:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Archery].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Archery].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Archer";
							break;
						}
						case 13:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.SpiritSpeak].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.SpiritSpeak].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Medium";
							break;
						}
						case 14:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Stealing].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Stealing].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Pickpocket";
							break;
						}
						case 15:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.EvalInt].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.EvalInt].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Scholar";
							break;
						}
						case 16:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Veterinary].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Veterinary].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Veterinarian";
							break;
						}
						case 17:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Swords].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Swords].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Swordsman";
							break;
						}
						case 18:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Macing].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Macing].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Armsman";
							break;
						}
						case 19:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Fencing].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Fencing].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Fencer";
							break;
						}
						case 20:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Wrestling].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Wrestling].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Wrestler";
							break;
						}
						case 21:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Focus].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Focus].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Driven";
							break;
						}
						case 22:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Meditation].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Meditation].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Stoic";
							break;
						}
						case 23:
						{
							if( !c.Female )
							{
								m_Squire.Title = "the He-Man";
							}
							else
							{
								m_Squire.Title = "the Shield Maiden";
							}
							break;
						}
						case 24:
						{
							m_Squire.Title = "the Weapon Master";
							break;
						}
						case 25:
						{
							m_Squire.Title = "the Musical Genius";
							break;
						}
						case 26:
						{
							m_Squire.Title = "the Pugilist";
							break;
						}
						case 27:
						{
							m_Squire.Title = "the Shadow";
							break;
						}
						case 28:
						{
							m_Squire.Title = "the Templar";
							break;
						}
						case 29:
						{
							m_Squire.Title = "the Knight";
							break;
						}
						case 30:
						{
							m_Squire.Title = "the Piercing Shield";
							break;
						}
						case 31:
						{
							m_Squire.Title = "the Captain Britannia";
							break;
						}
						case 32:
						{
							m_Squire.Title = "the Striking Shade";
							break;
						}
						case 33:
						{
							m_Squire.Title = "the Piercing Shade";
							break;
						}
						case 34:
						{
							m_Squire.Title = "the Crushing Shade";
							break;
						}
						case 35:
						{
							m_Squire.Title = "the Shooting Shade";
							break;
						}
						case 36:
						{
							m_Squire.Title = "the Singing Shade";
							break;
						}
						case 37:
						{
							m_Squire.Title = "the Monk";
							break;
						}
						case 38:
						{
							m_Squire.Title = "the Protector";
							break;
						}
						case 39:
						{
							m_Squire.Title = "the Ranger";
							break;
						}
						case 40:
						{
							m_Squire.Title = "the Dedicated Healer";
							break;
						}
						case 41:
						{
							m_Squire.Title = "the Strong";
							break;
						}
						case 42:
						{
							m_Squire.Title = "the Dexterous";
							break;
						}
						case 43:
						{
							m_Squire.Title = "the Intelligent";
							break;
						}
						case 44:
						{
							m_Squire.Title = "the Bardic Healer";
							break;
						}
						case 45:
						{
							if( c.Female )
							{
								m_Squire.Title = "the Priestess";
							}
							else
							{
								m_Squire.Title = "the Priest";
							}
							break;
						}
						case 46:
						{
							m_Squire.Title = "the Cup Bearer";
							break;
						}
						case 47:
						{
							m_Squire.Title = "the Dapifer";
							break;
						}
						case 48:
						{
							m_Squire.Title = "the Doorward";
							break;
						}
						case 49:
						{
							m_Squire.Title = "the Pursuivant";
							break;
						}
						case 50:
						{
							m_Squire.Title = "the Herald";
							break;
						}
						case 51:
						{
							m_Squire.Title = "the Jester";
							break;
						}
						case 52:
						{
							if( c.Female )
							{
								m_Squire.Title = "the Lady in Waiting";
							}
							else
							{
								m_Squire.Title = "the Gentleman in Waiting";
							}
							break;
						}
						case 53:
						{
							if( c.Female )
							{
								m_Squire.Title = "the Lady of Honor";
							}
							else
							{
								m_Squire.Title = "the Gentleman of Honor";
							}
							break;
						}
						case 54:
						{
							m_Squire.Title = "the Page";
							break;
						}
						case 55:
						{
							m_Squire.Title = "the Pantler";
							break;
						}
						case 56:
						{
							m_Squire.Title = "the Squire";
							break;
						}
						case 57:
						{
							m_Squire.Title = "the Minstrel";
							break;
						}
						case 58:
						{
							m_Squire.Title = "the Troubadour";
							break;
						}
						case 59:
						{
							m_Squire.Title = "the Jongleur";
							break;
						}
						case 60:
						{
							m_Squire.Title = "the Steward";
							break;
						}
						m_From.CloseGump( typeof( SquireTitleGump ) );
						m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageOne ) );
					}
					break;
				}
				case 3:
				{
					switch( index )
					{
						default:
						{
							m_From.CloseGump( typeof( SquireTitleGump ) );
							break;
						}
						case 1:
						{
							m_Squire.Title = "";
							break;
						}
					}
					m_From.CloseGump( typeof( SquireTitleGump ) );
					m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageOne ) );
					break;
				}
				case 4:
				{
					switch( index )
					{
						case 1:
						{
							m_Squire.Title = "the Dreamer";
							break;
						}
						case 2:
						{
							m_Squire.Title = "the Landsknect";
							break;
						}
						case 3:
						{
							if( !c.Female )
							{
								m_Squire.Title = "the Crossbow-man";
							}
							else
							{
								m_Squire.Title = "the Crossbow-maiden";
							}
							break;
						}
						case 4:
						{
							if( !c.Female )
							{
								m_Squire.Title = "the Longbow-man";
							}
							else
							{
								m_Squire.Title = "the Longbow-maiden";
							}
							break;
						}
						case 5:
						{
							m_Squire.Title = "the Lancer";
							break;
						}
						case 6:
						{
							m_Squire.Title = "the Dragoon";
							break;
						}
						case 7:
						{
							m_Squire.Title = "the Calvary Archer";
							break;
						}
						case 8:
						{
							if( c.Female )
							{
								m_Squire.Title = "the Maid";
							}
							else
							{
								m_Squire.Title = "the Gentleman";
							}
							break;
						}
						case 9:
						{
							m_Squire.Title = "the Runner";
							break;
						}
						case 10:
						{
							m_Squire.Title = "the Sprinter";
							break;
						}
						case 11:
						{
							m_Squire.Title = "the Hammerdon";
							break;
						}
						case 12:
						{
							m_Squire.Title = "the Healing Touch";
							break;
						}
						case 13:
						{
							m_Squire.Title = "the Light Shade";
							break;
						}
						case 14:
						{
							if( c.Female )
							{
								m_Squire.Title = "the Yeomaiden";
							}
							else
							{
								m_Squire.Title = "the Yeoman";
							}
							break;
						}
						case 15:
						{
							m_Squire.Title = "the Attendant";
							break;
						}
						case 16:
						{
							m_Squire.Title = "the Dancer";
							break;
						}
						case 17:
						{
							m_Squire.Title = "the Warrior";
							break;
						}
						case 18:
						{
							m_Squire.Title = "the Freelancer";
							break;
						}
						case 19:
						{
							m_Squire.Title = "the Thief";
							break;
						}
						case 20:
						{
							m_Squire.Title = "the Dark Knight";
							break;
						}
						case 21:
						{
							m_Squire.Title = "the Black Belt";
							break;
						}
						case 22:
						{
							m_Squire.Title = "the Devout";
							break;
						}
						case 23:
						{
							m_Squire.Title = "the Hunter";
							break;
						}
						case 24:
						{
							m_Squire.Title = "the Defender";
							break;
						}
						case 25:
						{
							m_Squire.Title = "the Wall";
							break;
						}
						case 26:
						{
							m_Squire.Title = "the Gladiator";
							break;
						}
						case 27:
						{
							m_Squire.Title = "the Sniper";
							break;
						}
						case 28:
						{
							m_Squire.Title = "the Medic";
							break;
						}
						case 29:
						{
							m_Squire.Title = "the Field Medic";
							break;
						}
						case 30:
						{
							m_Squire.Title = "the Berserker";
							break;
						}
						case 31:
						{
							m_Squire.Title = "the Ravager";
							break;
						}
						case 32:
						{
							m_Squire.Title = "the Sword";
							break;
						}
						case 33:
						{
							m_Squire.Title = "the Hammer";
							break;
						}
						case 34:
						{
							m_Squire.Title = "the Bow";
							break;
						}
						case 35:
						{
							m_Squire.Title = "the Point";
							break;
						}
						case 36:
						{
							m_Squire.Title = "the Bandage";
							break;
						}
						case 37:
						{
							m_Squire.Title = "the Shield";
							break;
						}
						case 38:
						{
							m_Squire.Title = "the Knife";
							break;
						}
						case 39:
						{
							m_Squire.Title = "the Cloak";
							break;
						}
						case 40:
						{
							m_Squire.Title = "the Weak";
							break;
						}
						case 41:
						{
							m_Squire.Title = "the Slow";
							break;
						}
						case 42:
						{
							m_Squire.Title = "the Dim";
							break;
						}
						case 43:
						{
							m_Squire.Title = "the Hidden";
							break;
						}
						case 44:
						{
							m_Squire.Title = "the Healing Blade";
							break;
						}
						case 45:
						{
							m_Squire.Title = "the Song Sword";
							break;
						}
						case 46:
						{
							m_Squire.Title = "the Caring Blade";
							break;
						}
						case 47:
						{
							m_Squire.Title = "the Resting Sword";
							break;
						}
						case 48:
						{
							m_Squire.Title = "the Channeled Blade";
							break;
						}
						case 49:
						{
							m_Squire.Title = "the Stolen Sword";
							break;
						}
						case 50:
						{
							m_Squire.Title = "the Cutthroat";
							break;
						}
						case 51:
						{
							m_Squire.Title = "the Spirit Channeler";
							break;
						}
						case 52:
						{
							m_Squire.Title = "the Fortune Teller";
							break;
						}
						case 53:
						{
							m_Squire.Title = "the Hammer of Rest";
							break;
						}
						case 54:
						{
							m_Squire.Title = "the Channeled Hammer";
							break;
						}
						case 55:
						{
							m_Squire.Title = "the Hammer of Song";
							break;
						}
						case 56:
						{
							m_Squire.Title = "the Healing Hammer";
							break;
						}
						case 57:
						{
							m_Squire.Title = "the Caring Hammer";
							break;
						}
						case 58:
						{
							m_Squire.Title = "the Singing Shot";
							break;
						}
						case 59:
						{
							m_Squire.Title = "the Channeled Shot";
							break;
						}
						case 60:
						{
							m_Squire.Title = "the Healing Shot";
							break;
						}
					}
					m_From.CloseGump( typeof( SquireTitleGump ) );
					m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageTwo ) );
					break;
				}
				case 5:
				{
					switch(index)
					{
						case 1: // Corrected Requirements 1.9.7
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Poisoning].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Poisoning].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Assassin";
							break;
						}
						case 2:
						{
							m_Squire.Title = "the Toxic Shade";
							break;
						}
						case 3:
						{
							m_Squire.Title = "the Venomous Blade";
							break;
						}
						case 4:
						{
							m_Squire.Title = "the Poison Shot";
							break;
						}
						case 5:
						{
							m_Squire.Title = "the Traitorous Healer";
							break;
						}
						case 6:
						{
							m_Squire.Title = "the Poisonous Point";
							break;
						}
						case 7:
						{
							m_Squire.Title = "the Singing Stab";
							break;
						}
						case 8:
						{
							m_Squire.Title = "the Healing Point";
							break;
						}
						case 9:
						{
							m_Squire.Title = "the Channelled Stab";
							break;
						}
						case 10:
						{
							m_Squire.Title = "the Protected Point";
							break;
						}
						case 11:
						{
							m_Squire.Title = "the Rested Point";
							break;
						}
						case 12:
						{
							m_Squire.Title = "the Serpent's Tooth";
							break;
						}
						case 13:
						{
							m_Squire.Title = "the Serpent's Fang";
							break;
						}
						case 14:
						{
							m_Squire.Title = "the Strong Poison";
							break;
						}
						case 15:
						{
							m_Squire.Title = "the Quick Poison";
							break;
						}
						case 16:
						{
							m_Squire.Title = "the Smart Poison";
							break;
						}
						case 17:
						{
							m_Squire.Title = "the Channelled Venom";
							break;
						}
						case 18:
						{
							m_Squire.Title = "the Villain";
							break;
						}
						case 19:
						{
							m_Squire.Title = "the Blunt Poison";
							break;
						}
						case 20:
						{
							m_Squire.Title = "the Stolen Venom";
							break;
						}
						case 21:
						{
							m_Squire.Title = "the Caring Point";
							break;
						}
						case 22:
						{
							m_Squire.Title = "the Caring Shot";
							break;
						}
						case 23:
						{
							m_Squire.Title = "the Healing Shot";
							break;
						}
						case 24:
						{
							m_Squire.Title = "the True Defender";
							break;
						}
						case 25:
						{
							m_Squire.Title = "the Resistant Wall";
							break;
						}
						case 26:
						{
							m_Squire.Title = "the Channelled Bubble";
							break;
						}
						case 27:
						{
							m_Squire.Title = "the Protector";
							break;
						}
						case 28:
						{
							m_Squire.Title = "the Soldier";
							break;
						}
						case 29:
						{
							m_Squire.Title = "the Disciple";
							break;
						}
						case 30:
						{
							m_Squire.Title = "the Mercenary";
							break;
						}
						case 31:
						{
							m_Squire.Title = "the Veteran";
							break;
						}
						case 32:
						{
							m_Squire.Title = "the Mentor";
							break;
						}
						case 33:
						{
							m_Squire.Title = "the Recruit";
							break;
						}
						case 34:
						{
							m_Squire.Title = "the Initiate";
							break;
						}
						case 35:
						{
							m_Squire.Title = "the True Assassin";
							break;
						}
						case 36:
						{
							m_Squire.Title = "the Thrifty";
							break;
						}
						case 37:
						{
							m_Squire.Title = "the Poor";
							break;
						}
						case 38:
						{
							m_Squire.Title = "the Moderately Wealthy";
							break;
						}
						case 39:
						{
							m_Squire.Title = "the Wealthy";
							break;
						}
						case 40:
						{
							m_Squire.Title = "the Pretty Rich";
							break;
						}
						case 41:
						{
							m_Squire.Title = "the Rich";
							break;
						}
						case 42:
						{
							m_Squire.Title = "the Filthy Rich";
							break;
						}
						case 43:
						{
							m_Squire.Title = "the Mysterious";
							break;
						}
						case 44:
						{
							m_Squire.Title = "the Toxic Mystery";
							break;
						}
						case 45:
						{
							m_Squire.Title = "the Athlete";
							break;
						}
						case 46:
						{
							m_Squire.Title = "the Desperado";
							break;
						}
						case 47:
						{
							m_Squire.Title = "the Sneaky";
							break;
						}
						case 48:
						{
							m_Squire.Title = "the Sword Artist";
							break;
						}
						case 49:
						{
							if( c.Female == true )
							{
								m_Squire.Title = "the Huntress";
							}
							else
							{
								m_Squire.Title = "the Hunter";
							}
							break;
						}
						case 50:
						{
							m_Squire.Title = "the Troublesome";
							break;
						}
						case 51:
						{
							m_Squire.Title = "the Acolyte";
							break;
						}
						case 52:
						{
							m_Squire.Title = "the Wingleader";
							break;
						}
						case 53:
						{
							m_Squire.Title = "the Mama Jama";
							break;
						}
						case 54:
						{
							m_Squire.Title = "the Rum Thief";
							break;
						}
						case 55:
						{
							m_Squire.Title = "the Red-Hand";
							break;
						}
						case 56:
						{
							m_Squire.Title = "the Heretic";
							break;
						}
						case 57:
						{
							m_Squire.Title = "the Sly";
							break;
						}
						case 58:
						{
							m_Squire.Title = "the Glorious";
							break;
						}
						case 59:
						{
							m_Squire.Title = "the Stalker";
							break;
						}
						case 60:
						{
							m_Squire.Title = "the Stalker";
							break;
						}
					}
					
					m_From.CloseGump( typeof( SquireTitleGump ) );
					m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageThree ) );
					break;
				}
				case 6: // Added 1.9.7
				{
					switch(index)
					{
						case 1:
						{
							string beginningtitle;
							if( m_Squire.Skills[SkillName.Chivalry].Base == 120.0 )
							{
								beginningtitle = "the Legendary ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 110.0 )
							{
								beginningtitle = "the Elder ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 100.0 )
							{
								beginningtitle = "the Grandmaster ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 90.0 )
							{
								beginningtitle = "the Master ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 80.0 )
							{
								beginningtitle = "the Adept ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 70.0 )
							{
								beginningtitle = "the Expert ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 60.0 )
							{
								beginningtitle = "the Journeyman ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 50.0 )
							{
								beginningtitle = "the Apprentice ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 40.0 )
							{
								beginningtitle = "the Novice ";
							}
							else if( m_Squire.Skills[SkillName.Chivalry].Base >= 30.0 )
							{
								beginningtitle = "the Neophyte ";
							}
							else
							{
								beginningtitle = "the ";
							}
							
							m_Squire.Title = beginningtitle + "Paladin";
							break;
						}
						case 2:
						{
							m_Squire.Title = "the Holy Sword";
							break;
						}
						case 3:
						{
							m_Squire.Title = "the Holy Hammer";
							break;
						}
						case 4:
						{
							m_Squire.Title = "the Holy Striker";
							break;
						}
						case 5:
						{
							m_Squire.Title = "the Holy Channel";
							break;
						}
						case 6:
						{
							m_Squire.Title = "the Holy Shade";
							break;
						}
						case 7:
						{
							m_Squire.Title = "the Holy Healer";
							break;
						}
						case 8:
						{
							m_Squire.Title = "the Holy Shield";
							break;
						}
						case 9:
						{
							m_Squire.Title = "the Slightly Devoted";
							break;
						}
						case 10:
						{
							m_Squire.Title = "the Kinda Devoted";
							break;
						}
						case 11:
						{
							m_Squire.Title = "the Devoted";
							break;
						}
						case 12:
						{
							m_Squire.Title = "the Very Devoted";
							break;
						}
						case 13:
						{
							m_Squire.Title = "the Extremely Devoted";
							break;
						}
						case 14:
						{
							m_Squire.Title = "the True Paladin";
							break;
						}
						case 15:
						{
							m_Squire.Title = "the Holy Toxin";
							break;
						}
						case 16:
						{
							m_Squire.Title = "the Holy Shot";
							break;
						}
						case 17:
						{
							m_Squire.Title = "the Blessed Ranger";
							break;
						}
						case 18:
						{
							m_Squire.Title = "the Caring Light";
							break;
						}
						case 19:
						{
							m_Squire.Title = "the Holy Light";
							break;
						}
						case 20:
						{
							m_Squire.Title = "the Holy Song";
							break;
						}
						case 21:
						{
							m_Squire.Title = "the Strong Paladin";
							break;
						}
						case 22:
						{
							m_Squire.Title = "the Smart Paladin";
							break;
						}
						case 23:
						{
							m_Squire.Title = "the Quick Paladin";
							break;
						}
						case 24:
						{
							m_Squire.Title = "the Worthy";
							break;
						}
						case 25:
						{
							m_Squire.Title = "the Despicable";
							break;
						}
						case 26:
						{
							m_Squire.Title = "the Shining Shadow";
							break;
						}
						case 27:
						{
							m_Squire.Title = "the Foetid";
							break;
						}
						case 28:
						{
							m_Squire.Title = "the Holy Warrior";
							break;
						}
						case 29:
						{
							if( m_Squire.Female )
							{
								m_Squire.Title = "the Vixen";
							}
							else
							{
								m_Squire.Title = "the Fox";
							}
							break;
						}
						case 30:
						{
							m_Squire.Title = "the Holy Focus";
							break;
						}
						case 31:
						{
							m_Squire.Title = "the Holy Tactician";
							break;
						}
						case 32:
						{
							m_Squire.Title = "the Holy Thief";
							break;
						}
						case 33:
						{
							m_Squire.Title = "the Faithless";
							break;
						}
						case 34:
						{
							m_Squire.Title = "the Humble";
							break;
						}
						case 35:
						{
							if( m_Squire.Female )
							{
								m_Squire.Title = "the Nun";
							}
							else
							{
								m_Squire.Title = "the Priest";
							}
							break;
						}
						case 36:
						{
							m_Squire.Title = "the True Healer";
							break;
						}
						case 37:
						{
							m_Squire.Title = "the True Holy Healer";
							break;
						}
						case 38:
						{
							m_Squire.Title = "the Ruffian";
							break;
						}
						case 39:
						{
							m_Squire.Title = "the Pure";
							break;
						}
						case 40:
						{
							m_Squire.Title = "the Holy Templar";
							break;
						}
						case 41:
						{
							m_Squire.Title = "the Invoker";
							break;
						}
						case 42:
						{
							m_Squire.Title = "the Holy Peacemaker";
							break;
						}
						case 43:
						{
							m_Squire.Title = "the True Hope";
							break;
						}
						case 44:
						{
							m_Squire.Title = "the Adventurous";
							break;
						}
						case 45:
						{
							m_Squire.Title = "the Fighter";
							break;
						}
						case 46:
						{
							m_Squire.Title = "the Skilled";
							break;
						}
						case 47:
						{
							if( m_Squire.Race == Race.Human )
							{
								m_Squire.Title = "the Human";
							}
							else
							{
								m_Squire.Title = "the Elf";
							}
							break;
						}
						case 48:
						{
							if( m_Squire.Female )
							{
								m_Squire.Title = "the Girl";
							}
							else
							{
								m_Squire.Title = "the Boy";
							}
							break;
						}
						case 49:
						{
							if( m_Squire.Female )
							{
								m_Squire.Title = "the Woman";
							}
							else
							{
								m_Squire.Title = "the Man";
							}
							break;
						}
						case 50:
						{
							if( m_Squire.Female )
							{
								m_Squire.Title = "the Heroine";
							}
							else
							{
								m_Squire.Title = "the Hero";
							}
							break;
						}
						case 51:
						{
							m_Squire.Title = "the Tempest";
							break;
						}
						case 52:
						{
							m_Squire.Title = "the Uber";
							break;
						}
						case 53:
						{
							m_Squire.Title = "the Tank";
							break;
						}
						case 54:
						{
							m_Squire.Title = "the Holy Bubble";
							break;
						}
						case 55:
						{
							m_Squire.Title = "the Panzer";
							break;
						}
						case 56:
						{
							m_Squire.Title = "the Great Paladin";
							break;
						}
						case 57:
						{
							m_Squire.Title = "the Great Hammerdin";
							break;
						}
						case 58:
						{
							m_Squire.Title = "the Great Defender";
							break;
						}
						case 59:
						{
							m_Squire.Title = "the Great Piercer";
							break;
						}
						case 60:
						{
							m_Squire.Title = "the Great Ranger";
							break;
						}
					}
					
					m_From.CloseGump( typeof( SquireTitleGump ) );
					m_From.SendGump( new SquireTitleGump( ((BaseCreature)m_Squire), m_From, SquireTitlePage.PageFour ) );
					break;
				}
			}
		}
		
		public SquireTitleGump( BaseCreature c, Mobile from, SquireTitlePage page ) : base( 250, 50 )
		{
			m_Squire = c;
			m_From = from;
			m_Page = page;
			
			from.CloseGump( typeof( SquireTitleGump ) );
			
			AddPage( 0 );

			AddBackground( 0, 0, 460, 422, 5054 ); 
			AddImageTiled( 10, 10, 440, 402, 2624 ); 
			AddAlphaRegion( 10, 10, 440, 402 ); 

			AddHtml( 120, 10, 210, 18, String.Format( "<basefont color = #FFFFFF><center><i>{0}</i></center></basefont>", c.Name ), false, false );

			int pages = ( Core.AOS ? 5 : 3 );
			int buttonID1, buttonID2;
			
			if ( page == SquireTitlePage.PageOne )
			{
				//Middle Display
				AddHtml( 145, 24, 160, 18, "<basefont color = #FFD57A><center>Titles Page: One</center></basefont>", false, false );
				
				AddHtml( 49, 20, 160, 18, "<basefont color = #FFD57A>None</basefont>", false, false );
				AddButton( 20, 14, 0x9A8, 0x9AA, GetButtonID( 3, 1 ), GumpButtonType.Reply, 0 );
				
				//First Third
				int Sideways = 20;
				int Downwards = 40;
				
				if( c.Skills.Anatomy.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Biologist</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 1 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.AnimalLore.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Naturalist</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 2 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Duelist</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 3 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Peacemaking.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pacifier</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 4 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Discordance.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Demoralizer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 5 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 6 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 7 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Provocation.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Rouser</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 8 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Lockpicking.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Infiltrator</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 9 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Tactics.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Tactician</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 10 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Bard</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 11 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 100.0 && c.Str >= 125 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>He-Man</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 23 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Skills.Swords.Base >= 100.0 && c.Str >= 125 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Shield Maiden</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 23 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base >= 100.0 && c.Skills.Peacemaking.Base >= 100.0 && c.Skills.Discordance.Base >= 100.0 && c.Skills.Provocation.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Musical Genius</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 25 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 100.0 && c.Skills.Stealing.Base >= 100.0 && c.Skills.Lockpicking.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Shadow</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 27 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Knight</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 29 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 100.0 && c.Skills.Wrestling.Base >= 100.0 && c.Str >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Captain Britannia</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 31 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 100.0 && c.Skills.Fencing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Piercing Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 33 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 100.0 && c.Skills.Archery.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Shooting Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 35 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 100.0 && c.Skills.Wrestling.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Monk</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 37 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 70.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Ranger</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 39 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Second Third
				Sideways = 160;
				Downwards = 40;
				
				if( c.Skills.Archery.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Archer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 12 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Medium</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 13 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Stealing.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pickpocket</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 14 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.EvalInt.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Scholar</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 15 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Veterinary.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Veterinarian</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 16 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Swordsman</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 17 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Armsman</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 18 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Fencing.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Fencer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 19 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Wrestling.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Wrestler</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 20 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Focus.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Driven</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 21 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Meditation.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Stoic</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 22 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 90.0 && c.Skills.Macing.Base >= 90.0 && c.Skills.Archery.Base >= 90.0 && c.Skills.Fencing.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Weapon Master</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 24 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Wrestling.Base >= 80.0 && c.Str >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pugilist</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 26 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 80.0 && ( c.Skills.Swords.Base >= 80.0 || c.Skills.Macing.Base >= 80.0 ) )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Templar</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 28 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Fencing.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Piercing Shield</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 30 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Striking Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 32 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 100.0 && c.Skills.Macing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Crushing Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 34 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 100.0 && c.Skills.Musicianship.Base >= 100.0 )
				{ // Corrected incorrect "Crushing Shade" display to "Singing Shade" 1.9.5
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Singing Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 36 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 50.0 && c.Skills.Swords.Base >= 50.0 && c.Skills.Parry.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Protector</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 38 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 80.0 && c.Skills.Veterinary.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dedicated Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 40 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Third Third
				Sideways = 300;
				Downwards = 40;
				
				if( c.Str >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Strong</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 41 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dexterous</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 42 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Int >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Intelligent</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 43 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 80.0 && c.Skills.Musicianship.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Bardic Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 44 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 80.0 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Priestess</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 45 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Skills.Healing.Base >= 80.0 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Priest</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 45 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Str >= 10 && c.Dex >= 40 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Cup Bearer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 46 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Str >= 50 && c.Dex >= 40 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dapifer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 47 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 60.0 || c.Skills.Fencing.Base >= 60.0 || c.Skills.Macing.Base >= 60.0 || c.Skills.Archery.Base >= 60.0 ) && c.Skills.Tactics.Base >= 60.0 && c.Str >= 70 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Doorward</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 48 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 60.0 || c.Skills.Fencing.Base >= 60.0 || c.Skills.Macing.Base >= 60.0 || c.Skills.Archery.Base >= 60.0 ) && c.Skills.Tactics.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pursuivant</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 49 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 90.0 || c.Skills.Fencing.Base >= 90.0 || c.Skills.Macing.Base >= 90.0 || c.Skills.Archery.Base >= 90.0 ) && c.Skills.Tactics.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Herald</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 50 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Str >= 20 && c.Dex >= 10 && c.Int >= 20 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Jester</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 51 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Int >= 60 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Lady in Waiting</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 52 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Int >= 60 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Gentleman in Waiting</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 52 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Int >= 50 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Maid of Honor</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 53 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Int >= 50 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Gentleman of Honor</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 53 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 30.0 || c.Skills.Fencing.Base >= 30.0 || c.Skills.Macing.Base >= 30.0 || c.Skills.Archery.Base >= 30.0 ) && c.Skills.Tactics.Base >= 30.0 && c.Str >= 30 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Page</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 54 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 50.0 || c.Skills.Fencing.Base >= 50.0 || c.Skills.Macing.Base >= 50.0 || c.Skills.Archery.Base >= 50.0 ) && c.Skills.Tactics.Base >= 30.0 && c.Str >= 40 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pantler</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 55 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Squire</basefont>", false, false );
				AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 56 ), GumpButtonType.Reply, 0 );
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 70.0 || c.Skills.Fencing.Base >= 70.0 || c.Skills.Macing.Base >= 70.0 || c.Skills.Archery.Base >= 70.0 ) && c.Skills.Tactics.Base >= 70.0 && c.Str >= 60 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Steward</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 60 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base >= 30.0 && c.Skills.Provocation.Base >= 30.0 && c.Skills.Peacemaking.Base >= 30.0 && c.Skills.Discordance.Base >= 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Jongleur</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 59 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base >= 50.0 && c.Skills.Provocation.Base >= 50.0 && c.Skills.Peacemaking.Base >= 50.0 && c.Skills.Discordance.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Minstrel</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 57 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base >= 90.0 && c.Skills.Provocation.Base >= 90.0 && c.Skills.Peacemaking.Base >= 90.0 && c.Skills.Discordance.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Troubadour</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 2, 58 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Navigation
				if( ( c.Controlled && from == c.ControlMaster ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					AddHtml( 353, 20, 160, 18, "<basefont color = #FFD57A>Page Two</basefont>", false, false );
					AddButton( 417, 22, 5601, 5605, GetButtonID( 1, 2 ), GumpButtonType.Reply, 0 );
				}
			}
			
			if ( page == SquireTitlePage.PageTwo )
			{
				//Middle Display
				AddHtml( 145, 24, 160, 18, "<basefont color = #FFD57A><center>Titles Page: Two</center></basefont>", false, false );
				
				AddHtml( 49, 20, 160, 18, "<basefont color = #FFD57A>None</basefont>", false, false );
				AddButton( 20, 14, 0x9A8, 0x9AA, GetButtonID( 3, 1 ), GumpButtonType.Reply, 0 );
				
				//First Third
				int Sideways = 20;
				int Downwards = 40;
				
				if( c.Int >= 40 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dreamer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 1 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 80.0 && c.Skills.Tactics.Base >= 80.0 && c.Skills.Parry.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Landsknect</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 2 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 80.0 && c.Skills.Tactics.Base >= 80.0 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Crossbow-man</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 3 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Skills.Archery.Base >= 80.0 && c.Skills.Tactics.Base >= 80.0 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Crossbow-maiden</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 3 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 80.0 && c.Skills.Tactics.Base >= 80.0 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Longbow-man</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 4 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Skills.Archery.Base >= 80.0 && c.Skills.Tactics.Base >= 80.0 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Longbow-maiden</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 4 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 90.0 && c.Skills.Tactics.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Lancer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 5 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 110.0 && c.Skills.Tactics.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dragoon</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 6 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 110.0 && c.Skills.Tactics.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Calvary Archer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 7 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Int >= 50 && c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Maid</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 8 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Int >= 50 && !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Gentleman</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 8 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 60 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Runner</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 9 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 90 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Sprinter</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 10 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 110.0 && c.Str >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hammerdon</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 11 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 110.0 && c.Skills.Anatomy.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healing Touch</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 12 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 110.0 && c.Skills.Hiding.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Light Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 13 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( !c.Female && c.Skills.Swords.Base >= 60.0 && c.Skills.Archery.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Yeoman</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 14 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Female && c.Skills.Swords.Base >= 60.0 && c.Skills.Archery.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Yeomaiden</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 14 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Tactics.Base >= 50.0 && c.Skills.Healing.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Attendant</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 15 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Wrestling.Base >= 80.0 && c.Skills.Musicianship.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dancer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 16 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 80.0 && c.Skills.Wrestling.Base >= 80.0 && c.Skills.Fencing.Base >= 80.0 && c.Skills.Tactics.Base >= 80.0 && c.Skills.Anatomy.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Warrior</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 17 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 50.0 && c.Skills.Wrestling.Base >= 50.0 && c.Skills.Fencing.Base >= 50.0 && c.Skills.Tactics.Base >= 50.0 && c.Skills.Anatomy.Base >= 50.0 && c.Skills.Archery.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Freelancer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 18 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 50.0 && c.Skills.Stealing.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Thief</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 19 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 90.0 && c.Skills.Stealing.Base >= 50.0 && c.Skills.Parry.Base >= 70.0 && ( c.Skills.Swords.Base >= 100.0 || c.Skills.Macing.Base >= 100.0 ) )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dark Knight</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 20 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Second Third
				Sideways = 160;
				Downwards = 40;
				
				if( c.Skills.Wrestling.Base >= 110.0 && c.Skills.Tactics.Base >= 110.0 && c.Skills.Anatomy.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Black Belt</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 21 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 110.0 && c.Skills.Anatomy.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Devout</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 22 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 110.0 && c.Skills.Tactics.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hunter</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 23 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 70.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Defender</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 24 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Wall</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 25 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Gladiator</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 26 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 120.0 && c.Skills.Tactics.Base >= 120.0 && c.Dex >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Sniper</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 27 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 60.0 && c.Skills.Anatomy.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Medic</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 28 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 80.0 && c.Skills.Anatomy.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Field Medic</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 29 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 100.0 && c.Str >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Berserker</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 30 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 100.0 && c.Str >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Ravager</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 31 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Sword</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 32 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hammer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 33 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Bow</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 34 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Fencing.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Point</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 35 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 120.0 || c.Skills.Veterinary.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Bandage</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 36 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Shield</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 37 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Stealing.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Knife</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 38 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Cloak</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 39 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Str >= 10.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Weak</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 40 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Third Third
				Sideways = 300;
				Downwards = 40;
				
				if( c.Dex >= 10.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Slow</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 41 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Int >= 10.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Dim</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 42 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hidden</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 43 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healing Blade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 44 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Song Sword</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 45 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Veterinary.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Caring Blade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 46 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Meditation.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Resting Sword</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 47 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Channeled Blade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 48 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Stealing.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Stolen Sword</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 49 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Stealing.Base >= 75.0 && c.Skills.Swords.Base >= 75.0 && c.Skills.Hiding.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Cutthroat</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 50 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 90.0 && c.Skills.Healing.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Spirit Channeler</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 51 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 90.0 && c.Skills.Meditation.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Fortune Teller</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 52 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 75.0 && c.Skills.Meditation.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hammer of Rest</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 53 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 75.0 && c.Skills.SpiritSpeak.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Channeled Hammer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 54 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 75.0 && c.Skills.Musicianship.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hammer of Song</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 55 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 75.0 && c.Skills.Healing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healing Hammer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 56 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Macing.Base >= 75.0 && c.Skills.Veterinary.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Caring Hammer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 57 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 75.0 && c.Skills.Musicianship.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Singing Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 58 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 75.0 && c.Skills.SpiritSpeak.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Channeled Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 59 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 75.0 && c.Skills.Healing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healing Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 4, 60 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Navigation
				if( ( c.Controlled && from == c.ControlMaster ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					AddHtml( 353, 20, 160, 18, "<basefont color = #FFD57A>Page Three</basefont>", false, false );
					AddButton( 417, 22, 5601, 5605, GetButtonID( 1, 3 ), GumpButtonType.Reply, 0 );
				}
			}
			
			if ( page == SquireTitlePage.PageThree ) // Added 1.9.5
			{
				//Middle Display
				AddHtml( 145, 24, 160, 18, "<basefont color = #FFD57A><center>Titles Page: Three</center></basefont>", false, false );
				
				AddHtml( 49, 20, 160, 18, "<basefont color = #FFD57A>None</basefont>", false, false );
				AddButton( 20, 14, 0x9A8, 0x9AA, GetButtonID( 3, 1 ), GumpButtonType.Reply, 0 );
				
				//First Third
				int Sideways = 20;
				int Downwards = 40;
				
				if( c.Skills.Poisoning.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Assassin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 1 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 100.0 && c.Skills.Hiding.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Toxic Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 2 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Venomous Blade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 3 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 100.0 && c.Skills.Archery.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Poison Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 4 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 100.0 && c.Skills.Healing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Traitorous Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 5 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 100.0 && c.Skills.Fencing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Poisonous Point</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 6 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Musicianship.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Singing Stab</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 7 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healing Point</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 8 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Channelled Stab</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 9 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Protected Point</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 10 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Meditation.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Rested Point</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 11 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Serpent's Tooth</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 12 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 && c.Skills.Tactics.Base >= 75.0 && c.Dex >= 70 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Serpent's Fang</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 13 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 80.0 && c.Str >= 75 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Strong Poison</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 14 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 80.0 && c.Dex >= 75 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Quick Poison</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 15 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 80.0 && c.Int >= 75 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Smart Poison</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 16 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 && c.Skills.SpiritSpeak.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Channelled Venom</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 17 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 100.0 && c.Skills.Hiding.Base >= 100.0 && c.Skills.Stealing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Villain</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 18 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 && c.Skills.Macing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Blunt Poison</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 19 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 && c.Skills.Stealing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Stolen Venom</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 20 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Second Third
				Sideways = 160;
				Downwards = 40;
				
				if( c.Skills.Veterinary.Base >= 75.0 && c.Skills.Fencing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Caring Point</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 21 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Veterinary.Base >= 75.0 && c.Skills.Archery.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Caring Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 22 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 75.0 && c.Skills.Archery.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Healing Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 23 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>True Defender</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 24 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 110.0 && c.Skills.MagicResist.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Resistant Wall</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 25 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 80.0 && c.Skills.MagicResist.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Channelled Bubble</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 26 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 90.0 && c.Skills.MagicResist.Base >= 90.0 && c.Skills.Parry.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Protector</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 27 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.MagicResist.Base >= 30.0 && c.Skills.Meditation.Base >= 30.0 && c.Skills.SpiritSpeak.Base >= 30.0 && c.Skills.Healing.Base >= 30.0 && c.Skills.Veterinary.Base >= 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Disciple</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 29 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 20.0 && c.Skills.Fencing.Base >= 20.0 && c.Skills.Macing.Base >= 20.0 && c.Skills.Archery.Base >= 20.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Recruit</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 33 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 35.0 && c.Skills.Fencing.Base >= 35.0 && c.Skills.Macing.Base >= 35.0 && c.Skills.Archery.Base >= 35.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Initiate</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 34 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 50.0 && c.Skills.Fencing.Base >= 50.0 && c.Skills.Macing.Base >= 50.0 && c.Skills.Archery.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Soldier</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 28 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 70.0 || c.Skills.Fencing.Base >= 70.0 || c.Skills.Macing.Base >= 70.0 || c.Skills.Archery.Base >= 70.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Mercenary</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 30 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 95.0 && c.Skills.Fencing.Base >= 95.0 && c.Skills.Macing.Base >= 95.0 && c.Skills.Archery.Base >= 95.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Veteran</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 31 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.SkillsTotal >= 700.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Mentor</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 32 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 95.0 && c.Skills.Fencing.Base >= 95.0 && c.Skills.Hiding.Base >= 95.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>True Assassin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 35 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 80 && c.Int >= 80 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Thrifty</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 36 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TotalGold <= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Poor</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 37 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TotalGold >= 1000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Moderately Wealthy</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 38 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TotalGold >= 5000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Wealthy</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 39 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TotalGold >= 10000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pretty Rich</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 40 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Third Third
				Sideways = 300;
				Downwards = 40;
				
				if( c.TotalGold >= 50000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Rich</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 41 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TotalGold >= 100000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Filthy Rich</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 42 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 90 && c.Skills.Hiding.Base >= 90 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Mysterious</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 43 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.SpiritSpeak.Base >= 90 && c.Skills.Hiding.Base >= 90 && c.Skills.Poisoning.Base >= 90 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Toxic Mystery</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 44 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Str >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Athlete</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 45 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 100 && c.Skills.Archery.Base >= 100.0 && c.Skills.Tactics.Base >= 100.0 && c.Skills.Stealing.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Desperado</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 46 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 100 && c.Skills.Hiding.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Sneaky</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 47 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Fencing.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Sword Artist</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 48 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 && c.Female == false )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hunter</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 49 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Skills.Archery.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 && c.Female == true )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Huntress</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 49 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Stealing.Base >= 60.0 && c.Skills.Poisoning.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Troublesome</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 50 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Healing.Base >= 60.0 && c.Skills.Veterinary.Base >= 60.0 && c.Skills.SpiritSpeak.Base >= 60.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Acolyte</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 51 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( ( c.Skills.Swords.Base >= 90.0 || c.Skills.Macing.Base >= 90.0  || c.Skills.Fencing.Base >= 90.0  || c.Skills.Archery.Base >= 90.0 ) && c.Skills.Tactics.Base >= 120.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Wingleader</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 52 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Str >= 110 && c.Int >= 70 && c.Dex >= 90 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Mama Jama</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 53 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 40 && c.Skills.Stealing.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Rum Thief</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 54 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 80 && c.Skills.Stealing.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Red-Hand</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 55 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Dex >= 90 && c.Skills.Stealing.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Heretic</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 56 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 90.0 && c.Skills.Stealing.Base >= 90.0 && c.Skills.Hiding.Base >= 90.0 && c.Int >= 80 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Sly</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 57 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Parry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Str >= 120 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Glorious</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 58 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Hiding.Base >= 70.0 && c.Dex >= 90 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Stalker</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 59 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Archery.Base >= 120.0 && c.Dex >= 125 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Deadeye</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 5, 60 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				// Changed Navigation To Lead To Page Four 1.9.7
				//Navigation
				if( ( c.Controlled && from == c.ControlMaster ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					AddHtml( 353, 20, 160, 18, "<basefont color = #FFD57A>Page Four</basefont>", false, false );
					AddButton( 417, 22, 5601, 5605, GetButtonID( 1, 4 ), GumpButtonType.Reply, 0 );
				}
			}
			// Added 1.9.7
			if ( page == SquireTitlePage.PageFour )
			{
				//Middle Display
				AddHtml( 145, 24, 160, 18, "<basefont color = #FFD57A><center>Titles Page: Four</center></basefont>", false, false );
				
				AddHtml( 49, 20, 160, 18, "<basefont color = #FFD57A>None</basefont>", false, false );
				AddButton( 20, 14, 0x9A8, 0x9AA, GetButtonID( 3, 1 ), GumpButtonType.Reply, 0 );
				
				//First Third
				int Sideways = 20;
				int Downwards = 40;
				
				if( c.Skills.Chivalry.Base > 30.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Paladin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 1 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.Swords.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Sword</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 2 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.Macing.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Hammer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 3 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.Fencing.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Striker</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 4 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.SpiritSpeak.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Channel</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 5 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.Hiding.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Shade</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 6 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.Healing.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 7 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base > 75.0 && c.Skills.Parry.Base > 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Shield</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 8 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TithingPoints > 0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Slightly Devoted</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 9 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TithingPoints > 1000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Kinda Devoted</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 10 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TithingPoints > 10000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Devoted</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 11 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TithingPoints > 50000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Very Devoted</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 12 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.TithingPoints > 100000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Extremely Devoted</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 13 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>True Paladin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 14 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Poisoning.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Toxin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 15 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 75.0 && c.Skills.Archery.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Shot</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 16 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Archery.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Blessed Ranger</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 17 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 75.0 && c.Skills.Veterinary.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Caring Light</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 18 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 75.0 && c.Skills.Veterinary.Base >= 75.0 && c.Skills.Healing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Light</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 19 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 75.0 && c.Skills.Musicianship.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Song</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 20 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Second Third
				Sideways = 160;
				Downwards = 40;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Str >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Strong Paladin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 21 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Int >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Smart Paladin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 22 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Dex >= 100 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Quick Paladin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 23 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Worthy</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 24 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Despicable</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 25 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 && c.Skills.Chivalry.Base >= 75.0 && c.Skills.Hiding.Base >= 75.0 && c.Skills.Stealing.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Shining Shadow</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 26 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Poisoning.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Foetid</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 27 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && ( c.Skills.Swords.Base >= 100.0 || c.Skills.Macing.Base >= 100.0 || c.Skills.Fencing.Base >= 100.0 ) )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Warrior</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 28 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Vixen</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 29 ), GumpButtonType.Reply, 0 );
				}
				else if( !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Fox</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 29 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Focus.Base >= 100.0 && c.Skills.Chivalry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Focus</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 30 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Tactics.Base >= 100.0 && c.Skills.Chivalry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Tactician</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 31 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Stealing.Base >= 100.0 && c.Skills.Chivalry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Thief</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 32 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base == 0.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Faithless</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 33 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 25.0 && c.Skills.Healing.Base >= 25.0 && c.Skills.Veterinary.Base >= 25.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Humble</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 34 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Female && c.Skills.Chivalry.Base >= 50.0 && c.Skills.Healing.Base >= 50.0 && c.Skills.Veterinary.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Nun</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 35 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Skills.Chivalry.Base >= 50.0 && c.Skills.Healing.Base >= 50.0 && c.Skills.Veterinary.Base >= 50.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Priest</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 35 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 75.0 && c.Skills.Healing.Base >= 75.0 && c.Skills.Veterinary.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>True Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 36 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Healing.Base >= 100.0 && c.Skills.Veterinary.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>True Holy Healer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 37 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 100.0 && c.Skills.Stealing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Ruffian</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 38 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Healing.Base >= 100.0 && c.Skills.Stealing.Base == 0.0 && c.Skills.Poisoning.Base == 0.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Pure</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 39 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 80.0 && c.Skills.Parry.Base >= 80.0 && ( c.Skills.Swords.Base >= 80.0 || c.Skills.Macing.Base >= 80.0 ) )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Templar</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 40 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Third Third
				Sideways = 300;
				Downwards = 40;
				
				if( c.Skills.Provocation.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Invoker</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 41 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Peacemaking.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Peacemaker</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 42 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Peacemaking.Base >= 100.0 && c.Skills.Healing.Base >= 100.0 && c.Skills.Veterinary.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>True Hope</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 43 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 70.0 && c.Skills.Archery.Base >= 70.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Adventurous</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 44 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Swords.Base >= 70.0 && c.Skills.Archery.Base >= 70.0 && c.Skills.Macing.Base >= 70.0 && c.Skills.Fencing.Base >= 70.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Fighter</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 45 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.SkillsTotal >= 6000 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Skilled</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 46 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Race == Race.Human )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Human</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 47 ), GumpButtonType.Reply, 0 );
				}
				else if( c.Race == Race.Elf )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Elf</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 47 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Girl</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 48 ), GumpButtonType.Reply, 0 );
				}
				else if( !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Boy</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 48 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Woman</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 49 ), GumpButtonType.Reply, 0 );
				}
				else if( !c.Female )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Man</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 49 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Female && c.Skills.Chivalry.Base >= 110.0 && c.Skills.Swords.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Heroine</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 50 ), GumpButtonType.Reply, 0 );
				}
				else if( !c.Female && c.Skills.Chivalry.Base >= 110.0 && c.Skills.Swords.Base >= 110.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Hero</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 50 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 90.0 && c.Skills.Poisoning.Base >= 90.0 && c.Skills.Swords.Base >= 90.0 && c.Skills.Archery.Base >= 90.0 && c.Skills.Macing.Base >= 90.0 && c.Skills.Fencing.Base >= 90.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Tempest</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 51 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 && c.Skills.Archery.Base >= 100.0 && c.Skills.Macing.Base >= 100.0 && c.Skills.Fencing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Uber</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 52 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 80.0 && c.Skills.Parry.Base >= 80.0 && c.Skills.MagicResist.Base >= 80.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Tank</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 53 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 75.0 && c.Skills.MagicResist.Base >= 75.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Holy Bubble</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 54 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Panzer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 55 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 && c.Skills.Swords.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Great Paladin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 56 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 && c.Skills.Macing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Great Hammerdin</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 57 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 && c.Skills.Wrestling.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Great Defender</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 58 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Skills.Parry.Base >= 100.0 && c.Skills.Fencing.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Great Piercer</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 59 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				if( c.Skills.Chivalry.Base >= 100.0 && c.Skills.MagicResist.Base >= 100.0 && c.Skills.Archery.Base >= 100.0 )
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #FFD57A>Great Ranger</basefont>", false, false );
					AddButton( Sideways, Downwards + 2, 0x845, 0x846, GetButtonID( 6, 60 ), GumpButtonType.Reply, 0 );
				}
				else
				{
					AddHtml( Sideways + 18, Downwards, 160, 18, "<basefont color = #808080>???</basefont>", false, false );
				}
				Downwards = Downwards + 18;
				
				//Navigation
				if( ( c.Controlled && from == c.ControlMaster ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					AddHtml( 353, 20, 160, 18, "<basefont color = #FFD57A>Page One</basefont>", false, false );
					AddButton( 417, 22, 5601, 5605, GetButtonID( 1, 1 ), GumpButtonType.Reply, 0 );
				}
			}
		}
	}
}