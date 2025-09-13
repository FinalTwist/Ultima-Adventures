using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DolphinSouthLargeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14581, -1, -3, 0}, {14588, -1, -2, 0}, {14592, -1, -1, 0}// 1	2	3	
			, {14582, 0, -3, 0}, {14585, 0, -2, 0}, {14589, 0, -1, 0}// 4	5	6	
			, {14583, 1, -3, 0}, {14586, 1, -2, 0}, {14590, 1, -1, 0}// 7	8	9	
			, {14584, 2, -3, 0}, {14587, 2, -2, 0}, {14591, 2, -1, 0}// 10	11	12	
			, {14596, -1, 0, 0}, {14600, -1, 1, 0}, {14604, -1, 2, 0}// 13	14	15	
			, {14608, -1, 3, 0}, {14593, 0, 0, 0}, {14597, 0, 1, 0}// 16	17	18	
			, {14601, 0, 2, 0}, {14605, 0, 3, 0}, {14594, 1, 0, 0}// 19	20	21	
			, {14598, 1, 1, 0}, {14602, 1, 2, 0}, {14606, 1, 3, 0}// 22	23	24	
			, {14595, 2, 0, 0}, {14599, 2, 1, 0}, {14603, 2, 2, 0}// 25	26	27	
			, {14607, 2, 3, 0}// 28	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new DolphinSouthLargeAddonDeed();
			}
		}

		[ Constructable ]
		public DolphinSouthLargeAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public DolphinSouthLargeAddon( Serial serial ) : base( serial )
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

	public class DolphinSouthLargeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DolphinSouthLargeAddon();
			}
		}

		[Constructable]
		public DolphinSouthLargeAddonDeed()
		{
			Name = "large dolphin rug (south)";
			ItemID = 0x44C3;
		}

		public DolphinSouthLargeAddonDeed( Serial serial ) : base( serial )
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