using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Targeting;
using System.IO;
using System.Collections.Generic;

namespace Server.Items
{
	public class MarketEast : Item
	{
        private ArrayList m_market;
        int marketCount = 1;

        private ArrayList m_marketmob;
        int marketmobCount = 1;

        public override bool HandlesOnMovement { get { return true; } }

		[Constructable]
        public MarketEast()
            : base(3023)
		{

            Name = "MarketEast 6-18h";
            Visible = false;
            m_market = new ArrayList();
            m_marketmob = new ArrayList();
			
		}
        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);


            int hours, minutes, uoDay, totalUoDays, totalMinutes;

            Server.Items.Clock.GetTime(m.Map, m.X, m.Y, out hours, out minutes, out totalMinutes);

            totalUoDays = (int)Math.Ceiling((double)totalMinutes / (60 * 24));

            Math.DivRem(totalUoDays, 30, out uoDay);

            //if (uoDay== 1 || uoDay == 7 || uoDay == 15 || uoDay== 23) 
            // {
            if (hours >= 6 && hours <= 18)
            {
                int marketplace = m_market.Count;
                int marketplacemob = m_marketmob.Count;

                if (marketplace >= marketCount && marketplacemob >= marketCount)
                    return;

                bool validLocation = false;
                Map map = this.Map;

                switch (Utility.Random(11))
                {

                    #region fisherman
                    case 0:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandFishEastAddon market = new MarketStandFishEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;
                                    Fisherman marketmob1 = new Fisherman(); 

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region mercer
                    case 1:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandMercerEastAddon market = new MarketStandMercerEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Tailor marketmob1 = new Tailor();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region vegetables
                    case 2:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandVegetablesEastAddon market = new MarketStandVegetablesEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Farmer marketmob1 = new Farmer();

                                    int gx = X - 2;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region cheese
                    case 3:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandCheeseEastAddon market = new MarketStandCheeseEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                   Farmer marketmob1 = new Farmer();

                                    int gx = X - 1;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                                            #endregion

                    #region wine
                    case 4:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandWineEastAddon market = new MarketStandWineEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X ;
                                        int ay = Y -1;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Barkeeper marketmob1 = new Barkeeper();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region herbs
                    case 5:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandHerbsEastAddon market = new MarketStandHerbsEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Herbalist marketmob1 = new Herbalist();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region mushroom
                    case 6:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandMushroomEastAddon market = new MarketStandMushroomEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Herbalist marketmob1 = new Herbalist();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region fur
                    case 7:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandFurEastAddon market = new MarketStandFurEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Furtrader marketmob1 = new Furtrader();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region bee
                    case 8:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandBeeEastAddon market = new MarketStandBeeEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Beekeeper marketmob1 = new Beekeeper();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region copper
                    case 9:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandCopperEastAddon market = new MarketStandCopperEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Blacksmith marketmob1 = new Blacksmith();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion

                    #region tool
                    case 10:
                        {
                            for (int i = marketplace; i < marketCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc = this.Location;//
                                    MarketStandToolEastAddon market = new MarketStandToolEastAddon();

                                    for (int k = 0; !validLocation && k < 10; ++k)
                                    {
                                        int ax = X;
                                        int ay = Y;
                                        int az = map.GetAverageZ(ax, ay);

                                        if (validLocation = map.CanFit(ax, ay, this.Z, 16, false, false))
                                            marketloc = new Point3D(ax, ay, Z);
                                        else if (validLocation = map.CanFit(ax, ay, az, 16, false, false))
                                            marketloc = new Point3D(ax, ay, az);
                                    }
                                    market.MoveToWorld(marketloc, this.Map);
                                    m_market.Add(market);
                                }

                            }
                            for (int i = marketplacemob; i < marketmobCount; ++i)
                            {
                                if (i == 0)
                                {
                                    Point3D marketloc6 = this.Location;//
                                    Tinker marketmob1 = new Tinker();

                                    int gx = X + 3;
                                    int gy = Y;
                                    int gz = map.GetAverageZ(gx, gy);

                                    if (validLocation = map.CanFit(gx, gy, this.Z, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, Z);
                                    else if (validLocation = map.CanFit(gx, gy, gz, 16, false, false))
                                        marketloc6 = new Point3D(gx, gy, gz);

                                    marketmob1.Home = marketloc6;
                                    marketmob1.RangeHome = 0;
                                    marketmob1.MoveToWorld(marketloc6, this.Map);
                                    m_marketmob.Add(marketmob1);
                                }
                            }
                            break;
                        }

                    #endregion
                }
            }

            else
            {
                foreach (Item that in m_market)
                    that.Delete();

                foreach (Mobile thats in m_marketmob)
                    thats.Delete();
            }
        }
		public MarketEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
            base.Serialize(writer);
            writer.Write(1); // Version
            writer.WriteItemList(m_market, true);
            writer.WriteMobileList(m_marketmob, true);
		}

		public override void Deserialize( GenericReader reader )
		{
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_market = reader.ReadItemList();
            m_marketmob = reader.ReadMobileList();
		}
	}
}
