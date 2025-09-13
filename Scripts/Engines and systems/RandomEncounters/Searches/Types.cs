//--------------------------------------------------------------------------------------------------
//  Copyright Joe Kraska, 2006. This program is free software. You can redistribute it
//  and/or modify it under terms of the GNU General Public License as published by the
//  Free Software Foundation; either version 2 of the License, or (at your option) any
//  later version. See COPYING.TXT for details.
//--------------------------------------------------------------------------------------------------
namespace Server.Searches {
//--------------------------------------------------------------------------------------------------
public enum DistanceFunction
{ 
    Linear, 
    Geometric
};
//--------------------------------------------------------------------------------------------------
public enum SearchDirection
{ 
    Inwards, 
    Outwards
};
//--------------------------------------------------------------------------------------------------
public enum ClockDirection
{ 
    Clockwise, 
    CounterClockwise
};
//--------------------------------------------------------------------------------------------------
public enum Compass
{ 
    North       = 0, 
    NorthWest   = 1, // UO 7
    West        = 2, // UO 6
    SouthWest   = 3, // UO 5
    South       = 4, // UO 4
    SouthEast   = 5, // UO 3
    East        = 6, // UO 2
    NorthEast   = 7  // UO 1
};
//--------------------------------------------------------------------------------------------------
public delegate bool Tour( Map map, int x, int y );
//--------------------------------------------------------------------------------------------------
}  // namespace Server.Search
//--------------------------------------------------------------------------------------------------
