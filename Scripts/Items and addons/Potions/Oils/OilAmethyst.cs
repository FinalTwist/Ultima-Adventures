using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class OilAmethyst : Item
	{
		[Constructable]
		public OilAmethyst() : this( 1 )
		{
		}

		[Constructable]
		public OilAmethyst( int amount ) : base( 0x1FDD )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Hue = 1170;
			Name = "oil of metal enhancement ( amethyst )";
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
			private OilAmethyst m_Oil;

			public OilTarget( OilAmethyst tube ) : base( 1, false, TargetFlags.None )
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
						xOil.Resource = CraftResource.None;
						MorphingItem.MorphMyItem( iOil, "IGNORED", "Amethyst", "IGNORED", MorphingTemplates.TemplateAmethyst("weapons") );
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
						xOil.Resource = CraftResource.None;
						MorphingItem.MorphMyItem( iOil, "IGNORED", "Amethyst", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
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

		public OilAmethyst( Serial serial ) : base( serial )
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