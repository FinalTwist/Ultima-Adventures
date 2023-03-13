using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;

namespace Server.Mobiles
{
	[CorpseName( "a charred corpse" )]
	public class DeadlyFoe3 : BaseCreature
	{
		[Constructable]
		public DeadlyFoe3() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a deadly fireling";
			Body = 755;
			BaseSoundID = 372;
			Hue = 1260;

			SetStr( 1651, 1800 );
			SetDex( 126, 145 );
			SetInt( 3426, 4650 );

			SetHits( 6011, 12140 );

			SetDamage( 19, 38 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.Anatomy, 75.1, 85.0 );
			SetSkill( SkillName.EvalInt, 90.1, 105.0 );
			SetSkill( SkillName.Magery, 90.1, 115.0 );
			SetSkill( SkillName.Meditation, 100.1, 205.0 );
			SetSkill( SkillName.MagicResist, 100.1, 205.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 6500;
			Karma = -6500;

			VirtualArmor = 52;

			PackItem( new Gold( 1000 ) );
			PackItem( new Gold( 1000 ) );
			PackItem( new BearMask() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich );
			AddLoot( LootPack.Gems );
			
		}

		
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }

		public void Polymorph( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( PolymorphSpell ) ) || !m.CanBeginAction( typeof( IncognitoSpell ) ) || m.IsBodyMod )
				return;

			IMount mount = m.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( m.Mounted )
				return;

			if ( m.BeginAction( typeof( PolymorphSpell ) ) )
			{
				Item disarm = m.FindItemOnLayer( Layer.OneHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				disarm = m.FindItemOnLayer( Layer.TwoHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				m.BodyMod = 207;
				m.HueMod = 0;
				
				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromMinutes( 3.0 ) )
			{
				m_Owner = owner;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CanBeginAction( typeof( PolymorphSpell ) ) )
				{
					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.EndAction( typeof( PolymorphSpell ) );
				}
			}
		}
			
		public void DoSpecialAbility( Mobile target )
		{
			if ( 0.6 >= Utility.RandomDouble() ) 
				Polymorph( target );

			if ( Hits < 100 )
				Say("Noooo I can not be defeated!");
					
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
		}

		public override void OnDamagedBySpell( Mobile caster )
		{
			if ( caster == this )
				return;

			SpawnImp( caster );
			Say("Come forth my friend and show them the true use of magic!");
		}

		public void SpawnImp( Mobile target )
		{
			Map map = target.Map;

			if ( map == null )
				return;

			int imps = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is Imp )
					++imps;
			}

			if ( imps < 5 )
			{
				BaseCreature imp = new Imp();

				imp.Team = this.Team;

				Point3D loc = target.Location;
				bool validLocation = false;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = target.X + Utility.Random( 3 ) - 1;
					int y = target.Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				imp.MoveToWorld( loc, map );

				imp.Combatant = target;
			}
		}

		public DeadlyFoe3( Serial serial ) : base( serial )
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
}