using System;
using System.Collections;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a swarm of bugs" )]
	public class Swarm : BaseCreature
	{
		public override bool DeleteCorpseOnDeath{ get{ return true; } }
		public override double DispelDifficulty{ get{ return 125.5; } }
		public override double DispelFocus{ get{ return 40.0; } }

		public override double GetFightModeRanking( Mobile m, FightMode acqType, bool bPlayerOnly )
		{
			return ( m.Str + ( m.Skills[SkillName.Magery].Value + m.Skills[SkillName.Necromancy].Value ) ) / Math.Max( GetDistanceToSqrt( m ), 1.0 );
		}

		[Constructable]
		public Swarm(): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.3, 0.6 )
		{
			Name = "a swarm of insects";
			Body = 739;

			SetStr( 150 );
			SetDex( 150 );
			SetInt( 100 );

			SetHits( ( Core.SE ) ? 160 : 80 );
			SetStam( 250 );
			SetMana( 0 );

			SetDamage( 10, 14 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 70.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 40;
			ControlSlots = 2;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune { get { return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }

		public override int GetAngerSound()
		{
			return 0x5CB;
		}

		public override int GetAttackSound()
		{
			return 0x5CC;
		}

		public override int GetHurtSound()
		{
			return 0x5CB;
		}

		public override void OnThink()
		{
			if ( Core.SE && Summoned )
			{
				ArrayList bugs = new ArrayList();

				foreach ( Mobile m in GetMobilesInRange( 5 ) )
				{
					if ( m is Swarm )
					{
						if ( ( (BaseCreature) m ).Summoned )
							bugs.Add( m );
					}
				}

				while ( bugs.Count > 6 )
				{
					int index = Utility.Random( bugs.Count );
					Dispel( ( (Mobile) bugs[index] ) );
					bugs.RemoveAt( index );
				}
			}

			base.OnThink();
		}

		public Swarm( Serial serial ): base( serial )
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