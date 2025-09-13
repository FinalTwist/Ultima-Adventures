using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HalloweenDeco2 : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3894, -1, 0, 0}, {3894, 0, -1, 0}, {3791, 1, 0, 0}// 1	3	6	
					};

		[ Constructable ]
		public HalloweenDeco2()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 18076, 0, 0, 0, 0, -1, "Scarecrow", 1);// 2
			AddComplexComponent( (BaseAddon) this, 18069, 0, 1, 0, 0, 2, "Jack-O-Lantern", 1);// 4
			AddComplexComponent( (BaseAddon) this, 4456, 1, -1, 0, 0, -1, "Happy Halloween", 1);// 5
		}

		public HalloweenDeco2( Serial serial ) : base( serial )
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