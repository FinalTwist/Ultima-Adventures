/* 	Based on Rikktor, still to get detailed information on the Abyssal Infernal */
using System;
using Server;
using Server.Items;
using Server.Targeting;
using System.Collections;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName( "an abyssal infernal corpse" )]
    public class AbyssalInfernal : BaseChampion
    {
	public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Power; } }

	public override Type[] UniqueList{ get{ return new Type[] {}; } }  // FIXME: Doesn't work, placeholder

	public override Type[] SharedList{ get{ return new Type[] {}; } } // FIXME: Doesn't work, placeholder

	public override Type[] DecorativeList{ get{ return new Type[] {}; } } // FIXME: Doesn't work, placeholder

	public override MonsterStatuetteType[] StatueTypes { get { return new MonsterStatuetteType[] { }; } }

		[Constructable]
		public AbyssalInfernal() : base( AIType.AI_Mage, FightMode.Weakest )
		{
			Body = 172;
			Name = "The Abyssal Infernal";

			SetStr( 1200, 1500 );
			SetDex( 201, 350 );
			SetInt( 550, 650 );

			SetHits( 5500 );
			SetStam( 203, 650 );

			SetDamage( 45, 75 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 80, 90 );

			SetSkill( SkillName.EvalInt, 100.1, 120.0 );
			SetSkill( SkillName.Magery, 110.1, 120.0 );
			SetSkill( SkillName.MagicResist, 80.2, 110.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 22500;
			Karma = -22500;

			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed

			VirtualArmor = 130;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 4 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override ScaleType ScaleType{ get{ return ScaleType.All; } }
		public override int Scales{ get{ return 20; } }


		public override int GetIdleSound() { return 1495; } 
		public override int GetAngerSound() { return 1492; } 
		public override int GetHurtSound() { return 1494; } 
		public override int GetDeathSound()	{ return 1493; }

		public AbyssalInfernal( Serial serial ) : base( serial )
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
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
		}
	}
}
