using System;
using Server;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using Server.Spells;
using Server.Network;
using Server.Multis;
using Server.Misc;
using System.Collections;

namespace Server.Items 
{
	public class TenFootPole : Item
	{
		private int m_Charges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public TenFootPole( ) : base( 0xE8A )
		{		
			Weight = 40.0; 
			Name = "ten foot pole";
			Charges = 20;
			Hue = 0x972;
		}

		public static int TapChance( double weight )
		{
			int chance = 50;

			if ( weight < 40.0 )
			{
				if ( weight > 38.0 ){ chance = 53; }
				else if ( weight > 37.0 ){ chance = 56; }
				else if ( weight > 36.0 ){ chance = 59; }
				else if ( weight > 35.0 ){ chance = 62; }
				else if ( weight > 34.0 ){ chance = 65; }
				else if ( weight > 33.0 ){ chance = 68; }
				else if ( weight > 32.0 ){ chance = 71; }
				else if ( weight > 31.0 ){ chance = 74; }
				else if ( weight > 30.0 ){ chance = 77; }
				else if ( weight > 29.0 ){ chance = 80; }
				else if ( weight > 28.0 ){ chance = 83; }
				else if ( weight > 27.0 ){ chance = 86; }
				else if ( weight > 26.0 ){ chance = 89; }
				else if ( weight > 25.0 ){ chance = 92; }
			}
			return chance;
		}

		public static void Material( Item pole, int level )
		{
			int pick = Utility.RandomMinMax(1,90) + level;
			int cHue = 0x972;
			double cWeight = 40.0;
			string wood = "wood";

			int boards = Utility.RandomMinMax( 1, 65536 );

			if ( boards >= 32768 ){ /* REGULAR */ }
			else if ( boards >= 16384 ){cHue = MaterialInfo.GetMaterialColor( "ash", "", 0 );			cWeight = cWeight-1; 	wood = "ashen";				}
			else if ( boards >= 8192 ){ cHue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); 		cWeight = cWeight-2; 	wood = "cherry wood";		}
			else if ( boards >= 4096 ){ cHue = MaterialInfo.GetMaterialColor( "ebony", "", 0 );			cWeight = cWeight-3; 	wood = "ebony wood";		}
			else if ( boards >= 2048 ){ cHue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); 	cWeight = cWeight-4; 	wood = "golden oak";		}
			else if ( boards >= 1024 ){ cHue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); 		cWeight = cWeight-5; 	wood = "hickory";			}
			else if ( boards >= 512 ){ 	cHue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); 		cWeight = cWeight-6; 	wood = "mahogany";			}
			else if ( boards >= 256 ){ 	cHue = MaterialInfo.GetMaterialColor( "oak", "", 0 );			cWeight = cWeight-8; 	wood = "oaken";				}
			else if ( boards >= 128 ){ 	cHue = MaterialInfo.GetMaterialColor( "pine", "", 0 );			cWeight = cWeight-9; 	wood = "pine wood";			}
			else if ( boards >= 64 ){ 	cHue = MaterialInfo.GetMaterialColor( "ghostwood", "", 0 );		cWeight = cWeight-10; 	wood = "ghostwood";			}
			else if ( boards >= 32 ){ 	cHue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 );		cWeight = cWeight-11; 	wood = "rosewood";			}
			else if ( boards >= 16 ){ 	cHue = MaterialInfo.GetMaterialColor( "walnut", "", 0 );		cWeight = cWeight-12; 	wood = "valnut wood";		}
			else if ( boards >= 8 ){ 	cHue = MaterialInfo.GetMaterialColor( "petrified", "", 0 ); 	cWeight = cWeight-13; 	wood = "petrified wood";	}
			else if ( boards >= 4 ){ 	cHue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); 	cWeight = cWeight-7; 	wood = "driftwood";			}
			else { 						cHue = MaterialInfo.GetMaterialColor( "elven", "", 0 );			cWeight = cWeight-14; 	wood = "elven wood";		}

			pole.Name = "ten foot " + wood + " pole";
			pole.Weight = cWeight;
			pole.Hue = cHue;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "" + TapChance( Weight ) + "% Avoiding Traps");
			list.Add( 1049644, "For Wall, Floor & Container Traps"); // PARENTHESIS
        }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

		public TenFootPole( Serial serial ) : base( serial )
		{ 
		} 
		
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
			writer.Write( (int) m_Charges );
		} 
		
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Charges = (int)reader.ReadInt();
					break;
				}
			}
		}
	}
}