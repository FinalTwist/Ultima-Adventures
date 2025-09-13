/*
Training Elemental script by Murzin @ RunUO with contribution by jjarmis
Instructions: drop into your scripts/custom/monster folder and
	you can either add it in-game or on a spawner but since
	he wont die, it doesnt need to be on a spawner.  modify
	the hits as much as you want but if you change other things
	it may not work as designed.
*/
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a training elemental1 corpse" )]
	public class TrainingElemental1 : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public TrainingElemental1() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0, 0 )
		{
			Name = "a Training Elemental1";
			Body = 14;
			BaseSoundID = 268;
			Hue = 0x21;
			CantWalk = true;

			SetStr( 50, 50 );
			SetDex( 350, 350 );
			SetInt( 71, 92 );

			SetHits( 30000, 30000 );

			SetDamage( 0, 0 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Poison, 0 );
			SetDamageType( ResistanceType.Energy, 0 );

			SetResistance( ResistanceType.Physical, 150 );
			SetResistance( ResistanceType.Fire, 150 );
			SetResistance( ResistanceType.Cold, 150 );
			SetResistance( ResistanceType.Poison, 150 );
			SetResistance( ResistanceType.Energy, 150 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 0;
			Karma = 0;

			VirtualArmor = 350;
			ControlSlots = 2;

		}

		public override void GenerateLoot()
		{
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune{ get{ return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void OnThink()
		{
			if ( Hits != HitsMax )
			{
				Hits = HitsMax;
			}

			if (this.Combatant != null && (this.Combatant is TrainingElemental || this.Combatant is TrainingElemental1) )
			{
				this.Combatant = null;
			}

		}

		public TrainingElemental1( Serial serial ) : base( serial )
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