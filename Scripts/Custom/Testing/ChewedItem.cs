using System;
using Server;

namespace Server.Items
{
	public class ChewedItem : Item
	{

        private string m_chew;

        public string Chew
        {
            get{ return m_chew; }
            set{ m_chew = value; }
        }

		[Constructable]
		public ChewedItem() : base( 0xA28 )
		{

			Weight = 1.0;
            m_chew = "";
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if (m_chew != "" )
				list.Add( 1075200, String.Format( "a slimy and disfigured item chewed up by " + m_chew) );
			else
				list.Add( 1075200, String.Format( "a slimy and disfigured item") );
		}

		public ChewedItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( (string) m_chew );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            m_chew = reader.ReadString();
		}
	}
}