using System;
using Server;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a squirrel corpse" )]	
	public class EvilSquirrel : BaseCreature
	{
		private DateTime m_NextFire;
		[Constructable]
		public EvilSquirrel() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a squirrel";
			Body = 0x116;

            Fame = 5000;
            Karma = 5000;

            SetStr( 44, 50 );
			SetDex( 35 );
			SetInt( 5 );

			SetHits( 100, 120 );

			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 34 );
			SetResistance( ResistanceType.Fire, 10, 14 );
			SetResistance( ResistanceType.Cold, 30, 35 );
			SetResistance( ResistanceType.Poison, 20, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics,120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
		}

		public override void OnThink()
		{
			if ( this.Combatant != null && this.m_NextFire < DateTime.UtcNow )
			{
				this.MovingParticles( this.Combatant, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160 );
				AOS.Damage( this.Combatant, this, Utility.Random( 20, 30 ), 0, 100, 0, 0, 0 );
				this.m_NextFire = DateTime.UtcNow + TimeSpan.FromSeconds( 30.0 );
			}
		}

		public EvilSquirrel( Serial serial ) : base( serial )
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