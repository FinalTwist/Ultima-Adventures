using System;
using Server;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Prompts;
using Server.Gumps;

namespace Server.Gumps 
{
    public class SkillListingGump : Gump
    {
		public static void RefreshSkillList( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				if( from.HasGump( typeof(SkillListingGump)) )
				{
					from.CloseGump( typeof(SkillListingGump) );
					from.SendGump( new SkillListingGump( from ) );
				}
			}
		}

		public static void OpenSkillList( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				from.CloseGump( typeof(SkillListingGump) );
				from.SendGump( new SkillListingGump( from ) );
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "skilllist", AccessLevel.Player, new CommandEventHandler( NewSkillsGump_OnCommand ) );
		}

		[Usage( "skilllist" )]
		[Description( "Shows the player the skills they want to watch." )]
		public static void NewSkillsGump_OnCommand( CommandEventArgs e )
		{
			OpenSkillList( e.Mobile );
		}

        public SkillListingGump ( Mobile from ) : base ( 25,25 )
        {
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
			int SkillDisplay = DB.SkillDisplay;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			int r = 0;
			Skill fromSkill = from.Skills[SkillName.Alchemy];

			fromSkill = from.Skills[SkillName.Alchemy]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Anatomy]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.AnimalLore]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.AnimalTaming]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Archery]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.ArmsLore]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Begging]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Blacksmith]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Bushido]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Camping]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Carpentry]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Cartography]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Chivalry]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Cooking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.DetectHidden]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Discordance]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.EvalInt]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Fencing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Fishing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Fletching]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Focus]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Forensics]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Healing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Herding]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Hiding]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Inscribe]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.ItemID]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Lockpicking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Lumberjacking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Macing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Magery]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.MagicResist]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Meditation]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Mining]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Musicianship]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Necromancy]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Ninjitsu]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Parry]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Peacemaking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Poisoning]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Provocation]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.RemoveTrap]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Snooping]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.SpiritSpeak]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Stealing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Stealth]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Swords]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Tactics]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Tailoring]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.TasteID]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Tinkering]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Tracking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Veterinary]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }
			fromSkill = from.Skills[SkillName.Wrestling]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ r++; }

			AddPage(0);

			AddImage(0, 0, 164);
			AddImage(86, 0, 164);

			if ( r > 6 )
			{
				r = r - 6;
				int g = r;
				int u = 0;
				int o = 2;

				while ( r > 0 )
				{
					u=u+24;
					AddImage(0, u, 164);
					AddImage(86, u, 164);
					r--;
				}

				while ( g > 0 )
				{
					o=o+24;
					AddImage(2, o, 165);
					AddImage(88, o, 165);
					g--;
				}
			}

			AddImage(2, 2, 165);
			AddImage(88, 2, 165);

			int color = 1153;
			int y = 8;

			fromSkill = from.Skills[SkillName.Alchemy]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Alchemy"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Anatomy]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Anatomy"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.AnimalLore]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Animal Lore"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.AnimalTaming]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Animal Taming"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Archery]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Archery"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.ArmsLore]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Arms Lore"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Begging]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Begging"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Blacksmith]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Blacksmithing"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Bushido]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Bushido"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Camping]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Camping"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Carpentry]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Carpentry"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Cartography]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Cartography"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Chivalry]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Chivalry"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Cooking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Cooking"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.DetectHidden]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Detect Hidden"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Discordance]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Discordance"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.EvalInt]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Eval Intelligence"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Fencing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Fencing"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Fishing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Fishing"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Fletching]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Fletching"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Focus]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Focus"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Forensics]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Forensics"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Healing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Healing"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Herding]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Herding"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Hiding]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Hiding"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Inscribe]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Inscription"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.ItemID]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Item ID"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Lockpicking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Lockpicking"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Lumberjacking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Lumberjacking"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Macing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Mace Fighting"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Magery]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Magery"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.MagicResist]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Magic Resist"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Meditation]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Meditation"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Mining]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Mining"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Musicianship]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Musicianship"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Necromancy]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Necromancy"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Ninjitsu]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Ninjitsu"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Parry]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Parrying"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Peacemaking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Peacemaking"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Poisoning]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Poisoning"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Provocation]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Provocation"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.RemoveTrap]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Remove Trap"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Snooping]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Snooping"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.SpiritSpeak]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Spirit Speak"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Stealing]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Stealing"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Stealth]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Stealth"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Swords]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Swordsmanship"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Tactics]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Tactics"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Tailoring]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Tailoring"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.TasteID]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Taste ID"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Tinkering]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Tinkering"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Tracking]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Tracking"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Veterinary]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Veterinary"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
			fromSkill = from.Skills[SkillName.Wrestling]; if ( fromSkill.Lock == SkillLock.Up || ( fromSkill.Lock == SkillLock.Locked && SkillDisplay > 0 ) ){ if ( fromSkill.Lock == SkillLock.Locked ){ color = 0x31; } AddLabel(8, y, color, @"Wrestling"); AddLabel(140, y, color, @"" + fromSkill.Base + ""); AddLabel(198, y, color, @"" + fromSkill.Value + ""); color = 1153; y=y+24; }
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			from.CloseGump( typeof( SkillListingGump ) );
		}
    }
}