using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Jester
{
	public class SurpriseGift : JesterSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Surprise Gift", "Here is a gift for you!",
				-1,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3.0 ); } }
		public override int RequiredTithing{ get{ return 80; } }
		public override int RequiredMana{ get{ return 20; } }

		public SurpriseGift( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( !base.CheckCast( caster ) )
				return false;

			if( (Caster.Followers + 3) > Caster.FollowersMax )
			{
				Caster.SendMessage( "You have too many followers to wrap a gift." );
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
				string FoolName = "a present";
				int FoolHue = 0;
				int FoolBody = Utility.RandomList( 1027, 1028, 1029, 1030 );
				int FoolFroze = 1;

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