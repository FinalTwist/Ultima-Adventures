using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a blood lich monarch's corpse" )]
	public class BloodLichMonarch : BaseCreature
	{
		private bool m_DamagedByNonHardcore;
		private List<PlayerMobile> m_SoulBoundDamagers;
		public List<PlayerMobile> SoulBoundDamagers {
			get {return m_SoulBoundDamagers;}
			set {m_SoulBoundDamagers = value;}
		}
		[Constructable]
		public BloodLichMonarch() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_SoulBoundDamagers = new List<PlayerMobile>(); 
			m_DamagedByNonHardcore = false;
			Name = NameList.RandomName( "ancient lich" );
			Title = "the blood lich monarch";

			BaseSoundID = 412;
			Body = 24;
			Hue = 1994;

			SetStr( 405, 605 );
			SetDex( 96, 115 );
			SetInt( 1466, 1745 );

			SetHits( 2900, 3555 );

			SetDamage( 50, 99 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 75 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 50, 75 );
			SetResistance( ResistanceType.Fire, 50, 75 );
			SetResistance( ResistanceType.Cold, 50, 75 );
			SetResistance( ResistanceType.Poison, 50, 75 );
			SetResistance( ResistanceType.Energy, 50, 75 );

			SetSkill( SkillName.EvalInt, 120.1, 130.0 );
			SetSkill( SkillName.Magery, 120.1, 130.0 );
			SetSkill( SkillName.Meditation, 120.1, 130.0 );
			SetSkill( SkillName.Poisoning, 100.1, 101.0 );
			SetSkill( SkillName.MagicResist, 205.2, 250.0 );
			SetSkill( SkillName.Tactics, 98.1, 100.0 );
			SetSkill( SkillName.Wrestling, 98.1, 120.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			// Fame = 30000;
			// Karma = -30000;

			VirtualArmor = 80;
			PackNecroReg( 30, 275 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;

			if ( killer is BaseCreature )
				killer = ((BaseCreature)killer).GetMaster();

			if ( killer is PlayerMobile )
			{
				if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
				{
					LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
					Server.Misc.ContainerFunctions.MakeTomb( MyChest, this, 1 );
					c.DropItem( MyChest );
				}

				if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 100 ) == 1 )
				{
					DemonPrison shard = new DemonPrison();
					c.DropItem( shard );
				}
			}
			// if it hasnt been damaged by a non soulbound mobile, drop soulbound items
			object[] list = new object[]
				{
					new MageBlood(1), new BalronBlood(1), new LichBlood(1), new ElderGazerBrain(1), new IceBlood(1), new FireBlood(1), new ElementalCore(1), new BearBone(1), 
					new DimensionalShard(1), new CelestialRune(1), new AncientMoss(1), new EagleEye(1), new HorseHeart(1), new MonkTear(1), new PixieDust(1), new PlanarRune(1), 
					new SageBlood(1), new ScorpionBlood(1), new SnakeOil(1), new TitanFlesh(1), new UnicornShoes(1)
				};
			if (!m_DamagedByNonHardcore) {
				foreach(PlayerMobile player in SoulBoundDamagers) {
					if (player.Backpack != null) {
						RoyalBloodUrn urn = (RoyalBloodUrn)player.Backpack.FindItemByType(typeof(RoyalBloodUrn));
						if (urn != null) {
							urn.Delete();
						} 
						for(int i = 0; i < Utility.RandomMinMax(1,3); i++) {
							player.Backpack.DropItem((Item)list[Utility.Random( list.Length )]);
						}
					}
				}
				if (Utility.RandomMinMax(1,500) == 3) {
					Phylactery phylactery = new Phylactery();
					phylactery.PowerLevel = 1;
					phylactery.AddRandomEssences(Utility.RandomMinMax(1,6));
					c.DropItem((Item)phylactery);
				}
			}
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override int GetIdleSound()
		{
			return 0x19D;
		}

		public override int GetAngerSound()
		{
			return 0x175;
		}

		public override int GetDeathSound()
		{
			return 0x108;
		}

		public override int GetAttackSound()
		{
			return 0xE2;
		}

		public override int GetHurtSound()
		{
			return 0x28B;
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
		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public BloodLichMonarch( Serial serial ) : base( serial )
		{
		}
    
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (bool) m_DamagedByNonHardcore);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_DamagedByNonHardcore = reader.ReadBool();
			m_SoulBoundDamagers = new List<PlayerMobile>();
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if (!(from is PlayerMobile)) {
				m_DamagedByNonHardcore = true;
			} else {
				PlayerMobile player = (PlayerMobile)from;
				if (!player.SoulBound) {
					m_DamagedByNonHardcore = true;
				} else {
					List<PlayerMobile> filtered = SoulBoundDamagers.FindAll(p => p.Serial == from.Serial);
					if (filtered.Count == 0) {
						SoulBoundDamagers.Add((PlayerMobile)from);
					}
				}
			}
			base.OnDamage(amount,from,willKill);
		}		
	}
}