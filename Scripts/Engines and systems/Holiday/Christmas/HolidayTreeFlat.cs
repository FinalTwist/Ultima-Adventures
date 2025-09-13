using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HolidayTreeFlat : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponentsA = new int[,] {
			  {11868, -3, -3, 13}, {11870, -3, -2, 13}, {11869, -2, -3, 13}// 1	2	3	
			, {11871, -2, -2, 13}, {11875, -2, -1, 13}, {11996, -2, 0, 13}// 4	5	6	
			, {11872, -1, -2, 13}, {11876, -1, -1, 13}, {11997, -1, 0, 13}// 7	8	9	
			, {12048, -1, 1, 13}, {12089, -1, 2, 13}, {11873, 0, -2, 13}// 10	11	12	
			, {11874, 0, -1, 13}, {11877, 0, -1, 13}, {11998, 0, 0, 13}// 13	14	15	
			, {12049, 0, 1, 13}, {12090, 0, 2, 13}, {11878, 1, -1, 13}// 16	17	18	
			, {11999, 1, 0, 13}, {12050, 1, 1, 13}, {12091, 1, 2, 13}// 19	20	21	
			, {11879, 2, -1, 13}, {12000, 2, 0, 13}, {12087, 2, 1, 13}// 22	23	24	
			, {12092, 2, 2, 13}, {11880, 3, -1, 13}, {12047, 3, 0, 13}// 25	26	27	
			, {12088, 3, 1, 13}, {12093, 3, 2, 13}, {12094, -1, 3, 13}// 28	29	30	
			, {12095, 0, 3, 13}, {12096, 1, 3, 13}, {12097, 2, 3, 13}// 31	32	33	
			, {12098, 3, 3, 13}// 34	
		};

        private static int[,] m_AddOnSimpleComponentsB = new int[,] {
			  {11868, -3, -3, 13}, {11870, -3, -2, 13}, {11869, -2, -3, 13}// 1	2	3	
			, {11871, -2, -2, 13}, {11875, -2, -1, 13}, {11996, -2, 0, 13}// 4	5	6	
			, {11872, -1, -2, 13}, {11876, -1, -1, 13}, {11997, -1, 0, 13}// 7	8	9	
			, {12048, -1, 1, 13}, {12089, -1, 2, 13}, {11873, 0, -2, 13}// 10	11	12	
			, {11874, 0, -1, 13}, {11877, 0, -1, 13}, {11998, 0, 0, 13}// 13	14	15	
			, {12049, 0, 1, 13}, {12090, 0, 2, 13}, {11878, 1, -1, 13}// 16	17	18	
			, {11999, 1, 0, 13}, {12050, 1, 1, 13}, {12091, 1, 2, 13}// 19	20	21	
			, {11879, 2, -1, 13}, {12000, 2, 0, 13}, {12087, 2, 1, 13}// 22	23	24	
			, {12092, 2, 2, 13}, {11880, 3, -1, 13}, {12047, 3, 0, 13}// 25	26	27	
			, {12088, 3, 1, 13}, {12093, 3, 2, 13}, {12094, -1, 3, 13}// 28	29	30	
			, {12095, 0, 3, 13}, {12096, 1, 3, 13}, {12097, 2, 3, 13}// 31	32	33	
			, {12098, 3, 3, 13}// 34	
		};

		public override BaseAddonDeed Deed
		{
			get
			{
				return new HolidayTreeFlatDeed();
			}
		}

		[ Constructable ]
		public HolidayTreeFlat()
		{
			if ( Utility.RandomMinMax( 1, 2 ) == 1 )
			{
				for (int i = 0; i < m_AddOnSimpleComponentsA.Length / 4; i++)
					AddComponent( new AddonComponent( m_AddOnSimpleComponentsA[i,0] ), m_AddOnSimpleComponentsA[i,1], m_AddOnSimpleComponentsA[i,2], m_AddOnSimpleComponentsA[i,3] );
			}
			else
			{
				for (int i = 0; i < m_AddOnSimpleComponentsB.Length / 4; i++)
					AddComponent( new AddonComponent( m_AddOnSimpleComponentsB[i,0] ), m_AddOnSimpleComponentsB[i,1], m_AddOnSimpleComponentsB[i,2], m_AddOnSimpleComponentsB[i,3] );
			}
		}

		public HolidayTreeFlat( Serial serial ) : base( serial )
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

	public class HolidayTreeFlatDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new HolidayTreeFlat();
			}
		}

		[Constructable]
		public HolidayTreeFlatDeed()
		{
			Name = "holiday tree";
			ItemID = 0x0CC8;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
			list.Add( 1070722, "For Low Ceilings");
        }

		public HolidayTreeFlatDeed( Serial serial ) : base( serial )
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