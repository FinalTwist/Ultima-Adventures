using System;
using Server;
using Server.Items;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a vortex corpse" )]
	public class EnergyVortex : BaseCreature
	{
		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override double DispelDifficulty { get { return 80.0; } }
		public override double DispelFocus { get { return 20.0; } }

		public override double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			return ( m.Int + m.Skills[SkillName.Magery].Value ) / Math.Max( GetDistanceToSqrt( m ), 1.0 );
		}

		[Constructable]
		public EnergyVortex() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 200 );
			SetDex( 200 );
			SetInt( 100 );

			SetHits( ( Core.SE ) ? 140 : 70 );
			SetStam( 250 );
			SetMana( 0 );

			SetDamage( 14, 17 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetResistance( ResistanceType.Physical, 60, 70 );

			switch ( Utility.Random( 4 ) )
			{
				case 0:
					Name = "an energy vortex";
					Body = 13;
					Hue = 20;
					BaseSoundID = 655;
					SetDamageType( ResistanceType.Energy, 100 );
					SetResistance( ResistanceType.Fire, 40, 50 );
					SetResistance( ResistanceType.Cold, 40, 50 );
					SetResistance( ResistanceType.Poison, 40, 50 );
					SetResistance( ResistanceType.Energy, 90, 100 );
					break;
				case 1:
					Name = "an icy vortex";
					Body = 13;
					Hue = 0x48D;
					BaseSoundID = 655;
					SetDamageType( ResistanceType.Cold, 100 );
					SetResistance( ResistanceType.Fire, 40, 50 );
					SetResistance( ResistanceType.Cold, 90, 100 );
					SetResistance( ResistanceType.Poison, 40, 50 );
					SetResistance( ResistanceType.Energy, 40, 50 );
					break;
				case 2:
					Name = "a scorching vortex";
					Body = 13;
					Hue = 0xB73;
					BaseSoundID = 838;
					SetDamageType( ResistanceType.Fire, 100 );
					SetResistance( ResistanceType.Fire, 90, 100 );
					SetResistance( ResistanceType.Cold, 40, 50 );
					SetResistance( ResistanceType.Poison, 40, 50 );
					SetResistance( ResistanceType.Energy, 40, 50 );
					break;
				case 3:
					Name = "a plague vortex";
					Body = 13;
					Hue = 0xB97;
					BaseSoundID = 655;
					SetDamageType( ResistanceType.Poison, 100 );
					SetResistance( ResistanceType.Fire, 40, 50 );
					SetResistance( ResistanceType.Cold, 40, 50 );
					SetResistance( ResistanceType.Poison, 90, 100 );
					SetResistance( ResistanceType.Energy, 40, 50 );
					break;
			}

			SetSkill( SkillName.MagicResist, 99.9 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
			ControlSlots = ( Core.SE ) ? 2 : 1;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override int GetAngerSound()
		{
			return 0x15;
		}

		public override int GetAttackSound()
		{
			return 0x28;
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


		public EnergyVortex( Serial serial ): base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 0;
		}
	}
}