//------------------------------------------------------------------------------
///  <summary>
///   
///  </summary>
//------------------------------------------------------------------------------
using System;
//------------------------------------------------------------------------------
using Server;
using Server.Mobiles;
using Server.Items;
//------------------------------------------------------------------------------
namespace System.Xml
{
    //--------------------------------------------------------------------------
    //  Hack of the base MS DOM to force line,char pos into the elements parsed
    //    from the XML being read; useful for printing out debugging info when
    //    we want to avoid xml validation (which we do).
    //--------------------------------------------------------------------------
    public class XmlLinePreservingDocument : XmlDocument
    {
        private XmlTextReader   xmlReader;

        public XmlLinePreservingDocument( string url )
        {
            xmlReader = new XmlTextReader( url );
        }

        public void DoLoad( )
        { 
            Load( xmlReader ); 
            xmlReader.Close();
        }

        public void Close( )
        { 
            xmlReader.Close();
        }

        //----------------------------------------------------------------------
        //  Our basic trick is to declare our own reader, override the xml element
        //  creator, and push a vanilla variety attribute onto each element as we
        //  create it, containg line, char position information. Kind of hacky,
        //  but concise and usable without gymnastics.
        //----------------------------------------------------------------------

        public override XmlElement CreateElement( string prefix, string localName, string nsURI )
        { 
            XmlElement newElement = base.CreateElement( prefix, localName, nsURI );
            newElement.SetAttribute( "lineNumber", "("+xmlReader.LineNumber+","+xmlReader.LinePosition+")");
            return newElement;
        }
    }
}
