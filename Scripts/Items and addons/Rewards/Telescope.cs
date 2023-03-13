using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TelescopeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {5268, -5, 0, 0}, {5211, -5, 1, 0}, {5210, -5, 2, 0}// 1	2	3	
			, {5269, -4, -1, 0}, {5212, -4, 2, 0}, {5270, -3, -2, 0}// 4	5	6	
			, {5273, -3, -1, 0}, {5262, -3, 1, 0}, {5267, -3, 2, 0}// 7	8	9	
			, {5271, -2, -3, 0}, {5274, -1, -4, 0}, {5272, -1, -3, 0}// 10	11	12	
			, {5263, -1, -1, 0}, {5261, -1, 1, 0}, {5245, 0, -5, 0}// 13	14	15	
			, {5264, 0, -1, 0}, {5259, 0, 0, 0}, {5258, 0, 1, 0}// 16	17	18	
			, {5254, 0, 2, 0}, {5244, 1, -5, 0}, {5265, 1, -1, 0}// 19	20	21	
			, {5260, 1, 0, 0}, {5257, 1, 1, 0}, {5255, 1, 2, 0}// 22	23	24	
			, {5243, 2, -5, 0}, {5247, 2, -2, 0}, {5248, 2, -1, 0}// 25	26	27	
			, {5250, 2, 0, 0}, {5225, 2, 1, 0}, {5224, 2, 2, 0}// 28	29	30	
			, {5242, 3, -5, 0}, {5241, 3, -4, 0}, {5239, 3, -3, 0}// 31	32	33	
			, {5246, 3, -2, 0}, {5249, 3, -1, 0}, {5251, 3, 0, 0}// 34	35	36	
			, {5226, 3, 1, 0}, {5223, 3, 2, 0}, {5240, 4, -4, 0}// 37	38	39	
			, {5237, 4, -3, 0}, {5236, 4, -2, 0}, {5231, 4, -1, 0}// 40	41	42	
			, {5230, 4, 0, 0}, {5229, 4, 1, 0}, {5227, 4, 2, 0}// 43	44	45	
			, {5238, 5, -3, 0}, {5235, 5, -2, 0}, {5232, 5, -1, 0}// 46	47	48	
			, {5233, 5, 0, 0}, {5234, 5, 1, 0}, {5213, -4, 3, 0}// 49	50	51	
			, {5266, -3, 3, 0}, {5214, -3, 4, 0}, {5209, -3, 5, 0}// 52	53	54	
			, {5215, -2, 4, 0}, {5217, -2, 5, 0}, {5256, -1, 3, 0}// 55	56	57	
			, {5216, -1, 4, 0}, {5218, -1, 5, 0}, {5253, 0, 3, 0}// 58	59	60	
			, {5252, 1, 3, 0}, {5219, 1, 5, 0}, {5221, 2, 3, 0}// 61	62	63	
			, {5220, 2, 4, 0}, {5222, 3, 3, 0}// 64	65	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new TelescopeAddonDeed();
			}
		}

		[ Constructable ]
		public TelescopeAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public TelescopeAddon( Serial serial ) : base( serial )
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

	public class TelescopeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new TelescopeAddon();
			}
		}

		[Constructable]
		public TelescopeAddonDeed()
		{
			Name = "telescope";
			ItemID = 0x14F6;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "To Be Built In A Home");
        } 

		public TelescopeAddonDeed( Serial serial ) : base( serial )
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