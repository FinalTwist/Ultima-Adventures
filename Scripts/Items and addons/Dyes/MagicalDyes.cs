using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class MagicalDyes : Item
	{
		[Constructable]
		public MagicalDyes() : this( 1 )
		{
		}

		[Constructable]
		public MagicalDyes( int amount ) : base( 0xF7D )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;

			switch ( Utility.RandomMinMax( 0, 80 ) ) 
			{
				case 0: Hue = 0x483; Name = "magical dye ( dark green )"; break;
				case 1: Hue = 0x484; Name = "magical dye ( dark blue )"; break;
				case 2: Hue = 0x485; Name = "magical dye ( dark red )"; break;
				case 3: Hue = 0x486; Name = "magical dye ( dark purple )"; break;
				case 4: Hue = 0x487; Name = "magical dye ( dark yellow )"; break;
				case 5: Hue = 0x488; Name = "magical dye ( dark bluegreen )"; break;
				case 6: Hue = 0x48A; Name = "magical dye ( blue green )"; break;
				case 7: Hue = 0x48B; Name = "magical dye ( purple green )"; break;
				case 8: Hue = 0x48C; Name = "magical dye ( green blue )"; break;
				case 9: Hue = 0x493; Name = "magical dye ( green purple )"; break;
				case 10: Hue = 0x494; Name = "magical dye ( blue red )"; break;
				case 11: Hue = 0x495; Name = "magical dye ( bright blue )"; break;
				case 12: Hue = 0x497; Name = "magical dye ( darkness )"; break;
				case 13: Hue = 0x498; Name = "magical dye ( blue darkness )"; break;
				case 14: Hue = 0x2EF; Name = "magical dye ( colorless )"; break;
				case 15: Hue = 0x47E; Name = "magical dye ( winter snow )"; break;
				case 16: Hue = 0x47F; Name = "magical dye ( winter ice )"; break;
				case 17: Hue = 0x480; Name = "magical dye ( solid ice )"; break;
				case 18: Hue = 0x481; Name = "magical dye ( snow white )"; break;
				case 19: Hue = 0x482; Name = "magical dye ( dark snow )"; break;
				case 20: Hue = 0x4AB; Name = "magical dye ( frost )"; break;
				case 21: Hue = 0xB83; Name = "magical dye ( jade )"; break;
				case 22: Hue = 0xB93; Name = "magical dye ( darker jade )"; break;
				case 23: Hue = 0xB94; Name = "magical dye ( dark jade )"; break;
				case 24: Hue = 0xB95; Name = "magical dye ( medium jade )"; break;
				case 25: Hue = 0xB96; Name = "magical dye ( light jade )"; break;
				case 26: Hue = 0x48F; Name = "magical dye ( green light )"; break;
				case 27: Hue = 0x490; Name = "magical dye ( purple light )"; break;
				case 28: Hue = 0x491; Name = "magical dye ( brown light )"; break;
				case 29: Hue = 0x48D; Name = "magical dye ( ice light )"; break;
				case 30: Hue = 0x48E; Name = "magical dye ( fire light )"; break;
				case 31: Hue = 0x499; Name = "magical dye ( gold )"; break;
				case 32: Hue = 0x4AA; Name = "magical dye ( rose red )"; break;
				case 33: Hue = 0x4AC; Name = "magical dye ( sun )"; break;
				case 34: Hue = 0x489; Name = "magical dye ( fire )"; break;
				case 35: Hue = 0x496; Name = "magical dye ( blaze )"; break;
				case 36: Hue = 0x492; Name = "magical dye ( slickness )"; break;
				case 37: Hue = 0x7E3; Name = "magical dye ( nightmare )"; break;
				case 38: Hue = 0x1;   Name = "magical dye ( pitch black )"; break;
				case 39: Hue = 0x81C; Name = "magical dye ( moonlight )"; break;
				case 40: Hue = 0x81B; Name = "magical dye ( dark nights )"; break;
				case 41: Hue = 0xB97; Name = "magical dye ( necrotic flesh )"; break;
				case 42: Hue = 0x6F3; Name = "magical dye ( bloody hell )"; break;
				case 43: Hue = 0xB85; Name = "magical dye ( bloodstone )"; break;
				case 44: Hue = 0x5B5; Name = "magical dye ( dark blood )"; break;
				case 45: Hue = 0x9C2; Name = "magical dye ( ghostly )"; break;
				case 46: Hue = 0x9C3; Name = "magical dye ( ghostly bright )"; break;
				case 47: Hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); Name = "magical dye ( dull copper metal )"; break;
				case 48: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); Name = "magical dye ( shadow iron metal )"; break;
				case 49: Hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); Name = "magical dye ( copper metal )"; break;
				case 50: Hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); Name = "magical dye ( bronze metal )"; break;
				case 51: Hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); Name = "magical dye ( gold metal )"; break;
				case 52: Hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); Name = "magical dye ( agapite metal )"; break;
				case 53: Hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); Name = "magical dye ( verite metal )"; break;
				case 54: Hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); Name = "magical dye ( valorite metal )"; break;
				case 55: Hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); Name = "magical dye ( steel metal )"; break;
				case 56: Hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); Name = "magical dye ( brass metal )"; break;
				case 57: Hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); Name = "magical dye ( mithril metal )"; break;
				case 58: Hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); Name = "magical dye ( obsidian metal )"; break;
				case 59: Hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); Name = "magical dye ( nepturite metal )"; break;
				case 60: Hue = MaterialInfo.GetMaterialColor( "deep sea", "", 0 ); Name = "magical dye ( deep sea skin )"; break;
				case 61: Hue = MaterialInfo.GetMaterialColor( "lizard", "", 0 ); Name = "magical dye ( lizard skin )"; break;
				case 62: Hue = MaterialInfo.GetMaterialColor( "serpent", "", 0 ); Name = "magical dye ( serpent skin )"; break;
				case 63: Hue = MaterialInfo.GetMaterialColor( "necrotic", "", 0 ); Name = "magical dye ( necrotic skin )"; break;
				case 64: Hue = MaterialInfo.GetMaterialColor( "volcanic", "", 0 ); Name = "magical dye ( volcanic skin )"; break;
				case 65: Hue = MaterialInfo.GetMaterialColor( "frozen", "", 0 ); Name = "magical dye ( frozen skin )"; break;
				case 66: Hue = MaterialInfo.GetMaterialColor( "goliath", "", 0 ); Name = "magical dye ( goliath skin )"; break;
				case 67: Hue = MaterialInfo.GetMaterialColor( "draconic", "", 0 ); Name = "magical dye ( draconic skin )"; break;
				case 68: Hue = MaterialInfo.GetMaterialColor( "hellish", "", 0 ); Name = "magical dye ( hellish skin )"; break;
				case 69: Hue = MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ); Name = "magical dye ( dinosaur skin )"; break;
				case 70: Hue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); Name = "magical dye ( ash wood )"; break;
				case 71: Hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); Name = "magical dye ( cherry wood )"; break;
				case 72: Hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); Name = "magical dye ( ebony wood )"; break;
				case 73: Hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); Name = "magical dye ( golden oak wood )"; break;
				case 74: Hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); Name = "magical dye ( hickory wood )"; break;
				case 75: Hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); Name = "magical dye ( mahogany wood )"; break;
				case 76: Hue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); Name = "magical dye ( oak wood )"; break;
				case 77: Hue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); Name = "magical dye ( pine wood )"; break;
				case 78: Hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); Name = "magical dye ( rosewood wood )"; break;
				case 79: Hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); Name = "magical dye ( walnut wood )"; break;
				case 80: Hue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); Name = "magical dye ( driftwood wood )"; break;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Used To Dye Almost Anything" );
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
				from.SendMessage( "What do you want to use the dye on?" );
				t = new DyeTarget( this );
				from.Target = t;
			}
		}

		private class DyeTarget : Target
		{
			private MagicalDyes m_Dye;

			public DyeTarget( MagicalDyes tube ) : base( 1, false, TargetFlags.None )
			{
				m_Dye = tube;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item iDye = targeted as Item;

					if ( !iDye.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only dye things in your pack." );
					}
					else if ( ( iDye.Stackable == true ) || ( iDye.ItemID == 8702 ) || ( iDye.ItemID == 4011 ) )
					{
						from.SendMessage( "You cannot dye that." );
					}
					else if ( iDye.IsChildOf( from.Backpack ) )
					{
						iDye.Hue = m_Dye.Hue;
							if ( iDye.Hue == 0x2EF ){ iDye.Hue = 0; }
						from.RevealingAction();
						from.PlaySound( 0x23E );
						from.AddToBackpack( new Bottle() );
						m_Dye.Consume();
					}
					else
					{
						from.SendMessage( "You cannot dye that with this." );
					}
				}
				else
				{
					from.SendMessage( "You cannot dye that with this." );
				}
			}
		}

		public MagicalDyes( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}