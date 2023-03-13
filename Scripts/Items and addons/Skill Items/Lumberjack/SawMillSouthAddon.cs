using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SawMillSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SawMillSouthAddonDeed();
			}
		}

		[ Constructable ]
		public SawMillSouthAddon()
		{
			AddComplexComponent( (BaseAddon) this, 1928, 0, 0, 0, 0, -1, "saw mill", 1);
			AddComplexComponent( (BaseAddon) this, 1928, 1, 0, 0, 0, -1, "saw mill", 1);
			AddComplexComponent( (BaseAddon) this, 4525, 1, 0, 5, 0, -1, "saw mill", 1);
			AddComplexComponent( (BaseAddon) this, 7130, 0, 0, 5, 0, -1, "saw mill", 1);
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public SawMillSouthAddon( Serial serial ) : base( serial )
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

	public class SawMillSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SawMillSouthAddon();
			}
		}

		[Constructable]
		public SawMillSouthAddonDeed()
		{
			Name = "saw mill deed (south)";
		}

		public SawMillSouthAddonDeed( Serial serial ) : base( serial )
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