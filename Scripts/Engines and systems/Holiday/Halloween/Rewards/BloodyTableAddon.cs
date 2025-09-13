using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BloodyTableAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {7379, 1, 2, 0}, {7379, 0, 2, 0}, {3790, 1, -1, 4}// 16	43	44	
			, {7385, 0, -1, 0}, {7385, 0, 0, 0}, {7385, 0, 1, 0}// 45	46	47	
			, {7385, 1, -1, 0}, {7385, 1, 0, 0}, {7385, 1, 1, 0}// 48	49	50	
			, {7374, 2, 1, 0}, {7374, 3, 1, 0}, {7373, -2, 2, 0}// 51	52	53	
			, {7376, 3, -2, 0}, {7385, 2, -1, 0}, {7385, 2, 0, 0}// 54	55	56	
			, {7371, 2, 1, 0}, {7385, -1, -1, 0}, {7385, -1, 0, 0}// 57	58	59	
			, {7385, -1, 1, 0}, {7380, -1, 2, 0}, {7377, -2, 1, 0}// 60	61	62	
			, {7374, 2, 2, 0}, {7383, 3, 0, 0}, {7383, 3, -1, 0}// 63	64	65	
			, {7381, 2, -2, 0}, {7381, 1, -2, 0}, {7381, -1, -2, 0}// 66	67	68	
			, {7586, 0, 1, 6}, {7600, 1, -1, 6}, {6924, 1, 1, 5}// 69	70	72	
			, {6927, 0, 0, 5}// 73	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new BloodyTableAddonDeed();
			}
		}

		[ Constructable ]
		public BloodyTableAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 2756, -2, 2, 0, 1157, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 2806, -2, 0, 0, 1157, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 2806, -2, 1, 0, 1157, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 2755, -2, -2, 0, 1157, -1, "", 1);// 4
			AddComplexComponent( (BaseAddon) this, 4611, 0, 0, 0, 1105, -1, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 4609, 0, 1, 0, 1105, -1, "", 1);// 6
			AddComplexComponent( (BaseAddon) this, 4610, 1, -1, 0, 1105, -1, "", 1);// 7
			AddComplexComponent( (BaseAddon) this, 4611, 1, 0, 0, 1105, -1, "", 1);// 8
			AddComplexComponent( (BaseAddon) this, 4609, 1, 1, 0, 1105, -1, "", 1);// 9
			AddComplexComponent( (BaseAddon) this, 4635, 2, 1, 0, 1105, -1, "", 1);// 10
			AddComplexComponent( (BaseAddon) this, 4635, 2, 0, 0, 1105, -1, "", 1);// 11
			AddComplexComponent( (BaseAddon) this, 4635, 2, -1, 0, 1105, -1, "", 1);// 12
			AddComplexComponent( (BaseAddon) this, 4633, -1, 1, 0, 1105, -1, "", 1);// 13
			AddComplexComponent( (BaseAddon) this, 4633, -1, 0, 0, 1105, -1, "", 1);// 14
			AddComplexComponent( (BaseAddon) this, 4633, -1, -1, 0, 1105, -1, "", 1);// 15
			AddComplexComponent( (BaseAddon) this, 2807, -1, -2, 0, 1157, -1, "", 1);// 17
			AddComplexComponent( (BaseAddon) this, 2749, -1, -1, 0, 1157, -1, "", 1);// 18
			AddComplexComponent( (BaseAddon) this, 2749, -1, 0, 0, 1157, -1, "", 1);// 19
			AddComplexComponent( (BaseAddon) this, 2749, -1, 1, 0, 1157, -1, "", 1);// 20
			AddComplexComponent( (BaseAddon) this, 2809, -1, 2, 0, 1157, -1, "", 1);// 21
			AddComplexComponent( (BaseAddon) this, 2807, 0, -2, 0, 1157, -1, "", 1);// 22
			AddComplexComponent( (BaseAddon) this, 2749, 0, -1, 0, 1157, -1, "", 1);// 23
			AddComplexComponent( (BaseAddon) this, 2749, 0, 0, 0, 1157, -1, "", 1);// 24
			AddComplexComponent( (BaseAddon) this, 2749, 0, 1, 0, 1157, -1, "", 1);// 25
			AddComplexComponent( (BaseAddon) this, 2809, 0, 2, 0, 1157, -1, "", 1);// 26
			AddComplexComponent( (BaseAddon) this, 2807, 1, -2, 0, 1157, -1, "", 1);// 27
			AddComplexComponent( (BaseAddon) this, 2749, 1, -1, 0, 1157, -1, "", 1);// 28
			AddComplexComponent( (BaseAddon) this, 2749, 1, 0, 0, 1157, -1, "", 1);// 29
			AddComplexComponent( (BaseAddon) this, 2749, 1, 1, 0, 1157, -1, "", 1);// 30
			AddComplexComponent( (BaseAddon) this, 2809, 1, 2, 0, 1157, -1, "", 1);// 31
			AddComplexComponent( (BaseAddon) this, 2807, 2, -2, 0, 1157, -1, "", 1);// 32
			AddComplexComponent( (BaseAddon) this, 2749, 2, -1, 0, 1157, -1, "", 1);// 33
			AddComplexComponent( (BaseAddon) this, 2806, -2, -1, 0, 1157, -1, "", 1);// 34
			AddComplexComponent( (BaseAddon) this, 2749, 2, 0, 0, 1157, -1, "", 1);// 35
			AddComplexComponent( (BaseAddon) this, 2749, 2, 1, 0, 1157, -1, "", 1);// 36
			AddComplexComponent( (BaseAddon) this, 2809, 2, 2, 0, 1157, -1, "", 1);// 37
			AddComplexComponent( (BaseAddon) this, 2757, 3, -2, 0, 1157, -1, "", 1);// 38
			AddComplexComponent( (BaseAddon) this, 2808, 3, -1, 0, 1157, -1, "", 1);// 39
			AddComplexComponent( (BaseAddon) this, 2808, 3, 0, 0, 1157, -1, "", 1);// 40
			AddComplexComponent( (BaseAddon) this, 2808, 3, 1, 0, 1157, -1, "", 1);// 41
			AddComplexComponent( (BaseAddon) this, 2754, 3, 2, 0, 1157, -1, "", 1);// 42
			AddComplexComponent( (BaseAddon) this, 4610, 0, -1, 0, 1105, -1, "", 1);// 71

		}

		public BloodyTableAddon( Serial serial ) : base( serial )
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

	public class BloodyTableAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BloodyTableAddon();
			}
		}

		[Constructable]
		public BloodyTableAddonDeed()
		{
			Name = "BloodyTable";
			Hue = 0x96C;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "Happy Halloween" );
		}

		public BloodyTableAddonDeed( Serial serial ) : base( serial )
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