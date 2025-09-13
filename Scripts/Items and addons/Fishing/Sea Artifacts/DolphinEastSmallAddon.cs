using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DolphinEastSmallAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18392, -2, -1, 0}, {18291, -2, 0, 0}, {18295, -1, -1, 0}// 1	2	3	
			, {18294, -1, 0, 0}, {18298, 0, -1, 0}, {18290, 0, 0, 0}// 4	5	6	
			, {18390, 1, -1, 0}, {18300, 1, 0, 0}, {18393, 2, -1, 0}// 7	8	9	
			, {18297, 2, 0, 0}, {18391, -2, 1, 0}, {18293, -1, 1, 0}// 10	11	12	
			, {18296, 0, 1, 0}, {18299, 1, 1, 0}, {18292, 2, 1, 0}// 13	14	15	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new DolphinEastSmallAddonDeed();
			}
		}

		[ Constructable ]
		public DolphinEastSmallAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public DolphinEastSmallAddon( Serial serial ) : base( serial )
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

	public class DolphinEastSmallAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DolphinEastSmallAddon();
			}
		}

		[Constructable]
		public DolphinEastSmallAddonDeed()
		{
			Name = "small dolphin run (east)";
			ItemID = 0x44C4;
		}

		public DolphinEastSmallAddonDeed( Serial serial ) : base( serial )
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