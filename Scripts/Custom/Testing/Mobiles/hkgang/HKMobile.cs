using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Mobiles;

namespace Server.Engines.HunterKiller
{
	public abstract class HKMobile : BaseCreature
	{
		public HKMobile( AIType aiType, FightMode mode, HunterKillerType type ) : base( aiType, mode, 18, ( type != HunterKillerType.WarriorType ) ? 6 : 1, 0.1, 0.2 )
		{
			Title = "the murderer";

			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

            AddItem( new Shirt( Utility.RandomNeutralHue() )); 

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );

			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;

			AddItem( hair );

			if (type != HunterKillerType.MageType)
			{
				AddItem( new CloseHelm() );
				AddItem( new PlateChest() );
	            AddItem( new PlateLegs() );
    	        AddItem( new PlateArms() );
        	    AddItem( new LeatherGorget() );
			}

			switch(type)
			{
			case HunterKillerType.WarriorType:

				if (Utility.RandomBool())
				{
					AddItem( new Spear() );
				}
				else
				{
					switch ( Utility.Random( 3 )) 
					{
					case 0: AddItem( new Longsword() ); break; 
					case 1: AddItem( new Broadsword() ); break; 
					case 2: AddItem( new VikingSword() ); break; 
					}

					switch ( Utility.Random( 8 )) 
					{ 
					case 0: AddItem( new BronzeShield() ); break; 
					case 1: AddItem( new HeaterShield() ); break; 
					case 2: AddItem( new MetalKiteShield() ); break; 
					case 3: AddItem( new MetalShield() ); break; 
					case 4: AddItem( new WoodenKiteShield() ); break; 
					case 5: AddItem( new WoodenShield() ); break; 
					case 6: AddItem( new OrderShield() ); break; 
					case 7: AddItem( new ChaosShield() ); break; 
					} 
				}

				break;

			case HunterKillerType.ArcherType:

				AddItem( new RepeatingCrossbow() );
				PackItem( new Bolt( Utility.Random( 50, 120 ) ) );

				break;

			case HunterKillerType.MageType:

				AddItem( new Robe( GetRandomHue() ) );
				AddItem( new ThighBoots() );
				AddItem( new LeatherGloves() );
				AddItem( new Cloak( GetRandomHue() ) );

				break;
			}

            PackGold( 350, 550 ); 
		}

		public HKMobile( Serial serial ) : base( serial )
		{
		}

        private static int GetRandomHue()
        {
            switch ( Utility.Random( 6 ) )
            {
                default:
                case 0: return 0;
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
                case 5: return Utility.RandomNeutralHue();
            }
        }

		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null ) 
			{
				mount.Rider = null;

				if (mount is Mobile) ((Mobile)mount).Delete();
			}

			return base.OnBeforeDeath();
		}

		public override void OnDeath( Container c )
		{

			Mobile m = FindMostRecentDamager( false );

			if ( m != null && m.Player )
			{
				bool gainedPath = false;

                int theirTotal = m.SkillsTotal;
                int ourTotal = this.SkillsTotal;

				int pointsToGain = 1 + ((theirTotal - ourTotal) / 50);

				if ( pointsToGain < 1 )
					pointsToGain = 1;
				else if ( pointsToGain > 4 )
					pointsToGain = 4;

				if ( VirtueHelper.Award( m, VirtueName.Justice, pointsToGain, ref gainedPath ) )
				{
					if ( gainedPath )
						m.SendLocalizedMessage( 1049367 ); // You have gained a path in Justice!
					else
						m.SendLocalizedMessage( 1049363 ); // You have gained in Justice.

					m.FixedParticles( 0x375A, 9, 20, 5027, EffectLayer.Waist );
					m.PlaySound( 0x1F7 );
				}
			}
            base.OnDeath(c);
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public override bool PlayerRangeSensitive{ get { return false; } }

		public override bool CanRummageCorpses{ get{ return true; } }

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