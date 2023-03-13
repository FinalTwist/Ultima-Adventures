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
	public class FireColumnTrap : BaseTrap
	{
		[Constructable]
		public FireColumnTrap() : base( 0x1B71 )
		{
			m_MinDamage = 50;
			m_MaxDamage = 200;

			m_WarningFlame = false; // WIZARD
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 2.0 ); } }
		public override int PassiveTriggerRange{ get{ return 3; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0.5 ); } }

		private int m_MinDamage;

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual int MinDamage
		{
			get { return m_MinDamage; }
			set { m_MinDamage = value; }
		}

		private int m_MaxDamage;

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual int MaxDamage
		{
			get { return m_MaxDamage; }
			set { m_MaxDamage = value; }
		}

		private bool m_WarningFlame;

		[CommandProperty( AccessLevel.GameMaster )]
		public virtual bool WarningFlame
		{
			get { return m_WarningFlame; }
			set { m_WarningFlame = value; }
		}

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

			if ( WarningFlame )
				DoEffect();

			if ( from.Alive && from.Player && CheckRange( from.Location, 0 ) )
			{
				int itHurts = (int)( (Utility.RandomMinMax(50,200) * ( 100 - from.FireResistance ) ) / 100 );
				Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, itHurts, 0, 100, 0, 0, 0 );

				if ( !WarningFlame )
					DoEffect();

				LoggingFunctions.LogTraps( from, "a fire column trap" );
			}
		}

		private void DoEffect()
		{
			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
			Effects.PlaySound( Location, Map, 0x225 );
		}

		public FireColumnTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_WarningFlame );
			writer.Write( m_MinDamage );
			writer.Write( m_MaxDamage );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_WarningFlame = reader.ReadBool();
					m_MinDamage = reader.ReadInt();
					m_MaxDamage = reader.ReadInt();
					break;
				}
			}

			if ( version == 0 )
			{
				m_WarningFlame = true;
				m_MinDamage = 10;
				m_MaxDamage = 40;
			}
		}
	}
}