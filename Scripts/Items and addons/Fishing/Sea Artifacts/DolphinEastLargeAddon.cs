using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DolphinEastLargeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14556, -3, -1, 0}, {14555, -3, 0, 0}, {14554, -3, 1, 0}// 1	2	3	
			, {14553, -3, 2, 0}, {14559, -2, -1, 0}, {14558, -2, 0, 0}// 4	5	6	
			, {14557, -2, 1, 0}, {14560, -2, 2, 0}, {14563, -1, -1, 0}// 7	8	9	
			, {14562, -1, 0, 0}, {14561, -1, 1, 0}, {14564, -1, 2, 0}// 10	11	12	
			, {14567, 0, -1, 0}, {14566, 0, 0, 0}, {14565, 0, 1, 0}// 13	14	15	
			, {14568, 0, 2, 0}, {14571, 1, -1, 0}, {14570, 1, 0, 0}// 16	17	18	
			, {14569, 1, 1, 0}, {14572, 1, 2, 0}, {14575, 2, -1, 0}// 19	20	21	
			, {14574, 2, 0, 0}, {14573, 2, 1, 0}, {14576, 2, 2, 0}// 22	23	24	
			, {14579, 3, -1, 0}, {14578, 3, 0, 0}, {14577, 3, 1, 0}// 25	26	27	
			, {14580, 3, 2, 0}// 28	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new DolphinEastLargeAddonDeed();
			}
		}

		[ Constructable ]
		public DolphinEastLargeAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public DolphinEastLargeAddon( Serial serial ) : base( serial )
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

	public class DolphinEastLargeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DolphinEastLargeAddon();
			}
		}

		[Constructable]
		public DolphinEastLargeAddonDeed()
		{
			Name = "large dolphin rug (east)";
			ItemID = 0x44C4;
		}

		public DolphinEastLargeAddonDeed( Serial serial ) : base( serial )
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