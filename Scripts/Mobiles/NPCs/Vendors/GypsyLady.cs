using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;
using System.Collections;
using Server.Gumps;

namespace Server.Mobiles
{
	public class GypsyLady : BasePerson
	{
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 4 ) && InLOS( m ) )
				{
					switch ( Utility.Random( 45 ))
					{
						case 0: Say("A reunion must not occur or the unlucky chimera must fold in the age of temptation."); break;
						case 1: Say("An injury shall happen."); break;
						case 2: Say("He shall not assimilate with the proud youth."); break;
						case 3: Say("They will not weave near an altar."); break;
						case 4: Say("She will finally intrude."); break;
						case 5: Say("The diamond possum shall not deflect near a fortress on a sunny day before the coming of beauty."); break;
						case 6: Say("A relationship ending will finally happen with the tired juggler during the planting season."); break;
						case 7: Say("The clever hamster will trespass with the saffron youth in a castle when the first flowers bloom."); break;
						case 8: Say("A betrayal will finally happen or he will babble at the coming of destruction."); break;
						case 9: Say("She will finally gutter in a market before it is too late."); break;
						case 10: Say("An introduction will finally happen or the seductive viper shall not worship with the fanatical druid during the planting season."); break;
						case 11: Say("The broken-hearted grandmother will act or he shall not meander."); break;
						case 12: Say("A defeat will not take place in a castle on a journey."); break;
						case 13: Say("The fearless trader shall not look with the violet druid."); break;
						case 14: Say("The honest runaway will succumb with the hungry hare before the coming of lust."); break;
						case 15: Say("It will judge in the summer."); break;
						case 16: Say("The emerald lion will never scare for the sake of winter."); break;
						case 17: Say("She will finally famish and a recovery will not happen near a well during a rainstorm."); break;
						case 18: Say("A fight shall take place for the sake of willpower."); break;
						case 19: Say("The fanatical wizard shall not wax near a holy site in the afternoon at the coming of luck."); break;
						case 20: Say("She shall not ascend after sunset."); break;
						case 21: Say("It will never pray with the shy zealot."); break;
						case 22: Say("A meeting must happen with the malicious cook."); break;
						case 23: Say("The remorseless muse will fraternize at the bridge."); break;
						case 24: Say("The russet berserker must gasp or the remorseless slave must not bicker with the triumphant lion near a farm on the spring equinox."); break;
						case 25: Say("He shall fence with the black countess near a portal."); break;
						case 26: Say("An introduction will not happen and the intelligent champion shall lower on the spring equinox."); break;
						case 27: Say("The hasty hostler must jump."); break;
						case 28: Say("An agreement must take place with the broken-hearted cleric."); break;
						case 29: Say("It will finally benefit in the citadel in the age of dreams."); break;
						case 30: Say("A financial difficulty will never take place or the clumsy zombie will finally weep at midnight."); break;
						case 31: Say("She shall weld or he will finally crush during the growing season."); break;
						case 32: Say("The greedy artist shall lace and the garnet general must not compose."); break;
						case 33: Say("The arrogant rogue must not comply with the indigo robber at the coming of joy."); break;
						case 34: Say("The lavender summoner will crush in a time of truth."); break;
						case 35: Say("A contest must not happen after the first frost."); break;
						case 36: Say("She must not consent in the age of entropy."); break;
						case 37: Say("The deluded pony will not forget in a graveyard on a windy day for the sake of alchemy."); break;
						case 38: Say("A reversal of fortune shall not happen and a fall shall not occur in the spring for the sake of lust."); break;
						case 39: Say("She will ensure in the citadel."); break;
						case 40: Say("The lazy juggler will finally enquire and a loss shall not occur."); break;
						case 41: Say("A promise will finally take place and it shall not bother during fall in a time of fear."); break;
						case 42: Say("He must not weary."); break;
						case 43: Say("The orange donkey shall not gutter at the bridge."); break;
						case 44: Say("The word to the dark mage depths is 'bravoka'."); break;
					};
					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public GypsyLady() : base( )
		{
			Job = JobFragment.gypsy;
			Karma = Utility.RandomMinMax( 13, -45 );
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			NameHue = -1;

			Body = 0x191;
			Name = NameList.RandomName( "female" );
			Title = "the gypsy";

			AddItem( new Kilt( Utility.RandomDyedHue() ) );
			AddItem( new Shirt( Utility.RandomDyedHue() ) );
			AddItem( new ThighBoots() );
			AddItem( new SkullCap( Utility.RandomDyedHue() ) );

			SetSkill( SkillName.Cooking, 65, 88 );
			SetSkill( SkillName.Snooping, 65, 88 );
			SetSkill( SkillName.Stealing, 65, 88 );
			SetSkill( SkillName.SpiritSpeak, 65, 88 );
			SetSkill( SkillName.Wrestling, 100 );

			SetStr( 100 );
			SetDex( 100 );
			SetInt( 100 );

			SetDamage( 15, 20 );
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			VirtualArmor = 30;

			Utility.AssignRandomHair( this );
			HairHue = Utility.RandomHairHue();
			FacialHairItemID = 0;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
						mobile.SendGump(new SpeechGump( "Visions of the Truth", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Gypsy" ) ));
					}
				}
            }
        }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private class TruthEntry : ContextMenuEntry
		{
			private GypsyLady m_GypsyLady;
			private Mobile m_From;

			public TruthEntry( GypsyLady GypsyLady, Mobile from ) : base( 2058, 12 )
			{
				m_GypsyLady = GypsyLady;
				m_From = from;
			}

			public override void OnClick()
			{
				m_GypsyLady.FindTruth( m_From );
			}
		}

        public void FindTruth( Mobile from )
        {
            if ( Deleted || !from.Alive )
                return;

			SayTo(from, "So you want me to reveal the truth of a parchment for you?");

            from.Target = new RevealTarget(this);
        }

        private class RevealTarget : Target
        {
            private GypsyLady m_GypsyLady;

            public RevealTarget( GypsyLady mage ) : base(12, false, TargetFlags.None)
            {
                m_GypsyLady = mage;
            }

            protected override void OnTarget( Mobile from, object targeted )
            {
				Container pack = from.Backpack;

				if ( targeted is ScrollClue )
				{
					ScrollClue scroll = (ScrollClue)targeted;

					int nCost = scroll.ScrollLevel * 100;

					if ( BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

					int toConsume = nCost;

					if ( scroll.ScrollIntelligence > 0 )
					{
						m_GypsyLady.SayTo(from, "That parchment hasn't been deciphered yet.");
					}
					else if (pack.ConsumeTotal(typeof(Gold), toConsume))
					{
						string WillSay = "";

						switch ( Utility.RandomMinMax( 0, 3 ) ) 
						{
							case 0: WillSay = "The spirits tell me that this parchment is"; break;
							case 1: WillSay = "My mind is showing me that this parchment is"; break;
							case 2: WillSay = "The voices all speak that this parchment is"; break;
							case 3: WillSay = "I can see beyond that this parchment is"; break;
						}

						if ( scroll.ScrollTrue == 1 )
						{
							m_GypsyLady.SayTo(from, WillSay + " truthfully written.");
						}
						else
						{
							m_GypsyLady.SayTo(from, WillSay + " falsely written.");
						}

						from.SendMessage(String.Format("You pay {0} gold.", toConsume));
					}
					else
					{
						m_GypsyLady.SayTo(from, "I require {0} gold for my visions.", toConsume);
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is SearchPage )
				{
					SearchPage scroll = (SearchPage)targeted;

					int nCost = ( 100 - scroll.LegendPercent ) * 50;

					if ( BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

					int toConsume = nCost;

					if (pack.ConsumeTotal(typeof(Gold), toConsume))
					{
						string WillSay = "";

						switch ( Utility.RandomMinMax( 0, 3 ) ) 
						{
							case 0: WillSay = "The spirits tell me that this legend "; break;
							case 1: WillSay = "My mind is showing me that this legend "; break;
							case 2: WillSay = "The voices all speak that this legend "; break;
							case 3: WillSay = "I can see beyond that this legend "; break;
						}

						if ( scroll.LegendReal == 1 )
						{
							m_GypsyLady.SayTo(from, WillSay + " really happened.");
						}
						else
						{
							m_GypsyLady.SayTo(from, WillSay + " never happened.");
						}

						from.SendMessage(String.Format("You pay {0} gold.", toConsume));
					}
					else
					{
						m_GypsyLady.SayTo(from, "I require {0} gold for my visions.", toConsume);
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is DynamicBook )
				{
					DynamicBook scroll = (DynamicBook)targeted;

					int nCost = (scroll.BookPower + 1) * 50;

					if ( BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

					int toConsume = nCost;

					if (pack.ConsumeTotal(typeof(Gold), toConsume))
					{
						string WillSay = "";

						switch ( Utility.RandomMinMax( 0, 3 ) ) 
						{
							case 0: WillSay = "The spirits tell me that this book "; break;
							case 1: WillSay = "My mind is showing me that this book "; break;
							case 2: WillSay = "The voices all speak that this book "; break;
							case 3: WillSay = "I can see beyond that this book "; break;
						}

						if ( scroll.BookTrue > 0 )
						{
							m_GypsyLady.SayTo(from, WillSay + " contains the truth.");
						}
						else
						{
							m_GypsyLady.SayTo(from, WillSay + " contains falsehoods.");
						}

						from.SendMessage(String.Format("You pay {0} gold.", toConsume));
					}
					else
					{
						m_GypsyLady.SayTo(from, "I require {0} gold for my visions.", toConsume);
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is SomeRandomNote )
				{
					SomeRandomNote scroll = (SomeRandomNote)targeted;

					int nCost = 100;

					if ( BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

					int toConsume = nCost;

					if (pack.ConsumeTotal(typeof(Gold), toConsume))
					{
						string WillSay = "";

						switch ( Utility.RandomMinMax( 0, 3 ) ) 
						{
							case 0: WillSay = "The spirits tell me that this parchment is"; break;
							case 1: WillSay = "My mind is showing me that this parchment is"; break;
							case 2: WillSay = "The voices all speak that this parchment is"; break;
							case 3: WillSay = "I can see beyond that this parchment is"; break;
						}

						if ( scroll.ScrollTrue == 1 )
						{
							m_GypsyLady.SayTo(from, WillSay + " truthfully written.");
						}
						else
						{
							m_GypsyLady.SayTo(from, WillSay + " falsely written.");
						}

						from.SendMessage(String.Format("You pay {0} gold.", toConsume));
					}
					else
					{
						m_GypsyLady.SayTo(from, "I require {0} gold for my visions.", toConsume);
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is DataPad )
				{
					int nCost = 100;

					if ( BaseVendor.BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost );
					}

					int toConsume = nCost;

					if (pack.ConsumeTotal(typeof(Gold), toConsume))
					{
						string WillSay = "";

						switch ( Utility.RandomMinMax( 0, 3 ) ) 
						{
							case 0: WillSay = "The spirits tell me that this glowing book is"; break;
							case 1: WillSay = "My mind is showing me that this glowing book is"; break;
							case 2: WillSay = "The voices all speak that this glowing book is"; break;
							case 3: WillSay = "I can see beyond that this glowing book is"; break;
						}

						m_GypsyLady.SayTo(from, WillSay + " truthfully written.");

						from.SendMessage(String.Format("You pay {0} gold.", toConsume));
					}
					else
					{
						m_GypsyLady.SayTo(from, "I require {0} gold for my visions.", toConsume);
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else
				{
					m_GypsyLady.SayTo(from, "That is not a book or parchment.");
				}
            }
        }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive )
			{
				list.Add( new TruthEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		public GypsyLady( Serial serial ) : base( serial )
		{
		}

		public override bool CanTeach { get { return true; } }

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