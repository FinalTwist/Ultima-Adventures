using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using System.Collections.Generic;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "an evil corpse" )]
	public class Honorae : BaseCursed
	{

		[Constructable]
		public Honorae()
			: base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = "An Honorae";
			Title = "";
			Body = 93;
			Hue = 1979;

			//BaseSoundID = 589;

			SetStr( 450, 550 );
			SetDex( 82, 160 );
			SetInt( 450, 550 );

			SetHits( 800 );

			SetDamage( 21, 32 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 55, 85 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.EvalInt, 100.5 );
			SetSkill( SkillName.Magery, 100.5 );
			SetSkill( SkillName.Meditation, 90 );
			SetSkill( SkillName.Anatomy, 100.5 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 119.9 );
			SetSkill( SkillName.Wrestling, 100.9 );
			SetSkill( SkillName.Necromancy, 100.9 );
			SetSkill( SkillName.SpiritSpeak, 119.9 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 64;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 10;
			MaxAuraDamage = 35;
			AuraRange = 3;
			//AuraPoison = Poison.Greater;
			AuraMessage = "You feel a cold grip you.";
			AuraType = ResistanceType.Cold;

		}
		
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 8 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

		}
		
		public override bool OnBeforeDeath()
		{

			return base.OnBeforeDeath();
		}


		public Honorae( Serial serial )
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
