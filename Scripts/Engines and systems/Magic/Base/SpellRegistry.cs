using System;
using System.Collections.Generic;
using System.IO;
using Server.Spells.Bushido;
using Server.Spells.Chivalry;
using Server.Items;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells
{
	public class SpellRegistry
	{
		private static Type[] m_Types = new Type[700];
		private static int m_Count;

		public static Type[] Types
		{
			get
			{
				m_Count = -1;
				return m_Types;
			}
		}
		
		//What IS this used for anyways.
		public static int Count
		{
			get
			{
				if ( m_Count == -1 )
				{
					m_Count = 0;

					for ( int i = 0; i < m_Types.Length; ++i )
						if ( m_Types[i] != null )
							++m_Count;
				}

				return m_Count;
			}
		}

		private static Dictionary<Type, Int32> m_IDsFromTypes = new Dictionary<Type, Int32>( m_Types.Length );
		
		private static Dictionary<Int32, SpecialMove> m_SpecialMoves = new Dictionary<Int32, SpecialMove>();
		public static Dictionary<Int32, SpecialMove> SpecialMoves { get { return m_SpecialMoves; } }

		public static int GetRegistryNumber( ISpell s )
		{
			return GetRegistryNumber( s.GetType() );
		}

		public static int GetRegistryNumber( SpecialMove s )
		{
			return GetRegistryNumber( s.GetType() );
		}

		public static int GetRegistryNumber( Type type )
		{
			if( m_IDsFromTypes.ContainsKey( type ) )
				return m_IDsFromTypes[type];

			return -1;
		}

		public static void Register( int spellID, Type type )
		{
			if ( spellID < 0 || spellID >= m_Types.Length )
				return;

			if ( m_Types[spellID] == null )
				++m_Count;

			m_Types[spellID] = type;

			if( !m_IDsFromTypes.ContainsKey( type ) )
				m_IDsFromTypes.Add( type, spellID );

			if( type.IsSubclassOf( typeof( SpecialMove ) ) )
			{
				SpecialMove spm = null;

				try
				{
					spm = Activator.CreateInstance( type ) as SpecialMove;
				}
				catch
				{
				}

				if( spm != null )
					m_SpecialMoves.Add( spellID, spm );
			}
		}

		public static SpecialMove GetSpecialMove( int spellID )
		{
			if ( spellID < 0 || spellID >= m_Types.Length )
				return null;

			Type t = m_Types[spellID];

			if ( t == null || !t.IsSubclassOf( typeof( SpecialMove ) ) || !m_SpecialMoves.ContainsKey( spellID ) )
				return null;

			return m_SpecialMoves[spellID];
		}

		private static object[] m_Params = new object[2];

		public static Spell NewSpell( int spellID, Mobile caster, Item scroll )
		{
			if ( spellID < 0 || spellID >= m_Types.Length )
				return null;

			Type t = m_Types[spellID];

			if( t != null && !t.IsSubclassOf( typeof( SpecialMove ) ) )
			{
				m_Params[0] = caster;
				m_Params[1] = scroll;

				try
				{
					return (Spell)Activator.CreateInstance( t, m_Params );
				}
				catch
				{
				}
			}

			return null;
		}

		private static string[] m_CircleNames = new string[]
			{
				"First",
				"Second",
				"Third",
				"Fourth",
				"Fifth",
				"Sixth",
				"Seventh",
				"Eighth",
				"Necromancy",
				"Chivalry",
				"Bushido",
				"Ninjitsu",
				"Spellweaving",
				"Magical",		// WIZARD ADDED FOR CERTAIN ITEMS
				"Undead",		// WIZARD ADDED FOR NEW POTIONS
				"Herbalist",	// WIZARD ADDED FOR NEW HERBALISM
				"Song",			// WIZARD ADDED FOR NEW BARD SONGS
				"DeathKnight",	// WIZARD ADDED FOR DEATH KNIGHT SPELLS
				"HolyMan",		// WIZARD ADDED FOR HOLY MAN SPELLS
				"Mystic",		// WIZARD ADDED FOR MYSTIC SPELLS
				"Jester",		// WIZARD ADDED FOR JESTER SPELLS
				"Research",		// WIZARD ADDED FOR RESEARCH SPELLS
				"Syth",			// WIZARD ADDED FOR SYTH SPELLS
				"Jedi"
			};

		public static Spell NewSpell( string name, Mobile caster, Item scroll )
		{
			for ( int i = 0; i < m_CircleNames.Length; ++i )
			{
				Type t = ScriptCompiler.FindTypeByFullName( String.Format( "Server.Spells.{0}.{1}", m_CircleNames[i], name ) );

				if ( t != null && !t.IsSubclassOf( typeof( SpecialMove ) ) )
				{
					m_Params[0] = caster;
					m_Params[1] = scroll;

					try
					{
						return (Spell)Activator.CreateInstance( t, m_Params );
					}
					catch
					{
					}
				}
			}

			return null;
		}
	}
}