using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "an toxic slug corpse" )]
	public class ToxicSlug : BaseCreature
	{
		[Constructable]
		public ToxicSlug() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a toxic slug";
			Body = 51;
            Hue = 44;

			SetStr( 250, 290 );
			SetDex( 70, 105 );
			SetInt( 10, 30 );

			SetHits( 300, 350 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 15 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 15 );
			SetResistance( ResistanceType.Poison, 70 );
			SetResistance( ResistanceType.Energy, 15 );

			SetSkill( SkillName.MagicResist, 25.0 );
			SetSkill( SkillName.Tactics, 50.0, 60.0 );
			SetSkill( SkillName.Wrestling, 50.0, 80.0 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int GetIdleSound() { return 1499; } 
		public override int GetAngerSound() { return 1496; } 
		public override int GetHurtSound() { return 1498; } 
		public override int GetDeathSound()	{ return 1497; }
		
		public override Poison HitPoison{ get{ return Poison.Greater; } }
		public override double HitPoisonChance{ get{ return 0.50; } }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from.Map != null )
			{
				int amt=0;
				Mobile target = this; 
				int rand = Utility.Random( 1, 100 );
				if ( willKill )
				{
					amt = ((( rand % 5 ) >> 2 ) + 3);
				} 
				if ( ( Hits < 100 ) && ( rand < 21 ) ) 
				{
					target = ( rand % 2 ) < 1 ? this : from;
					amt++;
				}
				if ( amt > 0 )
				{
					SpillAcid( target, amt );
					from.SendLocalizedMessage( 1070820 ); 
					amt ^= amt;
				}
			}
			base.OnDamage( amount, from, willKill );
		}

		public override Item NewHarmfulItem()
		{
			return new AcidSlime( TimeSpan.FromSeconds(10), 5, 10 );
		}

		public ToxicSlug( Serial serial ) : base( serial )
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