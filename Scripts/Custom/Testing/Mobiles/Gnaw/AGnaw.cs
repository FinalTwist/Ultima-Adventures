using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a gnaw corpse" )] // TODO: Corpse name?
		public class AGnaw : BaseCreature
	{

		[Constructable]
		public AGnaw() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Gnaw";
			Body = 23;
			Hue = 0x130;
			BaseSoundID = 0xE5;

			SetStr( 151, 172 );
			SetDex( 124, 145 );
			SetInt( 60, 86 );

			SetHits( 817, 857 );
			SetStam( 124, 145 );
			SetMana( 60, 86 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 64, 69 );
			SetResistance( ResistanceType.Fire, 53, 56 );
			SetResistance( ResistanceType.Cold, 22, 27 );
			SetResistance( ResistanceType.Poison, 27, 30 );
			SetResistance( ResistanceType.Energy, 21, 34 );

			SetSkill( SkillName.MagicResist, 96.8, 110.8 );
			SetSkill( SkillName.Tactics, 84.7, 103.7 );
			SetSkill( SkillName.Wrestling, 113.7, 116.7 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 35;//dire is 22
			
			
/*			if ( Utility.RandomDouble() < 0.3 )
				PackItem( new GnawsFang() );
*/
		}
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
		}


		public override int Meat { get { return 4; } }
		public override int Hides { get { return 12; } }
		public override HideType HideType { get { return HideType.Spined; } }
		public override Poison PoisonImmune { get { return Poison.Greater; } }
		
		
		public override void OnDamagedBySpell( Mobile caster )
		{
			if ( caster != this && 0.25 > Utility.RandomDouble() )
			{
				BaseCreature wolf = new SpawnedDireWolf( );

				wolf.Team = this.Team;
				wolf.MoveToWorld( this.Location, this.Map );
				wolf.Combatant = caster;

				Say( "* The Gnaw summons another beast! *"  ); 
			}

			base.OnDamagedBySpell( caster );
		}
		public override void OnGotMeleeAttack( Mobile attacker )
		{
			if ( attacker != this && 0.25 > Utility.RandomDouble() )
			{
				BaseCreature wolf = new SpawnedDireWolf( );

				wolf.Team = this.Team;
				wolf.MoveToWorld( this.Location, this.Map );
				wolf.Combatant = attacker;

				Say( "* The Gnaw summons another beast! *" ); 
			}

			base.OnGotMeleeAttack( attacker );
		}


		public AGnaw( Serial serial ) : base( serial )
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
