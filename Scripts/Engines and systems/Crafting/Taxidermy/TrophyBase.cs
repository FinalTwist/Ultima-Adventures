using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;
using Server.Targeting;

namespace Server.Items
{
	[Flipable(0x3942, 0x3943)]
	public class TrophyBase : Item
	{
		[Constructable]
		public TrophyBase() : base( 0x3942 )
		{
			Weight = 2.0;
			Name = "Trophy Base";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Double Click For Information");
            list.Add( 1049644, "Single Click To Use");
        }

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new TrophyGump( from, this ) );
		} 

		public class TrophyGump : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private TrophyBase m_Trophy;
			
			public TrophyGump( Mobile from, TrophyBase trophy ) : base( 6132, 3 )
			{
				m_Mobile = from;
				m_Trophy = trophy;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					m_Mobile.SendMessage("What corpse do you want to have mounted?");
					m_Mobile.Target = new CorpseTarget( m_Trophy );
				}
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump(new SpeechGump( "The Trophies of War", SpeechFunctions.SpeechText( from.Name, from.Name, "Trophy" ) ));
		}

		private class CorpseTarget : Target
		{
			private TrophyBase m_Trophy;

			public CorpseTarget( TrophyBase boards ) : base( 3, false, TargetFlags.None )
			{
				m_Trophy = boards;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Trophy.Deleted )
					return;

				if ( targeted is Fish || targeted is BigFish )
				{
					Item fish = (Item)targeted;
					Item trophy = new MountedTrophyHead();
					trophy.Name = "mounted fish";
					trophy.ItemID = 0x1E69;
					from.AddToBackpack( trophy );
					from.SendMessage("You mount the fish on the base.");
					fish.Delete();
					m_Trophy.Delete();
				}
				else if ( targeted is NewFish )
				{
					Item fish = (Item)targeted;
					Item trophy = new MountedTrophyHead();
					trophy.Hue = fish.Hue;
					trophy.Name = "mounted " + fish.Name;
					trophy.ItemID = 0x44E8;
					from.AddToBackpack( trophy );
					from.SendMessage("You mount the fish on the base.");
					fish.Delete();
					m_Trophy.Delete();
				}
				else if ( !(targeted is Corpse) )
				{
					from.SendMessage("That cannot be mounted as a trophy!");
                    return;
				}
				else
				{
					Region reg = Region.Find( from.Location, from.Map );

					object obj = targeted;

					if ( obj is Corpse )
						obj = ((Corpse)obj).Owner;

                    if ( obj != null )
                    {
						Corpse c = (Corpse)targeted;

						if ( c.VisitedByTaxidermist == true )
						{
							from.SendMessage("This has already been claimed as a trophy!");
							return;
						}
						else
						{
							string trophyName = c.Name;
								if ( c.m_Owner.Title != "" ){ trophyName = trophyName + " " + c.m_Owner.Title; }
							int trophyColor = c.Hue;
							int trophyID = 0;

							if ( ( typeof( AncientWyrm ) == c.Owner.GetType() ) || 
							( typeof( CaddelliteDragon ) == c.Owner.GetType() ) || 
							( typeof( DragonKing ) == c.Owner.GetType() ) || 
							( typeof( SlasherOfVoid ) == c.Owner.GetType() ) || 
							( typeof( VolcanicDragon ) == c.Owner.GetType() ) || 
							( typeof( AshDragon ) == c.Owner.GetType() ) || 
							( typeof( BottleDragon ) == c.Owner.GetType() ) || 
							( typeof( RadiationDragon ) == c.Owner.GetType() ) || 
							( typeof( CrystalDragon ) == c.Owner.GetType() ) || 
							( typeof( VoidDragon ) == c.Owner.GetType() ) || 
							( typeof( ElderDragon ) == c.Owner.GetType() ) || 
							( typeof( DeepSeaDragon ) == c.Owner.GetType() ) || 
							( typeof( ShadowWyrm ) == c.Owner.GetType() ) || 
							( typeof( ZombieDragon ) == c.Owner.GetType() ) )
							{
								trophyID = 0x21FB;
								if (typeof( ShadowWyrm ) == c.Owner.GetType() ){ trophyColor = 0x966; }
								else if (typeof( DragonKing ) == c.Owner.GetType() ){ trophyColor = 0xA65; }
							}
							else if ( ( typeof( Dragons ) == c.Owner.GetType() ) ||
							( typeof( RidingDragon ) == c.Owner.GetType() ) || 
							( typeof( GemDragon ) == c.Owner.GetType() ) || 
							( typeof( GhostDragyn ) == c.Owner.GetType() ) || 
							( typeof( GrayDragon ) == c.Owner.GetType() ) || 
							( typeof( BlueDragon ) == c.Owner.GetType() ) || 
							( typeof( MetalDragon ) == c.Owner.GetType() ) || 
							( typeof( Dragon ) == c.Owner.GetType() ) || 
							( typeof( StoneDragon ) == c.Owner.GetType() ) || 
							( typeof( WhiteDragon ) == c.Owner.GetType() ) || 
							( typeof( BlackDragon ) == c.Owner.GetType() ) || 
							( typeof( AsianDragon ) == c.Owner.GetType() ) || 
							( typeof( GreenDragon ) == c.Owner.GetType() ) || 
							( typeof( DragonGolem ) == c.Owner.GetType() ) )
							{
								if ( typeof( Dragon ) == c.Owner.GetType() ){ trophyColor = 0x9A2; }
								trophyID = 0x270D;
							}
							else if ( ( typeof( Wyrms ) == c.Owner.GetType() ) )
							{
								if ( c.Amount == 12 ){ trophyID = 0x270D; }
								else if ( c.Amount == 46 ){ trophyID = 0x270D; }
								else { trophyID = 0x33FD; }	
							}
							else if ( ( typeof( NightWyrm ) == c.Owner.GetType() ) || 
							( typeof( OnyxWyrm ) == c.Owner.GetType() ) || 
							( typeof( EmeraldWyrm ) == c.Owner.GetType() ) || 
							( typeof( AmethystWyrm ) == c.Owner.GetType() ) || 
							( typeof( SapphireWyrm ) == c.Owner.GetType() ) || 
							( typeof( GarnetWyrm ) == c.Owner.GetType() ) || 
							( typeof( TopazWyrm ) == c.Owner.GetType() ) || 
							( typeof( RubyWyrm ) == c.Owner.GetType() ) || 
							( typeof( SpinelWyrm ) == c.Owner.GetType() ) || 
							( typeof( QuartzWyrm ) == c.Owner.GetType() ) || 
							( typeof( JungleWyrm ) == c.Owner.GetType() ) || 
							( typeof( DesertWyrm ) == c.Owner.GetType() ) || 
							( typeof( MountainWyrm ) == c.Owner.GetType() ) || 
							( typeof( IceDragon ) == c.Owner.GetType() ) || 
							( typeof( LavaDragon ) == c.Owner.GetType() ) || 
							( typeof( WhiteWyrm ) == c.Owner.GetType() ) )
							{
								trophyID = 0x393B;
							}
							else if ( ( typeof( Lizardman ) == c.Owner.GetType() ) || 
							( typeof( Reptaur ) == c.Owner.GetType() ) || 
							( typeof( LizardmanArcher ) == c.Owner.GetType() ) )
							{
								trophyID = 0x393F;
							}
							else if ( ( typeof( Sakleth ) == c.Owner.GetType() ) || 
							( typeof( MutantLizardman ) == c.Owner.GetType() ) || 
							( typeof( Grathek ) == c.Owner.GetType() ) || 
							( typeof( Sleestax ) == c.Owner.GetType() ) || 
							( typeof( SaklethArcher ) == c.Owner.GetType() ) || 
							( typeof( SaklethMage ) == c.Owner.GetType() ) || 
							( typeof( Reptalar ) == c.Owner.GetType() ) || 
							( typeof( ReptalarShaman ) == c.Owner.GetType() ) || 
							( typeof( ReptalarChieftain ) == c.Owner.GetType() ) || 
							( typeof( SaklethArcher ) == c.Owner.GetType() ) )
							{
								trophyID = 0x33AB;
							}
							else if ( ( typeof( Goblin ) == c.Owner.GetType() ) || 
							( typeof( GoblinArcher ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3937;
							}
							else if ( ( typeof( Ratman ) == c.Owner.GetType() ) || 
							( typeof( RatmanMage ) == c.Owner.GetType() ) || 
							( typeof( RatmanArcher ) == c.Owner.GetType() ) )
							{
								trophyID = 0x392B;
							}
							else if ( ( typeof( Bugbear ) == c.Owner.GetType() ) ){ trophyID = 0x3935; }
							else if ( ( typeof( MinotaurCaptain ) == c.Owner.GetType() ) || 
							( typeof( RottingMinotaur ) == c.Owner.GetType() ) || 
							( typeof( MutantMinotaur ) == c.Owner.GetType() ) || 
							( typeof( MinotaurSmall ) == c.Owner.GetType() ) || 
							( typeof( MinotaurScout ) == c.Owner.GetType() ) || 
							( typeof( Minotaur ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3944;
							}
							else if ( ( typeof( Cyclops ) == c.Owner.GetType() ) || 
							( typeof( ZornTheBlacksmith ) == c.Owner.GetType() ) || 
							( typeof( ShamanicCyclops ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3931;
							}
							else if ( ( typeof( StoneGiant ) == c.Owner.GetType() ) || 
							( typeof( IceGiant ) == c.Owner.GetType() ) || 
							( typeof( LavaGiant ) == c.Owner.GetType() ) || 
							( typeof( MountainGiant ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3912;
							}
							else if ( typeof( UndeadGiant ) == c.Owner.GetType() ){ trophyID = 0x21F9; }
							else if ( ( typeof( Titan ) == c.Owner.GetType() ) || 
							( typeof( ElderTitan ) == c.Owner.GetType() ) )
							{
								trophyID = 0x21F7;
							}
							else if ( ( typeof( TundraOgre ) == c.Owner.GetType() ) || 
							( typeof( OgreMagi ) == c.Owner.GetType() ) || 
							( typeof( Ogre ) == c.Owner.GetType() ) )
							{
								trophyID = 0x33E1;
							}
							else if ( ( typeof( Ettin ) == c.Owner.GetType() ) || 
							( typeof( ArcticEttin ) == c.Owner.GetType() ) || 
							( typeof( AncientEttin ) == c.Owner.GetType() ) )
							{
								trophyID = 0x393D;
							}
							else if ( typeof( EttinShaman ) == c.Owner.GetType() ){ trophyID = 0x33A7; }
							else if ( ( typeof( HillGiant ) == c.Owner.GetType() ) || 
							( typeof( HillGiantShaman ) == c.Owner.GetType() ) )
							{
								trophyID = 0x33A9;
							}
							else if ( ( typeof( FireGargoyle ) == c.Owner.GetType() ) || 
							( typeof( Gargoyle ) == c.Owner.GetType() ) || 
							( typeof( AncientGargoyle ) == c.Owner.GetType() ) || 
							( typeof( GhostGargoyle ) == c.Owner.GetType() ) || 
							( typeof( MutantGargoyle ) == c.Owner.GetType() ) || 
							( typeof( CosmicGargoyle ) == c.Owner.GetType() ) || 
							( typeof( SpectralGargoyle ) == c.Owner.GetType() ) || 
							( typeof( ZombieGargoyle ) == c.Owner.GetType() ) || 
							( typeof( GargoyleMarble ) == c.Owner.GetType() ) || 
							( typeof( StygianGargoyle ) == c.Owner.GetType() ) || 
							( typeof( StygianGargoyleLord ) == c.Owner.GetType() ) || 
							( typeof( CodexGargoyleA ) == c.Owner.GetType() ) || 
							( typeof( CodexGargoyleB ) == c.Owner.GetType() ) || 
							( typeof( GargoyleWarrior ) == c.Owner.GetType() ) || 
							( typeof( StoneGargoyle ) == c.Owner.GetType() ) || 
							( typeof( ShadowDemon ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3933;
							}
							else if ( ( typeof( GargoyleRuby ) == c.Owner.GetType() ) || 
							( typeof( GargoyleEmerald ) == c.Owner.GetType() ) || 
							( typeof( GargoyleAmethyst ) == c.Owner.GetType() ) || 
							( typeof( GargoyleSapphire ) == c.Owner.GetType() ) || 
							( typeof( Tarjan ) == c.Owner.GetType() ) || 
							( typeof( BloodDemigod ) == c.Owner.GetType() ) || 
							( typeof( Xurtzar ) == c.Owner.GetType() ) || 
							( typeof( AbysmalDaemon ) == c.Owner.GetType() ) || 
							( typeof( DeepSeaDevil ) == c.Owner.GetType() ) || 
							( typeof( Devil ) == c.Owner.GetType() ) || 
							( typeof( BloodDemon ) == c.Owner.GetType() ) || 
							( typeof( Demon ) == c.Owner.GetType() ) || 
							( typeof( FireDemon ) == c.Owner.GetType() ) || 
							( typeof( Daemonic ) == c.Owner.GetType() ) || 
							( typeof( DaemonTemplate ) == c.Owner.GetType() ) || 
							( typeof( Daemon ) == c.Owner.GetType() ) )
							{
								if (typeof( DeepSeaDevil ) == c.Owner.GetType() ){ trophyColor = 1365; }
								trophyID = 0x567F;
							}

							else if ( typeof( Balron ) == c.Owner.GetType() ){ trophyID = 0x5681; }
							else if ( typeof( Archfiend ) == c.Owner.GetType() ){ trophyID = 0x5681; trophyColor = 0xB1E; }
							else if ( typeof( Fiend ) == c.Owner.GetType() ){ trophyID = 0x567F; trophyColor = 0xB1E; }
							else if ( ( typeof( GargoyleOnyx ) == c.Owner.GetType() ) || 
							( typeof( BlackGateDemon ) == c.Owner.GetType() ) )
							{
								trophyID = 0x392C; trophyColor = 0;
							}
							else if ( ( typeof( BrownBear ) == c.Owner.GetType() ) || 
							( typeof( GrizzlyBearRiding ) == c.Owner.GetType() ) || 
							( typeof( GrizzlyBear ) == c.Owner.GetType() ) )
							{
								trophyID = 0x1E67; 
							}
							else if ( ( typeof( SabretoothBear ) == c.Owner.GetType() ) || 
							( typeof( SabretoothBearRiding ) == c.Owner.GetType() ) || 
							( typeof( DeathBear ) == c.Owner.GetType() ) || 
							( typeof( DireBear ) == c.Owner.GetType() ) || 
							( typeof( ElderBrownBear ) == c.Owner.GetType() ) || 
							( typeof( ElderBrownBearRiding ) == c.Owner.GetType() ) || 
							( typeof( GreatBear ) == c.Owner.GetType() ) )
							{
								trophyID = 0x339B; 
							}
							else if ( typeof( CaveBear ) == c.Owner.GetType() ){ trophyID = 0x339D; }
							else if ( typeof( CaveBearRiding ) == c.Owner.GetType() ){ trophyID = 0x339D; }
							else if ( ( typeof( ElderBlackBear ) == c.Owner.GetType() ) || 
							( typeof( ElderBlackBearRiding ) == c.Owner.GetType() ) || 
							( typeof( BlackBear ) == c.Owner.GetType() ) || 
							( typeof( SabreclawCub ) == c.Owner.GetType() ) || 
							( typeof( KodiakBear ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3399; 
								trophyColor = 0;
							}
							else if ( typeof( PolarBear ) == c.Owner.GetType() ){ trophyID = 0x1E6C; trophyColor = 0; }
							else if ( typeof( ElderPolarBear ) == c.Owner.GetType() ){ trophyID = 0x339F; }
							else if ( typeof( ElderPolarBearRiding ) == c.Owner.GetType() ){ trophyID = 0x339F; }
							else if ( ( typeof( OgreLord ) == c.Owner.GetType() ) || 
							( typeof( ArcticOgreLord ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3378; 
							}
							else if ( typeof( Cerberus ) == c.Owner.GetType() ){ trophyID = 0x335A; }
							else if ( typeof( Drake ) == c.Owner.GetType() ){ trophyID = 0x3368; }
							else if ( typeof( SwampDrake ) == c.Owner.GetType() ){ trophyID = 0x3385; }
							else if ( typeof( SwampDrakeRiding ) == c.Owner.GetType() ){ trophyID = 0x3385; }
							else if ( typeof( AncientDrake ) == c.Owner.GetType() ){ trophyID = 0x3358; }
							else if ( typeof( Owlbear ) == c.Owner.GetType() ){ trophyID = 0x337B; }
							else if ( typeof( AbysmalOgre ) == c.Owner.GetType() ){ trophyID = 0x3354; }
							else if ( typeof( SeaDrake ) == c.Owner.GetType() ){ trophyID = 0x3381; }
							else if ( typeof( AncientCyclops ) == c.Owner.GetType() ){ trophyID = 0x3356; }
							else if ( typeof( StormGiant ) == c.Owner.GetType() ){ trophyID = 0x335C; }
							else if ( typeof( CloudGiant ) == c.Owner.GetType() ){ trophyColor = 0xB70; trophyID = 0x335C; }
							else if ( typeof( StarGiant ) == c.Owner.GetType() && c.Amount == 770 ){ trophyColor = 0xB73; trophyID = 0x3352; }
							else if ( typeof( StarGiant ) == c.Owner.GetType() ){ trophyColor = 0xB73; trophyID = 0x3374; }
							else if ( typeof( DemonOfTheSea ) == c.Owner.GetType() ){ trophyID = 0x337D; }
							else if ( typeof( DragonGhost ) == c.Owner.GetType() ){ trophyID = 0x337F; }
							else if ( ( typeof( Tiger ) == c.Owner.GetType() ) || 
							( typeof( SabretoothTiger ) == c.Owner.GetType() ) || 
							( typeof( SabretoothTigerRiding ) == c.Owner.GetType() ) || 
							( typeof( TigerRiding ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3389;
								trophyColor = 0;
							}
							else if ( ( typeof( WhiteTiger ) == c.Owner.GetType() ) || ( typeof( WhiteTigerRiding ) == c.Owner.GetType() ) || ( typeof( PredatorHellCatRiding ) == c.Owner.GetType() ) || 
							( typeof( PredatorHellCat ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3389;
							}
							else if ( ( typeof( Lion ) == c.Owner.GetType() ) || 
							( typeof( LionRiding ) == c.Owner.GetType() ) || 
							( typeof( SnowLion ) == c.Owner.GetType() ) || 
							( typeof( Manticore ) == c.Owner.GetType() ) || 
							( typeof( Chimera ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3376; 
							}
							else if ( typeof( Exodus ) == c.Owner.GetType() ){ trophyID = 0x5681; }
							else if ( ( typeof( Wyvra ) == c.Owner.GetType() ) || 
							( typeof( Hydra ) == c.Owner.GetType() ) || 
							( typeof( EnergyHydra ) == c.Owner.GetType() ) )
							{
								trophyID = 0x3372; 
								if ( typeof( Hydra ) == c.Owner.GetType() ){ trophyColor = 0xA5D; }
							}
							else if ( typeof( SkeletalDragon ) == c.Owner.GetType() ){ trophyID = 0x33B3; }
							else if ( typeof( Dracolich ) == c.Owner.GetType() ){ trophyID = 0x3364; }
							else if ( typeof( SwampThing ) == c.Owner.GetType() ){ trophyID = 0x3387; }
							else if ( typeof( Griffon ) == c.Owner.GetType() ){ trophyID = 0x3370; }
							else if ( typeof( GriffonRiding ) == c.Owner.GetType() ){ trophyID = 0x3370; }
							else if ( typeof( Walrus ) == c.Owner.GetType() ){ trophyID = 0x33DF; }
							else if ( typeof( Meglasaur ) == c.Owner.GetType() ){ trophyID = 0x3362; }
							else if ( typeof( Stegosaurus ) == c.Owner.GetType() ){ trophyID = 0x3393; }
							else if ( typeof( Tyranasaur ) == c.Owner.GetType() ){ trophyID = 0x3391; }
							else if ( typeof( DragonTurtle ) == c.Owner.GetType() ){ trophyID = 0x3366; }
							else if ( typeof( DeepSeaGiant ) == c.Owner.GetType() ){ trophyID = 0x3360; }
							else if ( typeof( Trollbear ) == c.Owner.GetType() ){ trophyID = 0x33E3; }
							else if ( typeof( Satan ) == c.Owner.GetType() ){ trophyID = 0x33AF; }
							else if ( typeof( SeaGiant ) == c.Owner.GetType() ){ trophyID = 0x3383; }
							else if ( typeof( SandGiant ) == c.Owner.GetType() || typeof( AbyssGiant ) == c.Owner.GetType() )
							{
								trophyID = 0x3352;
								if (typeof( SandGiant ) == c.Owner.GetType() ){ trophyColor = 0x96D; }
							}
							else if ( typeof( JungleGiant ) == c.Owner.GetType() ){ trophyID = 0x3374; }
							else if ( typeof( ForestGiant ) == c.Owner.GetType() || typeof( FireGiant ) == c.Owner.GetType() )
							{
								trophyID = 0x336C;
								if (typeof( FireGiant ) == c.Owner.GetType() ){ trophyColor = 0xA93; }
							}
							else if ( ( typeof( ZombieGiant ) == c.Owner.GetType() ) || 
							( typeof( FleshGolem ) == c.Owner.GetType() ) || 
							( typeof( AncientFleshGolem ) == c.Owner.GetType() ) )
							{
								trophyID = 0x336A;
							}
							else if ( typeof( FrostGiant ) == c.Owner.GetType() )
							{
								trophyID = 0x336E;
							}
							else if ( ( typeof( SeaTroll ) == c.Owner.GetType() ) || 
							( typeof( FrostTroll ) == c.Owner.GetType() ) || 
							( typeof( FrostTrollShaman ) == c.Owner.GetType() ) || 
							( typeof( SwampTroll ) == c.Owner.GetType() ) || 
							( typeof( TrollWitchDoctor ) == c.Owner.GetType() ) || 
							( typeof( Troll ) == c.Owner.GetType() ) )
							{
								trophyID = 0x1E6D; 
							}
							else if ( ( typeof( Orc ) == c.Owner.GetType() ) || 
							( typeof( OrcBomber ) == c.Owner.GetType() ) || 
							( typeof( OrcCaptain ) == c.Owner.GetType() ) || 
							( typeof( OrcishLord ) == c.Owner.GetType() ) || 
							( typeof( OrcishMage ) == c.Owner.GetType() ) || 
							( typeof( Urk ) == c.Owner.GetType() ) || 
							( typeof( UrkShaman ) == c.Owner.GetType() ) || 
							( typeof( Urc ) == c.Owner.GetType() ) || 
							( typeof( UrcShaman ) == c.Owner.GetType() ) || 
							( typeof( UrcBowman ) == c.Owner.GetType() ) || 
							( typeof( OrkMage ) == c.Owner.GetType() ) || 
							( typeof( OrkMonks ) == c.Owner.GetType() ) || 
							( typeof( OrkRogue ) == c.Owner.GetType() ) || 
							( typeof( OrkWarrior ) == c.Owner.GetType() ) )
							{
								trophyID = 0x1E6B; trophyColor = 0;
							}
							else if ( typeof( GreatHart ) == c.Owner.GetType() || typeof( Antelope ) == c.Owner.GetType() ){ trophyID = 0x1E68; }
							else if ( ( typeof( Gorilla ) == c.Owner.GetType() ) || 
							( typeof( Infected ) == c.Owner.GetType() ) || 
							( typeof( Ape ) == c.Owner.GetType() ) )
							{
								trophyID = 0x1E6A;
								if ( typeof( Infected ) != c.Owner.GetType() ){ trophyColor = 0; }
							}
							else if ( ( typeof( Yeti ) == c.Owner.GetType() ) ){ trophyID = 0x1E6A; trophyColor = 0x47E; }
							else if ( ( typeof( Pixie ) == c.Owner.GetType() ) || ( typeof( Sprite ) == c.Owner.GetType() ) || ( typeof( Faerie ) == c.Owner.GetType() ) )
							{
								switch ( Utility.RandomMinMax( 0, 4 ) )
								{
									case 0:	trophyID = 0x2A79; break;
									case 1:	trophyID = 0x2A75; break;
									case 2:	trophyID = 0x2A71; break;
									case 3:	trophyID = 0x2A77; break;
									case 4:	trophyID = 0x2A73; break;
								}
							}
							else if ( ( typeof( Unicorn ) == c.Owner.GetType() ) ){ trophyID = 0x33B1; }
							else if ( ( typeof( Dreadhorn ) == c.Owner.GetType() ) ){ trophyID = 0x33B1; }
							else if ( ( typeof( DarkUnicorn ) == c.Owner.GetType() ) ){ trophyID = 0x335E; }
							else if ( ( typeof( DarkUnicornRiding ) == c.Owner.GetType() ) ){ trophyID = 0x335E; }
							else if ( typeof( Nightmare ) == c.Owner.GetType() || typeof( AncientNightmareRiding ) == c.Owner.GetType() || typeof( AncientNightmare ) == c.Owner.GetType() || typeof( Placeron ) == c.Owner.GetType() ){ trophyID = 0x33AD; }
							else if ( ( typeof( Wyvern ) == c.Owner.GetType() ) || 
							( typeof( Wyverns ) == c.Owner.GetType() ) || 
							( typeof( Teradactyl ) == c.Owner.GetType() ) )
							{
								trophyID = 0x33B5;
							}
							else if ( typeof( AncientWyvern ) == c.Owner.GetType() ){ trophyID = 0x3397; }
							else if ( typeof( IceDevil ) == c.Owner.GetType() ){ trophyID = 0x338F; }
							else if ( typeof( Xenomorph ) == c.Owner.GetType() ){ trophyID = 0x33B9; trophyColor = 0; }
							else if ( typeof( Hippogriff ) == c.Owner.GetType() ){ trophyID = 0x33DD; }
							else if ( typeof( HippogriffRiding ) == c.Owner.GetType() ){ trophyID = 0x33DD; }
							else if ( typeof( HellBeast ) == c.Owner.GetType() ){ trophyID = 0x33DB; }
							else if ( typeof( Styguana ) == c.Owner.GetType() ){ trophyID = 0x33B7; }
							else if ( typeof( Watcher ) == c.Owner.GetType() ){ trophyID = 0x33BB; }
							else if ( typeof( CragCat ) == c.Owner.GetType() ){ trophyID = 0x33BD; }
							else if ( typeof( PrimevalGreenDragon ) == c.Owner.GetType() ){ trophyID = 0x33C1; }
							else if ( typeof( PrimevalNightDragon ) == c.Owner.GetType() ){ trophyID = 0x33D3; }
							else if ( typeof( PrimevalRedDragon ) == c.Owner.GetType() ){ trophyID = 0x33C5; }
							else if ( typeof( PrimevalRoyalDragon ) == c.Owner.GetType() ){ trophyID = 0x33C3; }
							else if ( typeof( PrimevalRunicDragon ) == c.Owner.GetType() ){ trophyID = 0x33BF; }
							else if ( typeof( PrimevalSeaDragon ) == c.Owner.GetType() ){ trophyID = 0x33C7; }
							else if ( typeof( ReanimatedDragon ) == c.Owner.GetType() ){ trophyID = 0x33D5; }
							else if ( typeof( VampiricDragon ) == c.Owner.GetType() ){ trophyID = 0x33CD; }
							else if ( typeof( PrimevalAbysmalDragon ) == c.Owner.GetType() ){ trophyID = 0x33CB; }
							else if ( typeof( PrimevalAmberDragon ) == c.Owner.GetType() ){ trophyID = 0x33D1; }
							else if ( typeof( PrimevalBlackDragon ) == c.Owner.GetType() ){ trophyID = 0x33A3; }
							else if ( typeof( PrimevalDragon ) == c.Owner.GetType() ){ trophyID = 0x33D9; }
							else if ( typeof( PrimevalSilverDragon ) == c.Owner.GetType() ){ trophyID = 0x33A5; }
							else if ( typeof( PrimevalVolcanicDragon ) == c.Owner.GetType() ){ trophyID = 0x33CF; }
							else if ( typeof( PrimevalFireDragon ) == c.Owner.GetType() ){ trophyID = 0x33D7; }
							else if ( typeof( PrimevalStygianDragon ) == c.Owner.GetType() ){ trophyID = 0x33C9; }
							else if ( typeof( TitanLithos ) == c.Owner.GetType() ){ trophyID = 0x338B; }
							else if ( typeof( TitanPyros ) == c.Owner.GetType() ){ trophyID = 0x338D; }
							else if ( typeof( TitanHydros ) == c.Owner.GetType() ){ trophyID = 0x338F; }

							if ( trophyID > 0 )
							{
								MountedTrophyHead trophy = new MountedTrophyHead();
								trophy.Hue = trophyColor;
								trophy.Name = "mounted head of " + trophyName;
								trophy.ItemID = trophyID;
								trophy.AnimalWhere = "From " + Server.Misc.Worlds.GetRegionName( from.Map, from.Location );
								if ( c.m_Killer != null && c.m_Killer is PlayerMobile )
								{
									string trophyKiller = c.m_Killer.Name + " the " + Server.Misc.GetPlayerInfo.GetSkillTitle( c.m_Killer );
									trophy.AnimalKiller = "Slain by " + trophyKiller;
								}
								from.AddToBackpack( trophy );
								from.SendMessage("You mount the head on the base.");
								c.VisitedByTaxidermist = true;
								m_Trophy.Delete();
							}
							else 
							{
								from.SendMessage("That cannot be mounted as a trophy!");
								return;
							}
						}
					}
					else
					{
						from.SendMessage("That cannot seem to mount this as a trophy!");
						return;
					}
				}
			}
		}

		public TrophyBase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}