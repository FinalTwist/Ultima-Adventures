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
	public enum SawTrapType
	{
		WestWall,
		NorthWall,
		WestFloor,
		NorthFloor
	}

	public class SawTrap : BaseTrap
	{
		[CommandProperty( AccessLevel.GameMaster )]
		public SawTrapType Type
		{
			get
			{
				switch ( ItemID )
				{
					case 0x1103: return SawTrapType.NorthWall;
					case 0x1116: return SawTrapType.WestWall;
					case 0x11AC: return SawTrapType.NorthFloor;
					case 0x11B1: return SawTrapType.WestFloor;
				}

				return SawTrapType.NorthWall;
			}
			set
			{
				ItemID = GetBaseID( value );
			}
		}

		public static int GetBaseID( SawTrapType type )
		{
			switch ( type )
			{
				case SawTrapType.NorthWall: return 0x1103;
				case SawTrapType.WestWall: return 0x1116;
				case SawTrapType.NorthFloor: return 0x11AC;
				case SawTrapType.WestFloor: return 0x11B1;
			}

			return 0;
		}

		[Constructable]
		public SawTrap() : this( SawTrapType.NorthFloor )
		{
		}

		[Constructable]
		public SawTrap( SawTrapType type ) : base( GetBaseID( type ) )
		{
		}

		public override bool PassivelyTriggered{ get{ return false; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 0; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.0 ); } }

		public override void OnTrigger( Mobile from )
		{
			if ( !from.Alive || !from.Player || from.AccessLevel > AccessLevel.Player )
				return;

			if ( Server.Misc.SeeIfGemInBag.GemInPocket( from ) == true || Server.Misc.SeeIfJewelInBag.JewelInPocket( from ) == true )
				return;

			if ( HiddenTrap.CheckTrapAvoidance( from, this ) == 0 )
				return;

			if ( !from.Player )
				return;

			if ( from is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( from ) )
			{
				Point3D air = new Point3D( ( from.X+1 ), ( from.Y+1 ), ( from.Z+5 ) );
				Effects.SendLocationParticles(EffectItem.Create(air, from.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( from, 0 ), 0, 5022, 0);
				from.PlaySound( 0x014 );
				return;
			}

			Effects.SendLocationEffect( Location, Map, GetBaseID( this.Type ) + 1, 6, 3, GetEffectHue(), 0 );
			Effects.PlaySound( Location, Map, 0x21C );

			int itHurts = (int)( (Utility.RandomMinMax(50,200) * ( 100 - from.PhysicalResistance ) ) / 100 );
			Spells.SpellHelper.Damage( TimeSpan.FromTicks( 1 ), from, from, itHurts ); // WIZARD

			from.LocalOverheadMessage( MessageType.Regular, 0x22, 500853 ); // You stepped onto a blade trap!

			LoggingFunctions.LogTraps( from, "a sawing blade trap" );
		}

		public SawTrap( Serial serial ) : base( serial )
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