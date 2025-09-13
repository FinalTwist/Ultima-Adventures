using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GypsyCampAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {5717, -2, 3, 2}, {5717, -2, 2, 2}, {5710, -2, 1, 2}// 1	2	3	
			, {5717, -2, -1, 2}, {5717, -2, -2, 2}, {5710, -2, -3, 2}// 4	5	6	
			, {1997, -2, 4, 8}, {1997, -5, -3, 8}, {1997, -5, -2, 8}// 7	8	9	
			, {1997, -5, -1, 8}, {1997, -5, 0, 8}, {1997, -5, 1, 8}// 10	11	12	
			, {1997, -5, 2, 8}, {1997, -5, 3, 8}, {1997, -5, 4, 8}// 13	14	15	
			, {1997, -4, -3, 8}, {1997, -4, -2, 8}, {1997, -4, -1, 8}// 16	17	18	
			, {1997, -4, 0, 8}, {1997, -4, 1, 8}, {1997, -4, 2, 8}// 19	20	21	
			, {1997, -4, 3, 8}, {1997, -4, 4, 8}, {1997, -3, -3, 8}// 22	23	24	
			, {1997, -3, -2, 8}, {1997, -3, -1, 8}, {1997, -3, 0, 8}// 25	26	27	
			, {1997, -3, 1, 8}, {1997, -3, 2, 8}, {1997, -3, 3, 8}// 28	29	30	
			, {1997, -3, 4, 8}, {1997, -2, -3, 8}, {1997, -2, -2, 8}// 31	32	33	
			, {1997, -2, -1, 8}, {1997, -2, 0, 8}, {1997, -2, 1, 8}// 34	35	36	
			, {1997, -2, 2, 8}, {1997, -2, 3, 8}, {2204, -4, 5, 2}// 37	38	39	
			, {2204, -3, 5, 2}, {2102, -5, 4, 9}, {2103, -6, 4, 9}// 40	41	42	
			, {2103, -6, 3, 9}, {2103, -6, -3, 9}, {2103, -6, -2, 9}// 43	44	45	
			, {2103, -6, -1, 9}, {2103, -6, 0, 9}, {2103, -6, 1, 9}// 46	47	48	
			, {2103, -6, 2, 9}, {2102, -5, -4, 9}, {2102, -4, -4, 9}// 49	50	51	
			, {2102, -3, -4, 9}, {2102, -2, -4, 9}, {2104, -6, -4, 9}// 52	53	54	
			, {2103, -2, -3, 9}, {2103, -2, -2, 9}, {2928, -5, 1, 9}// 55	56	57	
			, {2929, -4, 1, 9}, {2931, -5, 0, 9}, {2930, -4, 0, 9}// 58	59	60	
			, {2903, -5, -1, 9}, {2905, -5, 2, 9}, {2928, -5, -3, 9}// 61	62	63	
			, {2929, -3, -3, 9}, {2932, -4, -3, 9}, {6883, -4, -3, 14}// 64	65	66	
			, {2472, -3, -3, 15}, {737, -6, -4, 14}, {742, -6, -3, 14}// 69	70	71	
			, {742, -6, -2, 14}, {742, -6, -1, 14}, {742, -6, 0, 14}// 72	73	74	
			, {742, -6, 1, 14}, {742, -6, 2, 14}, {742, -6, 3, 14}// 75	76	77	
			, {742, -6, 4, 14}, {743, -5, 4, 14}, {743, -5, -4, 14}// 78	79	80	
			, {743, -4, -4, 14}, {743, -3, -4, 14}, {743, -2, -4, 14}// 81	82	83	
			, {742, -2, -3, 14}, {742, -2, -2, 14}, {4774, -4, 1, 15}// 84	85	87	
			, {3651, -4, -3, 7}, {1630, -5, 3, 28}, {1630, -5, -2, 28}// 88	90	91	
			, {1630, -5, -1, 28}, {1630, -5, 0, 28}, {1630, -5, 1, 28}// 92	93	94	
			, {1630, -5, 2, 28}, {1638, -5, 4, 28}, {1637, -5, -3, 28}// 95	96	97	
			, {1636, -2, -3, 28}, {1635, -2, 4, 28}, {1633, -2, -2, 28}// 98	99	100	
			, {1633, -2, -1, 28}, {1633, -2, 0, 28}, {1633, -2, 1, 28}// 101	102	103	
			, {1633, -2, 2, 28}, {1633, -2, 3, 28}, {1631, -3, -3, 28}// 104	105	106	
			, {1631, -4, -3, 28}, {1632, -4, 4, 28}, {1632, -3, 4, 28}// 107	108	109	
			, {1637, -4, -2, 31}, {1635, -3, 3, 31}, {1636, -3, -2, 31}// 110	111	112	
			, {1638, -4, 3, 31}, {1633, -3, -2, 28}, {1633, -3, -1, 28}// 113	114	115	
			, {1633, -3, 0, 28}, {1633, -3, 1, 28}, {1633, -3, 2, 28}// 116	117	118	
			, {1633, -3, 3, 28}, {1630, -5, -1, 28}, {1630, -4, 3, 28}// 119	120	121	
			, {1651, -3, -1, 31}, {1651, -3, 0, 31}, {1651, -3, 1, 31}// 122	123	124	
			, {1651, -3, 2, 31}, {1652, -4, 2, 31}, {1652, -4, 1, 31}// 125	126	127	
			, {1652, -4, 0, 31}, {1652, -4, -1, 31}, {2799, -5, -2, 9}// 128	129	130	
			, {2798, -3, 3, 9}, {2800, -5, 3, 9}, {2801, -3, -2, 9}// 131	132	133	
			, {2802, -5, 2, 9}, {2802, -5, 1, 9}, {2802, -5, 0, 9}// 134	135	136	
			, {2803, -4, -2, 9}, {2804, -3, -1, 9}, {2804, -3, 0, 9}// 137	138	139	
			, {2804, -3, 1, 9}, {2804, -3, 2, 9}, {2805, -4, 3, 9}// 140	141	142	
			, {2797, -4, 2, 9}, {2797, -4, 1, 9}, {2797, -4, 0, 9}// 143	144	145	
			, {2797, -4, -1, 9}, {742, -2, -1, 14}, {2103, -2, -1, 9}// 146	147	148	
			, {742, -2, 4, 14}, {743, -2, 4, 14}, {2101, -2, 4, 9}// 149	150	151	
			, {2103, -2, 3, 9}, {742, -2, 3, 14}, {742, -2, 0, 14}// 152	153	154	
			, {742, -2, 1, 14}, {742, -2, 2, 14}, {2103, -2, 1, 9}// 155	156	157	
			, {2103, -2, 0, 9}, {2103, -2, 2, 9}, {7135, 1, -1, 2}// 158	159	160	
			, {496, 6, -1, 2}, {498, 6, -4, 2}, {498, 6, -3, 2}// 161	162	163	
			, {498, 6, -2, 2}, {499, 2, -5, 3}, {500, 2, -1, 2}// 164	165	166	
			, {497, 3, -1, 2}, {497, 5, -1, 2}, {504, 2, -4, 2}// 167	168	169	
			, {504, 2, -3, 2}, {504, 2, -2, 2}, {505, 3, -5, 2}// 170	171	172	
			, {505, 4, -5, 2}, {505, 5, -5, 2}, {501, 6, -5, 2}// 173	174	175	
			, {1637, 3, -4, 16}, {1638, 3, -2, 16}, {1635, 6, -1, 27}// 176	177	178	
			, {1636, 5, -4, 16}, {1556, 4, -4, 16}, {1588, 3, -3, 16}// 179	180	181	
			, {1557, 5, -1, 27}, {1589, 6, -2, 27}, {1568, 5, -2, 35}// 182	183	184	
			, {2649, 4, -4, 8}, {7734, 4, -2, 2}, {7735, 3, -2, 2}// 185	186	187	
			, {7736, 2, -2, 2}, {7737, 2, -3, 2}, {7738, 3, -3, 2}// 188	189	190	
			, {7739, 4, -3, 2}, {7740, 4, -4, 2}, {7741, 3, -4, 2}// 191	192	193	
			, {7742, 2, -4, 2}, {4203, 7, -2, 2}, {4965, 4, 3, 2}// 194	195	212	
			, {4966, 2, 6, 3}, {4969, 4, 6, 3}, {4970, 3, 6, 3}// 213	214	215	
			, {4971, 5, 5, 3}, {4973, 5, 4, 3}, {6011, 5, 3, 2}// 216	217	218	
			, {4966, 3, 3, 2}, {4968, 5, 6, 3}, {4972, 2, 4, 2}// 219	220	221	
			, {4973, 2, 5, 4}, {3562, 4, 4, 2}, {4964, 2, 3, 0}// 222	226	233	
			, {3117, 3, 5, 2}, {3118, 4, 5, 2}, {3119, 4, 4, 2}// 237	238	239	
			, {3120, 4, 5, 5}// 240	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GypsyCampAddonDeed();
			}
		}

		[ Constructable ]
		public GypsyCampAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 2594, -5, -3, 17, 0, 29, "", 1);// 67
			AddComplexComponent( (BaseAddon) this, 2459, -4, -2, 23, 677, -1, "Elixir", 1);// 68
			AddComplexComponent( (BaseAddon) this, 18058, -5, 0, 15, 0, -1, "Crystal Ball", 1);// 86
			AddComplexComponent( (BaseAddon) this, 4081, -4, -3, 15, 0, -1, "Trammel & Felucca: A study of the moons", 1);// 89
			AddComplexComponent( (BaseAddon) this, 6951, 3, 6, 2, 1518, -1, "", 1);// 196
			AddComplexComponent( (BaseAddon) this, 6952, 5, 4, 2, 1518, -1, "", 1);// 197
			AddComplexComponent( (BaseAddon) this, 6953, 2, 5, 2, 1518, -1, "", 1);// 198
			AddComplexComponent( (BaseAddon) this, 6954, 4, 3, 2, 1518, -1, "", 1);// 199
			AddComplexComponent( (BaseAddon) this, 6959, 5, 6, 2, 1518, -1, "", 1);// 200
			AddComplexComponent( (BaseAddon) this, 6961, 2, 6, 2, 1518, -1, "", 1);// 201
			AddComplexComponent( (BaseAddon) this, 6962, 5, 3, 2, 1518, -1, "", 1);// 202
			AddComplexComponent( (BaseAddon) this, 6965, 2, 3, 2, 1518, -1, "", 1);// 203
			AddComplexComponent( (BaseAddon) this, 12790, 3, 5, 2, 1518, -1, "", 1);// 204
			AddComplexComponent( (BaseAddon) this, 12790, 3, 4, 2, 1518, -1, "", 1);// 205
			AddComplexComponent( (BaseAddon) this, 12790, 4, 4, 2, 1518, -1, "", 1);// 206
			AddComplexComponent( (BaseAddon) this, 12790, 4, 5, 2, 1518, -1, "", 1);// 207
			AddComplexComponent( (BaseAddon) this, 6951, 4, 6, 2, 1518, -1, "", 1);// 208
			AddComplexComponent( (BaseAddon) this, 6952, 5, 5, 2, 1518, -1, "", 1);// 209
			AddComplexComponent( (BaseAddon) this, 6953, 2, 4, 2, 1518, -1, "", 1);// 210
			AddComplexComponent( (BaseAddon) this, 6954, 3, 3, 2, 1518, -1, "", 1);// 211
			AddComplexComponent( (BaseAddon) this, 3555, 3, 5, 7, 0, 1, "", 1);// 223
			AddComplexComponent( (BaseAddon) this, 3555, 4, 5, 6, 0, 1, "", 1);// 224
			AddComplexComponent( (BaseAddon) this, 3555, 3, 4, 6, 0, 1, "", 1);// 225
			AddComplexComponent( (BaseAddon) this, 3561, 4, 5, 2, 0, 1, "", 1);// 227
			AddComplexComponent( (BaseAddon) this, 3561, 4, 5, 2, 0, 1, "", 1);// 228
			AddComplexComponent( (BaseAddon) this, 3561, 3, 5, 2, 0, 1, "", 1);// 229
			AddComplexComponent( (BaseAddon) this, 3561, 4, 4, 7, 0, 1, "", 1);// 230
			AddComplexComponent( (BaseAddon) this, 3561, 4, 5, 2, 0, 1, "", 1);// 231
			AddComplexComponent( (BaseAddon) this, 3561, 3, 4, 4, 0, 1, "", 1);// 232
			AddComplexComponent( (BaseAddon) this, 6571, 3, 5, 2, 0, 1, "", 1);// 234
			AddComplexComponent( (BaseAddon) this, 6571, 4, 4, 2, 0, 1, "", 1);// 235
			AddComplexComponent( (BaseAddon) this, 6571, 3, 4, 2, 0, 1, "", 1);// 236

		}

		public GypsyCampAddon( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class GypsyCampAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GypsyCampAddon();
			}
		}

		[Constructable]
		public GypsyCampAddonDeed()
		{
			Name = "GypsyCamp";
		}

		public GypsyCampAddonDeed( Serial serial ) : base( serial )
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