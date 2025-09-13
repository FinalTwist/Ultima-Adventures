using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RoseEastLargeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14500, -3, -1, 0}, {14499, -3, 0, 0}, {14498, -3, 1, 0}// 1	2	3	
			, {14497, -3, 2, 0}, {14504, -2, -1, 0}, {14502, -2, 0, 0}// 4	5	6	
			, {14501, -2, 1, 0}, {14503, -2, 2, 0}, {14508, -1, -1, 0}// 7	8	9	
			, {14506, -1, 0, 0}, {14505, -1, 1, 0}, {14507, -1, 2, 0}// 10	11	12	
			, {14512, 0, -1, 0}, {14510, 0, 0, 0}, {14509, 0, 1, 0}// 13	14	15	
			, {14511, 0, 2, 0}, {14516, 1, -1, 0}, {14514, 1, 0, 0}// 16	17	18	
			, {14513, 1, 1, 0}, {14515, 1, 2, 0}, {14520, 2, -1, 0}// 19	20	21	
			, {14518, 2, 0, 0}, {14517, 2, 1, 0}, {14519, 2, 2, 0}// 22	23	24	
			, {14524, 3, -1, 0}, {14522, 3, 0, 0}, {14521, 3, 1, 0}// 25	26	27	
			, {14523, 3, 2, 0}// 28	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new RoseEastLargeAddonDeed();
			}
		}

		[ Constructable ]
		public RoseEastLargeAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public RoseEastLargeAddon( Serial serial ) : base( serial )
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

	public class RoseEastLargeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RoseEastLargeAddon();
			}
		}

		[Constructable]
		public RoseEastLargeAddonDeed()
		{
			Name = "large rose rug (east)";
			ItemID = 0x234C;
		}

		public RoseEastLargeAddonDeed( Serial serial ) : base( serial )
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