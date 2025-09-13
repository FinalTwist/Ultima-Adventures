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
public class OctantSearch
{
    private const int           N = 8;

    private int                 m_Radius;
    private int                 m_Diameter;
    private int                 m_nEntries;
    private SearchSet[]         m_Octants;
    private OctantEntry[,]      m_Matrix;

    public OctantSearch( int radius )
    {
        m_Radius                = radius;
        m_Diameter              = radius*2+1;
        m_Octants               = new SearchSet[N];
        m_Matrix                = new OctantEntry[m_Diameter,m_Diameter];
        m_nEntries              = 0;

        GenerateBaseMatrix();
        GenerateOctants();
    }

    public bool SearchOctant( 
        Map                 map, 
        Compass             compass,
        int                 start,
        SearchDirection     direction,
        Tour                tour 
        )
    {
        SearchSet           oc = m_Octants[(int)compass];
        int                 incr = direction == SearchDirection.Outwards ? 1 : -1;

        for( int i=start;;i+=incr)
        {
            if( i < 0 || i > m_Radius ) return false;

            EntrySet entrySet = oc.GetEntrySet( i );

            if( entrySet == null ) continue;

            List<Entry> entries = entrySet.Entries;

            foreach( OctantEntry entry in entries )
            {
                if( tour( map, entry.X, entry.Y ) )
                    return true;
            }

            return false;
        }
    }
//--------------------------------------------------------------------------------------------------
//  Public, private or otherwise, everything below this line is for implementation or debugging ONLY
//--------------------------------------------------------------------------------------------------
    private void GenerateBaseMatrix( )
    {
        for( int y=-m_Radius; y<m_Radius+1; y++)
        {
            for( int x=-m_Radius; x<m_Radius+1; x++)
            {
                double theta = Trig.Theta( x, y );

                OctantEntry entry = new OctantEntry( x, y, theta );

                m_Matrix[y+m_Radius,x+m_Radius] = entry;
            }
        }
    }

    private void GenerateOctants()
    {
        for( int i=0; i<N; i++)
            m_Octants[i]= new SearchSet( i );

        for( int y=-m_Radius; y<m_Radius+1; y++)
        {
            for( int x=-m_Radius; x<m_Radius+1; x++)
            {
                OctantEntry entry = m_Matrix[y+m_Radius,x+m_Radius];

                //if( entry.Distance != 0 ) 
                //{
                    m_Octants[entry.Octant].AddEntry( entry );
                    m_nEntries++;
                //}
            }
        }
    }

    public void DumpOctants( )
    {
        Console.WriteLine("nEntries = "+m_nEntries);
        foreach( SearchSet oc in m_Octants )
        {
            Console.WriteLine( oc.ToString() );
            Console.WriteLine( );
        }
    }

    public void DumpMatrix( )
    {
        Console.WriteLine("\n");
        for( int y=-m_Radius; y<m_Radius+1; y++)
        {
            for( int x=-m_Radius; x<m_Radius+1; x++)
            {
                OctantEntry entry = m_Matrix[y+m_Radius,x+m_Radius];
                Console.Write( "{0:00} ", entry.Distance );
            }
            Console.WriteLine("");
        }
        Console.WriteLine("\n");
    }

    public override string ToString( )
    {
        string str = "[ ";

        foreach( SearchSet oc in m_Octants )
            str += oc.ToString() + ", ";

            str += " ]";
        
        return str;
    }
//==================================================================================================
//  SearchSet -- all entry sets, sorted by range, are held in octants in the search area
//==================================================================================================
    public class SearchSet
    {
        private     int                         m_Octant;
        private     SortedList<int,EntrySet>    m_EntrySets;
        public      int                         Octant { get { return m_Octant; } }

        public SearchSet( int octant )
        {
            m_Octant = octant;
            m_EntrySets = new SortedList<int,EntrySet>();
        }

        public EntrySet GetEntrySet( int i ) 
        {
            if( m_EntrySets.ContainsKey(i) ) return m_EntrySets[i];
            else return null;
        }

        public override string ToString( )
        {
            string str = "[ ";

            foreach( EntrySet entrySet in m_EntrySets.Values )
                str += entrySet.ToString() + ", ";

            str += " ]";
            
            return str;
        }

        public void AddEntry( Entry entry )
        {
            EntrySet entrySet;

            if( m_EntrySets.ContainsKey( entry.Distance ) )
            {
                entrySet = m_EntrySets[ entry.Distance ];
            }
            else
            {
                entrySet = new EntrySet( entry.Distance );
                m_EntrySets.Add( entry.Distance, entrySet );
            }

            entrySet.AddEntry( entry );
        }
    }
//--------------------------------------------------------------------------------------------------
//  OctantSearch Entry: just a place to store
//--------------------------------------------------------------------------------------------------
    public class OctantEntry :  Entry
    {
        private double      m_Theta;
        private int         m_Octant;
        public  int         Octant { get { return m_Octant; } }

        public OctantEntry( int x, int y, double theta ) : base ( x, y, DistanceFunction.Geometric )
        {
            m_Theta     = theta;

            ApplyOctant( 8 );
        }

        public void ApplyOctant( int nOctants )
        {
            double circ        = Math.PI * 2;
            double theta       = m_Theta;
            double peak        = nOctants * 2;
            int    octant       = 0;

            for( double i = 1.0; i < peak; i+= 2.0)
            {
                if( theta < circ * ( i / peak ))
                {
                    m_Octant = octant;
                    return;
                }
                octant += 1;
            }

            m_Octant = 0;
        }
    }
}
//--------------------------------------------------------------------------------------------------
} // namespace Searches
//--------------------------------------------------------------------------------------------------
