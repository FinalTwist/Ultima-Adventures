// By LordGreyWolf

using System;
using Server;
using Server.Gumps;

namespace Server.Commands
{
	public class SpecialAttackCommands
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SetPrimaryAbility", AccessLevel.Player, new CommandEventHandler( SetPrimaryAbility_OnCommand ) );
			CommandSystem.Register( "SetSecondaryAbility", AccessLevel.Player, new CommandEventHandler( SetSecondaryAbility_OnCommand ) );
			CommandSystem.Register( "SetThirdAbility", AccessLevel.Player, new CommandEventHandler( SetThirdAbility_OnCommand ) );
			CommandSystem.Register( "SetFourthAbility", AccessLevel.Player, new CommandEventHandler( SetFourthAbility_OnCommand ) );
			CommandSystem.Register( "SetFifthAbility", AccessLevel.Player, new CommandEventHandler( SetFifthAbility_OnCommand ) );
			CommandSystem.Register( "Set1", AccessLevel.Player, new CommandEventHandler( SetPrimaryAbility_OnCommand ) );
			CommandSystem.Register( "Set2", AccessLevel.Player, new CommandEventHandler( SetSecondaryAbility_OnCommand ) );
			CommandSystem.Register( "Set3", AccessLevel.Player, new CommandEventHandler( SetThirdAbility_OnCommand ) );
			CommandSystem.Register( "Set4", AccessLevel.Player, new CommandEventHandler( SetFourthAbility_OnCommand ) );
			CommandSystem.Register( "Set5", AccessLevel.Player, new CommandEventHandler( SetFifthAbility_OnCommand ) );
		}

		[Usage( "SetPrimaryAbility" )]
		[Aliases( "Set1" )]
		[Description( "Sets your Weapons Primary Ability Active." )]
		private static void SetPrimaryAbility_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if(!from.HasGump(typeof(SpecialAttackGump)))return;
			SpecialAttackGump gump = (SpecialAttackGump)from.FindGump(typeof(SpecialAttackGump));
			gump.OnResponse(from.NetState,new RelayInfo(1,null,null));
		}

		[Usage( "SetSecondaryAbility" )]
		[Aliases( "Set2" )]
		[Description( "Sets your Weapons Secondary Ability Active." )]
		private static void SetSecondaryAbility_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if(!from.HasGump(typeof(SpecialAttackGump)))return;
			SpecialAttackGump gump = (SpecialAttackGump)from.FindGump(typeof(SpecialAttackGump));
			if(gump.Abilities<2)return;
			gump.OnResponse(from.NetState,new RelayInfo(2,null,null));
		}

		[Usage( "SetThirdAbility" )]
		[Aliases( "Set3" )]
		[Description( "Sets your Weapons Third Ability Active." )]
		private static void SetThirdAbility_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if(!from.HasGump(typeof(SpecialAttackGump)))return;
			SpecialAttackGump gump = (SpecialAttackGump)from.FindGump(typeof(SpecialAttackGump));
			if(gump.Abilities<3)return;
			gump.OnResponse(from.NetState,new RelayInfo(3,null,null));
		}

		[Usage( "SetFourthAbility" )]
		[Aliases( "Set4" )]
		[Description( "Sets your Weapons Fourth Ability Active." )]
		private static void SetFourthAbility_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if(!from.HasGump(typeof(SpecialAttackGump)))return;
			SpecialAttackGump gump = (SpecialAttackGump)from.FindGump(typeof(SpecialAttackGump));
			if(gump.Abilities<4)return;
			gump.OnResponse(from.NetState,new RelayInfo(4,null,null));
		}

		[Usage( "SetFifthAbility" )]
		[Aliases( "Set5" )]
		[Description( "Sets your Weapons Fifth Ability Active." )]
		private static void SetFifthAbility_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			if(!from.HasGump(typeof(SpecialAttackGump)))return;
			SpecialAttackGump gump = (SpecialAttackGump)from.FindGump(typeof(SpecialAttackGump));
			if(gump.Abilities<5)return;
			gump.OnResponse(from.NetState,new RelayInfo(5,null,null));
		}
	}
}
