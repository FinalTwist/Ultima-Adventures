/* Copyright(c) 2016 UltimaLive
 * 
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using UltimaLive;
using System.IO;

namespace UltimaLive
{
    public class UltimaLiveUtility
    {
        public static List<Point2D> rasterCircle(Point2D center, int radius)
        {
            int x0 = center.X;
            int y0 = center.Y;

            List<Point2D> pointList = new List<Point2D>();
            int f = 1 - radius;
            int ddF_x = 1;
            int ddF_y = -2 * radius;
            int x = 0;
            int y = radius;

            pointList.Add(new Point2D(x0, y0 - radius));
            pointList.Add(new Point2D(x0, y0 + radius));
            pointList.Add(new Point2D(x0 - radius, y0));
            pointList.Add(new Point2D(x0 + radius, y0));

            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;

                Point2D p = new Point2D(x0 + x, y0 + y);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }
                p = new Point2D(x0 - x, y0 + y);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

                p = new Point2D(x0 + x, y0 - y);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

                p = new Point2D(x0 - x, y0 - y);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

                p = new Point2D(x0 + y, y0 + x);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

                p = new Point2D(x0 - y, y0 + x);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

                p = new Point2D(x0 + y, y0 - x);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

                p = new Point2D(x0 - y, y0 - x);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }

            }

            foreach (Point2D p in pointList)
            {
                Console.WriteLine("" + p.X + "," + p.Y);
            }

            return pointList;
        }

        //http://en.wikipedia.org/wiki/Midpoint_circle_algorithm
        public static List<Point2D> rasterFilledCircle(Point2D center, int radius)
        {
            int x0 = center.X;
            int y0 = center.Y;

            List<Point2D> pointList = new List<Point2D>();
            int f = 1 - radius;
            int ddF_x = 1;
            int ddF_y = -2 * radius;
            int x = 0;
            int y = radius;

            for (int h = y0 - radius; h <= y0 + radius; h++)
            {
                Point2D p = new Point2D(x0, h);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }
            }

            for (int h = x0 - radius; h <= x0 + radius; h++)
            {
                Point2D p = new Point2D(h, y0);
                if (!(pointList.Contains(p)))
                {
                    pointList.Add(p);
                }
            }

            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;

                for (int h = x0 - x; h <= x0 + x; h++)
                {
                    Point2D p = new Point2D(h, y0 + y);
                    if (!(pointList.Contains(p)))
                    {
                        pointList.Add(p);
                    }
                }

                for (int h = x0 - x; h <= x0 + x; h++)
                {
                    Point2D p = new Point2D(h, y0 - y);
                    if (!(pointList.Contains(p)))
                    {
                        pointList.Add(p);
                    }
                }

                for (int h = x0 - y; h <= x0 + y; h++)
                {
                    Point2D p = new Point2D(h, y0 + x);
                    if (!(pointList.Contains(p)))
                    {
                        pointList.Add(p);
                    }
                }

                for (int h = x0 - y; h <= x0 + y; h++)
                {
                    Point2D p = new Point2D(h, y0 - x);
                    if (!(pointList.Contains(p)))
                    {
                        pointList.Add(p);
                    }
                }
            }
            return pointList;
        }
    }
}