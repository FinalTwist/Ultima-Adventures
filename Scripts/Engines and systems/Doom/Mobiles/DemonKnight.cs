using System;
using System.Collections;
using Server;
using Server.Items;
using System.Collections.Generic;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a demon knight corpse" )]
	public class DemonKnight : BaseCursed
	{
		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		public static Type[] ArtifactRarity10 { get { return m_ArtifactRarity10; } }
		public static Type[] ArtifactRarity11 { get { return m_ArtifactRarity11; } }
		private static Type[] m_ArtifactRarity10 = new Type[]
			{
				typeof( LegacyOfTheDreadLord ),
				typeof( TheTaskmaster )
			};

		private static Type[] m_ArtifactRarity11 = new Type[]
			{
				Loot.ArtyTypes[Utility.Random(Loot.ArtyTypes.Length)]
			};

		public static Item CreateRandomArtifact()
		{

			Type type;
			type = Loot.ArtyTypes[Utility.Random(Loot.ArtyTypes.Length)];
			return Loot.Construct( type );
			
		}

		public static Mobile FindRandomPlayer( BaseCreature creature )
		{
			List<DamageStore> rights = BaseCreature.GetLootingRights( creature.DamageEntries, creature.HitsMax );

			for ( int i = rights.Count - 1; i >= 0; --i )
			{
				DamageStore ds = rights[i];

				if ( !ds.m_HasRight )
					rights.RemoveAt( i );
			}

			if ( rights.Count > 0 )
				return rights[Utility.Random( rights.Count )].m_Mobile;

			return null;
		}

		public static void DistributeArtifact( BaseCreature creature )
		{
			DistributeArtifact( creature, CreateRandomArtifact() );
		}

		public static void DistributeArtifact( BaseCreature creature, Item artifact )
		{
			DistributeArtifact( FindRandomPlayer( creature ), artifact );
		}

		public static void DistributeArtifact( Mobile to )
		{
			DistributeArtifact( to, CreateRandomArtifact() );
		}

		public static void DistributeArtifact( Mobile to, Item artifact )
		{
			if ( to == null || artifact == null )
				return;

			Container pack = to.Backpack;

			if ( pack == null || !pack.TryDropItem( to, artifact, false ) )
				to.BankBox.DropItem( artifact );

			to.SendLocalizedMessage( 1062317 ); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.
		}

		public static int GetArtifactChance( Mobile boss )
		{
			if ( !Core.AOS )
				return 0;

			int luck = LootPack.GetLuckChanceForKiller( boss );
			int chance;

			if ( boss is DemonKnight )
				chance = 1500 + (luck / 5);
			else
				chance = 750 + (luck / 10);

			return chance;
		}

		public static bool CheckArtifactChance( Mobile boss )
		{
			return GetArtifactChance( boss ) > Utility.Random( 3000 );
		}

		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.DoubleStrike;
				case 1: return WeaponAbility.WhirlwindAttack;
				case 2: return WeaponAbility.CrushingBlow;
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
			{
				DemonKnight.DistributeArtifact( this );
			}
			
			if (Utility.RandomMinMax(1, 80) == 69)
				c.DropItem(new DarkFatherCloak());
					
			if ( Utility.RandomDouble() < 0.15 )
				switch ( Utility.Random( 5 ))		  
				{
					case 0: c.DropItem( new ArmorEnhancementDeed() ); break;
					case 1: c.DropItem( new AosEnhancementDeed() ); break;
					case 2: c.DropItem( new EnhancementDeed() ); break;
					case 3: c.DropItem( new SkillEnhancementDeed() ); break;
					case 4: c.DropItem( new WeaponEnhancementDeed() ); break;
				};
				
			Mobile killer = this.LastKiller;
			
			if (killer is PlayerMobile || (killer is BaseCreature && ((BaseCreature)killer).ControlMaster != null && ((BaseCreature)killer).ControlMaster is PlayerMobile) )
			{
				if ( !(killer is PlayerMobile) )
					killer = ((BaseCreature)killer).ControlMaster;
				
				PlayerMobile pm = (PlayerMobile)killer;
					Item rngitem = null;
							switch ( Utility.Random( 4 ) ) //
										{					
												case 0: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Server.SkillHandlers.Stealing.ItemMutate( pm, pm.Luck, rngitem, 25); break;
												case 1: rngitem = Loot.RandomInstrument(); Server.SkillHandlers.Stealing.ItemMutate( pm, pm.Luck, rngitem, 25 ); break;
												case 2: rngitem = Loot.RandomQuiver(); Server.SkillHandlers.Stealing.ItemMutate( pm, pm.Luck, rngitem, 25 ); break;
												case 3: rngitem = Loot.RandomJewelry(); Server.SkillHandlers.Stealing.ItemMutate( pm, pm.Luck, rngitem, 25 ); break;
										}
							if (rngitem != null)
							{
								c.DropItem( rngitem );
							}						
			}
			
		}

		[Constructable]
		public DemonKnight() : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "demon knight" );
			Title = "the Dark Father";
			Body = 318;
			BaseSoundID = 0x165;

			SetStr( 950 );
			SetDex( 500 );
			SetInt( 1000 );

			SetHits( 8900 );
			SetMana( 9000 );

			SetDamage( 25, 37 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 40 );
			SetResistance( ResistanceType.Fire, 40 );
			SetResistance( ResistanceType.Cold, 40 );
			SetResistance( ResistanceType.Poison, 40 );
			SetResistance( ResistanceType.Energy, 40 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 110.0 );
			SetSkill( SkillName.Magery, 110.0 );
			SetSkill( SkillName.Meditation, 120.0 );
			SetSkill( SkillName.MagicResist, 150.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Poisoning, 80.0 );


			Fame = 28000;
			Karma = -28000;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 15;
			MaxAuraDamage = 45;
			AuraRange = 3;
			//AuraPoison = Poison.Greater;
			AuraMessage = "The pure evil shatters your being.";
			AuraType = ResistanceType.Cold;

			VirtualArmor = 64;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 2 );
			AddLoot( LootPack.HighScrolls, Utility.RandomMinMax( 6, 60 ) );
		}


		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 3; } }

		private static bool m_InHere;

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from != this && !m_InHere )
			{
				m_InHere = true;
				AOS.Damage( from, this, Utility.RandomMinMax( 8, 20 ), 100, 0, 0, 0, 0 );

				MovingEffect( from, 0xECA, 10, 0, false, false, 0, 0 );
				PlaySound( 0x491 );

				if ( 0.05 > Utility.RandomDouble() )
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( CreateBones_Callback ), from );

				m_InHere = false;
			}
			
			base.OnDamage( amount, from, willKill );
		}

		public virtual void CreateBones_Callback( object state )
		{
			Mobile from = (Mobile)state;
			Map map = from.Map;

			if ( map == null )
				return;

			int count = Utility.RandomMinMax( 1, 3 );

			for ( int i = 0; i < count; ++i )
			{
				int x = from.X + Utility.RandomMinMax( -1, 1 );
				int y = from.Y + Utility.RandomMinMax( -1, 1 );
				int z = from.Z;

				if ( !map.CanFit( x, y, z, 16, false, true ) )
				{
					z = map.GetAverageZ( x, y );

					if ( z == from.Z || !map.CanFit( x, y, z, 16, false, true ) )
						continue;
				}

				UnholyBone bone = new UnholyBone();

				bone.Hue = 0;
				bone.Name = "unholy bones";
				bone.ItemID = Utility.Random( 0xECA, 9 );

				bone.MoveToWorld( new Point3D( x, y, z ), map );
			}
		}

		public DemonKnight( Serial serial ) : base( serial )
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
