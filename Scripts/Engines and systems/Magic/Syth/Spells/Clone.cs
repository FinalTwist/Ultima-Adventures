using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Regions;
using Server.Mobiles;

namespace Server.Spells.Syth
{
	public class CloneBody : SythSpell
	{
		public override int spellIndex { get { return 279; } }
		public int CirclePower = 8;
		public static int spellID = 279;
		public override int RequiredTithing{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 10 )); } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 0.5 ); } }
		public override double RequiredSkill{ get{ return (double)(Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 2 ))); } }
		public override int RequiredMana{ get{ return Int32.Parse(  Server.Spells.Syth.SythSpell.SpellInfo( spellIndex, 3 )); } }

		private static SpellInfo m_Info = new SpellInfo(
				Server.Spells.Syth.SythSpell.SpellInfo( spellID, 1 ),
				Server.Misc.Research.CapsCast( Server.Spells.Syth.SythSpell.SpellInfo( spellID, 4 ) ),
				203,
				0
			);

		public CloneBody( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( CheckFizzle() )
			{
				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				if ( item is SoulOrb )
				{
					SoulOrb myOrb = (SoulOrb)item;
					if ( myOrb.m_Owner == Caster )
					{
						targets.Add( item );
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];
					item.Delete();
				}

				Caster.PlaySound( 0x243 );
				Effects.SendLocationEffect(Caster.Location, Caster.Map, 0x373A, 15, 0x81F, 0);
				Caster.SendMessage( "You create a cloning crystal with your genetic pattern." );
				SoulOrb iOrb = new SoulOrb();
				iOrb.m_Owner = Caster;
				iOrb.Name = "cloning crystal";
				iOrb.ItemID = 0x705;
				Caster.AddToBackpack( iOrb );
				Server.Items.SoulOrb.OnSummoned( Caster, iOrb );
			}
		}
	}
}