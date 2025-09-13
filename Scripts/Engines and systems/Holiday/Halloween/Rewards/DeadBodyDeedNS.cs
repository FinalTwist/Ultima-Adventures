using System;
using Server;

namespace Server.Items
{
	public class DeadBodyNSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DeadBodyNSDeed(); } }

		[Constructable]
		public DeadBodyNSAddon()
		{
			Name = "Dead Body";       
			AddComponent( new AddonComponent( 7480 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 7481 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 7479 ), 0, 1, 0 );
		}

		public DeadBodyNSAddon( Serial serial ) : base( serial )
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

	public class DeadBodyNSDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DeadBodyNSAddon(); } }

		[Constructable]
		public DeadBodyNSDeed()
		{
			Name = "Dead Body Deed (south)";
			Hue = 0x96C;
		}

		public DeadBodyNSDeed( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Happy Halloween" );
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