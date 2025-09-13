using System;
using Server;
using Server.Items;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a strange mist" )]
	public class GasCloud : BaseCreature
	{
		public override bool DeleteCorpseOnDeath { get { return true; } }
		public override double DispelDifficulty{ get{ return 100.0; } }
		public override double DispelFocus{ get{ return 40.0; } }

		public override double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			return ( m.Str + ( m.Skills[SkillName.Magery].Value + m.Skills[SkillName.Necromancy].Value ) ) / Math.Max( GetDistanceToSqrt( m ), 1.0 );
		}

		[Constructable]
		public GasCloud() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 200 );
			SetDex( 200 );
			SetInt( 100 );

			SetHits( 200 );
			SetStam( 250 );
			SetMana( 0 );

			SetDamage( 18, 22 );

			Name = "a poison cloud";
			Body = 273;
			Hue = 0xB45;
			BaseSoundID = 655;

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 99.9 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
			ControlSlots = 4;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			ArrayList targets = new ArrayList();

			Map map = this.Map;

			if ( Body != 273 ){ Body = 273; }

			if ( Utility.RandomMinMax( 1, 5 ) == 1 ){ Body = 13; this.Animate( 12, 5, 1, true, false, 0 ); }

			if ( Utility.RandomBool() )
			{
				foreach ( Mobile m in this.GetMobilesInRange( 7 ) )
				{
					bool addTarget = false;
					if ( this != m && this.SummonMaster != m && Utility.RandomBool() )
					{
						if ( m is PlayerMobile && m.Alive && !(m.Blessed) )
						{
							addTarget = true;
						}
						else if ( m is BaseCreature )
						{
							if ( ((BaseCreature)m).GetMaster() != this.SummonMaster )
							{
								addTarget = true;
							}
						}

						if ( addTarget )
						{
							targets.Add( m );
						}
					}
				}

				int damage = Utility.Random( 12, 25 );

				if ( targets.Count > 0 )
				{
					if ( targets.Count > 3 ){ damage = (int)((damage * 4) / targets.Count); }
					else if ( targets.Count > 1 ){ damage = (int)((damage * 2) / targets.Count); }

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = (Mobile)targets[i];

						Region house = m.Region;

						if( !(house is Regions.HouseRegion) )
						{
							this.DoHarmful( m );
							m.ApplyPoison( this, Poison.GetPoison( 2 ) );

							Effects.SendLocationEffect( m.Location, m.Map, 0x3400, 60 );
							Effects.PlaySound( m.Location, m.Map, 0x108 );
						}
					}
				}
			}
		}

		public override void OnThink()
		{
			if ( Core.SE && Summoned )
			{
				ArrayList spirtsOrVortexes = new ArrayList();

				foreach ( Mobile m in GetMobilesInRange( 5 ) )
				{
					if ( m is SummonSnakes || m is BladeSpirits || m is GasCloud || m is DeathVortex || m is SummonedTreefellow || m is EnergyVortex || m is SummonDragon )
					{
						if ( ( (BaseCreature) m ).Summoned )
							spirtsOrVortexes.Add( m );
					}
				}

				while ( spirtsOrVortexes.Count > 6 )
				{
					int index = Utility.Random( spirtsOrVortexes.Count );
					//TODO: Confim if it's the dispel with all the pretty effects or just a Deletion of it.
					Dispel( ( (Mobile) spirtsOrVortexes[index] ) );
					spirtsOrVortexes.RemoveAt( index );
				}
			}

			base.OnThink();
		}


		public GasCloud( Serial serial ): base( serial )
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