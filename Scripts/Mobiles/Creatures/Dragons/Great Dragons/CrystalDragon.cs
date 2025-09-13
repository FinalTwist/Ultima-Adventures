using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class CrystalDragon : BaseCreature
	{
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 28; } }
		public override int BreathColdDamage{ get{ return 24; } }
		public override int BreathPoisonDamage{ get{ return 24; } }
		public override int BreathEnergyDamage{ get{ return 24; } }
		public override int BreathEffectHue{ get{ return 0xA50; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override bool CanChew { get{return true;}}
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 1 ); }

		[Constructable]
		public CrystalDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "dragon" );
			Title = "the crystal dragon";
			Body = 105;
			BaseSoundID = 362;
			Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );

			SetStr( 896, 985 );
			SetDex( 86, 175 );
			SetInt( 586, 675 );

			SetHits( 558, 611 );

			SetDamage( 23, 30 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 15 );
			SetDamageType( ResistanceType.Cold, 15 );
			SetDamageType( ResistanceType.Poison, 15 );
			SetDamageType( ResistanceType.Energy, 15 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 20000;
			Karma = -20000;

			VirtualArmor = 60;

			AddItem( new LighterSource() );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Item scale = new CrystalScales();
			c.DropItem(scale);

			if ( 1 == Utility.RandomMinMax( 0, 3 ) )
			{
				LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
				MyChest.Name = "crystal chest";
				MyChest.Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
				MyChest.ItemID = Utility.RandomList( 0xe40, 0xe41 );
				c.DropItem(MyChest);
			}

			Mobile killer = this.LastKiller;
			if ( killer != null )
			{
				if ( killer is BaseCreature )
					killer = ((BaseCreature)killer).GetMaster();

				if ( killer is PlayerMobile )
				{
					if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) )
					{
						c.DropItem( new LargeCrystal() );
					}
					Server.Mobiles.Dragons.DropSpecial( this, killer, "", "Crystal", this.Name + " " + this.Title, c, 10, 0 );
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
			{
				this.Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
			{
				this.Hue = Utility.RandomList( 0x48D, 0x48E, 0x48F, 0x490, 0x491 );
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			reflect = true; // Every spell is reflected back to the caster
		}

        public override int GetAngerSound()
        {
            return 0x63E;
        }

        public override int GetDeathSound()
        {
            return 0x63F;
        }

        public override int GetHurtSound()
        {
            return 0x640;
        }

        public override int GetIdleSound()
        {
            return 0x641;
        }

		public override bool AutoDispel{ get{ return !Controlled; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public CrystalDragon( Serial serial ) : base( serial )
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

namespace Server.Items
{
	public class CrystalScales : Item
	{
		[Constructable]
		public CrystalScales() : base( 0x2DA )
		{
			Weight = Utility.RandomMinMax( 10, 25 );
			ItemID = 0x1444;
			Light = LightType.Circle300;
			Name = "sparkling crystal";
		}

		public CrystalScales(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
		}
	}
}