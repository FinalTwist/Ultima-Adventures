using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EvilFireplaceSouthFaceAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			{7134, 0, 0, 0}, {6660, 2, 0, 0}// 9	13	
		};
 
		public override BaseAddonDeed Deed
		{
			get
			{
				return new EvilFireplaceSouthFaceAddonDeed();
			}
		}

		[ Constructable ]
		public EvilFireplaceSouthFaceAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 8675, -1, 0, 0, 1175, -1, "fireplace", 1);// 1
			AddComplexComponent( (BaseAddon) this, 8675, 1, 0, 0, 1175, -1, "fireplace", 1);// 2
			AddComplexComponent( (BaseAddon) this, 1315, 0, 0, 0, 1175, -1, "fireplace", 1);// 3
			AddComplexComponent( (BaseAddon) this, 1315, 1, 0, 0, 1175, -1, "fireplace", 1);// 4
			AddComplexComponent( (BaseAddon) this, 765, 1, 0, 10, 1175, -1, "fireplace", 1);// 5
			AddComplexComponent( (BaseAddon) this, 766, 0, 0, 10, 1175, -1, "fireplace", 1);// 6
			AddComplexComponent( (BaseAddon) this, 767, -1, 0, 10, 1175, -1, "fireplace", 1);// 7
			AddComplexComponent( (BaseAddon) this, 4012, 0, 0, 0, 1175, 1, "fireplace", 1);// 8
			AddComplexComponent( (BaseAddon) this, 6571, 0, 0, 4, 0, 1, "fire", 1);// 10
			AddComplexComponent( (BaseAddon) this, 7906, 0, 1, 11, 0, -1, "ghostly face", 1);// 11
			AddComplexComponent( (BaseAddon) this, 2562, 1, 1, 13, 0, 1, "", 1);// 12
		}

		public EvilFireplaceSouthFaceAddon( Serial serial ) : base( serial )
		{
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

	public class EvilFireplaceSouthFaceAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EvilFireplaceSouthFaceAddon();
			}
		}

		[Constructable]
		public EvilFireplaceSouthFaceAddonDeed()
		{
			Name = "Evil Fireplace South Deed";
			Hue = 1175;
		}

		public EvilFireplaceSouthFaceAddonDeed( Serial serial ) : base( serial )
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
	public class EvilFireplaceEastFaceAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			{7137, 0, 0, 0}, {6659, 0, 2, 0}// 9	11	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new EvilFireplaceEastFaceAddonDeed();
			}
		}

		[ Constructable ]
		public EvilFireplaceEastFaceAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 8674, 0, 1, 0, 1175, -1, "fireplace", 1);// 1
			AddComplexComponent( (BaseAddon) this, 8674, 0, -1, 0, 1175, -1, "fireplace", 1);// 2
			AddComplexComponent( (BaseAddon) this, 1315, 0, 0, 0, 1175, -1, "fireplace", 1);// 3
			AddComplexComponent( (BaseAddon) this, 1315, 0, 1, 0, 1175, -1, "fireplace", 1);// 4
			AddComplexComponent( (BaseAddon) this, 765, 0, 1, 10, 1175, -1, "fireplace", 1);// 5
			AddComplexComponent( (BaseAddon) this, 767, 0, 0, 10, 1175, -1, "fireplace", 1);// 6
			AddComplexComponent( (BaseAddon) this, 766, 0, -1, 10, 1175, -1, "fireplace", 1);// 7
			AddComplexComponent( (BaseAddon) this, 4012, 0, 0, 0, 1175, 1, "fireplace", 1);// 8
			AddComplexComponent( (BaseAddon) this, 6571, 0, 0, 4, 0, 1, "fire", 1);// 10
			AddComplexComponent( (BaseAddon) this, 7924, 1, 0, 11, 0, -1, "ghostly face", 1);// 12
			AddComplexComponent( (BaseAddon) this, 2557, 1, 1, 13, 0, 1, "", 1);// 13
		}

		public EvilFireplaceEastFaceAddon( Serial serial ) : base( serial )
		{
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

	public class EvilFireplaceEastFaceAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new EvilFireplaceEastFaceAddon();
			}
		}

		[Constructable]
		public EvilFireplaceEastFaceAddonDeed()
		{
			Name = "Evil Fireplace East Deed";
			Hue = 1175;
		}

		public EvilFireplaceEastFaceAddonDeed( Serial serial ) : base( serial )
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