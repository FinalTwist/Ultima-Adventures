using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;

namespace Server.Mobiles
{
	[CorpseName( "a charred corpse" )]
	public class DeadlyFoe1 : BaseCreature
	{ //set cantwalk true blessed true
		[Constructable]
		public DeadlyFoe1() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a baby fireling";
			Body = 130;
			BaseSoundID = 0x174;
                        Hue = 1161;

			SetStr( 351, 400 );
			SetDex( 126, 145 );
			SetInt( 226, 250 );

			SetHits( 2011, 2140 );

			SetDamage( 10, 19 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.Anatomy, 75.1, 85.0 );
			SetSkill( SkillName.EvalInt, 90.1, 105.0 );
			SetSkill( SkillName.Magery, 90.1, 105.0 );
			SetSkill( SkillName.Meditation, 90.1, 105.0 );
			SetSkill( SkillName.MagicResist, 90.1, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 40.1, 80.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 32;
		}
		
		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public void Taunt( Mobile m )
		{
		Say("*Begins to absorb the surrounding enemy's life force*");
		}
			
		public void DoSpecialAbility( Mobile target )
		{

			if ( 0.1 >= Utility.RandomDouble() ) 
				Taunt( this );

			if ( Hits < 100 )
				Say("*Begins to transform*");
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

		public DeadlyFoe1( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			DeadlyFoe2 rm = new DeadlyFoe2();
			rm.Team = this.Team;
			rm.Combatant = this.Combatant;
			rm.NoKillAwards = true;

			if ( rm.Backpack == null )
			{
				Backpack pack = new Backpack();
				pack.Movable = false;
				rm.AddItem( pack );
			}

			Effects.PlaySound(this, Map, GetDeathSound());
			Effects.SendLocationEffect( Location, Map, 0x3709, 30, 10, 0x835, 0 );
			rm.MoveToWorld( Location, Map );

			Delete();
			return false;
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