using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;

namespace Server.Items
{
	public class PotionCauldron : BaseAddon
	{
		private int m_Pool;
		private int m_Uses;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Pool
		{
			get{ return m_Pool; }
			set{ m_Pool = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; InvalidateProperties(); }
		}

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromMinutes( 30.0 ); } } // HOW LONG UNTIL THE POOL DECAYS IN MINUTES

		[ Constructable ]
		public PotionCauldron()
		{
			int potionHue = 0;
			string potionName = "";

			m_Pool = Utility.Random( 30 );
			m_Uses = Utility.RandomMinMax( 1, 10 );

			switch ( m_Pool ) 
			{
				case 0: potionHue = 1109; potionName = "Cauldron of Nightsight Liquid"; break;
				case 1: potionHue = 45; potionName = "Cauldron of Lesser Cure Liquid"; break;
				case 2: potionHue = 45; potionName = "Cauldron of Cure Liquid"; break;
				case 3: potionHue = 45; potionName = "Cauldron of Greater Cure Liquid"; break;
				case 4: potionHue = 396; potionName = "Cauldron of Agility Liquid"; break;
				case 5: potionHue = 396; potionName = "Cauldron of Greater Agility Liquid"; break;
				case 6: potionHue = 1001; potionName = "Cauldron of Strength Liquid"; break;
				case 7: potionHue = 1001; potionName = "Cauldron of Greater Strength Liquid"; break;
				case 8: potionHue = 73; potionName = "Cauldron of Lesser Poison Liquid"; break;
				case 9: potionHue = 73; potionName = "Cauldron of Poison Liquid"; break;
				case 10: potionHue = 73; potionName = "Cauldron of Greater Poison Liquid"; break;
				case 11: potionHue = 73; potionName = "Cauldron of Deadly Poison Liquid"; break;
				case 12: potionHue = 140; potionName = "Cauldron of Refresh Liquid"; break;
				case 13: potionHue = 140; potionName = "Cauldron of Total Refresh Liquid"; break;
				case 14: potionHue = 50; potionName = "Cauldron of Lesser Heal Liquid"; break;
				case 15: potionHue = 50; potionName = "Cauldron of Heal Liquid"; break;
				case 16: potionHue = 50; potionName = "Cauldron of Greater Heal Liquid"; break;
				case 17: potionHue = 425; potionName = "Cauldron of Lesser Explosion Liquid"; break;
				case 18: potionHue = 425; potionName = "Cauldron of Explosion Liquid"; break;
				case 19: potionHue = 425; potionName = "Cauldron of Greater Explosion Liquid"; break;
				case 20: potionHue = 0x490; potionName = "Cauldron of Lesser Invisibility Liquid"; break;
				case 21: potionHue = 0x490; potionName = "Cauldron of Invisibility Liquid"; break;
				case 22: potionHue = 0x490; potionName = "Cauldron of Greater Invisibility Liquid"; break;
				case 23: potionHue = 0x48E; potionName = "Cauldron of Lesser Rejuvenate Liquid"; break;
				case 24: potionHue = 0x48E; potionName = "Cauldron of Rejuvenate Liquid"; break;
				case 25: potionHue = 0x48E; potionName = "Cauldron of Greater Rejuvenate Liquid"; break;
				case 26: potionHue = 0x48D; potionName = "Cauldron of Lesser Mana Liquid"; break;
				case 27: potionHue = 0x48D; potionName = "Cauldron of Mana Liquid"; break;
				case 28: potionHue = 0x48D; potionName = "Cauldron of Greater Mana Liquid"; break;
				case 29: potionHue = 0x496; potionName = "Cauldron of Invulnerability Liquid"; break;
			}

			AddComplexComponent( (BaseAddon) this, 0x2068, 0, 0, 0, 0, 0, potionName, 1);
			AddComplexComponent( (BaseAddon) this, 0xFAC, 0, 0, 0, 0, 0, potionName, 1);
			AddComplexComponent( (BaseAddon) this, 0x970, 0, 0, 8, potionHue, 0, potionName, 1);

			RefreshDecay( true );
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckAddComponents ) );
		}

		public PotionCauldron( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            ac.Light = LightType.Circle150;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 3 ) )
			{
				from.SendMessage( "You will have to get closer to take liquid from the cauldron." );
			}
			else if ( m_Uses > 0 )
			{
				if ( from.Backpack != null )
				{
					Item iBottle = from.Backpack.FindItemByType( typeof ( Bottle ) );

					if ( iBottle != null )
					{
						if ( iBottle.Amount > 1){ iBottle.Amount = iBottle.Amount - 1; }
						else { iBottle.Consume(); }

						from.PlaySound( 0x4E );
						from.SendMessage( "You fill a potion bottle with the liquid from the cauldron." );
						this.m_Uses = this.m_Uses - 1;

						if ( m_Pool == 0 ){ from.AddToBackpack( new NightSightPotion() ); }
						else if ( m_Pool == 1 ){ from.AddToBackpack( new LesserCurePotion() ); }
						else if ( m_Pool == 2 ){ from.AddToBackpack( new CurePotion() ); }
						else if ( m_Pool == 3 ){ from.AddToBackpack( new GreaterCurePotion() ); }
						else if ( m_Pool == 4 ){ from.AddToBackpack( new AgilityPotion() ); }
						else if ( m_Pool == 5 ){ from.AddToBackpack( new GreaterAgilityPotion() ); }
						else if ( m_Pool == 6 ){ from.AddToBackpack( new StrengthPotion() ); }
						else if ( m_Pool == 7 ){ from.AddToBackpack( new GreaterStrengthPotion() ); }
						else if ( m_Pool == 8 ){ from.AddToBackpack( new LesserPoisonPotion() ); }
						else if ( m_Pool == 9 ){ from.AddToBackpack( new PoisonPotion() ); }
						else if ( m_Pool == 10 ){ from.AddToBackpack( new GreaterPoisonPotion() ); }
						else if ( m_Pool == 11 ){ from.AddToBackpack( new DeadlyPoisonPotion() ); }
						else if ( m_Pool == 12 ){ from.AddToBackpack( new RefreshPotion() ); }
						else if ( m_Pool == 13 ){ from.AddToBackpack( new TotalRefreshPotion() ); }
						else if ( m_Pool == 14 ){ from.AddToBackpack( new LesserHealPotion() ); }
						else if ( m_Pool == 15 ){ from.AddToBackpack( new HealPotion() ); }
						else if ( m_Pool == 16 ){ from.AddToBackpack( new GreaterHealPotion() ); }
						else if ( m_Pool == 17 ){ from.AddToBackpack( new LesserExplosionPotion() ); }
						else if ( m_Pool == 18 ){ from.AddToBackpack( new ExplosionPotion() ); }
						else if ( m_Pool == 19 ){ from.AddToBackpack( new GreaterExplosionPotion() ); }
						else if ( m_Pool == 20 ){ from.AddToBackpack( new LesserInvisibilityPotion() ); }
						else if ( m_Pool == 21 ){ from.AddToBackpack( new InvisibilityPotion() ); }
						else if ( m_Pool == 22 ){ from.AddToBackpack( new GreaterInvisibilityPotion() ); }
						else if ( m_Pool == 23 ){ from.AddToBackpack( new LesserRejuvenatePotion() ); }
						else if ( m_Pool == 24 ){ from.AddToBackpack( new RejuvenatePotion() ); }
						else if ( m_Pool == 25 ){ from.AddToBackpack( new GreaterRejuvenatePotion() ); }
						else if ( m_Pool == 26 ){ from.AddToBackpack( new LesserManaPotion() ); }
						else if ( m_Pool == 27 ){ from.AddToBackpack( new ManaPotion() ); }
						else if ( m_Pool == 28 ){ from.AddToBackpack( new GreaterManaPotion() ); }
						else { from.AddToBackpack( new InvulnerabilityPotion() ); }
					}
					else
					{
						from.SendMessage( "You do not have an empty potion bottle to draw liquid from the cauldron." );
					}

				}
			}
			else
			{
				from.SendMessage( "There is no longer enough in the cauldron to fill an entire bottle." );
			}
		}

		public void CheckAddComponents()
		{
			if( Deleted )
				return;
			AddComponents();
		}

		public virtual void AddComponents()
		{
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
			writer.WriteDeltaTime( m_DecayTime );
			writer.WriteEncodedInt( (int) m_Pool );
			writer.WriteEncodedInt( (int) m_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
			m_Pool = reader.ReadEncodedInt();
			m_Uses = reader.ReadEncodedInt();
		}
	}
	public class EmptyPotionCauldron : BaseAddon
	{
		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromMinutes( 30.0 ); } } // HOW LONG UNTIL THE POOL DECAYS IN MINUTES

		[ Constructable ]
		public EmptyPotionCauldron()
		{
			AddComplexComponent( (BaseAddon) this, 0x2068, 0, 0, 0, 0, 0, "Empty Cauldron", 1);
			AddComplexComponent( (BaseAddon) this, 0xFAC, 0, 0, 0, 0, 0, "Empty Cauldron", 1);

			RefreshDecay( true );
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckAddComponents ) );
		}

		public EmptyPotionCauldron( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            ac.Light = LightType.Circle150;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public void CheckAddComponents()
		{
			if( Deleted )
				return;
			AddComponents();
		}

		public virtual void AddComponents()
		{
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 );
			writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
		}
	}
}