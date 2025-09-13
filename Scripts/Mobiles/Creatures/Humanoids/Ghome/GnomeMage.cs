using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a gnome corpse" )]
	public class GnomeMage : BaseCreature
	{
		[Constructable]
		public GnomeMage () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gnome magician";
			Body = 362;
			BaseSoundID = 422;

			SetStr( 116, 150 );
			SetDex( 91, 115 );
			SetInt( 161, 185 );

			SetHits( 70, 90 );

			SetDamage( 4, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 60.1, 72.5 );
			SetSkill( SkillName.Magery, 60.1, 72.5 );
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 65.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 30;

			PackReg( 6 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.IntelligentAction.BeforeMyDeath( this );
			return base.OnBeforeDeath();
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 1; } }

		public GnomeMage( Serial serial ) : base( serial )
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
