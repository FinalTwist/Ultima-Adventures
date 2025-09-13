using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles 
{ 
	[CorpseName( "a corpse" )] 
	public class DeadWizard : BaseCreature 
	{ 
		[Constructable] 
		public DeadWizard() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			switch ( Utility.Random( 3 ) )
			{
				case 0: Hue = 1150; 	BaseSoundID = 412;		AddItem( new LightSource() );	break;	// GHOST
				case 1: Hue = 0x430;	BaseSoundID = 0x4FB;									break;	// SKELETON
				case 2: Hue = 0xB97;	BaseSoundID = 471;										break;	// ZOMBIE
			}

			if ( this.Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" ); 
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: Title = "the undead wizard"; break;
					case 1: Title = "the undead sorceress"; break;
					case 2: Title = "the undead mage"; break;
					case 3: Title = "the undead conjurer"; break;
					case 4: Title = "the undead magician"; break;
					case 5: Title = "the undead witch"; break;
				}
			} 
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" );
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0: Title = "the undead wizard"; break;
					case 1: Title = "the undead sorcerer"; break;
					case 2: Title = "the undead mage"; break;
					case 3: Title = "the undead conjurer"; break;
					case 4: Title = "the undead magician"; break;
					case 5: Title = "the undead warlock"; break;
				}
			}

			int clothHue = Utility.RandomMinMax( 0, 12 );

			Robe robe = new Robe( );
				robe.Hue = RandomThings.GetRandomColor( clothHue );
				AddItem( robe );

			if ( 50 > Utility.Random( 100 ) )
			{ 
				WizardsHat hat = new WizardsHat( );
					hat.Hue = RandomThings.GetRandomColor( clothHue );
					AddItem( hat );
			}

			Item boots = new ThighBoots( );
				boots.Hue = Utility.RandomNeutralHue();
				AddItem( boots );

			if ( Utility.RandomMinMax( 1, 4 ) > 1 )
			{
				QuarterStaff staff = new QuarterStaff();
				staff.Name = "staff";
				staff.ItemID = Utility.RandomList( 0xDF0, 0x13F8, 0xE89, 0x2D25, 0x26BC, 0x26C6, 0xDF2, 0xDF3, 0xDF4, 0xDF5 );
				if ( staff.ItemID == 0x26BC || staff.ItemID == 0x26C6 ){ staff.Name = "scepter"; }
				if ( staff.ItemID == 0xDF2 || staff.ItemID == 0xDF3 || staff.ItemID == 0xDF4 || staff.ItemID == 0xDF5 ){ staff.Name = "magic wand"; }
				staff.LootType = LootType.Blessed;
				staff.Attributes.SpellChanneling = 1;
				AddItem( staff );
			}

			SetStr( 235, 280 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 190, 235 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Macing, 90.1, 100.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 20;

			PackReg( Utility.RandomMinMax( 2, 10 ) );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
			PackReg( Utility.RandomMinMax( 2, 10 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysAttackable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool BleedImmune{ get{ return true; } }

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.BeforeMyBirth( this );

			if ( this.Hue == 1150 ){ MorphingTime.BlessMyClothes( this ); MorphingTime.ColorMyClothes( this, 1150 ); }
			else if ( this.Hue == 0xB97 ){ MorphingTime.ColorMyClothes( this, 0xB9A ); }
			else
			{
				Item helm = new WornHumanDeco();
					helm.Name = "skull";
					helm.ItemID = 0x1451;
					helm.Hue = this.Hue;
					helm.Layer = Layer.Ring;
					AddItem( helm );

				Item hands = new WornHumanDeco();
					hands.Name = "bony fingers";
					hands.ItemID = 0x1450;
					hands.Hue = this.Hue;
					hands.Layer = Layer.Gloves;
					AddItem( hands );
			}

			base.OnAfterSpawn();
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			if ( this.Hue == 1150 ){ this.Body = 13; }
			else if ( this.Hue == 0xB97 ){ this.Body = 155; }
			else { this.Body = 50; }

			return base.OnBeforeDeath();
		}

		public DeadWizard( Serial serial ) : base( serial )
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