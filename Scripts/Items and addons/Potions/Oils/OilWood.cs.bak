using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class OilWood : Item
	{
		[Constructable]
		public OilWood() : this( 1 )
		{
		}

		[Constructable]
		public OilWood( int amount ) : base( 0x1FDD )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;

			switch ( Utility.RandomMinMax( 0, 10 ) )
			{
				case 0:		Name = "oil of wood polish ( oak )";			Hue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); break;
				case 1:		Name = "oil of wood polish ( ash )";			Hue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); break;
				case 2:		Name = "oil of wood polish ( cherry )";			Hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); break;
				case 3:		Name = "oil of wood polish ( walnut )";			Hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); break;
				case 4:		Name = "oil of wood polish ( golden oak )";		Hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); break;
				case 5:		Name = "oil of wood polish ( ebony )";			Hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); break;
				case 6:		Name = "oil of wood polish ( hickory )";		Hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); break;
				case 7:		Name = "oil of wood polish ( pine )";			Hue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); break;
				case 8:		Name = "oil of wood polish ( rosewood )";		Hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); break;
				case 9:		Name = "oil of wood polish ( mahogany )";		Hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); break;
				case 10:	Name = "oil of wood polish ( driftwood )";		Hue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); break;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Rub On Wooden Weapons or Armor" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( from.Skills[SkillName.Carpentry].Base >= 90 || from.Skills[SkillName.Fletching].Base >= 90 )
			{
				from.SendMessage( "What do you want to use the oil on?" );
				t = new OilTarget( this );
				from.Target = t;
			}
			else
			{
				from.SendMessage( "Only a master carpenter or fletcher can use this oil." );
			}
		}

		private class OilTarget : Target
		{
			private OilWood m_Oil;

			public OilTarget( OilWood tube ) : base( 1, false, TargetFlags.None )
			{
				m_Oil = tube;
			}
			
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				Item iOil = targeted as Item;

				if ( from.Backpack.FindItemByType( typeof ( OilCloth ) ) == null )
				{
					from.SendMessage( "You need an oil cloth to apply this." );
				}
				else if ( iOil is BaseWeapon )
				{
					BaseWeapon xOil = (BaseWeapon)iOil;

					if ( !iOil.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this oil on items in your pack." );
					}
					else if ( iOil.IsChildOf( from.Backpack ) && MaterialInfo.IsWoodenItem( iOil ) )
					{
						if ( m_Oil.Name == "oil of wood polish ( oak )" ) { xOil.Resource = CraftResource.OakTree; }
						else if ( m_Oil.Name == "oil of wood polish ( ash )" ) { xOil.Resource = CraftResource.AshTree; }
						else if ( m_Oil.Name == "oil of wood polish ( cherry )" ) { xOil.Resource = CraftResource.CherryTree; }
						else if ( m_Oil.Name == "oil of wood polish ( walnut )" ) { xOil.Resource = CraftResource.WalnutTree; }
						else if ( m_Oil.Name == "oil of wood polish ( golden oak )" ) { xOil.Resource = CraftResource.GoldenOakTree; }
						else if ( m_Oil.Name == "oil of wood polish ( ebony )" ) { xOil.Resource = CraftResource.EbonyTree; }
						else if ( m_Oil.Name == "oil of wood polish ( hickory )" ) { xOil.Resource = CraftResource.HickoryTree; }
						else if ( m_Oil.Name == "oil of wood polish ( pine )" ) { xOil.Resource = CraftResource.PineTree; }
						else if ( m_Oil.Name == "oil of wood polish ( rosewood )" ) { xOil.Resource = CraftResource.RosewoodTree; }
						else if ( m_Oil.Name == "oil of wood polish ( mahogany )" ) { xOil.Resource = CraftResource.MahoganyTree; }
						else if ( m_Oil.Name == "oil of wood polish ( driftwood )" ) { xOil.Resource = CraftResource.DriftwoodTree; }

						from.RevealingAction();
						from.PlaySound( 0x23E );
						from.AddToBackpack( new Bottle() );
						m_Oil.Consume();
						from.Backpack.FindItemByType( typeof ( OilCloth ) ).Delete();
					}
					else
					{
						from.SendMessage( "You cannot rub this oil on that." );
					}
				}
				else if ( iOil is BaseArmor )
				{
					BaseArmor xOil = (BaseArmor)iOil;

					if ( !iOil.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this oil on items in your pack." );
					}
					else if ( iOil.IsChildOf( from.Backpack ) && MaterialInfo.IsWoodenItem( iOil ) )
					{
						if ( m_Oil.Name == "oil of wood polish ( oak )" ){ xOil.Resource = CraftResource.OakTree; }
						else if ( m_Oil.Name == "oil of wood polish ( ash )" ){ xOil.Resource = CraftResource.AshTree; }
						else if ( m_Oil.Name == "oil of wood polish ( cherry )" ){ xOil.Resource = CraftResource.CherryTree; }
						else if ( m_Oil.Name == "oil of wood polish ( walnut )" ){ xOil.Resource = CraftResource.WalnutTree; }
						else if ( m_Oil.Name == "oil of wood polish ( golden oak )" ){ xOil.Resource = CraftResource.GoldenOakTree; }
						else if ( m_Oil.Name == "oil of wood polish ( ebony )" ){ xOil.Resource = CraftResource.EbonyTree; }
						else if ( m_Oil.Name == "oil of wood polish ( hickory )" ){ xOil.Resource = CraftResource.HickoryTree; }
						else if ( m_Oil.Name == "oil of wood polish ( pine )" ){ xOil.Resource = CraftResource.PineTree; }
						else if ( m_Oil.Name == "oil of wood polish ( rosewood )" ){ xOil.Resource = CraftResource.RosewoodTree; }
						else if ( m_Oil.Name == "oil of wood polish ( mahogany )" ){ xOil.Resource = CraftResource.MahoganyTree; }
						else if ( m_Oil.Name == "oil of wood polish ( driftwood )" ){ xOil.Resource = CraftResource.DriftwoodTree; }

						from.RevealingAction();
						from.PlaySound( 0x23E );
						from.AddToBackpack( new Bottle() );
						m_Oil.Consume();
						from.Backpack.FindItemByType( typeof ( OilCloth ) ).Delete();
					}
					else
					{
						from.SendMessage( "You cannot rub this oil on that." );
					}
				}
				else
				{
					from.SendMessage( "You cannot rub this oil on that." );
				}
			}
		}

		public OilWood( Serial serial ) : base( serial )
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
			ItemID = 0x1FDD;
		}
	}
}