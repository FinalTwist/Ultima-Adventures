using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Regions;

namespace Server.Mobiles
{
	public abstract class BaseMount : BaseCreature, IMount
	{
		private Mobile m_Rider;
		private Item m_InternalItem;
		private DateTime m_NextMountAbility;

		public virtual TimeSpan MountAbilityDelay { get { return TimeSpan.Zero; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextMountAbility
		{
			get { return m_NextMountAbility; }
			set { m_NextMountAbility = value; }
		}

		protected Item InternalItem { get { return m_InternalItem; } }

		public virtual bool AllowMaleRider{ get{ return true; } }
		public virtual bool AllowFemaleRider{ get{ return true; } }

		public BaseMount( string name, int bodyID, int itemID, AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed ) : base ( aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed )
		{
			Name = name;
			Body = bodyID;

			m_InternalItem = new MountItem( this, itemID );
		}

		public BaseMount( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_NextMountAbility );

			writer.Write( m_Rider );
			writer.Write( m_InternalItem );
		}

		[Hue, CommandProperty( AccessLevel.GameMaster )]
		public override int Hue
		{
			get
			{
				return base.Hue;
			}
			set
			{
				base.Hue = value;

				if ( m_InternalItem != null )
					m_InternalItem.Hue = value;
			}
		}

		public override bool OnBeforeDeath()
		{
			if ( Rider != null ){ Server.Mobiles.EtherealMount.EthyDismount( Rider, true ); }
			Rider = null;

			return base.OnBeforeDeath();
		}

		public override void OnAfterDelete()
		{
			if ( m_InternalItem != null )
				m_InternalItem.Delete();

			m_InternalItem = null;

			base.OnAfterDelete();
		}

		public override void OnDelete()
		{
			if ( Rider != null ){ Server.Mobiles.EtherealMount.EthyDismount( Rider, true ); }
			Rider = null;

			base.OnDelete();
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_NextMountAbility = reader.ReadDateTime();
					goto case 0;
				}
				case 0:
				{
					m_Rider = reader.ReadMobile();
					m_InternalItem = reader.ReadItem();

					if ( m_InternalItem == null )
						Delete();

					break;
				}
			}
		}

		public virtual void OnDisallowedRider( Mobile m )
		{
			m.SendMessage( "You may not ride this creature." );
		}

		public override void OnDoubleClick( Mobile from )
		{
		    if ( IsDeadPet )
				return;

			if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				if ( Core.AOS ) // You cannot ride a mount in your current form.
					PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 1062061, from.NetState );
				else
					from.SendLocalizedMessage( 1061628 ); // You can't do that while polymorphed.

				return;
			}

			if ( !CheckMountAllowed( from, true ) )
				return;

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 1005583 ); // Please dismount first.
				return;
			}

			if ( from.Female ? !AllowFemaleRider : !AllowMaleRider )
			{
				OnDisallowedRider( from );
				return;
			}

			if ( !Multis.DesignContext.Check( from ) )
				return;

			if ( from.HasTrade )
			{
				from.SendLocalizedMessage( 1042317, "", 0x41 ); // You may not ride at this time
				return;
			}

			if ( Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion( from.Region ) )
			{
				from.SendMessage( "You cannot mount that while you are in this place." );
				return;
			}

			int range = 1;
				if ( Server.Misc.MyServerSettings.FriendsAvoidHeels() ){ range = 5; }

			if ( from.InRange( this, range ) )
			{
				bool canAccess = ( from.AccessLevel >= AccessLevel.GameMaster )
					|| ( Controlled && ControlMaster == from )
					|| ( Summoned && SummonMaster == from );

				if ( canAccess )
				{
					if ( this.Poisoned )
						PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 1049692, from.NetState ); // This mount is too ill to ride.
					else
						Server.Mobiles.BaseMount.Ride( this, from );
				}
				else if ( !Controlled && !Summoned )
				{
					// That mount does not look broken! You would have to tame it to ride it.
					PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 501263, from.NetState );
				}
				else
				{
					// This isn't your mount; it refuses to let you ride.
					PrivateOverheadMessage( Network.MessageType.Regular, 0x3B2, 501264, from.NetState );
				}
			}
			else
			{
				from.SendLocalizedMessage( 500206 ); // That is too far away to ride.
			}
		}

		public static int MountBody( int body )
		{
			if ( !MyServerSettings.NewMounts() )
			{
				if ( body == 190 ){ body = 16069; }
				else if ( body == 177 ){ body = 16069; }
				else if ( body == 34 ){ body = 16069; }
				else if ( body == 179 ){ body = 16069; }
				else if ( body == 212 ){ body = 16069; }
				else if ( body == 795 ){ body = 16031; }
				else if ( body == 27 ){ body = 0x3EB4; }
				else if ( body == 354 ){ body = 0x3EB4; }
				else if ( body == 116 ){ body = 16036; }
				else if ( body == 117 ){ body = 16036; }
				else if ( body == 219 ){ body = 16036; }
				else if ( body == 61 ){ body = 55; }
				else if ( body == 585 ){ body = 0x3E90; }
			}
			return body;
		}

		public static void Ride( BaseMount mount, Mobile rider )
		{
			if ( !MyServerSettings.NewMounts() )
			{
			    if ( mount is GrizzlyBearRiding ){ mount.Hue = 0x797; }
			    else if ( mount is AncientNightmareRiding ){ mount.Hue = 1470; }
			    else if ( mount is DarkUnicornRiding ){ mount.Hue = 1470; }
			    else if ( mount is Dreadhorn ){ mount.Hue = 0xB42; }
			    else if ( mount is SabretoothBearRiding ){ mount.Hue = 0x5BC; }
			    else if ( mount is CaveBearRiding ){ mount.Hue = 0x789; }
			    else if ( mount is ElderBrownBearRiding ){ mount.Hue = 0x908; }
			    else if ( mount is ElderBlackBearRiding ){ mount.Hue = 0xAB1; }
			    else if ( mount.Body == 116 ){ mount.Hue = 0x6FC; }
			    else if ( mount.Body == 219 ){ mount.Hue = 0xA70; }
			    else if ( mount.Body == 117 ){ mount.Hue = 0xB77; }
			}
			if ( mount is Wyverns ){ mount.Hue = 0x991; }

			mount.Rider = rider;
		}

		public override void OnThink()
		{
			if ( !MyServerSettings.NewMounts() )
			{
			    if ( this is GrizzlyBearRiding ){ this.Hue = 0; }
			    else if ( this is AncientNightmareRiding ){ this.Hue = 0; }
			    else if ( this is DarkUnicornRiding ){ this.Hue = 0; }
			    else if ( this is Dreadhorn ){ this.Hue = 0; }
			    else if ( this is SabretoothBearRiding ){ this.Hue = 0x54B; }
			    else if ( this is CaveBearRiding ){ this.Hue = 0; }
			    else if ( this is ElderBrownBearRiding ){ this.Hue = 0; }
			    else if ( this is ElderBlackBearRiding ){ this.Hue = 0; }
			    else if ( this.Body == 116 ){ this.Hue = 0; }
			    else if ( this.Body == 219 ){ this.Hue = 0; }
			    else if ( this.Body == 117 ){ this.Hue = 0; }
			}
			if ( this is Wyverns ){ this.Hue = 0; }

			base.OnThink();
		}

		protected override void OnLocationChange( Point3D oldLocation )
		{
			if ( !MyServerSettings.NewMounts() )
			{
			    if ( this is GrizzlyBearRiding ){ this.Hue = 0; }
			    else if ( this is AncientNightmareRiding ){ this.Hue = 0; }
			    else if ( this is DarkUnicornRiding ){ this.Hue = 0; }
			    else if ( this is Dreadhorn ){ this.Hue = 0; }
			    else if ( this is SabretoothBearRiding ){ this.Hue = 0x54B; }
			    else if ( this is CaveBearRiding ){ this.Hue = 0; }
			    else if ( this is ElderBrownBearRiding ){ this.Hue = 0; }
			    else if ( this is ElderBlackBearRiding ){ this.Hue = 0; }
			    else if ( this.Body == 116 ){ this.Hue = 0; }
			    else if ( this.Body == 219 ){ this.Hue = 0; }
			    else if ( this.Body == 117 ){ this.Hue = 0; }
			}
			if ( this is Wyverns ){ this.Hue = 0; }

			base.OnLocationChange( oldLocation );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ItemID
		{
			get
			{
				if ( m_InternalItem != null )
					return m_InternalItem.ItemID;
				else
					return 0;
			}
			set
			{
				if ( m_InternalItem != null )
					m_InternalItem.ItemID = value;
			}
		}

		public static void Dismount( Mobile m )
		{
			IMount mount = m.Mount;

			if ( mount != null )
			{
				Server.Mobiles.EtherealMount.EthyDismount( m, true );
				mount.Rider = null;
				if (mount is Mobile)
				{
					Mobile animal = (Mobile)mount;
					animal.Hidden = false;
				}
				
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Rider
		{
			get
			{
				return m_Rider;
			}
			set
			{
				if ( m_Rider != value )
				{
					if ( value == null )
					{
						Point3D loc = m_Rider.Location;
						Map map = m_Rider.Map;

						if ( map == null || map == Map.Internal )
						{
							loc = m_Rider.LogoutLocation;
							map = m_Rider.LogoutMap;
						}

						Direction = m_Rider.Direction;
						Location = loc;
						Map = map;

						if ( m_InternalItem != null )
							m_InternalItem.Internalize();
					}
					else
					{
						if ( m_Rider != null )
							Dismount( m_Rider );

						Dismount( value );

						if ( m_InternalItem != null )
							value.AddItem( m_InternalItem );

						value.Direction = this.Direction;

						Internalize();
					}

					m_Rider = value;
				}
			}
		}

		private class BlockEntry
		{
			public BlockMountType m_Type;
			public DateTime m_Expiration;

			public bool IsExpired{ get{ return ( DateTime.UtcNow >= m_Expiration ); } }

			public BlockEntry( BlockMountType type, DateTime expiration )
			{
				m_Type = type;
				m_Expiration = expiration;
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public static void SetMountPrevention( Mobile mob, BlockMountType type, TimeSpan duration )
		{
			if ( mob == null )
				return;

			DateTime expiration = DateTime.UtcNow + duration;

			BlockEntry entry = m_Table[mob] as BlockEntry;

			if ( entry != null )
			{
				entry.m_Type = type;
				entry.m_Expiration = expiration;
			}
			else
			{
				m_Table[mob] = entry = new BlockEntry( type, expiration );
			}
		}

		public static void ClearMountPrevention( Mobile mob )
		{
			if ( mob != null )
				m_Table.Remove( mob );
		}

		public static BlockMountType GetMountPrevention( Mobile mob )
		{
			if ( mob == null )
				return BlockMountType.None;

			BlockEntry entry = m_Table[mob] as BlockEntry;

			if ( entry == null )
				return BlockMountType.None;

			if ( entry.IsExpired )
			{
				m_Table.Remove( mob );
				return BlockMountType.None;
			}

			return entry.m_Type;
		}

		public static bool CheckMountAllowed( Mobile mob, bool message )
		{
			BlockMountType type = GetMountPrevention( mob );

			if ( type == BlockMountType.None )
				return true;

			if ( message )
			{
				switch ( type )
				{
					case BlockMountType.Dazed:
					{
						mob.SendLocalizedMessage( 1040024 ); // You are still too dazed from being knocked off your mount to ride!
						break;
					}
					case BlockMountType.BolaRecovery:
					{
						mob.SendLocalizedMessage( 1062910 ); // You cannot mount while recovering from a bola throw.
						break;
					}
					case BlockMountType.DismountRecovery:
					{
						mob.SendLocalizedMessage( 1070859 ); // You cannot mount while recovering from a dismount special maneuver.
						break;
					}
				}
			}

			return false;
		}

		public virtual void OnRiderDamaged( int amount, Mobile from, bool willKill )
		{
			if( m_Rider == null )
				return;

			Mobile attacker = from;
			if( attacker == null )
				attacker = m_Rider.FindMostRecentDamager( true );

			if( !(attacker == this || attacker == m_Rider || willKill || DateTime.UtcNow < m_NextMountAbility) )
			{
				if( DoMountAbility( amount, from ) )
					m_NextMountAbility = DateTime.UtcNow + MountAbilityDelay;

			}
		}

		public virtual bool DoMountAbility( int damage, Mobile attacker )
		{
			return false;
		}
	}

	public class MountItem : Item, IMountItem
	{
		private BaseMount m_Mount;

		public override double DefaultWeight { get { return 0; } }

		public MountItem( BaseMount mount, int itemID ) : base( itemID )
		{
			Layer = Layer.Mount;
			Movable = false;

			m_Mount = mount;
		}

		public MountItem( Serial serial ) : base( serial )
		{
		}

		public override void OnAfterDelete()
		{
			if ( m_Mount != null )
				m_Mount.Delete();

			m_Mount = null;

			base.OnAfterDelete();
		}

		public override DeathMoveResult OnParentDeath(Mobile parent)
		{
			if ( m_Mount != null )
			{
				if ( m_Mount.Rider != null ){ Server.Mobiles.EtherealMount.EthyDismount( m_Mount.Rider, true ); }
				m_Mount.Rider = null;
			}

			return DeathMoveResult.RemainEquiped;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Mount );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Mount = reader.ReadMobile() as BaseMount;

					if ( m_Mount == null )
						Delete();

					break;
				}
			}
		}

		public IMount Mount
		{
			get
			{
				return m_Mount;
			}
		}
	}

	public enum BlockMountType
	{
		None = -1,
		Dazed,
		BolaRecovery,
		DismountRecovery
	}
}
