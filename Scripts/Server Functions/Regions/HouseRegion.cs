using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Multis;
using Server.Spells;
using Server.Spells.Sixth;
using Server.Guilds;
using Server.Gumps;
using System.Text;
using System.IO;
using Server.Misc;

namespace Server.Regions
{
	public class HouseRegion : BaseRegion
	{
		public static readonly int HousePriority = Region.DefaultPriority + 1;

		private BaseHouse m_House;

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( OnLogin );
		}

		public static void OnLogin( LoginEventArgs e )
		{
			BaseHouse house = BaseHouse.FindHouseAt( e.Mobile );

			if ( house != null && !house.Public && !house.IsFriend( e.Mobile ) )
				e.Mobile.Location = house.BanLocation;
		}

		public override bool AllowHarmful( Mobile from, Mobile target ) // WIZARD ADDED
		{
			if ( ( m_House.IsOwner( from ) || m_House.IsCoOwner( from ) || m_House.IsFriend( from ) ) && ( m_House.IsOwner( target ) || m_House.IsCoOwner( target ) || m_House.IsFriend( target ) ) )
				return true;
			else if (( from is PlayerMobile ) && ( target is PlayerMobile ))
				return false;
			else if ( from.Region != target.Region ) // SO YOU CAN'T JUST SHOOT MONSTERS FROM YOUR PORCH
				return false;
			else
				return base.AllowHarmful( from, target );
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( m_House.IsOwner( m ) || m_House.IsCoOwner( m ) || m_House.IsFriend( m ) )
			{
				return true;
			}
			else
			{
				m.SendMessage( "You cannot use magic in this home." );
				return false;
			}
		}

		public override void OnEnter( Mobile m )
		{
			base.OnEnter( m );
			//LoggingFunctions.LogRegions( m, "a Home", "enter" );
		}

		public override void OnExit( Mobile m )
		{
			base.OnExit( m );
			//LoggingFunctions.LogRegions( m, "a Home", "exit" );
		}

		// WIZARD ADDED THIS TO SEE IF THE LEVEL IS RIGHT IN THE DUNGEON HOMES
		public override void AlterLightLevel( Mobile m, ref int global, ref int personal )
		{
			if ( ( m.Map == Map.Ilshenar) && ( m.X > 1630 ) && ( m.Y < 1080 ) )
			{
				global = LightCycle.JailLevel;
			}
			else if ( ( m.Map == Map.Ilshenar) )
			{
				global = LightCycle.DungeonLevel;
			}
			else if ( m.Map == Map.Felucca && m.X > 6446 && m.Y > 3056 )
			{
				global = LightCycle.DungeonLevel;
			}
			else if ( m.Map == Map.Trammel && m.X > 5215 && m.Y > 1038 && m.X < 5419 && m.Y < 1318 ) // UMBRA CAVES
			{
				global = LightCycle.NightLevel;
			}
			else if ( m.Map == Map.Trammel && m.X > 6537 && m.Y > 3779 && m.X < 6780 && m.Y < 4080 ) // SHIPWRECK GROTTO
			{
				global = LightCycle.NightLevel;
			}
		}

		public HouseRegion( BaseHouse house ) : base( null, house.Map, HousePriority, GetArea( house ) )
		{
			m_House = house;

			Point3D ban = house.RelativeBanLocation;

			this.GoLocation = new Point3D( house.X + ban.X, house.Y + ban.Y, house.Z + ban.Z );
		}

		private static Rectangle3D[] GetArea( BaseHouse house )
		{
			int x = house.X;
			int y = house.Y;
			int z = house.Z;

			Rectangle2D[] houseArea = house.Area;
			Rectangle3D[] area = new Rectangle3D[houseArea.Length];

			for ( int i = 0; i < area.Length; i++ )
			{
				Rectangle2D rect = houseArea[i];
				area[i] = Region.ConvertTo3D( new Rectangle2D( x + rect.Start.X, y + rect.Start.Y, rect.Width, rect.Height ) );
			}

			return area;
		}

		public override bool SendInaccessibleMessage( Item item, Mobile from )
		{
			if ( item is Container )
				item.SendLocalizedMessageTo( from, 501647 ); // That is secure.
			else
				item.SendLocalizedMessageTo( from, 1061637 ); // You are not allowed to access this.

			return true;
		}

		public override bool CheckAccessibility( Item item, Mobile from )
		{
			return m_House.CheckAccessibility( item, from );
		}

		private bool m_Recursion;

		// Use OnLocationChanged instead of OnEnter because it can be that we enter a house region even though we're not actually inside the house
		public override void OnLocationChanged( Mobile m, Point3D oldLocation )
		{
			if ( m_Recursion )
				return;

			base.OnLocationChanged( m, oldLocation );

			m_Recursion = true;

			if ( m is BaseCreature && ((BaseCreature)m).NoHouseRestrictions )
			{
			}
			else if ( m is BaseCreature && ((BaseCreature)m).IsHouseSummonable && !(BaseCreature.Summoning || m_House.IsInside( oldLocation, 16 )) )
			{
			}
			else if ( (m_House.Public || !m_House.IsAosRules) && m_House.IsBanned( m ) && m_House.IsInside( m ) )
			{
				m.Location = m_House.BanLocation;

				if( !Core.SE )
					m.SendLocalizedMessage( 501284 ); // You may not enter.
			}
			else if ( m_House.IsAosRules && !m_House.Public && !m_House.HasAccess( m ) && m_House.IsInside( m ) )
			{
				m.Location = m_House.BanLocation;

				if( !Core.SE )
					m.SendLocalizedMessage( 501284 ); // You may not enter.
			}
			else if ( m_House.IsCombatRestricted( m ) && m_House.IsInside( m ) && !m_House.IsInside( oldLocation, 16 ) )
			{
				m.Location = m_House.BanLocation;
				m.SendLocalizedMessage( 1061637 ); // You are not allowed to access this.
			}
			else if ( m_House is HouseFoundation )
			{
				HouseFoundation foundation = (HouseFoundation)m_House;

				if ( foundation.Customizer != null && foundation.Customizer != m && m_House.IsInside( m ) )
					m.Location = m_House.BanLocation;
			}

			if ( m_House.InternalizedVendors.Count > 0 && m_House.IsInside( m ) && !m_House.IsInside( oldLocation, 16 ) && m_House.IsOwner( m ) && m.Alive && !m.HasGump( typeof( NoticeGump ) ) )
			{
				/* This house has been customized recently, and vendors that work out of this
				 * house have been temporarily relocated.  You must now put your vendors back to work.
				 * To do this, walk to a location inside the house where you wish to station
				 * your vendor, then activate the context-sensitive menu on your avatar and
				 * select "Get Vendor".
				 */
				m.SendGump( new NoticeGump( 1060635, 30720, 1061826, 32512, 320, 180, null, null ) );
			}

			m_Recursion = false;
		}

		public override bool OnMoveInto( Mobile from, Direction d, Point3D newLocation, Point3D oldLocation )
		{
			if ( !base.OnMoveInto( from, d, newLocation, oldLocation ) )
				return false;

			if ( from is BaseCreature && ((BaseCreature)from).NoHouseRestrictions )
			{
			}
			else if ( from is BaseCreature && !((BaseCreature)from).Controlled ) // Untamed creatures cannot enter public houses
			{
				//return false; // final, creature shouldn't be subjec tto invisible barrier, build walls and doors!
			}
			else if ( from is BaseCreature && ((BaseCreature)from).IsHouseSummonable && !(BaseCreature.Summoning || m_House.IsInside( oldLocation, 16 )) )
			{
				//return false;
			}
			else if ( from is BaseCreature && !((BaseCreature)from).Controlled && m_House.IsAosRules && !m_House.Public)
			{
				//return false;
			}
			else if ( (m_House.Public || !m_House.IsAosRules) && m_House.IsBanned( from ) && m_House.IsInside( newLocation, 16 ) )
			{
				from.Location = m_House.BanLocation;

				if( !Core.SE )
					from.SendLocalizedMessage( 501284 ); // You may not enter.

				return false;
			}
			else if ( m_House.IsAosRules && !m_House.Public && !m_House.HasAccess( from ) && m_House.IsInside( newLocation, 16 ) )
			{
				if( !Core.SE )
					from.SendLocalizedMessage( 501284 ); // You may not enter.

				return false;
			}
			else if ( m_House.IsCombatRestricted( from ) && !m_House.IsInside( oldLocation, 16 ) && m_House.IsInside( newLocation, 16 ) )
			{
				from.SendLocalizedMessage( 1061637 ); // You are not allowed to access this.
				return false;
			}
			else if ( m_House is HouseFoundation )
			{
				HouseFoundation foundation = (HouseFoundation)m_House;

				if ( foundation.Customizer != null && foundation.Customizer != from && m_House.IsInside( newLocation, 16 ) )
					return false;
			}

			if ( m_House.InternalizedVendors.Count > 0 && m_House.IsInside( from ) && !m_House.IsInside( oldLocation, 16 ) && m_House.IsOwner( from ) && from.Alive && !from.HasGump( typeof( NoticeGump ) ) )
			{
				/* This house has been customized recently, and vendors that work out of this
				 * house have been temporarily relocated.  You must now put your vendors back to work.
				 * To do this, walk to a location inside the house where you wish to station
				 * your vendor, then activate the context-sensitive menu on your avatar and
				 * select "Get Vendor".
				 */
				from.SendGump( new NoticeGump( 1060635, 30720, 1061826, 32512, 320, 180, null, null ) );
			}

			Server.Misc.RegionMusic.MusicRegion( from, this );

			return true;
		}

		public override bool OnDecay( Item item )
		{
			if ( (m_House.IsLockedDown( item ) || m_House.IsSecure( item )) && m_House.IsInside( item ) )
				return false;
			else
				return base.OnDecay(item );
		}

		public static TimeSpan CombatHeatDelay = TimeSpan.FromSeconds( 30.0 );

		public override TimeSpan GetLogoutDelay( Mobile m )
		{
			if ( m_House.IsFriend( m ) && m_House.IsInside( m ) )
			{
				for ( int i = 0; i < m.Aggressed.Count; ++i )
				{
					AggressorInfo info = m.Aggressed[i];

					if ( info.Defender.Player && (DateTime.UtcNow - info.LastCombatTime) < CombatHeatDelay )
						return base.GetLogoutDelay( m );
				}

				return TimeSpan.Zero;
			}

			return base.GetLogoutDelay( m );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );

			Mobile from = e.Mobile;
			Item sign = m_House.Sign;

			bool isOwner = m_House.IsOwner( from );
			bool isCoOwner = isOwner || m_House.IsCoOwner( from );
			bool isFriend = isCoOwner || m_House.IsFriend( from );

			if ( !isFriend )
				return;
			
			if ( !from.Alive )
				return;
			
			if ( Core.ML && Insensitive.Equals( e.Speech, "I wish to resize my house" ) )
			{
				if ( from.Map != sign.Map || !from.InRange( sign, 0 ) )
				{
				    from.SendLocalizedMessage( 500295 ); // you are too far away to do that.
				}
				else if ( DateTime.UtcNow  <= m_House.BuiltOn.AddHours ( 1 ) )
				{
					from.SendLocalizedMessage( 1080178 ); // You must wait one hour between each house demolition.
				}
				else if ( isOwner )
				{
					from.CloseGump( typeof( ConfirmHouseResize ) );
					from.CloseGump( typeof( HouseGumpAOS ) );
					from.SendGump( new ConfirmHouseResize( from, m_House ) );	
				}
				else
				{
					from.SendLocalizedMessage( 501320 ); // Only the house owner may do this.
				}
			}
			
			if ( !m_House.IsInside( from ) || !m_House.IsActive )
				return;

			else if ( e.HasKeyword( 0x33 ) ) // remove thyself
			{
				if ( isFriend )
				{
					from.SendLocalizedMessage( 501326 ); // Target the individual to eject from this house.
					from.Target = new HouseKickTarget( m_House );
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x34 ) ) // I ban thee
			{
				if ( !isFriend )
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
				else if ( !m_House.Public && m_House.IsAosRules )
				{
					from.SendLocalizedMessage( 1062521 ); // You cannot ban someone from a private house.  Revoke their access instead.
				}
				else
				{
					from.SendLocalizedMessage( 501325 ); // Target the individual to ban from this house.
					from.Target = new HouseBanTarget( true, m_House );
				}
			}
			else if ( e.HasKeyword( 0x23 ) ) // I wish to lock this down
			{
				if ( isCoOwner )
				{
					//from.SendLocalizedMessage( 502097 ); // Lock what down?
					//from.Target = new LockdownTarget( false, m_House );
					from.SendMessage("Simply place an item in the house to lock it down.");
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x24 ) ) // I wish to release this
			{
				if ( isCoOwner )
				{
					from.SendLocalizedMessage( 502100 ); // Choose the item you wish to release
					from.Target = new LockdownTarget( true, m_House );
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x25 ) ) // I wish to secure this
			{
				if ( isOwner )
				{
					//from.SendLocalizedMessage( 502103 ); // Choose the item you wish to secure
					//from.Target = new  SecureTarget( false, m_House );
					from.SendMessage("Simply place a container in the house to secure it.");
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x26 ) ) // I wish to unsecure this
			{
				if ( isOwner )
				{
					//from.SendLocalizedMessage( 502106 ); // Choose the item you wish to unsecure
					//from.Target = new  SecureTarget( true, m_House );
					from.SendMessage("Simply take an container to move it.");
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x27 ) ) // I wish to place a strongbox
			{
				if ( isOwner )
				{
					from.SendLocalizedMessage( 502109 ); // Owners do not get a strongbox of their own.
				}
				else if ( isCoOwner )
				{
					m_House.AddStrongBox( from );
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
			else if ( e.HasKeyword( 0x28 ) ) // trash barrel
			{
				if ( isCoOwner )
				{
					m_House.AddTrashBarrel( from );
				}
				else if ( isFriend )
				{
					from.SendLocalizedMessage( 1010587 ); // You are not a co-owner of this house.
				}
				else
				{
					from.SendLocalizedMessage( 502094 ); // You must be in your house to do this.
				}
			}
		}

		public override bool OnDoubleClick( Mobile from, object o )
		{
			if ( o is Container )
			{
				Container c = (Container)o;

				SecureAccessResult res = m_House.CheckSecureAccess( from, c );

				switch ( res )
				{
					case SecureAccessResult.Insecure: break;
					case SecureAccessResult.Accessible: return true;
					case SecureAccessResult.Inaccessible: c.SendLocalizedMessageTo( from, 1010563 ); return false;
				}
			}

			return base.OnDoubleClick( from, o );
		}

		public override bool OnSingleClick( Mobile from, object o )
		{
			if ( o is Item )
			{
				Item item = (Item)o;

				if ( m_House.IsLockedDown( item ) )
					item.LabelTo( from, 501643 ); // [locked down]
				else if ( m_House.IsSecure( item ) )
					item.LabelTo( from, 501644 ); // [locked down & secure]
			}

			return base.OnSingleClick( from, o );
		}

		public BaseHouse House
		{
			get
			{
				return m_House;
			}
		}
	}
}