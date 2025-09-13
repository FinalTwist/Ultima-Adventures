using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	public class OilLeather : Item
	{
		[Constructable]
		public OilLeather() : this( 1 )
		{
		}

		[Constructable]
		public OilLeather( int amount ) : base( 0x1FDD )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;

			switch ( Utility.RandomMinMax( 0, 9 ) ) 
			{
				case 0: Hue = MaterialInfo.GetMaterialColor( "deep sea", "", 0 ); Name = "oil of leather enhancement ( deep sea )";	break;
				case 1: Hue = MaterialInfo.GetMaterialColor( "lizard", "", 0 ); Name = "oil of leather enhancement ( lizard )";	break;
				case 2: Hue = MaterialInfo.GetMaterialColor( "serpent", "", 0 ); Name = "oil of leather enhancement ( serpent )";	break;
				case 3: Hue = MaterialInfo.GetMaterialColor( "necrotic", "", 0 ); Name = "oil of leather enhancement ( necrotic )";	break;
				case 4: Hue = MaterialInfo.GetMaterialColor( "volcanic", "", 0 ); Name = "oil of leather enhancement ( volcanic )";	break;
				case 5: Hue = MaterialInfo.GetMaterialColor( "frozen", "", 0 ); Name = "oil of leather enhancement ( frozen )";	break;
				case 6: Hue = MaterialInfo.GetMaterialColor( "goliath", "", 0 ); Name = "oil of leather enhancement ( goliath )";	break;
				case 7: Hue = MaterialInfo.GetMaterialColor( "draconic", "", 0 ); Name = "oil of leather enhancement ( draconic )";	break;
				case 8: Hue = MaterialInfo.GetMaterialColor( "hellish", "", 0 ); Name = "oil of leather enhancement ( hellish )";	break;
				case 9: Hue = MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ); Name = "oil of leather enhancement ( dinosaur )";	break;
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Rub On Leather To Change It" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( from.Skills[SkillName.Tailoring].Base >= 90 )
			{
				from.SendMessage( "What do you want to use the oil on?" );
				t = new OilTarget( this );
				from.Target = t;
			}
			else
			{
				from.SendMessage( "Only a master tailor can use this oil." );
			}
		}

		private class OilTarget : Target
		{
			private OilLeather m_Oil;

			public OilTarget( OilLeather tube ) : base( 1, false, TargetFlags.None )
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
				else if ( iOil is BaseArmor )
				{
					BaseArmor xOil = (BaseArmor)iOil;

					if ( !iOil.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this oil on items in your pack." );
					}
					else if ( iOil.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsLeatherItem( iOil ))
                    {
						if ( m_Oil.Name == "oil of leather enhancement ( deep sea )" ) { xOil.Resource = CraftResource.SpinedLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( lizard )" ) { xOil.Resource = CraftResource.HornedLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( serpent )" ) { xOil.Resource = CraftResource.BarbedLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( necrotic )" ) { xOil.Resource = CraftResource.NecroticLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( volcanic )" ) { xOil.Resource = CraftResource.VolcanicLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( frozen )" ) { xOil.Resource = CraftResource.FrozenLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( goliath )" ) { xOil.Resource = CraftResource.GoliathLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( draconic )" ) { xOil.Resource = CraftResource.DraconicLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( hellish )" ) { xOil.Resource = CraftResource.HellishLeather; }
						else if ( m_Oil.Name == "oil of leather enhancement ( dinosaur )" ) { xOil.Resource = CraftResource.DinosaurLeather; }

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
				else if ( iOil is BaseWeapon )
				{
					BaseWeapon xOil = (BaseWeapon)iOil;

					if ( !iOil.IsChildOf( from.Backpack ) )
					{
						from.SendMessage( "You can only use this oil on items in your pack." );
					}
					else if ( iOil.IsChildOf( from.Backpack ) && Server.Misc.MaterialInfo.IsLeatherItem( iOil ))
                    {
						if ( m_Oil.Hue == 0x555 || m_Oil.Name == "oil of leather enhancement ( deep sea )" ) { xOil.Resource = CraftResource.SpinedLeather; }
						else if ( m_Oil.Hue == 0x586 || m_Oil.Name == "oil of leather enhancement ( lizard )" ) { xOil.Resource = CraftResource.HornedLeather; }
						else if ( m_Oil.Hue == 0x7D1 || m_Oil.Name == "oil of leather enhancement ( serpent )" ) { xOil.Resource = CraftResource.BarbedLeather; }
						else if ( m_Oil.Hue == 0xB97 || m_Oil.Name == "oil of leather enhancement ( necrotic )" ) { xOil.Resource = CraftResource.NecroticLeather; }
						else if ( m_Oil.Hue == 0x4EB || m_Oil.Name == "oil of leather enhancement ( volcanic )" ) { xOil.Resource = CraftResource.VolcanicLeather; }
						else if ( m_Oil.Hue == 0x47E || m_Oil.Name == "oil of leather enhancement ( frozen )" ) { xOil.Resource = CraftResource.FrozenLeather; }
						else if ( m_Oil.Hue == 0x6DF || m_Oil.Name == "oil of leather enhancement ( goliath )" ) { xOil.Resource = CraftResource.GoliathLeather; }
						else if ( m_Oil.Hue == 0x846 || m_Oil.Name == "oil of leather enhancement ( draconic )" ) { xOil.Resource = CraftResource.DraconicLeather; }
						else if ( m_Oil.Hue == 0x5B5 || m_Oil.Name == "oil of leather enhancement ( hellish )" ) { xOil.Resource = CraftResource.HellishLeather; }
						else if ( m_Oil.Hue == 0x430 || m_Oil.Name == "oil of leather enhancement ( dinosaur )" ) { xOil.Resource = CraftResource.DinosaurLeather; }

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

		public OilLeather( Serial serial ) : base( serial )
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