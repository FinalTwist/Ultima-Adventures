using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SkullEastSmallAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {18185, -2, -1, 0}, {18184, -2, 0, 0}, {18196, -2, 1, 0}// 1	2	3	
			, {18188, -1, -1, 0}, {18187, -1, 0, 0}, {18195, -1, 1, 0}// 4	5	6	
			, {18191, 0, -1, 0}, {18183, 0, 0, 0}, {18193, 0, 1, 0}// 7	8	9	
			, {18189, 1, -1, 0}, {18192, 1, 0, 0}, {18197, 1, 1, 0}// 10	11	12	
			, {18190, 2, -1, 0}, {18186, 2, 0, 0}, {18194, 2, 1, 0}// 13	14	15	
					};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new SkullEastSmallAddonDeed();
			}
		}

		[ Constructable ]
		public SkullEastSmallAddon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public SkullEastSmallAddon( Serial serial ) : base( serial )
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

	public class SkullEastSmallAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SkullEastSmallAddon();
			}
		}

		[Constructable]
		public SkullEastSmallAddonDeed()
		{
			Name = "small skull rug (east)";
			ItemID = 0x1AE2;
		}

		public SkullEastSmallAddonDeed( Serial serial ) : base( serial )
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