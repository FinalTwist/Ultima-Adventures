using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HalloweenDeco4 : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			{3807, 0, 1, 0}, {3807, 1, 1, 0}, {3809, 0, 0, 0}, {3809, 1, 0, 0}, {6921, 1, 1, 0}, {3898, -1, 1, 0}
		};

		[ Constructable ]
		public HalloweenDeco4()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 4460, 0, -1, 0, 0, -1, "Happy Halloween", 1);// 5
			AddComplexComponent( (BaseAddon) this, 4462, 1, -1, 0, 0, -1, "Happy Halloween", 1);// 6
			AddComplexComponent( (BaseAddon) this, 16638, 0, 0, 1, 0, 2, "Lantern", 1);// 9
		}

		public HalloweenDeco4( Serial serial ) : base( serial )
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
}