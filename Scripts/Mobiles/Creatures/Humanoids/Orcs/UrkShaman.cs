using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an orcish corpse" )] 
	public class UrkShaman : BaseCreature 
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
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }
		

		[Constructable] 
		public UrkShaman() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			((BaseCreature)this).midrace = 5;
			BaseSoundID = 0x45A;
			Hue = 0x430;
			Body = 0x190; 
			Name = NameList.RandomName( "urk" );
			HairItemID = 0;
			FacialHairItemID = 0;

			switch ( Utility.RandomMinMax( 0, 5 ) )
			{
				case 0: Title = "the urk shaman"; break;
				case 1: Title = "the urk witch doctor"; break;
				case 2: Title = "the urk priest"; break;
				case 3: Title = "the urk diviner"; break;
				case 4: Title = "the urk seer"; break;
				case 5: Title = "the urk theurgist"; break;
			}

			Item helm = new WornHumanDeco();
				helm.Name = "urkish face";
				helm.ItemID = 0x141B;
				helm.Hue = 0x430;
				helm.Layer = Layer.Helm;
				AddItem( helm );

			Robe robe = new Robe();
				robe.Name = "urkish rat fur robe";
				robe.Hue = 0x972;
				robe.Resistances.Poison = 10;
				AddItem( robe );

			if ( Utility.RandomMinMax( 1, 4 ) > 1 )
			{
				QuarterStaff staff = new QuarterStaff();
				staff.Name = "urkish staff";
				staff.ItemID = Utility.RandomList( 0xDF0, 0x13F8, 0xE89, 0x2D25 );
				staff.LootType = LootType.Blessed;
				staff.Attributes.SpellChanneling = 1;
				staff.Hue = 0x7D1;
				((BaseWeapon)staff).AosElementDamages.Physical = 60;
				((BaseWeapon)staff).AosElementDamages.Poison = 40;
				AddItem( staff );

				QuarterStaff stick = new QuarterStaff();
				stick.Name = "urkish staff";
				stick.ItemID = staff.ItemID;
				stick.Hue = 0x7D1;
				((BaseWeapon)stick).AosElementDamages.Physical = 60;
				((BaseWeapon)stick).AosElementDamages.Poison = 40;
				PackItem( stick );
			}

			SetStr( 81, 105 );
			SetDex( 91, 115 );
			SetInt( 96, 120 );

			SetHits( 49, 63 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 60, 80 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 75.1, 100.0 );
			SetSkill( SkillName.Magery, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );
			SetSkill( SkillName.Macing, 20.3, 60.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 16;
			PackReg( Utility.RandomMinMax( 2, 10 ) );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 1 : 0; } }

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			this.Body = 17;
			return base.OnBeforeDeath();
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.SavagesAndOrcs; }
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.PoisonVictim( defender, this );
		}

		public UrkShaman( Serial serial ) : base( serial )
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