using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "a demonic corpse" )]
	public class IceFiendLord : BaseCreature
	{

		[Constructable]
		public IceFiendLord() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Ice Fiend Lord";
			Title = "the ultimate fiend";
			Body = 43;
			BaseSoundID = 357;
			Hue = 0x83EC;

			SetStr( 376, 405 );
			SetDex( 176, 195 );
			SetInt( 201, 225 );

			SetHits( 226, 243 );

			SetDamage( 8, 19 );

			SetSkill( SkillName.EvalInt, 80.1, 90.0 );
			SetSkill( SkillName.Magery, 80.1, 90.0 );
			SetSkill( SkillName.MagicResist, 75.1, 85.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 60;

			switch ( Utility.Random( 20 ) )
			{
				case 0: PackItem( new Cake() ); break;
				case 1: PackItem( new Bandage() ); break;
				case 2: PackItem( new GreaterHealPotion() ); break;
				case 3: PackItem( new FancyDress() ); break;
				case 4: PackItem( new Shirt() ); break;
				case 5: PackItem( new BattleAxe() ); break;
				case 6: PackItem( new Shoes() ); break;
				case 7: PackItem( new FiendishBow() ); break;
			}

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 2 );

		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

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

				m.BodyMod = 81;
				m.HueMod = 0;

				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromMinutes( 2.0 ) )
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

		public void SpawnIceImps( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int iceimps = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is IceImp || m is IceImp || m is IceImp )
					++iceimps;
			}

			if ( iceimps < 16 )
			{
				int newIceImps = Utility.RandomMinMax( 3, 6 );

				for ( int i = 0; i < newIceImps; ++i )
				{
					BaseCreature iceimp;

					switch ( Utility.Random( 5 ) )
					{
						default:
						case 0: case 1:	iceimp = new IceImp(); break;
						case 2: case 3:	iceimp = new IceImp(); break;
						case 4:			iceimp = new IceImp(); break;
					}

					iceimp.Team = this.Team;

					bool validLocation = false;
					Point3D loc = this.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					iceimp.MoveToWorld( loc, map );
					iceimp.Combatant = target;
				}
			}
		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( 0.6 >= Utility.RandomDouble() ) // 60% chance to polymorph attacker into a bullfrog
				Polymorph( target );

			if ( 0.2 >= Utility.RandomDouble() ) // 20% chance to more iceimps
				SpawnIceImps( target );

			if ( Hits < 500 && !IsBodyMod ) // IceFiendLord is low on life, polymorph into an iceimp
				Polymorph( this );
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

		public IceFiendLord( Serial serial ) : base( serial )
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