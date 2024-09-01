/***************************************************************************
 *                              PacketProfile.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.IO;

namespace Server.Diagnostics {
	public abstract class BasePacketProfile : BaseProfile {
		private long _totalLength;

		public long TotalLength {
			get {
				return _totalLength;
			}
		}

		public double AverageLength {
			get {
				return ( double ) _totalLength / Math.Max( 1, this.Count );
			}
		}

		protected BasePacketProfile(string name)
			: base( name ) {
		}

		public void Finish( int length ) {
			Finish();

			_totalLength += length;
		}

		public override void WriteTo( TextWriter op ) {
			base.WriteTo( op );

			op.Write( "\t{0,12:F2} {1,-12:N0}", AverageLength, TotalLength );
		}
	}

	public class PacketSendProfile : BasePacketProfile {
		private static Dictionary<Type, PacketSendProfile> _profiles = new Dictionary<Type, PacketSendProfile>();

		public static IEnumerable<PacketSendProfile> Profiles {
			get {
				return _profiles.Values;
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static PacketSendProfile Acquire( Type type ) {
			PacketSendProfile prof;

			if ( !_profiles.TryGetValue( type, out prof ) ) {
				_profiles.Add( type, prof = new PacketSendProfile( type ) );
			}

			return prof;
		}

		private long _created;

		public void Increment() {
			Interlocked.Increment(ref _created);
		}

		public PacketSendProfile( Type type )
			: base( type.FullName ) {
		}

		public override void WriteTo( TextWriter op ) {
			base.WriteTo( op );

			op.Write( "\t{0,12:N0}", _created );
		}
    }

    public class PacketHistoryProfile : BaseProfile
    {
        private static Dictionary<string, PacketHistoryProfile> _profiles = new Dictionary<string, PacketHistoryProfile>();

        public static IEnumerable<PacketHistoryProfile> Profiles
        {
            get
            {
                return _profiles.Values;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static PacketHistoryProfile Acquire(string username)
        {
            PacketHistoryProfile prof;

            if (!_profiles.TryGetValue(username, out prof))
            {
                _profiles.Add(username, prof = new PacketHistoryProfile(username));
            }

            return prof;
        }

        private Queue<PacketHistory> _packets;
        private object _addLock = new object();
        const int MAX_QUEUE_SIZE = 500;

        public PacketHistoryProfile(string username)
            : base(username)
        {
            _packets = new Queue<PacketHistory>(MAX_QUEUE_SIZE);
        }
        
        public void Add(int packetType, long length, Type type)
        {
            lock (_addLock)
            {
                if (MAX_QUEUE_SIZE < _packets.Count + 1)
                {
                    _packets.Dequeue();
                }

                _packets.Enqueue(new PacketHistory(Core.TickCount, packetType, length, type));
            }
        }
        
        private class PacketHistory
        {
            public readonly int PacketType;
            public readonly long Timestamp;
            public readonly long Length;
            public readonly Type Type;

            public PacketHistory(long timestamp, int packetType, long length, Type type)
            {
                Timestamp = timestamp;
                PacketType = packetType;
                Length = length;
                Type = type;
            }
        }

        public override void WriteTo(TextWriter op)
        {
            if (_packets.Count < 1) { return; }

            Queue<PacketHistory> packets = Interlocked.Exchange(ref _packets, new Queue<PacketHistory>(MAX_QUEUE_SIZE));
            op.WriteLine("Captured '{0}' Packets for '{1}'", packets.Count, Name);
            op.WriteLine("Timestamp\tType\tLength\tClass");

            while (0 < packets.Count)
            {
                PacketHistory packet = packets.Dequeue();
                op.WriteLine("{0}\t0x{1:X2}\t{2:D6}\t{3}", packet.Timestamp, packet.PacketType, packet.Length, packet.Type);
            }
        }
    }

    public class PacketReceiveProfile : BasePacketProfile {
		private static Dictionary<int, PacketReceiveProfile> _profiles = new Dictionary<int, PacketReceiveProfile>();

		public static IEnumerable<PacketReceiveProfile> Profiles {
			get {
				return _profiles.Values;
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static PacketReceiveProfile Acquire( int packetId ) {
			PacketReceiveProfile prof;

			if ( !_profiles.TryGetValue( packetId, out prof ) ) {
				_profiles.Add( packetId, prof = new PacketReceiveProfile( packetId ) );
			}

			return prof;
		}

		public PacketReceiveProfile( int packetId )
			: base( String.Format( "0x{0:X2}", packetId ) ) {
		}
    }
}