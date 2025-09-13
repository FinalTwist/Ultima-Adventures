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
//--------------------------------------------------------------------------------------------------
//  Entry: just a place to store
//--------------------------------------------------------------------------------------------------
public class Entry 
{
    private int         m_X;
    private int         m_Y;
    private int         m_Distance;

    public int          Distance{ get { return m_Distance; } }
    public int          X{ get { return m_X; } }
    public int          Y{ get { return m_Y; } }

    public Entry( int x, int y, DistanceFunction distanceFunction )
    {
        m_X         = x;
        m_Y         = y;

        if( distanceFunction == DistanceFunction.Geometric ) ApplyGeometricDistance();
        else ApplyLinearDistance();
    }

    public void ApplyGeometricDistance( )
    {
        m_Distance = (int) Math.Sqrt ( m_X * m_X + m_Y * m_Y  );
    }
    
    public void ApplyLinearDistance( )
    {
        int absX    = Math.Abs( m_X );
        int absY    = Math.Abs( m_Y );

        if( absY > absX )  m_Distance = absY;
        else               m_Distance = absX;
    }
    public override string ToString( )
    {
        return "" + m_Distance;
    }
}
//--------------------------------------------------------------------------------------------------
//  EntrySet: just a place to store
//--------------------------------------------------------------------------------------------------
public class EntrySet
{
    private int             m_Distance;
    private List<Entry>     m_Entries;

    public  List<Entry>     Entries { get { return m_Entries; } }
    public  int             Distance { get { return m_Distance; } }

    public EntrySet( int distance )
    {
        m_Distance = distance;
        m_Entries = new List<Entry>();
    }

    public void AddEntry( Entry entry )
    {
        m_Entries.Add( entry );
    }
    
    public override string ToString()
    {
        string str = "[ ";

        foreach( Entry entry in m_Entries )
            str += entry.ToString() + ", ";

        str += " ]";

        return str;
    }
}
//--------------------------------------------------------------------------------------------------
}  // namespace Server.Search
//--------------------------------------------------------------------------------------------------
