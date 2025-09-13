using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Jester
{
	public class PoppingBalloon : JesterSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Popping Balloon", "Would you like a balloon?",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
		public override int RequiredTithing{ get{ return 100; } }
		public override int RequiredMana{ get{ return 20; } }

		public PoppingBalloon( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage( "You have too many followers to blow up a balloon." );
				return false;
			}

			return true;
		}

		public override void OnCast()
		{
			Map map = Caster.Map;

			Point3D p = Caster.Location;

			if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				string FoolName = "a balloon";
				int FoolHue = Utility.RandomList( 0xB3D, 0xB3E, 0xB3F, 0xB40, 0xAD1, 0x9A2, 0x94C, 0x916, 0x947, 0x92E, 0x88E, 0x855 );
				int FoolBody = 1026;
				int FoolFroze = 0;

				Caster.Hidden = true;
				Server.Mobiles.SummonedPrank.MakePrankster( Caster, p, FoolName, FoolBody, FoolHue, FoolFroze );
				Caster.Hidden = false;

				Caster.PlaySound( Caster.Female ? 794 : 1066 );
				Caster.Say( "*giggles*" );
			}

			FinishSequence();
		}
	}
}