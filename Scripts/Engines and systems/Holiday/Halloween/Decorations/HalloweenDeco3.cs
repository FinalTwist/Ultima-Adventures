using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HalloweenDeco3 : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {4677, 0, 0, 0}, {2448, 0, 1, 0}, {7393, 0, 1, 3}// 1	2	3	
			, {4650, 0, 1, 0}, {3894, -1, 0, 0}, {3894, 0, -1, 0}// 4	5	6	
			, {3894, -1, -1, 0}, {3180, -1, 0, 4}, {3180, 0, -1, 4}// 7	8	9	
					};

		[ Constructable ]
		public HalloweenDeco3()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 16638, 1, 0, 0, 0, 2, "Lantern", 1);// 10
		}

		public HalloweenDeco3( Serial serial ) : base( serial )
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