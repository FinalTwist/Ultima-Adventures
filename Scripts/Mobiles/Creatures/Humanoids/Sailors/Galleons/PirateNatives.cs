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
	public class PirateNatives : BasePirate 
	{
		[Constructable] 
		public PirateNatives()
		{
			Name = NameList.RandomName( "lizardman" );
			Title = "the savage pirate";
			Hue = 743;

			int leader = Server.Misc.RandomThings.GetRandomColor( 0 );

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Item cloth9 = new FemaleLeatherChest();
					cloth9.Hue = leader;
					cloth9.Name = "Native Tunic";
					AddItem( cloth9 );
			}
			else
			{
				Body = 0x190;
			}

			HairHue = 0x96C;

			AddItem( new Spear() );

			Item cloth1 = new SavageArms();
			  	cloth1.Hue = leader;
				cloth1.Name = "Native Guantlets";
			  	AddItem( cloth1 );
			Item cloth2 = new SavageLegs();
			  	cloth2.Hue = leader;
				cloth2.Name = "Native Leggings";
			  	AddItem( cloth2 );
			Item cloth3 = new StagMask();
			  	cloth3.Hue = 932;
				cloth3.Name = "Native Tribal Mask";
			  	AddItem( cloth3 );
			Item cloth4 = new LeatherSkirt();
			  	cloth4.Hue = leader;
				cloth4.Name = "Native Skirt";
			  	cloth4.Layer = Layer.Waist;
			  	AddItem( cloth4 );

			AI = AIType.AI_Melee;
			FightMode = FightMode.Closest;
			ship = new GalleonBarbarian();
			ship.Hue = ShipColor( "" );

			SetStr( 536, 585 );
			SetDex( 126, 145 );
			SetInt( 281, 305 );

			SetHits( 322, 351 );
			SetMana( 0 );

			SetDamage( 16, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );
			SetSkill( SkillName.Fencing, 80.1, 90.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 48;
			healme = "Heal me my tribesman!";
		}

		public PirateNatives( Serial serial ) : base( serial )
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