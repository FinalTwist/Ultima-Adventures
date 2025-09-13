// Wolf Family
// a RunUO ver 2.0 Script
// Written by David 
// last edited 6/17/06

using System;
using Server.Mobiles;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a gorilla corpse" )]
	public class MotherGorilla : BaseCreature
	{
		private ArrayList m_pups;
		int pupCount = Utility.RandomMinMax( 2, 5  );

		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanRegenHits{ get{ return true; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RespawnPups
		{
			get{ return false; }
			set{ if( value ) SpawnBabies(); }
		}

		[Constructable]
        public MotherGorilla() : base(AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.1, 0.3)
		{
			Name = "a silverback";
			Body = 0x1D;
			Hue = 1150;
			BaseSoundID = 0x9E;
			

			SetStr( 153, 195 );
			SetDex( 136, 155 );
			SetInt( 136, 160 );

			SetHits( 138, 151 );
			SetMana( 0 );

			SetDamage( 12, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Cold, 46, 56 );

			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 58.3, 68.0 );
			SetSkill( SkillName.Wrestling, 68.3, 78.0 );

			Fame = 900;
			Karma = 0;

			VirtualArmor = 20;

			Tamable = false;

			m_pups = new ArrayList();
			Timer m_timer = new GorillaFamilyTimer( this );
			m_timer.Start();
		}

		public override bool OnBeforeDeath()
		{	
			foreach( Mobile m in m_pups )
			{	
				if( m is GorilaPup && m.Alive && ((GorilaPup)m).ControlMaster != null )
					m.Kill();
			}
			
			return base.OnBeforeDeath();
		}
		
		public void SpawnBabies()
		{

			Defrag();
			int family = m_pups.Count;

			if( family >= pupCount )
				return;

			//Say( "family {0}, should be {1}", family, pupCount );

			Map map = this.Map;

			if ( map == null )
				return;

			int hr = (int)((this.RangeHome + 1) / 2);

			for ( int i = family; i < pupCount; ++i )
			{
				GorilaPup pup = new GorilaPup();

				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 5 ) - 1;
					int y = Y + Utility.Random( 5 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				pup.Mother = this;
				pup.Team = this.Team;
				pup.Home = this.Location;
				pup.RangeHome = ( hr > 4 ? 4 : hr );
				
				pup.MoveToWorld( loc, map );
				m_pups.Add( pup );
			}
		}

		protected override void OnLocationChange( Point3D oldLocation )
		{

			try
			{
				foreach( Mobile m in m_pups )
				{	
					if( m is GorilaPup && m.Alive && ((GorilaPup)m).ControlMaster == null )
					{
						((GorilaPup)m).Home = this.Location;
					}
				}
			}
			catch{}

			base.OnLocationChange( oldLocation );
		}
		
		public void Defrag()
		{
			for ( int i = 0; i < m_pups.Count; ++i )
			{
				try
				{
					object o = m_pups[i];

					GorilaPup pup = o as GorilaPup;

					if ( pup == null || !pup.Alive )
					{
						m_pups.RemoveAt( i );
						--i;
					}

					else if ( pup.Controlled || pup.IsStabled )
					{
						pup.Mother = null;
						m_pups.RemoveAt( i );
						--i;
					}
				}
				catch{}
			}
		}

		public override void OnDelete()
		{
			Defrag();

			foreach( Mobile m in m_pups )
			{	
				if( m.Alive && ((GorilaPup)m).ControlMaster == null )
					m.Delete();
			}

			base.OnDelete();
		}

		public MotherGorilla(Serial serial) : base(serial)
		{
			m_pups = new ArrayList();
			Timer m_timer = new GorillaFamilyTimer( this );
			m_timer.Start();
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.WriteMobileList( m_pups, true );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_pups = reader.ReadMobileList();
		}
	}

	[CorpseName( "a gorilla corpse" )]
	public class GorilaPup : BaseCreature
	{
		public override int Meat{ get{ return 5; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }	

		private MotherGorilla m_mommy;

		[CommandProperty( AccessLevel.GameMaster )]
		public MotherGorilla Mother
		{
			get{ return m_mommy; }
			set{ m_mommy = value; }
		}

		[Constructable]
        public GorilaPup() : base(AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			Name = "a gorilla";
			Body = 0x1D;
			BaseSoundID = 0x9E;

			SetStr( 53, 95 );
			SetDex( 36, 55 );
			SetInt( 36, 60 );

			SetHits( 38, 51 );
			SetMana( 0 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 15 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 43.3, 58.0 );
			SetSkill( SkillName.Wrestling, 43.3, 58.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 20;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -18.9;
		}

		public override void OnCombatantChange()
		{
			if( Combatant != null && Combatant.Alive && m_mommy != null && m_mommy.Combatant == null )
				m_mommy.Combatant = Combatant;
		}

		public GorilaPup(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( m_mommy );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_mommy = (MotherGorilla)reader.ReadMobile();
		}
	}

	public class GorillaFamilyTimer : Timer
	{
		private MotherGorilla m_from;

		public GorillaFamilyTimer( MotherGorilla from  ) : base( TimeSpan.FromMinutes( 1 ), TimeSpan.FromMinutes( 20 ) )
		{
			Priority = TimerPriority.OneMinute; 
			m_from = from;
		}

		protected override void OnTick()
		{
			if ( m_from != null && m_from.Alive )
				m_from.SpawnBabies();
			else
				Stop();
		}
	}
}
