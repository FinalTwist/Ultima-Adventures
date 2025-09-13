using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	public class BoatSailorArcher : BaseSailor
	{
		[Constructable]
		public BoatSailorArcher()
		{
			AI = AIType.AI_Archer;
			FightMode = FightMode.Evil;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Title = "the sailor"; break;
				case 1: Title = "the fisherman"; break;
				case 2: Title = "the mariner"; break;
				case 3: Title = "the seafarer"; break;
			}

			SetStr( (int)(level*0.75) );
			SetDex( level );
			SetInt( (int)(level*0.50) );

			Karma = -1 * Karma;

			if ( Female )
			{
				Utility.AssignRandomHair( this );
			}
			else
			{
				Utility.AssignRandomHair( this );
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
			}
		}

		public BoatSailorArcher( Serial serial ) : base( serial )
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

	public class BoatSailorBard : BaseSailor
	{
		[Constructable]
		public BoatSailorBard()
		{
			AI = AIType.AI_Archer;
			FightMode = FightMode.Evil;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Title = "the sailor"; break;
				case 1: Title = "the fisherman"; break;
				case 2: Title = "the mariner"; break;
				case 3: Title = "the seafarer"; break;
			}

			SetStr( (int)(level*0.75) );
			SetDex( level );
			SetInt( (int)(level*0.50) );

			Karma = -1 * Karma;

			if ( Female )
			{
				Utility.AssignRandomHair( this );
			}
			else
			{
				Utility.AssignRandomHair( this );
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
			}

			switch ( Utility.Random( 5 ))
			{
				case 0: PackItem( new BambooFlute() );	break;
				case 1: PackItem( new Drums() );		break;
				case 2: PackItem( new Tambourine() );	break;
				case 3: PackItem( new LapHarp() );		break;
				case 4: PackItem( new Lute() );			break;
			}
		}

		public BoatSailorBard( Serial serial ) : base( serial )
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

	public class BoatSailorMage : BaseSailor
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
		public BoatSailorMage()
		{
			AI = AIType.AI_Mage;
			FightMode = FightMode.Evil;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Title = "the sailor"; break;
				case 1: Title = "the fisherman"; break;
				case 2: Title = "the mariner"; break;
				case 3: Title = "the seafarer"; break;
			}

			SetDex( (int)(level*0.75) );
			SetInt( level );
			SetStr( (int)(level*0.50) );

			Karma = -1 * Karma;

			if ( Female )
			{
				Utility.AssignRandomHair( this );
			}
			else
			{
				Utility.AssignRandomHair( this );
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
			}
		}

		public BoatSailorMage( Serial serial ) : base( serial )
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

	public class ElfBoatSailorArcher : BaseSailor
	{
		[Constructable]
		public ElfBoatSailorArcher()
		{
			AI = AIType.AI_Archer;
			FightMode = FightMode.Evil;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Title = "the elf sailor"; break;
				case 1: Title = "the elf fisherman"; break;
				case 2: Title = "the elf mariner"; break;
				case 3: Title = "the elf seafarer"; break;
			}

			SetStr( (int)(level*0.75) );
			SetDex( level );
			SetInt( (int)(level*0.50) );

			Karma = -1 * Karma;

			Race = Race.Elf;
			if ( this.Female = Utility.RandomBool() )
			{
				Body = 606;
				Name = NameList.RandomName( "elf_female" );
				Utility.AssignRandomHair( this );
			}
			else
			{
				Body = 605;
				Name = NameList.RandomName( "elf_male" );
				Utility.AssignRandomHair( this );
			}
		}

		public ElfBoatSailorArcher( Serial serial ) : base( serial )
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

	public class ElfBoatSailorBard : BaseSailor
	{
		[Constructable]
		public ElfBoatSailorBard()
		{
			AI = AIType.AI_Archer;
			FightMode = FightMode.Evil;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Title = "the elf sailor"; break;
				case 1: Title = "the elf fisherman"; break;
				case 2: Title = "the elf mariner"; break;
				case 3: Title = "the elf seafarer"; break;
			}

			SetStr( (int)(level*0.75) );
			SetDex( level );
			SetInt( (int)(level*0.50) );

			Karma = -1 * Karma;

			Race = Race.Elf;
			if ( Female )
			{
				Body = 606;
				Name = NameList.RandomName( "elf_female" );
				Utility.AssignRandomHair( this );
			}
			else
			{
				Body = 605;
				Name = NameList.RandomName( "elf_male" );
				Utility.AssignRandomHair( this );
			}

			switch ( Utility.Random( 5 ))
			{
				case 0: PackItem( new BambooFlute() );	break;
				case 1: PackItem( new Drums() );		break;
				case 2: PackItem( new Tambourine() );	break;
				case 3: PackItem( new LapHarp() );		break;
				case 4: PackItem( new Lute() );			break;
			}
		}

		public ElfBoatSailorBard( Serial serial ) : base( serial )
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

	public class ElfBoatSailorMage : BaseSailor
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
		public ElfBoatSailorMage()
		{
			AI = AIType.AI_Mage;
			FightMode = FightMode.Evil;

			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0: Title = "the sailor"; break;
				case 1: Title = "the fisherman"; break;
				case 2: Title = "the mariner"; break;
				case 3: Title = "the seafarer"; break;
			}

			SetDex( (int)(level*0.75) );
			SetInt( level );
			SetStr( (int)(level*0.50) );

			Karma = -1 * Karma;

			Race = Race.Elf;
			if ( Female )
			{
				Body = 606;
				Name = NameList.RandomName( "elf_female" );
				Utility.AssignRandomHair( this );
			}
			else
			{
				Body = 605;
				Name = NameList.RandomName( "elf_male" );
				Utility.AssignRandomHair( this );
			}
		}

		public ElfBoatSailorMage( Serial serial ) : base( serial )
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