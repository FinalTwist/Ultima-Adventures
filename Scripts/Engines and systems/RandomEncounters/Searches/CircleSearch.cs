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
public class CircleSearch
{
    private int                 m_Radius;
    private int                 m_Diameter;
    private int                 m_nEntries;
    private SearchSet[]         m_SearchSets;
    private CircleEntry[,,]     m_Matrices;
//--------------------------------------------------------------------------------------------------
//
//--------------------------------------------------------------------------------------------------
    public CircleSearch( int radius )
    {
        m_Radius                = radius;
        m_Diameter              = radius*2+1;
        m_SearchSets            = new SearchSet[m_Radius+1];
        m_Matrices              = new CircleEntry[m_Radius+1,m_Diameter,m_Diameter];
        m_nEntries              = 0;

        GenerateBaseMatrix();
        GenerateSearchSets();
    }
//--------------------------------------------------------------------------------------------------
//  SearchCircle -- search around the radius
//--------------------------------------------------------------------------------------------------
    public bool SearchCircle( 
        Map                 map, 
        int                 radius, 
        Tour                tour 
        )
    {
        return SearchCircle( map, radius, ClockDirection.CounterClockwise, tour );
    }
//--------------------------------------------------------------------------------------------------
    public bool SearchCircle( 
        Map                 map, 
        int                 radius, 
        ClockDirection      clockDirection,
        Tour                tour 
        )
    {
        SearchSet           ss = m_SearchSets[radius];

        SortedList<double,CircleEntry> entries = ss.Entries;

        if( entries == null ) return false;

        IList<CircleEntry> values;

        if( clockDirection == ClockDirection.Clockwise )
        {
            List<CircleEntry> list = new List<CircleEntry>( entries.Values );
            list.Reverse();
            values = list;
        }
        else 
            values = entries.Values;

        foreach( CircleEntry entry in values )
        {
            if( tour( map, entry.X, entry.Y ) ) return true;
        }

        return false;
    }
//--------------------------------------------------------------------------------------------------
//  SearchContentricCircles -- search around the radii, moving in or out
//--------------------------------------------------------------------------------------------------
    public bool SearchConcentricCircles( 
        Map                 map, 
        int                 start, 
        SearchDirection     searchDirection,
        Tour                tour 
        )
    {
        return SearchConcentricCircles( map, start, ClockDirection.CounterClockwise, searchDirection, tour );
    }
//--------------------------------------------------------------------------------------------------
    public bool SearchConcentricCircles( 
        Map                 map, 
        int                 start, 
        ClockDirection      clockDirection,
        SearchDirection     searchDirection,
        Tour                tour 
        )
    {
        int                 incr = ( searchDirection == SearchDirection.Outwards ? 1 : -1 );

        for( int i=start;; i+=incr)
        {
            if( i < 0 || i > m_Radius ) return false;

            SearchSet           ss = m_SearchSets[i];

            SortedList<double,CircleEntry> entries = ss.Entries;

            if( entries == null ) continue;

            IList<CircleEntry> values;

            if( clockDirection == ClockDirection.Clockwise )
            {
                List<CircleEntry> list = new List<CircleEntry>( entries.Values );
                list.Reverse();
                values = list;
            }
            else 
                values = entries.Values;

            foreach( CircleEntry entry in values )
            {
                if( tour( map, entry.X, entry.Y ) )
                    return true;
            }

            return false;
        }
    }
//--------------------------------------------------------------------------------------------------
//  Generate the base matrix of entries, using Bresenham's algorithm
//--------------------------------------------------------------------------------------------------
    private void GenerateBaseMatrix( )
    {
        for( int i=0; i< m_Radius+1; i++ )
        {
            int x = 0;
            int y = i;
            int d = 3 - 2 * i;

            while( x <= y )
            {
                { int dX = +x; int dY = +y; AddMatrixEntry( i, dX, dY ); }
                { int dX = +x; int dY = -y; AddMatrixEntry( i, dX, dY ); }
                { int dX = -x; int dY = +y; AddMatrixEntry( i, dX, dY ); }
                { int dX = -x; int dY = -y; AddMatrixEntry( i, dX, dY ); }
                { int dX = +y; int dY = +x; AddMatrixEntry( i, dX, dY ); }
                { int dX = +y; int dY = -x; AddMatrixEntry( i, dX, dY ); }
                { int dX = -y; int dY = +x; AddMatrixEntry( i, dX, dY ); }
                { int dX = -y; int dY = -x; AddMatrixEntry( i, dX, dY ); }

                if( d < 0 ) 
                {   
                    d = d + 4 * x + 6;
                }
                else
                {
                    d = d + 4 * ( x - y ) + 10;
                    y--;
                }
                x++;
            }
        }
    }

    private void AddMatrixEntry( int i, int x, int y )
    {
        double  theta = Trig.Theta( x, y );
        CircleEntry   entry = new CircleEntry(  x, y, theta );
        m_Matrices[i,y+m_Radius,x+m_Radius] = entry;
    }
//--------------------------------------------------------------------------------------------------
//  Create/sort search sets according to radii
//--------------------------------------------------------------------------------------------------
    private void GenerateSearchSets()
    {
        for( int i=0; i<m_Radius+1; i++)
        {
            m_SearchSets[i]= new SearchSet( i );

            for( int y=-m_Radius; y<m_Radius+1; y++)
            {
                for( int x=-m_Radius; x<m_Radius+1; x++)
                {
                    CircleEntry entry = m_Matrices[i,y+m_Radius,x+m_Radius];

                    if( entry == null ) continue;
                    if( entry.Distance > m_Radius ) continue;

                    m_SearchSets[i].AddEntry( entry );

                    m_nEntries++;
                }
            }
        }
    }
//--------------------------------------------------------------------------------------------------
//  Debugging
//--------------------------------------------------------------------------------------------------
    public void DumpSearchSets( )
    {
        Console.WriteLine("nEntries = "+m_nEntries);
        foreach( SearchSet oc in m_SearchSets )
        {
            Console.WriteLine( oc.ToString() );
            Console.WriteLine( );
        }
    }
//--------------------------------------------------------------------------------------------------
//  Debugging
//--------------------------------------------------------------------------------------------------
    public void DumpMatrices( )
    {
        Console.WriteLine("\n");
        for( int i=0; i<m_Radius+1; i++)
        {
            for( int y=-m_Radius; y<m_Radius+1; y++)
            {
                for( int x=-m_Radius; x<m_Radius+1; x++)
                {
                    CircleEntry entry = m_Matrices[i,y+m_Radius,x+m_Radius];

                    if( entry==null ) Console.Write("-- ");
                    else Console.Write( "{0:00} ", entry.Distance );
                }
                Console.WriteLine("");
            }
            Console.WriteLine("\n");
        }
    }
//--------------------------------------------------------------------------------------------------
//  Debugging
//--------------------------------------------------------------------------------------------------
    public override string ToString( )
    {
        string str = "[ ";

        foreach( SearchSet oc in m_SearchSets )
            str += oc.ToString() + ", ";

            str += " ]";
        
        return str;
    }
//--------------------------------------------------------------------------------------------------
//  SearchSet -- all entry sets, sorted by THETA, are held in octants in the search area
//--------------------------------------------------------------------------------------------------
    public class SearchSet
    {
        private     int                                 m_Radius;
        private     SortedList<double,CircleEntry>      m_Entries;
        public      SortedList<double,CircleEntry>      Entries { get { return m_Entries; } }

        public SearchSet( int radius )
        {
            m_Radius = radius;
            m_Entries = new SortedList<double,CircleEntry>();
        }

        public override string ToString( )
        {
            string str = "[ ";

            foreach( CircleEntry entry in m_Entries.Values )
                str += entry.ToString() + ", ";

            str += " ]";
            
            return str;
        }

        public void AddEntry( CircleEntry entry )
        {
            m_Entries.Add( entry.Theta, entry );
        }
    }
//--------------------------------------------------------------------------------------------------
//  CirclEntry: just a place to store
//--------------------------------------------------------------------------------------------------
    public class CircleEntry :  Entry
    {
        private double      m_Theta;
        public  double      Theta{ get { return m_Theta; } }

        public CircleEntry( int x, int y, double theta ) : base ( x, y, DistanceFunction.Geometric )
        {
            m_Theta     = theta;
        }
        public override string ToString( )
        {
            //return string.Format("{0:000.00}", m_Theta);
            return string.Format("{0:00}", Distance);
        }
    }
}
//--------------------------------------------------------------------------------------------------
} // namespace Searches
//--------------------------------------------------------------------------------------------------
