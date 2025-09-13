//------------------------------------------------------------------------------
///  <summary>
///   
///  </summary>
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
namespace Server.Misc
{
    //--------------------------------------------------------------------------
    //  Both encounters and elements can have elements:
    //--------------------------------------------------------------------------
    public interface IElementContainer 
    {
        void                        AddElement ( EncounterElement element );
        ArrayList                   Elements { get; }
    }
    //--------------------------------------------------------------------------
    //  Random Encounter : We'll use this later to store our random
    //  encounter templates (read from XML)
    //--------------------------------------------------------------------------
    public class RandomEncounter : IElementContainer
    {
        private XmlNode             m_XmlNode;         // e.g., "Mobile"
        private string              m_Facet;           // e.g., "Felucca"
        private string              m_RegionType;      // e.g., "Guarded"
        private string              m_RegionName;      // e.g., "Minoc"
        private float               m_Probability;     // 0.0-1.0, where 1.0==100%
        private int                 m_Shortest;        // minimum range from player
        private int                 m_Farthest;        // maximum range from player
        private int                 m_Distance;        // actual range from player
        private LandType            m_LandType;        // encounter on a road T/F
        private EncounterTime       m_EncounterTime;   // encounter at particular time Day, Night, Any
        private double              m_Level;           // encounter on a road T/F
        private LevelType           m_LevelType;       // encounter on a road T/F
        private bool                m_ScaleUp;         // encounter on a road T/F
        private bool                m_Inclusive;       // encounter on a road T/F
        private ArrayList           m_Elements;        // list of things in the encounter...
        //--------------------------------------------------------------------- 
        public XmlNode              XmlNode { get { return m_XmlNode; } }
        public string               Key { get { return m_Facet + ":" + m_RegionType + ":" + m_RegionName + ":" + m_LandType + ":" + m_EncounterTime; } }
        public string               Facet { get { return m_Facet; } }
        public string               RegionType { get { return m_RegionType; } }
        public string               RegionName { get { return m_RegionName; } }
        public float                Probability { get { return m_Probability; } }
        public int                  Distance { get { return m_Distance; } }
        public LandType             LandType { get { return m_LandType; } }
        public EncounterTime        EncounterTime { get { return m_EncounterTime; } }
        public double               Level { get { return m_Level; } }
        public LevelType            LevelType { get { return m_LevelType; } }
        public bool                 ScaleUp { get { return m_ScaleUp; } }
        public bool                 Inclusive { get { return m_Inclusive; } }
        public ArrayList            Elements { get { return m_Elements; } }
        //--------------------------------------------------------------------- 
        //  Ctor
        //--------------------------------------------------------------------- 
        public RandomEncounter( 
            XmlNode node, 
            string facet, 
            string regionType, 
            string regionName, 
            string probability, 
            string shortest, 
            string farthest, 
            string landType,
            string time,
            string level,
            string levelType,
            string scaleUp
            )
        {
            m_XmlNode       = node;
            m_Facet         = facet;
            m_RegionType    = regionType;
            m_RegionName    = regionName;

            if( probability=="*" )
            { 
                m_Probability = -1; 
                m_Inclusive = true;
            }

            else
            { 
                m_Probability   = float.Parse( probability, RandomEncounterEngine.Language );
                m_Inclusive = false;
            }

            m_Shortest      = int.Parse( shortest );
            m_Farthest      = int.Parse( farthest );
            m_LandType      = (LandType) Enum.Parse( typeof( LandType ), landType );
            m_EncounterTime = (EncounterTime) Enum.Parse( typeof( EncounterTime ), time );
            m_Level         = double.Parse( level );
            m_LevelType     = (LevelType) Enum.Parse( typeof( LevelType ), levelType );
            m_ScaleUp       = bool.Parse( scaleUp );
            m_Elements      = new ArrayList();
        }
        //--------------------------------------------------------------------- 
        public void AddElement( EncounterElement element )
        {
            m_Elements.Add( element );
        }
        //----------------------------------------------------------------------
        //  Pick() -- this takes an encounter description and creates a real
        //            instance. The primary purpose is to take the min/max
        //            ranges of the elements and convert them into a specific
        //            count -- nothing fancy. )
        //----------------------------------------------------------------------
        public RandomEncounter Pick( )
        {
            RandomEncounter         actualEncounter = new RandomEncounter( 
                                        m_XmlNode,
                                        m_Facet,
                                        m_RegionType,
                                        m_RegionName,
                                        m_Inclusive ? "*" : m_Probability.ToString(),
                                        m_Shortest.ToString(),
                                        m_Farthest.ToString(),
                                        m_LandType.ToString(),
                                        m_EncounterTime.ToString(),
                                        m_Level.ToString(),
                                        m_LevelType.ToString(),
                                        m_ScaleUp.ToString()
                                    );

            actualEncounter.m_Distance = Utility.RandomMinMax( m_Shortest, m_Farthest );
            
            foreach( EncounterElement element in m_Elements )
            {
                ArrayList pickedElements = element.Pick();
                
                foreach( EncounterElement pickedElement in pickedElements )
                {
                    actualEncounter.m_Elements.Add( pickedElement );
                }
            }

            return actualEncounter;
        }
        //--------------------------------------------------------------------- 
        public override string ToString()
        {
            return Key + ":p=" + m_Probability.ToString() + ":level=" + m_Level + ":class=" + m_LevelType + ":scaleUp=" + m_ScaleUp + ":distance=(" + m_Shortest + ":" + m_Farthest + "=" + m_Distance + ")" + ":at=" + m_XmlNode.Attributes[ "lineNumber" ].Value;
        }
    }
    //--------------------------------------------------------------------------
    //  Both encounters and elements can have probability:
    //--------------------------------------------------------------------------
    public interface IProbability
    {
        float Probability{get;}
    }
    //--------------------------------------------------------------------------
    //  An "EncounterSet" is a set of encounters at equal probability. This
    //     exists so that we can collect and randomly pick from encounters at
    //     the same probability within the same region...
    //--------------------------------------------------------------------------
    public class EncounterSet : ArrayList, IComparable, IProbability
    {
        private float m_Probability;

        public float Probability { get { return m_Probability; } }

        public EncounterSet( float probability ) : base () 
        {
            m_Probability = probability;
        }
        //--------------------------------------------------------------------- 
        //  This CompareTo() method will be used later by the RedBlackTree
        //  container; it will use this method to automatically keep everything
        //  ordered by probability (we'll need this ordering later when we make
        //  a random draw, and try to decide which encounter (if any) to give
        //  a particular player
        //--------------------------------------------------------------------- 
        public int CompareTo( object obj )
        {
            IProbability toCompare = (IProbability) obj;

            if ( m_Probability < toCompare.Probability ) return -1;
            if ( m_Probability > toCompare.Probability ) return 1;
            else return 0;
        }
        //--------------------------------------------------------------------- 
        //  So we don't have to generate an array to search the tree; just a
        //  hack for the persnickety.
        //--------------------------------------------------------------------- 
        public class QuickSearch : IComparable, IProbability
        {
            private float m_Probability;

            public float Probability { get { return m_Probability; } }

            public QuickSearch( float probability )
            {
                m_Probability = probability;
            }
            public int CompareTo( object obj )
            {
                IProbability toCompare = (IProbability) obj;

                if ( m_Probability < toCompare.Probability ) return -1;
                if ( m_Probability > toCompare.Probability ) return 1;
                else return 0;
            }
        }
    }
    //--------------------------------------------------------------------------
    //  Random Encounter Element : Each encounter might include several descriptions
    //   of mobiles and items... this is an "element"
    //--------------------------------------------------------------------------
    public class EncounterElement : IElementContainer
    {
        private XmlNode             m_XmlNode;      // e.g., "Mobile"
        private float               m_Probability;  // 0.0-1.0, where 1.0==100%
        private int                 m_Min, m_Max;   // i.e., the range of possible N
        private int                 m_N;            // actual is for the draw
        private string              m_PickFrom;     // e.g., "OrcCaptain"
        private int                 m_ID;           // numeric: items only
        private bool                m_ForceAttack;  // force mob to attack player
        private EffectType          m_Effect;       // 
        private int                 m_EffectHue;
        private ArrayList           m_Elements;     // list of things in the encounter...
        //----------------------------------------------------------------------
        public XmlNode              XmlNode { get { return m_XmlNode; } }
        public float                Probability { get { return m_Probability; } }
        public int                  N { get { return m_N; } }
        public string               PickFrom { get { return m_PickFrom; } }
        public int                  ID { get { return m_ID; } }
        public bool                 ForceAttack { get { return m_ForceAttack; } }
        public EffectType           Effect { get { return m_Effect; } }
        public int                  EffectHue { get { return m_EffectHue; } }
        public ArrayList            Elements { get { return m_Elements; } }
        //----------------------------------------------------------------------
        //  Ctor
        //----------------------------------------------------------------------
        public EncounterElement( XmlNode xmlNode, string probability, string pickFrom, string id, string min, string max, string force, string effect, string effectHue )
        {
            m_XmlNode       = xmlNode;
            m_Probability   = float.Parse( probability, RandomEncounterEngine.Language );
            m_PickFrom      = pickFrom;
            m_ID            = int.Parse( id );
            m_Min           = int.Parse( min );
            m_Max           = int.Parse( max );
            m_ForceAttack   = bool.Parse( force );
            m_Effect        = (EffectType) Enum.Parse( typeof( EffectType ), effect );
            m_EffectHue     = int.Parse( effectHue );
            m_Elements      = new ArrayList();
        }
        //--------------------------------------------------------------------- 
        public void AddElement( EncounterElement element )
        {
            m_Elements.Add( element );
        }
        //----------------------------------------------------------------------
        //  Pick() -- this takes an element description and returns a copy with
        //            its min-max range converted to an actual; nothing big here
        //----------------------------------------------------------------------
        public ArrayList Pick()
        {
            ArrayList   pickedElements  = new ArrayList();

            //-----------------------------------------------------------------  
            // for elements that may not be present (indicated by a p value),
            // then we check p, not on a per-n basis, but rather for the whole
            // set:
            //-----------------------------------------------------------------  

            if( Utility.RandomDouble() > Probability) return pickedElements; // empty list okay

            int       n         = Utility.RandomMinMax( m_Min, m_Max );
            string[]  picks     = m_PickFrom.Split(new Char [] {','}); 
            string    pick      ="";

            for( int i=0; i<n; i++ )
            {
                if (picks.Length==1) pick = picks[0];
                else if (picks.Length>=2) pick = picks[Utility.RandomMinMax( 0, picks.Length-1)];

                EncounterElement  pickedElement = 
                    new EncounterElement( 
                        m_XmlNode,
                        "1",
                        pick,
                        m_ID.ToString(),
                        m_Min.ToString(),
                        m_Max.ToString(),
                        m_ForceAttack.ToString(),
                        m_Effect.ToString(),
                        m_EffectHue.ToString()
                        );

                pickedElement.m_N = n;

                foreach( EncounterElement childElement in m_Elements )
                {
                    ArrayList pickedChildElements = childElement.Pick();
                    foreach( EncounterElement pickedChildElement in pickedChildElements )
                    {
                        pickedElement.m_Elements.Add( pickedChildElement );
                    }
                }

                pickedElements.Add( pickedElement );
            }

            return pickedElements; // empty list okay
        }
        //----------------------------------------------------------------------
        public override string ToString()
        {
            return m_XmlNode.Name + ":p=" + m_Probability + ":pick=" + m_PickFrom + ":id=" + m_ID + ":at=" + m_XmlNode.Attributes[ "lineNumber" ].Value + ":min=" + m_Min + ":max=" + m_Max + ":n=" + m_N + ":force=" + m_ForceAttack ;
        }
    }
    //--------------------------------------------------------------------------
    //  RegionRecord -- a place to hang encounters, the per region timers, and
    //       so forth.
    //--------------------------------------------------------------------------
    public class RegionRecord
    {
        private string                      m_Key;
        private RedBlackTree                m_PossibleEncounters;

        public string                       Key { get { return m_Key; } }
        public RedBlackTree                 PossibleEncounters { get { return m_PossibleEncounters; } }
        public void                         Clear() { m_PossibleEncounters.Clear(); }

        public RegionRecord( string key )
        {
            m_Key                   = key;
            m_PossibleEncounters    = new RedBlackTree();
        }

        public void AddEncounter( RandomEncounter encounter )
        {
            object o = m_PossibleEncounters.Find( new EncounterSet.QuickSearch( encounter.Probability ));

            EncounterSet encounterSet;

            if (o==null)
            {
                encounterSet = new EncounterSet( encounter.Probability );
                m_PossibleEncounters.Add( encounterSet );
            }
            else
            {
                encounterSet = (EncounterSet) o;
            }

            encounterSet.Add( encounter );
        }

        public void DumpAll()
        {
            foreach ( EncounterSet encounterSet in m_PossibleEncounters )
                foreach ( RandomEncounter encounter in encounterSet )
                    RandomEncounterEngine.DumpEncounter( 1, encounter );
        }

    }
}
