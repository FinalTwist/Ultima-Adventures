using System;
using Server.Network;
using Server.Items;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	public class SurgeonsKnife : SkinningKnife
	{
		public override int Hue{ get { return 0x1B0; } }

		[Constructable]
		public SurgeonsKnife() : base()
		{
			Name = "surgeons knife";
			Hue = 28;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendLocalizedMessage( 1010018 ); // What do you want to use this item on?
			from.Target = new CorpseTarget( this );
		}

		public SurgeonsKnife( Serial serial ) : base( serial )
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

		private class CorpseTarget : Target
		{
			private SurgeonsKnife m_Knife;

			public CorpseTarget( SurgeonsKnife blade ) : base( 3, false, TargetFlags.None )
			{
				m_Knife = blade;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Knife.Deleted )
					return;

				if ( !(targeted is Corpse) )
				{
					from.SendLocalizedMessage( 1042600 ); // That is not a corpse!
                    return;
				}
				else
				{
					object obj = targeted;

					if ( obj is Corpse )
						obj = ((Corpse)obj).Owner;

                    if ( obj != null )
                    {
						Corpse c = (Corpse)targeted;
						Mobile m = c.m_Owner;

						SlayerEntry giant = SlayerGroup.GetEntryByName( SlayerName.GiantKiller );

						if ( m_Knife.HitPoints > 0 && !(c.VisitedByTaxidermist) && from.Skills[SkillName.Forensics].Value >= Utility.RandomMinMax( 30, 250 ) && giant.Slays(m) )
						{
							Item piece = new FrankenLegLeft(); piece.Delete();

							switch ( Utility.Random( 7 ) )
							{
								case 0: piece = new FrankenLegLeft(); from.SendMessage("You sever off the giant's left leg."); break;
								case 1: piece = new FrankenLegRight(); from.SendMessage("You sever off the giant's right leg."); break;
								case 2: piece = new FrankenArmLeft(); from.SendMessage("You sever off the giant's left arm."); break;
								case 3: piece = new FrankenArmRight(); from.SendMessage("You sever off the giant's right arm."); break;
								case 4: piece = new FrankenHead(); from.SendMessage("You sever off the giant's head."); break;
								case 5: piece = new FrankenTorso(); from.SendMessage("You sever apart the giant's torso."); break;
								case 6: piece = new FrankenBrain(); from.SendMessage("You remove the giant's fresh brain."); break;
							}

							if ( piece is FrankenBrain )
							{
								FrankenBrain brain = (FrankenBrain)piece;

								string brainName = m.Name;
									if ( m.Title != "" ){ brainName = brainName + " " + m.Title; }

								brain.BrainSource = brainName;
								brain.BrainLevel = ( IntelligentAction.GetCreatureLevel( m ) + 5 ); // TITAN LICHES SEEM TO HAVE LEVEL 96 BRAINS
									if ( brain.BrainLevel > 100 ){ brain.BrainLevel = 100; }
							}

							from.AddToBackpack( piece );
						}

						if ( m_Knife.HitPoints < 1 )
						{
							from.SendMessage("This knife is too dull and needs to be replaced or repaired.");
							return;
						}
						else if ( c.VisitedByTaxidermist == true )
						{
							from.SendMessage("This corpse has already been cut up.");
							return;
						}
						else if ( !( (from.Backpack).ConsumeTotal( typeof( Jar ), 1 ) ) )
						{
							from.SendMessage("You forgot a jar, losing your chance to get anything extra from the corpse.");
							return;
						}
						else if ( from.CheckSkill( SkillName.Forensics, -5, 100 ) )
						{
							if ( m_Knife.HitPoints > 0 && Utility.RandomMinMax( 1, 4 ) == 1 ){ m_Knife.HitPoints--; }

							BottleOfParts guts = new BottleOfParts();

							int nHue = 0;
							string sName = "";

							if ( typeof( Actor ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( CrystalGoliath ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( MetalBeetle ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( Necromental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( AnyElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( AnimatedRocks ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( GemElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( AnyGemElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( AgapiteElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( AirElemental ) == c.Owner.GetType() ){ sName = "mystical air"; }
							else if ( typeof( TitanStratos ) == c.Owner.GetType() ){ sName = "mystical air"; }
							else if ( typeof( Alligator ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SwampGator ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Snapper ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Turtle ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Toraxen ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( AncientLich ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( DemiLich ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SoulReaper ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( AncientWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( NightWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( OnyxWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( EmeraldWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( AmethystWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( SapphireWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( GarnetWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( TopazWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( RubyWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( SpinelWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( QuartzWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( JungleWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( DesertWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( MountainWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( Anhkheg ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( AntLion ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( Archmage ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Stirge ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BoatSailorMage ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BoatSailorArcher ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BoatSailorBard ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BoatPirateMage ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BoatPirateArcher ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BoatPirateBard ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( ArcticOgreLord ) == c.Owner.GetType() ){ sName = "ogre thumbs"; }
							else if ( typeof( Artist ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( IceDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( BabyDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( LavaDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( Drakkul ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( DrakkulMage ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( DrakkulChief ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( CaddelliteDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( MysticalFox ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Xurtzar ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( TitanPyros ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Balron ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Fiend ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Archfiend ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Satan ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Marilith ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Bandit ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Basilisk ) == c.Owner.GetType() ){ sName = "horrid breath"; }
							else if ( typeof( Beetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( Beholder ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( SoulSucker ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( Berserker ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Adventurers ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BombWorshipper ) == c.Owner.GetType() ){ sName = "human blood"; }
							
							else if ( typeof( Jedi ) == c.Owner.GetType() ){ sName = "human blood"; }
							
							else if ( typeof( Syth ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( SavageAlien ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Psionicist ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Monks ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Bird ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( BlackBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( SabreclawCub ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Bugbear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( SabretoothBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( SabretoothBearRiding ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( BlackDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( AsianDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalFireDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalGreenDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalNightDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalRedDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalRoyalDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalRunicDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalSeaDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( ReanimatedDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( VampiricDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalAbysmalDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalAmberDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalBlackDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalSilverDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalVolcanicDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( PrimevalStygianDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( GrayDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( DeepSeaDevil ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( BlackGateDemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( SlimeDevil ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( BlackPudding ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( GreenSlime ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( BlackWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( BloodAssassin ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BloodDemigod ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BloodDemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( BloodElemental ) == c.Owner.GetType() ){ sName = "dried blood"; }
							else if ( typeof( BloodSpawn ) == c.Owner.GetType() ){ sName = "dried blood"; }
							else if ( typeof( BloodWorm ) == c.Owner.GetType() ){ sName = "worm guts"; }
							else if ( typeof( BlueDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( SlasherOfVoid ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( Boar ) == c.Owner.GetType() ){ sName = "pig snouts"; }
							else if ( typeof( Bogling ) == c.Owner.GetType() ){ sName = "swamp gas"; }
							else if ( typeof( BogThing ) == c.Owner.GetType() ){ sName = "swamp gas"; }
							else if ( typeof( SwampThing ) == c.Owner.GetType() ){ sName = "swamp gas"; }
							else if ( typeof( BoneDemon ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( BoneGolem ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( GiantSkeleton ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( BoneKnight ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( BoneSlasher ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( KhumashGor ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalSamurai ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( DeadKnight ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalPirate ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( BoneMagi ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Brigand ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( BronzeElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( Panda ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( ElderBrownBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( ElderBlackBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( ElderPolarBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( ElderBrownBearRiding ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( ElderBlackBearRiding ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( ElderPolarBearRiding ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( BrownBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Bull ) == c.Owner.GetType() ){ sName = "cow eyes"; }
							else if ( typeof( Grum ) == c.Owner.GetType() ){ sName = "cow eyes"; }
							else if ( typeof( PoisonFrog ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( BullFrog ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( Frog ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( CarcassWorm ) == c.Owner.GetType() ){ sName = "worm guts"; }
							else if ( typeof( Slitheran ) == c.Owner.GetType() ){ sName = "worm guts"; }
							else if ( typeof( Cat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( BlackCat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( WhiteCat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( CaveBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( CaveBearRiding ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( CaveLizard ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Stalker ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Centaur ) == c.Owner.GetType() ){ sName = "centaur fingers"; }
							else if ( typeof( Chicken ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( CinderElemental ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( MetalDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( CopperElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( KelpElemental ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( WeedElemental ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( Corpser ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( BloodLotus ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( CorruptCentaur ) == c.Owner.GetType() ){ sName = "centaur fingers"; }
							else if ( typeof( SabretoothTiger ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Cougar ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( SabretoothCub ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Cow ) == c.Owner.GetType() ){ sName = "cow eyes"; }
							else if ( typeof( Crane ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( CrystalElemental ) == c.Owner.GetType() ){ sName = "crystal shards"; }
							else if ( typeof( Cyclops ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( AncientCyclops ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( IceGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( LavaGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( ShamanicCyclops ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( ZornTheBlacksmith ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( Daemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( DaemonTemplate ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Daemonic ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( IceDevil ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( AbysmalDaemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Tarjan ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( TheAncientTree ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( Ent ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( AncientEnt ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( EvilEnt ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( DarkReaper ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( DeadReaper ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( DarkUnicorn ) == c.Owner.GetType() ){ sName = "unicorn teeth"; }
							else if ( typeof( Dreadhorn ) == c.Owner.GetType() ){ sName = "unicorn teeth"; }
							else if ( typeof( DarkUnicornRiding ) == c.Owner.GetType() ){ sName = "unicorn teeth"; }
							else if ( typeof( DarkWisp ) == c.Owner.GetType() ){ sName = "wisp light"; }
							else if ( typeof( DeadlyScorpion ) == c.Owner.GetType() ){ sName = "scorpion stingers"; }
							else if ( typeof( DeathBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( DeathwatchBeetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( DeathwatchBeetleHatchling ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( DeathWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Neptar ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( NeptarWizard ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( DeepSeaSerpent ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( Jormungandr ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( Cronosaurus ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( Demon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( DesertOstard ) == c.Owner.GetType() ){ sName = "ostard scales"; }
							else if ( typeof( DireBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( DireBoar ) == c.Owner.GetType() ){ sName = "pig snouts"; }
							else if ( typeof( DireWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( DarkHound ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Jackalwitch ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Dog ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Dolphin ) == c.Owner.GetType() ){ sName = "dolphin teeth"; }
							else if ( typeof( Dragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( DragonKing ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( Drake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( AbysmalDrake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( AncientDrake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SwampDrake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SwampDrakeRiding ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( DreadSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( PhaseSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( ShadowRecluse ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( AlienSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( DullCopperElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( DustElemental ) == c.Owner.GetType() ){ sName = "magical dust"; }
							else if ( typeof( Eagle ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( EarthElemental ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( TitanLithos ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( MeteorElemental ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( StoneDragon ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( StoneElemental ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( StoneRoper ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( CaddelliteElemental ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( Efreet ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( Ifreet ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( Afreet ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( ElderGazer ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( Seeker ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( Watcher ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( ElectricalElemental ) == c.Owner.GetType() ){ sName = "electricity"; }
							else if ( typeof( ElfBoatPirateBard ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfBoatPirateArcher ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfBoatPirateMage ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfBoatSailorBard ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfBoatSailorArcher ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfBoatSailorMage ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfBerserker ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfMage ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfMonks ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfPirateCaptain ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfPirateCrew ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfPirateCrewBow ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( ElfRogue ) == c.Owner.GetType() ){ sName = "elven blood"; }
							else if ( typeof( EtherealWarrior ) == c.Owner.GetType() ){ sName = "angelic feathers"; }
							else if ( typeof( Ettin ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( EvilMageLord ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( EvilMage ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Executioner ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Exodus ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( Ferret ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( FireBat ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireBeetle ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( Angel ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( Archangel ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireDemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Vulcrum ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( Sunlyte ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireElemental ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( FireGiant ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireMephit ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireSteed ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( FireToad ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( FireWyrmling ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( FleshGolem ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Mutant ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( AncientFleshGolem ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( SkinGolem ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( ForestGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( HillGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( HillGiantShaman ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( MountainGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( StoneGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( ForestOstard ) == c.Owner.GetType() ){ sName = "ostard scales"; }
							else if ( typeof( FrailSkeleton ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( FrenziedOstard ) == c.Owner.GetType() ){ sName = "ostard scales"; }
							else if ( typeof( FrostGiant ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( FrostOoze ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( Fungal ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( FungalMage ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( CreepingFungus ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( FrostSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( FrostTroll ) == c.Owner.GetType() ){ sName = "troll claws"; }
							else if ( typeof( FrozenCorpse ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( Bullradon ) == c.Owner.GetType() ){ sName = "bony horns"; }
							else if ( typeof( BullradonRiding ) == c.Owner.GetType() ){ sName = "bony horns"; }
							else if ( typeof( Gargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleWarrior ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( CodexGargoyleA ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( CodexGargoyleA ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( StygianGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( StygianGargoyleLord ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleAmethyst ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( CosmicGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( MutantGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( AncientGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleEmerald ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleMarble ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleOnyx ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleRuby ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GargoyleSapphire ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( GarnetElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( Gazer ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( GhostGargoyle ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( DemonicGhost ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( GhostPirate ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( GhostWarrior ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( GhostWizard ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Ghostly ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Shroud ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Vordo ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Ghoul ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( GiantAdder ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( GiantBat ) == c.Owner.GetType() ){ sName = "bat ears"; }
							else if ( typeof( Bat ) == c.Owner.GetType() ){ sName = "bat ears"; }
							else if ( typeof( GiantBlackWidow ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( Tarantula ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( ZombieSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( Drider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( DriderWizard ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( GiantCrab ) == c.Owner.GetType() ){ sName = "crab meat"; }
							else if ( typeof( Lobstran ) == c.Owner.GetType() ){ sName = "crab meat"; }
							else if ( typeof( GiantLeech ) == c.Owner.GetType() ){ sName = "leech spit"; }
							else if ( typeof( MarshWurm ) == c.Owner.GetType() ){ sName = "leech spit"; }
							else if ( typeof( GiantLizard ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( GiantRat ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( SicklyRat ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( GiantSerpent ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( GiantSnake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Titanoboa ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( RandomSerpent ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( JadeSerpent ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( GiantSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( GiantToad ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( Toad ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( Goat ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Ramadon ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Elephant ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Mastadon ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Tuskadon ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Mammoth ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Hobgoblin ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( Goblin ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( GoblinArcher ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( Gnome ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( GnomeWarrior ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( GnomeMage ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( GoldenElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( GoldenSerpent ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Golem ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( ExcavationDroid ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( SecurityDroid ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( ServiceDroid ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( MetalGolem ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( CombatDroid ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( BattleDroid ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( MaintenanceDroid ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( CaddelliteGolem ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( GolemController ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Gorgon ) == c.Owner.GetType() ){ sName = "horrid breath"; }
							else if ( typeof( GorgonRiding ) == c.Owner.GetType() ){ sName = "horrid breath"; }
							else if ( typeof( Gorilla ) == c.Owner.GetType() ){ sName = "ape ears"; }
							else if ( typeof( Infected ) == c.Owner.GetType() ){ sName = "ape ears"; }
							else if ( typeof( Ape ) == c.Owner.GetType() ){ sName = "ape ears"; }
							else if ( typeof( GreatBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Devil ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( GreatHart ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( GreenDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( GreyWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( MadDog ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( GrizzlyBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( GrizzlyBearRiding ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Owlbear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Trollbear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Harpy ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( GiantHawk ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( GiantRaven ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( HarpyElder ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( HarpyHen ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( Griffon ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( GriffonRiding ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( Hippogriff ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( HippogriffRiding ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( HeadlessOne ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( HellCat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( HellHound ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( HellBeast ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( Cerberus ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( DemonDog ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( HellSteed ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( HenchHorse ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Zebra ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( HenchmanArcher ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( HenchmanFighter ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( HenchmanWizard ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( HenchmanMonster ) == c.Owner.GetType() ){ sName = "entrails"; }
							else if ( typeof( Hind ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Roc ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( HordeMinion ) == c.Owner.GetType() ){ sName = "abysmal essence"; }
							else if ( typeof( Horse ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Antelope ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( HugeLizard ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Hydra ) == c.Owner.GetType() ){ sName = "hydra urine"; }
							else if ( typeof( EnergyHydra ) == c.Owner.GetType() ){ sName = "hydra urine"; }
							else if ( typeof( IceColossus ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IcebergElemental ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceElemental ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceSteed ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceGhoul ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceGolem ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceSerpent ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceSnake ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( IceToad ) == c.Owner.GetType() ){ sName = "frog tongues"; }
							else if ( typeof( Iguana ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Imp ) == c.Owner.GetType() ){ sName = "imp tails"; }
							else if ( typeof( IronBeetle ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( DragonGolem ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( IronCobra ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( Jackal ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( JackRabbit ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Weasel ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( JungleGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( JungleViper ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SeaHorses ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Tortuga ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( ForestElemental ) == c.Owner.GetType() ){ sName = "elemental powder"; }
							else if ( typeof( Kirin ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Kobold ) == c.Owner.GetType() ){ sName = "scaly fingers"; }
							else if ( typeof( KoboldMage ) == c.Owner.GetType() ){ sName = "scaly fingers"; }
							else if ( typeof( KodiakBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( Kraken ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( Calamari ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( Krakoa ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( Dagon ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( LargeSnake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( LargeSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( LavaElemental ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( MagmaElemental ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( LavaLizard ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( LavaPuddle ) == c.Owner.GetType() ){ sName = "liquid fire"; }
							else if ( typeof( LavaSerpent ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( LavaSnake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( LesserDemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( YoungRoc ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( LesserSeaSnake ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SteamElemental ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( Leviathan ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( DragonTurtle ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( Lich ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( LichKing ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Surtaz ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( LichLord ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Nazghoul ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Lion ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( LionRiding ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( SnowLion ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Kilrathi ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( KilrathiGunner ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( CragCat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Chimera ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Manticore ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( LivingBronzeStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( LivingGoldStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( AnyStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( LivingIronStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( LivingShadowIronStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( LivingJadeStatue ) == c.Owner.GetType() ){ sName = "jade chunks"; }
							else if ( typeof( LivingMarbleStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( LivingSilverStatue ) == c.Owner.GetType() ){ sName = "silver shavings"; }
							else if ( typeof( LivingStoneStatue ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( RustGolem ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( SilverElemental ) == c.Owner.GetType() ){ sName = "silver shavings"; }
							else if ( typeof( WaxSculpture ) == c.Owner.GetType() ){ sName = "wax shavings"; }
							else if ( typeof( Lizardman ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Reptaur ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Grathek ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Sleestax ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( LizardmanArcher ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Llama ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( LowerDemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( Mantis ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( MechanicalScorpion ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( Medusa ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Meglasaur ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Styguana ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Tyranasaur ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Megalodon ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Lochasaur ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Shark ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( GreatWhite ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Tyranasaur ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Basilosaurus ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Iguanodon ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Teradactyl ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Gorceratops ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( GorceratopsRiding ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( MindFlayer ) == c.Owner.GetType() ){ sName = "illithid brains"; }
							else if ( typeof( Minotaur ) == c.Owner.GetType() ){ sName = "minotaur hooves"; }
							else if ( typeof( MinotaurSmall ) == c.Owner.GetType() ){ sName = "minotaur hooves"; }
							else if ( typeof( MutantMinotaur ) == c.Owner.GetType() ){ sName = "minotaur hooves"; }
							else if ( typeof( MinotaurCaptain ) == c.Owner.GetType() ){ sName = "minotaur hooves"; }
							else if ( typeof( RottingMinotaur ) == c.Owner.GetType() ){ sName = "minotaur hooves"; }
							else if ( typeof( MinotaurScout ) == c.Owner.GetType() ){ sName = "minotaur hooves"; }
							else if ( typeof( MLDryad ) == c.Owner.GetType() ){ sName = "dryad tears"; }
							else if ( typeof( Mongbat ) == c.Owner.GetType() ){ sName = "bat ears"; }
							else if ( typeof( MonstrousSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( AbyssCrawler ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( CaveFisher ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( Arachnar ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( MountainGoat ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( MudElemental ) == c.Owner.GetType() ){ sName = "mystical mud"; }
							else if ( typeof( MudMan ) == c.Owner.GetType() ){ sName = "mystical mud"; }
							else if ( typeof( SewageElemental ) == c.Owner.GetType() ){ sName = "mystical mud"; }
							else if ( typeof( Mummy ) == c.Owner.GetType() ){ sName = "mummy wraps"; }
							else if ( typeof( MummyGiant ) == c.Owner.GetType() ){ sName = "mummy wraps"; }
							else if ( typeof( DiseasedMummy ) == c.Owner.GetType() ){ sName = "mummy wraps"; }
							else if ( typeof( MummyLord ) == c.Owner.GetType() ){ sName = "mummy wraps"; }
							else if ( typeof( Naga ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Native ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( NativeArcher ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( NativeWitchDoctor ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Neanderthal ) == c.Owner.GetType() ){ sName = "large teeth"; }
							else if ( typeof( Morlock ) == c.Owner.GetType() ){ sName = "large teeth"; }
							else if ( typeof( Durgar ) == c.Owner.GetType() ){ sName = "large teeth"; }
							else if ( typeof( Nightmare ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( AncientNightmare ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( AncientNightmareRiding ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( Placeron ) == c.Owner.GetType() ){ sName = "hellish smoke"; }
							else if ( typeof( ObsidianElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( Ogre ) == c.Owner.GetType() ){ sName = "ogre thumbs"; }
							else if ( typeof( OgreLord ) == c.Owner.GetType() ){ sName = "ogre thumbs"; }
							else if ( typeof( OgreMagi ) == c.Owner.GetType() ){ sName = "ogre thumbs"; }
							else if ( typeof( AbysmalOgre ) == c.Owner.GetType() ){ sName = "ogre thumbs"; }
							else if ( typeof( WoodlandDevil ) == c.Owner.GetType() ){ sName = "oni fur"; }
							else if ( typeof( OphidianArchmage ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( OphidianKnight ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( OphidianMage ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( OphidianMatriarch ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( OphidianWarrior ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Serpentar ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SerpentarWizard ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Serpyn ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SerpynChampion ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SerpynSorceress ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Urc ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( UrcBowman ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( UrcShaman ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( Urk ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( UrkShaman ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( Orc ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrcBomber ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrcCaptain ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrcishLord ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrcishMage ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrkDemigod ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrkMage ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( Orx ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrxWarrior ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrkMonks ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrkRogue ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( OrkWarrior ) == c.Owner.GetType() ){ sName = "orcish bile"; }
							else if ( typeof( PackHorse ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( PackMule ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( PackLlama ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Panther ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Bobcat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Phantom ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Phoenix ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( Pig ) == c.Owner.GetType() ){ sName = "pig snouts"; }
							else if ( typeof( PirateCaptain ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( PirateCrew ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( PirateCrewBow ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( PirateCrewMage ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( PirateLand ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( Pixie ) == c.Owner.GetType() ){ sName = "pixie sparkles"; }
							else if ( typeof( PoisonBeetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( PoisonBeetleRiding ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( Skellot ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( Alien ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( AlienSmall ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( Shaclaw ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( AntaurKing ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( AntaurProgenitor ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( AntaurSoldier ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( AntaurWorker ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( Lavapede ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( PoisonCloud ) == c.Owner.GetType() ){ sName = "poisonous gas"; }
							else if ( typeof( PoisonElemental ) == c.Owner.GetType() ){ sName = "poisonous gas"; }
							else if ( typeof( PolarBear ) == c.Owner.GetType() ){ sName = "bear hairs"; }
							else if ( typeof( PredatorHellCat ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Quagmire ) == c.Owner.GetType() ){ sName = "gore"; }
							else if ( typeof( QuartzElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( Rabbit ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( LightningElemental ) == c.Owner.GetType() ){ sName = "elemental powder"; }
							else if ( typeof( Raptor ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Raptus ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Lurker ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Rat ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( DiseasedRat ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( Ratman ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( RatmanArcher ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( RatmanMage ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( Ravenous ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( RavenousRiding ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( RaptorRiding ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Reaper ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( RestlessSoul ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( RevenantLion ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( RidableLlama ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( Ridgeback ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Rogue ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( RottingCorpse ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( WalkingCorpse ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( RuneBeetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( MutantLizardman ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Sakleth ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SaklethArcher ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SaklethMage ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( Reptalar ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( ReptalarShaman ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( ReptalarChieftain ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SandGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( SandSpider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( SandVortex ) == c.Owner.GetType() ){ sName = "mystical dirt"; }
							else if ( typeof( Satyr ) == c.Owner.GetType() ){ sName = "bony horns"; }
							else if ( typeof( Xatyr ) == c.Owner.GetType() ){ sName = "bony horns"; }
							else if ( typeof( Savage ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( SavageRider ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( SavageRidgeback ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SavageShaman ) == c.Owner.GetType() ){ sName = "human blood"; }
							else if ( typeof( AxeBeak ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( Scorpion ) == c.Owner.GetType() ){ sName = "scorpion stingers"; }
							else if ( typeof( SeaGiant ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( DeepSeaGiant ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SeaHorse ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SeaSerpent ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SeaSnake ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SeaDrake ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( Wyvra ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( BottleDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( Dragons ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( Dragonogre ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( RadiationDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( CrystalDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( VoidDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( ElderDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( GemDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( Sewerrat ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( Mouse ) == c.Owner.GetType() ){ sName = "rat tails"; }
							else if ( typeof( Shade ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( ShadowDemon ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( ShadowFiend ) == c.Owner.GetType() ){ sName = "darkness"; }
							else if ( typeof( ShadowHound ) == c.Owner.GetType() ){ sName = "darkness"; }
							else if ( typeof( Vrock ) == c.Owner.GetType() ){ sName = "darkness"; }
							else if ( typeof( ShadowIronElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( ShadowWisp ) == c.Owner.GetType() ){ sName = "wisp light"; }
							else if ( typeof( ShadowWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( Sheep ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( SilverSerpent ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( BloodSnake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SilverSteed ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( SkeletalDragon ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Dracolich ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletonDragon ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalKnight ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalMage ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalWarrior ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( GraveSeeker ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( GraveDustElemental ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalMount ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletalWizard ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Skeleton ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletonArcher ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( SkeletonHorse ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Slime ) == c.Owner.GetType() ){ sName = "slime"; }
							else if ( typeof( Snake ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SnowElemental ) == c.Owner.GetType() ){ sName = "enchanted frost"; }
							else if ( typeof( SnowHarpy ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( SnowLeopard ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Tiger ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( Jaguar ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( WhiteTiger ) == c.Owner.GetType() ){ sName = "cat whiskers"; }
							else if ( typeof( SnowOstard ) == c.Owner.GetType() ){ sName = "ostard scales"; }
							else if ( typeof( SoulWorm ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( SpectralGargoyle ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Spectre ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Kull ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( GhostDragyn ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( GrundulVarg ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Murk ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( LostKnight ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Spectres ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Spirit ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Sphinx ) == c.Owner.GetType() ){ sName = "sphinx fur"; }
							else if ( typeof( AncientSphinx ) == c.Owner.GetType() ){ sName = "sphinx fur"; }
							else if ( typeof( RoyalSphinx ) == c.Owner.GetType() ){ sName = "sphinx fur"; }
							else if ( typeof( SpinelElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( Squirrel ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( StarRubyElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( XormiteElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( DilithiumElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( TrilithiumElemental ) == c.Owner.GetType() ){ sName = "crushed gems"; }
							else if ( typeof( StoneGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( StoneHarpy ) == c.Owner.GetType() ){ sName = "quills"; }
							else if ( typeof( StormGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( CloudGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( StarGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( AbyssGiant ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( StrangleVine ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( Succubus ) == c.Owner.GetType() ){ sName = "succubus pheromones"; }
							else if ( typeof( SwampDragon ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( SwampTentacle ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( SwampTroll ) == c.Owner.GetType() ){ sName = "troll claws"; }
							else if ( typeof( TrollWitchDoctor ) == c.Owner.GetType() ){ sName = "troll claws"; }
							else if ( typeof( FrostTrollShaman ) == c.Owner.GetType() ){ sName = "troll claws"; }
							else if ( typeof( TerathanAvenger ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( TerathanDrone ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( TerathanMatriarch ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( TerathanWarrior ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( TimberWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( NecroticHound ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Fox ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Titan ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( ElderTitan ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( TopazElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( ToxicElemental ) == c.Owner.GetType() ){ sName = "poisonous gas"; }
							else if ( typeof( Xenomorph ) == c.Owner.GetType() ){ sName = "gore"; }
							else if ( typeof( Xenomutant ) == c.Owner.GetType() ){ sName = "gore"; }
							else if ( typeof( AcidPuddle ) == c.Owner.GetType() ){ sName = "poisonous gas"; }
							else if ( typeof( Tritun ) == c.Owner.GetType() ){ sName = "fish scales"; }
							else if ( typeof( TritunMage ) == c.Owner.GetType() ){ sName = "fish scales"; }
							else if ( typeof( Troll ) == c.Owner.GetType() ){ sName = "troll claws"; }
							else if ( typeof( TropicalBird ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( SwampBird ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( DesertBird ) == c.Owner.GetType() ){ sName = "bird beaks"; }
							else if ( typeof( GuardianWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( TundraOgre ) == c.Owner.GetType() ){ sName = "ogre thumbs"; }
							else if ( typeof( Typhoon ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( UndeadDruid ) == c.Owner.GetType() ){ sName = "bone powder"; }
							else if ( typeof( Unicorn ) == c.Owner.GetType() ){ sName = "unicorn teeth"; }
							else if ( typeof( ValoriteElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( Vampire ) == c.Owner.GetType() ){ sName = "vampire fangs"; }
							else if ( typeof( AlbinoBat ) == c.Owner.GetType() ){ sName = "bat ears"; }
							else if ( typeof( VampireBat ) == c.Owner.GetType() ){ sName = "bat ears"; }
							else if ( typeof( VampireLord ) == c.Owner.GetType() ){ sName = "vampire fangs"; }
							else if ( typeof( VampireWoods ) == c.Owner.GetType() ){ sName = "vampire fangs"; }
							else if ( typeof( VampirePrince ) == c.Owner.GetType() ){ sName = "vampire fangs"; }
							else if ( typeof( Dracula ) == c.Owner.GetType() ){ sName = "vampire fangs"; }
							else if ( typeof( VeriteElemental ) == c.Owner.GetType() ){ sName = "crushed stone"; }
							else if ( typeof( Viscera ) == c.Owner.GetType() ){ sName = "gore"; }
							else if ( typeof( VolcanicDragon ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( AshDragon ) == c.Owner.GetType() ){ sName = "magical ashes"; }
							else if ( typeof( VorpalBunny ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( WailingBanshee ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( DragonGhost ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( WalkingReaper ) == c.Owner.GetType() ){ sName = "enchanted sap"; }
							else if ( typeof( Walrus ) == c.Owner.GetType() ){ sName = "ivory pieces"; }
							else if ( typeof( WaterSpawn ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( WaterElemental ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( DeepWaterElemental ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( WaterNaga ) == c.Owner.GetType() ){ sName = "reptile scales"; }
							else if ( typeof( WereWolf ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( WolfMan ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( Gnoll ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( WhippingVine ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( WhiteDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( WhiteRabbit ) == c.Owner.GetType() ){ sName = "animal tongues"; }
							else if ( typeof( WhiteWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( WinterWolf ) == c.Owner.GetType() ){ sName = "dog hairs"; }
							else if ( typeof( WhiteWyrm ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( Wyrms ) == c.Owner.GetType() ){ sName = "wyrm spit"; }
							else if ( typeof( ArcticEttin ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( AncientEttin ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( EttinShaman ) == c.Owner.GetType() ){ sName = "giant blood"; }
							else if ( typeof( Wisp ) == c.Owner.GetType() ){ sName = "wisp light"; }
							else if ( typeof( WoodenGolem ) == c.Owner.GetType() ){ sName = "wood splinters"; }
							else if ( typeof( DriftwoodElemental ) == c.Owner.GetType() ){ sName = "wood splinters"; }
							else if ( typeof( Wraith ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( SeaGhost ) == c.Owner.GetType() ){ sName = "ghostly mist"; }
							else if ( typeof( Wyvern ) == c.Owner.GetType() ){ sName = "wyvern poison"; }
							else if ( typeof( Wyverns ) == c.Owner.GetType() ){ sName = "wyvern poison"; }
							else if ( typeof( AncientWyvern ) == c.Owner.GetType() ){ sName = "wyvern poison"; }
							else if ( typeof( xDryad ) == c.Owner.GetType() ){ sName = "dryad tears"; }
							else if ( typeof( Serpentaur ) == c.Owner.GetType() ){ sName = "bony horns"; }
							else if ( typeof( HookHorror ) == c.Owner.GetType() ){ sName = "bony horns"; }
							else if ( typeof( Yeti ) == c.Owner.GetType() ){ sName = "yeti claws"; }
							else if ( typeof( AbrozChieftain ) == c.Owner.GetType() ){ sName = "scaly fingers"; }
							else if ( typeof( AbrozShaman ) == c.Owner.GetType() ){ sName = "scaly fingers"; }
							else if ( typeof( AbrozWarrior ) == c.Owner.GetType() ){ sName = "scaly fingers"; }
							else if ( typeof( Zombie ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( ZombieMage ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( BaronAlmric ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( SeaZombie ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( UndeadGiant ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( ZombieGiant ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( TitanLich ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( Wight ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( ZombieDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( ZombieGargoyle ) == c.Owner.GetType() ){ sName = "gargoyle horns"; }
							else if ( typeof( AquaticGhoul ) == c.Owner.GetType() ){ sName = "dead skin"; }
							else if ( typeof( SeaWeeder ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( WaterBeetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( WaterBeetleRiding ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( TigerBeetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( TigerBeetleRiding ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( GlowBeetle ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( GlowBeetleRiding ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( UmberHulk ) == c.Owner.GetType() ){ sName = "insect ichor"; }
							else if ( typeof( WaterStrider ) == c.Owner.GetType() ){ sName = "spider legs"; }
							else if ( typeof( OilSlick ) == c.Owner.GetType() ){ sName = "oil"; }
							else if ( typeof( FloatingEye ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( SeaTroll ) == c.Owner.GetType() ){ sName = "troll claws"; }
							else if ( typeof( GiantLamprey ) == c.Owner.GetType() ){ sName = "leech spit"; }
							else if ( typeof( Locathah ) == c.Owner.GetType() ){ sName = "fish scales"; }
							else if ( typeof( StormCloud ) == c.Owner.GetType() ){ sName = "electricity"; }
							else if ( typeof( SeaHag ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( WaterWeird ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SeaweedElemental ) == c.Owner.GetType() ){ sName = "cursed leaves"; }
							else if ( typeof( GiantEel ) == c.Owner.GetType() ){ sName = "electricity"; }
							else if ( typeof( GiantSquid ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( RottingSquid ) == c.Owner.GetType() ){ sName = "ink"; }
							else if ( typeof( EyeOfTheDeep ) == c.Owner.GetType() ){ sName = "gazing eyes"; }
							else if ( typeof( SeaHagGreater ) == c.Owner.GetType() ){ sName = "sea water"; }
							else if ( typeof( SeaDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( DeepSeaDragon ) == c.Owner.GetType() ){ sName = "dragon smoke"; }
							else if ( typeof( DemonOfTheSea ) == c.Owner.GetType() ){ sName = "demonic hellfire"; }
							else if ( typeof( BoneSailor ) == c.Owner.GetType() ){ sName = "bone powder"; }

							if ( sName != "" )
							{
								Server.Items.BottleOfParts.FillJar( sName, 0, guts );
								from.CheckSkill( SkillName.Anatomy, 0, 100 );
								from.AddToBackpack( guts );
								if ( from.Skills[SkillName.Forensics].Value < Utility.RandomMinMax( 1, 110 ) ) { m_Knife.HitPoints = m_Knife.HitPoints - 1; }
								from.SendMessage("You get a " + guts.Name + " from the corpse.");
							}
							else
							{
								from.AddToBackpack( new Jar() );
								from.SendMessage("These corpses never have anything extra of value.");
							}
							return;
						}
						else
						{
							from.AddToBackpack( new Jar() );
							from.SendLocalizedMessage( 500485 ); // You see nothing useful to carve from the corpse.
							if ( from.Skills[SkillName.Forensics].Value < Utility.RandomMinMax( 1, 110 ) ) { m_Knife.HitPoints = m_Knife.HitPoints - 1; }
							return;
						}

						c.VisitedByTaxidermist = true;
					}
					else
					{
						from.SendLocalizedMessage( 500485 ); // You see nothing useful to carve from the corpse.
						return;
					}
				}
			}
		}
	}
}

namespace Server.Items
{
	public class BottleOfParts : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public BottleOfParts() : base( 0x1007 )
		{
			Name = "jar of parts";
			Stackable = true;
		}

		public BottleOfParts( Serial serial ) : base( serial )
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

		public static void FillJar( string guts, int value, Item jar )
		{
			if ( guts == "abysmal essence" || value == 61 ){ jar.Hue = 0xA97; jar.Name = "jar of abysmal essence"; }
			else if ( guts == "angelic feathers" || value == 43 ){ jar.Hue = 0xB92; jar.Name = "jar of angelic feathers"; }
			else if ( guts == "animal tongues" || value == 45 ){ jar.Hue = 0xF1; jar.Name = "jar of animal tongues"; }
			else if ( guts == "ape ears" || value == 58 ){ jar.Hue = 0x3CC; jar.Name = "jar of ape ears"; }
			else if ( guts == "bat ears" || value == 53 ){ jar.Hue = 0x723; jar.Name = "jar of bat ears"; }
			else if ( guts == "bear hairs" || value == 16 ){ jar.Hue = 0x906; jar.Name = "jar of bear hairs"; }
			else if ( guts == "bird beaks" || value == 15 ){ jar.Hue = 0x38; jar.Name = "jar of bird beaks"; }
			else if ( guts == "bone powder" || value == 5 ){ jar.Hue = 0x482; jar.Name = "jar of bone powder"; }
			else if ( guts == "bony horns" || value == 49 ){ jar.Hue = 0x60B; jar.Name = "jar of bony horns"; }
			else if ( guts == "cat whiskers" || value == 24 ){ jar.Hue = 0x38A; jar.Name = "jar of cat whiskers"; }
			else if ( guts == "centaur fingers" || value == 25 ){ jar.Hue = 0x27B; jar.Name = "jar of centaur fingers"; }
			else if ( guts == "cow eyes" || value == 22 ){ jar.Hue = 0x367; jar.Name = "jar of cow eyes"; }
			else if ( guts == "crab meat" || value == 54 ){ jar.Hue = 0x5E6; jar.Name = "jar of crab meat"; }
			else if ( guts == "crushed gems" || value == 51 ){ jar.Hue = 0x495; jar.Name = "jar of crushed gems"; }
			else if ( guts == "crushed stone" || value == 2 ){ jar.Hue = 0x3CD; jar.Name = "jar of crushed stone"; }
			else if ( guts == "crystal shards" || value == 28 ){ jar.Hue = 0xA5C; jar.Name = "jar of crystal shards"; }
			else if ( guts == "cursed leaves" || value == 27 ){ jar.Hue = 0x2F6; jar.Name = "jar of cursed leaves"; }
			else if ( guts == "darkness" || value == 84 ){ jar.Hue = 0x497; jar.Name = "jar of darkness"; }
			else if ( guts == "dead skin" || value == 48 ){ jar.Hue = 0xB97; jar.Name = "jar of dead skin"; }
			else if ( guts == "demonic hellfire" || value == 12 ){ jar.Hue = 0x4EC; jar.Name = "jar of demonic hellfire"; }
			else if ( guts == "dog hairs" || value == 11 ){ jar.Hue = 0x908; jar.Name = "jar of dog hairs"; }
			else if ( guts == "dolphin teeth" || value == 36 ){ jar.Hue = 0xC4; jar.Name = "jar of dolphin teeth"; }
			else if ( guts == "dragon smoke" || value == 10 ){ jar.Hue = 0x963; jar.Name = "jar of dragon smoke"; }
			else if ( guts == "dried blood" || value == 18 ){ jar.Hue = 0x5B5; jar.Name = "jar of dried blood"; }
			else if ( guts == "dryad tears" || value == 73 ){ jar.Hue = 0x17D; jar.Name = "jar of dryad tears"; }
			else if ( guts == "electricity" || value == 41 ){ jar.Hue = 0x800; jar.Name = "jar of electricity"; }
			else if ( guts == "elemental powder" || value == 64 ){ jar.Hue = 0xB8F; jar.Name = "jar of elemental powder"; }
			else if ( guts == "elven blood" || value == 42 ){ jar.Hue = 0x5B5; jar.Name = "jar of elven blood"; }
			else if ( guts == "enchanted frost" || value == 46 ){ jar.Hue = 0x47E; jar.Name = "jar of enchanted frost"; }
			else if ( guts == "enchanted sap" || value == 30 ){ jar.Hue = 0x477; jar.Name = "jar of enchanted sap"; }
			else if ( guts == "entrails" || value == 1 ){ jar.Hue = 0x845; jar.Name = "jar of entrails"; }
			else if ( guts == "fish scales" || value == 87 ){ jar.Hue = 0x315; jar.Name = "jar of fish scales"; }
			else if ( guts == "frog tongues" || value == 23 ){ jar.Hue = 0x29; jar.Name = "jar of frog tongues"; }
			else if ( guts == "gargoyle horns" || value == 50 ){ jar.Hue = 0x14F; jar.Name = "jar of gargoyle horns"; }
			else if ( guts == "gazing eyes" || value == 14 ){ jar.Hue = 0x8BE; jar.Name = "jar of gazing eyes"; }
			else if ( guts == "ghostly mist" || value == 52 ){ jar.Hue = 0x481; jar.Name = "jar of ghostly mist"; }
			else if ( guts == "giant blood" || value == 29 ){ jar.Hue = 0x5B5; jar.Name = "jar of giant blood"; }
			else if ( guts == "gore" || value == 82 ){ jar.Hue = 0xA19; jar.Name = "jar of gore"; }
			else if ( guts == "hellish smoke" || value == 40 ){ jar.Hue = 0x963; jar.Name = "jar of hellish smoke"; }
			else if ( guts == "horrid breath" || value == 13 ){ jar.Hue = 0x3BC; jar.Name = "jar of horrid breath"; }
			else if ( guts == "human blood" || value == 8 ){ jar.Hue = 0x5B5; jar.Name = "jar of human blood"; }
			else if ( guts == "hydra urine" || value == 62 ){ jar.Hue = 0xFF; jar.Name = "jar of hydra urine"; }
			else if ( guts == "illithid brains" || value == 71 ){ jar.Hue = 0x1F; jar.Name = "jar of illithid brains"; }
			else if ( guts == "imp tails" || value == 63 ){ jar.Hue = 0x6E9; jar.Name = "jar of imp tails"; }
			else if ( guts == "ink" || value == 66 ){ jar.Hue = 0x497; jar.Name = "jar of ink"; }
			else if ( guts == "insect ichor" || value == 7 ){ jar.Hue = 0x291; jar.Name = "jar of insect ichor"; }
			else if ( guts == "ivory pieces" || value == 89 ){ jar.Hue = 0xB89; jar.Name = "jar of ivory pieces"; }
			else if ( guts == "jade chunks" || value == 67 ){ jar.Hue = 0xB93; jar.Name = "jar of jade chunks"; }
			else if ( guts == "large teeth" || value == 77 ){ jar.Hue = 0x30D; jar.Name = "jar of large teeth"; }
			else if ( guts == "leech spit" || value == 55 ){ jar.Hue = 0x4F8; jar.Name = "jar of leech spit"; }
			else if ( guts == "liquid fire" || value == 60 ){ jar.Hue = 0x48E; jar.Name = "jar of liquid fire"; }
			else if ( guts == "magical ashes" || value == 26 ){ jar.Hue = 0xB85; jar.Name = "jar of magical ashes"; }
			else if ( guts == "magical dust" || value == 38 ){ jar.Hue = 0x8A5; jar.Name = "jar of magical dust"; }
			else if ( guts == "minotaur hooves" || value == 72 ){ jar.Hue = 0x27D; jar.Name = "jar of minotaur hooves"; }
			else if ( guts == "mummy wraps" || value == 76 ){ jar.Hue = 0x399; jar.Name = "jar of mummy wraps"; }
			else if ( guts == "mystical air" || value == 3 ){ jar.Hue = 0x430; jar.Name = "jar of mystical air"; }
			else if ( guts == "mystical dirt" || value == 39 ){ jar.Hue = 0x8AA; jar.Name = "jar of mystical dirt"; }
			else if ( guts == "mystical mud" || value == 75 ){ jar.Hue = 542; jar.Name = "jar of mystical mud"; }
			else if ( guts == "ogre thumbs" || value == 9 ){ jar.Hue = 0x841; jar.Name = "jar of ogre thumbs"; }
			else if ( guts == "oil" || value == 44 ){ jar.Hue = 0x497; jar.Name = "jar of oil"; }
			else if ( guts == "oni fur" || value == 78 ){ jar.Hue = 0x906; jar.Name = "jar of oni fur"; }
			else if ( guts == "orcish bile" || value == 79 ){ jar.Hue = 0x2F1; jar.Name = "jar of orcish bile"; }
			else if ( guts == "ostard scales" || value == 35 ){ jar.Hue = 0x173; jar.Name = "jar of ostard scales"; }
			else if ( guts == "pig snouts" || value == 20 ){ jar.Hue = 0x15F; jar.Name = "jar of pig snouts"; }
			else if ( guts == "pixie sparkles" || value == 80 ){ jar.Hue = 0x68A; jar.Name = "jar of pixie sparkles"; }
			else if ( guts == "poisonous gas" || value == 81 ){ jar.Hue = 0x559; jar.Name = "jar of poisonous gas"; }
			else if ( guts == "quills" || value == 59 ){ jar.Hue = 0x6C0; jar.Name = "jar of quills"; }
			else if ( guts == "rat tails" || value == 56 ){ jar.Hue = 0x709; jar.Name = "jar of rat tails"; }
			else if ( guts == "reptile scales" || value == 4 ){ jar.Hue = 0x29C; jar.Name = "jar of reptile scales"; }
			else if ( guts == "scaly fingers" || value == 65 ){ jar.Hue = 0x14E; jar.Name = "jar of scaly fingers"; }
			else if ( guts == "scorpion stingers" || value == 33 ){ jar.Hue = 0x4F5; jar.Name = "jar of scorpion stingers"; }
			else if ( guts == "sea water" || value == 34 ){ jar.Hue = 0x65; jar.Name = "jar of sea water"; }
			else if ( guts == "silver shavings" || value == 68 ){ jar.Hue = 0x835; jar.Name = "jar of silver shavings"; }
			else if ( guts == "slime" || value == 17 ){ jar.Hue = 0xB93; jar.Name = "jar of slime"; }
			else if ( guts == "sphinx fur" || value == 85 ){ jar.Hue = 0x6E9; jar.Name = "jar of sphinx fur"; }
			else if ( guts == "spider legs" || value == 37 ){ jar.Hue = 0x259; jar.Name = "jar of spider legs"; }
			else if ( guts == "sprite teeth" || value == 74 ){ jar.Hue = 0x48D; jar.Name = "jar of sprite teeth"; }
			else if ( guts == "succubus pheromones" || value == 86 ){ jar.Hue = 0xEC; jar.Name = "jar of succubus pheromones"; }
			else if ( guts == "swamp gas" || value == 21 ){ jar.Hue = 0x5A6; jar.Name = "jar of swamp gas"; }
			else if ( guts == "troll claws" || value == 47 ){ jar.Hue = 0x168; jar.Name = "jar of troll claws"; }
			else if ( guts == "unicorn teeth" || value == 31 ){ jar.Hue = 0x430; jar.Name = "jar of unicorn teeth"; }
			else if ( guts == "vampire fangs" || value == 88 ){ jar.Hue = 0xB89; jar.Name = "jar of vampire fangs"; }
			else if ( guts == "wax shavings" || value == 69 ){ jar.Hue = 0x47E; jar.Name = "jar of wax shavings"; }
			else if ( guts == "wisp light" || value == 32 ){ jar.Hue = 0x491; jar.Name = "jar of wisp light"; }
			else if ( guts == "wood splinters" || value == 90 ){ jar.Hue = 0x7DA; jar.Name = "jar of wood splinters"; }
			else if ( guts == "worm guts" || value == 19 ){ jar.Hue = 0x709; jar.Name = "jar of worm guts"; }
			else if ( guts == "wyrm spit" || value == 6 ){ jar.Hue = 0x487; jar.Name = "jar of wyrm spit"; }
			else if ( guts == "wyvern poison" || value == 91 ){ jar.Hue = 0x5AA; jar.Name = "jar of wyvern poison"; }
			else if ( guts == "yeti claws" || value == 93 ){ jar.Hue = 0x3D5; jar.Name = "jar of yeti claws"; }
		}
	}
}