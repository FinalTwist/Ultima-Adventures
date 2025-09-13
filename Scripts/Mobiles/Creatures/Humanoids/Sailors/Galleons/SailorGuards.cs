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
	public class SailorGuards : BasePirate 
	{
		[Constructable] 
		public SailorGuards()
		{
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();

			if ( this.Female = Utility.RandomBool() ) 
			{
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" ); 
				Utility.AssignRandomHair( this );
				HairHue = Utility.RandomHairHue();
			} 
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" );
				Utility.AssignRandomHair( this );
				int HairColor = Utility.RandomHairHue();
				FacialHairItemID = Utility.RandomList( 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
				HairHue = HairColor;
				FacialHairHue = HairColor;
			}

			switch( Utility.RandomMinMax( 1, 6 ) )
			{
				case 1: AddItem( new Halberd() );		break;
				case 2: AddItem( new Spear() );		break;
				case 3: AddItem( new Pike() );		break;
				case 4: AddItem( new Longsword() );	AddItem( new OrderShield() );		break;
				case 5: AddItem( new Longsword() );	AddItem( new MetalShield() );		break;
				case 6: AddItem( new Longsword() );	AddItem( new BronzeShield() );	break;
			}

			AI = AIType.AI_Melee;
			FightMode = FightMode.Evil;
			if ( Utility.RandomBool() ){ ship = new GalleonLarge(); } else { ship = new GalleonRoyal(); }
			ship.Hue = ShipColor( "" );

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
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Fencing, 90.1, 100.0 );
			SetSkill( SkillName.Swords, 90.1, 100.0 );

			Fame = 20000;
			Karma = 20000;

			VirtualArmor = 90;
			healme = "Heal me my comrades!";
		}

		public SailorGuards( Serial serial ) : base( serial )
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