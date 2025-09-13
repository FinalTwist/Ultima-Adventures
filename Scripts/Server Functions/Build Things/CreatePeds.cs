using System;
using Server;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Items;

namespace Server.Misc
{
    class BuildSteadPeds
    {
		public static Item ChooseType()
		{
			Item item = null;

			item = new StealBase();

			return item;
		}

		public static void CreateStealPeds()
		{
			Item stealPedestal = new StealBase(); stealPedestal.Delete();

			ArrayList SBtargets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( ( item is StealBase ) || ( item is StealBaseEmpty ) )
			{
				SBtargets.Add( item );
			}
			for ( int i = 0; i < SBtargets.Count; ++i )
			{
				Item item = ( Item )SBtargets[ i ];
				item.Delete();
			}

			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5200, 775, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5904, 96, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5984, 184, 44), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5700, 83, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5668, 319, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5321, 741, -20), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5688, 1347, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5579, 1848, 5), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5161, 849, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5236, 135, 15), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5288, 623, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5686, 521, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5566, 820, 45), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5553, 82, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5400, 1404, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5338, 1538, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5389, 2021, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5912, 3447, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6147, 2675, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6409, 1497, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6443, 1496, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6449, 311, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6367, 104, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6601, 213, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6559, 373, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6199, 484, 59), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5272, 387, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5443, 413, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6437, 2326, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6234, 2357, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6393, 2322, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6112, 2442, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5173, 2618, 21), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5790, 2532, -24), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6115, 1018, 5), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6280, 3825, -5), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6039, 3877, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6805, 1674, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7031, 1651, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6782, 1559, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7049, 1442, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5942, 1964, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6300, 1163, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6596, 2065, -30), Map.Felucca);

			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6186, 543, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6260, 56, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6270, 204, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(3806, 3392, 20), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5595, 2185, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5610, 398, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(3781, 1847, 22), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5262, 347, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5608, 1456, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5239, 1381, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(4232, 3289, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5234, 2934, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5418, 2957, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(3503, 2285, 27), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(244, 3486, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5225, 541, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5302, 897, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(520, 192, 27), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5584, 796, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5943 ,387, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5683 ,3253, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6483, 2889, 50), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6890, 2882, 55), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6777, 194, 30), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(4382, 3984, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(4799, 3805, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6951, 3878, 25), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(4969, 3521, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(550, 3829, 78), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6801, 3184, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6819, 3171, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6875, 3090, 5), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6478, 3877, 10), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6504, 3363, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6504, 3450, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6346, 3604, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6817, 3606, 5), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6948, 3615, -9), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6943, 3485, 20), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6801, 3807, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6313, 3861, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6024, 1717, 5), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5667, 1025, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5904, 1761, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5377, 2015, 0), Map.Trammel);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5245, 2023, 0), Map.Trammel);

			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1970, 85, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2305, 66, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2179, 221, 2), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2074, 489, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2214, 832, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2467, 872, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2457, 486, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1928, 567, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2339, 855, 2), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2338, 494, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1952, 812, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2071, 193, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2209, 392, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2056, 903, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2471, 182, 0), Map.Malas);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1586, 251, 0), Map.Malas);

			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(80, 2721, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(239, 2586, -19), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(97, 2404, -22), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(632, 2259, -32), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(872, 2158, -68), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(456, 2787, 22), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(691, 2583, -23), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(248, 1369, 0), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(440, 2896, -22), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(461, 2238, 9), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(768, 321, 46), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(477, 1927, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(756, 2004, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(602, 2678, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1082, 2417, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1133, 2427, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(38, 1925, -28), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(572, 3655, 21), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(692, 3626, 20), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(364, 3923, 0), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(360, 3875, 0), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(532, 3368, 0), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(566, 3323, 0), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(672, 3941, -5), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(728, 3392, 20), Map.TerMur);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(256, 3662, 50), Map.TerMur);

			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(2116, 1230, 20), Map.Ilshenar);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1694, 1490, 10), Map.Ilshenar);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1670, 1178, -32), Map.Ilshenar);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(1892, 1355, -42), Map.Ilshenar);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6371, 2076, 15), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6172, 2063, -16), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6402, 671, 20), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6812, 696, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7003, 2420, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6985, 2490, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7111, 2506, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7023, 2365, 2), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6740, 2271, 10), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6835, 2332, 5), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6330, 1009, 11), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7053, 2095, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6986, 1912, 15), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(7031, 707, 35), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6945, 1182, -109), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5219, 2326, -15), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5206, 1923, 25), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6034, 1671, -48), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5919, 1792, -5), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6027, 451, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(5997, 530, 0), Map.Felucca);
			stealPedestal = ChooseType();			stealPedestal.MoveToWorld (new Point3D(6073, 644, 0), Map.Felucca);

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			EssenceBase essPedestal = new EssenceBase( "drow" ); essPedestal.Delete();

			ArrayList EStargets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( ( item is EssenceBase ) || ( item is EssenceBaseEmpty ) )
			{
				EStargets.Add( item );
			}
			for ( int i = 0; i < EStargets.Count; ++i )
			{
				Item item = ( Item )EStargets[ i ];
				item.Delete();
			}

			essPedestal = new EssenceBase( "tritun" );		essPedestal.MoveToWorld (new Point3D(920, 3259, 40), Map.TerMur);
			essPedestal = new EssenceBase( "ork" );			essPedestal.MoveToWorld (new Point3D(1044, 2425, -28), Map.TerMur);
			essPedestal = new EssenceBase( "ork" );			essPedestal.MoveToWorld (new Point3D(248, 1946, -28), Map.TerMur);
			essPedestal = new EssenceBase( "drow" );		essPedestal.MoveToWorld (new Point3D(5432, 1348, 0), Map.Felucca);
			essPedestal = new EssenceBase( "vampire" );		essPedestal.MoveToWorld (new Point3D(5774, 2746, 5), Map.Felucca);
			essPedestal = new EssenceBase( "ghost" );		essPedestal.MoveToWorld (new Point3D(6500, 649, 0), Map.Trammel);
			essPedestal = new EssenceBase( "demon" );		essPedestal.MoveToWorld (new Point3D(6121, 208, 22), Map.Felucca);
			essPedestal = new EssenceBase( "ice" );			essPedestal.MoveToWorld (new Point3D(6432, 526, 0), Map.Felucca);
			essPedestal = new EssenceBase( "fire" );		essPedestal.MoveToWorld (new Point3D(6251, 2482, 0), Map.Felucca);
			essPedestal = new EssenceBase( "shadow" );		essPedestal.MoveToWorld (new Point3D(5042, 3517, 0), Map.Trammel);
			essPedestal = new EssenceBase( "dark" );		essPedestal.MoveToWorld (new Point3D(411, 2124, -1), Map.TerMur);
			essPedestal = new EssenceBase( "lizard" );		essPedestal.MoveToWorld (new Point3D(6223, 1341, 0), Map.Felucca);
			essPedestal = new EssenceBase( "darkness" );	essPedestal.MoveToWorld (new Point3D(6788, 2340, 0), Map.Felucca);

			/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

			ArrayList FMtargets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is RunesBase || item is RunesBaseEmpty || item is FlamesBase || item is FlamesBaseEmpty || item is BaneBase || item is BaneBaseEmpty || item is PaganBase || item is PaganBaseEmpty )
			{
				FMtargets.Add( item );
			}
			for ( int i = 0; i < FMtargets.Count; ++i )
			{
				Item item = ( Item )FMtargets[ i ];
				item.Delete();
			}

			int most = 79;
			int choice = 0;

			string KeepTrack = "_";

			choice = Utility.RandomMinMax( 1, most ); KeepTrack = KeepTrack + choice.ToString() + "_";
				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 1, "shadowlord" ); // BOOK OF TRUTH

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 2, "shadowlord" ); // BELL OF COURAGE

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 3, "shadowlord" ); // CANDLE OF LOVE

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 1, "serpent" ); // SCALES OF ETHICALITY

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 2, "serpent" ); // ORB OF LOGIC

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 3, "serpent" ); // LANTERN OF DISCIPLINE

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 1, "pagan" ); // BREATH OF AIR

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 2, "pagan" ); // TONGUE OF FLAME

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 3, "pagan" ); // HEART OF EARTH

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 4, "pagan" ); // TEAR OF THE SEAS

				while ( UsedNumberCheck( KeepTrack, choice ) == true ){ choice = Utility.RandomMinMax( 1, most ); }
					KeepTrack = KeepTrack + choice.ToString() + "_"; 
					CreateSpecialPedestal( choice, 0, "runes" ); // VIRTUE CHEST
		}

		public static bool UsedNumberCheck( string info, int numb )
		{
			string number = "_" + numb.ToString() + "_";
			if ( info.Contains(number) ){ return true; }
			return false;
		}

		public static void CreateSpecialPedestal( int choice, int ped, string category )
		{
			Item specialPed = new FlamesBase( "love" ); specialPed.Delete();

			if ( category == "shadowlord" )
			{
				if ( ped == 1 ){ specialPed = new FlamesBase( "truth" ); }
				else if ( ped == 2 ){ specialPed = new FlamesBase( "courage" ); }
				else { specialPed = new FlamesBase( "love" ); }
			}
			else if ( category == "serpent" )
			{
				if ( ped == 1 ){ specialPed = new BaneBase( "ethicality" ); }
				else if ( ped == 2 ){ specialPed = new BaneBase( "logic" ); }
				else { specialPed = new BaneBase( "discipline" ); }
			}
			else if ( category == "pagan" )
			{
				if ( ped == 1 ){ specialPed = new PaganBase( "air" ); }
				else if ( ped == 2 ){ specialPed = new PaganBase( "fire" ); }
				else if ( ped == 3 ){ specialPed = new PaganBase( "earth" ); }
				else { specialPed = new PaganBase( "water" ); }
			}
			else if ( category == "runes" )
			{
				specialPed = new RunesBase();
			}

			switch ( choice )
			{
				case 1 : specialPed.MoveToWorld (new Point3D(6295, 387, 5), Map.Felucca); break; // the Vault of the Black Knight
				case 2 : specialPed.MoveToWorld (new Point3D(6441, 3808, 10), Map.Felucca); break; // the Undersea Pass
				case 3 : specialPed.MoveToWorld (new Point3D(5820, 2785, 5), Map.Felucca); break; // the Crypts of Dracula
				case 4 : specialPed.MoveToWorld (new Point3D(5596, 1874, 0), Map.Felucca); break; // the Lodoria Catacombs
				case 5 : specialPed.MoveToWorld (new Point3D(5275, 675, 5), Map.Felucca); break; // Dungeon Deceit
				case 6 : specialPed.MoveToWorld (new Point3D(5443, 2430, 40), Map.Felucca); break; // Dungeon Despise
				case 7 : specialPed.MoveToWorld (new Point3D(5149, 843, 0), Map.Felucca); break; // Dungeon Destard
				case 8 : specialPed.MoveToWorld (new Point3D(5826, 1418, 0), Map.Felucca); break; // the City of Embers
				case 9 : specialPed.MoveToWorld (new Point3D(6085, 69, 27), Map.Felucca); break; // Dungeon Hythloth
				case 10 : specialPed.MoveToWorld (new Point3D(5672, 311, 0), Map.Felucca); break; // the Ice Fiend Lair
				case 11 : specialPed.MoveToWorld (new Point3D(5208, 1593, 0), Map.Felucca); break; // Terathan Keep
				case 12 : specialPed.MoveToWorld (new Point3D(5378, 409, 0), Map.Felucca); break; // the Halls of Undermountain
				case 13 : specialPed.MoveToWorld (new Point3D(5871, 3438, 0), Map.Felucca); break; // the Volcanic Cave
				case 14 : specialPed.MoveToWorld (new Point3D(5527, 1352, 0), Map.Felucca); break; // Dungeon Wrong
				case 15 : specialPed.MoveToWorld (new Point3D(6152, 2872, 0), Map.Felucca); break; // Stonegate Castle

				case 16 : specialPed.MoveToWorld (new Point3D(6897, 2874, 50), Map.Trammel); break; // Vordo's Castle
				case 17 : specialPed.MoveToWorld (new Point3D(3882, 3281, 40), Map.Trammel); break; // the Mausoleum
				case 18 : specialPed.MoveToWorld (new Point3D(495, 3811, 78), Map.Trammel); break; // the Tower of Brass
				case 19 : specialPed.MoveToWorld (new Point3D(4734, 3682, 0), Map.Trammel); break; // the Dragon's Maw
				case 20 : specialPed.MoveToWorld (new Point3D(6966, 3848, 25), Map.Trammel); break; // the Cave of the Zuluu
				case 21 : specialPed.MoveToWorld (new Point3D(5333, 895, 0), Map.Trammel); break; // the Ancient Pyramid
				case 22 : specialPed.MoveToWorld (new Point3D(5939, 654, 0), Map.Trammel); break; // Dungeon Exodus
				case 23 : specialPed.MoveToWorld (new Point3D(5843, 1752, 0), Map.Trammel); break; // the Caverns of Poseidon
				case 24 : specialPed.MoveToWorld (new Point3D(5620, 2172, 0), Map.Trammel); break; // Dungeon Clues
				case 25 : specialPed.MoveToWorld (new Point3D(5622, 367, 0), Map.Trammel); break; // Dardin's Pit
				case 26 : specialPed.MoveToWorld (new Point3D(5242, 219, 0), Map.Trammel); break; // Dungeon Doom
				case 27 : specialPed.MoveToWorld (new Point3D(5528, 1246, 0), Map.Trammel); break; // the Fires of Hell
				case 28 : specialPed.MoveToWorld (new Point3D(5636, 1513, 0), Map.Trammel); break; // the Mines of Morinia
				case 29 : specialPed.MoveToWorld (new Point3D(5915, 462, 0), Map.Trammel); break; // the Perinian Depths
				case 30 : specialPed.MoveToWorld (new Point3D(5506, 818, 0), Map.Trammel); break; // the Dungeon of Time Awaits

				case 31 : specialPed.MoveToWorld (new Point3D(1961, 562, 0), Map.Malas); break; // the Ancient Prison
				case 32 : specialPed.MoveToWorld (new Point3D(2134, 873, 0), Map.Malas); break; // the Cave of Fire
				case 33 : specialPed.MoveToWorld (new Point3D(2449, 168, 0), Map.Malas); break; // the Cave of Souls
				case 34 : specialPed.MoveToWorld (new Point3D(2085, 216, 0), Map.Malas); break; // Dungeon Ankh
				case 35 : specialPed.MoveToWorld (new Point3D(1968, 180, 0), Map.Malas); break; // Dungeon Bane
				case 36 : specialPed.MoveToWorld (new Point3D(2171, 520, 2), Map.Malas); break; // Dungeon Hate
				case 37 : specialPed.MoveToWorld (new Point3D(2202, 832, 0), Map.Malas); break; // Dungeon Scorn
				case 38 : specialPed.MoveToWorld (new Point3D(1935, 813, 0), Map.Malas); break; // Dungeon Torment
				case 39 : specialPed.MoveToWorld (new Point3D(2329, 477, 0), Map.Malas); break; // Dungeon Vile
				case 40 : specialPed.MoveToWorld (new Point3D(2205, 165, 2), Map.Malas); break; // Dungeon Wicked
				case 41 : specialPed.MoveToWorld (new Point3D(2315, 893, 2), Map.Malas); break; // Dungeon Wrath
				case 42 : specialPed.MoveToWorld (new Point3D(2473, 838, 0), Map.Malas); break; // the Flooded Temple
				case 43 : specialPed.MoveToWorld (new Point3D(2083, 542, 0), Map.Malas); break; // the Gargoyle Crypts
				case 44 : specialPed.MoveToWorld (new Point3D(2457, 471, 0), Map.Malas); break; // the Serpent Sanctum
				case 45 : specialPed.MoveToWorld (new Point3D(2313, 168, 2), Map.Malas); break; // the Tomb of the Fallen Wizard

				case 46 : specialPed.MoveToWorld (new Point3D(729, 2626, -28), Map.TerMur); break; // the Blood Temple
				case 47 : specialPed.MoveToWorld (new Point3D(747, 1978, -28), Map.TerMur); break; // the Dungeon of the Mad Archmage
				case 48 : specialPed.MoveToWorld (new Point3D(24, 2708, -28), Map.TerMur); break; // the Tombs
				case 49 : specialPed.MoveToWorld (new Point3D(503, 2318, -1), Map.TerMur); break; // the Dungeon of the Lich King
				case 50 : specialPed.MoveToWorld (new Point3D(47, 3252, 20), Map.TerMur); break; // the Forgotten Halls
				case 51 : specialPed.MoveToWorld (new Point3D(424, 2827, 22), Map.TerMur); break; // the Ice Queen Fortress
				case 52 : specialPed.MoveToWorld (new Point3D(937, 2336, -28), Map.TerMur); break; // the Halls of Ogrimar
				case 53 : specialPed.MoveToWorld (new Point3D(662, 2208, -27), Map.TerMur); break; // Dungeon Rock
				case 54 : specialPed.MoveToWorld (new Point3D(354, 3935, 20), Map.TerMur); break; // the Scurvy Reef
				case 55 : specialPed.MoveToWorld (new Point3D(487, 3387, 0), Map.TerMur); break; // the Tomb of Kazibal
				case 56 : specialPed.MoveToWorld (new Point3D(800, 3268, 0), Map.TerMur); break; // the Catacombs of Azerok
				case 57 : specialPed.MoveToWorld (new Point3D(339, 3626, 3), Map.TerMur); break; // the Azure Castle
				case 58 : specialPed.MoveToWorld (new Point3D(752, 4019, 0), Map.TerMur); break; // the Undersea Castle
				case 59 : specialPed.MoveToWorld (new Point3D(203, 2629, -17), Map.TerMur); break; // the Altar of the Dragon King
				case 60 : specialPed.MoveToWorld (new Point3D(865, 2177, -66), Map.TerMur); break; // the Ratmen Mines
				case 61 : specialPed.MoveToWorld (new Point3D(1127, 2188, -28), Map.TerMur); break; // the Pixie Cave
				case 62 : specialPed.MoveToWorld (new Point3D(461, 2617, -28), Map.TerMur); break; // the Spider Cave

				case 63 : specialPed.MoveToWorld (new Point3D(237, 3486, 0), Map.Trammel); break; // the Cave of Banished Mages
				case 64 : specialPed.MoveToWorld (new Point3D(5765, 3248, 0), Map.Trammel); break; // the City of the Dead
				case 65 : specialPed.MoveToWorld (new Point3D(6495, 2877, 45), Map.Trammel); break; // the Crypts of Kuldar
				case 66 : specialPed.MoveToWorld (new Point3D(6340, 2831, 5), Map.Trammel); break; // the Kuldara Sewers
				case 67 : specialPed.MoveToWorld (new Point3D(5354, 53, 15), Map.Felucca); break; // the Mind Flayer City
				case 68 : specialPed.MoveToWorld (new Point3D(1936, 1549, -7), Map.Ilshenar); break; // the Glacial Scar
				case 69 : specialPed.MoveToWorld (new Point3D(1861, 1222, -42), Map.Ilshenar); break; // the Stygian Abyss
				case 70 : specialPed.MoveToWorld (new Point3D(6185, 3645, -60), Map.Felucca); break; // the Temple of Osirus
				case 71 : specialPed.MoveToWorld (new Point3D(6241, 2091, 34), Map.Felucca); break; // the Daemon's Crag
				case 72 : specialPed.MoveToWorld (new Point3D(5755, 2516, 0), Map.Felucca); break; // Dungeon Covetous
				case 73 : specialPed.MoveToWorld (new Point3D(6928, 1574, 0), Map.Felucca); break; // the Castle of Dracula
				case 74 : specialPed.MoveToWorld (new Point3D(6897, 2337, 20), Map.Felucca); break; // the Zealan Tombs
				case 75 : specialPed.MoveToWorld (new Point3D(6339, 1271, 1), Map.Felucca); break; // the Hall of the Mountain King
				case 76 : specialPed.MoveToWorld (new Point3D(6768, 1038, 21), Map.Felucca); break; // Morgaelin's Inferno
				case 77 : specialPed.MoveToWorld (new Point3D(5187, 2225, 10), Map.Felucca); break; // the Depths of Carthax Lake
				case 78 : specialPed.MoveToWorld (new Point3D(6109, 698, 0), Map.Felucca); break; // Argentrock Castle
				case 79 : specialPed.MoveToWorld (new Point3D(6216, 1343, 0), Map.Felucca); break; // the Sanctum of Saltmarsh
			}
		}
	}
}