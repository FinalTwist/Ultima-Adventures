using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Research;
using Server.Commands;
using Server.Misc;

namespace Server.Scripts.Commands
{
	public class ResearchCommands
	{
		public static void Initialize()
		{
            Properties.Initialize();

			Register( "CastConjure", AccessLevel.Player, new CommandEventHandler( CastConjure_OnCommand ) );
			Register( "CastDeathSpeak", AccessLevel.Player, new CommandEventHandler( CastDeathSpeak_OnCommand ) );
			Register( "CastSneak", AccessLevel.Player, new CommandEventHandler( CastSneak_OnCommand ) );
			Register( "CastCreateFire", AccessLevel.Player, new CommandEventHandler( CastCreateFire_OnCommand ) );
			Register( "CastElectricalElemental", AccessLevel.Player, new CommandEventHandler( CastElectricalElemental_OnCommand ) );
			Register( "CastConfusionBlast", AccessLevel.Player, new CommandEventHandler( CastConfusionBlast_OnCommand ) );
			Register( "CastSeeTruth", AccessLevel.Player, new CommandEventHandler( CastSeeTruth_OnCommand ) );
			Register( "CastIcicle", AccessLevel.Player, new CommandEventHandler( CastIcicle_OnCommand ) );
			Register( "CastExtinguish", AccessLevel.Player, new CommandEventHandler( CastExtinguish_OnCommand ) );
			Register( "CastRockFlesh", AccessLevel.Player, new CommandEventHandler( CastRockFlesh_OnCommand ) );
			Register( "CastMassMight", AccessLevel.Player, new CommandEventHandler( CastMassMight_OnCommand ) );
			Register( "CastEndureCold", AccessLevel.Player, new CommandEventHandler( CastEndureCold_OnCommand ) );
			Register( "CastWeedElemental", AccessLevel.Player, new CommandEventHandler( CastWeedElemental_OnCommand ) );
			Register( "CastSpawnCreature", AccessLevel.Player, new CommandEventHandler( CastSpawnCreature_OnCommand ) );
			Register( "CastHealingTouch", AccessLevel.Player, new CommandEventHandler( CastHealingTouch_OnCommand ) );
			Register( "CastSnowBall", AccessLevel.Player, new CommandEventHandler( CastSnowBall_OnCommand ) );
			Register( "CastClone", AccessLevel.Player, new CommandEventHandler( CastClone_OnCommand ) );
			Register( "CastGrantPeace", AccessLevel.Player, new CommandEventHandler( CastGrantPeace_OnCommand ) );
			Register( "CastSleep", AccessLevel.Player, new CommandEventHandler( CastSleep_OnCommand ) );
			Register( "CastEndureHeat", AccessLevel.Player, new CommandEventHandler( CastEndureHeat_OnCommand ) );
			Register( "CastIceElemental", AccessLevel.Player, new CommandEventHandler( CastIceElemental_OnCommand ) );
			Register( "CastEtherealTravel", AccessLevel.Player, new CommandEventHandler( CastEtherealTravel_OnCommand ) );
			Register( "CastWizardEye", AccessLevel.Player, new CommandEventHandler( CastWizardEye_OnCommand ) );
			Register( "CastFrostField", AccessLevel.Player, new CommandEventHandler( CastFrostField_OnCommand ) );
			Register( "CastCreateGold", AccessLevel.Player, new CommandEventHandler( CastCreateGold_OnCommand ) );
			Register( "CastAnimateBones", AccessLevel.Player, new CommandEventHandler( CastAnimateBones_OnCommand ) );
			Register( "CastCauseFear", AccessLevel.Player, new CommandEventHandler( CastCauseFear_OnCommand ) );
			Register( "CastIgnite", AccessLevel.Player, new CommandEventHandler( CastIgnite_OnCommand ) );
			Register( "CastMudElemental", AccessLevel.Player, new CommandEventHandler( CastMudElemental_OnCommand ) );
			Register( "CastBanishDaemon", AccessLevel.Player, new CommandEventHandler( CastBanishDaemon_OnCommand ) );
			Register( "CastFadefromSight", AccessLevel.Player, new CommandEventHandler( CastFadefromSight_OnCommand ) );
			Register( "CastGasCloud", AccessLevel.Player, new CommandEventHandler( CastGasCloud_OnCommand ) );
			Register( "CastSwarm", AccessLevel.Player, new CommandEventHandler( CastSwarm_OnCommand ) );
			Register( "CastMaskofDeath", AccessLevel.Player, new CommandEventHandler( CastMaskofDeath_OnCommand ) );
			Register( "CastEnchant", AccessLevel.Player, new CommandEventHandler( CastEnchant_OnCommand ) );
			Register( "CastFlameBolt", AccessLevel.Player, new CommandEventHandler( CastFlameBolt_OnCommand ) );
			Register( "CastPoisonElemental", AccessLevel.Player, new CommandEventHandler( CastPoisonElemental_OnCommand ) );
			Register( "CastCallDestruction", AccessLevel.Player, new CommandEventHandler( CastCallDestruction_OnCommand ) );
			Register( "CastDivination", AccessLevel.Player, new CommandEventHandler( CastDivination_OnCommand ) );
			Register( "CastFrostStrike", AccessLevel.Player, new CommandEventHandler( CastFrostStrike_OnCommand ) );
			Register( "CastMagicSteed", AccessLevel.Player, new CommandEventHandler( CastMagicSteed_OnCommand ) );
			Register( "CastCreateGolem", AccessLevel.Player, new CommandEventHandler( CastCreateGolem_OnCommand ) );
			Register( "CastSleepField", AccessLevel.Player, new CommandEventHandler( CastSleepField_OnCommand ) );
			Register( "CastConflagration", AccessLevel.Player, new CommandEventHandler( CastConflagration_OnCommand ) );
			Register( "CastGemElemental", AccessLevel.Player, new CommandEventHandler( CastGemElemental_OnCommand ) );
			Register( "CastMeteorShower", AccessLevel.Player, new CommandEventHandler( CastMeteorShower_OnCommand ) );
			Register( "CastIntervention", AccessLevel.Player, new CommandEventHandler( CastIntervention_OnCommand ) );
			Register( "CastHailStorm", AccessLevel.Player, new CommandEventHandler( CastHailStorm_OnCommand ) );
			Register( "CastAerialServant", AccessLevel.Player, new CommandEventHandler( CastAerialServant_OnCommand ) );
			Register( "CastOpenGround", AccessLevel.Player, new CommandEventHandler( CastOpenGround_OnCommand ) );
			Register( "CastCharm", AccessLevel.Player, new CommandEventHandler( CastCharm_OnCommand ) );
			Register( "CastExplosion", AccessLevel.Player, new CommandEventHandler( CastExplosion_OnCommand ) );
			Register( "CastAcidElemental", AccessLevel.Player, new CommandEventHandler( CastAcidElemental_OnCommand ) );
			Register( "CastInvokeDevil", AccessLevel.Player, new CommandEventHandler( CastInvokeDevil_OnCommand ) );
			Register( "CastAirWalk", AccessLevel.Player, new CommandEventHandler( CastAirWalk_OnCommand ) );
			Register( "CastAvalanche", AccessLevel.Player, new CommandEventHandler( CastAvalanche_OnCommand ) );
			Register( "CastDeathVortex", AccessLevel.Player, new CommandEventHandler( CastDeathVortex_OnCommand ) );
			Register( "CastWithstandDeath", AccessLevel.Player, new CommandEventHandler( CastWithstandDeath_OnCommand ) );
			Register( "CastMassSleep", AccessLevel.Player, new CommandEventHandler( CastMassSleep_OnCommand ) );
			Register( "CastRingofFire", AccessLevel.Player, new CommandEventHandler( CastRingofFire_OnCommand ) );
			Register( "CastBloodElemental", AccessLevel.Player, new CommandEventHandler( CastBloodElemental_OnCommand ) );
			Register( "CastDevastation", AccessLevel.Player, new CommandEventHandler( CastDevastation_OnCommand ) );
			Register( "CastRestoration", AccessLevel.Player, new CommandEventHandler( CastRestoration_OnCommand ) );
			Register( "CastMassDeath", AccessLevel.Player, new CommandEventHandler( CastMassDeath_OnCommand ) );
		}

	    public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		public static bool CanCast( Mobile from, int spell )
		{
			if ( ResearchBarSettings.ResearchMaterials( from ) == null )
				return false;

			if ( !ResearchBarSettings.HasSpell( from, spell ) )
				return false;

			if ( !Multis.DesignContext.Check( from ) )
				return false; // They are customizing

			return true;
		}

		[Usage( "CastConjure" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastConjure_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 1 ) ){ new ResearchConjure( e.Mobile, null ).Cast(); } }

		[Usage( "CastDeathSpeak" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastDeathSpeak_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 2 ) ){ new ResearchDeathSpeak( e.Mobile, null ).Cast(); } }

		[Usage( "CastSneak" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSneak_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 3 ) ){ new ResearchSneak( e.Mobile, null ).Cast(); } }

		[Usage( "CastCreateFire" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastCreateFire_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 4 ) ){ new ResearchCreateFire( e.Mobile, null ).Cast(); } }

		[Usage( "CastElectricalElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastElectricalElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 5 ) ){ new ResearchSummonElectricalElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastConfusionBlast" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastConfusionBlast_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 6 ) ){ new ResearchConfusionBlast( e.Mobile, null ).Cast(); } }

		[Usage( "CastSeeTruth" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSeeTruth_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 7 ) ){ new ResearchSeeTruth( e.Mobile, null ).Cast(); } }

		[Usage( "CastIcicle" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastIcicle_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 8 ) ){ new ResearchIcicle( e.Mobile, null ).Cast(); } }

		[Usage( "CastExtinguish" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastExtinguish_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 9 ) ){ new ResearchExtinguish( e.Mobile, null ).Cast(); } }

		[Usage( "CastRockFlesh" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastRockFlesh_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 10 ) ){ new ResearchRockFlesh( e.Mobile, null ).Cast(); } }

		[Usage( "CastMassMight" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMassMight_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 11 ) ){ new ResearchMassMight( e.Mobile, null ).Cast(); } }

		[Usage( "CastEndureCold" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastEndureCold_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 12 ) ){ new ResearchEndureCold( e.Mobile, null ).Cast(); } }

		[Usage( "CastWeedElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastWeedElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 13 ) ){ new ResearchSummonWeedElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastSpawnCreature" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSpawnCreature_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 14 ) ){ new ResearchSummonCreature( e.Mobile, null ).Cast(); } }

		[Usage( "CastHealingTouch" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastHealingTouch_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 15 ) ){ new ResearchHealingTouch( e.Mobile, null ).Cast(); } }

		[Usage( "CastSnowBall" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSnowBall_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 16 ) ){ new ResearchSnowBall( e.Mobile, null ).Cast(); } }

		[Usage( "CastClone" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastClone_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 17 ) ){ new ResearchClone( e.Mobile, null ).Cast(); } }

		[Usage( "CastGrantPeace" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastGrantPeace_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 18 ) ){ new ResearchGrantPeace( e.Mobile, null ).Cast(); } }

		[Usage( "CastSleep" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSleep_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 19 ) ){ new ResearchSleep( e.Mobile, null ).Cast(); } }

		[Usage( "CastEndureHeat" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastEndureHeat_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 20 ) ){ new ResearchEndureHeat( e.Mobile, null ).Cast(); } }

		[Usage( "CastIceElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastIceElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 21 ) ){ new ResearchSummonIceElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastEtherealTravel" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastEtherealTravel_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 22 ) ){ new ResearchEtherealTravel( e.Mobile, null ).Cast(); } }

		[Usage( "CastWizardEye" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastWizardEye_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 23 ) ){ new ResearchWizardEye( e.Mobile, null ).Cast(); } }

		[Usage( "CastFrostField" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastFrostField_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 24 ) ){ new ResearchFrostField( e.Mobile, null ).Cast(); } }

		[Usage( "CastCreateGold" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastCreateGold_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 25 ) ){ new ResearchCreateGold( e.Mobile, null ).Cast(); } }

		[Usage( "CastAnimateBones" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastAnimateBones_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 26 ) ){ new ResearchSummonDead( e.Mobile, null ).Cast(); } }

		[Usage( "CastCauseFear" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastCauseFear_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 27 ) ){ new ResearchCauseFear( e.Mobile, null ).Cast(); } }

		[Usage( "CastIgnite" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastIgnite_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 28 ) ){ new ResearchIgnite( e.Mobile, null ).Cast(); } }

		[Usage( "CastMudElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMudElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 29 ) ){ new ResearchSummonMudElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastBanishDaemon" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastBanishDaemon_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 30 ) ){ new ResearchBanishDaemon( e.Mobile, null ).Cast(); } }

		[Usage( "CastFadefromSight" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastFadefromSight_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 31 ) ){ new ResearchFadefromSight( e.Mobile, null ).Cast(); } }

		[Usage( "CastGasCloud" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastGasCloud_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 32 ) ){ new ResearchGasCloud( e.Mobile, null ).Cast(); } }

		[Usage( "CastSwarm" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSwarm_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 33 ) ){ new ResearchSwarm( e.Mobile, null ).Cast(); } }

		[Usage( "CastMaskofDeath" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMaskofDeath_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 34 ) ){ new ResearchMaskofDeath( e.Mobile, null ).Cast(); } }

		[Usage( "CastEnchant" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastEnchant_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 35 ) ){ new ResearchEnchant( e.Mobile, null ).Cast(); } }

		[Usage( "CastFlameBolt" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastFlameBolt_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 36 ) ){ new ResearchFlameBolt( e.Mobile, null ).Cast(); } }

		[Usage( "CastGemElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastGemElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 37 ) ){ new ResearchSummonGemElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastCallDestruction" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastCallDestruction_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 38 ) ){ new ResearchCallDestruction( e.Mobile, null ).Cast(); } }

		[Usage( "CastDivination" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastDivination_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 39 ) ){ new ResearchDivination( e.Mobile, null ).Cast(); } }

		[Usage( "CastFrostStrike" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastFrostStrike_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 40 ) ){ new ResearchFrostStrike( e.Mobile, null ).Cast(); } }

		[Usage( "CastMagicSteed" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMagicSteed_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 41 ) ){ new ResearchMagicSteed( e.Mobile, null ).Cast(); } }

		[Usage( "CastCreateGolem" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastCreateGolem_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 42 ) ){ new ResearchCreateGolem( e.Mobile, null ).Cast(); } }

		[Usage( "CastSleepField" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastSleepField_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 43 ) ){ new ResearchSleepField( e.Mobile, null ).Cast(); } }

		[Usage( "CastConflagration" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastConflagration_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 44 ) ){ new ResearchConflagration( e.Mobile, null ).Cast(); } }

		[Usage( "CastAcidElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastAcidElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 45 ) ){ new ResearchSummonAcidElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastMeteorShower" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMeteorShower_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 46 ) ){ new ResearchMeteorShower( e.Mobile, null ).Cast(); } }

		[Usage( "CastIntervention" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastIntervention_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 47 ) ){ new ResearchIntervention( e.Mobile, null ).Cast(); } }

		[Usage( "CastHailStorm" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastHailStorm_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 48 ) ){ new ResearchHailStorm( e.Mobile, null ).Cast(); } }

		[Usage( "CastAerialServant" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastAerialServant_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 49 ) ){ new ResearchAerialServant( e.Mobile, null ).Cast(); } }

		[Usage( "CastOpenGround" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastOpenGround_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 50 ) ){ new ResearchOpenGround( e.Mobile, null ).Cast(); } }

		[Usage( "CastCharm" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastCharm_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 51 ) ){ new ResearchCharm( e.Mobile, null ).Cast(); } }

		[Usage( "CastExplosion" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastExplosion_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 52 ) ){ new ResearchExplosion( e.Mobile, null ).Cast(); } }

		[Usage( "CastPoisonElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastPoisonElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 53 ) ){ new ResearchSummonPoisonElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastInvokeDevil" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastInvokeDevil_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 54 ) ){ new ResearchSummonDevil( e.Mobile, null ).Cast(); } }

		[Usage( "CastAirWalk" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastAirWalk_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 55 ) ){ new ResearchAirWalk( e.Mobile, null ).Cast(); } }

		[Usage( "CastAvalanche" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastAvalanche_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 56 ) ){ new ResearchAvalanche( e.Mobile, null ).Cast(); } }

		[Usage( "CastDeathVortex" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastDeathVortex_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 57 ) ){ new ResearchDeathVortex( e.Mobile, null ).Cast(); } }

		[Usage( "CastWithstandDeath" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastWithstandDeath_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 58 ) ){ new ResearchWithstandDeath( e.Mobile, null ).Cast(); } }

		[Usage( "CastMassSleep" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMassSleep_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 59 ) ){ new ResearchMassSleep( e.Mobile, null ).Cast(); } }

		[Usage( "CastRingofFire" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastRingofFire_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 60 ) ){ new ResearchRingofFire( e.Mobile, null ).Cast(); } }

		[Usage( "CastBloodElemental" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastBloodElemental_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 61 ) ){ new ResearchSummonBloodElemental( e.Mobile, null ).Cast(); } }

		[Usage( "CastDevastation" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastDevastation_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 62 ) ){ new ResearchDevastation( e.Mobile, null ).Cast(); } }

		[Usage( "CastRestoration" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastRestoration_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 63 ) ){ new ResearchRestoration( e.Mobile, null ).Cast(); } }

		[Usage( "CastMassDeath" )]
		[Description( "Casts the Ancient Researched Spell" )]
		public static void CastMassDeath_OnCommand( CommandEventArgs e ){ if ( CanCast( e.Mobile, 64 ) ){ new ResearchMassDeath( e.Mobile, null ).Cast(); } }
	}
}
