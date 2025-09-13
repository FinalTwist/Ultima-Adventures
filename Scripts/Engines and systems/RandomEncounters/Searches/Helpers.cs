//--------------------------------------------------------------------------------------------------
//  Copyright Joe Kraska, 2006. This program is free software. You can redistribute it
//  and/or modify it under terms of the GNU General Public License as published by the
//  Free Software Foundation; either version 2 of the License, or (at your option) any
//  later version. See COPYING.TXT for details.
//--------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
//--------------------------------------------------------------------------------------------------
namespace Server.Searches {
//==================================================================================================
//  OctantSearch main class
//==================================================================================================
public class Trig
{
    public static double Theta( int x, int y )
    {
        double theta;

        if      ( y == 0 && x < 0  )    theta = Math.PI / 2;
        else if ( y == 0 && x > 0  )    theta = Math.PI / 2 + Math.PI;
        else if ( y < 0  && x == 0 )    theta = 0;
        else if ( y > 0  && x == 0 )    theta = Math.PI;
        else if ( x == 0 && y == 0 )    theta = 0;
        else
        {
            float val = (float) x / (float) y;

            theta = Math.Atan( val ); 

            if      ( x < 0 && y < 0 )  return theta;
            else if ( x < 0 && y > 0 )  theta += Math.PI;
            else if ( x > 0 && y > 0 )  theta += Math.PI;
            else if ( x > 0 && y < 0 )  theta += Math.PI * 2;
        }

        return theta;
    }
}
//--------------------------------------------------------------------------------------------------
//  duplicate of the runuo Utility; done so that the module can be tested without being compiled
//  by the script compiler:
//--------------------------------------------------------------------------------------------------
//public class Utility
//{
//    private static  Random          m_Random = new Random();
//    public static int RandomMinMax( int min, int max )
//    {
//        if ( min > max ) { int copy = min; min = max; max = copy; }
//        else if ( min == max ) { return min; }
//        return min + m_Random.Next( (max - min) + 1 );
//    }
//}
//--------------------------------------------------------------------------------------------------
} // namespace MapVisitors
//--------------------------------------------------------------------------------------------------
