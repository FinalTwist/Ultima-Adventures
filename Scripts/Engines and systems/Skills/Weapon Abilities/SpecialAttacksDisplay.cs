using System;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Items;

namespace Server.Commands
{
	public class SpecialAttacksDisplayCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SpecialAttacksDisplay", AccessLevel.Player, new CommandEventHandler( SpecialAttacksDisplay_Command ) );
			CommandSystem.Register( "SAD", AccessLevel.Player, new CommandEventHandler( SpecialAttacksDisplay_Command ) );
		}

		[Usage( "SpecialAttacksDisplay" )]
		[Aliases( "SAD" )]
		[Description( "Opens your Weapons Special Attacks Display." )]
		private static void SpecialAttacksDisplay_Command( CommandEventArgs e )
		{
			PlayerMobile pm = e.Mobile as PlayerMobile;
			BaseWeapon weapon = (BaseWeapon)(pm.Weapon);
			int number = 0;
			if(weapon.PrimaryAbility!=null) if(pm.Skills[weapon.DefSkill].Value>=70.0 || (pm.Skills[weapon.GetUsedSkill(pm,true)].Value>=70.0 )) number++;
			if(weapon.SecondaryAbility!=null) if(pm.Skills[weapon.DefSkill].Value>=80.0 || (pm.Skills[weapon.GetUsedSkill(pm,true)].Value>=80.0 )) number++;
			if(weapon.ThirdAbility!=null) if(pm.Skills[weapon.DefSkill].Value>=90.0 || (pm.Skills[weapon.GetUsedSkill(pm,true)].Value>=90.0 )) number++;
			if(weapon.FourthAbility!=null) if(pm.Skills[weapon.DefSkill].Value>=100.0 || (pm.Skills[weapon.GetUsedSkill(pm,true)].Value>=100.0 )) number++;
			if(weapon.FifthAbility!=null) if(pm.Skills[weapon.DefSkill].Value>=110.0 || (pm.Skills[weapon.GetUsedSkill(pm,true)].Value>=110.0 )) number++;
			if (number > 0) pm.SendGump(new SpecialAttackGump( weapon, pm, number ));
			else pm.SendMessage("Your weapon skills are not high enough to use a special attack of any kind");
		}
	}
}
