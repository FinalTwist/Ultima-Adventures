using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public sealed class StopMusic : Packet
	{
		public static readonly Packet Instance = Packet.SetStatic( new StopMusic() );

		public StopMusic() : base( 0x6D, 3 )
		{
			m_Stream.Write( (short) 0x1FFF );
		}
	}

	[Flipable( 0x2AF9, 0x2AFD )]
	public class DawnsMusicBox : Item, ISecurable
	{
		public override int LabelNumber { get { return 1075198; } } // Dawn�s Music Box

		private List<MusicName> m_Tracks;

		public List<MusicName> Tracks
		{
			get { return m_Tracks; }
		}

		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get { return m_Level; }
			set { m_Level = value; }
		}

		[Constructable]
		public DawnsMusicBox() : base( 0x2AF9 )
		{
			Weight = 1.0;

			m_Tracks = new List<MusicName>();

			while ( m_Tracks.Count < 4 )
			{
				MusicName name = RandomTrack( DawnsMusicRarity.Common );

				if ( !m_Tracks.Contains( name ) )
					m_Tracks.Add( name );
			}
		}

		public DawnsMusicBox( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterDuped( Item newItem )
		{
			DawnsMusicBox box = newItem as DawnsMusicBox;

			if ( box == null )
				return;

			box.m_Tracks = new List<MusicName>();
			box.m_Tracks.AddRange( m_Tracks );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			int commonSongs = 0;
			int uncommonSongs = 0;
			int rareSongs = 0;

			for ( int i = 0; i < m_Tracks.Count; i++ )
			{
				DawnsMusicInfo info = GetInfo( m_Tracks[ i ] );

				switch ( info.Rarity )
				{
					case DawnsMusicRarity.Common: commonSongs++; break;
					case DawnsMusicRarity.Uncommon: uncommonSongs++; break;
					case DawnsMusicRarity.Rare: rareSongs++; break;
				}
			}

			if ( commonSongs > 0 )
				list.Add( 1075234, commonSongs.ToString() ); // ~1_NUMBER~ Common Tracks
			if ( uncommonSongs > 0 )
				list.Add( 1075235, uncommonSongs.ToString() ); // ~1_NUMBER~ Uncommon Tracks
			if ( rareSongs > 0 )
				list.Add( 1075236, rareSongs.ToString() ); // ~1_NUMBER~ Rare Tracks
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			SetSecureLevelEntry.AddTo( from, this, list ); // Set secure level
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) && !IsLockedDown )
				from.SendLocalizedMessage( 1061856 ); // You must have the item in your backpack or locked down in order to use it.
			else if ( IsLockedDown && !HasAccces( from ) )
				from.SendLocalizedMessage( 502436 ); // That is not accessible.
			else
			{
				from.CloseGump( typeof( DawnsMusicBoxGump ) );
				from.SendGump( new DawnsMusicBoxGump( this ) );
			}
		}

		public bool HasAccces( Mobile m )
		{
			if ( m.AccessLevel >= AccessLevel.GameMaster )
				return true;

			BaseHouse house = BaseHouse.FindHouseAt( this );

			return ( house != null && house.HasAccess( m ) );
		}

		private Timer m_Timer;
		private int m_ItemID = 0;
		private int m_Count = 0;

		public void PlayMusic( Mobile m, MusicName music )
		{
			if ( m_Timer != null && m_Timer.Running )
				EndMusic( m );
			else
				m_ItemID = ItemID;

			m.Send( new PlayMusic( music ) );
			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), TimeSpan.FromSeconds( 0.5 ), 4, new TimerCallback( Animate ) );
		}

		public void EndMusic( Mobile m )
		{
			if ( m_Timer != null && m_Timer.Running )
				m_Timer.Stop();

			m.Send( StopMusic.Instance );

			if ( m_Count > 0 )
				ItemID = m_ItemID;

			m_Count = 0;
		}

		private void Animate()
		{
			m_Count++;

			if ( m_Count >= 4 )
			{
				m_Count = 0;
				ItemID = m_ItemID;
			}
			else
				ItemID++;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (int) m_Tracks.Count );

			for ( int i = 0; i < m_Tracks.Count; i++ )
				writer.Write( (int) m_Tracks[ i ] );

			writer.Write( (int) m_Level );
			writer.Write( (int) m_ItemID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			int count = reader.ReadInt();
			m_Tracks = new List<MusicName>();

			for ( int i = 0; i < count; i++ )
				m_Tracks.Add( (MusicName) reader.ReadInt() );

			m_Level = (SecureLevel) reader.ReadInt();
			m_ItemID = reader.ReadInt();
		}

		private static Dictionary<MusicName, DawnsMusicInfo> m_Info = new Dictionary<MusicName, DawnsMusicInfo>();

		public static void Initialize()
		{
			m_Info.Add( MusicName.Opn_Gen, new DawnsMusicInfo( 1075152, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen2, new DawnsMusicInfo( 1075163, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen3, new DawnsMusicInfo( 1075145, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen4, new DawnsMusicInfo( 1075144, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen5, new DawnsMusicInfo( 1075146, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen6, new DawnsMusicInfo( 1075161, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen7, new DawnsMusicInfo( 1075176, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen8, new DawnsMusicInfo( 1075171, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen9, new DawnsMusicInfo( 1075160, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen10, new DawnsMusicInfo( 1075175, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen11, new DawnsMusicInfo( 1075159, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen12, new DawnsMusicInfo( 1075170, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen13, new DawnsMusicInfo( 1075168, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen14, new DawnsMusicInfo( 1075169, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Gen15, new DawnsMusicInfo( 1075147, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Jung, new DawnsMusicInfo( 1075185, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Jung2, new DawnsMusicInfo( 1075148, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sav, new DawnsMusicInfo( 1075150, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sav2, new DawnsMusicInfo( 1075177, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sav_Jun, new DawnsMusicInfo( 1075149, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod, new DawnsMusicInfo( 1075174, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod2, new DawnsMusicInfo( 1075173, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod3, new DawnsMusicInfo( 1075167, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod4, new DawnsMusicInfo( 1075154, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod5, new DawnsMusicInfo( 1075143, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod6, new DawnsMusicInfo( 1075153, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod7, new DawnsMusicInfo( 1075180, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod8, new DawnsMusicInfo( 1075164, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Opn_Sos_Lod9, new DawnsMusicInfo( 1075165, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank, new DawnsMusicInfo( 1075166, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank2, new DawnsMusicInfo( 1075179, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank3, new DawnsMusicInfo( 1075155, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank4, new DawnsMusicInfo( 1075142, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank5, new DawnsMusicInfo( 1075151, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank6, new DawnsMusicInfo( 1075156, DawnsMusicRarity.Common ) );
			m_Info.Add( MusicName.Bank7, new DawnsMusicInfo( 1075172, DawnsMusicRarity.Common ) );

			m_Info.Add( MusicName.City_Britain, new DawnsMusicInfo( 1075131, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Darkmoor, new DawnsMusicInfo( 1075181, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen, new DawnsMusicInfo( 1075182, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen2, new DawnsMusicInfo( 1075132, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen5, new DawnsMusicInfo( 1075133, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen6, new DawnsMusicInfo( 1075134, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen7, new DawnsMusicInfo( 1075186, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen8, new DawnsMusicInfo( 1075135, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen9, new DawnsMusicInfo( 1075183, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen10, new DawnsMusicInfo( 1075136, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen11, new DawnsMusicInfo( 1075184, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen12, new DawnsMusicInfo( 1075137, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen13, new DawnsMusicInfo( 1075135, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen14, new DawnsMusicInfo( 1075183, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen15, new DawnsMusicInfo( 1075136, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen16, new DawnsMusicInfo( 1075184, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Gen17, new DawnsMusicInfo( 1075137, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Good, new DawnsMusicInfo( 1075134, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Good2, new DawnsMusicInfo( 1075186, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Good4, new DawnsMusicInfo( 1075135, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Good5, new DawnsMusicInfo( 1075183, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Good6, new DawnsMusicInfo( 1075136, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Good7, new DawnsMusicInfo( 1075184, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Lod, new DawnsMusicInfo( 1075137, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Lodoria, new DawnsMusicInfo( 1075135, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Sarth, new DawnsMusicInfo( 1075183, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Sarth2, new DawnsMusicInfo( 1075136, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Sarth3, new DawnsMusicInfo( 1075184, DawnsMusicRarity.Uncommon ) );
			m_Info.Add( MusicName.City_Yew, new DawnsMusicInfo( 1075137, DawnsMusicRarity.Uncommon ) );

			m_Info.Add( MusicName.OpenTitle, new DawnsMusicInfo( 1075138, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn, new DawnsMusicInfo( 1075139, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn2, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn3, new DawnsMusicInfo( 1075139, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn4, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn5, new DawnsMusicInfo( 1075139, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn6, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn7, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Inn8, new DawnsMusicInfo( 1075139, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Pirate, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Pirate2, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Pirate3, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Pirate4, new DawnsMusicInfo( 1075139, DawnsMusicRarity.Rare ) );
			m_Info.Add( MusicName.Pirate5, new DawnsMusicInfo( 1075140, DawnsMusicRarity.Rare ) );

		}

		public static MusicName[] m_CommonTracks = new MusicName[]
		{
			MusicName.Opn_Gen,
			MusicName.OpenTitle,
			MusicName.Opn_Gen2,
			MusicName.Opn_Gen3,
			MusicName.Opn_Gen4,
			MusicName.Opn_Gen5,
			MusicName.Opn_Gen6,
			MusicName.Opn_Gen7,
			MusicName.Opn_Gen8,
			MusicName.Opn_Gen9,
			MusicName.Opn_Gen10,
			MusicName.Opn_Gen11,
			MusicName.Opn_Gen12,
			MusicName.Opn_Gen13,
			MusicName.Opn_Gen14,
			MusicName.Opn_Gen15,
			MusicName.Opn_Jung,
			MusicName.Opn_Jung2,
			MusicName.Opn_Sav,
			MusicName.Opn_Sav2,
			MusicName.Opn_Sav_Jun,
			MusicName.Opn_Sos_Lod,
			MusicName.Opn_Sos_Lod2,
			MusicName.Opn_Sos_Lod3,
			MusicName.Opn_Sos_Lod4,
			MusicName.Opn_Sos_Lod5,
			MusicName.Opn_Sos_Lod6,
			MusicName.Opn_Sos_Lod7,
			MusicName.Opn_Sos_Lod8,
			MusicName.Opn_Sos_Lod9,
			MusicName.Bank,
			MusicName.Bank2,
			MusicName.Bank3,
			MusicName.Bank4,
			MusicName.Bank5,
			MusicName.Bank6,
			MusicName.Bank7			
					};

		public static MusicName[] m_UncommonTracks = new MusicName[]
		{
			MusicName.City_Britain,
			MusicName.City_Darkmoor,
			MusicName.City_Gen,
			MusicName.City_Gen2,
			MusicName.City_Gen5,
			MusicName.City_Gen6,
			MusicName.City_Gen7,
			MusicName.City_Gen8,
			MusicName.City_Gen9,
			MusicName.City_Gen10,
			MusicName.City_Gen11,
			MusicName.City_Gen12,
			MusicName.City_Gen13,
			MusicName.City_Gen14,
			MusicName.City_Gen15,
			MusicName.City_Gen16,
			MusicName.City_Good,
			MusicName.City_Good2,
			MusicName.City_Good4,
			MusicName.City_Good5,
			MusicName.City_Good6,
			MusicName.City_Good7,
			MusicName.City_Lod,
			MusicName.City_Lodoria,
			MusicName.City_Sarth,
			MusicName.City_Sarth2,
			MusicName.City_Sarth3,
			MusicName.City_Yew
		};

		public static MusicName[] m_RareTracks = new MusicName[]
		{
			MusicName.OpenTitle,
			MusicName.Inn,
			MusicName.Inn2,
			MusicName.Inn3,
			MusicName.Inn4,
			MusicName.Inn5,
			MusicName.Inn6,
			MusicName.Inn7,
			MusicName.Pirate,
			MusicName.Pirate2,
			MusicName.Pirate3,
			MusicName.Pirate4,
			MusicName.Pirate5
		};

		public static DawnsMusicInfo GetInfo( MusicName name )
		{
			if ( m_Info.ContainsKey( name ) )
				return m_Info[ name ];

			return null;
		}

		public static MusicName RandomTrack( DawnsMusicRarity rarity )
		{
			MusicName[] list = null;

			switch ( rarity )
			{
				default:
				case DawnsMusicRarity.Common: list = m_CommonTracks; break;
				case DawnsMusicRarity.Uncommon: list = m_UncommonTracks; break;
				case DawnsMusicRarity.Rare: list = m_RareTracks; break;
			}

			return list[ Utility.Random( list.Length ) ];
		}
	}
}
