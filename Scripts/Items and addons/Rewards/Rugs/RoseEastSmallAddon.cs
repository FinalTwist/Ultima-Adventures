using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RoseEastSmallAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18274, -2, -1, 0}, {18272, -2, 0, 0}, {18265, -1, -1, 0}// 1	2	3	
			, {18264, -1, 0, 0}, {18268, 0, -1, 0}, {18260, 0, 0, 0}// 4	5	6	
			, {18271, 1, -1, 0}, {18270, 1, 0, 0}, {18262, 2, -1, 0}// 7	8	9	
			, {18261, 2, 0, 0}, {18273, -2, 1, 0}, {18263, -1, 1, 0}// 10	11	12	
			, {18266, 0, 1, 0}, {18269, 1, 1, 0}, {18267, 2, 1, 0}// 13	14	15	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new RoseEastSmallAddonDeed();
			}
		}

		[ Constructable ]
		public RoseEastSmallAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public RoseEastSmallAddon( Serial serial ) : base( serial )
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

	public class RoseEastSmallAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RoseEastSmallAddon();
			}
		}

		[Constructable]
		public RoseEastSmallAddonDeed()
		{
			Name = "small rose rug (east)";
			ItemID = 0x234C;
		}

		public RoseEastSmallAddonDeed( Serial serial ) : base( serial )
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