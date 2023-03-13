using System;
using Server.Items;

namespace Server.Items
{
	public class PureWhiteFeather : Item
	{
		
		//int ICommodity.DescriptionNumber { get { return CraftResources.IsStandard( m_Resource ) ? LabelNumber : "pure white feather" + ( (int)m_Resource - (int)CraftResource.Feather ); } }
		//bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public PureWhiteFeather() : this( 1 )
		{
		}

		[Constructable]
		public PureWhiteFeather( int amount ) : base( 0x1BD1 )
		{
			Stackable = true;
			Weight = 0.1;
		    Hue = 1153;
			Amount = amount;
		}

		public PureWhiteFeather( Serial serial ) : base( serial )
		{
		}

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
