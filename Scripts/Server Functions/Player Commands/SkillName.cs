using System;
using Server;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;

namespace Server.Items
{
	public class SkillTitle
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SkillName", AccessLevel.Player, new CommandEventHandler( SkillName_OnCommand ) );
		}

		[Usage( "SkillName <name>" )]
		[Description( "Sets the character's advertised skill title...no matter the proficiency." )]
		public static void SkillName_OnCommand( CommandEventArgs e )
		{
            Mobile m = e.Mobile;

			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if ( e.Length >= 1 )
			{
				int success = 0;
				if ( e.Arguments[0] == "alchemy" ){ success=1; DB.CharacterSkill = 1; }
				else if ( e.Arguments[0] == "anatomy" ){ success=1; DB.CharacterSkill = 2; }
				else if ( e.Arguments[0] == "animal lore" ){ success=1; DB.CharacterSkill = 3; }
				else if ( e.Arguments[0] == "animal taming" ){ success=1; DB.CharacterSkill = 4; }
				else if ( e.Arguments[0] == "archery" ){ success=1; DB.CharacterSkill = 5; }
				else if ( e.Arguments[0] == "arms lore" ){ success=1; DB.CharacterSkill = 6; }
				else if ( e.Arguments[0] == "begging" ){ success=1; DB.CharacterSkill = 7; }
				else if ( e.Arguments[0] == "blacksmithing" ){ success=1; DB.CharacterSkill = 8; }
				else if ( e.Arguments[0] == "bushido" ){ success=1; DB.CharacterSkill = 9; }
				else if ( e.Arguments[0] == "camping" ){ success=1; DB.CharacterSkill = 10; }
				else if ( e.Arguments[0] == "carpentry" ){ success=1; DB.CharacterSkill = 11; }
				else if ( e.Arguments[0] == "cartography" ){ success=1; DB.CharacterSkill = 12; }
				else if ( e.Arguments[0] == "chivalry" ){ success=1; DB.CharacterSkill = 13; }
				else if ( e.Arguments[0] == "cooking" ){ success=1; DB.CharacterSkill = 14; }
				else if ( e.Arguments[0] == "detect hidden" ){ success=1; DB.CharacterSkill = 15; }
				else if ( e.Arguments[0] == "discordance" ){ success=1; DB.CharacterSkill = 16; }
				else if ( e.Arguments[0] == "evaluating intelligence" ){ success=1; DB.CharacterSkill = 17; }
				else if ( e.Arguments[0] == "fencing" ){ success=1; DB.CharacterSkill = 18; }
				else if ( e.Arguments[0] == "fishing" ){ success=1; DB.CharacterSkill = 19; }
				else if ( e.Arguments[0] == "bowcraft/fletching" ){ success=1; DB.CharacterSkill = 20; }
				else if ( e.Arguments[0] == "bowcraft" ){ success=1; DB.CharacterSkill = 20; }
				else if ( e.Arguments[0] == "fletching" ){ success=1; DB.CharacterSkill = 20; }
				else if ( e.Arguments[0] == "focus" ){ success=1; DB.CharacterSkill = 21; }
				else if ( e.Arguments[0] == "forensic evaluation" ){ success=1; DB.CharacterSkill = 22; }
				else if ( e.Arguments[0] == "healing" ){ success=1; DB.CharacterSkill = 23; }
				else if ( e.Arguments[0] == "herding" ){ success=1; DB.CharacterSkill = 24; }
				else if ( e.Arguments[0] == "hiding" ){ success=1; DB.CharacterSkill = 25; }
				else if ( e.Arguments[0] == "inscription" ){ success=1; DB.CharacterSkill = 26; }
				else if ( e.Arguments[0] == "item identification" ){ success=1; DB.CharacterSkill = 27; }
				else if ( e.Arguments[0] == "lockpicking" ){ success=1; DB.CharacterSkill = 28; }
				else if ( e.Arguments[0] == "lumberjacking" ){ success=1; DB.CharacterSkill = 29; }
				else if ( e.Arguments[0] == "mace fighting" ){ success=1; DB.CharacterSkill = 30; }
				else if ( e.Arguments[0] == "magery" ){ success=1; DB.CharacterSkill = 31; }
				else if ( e.Arguments[0] == "resisting spells" ){ success=1; DB.CharacterSkill = 32; }
				else if ( e.Arguments[0] == "meditation" ){ success=1; DB.CharacterSkill = 33; }
				else if ( e.Arguments[0] == "mining" ){ success=1; DB.CharacterSkill = 34; }
				else if ( e.Arguments[0] == "musicianship" ){ success=1; DB.CharacterSkill = 35; }
				else if ( e.Arguments[0] == "necromancy" ){ success=1; DB.CharacterSkill = 36; }
				else if ( e.Arguments[0] == "ninjitsu" ){ success=1; DB.CharacterSkill = 37; }
				else if ( e.Arguments[0] == "parrying" ){ success=1; DB.CharacterSkill = 38; }
				else if ( e.Arguments[0] == "peacemaking" ){ success=1; DB.CharacterSkill = 39; }
				else if ( e.Arguments[0] == "poisoning" ){ success=1; DB.CharacterSkill = 40; }
				else if ( e.Arguments[0] == "provocation" ){ success=1; DB.CharacterSkill = 41; }
				else if ( e.Arguments[0] == "remove trap" ){ success=1; DB.CharacterSkill = 42; }
				else if ( e.Arguments[0] == "snooping" ){ success=1; DB.CharacterSkill = 43; }
				else if ( e.Arguments[0] == "spirit speak" ){ success=1; DB.CharacterSkill = 44; }
				else if ( e.Arguments[0] == "stealing" ){ success=1; DB.CharacterSkill = 45; }
				else if ( e.Arguments[0] == "stealth" ){ success=1; DB.CharacterSkill = 46; }
				else if ( e.Arguments[0] == "swordsmanship" ){ success=1; DB.CharacterSkill = 47; }
				else if ( e.Arguments[0] == "tactics" ){ success=1; DB.CharacterSkill = 48; }
				else if ( e.Arguments[0] == "tailoring" ){ success=1; DB.CharacterSkill = 49; }
				else if ( e.Arguments[0] == "taste identification" ){ success=1; DB.CharacterSkill = 50; }
				else if ( e.Arguments[0] == "tinkering" ){ success=1; DB.CharacterSkill = 51; }
				else if ( e.Arguments[0] == "tracking" ){ success=1; DB.CharacterSkill = 52; }
				else if ( e.Arguments[0] == "veterinary" ){ success=1; DB.CharacterSkill = 53; }
				else if ( e.Arguments[0] == "wrestling" ){ success=1; DB.CharacterSkill = 54; }
				else if ( e.Arguments[0] == "clear" ){ success=1; DB.CharacterSkill = 0; }

				if ( success == 1 )
				{
					m.InvalidateProperties();
					m.SendMessage( "Your skill title has been changed." );
				}
				else
				{
					m.SendMessage( "That is not a valid skill!" );
				}
			}
			else
			{
				m.SendMessage( "Format: SkillName followed by the name of the skill in quotes" );
			}
		}
	}
}