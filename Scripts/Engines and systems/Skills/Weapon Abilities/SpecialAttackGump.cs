using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;

namespace Server.Gumps
{
	public class SpecialAttackGump : Gump
	{
		private BaseWeapon m_weapon;
		
		public int Abilities;
		
		private int Primary;
		private int Secondary;
		private int Third;
		private int Fourth;
		private int Fifth;

		private int PrimaryIcon {get{return GetAbilInfo( Primary );}}
		private int SecondaryIcon {get{return GetAbilInfo( Secondary );}}
		private int ThirdIcon {get{return GetAbilInfo( Third );}}
		private int FourthIcon {get{return GetAbilInfo( Fourth );}}
		private int FifthIcon {get{return GetAbilInfo( Fifth );}}

		private int PrimaryState = 9781;
		private int SecondaryState = 9781;
		private int ThirdState = 9781;
		private int FourthState = 9781;
		private int FifthState = 9781;
		
		public SpecialAttackGump(BaseWeapon weapon, Mobile from,int abilities): base( 0, 0 )
		{
			m_weapon = weapon;
			Abilities = abilities;
			InitializeGump( from );
		}

		public SpecialAttackGump(BaseWeapon weapon, Mobile from, int abilities, int primary, int secondary): this( weapon, from, abilities, primary, secondary, 9781, 9781, 9781) {}
		public SpecialAttackGump(BaseWeapon weapon, Mobile from, int abilities, int primary, int secondary, int third, int fourth, int fifth): base( 0, 0 )
		{
			m_weapon = weapon;
			Abilities = abilities;
			PrimaryState = primary;
			SecondaryState = secondary;
			ThirdState = third;
			FourthState = fourth;
			FifthState = fifth;
			InitializeGump( from );
		}

		private void InitializeGump( Mobile from )
		{
			Closable=false;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			int nDown = -60;
			int nRight = 5;
			int nPos = 10;
	
			AddButton((nPos+34)+nRight, 90+nDown, 0x082D, 0x082D, 0, GumpButtonType.Reply, 0);
			
			CustomWeaponAbilities.SetAbilities(m_weapon, ref Primary, ref Secondary, ref Third, ref Fourth, ref Fifth);

			AddImage((nPos+34)+nRight, 109+nDown, 0x5DD0);
			AddButton((nPos+34)+nRight, 109+nDown, PrimaryIcon, PrimaryIcon, 1, GumpButtonType.Reply, 0);
				if ( Server.Misc.GetPlayerInfo.isJester( from ) ){ AddImage((nPos+34)+nRight, 109+nDown, PrimaryIcon, 32); }
				else if ( Server.Misc.GetPlayerInfo.isSyth( from, false ) ){ AddImage((nPos+34)+nRight, 109+nDown, PrimaryIcon, 0x22); }
				else if ( Server.Misc.GetPlayerInfo.isJedi( from, false ) ){ AddImage((nPos+34)+nRight, 109+nDown, SecondaryIcon, 2825 ); }
			AddImage(nPos+nRight, 109+nDown, PrimaryState);
			if ( AbilityNamesWeapon.From( from ) ){ AddLabel((nPos+86)+nRight, 120+nDown, 0x481, @"" + GetAbilName( Primary ) + ""); }

			if(Abilities >= 2)
			{
				AddImage((nPos+34)+nRight, 150+nDown, 0x5DD0);
				AddButton((nPos+34)+nRight, 150+nDown, SecondaryIcon, SecondaryIcon, 2, GumpButtonType.Reply, 0);
					if ( Server.Misc.GetPlayerInfo.isJester( from ) ){ AddImage((nPos+34)+nRight, 150+nDown, SecondaryIcon, 69); }
					else if ( Server.Misc.GetPlayerInfo.isSyth( from, false ) ){ AddImage((nPos+34)+nRight, 150+nDown, SecondaryIcon, 0x22); }
					else if ( Server.Misc.GetPlayerInfo.isJedi( from, false ) ){ AddImage((nPos+34)+nRight, 109+nDown, SecondaryIcon, 2825 ); }
				AddImage(nPos+nRight, 150+nDown, SecondaryState);
				if ( AbilityNamesWeapon.From( from ) ){ AddLabel((nPos+86)+nRight, 161+nDown, 0x481, @"" + GetAbilName( Secondary ) + ""); }
			}
			if(Abilities >= 3)
			{
				AddImage((nPos+34)+nRight, 191+nDown, 0x5DD0);
				AddButton((nPos+34)+nRight, 191+nDown, ThirdIcon, ThirdIcon, 3, GumpButtonType.Reply, 0);
					if ( Server.Misc.GetPlayerInfo.isJester( from ) ){ AddImage((nPos+34)+nRight, 191+nDown, ThirdIcon, 93); }
					else if ( Server.Misc.GetPlayerInfo.isSyth( from, false ) ){ AddImage((nPos+34)+nRight, 191+nDown, ThirdIcon, 0x22); }
					else if ( Server.Misc.GetPlayerInfo.isJedi( from, false ) ){ AddImage((nPos+34)+nRight, 109+nDown, PrimaryIcon, 2825 ); }
				AddImage(nPos+nRight, 191+nDown, ThirdState);
				if ( AbilityNamesWeapon.From( from ) ){ AddLabel((nPos+86)+nRight, 202+nDown, 0x481, @"" + GetAbilName( Third ) + ""); }
			}
			if(Abilities >= 4)
			{
				AddImage((nPos+34)+nRight, 232+nDown, 0x5DD0);
				AddButton((nPos+34)+nRight, 232+nDown, FourthIcon, FourthIcon, 4, GumpButtonType.Reply, 0);
					if ( Server.Misc.GetPlayerInfo.isJester( from ) ){ AddImage((nPos+34)+nRight, 232+nDown, FourthIcon, 114); }
					else if ( Server.Misc.GetPlayerInfo.isSyth( from, false ) ){ AddImage((nPos+34)+nRight, 232+nDown, FourthIcon, 0x22); }
					else if ( Server.Misc.GetPlayerInfo.isJedi( from, false ) ){ AddImage((nPos+34)+nRight, 109+nDown, SecondaryIcon, 2825 ); }
				AddImage(nPos+nRight, 232+nDown, FourthState);
				if ( AbilityNamesWeapon.From( from ) ){ AddLabel((nPos+86)+nRight, 243+nDown, 0x481, @"" + GetAbilName( Fourth ) + ""); }
			}
			if(Abilities >= 5)
			{
				AddImage((nPos+34)+nRight, 273+nDown, 0x5DD0);
				AddButton((nPos+34)+nRight, 273+nDown, FifthIcon, FifthIcon, 5, GumpButtonType.Reply, 0);
					if ( Server.Misc.GetPlayerInfo.isJester( from ) ){ AddImage((nPos+34)+nRight, 273+nDown, FifthIcon, 253); }
					else if ( Server.Misc.GetPlayerInfo.isSyth( from, false ) ){ AddImage((nPos+34)+nRight, 273+nDown, FifthIcon, 0x22); }
					else if ( Server.Misc.GetPlayerInfo.isJedi( from, false ) ){ AddImage((nPos+34)+nRight, 109+nDown, SecondaryIcon, 2825 ); }
				AddImage(nPos+nRight, 273+nDown, FifthState);
				if ( AbilityNamesWeapon.From( from ) ){ AddLabel((nPos+86)+nRight, 284+nDown, 0x481, @"" + GetAbilName( Fifth ) + ""); }
			}
		}

		public static int GetAbilInfo( int Icon )
		{
			switch( Icon )
			{
				case 1: Icon = 0x2; break;
				case 2: Icon = 0x5; break;
				case 3: Icon = 0x7; break;
				case 4: Icon = 0xA; break;
				case 5: Icon = 0x13; break;
				case 6: Icon = 0x14; break;
				case 7: Icon = 0x17; break;
				case 8: Icon = 0x3F0; break;
				case 9: Icon = 0x3F5; break;
				case 10: Icon = 0x3F6; break;
				case 11: Icon = 0x3F8; break;
				case 12: Icon = 0x3FE; break;
				case 13: Icon = 0x403; break;
				case 14: Icon = 0x3FB; break;
				case 15: Icon = 0x3ED; break;
				case 16: Icon = 0x6; break;
				case 17: Icon = 0x11; break;
				case 18: Icon = 0x3F7; break;
				case 19: Icon = 0x401; break;
				case 20: Icon = 0x30; break;
				case 21: Icon = 0x2C; break;
				case 22: Icon = 0x16; break;
				case 23: Icon = 0x3; break;
				case 24: Icon = 0x4; break;
				case 25: Icon = 0x3EB; break;
				case 26: Icon = 0x3F1; break;
				case 27: Icon = 0x3F9; break;
				case 28: Icon = 0x3FC; break;
				case 29: Icon = 0x3E8; break;
				case 30: Icon = 0x15; break;
				case 31: Icon = 0x1; break;
				case 32: Icon = 0x8; break;
				case 33: Icon = 0x400; break;
				case 34: Icon = 0x2D; break;
				case 35: Icon = 0x3E9; break;
				case 36: Icon = 0x3EC; break;
				case 37: Icon = 0x402; break;
				case 38: Icon = 0x3F2; break;
				case 39: Icon = 0x2E; break;
				case 40: Icon = 0x2B; break;
				case 41: Icon = 0x19; break;
				case 42: Icon = 0x1A; break;
				case 43: Icon = 0x1C; break;
				case 44: Icon = 0x1B; break;
				case 45: Icon = 0x3F3; break;
				case 46: Icon = 0x3EE; break;
				case 47: Icon = 0x3F4; break;
				case 48: Icon = 0x3EF; break;
				case 49: Icon = 0x3FA; break;
				case 50: Icon = 0x3FD; break;
				case 51: Icon = 0x18; break;
				case 52: Icon = 0x3EA; break;
				case 53: Icon = 0x3FF; break;
				case 54: Icon = 0x12; break;
				case 55: Icon = 0xB; break;
			}
			return Icon;
		}

		public static string GetAbilName( int nAbility )
		{
			string Ability = "Armor Ignore";
			switch( nAbility )
			{
				case 1: Ability = "Armor Ignore"; break;
				case 2: Ability = "Bleed Attack"; break;
				case 3: Ability = "Concussion Blow"; break;
				case 4: Ability = "Crushing Blow"; break;
				case 5: Ability = "Disarm"; break;
				case 6: Ability = "Dismount"; break;
				case 7: Ability = "Double Strike"; break;
				case 8: Ability = "Infectious Strike"; break;
				case 9: Ability = "Mortal Strike"; break;
				case 10: Ability = "Moving Shot"; break;
				case 11: Ability = "Paralyzing Blow"; break;
				case 12: Ability = "Shadow Strike"; break;
				case 13: Ability = "Whirlwind Attack"; break;
				case 14: Ability = "Riding Swipe"; break;
				case 15: Ability = "Frenzied Whirlwind"; break;
				case 16: Ability = "Block"; break;
				case 17: Ability = "Defense Mastery"; break;
				case 18: Ability = "Nerve Strike"; break;
				case 19: Ability = "Talon Strike"; break;
				case 20: Ability = "Feint"; break;
				case 21: Ability = "Dual Wield"; break;
				case 22: Ability = "Double Shot"; break;
				case 23: Ability = "Armor Pierce"; break;
				case 24: Ability = "Bladeweave"; break;
				case 25: Ability = "Force Arrow"; break;
				case 26: Ability = "Lightning Arrow"; break;
				case 27: Ability = "Psychic Attack"; break;
				case 28: Ability = "Serpent Arrow"; break;
				case 29: Ability = "Force of Nature"; break;
				case 30: Ability = "Disrobe"; break;
				case 31: Ability = "Achilles Strike"; break;
				case 32: Ability = "Consecrated Strike"; break;
				case 33: Ability = "Stunning Strike"; break;
				case 34: Ability = "Earth Strike"; break;
				case 35: Ability = "Fire Strike"; break;
				case 36: Ability = "Freeze Strike"; break;
				case 37: Ability = "Toxic Strike"; break;
				case 38: Ability = "Lightning Strike"; break;
				case 39: Ability = "Elemental Strike"; break;
				case 40: Ability = "Drain Strength"; break;
				case 41: Ability = "Drain Dexterity"; break;
				case 42: Ability = "Drain Intellect"; break;
				case 43: Ability = "Drain Stamina"; break;
				case 44: Ability = "Drain Mana"; break;
				case 45: Ability = "Magic Protection"; break;
				case 46: Ability = "Greater Magic Protection"; break;
				case 47: Ability = "Melee Protection"; break;
				case 48: Ability = "Greater Melee Protection"; break;
				case 49: Ability = "Riding Attack"; break;
				case 50: Ability = "Shadow Infectious Strike"; break;
				case 51: Ability = "Double Whirlwind Attack"; break;
				case 52: Ability = "Fists of Fury"; break;
				case 53: Ability = "Spin Attack"; break;
				case 54: Ability = "Devastating Blow"; break;
				case 55: Ability = "Death Blow"; break;
			}
			return Ability;
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int idx = info.ButtonID;
			if(idx==0) sender.Mobile.CloseGump(typeof(SpecialAttackGump));
			if(idx==1)
			{
				if(PrimaryState==9781)
				{
					if(CustomWeaponAbilities.ServerSideSetAbility(sender.Mobile,Primary))
					{
						PrimaryState=9780;
						SecondaryState=9781;
						ThirdState=9781;
						FourthState=9781;
						FifthState=9781;
					}
					sender.Mobile.CloseGump(typeof(SpecialAttackGump));
					sender.Mobile.SendGump(new SpecialAttackGump(m_weapon, sender.Mobile, Abilities, PrimaryState, SecondaryState, ThirdState, FourthState, FifthState));
				}
				else
				{
					PrimaryState=9781;
					WeaponAbility.ClearCurrentAbility( sender.Mobile );
				}
			}
			if(idx==2)
			{
				if(SecondaryState==9781)
				{
					if(CustomWeaponAbilities.ServerSideSetAbility(sender.Mobile,Secondary))
					{
						SecondaryState=9780;
						PrimaryState=9781;
						ThirdState=9781;
						FourthState=9781;
						FifthState=9781;
					}
					sender.Mobile.CloseGump(typeof(SpecialAttackGump));
					sender.Mobile.SendGump(new SpecialAttackGump(m_weapon, sender.Mobile, Abilities, PrimaryState, SecondaryState, ThirdState, FourthState, FifthState));
				}
				else
				{
					SecondaryState=9781;
					WeaponAbility.ClearCurrentAbility( sender.Mobile );
				}
			}
			if(idx==3)
			{
				if(ThirdState==9781)
				{
					if(CustomWeaponAbilities.ServerSideSetAbility(sender.Mobile,Third))
					{
						ThirdState=9780;
						SecondaryState=9781;
						PrimaryState=9781;
						FourthState=9781;
						FifthState=9781;
					}
					sender.Mobile.CloseGump(typeof(SpecialAttackGump));
					sender.Mobile.SendGump(new SpecialAttackGump(m_weapon, sender.Mobile, Abilities, PrimaryState, SecondaryState, ThirdState, FourthState, FifthState));
					
				}
				else
				{
					ThirdState=9781;
					WeaponAbility.ClearCurrentAbility( sender.Mobile );
				}
			}
			if(idx==4)
			{
				if(FourthState==9781)
				{
					if(CustomWeaponAbilities.ServerSideSetAbility(sender.Mobile,Fourth))
					{
						FourthState=9780;
						SecondaryState=9781;
						PrimaryState=9781;
						ThirdState=9781;
						FifthState=9781;
					}
					sender.Mobile.CloseGump(typeof(SpecialAttackGump));
					sender.Mobile.SendGump(new SpecialAttackGump(m_weapon, sender.Mobile, Abilities, PrimaryState, SecondaryState, ThirdState, FourthState, FifthState));
				}
				else
				{
					FourthState=9781;
					WeaponAbility.ClearCurrentAbility( sender.Mobile );
				}
			}
			if(idx==5)
			{
				if(FifthState==9781)
				{
					if(CustomWeaponAbilities.ServerSideSetAbility(sender.Mobile,Fifth))
					{
						FifthState=9780;
						SecondaryState=9781;
						PrimaryState=9781;
						FourthState=9781;
						ThirdState=9781;
					}
					sender.Mobile.CloseGump(typeof(SpecialAttackGump));
					sender.Mobile.SendGump(new SpecialAttackGump(m_weapon, sender.Mobile, Abilities, PrimaryState, SecondaryState, ThirdState, FourthState, FifthState));
				}
				else
				{
					FifthState=9781;
					WeaponAbility.ClearCurrentAbility( sender.Mobile );
				}
			}
		}
	}
}

namespace Server.Items
{
    class AbilityNamesWeapon
    {
        public static void Initialize()
        {
            CommandSystem.Register("abilitynames", AccessLevel.Player, new CommandEventHandler(OnToggleAbilityNames));
        }

        [Usage("abilitynames")]
        [Description("Enables or disables the weapon ability names on the tool bar.")]
        private static void OnToggleAbilityNames(CommandEventArgs e)
        {
            Mobile m = e.Mobile;
			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterWepAbNames != 1 )
				{
					DB.CharacterWepAbNames = 1;
					m.SendMessage(68, "You have enabled weapon ability names.");
				}
				else
				{
					DB.CharacterWepAbNames = 0;
					m.SendMessage(38, "You have disabled weapon ability names.");
				}
			}
        }

        public static bool From( Mobile m )
        {
			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				if ( DB.CharacterWepAbNames > 0 )
					return true;
			}

			return false;
        }
    }
}