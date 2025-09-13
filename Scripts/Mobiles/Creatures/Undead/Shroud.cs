using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Shroud : BaseCreature
	{
		[Constructable]
		public Shroud() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = Server.Misc.RandomThings.GetRandomWizardName();
			Title = "the shroud";
			Body = 365;
			BaseSoundID = 0x47D;

			SetStr( 416, 505 );
			SetDex( 146, 165 );
			SetInt( 566, 655 );

			SetHits( 250, 303 );

			SetDamage( 11, 13 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Necromancy, 90, 110.0 );
			SetSkill( SkillName.SpiritSpeak, 90.0, 110.0 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 150.5, 200.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 18000;
			Karma = -18000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls, 1 );
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( Server.Misc.GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomDouble() > 0.80)
					{
						Robe robe = new Robe();
						robe.Name = "evil robe of " + this.Name + " " + this.Title;
						robe.Hue = 0xB5E;
						robe.Attributes.CastRecovery = 1;
						robe.Attributes.CastSpeed = 1;
						robe.Attributes.LowerManaCost = 25;
						robe.Attributes.LowerRegCost = 25;
						robe.Attributes.SpellDamage = 25;
						c.DropItem( robe );
					}
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			return base.OnBeforeDeath();
		}

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public Shroud( Serial serial ) : base( serial )
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