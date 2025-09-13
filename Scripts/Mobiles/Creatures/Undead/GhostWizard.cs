using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly essence" )]
	public class GhostWizard : BaseCreature 
	{ 
		[Constructable] 
		public GhostWizard() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			switch( Utility.RandomMinMax( 0, 1 ) )
			{
				case 0: Body = 401;	break;
				case 1: Body = 400;	break;
			}

			switch( Utility.RandomMinMax( 0, 8 ) )
			{
				case 0: Name = "a ghost";		break;
				case 1: Name = "a spirit";		break;
				case 2: Name = "a phantom";		break;
				case 3: Name = "a shadow";		break;
				case 4: Name = "an apparition";	break;
				case 5: Name = "a haunt";		break;
				case 6: Name = "a shade";		break;
				case 7: Name = "a spectre";		break;
				case 8: Name = "a wraith";		break;
			}

			int GhostHue = 0x47E;

			Hue = GhostHue;

			if ( 1 == Utility.RandomMinMax( 1, 2 ) ){ Utility.AssignRandomHair( this ); HairHue = GhostHue; }

			switch( Utility.RandomMinMax( 0, 1 ) )
			{
				case 0: AddItem( new Boots() );			break;
				case 1: AddItem( new ThighBoots() );	break;
			}

			if ( 1 == Utility.RandomMinMax( 1, 2 ) ){ AddItem( new Cloak() ); }
			if ( 1 == Utility.RandomMinMax( 1, 2 ) )
			{
				switch( Utility.RandomMinMax( 0, 6 ) )
				{
					case 0: AddItem( new WizardsHat() );	break;
					case 1: AddItem( new WitchHat() );		break;
					case 2: AddItem( new ReaperCowl() );	break;
					case 3: AddItem( new ReaperHood() );	break;
					case 4: AddItem( new ClothHood() );		break;
					case 5: AddItem( new ClothCowl() );		break;
					case 6: AddItem( new FancyHood() );		break;
				}
			}

			if ( Body == 400 ){ AddItem( new Robe() ); }
			else
			{
				switch( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: AddItem( new FancyDress() );	break;
					case 1: AddItem( new GildedDress() );	break;
					case 2: AddItem( new PlainDress() );	break;
				}
			}

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, GhostHue );

			BaseSoundID = 0x482;

			SetStr( 81, 105 );
			SetDex( 91, 115 );
			SetInt( 96, 120 );

			SetHits( 49, 63 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 75.1, 100.0 );
			SetSkill( SkillName.Magery, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 16;
			PackReg( Utility.RandomMinMax( 1, 5 ) );
			PackReg( Utility.RandomMinMax( 1, 5 ) );
			PackReg( Utility.RandomMinMax( 1, 5 ) );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool OnBeforeDeath()
		{
			if ( Server.Misc.IntelligentAction.HealThySelf( this ) ){ return false; }
			this.Body = 13;
			return base.OnBeforeDeath();
		}

		public override bool BleedImmune{ get{ return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public GhostWizard( Serial serial ) : base( serial )
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