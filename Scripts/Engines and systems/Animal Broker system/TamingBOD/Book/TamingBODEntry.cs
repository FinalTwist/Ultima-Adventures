using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class TamingBODEntry : IComparable //#01
	{

		public int CompareTo( object obj )
		{
			int ok = 5;
			return ok;
		}

		private int reward;
		private int m_amount;
		private int m_tamed;
		
		public int Reward
		{
			get{ return reward; }
			set{ reward = value; }
		}
		
		public int AmountToTame
		{
			get{ return m_amount; }
			set{ m_amount = value; }
		}
		
		public int AmountTamed
		{
			get{ return m_tamed; }
			set{ m_tamed = value; }
		}
		
		public TamingBODEntry( int ak, int atk, int gpreward )
		{
			AmountToTame = atk;
			AmountTamed = ak;
			Reward = gpreward;
		}
		
		
		public TamingBODEntry( GenericReader reader )
		{ 
			Deserialize( reader );
		} 

		public void Serialize( GenericWriter writer ) 
		{ 
			writer.Write( (int) 0 ); // version 

			writer.Write( reward );
			writer.Write( m_amount );
			writer.Write( m_tamed );
		}

		public void Deserialize( GenericReader reader ) 
		{ 
			int version = reader.ReadInt(); 

			reward = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_tamed = reader.ReadInt();

            if ( m_tamed > m_amount )
                m_tamed = m_amount;
		}
	}
}
