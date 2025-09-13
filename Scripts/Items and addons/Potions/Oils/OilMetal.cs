using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class OilMetal : Item
	{
		[Constructable]
		public OilMetal() : this( 1 )
		{
		}

		[Constructable]
		public OilMetal( int amount ) : base( 0x1FDD )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;

			switch ( Utility.RandomMinMax( 0, 12 ) ) 
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); Name = "oil of metal enhancement ( dull copper )";	break;
				case 1: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); Name = "oil of metal enhancement ( shadow iron )";	break;
				case 2: Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); Name = "oil of metal enhancement ( copper )";		break;
				case 3: Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); Name = "oil of metal enhancement ( bronze )";		break;
				case 4: Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); Name = "oil of metal enhancement ( gold )";		break;
				case 5: Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); Name = "oil of metal enhancement ( agapite )";		break;
				case 6: Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); Name = "oil of metal enhancement ( verite )";		break;
				case 7: Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); Name = "oil of metal enhancement ( valorite )";	break;
				case 8: Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 ); Name = "oil of metal enhancement ( steel )";		break;
				case 9: Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 ); Name = "oil of metal enhancement ( brass )";		break;
				case 10: Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); Name = "oil of metal enhancement ( mithril )";	break;
				case 11: Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); Name = "oil of metal enhancement ( obsidian )";	break;
				case 12: Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); Name = "oil of metal enhancement ( nepturite )";	break;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Rub On Metal To Change It" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( from.Skills[SkillName.Blacksmith].Base >= 90 )
			{
				from.SendMessage( "What do you want to use the oil on?" );
				t = new OilTarget( this );
				from.Target = t;
			}
			else
			{
				from.SendMessage( "Only a master blacksmith can use this oil." );
			}
		}

		private class OilTarget : Target
		{
			private OilMetal m_Oil;

			public OilTarget( OilMetal tube ) : base( 1, false, TargetFlags.None )
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
					else if ( iOil.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsMetalItem( iOil ) )
					{
						if ( m_Oil.Name == "oil of metal enhancement ( dull copper )" ) { xOil.Resource = CraftResource.DullCopper; }
						else if ( m_Oil.Name == "oil of metal enhancement ( shadow iron )" ) { xOil.Resource = CraftResource.ShadowIron; }
						else if ( m_Oil.Name == "oil of metal enhancement ( copper )" ) { xOil.Resource = CraftResource.Copper; }
						else if ( m_Oil.Name == "oil of metal enhancement ( bronze )" ) { xOil.Resource = CraftResource.Bronze; }
						else if ( m_Oil.Name == "oil of metal enhancement ( gold )" ) { xOil.Resource = CraftResource.Gold; }
						else if ( m_Oil.Name == "oil of metal enhancement ( agapite )" ) { xOil.Resource = CraftResource.Agapite; }
						else if ( m_Oil.Name == "oil of metal enhancement ( verite )" ) { xOil.Resource = CraftResource.Verite; }
						else if ( m_Oil.Name == "oil of metal enhancement ( valorite )" ) { xOil.Resource = CraftResource.Valorite; }
						else if ( m_Oil.Name == "oil of metal enhancement ( steel )" ) { xOil.Resource = CraftResource.Steel; }
						else if ( m_Oil.Name == "oil of metal enhancement ( brass )" ) { xOil.Resource = CraftResource.Brass; }
						else if ( m_Oil.Name == "oil of metal enhancement ( mithril )" ) { xOil.Resource = CraftResource.Mithril; }
						else if ( m_Oil.Name == "oil of metal enhancement ( obsidian )" ) { xOil.Resource = CraftResource.Obsidian; }
						else if ( m_Oil.Name == "oil of metal enhancement ( nepturite )" ) { xOil.Resource = CraftResource.Nepturite; }

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
					else if ( iOil.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsMetalItem( iOil ) )
					{
						if ( m_Oil.Name == "oil of metal enhancement ( dull copper )" ) { xOil.Resource = CraftResource.DullCopper; }
						else if ( m_Oil.Name == "oil of metal enhancement ( shadow iron )" ) { xOil.Resource = CraftResource.ShadowIron; }
						else if ( m_Oil.Name == "oil of metal enhancement ( copper )" ) { xOil.Resource = CraftResource.Copper; }
						else if ( m_Oil.Name == "oil of metal enhancement ( bronze )" ) { xOil.Resource = CraftResource.Bronze; }
						else if ( m_Oil.Name == "oil of metal enhancement ( gold )" ) { xOil.Resource = CraftResource.Gold; }
						else if ( m_Oil.Name == "oil of metal enhancement ( agapite )" ) { xOil.Resource = CraftResource.Agapite; }
						else if ( m_Oil.Name == "oil of metal enhancement ( verite )" ) { xOil.Resource = CraftResource.Verite; }
						else if ( m_Oil.Name == "oil of metal enhancement ( valorite )" ) { xOil.Resource = CraftResource.Valorite; }
						else if ( m_Oil.Name == "oil of metal enhancement ( steel )" ) { xOil.Resource = CraftResource.Steel; }
						else if ( m_Oil.Name == "oil of metal enhancement ( brass )" ) { xOil.Resource = CraftResource.Brass; }
						else if ( m_Oil.Name == "oil of metal enhancement ( mithril )" ) { xOil.Resource = CraftResource.Mithril; }
						else if ( m_Oil.Name == "oil of metal enhancement ( obsidian )" ) { xOil.Resource = CraftResource.Obsidian; }
						else if ( m_Oil.Name == "oil of metal enhancement ( nepturite )" ) { xOil.Resource = CraftResource.Nepturite; }

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

		public OilMetal( Serial serial ) : base( serial )
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