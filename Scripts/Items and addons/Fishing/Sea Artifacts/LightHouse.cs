using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LightHouseAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {6843, -3, -3, 0}, {6844, -3, -2, 0}, {6845, -3, -1, 0}// 1	2	3	
			, {6842, -2, -3, 0}, {6862, -2, -2, 0}, {6861, -2, -1, 0}// 4	5	6	
			, {6846, -2, 0, 0}, {6849, -2, 1, 0}, {6841, -1, -3, 0}// 7	8	9	
			, {6863, -1, -2, 0}, {6859, -1, -1, 0}, {6858, -1, 0, 0}// 10	11	12	
			, {6855, -1, 1, 0}, {6852, -1, 2, 0}, {6820, -1, 3, 0}// 13	14	15	
			, {6838, 0, -2, 0}, {6860, 0, -1, 0}, {6821, 0, 3, 0}// 16	17	18	
			, {6835, 1, -2, 0}, {6832, 1, -1, 0}, {6822, 1, 3, 0}// 19	20	21	
			, {6829, 2, -1, 0}, {6823, 2, 3, 0}, {6828, 3, -1, 0}// 22	24	25	
			, {6827, 3, 0, 0}, {6826, 3, 1, 0}, {6825, 3, 2, 0}// 26	27	28	
			, {6824, 3, 3, 0}// 29	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new LightHouseAddonDeed();
			}
		}

		[ Constructable ]
		public LightHouseAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 6864, 2, 2, 0, 0, 2, "lighthouse", 1);// 23
		}

		public LightHouseAddon( Serial serial ) : base( serial )
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
                ac.Light = LightType.Circle300;

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

	public class LightHouseAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new LightHouseAddon();
			}
		}

		[Constructable]
		public LightHouseAddonDeed()
		{
			Name = "lighthouse";
			ItemID = 0xA18;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "To Be Built In A Home");
        } 

		public LightHouseAddonDeed( Serial serial ) : base( serial )
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