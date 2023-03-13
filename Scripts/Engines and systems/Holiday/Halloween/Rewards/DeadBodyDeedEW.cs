using System;
using Server;

namespace Server.Items
{
	public class DeadBodyEWAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DeadBodyEWDeed(); } }

		[Constructable]
		public DeadBodyEWAddon()
		{
			Name = "Dead Body";       
			AddComponent( new AddonComponent( 7451 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 7452 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 7450 ), 1, 0, 0 );
		}

		public DeadBodyEWAddon( Serial serial ) : base( serial )
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

	public class DeadBodyEWDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DeadBodyEWAddon(); } }

		[Constructable]
		public DeadBodyEWDeed()
		{
			Name = "Dead Body Deed (east)";
			Hue = 0x96C;
		}

		public DeadBodyEWDeed( Serial serial ) : base( serial )
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