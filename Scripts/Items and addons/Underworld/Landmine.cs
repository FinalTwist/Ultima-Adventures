using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Misc;

namespace Server.Items
{
	public class LandmineSetup : Item
	{
		[Constructable]
		public LandmineSetup( ) : base( 0x3EA2 )
		{
			Weight = 5.0;
			Name = "landmine";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This mine needs to be in your pack to setup." );
				return;
			}
			else
			{
				int mines = 0;

				foreach ( Item m in from.GetItemsInRange( 10 ) )
				{
					if ( m is Landmine )
						++mines;
				}

				if ( mines > 2 )
				{
					from.SendMessage( "There are already too many landmines in the area!" );
				}
				else if ( !from.Region.AllowHarmful( from, from ) )
				{
					from.SendMessage( "That doesn't feel like a good idea." ); 
					return;
				}
				else
				{
					int Power = (int)(from.Skills[SkillName.RemoveTrap].Value / 2) + 24;

					from.PlaySound( 0x42 );

					Landmine mine = new Landmine( from, Power ); 
					mine.Map = from.Map; 
					mine.Location = from.Location;
					this.Delete();
					from.SendMessage( "You place the landmine at your feet." ); 
				}
			}
		}

		public LandmineSetup( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class Landmine : Item
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public int power;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Power { get{ return power; } set{ power = value; } }

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromSeconds( 180.0 ); } } // HOW LONG UNTIL THE TRAP DECAYS IN SECONDS

		[Constructable]
		public Landmine( Mobile source, int level ) : base( 0x3EA2 )
		{
			Movable = false;
			Name = "a landmine";
			owner = source;
			power = level;
			RefreshDecay( true );
		}

		public Landmine(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( owner != m )
			{
				int StrMax = power;
				int StrMin = (int)(power/2);

				if (
				( m is PlayerMobile && m.Blessed == false && m.Alive && m.AccessLevel == AccessLevel.Player && Server.Misc.SeeIfGemInBag.GemInPocket( m ) == false && Server.Misc.SeeIfJewelInBag.JewelInPocket( m ) == false ) 
				|| 
				( m is BaseCreature && m.Blessed == false && !(m is PlayerMobile ) ) 
				)
				{
					int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.PhysicalResistance ) ) / 100 ) + 10;
					m.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					m.PlaySound( 0x307 );
					m.Damage( itHurts, owner );
					this.Delete();
				}
			}

			return true;
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteDeltaTime( m_DecayTime );
			writer.Write( (Mobile)owner );
			writer.Write( (int)power );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
			owner = reader.ReadMobile();
			power = reader.ReadInt();
		}
	}
}