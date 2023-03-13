using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly essence" )]
	public class GhostPirate : BaseCreature
	{
		[Constructable]
		public GhostPirate() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
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
				case 4: Name = "a apparition";	break;
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

			switch ( Utility.Random( 1 ))
			{
				case 0: AddItem( new LongPants ( Utility.RandomRedHue() ) ); break;
				case 1: AddItem( new ShortPants( Utility.RandomRedHue() ) ); break;
			}				
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new FancyShirt( Utility.RandomRedHue() ) ); break;
				case 1: AddItem( new Shirt( Utility.RandomRedHue() ) ); break;
				case 2: AddItem( new Doublet( Utility.RandomRedHue() ) ); break;
			}					
			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new Bandana( Utility.RandomRedHue() ) ); break;
				case 1: AddItem( new SkullCap( Utility.RandomRedHue() ) ); break;
			}	

			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Dagger() ); break;
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

		public GhostPirate( Serial serial ) : base( serial )
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