using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Gumps 
{
    public class StatsGump : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "status", AccessLevel.Player, new CommandEventHandler( MyStats_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "status" )]
		[Description( "Opens Stats Gump." )]
		public static void MyStats_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if (AdventuresFunctions.IsPuritain((object)from))
				return;

			from.CloseGump( typeof( StatsGump ) );
			from.SendGump( new StatsGump( from, 0 ) );
        }
   
        public StatsGump ( Mobile from, int origin ) : base ( 25,25 )
        {
			m_Origin = origin;

            int LRCCap = MyServerSettings.LowerReagentCostCap();
            int LMCCap = MyServerSettings.LowerManaCostCap();
            double BandageSpeedCap = MyServerSettings.BandageSpeedMin();
			double BandageSpeeds = 4 * (1- ((double)from.Dex/300));// ( 300 - dex ) /60;
			if (BandageSpeeds < 0)
				BandageSpeeds = 0;

            int SwingSpeedCap = MyServerSettings.SwingSpeedCap();
            int HCICap = MyServerSettings.HitChanceCap();
            int DCICap = MyServerSettings.DefendChanceCap();
            int FCCap = MyServerSettings.CastSpeedCap(); // FC 4 For Paladin, otherwise FC 2 for Mage
            int FCRCap = MyServerSettings.CastRecoveryCap();
            int DamageIncreaseCap = MyServerSettings.DamageIncreaseCap();
            int SDICap = MyServerSettings.RealSpellDamageCap(); 
            int ReflectDamageCap = MyServerSettings.ReflectDamageCap();
            int SSICap = MyServerSettings.WeaponSpeedCap();
			int HPRCap = MyServerSettings.RegenHitsCap();
			int STRCap = MyServerSettings.RegenStamCap();
			int MARCap = MyServerSettings.RegenManaCap();
            
            int LRC = AosAttributes.GetValue( from, AosAttribute.LowerRegCost ) > LRCCap ? LRCCap : AosAttributes.GetValue( from, AosAttribute.LowerRegCost );
            int LMC = AosAttributes.GetValue( from, AosAttribute.LowerManaCost ) > LMCCap ? LMCCap : AosAttributes.GetValue( from, AosAttribute.LowerManaCost );
            double BandageSpeed = ( BandageSpeedCap + BandageSpeeds ) < BandageSpeedCap ? BandageSpeedCap : ( BandageSpeedCap + BandageSpeeds );
            TimeSpan SwingSpeed = (from.Weapon as BaseWeapon).GetDelay(from) > TimeSpan.FromSeconds(SwingSpeedCap) ? TimeSpan.FromSeconds(SwingSpeedCap) : (from.Weapon as BaseWeapon).GetDelay(from);
            int HCI = AosAttributes.GetValue( from, AosAttribute.AttackChance ) > HCICap ? HCICap : AosAttributes.GetValue( from, AosAttribute.AttackChance );
            int DCI = AosAttributes.GetValue( from, AosAttribute.DefendChance ) > DCICap ? DCICap : AosAttributes.GetValue( from, AosAttribute.DefendChance );
            int FC = AosAttributes.GetValue( from, AosAttribute.CastSpeed ) > FCCap ? FCCap : AosAttributes.GetValue( from, AosAttribute.CastSpeed );
            int FCR = AosAttributes.GetValue( from, AosAttribute.CastRecovery ) > FCRCap ? FCRCap : AosAttributes.GetValue( from, AosAttribute.CastRecovery );
            int DamageIncrease = AosAttributes.GetValue( from, AosAttribute.WeaponDamage ) > DamageIncreaseCap ? DamageIncreaseCap : AosAttributes.GetValue( from, AosAttribute.WeaponDamage );
            int SDI = AosAttributes.GetValue( from, AosAttribute.SpellDamage ) > SDICap ? SDICap : AosAttributes.GetValue( from, AosAttribute.SpellDamage );
            int ReflectDamage = AosAttributes.GetValue( from, AosAttribute.ReflectPhysical ) > ReflectDamageCap ? ReflectDamageCap : AosAttributes.GetValue( from, AosAttribute.ReflectPhysical );
            int SSI = AosAttributes.GetValue( from, AosAttribute.WeaponSpeed ) > SSICap ? SSICap : AosAttributes.GetValue( from, AosAttribute.WeaponSpeed );
            int HealCost = GetPlayerInfo.GetResurrectCost( from );
			int CharacterLevel = GetPlayerInfo.GetPlayerLevel( from );
            int EP = BasePotion.EnhancePotions( from );
			int HPR = AosAttributes.GetValue( from, AosAttribute.RegenHits ) > HPRCap ? HPRCap : AosAttributes.GetValue( from, AosAttribute.RegenHits );
			int STAR = AosAttributes.GetValue( from, AosAttribute.RegenStam ) > STRCap ? STRCap : AosAttributes.GetValue( from, AosAttribute.RegenStam );
			int MANR = AosAttributes.GetValue( from, AosAttribute.RegenMana ) > MARCap ? MARCap : AosAttributes.GetValue( from, AosAttribute.RegenMana );

			int BonHP = Server.Misc.AdventuresFunctions.DiminishingReturns( AosAttributes.GetValue( from, AosAttribute.BonusHits ), MyServerSettings.StatBonusCap());
			int BonMN = Server.Misc.AdventuresFunctions.DiminishingReturns( AosAttributes.GetValue( from, AosAttribute.BonusMana ), MyServerSettings.StatBonusCap());
			int BonST = Server.Misc.AdventuresFunctions.DiminishingReturns( AosAttributes.GetValue( from, AosAttribute.BonusStam ), MyServerSettings.StatBonusCap());

			AddPage(0);
			AddImage(300, 300, 155);
			AddImage(0, 300, 155);
			AddImage(0, 0, 155);
			AddImage(300, 0, 155);
			AddImage(600, 0, 155);
			AddImage(600, 300, 155);
			AddImage(794, 0, 155);
			AddImage(794, 300, 155);

			AddImage(2, 2, 129);
			AddImage(302, 2, 129);
			AddImage(2, 298, 129);
			AddImage(598, 2, 129);
			AddImage(598, 298, 129);
			AddImage(301, 298, 129);
			AddImage(792, 2, 129);
			AddImage(792, 298, 129);

			AddImage(5, 8, 145);
			AddImage(7, 354, 142);
			AddImage(324, 563, 140);
			AddImage(433, 29, 140);
			AddImage(175, 29, 140);
			AddImage(165, 7, 156);
			AddImage(191, 7, 156);
			AddImage(219, 5, 156);
			AddImage(698, 7, 156);
			AddImage(519, 563, 140);
			AddImage(748, 381, 144);
			AddImage(893, 7, 146);
			AddImage(630, 29, 140);
			AddImage(893, 6, 156);

			string title = "CHARACTER SHEET";
			if ( m_Origin > 0 ){ title = "PLAYERS HANDBOOK"; }

			AddHtml( 166, 60, 203, 29, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + title + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 472, 60, 432, 29, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + from.Name + " the " + GetPlayerInfo.GetSkillTitle( from ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(151, 113, 3823);
			AddHtml( 190, 116, 57, 19, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 116, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + Banker.GetBalance( from ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 96, 156, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Strength</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 196, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Dexterity</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 236, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Intelligence</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 276, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Fame</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 316, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Karma</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 356, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Tithing Points</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 396, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hunger</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 436, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Thirst</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 96, 476, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Enhance Potions</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 156, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.RawStr, from.Str - from.RawStr ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 196, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.RawDex, from.Dex - from.RawDex ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 236, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.RawInt, from.Int - from.RawInt ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 276, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Fame ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 316, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Karma ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 356, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.TithingPoints ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 396, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Hunger ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 436, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Thirst ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 236, 476, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", EP ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 406, 116, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Balance Influence</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 156, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hits</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 196, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Stamina</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 236, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Mana</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 276, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hits Regen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 316, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Stamina Regen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 356, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Mana Regen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 396, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Low Reagent</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 436, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Low Mana</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 476, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Resurrect Cost</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 406, 516, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Murders</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			if (((PlayerMobile)from).SoulBound) {
				AddHtml( 406, 550, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Soul Force</BIG></BASEFONT></BODY>", (bool)false, (bool)false);	
			}
			AddHtml( 546, 116, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", Math.Abs(((PlayerMobile)from).BalanceEffect) ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 156, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.Hits - BonHP, BonHP ) + "/" + MyServerSettings.StatBonusCap() + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 196, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.Stam - BonST, BonST ) + "/" + MyServerSettings.StatBonusCap() + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 236, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} + {1}", from.Mana - BonMN, BonMN ) + "/" + MyServerSettings.StatBonusCap() + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 276, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", HPR + "/" + HPRCap) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 316, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", STAR + "/" + STRCap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 356, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", MANR + "/" + MARCap) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 396, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", LRC + "/" + LRCCap) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 436, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", LMC + "/" + LMCCap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 476, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0} Gold", HealCost ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 546, 516, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", from.Kills) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			if (((PlayerMobile)from).SoulBound) {
				AddHtml( 546, 550, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", ((PlayerMobile)from).SoulForce ) + "/" + MyServerSettings.SoulForceCap() + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			AddHtml( 723, 116, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Hit Chance</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 156, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Defend Chance</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 196, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Swing Speed</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 236, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Swing Speed +</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 276, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Bandage Speed</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 316, 169, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Damage Increase</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 356, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Reflect Damage</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 396, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Fast Cast</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 436, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Cast Recovery</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 723, 476, 126, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Spell Damage +</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 863, 116, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", HCI + "/" + HCICap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 156, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", DCI + "/" + DCICap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 196, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}s", new DateTime(SwingSpeed.Ticks).ToString("s.ff") ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 236, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", SSI + "/" + SSICap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 276, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0:0.0}s", new DateTime(TimeSpan.FromSeconds( BandageSpeed ).Ticks).ToString("s.ff") + "/" + new DateTime(TimeSpan.FromSeconds( BandageSpeedCap ).Ticks).ToString("s.ff") ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 316, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", DamageIncrease + "/" + DamageIncreaseCap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 356, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", ReflectDamage + "/" + ReflectDamageCap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 396, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", FC + "/" + FCCap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 436, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}", FCR + "/" + FCRCap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 863, 476, 126, 24, @"<BODY><BASEFONT Color=#FCFF00><BIG><DIV ALIGN=RIGHT>" + String.Format(" {0}%", SDI + "/" + SDICap ) + "</DIV></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			if ( m_Origin != 1 )
			{
				AddButton(1015, 233, 236, 236, 1, GumpButtonType.Reply, 0);
				AddHtml( 1007, 307, 71, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>Refresh</CENTER></BIG></BASEFONT></BODY></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
			else
			{
				AddImage(1000, 240, 11424);
			}
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			switch ( info.ButtonID )
			{
				case 1:
				{
					from.SendSound( 0x4A );
					from.CloseGump( typeof( StatsGump ) );
					from.SendGump( new StatsGump( from, m_Origin ) );
					break;
				}
			}
		}
    }
}