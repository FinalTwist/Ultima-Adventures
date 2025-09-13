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
	public enum SpikeTrapType
	{
		WestWall,
		NorthWall,
		WestFloor,
		NorthFloor
	}

	public class SpikeTrap : BaseTrap
	{
		[CommandProperty( AccessLevel.GameMaster )]
		public SpikeTrapType Type
		{
			get
			{
				switch ( ItemID )
				{
					case 4360: case 4361: case 4366: return SpikeTrapType.WestWall;
					case 4379: case 4380: case 4385: return SpikeTrapType.NorthWall;
					case 4506: case 4507: case 4511: return SpikeTrapType.WestFloor;
					case 4512: case 4513: case 4517: return SpikeTrapType.NorthFloor;
				}

				return SpikeTrapType.WestWall;
			}
			set
			{
				bool extended = this.Extended;

				ItemID = ( extended ? GetExtendedID( value ) : GetBaseID( value ) );
			}
		}

		public bool Extended
		{
			get{ return ( ItemID == GetExtendedID( this.Type ) ); }
			set
			{
				if ( value )
					ItemID = GetExtendedID( this.Type );
				else
					ItemID = GetBaseID( this.Type );
			}
		}

		public static int GetBaseID( SpikeTrapType type )
		{
			switch ( type )
			{
				case SpikeTrapType.WestWall: return 4360;
				case SpikeTrapType.NorthWall: return 4379;
				case SpikeTrapType.WestFloor: return 4506;
				case SpikeTrapType.NorthFloor: return 4512;
			}

			return 0;
		}

		public static int GetExtendedID( SpikeTrapType type )
		{
			return GetBaseID( type ) + GetExtendedOffset( type );
		}

		public static int GetExtendedOffset( SpikeTrapType type )
		{
			switch ( type )
			{
				case SpikeTrapType.WestWall: return 6;
				case SpikeTrapType.NorthWall: return 6;

				case SpikeTrapType.WestFloor: return 5;
				case SpikeTrapType.NorthFloor: return 5;
			}

			return 0;
		}

		[Constructable]
		public SpikeTrap() : this( SpikeTrapType.WestFloor )
		{
		}

		[Constructable]
		public SpikeTrap( SpikeTrapType type ) : base( GetBaseID( type ) )
		{
		}

		public override bool PassivelyTriggered{ get{ return false; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 0; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 6.0 ); } }

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

			Effects.SendLocationEffect( Location, Map, GetBaseID( this.Type ) + 1, 18, 3, GetEffectHue(), 0 );
			Effects.PlaySound( Location, Map, 0x22C );

			int itHurts = 0;

			foreach ( Mobile mob in GetMobilesInRange( 0 ) )
			{
				if ( mob.Alive && !mob.IsDeadBondedPet && mob.Player )
				{
					itHurts = (int)( (Utility.RandomMinMax(50,200) * ( 100 - mob.PhysicalResistance ) ) / 100 );
					Spells.SpellHelper.Damage( TimeSpan.FromTicks( 1 ), mob, mob, itHurts );
				}
			}

			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( OnSpikeExtended ) );

			from.LocalOverheadMessage( MessageType.Regular, 0x22, 500852 ); // You stepped onto a spike trap!

			LoggingFunctions.LogTraps( from, "a spike trap" );
		}

		public virtual void OnSpikeExtended()
		{
			Extended = true;
			Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerCallback( OnSpikeRetracted ) );
		}

		public virtual void OnSpikeRetracted()
		{
			Extended = false;
			Effects.SendLocationEffect( Location, Map, GetExtendedID( this.Type ) - 1, 6, 3, GetEffectHue(), 0 );
		}

		public SpikeTrap( Serial serial ) : base( serial )
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

			Extended = false;
		}
	}
}