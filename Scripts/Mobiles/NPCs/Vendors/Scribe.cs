using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Scribe : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.LibrariansGuild; } }

		[Constructable]
		public Scribe() : base( "the scribe" )
		{
			Job = JobFragment.scholar;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.EvalInt, 60.0, 83.0 );
			SetSkill( SkillName.Inscribe, 90.0, 100.0 );
			SetSkill( SkillName.ItemID, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBScribe() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( Utility.RandomNeutralHue() ) );
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The Written Word", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Scribe" ) ));
					}
				}
            }
        }

		///////////////////////////////////////////////////////////////////////////

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				string sMessage = "";

				if ( dropped.Amount == 500 )
				{
					if ( from.Skills[SkillName.Inscribe].Value >= 30 )
					{
						if ( Server.Misc.Research.AlreadyHasBag( from ) )
						{
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Here. You already have a pack." ) ); 
						}
						else
						{
							ResearchBag bag = new ResearchBag();
							from.PlaySound( 0x2E6 );
							Server.Misc.Research.SetupBag( from, bag );
							from.AddToBackpack( bag );
							this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Good luck with your research." ) ); 
							dropped.Delete();
						}
					}
					else
					{
						sMessage = "You need to be a neophyte scribe before I sell that to you.";
						from.AddToBackpack ( dropped );
					}
				}
				else
				{
					sMessage = "You look like you need this more than I do.";
					from.AddToBackpack ( dropped );
				}

				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, sMessage, from.NetState);
			}
			else if ( dropped is Runebook )
			{
				if ( dropped.ItemID == 0x22C5 ){ dropped.ItemID = 0x0F3D; }
				else if ( dropped.ItemID == 0x0F3D ){ dropped.ItemID = 0x5687; }
				else if ( dropped.ItemID == 0x5687 ){ dropped.ItemID = 0x4F50; }
				else if ( dropped.ItemID == 0x4F50 ){ dropped.ItemID = 0x4F51; }
				else if ( dropped.ItemID == 0x4F51 ){ dropped.ItemID = 0x5463; }
				else if ( dropped.ItemID == 0x5449 ){ dropped.ItemID = 0x544A; }
				else { dropped.ItemID = 0x22C5; }

				from.PlaySound( 0x249 );
				from.AddToBackpack ( dropped );
				this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have changed the cover of your book.", from.NetState);
			}

			return base.OnDragDrop( from, dropped );
		}

		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Scribe m_Scribe;
			private Mobile m_From;

			public FixEntry( Scribe Scribe, Mobile from ) : base( 6120, 12 )
			{
				m_Scribe = Scribe;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Scribe.BeginRepair( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new FixEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

        public void BeginRepair(Mobile from)
        {
            if ( Deleted || !from.Alive )
                return;

			int nCost = 50;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				SayTo(from, "Since you are begging, do you still want me to identify an unknown scroll, it will only cost you  " + nCost.ToString() + " gold?");
			}
			else { SayTo(from, "If you want me to identify an unknown scroll, it will cost you  " + nCost.ToString() + " gold."); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Scribe m_Scribe;

            public RepairTarget(Scribe mage) : base(12, false, TargetFlags.None)
            {
                m_Scribe = mage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				int nCost = 50;

				if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
				{
					nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				}

                if (targeted is UnknownScroll && from.Backpack != null)
                {
                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS

                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));

						m_Scribe.PlaySound(0x249);
						UnknownScroll rolls = (UnknownScroll)targeted;

						int paperType = 1;

						if ( rolls.ScrollType == 1 ) // MAGERY
						{
							if ( rolls.ScrollLevel == 2 ){ paperType = Utility.RandomMinMax( 13, 24 ); }
							else if ( rolls.ScrollLevel == 3 ){ paperType = Utility.RandomMinMax( 25, 36 ); }
							else if ( rolls.ScrollLevel == 4 ){ paperType = Utility.RandomMinMax( 37, 48 ); }
							else if ( rolls.ScrollLevel == 5 ){ paperType = Utility.RandomMinMax( 49, 60 ); }
							else if ( rolls.ScrollLevel == 6 ){ paperType = Utility.RandomMinMax( 57, 64 ); }
							else { paperType = Utility.RandomMinMax( 1, 12 ); }
						}
						else if ( rolls.ScrollType == 3 ) // BARD
						{
							paperType = Utility.RandomMinMax( 82, 97 );
						}
						else
						{
							if ( rolls.ScrollLevel == 2 ){ paperType = Utility.RandomMinMax( 68, 70 ); }
							else if ( rolls.ScrollLevel == 3 ){ paperType = Utility.RandomMinMax( 71, 73 ); }
							else if ( rolls.ScrollLevel == 4 ){ paperType = Utility.RandomMinMax( 74, 76 ); }
							else if ( rolls.ScrollLevel == 5 ){ paperType = Utility.RandomMinMax( 77, 79 ); }
							else if ( rolls.ScrollLevel == 6 ){ paperType = Utility.RandomMinMax( 80, 81 ); }
							else { paperType = Utility.RandomMinMax( 65, 67 ); }
						}

						string paperName = "";

						if ( Utility.RandomMinMax( 1, 100 ) > 10 )
						{
							if ( paperType == 1 ){ from.AddToBackpack( new ReactiveArmorScroll() ); paperName = "reactive armor"; }
							else if ( paperType == 2 ){ from.AddToBackpack( new ClumsyScroll() ); paperName = "clumsy"; }
							else if ( paperType == 3 ){ from.AddToBackpack( new CreateFoodScroll() ); paperName = "create food"; }
							else if ( paperType == 4 ){ from.AddToBackpack( new FeeblemindScroll() ); paperName = "feeblemind"; }
							else if ( paperType == 5 ){ from.AddToBackpack( new HealScroll() ); paperName = "heal"; }
							else if ( paperType == 6 ){ from.AddToBackpack( new MagicArrowScroll() ); paperName = "magic arrow"; }
							else if ( paperType == 7 ){ from.AddToBackpack( new NightSightScroll() ); paperName = "night sight"; }
							else if ( paperType == 8 ){ from.AddToBackpack( new WeakenScroll() ); paperName = "weaken"; }
							else if ( paperType == 9 ){ from.AddToBackpack( new AgilityScroll() ); paperName = "agility"; }
							else if ( paperType == 10 ){ from.AddToBackpack( new CunningScroll() ); paperName = "cunning"; }
							else if ( paperType == 11 ){ from.AddToBackpack( new CureScroll() ); paperName = "cure"; }
							else if ( paperType == 12 ){ from.AddToBackpack( new HarmScroll() ); paperName = "harm"; }
							else if ( paperType == 13 ){ from.AddToBackpack( new MagicTrapScroll() ); paperName = "magic trap"; }
							else if ( paperType == 14 ){ from.AddToBackpack( new MagicUnTrapScroll() ); paperName = "magic untrap"; }
							else if ( paperType == 15 ){ from.AddToBackpack( new ProtectionScroll() ); paperName = "protection"; }
							else if ( paperType == 16 ){ from.AddToBackpack( new StrengthScroll() ); paperName = "strength"; }
							else if ( paperType == 17 ){ from.AddToBackpack( new BlessScroll() ); paperName = "bless"; }
							else if ( paperType == 18 ){ from.AddToBackpack( new FireballScroll() ); paperName = "fireball"; }
							else if ( paperType == 19 ){ from.AddToBackpack( new MagicLockScroll() ); paperName = "magic lock"; }
							else if ( paperType == 20 ){ from.AddToBackpack( new PoisonScroll() ); paperName = "poison"; }
							else if ( paperType == 21 ){ from.AddToBackpack( new TelekinisisScroll() ); paperName = "telekinesis"; }
							else if ( paperType == 22 ){ from.AddToBackpack( new TeleportScroll() ); paperName = "teleport"; }
							else if ( paperType == 23 ){ from.AddToBackpack( new UnlockScroll() ); paperName = "unlock"; }
							else if ( paperType == 24 ){ from.AddToBackpack( new WallOfStoneScroll() ); paperName = "wall of stone"; }
							else if ( paperType == 25 ){ from.AddToBackpack( new ArchCureScroll() ); paperName = "arch cure"; }
							else if ( paperType == 26 ){ from.AddToBackpack( new ArchProtectionScroll() ); paperName = "arch protection"; }
							else if ( paperType == 27 ){ from.AddToBackpack( new CurseScroll() ); paperName = "curse"; }
							else if ( paperType == 28 ){ from.AddToBackpack( new FireFieldScroll() ); paperName = "fire field"; }
							else if ( paperType == 29 ){ from.AddToBackpack( new GreaterHealScroll() ); paperName = "greater heal"; }
							else if ( paperType == 30 ){ from.AddToBackpack( new LightningScroll() ); paperName = "lightning"; }
							else if ( paperType == 31 ){ from.AddToBackpack( new ManaDrainScroll() ); paperName = "mana drain"; }
							else if ( paperType == 32 ){ from.AddToBackpack( new RecallScroll() ); paperName = "recall"; }
							else if ( paperType == 33 ){ from.AddToBackpack( new BladeSpiritsScroll() ); paperName = "blade spirits"; }
							else if ( paperType == 34 ){ from.AddToBackpack( new DispelFieldScroll() ); paperName = "dispel field"; }
							else if ( paperType == 35 ){ from.AddToBackpack( new IncognitoScroll() ); paperName = "incognito"; }
							else if ( paperType == 36 ){ from.AddToBackpack( new MagicReflectScroll() ); paperName = "magic reflect"; }
							else if ( paperType == 37 ){ from.AddToBackpack( new MindBlastScroll() ); paperName = "mind blast"; }
							else if ( paperType == 38 ){ from.AddToBackpack( new ParalyzeScroll() ); paperName = "paralyze"; }
							else if ( paperType == 39 ){ from.AddToBackpack( new PoisonFieldScroll() ); paperName = "poison field"; }
							else if ( paperType == 40 ){ from.AddToBackpack( new SummonCreatureScroll() ); paperName = "summon creature"; }
							else if ( paperType == 41 ){ from.AddToBackpack( new DispelScroll() ); paperName = "dispel"; }
							else if ( paperType == 42 ){ from.AddToBackpack( new EnergyBoltScroll() ); paperName = "energy bolt"; }
							else if ( paperType == 43 ){ from.AddToBackpack( new ExplosionScroll() ); paperName = "explosion"; }
							else if ( paperType == 44 ){ from.AddToBackpack( new InvisibilityScroll() ); paperName = "invisibility"; }
							else if ( paperType == 45 ){ from.AddToBackpack( new MarkScroll() ); paperName = "mark"; }
							else if ( paperType == 46 ){ from.AddToBackpack( new MassCurseScroll() ); paperName = "mass curse"; }
							else if ( paperType == 47 ){ from.AddToBackpack( new ParalyzeFieldScroll() ); paperName = "paralyze field"; }
							else if ( paperType == 48 ){ from.AddToBackpack( new RevealScroll() ); paperName = "reveal"; }
							else if ( paperType == 49 ){ from.AddToBackpack( new ChainLightningScroll() ); paperName = "chain lightning"; }
							else if ( paperType == 50 ){ from.AddToBackpack( new EnergyFieldScroll() ); paperName = "energy field"; }
							else if ( paperType == 51 ){ from.AddToBackpack( new FlamestrikeScroll() ); paperName = "flamestrike"; }
							else if ( paperType == 52 ){ from.AddToBackpack( new GateTravelScroll() ); paperName = "gate travel"; }
							else if ( paperType == 53 ){ from.AddToBackpack( new ManaVampireScroll() ); paperName = "mana vampire"; }
							else if ( paperType == 54 ){ from.AddToBackpack( new MassDispelScroll() ); paperName = "mass dispel"; }
							else if ( paperType == 55 ){ from.AddToBackpack( new MeteorSwarmScroll() ); paperName = "meteor swarm"; }
							else if ( paperType == 56 ){ from.AddToBackpack( new PolymorphScroll() ); paperName = "polymorph"; }
							else if ( paperType == 57 ){ from.AddToBackpack( new EarthquakeScroll() ); paperName = "earthquake"; }
							else if ( paperType == 58 ){ from.AddToBackpack( new EnergyVortexScroll() ); paperName = "energy vortex"; }
							else if ( paperType == 59 ){ from.AddToBackpack( new ResurrectionScroll() ); paperName = "resurrection"; }
							else if ( paperType == 60 ){ from.AddToBackpack( new SummonAirElementalScroll() ); paperName = "summon air elemental"; }
							else if ( paperType == 61 ){ from.AddToBackpack( new SummonDaemonScroll() ); paperName = "summon daemon"; }
							else if ( paperType == 62 ){ from.AddToBackpack( new SummonEarthElementalScroll() ); paperName = "summon earth elemental"; }
							else if ( paperType == 63 ){ from.AddToBackpack( new SummonFireElementalScroll() ); paperName = "summon fire elemental"; }
							else if ( paperType == 64 ){ from.AddToBackpack( new SummonWaterElementalScroll() ); paperName = "summon water elemental"; }
							else if ( paperType == 65 ){ from.AddToBackpack( new CurseWeaponScroll() ); paperName = "curse weapon"; }
							else if ( paperType == 66 ){ from.AddToBackpack( new BloodOathScroll() ); paperName = "blood oath"; }
							else if ( paperType == 67 ){ from.AddToBackpack( new CorpseSkinScroll() ); paperName = "corpse skin"; }
							else if ( paperType == 68 ){ from.AddToBackpack( new EvilOmenScroll() ); paperName = "evil omen"; }
							else if ( paperType == 69 ){ from.AddToBackpack( new PainSpikeScroll() ); paperName = "pain spike"; }
							else if ( paperType == 70 ){ from.AddToBackpack( new WraithFormScroll() ); paperName = "wraith form"; }
							else if ( paperType == 71 ){ from.AddToBackpack( new MindRotScroll() ); paperName = "mind rot"; }
							else if ( paperType == 72 ){ from.AddToBackpack( new SummonFamiliarScroll() ); paperName = "summon familiar"; }
							else if ( paperType == 73 ){ from.AddToBackpack( new AnimateDeadScroll() ); paperName = "animate dead"; }
							else if ( paperType == 74 ){ from.AddToBackpack( new HorrificBeastScroll() ); paperName = "horrific beast"; }
							else if ( paperType == 75 ){ from.AddToBackpack( new PoisonStrikeScroll() ); paperName = "poison strike"; }
							else if ( paperType == 76 ){ from.AddToBackpack( new WitherScroll() ); paperName = "wither"; }
							else if ( paperType == 77 ){ from.AddToBackpack( new StrangleScroll() ); paperName = "strangle"; }
							else if ( paperType == 78 ){ from.AddToBackpack( new LichFormScroll() ); paperName = "lich form"; }
							else if ( paperType == 79 ){ from.AddToBackpack( new ExorcismScroll() ); paperName = "exorcism"; }
							else if ( paperType == 80 ){ from.AddToBackpack( new VengefulSpiritScroll() ); paperName = "vengeful spirit"; }
							else if ( paperType == 81 ){ from.AddToBackpack( new VampiricEmbraceScroll() ); paperName = "vampiric embrace"; }
							else if ( paperType == 82 ){ from.AddToBackpack( new ArmysPaeonScroll() ); paperName = "army's paeon sheet music"; }
							else if ( paperType == 83 ){ from.AddToBackpack( new EnchantingEtudeScroll() ); paperName = "enchanting etude sheet music"; }
							else if ( paperType == 84 ){ from.AddToBackpack( new EnergyCarolScroll() ); paperName = "energy carol sheet music"; }
							else if ( paperType == 85 ){ from.AddToBackpack( new EnergyThrenodyScroll() ); paperName = "energy threnody sheet music"; }
							else if ( paperType == 86 ){ from.AddToBackpack( new FireCarolScroll() ); paperName = "fire carol sheet music"; }
							else if ( paperType == 87 ){ from.AddToBackpack( new FireThrenodyScroll() ); paperName = "fire threnody sheet music"; }
							else if ( paperType == 88 ){ from.AddToBackpack( new FoeRequiemScroll() ); paperName = "foe requiem sheet music"; }
							else if ( paperType == 89 ){ from.AddToBackpack( new IceCarolScroll() ); paperName = "ice carol sheet music"; }
							else if ( paperType == 90 ){ from.AddToBackpack( new IceThrenodyScroll() ); paperName = "ice threnody sheet music"; }
							else if ( paperType == 91 ){ from.AddToBackpack( new KnightsMinneScroll() ); paperName = "knight's minne sheet music"; }
							else if ( paperType == 92 ){ from.AddToBackpack( new MagesBalladScroll() ); paperName = "mage's ballad sheet music"; }
							else if ( paperType == 93 ){ from.AddToBackpack( new MagicFinaleScroll() ); paperName = "magic finale sheet music"; }
							else if ( paperType == 94 ){ from.AddToBackpack( new PoisonCarolScroll() ); paperName = "poison carol sheet music"; }
							else if ( paperType == 95 ){ from.AddToBackpack( new PoisonThrenodyScroll() ); paperName = "poison threnody sheet music"; }
							else if ( paperType == 96 ){ from.AddToBackpack( new SheepfoeMamboScroll() ); paperName = "shepherd's dance sheet music"; }
							else { from.AddToBackpack( new SinewyEtudeScroll() ); paperName = "sinewy etude sheet music"; }

							m_Scribe.SayTo(from, "This seems to be a scroll of " + paperName + ".");
						}
						else
						{
							int nJunk = Utility.RandomMinMax( 1, 6 );

							switch( nJunk )
							{
								case 1: paperName = "useless scribbles"; break;
								case 2: paperName = "a useless recipe"; break;
								case 3: paperName = "a useless list of monsters"; break;
								case 4: paperName = "useless writings"; break;
								case 5: paperName = "a useless drawing"; break;
								case 6: paperName = "a useless map"; break;
							}
							m_Scribe.SayTo(from, "This seems to be " + paperName + ".");
						}

						rolls.Delete();
                    }
                    else
                    {
                        m_Scribe.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is ScrollClue )
				{
					Container packs = from.Backpack;
					nCost = 100;
					ScrollClue WhatIsIt = (ScrollClue)targeted;

					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
					}
					int toConsume = nCost;

                    if ( WhatIsIt.ScrollIntelligence == 0 )
                    {
                        m_Scribe.SayTo( from, "That was already deciphered by someone." );
					}
                    else if (packs.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( WhatIsIt.ScrollIntelligence >= 80 ){ WhatIsIt.Name = "diabolically coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 70 ){ WhatIsIt.Name = "ingeniously coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 60 ){ WhatIsIt.Name = "deviously coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 50 ){ WhatIsIt.Name = "cleverly coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 40 ){ WhatIsIt.Name = "adeptly coded parchment"; }
						else if ( WhatIsIt.ScrollIntelligence >= 30 ){ WhatIsIt.Name = "expertly coded parchment"; }
						else { WhatIsIt.Name = "plainly coded parchment"; }

						WhatIsIt.ScrollIntelligence = 0;
						WhatIsIt.InvalidateProperties();
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
						m_Scribe.SayTo(from, "Let me show you what this reads...");
						WhatIsIt.ScrollSolved = "Deciphered by " + m_Scribe.Name + " the Scribe";
						from.PlaySound( 0x249 );
						WhatIsIt.InvalidateProperties();
                    }
                    else
                    {
                        m_Scribe.SayTo(from, "It would cost you {0} gold to have that deciphered.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				else
				{
					m_Scribe.SayTo(from, "That does not need my services.");
				}
            }
        }

		public Scribe( Serial serial ) : base( serial )
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