using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dracolich corpse" )]
	public class Dracolich : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 20; } }
		public override int BreathFireDamage{ get{ return 20; } }
		public override int BreathColdDamage{ get{ return 20; } }
		public override int BreathPoisonDamage{ get{ return 20; } }
		public override int BreathEnergyDamage{ get{ return 20; } }
		public override int BreathEffectHue{ get{ return 0x9C1; } }
		public override int BreathEffectSound{ get{ return 0x653; } }
		public override int BreathEffectItemID{ get{ return 0x37BC; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 25 ); }

		[Constructable]
		public Dracolich () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the dracolich";
			Body = 104;
			BaseSoundID = 0x488;

			SetStr( 898, 1030 );
			SetDex( 68, 200 );
			SetInt( 488, 620 );

			SetHits( 558, 599 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Cold, 40, 60 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.3, 130.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;

			PackNecroReg( 12, 40 );
			PackNecroReg( 12, 40 );

			Item dracoItem = null;

			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: dracoItem = Loot.RandomWeapon( false ); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "Dracolich", "IGNORED", MorphingTemplates.TemplateDracolich("weapons") ); PackItem( dracoItem ); break;
				case 1: dracoItem = Loot.RandomJewelry(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "Dracolich", "IGNORED", MorphingTemplates.TemplateDracolich("misc") ); PackItem( dracoItem ); break;
				case 2:
					switch ( Utility.RandomMinMax( 0, 6 ) )
					{
						case 0: dracoItem = new BoneLegs(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Leggings", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
						case 1: dracoItem = new BoneGloves(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Gloves", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
						case 2: dracoItem = new BoneArms(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Arms", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
						case 3: dracoItem = new BoneChest(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Tunic", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
						case 4: dracoItem = new BoneSkirt(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Tunic", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
						case 5: dracoItem = new OrcHelm(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Helm", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
						case 6: dracoItem = new WoodenShield(); MorphingItem.MorphMyItem( dracoItem, "IGNORED", "IGNORED", "Dracolich Shield", MorphingTemplates.TemplateDracolich("armors") ); PackItem( dracoItem ); break;
					}
				break;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.Gems, 5 );
			AddLoot( LootPack.HighScrolls, 2 );
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
					if ( Utility.RandomMinMax( 1, 20 ) == 1 && killer.Skills[SkillName.Necromancy].Base >= 50 )
					{
						c.DropItem( new DracolichSkull() );
					}
				}
			}
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void OnGaveMeleeAttack( Mobile m )
		{
			base.OnGaveMeleeAttack( m );

			if ( 0.1 >= Utility.RandomDouble() )
				DoSpecialAbility( m );
		}

		public override void OnGotMeleeAttack( Mobile m )
		{
			base.OnGotMeleeAttack( m );

			if ( 0.1 >= Utility.RandomDouble() )
				DoSpecialAbility( m );
		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;

			if ( 0.25 >= Utility.RandomDouble() ) // 25% chance
				SpawnCreature( target );
		}

		public void SpawnCreature( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int monsters = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 4 ) )
			{
				if ( m is BoneKnight || m is SkeletalWarrior || m is Skeleton || m is SkeletalKnight )
					++monsters;
			}

			if ( monsters < 6 )
			{
				PlaySound( 0x216 );

				int newmonsters = Utility.RandomMinMax( 1, 3 );

				for ( int i = 0; i < newmonsters; ++i )
				{
					BaseCreature monster;

					switch ( Utility.Random( 7 ) )
					{
						default:
						case 0: monster = new BoneKnight(); break;
						case 1: monster = new SkeletalWarrior(); break;
						case 2: monster = new SkeletalKnight(); break;
						case 3: monster = new SkeletalKnight(); break;
						case 4: monster = new Skeleton(); break;
						case 5: monster = new Skeleton(); break;
						case 6: monster = new Skeleton(); break;
					}

					monster.Team = this.Team;

					bool validLocation = false;
					Point3D loc = this.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					monster.ControlSlots = 666; // WIZARD ADDED FOR MONSTER CLEANUP
					monster.MoveToWorld( loc, map );
					monster.Combatant = target;
				}
			}
		}

		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool BleedImmune{ get{ return true; } }

		public Dracolich( Serial serial ) : base( serial )
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