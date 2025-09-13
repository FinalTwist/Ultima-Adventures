using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SkullSouthLargeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14495, -1, -3, 0}, {14494, 0, -3, 0}, {14493, 1, -3, 0}// 1	2	3	
			, {14496, 2, -3, 0}, {14491, -1, -2, 0}, {14487, -1, -1, 0}// 4	5	6	
			, {14483, -1, 0, 0}, {14479, -1, 1, 0}, {14475, -1, 2, 0}// 7	8	9	
			, {14472, -1, 3, 0}, {14490, 0, -2, 0}, {14486, 0, -1, 0}// 10	11	12	
			, {14482, 0, 0, 0}, {14478, 0, 1, 0}, {14474, 0, 2, 0}// 13	14	15	
			, {14471, 0, 3, 0}, {14489, 1, -2, 0}, {14485, 1, -1, 0}// 16	17	18	
			, {14481, 1, 0, 0}, {14477, 1, 1, 0}, {14473, 1, 2, 0}// 19	20	21	
			, {14470, 1, 3, 0}, {14492, 2, -2, 0}, {14488, 2, -1, 0}// 22	23	24	
			, {14484, 2, 0, 0}, {14480, 2, 1, 0}, {14476, 2, 2, 0}// 25	26	27	
			, {14469, 2, 3, 0}// 28	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new SkullSouthLargeAddonDeed();
			}
		}

		[ Constructable ]
		public SkullSouthLargeAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public SkullSouthLargeAddon( Serial serial ) : base( serial )
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

	public class SkullSouthLargeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SkullSouthLargeAddon();
			}
		}

		[Constructable]
		public SkullSouthLargeAddonDeed()
		{
			Name = "large skull rug (south)";
			ItemID = 0x1AE3;
		}

		public SkullSouthLargeAddonDeed( Serial serial ) : base( serial )
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