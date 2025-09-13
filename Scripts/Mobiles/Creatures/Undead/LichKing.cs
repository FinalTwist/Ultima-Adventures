using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a liche's corpse" )]
	public class LichKing : BaseCreature
	{
		[Constructable]
		public LichKing() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "ancient lich" );
			Title = "the king of the dead";
			Body = 768;
			BaseSoundID = 609;

			SetStr( 316, 405 );
			SetDex( 196, 215 );
			SetInt( 1066, 1145 );

			SetHits( 1500, 2000 );

			SetDamage( 45, 75 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 35, 40 );

			SetSkill( SkillName.EvalInt, 120.1, 130.0 );
			SetSkill( SkillName.Magery, 120.1, 130.0 );
			SetSkill( SkillName.Meditation, 100.1, 101.0 );
			SetSkill( SkillName.Poisoning, 100.1, 101.0 );
			SetSkill( SkillName.MagicResist, 175.2, 200.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 75.1, 100.0 );
			SetSkill( SkillName.Necromancy, 100.1, 120.0 );
			SetSkill( SkillName.SpiritSpeak, 100.1, 120.0 );

			Fame = 28000;
			Karma = -28000;

			VirtualArmor = 60;
			PackNecroReg( 30, 275 );

			int[] list = new int[]
				{
					0x1B11, 0x1B12, 0x1B13, 0x1B14, 0x1B15, 0x1B16, 0x1B19, 0x1B1A, // bone parts
					0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
					0x1B17, 0x1B18, 0x1B1B, 0x1B1C, // ribs and spines
					0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
					0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 // bones
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			GhostlyDust ingut = new GhostlyDust();
   			ingut.Amount = Utility.RandomMinMax( 1, 3 );
   			c.DropItem(ingut);

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 5 ) == 1 && !Server.Items.CharacterDatabase.GetSpecialsKilled( killer, "LichKing" ) )
					{
						Server.Items.CharacterDatabase.SetSpecialsKilled( killer, "LichKing", true );
						ManualOfItems book = new ManualOfItems();
							book.Hue = 0x845;
							book.Name = "Tome of Lich King Relics";
							book.m_Charges = 1;
							book.m_Skill_1 = 99;
							book.m_Skill_2 = 0;
							book.m_Skill_3 = 0;
							book.m_Skill_4 = 0;
							book.m_Skill_5 = 0;
							book.m_Value_1 = 20.0;
							book.m_Value_2 = 0.0;
							book.m_Value_3 = 0.0;
							book.m_Value_4 = 0.0;
							book.m_Value_5 = 0.0;
							book.m_Slayer_1 = 6;
							book.m_Slayer_2 = 0;
							book.m_Owner = null;
							book.m_Extra = "of the Lich King";
							book.m_FromWho = "Taken from the King of the Dead";
							book.m_HowGiven = "Acquired by";
							book.m_Points = 150;
							book.m_Hue = 0x845;
							c.DropItem( book );
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
					{
						LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
						Server.Misc.ContainerFunctions.MakeTomb( MyChest, this, 1 );
						c.DropItem( MyChest );
					}

					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 50 ) == 1 )
					{
						DemonPrison shard = new DemonPrison();
						c.DropItem( shard );
					}
				}
			}
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.BeforeMyBirth( this );
			base.OnAfterSpawn();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, defender );
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.IntelligentAction.BeforeMyDeath( this );
			Server.Misc.IntelligentAction.DropItem( this, this.LastKiller );
			return base.OnBeforeDeath();
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }

        public override int GetAngerSound()
        {
            return 0x61E;
        }

        public override int GetDeathSound()
        {
            return 0x61F;
        }

        public override int GetHurtSound()
        {
            return 0x620;
        }

        public override int GetIdleSound()
        {
            return 0x621;
        }

		public LichKing( Serial serial ) : base( serial )
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