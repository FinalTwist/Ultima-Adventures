using System;
using Server;

namespace Server.Items
{
	public class DistilleryEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DistilleryEastDeed(); } }

		public override bool RetainDeedHue{ get{ return true; } }

		[Constructable]
		public DistilleryEastAddon() : this( 0 )
		{
		}

		[Constructable]
		public DistilleryEastAddon( int hue )
		{
			AddComponent( new LocalizedAddonComponent( 0x3DBB, 1150640  ), 0, 1, 0 );
			AddComponent( new LocalizedAddonComponent( 0x3DBA, 1150640 ), 0, 0, 0 );
			Hue = hue;
		}

		public DistilleryEastAddon( Serial serial ) : base( serial )
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

	public class DistilleryEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DistilleryEastAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1150664; } } // goza (east)

		[Constructable]
		public DistilleryEastDeed()
		{
		}

		public DistilleryEastDeed( Serial serial ) : base( serial )
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

	public class DistillerySouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DistillerySouthDeed(); } }

		public override bool RetainDeedHue{ get{ return true; } }

		[Constructable]
		public DistillerySouthAddon() : this( 0 )
		{
		}

		[Constructable]
		public DistillerySouthAddon( int hue )
		{
			AddComponent( new LocalizedAddonComponent( 0x3DB8, 1150640 ), 1, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x3DB9, 1150640 ), 0, 0, 0 );
			Hue = hue;
		}

		public DistillerySouthAddon( Serial serial ) : base( serial )
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

	public class DistillerySouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DistillerySouthAddon( this.Hue ); } }
		public override int LabelNumber{ get{ return 1150663; } } // goza (south)

		[Constructable]
		public DistillerySouthDeed()
		{
		}

		public DistillerySouthDeed( Serial serial ) : base( serial )
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