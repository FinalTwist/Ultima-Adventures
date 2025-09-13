/*
BandagePacket.cs
snicker7
08/24/06

Description:
This file adds a packethandler for the new Bandage packet in UO
Client versions 5.0.4+, which supports the BandageSelf and
BandageSelected macros in the client. The new targeting system
must be enabled in the Client for these packets to be sent in
current clients.
*/
using System;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Network
{
	public class BandagePacket
	{
		public static void Initialize()
		{
			PacketHandlers.RegisterExtended( 0x2C,  true, new OnPacketReceive( BandageRequest ) );
		}
		
		public static void BandageRequest( NetState state, PacketReader pvSrc )
		{
			Mobile from = state.Mobile;
			
			if ( from.AccessLevel >= AccessLevel.Counselor || Core.TickCount - from.NextActionTime >= 0 )
			{
				Serial use = pvSrc.ReadInt32();
				Serial targ = pvSrc.ReadInt32();
				
				Bandage bandage = World.FindItem( use ) as Bandage;
				
				if( bandage != null && from.InRange( bandage.GetWorldLocation(), Core.AOS ? 2 : 1 ) )
				{
					from.RevealingAction();
					
					Mobile to = World.FindMobile(targ);
					
					if ( to != null )
					{
						if ( from.InRange( bandage.GetWorldLocation(), Core.AOS ? 2 : 1 ) )
						{
							if ( BandageContext.BeginHeal( from, to ) != null )
								bandage.Consume();
						}
						else
						{
							from.SendLocalizedMessage( 500295 ); // You are too far away to do that.
						}
					}
					else
					{
						from.SendLocalizedMessage( 500970 ); // Bandages can not be used on that.
					}
					
					from.NextActionTime = Core.TickCount + 500;			
				}
			}
			else
			{
				from.SendActionMessage();
			}
		}
	}
}