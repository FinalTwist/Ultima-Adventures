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
using Server.Prompts;

namespace Server.Mobiles
{
	public enum SquireDialogTree
	{
		HideFailure,
		TooSoonToHide,
		MissingInstrument,
		TooSoonToPlayMusic,
		MissingSnow,
		TooSoonToPackSnow,
		MasterHasANewNickname,
		SquireHasANewNickname,
		SquireHealsMaster,
		SquireCuresMaster,
		SquireRezsMaster,
		ASquiresConcern,
		ASquiresStay,
		SquiresNewName,
		ShowingOffASquiresBackpack,
		SquireCantReach,
		SquireCantLiftNotItem,
		SquireCantLiftItem,
		SquireCantLiftCorpse,
		ItemIsNotCorpse,
		SquireCantLootAllItems,
		SquireCantLootNotCorpse,
		SquireHealsWounded,
		SquireCuresHumanoid,
		SquireCuresAnimal,
		SquireRezsHumanoid,
		SquireRezsAnimal,
		SquireCantRez,
		WoundedIsNotHurtEnough,
		WoundedOutOfRange,
		WoundedInvisible,
		SquireHealsPlayer,
		SquireCuresPlayer,
		SquireRezsPlayer,
		SquireCantRezPlayer,
		WoundedPlayerIsNotHurtEnough,
		WoundedPlayerOutOfRange,
		HealingTargetNotCreature,
		BeginProvoking,
		WheredMyInstrumentGo,
		LoyalToTheirMaster,
		CantDiscord,
		CantCalm,
		CantCalmHere,
		CantCalmThere,
		AlreadyDiscord,
		AlreadyCalmed,
		NoChanceToProvoke,
		NoChanceToCalm,
		CantProvokeOne,
		CantInciteAnger,
		TooFarApartToProvoke,
		BadPerformance,
		FailedPerformanceProvoke,
		FailedPerformanceDiscord,
		GoodPerformanceProvoke,
		GoodPerformanceDiscord,
		GoodPerformancePeace,
		ProvokeOnThemselves,
		PeaceNobody,
		ThisIsTooHeavy,
		TooHeavyForNow,
		ThankYou,
		ToldToShutUp,
		CanTalkAgain,
		NoPotions,
		TooSoonToDrink,
		AgilityPotion,
		PoisonPotion,
		RefreshPotion,
		StrengthPotion,
		HealthPotion,
		CantHealthPotion,
		StillPoisoned,
		MortallyWoundedHP,
		CurePotion,
		CantCurePotion,
		UsePowerScroll,
		CantUsePowerScroll,
		MissingLockpicks,
		LockpickTooFar,
		NotLocked,
		CannotUnlock,
		BrokenLockpick,
		AbnormalLock,
		HardLock,
		UnsuccessfulLockpick,
		SuccessfulLockpick,
		HandsAreFull,
		StealingNotAllowedHere,
		NotAPartOfThievesGuild,
		SuspendedFromThievesGuild,
		CannotStealFromVendors,
		CannotSeeStealingTarget,
		FullBackpackStealing,
		NeedToBeCloserToSteal,
		CannotStealThat,
		CannotStealWhileMorphed,
		NotSkilledEnoughToStealItem,
		CannotStealFromTheirHands,
		StealFromSelf,
		TooHeavyToSteal,
		SuccessfulSteal,
		UnsuccessfulSteal,
		TooSoonToLockpick,
		TooSoonToSteal,
		WhatShouldISteal,
		LearnsFromContract,
		RefusesToLootPlayers, 
		UnequipsTwoHandedForShield, 
		FirstHandMissing, 
		SecondHandMissing, 
		EmptyHands, 
		SuccessfulSetCreation, 
		Unarmed,
		SpiritSpeakSuccess, 
		SpiritSpeakFail, 
		TooSoonToSpiritSpeak, 
		SpiritChannelFades, 
		StillConnectedToSpirits,
		OpenQuiver,
		NotAQuiver,
		PoisonToApply,
		ApplyPoisonTo,
		NotAPoisonPotion,
		TooFarToPoison,
		CannotPoisonNotInfectious,
		CannotPoisonNotBPFoD,
		PoisoningSuccess,
		PoisoningFailure,
		TerribleMistake,
		NoAnkhNearby,
		TitheSuccess,
		NoGoldToTithe,
		NotEnoughTithe,
		NotEnoughMana,
		NoChivalryBook,
		TooSoonToMeditate,
		TooSoonToCastASpell,
		NotEnoughSpellSkill,
		NoExplosionPotion,
		SquireHasANewTeam // Added 1.9.7
	}
	
	public class SquireDialog
	{
		private SquireDialogTree m_Dialog;
		
		public static void DoSquireDialog( Mobile master, Squire squire, SquireDialogTree dialog, BaseCreature creature, PlayerMobile player )
		{
			if( dialog == SquireDialogTree.HideFailure )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "This is just too hard..." ); break;
					case 4: squire.Emote( "*Cannot grasp the concept of hiding.*" ); break;
					case 3: squire.Say( "Hiding just seems out of my grasp at the moment." ); break;
					case 2: squire.Say( squire.m_MasterNickname.ToUpper() + ", I'VE FAILED YOU!" ); break;
					case 1: squire.Say( "I've failed to hide, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I am sorry, " + squire.m_MasterNickname + ", I have failed to hide." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToHide )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Ah! I've failed to hide! Wait... No, it's still too soon." ); break;
					case 4: squire.Emote( "*Trips over their own feet trying to hide this soon.*" ); break;
					case 3: squire.Say( "Give me some time, please." ); break;
					case 2: squire.Say( "I need a moment." ); break;
					case 1: squire.Say( "I will not attempt to hide again this soon, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "It's a little too soon for me to attempt hiding, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.MissingInstrument )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "An instrument would be useful about now." ); break;
					case 4: squire.Emote( "*Plays 'the worlds smallest lute'.*" ); break;
					case 3: squire.Say( "Would you like me to whistle?" ); break;
					case 2: squire.Say( "I need something to play, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( "Now where did I put it..." ); break;
					case 0: squire.Say( "I don't seem to have an instrument, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToPlayMusic )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I might break my instrument playing this fast." ); break;
					case 4: squire.Emote( "*Catches their balance after attempting to play again too soon.*" ); break;
					case 3: squire.Say( "I cannot play this quick." ); break;
					case 2: squire.Say( squire.m_MasterNickname + ", it's a little too soon." ); break;
					case 1: squire.Say( "I might pull a muscle if I try to play too soon." ); break;
					case 0: squire.Say( "It is too soon for me to play my instrument, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.MissingSnow )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Snow is beyond my grasp." ); break;
					case 4: squire.Emote( "*Tries to gather snow from the ground and fails.*" ); break;
					case 3: squire.Say( "There's no snow in my pack." ); break;
					case 2: squire.Say( squire.m_MasterNickname + ", would you happen to have some snow for me to throw?" ); break;
					case 1: squire.Say( "I cannot seem to find any snow." ); break;
					case 0: squire.Say( "I don't seem to have any snow, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToPackSnow )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't pack this little amount of snow." ); break;
					case 4: squire.Emote( "*Tries and fails to pack snow.*" ); break;
					case 3: squire.Say( "Snow is a little light right now. Give it a moment." ); break;
					case 2: squire.Say( "I cannot pack this snow right away, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( "There is too little snow in this pile." ); break;
					case 0: squire.Say( "It is too soon to try to pack this snow, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.MasterHasANewNickname )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Let me try screaming it... " + squire.m_MasterNickname.ToUpper() + "!" ); squire.Say( "Yeah, that works!" ); break;
					case 4: squire.Emote( "*Is more than excited to refer to you as " + squire.m_MasterNickname + ".*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + " fits you very well!" ); break;
					case 2: squire.Say( "I cannot wait to get used to referring to you as " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + "... Alright." ); break;
					case 0: squire.Say( "Alright, I will refer to you as " + squire.m_MasterNickname + " from now on." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireHasANewNickname )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Let me try screaming it... " + squire.m_SquireNickname.ToUpper() + "!" ); squire.Say( "Yeah, that works!" ); break;
					case 4: squire.Emote( "*Is more than excited to be referred to as " + squire.m_SquireNickname + "!*" ); break;
					case 3: squire.Say( squire.m_SquireNickname + " fits me very well!" ); break;
					case 2: squire.Say( "I am certain I will get used to being called " + squire.m_SquireNickname + " from now on." ); break;
					case 1: squire.Say( squire.m_SquireNickname + "... It's not... Terrible..." ); break;
					case 0: squire.Say( "Alright, I will answer to " + squire.m_SquireNickname + " from now on." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireHealsMaster )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Stay calm " + squire.m_SquireNickname + ", " + squire.m_MasterNickname + " will be just fine, do your job." ); break;
					case 4: squire.Emote( "*Mutters a little prayer.*" ); break;
					case 3: squire.Say( "DON'T YOU DIE ON ME, " + squire.m_MasterNickname.ToUpper() + "!" ); break;
					case 2: squire.Say( "You'll be okay, " + squire.m_MasterNickname + " just hang in there." ); break;
					case 1: squire.Say( "Your wounds aren't the worst I've seen." ); break;
					case 0: squire.Say( "Let me bandage that wound, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCuresMaster )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "This poison will be quickly done away with." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Nox'.*" ); break;
					case 3: squire.Say( "This poison isn't THAT bad." ); break;
					case 2: squire.Say( "The mages of Moonglow taught me this one." ); break;
					case 1: squire.Say( "I learned this little remedy in Papua during my younger years." ); break;
					case 0: squire.Say( "Let me attempt to cure you, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireRezsMaster )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It is too soon for you to leave this world, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Corp'.*" ); break;
					case 3: squire.Say( squire.m_SquireNickname + "'s got you, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "This is a healing technique I picked up in Nujel'm." ); break;
					case 1: squire.Say( "I am a failure as a squire, please forgive me, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "Let me attempt to resurrect you, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.ASquiresConcern )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Please don't die on me, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Sweats nervously.*" ); break;
					case 3: squire.Say( "I can't believe I lost track of " + squire.m_MasterNickname + " I am a terrible squire." ); break;
					case 2: squire.Say( squire.m_MasterNickname + " is wounded, but I can't see them!" ); break;
					case 1: squire.Say( squire.m_MasterNickname.ToUpper() + " WHERE ARE YOU!" ); break;
					case 0: squire.Say( squire.m_MasterNickname + "? Please show yourself! " + squire.m_MasterNickname + "!?!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.ASquiresStay )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't move from where I am, " + squire.m_MasterNickname + " please call me over." ); break;
					case 4: squire.Emote( "*Sweats anxiously.*" ); break;
					case 3: squire.Say( squire.m_SquireNickname + " will stay right here, just as " + squire.m_MasterNickname + " told them to." ); break;
					case 2: squire.Say( "Help me help you, " + squire.m_MasterNickname + ". Call me over!" ); break;
					case 1: squire.Say( squire.m_MasterNickname.ToUpper() + " YOU TOLD ME NOT TO LEAVE THIS SPOT!" ); break;
					case 0: squire.Say( "I would be there to heal you, " + squire.m_MasterNickname + ", but you asked me to stay." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquiresNewName )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "How about, " + squire.Name + "." ); break;
					case 4: squire.Emote( "*Thinks deeply on " + squire.Name + " as their new name.*" ); break;
					case 3: squire.Say( "What's my name? How about " + squire.Name + "!" ); break;
					case 2: squire.Say( squire.Name + " sounds good, right?" ); break;
					case 1: squire.Say( squire.m_MasterNickname + " and " + squire.Name + "." ); break;
					case 0: squire.Say( "I think the name " + squire.Name + " suits me best, don't you?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.ShowingOffASquiresBackpack )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Look at the contents of my pack if you want." ); break;
					case 4: squire.Emote( "*Proudly opens their backpack to you.*" ); break;
					case 3: squire.Say( "I am sure your backpack's contents are more interesting than mine." ); break;
					case 2: squire.Say( "Here is what your little " + squire.m_SquireNickname + " is holding." ); break;
					case 1: squire.Say( "Y-you'd like to see my what? Oh, my backpack." ); break;
					case 0: squire.Say( "Here is what I am holding." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantReach )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That's a little too far for me." ); break;
					case 4: squire.Emote( "*Attempts to reach the target, but fails.*" ); break;
					case 3: squire.Say( "My arms can't reach!" ); break;
					case 2: squire.Say( "It is a little out of my reach." ); break;
					case 1: squire.Say( squire.m_MasterNickname + " could you bring me closer?" ); break;
					case 0: squire.Say( "I can't reach that from here." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantLiftNotItem )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That isn't an item, so I can't lift it." ); break;
					case 4: squire.Emote( "*Attempts to lift the target, but fails.*" ); break;
					case 3: squire.Say( "Are you sure?" ); break;
					case 2: squire.Say( "That's not something I can carry." ); break;
					case 1: squire.Say( "Are you serious?" ); break;
					case 0: squire.Say( "I can't pick that up." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantLiftItem )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm not as strong as you are, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Attempts to lift the target, but fails.*" ); break;
					case 3: squire.Say( "Looks a little heavy." ); break;
					case 2: squire.Say( "Is that even moveable?" ); break;
					case 1: squire.Say( "It might be a bit too heavy for me." ); break;
					case 0: squire.Say( "I failed to lift that item." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantLiftCorpse )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It looks like it's rotting. Are you sure you don't mean to have me loot this?" ); break;
					case 4: squire.Emote( "*Gives a meek attempt at lifting the corpse.*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + "... That's gross." ); break;
					case 2: squire.Say( "Looks like it has items on it, are you sure you don't want me to loot this?" ); break;
					case 1: squire.Say( "I refuse to pick up a body." ); break;
					case 0: squire.Say( "I can't lift this, did you want me to loot it instead?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.ItemIsNotCorpse )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "This isn't lootable." ); break;
					case 4: squire.Emote( "*Pokes the item before looking up at you.*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + "... Are you alright?" ); break;
					case 2: squire.Say( "It doesn't look like a corpse to me." ); break;
					case 1: squire.Say( "This isn't a corpse." ); break;
					case 0: squire.Say( "Did you mean grab?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantLootAllItems )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "There was too much." ); break;
					case 4: squire.Emote( "*Items slip out of " + squire.Name + "'s hands and back into the corpse.*" ); break;
					case 3: squire.Say( "I couldn't hold all of the items." ); break;
					case 2: squire.Say( "There was too much for me to grab." ); break;
					case 1: squire.Say( "I'm sorry, " + squire.m_MasterNickname + " but I couldn't gather all the items." ); break;
					case 0: squire.Say( "I could not pick up all of the items." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantLootNotCorpse )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That isn't a corpse, so I can't loot it." ); break;
					case 4: squire.Emote( "*Attempts to loot the target, but fails.*" ); break;
					case 3: squire.Say( "Are you sure?" ); break;
					case 2: squire.Say( "That's not something I can loot." ); break;
					case 1: squire.Say( "Are you serious?" ); break;
					case 0: squire.Say( "I can't loot that." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireHealsWounded )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Stay calm " + squire.m_SquireNickname + ", " + creature.Name + " will be just fine, do your job." ); break;
					case 4: squire.Emote( "*Mutters a little prayer.*" ); break;
					case 3: squire.Say( "I'll make sure you don't die, " + creature.Name + "." ); break;
					case 2: squire.Say( "You'll be okay, " + creature.Name + " just hang in there." ); break;
					case 1: squire.Say( "Your wounds aren't the worst I've seen." ); break;
					case 0: squire.Say( "Let me bandage that wound, " + creature.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCuresHumanoid )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "This poison will be quickly done away with." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Nox'.*" ); break;
					case 3: squire.Say( "This poison isn't THAT bad." ); break;
					case 2: squire.Say( "The mages of Moonglow taught me this one." ); break;
					case 1: squire.Say( "I learned this little remedy in Papua during my younger years." ); break;
					case 0: squire.Say( "Let me attempt to cure you, " + creature.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCuresAnimal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "This poison will be quickly done away with." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Nox'.*" ); break;
					case 3: squire.Say( "Your poison isn't too bad, " + creature.Name + "." ); break;
					case 2: squire.Say( "I learned to care for animals in my travels, here..." ); break;
					case 1: squire.Say( "Papua's remedies are potent, I'm told." ); break;
					case 0: squire.Say( "There there, " + creature.Name + " you'll be cured soon." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireRezsHumanoid )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It is too soon for you to leave this world, " + creature.Name + "." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Corp'.*" ); break;
					case 3: squire.Say( squire.Name + "'s got you, " + creature.Name + "." ); break;
					case 2: squire.Say( "This is a healing technique I picked up in Nujel'm." ); break;
					case 1: squire.Say( squire.m_MasterNickname + " must be a good friend of yours to have me do this." ); break;
					case 0: squire.Say( "Let me attempt to resurrect you, " + creature.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireRezsAnimal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It is too soon for you to leave this world, " + creature.Name + "." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Corp'.*" ); break;
					case 3: squire.Say( squire.Name + "'s got you, " + creature.Name + "." ); break;
					case 2: squire.Say( "This is a healing technique I picked up in Delucia." ); break;
					case 1: squire.Say( squire.m_MasterNickname + " is a kind person." ); break;
					case 0: squire.Say( "Let me attempt to resurrect you, " + creature.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantRez )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I am simply too unskilled to do this." ); break;
					case 4: squire.Say( "I cannot resurrect " + creature.Name + "." ); break;
					case 3: squire.Say( "I am sorry but there is nothing I can do." ); break;
					case 2: squire.Say( "The mages of Moonglow did not prepare me enough for this." ); break;
					case 1: squire.Say( "You've overestimated my abilities, " + squire.m_MasterNickname + ", I cannot resurrect " + creature.Name + "." ); break;
					case 0: squire.Say( "I apologize, " + creature.Name + ", but I cannot resurrect you for I am not skilled enough." ); break;
				}
			}
			else if( dialog == SquireDialogTree.WoundedIsNotHurtEnough )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "You will be fine soon enough." ); break;
					case 4: squire.Emote( "*Chuckles.*" ); break;
					case 3: squire.Say( "They'll be fine in a moment." ); break;
					case 2: squire.Say( "I refuse to waste bandages." ); break;
					case 1: squire.Say( squire.m_MasterNickname + " you are too kind, it would be a waste of bandages at this point." ); break;
					case 0: squire.Say( creature.Name + " is not wounded enough." ); break;
				}
			}
			else if( dialog == SquireDialogTree.WoundedOutOfRange )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "They're too far." ); break;
					case 4: squire.Emote( "*Attempts to stretch arms towards " + creature.Name + ".*" ); break;
					case 3: squire.Say( "Bandages are more effective when applied directly on the wound." ); break;
					case 2: squire.Say( "I cannot reach " + creature.Name + "." ); break;
					case 1: squire.Say( "This '" + creature.Name + "' is too far away." ); break;
					case 0: squire.Say( creature.Name + " is out of my range." ); break;
				}
			}
			else if( dialog == SquireDialogTree.WoundedInvisible )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "You must be able to see the unseeable." ); break;
					case 4: squire.Emote( "*Tosses a bandage in the air before them.*" ); squire.Say( "Did I do it?" ); break;
					case 3: squire.Say( "They must be hiding." ); break;
					case 2: squire.Say( "You want me to heal the air?" ); break;
					case 1: squire.Say( "What are you telling me to heal?" ); break;
					case 0: squire.Say( "I cannot see that." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireHealsPlayer )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Stay calm " + squire.m_SquireNickname + ", " + player.Name + " will be just fine, do your job." ); break;
					case 4: squire.Emote( "*Mutters a little prayer.*" ); break;
					case 3: squire.Say( "I'll make sure you don't die, " + player.Name + "." ); break;
					case 2: squire.Say( "You'll be okay, " + player.Name + " just hang in there." ); break;
					case 1: squire.Say( "Your wounds aren't the worst I've seen." ); break;
					case 0: squire.Say( "Let me bandage that wound, " + player.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCuresPlayer )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "This poison will be quickly done away with." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Nox'.*" ); break;
					case 3: squire.Say( "This poison isn't THAT bad." ); break;
					case 2: squire.Say( "The mages of Moonglow taught me this one." ); break;
					case 1: squire.Say( "I learned this little remedy in Papua during my younger years." ); break;
					case 0: squire.Say( "Let me attempt to cure you, " + player.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireRezsPlayer )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It is too soon for you to leave this world, " + player.Name + "." ); break;
					case 4: squire.Emote( "*Mouths the words 'An Corp'.*" ); break;
					case 3: squire.Say( squire.Name + "'s got you, " + player.Name + "." ); break;
					case 2: squire.Say( "This is a healing technique I picked up in Nujel'm." ); break;
					case 1: squire.Say( squire.m_MasterNickname + " must be a good friend of yours to have me do this." ); break;
					case 0: squire.Say( "Let me attempt to resurrect you, " + player.Name + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireCantRezPlayer )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I am simply too unskilled to do this." ); break;
					case 4: squire.Say( "I cannot resurrect " + player.Name + "." ); break;
					case 3: squire.Say( "I am sorry but there is nothing I can do." ); break;
					case 2: squire.Say( "The mages of Moonglow did not prepare me enough for this." ); break;
					case 1: squire.Say( "You've overestimated my abilities, " + squire.m_MasterNickname + ", I cannot resurrect " + player.Name + "." ); break;
					case 0: squire.Say( "I apologize, " + player.Name + ", but I cannot resurrect you for I am not skilled enough." ); break;
				}
			}
			else if( dialog == SquireDialogTree.WoundedPlayerIsNotHurtEnough )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "You will be fine soon enough." ); break;
					case 4: squire.Emote( "*Chuckles.*" ); break;
					case 3: squire.Say( "They'll be fine in a moment." ); break;
					case 2: squire.Say( "I refuse to waste bandages." ); break;
					case 1: squire.Say( squire.m_MasterNickname + " you are too kind, it would be a waste of bandages at this point." ); break;
					case 0: squire.Say( player.Name + " is not wounded enough." ); break;
				}
			}
			else if( dialog == SquireDialogTree.WoundedPlayerOutOfRange )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "They're too far." ); break;
					case 4: squire.Emote( "*Attempts to stretch arms towards " + player.Name + ".*" ); break;
					case 3: squire.Say( "Bandages are more effective when applied directly on the wound." ); break;
					case 2: squire.Say( "I cannot reach " + player.Name + "." ); break;
					case 1: squire.Say( "This '" + player.Name + "' is too far away." ); break;
					case 0: squire.Say( player.Name + " is out of my range." ); break;
				}
			}
			else if( dialog == SquireDialogTree.HealingTargetNotCreature )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Are you alright?" ); break;
					case 4: squire.Emote( "*Confusedly looks up at " + squire.m_MasterNickname + ".*" ); break;
					case 3: squire.Say( "Wounds are necessary, and that cannot be wounded." ); break;
					case 2: squire.Say( "Why would I heal that?" ); break;
					case 1: squire.Say( "That is not a creature." ); break;
					case 0: squire.Say( "That cannot be healed." ); break;
				}
			}
			else if( dialog == SquireDialogTree.BeginProvoking )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Who would you like to fight someone?" ); break;
					case 4: squire.Emote( "*Prepares instrument, looking at you for a target.*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + ", who do you want to fight?" ); break;
					case 2: squire.Say( "Whom should I provoke?" ); break;
					case 1: squire.Say( "Who should I get angry?" ); break;
					case 0: squire.Say( "Who would you like me to anger?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.WheredMyInstrumentGo )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I am sure it was just here!" ); break;
					case 4: squire.Emote( "*Rifles through bag looking for instrument.*" ); break;
					case 3: squire.Say( "My instrument... It's gone!" ); break;
					case 2: squire.Say( "Now where did I put that..." ); break;
					case 1: squire.Say( "That's weird, I just had it here a moment ago..." ); break;
					case 0: squire.Say( "I can't seem to find my instrument." ); break;
				}
			}
			else if( dialog == SquireDialogTree.LoyalToTheirMaster )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That's not going to work on a creature owned by someone else." ); break;
					case 4: squire.Say( "I refuse to provoke something owned by another person." ); break;
					case 3: squire.Say( "It belongs to someone else, I can't provoke it." ); break;
					case 2: squire.Say( "That creature belongs to somebody else." ); break;
					case 1: squire.Say( "That creature is too loyal to their master to get provoked." ); break;
					case 0: squire.Say( "I can't provoke this, it's too loyal to its master." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantDiscord )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My song wouldn't affect that." ); break;
					case 4: squire.Say( "Does that even have ears?" ); break;
					case 3: squire.Say( "I'm not sure that can be discorded." ); break;
					case 2: squire.Say( "That thing can't be discorded." ); break;
					case 1: squire.Say( "I don't think I can sing a song of discord to that." ); break;
					case 0: squire.Say( "A song of discord would have no effect on that." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantCalm )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My song wouldn't affect that." ); break;
					case 4: squire.Say( "Does that even have ears?" ); break;
					case 3: squire.Say( "I'm not sure that can be calmed." ); break;
					case 2: squire.Say( "Did you hit your head, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( "I don't believe you can calm an inanimate object..." ); break;
					case 0: squire.Say( "That cannot be calmed." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantCalmHere )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My song wouldn't affect that here." ); break;
					case 4: squire.Say( "I'm sorry, I can't play here." ); break;
					case 3: squire.Say( "This area does not allow me to play calming music." ); break;
					case 2: squire.Say( squire.m_MasterNickname + ", I can't play a calming song here." ); break;
					case 1: squire.Say( "This area has a strange aura around it, I can't calm things here." ); break;
					case 0: squire.Say( "Peacemaking won't work here." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantCalmThere )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My song wouldn't affect that there." ); break;
					case 4: squire.Say( "I'm sorry, I can't play there." ); break;
					case 3: squire.Say( "That area does not allow me to play calming music." ); break;
					case 2: squire.Say( squire.m_MasterNickname + ", I can't play a calming song there." ); break;
					case 1: squire.Say( "That area has a strange aura around it, I can't calm things there." ); break;
					case 0: squire.Say( "Peacemaking won't work there." ); break;
				}
			}
			else if( dialog == SquireDialogTree.AlreadyDiscord )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My song wouldn't affect again right away." ); break;
					case 4: squire.Say( "Another song of discord wouldn't have an effect on that." ); break;
					case 3: squire.Say( "That can't be discorded again." ); break;
					case 2: squire.Say( "I can't discord something already under the song's effect." ); break;
					case 1: squire.Say( "I'm sorry, but that's already discorded." ); break;
					case 0: squire.Say( "That creature is already discorded." ); break;
				}
			}
			else if( dialog == SquireDialogTree.AlreadyCalmed )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My song wouldn't affect again right away." ); break;
					case 4: squire.Say( "Another song of calming wouldn't have an effect on that." ); break;
					case 3: squire.Say( "That can't be calmed again." ); break;
					case 2: squire.Say( "I can't calm something that is already calm." ); break;
					case 1: squire.Say( "I'm sorry, but that's already calmed." ); break;
					case 0: squire.Say( "That creature has already made its peace." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoChanceToProvoke )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "There's just nothing I can do to provoke that creature." ); break;
					case 4: squire.Say( "I simply have no chance to provoke that creature." ); break;
					case 3: squire.Say( "I cannot provoke that, I just have no chance." ); break;
					case 2: squire.Say( "I just don't have a chance to provoke that creature." ); break;
					case 1: squire.Say( "Sorry, " + squire.m_MasterNickname + " but I have no chance to provoke that." ); break;
					case 0: squire.Say( "I've got no chance to provoke this creature." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoChanceToCalm )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "There's just nothing I can do to calm that creature." ); break;
					case 4: squire.Say( "I simply have no chance to calm that creature." ); break;
					case 3: squire.Say( "I cannot calm that, I just have no chance." ); break;
					case 2: squire.Say( "I just don't have a chance to calm that creature." ); break;
					case 1: squire.Say( "Sorry, " + squire.m_MasterNickname + " but I have no chance to calm that." ); break;
					case 0: squire.Say( "I've got no chance to calm this creature." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantProvokeOne )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Now who should feel their wrath?" ); break;
					case 4: squire.Say( "Who can we get this guy to fight?" ); break;
					case 3: squire.Say( "Success! Who should they fight?" ); break;
					case 2: squire.Say( squire.m_MasterNickname + ", who would you like them to attack?" ); break;
					case 1: squire.Say( "Okay! They're plenty angry! Who do you want them to fight?" ); break;
					case 0: squire.Say( "Alright! This one is angered, who would you like me to provoke them to attack?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantInciteAnger )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That cannot be angered." ); break;
					case 4: squire.Say( "That cannot feel anger." ); break;
					case 3: squire.Say( "Anger cannot be incited from this." ); break;
					case 2: squire.Say( squire.m_MasterNickname + " are you alright?" ); break;
					case 1: squire.Say( "That can't feel anger." ); break;
					case 0: squire.Say( "I can't provoke that!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooFarApartToProvoke )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "They're too far apart." ); break;
					case 4: squire.Say( "Could we get them closer together somehow " + squire.m_MasterNickname + "?" ); break;
					case 3: squire.Say( "I don't think that creature can be mad at this other creature if they're this far apart." ); break;
					case 2: squire.Say( squire.m_MasterNickname + " do you have any ideas on how we could get them closer together?" ); break;
					case 1: squire.Say( "If only we could get them closer together..." ); break;
					case 0: squire.Say( "The two creatures are too far away from each other for me to provoke them." ); break;
				}
			}
			else if( dialog == SquireDialogTree.BadPerformance )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( squire.m_SquireNickname + " might need a little more practice, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Embarrassedly attempts to cover face at their poor performance.*" ); break;
					case 3: squire.Say( "My instrument must not be tuned correctly..." ); break;
					case 2: squire.Say( "Oof, was that at the wrong key?" ); break;
					case 1: squire.Say( "It seems a need a little more practice." ); break;
					case 0: squire.Say( "I'm sorry, " + squire.m_MasterNickname + " I played the best I could." ); break;
				}
			}
			else if( dialog == SquireDialogTree.FailedPerformanceProvoke )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I might need a little more practice..." ); break;
					case 4: squire.Emote( "*Confusedly looks at creatures.*" ); squire.Say("Why aren't they fighting?" ); break;
					case 3: squire.Say( "My instrument must not be tuned correctly..." ); break;
					case 2: squire.Say( "Did I play well enough?" ); break;
					case 1: squire.Say( "I couldn't incite enough anger with my music." ); break;
					case 0: squire.Say( "It seems I couldn't get them angry enough..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.FailedPerformanceDiscord )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I might need a little more practice..." ); break;
					case 4: squire.Emote( "*Confusedly looks at creature.*" ); squire.Say( "They don't look weaker..." ); break;
					case 3: squire.Say( "My instrument must not be tuned correctly..." ); break;
					case 2: squire.Say( "Did I play well enough?" ); break;
					case 1: squire.Say( "I couldn't lower their attack with my music." ); break;
					case 0: squire.Say( "It seems I couldn't get throw them off balance..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.GoodPerformanceProvoke )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Awesome!" ); break;
					case 4: squire.Emote( "*Looks overjoyed as the two creatures start fighting.*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + " look at them fight!" ); break;
					case 2: squire.Say( "I can't wait till they kill each other!" ); break;
					case 1: squire.Say( "Look at them go!" ); break;
					case 0: squire.Say( "Yes! Fight!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.GoodPerformanceDiscord )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "They're thrown off balance!" ); break;
					case 4: squire.Emote( "*Pumps their fist in success of their song.*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + ", go for their throat!" ); break;
					case 2: squire.Say( "Now's the time to attack!" ); break;
					case 1: squire.Say( "They're weakened!" ); break;
					case 0: squire.Say( "Yes! Feel the lower attack!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.GoodPerformancePeace )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Their fighting has stopped!" ); break;
					case 4: squire.Emote( "*Breaths a sigh of relief.*" ); break;
					case 3: squire.Say( squire.m_MasterNickname + ", they've stopped!" ); break;
					case 2: squire.Say( "Stop fighting!" ); break;
					case 1: squire.Say( "Peace! Haha!" ); break;
					case 0: squire.Say( "Everybody, just CALM DOWN!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.ProvokeOnThemselves )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", but a creature can't fight itself." ); break;
					case 4: squire.Emote( "*Is disappointed they can't make someone fight themselves.*" ); break;
					case 3: squire.Say( "I don't think their self-hatred is strong enough." ); break;
					case 2: squire.Say( "No can do, " + squire.m_MasterNickname + ", I can't make them fight themselves." ); break;
					case 1: squire.Say( "I wish they would punch themselves in the face..." ); break;
					case 0: squire.Say( "I can't get this creature to fight itself." ); break;
				}
			}
			else if( dialog == SquireDialogTree.PeaceNobody )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", but nobody heard my song." ); break;
					case 4: squire.Emote( "*Hums happily to themselves.*" ); break;
					case 3: squire.Say( "Nobody can hear this next song!" ); break;
					case 2: squire.Say( "Music, nobody can hear!" ); break;
					case 1: squire.Say( "Nobody is around to hear my music!" ); break;
					case 0: squire.Say( "I've played hypnotic music but nobody can hear it!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.ThisIsTooHeavy )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", but I can't use this." ); break;
					case 4: squire.Emote( "*Struggles attempting to use the item, meekly handing it back to you.*" ); break;
					case 3: squire.Say( "Please take this back, I cannot effectively use it." ); break;
					case 2: squire.Say( "Are you kidding me? I can't hold this! I'm too weak!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", this is too heavy for me to use. Please take it back." ); break;
					case 0: squire.Say( "I'm sorry, I am too weak to wear this." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooHeavyForNow )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", but I can't use this right now, I'll store it in my backpack for now." ); break;
					case 4: squire.Emote( "*Fails to equip a piece of equipment, places it in their backpack instead.*" ); break;
					case 3: squire.Say( "I'll store this in my pack for now, I can't currently use it." ); break;
					case 2: squire.Say( "I'm too weak for this equipment right now, I'll put it in my pack." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", this is too heavy for me to use at the moment, I'll put it in my pack." ); break;
					case 0: squire.Say( "I'm sorry, I am too weak to wear this right now." ); break;
				}
			}
			else if( dialog == SquireDialogTree.ThankYou )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you, " + squire.m_MasterNickname + ", I'll use it well!" ); break;
					case 4: squire.Emote( "*Proudly dawns the equipment.*" ); break;
					case 3: squire.Say( "I promise to use this to the best of my abilities." ); break;
					case 2: squire.Say( "I'm sure this will help me become harder to kill!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", thank you very much for the equipment!" ); break;
					case 0: squire.Say( "Thank you! I'll make good use of this." ); break;
				}
			}
			else if( dialog == SquireDialogTree.ToldToShutUp )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Okay, " + squire.m_MasterNickname + ", I'll shut up." ); break;
					case 4: squire.Emote( "*Zips their lip.*" ); break;
					case 3: squire.Say( "Sure." ); break;
					case 2: squire.Say( "I will stop talking now." ); break;
					case 1: squire.Say( "Alright, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "Alright, I'll be quiet." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CanTalkAgain )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Okay, " + squire.m_MasterNickname + ", I'll start talking again." ); break;
					case 4: squire.Emote( "*Breathes a sigh of relief.*" ); break;
					case 3: squire.Say( "Sure!" ); break;
					case 2: squire.Say( "I will start talking again!" ); break;
					case 1: squire.Say( "Alright, " + squire.m_MasterNickname + "!" ); break;
					case 0: squire.Say( "Thank you, I had a lot to squire.Say!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoPotions )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "No can do, " + squire.m_MasterNickname + ", I can't find them in my backpack." ); break;
					case 4: squire.Emote( "*Sighs deeply, searching their backpack for the potion.*" ); break;
					case 3: squire.Say( "I cannot seem to find those potions in my backpack." ); break;
					case 2: squire.Say( "Where'd they go! I cannot seem to find them!" ); break;
					case 1: squire.Say( "Sorry, " + squire.m_MasterNickname + ", but I don't have those potions." ); break;
					case 0: squire.Say( "I don't seem to have any potions of that type." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToDrink )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "No can do, " + squire.m_MasterNickname + ", I can't swallow that right away." ); break;
					case 4: squire.Emote( "*Shakes their head, corking the bottle and stuffing it back in their pack.*" ); break;
					case 3: squire.Say( "I am sorry, I just can't swallow that again so soon." ); break;
					case 2: squire.Say( "I simply cannot swallow that again so soon." ); break;
					case 1: squire.Say( "Sorry, " + squire.m_MasterNickname + ", but I can't swallow more of that concoction." ); break;
					case 0: squire.Say( "I can't drink that right away." ); break;
				}
			}
			else if( dialog == SquireDialogTree.AgilityPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", I feel amazing!" ); break;
					case 4: squire.Emote( "*Takes a deep breath, feeling exhilarated.*" ); break;
					case 3: squire.Say( "I feel considerably lighter!" ); break;
					case 2: squire.Say( "What was in that potion!?" ); break;
					case 1: squire.Say( "Wow, " + squire.m_MasterNickname + ", I feel great!" ); break;
					case 0: squire.Say( "I feel like I could run farther!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.PoisonPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", I feel like I'm dying..." ); break;
					case 4: squire.Emote( "*Holds their breath, hoping not to throw up.*" ); break;
					case 3: squire.Say( "I feel considerably sick already, what did you do to me?" ); break;
					case 2: squire.Say( "What was in that potion?" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", what did you do?" ); break;
					case 0: squire.Say( "I don't feel so good..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.RefreshPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", I feel great!" ); break;
					case 4: squire.Emote( "*Takes a deep breath, feeling refreshed.*" ); break;
					case 3: squire.Say( "I feel considerably better!" ); break;
					case 2: squire.Say( "This is amazing! I feel great! I! Can! Do! This!" ); break;
					case 1: squire.Say( "Wow, " + squire.m_MasterNickname + ", I feel refreshed!" ); break;
					case 0: squire.Say( "I feel absolutely refreshed!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.StrengthPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", I feel stronger!" ); break;
					case 4: squire.Emote( "*Opens their eyes sharply, a sign of great strength.*" ); break;
					case 3: squire.Say( "Strength flows through my veins!" ); break;
					case 2: squire.Say( "This is amazing! I feel great! I! Can! Do! This!" ); break;
					case 1: squire.Say( "Wow, " + squire.m_MasterNickname + ", I feel stronger!" ); break;
					case 0: squire.Say( "I feel much stronger!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.HealthPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", I feel much better!" ); break;
					case 4: squire.Emote( "*Their joints and wounds shudder as they begin to heal.*" ); break;
					case 3: squire.Say( "I can feel my wounds healing!" ); break;
					case 2: squire.Say( "My organs feel much better!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", thank you for this beverage." ); break;
					case 0: squire.Say( "That's better." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantHealthPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", but I can't, I'm feeling too well." ); break;
					case 4: squire.Emote( "*Stuffs the health potion back in their backpack.*" ); break;
					case 3: squire.Say( "Don't worry about me!" ); break;
					case 2: squire.Say( "I'm feeling a little too well to drink a health potion right now." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I can't, I'll put this back in my backpack." ); break;
					case 0: squire.Say( "I'm not wounded enough to drink this concoction." ); break;
				}
			}
			else if( dialog == SquireDialogTree.StillPoisoned )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Can I have a cure potion first, " + squire.m_MasterNickname + "? This health potion won't do me any good otherwise." ); break;
					case 4: squire.Emote( "*Stuffs the health potion back in their backpack.*" ); break;
					case 3: squire.Say( "This won't do me any good while I'm poisoned!" ); break;
					case 2: squire.Say( "Sorry, I can't swallow this while poisoned." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I can't, I am still poisoned." ); break;
					case 0: squire.Say( "I cannot drink a health potion while poisoned." ); break;
				}
			}
			else if( dialog == SquireDialogTree.MortallyWoundedHP )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I need bandages first, " + squire.m_MasterNickname + ". This health potion won't do me any good otherwise." ); break;
					case 4: squire.Emote( "*Stuffs the health potion back in their backpack.*" ); break;
					case 3: squire.Say( "This won't do me any good while I'm wounded this bad!" ); break;
					case 2: squire.Say( "Sorry, I can't swallow this while there is a large gash in my stomach." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I can't, I am still mortally wounded." ); break;
					case 0: squire.Say( "I cannot drink a health potion while mortally wounded." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CurePotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you so much, " + squire.m_MasterNickname + ", I feel a bit better!" ); break;
					case 4: squire.Emote( "*The pain on their face fades to relief.*" ); break;
					case 3: squire.Say( "I can feel my stomach healing!" ); break;
					case 2: squire.Say( "I feel less sick!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", thank you for this beverage." ); break;
					case 0: squire.Say( "I'm feeling a little better." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantCurePotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", I'm not feeling sick!" ); break;
					case 4: squire.Emote( "*Stuffs the cure potion back in their backpack.*" ); break;
					case 3: squire.Say( "There's no reason for me to drink a cure potion now." ); break;
					case 2: squire.Say( "I refuse to waste your resources, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I would have to be poisoned before I swallow that... Concoction." ); break;
					case 0: squire.Say( "That would be a waste, I'm not poisoned." ); break;
				}
			}
			else if( dialog == SquireDialogTree.UsePowerScroll )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thank you, " + squire.m_MasterNickname + ", I feel like there's more to know!" ); break;
					case 4: squire.Emote( "*Takes a deep breath, feeling like there is more they can learn.*" ); break;
					case 3: squire.Say( "There is so much more to this world than I imagined." ); break;
					case 2: squire.Say( "I have a greater thirst for knowledge, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I feel like I can learn more now." ); break;
					case 0: squire.Say( "I can feel my limits rising!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.CantUsePowerScroll )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", my limits for this knowledge are already higher!" ); break;
					case 4: squire.Emote( "*Stuffs the power scroll into their backpack.*" ); break;
					case 3: squire.Say( "There's no reason for me to use this, for my limits are already higher." ); break;
					case 2: squire.Say( "I refuse to waste your power scroll, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I cannot use this power scroll, I will hold it in my backpack." ); break;
					case 0: squire.Say( "I cannot use this power scroll, my limits are already higher than this." ); break;
				}
			}
			else if( dialog == SquireDialogTree.MissingLockpicks )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I seem to be out of lockpicks." ); break;
					case 4: squire.Emote( "*Searches for lockpicks in their backpack, finding none.*" ); break;
					case 3: squire.Say( "There doesn't seem to be any lockpicks in my backpack." ); break;
					case 2: squire.Say( "I cannot find my lockpicks, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I don't seem to have any lockpicks." ); break;
					case 0: squire.Say( "I cannot seem to locate my lockpicks..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.LockpickTooFar )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", lockpicking takes a delicate touch, I need to be closer." ); break;
					case 4: squire.Emote( "*Is frustrated they aren't closer to the lock to pick it.*" ); break;
					case 3: squire.Say( "I need to be right next to a lock to pick it." ); break;
					case 2: squire.Say( "I cannot pick a lock that far away, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", that is too far away for me to attempt to pick it." ); break;
					case 0: squire.Say( "I can't pick that lock this far away." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotLocked )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", this isn't locked." ); break;
					case 4: squire.Emote( "*Puts their lockpicks away, there is no job to be done here.*" ); break;
					case 3: squire.Say( "This isn't locked." ); break;
					case 2: squire.Say( "I cannot pick a lock that isn't actually locked, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", this isn't locked." ); break;
					case 0: squire.Say( "This doesn't appear to be locked." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotUnlock )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", I cannot unlock this." ); break;
					case 4: squire.Emote( "*Cannot unlock this.*" ); break;
					case 3: squire.Say( "I see no way in which I can unlock this." ); break;
					case 2: squire.Say( "I cannot pick this lock, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I cannot unlock this." ); break;
					case 0: squire.Say( "I can't unlock this!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.BrokenLockpick )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", my lockpick broke." ); break;
					case 4: squire.Emote( "*The lockpick breaks in their hand.*" ); break;
					case 3: squire.Say( "My lockpick seems to have broken." ); break;
					case 2: squire.Say( "I broke my lockpick, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I broke a lockpick." ); break;
					case 0: squire.Say( "I broke a lockpick!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.AbnormalLock )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I do not think this can be unlocked by normal means." ); break;
					case 4: squire.Emote( "*Is perplexed by this lock.*" ); break;
					case 3: squire.Say( "This cannot be unlocked by normal means." ); break;
					case 2: squire.Say( "This is a strange lock, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I do not believe this lock can be picked by normal means." ); break;
					case 0: squire.Say( "This doesn't look like it can be unlocked by normal means!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.HardLock )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I'm not skilled enough to unlock this." ); break;
					case 4: squire.Emote( "*Admits defeat to this lock.*" ); break;
					case 3: squire.Say( "This lock seems tougher than the others I've seen." ); break;
					case 2: squire.Say( "I don't think I can unlock this right now, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I think I need to train a bit more before we try this one." ); break;
					case 0: squire.Say( "I don't see how this lock can be manipulated." ); break;
				}
			}
			else if( dialog == SquireDialogTree.UnsuccessfulLockpick )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", let me try picking that again." ); break;
					case 4: squire.Emote( "*Was unable to pick the lock.*" ); break;
					case 3: squire.Say( "I couldn't pick this lock." ); break;
					case 2: squire.Say( "I wasn't successful in picking this lock, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I failed to pick this lock." ); break;
					case 0: squire.Say( "I was unable to pick this lock." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SuccessfulLockpick )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I've done it, " + squire.m_MasterNickname + ", the lock has been picked!" ); break;
					case 4: squire.Emote( "*Was able to pick the lock.*" ); break;
					case 3: squire.Say( "Have at the contents, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I was successful in picking this lock, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I've successfully picked the lock!" ); break;
					case 0: squire.Say( "The lock has yielded to my skill!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.HandsAreFull )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", my hands are full." ); break;
					case 4: squire.Emote( "*Hands are a little too full.*" ); break;
					case 3: squire.Say( "I need to empty my hands, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I've got my hands a little full here, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I need to the items in my hands away first." ); break;
					case 0: squire.Say( "My hands are a little full to steal." ); break;
				}
			}
			else if( dialog == SquireDialogTree.StealingNotAllowedHere )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I cannot do it, " + squire.m_MasterNickname + ", I cannot steal in this area!" ); break;
					case 4: squire.Emote( "*Is confused by the invisible force stopping them from stealing.*" ); break;
					case 3: squire.Say( "This area won't allow us to steal, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I can't steal in this area, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", we cannot steal here." ); break;
					case 0: squire.Say( "Stealing isn't allowed in this area." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotAPartOfThievesGuild )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", we need to join the thieves guild!" ); break;
					case 4: squire.Emote( "*Cannot steal from another player without first joining the thieves guild.*" ); break;
					case 3: squire.Say( "Join the thieves guild first, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "We cannot steal from players unless we're in the thieves guild, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", we need to join the thieves guild!" ); break;
					case 0: squire.Say( "We cannot steal from players because we are not a part of the thieves guild." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SuspendedFromThievesGuild )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't, " + squire.m_MasterNickname + ", we're suspended from the guild!" ); break;
					case 4: squire.Emote( "*Suspention from the guild stops them.*" ); break;
					case 3: squire.Say( "We cannot steal from players while suspended from the guild, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "Suspention is preventing us from stealing from other players, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", we cannot steal from players while suspended!" ); break;
					case 0: squire.Say( "We're currently on suspention in the thieves guild!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotStealFromVendors )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That is a vendor, " + squire.m_MasterNickname + ", we cannot steal from them!" ); break;
					case 4: squire.Emote( "*Refuses to steal from a vendor.*" ); break;
					case 3: squire.Say( "Vendors cannot be stolen from, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "We cannot steal from vendors, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", we cannot steal from vendors!" ); break;
					case 0: squire.Say( "We cannot steal from vendors!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotSeeStealingTarget )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't see it, " + squire.m_MasterNickname + ", maybe if you brought me closer?" ); break;
					case 4: squire.Emote( "*Unable to locate the item you want them to steal.*" ); break;
					case 3: squire.Say( "I cannot see it, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I'd have to be able to see what you want me to steal, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I cannot steal what I cannot see." ); break;
					case 0: squire.Say( "I cannot see that." ); break;
				}
			}
			else if( dialog == SquireDialogTree.FullBackpackStealing )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "My backpack is full, " + squire.m_MasterNickname + ", I cannot fit any more." ); break;
					case 4: squire.Emote( "*Cannot stuff any more items in their backpack.*" ); break;
					case 3: squire.Say( "My backpack cannot hold anymore, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "There is no more that I can fit in my backpack, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", my backpack is full." ); break;
					case 0: squire.Say( "My backpack cannot hold any more." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NeedToBeCloserToSteal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't steal it, " + squire.m_MasterNickname + ", I'm too far away." ); break;
					case 4: squire.Emote( "*Arms cannot reach that far.*" ); break;
					case 3: squire.Say( "I am too far away from the target to steal it, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I can't steal the target this far away, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", bring me closer to the target." ); break;
					case 0: squire.Say( "I need to be closer to the target to steal it." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotStealThat )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I cannot steal that, " + squire.m_MasterNickname + ", what do you expect from me?" ); break;
					case 4: squire.Emote( "*Is unable to steal that.*" ); break;
					case 3: squire.Say( "I can't steal that, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "What do you expect of me, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", that is not something I can steal!" ); break;
					case 0: squire.Say( "I cannot steal that!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotStealWhileMorphed )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't steal, " + squire.m_MasterNickname + ", not while I'm transformed." ); break;
					case 4: squire.Emote( "*is unable to steal while transformed.*" ); break;
					case 3: squire.Say( "This transformation prevents me from stealing, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I will be unable to steal until I transform back, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I cannot steal while I'm something or somebody else." ); break;
					case 0: squire.Say( "I cannot steal while I'm not myself!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotSkilledEnoughToStealItem )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm not skilled enough, " + squire.m_MasterNickname + ", I cannot steal that item." ); break;
					case 4: squire.Emote( "*Realizes they are not skilled enough to steal that item.*" ); break;
					case 3: squire.Say( "I cannot steal that item now, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "I need to train more, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I need to train more first." ); break;
					case 0: squire.Say( "I am not skilled enough to steal that item." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotStealFromTheirHands )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm not skilled enough, " + squire.m_MasterNickname + ", not skilled enough to steal directly from their hands." ); break;
					case 4: squire.Emote( "*Is unsure of their ability to steal straight from that person's hand.*" ); break;
					case 3: squire.Say( "They would certainly notice us, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "Perhaps something from their backpack instead, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I don't think I am skilled enough to take the item from their hands." ); break;
					case 0: squire.Say( "It would be foolish for us to attempt to steal from their hands." ); break;
				}
			}
			else if( dialog == SquireDialogTree.StealFromSelf )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I could just give it to you, " + squire.m_MasterNickname + ", I don't need to steal it from myself." ); break;
					case 4: squire.Emote( "*Is confused as to why they were told to steal their own item.*" ); break;
					case 3: squire.Say( "That's a little strange, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "Wouldn't you rather I just hand it to you, " + squire.m_MasterNickname + "?" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I've caught myself trying to steal!" ); break;
					case 0: squire.Say( "Aha! I've caught you! Wait..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooHeavyToSteal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That would be too heavy, " + squire.m_MasterNickname + ", I would not be able to move it." ); break;
					case 4: squire.Emote( "*Cannot move that item fast enough with how heavy it is.*" ); break;
					case 3: squire.Say( "That is too heavy to steal, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "Perhaps if I was stronger, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I wouldn't be able to move that fast enough." ); break;
					case 0: squire.Say( "That is too heavy to steal." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SuccessfulSteal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I've succeeded in stealing the item, " + squire.m_MasterNickname + ", we should run!" ); break;
					case 4: squire.Emote( "*Quickly pockets the item.*" ); break;
					case 3: squire.Say( "I've got it, " + squire.m_MasterNickname + "!" ); break;
					case 2: squire.Say( "I've succeeded, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I've succeeded in stealing!" ); break;
					case 0: squire.Say( "I've successfully stolen the item!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.UnsuccessfulSteal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I've failed to steal the item, " + squire.m_MasterNickname + ", are we gonna be alright?" ); break;
					case 4: squire.Emote( "*Was unable to liberate the item from its owner.*" ); break;
					case 3: squire.Say( "I've failed to take the item, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "I was unable to steal the item, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I've failed to steal the item." ); break;
					case 0: squire.Say( "I've failed to steal the item." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToLockpick )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Give me one moment, " + squire.m_MasterNickname + ", then I'll be ready to try again." ); break;
					case 4: squire.Emote( "*Sighs in defeat, needs another moment before trying again.*" ); break;
					case 3: squire.Say( "I can't pick the lock again right away, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "I need a little time, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", let me prepare myself first." ); break;
					case 0: squire.Say( "I need a moment." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToSteal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Give me one moment, " + squire.m_MasterNickname + ", then I'll be ready to try again." ); break;
					case 4: squire.Emote( "*Sighs in defeat, needs another moment before trying again.*" ); break;
					case 3: squire.Say( "I can't attempt to steal again right away, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "I need a little time, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", let me prepare myself first." ); break;
					case 0: squire.Say( "I need a moment." ); break;
				}
			}
			else if( dialog == SquireDialogTree.WhatShouldISteal )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Point me to the target, " + squire.m_MasterNickname + ", I'll steal it on your command." ); break;
					case 4: squire.Emote( "*Awaits stealing orders.*" ); break;
					case 3: squire.Say( "Point to the target, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "Point to the item, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", what's the target?" ); break;
					case 0: squire.Say( "What should I steal?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.LearnsFromContract )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That's great, " + squire.m_MasterNickname + ", I feel like I know so much more!" ); break;
					case 4: squire.Emote( "*Feels much more intelligent.*" ); break;
					case 3: squire.Say( "Thank you, " + squire.m_MasterNickname + ", I've learned a lot!" ); break;
					case 2: squire.Say( "I've learned so much, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I feel smarter!" ); break;
					case 0: squire.Say( "I feel more intelligent already!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.RefusesToLootPlayers ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "No can do, " + squire.m_MasterNickname + ", I would be going against the virtues." ); break;
					case 4: squire.Emote( "*Feels very uneasy about looting this person.*" ); break;
					case 3: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I simply cannot loot them." ); break;
					case 2: squire.Say( "This would be wrong, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I don't feel right looting this person." ); break;
					case 0: squire.Say( "I refuse to loot a player." ); break;
				}
			}
			else if( dialog == SquireDialogTree.UnequipsTwoHandedForShield ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Thanks, " + squire.m_MasterNickname + ", please take this two handed weapon back in exchange!" ); break;
					case 4: squire.Emote( "*Trades the two handed weapon they were using for the shield.*" ); break;
					case 3: squire.Say( "Thank you, " + squire.m_MasterNickname + ", please take this two handed weapon back." ); break;
					case 2: squire.Say( "With this shield, I can no longer use that two handed weapon you gave me, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I'm no longer using that two handed weapon you gave me." ); break;
					case 0: squire.Say( "I've removed the two handed weapon I was holding." ); break;
				}
			}
			else if( dialog == SquireDialogTree.FirstHandMissing ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Sorry, " + squire.m_MasterNickname + ", I seem to have lost the item!" ); break;
					case 4: squire.Emote( "*Rifles through their backpack unable to locate the item.*" ); break;
					case 3: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I seem to have lost the item you want me to hold." ); break;
					case 2: squire.Say( "Where did I put it, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I can't seem to find the first item I'm to hold." ); break;
					case 0: squire.Say( "I can't seem to find what I'm supposed to be holding in my first hand..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SecondHandMissing ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Sorry, " + squire.m_MasterNickname + ", I seem to have lost the item!" ); break;
					case 4: squire.Emote( "*Rifles through their backpack unable to locate the item.*" ); break;
					case 3: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I seem to have lost the item you want me to hold." ); break;
					case 2: squire.Say( "Where did I put it, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I can't seem to find the item I'm to hold." ); break;
					case 0: squire.Say( "I can't seem to find what I'm supposed to be holding in my second hand..." ); break;
				}
			}
			else if( dialog == SquireDialogTree.EmptyHands ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Okay, " + squire.m_MasterNickname + ", I'll go barehanded!" ); break;
					case 4: squire.Emote( "*Clenches fists, punching the air before them.*" ); break;
					case 3: squire.Say( "Alright, " + squire.m_MasterNickname + ", I'll fight empty handed." ); break;
					case 2: squire.Say( "Bare handed I go, " + squire.m_MasterNickname + "!" ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I suppose wrestling will be my skill then?" ); break;
					case 0: squire.Say( "Guess I'm going empty handed!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.SuccessfulSetCreation ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Okay, " + squire.m_MasterNickname + ", I'll fight with bravery!" ); break;
					case 4: squire.Emote( "*Grips equipment accepting their place.*" ); break;
					case 3: squire.Say( "Alright, " + squire.m_MasterNickname + ", I'll fight with these." ); break;
					case 2: squire.Say( "Weapon set created, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I will use these weapons bravely." ); break;
					case 0: squire.Say( "Alright, set created!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.Unarmed ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Okay, " + squire.m_MasterNickname + ", I've unarmed myself." ); break;
					case 4: squire.Emote( "*Places equipment away in their backpack.*" ); break;
					case 3: squire.Say( "Alright, " + squire.m_MasterNickname + ", I've stored my equipment." ); break;
					case 2: squire.Say( "Equipment has been stored, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I have unequipped myself." ); break;
					case 0: squire.Say( "Alright, I've put the items I was holding in my backpack." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SpiritSpeakSuccess ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Okay, " + squire.m_MasterNickname + ", let's hear the spirits." ); break;
					case 4: squire.Emote( "*Humms a mantra as they begin communing with spirits.*" ); break;
					case 3: squire.Say( "So, " + squire.m_MasterNickname + ", I can talk to spirits." ); break;
					case 2: squire.Say( "I've done it, " + squire.m_MasterNickname + ", I've channeled the spirit world." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I can now commune with ghosts." ); break;
					case 0: squire.Say( "I've successfully channeled the spirit world." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SpiritSpeakFail ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'm sorry, " + squire.m_MasterNickname + ", I cannot hear the spirits." ); break;
					case 4: squire.Emote( "*Grimaces as they fail to channel the spirit world.*" ); break;
					case 3: squire.Say( "Sorry, " + squire.m_MasterNickname + ", I cannot speak with spirits." ); break;
					case 2: squire.Say( "I have failed, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( squire.m_MasterNickname + ", I am unable to commune with ghosts." ); break;
					case 0: squire.Say( "I've failed to channel the spirit world." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToSpiritSpeak ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't channel again so soon." ); break;
					case 4: squire.Emote( "*Attempts to channel the spirits but fails.*" ); break;
					case 3: squire.Say( "My connection with the spirit world is weak." ); break;
					case 2: squire.Say( "I cannot attempt to channel spirits again this soon, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( "The channels are weak, I need time." ); break;
					case 0: squire.Say( "It is too soon to try to commune with spirits again, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SpiritChannelFades ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can no longer talk to the spirits." ); break;
					case 4: squire.Emote( "*Feels their connection with the spirits fade.*" ); break;
					case 3: squire.Say( "The channel I had to the spirit world has faded." ); break;
					case 2: squire.Say( "My connection to the spirit world is gone, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( "My communion with the spirit world has ended." ); break;
					case 0: squire.Say( "My channel to the spirit world is fading, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.StillConnectedToSpirits ) 
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I am still able to talk to the spirits." ); break;
					case 4: squire.Emote( "*Is still connected to the spirits.*" ); break;
					case 3: squire.Say( "The channel I had to the spirit world is currently active." ); break;
					case 2: squire.Say( "My connection to the spirit world is still active, " + squire.m_MasterNickname + "." ); break;
					case 1: squire.Say( "My communion with the spirit world is still channelled." ); break;
					case 0: squire.Say( "My channel to the spirit world is still in tact, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.OpenQuiver )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Look at the contents of my quiver if you want." ); break;
					case 4: squire.Emote( "*Proudly opens their quiver to you.*" ); break;
					case 3: squire.Say( "I am sure your backpack's contents are more interesting than my quiver." ); break;
					case 2: squire.Say( "Here is what your little " + squire.m_SquireNickname + " is holding." ); break;
					case 1: squire.Say( "Y-you'd like to see my what? Oh, my quiver." ); break;
					case 0: squire.Say( "Here is what is in my quiver." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotAQuiver )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Are you okay, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Briefly looks over their shoulder at their back.*" ); break;
					case 3: squire.Say( "I do not believe I am wearing a quiver, " + squire.m_MasterNickname + "." ); break;
					case 2: squire.Say( "There is no quiver on my back." ); break;
					case 1: squire.Say( "I apologize, " + squire.m_MasterNickname + ", but I am not holding a quiver." ); break;
					case 0: squire.Say( "I am not wearing a quiver, though." ); break;
				}
			}
			else if( dialog == SquireDialogTree.PoisonToApply )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "What poison should be used, " + squire.m_MasterNickname + "?" ); break;
					case 4: squire.Emote( "*Is ready for you to choose the poison for them to use.*" ); break;
					case 3: squire.Say( "Which bottle should I use, " + squire.m_MasterNickname + "?" ); break;
					case 2: squire.Say( "Which poison should I use?" ); break;
					case 1: squire.Say( "What poison do you want me to use, " + squire.m_MasterNickname + "?" ); break;
					case 0: squire.Say( "What poison should I use?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.ApplyPoisonTo )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "What needs some poison, " + squire.m_MasterNickname + "?" ); break;
					case 4: squire.Emote( "*Readies their poison.*" ); break;
					case 3: squire.Say( "What would you have me poison, " + squire.m_MasterNickname + "?" ); break;
					case 2: squire.Say( "What is it I should poison?" ); break;
					case 1: squire.Say( "What should I poison, " + squire.m_MasterNickname + "?" ); break;
					case 0: squire.Say( "What would you like me to apply the poison to?" ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotAPoisonPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "That is not poison, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Is confused by that not being poison.*" ); break;
					case 3: squire.Say( "I see no poison here." ); break;
					case 2: squire.Say( "That doesn't appear to be poison." ); break;
					case 1: squire.Say( "Are you sure that's poison, " + squire.m_MasterNickname + "?" ); break;
					case 0: squire.Say( "That is not a poison potion." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooFarToPoison )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "You're a little too far for that, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Eyes the distance between you two.*" ); break;
					case 3: squire.Say( "I cannot poison something so far away." ); break;
					case 2: squire.Say( "Your backpack is a little too far away for me to reach." ); break;
					case 1: squire.Say( "Could you come closer, " + squire.m_MasterNickname + "?" ); break;
					case 0: squire.Say( "You're too far away for me to use that." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotPoisonNotInfectious )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I cannot poison that, it is not infectious, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Is unable to poison that item.*" ); break;
					case 3: squire.Say( "It is impossible for me to poison that." ); break;
					case 2: squire.Say( "I cannot poison something that cannot be infected." ); break;
					case 1: squire.Say( "That is not infectious, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I am sorry, " + squire.m_MasterNickname + ", but I cannot poison something that is not infectious." ); break;
				}
			}
			else if( dialog == SquireDialogTree.CannotPoisonNotBPFoD )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I cannot poison that, it is not bladed, piercing, food or drink, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Is unable to poison that item.*" ); break;
					case 3: squire.Say( "It is impossible for me to poison that." ); break;
					case 2: squire.Say( "I cannot poison something that is not bladed, piercing, food or drink." ); break;
					case 1: squire.Say( "That is not bladed, piercing, food or drink, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I am sorry, " + squire.m_MasterNickname + ", but I cannot poison something that is not bladed, piercing, or food or drink." ); break;
				}
			}
			else if( dialog == SquireDialogTree.PoisoningSuccess )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It was a success, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Grins as their poisoning succeeds.*" ); break;
					case 3: squire.Say( "The item has been poisoned." ); break;
					case 2: squire.Say( "Poisoning has happened successfully." ); break;
					case 1: squire.Say( "I've applied the poison, " + squire.m_MasterNickname + "!" ); break;
					case 0: squire.Say( "I've succeeded in applying the poison!" ); break;
				}
			}
			else if( dialog == SquireDialogTree.PoisoningFailure )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "It was a failure, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Grimaces as they fail their poisoning.*" ); break;
					case 3: squire.Say( "The item has not been poisoned." ); break;
					case 2: squire.Say( "Poisoning has failed successfully." ); break;
					case 1: squire.Say( "I've failed to applied the poison, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I have failed to apply a sufficient amount of poison." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TerribleMistake )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I have made a terrible mistake, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Grimaces as they poison themselves.*" ); break;
					case 3: squire.Say( "I got some in my mouth!" ); break;
					case 2: squire.Say( "I need a cure potion quickly!" ); break;
					case 1: squire.Say( "I've failed, and made a terrible mistake, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I have made a terrible mistake while applying this poison." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoAnkhNearby )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I'll need to be a little more closer to an ankh in order to tithe, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Wonders if they're expected to tithe their gold to the ground.*" ); break;
					case 3: squire.Say( "I don't seem to be near an ankh." ); break;
					case 2: squire.Say( "Sorry, I need to be closer to an ankh in order to tithe my gold." ); break;
					case 1: squire.Say( "I am not close enough to an ankh, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I must be near an ankh to tithe." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TitheSuccess )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Emote( "*Lays their gold before the ankh, praying for good fortune.*" ); break;
					case 4: squire.Emote( "*Tithes gold as a sign of devotion.*" ); break;
					case 3: squire.Emote( "*Prays to the shrine, tithing their gold.*" ); break;
					case 2: squire.Emote( "*Tithes their gold to the ankh.*" ); break;
					case 1: squire.Emote( "*Prays for good fortune as they tithe their gold.*" ); break;
					case 0: squire.Emote( "*Mutters a prayer as they tithe their gold.*" ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoGoldToTithe )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I will require more gold to tithe, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Checks their backpack thuroughly for gold to tithe.*" ); break;
					case 3: squire.Say( "There is no gold in my backpack which I may tithe." ); break;
					case 2: squire.Say( "Sorry, in order to tithe gold, first I must have gold to tithe." ); break;
					case 1: squire.Say( "May I have some funds to tithe, " + squire.m_MasterNickname + "?" ); break;
					case 0: squire.Say( "I lack funds." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotEnoughTithe )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I am too low on tithing points, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Is disappointed in their lack of tithing points.*" ); break;
					case 3: squire.Say( "I might not be devoted enough to cast this, I will need to tithe more." ); break;
					case 2: squire.Say( "Sorry, I lack the tithing points required to cast this." ); break;
					case 1: squire.Say( "I will need more tithe points to cast that, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I am too low on tithing points to cast this." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotEnoughMana )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I am too low on mana, " + squire.m_MasterNickname + "." ); break;
					case 4: squire.Emote( "*Lacks the mana required to cast this.*" ); break;
					case 3: squire.Say( "I am low on mana." ); break;
					case 2: squire.Say( "Sorry, I lack the mana required to cast this." ); break;
					case 1: squire.Say( "I will need more mana to cast that, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I am too low on mana to cast this." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoChivalryBook )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I would struggle casting that, " + squire.m_MasterNickname + ", I need a chivalry, Bushido or Necro book." ); break;
					case 4: squire.Emote( "*Does not have a chivalry, Bushido or Necro book.*" ); break;
					case 3: squire.Say( "Will need a chivalry, Bushido or Necro book to cast this." ); break;
					case 2: squire.Say( "Sorry, I don't seem to have a book of chivalry on me." ); break;
					case 1: squire.Say( "I am unfamiliar with that spell, " + squire.m_MasterNickname + ", I will need a chivalry, Bushido or Necro book." ); break;
					case 0: squire.Say( "I will need a book of chivalry, Bushido or Necro to cast that." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToMeditate )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I cannot concentrate on meditating again this soon." ); break;
					case 4: squire.Emote( "*Becomes too infuriated to meditate.*" ); break;
					case 3: squire.Say( "Give me some time, please." ); break;
					case 2: squire.Say( "I need a moment." ); break;
					case 1: squire.Say( "I will not attempt to meditate again this soon, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "It's a little too soon for me to attempt meditating, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.TooSoonToCastASpell )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I can't concentrate on casting another spell this soon." ); break;
					case 4: squire.Emote( "*Cannot concentrate on casting another spell this soon.*" ); break;
					case 3: squire.Say( "Give me some time, please." ); break;
					case 2: squire.Say( "I need a moment." ); break;
					case 1: squire.Say( "I will not attempt to cast a spell again this soon, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "It's a little too soon for me to attempt casting another spell, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NotEnoughSpellSkill )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I lack the knowledge to cast this." ); break;
					case 4: squire.Emote( "*Is not confident in their ability to cast this spell.*" ); break;
					case 3: squire.Say( "I am not confident in my ability to cast this yet." ); break;
					case 2: squire.Say( "I lack the knowledge to cast this yet." ); break;
					case 1: squire.Say( "I will need to get better at this skill first, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I am not skilled enough to cast that yet, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.NoExplosionPotion )
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "I don't seem to have one." ); break;
					case 4: squire.Emote( "*Checks their backpack for an explosion potion.*" ); break;
					case 3: squire.Say( "It's hard for me to throw what I don't have." ); break;
					case 2: squire.Say( "Do you have an explosion potion for me?" ); break;
					case 1: squire.Say( "I might need an explosion potion to throw, " + squire.m_MasterNickname + "." ); break;
					case 0: squire.Say( "I don't seem to have an explosion potion, " + squire.m_MasterNickname + "." ); break;
				}
			}
			else if( dialog == SquireDialogTree.SquireHasANewTeam ) // Added 1.9.7
			{
				switch( Utility.Random( 6 ) )
				{
					case 5: squire.Say( "Let me try screaming it... GO " + squire.m_SquireTeam.ToUpper() + "!" ); squire.Say( "Yeah, that works!" ); break;
					case 4: squire.Emote( "*Is more than excited to be a part of " + squire.m_SquireTeam + "!*" ); break;
					case 3: squire.Say( squire.m_SquireTeam + ", huh? I will try to get along with them!" ); break;
					case 2: squire.Say( "I am certain I will get used to the team " + squire.m_SquireTeam + "." ); break;
					case 1: squire.Say( squire.m_SquireTeam + ", let's do this!" ); break;
					case 0: squire.Say( "Alright, I will belong to " + squire.m_SquireTeam + " from now on." ); break;
				}
			}
		}
	}
}