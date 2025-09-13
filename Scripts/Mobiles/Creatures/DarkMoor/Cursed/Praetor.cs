using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "an evil corpse" )]
	public class Praetor : BaseCursed
	{
		//public overwride bool morph{ get{ return false; } }

		[Constructable]
		public Praetor()
			: base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Praetor";
			Title = "";
			Body = 970;
			Hue = 1974;

			//BaseSoundID = 589;

			SetStr( 250, 350 );
			SetDex( 82, 90 );
			SetInt( 250, 350 );

			SetHits( 300 );

			SetDamage( 17, 23 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 55, 85 );
			SetResistance( ResistanceType.Poison, 70, 70 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.EvalInt, 100.5 );
			SetSkill( SkillName.Magery, 95.5 );
			SetSkill( SkillName.Meditation, 90 );
			SetSkill( SkillName.Anatomy, 100.5 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 119.9 );
			SetSkill( SkillName.Wrestling, 95.9 );
			SetSkill( SkillName.Necromancy, 95.9 );
			SetSkill( SkillName.SpiritSpeak, 119.9 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 54;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 10;
			MinAuraDamage = 5;
			MaxAuraDamage = 15;
			AuraRange = 2;
			//AuraPoison = Poison.Greater;
			AuraMessage = "You feel a cold grip you.";
			AuraType = ResistanceType.Cold;

		}
		
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor, 8 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

		}
		
		public override bool OnBeforeDeath()
		{

			return base.OnBeforeDeath();
		}


		public Praetor( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}
}
