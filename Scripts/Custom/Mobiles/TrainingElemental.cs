/*
Training Elemental script 2.0 by Murzin @ RunUO, 2.0 RC 1
Instructions: drop into your scripts/custom/monster folder and
	you can either add it in-game or on a spawner but since
	he wont die, it doesnt need to be on a spawner.  modify
	the hits as much as you want but if you change other things
	it may not work as designed.  Its important to keep the hits
	higher than the damage he will take from any 1 attack.
*/
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a training elemental corpse" )]
	public class TrainingElemental : BaseCreature
	{
		[Constructable]
		public TrainingElemental() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0, 0 )
		{
			Name = "a Training Elemental";
			Body = 14;
			BaseSoundID = 268;
			Hue = 0x21;
			CantWalk = true;
			SetStr( 50, 50 );
			SetDex( 100, 100 );
			SetInt( 71, 92 );
			SetHits( 50000 );
			SetDamage( 0, 0 );
			SetDamageType( ResistanceType.Physical, 1 );
			SetDamageType( ResistanceType.Fire, 0 );
			SetDamageType( ResistanceType.Cold, 0 );
			SetDamageType( ResistanceType.Poison, 0 );
			SetDamageType( ResistanceType.Energy, 0 );
			SetResistance( ResistanceType.Physical, 25 );
			SetResistance( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Cold, 25 );
			SetResistance( ResistanceType.Poison, 25 );
			SetResistance( ResistanceType.Energy, 25 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 100.0 );
			Fame = 0;
			Karma = 0;
			VirtualArmor = 350;
			RangePerception = 2;
			RangeFight = 2;
		}

		public override void GenerateLoot() { }

		public override bool AutoDispel{ get{ return true; } }

		public override bool BardImmune{ get{ return true; } }

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		/*public override void OnThink( )
		{
			if ( Hits != HitsMax )
				Hits = HitsMax;

			base.OnThink();
		}*/

		public TrainingElemental( Serial serial ) : base( serial )
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