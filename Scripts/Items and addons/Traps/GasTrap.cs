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
	public enum GasTrapType
	{
		NorthWall,
		WestWall,
		Floor
	}

	public class GasTrap : BaseTrap
	{
		private Poison m_Poison;

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get{ return m_Poison; }
			set{ m_Poison = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public GasTrapType Type
		{
			get
			{
				switch ( ItemID )
				{
					case 0x113C: return GasTrapType.NorthWall;
					case 0x1147: return GasTrapType.WestWall;
					case 0x11A8: return GasTrapType.Floor;
				}

				return GasTrapType.WestWall;
			}
			set
			{
				ItemID = GetBaseID( value );
			}
		}

		public static int GetBaseID( GasTrapType type )
		{
			switch ( type )
			{
				case GasTrapType.NorthWall: return 0x113C;
				case GasTrapType.WestWall: return 0x1147;
				case GasTrapType.Floor: return 0x11A8;
			}

			return 0;
		}

		[Constructable]
		public GasTrap() : this( GasTrapType.Floor )
		{
		}

		[Constructable]
		public GasTrap( GasTrapType type ) : this( type, Poison.Lesser )
		{
		}

		[Constructable]
		public GasTrap(  Poison poison ) : this( GasTrapType.Floor, Poison.Lesser )
		{
		}

		[Constructable]
		public GasTrap( GasTrapType type, Poison poison ) : base( GetBaseID( type ) )
		{
			m_Poison = poison;
		}

		public override bool PassivelyTriggered{ get{ return false; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.Zero; } }
		public override int PassiveTriggerRange{ get{ return 0; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.0 ); } }

		public override void OnTrigger( Mobile from )
		{
			if ( m_Poison == null || !from.Player || !from.Alive || from.AccessLevel > AccessLevel.Player )
				return;

			if ( Server.Misc.SeeIfGemInBag.GemInPocket( from ) == true || Server.Misc.SeeIfJewelInBag.JewelInPocket( from ) == true )
				return;

			if ( HiddenTrap.CheckTrapAvoidance( from, this ) == 0 )
				return;

			if ( !from.Player )
				return;

			Effects.SendLocationEffect( Location, Map, GetBaseID( this.Type ) - 2, 16, 3, GetEffectHue(), 0 );
			Effects.PlaySound( Location, Map, 0x231 );

			int itHurts = from.PoisonResistance;
			int itSicks = 0;

			if ( itHurts >= 70 ){ itSicks = 1; }
			else if ( itHurts >= 50 ){ itSicks = 2; }
			else if ( itHurts >= 30 ){ itSicks = 3; }
			else if ( itHurts >= 10 ){ itSicks = 4; }
			else { itSicks = 5; }

			switch( Utility.RandomMinMax( 1, itSicks ) )
			{
				case 1: m_Poison = Poison.Lesser;	break;
				case 2: m_Poison = Poison.Regular;	break;
				case 3: m_Poison = Poison.Greater;	break;
				case 4: m_Poison = Poison.Deadly;	break;
				case 5: m_Poison = Poison.Lethal;	break;
			}

			from.ApplyPoison( from, m_Poison );

			from.LocalOverheadMessage( MessageType.Regular, 0x22, 500855 ); // You are enveloped by a noxious gas cloud!

			LoggingFunctions.LogTraps( from, "a poison gas trap" );
		}

		public GasTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			Poison.Serialize( m_Poison, writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Poison = Poison.Deserialize( reader );
					break;
				}
			}
		}
	}
}