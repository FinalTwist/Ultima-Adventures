//--------------------------------------------------------------------------------
// Copyright Joe Kraska, 2006. This file is restricted according to the GPL.
// Terms and conditions can be found in COPYING.txt.
//--------------------------------------------------------------------------------
using System;
//--------------------------------------------------------------------------------
using Server;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using System.IO;
using System.Collections.Generic;
//--------------------------------------------------------------------------------
namespace Server.Misc {
//--------------------------------------------------------------------------------
// Import code.
//--------------------------------------------------------------------------------
public class ImportRecord
{
    Mobile          m_mobile;
    string          m_spawn;
    Point3D         m_location;
    int             m_hRange;
    int             m_sRange;
    int             m_n;
    string          m_mapName;
    string          m_regionName;
    double          m_level;
    double          m_p;
    bool            m_hostile;

    public Mobile   Mobile    { get { return m_mobile; } }
    public double   N         { get { return m_n; } }
    public double   P         { get { return m_p; } }
    public double   Level     { get { return m_level; } }
    public string   Spawn     { get { return m_spawn; } }
    public Point3D  Location  { get { return m_location; } }
    public bool     Hostile   { get { return m_hostile; } }

    public ImportRecord(
        Mobile          mobile,
        string          spawn,
        int             x,
        int             y,
        int             z,
        int             hRange,
        int             sRange,
        int             n,
        string          mapName,
        string          regionName,
        double          level,
        bool            hostile,
        double          p
        )
    {
        m_mobile        = mobile;
        m_spawn         = spawn;
        m_location      = new Point3D( x, y, z );
        m_hRange        = hRange;
        m_sRange        = sRange;
        m_n             = n;
        m_mapName       = mapName;
        m_regionName    = regionName;
        m_level         = level;
        m_hostile       = hostile;
        m_p             = p;
    }

    public override string ToString()
    {
        string format = String.Format( "{0} ({1},{2},{3}) {4}/{5} n={6}, map={7}, region={8}, level={9:0.0}, hostile={10} p={11:0.000}",
            m_spawn,
            m_location.X,
            m_location.Y,
            m_location.Z,
            m_hRange,
            m_sRange,
            m_n,
            m_mapName,
            m_regionName,
            m_level,
            m_hostile,
            m_p
        );
        return format;
    }
}
//--------------------------------------------------------------------------------
public class ImportHelper
{
    private static Dictionary<string,Dictionary<string,List<ImportRecord>>> facetEncounters; // keyed mapname, keyed region name, sorted by p

    public static void Import( Mobile mobile )
    {
        facetEncounters = new Dictionary<string,Dictionary<string,List<ImportRecord>>>(); 

        string  topDir = Path.Combine( Core.BaseDirectory, "Data/Import/Premiums/"  );

        if( !Directory.Exists( topDir ) )
        {
            Console.WriteLine("#### RandomEncounters: Error; no such directory \"{0}\" ... ", topDir );
            return;
        }

        string[] dirs = Directory.GetDirectories( topDir );

        foreach( string dir in dirs )
        {
            string  mapDir = Path.GetFileName( dir );

            Console.WriteLine("RandomEncounters: importing from \"{0}\"",  dir );

            if( !Directory.Exists( dir ) )
            {
                Console.WriteLine("#### RandomEncounters: Error; no such directory \"{0}\" ... ", dir );
                return;
            }

            string[] files = Directory.GetFiles( dir, "*.map" );

            foreach( string file in files )
            {
                ProcessFile( file, mapDir );
            }
        }

        DumpXml();
    }

    public static void ProcessFile( string fileName, string mapName )
    {
        Console.WriteLine( "    " + fileName );

        StreamReader streamReader = new StreamReader( fileName );

        string line;

        while( ( line = streamReader.ReadLine() ) != null )
        {
            if( line[0] != '*' ) continue;

            string trimmed = line.Substring(2,line.Length-2);

            ProcessSpawnLine( trimmed, mapName );
        }
    }

    public static void ProcessSpawnLine( string spawn, string mapName )
    {
            string[] tokens = spawn.Split('|');

            string[]    sets = new string[6];
            int[]       counts = new int[6];

            sets[0]          = tokens[0]; //.Split(':');
            sets[1]          = tokens[1]; //.Split(':');
            sets[2]          = tokens[2]; //.Split(':');
            sets[3]          = tokens[3]; //.Split(':');
            sets[4]          = tokens[4]; //.Split(':');
            sets[5]          = tokens[5]; //.Split(':');

            int     x       = int.Parse( tokens[6] );
            int     y       = int.Parse( tokens[7] );
            int     z       = int.Parse( tokens[8] );

            int     hRange  = int.Parse( tokens[tokens.Length-9] );
            int     sRange  = int.Parse( tokens[tokens.Length-8] );

            counts[0]       = int.Parse( tokens[tokens.Length-6] );
            counts[1]       = int.Parse( tokens[tokens.Length-5] );
            counts[2]       = int.Parse( tokens[tokens.Length-4] );
            counts[3]       = int.Parse( tokens[tokens.Length-3] );
            counts[4]       = int.Parse( tokens[tokens.Length-2] );
            counts[5]       = int.Parse( tokens[tokens.Length-1] );

            for( int i = 0; i < 6; i++ )
            {
                if( counts[i] > 0 ) ProcessSpawnSet( sets[i], x, y, z, hRange, sRange, counts[i], mapName );
            }
    }
    public static void ProcessSpawnSet( 
        string   spawnSet,
        int      x,
        int      y,
        int      z,
        int      hRange,
        int      sRange,
        int      n,
        string   mapName
        )
    {
        Point3D     location = new Point3D( x, y, z );

        Map         mapInstance = null;

        Map[]       maps = Map.Maps;

        foreach( Map map in maps )
        {
            if( map.Name == mapName )
            {
                mapInstance = map;
                break;
            }
        }

        if( mapInstance == null )
        {
            Console.WriteLine("#### RandomEncounters: no map for map nammed \"{0}\".", mapName );
        }

        Dictionary<string,List<ImportRecord>> regionEncounters;

        if( facetEncounters.ContainsKey( mapName ) )
        {
            regionEncounters = facetEncounters[mapName];
        }
        else
        {
           regionEncounters = new Dictionary<string,List<ImportRecord>>();

           facetEncounters.Add( mapName, regionEncounters );    
        }

        Region      region = Region.Find( location, mapInstance );

        if( 
            region != null 
            && 
            region.Name != null 
            && 
            region.Name != "" )
        {
            string[] spawns = spawnSet.Split(':');

            List<ImportRecord> encounterSet;

            if( regionEncounters.ContainsKey( region.Name ) )
            {
                encounterSet = regionEncounters[region.Name];
            }
            else
            {
            /*
                public class ProbabilityComparer : IComparer
                {
                    public int Compare( object o1, object o2 )
                    {
                        ImportRecord r1 = (ImportRecord) o1;
                        ImportRecord r2 = (ImportRecord) o2;

                        if ( r1.P > r2.P ) return -1;
                        if ( r1.P < r2.P ) return 1;

                        else return 0;
                    }
                }
            */
                encounterSet = new List<ImportRecord>( );

                regionEncounters.Add( region.Name, encounterSet );    
            }

            foreach( string spawn in spawns )
            {
                Type        typeObject  = SpawnerType.GetType( spawn );
                object      created     = null;
                double      level       = 0.0;
                double      p           = 0.0;

                if (typeObject == null) 
                {
                    continue;
                }
                try
                {
                    created = Activator.CreateInstance( typeObject );
                }
                catch( Exception e )
                {
                    Console.WriteLine(e);
                    continue; 
                }

                if( created is TownHerald ) continue;
                if( created is BaseVendor ) continue;
                if( created is Mobile )
                {
                    Mobile m = (Mobile) created;

                    bool hostile = (m is Townsperson) ? false : true;

                    if( m is BaseCreature )
                    {
                        BaseCreature c = (BaseCreature) m;

                        switch( c.AI )
                        {
                            case AIType.AI_Healer: { hostile = false; break; }
                            case AIType.AI_Vendor: { hostile = false; break; }
                            case AIType.AI_Animal: { if( c.Fame < 400 ) hostile = false; break; }
                        }
                    }

                    level = Helpers.CalculateLevelForMobile( m, LevelType.Overall );

                    p = 1.0 / Math.Pow( 1 + level, 1.4 );

                    ImportRecord ir = new ImportRecord(
                        (Mobile) created,
                        spawn,
                        x,
                        y,
                        z,
                        hRange,
                        sRange,
                        n,
                        mapName,
                        region.Name,
                        level,
                        hostile,
                        p
                        );

                    //Console.WriteLine("        "+ir);

                    encounterSet.Add( ir );
                }
            }
        }
    }
    public static void DumpXml()
    {
        string          importName = Path.Combine( Core.BaseDirectory, "Data/Import/Premiums/RandomEncounters.xml"  );

        using ( StreamWriter sw = new StreamWriter( importName, false ) )
        {
            sw.NewLine = "\n";

            sw.WriteLine(
                "<RandomEncounters\n"+
                "    skiphidden=\"true\"\n"+
                "    delay=\"60.0\"\n"+
                "    interval=\"600.0:1200.0:1800.0\"\n"+
                "    cleanup=\"1800.0\"\n"+
                "    cleanupGrace=\"0\"\n"+
                "    debug=\"false\"\n"+
                "    debugEffect=\"false\"\n"+
                "    RTFM=\"false\"\n"+
                "    >\n"
                );

            Map[]       maps = Map.Maps;
            foreach( string facet in facetEncounters.Keys )
            {
                Dictionary<string,Region> regions = null;
                foreach( Map map in maps )
                {
                    if( map.Name == facet )
                    {
                        regions = map.Regions;
                        break;
                    }
                }

                sw.WriteLine( "    <Facet name=\"{0}\">",facet );

                Dictionary<string,List<ImportRecord>> regionEncounters = facetEncounters[facet];
                
                foreach( string regionName in regionEncounters.Keys )
                {
                    Region region = regions[regionName];

                    string regionType = Helpers.RegionCategory( region );

                    sw.WriteLine( "        <Region name=\"{0}\" type=\"{1}\">",regionName, regionType );

                    List<ImportRecord> encounterSet = regionEncounters[regionName];

                    encounterSet.Sort( new PSort() );

                    foreach( ImportRecord ir in encounterSet )
                    {
                        double  fameFactor = Math.Pow( ir.Mobile.Fame, .2 );
                        double  level = ir.Level < fameFactor ? ir.Level : fameFactor;

                        int n = 4;
                        if( level > 1 ) n = 3;
                        if( level > 4 )  n = 2;
                        else if( level > 6 ) n = 1;

                        string forceFlag="";

                        if( ir.Hostile ) forceFlag = " forceAttack=\"true\"";

                        sw.WriteLine( "            <Encounter p=\"{0:0.000}\" level=\"{1:0.000}\" distance=\"7:11\">",ir.P, level );
                        sw.WriteLine( "                <Mobile pick=\"{0}\" n=\"1:{1}\"{2}/> <!-- x={3},y={4},z={5} -->", ir.Spawn, n, forceFlag, ir.Location.X, ir.Location.Y, ir.Location.Z);
                        sw.WriteLine( "            </Encounter>" );
                    }

                    sw.WriteLine( "        </Region>" );
                }

                sw.WriteLine( "    </Facet>" );
            }
            sw.WriteLine( "</RandomEncounters>" );
        }
    }

    public class PSort : IComparer<ImportRecord>
    {
        public int Compare( ImportRecord r1, ImportRecord r2 )
        {
            if       ( r1.P > r2.P ) return -1;
            else if  ( r1.P < r2.P ) return 1;
            else     return 0;
        }
    }
}
//--------------------------------------------------------------------------------
}
//--------------------------------------------------------------------------------
