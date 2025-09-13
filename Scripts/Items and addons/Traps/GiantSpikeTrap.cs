using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using Server.Mobiles;
using System.Text;
using System.IO;

namespace Server.Items
{
	public class GiantSpikeTrap : BaseTrap
	{
		[Constructable]
		public GiantSpikeTrap() : base( 1 )
		{
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 3; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.0 ); } }

		public override void OnTrigger( Mobile from )
		{
			if ( !from.Player || !from.Alive || from.AccessLevel > AccessLevel.Player )
				return;

			if ( Server.Misc.SeeIfGemInBag.GemInPocket( from ) == true || Server.Misc.SeeIfJewelInBag.JewelInPocket( from ) == true )
				return;

			if ( HiddenTrap.CheckTrapAvoidance( from, this ) == 0 )
				return;

			if ( !from.Player )
				return;

			Effects.SendLocationEffect( Location, Map, 0x1D99, 48, 2, GetEffectHue(), 0 );

			if ( from.Alive && CheckRange( from.Location, 0 ) && from.Player )
			{
				int itHurts = (int)( (Utility.RandomMinMax(50,200) * ( 100 - from.PhysicalResistance ) ) / 100 );
				Spells.SpellHelper.Damage( TimeSpan.FromTicks( 1 ), from, from, itHurts ); // WIZARD
			}

			LoggingFunctions.LogTraps( from, "a giant spike trap" );
		}

		public GiantSpikeTrap( Serial serial ) : base( serial )
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