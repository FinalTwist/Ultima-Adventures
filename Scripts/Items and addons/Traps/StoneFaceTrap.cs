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
	public enum StoneFaceTrapType
	{
		NorthWestWall,
		NorthWall,
		WestWall
	}

	public class StoneFaceTrap : BaseTrap
	{
		[CommandProperty( AccessLevel.GameMaster )]
		public StoneFaceTrapType Type
		{
			get
			{
				switch ( ItemID )
				{
					case 0x10F5: case 0x10F6: case 0x10F7: return StoneFaceTrapType.NorthWestWall;
					case 0x10FC: case 0x10FD: case 0x10FE: return StoneFaceTrapType.NorthWall;
					case 0x110F: case 0x1110: case 0x1111: return StoneFaceTrapType.WestWall;
				}

				return StoneFaceTrapType.NorthWestWall;
			}
			set
			{
				bool breathing = this.Breathing;

				ItemID = ( breathing ? GetFireID( value ) : GetBaseID( value ) );
			}
		}

		public bool Breathing
		{
			get{ return ( ItemID == GetFireID( this.Type ) ); }
			set
			{
				if ( value )
					ItemID = GetFireID( this.Type );
				else
					ItemID = GetBaseID( this.Type );
			}
		}

		public static int GetBaseID( StoneFaceTrapType type )
		{
			switch ( type )
			{
				case StoneFaceTrapType.NorthWestWall: return 0x10F5;
				case StoneFaceTrapType.NorthWall: return 0x10FC;
				case StoneFaceTrapType.WestWall: return 0x110F;
			}

			return 0;
		}

		public static int GetFireID( StoneFaceTrapType type )
		{
			switch ( type )
			{
				case StoneFaceTrapType.NorthWestWall: return 0x10F7;
				case StoneFaceTrapType.NorthWall: return 0x10FE;
				case StoneFaceTrapType.WestWall: return 0x1111;
			}

			return 0;
		}

		[Constructable]
		public StoneFaceTrap() : base( 0x10FC )
		{
			Light = LightType.Circle225;
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 2; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.Zero; } }

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

			Effects.PlaySound( Location, Map, 0x359 );

			Breathing = true;

			Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerCallback( FinishBreath ) );
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( TriggerDamage ) );

			LoggingFunctions.LogTraps( from, "a trapped stone face" );
		}

		public virtual void FinishBreath()
		{
			Breathing = false;
		}

		public virtual void TriggerDamage()
		{
			int itHurts = 0;

			foreach ( Mobile mob in GetMobilesInRange( 1 ) )
			{
				if ( mob.Alive && !mob.IsDeadBondedPet && mob.AccessLevel == AccessLevel.Player && mob.Player )
				{
					itHurts = (int)( (Utility.RandomMinMax(50,200) * ( 100 - mob.FireResistance ) ) / 100 );
					Spells.SpellHelper.Damage( TimeSpan.FromTicks( 1 ), mob, mob, itHurts ); // WIZARD
				}
			}
		}

		public StoneFaceTrap( Serial serial ) : base( serial )
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

			Breathing = false;
		}
	}

	public class StoneFaceTrapNoDamage : StoneFaceTrap
	{
		[Constructable]
		public StoneFaceTrapNoDamage()
		{
		}

		public StoneFaceTrapNoDamage( Serial serial ) : base( serial )
		{
		}

		public override void TriggerDamage()
		{
			// nothing..
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