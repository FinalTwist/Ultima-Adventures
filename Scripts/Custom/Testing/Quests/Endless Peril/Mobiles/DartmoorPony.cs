using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a pony corpse" )]
	public class DartmoorPony  : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		[Constructable]
		public DartmoorPony () : this( false, 1.5 )
		{
		}

		[Constructable]
		public DartmoorPony ( bool summoned, double scalar ) : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a Dartmoor Pony";
			Body = 200;
			Hue = 2208;

			
			SetStr( (int)(251*scalar), (int)(350*scalar) );
			SetDex( (int)(76*scalar), (int)(100*scalar) );
			SetInt( (int)(101*scalar), (int)(150*scalar) );

			SetHits( (int)(151*scalar), (int)(210*scalar) );

			SetDamage( (int)(15*scalar), (int)(30*scalar) );

			SetDamageType( ResistanceType.Physical, 100 );

			
			SetResistance( ResistanceType.Cold, (int)(20*scalar), (int)(40*scalar) );
			SetResistance( ResistanceType.Poison, (int)(20*scalar), (int)(35*scalar) );
			SetResistance( ResistanceType.Energy, (int)(40*scalar), (int)(50*scalar) );

			SetSkill( SkillName.MagicResist, (190.1*scalar), (210.0*scalar) );
			SetSkill( SkillName.Tactics, (100.1*scalar), (100.0*scalar) );
			SetSkill( SkillName.Wrestling, (100.1*scalar), (150.0*scalar) );
			SetSkill( SkillName.Magery, (100.1*scalar), (150.0*scalar) );
			SetSkill( SkillName.EvalInt, (100.1*scalar), (150.0*scalar) );
			SetSkill( SkillName.Meditation, (100.1*scalar), (150.0*scalar) );

			PackGold( 100, 250 ); 
				//if ( 0.2 > Utility.RandomDouble() ) 
		                PackItem( new GraniteStone(1) );

		}	

		public override int GetAngerSound()
		{
			return 541;
		}

		public override int GetIdleSound()
		{
			if ( !Controlled )
				return 541;

			return base.GetIdleSound();
		}

		public override int GetDeathSound()
		{
			if ( !Controlled )
				return 542;

			return base.GetDeathSound();
		}

		
		public override int GetHurtSound()
		{
			if ( Controlled )
				return 320;

			return base.GetHurtSound();
		}

		
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 > Utility.RandomDouble() )
				defender.Combatant = null;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( master != null && master.Player && master.Map == this.Map && master.InRange( Location, 20 ) )
				{
					if ( master.Mana >= amount )
					{
						master.Mana -= amount;
					}
					else
					{
						amount -= master.Mana;
						master.Mana = 0;
						master.Damage( amount );
					}
				}
			}

			base.OnDamage( amount, from, willKill );
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public DartmoorPony ( Serial serial ) : base( serial )
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
