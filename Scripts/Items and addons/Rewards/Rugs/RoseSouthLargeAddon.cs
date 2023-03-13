using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RoseSouthLargeAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {14552, -1, -3, 0}, {14548, -1, -2, 0}, {14544, -1, -1, 0}// 1	2	3	
			, {14540, -1, 0, 0}, {14536, -1, 1, 0}, {14532, -1, 2, 0}// 4	5	6	
			, {14550, 0, -3, 0}, {14546, 0, -2, 0}, {14542, 0, -1, 0}// 7	8	9	
			, {14538, 0, 0, 0}, {14534, 0, 1, 0}, {14530, 0, 2, 0}// 10	11	12	
			, {14549, 1, -3, 0}, {14545, 1, -2, 0}, {14541, 1, -1, 0}// 13	14	15	
			, {14537, 1, 0, 0}, {14533, 1, 1, 0}, {14529, 1, 2, 0}// 16	17	18	
			, {14551, 2, -3, 0}, {14547, 2, -2, 0}, {14543, 2, -1, 0}// 19	20	21	
			, {14539, 2, 0, 0}, {14535, 2, 1, 0}, {14531, 2, 2, 0}// 22	23	24	
			, {14528, -1, 3, 0}, {14527, 0, 3, 0}, {14526, 1, 3, 0}// 25	26	27	
			, {14525, 2, 3, 0}// 28	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new RoseSouthLargeAddonDeed();
			}
		}

		[ Constructable ]
		public RoseSouthLargeAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public RoseSouthLargeAddon( Serial serial ) : base( serial )
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

	public class RoseSouthLargeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RoseSouthLargeAddon();
			}
		}

		[Constructable]
		public RoseSouthLargeAddonDeed()
		{
			Name = "large rose rug (south)";
			ItemID = 0x234D;
		}

		public RoseSouthLargeAddonDeed( Serial serial ) : base( serial )
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