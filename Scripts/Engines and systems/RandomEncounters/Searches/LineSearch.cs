//------------------------------------------------------------------------------
using System;
using System.Collections;
//------------------------------------------------------------------------------
namespace Server.Searches {
//------------------------------------------------------------------------------
//    void Initialize()
//    {
//    matrix = Matrix( 24 )
//    n_octants = 8
//    matrix.apply_geometric_distance( )
//    matrix.apply_octants( n_octants )
//    set = SearchSet( n_octants )
//    matrix.add_entries( set )
//    ##matrix.dump( )
//    set.sort()
//    set.dump()
//    codegen(set)
//    }
public class LineSearch
{
    public LineSearch(){}

    public bool SearchLine( Map map, Point2D begin, Point2D end, Tour tour )
    {
        int       x1=begin.X, y1=begin.Y;
        int       x2=end.X,   y2=end.Y;

        if ( x1 == x2 && y1 == y2 )
        {
            return tour( map, x1, y1 );
        }

        int       dX = Math.Abs(x2-x1);    // store the change in X and Y of the line endpoints
        int       dY = Math.Abs(y2-y1);

        int       xcursor = x1;
        int       ycursor = y1;
        
        int       Xincr; if (x2 < x1) { Xincr=-1; } else { Xincr=1; }    // which direction in X?
        int       Yincr; if (y2 < y1) { Yincr=-1; } else { Yincr=1; }    // which direction in Y?

        if (dX >= dY)   // if X is the independent variable
        {           
            int dPr         = dY<<1;                      // amount to increment decision if right is chosen (always)
            int dPru        = dPr - (dX<<1);              // amount to increment decision if up is chosen
            int P           = dPr - dX;                   // decision variable start value

            for (; dX>=0; dX--)                           // process each point in the line one 
                                                          // at a time (just use dX)
            {
                if( tour( map, xcursor, ycursor )) return true;

                if (P > 0)                                // is the pixel going right AND up?
                { 
                    xcursor+=Xincr;                       // increment independent variable
                    ycursor+=Yincr;                       // increment dependent variable
                    P+=dPru;                              // increment decision (for up)
                }
                else                                      // is the pixel just going right?
                {
                    xcursor+=Xincr;                       // increment independent variable
                    P+=dPr;                               // increment decision (for right)
                }
            }               

            return false;
        }
        else                                              // if Y is the independent variable
        {
            int dPr         = dX<<1;                      // amount to increment decision if right is chosen (always)
            int dPru        = dPr - (dY<<1);              // amount to increment decision if up is chosen
            int P           = dPr - dY;                   // decision variable start value

            for (; dY>=0; dY--)                           // process each point in the line one at a time (just use dY)
            {
                if( tour( map, xcursor, ycursor )) return true;

                if (P > 0)                                // is the pixel going up AND right?
                { 
                    xcursor+=Xincr;                       // increment dependent variable
                    ycursor+=Yincr;                       // increment independent variable
                    P+=dPru;                              // increment decision (for up)
                }
                else                                      // is the pixel just going up?
                {
                    ycursor+=Yincr;                       // increment independent variable
                    P+=dPr;                               // increment decision (for right)
                }
            }               

            return false;
        }
    }
}
//------------------------------------------------------------------------------
} // namespace Server.Misc
//------------------------------------------------------------------------------
