using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly essence" )]
	public class GhostWarrior : BaseCreature
	{
		[Constructable]
		public GhostWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
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

			switch( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: AddItem( new FancyShirt() );	break;
				case 1: AddItem( new Shirt() );			break;
				case 2: AddItem( new FormalShirt() );	break;
			}

			if ( 1 == Utility.RandomMinMax( 1, 2 ) && Body == 400 ){ AddItem( new Robe() ); }

			if ( 1 == Utility.RandomMinMax( 1, 2 ) && Body == 401 )
			{
				switch( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0: AddItem( new FancyDress() );	break;
					case 1: AddItem( new GildedDress() );	break;
					case 2: AddItem( new PlainDress() );	break;
				}
			}

			switch( Utility.RandomMinMax( 0, 1 ) )
			{
				case 0: AddItem( new LongPants() );		break;
				case 1: AddItem( new ShortPants() );	break;
			}

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
			}

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, GhostHue );

			BaseSoundID = 0x482;

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 65.0, 87.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			Fame = 1000;
			Karma = -1000;

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			VirtualArmor = 18;

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Average );
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 13;
			return base.OnBeforeDeath();
		}

		public override bool BleedImmune{ get{ return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public GhostWarrior( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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