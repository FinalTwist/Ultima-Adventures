using System;

namespace Server.Items
{
	public interface IShipwreckedItem
	{
		bool IsShipwreckedItem { get; set; }
	}

	public class ShipwreckedItem : Item, IDyable, IShipwreckedItem
	{
		public string ShipName;
		
		[CommandProperty(AccessLevel.Owner)]
		public string Ship_Name { get { return ShipName; } set { ShipName = value; InvalidateProperties(); } }

		public ShipwreckedItem( int itemID, string ThisShip ) : base( itemID )
		{
			int weight = this.ItemData.Weight;

			ShipName = ThisShip;

			if ( weight >= 255 )
				weight = 1;

			this.Weight = weight;
		}

		public override void OnSingleClick( Mobile from )
		{
			this.LabelTo( from, 1050039, String.Format( "#{0}\t#1041645", LabelNumber ) );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1041645 ); // recovered from a shipwreck
            list.Add( 1049644, ShipName );
		}

		public ShipwreckedItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ShipName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ShipName = reader.ReadString();
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			if ( ItemID >= 0x13A4 && ItemID <= 0x13AE )
			{
				Hue = sender.DyedHue;
				return true;
			}

			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

		#region IShipwreckedItem Members

		public bool IsShipwreckedItem
		{
			get
			{
				return true;	//It's a ShipwreckedItem item.  'Course it's gonna be a Shipwreckeditem
			}
			set
			{
			}
		}

		#endregion
	}
}