using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a zuluu corpse" )]
	public class ZuluuWitchDoctor : BaseCreature
	{
		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ if ( YellHue < 2 ){ return 100; } else { return 0; } } }
		public override int BreathColdDamage{ get{ if ( YellHue == 3 ){ return 100; } else { return 0; } } }
		public override int BreathPoisonDamage{ get{ if ( YellHue == 2 ){ return 100; } else { return 0; } } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ if ( YellHue == 1 ){ return 0x488; } else if ( YellHue == 2 ){ return 0xB92; } else if ( YellHue == 3 ){ return 0x5B5; } else { return 0x4FD; } } }
		public override int BreathEffectSound{ get{ return 0x238; } }
		public override int BreathEffectItemID{ get{ return 0x1005; } } // EXPLOSION POTION
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 2 ); }
		public override double BreathDamageScalar{ get{ return 0.4; } }

		[Constructable]
		public ZuluuWitchDoctor() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			((BaseCreature)this).midrace = 1;
			Name = "a zuluu witch doctor";
			Hue = 0x89D;

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Item cloth9 = new FemaleLeatherChest();
					cloth9.Hue = 0xB9A;
					cloth9.Name = "Zuluu Tunic";
					AddItem( cloth9 );
			}
			else
			{
				Body = 0x190;
			}

			HairHue = 0x96C;

			SetStr( 146, 165 );
			SetDex( 71, 130 );
			SetInt( 381, 405 );

			SetDamage( 14, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 87.5, 100.0 );
			SetSkill( SkillName.Fencing, 72.5, 85.0 );
			SetSkill( SkillName.Macing, 82.5, 105.0 );
			SetSkill( SkillName.Magery, 82.5, 95.0 );
			SetSkill( SkillName.Meditation, 87.5, 100.0 );
			SetSkill( SkillName.MagicResist, 87.5, 100.0 );
			SetSkill( SkillName.Swords, 72.5, 85.0 );
			SetSkill( SkillName.Tactics, 72.5, 85.0 );
			SetSkill( SkillName.Wrestling, 72.5, 85.0 );

			Fame = 4200;
			Karma = -4200;
			VirtualArmor = 10;
			
			PackReg( 10, 15 );
			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );

			AddItem( new WildStaff() );

			Item cloth1 = new SavageArms();
			  	cloth1.Hue = 0xB9A;
				cloth1.Name = "Zuluu Guantlets";
			  	AddItem( cloth1 );
			Item cloth2 = new SavageLegs();
			  	cloth2.Hue = 0xB9A;
				cloth2.Name = "Zuluu Leggings";
			  	AddItem( cloth2 );
			Item cloth3 = new HornedTribalMask();
			  	cloth3.Hue = 0xB9A;
				cloth3.Name = "Zuluu Tribal Mask";
			  	AddItem( cloth3 );
			Item cloth4 = new LeatherSkirt();
			  	cloth4.Hue = 0xB9A;
				cloth4.Name = "Zuluu Skirt";
			  	cloth4.Layer = Layer.Waist;
			  	AddItem( cloth4 );

			if ( Utility.RandomMinMax( 1, 12 ) == 1 )
			{
				SwampDragon pet = new SwampDragon();
				pet.Hue = 2006;
				pet.Name = "a forest dragyn";
				pet.Rider = this;
				ActiveSpeed = 0.1;
				PassiveSpeed = 0.2;
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Potions );
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			return base.OnBeforeDeath();
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }

		public ZuluuWitchDoctor( Serial serial ) : base( serial )
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