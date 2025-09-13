using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class VirtueAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {16602, -2, -2, 0}, {16608, -2, -1, 0}, {16603, -1, -2, 0}// 1	2	3	
			, {16609, -1, -1, 0}, {16604, 0, -2, 0}, {16610, 0, -1, 0}// 4	5	6	
			, {16605, 1, -2, 0}, {16611, 1, -1, 0}, {16614, -2, 0, 0}// 7	8	9	
			, {16620, -2, 1, 0}, {16626, -2, 2, 0}, {16632, -2, 3, 0}// 10	11	12	
			, {16615, -1, 0, 0}, {16621, -1, 1, 0}, {16627, -1, 2, 0}// 13	14	15	
			, {16633, -1, 3, 0}, {16616, 0, 0, 0}, {16622, 0, 1, 0}// 16	17	18	
			, {16628, 0, 2, 0}, {16634, 0, 3, 0}, {16617, 1, 0, 0}// 19	20	21	
			, {16623, 1, 1, 0}, {16629, 1, 2, 0}, {16635, 1, 3, 0}// 22	23	24	
			, {16606, 2, -2, 0}, {16612, 2, -1, 0}, {16607, 3, -2, 0}// 25	26	27	
			, {16613, 3, -1, 0}, {16618, 2, 0, 0}, {16624, 2, 1, 0}// 28	29	30	
			, {16630, 2, 2, 0}, {16636, 2, 3, 0}, {16619, 3, 0, 0}// 31	32	33	
			, {16625, 3, 1, 0}, {16631, 3, 2, 0}, {16637, 3, 3, 0}// 34	35	36	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new VirtueAddonDeed();
			}
		}

		[ Constructable ]
		public VirtueAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public VirtueAddon( Serial serial ) : base( serial )
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

	public class VirtueAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new VirtueAddon();
			}
		}

		[Constructable]
		public VirtueAddonDeed()
		{
			ItemID = 0x573C;
			Name = "rune of virtue";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Places a Virtue Symbol on the Floor of a Home");
        } 

		public VirtueAddonDeed( Serial serial ) : base( serial )
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