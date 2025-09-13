using System;
using Server;
using Server.Mobiles;

namespace Server
{
	public class OppositionGroup
	{
		private Type[][] m_Types;

		public OppositionGroup( Type[][] types )
		{
			m_Types = types;
		}

		public bool IsEnemy( object from, object target )
		{
			int fromGroup = IndexOf( from );
			int targGroup = IndexOf( target );

			return ( fromGroup != -1 && targGroup != -1 && fromGroup != targGroup );
		}

		public int IndexOf( object obj )
		{
			if ( obj == null )
				return -1;

			Type type = obj.GetType();

			for ( int i = 0; i < m_Types.Length; ++i )
			{
				Type[] group = m_Types[i];

				bool contains = false;

				for ( int j = 0; !contains && j < group.Length; ++j )
					contains = ( type == group[j] );

				if ( contains )
					return i;
			}

			return -1;
		}

		private static OppositionGroup m_TerathansAndOphidians = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( WaterSpawn )
				},
				new Type[]
				{
					typeof( WaterSpawn )
				}
			} );

		public static OppositionGroup TerathansAndOphidians
		{
			get{ return m_TerathansAndOphidians; }
		}

		private static OppositionGroup m_SavagesAndOrcs = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( WaterSpawn )
				},
				new Type[]
				{
					typeof( WaterSpawn )
				}
			} );

		public static OppositionGroup SavagesAndOrcs
		{
			get{ return m_SavagesAndOrcs; }
		}
		
		private static OppositionGroup m_FeyAndUndead = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( WaterSpawn )
				},
				new Type[]
				{
					typeof( WaterSpawn )
				}
			} );

		public static OppositionGroup FeyAndUndead
		{
			get{ return m_FeyAndUndead; }
		}
	}
}