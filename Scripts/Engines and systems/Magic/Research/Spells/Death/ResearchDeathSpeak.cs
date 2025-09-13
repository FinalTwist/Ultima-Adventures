using System;
using Server;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Spells.Research
{
	public class ResearchDeathSpeak : ResearchSpell
	{
		public override int spellIndex { get { return 2; } }
		public int CirclePower = 1;
		public static int spellID = 2;
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 8 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse( Server.Misc.Research.SpellInformation( spellIndex, 7 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Misc.Research.SpellInformation( spellID, 2 ),
				Server.Misc.Research.CapsCast( Server.Misc.Research.SpellInformation( spellID, 4 ) ),
				233,
				9042
			);

		public ResearchDeathSpeak( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Corpse toChannel = null;
			int regenerate = 0;

			int bonus = (int)(DamagingSkill( Caster )/4);
				if ( bonus > 60 ){ bonus = 60; }
				if ( bonus < 2 ){ bonus = 2; }

			foreach ( Item item in Caster.GetItemsInRange( 3 ) )
			{
				if( item is Corpse && !( (Corpse)item ).Channeled && !( (Corpse)item ).Animated )
				{
					Corpse body = (Corpse)item;

					if ( !(body.m_Owner is PlayerMobile) )
					{
						if ( body.m_Owner is BaseCreature )
						{
							SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );
							if ( undead.Slays(body.m_Owner) )
							{
								toChannel = (Corpse)item;
								regenerate = (int)( ( ( body.m_Owner ).Fame ) / 50) + bonus;
								break;
							}
						}
					}
				}
			}

			if ( regenerate > 0 )
			{
				if ( toChannel != null )
				{
					toChannel.Channeled = true;
					toChannel.Hue = 0x835;
				}

				int mana = Caster.ManaMax - Caster.Mana;
				if ( mana > regenerate ){ Caster.Mana = Caster.Mana + regenerate; regenerate = 0; }
				else { Caster.Mana = Caster.ManaMax; regenerate = regenerate - mana; }

				int stam = Caster.ManaMax - Caster.Stam;
				if ( stam > regenerate ){ Caster.Stam = Caster.Stam + regenerate; regenerate = 0; }
				else { Caster.Stam = Caster.StamMax; regenerate = regenerate - stam; }

				int hits = Caster.ManaMax - Caster.Hits;
				if ( hits > regenerate ){ Caster.Hits = Caster.Hits + regenerate; regenerate = 0; }
				else { Caster.Hits = Caster.HitsMax; regenerate = regenerate - hits; }

				if ( Caster.Karma < 0 )
				{
					Caster.FixedParticles( 0x3400, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
				}
				else
				{
					Caster.FixedParticles( 0x375A, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
				}

				if ( Caster.Karma < 0 )
				{
					Caster.Say( "Xtee Mee Glau" );
					Caster.PlaySound( 0x481 );
				}
				else
				{
					Caster.Say( "Anh Mi Sah Ko" );
					Caster.PlaySound( 0x24A );
				}

				Server.Misc.Research.ConsumeScroll( Caster, true, spellIndex, false );

				KarmaMod( Caster, ((int)RequiredSkill+RequiredMana) );

				Caster.SendMessage( "You speak in strange tongues to the soul of the dead supernatural creature." );
			}
			else
			{
				Caster.SendMessage( "You fail to speak to any dead supernatural creatures in the area." );
				Caster.PlaySound( 0x1D6 );
			}

			FinishSequence();
		}
	}
}