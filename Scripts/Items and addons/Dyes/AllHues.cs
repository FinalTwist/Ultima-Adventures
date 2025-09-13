/************************************************
 All Hues
X-SirSly-X
2006.04.12
 ver 1.0

Their are a few built in random color commands, 
such as the ones listed below:

Utility.RandomAnimalHue();
Utility.RandomBirdHue();
Utility.RandomBlueHue();
Utility.RandomDyedHue();
Utility.RandomGreenHue();
Utility.RandomHairHue();
Utility.RandomMetalHue();
Utility.RandomNeutralHue();
Utility.RandomNondyedHue();
Utility.RandomOrangeHue();
Utility.RandomPinkHue();
Utility.RandomRedHue();
Server.Misc.RandomThings.GetRandomSkinColor();
Utility.RandomSlimeHue();
Utility.RandomSnakeHue();
Utility.RandomYellowHue();

But their comes a time when those random lists
won't do the job your looking for.

************************************************/

using System;
using Server;

namespace Server.AllHues
{

	public class AllHuesInfo
	{
/*
		public static int Random
		{
			get
			{
				switch ( Utility.Random( 16 ) )
				{
					case 0: RanHue = Utility.RandomAnimalHue(); break;
					case 1: RanHue = Utility.RandomBirdHue(); break;
					case 2: RanHue = Utility.RandomBlueHue(); break;
					case 3: RanHue = Utility.RandomDyedHue(); break;
					case 4: RanHue = Utility.RandomGreenHue(); break;
					case 5: RanHue = Utility.RandomHairHue(); break;
					case 6: RanHue = Utility.RandomMetalHue(); break;
					case 7: RanHue = Utility.RandomNeutralHue(); break;
					case 8: RanHue = Utility.RandomNondyedHue(); break;
					case 9: RanHue = Utility.RandomOrangeHue(); break;
					case 10: RanHue = Utility.RandomPinkHue(); break;
					case 11: RanHue = Utility.RandomRedHue(); break;
					case 12: RanHue = Server.Misc.RandomThings.GetRandomSkinColor(); break;
					case 13: RanHue = Utility.RandomSlimeHue(); break;
					case 14: RanHue = Utility.RandomSnakeHue(); break;
					case 15: RanHue = Utility.RandomYellowHue(); break;
				}
			}
		}
*/
		public static int Reds
		{
			get
			{
				return Utility.RandomList
				(
					30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
					130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140,
					230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240,
					330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340
				);
			}
		}

		public static int OddNeon
		{
			get
			{
				return Utility.RandomList
				(
					1057, 1058, 1059, 1060, 1061, 1062, 1063, 1064, 1065, 1066,
					1067, 1068, 1069, 1071, 1072, 1073, 1074, 1075, 1076, 1077,
					1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085, 1086, 1087,
					1088, 1089, 1090, 1091, 1092, 1093, 1094, 1095, 1096, 1097,
					1098, 1099,
					
					1176, 1177, 1178, 1179, 1180, 1181, 1182, 1183, 1184, 1185,
					1186, 1187,
					
					1195, 1196, 1197, 1198,
					
					1288, 1289, 1290, 1291, 1292, 1293, 1294, 1295, 1296, 1297,
					1298,

					1377, 1378, 1379, 1380, 1381, 1382, 1383, 1384, 1385, 1386,
					1387, 1388, 1389, 1390, 1391, 1392, 1393, 1394, 1395, 1386,
					1387, 1388, 1389, 1390, 1391, 1392, 1393, 1394, 1395, 1396,
					1397, 1398, 
					
//					1453-1480
//					1486-1499
//					1553-1598
				
					1653, 1654, 1655, 1656, 1657, 1658, 1659, 1660, 1661, 1662

//					1671-1698
				);
			}
		}

	}
}