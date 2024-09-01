using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class HorseArmor : Item
	{
		[Constructable]
		public HorseArmor() : base( 0x040A )
		{
			Weight = 25.0;
			Name = "horse barding";
			Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 );

			int chance = 0;
			double chancetest = Utility.RandomDouble();
			
            if (chancetest < 0.50 )
                chance = 3;
            else if (chancetest < 0.70)
                chance = 7;
            else if (chancetest < 0.85)
                chance = 9;
            else if (chancetest < 0.95)
                chance = 11;
            else if (chancetest >= 0.95)
                chance = 14;
            
			string ArmorMaterial = null;
            switch ( Utility.Random( chance ) )
            {
                case 0: break; // Stick with default
                case 1: Hue = Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); break;
                case 2: Hue = Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); break;
                case 3: Hue = Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); break;
                case 4: Hue = Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); break;
                case 5: Hue = Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); break;
                case 6: Hue = Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); break;
                case 7: Hue = Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); break;
                case 8: Hue = Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); break;
                case 9: Hue = Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); break;
                case 10: Hue = Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); break;
                case 11: Hue = Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); break;
                case 12: Hue = Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); break;
                case 13: Hue = Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); break;
            }
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "Which horse do you want to use this on?" );
				t = new HorseTarget( this, Hue );
				from.Target = t;
			}
		}

		private class HorseTarget : Target
		{
			private HorseArmor m_Horse;
			private int m_Hue;

			public HorseTarget( HorseArmor armor, int hue ) : base( 8, false, TargetFlags.None )
			{
				m_Horse = armor;
				m_Hue = hue;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				BaseMount mount = targeted as BaseMount;
				if ( mount != null )
				{
					if ( mount.ControlMaster == from
						&& (
							mount is Horse || mount is AngryHorse || mount is SeaHorse
							|| mount is ZebraRiding || mount is Zebra 
							|| mount is FireSteed || mount is IceSteed || mount is RainbowSteed || mount is SilverSteed
							|| mount is Nightmare || mount is AncientNightmareRiding
							|| mount is Unicorn || mount is DarkUnicornRiding || mount is RainbowUnicorn
						) )
					{
						if ( MyServerSettings.ClientVersion() )
						{
							mount.Body = 587;
							mount.ItemID = 587;
						}
						else
						{
							mount.Body = 0xE2;
							mount.ItemID = 0x3EA0;
						}

						mount.Hue = m_Hue;

						from.RevealingAction();
						from.PlaySound( 0x0AA );

						m_Horse.Consume();
					}
					else
					{
						from.SendMessage( "This armor is only for horses you own." );
					}
				}
				else
				{
					from.SendMessage( "This armor is only for horses you own." );
				}
			}
		}

		public HorseArmor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			if (version == 0)
            	reader.ReadString(); // ArmorMaterial. Throw away
		}
	}
}