using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a tritun corpse" )]
	public class TritunMage : BaseCreature
	{
		[Constructable]
		public TritunMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a tritun wizard";
			Body = 678;
			BaseSoundID = 0x553;

			SetStr( 146, 165 );
			SetDex( 71, 130 );
			SetInt( 181, 205 );

			SetDamage( 8, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 77.5, 100.0 );
			SetSkill( SkillName.Fencing, 62.5, 85.0 );
			SetSkill( SkillName.Macing, 62.5, 85.0 );
			SetSkill( SkillName.Magery, 72.5, 95.0 );
			SetSkill( SkillName.Meditation, 77.5, 100.0 );
			SetSkill( SkillName.MagicResist, 77.5, 100.0 );
			SetSkill( SkillName.Swords, 62.5, 85.0 );
			SetSkill( SkillName.Tactics, 62.5, 85.0 );
			SetSkill( SkillName.Wrestling, 62.5, 85.0 );

			Fame = 1200;
			Karma = -1200;
			VirtualArmor = 10;
			
			PackReg( 10, 15 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Potions );
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			return base.OnBeforeDeath();
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Blue ); } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }

		public TritunMage( Serial serial ) : base( serial )
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