using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Multis;

namespace Server.Mobiles 
{
	public class PirateCult : BasePirate 
	{
		[Constructable] 
		public PirateCult()
		{
			SpeechHue = Server.Misc.RandomThings.GetRandomColor( 0 );
			Title = "of the Leviathan Cult";
			string cult = "Cult";
			switch( Utility.RandomMinMax( 1, 8 ) )
			{
				case 1: cult = "Cult";		break;
				case 2: cult = "Order";		break;
				case 3: cult = "Clan";		break;
				case 4: cult = "Faith";		break;
				case 5: cult = "Sect";		break;
				case 6: cult = "Faction";	break;
				case 7: cult = "Glory";		break;
				case 8: cult = "Rite";		break;
			}
			switch( Utility.RandomMinMax( 1, 8 ) )
			{
				case 1: Title = "of the Leviathan " + cult;		break;
				case 2: Title = "of the Krakoa " + cult;		break;
				case 3: Title = "of the Dagon " + cult;			break;
				case 4: Title = "of the Hydros " + cult;		break;
				case 5: Title = "of the Kraken " + cult;		break;
				case 6: Title = "of the Storm Giant " + cult;	break;
				case 7: Title = "of the Jormungandr " + cult;	break;
				case 8: Title = "of the Dragon Turtle " + cult;	break;
			}

			Hue = Server.Misc.RandomThings.GetRandomSkinColor(); 

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
				Utility.AssignRandomHair( this );
				HairHue = Utility.RandomHairHue();
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
				Utility.AssignRandomHair( this );
				int HairColor = Utility.RandomHairHue();
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				HairHue = HairColor;
				FacialHairHue = HairColor;
            }

			AddItem( new Robe( 0 ) );	
			MorphingTime.ColorMyClothes( this, SpeechHue );

			int hood = Server.Misc.RandomThings.GetRandomColor( 0 );
			AddItem( new Cloak ( hood ) );
			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: AddItem( new ClothCowl( hood ) ); break;
				case 1: AddItem( new ClothHood( hood ) ); break;
				case 2: AddItem( new FancyHood( hood ) ); break;
			}

            AddItem( new ElvenBoots( 0x83A ) );

			WizardStaff staff = new WizardStaff();
			switch( Utility.RandomMinMax( 1, 5 ) )
			{
				case 1: staff.AosElementDamages.Fire = 75; staff.damageType = 1;	break;
				case 2: staff.AosElementDamages.Cold = 75; staff.damageType = 2;	break;
				case 3: staff.AosElementDamages.Energy = 75; staff.damageType = 3;	break;
				case 4: staff.AosElementDamages.Poison = 75; staff.damageType = 4;	break;
				case 5: staff.damageType = 0;										break;
			}
			AddItem( staff );

			AI = AIType.AI_Mage;
			FightMode = FightMode.Closest;
			ship = AverageShip();

			switch( Utility.RandomMinMax( 1, 5 ) )
			{
				case 1: ship.Hue = ShipColor( "" );			break;
				case 2: ship.Hue = ShipColor( "demon" );	break;
				case 3: ship.Hue = ShipColor( "undead" );	break;
				case 4: ship.Hue = ShipColor( "titan" );	break;
				case 5: ship.Hue = ShipColor( "lizard" );	break;
			}

			SetStr( 786, 985 );
			SetDex( 177, 255 );
			SetInt( 151, 250 );

			SetHits( 592, 711 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 95.5, 100.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 20000;
			Karma = -20000;

			VirtualArmor = 90;
			healme = "Heal me my followers!";
		}

		public PirateCult( Serial serial ) : base( serial )
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
	}
}