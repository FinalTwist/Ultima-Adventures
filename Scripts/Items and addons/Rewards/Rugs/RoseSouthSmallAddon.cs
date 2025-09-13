using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RoseSouthSmallAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18259, -1, -2, 0}, {18246, 0, -2, 0}, {18247, 1, -2, 0}// 1	2	3	
			, {18248, -1, -1, 0}, {18251, -1, 0, 0}, {18254, -1, 1, 0}// 4	5	6	
			, {18257, -1, 2, 0}, {18249, 0, -1, 0}, {18245, 0, 0, 0}// 7	8	9	
			, {18255, 0, 1, 0}, {18258, 0, 2, 0}, {18250, 1, -1, 0}// 10	11	12	
			, {18253, 1, 0, 0}, {18256, 1, 1, 0}, {18252, 1, 2, 0}// 13	14	15	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new RoseSouthSmallAddonDeed();
			}
		}

		[ Constructable ]
		public RoseSouthSmallAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public RoseSouthSmallAddon( Serial serial ) : base( serial )
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

	public class RoseSouthSmallAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RoseSouthSmallAddon();
			}
		}

		[Constructable]
		public RoseSouthSmallAddonDeed()
		{
			Name = "small rose rug (south)";
			ItemID = 0x234D;
		}

		public RoseSouthSmallAddonDeed( Serial serial ) : base( serial )
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