using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class halloween_coffin_eastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new halloween_coffin_eastAddonDeed();
			}
		}

		[ Constructable ]
		public halloween_coffin_eastAddon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 7233 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 7234 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 7235 );
			AddComponent( ac, 1, 0, 0 );
		}

		public halloween_coffin_eastAddon( Serial serial ) : base( serial )
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

	public class halloween_coffin_eastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new halloween_coffin_eastAddon();
			}
		}

		[Constructable]
		public halloween_coffin_eastAddonDeed()
		{
			Name = "Wooden Coffin East";
			Hue = 0x96C;
			ItemID = 0x3F0E;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public halloween_coffin_eastAddonDeed( Serial serial ) : base( serial )
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
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class halloween_coffin_southAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new halloween_coffin_southAddonDeed();
			}
		}

		[ Constructable ]
		public halloween_coffin_southAddon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 7247 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 7248 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 7249 );
			AddComponent( ac, 0, 1, 0 );
		}

		public halloween_coffin_southAddon( Serial serial ) : base( serial )
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

	public class halloween_coffin_southAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new halloween_coffin_southAddon();
			}
		}

		[Constructable]
		public halloween_coffin_southAddonDeed()
		{
			Name = "Wooden Coffin South";
			Hue = 0x96C;
			ItemID = 0x3F0E;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public halloween_coffin_southAddonDeed( Serial serial ) : base( serial )
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
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class halloween_block_southAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new halloween_block_southAddonDeed();
			}
		}

		[ Constructable ]
		public halloween_block_southAddon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 4724 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 4725 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 4726 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 4727 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 4728 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 4729 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 4730 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 4731 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 4732 );
			AddComponent( ac, -1, -1, 0 );
		}

		public halloween_block_southAddon( Serial serial ) : base( serial )
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

	public class halloween_block_southAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new halloween_block_southAddon();
			}
		}

		[Constructable]
		public halloween_block_southAddonDeed()
		{
			Name = "Executioner Block South Deed";
			Hue = 0x96C;
		}

		public halloween_block_southAddonDeed( Serial serial ) : base( serial )
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
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class halloween_block_eastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new halloween_block_eastAddonDeed();
			}
		}

		[ Constructable ]
		public halloween_block_eastAddon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 4715 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 4716 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 4717 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 4718 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 4719 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 4720 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 4721 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 4722 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 4723 );
			AddComponent( ac, -1, -1, 0 );
		}

		public halloween_block_eastAddon( Serial serial ) : base( serial )
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

	public class halloween_block_eastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new halloween_block_eastAddon();
			}
		}

		[Constructable]
		public halloween_block_eastAddonDeed()
		{
			Name = "Executioner Block East Deed";
			Hue = 0x96C;
		}

		public halloween_block_eastAddonDeed( Serial serial ) : base( serial )
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