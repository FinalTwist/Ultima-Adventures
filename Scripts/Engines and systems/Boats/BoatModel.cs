using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BoatModel : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BoatModelDeed();
			}
		}

		[ Constructable ]
		public BoatModel()
		{
			AddonComponent ac = null;

			ac = new AddonComponent( 6986 );	AddComponent( ac, 0, 1, 0 );
		}

		public BoatModel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class BoatModelDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BoatModel();
			}
		}

		[Constructable]
		public BoatModelDeed()
		{
			Weight = 1;
			Name = "boat model";
		}

		public BoatModelDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}