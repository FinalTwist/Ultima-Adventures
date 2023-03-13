using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Research
{
	public class ResearchSeeTruth : ResearchSpell
	{
		public override int spellIndex { get { return 7; } }
		public int CirclePower = 1;
		public static int spellID = 7;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				215,
				9001
			);

		public ResearchSeeTruth( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this, spellID );
		}

		private class InternalTarget : Target
		{
			private ResearchSeeTruth m_Owner;
			private int m_SpellIndex;

			public InternalTarget( ResearchSeeTruth owner, int spellIndex ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
				m_SpellIndex = spellIndex;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				bool consume = false;

				if ( targeted is ScrollClue )
				{
					ScrollClue scroll = (ScrollClue)targeted;
					consume = true;
					from.PlaySound( 0xF9 );

					if ( scroll.ScrollIntelligence > 0 )
					{
						from.SendMessage("That parchment hasn't been deciphered yet.");
					}
					else
					{
						string WillSay = "";

						switch ( Utility.RandomMinMax( 0, 3 ) ) 
						{
							case 0: WillSay = "The spirits tell you that this parchment is"; break;
							case 1: WillSay = "Your mind is showing you that this parchment is"; break;
							case 2: WillSay = "The voices all speak that this parchment is"; break;
							case 3: WillSay = "You can see beyond that this parchment is"; break;
						}

						if ( scroll.ScrollTrue == 1 )
						{
							from.SendMessage(WillSay + " truthfully written.");
						}
						else
						{
							from.SendMessage(WillSay + " falsely written.");
						}
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is SearchPage )
				{
					SearchPage scroll = (SearchPage)targeted;
					consume = true;
					from.PlaySound( 0xF9 );

					string WillSay = "";

					switch ( Utility.RandomMinMax( 0, 3 ) ) 
					{
						case 0: WillSay = "The spirits tell you that this legend "; break;
						case 1: WillSay = "Your mind is showing you that this legend "; break;
						case 2: WillSay = "The voices all speak that this legend "; break;
						case 3: WillSay = "You can see beyond that this legend "; break;
					}

					if ( scroll.LegendReal == 1 )
					{
						from.SendMessage(WillSay + " really happened.");
					}
					else
					{
						from.SendMessage(WillSay + " never happened.");
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is DynamicBook )
				{
					DynamicBook scroll = (DynamicBook)targeted;
					consume = true;
					from.PlaySound( 0xF9 );

					string WillSay = "";

					switch ( Utility.RandomMinMax( 0, 3 ) ) 
					{
						case 0: WillSay = "The spirits tell you that this book "; break;
						case 1: WillSay = "Your mind is showing you that this book "; break;
						case 2: WillSay = "The voices all speak that this book "; break;
						case 3: WillSay = "You can see beyond that this book "; break;
					}

					if ( scroll.BookTrue > 0 )
					{
						from.SendMessage(WillSay + " contains the truth.");
					}
					else
					{
						from.SendMessage(WillSay + " contains falsehoods.");
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is SomeRandomNote )
				{
					SomeRandomNote scroll = (SomeRandomNote)targeted;
					consume = true;
					from.PlaySound( 0xF9 );

					string WillSay = "";

					switch ( Utility.RandomMinMax( 0, 3 ) ) 
					{
						case 0: WillSay = "The spirits tell you that this parchment is"; break;
						case 1: WillSay = "Your mind is showing you that this parchment is"; break;
						case 2: WillSay = "The voices all speak that this parchment is"; break;
						case 3: WillSay = "You can see beyond that this parchment is"; break;
					}

					if ( scroll.ScrollTrue == 1 )
					{
						from.SendMessage(WillSay + " truthfully written.");
					}
					else
					{
						from.SendMessage(WillSay + " falsely written.");
					}
				}
				///////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is DataPad )
				{
					consume = true;
					from.PlaySound( 0xF9 );
					string WillSay = "";

					switch ( Utility.RandomMinMax( 0, 3 ) ) 
					{
						case 0: WillSay = "The spirits tell you that this glowing book is"; break;
						case 1: WillSay = "Your mind is showing you that this glowing book is"; break;
						case 2: WillSay = "The voices all speak that this glowing book is"; break;
						case 3: WillSay = "You can see beyond that this glowing book is"; break;
					}

					from.SendMessage(WillSay + " truthfully written.");
				}
				///////////////////////////////////////////////////////////////////////////////////
				else
				{
					from.SendMessage("That is not a book or parchment.");
				}

				if ( consume ){ Server.Misc.Research.ConsumeScroll( from, true, m_SpellIndex, false ); }
				m_Owner.FinishSequence();
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}