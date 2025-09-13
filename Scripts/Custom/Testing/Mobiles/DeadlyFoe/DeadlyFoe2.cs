using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;

namespace Server.Mobiles
{
	public class DeadlyFoe2 : BaseCreature
	{
		[Constructable]
		public DeadlyFoe2() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a grown fireling";
			Body = 754;
			BaseSoundID = 372;
			
			SetStr( 651, 800 );
			SetDex( 126, 145 );
			SetInt( 426, 650 );

			SetHits( 3011, 4140 );

			SetDamage( 14, 28 );

			SetDamageType( ResistanceType.Physical, 20 );
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

			if ( Hits < 50 )
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
		
		public DeadlyFoe2( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			DeadlyFoe3 rm = new DeadlyFoe3();
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