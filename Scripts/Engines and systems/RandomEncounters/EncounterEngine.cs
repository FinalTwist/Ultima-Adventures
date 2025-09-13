//------------------------------------------------------------------------------
///  <summary>
///   
///  </summary>
//------------------------------------------------------------------------------
#pragma warning disable 436, 642
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Globalization;
//------------------------------------------------------------------------------
using Server;
using Server.Misc;
using Server.Items;
using Server.Regions;
using Server.Mobiles;
using Server.Targeting;
using Server.Accounting;
using Server.Network;
using Server.Engines.XmlSpawner2;
//------------------------------------------------------------------------------
namespace Server.Misc {
//--------------------------------------------------------------------------
//  Random Encounter Engine: Picks Random Encounters Based on # Users and
//  then by difficulty and probability;
//--------------------------------------------------------------------------
public class RandomEncounterEngine
{
    private static string               m_Picker;
    private static CultureInfo          m_Language;
    private static bool                 m_SkipHidden;
    private static float                m_Delay;
    private static float[]              m_Intervals;
    private static float                m_Cleanup;
    private static int                  m_CleanupGrace;
    private static bool                 m_Debug;
    private static bool                 m_DebugEffect;
    private static Hashtable            m_RegionHash;
    private static ReinitializeTimer    m_ReinitializeTimer;
    private static DeleteTimer          m_DeleteTimer;
    private static EncounterTimer[]     m_EncounterTimers;
    private static string               m_EncountersFile = "./Scripts/Engines and systems/RandomEncounters/RandomEncounters.xml";
	
    //----------------------------------------------------------------------
    public static CultureInfo           Language { get { return m_Language; } }
    public static bool                  SkipHidden { get { return m_SkipHidden; } }
    public static float                 Delay { get { return m_Delay; } }
    public static float                 Cleanup { get { return m_Cleanup; } }
    public static int                   CleanupGrace { get { return m_CleanupGrace; } }
    public static float[]               Intervals { get { return m_Intervals; } }
    public static bool                  Debug { get { return m_Debug; } }
    public static bool                  DebugEffect { get { return m_DebugEffect; } }
    //----------------------------------------------------------------------
    public static void                  Stop() { foreach( EncounterTimer timer in m_EncounterTimers) timer.Stop(); m_DeleteTimer.Stop();}
    public static string                EncountersFile { get { return m_EncountersFile; } }
    //----------------------------------------------------------------------
    // Initialize; this is the engine entry point (called by the core);
    //----------------------------------------------------------------------
    public static void Initialize()
    {
        m_RegionHash = new Hashtable();

        if (MaybeLoadXml())
        {
            //if (m_Debug) DumpAll();

            string intervals = "";

            for( int i=0; i < m_Intervals.Length; i++ )
            {
                if( i!=0 ) intervals += ":";

                intervals += m_Intervals[i];
            }

            Console.WriteLine(
                "RandomEncounters: **WE'RE LIVE, BABY***..."
                );
            Console.WriteLine(
                "    [picker={0} language={1} skiphidden={2} delay={3} intervals={4} cleanup={5} grace={6} debug={7} animateDebug={8}]",
                m_Picker,
                m_Language,
                m_SkipHidden,
                m_Delay,
                intervals,
                m_Cleanup,
                m_CleanupGrace,
                m_Debug,
                m_DebugEffect
                );

            //Dungeon Wilderness Guarded House Jail

            m_EncounterTimers = new EncounterTimer[m_Intervals.Length];

            if( m_Intervals.Length >= 3 )
            {
                m_EncounterTimers[0] = new EncounterTimer( "Dungeon", m_Intervals[0] );
                m_EncounterTimers[1] = new EncounterTimer( "Wilderness", m_Intervals[1] );
                m_EncounterTimers[2] = new EncounterTimer( "Guarded", m_Intervals[2] );
            }
            if ( m_Intervals.Length >= 4 )
            {
                //m_EncounterTimers[3] = new EncounterTimer( "House", m_Intervals[3] );
                m_EncounterTimers[3] = new EncounterTimer( "House", m_Intervals[3] );
            }
            if ( m_Intervals.Length >= 5 )
            {
                m_EncounterTimers[4] = new EncounterTimer( "Jail", m_Intervals[4] );
            }
            if ( m_Intervals.Length == 6 )
            {
                m_EncounterTimers[5] = new EncounterTimer( "Public", m_Intervals[5] );
            }

            foreach( EncounterTimer timer in m_EncounterTimers ) timer.Start();

            if( m_ReinitializeTimer==null )
            {
                m_ReinitializeTimer = new ReinitializeTimer();
                m_ReinitializeTimer.Start();
            }

//        if (cleanupList.Count>0)
//        {
//            DeleteEncounterTimer deleter = new DeleteEncounterTimer( m_Cleanup, cleanupList );
//            deleter.Start();
//        }

            m_DeleteTimer = new DeleteTimer( Cleanup, Cleanup );
            m_DeleteTimer.Start();
        }
        else
        {
            Console.WriteLine("##### RandomEncounters: ***FAILED INITIALIZATION***!");
        }
    }
    //----------------------------------------------------------------------
    // Check to seee if the load file exist, and then attempts the load
    //----------------------------------------------------------------------
    private static bool MaybeLoadXml()
    {
        if (!System.IO.File.Exists( m_EncountersFile )) 
        {
            Console.WriteLine( 
                "\n"+
                "#### RandomEncounters: Did not find the config file at the following location:\n\n"+
                "                       \"{0}\"\n\n"+
                "                       THIS SYSTEM IS MISCONFIGURED!\n\n"+
                "                       Random encounters will NOT work unless you put the RandomEncounters\n"+
                "                       directory into a sub-directory called \"Custom\" in your Scripts directory.\n"+
                "                       You could also edit the m_EncountersFile entry to be something more to your\n"+
                "                       liking... the Scripts/Data directory, perhaps?\n\n"
                , m_EncountersFile );
            return false;
        }

        return LoadXml();
    }
    //----------------------------------------------------------------------
    // Actual XML load-out and node iteration
    //----------------------------------------------------------------------
    private static bool LoadXml()
    {
        XmlLinePreservingDocument     xmlDoc      = new XmlLinePreservingDocument( m_EncountersFile );

        try
        {
            xmlDoc.DoLoad();

            string language     = "en-US";
            string skipHidden   = "true";
            string picker       = "sqrt";
            string delay        = "60";
            string interval     = "1800";
            string cleanup      = "300";
            string cleanupgrace = "1";
            string debug        = "false";
            string debugEffect  = "false";
            string RTFM         = "true";

            //------------------------------------------------------------------
            // Pull out initial information for configuration tags
            //------------------------------------------------------------------

            XmlNode         root = xmlDoc["RandomEncounters"];

            try { language      = root.Attributes[ "language" ].Value; } catch {}
            try { skipHidden    = root.Attributes[ "skiphidden" ].Value; } catch {}
            try { picker        = root.Attributes[ "picker" ].Value; } catch {}
            try { delay         = root.Attributes[ "delay" ].Value; } catch {}
            try { interval      = root.Attributes[ "interval" ].Value; } catch {}
            try { cleanup       = root.Attributes[ "cleanup" ].Value; } catch {}
            try { cleanupgrace  = root.Attributes[ "cleanupGrace" ].Value; } catch {}
            try { debug         = root.Attributes[ "debug" ].Value; } catch {}
            try { debugEffect   = root.Attributes[ "debugEffect" ].Value; } catch {}
            try { RTFM          = root.Attributes[ "RTFM" ].Value; } catch {}

            m_Language          = new CultureInfo( language );
            m_SkipHidden        = bool.Parse( skipHidden );
            m_Picker            = picker;
            m_Delay             = float.Parse( delay, m_Language );
            m_Cleanup           = float.Parse( cleanup, m_Language );
            m_CleanupGrace      = int.Parse( cleanupgrace, m_Language );
            m_Debug             = bool.Parse( debug );
            m_DebugEffect       = bool.Parse( debugEffect );

            bool rtfm = bool.Parse( RTFM );

            if( !rtfm )
            {
                Console.WriteLine(
                    "\n"+
                    "##### RandomEncounters: SYSTEM CONFIG FILE FOUND, HOWEVER IT APPEARS THAT YOU DID NOT ACTUALLY READ IT!\n\n"+
                    "                        RandomEncounters will **NOT** work if you have not read and edited the config file.\n"+
                    "                        It is suggested that you do so now; the file can be found here:\n\n"+
                    "                        \"{0}\"\n",
                    m_EncountersFile
                    );
                
                throw new Exception("RTFM");
            }

            //  Break out our intervals into our acceptible set; careful here, 
            //  changing this to return less than 3 intervals will break other code
            //  Dungeon Wilderness Guarded House Jail
            {
                string[] tokens = interval.Split(new Char []{':'}); 

                int n = tokens.Length < 3 ? 3 : tokens.Length;

                m_Intervals = new float[n];

                if( tokens.Length == 6 )
                {
                    m_Intervals[5] = float.Parse( tokens[5], m_Language ); // Public
                }
                if( tokens.Length >= 5 )
                {
                    m_Intervals[4] = float.Parse( tokens[4], m_Language ); // Jail
                }
                if( tokens.Length >= 4 )
                {
                    m_Intervals[3] = float.Parse( tokens[3], m_Language ); // House
                }
                if( tokens.Length >= 3 )
                {
                    m_Intervals[0] = float.Parse( tokens[0], m_Language ); // Guarded
                    m_Intervals[1] = float.Parse( tokens[1], m_Language ); // Guarded
                    m_Intervals[2] = float.Parse( tokens[2], m_Language ); // Guarded
                }

                if( tokens.Length == 2 )
                {
                    m_Intervals[0] = float.Parse( tokens[0], m_Language ); // Dungeon
                    m_Intervals[1] = float.Parse( tokens[1], m_Language ); // Wilderness
                    m_Intervals[2] = float.Parse( tokens[1], m_Language ); // Guarded
                }

                if( tokens.Length == 1 )
                {
                    m_Intervals[0] = float.Parse( tokens[0], m_Language ); // Dungeon
                    m_Intervals[1] = float.Parse( tokens[0], m_Language ); // Wilderness
                    m_Intervals[2] = float.Parse( tokens[0], m_Language ); // Guarded
                }

            }

            XmlNodeList facetNodes = xmlDoc.GetElementsByTagName("Facet");
            if (facetNodes.Count==0) Console.WriteLine( "RandomEncounters: There appeared to be NO FACETS defined in the configuration file!");
            //------------------------------------------------------------------
            // Iterate over facets
            //------------------------------------------------------------------
            foreach ( XmlNode facetNode in facetNodes )
            {   
                string          facetName = "";

                try { facetName = facetNode.Attributes[ "name" ].Value; }
                catch
                {
                    Console.WriteLine(
                        "RandomEncounters: Facet at {1} had no name. THIS IS ILLEGAL. IGNORED ENTIRE FACET!",
                        facetNode.Attributes[ "lineNumber" ].Value
                        );
                    continue;
                }

                XmlNodeList regionNodes = facetNode.SelectNodes("./Region");
                if (regionNodes.Count==0) Console.WriteLine( 
                    "RandomEncounters: Facet \"{0}\" at {1} had no elements. IGNORED.", 
                    facetName,
                    facetNode.Attributes[ "lineNumber" ].Value
                    );
                //--------------------------------------------------------------
                // Now over regions
                //--------------------------------------------------------------
                foreach ( XmlNode regionNode in regionNodes )
                {
                    string          regionType = "Wilderness";
                    string          regionName = "default";
                    string          failed     = "";

                    try { regionType     = regionNode.Attributes[ "type" ].Value; } catch { failed = "type"; }
                    try { regionName     = regionNode.Attributes[ "name" ].Value; } catch { ; }

                    if (failed!="") 
                    {
                        Console.WriteLine("Attempted to add an element without a {0} at {1}. IGNORING.", 
                            failed,
                            regionNode.Attributes[ "lineNumber" ].Value
                            );
                        return false;
                    }


                    XmlNodeList encounterNodes = regionNode.SelectNodes("./Encounter");
                    if (encounterNodes.Count==0) Console.WriteLine( 
                        "RandomEncounters: {0} Region \"{1}\" at {2} had no elements. IGNORED.", 
                        regionType, 
                        regionName,
                        regionNode.Attributes[ "lineNumber" ].Value
                        );

                    //------------------------------------------------------
                    // Now over encounters
                    //------------------------------------------------------
                    foreach ( XmlNode encounterNode in encounterNodes )
                    {
                        string encounterProbability  = "1.0";
                        string encounterDistance     = "7";
                        string encounterLand         = "AnyLand";
                        string encounterTime         = "AnyTime";
                        string encounterWater        = "false";
                        string encounterLevel        = "1";
                        string encounterScale        = "false";

                        try { encounterDistance     = encounterNode.Attributes[ "distance" ].Value; } catch { }
                        try { encounterProbability  = encounterNode.Attributes[ "p" ].Value; } catch { }
                        try { encounterLand         = encounterNode.Attributes[ "landType" ].Value; } catch { }
                        try { encounterWater        = encounterNode.Attributes[ "water" ].Value; } catch { }
                        try { encounterTime         = encounterNode.Attributes[ "time" ].Value; } catch { }
                        try { encounterLevel        = encounterNode.Attributes[ "level" ].Value; } catch { }
                        try { encounterScale        = encounterNode.Attributes[ "scaleUp" ].Value; } catch { }

                        if( bool.Parse(encounterWater) == true ) 
                        {
                            if (m_Debug) Console.WriteLine(
                                "RandomEncounters, WARNING: \"water\" tag is deprecated; use landType=\"Water\" instead"
                                );
                            encounterLand = "Water";
                        }

                        string[]  distance_tok = encounterDistance.Split(new Char [] {':'}); // splits to shortest:farthest, or just distance if no ':'

                        string[]  level_tok = encounterLevel.Split(new Char[] {':'});

                        RandomEncounter randomEncounter = new RandomEncounter(
                                encounterNode,
                                facetName,
                                regionType,
                                regionName,
                                encounterProbability,
                                distance_tok[0],
                                (distance_tok.Length > 1 ? distance_tok[1] : distance_tok[0]),
                                encounterLand,
                                encounterTime,
                                level_tok[0],
                                (level_tok.Length > 1 ? level_tok[1] : "Overall"),
                                encounterScale
                                );

                        XmlNodeList elementNodes = encounterNode.SelectNodes("Mobile | Item ");

                        if (elementNodes.Count==0) Console.WriteLine( 
                            "RandomEncounters: Encounter on line {0} had no children. IGNORED.", 
                            encounterNode.Attributes[ "lineNumber" ].Value
                            );

                        //----------------------------------------------
                        // Now iterate over subelements
                        //----------------------------------------------
                        foreach ( XmlNode elementNode in elementNodes )
                            AddRecursiveMobilesAndItems( 
                                encounterNode,
                                elementNode,
                                randomEncounter
                                );

                        //------------------------------------------
                        // Now they we've built it up, process it:
                        //------------------------------------------
                        ProcessNewEncounter( randomEncounter);
                    }
                } 
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("RandomEncounters: Exception caught attempting to load file: " + m_EncountersFile);
            Console.WriteLine("{0}", e);
            xmlDoc.Close();
            return false;
        }
    }
    //----------------------------------------------------------------------
    //  Now we recursively decend the Mobile/Item hierarchy, allowing sub
    //  containment (when it makes sense):
    //----------------------------------------------------------------------
    private static void AddRecursiveMobilesAndItems( 
        XmlNode             parentNode,
        XmlNode             childNode,
        IElementContainer   elementContainer
        )
    {   
        bool    reject      = false;
        string  probability = "1.0";
        string  pick        = "";
        string  n           = "1";
        string  id          = "0";
        string  forceAttack = "false";
        string  effectStr   = "None";
        
        try { probability = childNode.Attributes[ "p" ].Value; } catch {}
        try { n = childNode.Attributes[ "n" ].Value; } catch {}
        try { id = childNode.Attributes[ "id" ].Value; } catch {}
        try { forceAttack = childNode.Attributes[ "forceAttack" ].Value; } catch {}
        try { effectStr = childNode.Attributes[ "effect" ].Value; } catch {}

        string[] effectOptions = effectStr.Split(':');
        string effect = effectOptions[0];
        string effectHue = "0";
        if( effectOptions.Length > 1 ) effectHue = effectOptions[1];

        try { pick = childNode.Attributes[ "pick" ].Value; } 
        catch 
        {
            Console.WriteLine("Attempted to add an element without a pick at {0}. IGNORING.", 
                childNode.Attributes[ "lineNumber" ].Value
                );
            return;
        }

        if( parentNode.Name == "Item" && childNode.Name == "Mobile" ) reject = true;

        //------------------------------------------------------------------
        //  I anticpate more rejection reasons; in any case, the message is decoupled 
        //  entirely from the reason...
        //------------------------------------------------------------------
        
        if (reject)
        {
            Console.WriteLine( 
                "RandomEncounters: Tried to add {0} \"{1}\" to {2} at {3}. THIS IS ILLEGAL. IGNORED.", 
                childNode.Name,
                pick,
                parentNode.Name,
                childNode.Attributes[ "lineNumber" ].Value
                );
            return; 
        }

        string[]        tokens = n.Split(new Char [] {':'}); // splits to min:max, or just n if no ':'

        EncounterElement newElement = 
            new EncounterElement(
                childNode,
                probability,
                pick,
                id,
                tokens[0],
                (tokens.Length > 1 ? tokens[1] : tokens[0]),
                forceAttack,
                effect,
                effectHue
                );

        XmlNodeList subNodes = childNode.SelectNodes("Mobile | Item | Prop");

        //----------------------------------------------
        // Now iterate over subelements
        //----------------------------------------------
        foreach ( XmlNode subNode in subNodes )
        {
            if (subNode.Name=="Prop") AddProp( childNode, subNode, newElement );

            else AddRecursiveMobilesAndItems( 
                childNode,
                subNode,
                newElement
                );
        }

        elementContainer.AddElement( newElement );
    }
    //----------------------------------------------------------------------
    //  Record a property modification to an Item or mobile
    //----------------------------------------------------------------------
    private static void AddProp ( XmlNode parentNode, XmlNode childNode, EncounterElement element )
    {
        Console.WriteLine("Adding property to "+parentNode.Name);
    }
    //----------------------------------------------------------------------
    //  Process New Encounter -- this fills up our records database,
    //    per facet, per region, per region name, of probability-organized
    //    enounters to pick from.
    //----------------------------------------------------------------------
    private static void ProcessNewEncounter( RandomEncounter encounter )
    {
        //------------------------------------------------------------------
        //  Previously encountered something pertaining to this facet, region,
        //  and region name...
        //------------------------------------------------------------------
        if( m_RegionHash.Contains( encounter.Key ) )
        {
            RegionRecord region = (RegionRecord) m_RegionHash[ encounter.Key ];

            region.AddEncounter( encounter );
        }
        //------------------------------------------------------------------
        //  Have never encountered anything for this facet, region, and region
        //  name:
        //------------------------------------------------------------------
        else
        {
            RegionRecord region = new RegionRecord( encounter.Key );
        
            region.AddEncounter( encounter );

            m_RegionHash[ encounter.Key ] = region;
        }
    }
    //----------------------------------------------------------------------
    //  DumpAll -- dump all encounter info to the Console
    //----------------------------------------------------------------------
    private static void DumpAll()
    {
        Console.WriteLine( "RandomEncounters: dumping tables:" );
        foreach ( string key in m_RegionHash.Keys )
        {
            RegionRecord region = (RegionRecord) m_RegionHash[ key ];
            region.DumpAll();
        }
    }
    
    //----------------------------------------------------------------------
    // Generate encounters -- where the real encounters are generated
    //----------------------------------------------------------------------
    public static void GenerateEncounters( string timerType )
    {
        ArrayList       playerMobiles       = GenerateLivePlayerMobiles();
        ArrayList       candidateMobiles    = new ArrayList();
        ArrayList       chosenMobiles       = new ArrayList();
        ArrayList       cleanupList         = new ArrayList();

        //------------------------------------------------------------------
        //  First we winnow down the possible mobiles to the set that satisfy the
        //  timers region...
        //------------------------------------------------------------------

        foreach (Mobile playerMobile in playerMobiles)
        {
            Region          currentRegion = playerMobile.Region;
            string          str;

            str = Helpers.RegionCategory( currentRegion );

            if( str != timerType ) continue;

            if( m_Debug ) 
                Console.WriteLine( 
                    "RandomEncounters: Assigning player \"{0}, map=\"{1}\" to candidate set for regionType={2}, regionName=\"{3}\" ... ", 
                    playerMobile.Name, playerMobile.Map.Name, str, currentRegion.Name 
                    );

            candidateMobiles.Add( playerMobile );
        }

        int nPlayersInRegion = candidateMobiles.Count;

        if( nPlayersInRegion == 0 ) 
        {
            //Console.WriteLine("RandomEncounters: No one in region for timerType:" timerType);
            return; // none for this region timer...
        }

        //------------------------------------------------------------------
        //  Now we narrow the set further based on the picking method:
        //------------------------------------------------------------------

        if( m_Picker=="sqrt" )
        {
            int             nEncounterChecks= (int) Math.Sqrt( chosenMobiles.Count );

            for( int i=0; i < candidateMobiles.Count; i++ )
            {
                int         chosen          = Utility.RandomMinMax( 0, candidateMobiles.Count-1 );
                Mobile      chosenMobile    = (Mobile) candidateMobiles[chosen];

                candidateMobiles.RemoveAt( chosen );
                chosenMobiles.Add( chosenMobile );
            }
        }

        else
        {
            chosenMobiles = candidateMobiles;
        }

        if (m_Debug) Console.WriteLine(
            "RandomEncounters: Generating {0} encounter checks for {1} players in {2} region...", 
            chosenMobiles.Count,
            nPlayersInRegion,
            timerType
            );

        foreach (PlayerMobile chosenMobile in chosenMobiles)
        {
            Region              currentRegion       = chosenMobile.Region;
            string              regionName          = currentRegion.Name;

            EncounterTime       encounterTime       = Helpers.DetermineTimeForMobile( chosenMobile );
            LandType            landType            = Helpers.DetermineLandTypeForMobile( chosenMobile );

            if( regionName == "" ) regionName = "default";
                
            if( m_Debug ) Console.WriteLine( "RandomEncounters: Picking encounter for player=\"{0}, map=\"{1}\", region=\"{2}\" ... ", 
                chosenMobile.Name, 
                chosenMobile.Map.Name,
                regionName
                );

            //------------------------------------------------------------------------------------------------------
            //  Search for matches in specific region; check default region if non in specific region
            //------------------------------------------------------------------------------------------------------

            string[] keys1 = new string[3] 
            {
                chosenMobile.Map.Name + ":" + timerType + ":" + regionName + ":" + landType         + ":" + encounterTime,
                chosenMobile.Map.Name + ":" + timerType + ":" + regionName + ":" + landType         + ":" + EncounterTime.AnyTime,
                chosenMobile.Map.Name + ":" + timerType + ":" + regionName + ":" + LandType.AnyLand + ":" + EncounterTime.AnyTime,
            };

            RedBlackTree        possibleEncounters = null;

            foreach( string key in keys1 )
            {
                //Console.WriteLine("Searching for region key: "+key);  Final, this was too much
                if ( m_RegionHash.Contains( key ) ) //  Search to for match against named region;
                {
                    possibleEncounters = ((RegionRecord) m_RegionHash[ key ]).PossibleEncounters;
                    Console.WriteLine("Found match for key: {0}, with value: {1}", key, possibleEncounters);
                    break;
                }
            }

            //------------------------------------------------------------------------------------------------------
            //  Check general regions only if both we did not find a specific match, and the area searched was not
            //     already general because the player was in no specific region
            //------------------------------------------------------------------------------------------------------

            if( possibleEncounters == null && regionName != "default" )
            {
                string[] keys2 = new string[3] 
                {
                    chosenMobile.Map.Name + ":" + timerType + ":" + "default"  + ":" + landType         + ":" + encounterTime,
                    chosenMobile.Map.Name + ":" + timerType + ":" + "default"  + ":" + landType         + ":" + EncounterTime.AnyTime,
                    chosenMobile.Map.Name + ":" + timerType + ":" + "default"  + ":" + LandType.AnyLand + ":" + EncounterTime.AnyTime,
                };

                foreach( string key in keys2 )
                {
                    //Console.WriteLine("Searching for region key: "+key);
                    if ( m_RegionHash.Contains( key ) ) //  Search to for match against named region;
                    {
                        Console.WriteLine("Found match for key: "+key);
                        possibleEncounters = ((RegionRecord) m_RegionHash[ key ]).PossibleEncounters;

                        break;
                    }
                }
            }

            if( possibleEncounters == null ) continue;

            double draw = Utility.RandomDouble();

            EncounterSet inclusiveSet = null;

            foreach( EncounterSet encounterSet in possibleEncounters )
            {
                if( encounterSet.Probability == -1 )
                {
                    inclusiveSet = encounterSet;
                    continue;
                }
                if ( draw <= encounterSet.Probability ) 
                {
                    int choice = 0;

                    //  account for multiple encounters of the same probability; the set exists to
                    //  give each matching p an equal chance of being chosen
                    
                    if (encounterSet.Count!=0) { choice = Utility.RandomMinMax( 0, encounterSet.Count-1 ); }

                    RandomEncounter encounter = (RandomEncounter) encounterSet[choice];

                    double playerLevel = Helpers.CalculateLevelForMobile( chosenMobile, encounter.LevelType);

                    if( encounter.Level <= playerLevel )
                    {
                        Console.WriteLine("Encounter Level "+encounter.LevelType+" <= Player Level "+playerLevel );

                        GenerateEncounter( chosenMobile, encounter, cleanupList );

                        break;
                    }
                    else
                        Console.WriteLine("Encounter Level "+encounter.LevelType+" > Player Level "+playerLevel );
                }
            }

            if( inclusiveSet != null )
            foreach( RandomEncounter encounter in inclusiveSet )
            {
                double playerLevel = Helpers.CalculateLevelForMobile( chosenMobile, encounter.LevelType);

                if( encounter.Level <= playerLevel )
                {
                    Console.WriteLine("Encounter Level "+encounter.LevelType+" <= Player Level "+playerLevel );

                    GenerateEncounter( chosenMobile, encounter, cleanupList );
                }
                    Console.WriteLine("Encounter Level "+encounter.LevelType+" > Player Level "+playerLevel );
            }
        }

//        if (cleanupList.Count>0)
//        {
//            DeleteEncounterTimer deleter = new DeleteEncounterTimer( m_Cleanup, cleanupList );
//            deleter.Start();
//        }
    }
    //----------------------------------------------------------------------
    //  GenerateEncounter -- this takes an encounter description and
    //     creates an actual encounter. Since our random draw in the previous
    //     section means that we got here, all that's left to do is narrow
    //     down the actual encounter template to a definitive description
    //     (this converts min-max ranges to actuals), plot out positions
    //     for the various elements, create their Mobiles/Items, and go.
    //----------------------------------------------------------------------
    private static void GenerateEncounter( 
        PlayerMobile playerMobile, 
        RandomEncounter encounterTemplate, 
        ArrayList cleanupList 
        )
    {
        RandomEncounter     encounter       = encounterTemplate.Pick();
        Map                 map             = playerMobile.Map;
        Point3D             location        = new Point3D( playerMobile.X, playerMobile.Y, playerMobile.Z );
        Point3D             spawnPoint      = new Point3D( );

        if( m_Debug )
        {
            Console.WriteLine( "RandomEncounters: generating instances: {0} ...", encounter);
            DumpEncounter( 1, encounter );
        }

// test code
//            Tile        playerTile  = map.Tiles.GetLandTile( location.X, location.Y );
//            Tile[]      staticTiles = map.Tiles.GetStaticTiles( location.X, location.Y, true );
//
//            Console.WriteLine("Player Location: {0}", location );
//            Console.WriteLine("    Land Tile id={0} z={1} height={2}: ", playerTile.ID, playerTile.Z, playerTile.Height );
//
//            uint flags = (uint) TileData.LandTable[playerTile.ID & 0x3FFF].Flags;
//            Console.WriteLine("    Land Flags: {0:x}", flags );
//
//            foreach( Tile staticTile in staticTiles )
//            {
//                Console.WriteLine("    Static Tile id={0} z={1} height={2}", staticTile.ID, staticTile.Z, staticTile.Height );
//                flags = (uint) TileData.LandTable[staticTile.ID & 0x3FFF].Flags;
//                Console.WriteLine("    Static Flags: {0:x}", flags );
//            }

        foreach( EncounterElement element in encounter.Elements )
        {
            bool running = ( ( playerMobile.Direction &  Direction.Running ) > 0 );

            bool foundQuadrant = false;

            if( running )
            {
                foundQuadrant = SpawnFinder.FindAhead(
                  playerMobile,
                  location,
                  ref spawnPoint,
                  encounter.LandType,
                  encounter.Distance,
                  element.Effect,
                  element.EffectHue
                  );
            }

            if( !foundQuadrant )
                if( !SpawnFinder.FindInwards( 
                      playerMobile,
                      location,
                      ref spawnPoint,
                      encounter.LandType,
                      encounter.Distance,
                      element.Effect,
                      element.EffectHue
                      )
                  )
                {
                    //----------------------------------------------------------
                    // if we can't find a location, there's no chance we'll find any others: 
                    // STOP! do not attempt to add any more elements to the encounter, there
                    // simply is NO SPACE LEFT.
                    //----------------------------------------------------------
                    // Console.WriteLine("NO SPAWN LOCATION AVAILABLE");
                    return; 
                }

            GenerateEncounterRecursive(
                map,
                playerMobile,
                spawnPoint,
                cleanupList,
                encounterTemplate,
                element,
                null
                );
        }
    }
    private static void GenerateEncounterRecursive( 
        Map                     map,
        PlayerMobile            playerMobile,
        Point3D                 parentLocation,
        ArrayList               cleanupList,
        RandomEncounter         encounterTemplate,
        EncounterElement        parentElement,
        object                  parentObject
        )
    {
        Type        typeObject  = SpawnerType.GetType( parentElement.PickFrom );

        if (typeObject == null) 
        {
            Console.WriteLine( "RandomEncounters: Bad attempt to generate an element of type \"{0}\".", parentElement.PickFrom );
            return;
        }

        Point3D     spawnPoint;

        if( parentObject == null ) spawnPoint = parentLocation;

        else
        {   
            spawnPoint = new Point3D();
            if( 
                !SpawnFinder.FindOutwards( 
                    playerMobile,
                    parentLocation,
                    ref spawnPoint,
                    encounterTemplate.LandType,
                    1,
                    parentElement.Effect,
                    parentElement.EffectHue
                    )
              )
            {
                //Console.WriteLine("NO SPAWN LOCATION AVAILABLE");
                return;
            }
        }

        object  created;

        try
        {
            created = Activator.CreateInstance( typeObject );
        }
        catch( Exception e )
            //--------------------------------------------------------------
            //  most likely reason that we may fail is an attempt to construct an object
            //  that requires arguments in its constructor; here we'll help the user
            //  diagnose their problem, but otherwise ignore this part of the encounter
            //  without terminating:
            //--------------------------------------------------------------
        {
            string line = parentElement.XmlNode.Attributes[ "lineNumber" ].Value;
            Console.WriteLine( "RandomEncounters: Bad attempt to contruct an element of type \"{0}\", on line {1}", parentElement.PickFrom, line );
            Console.WriteLine("{0}", e);
            return; // go no deeper, we're done here, stop recursing
        }
   
        if ( created is Mobile )
        {
            Mobile          m = (Mobile) created;

            XmlAttach.AttachTo( m, new XmlDateCount( "RandomEncountersCreated" ));

            //ObjectPropertyList props = m.PropertyList;

            cleanupList.Add(m);

            //here's where we might add monster to its own party, if it had one
            //if( parentObject!=null && parentObject is Mobile )
            //{
            //    Mobile      parentMobile = (Mobile) parentObject;
            //}

            m.OnBeforeSpawn( spawnPoint, map );

            m.MoveToWorld( spawnPoint, map );

            if ( m is BaseCreature )
            {
                BaseCreature c = (BaseCreature) m;

                if( c.IsEnemy( playerMobile ) )
                {
                    //  deprecated now with the configurable forceAttack option
                    // if( c.AI == AIType.AI_Animal )
                    // {
                    //    if( c.Fame > 350 )
                    //    {
                    //        c.Combatant = playerMobile;
                    //    }
                   //  }
                    if( c.AI ==AIType.AI_Vendor );
                    else if( parentElement.ForceAttack ) c.Combatant = playerMobile;
                }

                c.Home = spawnPoint;
            }

            m.OnAfterSpawn();

            if( encounterTemplate.ScaleUp )
            {
                double  pLevel   = Helpers.CalculateLevelForMobile( playerMobile, LevelType.Fighter );

                double  mLevel   = Helpers.CalculateLevelForMobile( m, LevelType.Overall );

                double  ratio    = pLevel / mLevel;

                int     scaleUp  = (int)(ratio - .5);

                if( scaleUp > 2 ) scaleUp = 2;

                //--------------------------------------------------------------
                //  Handle add-ons from level based scaling here:
                //--------------------------------------------------------------

                if( scaleUp > 0 )
                for( int i=0; i<scaleUp; i++ )
                {   
                    Point3D addonPoint = new Point3D();

                    if( 
                        !SpawnFinder.FindOutwards( 
                            playerMobile,
                            parentLocation,
                            ref addonPoint,
                            encounterTemplate.LandType,
                            1,
                            parentElement.Effect,
                            parentElement.EffectHue
                            )
                      )
                    {
                        return;
                    }

                    Mobile addonMob = (Mobile) Activator.CreateInstance( typeObject );

                    XmlAttach.AttachTo( addonMob, new XmlDateCount( "RandomEncountersCreated" ));

                    cleanupList.Add( addonMob );

                    addonMob.OnBeforeSpawn( addonPoint, map );

                    addonMob.MoveToWorld( addonPoint, map );

                    if ( addonMob is BaseCreature )
                    {
                        BaseCreature c = (BaseCreature) addonMob;

                        if( c.IsEnemy( playerMobile ) )
                        {
                            //if( c.AI == AIType.AI_Animal )
                            //{
                            //    if( c.Fame > 350 ) c.Combatant = playerMobile;
                            //}
                            if( c.AI ==AIType.AI_Vendor );
                            else if( parentElement.ForceAttack ) c.Combatant = playerMobile;
                        }

                        c.Home = addonPoint;
                    }

                    addonMob.OnAfterSpawn();
                }
            }
        }
        else if ( created is Item )
        {
            Item          item = (Item) created;

            //ObjectPropertyList props = item.PropertyList;

            //if( !item.Movable) XmlAttach.AttachTo( item, new XmlDate( "RandomEncountersCreated" ));
            XmlAttach.AttachTo( item, new XmlDateCount( "RandomEncountersCreated" ));

            item.OnBeforeSpawn( spawnPoint, map );

            //--------------------------------------------------------------
            // some core items won't decay, but we need them to:
            // the code is in the core, and we don't want to modify that,
            // so we will do it here instead
            //--------------------------------------------------------------

            if ( !item.Movable ) 
            {
                cleanupList.Add(created);
            }

            if( parentObject!=null)
            {
                if( parentObject is Mobile )
                {
                    Mobile  mobileParent = (Mobile) parentObject;
                    //Console.WriteLine( "RandomEncounters: added item to mobile: " + item );

                    mobileParent.AddItem( item );
                }
                else if( parentObject is Container )
                {
                    Container  itemParent = (Container) parentObject;

                    //Console.WriteLine( "RandomEncounters: added item to container: " + item );
                    itemParent.AddItem( item );
                }
                else
                {
                    item.MoveToWorld( spawnPoint, map );
                    //Console.WriteLine( "RandomEncounters: inserted item into world: " + item );
                }
            }
            else
            {
                item.MoveToWorld( spawnPoint, map );
                //Console.WriteLine( "RandomEncounters: inserted item into world: " + item );
            }

            item.OnAfterSpawn();
        }
        foreach( EncounterElement childElement in parentElement.Elements )
        {
            GenerateEncounterRecursive( 
                map,
                playerMobile,
                spawnPoint,
                cleanupList,
                encounterTemplate,
                childElement,
                created
                );
        }
    }
    //----------------------------------------------------------------------
    //  Dump out text for debugging information
    //----------------------------------------------------------------------
    internal static void DumpEncounter( int depth, RandomEncounter encounter )
    {
        string prepend = "";
        for( int i=0; i<depth; i++) prepend+="    ";

        Console.WriteLine( "{0}{1}", prepend, encounter);

        foreach ( EncounterElement element in encounter.Elements )
        {
            DumpElementRecursive( 2, element );
        }
    }
    //----------------------------------------------------------------------
    //  Dump out text for debugging information -- recursive version
    //----------------------------------------------------------------------
    internal static void DumpElementRecursive( int depth, EncounterElement element )
    {
        string prepend = "";
        for( int i=0; i<depth; i++) prepend+="    ";

        Console.WriteLine( "{0}{1}", prepend, element);
        foreach ( EncounterElement childElement in element.Elements )
        {
            DumpElementRecursive( depth+1, childElement );
        }
    }
    //----------------------------------------------------------------------
    //  GenerateLivePlayerMobiles - who's online?
    //----------------------------------------------------------------------
    public static ArrayList GenerateLivePlayerMobiles()
    {
        ArrayList mobiles = new ArrayList();
        
        foreach ( NetState state in NetState.Instances ) 
        {
            if ( state.Mobile is PlayerMobile )
            {
                Mobile          mobile          = state.Mobile;
                //PlayerMobile    pm              = mobile as PlayerMobile;
                AccessLevel     accessLevel     = mobile.AccessLevel;

                if (accessLevel != AccessLevel.Player)                  continue; // staff doesn't qualify
                if (!mobile.Alive)                                      continue; // no randoms for dead players
                //if (pm!=null && pm.Young)                               continue; // young don't qualify
                if (RandomEncounterEngine.SkipHidden && mobile.Hidden)  continue; // skip hidden players

                mobiles.Add ( state.Mobile );
            }
        }

        return mobiles;
    }
    //----------------------------------------------------------------------
    // Place each value of the road tile range set into a master hash table
    //   for rapid lookup.
    //----------------------------------------------------------------------
    private static Hashtable GenerateRoadTileHash()
    {
        Hashtable roadTiles = new Hashtable();

        for( int i = 0; i < m_RoadTileIds.Length; i += 2 )
        {
            for( int j = m_RoadTileIds[i]; j < m_RoadTileIds[i+1]; j++ )
                roadTiles[j]=j;
        }

        return roadTiles;
    }
    //----------------------------------------------------------------------
    //
    //----------------------------------------------------------------------
    private static int[] m_RoadTileIds = new int[]
    {
        0x071, 0x08C,
        0x0E8, 0x0EB,
        0x14C, 0x14F,
        0x161, 0x174,
        0x1F0, 0x1F3,
        0x26E, 0x279,
        0x27E, 0x281,
        0x324, 0x3AC,
        0x547, 0x556,
        0x597, 0x5A6,
        0x637, 0x63A,
        0x7AE, 0x7B1,
        0x442, 0x479, // Sand stones
        0x501, 0x510, // Sand stones
        0x009, 0x015, // Furrows
        0x150, 0x15C  // Furrows 
    };
    //----------------------------------------------------------------------
    // Import Nerun's/premium spawners from Data/Import/Premiums
    //----------------------------------------------------------------------
}
//------------------------------------------------------------------------------
} // namespace Server.Misc
//------------------------------------------------------------------------------
