using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SkullEastLargeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14444, -3, -1, 0}, {14443, -3, 0, 0}, {14442, -3, 1, 0}// 1	2	3	
			, {14441, -3, 2, 0}, {14447, -2, -1, 0}, {14446, -2, 0, 0}// 4	5	6	
			, {14445, -2, 1, 0}, {14448, -2, 2, 0}, {14451, -1, -1, 0}// 7	8	9	
			, {14450, -1, 0, 0}, {14449, -1, 1, 0}, {14452, -1, 2, 0}// 10	11	12	
			, {14455, 0, -1, 0}, {14454, 0, 0, 0}, {14453, 0, 1, 0}// 13	14	15	
			, {14456, 0, 2, 0}, {14459, 1, -1, 0}, {14458, 1, 0, 0}// 16	17	18	
			, {14457, 1, 1, 0}, {14460, 1, 2, 0}, {14463, 2, -1, 0}// 19	20	21	
			, {14462, 2, 0, 0}, {14461, 2, 1, 0}, {14464, 2, 2, 0}// 22	23	24	
			, {14467, 3, -1, 0}, {14466, 3, 0, 0}, {14465, 3, 1, 0}// 25	26	27	
			, {14468, 3, 2, 0}// 28	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new SkullEastLargeAddonDeed();
			}
		}

		[ Constructable ]
		public SkullEastLargeAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public SkullEastLargeAddon( Serial serial ) : base( serial )
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

	public class SkullEastLargeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SkullEastLargeAddon();
			}
		}

		[Constructable]
		public SkullEastLargeAddonDeed()
		{
			Name = "large skull rug (east)";
			ItemID = 0x1AE2;
		}

		public SkullEastLargeAddonDeed( Serial serial ) : base( serial )
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