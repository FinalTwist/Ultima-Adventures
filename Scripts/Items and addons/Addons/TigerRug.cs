using System;
using Server;

namespace Server.Items
{
	public class TigerRugEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new TigerRugEastDeed(); } }

		[Constructable]
		public TigerRugEastAddon()
		{
			AddComponent( new AddonComponent( 21225 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 21228 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 21229 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 21230 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 21231 ), -1, 1, 0 );
			AddComponent( new AddonComponent( 21237 ), -1, 0, 0 );
		}

		public TigerRugEastAddon( Serial serial ) : base( serial )
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

	public class TigerRugEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new TigerRugEastAddon(); } }

		[Constructable]
		public TigerRugEastDeed()
		{
			Name = "tiger rug (east)";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public TigerRugEastDeed( Serial serial ) : base( serial )
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

	public class TigerRugSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new TigerRugSouthDeed(); } }

		[Constructable]
		public TigerRugSouthAddon()
		{
			AddComponent( new AddonComponent( 21221 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 21222 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 21223 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 21224 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 21238 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 21251 ), 0, 1, 0 );
		}

		public TigerRugSouthAddon( Serial serial ) : base( serial )
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

	public class TigerRugSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new TigerRugSouthAddon(); } }

		[Constructable]
		public TigerRugSouthDeed()
		{
			Name = "tiger rug (south)";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public TigerRugSouthDeed( Serial serial ) : base( serial )
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
	public class WhiteTigerRugEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new WhiteTigerRugEastDeed(); } }

		[Constructable]
		public WhiteTigerRugEastAddon()
		{
			AddComponent( new AddonComponent( 21240 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 21239 ), -1, 1, 0 );
			AddComponent( new AddonComponent( 21236 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 21235 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 21234 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 21233 ), 1, 1, 0 );
		}

		public WhiteTigerRugEastAddon( Serial serial ) : base( serial )
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

	public class WhiteTigerRugEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new WhiteTigerRugEastAddon(); } }

		[Constructable]
		public WhiteTigerRugEastDeed()
		{
			Name = "white tiger rug (east)";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public WhiteTigerRugEastDeed( Serial serial ) : base( serial )
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

	public class WhiteTigerRugSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new WhiteTigerRugSouthDeed(); } }

		[Constructable]
		public WhiteTigerRugSouthAddon()
		{
			AddComponent( new AddonComponent( 21232 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 21226 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 21242 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 21227 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 21220 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 21241 ), 1, 1, 0 );
		}

		public WhiteTigerRugSouthAddon( Serial serial ) : base( serial )
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

	public class WhiteTigerRugSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new WhiteTigerRugSouthAddon(); } }

		[Constructable]
		public WhiteTigerRugSouthDeed()
		{
			Name = "white tiger rug (south)";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public WhiteTigerRugSouthDeed( Serial serial ) : base( serial )
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