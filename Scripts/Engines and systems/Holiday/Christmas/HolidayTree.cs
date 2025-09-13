using System;
using Server;

namespace Server.Items
{
	public class HolidayTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeDeed(); } }

		[Constructable]
		public HolidayTreeAddon()
		{
			int tree = Utility.RandomList( 0xCC2, 0xD3E );
			AddComponent( new AddonComponent( tree ), 0, 0, 0 );
		}

		public HolidayTreeAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class HolidayTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeAddon(); } }

		[Constructable]
		public HolidayTreeDeed()
		{
			Name = "holiday tree";
			ItemID = 0x0CC8;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeDeed( Serial serial ) : base( serial )
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