using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SkullSouthSmallAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18244, -1, -2, 0}, {18243, 0, -2, 0}, {18241, -1, -1, 0}// 1	2	3	
			, {18238, -1, 0, 0}, {18211, -1, 1, 0}, {18200, -1, 2, 0}// 4	5	6	
			, {18240, 0, -1, 0}, {18237, 0, 0, 0}, {18210, 0, 1, 0}// 7	8	9	
			, {18199, 0, 2, 0}, {18242, 1, -2, 0}, {18239, 1, -1, 0}// 10	11	12	
			, {18236, 1, 0, 0}, {18209, 1, 1, 0}, {18198, 1, 2, 0}// 13	14	15	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new SkullSouthSmallAddonDeed();
			}
		}

		[ Constructable ]
		public SkullSouthSmallAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public SkullSouthSmallAddon( Serial serial ) : base( serial )
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

	public class SkullSouthSmallAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SkullSouthSmallAddon();
			}
		}

		[Constructable]
		public SkullSouthSmallAddonDeed()
		{
			Name = "small skull rug (south)";
			ItemID = 0x1AE3;
		}

		public SkullSouthSmallAddonDeed( Serial serial ) : base( serial )
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