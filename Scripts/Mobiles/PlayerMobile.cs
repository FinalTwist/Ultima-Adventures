using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Engines.Help;
using Server.ContextMenus;
using Server.Network;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Seventh;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Spells.Bushido;
using Server.Spells.Song;
using Server.Targeting;
using Server.Engines.Quests;
using Server.Regions;
using Server.Accounting;
using Server.Engines.CannedEvil;
using Server.Engines.Craft;
using Server.Engines.PartySystem;
using Server.Poker; // Poker edit
using Server.Engines.Soulbound;

namespace Server.Mobiles
{
	#region Enums
	[Flags]
	public enum PlayerFlag // First 16 bits are reserved for default-distro use, start custom flags at 0x00010000
	{
		None				= 0x00000000,
		Glassblowing			= 0x00000001,
		Masonry				= 0x00000002,
		SandMining			= 0x00000004,
		StoneMining			= 0x00000008,
		ToggleMiningStone		= 0x00000010,
		KarmaLocked			= 0x00000020,
		IsAutomated			= 0x00000040,
		UseOwnFilter			= 0x00000080,
		Avatar				= 0x00000100,
		IsZen				= 0x00000200,
		Young				= 0x00000400,
		AcceptGuildInvites		= 0x00000800,
		DisplayChampionTitle		= 0x00001000,
		PublicMyRunUO			= 0x00004000,
		JustWoreRing			= 0x00013000,
		InBed				= 0x00015000,
		WellRested			= 0x00016000,
		HasStatReward			= 0x00018000
	}

	public enum NpcGuild
	{
		None,
		MagesGuild,
		WarriorsGuild,
		ThievesGuild,
		RangersGuild,
		HealersGuild,
		MinersGuild,
		MerchantsGuild,
		TinkersGuild,
		TailorsGuild,
		FishermensGuild,
		BardsGuild,
		BlacksmithsGuild,
		NecromancersGuild,
		AlchemistsGuild,
		DruidsGuild,
		ArchersGuild,
		CarpentersGuild,
		CartographersGuild,
		LibrariansGuild,
		CulinariansGuild,
		AssassinsGuild
	}

	public enum SolenFriendship
	{
		None,
		Red,
		Black
	}
	#endregion

	public partial class PlayerMobile : Mobile, IHonorTarget
	{


		// DisruptAfk mod
		private bool _DisruptAfk = false;
		public bool DisruptAfk { get { return _DisruptAfk; } set { _DisruptAfk = value; } }

		private class CountAndTimeStamp
		{
			private int m_Count;
			private DateTime m_Stamp;

			public CountAndTimeStamp()
			{
			}

			public DateTime TimeStamp { get{ return m_Stamp; } }
			public int Count
			{
				get { return m_Count; }
				set	{ m_Count = value; m_Stamp = DateTime.UtcNow; }
			}
		}

		private DesignContext m_DesignContext;

		private NpcGuild m_NpcGuild;
		private DateTime m_NpcGuildJoinTime;
		private DateTime m_NextBODTurnInTime;
		private TimeSpan m_NpcGuildGameTime;
		private PlayerFlag m_Flags;
		public bool High;
		private int m_StepsTaken;
		private int m_Profession;
		private bool m_IsStealthing; // IsStealthing should be moved to Server.Mobiles
		private bool m_IgnoreMobiles; // IgnoreMobiles should be moved to Server.Mobiles
		private bool m_NinjaWepCooldown;
		private int m_SoulForce;
		private bool m_InGauntlet;
		private int m_LastGauntletLevel;
		private bool m_SbRes;
		public bool SbRes
		{ 
			get { return m_SbRes; } 
			set { m_SbRes = value; } 
		}
		private DateTime m_TombstDelay;
		public DateTime TombstDelay
		{ 
			get { return m_TombstDelay; } 
			set { m_TombstDelay = value; } 
		}
		private DateTime m_LastLogout;
		public DateTime LastLogout
		{ 
			get { return m_LastLogout; } 
			set { m_LastLogout = value; } 
		}
		private DateTime m_LastLogin;
		public DateTime LastLogin
		{
			get { return m_LastLogin; } 
			set { m_LastLogin = value; } 
		}
		private bool m_SbResTimer;
		public bool SbResTimer
		{ 
			get { return m_SbResTimer; } 
			set { m_SbResTimer = value; } 
		}
		private bool m_flagged;
		public bool flagged
		{ 
			get { return m_flagged; } 
			set { m_flagged = value; } 
		}
		private bool m_caught;
		public bool caught
		{ 
			get { return m_caught; } 
			set { m_caught = value; } 
		}
		private Body m_OriginalBody;
		private List<SongEffect> m_SongEffects;
		public List<SongEffect> SongEffects 
		{ 
			get { return m_SongEffects; } 
			set { m_SongEffects = value; } 
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int SoulForce {
			get { return m_SoulForce; }
			set { m_SoulForce = value;}
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int LastGauntletLevel {
			get { return m_LastGauntletLevel; }
			set { m_LastGauntletLevel = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool InGauntlet {
			get { return m_InGauntlet; }
			set { m_InGauntlet = value; }
		}
		public Body OriginalBody {
			get { return m_OriginalBody; } 
			set { m_OriginalBody = value; } 
		}

		/*
		 * a value of zero means, that the mobile is not executing the spell. Otherwise,
		 * the value should match the BaseMana required
		*/
		private int m_ExecutesLightningStrike; // move to Server.Mobiles??

		private DateTime m_LastOnline;
		private Server.Guilds.RankDefinition m_GuildRank;

		private int m_GuildMessageHue, m_ExtraSlots;

		private List<Mobile> m_AutoStabled;
		private List<Mobile> m_AllFollowers;
		private List<Mobile> m_RecentlyReported;

		private bool m_sorc;
		private bool m_troub;
		private bool m_alch;

  		private bool m_soulboundresurrect;
    		private int m_mentalexhaustcount;
     		public int MentalExhaustCount
		{
			get { return m_mentalexhaustcount; }
			set { m_mentalexhaustcount = value; }
		}

		private PokerGame m_PokerGame; //Edit for Poker System
		public PokerGame PokerGame
		{
			get { return m_PokerGame; }
			set { m_PokerGame = value; }
		}

		#region Getters & Setters

		public List<Mobile> RecentlyReported
		{
			get
			{
				return m_RecentlyReported;
			}
			set
			{
				m_RecentlyReported = value;
			}
		}

		public List<Mobile> AutoStabled { get { return m_AutoStabled; } }

		public bool NinjaWepCooldown
		{
			get
			{
				return m_NinjaWepCooldown;
			}
			set
			{
				m_NinjaWepCooldown = value;
			}
		}

		public List<Mobile> AllFollowers
		{
			get
			{
				if( m_AllFollowers == null )
					m_AllFollowers = new List<Mobile>();
				return m_AllFollowers;
			}
		}

		public Server.Guilds.RankDefinition GuildRank
		{
			get
			{
				if( this.AccessLevel >= AccessLevel.GameMaster )
					return Server.Guilds.RankDefinition.Leader;
				else
					return m_GuildRank;
			}
			set{ m_GuildRank = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int GuildMessageHue
		{
			get{ return m_GuildMessageHue; }
			set{ m_GuildMessageHue = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ExtraSlots
		{
			get { return m_ExtraSlots; }
			set { m_ExtraSlots = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Profession
		{
			get{ return m_Profession; }
			set{ m_Profession = value; }
		}

		public int StepsTaken
		{
			get{ return m_StepsTaken; }
			set{ m_StepsTaken = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsStealthing // IsStealthing should be moved to Server.Mobiles
		{
			get { return m_IsStealthing; }
			set { m_IsStealthing = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IgnoreMobiles // IgnoreMobiles should be moved to Server.Mobiles
		{
			get
			{
				return m_IgnoreMobiles;
			}
			set
			{
				if( m_IgnoreMobiles != value )
				{
					m_IgnoreMobiles = value;
					Delta( MobileDelta.Flags );
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public NpcGuild NpcGuild
		{
			get{ return m_NpcGuild; }
			set{ m_NpcGuild = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NpcGuildJoinTime
		{
			get{ return m_NpcGuildJoinTime; }
			set{ m_NpcGuildJoinTime = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextBODTurnInTime
		{
			get{ return m_NextBODTurnInTime; }
			set{ m_NextBODTurnInTime = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastOnline
		{
			get{ return m_LastOnline; }
			set{ m_LastOnline = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public long LastMoved
		{
			get{ return LastMoveTime; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NpcGuildGameTime
		{
			get{ return m_NpcGuildGameTime; }
			set{ m_NpcGuildGameTime = value; }
		}

		private int m_BalanceStatus; //-1 is evil, 1 is good

		[CommandProperty( AccessLevel.GameMaster )]
		public int BalanceStatus
		{
			get { return m_BalanceStatus; }
			set { m_BalanceStatus = value; }
		}

		private bool m_SoulBound;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SoulBound
		{
			get { return m_SoulBound; }
			set { m_SoulBound = value; }
		}

		private string m_lastwords;

		public string lastwords
		{
			get { return m_lastwords; }
			set { m_lastwords = value; }
		}

		private string m_lastdeeds;
		public string lastdeeds
		{
			get { return m_lastdeeds; }
			set { m_lastdeeds = value; }
		}

		private bool m_sbmaster;
		public bool sbmaster
		{
			get { return m_sbmaster; }
			set { m_sbmaster = value; }
		}

		private bool m_sbmasterspeed;
		public bool sbmasterspeed
		{
			get { return m_sbmasterspeed; }
			set { m_sbmasterspeed = value; }
		}

		private DateTime m_soulbounddate;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime SoulBoundDate
		{
			get { return m_soulbounddate; }
			set { m_soulbounddate = value; }
		}

		private DateTime m_lastautores;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastAutoRes
		{
			get { return m_lastautores; }
			set { m_lastautores = value; }
		}

		private int m_BalanceEffect;

		[CommandProperty( AccessLevel.GameMaster )]
		public int BalanceEffect
		{
			get { return m_BalanceEffect; }
			set { m_BalanceEffect = value; }
		}
		
		private int m_THC;

		[CommandProperty( AccessLevel.GameMaster )]
		public int THC
		{
			get { return m_THC; }
			set { m_THC = value; }
		}
		
		private int m_BlacksmithBOD;

		[CommandProperty( AccessLevel.GameMaster )]
		public int BlacksmithBOD
		{
			get { return m_BlacksmithBOD; }
			set { m_BlacksmithBOD = value; }
		}
		
		private int m_TailorBOD;

		[CommandProperty( AccessLevel.GameMaster )]
		public int TailorBOD
		{
			get { return m_TailorBOD; }
			set { m_TailorBOD = value; }
		}

		private int m_midhumans;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midhumans
		{
			get { return m_midhumans; }
			set { m_midhumans = value; }
		}

		private int m_midhumanacc;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midhumanacc
		{
			get { return m_midhumanacc; }
			set { m_midhumanacc = value; }
		}

		private int m_midgargoyles;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midgargoyles
		{
			get { return m_midgargoyles; }
			set { m_midgargoyles = value; }
		}

		private int m_midgargoyleacc;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midgargoyleacc
		{
			get { return m_midgargoyleacc; }
			set { m_midgargoyleacc = value; }
		}

		private int m_midorcs;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midorcs
		{
			get { return m_midorcs; }
			set { m_midorcs = value; }
		}

		private int m_midorcacc;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midorcacc
		{
			get { return m_midorcacc; }
			set { m_midorcacc = value; }
		}

		private int m_midlizards;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midlizards
		{
			get { return m_midlizards; }
			set { m_midlizards = value; }
		}

		private int m_midlizardacc;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midlizardacc
		{
			get { return m_midlizardacc; }
			set { m_midlizardacc = value; }
		}

		private int m_midpirates;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midpirates
		{
			get { return m_midpirates; }
			set { m_midpirates = value; }
		}

		private int m_midpirateacc;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midpirateacc
		{
			get { return m_midpirateacc; }
			set { m_midpirateacc = value; }
		}

		private int m_midrace;

		[CommandProperty( AccessLevel.GameMaster )]
		public int midrace
		{
			get { return m_midrace; }
			set { m_midrace = value; }
		}

		public int ExecutesLightningStrike
		{
			get { return m_ExecutesLightningStrike; }
			set { m_ExecutesLightningStrike = value; }
		}

		#endregion

		#region PlayerFlags
		public PlayerFlag Flags
		{
			get{ return m_Flags; }
			set{ m_Flags = value; }
		}
		
		private bool m_IsZen;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool IsZen
        {
            get{ return m_IsZen; }
            set{ m_IsZen = value; }
        }
		
		private int m_FastGain;
		[CommandProperty( AccessLevel.GameMaster )]
        public int FastGain
        {
            get{ return m_FastGain; }
            set{ m_FastGain = value; }
        }

		private int m_Stealthing;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Stealthing
        {
            get{ return m_Stealthing; }
            set{ m_Stealthing = value; }
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Glassblowing
		{
			get{ return GetFlag( PlayerFlag.Glassblowing ); }
			set{ SetFlag( PlayerFlag.Glassblowing, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Masonry
		{
			get{ return GetFlag( PlayerFlag.Masonry ); }
			set{ SetFlag( PlayerFlag.Masonry, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool SandMining
		{
			get{ return GetFlag( PlayerFlag.SandMining ); }
			set{ SetFlag( PlayerFlag.SandMining, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool StoneMining
		{
			get{ return GetFlag( PlayerFlag.StoneMining ); }
			set{ SetFlag( PlayerFlag.StoneMining, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool ToggleMiningStone
		{
			get{ return GetFlag( PlayerFlag.ToggleMiningStone ); }
			set{ SetFlag( PlayerFlag.ToggleMiningStone, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool KarmaLocked
		{
			get{ return GetFlag( PlayerFlag.KarmaLocked ); }
			set{ SetFlag( PlayerFlag.KarmaLocked, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsAutomated
		{
			get{ return GetFlag( PlayerFlag.IsAutomated ); }
			set{ SetFlag( PlayerFlag.IsAutomated, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool UseOwnFilter
		{
			get{ return GetFlag( PlayerFlag.UseOwnFilter ); }
			set{ SetFlag( PlayerFlag.UseOwnFilter, value ); }
		}

		private bool m_avatar;
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Avatar
		{
			get{ return GetFlag( PlayerFlag.Avatar ); }
			set{ SetFlag( PlayerFlag.Avatar, value ); m_avatar = value; InvalidateMyRunUO(); }
		}
		
		public bool PublicMyRunUO
		{
			get{ return GetFlag( PlayerFlag.PublicMyRunUO ); }
			set{ SetFlag( PlayerFlag.PublicMyRunUO, value ); InvalidateMyRunUO(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AcceptGuildInvites
		{
			get{ return GetFlag( PlayerFlag.AcceptGuildInvites ); }
			set{ SetFlag( PlayerFlag.AcceptGuildInvites, value ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HasStatReward
		{
			get{ return GetFlag( PlayerFlag.HasStatReward ); }
			set{ SetFlag( PlayerFlag.HasStatReward, value ); }
		}
		#endregion

		#region Auto Arrow Recovery
		private Dictionary<Type, int> m_RecoverableAmmo = new Dictionary<Type,int>();

		public Dictionary<Type, int> RecoverableAmmo
		{
			get { return m_RecoverableAmmo; }
		}

		public void RecoverAmmo()
		{
			if ( Core.SE && Alive )
			{
				foreach ( KeyValuePair<Type, int> kvp in m_RecoverableAmmo )
				{
					if ( kvp.Value > 0 )
					{
						Item ammo = null;

						try
						{
							ammo = Activator.CreateInstance( kvp.Key ) as Item;
						}
						catch
						{
						}

						if ( ammo != null )
						{
							string name = ammo.Name;
							ammo.Amount = kvp.Value;

							if ( name == null )
							{
								if ( ammo is Arrow )
									name = "arrow";
								else if ( ammo is Bolt )
									name = "bolt";
							}

							if ( name != null && ammo.Amount > 1 )
								name = String.Format( "{0}s", name );

							if ( name == null )
								name = String.Format( "#{0}", ammo.LabelNumber );

							PlaceInBackpack( ammo );
							Server.Gumps.QuickBar.RefreshQuickBar( this );
							SendLocalizedMessage( 1073504, String.Format( "{0}\t{1}", ammo.Amount, name ) ); // You recover ~1_NUM~ ~2_AMMO~.
						}
					}
				}

				m_RecoverableAmmo.Clear();
			}
		}

		#endregion

		private DateTime m_AnkhNextUse;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime AnkhNextUse
		{
			get{ return m_AnkhNextUse; }
			set{ m_AnkhNextUse = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan DisguiseTimeLeft
		{
			get{ return DisguiseTimers.TimeRemaining( this ); }
		}

		private DateTime m_PeacedUntil;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime PeacedUntil
		{
			get { return m_PeacedUntil; }
			set { m_PeacedUntil = value; }
		}

		#region Scroll of Alacrity
		private DateTime m_AcceleratedStart;

		[CommandProperty(AccessLevel.GameMaster)]
		public DateTime AcceleratedStart
		{
			get { return m_AcceleratedStart; }
			set { m_AcceleratedStart = value; }
		}

		private SkillName m_AcceleratedSkill;

		[CommandProperty(AccessLevel.GameMaster)]
		public SkillName AcceleratedSkill
		{
			get { return m_AcceleratedSkill; }
			set { m_AcceleratedSkill = value; }
		}
		#endregion

		public static Direction GetDirection4( Point3D from, Point3D to )
		{
			int dx = from.X - to.X;
			int dy = from.Y - to.Y;

			int rx = dx - dy;
			int ry = dx + dy;

			Direction ret;

			if ( rx >= 0 && ry >= 0 )
				ret = Direction.West;
			else if ( rx >= 0 && ry < 0 )
				ret = Direction.South;
			else if ( rx < 0 && ry < 0 )
				ret = Direction.East;
			else
				ret = Direction.North;

			return ret;
		}

		public override bool OnDroppedItemToWorld( Item item, Point3D location )
		{
			if ( !base.OnDroppedItemToWorld( item, location ) )
				return false;

			if (item.LootType == LootType.Ensouled) {
				this.SendLocalizedMessage( 1061218 ); // Dropping this item would result in all bound souls being released, destroying it
				return false;
			}

			IPooledEnumerable mobiles = Map.GetMobilesInRange( location, 0 );

			foreach ( Mobile m in mobiles )
			{
					if ( m.Z >= location.Z && m.Z < location.Z + 16 && ( !m.Hidden || m.AccessLevel == AccessLevel.Player ) )
				{
					mobiles.Free();
					return false;
				}
			}

			mobiles.Free();

			BounceInfo bi = item.GetBounce();

			if ( bi != null )
			{
				Type type = item.GetType();

				if ( type.IsDefined( typeof( FurnitureAttribute ), true ) || type.IsDefined( typeof( DynamicFlipingAttribute ), true ) )
				{
					object[] objs = type.GetCustomAttributes( typeof( FlipableAttribute ), true );

					if ( objs != null && objs.Length > 0 )
					{
						FlipableAttribute fp = objs[0] as FlipableAttribute;

						if ( fp != null )
						{
							int[] itemIDs = fp.ItemIDs;

							Point3D oldWorldLoc = bi.m_WorldLoc;
							Point3D newWorldLoc = location;

							if ( oldWorldLoc.X != newWorldLoc.X || oldWorldLoc.Y != newWorldLoc.Y )
							{
								Direction dir = GetDirection4( oldWorldLoc, newWorldLoc );

								if ( itemIDs.Length == 2 )
								{
									switch ( dir )
									{
										case Direction.North:
										case Direction.South: item.ItemID = itemIDs[0]; break;
										case Direction.East:
										case Direction.West: item.ItemID = itemIDs[1]; break;
									}
								}
								else if ( itemIDs.Length == 4 )
								{
									switch ( dir )
									{
										case Direction.South: item.ItemID = itemIDs[0]; break;
										case Direction.East: item.ItemID = itemIDs[1]; break;
										case Direction.North: item.ItemID = itemIDs[2]; break;
										case Direction.West: item.ItemID = itemIDs[3]; break;
									}
								}
							}
						}
					}
				}
			}

			BaseHouse house = BaseHouse.FindHouseAt( location, Map, 2 );
			if (house != null && house.IsCoOwner((Mobile)this))
			{
				int amt = 1 + item.TotalItems;
				if (!house.CheckAosLockdowns( amt ) || !house.CheckAosStorage( amt ))
				{
					SendMessage("This would exceed the house's maximum items allowance.");
					return false;
				}
				else if (!(item is Container))
					house.LockDown( (Mobile)this, item );
					//house.SetLockdown( item, true );.
				else 
					house.AddSecure( (Mobile)this, item );
					//house.LockDown( (Mobile)this, item );
					//house.SetSecure((Mobile)this, item);
			}

			return true;
		}

		public override void Lift(Item item, int amount, out bool rejected, out LRReason reject)
		{

			if (BaseHouse.CheckLockedDownOrSecured(item))
			{
				rejected = true;
				reject = LRReason.Inspecific;
				BaseHouse house = BaseHouse.FindHouseAt( item.Location, Map, 2 );
				if (house != null)
				{
					if (house.IsCoOwner( (Mobile)this ))
					{
						//if (item is Container)
						//	house.ReleaseSecure( (Mobile)this, item );
						//else 

						house.Release( (Mobile)this, item );
						//item.Movable = true;
						rejected = false;
						//house.Release((Mobile)this, item);
					}
				}
			}
			
			base.Lift(item, amount, out rejected, out reject);
		}

		public override int GetPacketFlags()
		{
			int flags = base.GetPacketFlags();

			if ( m_IgnoreMobiles )
				flags |= 0x10;

			return flags;
		}

		public override int GetOldPacketFlags()
		{
			int flags = base.GetOldPacketFlags();

			if ( m_IgnoreMobiles )
				flags |= 0x10;

			return flags;
		}

		public bool GetFlag( PlayerFlag flag )
		{
			return ( (m_Flags & flag) != 0 );
		}

		public void SetFlag( PlayerFlag flag, bool value )
		{
			if ( value )
				m_Flags |= flag;
			else
				m_Flags &= ~flag;
		}

		public DesignContext DesignContext
		{
			get{ return m_DesignContext; }
			set{ m_DesignContext = value; }
		}

		public static void Initialize()
		{
			if ( FastwalkPrevention )
				PacketHandlers.RegisterThrottler( 0x02, new ThrottlePacketCallback( MovementThrottle_Callback ) );

			EventSink.Login += new LoginEventHandler( OnLogin );
			EventSink.Logout += new LogoutEventHandler( OnLogout );
			EventSink.Connected += new ConnectedEventHandler( EventSink_Connected );
			EventSink.Disconnected += new DisconnectedEventHandler( EventSink_Disconnected );

			if( Core.SE )
			{
				Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckPets ) );
			}

		}

		private static void CheckPets()
		{
			foreach( Mobile m in World.Mobiles.Values )
			{
				if( m is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)m;

					if((
						( !pm.Mounted || 
						( pm.Mount != null && pm.Mount is EtherealMount )) && ( pm.AllFollowers.Count > pm.AutoStabled.Count )) ||
						( pm.Mounted && ( pm.AllFollowers.Count  > ( pm.AutoStabled.Count +1 ))))
					{
						pm.AutoStablePets(); /* autostable checks summons, et al: no need here */
					}
				}
			}
		}

		public override void OnSkillInvalidated( Skill skill )
		{
			if ( skill.SkillName == SkillName.MagicResist )
				UpdateResistances();

			if ( skill.SkillName == SkillName.Herding || skill.SkillName == SkillName.Veterinary || skill.SkillName == SkillName.AnimalLore || skill.SkillName == SkillName.AnimalTaming )
			{
				UpdateFollowers();
				MaxFollowersIntelBased.Evaluate(this);
			}

			if ( ( skill.SkillName == SkillName.Necromancy ) && ( this.Hue == 0x47E ) && ( this.Skills[SkillName.Necromancy].Base < 100 ) )
			{
				this.Hue = Server.Misc.RandomThings.GetRandomSkinColor();
				this.HairHue = Utility.RandomHairHue();
			}
		}


		public override int GetMaxResistance( ResistanceType type )
		{
			if ( AccessLevel > AccessLevel.Player )
				return 100;

			int max = base.GetMaxResistance( type );

			if ( type != ResistanceType.Physical && 60 < max && Spells.Fourth.CurseSpell.UnderEffect( this ) )
				max = 60;
				
			if ( type != ResistanceType.Physical && !Spells.Fourth.CurseSpell.UnderEffect( this ) && max == 60) //bug where a player had stuck at 60
				max = 70;

			if( Core.ML && this.Race == Race.Elf && type == ResistanceType.Energy )
				max += 5; //Intended to go after the 60 max from curse

			if ( type == ResistanceType.Physical && Spells.Research.ResearchRockFlesh.UnderEffect( this ) )
				max = 90;

			return max;
		}

		protected override void OnRaceChange( Race oldRace )
		{
			ValidateEquipment();
			UpdateResistances();
		}

		public override int MaxWeight { get 
		{ 
			if (AdventuresFunctions.IsPuritain((object)this))
				return (((Core.ML && this.Race == Race.Human) ? 80 : 40) + (int)(3 * this.Str));

			return (((Core.ML && this.Race == Race.Human) ? 100 : 40) + (int)(4 * this.Str)); } }

		private int m_LastGlobalLight = -1, m_LastPersonalLight = -1;

		public override void OnNetStateChanged()
		{
			m_LastGlobalLight = -1;
			m_LastPersonalLight = -1;
		}

		public override void CriminalAction( bool message )
		{
			bool seen = false;
			if (!Hidden)
			{
				foreach ( Mobile witness in this.GetMobilesInRange( 12 ) ) 
				{
					if (witness is BaseCreature && ((BaseCreature)witness).CanSee(this) && ( witness.Body.IsHuman || witness is BaseBlue || witness is BaseChild || witness is Townsperson || witness is BaseRed || witness is BasePerson || witness is BaseVendor || witness is BaseCursed ) && !((BaseCreature)witness).IsEnemy(this) )
					{
						seen = true; 		
					}
				}
			}
			if (!seen)
			{
				this.SendMessage( 0,  "Your criminal act hasn't been seen." ); 
				return;
			}
			if (seen)
				this.SendMessage( 0,  "You've just been spotted doing a criminal action!" );

			base.CriminalAction(message);
		}

		public override void ComputeBaseLightLevels( out int global, out int personal )
		{
			global = LightCycle.ComputeLevelFor( this );

			if ( this.LightLevel < 13 && AosAttributes.GetValue( this, AosAttribute.NightSight ) > 0 )
				personal = 12;
			else
				personal = this.LightLevel;
		}

		public override void CheckLightLevels( bool forceResend )
		{
			if ( Profession == 1 )
			{
				Criminal = true;
				if ( Kills < 1 ){ Kills = 1; }
			}

			NetState ns = this.NetState;

			if ( ns == null )
				return;

			int global, personal;

			ComputeLightLevels( out global, out personal );

			if ( !forceResend )
				forceResend = ( global != m_LastGlobalLight || personal != m_LastPersonalLight );

			if ( !forceResend )
				return;

			m_LastGlobalLight = global;
			m_LastPersonalLight = personal;

			ns.Send( GlobalLightLevel.Instantiate( global ) );
			ns.Send( new PersonalLightLevel( this, personal ) );
		}

		public override int GetMinResistance( ResistanceType type )
		{
			int magicResist = (int)(Skills[SkillName.MagicResist].Value * 10);
			int min = int.MinValue;
			if (AdventuresFunctions.IsPuritain((object)this))
			{
				if ( magicResist >= 1000 )
					min = 10 + ((magicResist - 1000) / 50);
				else if ( magicResist >= 400 )
					min = (magicResist - 400) / 60;

				if ( min > MaxPlayerResistance )
					min = MaxPlayerResistance;
			}
			else
			{
				if ( magicResist >= 1000 )
					min = 40 + ((magicResist - 1000) / 50);
				else if ( magicResist >= 400 )
					min = (magicResist - 400) / 15;

				if ( min > MaxPlayerResistance )
					min = MaxPlayerResistance;
			}

			int baseMin = base.GetMinResistance( type );

			if (Alchemist())
			{
				int mini = baseMin;
				foreach ( Mobile m in GetMobilesInRange( 10 ) )
				{
					if ( m is FrankenFighter)
					{
						BaseCreature pet = (BaseCreature)m;

						if ( pet.Controlled && pet.ControlMaster == this )
						{
							int mm = 0;
							switch ( type )
							{
								case ResistanceType.Physical: mm= m.PhysicalResistance; break;
								case ResistanceType.Fire: mm = m.FireResistance; break;
								case ResistanceType.Cold: mm = m.ColdResistance; break;
								case ResistanceType.Poison: mm = m.PoisonResistance; break;
								case ResistanceType.Energy: mm = m.EnergyResistance; break;
							}
							if (mm > mini)
								mini = mm;
						}
					}
				}

				if (mini > baseMin )
					baseMin = mini;
			}

			if ( min < baseMin )
				min = baseMin;

			return min;
		}

		public override void OnManaChange(int oldValue)
		{
			base.OnManaChange(oldValue);
			if (m_ExecutesLightningStrike > 0)
			{
				if (Mana < m_ExecutesLightningStrike)
				{
					LightningStrike.ClearCurrentMove(this);
				}
			}
		}

		private static void OnLogin( LoginEventArgs e )
		{
			Mobile from = e.Mobile;

			CheckAtrophies( from );

			if ( AccountHandler.LockdownLevel > AccessLevel.Player )
			{
				string notice;

				Accounting.Account acct = from.Account as Accounting.Account;

				if ( acct == null || !acct.HasAccess( from.NetState ) )
				{
					if ( from.AccessLevel == AccessLevel.Player )
						notice = "The server is currently under lockdown. No players are allowed to log in at this time.";
					else
						notice = "The server is currently under lockdown. You do not have sufficient access level to connect.";

					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( Disconnect ), from );
				}
				else if ( from.AccessLevel >= AccessLevel.Administrator )
				{
					notice = "The server is currently under lockdown. As you are an administrator, you may change this from the [Admin gump.";
				}
				else
				{
					notice = "The server is currently under lockdown. You have sufficient access level to connect.";
				}

				from.SendGump( new NoticeGump( 1060637, 30720, notice, 0xFFC000, 300, 140, null, null ) );
				return;
			}

			if( from is PlayerMobile ) {
				PlayerMobile player = (PlayerMobile)from;
				player.TombstDelay = DateTime.UtcNow;
				Phylactery phylactery = player.FindPhylactery();
				if (phylactery != null) {
					phylactery.PhylacteryMods = new List<PhylacteryMod>();
					phylactery.UpdateOwnerSoul(player);
					if (phylactery.Temporary) {
						phylactery.Delete();
					}					
				}
				player.ClaimAutoStabledPets();
				player.UpdateResistances();
				player.UpdateFollowers();

				if (player.GetFlag( PlayerFlag.IsAutomated ))
					AdventuresAutomation.StopAction(player); 

				player.LastLogin = DateTime.Now;
				if ( player.GetFlag( PlayerFlag.InBed ) )
				{
					if ( (DateTime.Now - player.LastLogout) > TimeSpan.FromHours( 8 ) )
						player.SetFlag( PlayerFlag.WellRested, true );
				}

				if (player.THC > 0)
				{
					player.High = true;
					new Server.Items.Crops.DrugSystem_StonedTimer(player, player.THC).Start();
				}
				else if (player.THC <= 0 && player.High)
					player.High = false;
			}
			
			
		}

		public void UpdateFollowers()
		{
			int total = 0;
			if ( AllFollowers.Count > 0 )
			{
				for ( int i = m_AllFollowers.Count - 1; i >= 0; --i )
				{
					BaseCreature pet = AllFollowers[i] as BaseCreature;

					if (pet == null || pet.ControlMaster == null || pet.IsHitchStabled)
                        continue;
					
					total += pet.ControlSlots;

				}
			}
			Followers = total;
		}



		

		public Phylactery FindPhylactery() {
			Container backpack = this.Backpack;
			if (backpack != null) {
				Phylactery phylactery = (Phylactery)backpack.FindItemByType(typeof(Phylactery));
				if (phylactery != null) {
					return phylactery;
				}
			}
			return null;
		}

		private bool m_NoDeltaRecursion;

		public void ValidateEquipment()
		{
			if ( m_NoDeltaRecursion || Map == null || Map == Map.Internal )
				return;

			if ( this.Items == null )
				return;

			m_NoDeltaRecursion = true;
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( ValidateEquipment_Sandbox ) );
		}

		private void ValidateEquipment_Sandbox()
		{
			try
			{
				if ( Map == null || Map == Map.Internal )
					return;

				List<Item> items = this.Items;

				if ( items == null )
					return;

				bool moved = false;

				int str = this.Str;
				int dex = this.Dex;
				int intel = this.Int;


				Mobile from = this;
				MaxFollowersIntelBased.Evaluate(from);

				Sorcerer();
				Troubadour();

				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;

					Item item = items[i];



					if ( item is BaseWeapon )
					{
						BaseWeapon weapon = (BaseWeapon)item;

						bool drop = false;

						if( dex < weapon.DexRequirement )
							drop = true;
						else if( str < AOS.Scale( weapon.StrRequirement, 100 - weapon.GetLowerStatReq() ) )
							drop = true;
						else if( intel < weapon.IntRequirement )
							drop = true;
						else if( weapon.RequiredRace != null && weapon.RequiredRace != this.Race )
							drop = true;

						if ( drop )
						{
							string name = weapon.Name;

							if ( name == null )
								name = String.Format( "#{0}", weapon.LabelNumber );

							from.SendLocalizedMessage( 1062001, name ); // You can no longer wield your ~1_WEAPON~
							from.AddToBackpack( weapon );
							moved = true;
						}
					}
					else if ( item is BaseArmor )
					{
						BaseArmor armor = (BaseArmor)item;

						bool drop = false;

						if ( !armor.AllowMaleWearer && !from.Female && from.AccessLevel < AccessLevel.GameMaster )
						{
							drop = true;
						}
						else if ( !armor.AllowFemaleWearer && from.Female && from.AccessLevel < AccessLevel.GameMaster )
						{
							drop = true;
						}
						else if( armor.RequiredRace != null && armor.RequiredRace != this.Race )
						{
							drop = true;
						}
						else
						{
							int strBonus = armor.ComputeStatBonus( StatType.Str ), strReq = armor.ComputeStatReq( StatType.Str );
							int dexBonus = armor.ComputeStatBonus( StatType.Dex ), dexReq = armor.ComputeStatReq( StatType.Dex );
							int intBonus = armor.ComputeStatBonus( StatType.Int ), intReq = armor.ComputeStatReq( StatType.Int );

							if( dex < dexReq || (dex + dexBonus) < 1 )
								drop = true;
							else if( str < strReq || (str + strBonus) < 1 )
								drop = true;
							else if( intel < intReq || (intel + intBonus) < 1 )
								drop = true;
						}

						if ( drop )
						{
							string name = armor.Name;

							if ( name == null )
								name = String.Format( "#{0}", armor.LabelNumber );

							if ( armor is BaseShield )
								from.SendLocalizedMessage( 1062003, name ); // You can no longer equip your ~1_SHIELD~
							else
								from.SendLocalizedMessage( 1062002, name ); // You can no longer wear your ~1_ARMOR~

							from.AddToBackpack( armor );
							moved = true;
						}
					}
					else if ( item is BaseClothing )
					{
						BaseClothing clothing = (BaseClothing)item;

						bool drop = false;

						if ( !clothing.AllowMaleWearer && !from.Female && from.AccessLevel < AccessLevel.GameMaster )
						{
							drop = true;
						}
						else if ( !clothing.AllowFemaleWearer && from.Female && from.AccessLevel < AccessLevel.GameMaster )
						{
							drop = true;
						}
						else if( clothing.RequiredRace != null && clothing.RequiredRace != this.Race )
						{
							drop = true;
						}
						else
						{
							int strBonus = clothing.ComputeStatBonus( StatType.Str );
							int strReq = clothing.ComputeStatReq( StatType.Str );

							if( str < strReq || (str + strBonus) < 1 )
								drop = true;
						}

						if ( drop )
						{
							string name = clothing.Name;

							if ( name == null )
								name = String.Format( "#{0}", clothing.LabelNumber );

							from.SendLocalizedMessage( 1062002, name ); // You can no longer wear your ~1_ARMOR~

							from.AddToBackpack( clothing );
							moved = true;
						}
					}

				}

				if ( moved )
					from.SendLocalizedMessage( 500647 ); // Some equipment has been moved to your backpack.
			}
			catch ( Exception e )
			{
				Console.WriteLine( e );
			}
			finally
			{
				m_NoDeltaRecursion = false;
			}
		}

		public override void Delta( MobileDelta flag )
		{
			base.Delta( flag );

			if ( (flag & MobileDelta.Stat) != 0 )
				ValidateEquipment();

			if ( (flag & (MobileDelta.Name | MobileDelta.Hue)) != 0 )
				InvalidateMyRunUO();
		}

		private static void Disconnect( object state )
		{
			NetState ns = ((Mobile)state).NetState;

			if ( ns != null )
				ns.Dispose();
		}

		private static void OnLogout( LogoutEventArgs e )
		{
			if( e.Mobile is PlayerMobile ) {
				((PlayerMobile)e.Mobile).LastLogout = DateTime.Now;
				((PlayerMobile)e.Mobile).AutoStablePets();
			}
		}

		private static void EventSink_Connected( ConnectedEventArgs e )
		{
			PlayerMobile pm = e.Mobile as PlayerMobile;

			if ( pm != null )
			{
				pm.m_SessionStart = DateTime.UtcNow;

				if ( pm.m_Quest != null )
					pm.m_Quest.StartTimer();

				pm.BedrollLogout = false;
				pm.LastOnline = DateTime.UtcNow;
			}

			DisguiseTimers.StartTimer( e.Mobile );

			Timer.DelayCall( TimeSpan.Zero, new TimerStateCallback( ClearSpecialMovesCallback ), e.Mobile );
		}

		private static void ClearSpecialMovesCallback( object state )
		{
			Mobile from = (Mobile)state;

			SpecialMove.ClearAllMoves( from );
		}

		private static void EventSink_Disconnected( DisconnectedEventArgs e )
		{
			Mobile from = e.Mobile;
			DesignContext context = DesignContext.Find( from );

			if ( context != null )
			{
				/* Client disconnected
				 *  - Remove design context
				 *  - Eject all from house
				 *  - Restore relocated entities
				 */

				// Remove design context
				DesignContext.Remove( from );

				// Eject all from house
				from.RevealingAction();

				foreach ( Item item in context.Foundation.GetItems() )
					item.Location = context.Foundation.BanLocation;

				foreach ( Mobile mobile in context.Foundation.GetMobiles() )
					mobile.Location = context.Foundation.BanLocation;

				// Restore relocated entities
				context.Foundation.RestoreRelocatedEntities();
			}

			PlayerMobile pm = e.Mobile as PlayerMobile;

			if ( pm != null )
			{
				pm.m_GameTime += (DateTime.UtcNow - pm.m_SessionStart);

				if ( pm.m_Quest != null )
					pm.m_Quest.StopTimer();

				pm.m_SpeechLog = null;
				pm.LastOnline = DateTime.UtcNow;
			}

			DisguiseTimers.StopTimer( from );
		}

		public override void RevealingAction()
		{
			if ( m_DesignContext != null )
				return;

			Spells.Sixth.InvisibilitySpell.RemoveTimer( this );

			Item ring = this.FindItemOnLayer( Layer.Ring );
			if (ring != null && ring is OneRing)
				return;
				
			base.RevealingAction();

			m_IsStealthing = false; // IsStealthing should be moved to Server.Mobiles
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override bool Hidden
		{
			get
			{
				return base.Hidden;
			}
			set
			{
				base.Hidden = value;

				RemoveBuff( BuffIcon.Invisibility );	//Always remove, default to the hiding icon EXCEPT in the invis spell where it's explicitly set

				if( !Hidden )
				{
					RemoveBuff( BuffIcon.HidingAndOrStealth );
				}
				else// if( !InvisibilitySpell.HasTimer( this ) )
				{
					BuffInfo.AddBuff( this, new BuffInfo( BuffIcon.HidingAndOrStealth, 1075655 ) );	//Hidden/Stealthing & You Are Hidden
				}
			}
		}

		public override void OnSubItemAdded( Item item )
		{
			if ( AccessLevel < AccessLevel.GameMaster && item.IsChildOf( this.Backpack ) )
			{
				int maxWeight = WeightOverloading.GetMaxWeight( this );
				int curWeight = Mobile.BodyWeight + this.TotalWeight;

				if ( curWeight > maxWeight )
					this.SendLocalizedMessage( 1019035, true, String.Format( " : {0} / {1}", curWeight, maxWeight ) );
			}

			Server.Gumps.QuickBar.RefreshQuickBar( this );
			Server.Gumps.SkillListingGump.RefreshSkillList( this );
		}

		public override bool CanBeHarmful( Mobile target, bool message, bool ignoreOurBlessedness )
		{
			if ( m_DesignContext != null || (target is PlayerMobile && ((PlayerMobile)target).m_DesignContext != null) )
				return false;

			if ( (target is BaseVendor && ((BaseVendor)target).IsInvulnerable) || target is PlayerVendor || target is PlayerBarkeeper )
			{
				if ( message )
				{
					if ( target.Title == null )
						SendMessage( "{0} the vendor cannot be harmed.", target.Name );
					else
						SendMessage( "{0} {1} cannot be harmed.", target.Name, target.Title );
				}

				return false;
			}
			if (target is BaseCreature && Server.Misc.Worlds.GetRegionName( target.Map, target.Location ) == "the Basement" && ((BaseCreature)target).Controlled && ((BaseCreature)target).ControlMaster == this)
				return true;

			return base.CanBeHarmful( target, message, ignoreOurBlessedness );
		}

		public override bool CanBeBeneficial( Mobile target, bool message, bool allowDead )
		{
			if ( m_DesignContext != null || (target is PlayerMobile && ((PlayerMobile)target).m_DesignContext != null) )
				return false;

			return base.CanBeBeneficial( target, message, allowDead );
		}

		public override bool CheckContextMenuDisplay( IEntity target )
		{
			return ( m_DesignContext == null );
		}

		public override void OnItemAdded( Item item )
		{
			base.OnItemAdded( item );

			if ( item is BaseArmor || item is BaseWeapon )
			{
				Hits=Hits; Stam=Stam; Mana=Mana;
			}

			if ( this.NetState != null )
				CheckLightLevels( false );

			Server.Items.BarbaricSatchel.BarbaricRobe( item, this );
			Server.Gumps.QuickBar.RefreshQuickBar( this );
			Server.Gumps.SkillListingGump.RefreshSkillList( this );

			InvalidateMyRunUO();
		}



		public override void OnItemRemoved( Item item )
		{
			base.OnItemRemoved( item );

			if ( item is BaseArmor || item is BaseWeapon )
			{
				Hits=Hits; Stam=Stam; Mana=Mana;
			}

			if ( this.NetState != null )
				CheckLightLevels( false );

			InvalidateMyRunUO();
		}

		public override double ArmorRating
		{
			get
			{
				//BaseArmor ar;
				double rating = 0.0;

				AddArmorRating( ref rating, NeckArmor );
				AddArmorRating( ref rating, HandArmor );
				AddArmorRating( ref rating, HeadArmor );
				AddArmorRating( ref rating, ArmsArmor );
				AddArmorRating( ref rating, LegsArmor );
				AddArmorRating( ref rating, ChestArmor );
				AddArmorRating( ref rating, ShieldArmor );

				if( this.FindItemOnLayer( Layer.Shoes ) is BaseArmor )
					AddArmorRating( ref rating, (BaseArmor)(this.FindItemOnLayer( Layer.Shoes )));
				if( this.FindItemOnLayer( Layer.Cloak ) is BaseArmor )
					AddArmorRating( ref rating, (BaseArmor)(this.FindItemOnLayer( Layer.Cloak )));
				if( this.FindItemOnLayer( Layer.OuterTorso ) is BaseArmor )
					AddArmorRating( ref rating, (BaseArmor)(this.FindItemOnLayer( Layer.OuterTorso )));

				return VirtualArmor + VirtualArmorMod + rating;
			}
		}

		private void AddArmorRating( ref double rating, Item armor )
		{
			BaseArmor ar = armor as BaseArmor;

			if( ar != null && ( !Core.AOS || ar.ArmorAttributes.MageArmor == 0 ))
				rating += ar.ArmorRatingScaled;
		}

		#region [Stats]Max
		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax
		{
			get
			{
				int strBase;
				int strOffs = GetStatOffset( StatType.Str );

				strBase = this.Str;	//this.Str already includes GetStatOffset/str

				//scale the offset to hits from item bonuses with diminishing returns
				strOffs = Server.Misc.AdventuresFunctions.DiminishingReturns( AosAttributes.GetValue( this, AosAttribute.BonusHits ), MyServerSettings.StatBonusCap());

				if ( Core.ML && strOffs > MyServerSettings.StatBonusCap() && AccessLevel <= AccessLevel.Player )
					strOffs = MyServerSettings.StatBonusCap();

				if ( AnimalForm.UnderTransformation( this, typeof( MysticalFox ) ) || AnimalForm.UnderTransformation( this, typeof( GreyWolf ) ) )
					strOffs += 50;

				return ( Server.Misc.MyServerSettings.PlayerLevelMod( strBase, this ) ) + strOffs;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int StamMax
		{
			get{ 
				int stamOffs = Server.Misc.AdventuresFunctions.DiminishingReturns( AosAttributes.GetValue( this, AosAttribute.BonusStam ), MyServerSettings.StatBonusCap());
				if (stamOffs > MyServerSettings.StatBonusCap() && AccessLevel <= AccessLevel.Player) {
					stamOffs = MyServerSettings.StatBonusCap();
				}
				return ( Server.Misc.MyServerSettings.PlayerLevelMod( base.StamMax, this ) ) + stamOffs; 
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax
		{
			get{ 
				int manaOffs = Server.Misc.AdventuresFunctions.DiminishingReturns( AosAttributes.GetValue( this, AosAttribute.BonusMana ), MyServerSettings.StatBonusCap());
				if (manaOffs > MyServerSettings.StatBonusCap() && AccessLevel <= AccessLevel.Player) {
					manaOffs = MyServerSettings.StatBonusCap();
				}
				return ( Server.Misc.MyServerSettings.PlayerLevelMod( base.ManaMax, this ) ) + manaOffs; 
			}
		}
		#endregion

		#region Stat Getters/Setters

		[CommandProperty( AccessLevel.GameMaster )]
		public override int Str
		{
			get
			{
				if( Core.ML && this.AccessLevel == AccessLevel.Player )
					return Math.Min( base.Str, 500 );

				return base.Str;
			}
			set
			{
				base.Str = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int Int
		{
			get
			{
				if( Core.ML && this.AccessLevel == AccessLevel.Player )
					return Math.Min( base.Int, 500 );

				return base.Int;
			}
			set
			{
				base.Int = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int Dex
		{
			get
			{
				if( Core.ML && this.AccessLevel == AccessLevel.Player )
					return Math.Min( base.Dex, 500 );

				return base.Dex;
			}
			set
			{
				base.Dex = value;
			}
		}

		#endregion

		private DateTime m_NextSound;	
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override bool Move( Direction d )
		{	

			FlavorText( false );
			AmbientSounds( false );
							
			NetState ns = this.NetState;

			if ( ns != null )
			{
				if ( HasGump( typeof( ResurrectGump ) ) ) {
					if ( Alive ) {
						CloseGump( typeof( ResurrectGump ) );
					} else {
						SendLocalizedMessage( 500111 ); // You are frozen and cannot move.
						return false;
					}
				}
				else if ( HasGump( typeof( ResurrectCostGump ) ) ) {
					if ( Alive ) {
						CloseGump( typeof( ResurrectCostGump ) );
					} else {
						SendLocalizedMessage( 500111 ); // You are frozen and cannot move.
						return false;
					}
				}
				else if ( HasGump( typeof( ResurrectNowGump ) ) ) { if ( Alive ) { CloseGump( typeof( ResurrectNowGump ) ); } }
			}

			int speed = ComputeMovementSpeed(d);
			//TimeSpan speed = ComputeMovementSpeed( d );

			bool res;

			if ( !Alive )
				Server.Movement.MovementImpl.IgnoreMovableImpassables = true;

			res = base.Move( d );

			Server.Movement.MovementImpl.IgnoreMovableImpassables = false;

			if ( !res )
				return false;

			m_NextMovementTime += speed;

			return true;
		}

		public void AmbientSounds( bool test )
		{

			if (Utility.RandomMinMax(1, 500) == 69 || test)
			{
				// get region
				Region reg = Region.Find( this.Location, this.Map );

				// get tile
					string category = "";
					if ( reg.IsPartOf( typeof( TownRegion ) ) || reg.IsPartOf( typeof( VillageRegion ) ) ){										category = "city"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "forest" ) ){		category = "forest"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "swamp" ) ){		category = "swamp"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "jungle" ) ){		category = "jungle"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "sand" ) ){		category = "sand"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "cave" ) ){		category = "cave"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "dirt" ) ){		category = "dirt";}
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "snow" ) ){		category = "snow"; }
					//else if ( Server.Misc.Worlds.TestOcean ( this.Map, this.X, this.Y, 5 ) ){ 			category = "sea"; }


				// sounds
				if ( DateTime.UtcNow >= m_NextSound && MyServerSettings.EnableAmbientSoundEffects() && !(Worlds.IsOnBoat( this )) )
				{		
					int sound =0;
					
					if (category != "")
					{
						if (category == "city")
						{
							switch (Utility.Random(30))
							{
								case 0: this.PlaySound( 0x015 ); break;
								case 1: this.PlaySound( 0x017 ); break;
								case 2: this.PlaySound( 0x057 ); break;
								case 3: this.PlaySound( 0x23E ); break;
								case 4: this.PlaySound( 0x249 ); break;
								case 5: this.PlaySound( 0x02A ); break;
								case 6: this.PlaySound( 0x038 ); break;
								case 7: this.PlaySound( 0x039 ); break;
								case 8: this.PlaySound( 0x043 ); break;
								case 9: this.PlaySound( 0x044 ); break;
								case 10: this.PlaySound( 0x045 ); break;
								case 11: this.PlaySound( 0x046 ); break;
								case 12: this.PlaySound( 0x04C ); break;
								case 13: this.PlaySound( 0x04D ); break;
								case 14: this.PlaySound( 0x055 ); break;
								case 15: this.PlaySound( 0x070 ); break;
								case 16: this.PlaySound( 0x086 ); break;
								case 17: this.PlaySound( 0x226 ); break;
								case 18: this.PlaySound( 0x242 ); break;
								case 19: this.PlaySound( 0x30A ); break;
								case 20: this.PlaySound( 0x30B ); break;
								case 21: this.PlaySound( 0x30C ); break;
								case 22: this.PlaySound( 0x30D ); break;
								case 23: this.PlaySound( 0x30E ); break;
								case 24: this.PlaySound( 0x30F ); break;
								case 25: this.PlaySound( 0x310 ); break;
								case 26: this.PlaySound( 0x311 ); break;
								case 27: this.PlaySound( 0x312 ); break;
								case 28: this.PlaySound( 0x53B ); break;
								case 29: this.PlaySound( 0x53C ); break;

							}
						}
						else if (category == "forest")
						{
							switch (Utility.Random(8))
							{
								case 0: this.PlaySound( 0x002 ); break;
								case 1: this.PlaySound( 0x003 ); break;
								case 2: this.PlaySound( 0x009 ); break;
								case 3: this.PlaySound( 0x00A ); break;
								case 4: this.PlaySound( 0x001 ); break;
								case 5: this.PlaySound( 0x01A ); break;
								case 6: this.PlaySound( 0x07D ); break;
								case 7: this.PlaySound( 0x07E ); break;

							}
						}
						else if (category == "swamp")
						{
							switch (Utility.Random(7))
							{
								case 0: this.PlaySound( 0x007 ); break;
								case 1: this.PlaySound( 0x008 ); break;
								case 2: this.PlaySound( 0x00D ); break;
								case 3: this.PlaySound( 0x006 ); break;
								case 4: this.PlaySound( 0x003 ); break;
								case 5: this.PlaySound( 0x2B6 ); break;
								case 6: this.PlaySound( 0x2B7 ); break;

							}
						}
						else if (category == "jungle")
						{
							switch (Utility.Random(8))
							{
								case 0: this.PlaySound( 0x004 ); break;
								case 1: this.PlaySound( 0x005 ); break;
								case 2: this.PlaySound( 0x00B ); break;
								case 3: this.PlaySound( 0x003 ); break;
								case 4: this.PlaySound( 0x09E ); break;
								case 5: this.PlaySound( 0x2B3 ); break;
								case 6: this.PlaySound( 0x2B4 ); break;
								case 7: this.PlaySound( 0x2B5 ); break;

							}
						}
						else if (category == "sand")
						{
							switch (Utility.Random(4))
							{
								case 0: this.PlaySound( 0x655 ); break;
								case 1: this.PlaySound( 0x12E ); break;
								case 2: this.PlaySound( 0x014 ); break;
								case 3: this.PlaySound( 0x015 ); break;

							}
						}
						else if (category == "cave")
						{
							switch (Utility.Random(8))
							{
								case 0: this.PlaySound( 0x668 ); break;
								case 1: this.PlaySound( 0x669 ); break;
								case 2: this.PlaySound( 0x65B ); break;
								case 3: this.PlaySound( 0x011 ); break;
								case 4: this.PlaySound( 0x0EF ); break;
								case 5: this.PlaySound( 0x11F ); break;
								case 6: this.PlaySound( 0x2DE ); break;
								case 7: this.PlaySound( 0x2DF ); break;
							}
						}
						else if (category == "dirt")
						{
							switch (Utility.Random(7))
							{
								case 0: this.PlaySound( 0x002 ); break;
								case 1: this.PlaySound( 0x003 ); break;
								case 2: this.PlaySound( 0x009 ); break;
								case 3: this.PlaySound( 0x00A ); break;
								case 4: this.PlaySound( 0x019 ); break;
								case 5: this.PlaySound( 0x01F ); break;
								case 6: this.PlaySound( 0x09D ); break;

							}
						}
						else if (category == "snow")
						{
							switch (Utility.Random(3))
							{
								case 0: this.PlaySound( 0x5C7 ); break;
								case 1: this.PlaySound( 0x016 ); break;
								case 2: this.PlaySound( 0x28F ); break;

							}
						}
						else if (category == "sea")
						{
							switch (Utility.Random(5))
							{
								case 0: this.PlaySound( 0x012 ); break;
								case 1: this.PlaySound( 0x013 ); break;
								case 2: this.PlaySound( 0x014 ); break;
								case 3: this.PlaySound( 0x026 ); break;
								case 4: this.PlaySound( 0x364 ); break;
							}
						}	
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax(30, 240)) );	
				} 
			}
		}

		public void FlavorText( bool test)
		{

			// check to make sure this doesn't happen too often
			if ((Utility.RandomMinMax(1, 500) == 69 && Utility.RandomDouble() > 0.90) || test)
			{
				// get region
				Region reg = Region.Find( this.Location, this.Map );

				// get tile
					string category = "";
					if ( reg.IsPartOf( typeof( TownRegion ) ) || reg.IsPartOf( typeof( VillageRegion ) ) ){										category = "city"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "forest" ) ){		category = "forest"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "swamp" ) ){		category = "swamp"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "jungle" ) ){		category = "jungle"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "sand" ) ){		category = "sand"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "cave" ) ){		category = "cave"; }
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "dirt" ) ){		category = "dirt";}
					else if ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "snow" ) ){		category = "snow"; }
					//else if ( Server.Misc.Worlds.TestOcean ( this.Map, this.X, this.Y, 5 ) ){ 			category = "sea"; }

				// flavor text

				string flavortext = "";
				
				//situational awareness of mobiles
				foreach ( Mobile mobile in this.GetMobilesInRange( 5 ) )
				{
					if (CanSee(mobile) && InLOS(mobile) && Utility.RandomBool())
					{
						if (mobile is BaseVendor)
						{
							switch (Utility.Random(10))
							{
								case 0: flavortext = "A Vendor looks up at you, expectantly... He nervously holds his breath, hoping you'll speak to him."; break;
								case 1: flavortext = "A vendor quickly hides something behind his back as you approach.  You think you noticed something gleaming."; break;
								case 2: flavortext = "You notice a gruff vendor nearby, he must have had a tough day - he shrugs and continues to order his wares."; break;
								case 3: flavortext = "A vendor appears joyful and he looks up at you expectantly."; break;
								case 4: flavortext = "You hear a vendor nearby mention taxes, and spits, leaving a hulking puddle of muccus on the ground."; break;
								case 5: flavortext = "You feel someone nearby who wants to sell you something..."; break;
								case 6: flavortext = "A vendor waves for you to speak to him."; break;
								case 7: flavortext = "A vendor waves, showing his wares."; break;
								case 8: flavortext = "A vendor nearby re-arranges his wares, for what seems like the hundreth time."; break;
								case 9: flavortext = "You hear metal clicking as someone counts a few coins nearby."; break;
							}
						}
						if (mobile is BaseRed)
						{
							switch (Utility.Random(10))
							{
								case 0: flavortext = "A nearby killer smirks at his sword, showing yellowed teeth."; break;
								case 1: flavortext = "A villain nearby spits on the ground, then starts to violently hack and cough.."; break;
								case 2: flavortext = "You hear someone nearby mutter something about ' ... a stupid blue'."; break;
								case 3: flavortext = "You see someone nearby clean the blood of of his weapon with a rag, and lick the shiny metal."; break;
								case 4: flavortext = "You hear someone hum an evil tune."; break;
								case 5: flavortext = "Someone nearby mutters a prayer to Makal, the lord of destruction."; break;
								case 6: flavortext = "Someone nearby mutters something about ' ... more blood for the offering'."; break;
								case 7: flavortext = "A killer stands nearby, ready to pounce and kill any living being."; break;
								case 8: flavortext = "You hear someone nearby mutter something about '... having to give frank at the pit a good thrashing'."; break;
								case 9: flavortext = "Someone nearby slowly sharpens their weapon."; break;
							}
						}
						if (mobile is BlueGuard)
						{
							switch (Utility.Random(10))
							{
								case 0: flavortext = "You hear the heavy clank of a guard's solid metal armor, and a guard mutter '... not again'."; break;
								case 1: flavortext = "You hear a guard mutter about having to use the facilities... "; break;
								case 2: flavortext = "A guard nearby complains about a rock in his armor '... it hurts there!' he exclaims."; break;
								case 3: flavortext = "You see a guard nearby, slowly humming a tune dedicated to a long lost love."; break;
								case 4: flavortext = "A guard kicks a rock nearby, hitting a small animal, who yelps and scurries away."; break;
								case 5: flavortext = "A guard steadies his armor, fastening a plate that has gone loose,"; break;
								case 6: flavortext = "You hear a guard mutter about a creaky elbow guard and needing some oil from the local tinker."; break;
								case 7: flavortext = "A guard mutters something about being happy to be out of his house and away from his wife."; break;
								case 8: flavortext = "You hear a guard quietly curse a child who stole his favorite button."; break;
								case 9: flavortext = "You think you hear a guard ... urinate in his armor??"; break;


							}
						}
						if (mobile is BaseChild)
						{
							switch (Utility.Random(10))// 
							{
								case 0: flavortext = "A child nearby sobs, then cries gently - missing his parents no doubt and having been kicked out from his home."; break;
								case 1: flavortext = "A child nearby looks at your intently, wondering if you have anything of value..."; break;
								case 2: flavortext = "You see one of the urchins of the city play with a round apparatus that appears to blink in and out of existence... what is that???"; break;
								case 3: flavortext = "You hear a child mumble '.. never going back home!  I'm going to be a knight!'."; break;
								case 4: flavortext = "A child's ball hits a nearby tree and a shower of acrons fall to the ground - the child whoops in delight."; break;
								case 5: flavortext = "You see a child nearby sheepishly open a brown bag and look inside, then suddenly yell in delight!"; break;
								case 6: flavortext = "A child nearby cries out in pain as a rock digs in his foot through his broken and tattered shoes. "; break;
								case 7: flavortext = "You hear an urchin sob nearby, muttering about not having a father anymore because some evil adventurer killed him."; break;
								case 8: flavortext = "A child look at you with awe in his eyes, a world of amazing possibilities suddenly opens up in his mind but are quickly shattered by your indifference."; break;
								case 9: flavortext = "You notice a child unzipping his pants and peeing on a small grasshopper.  The grasshopper jumps on his leg and the child yelps in disgust!"; break;


							}
						}
						if (mobile is BaseCreature && ((BaseCreature)mobile).Body.IsHuman && !((BaseCreature)mobile).IsEnemy(this))
						{
							switch (Utility.Random(10)) //
							{
								case 0: flavortext = "A citizen of these lands whistles a gentle tune that reminds you of your childhood."; break;
								case 1: flavortext = "Something about that person reminds you of your own parent - could it be a relative...?"; break;
								case 2: flavortext = "Someone stumbles on a rock and curses to the god of rats, spitting on the ground."; break;
								case 3: flavortext = "Someone looks up at you, examines your armor, and shakes his head dissaprovingly."; break;
								case 4: flavortext = "Someone looks up at you, examines your armor, and ghasps in amazement."; break;
								case 5: flavortext = "You feel someone examining you itently, studying your every move."; break;
								case 6: flavortext = "A citizen gently nods at you, then looks away in disgust - What just happened??"; break;
								case 7: flavortext = "A citizen looks up at the sky, takes a deep breath and raises both arms, exclaiming something about Ophidians wanting to take over the world.  He must be crazy."; break;
								case 8: flavortext = "A person in tattered clothes squats nearby, and lets out a huge sigh of relief.  Are they going to do a number two?"; break;
								case 9: flavortext = "You see someone taking out their coinpouch, counting the contents and cursing the gods.  So much for that ale, he mutters"; break;
							}
						}
					}
				}
				//situational awareness of items nearby
				if (flavortext == "" && Utility.RandomBool())
				{
					foreach (Item item in GetItemsInRange(5))
					{
						if (item is StoryItem)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "You spot a strange item with markings nearby.  Maybe it tells of a story?"; break;
								case 1: flavortext = "What's that thing on the ground there?  It seems to have markings on it."; break;
								case 2: flavortext = "An adventurer took some time to write down his adventures - you can read them on that item nearby."; break;
							}
						}
						if (item is TombStone)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "You spot a cracked marble tombstone, weathered with age.  You wonder when this adventurer passed here."; break;
								case 1: flavortext = "A tombstone with something written on it lies nearby, a remnant of an adventurer who also wandered these lands."; break;
								case 2: flavortext = "A tombstone, evidence you are not the first to tread here, lies nearby.  You wonder what finally ended this adventurer's life."; break;
							}
						}
						if (item is CorpseItem)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "Old, dried, crackling bones lie in a heap on the floor.  You wonder why no one has bothered to move them... or do something more sinister with them..."; break;
								case 1: flavortext = "You see old bones on the ground and peer into the empty eye sockets of the skull.  It seems to look into your very being."; break;
								case 2: flavortext = "You spot old bones nearby, devoid of flesh.  A few insects crawl in and out of the heap, feasting on what little remains of the adventurer."; break;
							}
						}
						if (item is BaseDoor)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "An old door, weathered with age creaks nearby.  Did something just move it?"; break;
								case 1: flavortext = "You see markings scratched on the old door: '.. fluffy pantypants was here'.  "; break;
								case 2: flavortext = "You see something written on a door near you: '... for a good time, visit bella in moon'."; break;
							}
						}
						if (item is HiddenTrap)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "The hair on your neck suddenly stands erect.  There is danger nearby!"; break;
								case 1: flavortext = "A cold chill passes through you - there is danger nearby!"; break;
								case 2: flavortext = "You sense a devious mechanism nearby.. placed here by an evil presence long ago."; break;
							}
						}
						if (item is TrashBarrel)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "The trash barrel who talks trash seems to settle in place, lonely and looking for companionship."; break;
								case 1: flavortext = "The trash talking barrel sit there, dejected and lonely, hoping to entertain a passerby."; break;
								case 2: flavortext = "A trash talking barrel sits nearby.  You can almost see tears of lonelyness slowly drip down to the ground."; break;
							}
						}
						if (item is BaseWeapon)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "A glint of metal catches your eye nearby.  Did you leave that weapon there?"; break;
								case 1: flavortext = "A discraded weapon lays on the ground, you wonder who could have left this piece here."; break;
								case 2: flavortext = "A discarded weapon lays on the ground nearby - scratched and worn by battle, its previous owner failed to give it the honor it is owed."; break;
							}
						}
						if (item is BaseShoppe)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "One of the items in the shoppe shelf starts moving slowly to the left... then clanks down."; break;
								case 1: flavortext = "A small critter peeks its head out of the shoppe, looks at you, and skitters off."; break;
								case 2: flavortext = "Dust starts accumulating on the shelves of the shoppe."; break;
							}
						}
						if (item is Coffer)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "A small metallic box catches your eye - it is locked?  "; break;
								case 1: flavortext = "A small metal box seems to hold something special, someone opened it recently"; break;
								case 2: flavortext = "A metal box sits near you, a nearby merchant completely oblivious to the presence of would be thieves..."; break;
							}
						}
						if (item is DungeonChest)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = "You spot a strange container, filled by denizens of this strange place."; break;
								case 1: flavortext = "You look closely at the container near you - is that the small string of a trap attached to teh side?"; break;
								case 2: flavortext = "You hear something move from the container near you - as it... trying to talk to you?"; break;
							}
						}
						/*
						if (item is DungeonChest)
						{
							switch (Utility.Random(3)) //
							{
								case 0: flavortext = ""; break;
								case 1: flavortext = ""; break;
								case 2: flavortext = ""; break;
							}
						}
						*/
					}

				}
				//situational awareness of statics nearby
				if (flavortext == "")
				{

					// do not know how to check for statics nearby!



				}
				if (flavortext == "") // no text yet
				{
					if ( reg.IsPartOf( typeof( TownRegion ) ) || reg.IsPartOf( typeof( VillageRegion ) ) )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "You overhear the mixed sounds of a busy village street, merchants selling their wares, children playing while citizens gossip."; break;
								case 1: flavortext = "Someone nearby yelps, and you see a rat scurry before you, followed by a bulky fellow chasing it with a broom."; break;
								case 2: flavortext = "You hear the shouts of merchants offering their wares, and old housewives telling their husbands to come home."; break;
								case 3: flavortext = "You see  tall man stomp out of a building, holding an odd brromstick, yell 'The goat is Strong!' and dissapear back from whence he came."; break;
								case 4: flavortext = "A citizen looks at you with an odd expression of disgust, opens his mouth and lets out a loud BURP!.  He appologizes and looks away. "; break;
								case 5: flavortext = "You suddenly smell an aweful smell!  Did someone let out gas nearby??"; break;
								case 6: flavortext = "You hear the shouts of the town herald nearby, and someone talking about the next lottery draw."; break;
								case 7: flavortext = "Someone suddenly yells out and you see a small dog rush accross the road with a walking cane in his mouth."; break;
								case 8: flavortext = "A man comes out of a building, shakes another and yells 'THOSE DAMN KIDS!!  DON'T HAVE KIDS OKAY?!?!?' and stomps off in the distance."; break;
								case 9: flavortext = "Some sort of muffled transaction seems to be going on along one wall, but as the parties, hooded and wearing scarves, see you looking in their direction, they quickly melt into the crowd."; break;


							}


					}
					else if ( reg.IsPartOf( typeof( NecromancerRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "A thick fog, glowing in the sun, greatly reduces your ability to see into the distance. Some wind blows from the southeast."; break;
								case 1: flavortext = "From the corner of your eye you notice a pale, green glow, but when you turn your head to look at it, it's gone."; break;
								case 2: flavortext = "Small tendrils of an eerie smoke appears from the ground around you, you can almost sense their malintent."; break;
								case 3: flavortext = "You suddenly see something moving to your right - you look at find everything still."; break;
								case 4: flavortext = "You spot a small moving object on the ground - its a severed thumb!  It wiggles and stands up as you look at it, giving you a very odd thumbs up!"; break;
								case 5: flavortext = "A face suddenly appears in the ground before you!  Oh nevermind, its just some old leaves."; break;
								case 6: flavortext = "The air here smells of decay, an odd breeze blows in from the west.  Something died nearby not very long ago."; break;
								case 7: flavortext = "Small incorporeal shapes move in the side of your vision, but vanish when you look direcctly at them."; break;
								case 8: flavortext = "You hear a moan coming from nearby, a deep, longing sound that conveys soul crunching regret."; break;
								case 9: flavortext = "The smell of decay is everpresent here, rotten flesh mixed in with the acrid smell of soil."; break;


							}
					}
					else if ( reg.IsPartOf( typeof( OutDoorBadRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "Some light clouds pass overhead. A strong wind blows from the northeast."; break;
								case 1: flavortext = "You see evidence of battle taking place here - scuff marks from the swing of blades."; break;
								case 2: flavortext = "The sound of a weapon being sharpened echoes from nearby."; break;
								case 3: flavortext = "There is a distinct smell of blood here.  Something died recently here."; break;
								case 4: flavortext = "You wonder why these denizens decided to take up residence here."; break;
								case 5: flavortext = "An evil breeze blows from the north, cold and frigid."; break;
								case 6: flavortext = "The sound of ravens echoes in the distance, no doubt feasting on a poor being's decayed corpse."; break;
								case 7: flavortext = "This area was once host to a large battle, areas scarred by blasts of magic elements abound. "; break;
								case 8: flavortext = "You hear an evil laught coming from nearby... you think it was directed at you!"; break;
								case 9: flavortext = "There is an everpresent feeling of evil here, weighing on your shoulders an making your movements slower thn usual."; break;


							}
					}
					else if ( reg.IsPartOf( typeof( DungeonRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "The air is damp, and a stink assaults you from one of the tunnels."; break;
								case 1: flavortext = "You suddenly see movement in your side vision, was that a trap?"; break;
								case 2: flavortext = "This area seems to have been witness to some incredibly violent acts recently."; break;
								case 3: flavortext = "You spot a small graffiti on a nearby wall... you squint but can't make out the words.  Perhaps a long lost language."; break;
								case 4: flavortext = "You see something odd about a wall to the east - it draws your attention and you feel compelled to investigate."; break;
								case 5: flavortext = "You see signs that this area has been used as a bathroom recently.  You wonder where in fact denizens of this place go to relieve themselves. "; break;
								case 6: flavortext = "You see marks near you - on further examination, they seem to be signs that someone - or something - slept here recently."; break;
								case 7: flavortext = "Drops of water echo from above in the distance, echoing in the halls of this place."; break;
								case 8: flavortext = "Someone - or something - died here recently.  The corpse appears to have been ... eaten."; break;
								case 9: flavortext = "Large claw marks can be seen from the western wall - who - or what - made them?"; break;


							}
					}
					else if ( reg.IsPartOf( typeof( PirateRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "You see a small note scribbled with 'A friend can betray you, but an enemy will always stay the same.'"; break;
								case 1: flavortext = "You see some words written on an old note'If ye can’t trust a pirate, ye damn well can’t trust a merchant either!'"; break;
								case 2: flavortext = "You see an old note on the ground: 'Give me freedom or give me the rope. For I shall not take the shackles that subjugate the poor to uphold the rich.'"; break;
								case 3: flavortext = "You see an old note on the ground: 'Yarrrr! there be ony two ranks of leader amongst us pirates! Captain and if your really notorious then it’s Cap’n!'"; break;
								case 4: flavortext = "You see words scribbled on the ground: 'Always be yourself, unless you can be a pirate. Then always be a pirate.'"; break;
								case 5: flavortext = "You read on a note: 'Life’s pretty good, and why wouldn’t it be? I’m a pirate, after all.' written on a rock."; break;
								case 6: flavortext = "You see the words 'The average man will bristle if you say his father was dishonest, but he will brag a little if he discovers that his great- grandfather was a pirate.' written on an old bottle."; break;
								case 7: flavortext = "You see the words 'I’ve got a jar of dirt! I’ve got a jar of dirt, and guess what’s inside it?” “Why is the rum always gone?' written on an old rag."; break;
								case 8: flavortext = "You see the words 'Pirate’s code: First freedom and the captain. Second the loot, third woman and the rum and at the end no mercy if they not immediately surrender!' written on an abandoned rusty sword."; break;
								case 9: flavortext = "You see the words 'How much does the pirate pay for an ear piercing? … A buccaneer!' written on an old banana peel."; break;


							}
					}
					else if ( reg.IsPartOf( typeof( CaveRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "The walls of the cave glimmer with a strange mineral here.  When you look closer at the sparkle, you see minuscule creatures moving about."; break;
								case 1: flavortext = "You hear a sudden sound coming form the north - as if something was burrowing in the walls themselves."; break;
								case 2: flavortext = "The ceiling here is packed with many disgusting layers of bat guano.  It stinks and falls in large chunks on the ground."; break;
								case 3: flavortext = "The floor of this cave look grey, but you glimpse fragments of ore mixed with the rock."; break;
								case 4: flavortext = "You hear a low rumble and stop - it seems as though the roof of the cave could fall at any moment."; break;
								case 5: flavortext = "A giant precious gem is lodged in the ceiling of the cave high above your head."; break;
								case 6: flavortext = "The entire cave is covered with a fine particle dust."; break;
								case 7: flavortext = "The echo is particularly strong here - the cave's dome-like ceiling is impressive."; break;
								case 8: flavortext = "A bat flies towards you, and dodges at the last moment!"; break;
								case 9: flavortext = "You see multiple marks from a pickaxe here - someone obvisouly tried to mine the minerals here and failed miserably."; break;


							}
					}
					else if ( reg.IsPartOf( typeof( BardDungeonRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "The torches flicker and cast strange shadows at the walls."; break;
								case 1: flavortext = "You see a graffiti on the wall:  The stone's name is the key to entering the tower!"; break;
								case 2: flavortext = "You spot some scratches on the wall: Hidden passages!!  Hidden passages everywhere!!"; break;
								case 3: flavortext = "Someone wrote on the wall:  I've been in this @#$@#$ prison for over 4 weeks now - I NEED to escape!"; break;
								case 4: flavortext = "You feel an everpresent glare... maybe from Mangar himself, looking at your every move."; break;
								case 5: flavortext = "A voice suddenly BOOMS: 'HAHAHAHA ARE YOU LOST, IGNORANT ONE?  YOU WILL NEVER DEFEAT ME!'"; break;
								case 6: flavortext = "You see something scribbled on a wall: 'must remain sane... must... rema...snajskfdh'"; break;
								case 7: flavortext = "You see roman numerals on a wall... 'CXXV... I was here for CXXV days...'"; break;
								case 8: flavortext = "You hear a voice whisper: 'You will not escape, my slave... join me and we shall be powerful together!'"; break;
								case 9: flavortext = "You see evidence of someone trying to open a secret wall without success to the north."; break;

							}
					}
					else if ( reg.IsPartOf( typeof( PublicRegion ) )  )
					{
						switch (Utility.Random(10)) //
							{
								case 0: flavortext = "You hear someone fizzling a spell nearby... but suddenly smell a foul order... was it really a spell fizzle?"; break;
								case 1: flavortext = "A small mouse scurries away from a chair."; break;
								case 2: flavortext = "You wonder where bank boxes disapear to when closed."; break;
								case 3: flavortext = "You see a paper on the ground:'vendor buy bank guards'.  What a cryptic message!"; break;
								case 4: flavortext = "A vendor nearby curses as he has to adjust his prices for the 20th time today due to changes in the balance."; break;
								case 5: flavortext = "Someone sings a jolly tune nearby."; break;
								case 6: flavortext = "Patrons yell in horror as their favorite bagball team misses a goal."; break;
								case 7: flavortext = "You see sherry the mouse peeking from a small hole and wonder what peaked her interest."; break;
								case 8: flavortext = "Someone trips and falls.  Is this area truly safe??"; break;
								case 9: flavortext = "Someone mutters how grateful they are items don't have stupid minimum-usage levels."; break;


							}
					}
					else if ( reg.IsPartOf( typeof( OutDoorRegion ) )  )
					{
						if (category != "")
						{
							
							if (category == "forest")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = "High above you a murder of crows crosses the sky."; break;
									case 1: flavortext = "You hear an owl shrieking in the distance."; break;
									case 2: flavortext = "The trees are tick here, casting a deep shadow all around you."; break;
									case 3: flavortext = "Did that tree just move?"; break;
									case 4: flavortext = "A carving on a tree shows a heart and two initials."; break;
									case 5: flavortext = "There is blood in the ground here - some small animal got caught here recently."; break;
									case 6: flavortext = "Someone tried to chop a tree here - don't they know trees just regrow?"; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
							else if (category == "swamp")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = ""; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
							else if (category == "jungle")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = ""; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
							else if (category == "sand")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = ""; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
							else if (category == "cave")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = ""; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
							else if (category == "dirt")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = ""; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
									/*
A slight breeze makes the grass move gently in the wind.
A little mouse catches sight of you and flees into a small hole in the ground.*/

								
							}
							else if (category == "snow")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = "The weather is very uncomfortable because of the heavy snow. A storm blows from the southeast."; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
							else if (category == "sea")
							{
								switch (Utility.Random(10)) //
								{
									case 0: flavortext = ""; break;
									case 1: flavortext = ""; break;
									case 2: flavortext = ""; break;
									case 3: flavortext = ""; break;
									case 4: flavortext = ""; break;
									case 5: flavortext = ""; break;
									case 6: flavortext = ""; break;
									case 7: flavortext = ""; break;
									case 8: flavortext = ""; break;
									case 9: flavortext = ""; break;


								}
							}
						}
					}	
				}

				if (flavortext != "")
					SendMessage(40, flavortext );

			}
		}

		public override bool CheckMovement( Direction d, out int newZ )
		{
			DesignContext context = m_DesignContext;

			if ( context == null )
				return base.CheckMovement( d, out newZ );

			HouseFoundation foundation = context.Foundation;

			newZ = foundation.Z + HouseFoundation.GetLevelZ( context.Level, context.Foundation );

			int newX = this.X, newY = this.Y;
			Movement.Movement.Offset( d, ref newX, ref newY );

			int startX = foundation.X + foundation.Components.Min.X + 1;
			int startY = foundation.Y + foundation.Components.Min.Y + 1;
			int endX = startX + foundation.Components.Width - 1;
			int endY = startY + foundation.Components.Height - 2;

			return ( newX >= startX && newY >= startY && newX < endX && newY < endY && Map == foundation.Map );
		}

		public override bool AllowItemUse( Item item )
		{
			return DesignContext.Check( this );
		}

		public SkillName[] AnimalFormRestrictedSkills{ get{ return m_AnimalFormRestrictedSkills; } }

		private SkillName[] m_AnimalFormRestrictedSkills = new SkillName[]
		{
			SkillName.ArmsLore,	SkillName.Begging, SkillName.Discordance, SkillName.Forensics,
			SkillName.Inscribe, SkillName.ItemID, SkillName.Meditation, SkillName.Peacemaking,
			SkillName.Provocation, SkillName.RemoveTrap, SkillName.SpiritSpeak, SkillName.Stealing,
			SkillName.TasteID
		};

		public override bool AllowSkillUse( SkillName skill )
		{/*
			if ( AnimalForm.UnderTransformation( this ) )
			{
				for( int i = 0; i < m_AnimalFormRestrictedSkills.Length; i++ )
				{
					if( m_AnimalFormRestrictedSkills[i] == skill )
					{
						SendLocalizedMessage( 1070771 ); // You cannot use that skill in this form.
						return false;
					}
				}
			}*/

			return DesignContext.Check( this );
		}

		private bool m_LastProtectedMessage;
		private int m_NextProtectionCheck = 10;

		public virtual void RecheckTownProtection()
		{
			m_NextProtectionCheck = 10;

			bool isProtected = false;

			if ( isProtected != m_LastProtectedMessage )
			{
				if ( isProtected )
					SendLocalizedMessage( 500112 ); // You are now under the protection of the town guards.
				else
					SendLocalizedMessage( 500113 ); // You have left the protection of the town guards.

				m_LastProtectedMessage = isProtected;
			}
		}

		public override void MoveToWorld( Point3D loc, Map map )
		{
			base.MoveToWorld( loc, map );

			RecheckTownProtection();
		}

		public override void SetLocation( Point3D loc, bool isTeleport )
		{
			if ( !isTeleport && AccessLevel == AccessLevel.Player )
			{
				// moving, not teleporting
				int zDrop = ( this.Location.Z - loc.Z );

				if ( zDrop > 20 ) // we fell more than one story
					Hits -= ((zDrop / 20) * 10) - 5; // deal some damage; does not kill, disrupt, etc
			}

			base.SetLocation( loc, isTeleport );

			if ( isTeleport || --m_NextProtectionCheck == 0 )
				RecheckTownProtection();

			/* Begin UltimaLive Mod */
			if (BlockQuery != null)
			{
			m_PreviousMapBlock = BlockQuery.QueryMobile(this, m_PreviousMapBlock);
			}
			/* End UltimaLive Mod */
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from == this )
			{
				if ( m_Quest != null )
					m_Quest.GetContextMenuEntries( list );

				if (Alive)
				{
					list.Add( new CallbackEntry(1079452, new ContextCallback(delegate 
					{
						from.CloseGump(typeof(StoryItemGump));
						from.SendGump(new StoryItemGump(null, this, 0));	
					}
					)));
				}

				BaseHouse house = BaseHouse.FindHouseAt( this );

				if ( house != null )
				{
					if ( Alive && house.InternalizedVendors.Count > 0 && house.IsOwner( this ) )
						list.Add( new CallbackEntry( 6204, new ContextCallback( GetVendor ) ) );

					if ( house.IsAosRules )
						list.Add( new CallbackEntry( 6207, new ContextCallback( LeaveHouse ) ) );
				}

				if ( m_JusticeProtectors.Count > 0 )
					list.Add( new CallbackEntry( 6157, new ContextCallback( CancelProtection ) ) );

			}
			if ( from != this )
			{
				if ( Alive && Core.Expansion >= Expansion.AOS )
				{
					Party theirParty = from.Party as Party;
					Party ourParty = this.Party as Party;

					if ( theirParty == null && ourParty == null ) {
						list.Add( new AddToPartyEntry( from, this ) );
					} else if ( theirParty != null && theirParty.Leader == from ) {
						if ( ourParty == null ) {
							list.Add( new AddToPartyEntry( from, this ) );
						} else if ( ourParty == theirParty ) {
							list.Add( new RemoveFromPartyEntry( from, this ) );
						}
					}
				}

				BaseHouse curhouse = BaseHouse.FindHouseAt( this );

				if( curhouse != null )
				{
					if ( Alive && Core.Expansion >= Expansion.AOS && curhouse.IsAosRules && curhouse.IsFriend( from ) )
						list.Add( new EjectPlayerEntry( from, this ) );
				}
               /* if (from is PlayerMobile)
                {
                    	list.Add(new Server.Achievements.AchivementGumpEntry((PlayerMobile)from, this));
                }*/

			}
		}

		private void CancelProtection()
		{
			for ( int i = 0; i < m_JusticeProtectors.Count; ++i )
			{
				Mobile prot = m_JusticeProtectors[i];

				string args = String.Format( "{0}\t{1}", this.Name, prot.Name );

				prot.SendLocalizedMessage( 1049371, args ); // The protective relationship between ~1_PLAYER1~ and ~2_PLAYER2~ has been ended.
				this.SendLocalizedMessage( 1049371, args ); // The protective relationship between ~1_PLAYER1~ and ~2_PLAYER2~ has been ended.
			}

			m_JusticeProtectors.Clear();
		}

		public void ResetPlayer()
		{
			ResetPlayer(this);
		}
		public void ResetPlayer(Mobile m)
		{
			ResetPlayer(m, false);
		}

		public void ResetPlayer(Mobile m, bool newd)
		{
			if ( !(this.SoulBound) )
				return;

			if (!m_SbRes)
			{
				if ( HasGump( typeof( ResurrectGump ) ) ) 
				{
						CloseGump( typeof( ResurrectGump ) );
				}
				else if ( HasGump( typeof( ResurrectCostGump ) ) ) 
				{
						CloseGump( typeof( ResurrectCostGump ) );
				} 
				
				m.SendMessage("Your soul needs more time to recover from your last incarnation.");

				if (!m_SbResTimer)
				{
					new SbResTick( this ).Start();
					m_SbResTimer = true;
				}
				
				return;

			}
			else if (m_SbRes)
			{
				m_SbRes = false;
				m_SbResTimer = false;
			}

			Phylactery phylactery = this.FindPhylactery();
			ArrayList tames = new ArrayList();
			foreach ( Mobile creature in World.Mobiles.Values )
			{
				if ( creature is BaseCreature )
				{
					if ( ((BaseCreature)creature).Controlled && ((BaseCreature)creature).ControlMaster == this)
					{  
						BaseCreature c = (BaseCreature)creature;

						if (c is HenchmanWizard || c is HenchmanMonster || c is HenchmanFighter || c is HenchmanArcher || c is Squire || c is PackBeast || c is GolemFighter || c is GolemPorter || c is FrankenPorter)
							tames.Add(c);
						else
						{
							c.Say( 1043255, c.Name ); // ~1_NAME~ appears to have decided that is better off without a master!
							c.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy
							c.IsBonded = false;
							c.BondingBegin = DateTime.MinValue;
							c.OwnerAbandonTime = DateTime.MinValue;
							c.ControlTarget = null;
							
							c.AIObject.DoOrderRelease(); // this will prevent no release of creatures left alone with AI disabled (and consequent bug of Followers)
						}
					}
					if ( ((BaseCreature)creature).Summoned && ((BaseCreature)creature).SummonMaster == this)
					{  
						BaseCreature c = (BaseCreature)creature;

						tames.Add(c);

					}
				}
			}

			for ( int i = 0; i < this.Stabled.Count; ++i )
			{
				BaseCreature pet = this.Stabled[i] as BaseCreature;

				pet.Delete();
			}

			for ( int i = 0; i < tames.Count; ++i )
			{
				BaseCreature pet = tames[i] as BaseCreature;

				pet.Delete();
			}

			AnimalTrainer.CleanClaimList((Mobile)this);

			m.RawStr = 40;
			m.RawInt = 40;
			m.RawDex = 40;
			m.Kills = 0;

			//reset skills and stats
			for( int s = 0; s < m.Skills.Length; s++ )
			{
				m.Skills[s].Base = 0;
			}
			Template template = Template.GetRandomTemplate(this);			
			m.Karma = Utility.RandomMinMax ( 10, 200);	
			this.BalanceStatus = 0;
			this.BalanceEffect = 0;
			this.BAC = 0;
			this.THC = 0;
			this.m_TailorBOD = 0;
			this.m_BlacksmithBOD = 0;

			if (THC <= 0)
				this.High = false;
			else
				this.High = true;
			
			if (this == AetherGlobe.EvilChamp )
				AetherGlobe.EvilChamp = null;
			if (this == AetherGlobe.GoodChamp)
				AetherGlobe.GoodChamp = null;

			this.Criminal = false;

   			m_soulboundresurrect = true;
   			this.Resurrect();
      			m_soulboundresurrect = false;

			this.Hits = this.HitsMax;
			this.Stam = this.StamMax;
			this.Mana = this.ManaMax;
			this.Thirst = 20;
			this.Hunger = 20;
			this.Hidden = true;


			Item robe = this.FindItemOnLayer( Layer.OuterTorso );
			if (robe != null)
				robe.Delete();
			
			//setup the new char
			if ( Female = Utility.RandomBool() ) 
			{ 
			Body = 401; 
			Name = NameList.RandomName( "female" );
			}
			else 
			{ 
			Body = 400; 			
			Name = NameList.RandomName( "male" ); 
			}

			Title = TavernPatrons.GetTitle();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			Utility.AssignRandomHair( this );
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();

			int HairColor = Utility.RandomHairHue();
			HairHue = HairColor;
			FacialHairHue = HairColor;
			// reset quests
			DoneQuests = new List<QuestRestartInfo>();
			this.HandleDeletion(false);
			List<BaseHouse> houses =  BaseHouse.GetHouses(this);
			foreach(BaseHouse house in houses) {
				house.Delete();
			}
			this.Fame = 0;

			if ( BankBox != null )
			{
				if( BankBox.Items.Count > 0 )
				{
					List<Item> list = new List<Item>( BankBox.Items );
					foreach (Item thing in list)
					{
						if (thing.LootType != LootType.Blessed && thing.LootType != LootType.Ensouled) {
							thing.Delete();
						}
					}
				}
			}
			string world = Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );
			Map map = Worlds.GetMyDefaultMap( world );
			Point3D p = Worlds.GetRandomTown(world, true);
			// if they have a phylactery, lets make it difficult respawn (unless their phylactery is really weak)
			if (phylactery != null) {
				if (phylactery.TotalPower() > 20 || p == Point3D.Zero) {
					if (phylactery.TotalPower() > 100) {
						phylactery.RemoveRandomEssences(0.998, this);
						}
					p = Worlds.GetRandomTown(world, true);
					//p = Worlds.GetRandomLocation( world, "land" );
					}
				phylactery.PhylacteryMods = new List<PhylacteryMod>();
				phylactery.UpdateOwnerSoul(this);
			}
			if ( !newd && p != Point3D.Zero )
			{
				this.MoveToWorld( p, map );
				Effects.PlaySound( this.Location, this.Map, 0x1FC );
				m.SendMessage("You Awaken Anew, but in a different body.  May this life be better than the last.");
				this.PlaySound( 0x214 );
				this.FixedEffect( 0x376A, 10, 16 );
			}
			else if (newd)
			{
				Point3D loco = new Point3D(4099, 3535, 20);
				this.MoveToWorld( loco, Map.Trammel );
				Effects.PlaySound( this.Location, this.Map, 0x1FC );
			}

			if (this.Party != null) {
				((Party)this.Party).SendTeleportGump(m);
			}

			Container pack = this.Backpack;

			if ( pack != null )
			{
				if (template.Items != null) {
					foreach(Item item in template.Items) {
						pack.DropItem(item);
					}
				}
				
				if ( Utility.RandomMinMax( 1, 10 ) > 3 )
				{
					switch ( Utility.RandomMinMax( 0, 5 ) )
					{
						case 0: pack.DropItem( new BreadLoaf( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 1: pack.DropItem( new CheeseWheel( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 2: pack.DropItem( new Ribs( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 3: pack.DropItem( new Apple( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 4: pack.DropItem( new CookedBird( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 5: pack.DropItem( new LambLeg( Utility.RandomMinMax( 1, 3 ) ) ); break;
					}
				}
				if ( Utility.RandomMinMax( 1, 10 ) > 3 )
				{
					switch ( Utility.RandomMinMax( 0, 4 ) )
					{
						case 0: pack.DropItem( new BeverageBottle( BeverageType.Ale ) ); break;
						case 1: pack.DropItem( new BeverageBottle( BeverageType.Wine ) ); break;
						case 2: pack.DropItem( new BeverageBottle( BeverageType.Liquor ) ); break;
						case 3: pack.DropItem( new Jug( BeverageType.Cider ) ); break;
						case 4: pack.DropItem( new Waterskin() ); break;
					}
				}
				if ( Utility.RandomMinMax( 1, 10 ) > 3 )
				{
					switch ( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: pack.DropItem( new Torch() ); break;
						case 1: pack.DropItem( new Candle() ); break;
						case 2: pack.DropItem( new Lantern() ); break;
					}
				}
				if ( Utility.RandomMinMax( 1, 10 ) > 3 )
				{
					switch ( Utility.RandomMinMax( 0, 2 ) )
					{
						case 0: pack.DropItem( new Bandage( Utility.RandomMinMax( 5, 15 ) ) ); break;
						case 1: LesserCurePotion pot1 = new LesserCurePotion(); pot1.Amount = Utility.RandomMinMax( 1, 2 ); pack.DropItem( pot1 ); break;
						case 2: LesserHealPotion pot2 = new LesserHealPotion(); pot2.Amount = Utility.RandomMinMax( 1, 2 ); pack.DropItem( pot2 ); break;
					}
				}

				if ( Utility.RandomMinMax( 1, 10 ) > 3 )
				{
					switch ( Utility.RandomMinMax( 0, 8 ) )
					{
						case 0: TenFootPole pole = new TenFootPole(); pole.Charges = Utility.RandomMinMax( 1, 10 ); pack.DropItem( pole ); break;
						case 1: pack.DropItem( new Lockpick() ); break;
						case 2: pack.DropItem( new SkeletonsKey() ); break;
						case 3: pack.DropItem( new Bottle( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 4: pack.DropItem( new Pouch() ); break;
						case 5: pack.DropItem( new Bag() ); break;
						case 6: pack.DropItem( new Bedroll() ); break;
						case 7: pack.DropItem( new Kindling( Utility.RandomMinMax( 1, 3 ) ) ); break;
						case 8: pack.DropItem( new BlueBook() ); break;
					}
				}
				Gold gold = new Gold(1000);
				if (phylactery != null) {
					gold.Amount += phylactery.CalculateExtraGold(gold.Amount);
				}
				pack.DropItem( gold );
				pack.DropItem( new Torch() );
				pack.DropItem( new BlueBook() );
			}
			
			m_soulbounddate = DateTime.UtcNow;
			
		}

		private class SbResTick : Timer
		{
			private Mobile m_From;

			public SbResTick( PlayerMobile from ) : base( TimeSpan.FromMinutes( 4 ) )
			{
				m_From = from;
				from.SbResTimer = true;
			}

			protected override void OnTick()
			{
				((PlayerMobile)m_From).SbRes = true;
				((PlayerMobile)m_From).SbResTimer = false;
			}
		}

		private void GetVendor()
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( CheckAlive() && house != null && house.IsOwner( this ) && house.InternalizedVendors.Count > 0 )
			{
				CloseGump( typeof( ReclaimVendorGump ) );
				SendGump( new ReclaimVendorGump( house ) );
			}
		}

		private void LeaveHouse()
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null )
				this.Location = house.BanLocation;
		}

		private delegate void ContextCallback();

		private class CallbackEntry : ContextMenuEntry
		{
			private ContextCallback m_Callback;

			public CallbackEntry( int number, ContextCallback callback ) : this( number, -1, callback )
			{
			}

			public CallbackEntry( int number, int range, ContextCallback callback ) : base( number, range )
			{
				m_Callback = callback;
			}

			public override void OnClick()
			{
				if ( m_Callback != null )
					m_Callback();
			}
		}

		public override void DisruptiveAction()
		{
			if( Meditating )
			{
				RemoveBuff( BuffIcon.ActiveMeditation );
			}

			base.DisruptiveAction();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this == from && !Warmode )
			{
				IMount mount = Mount;

				if ( mount != null && !DesignContext.Check( this ) )
					return;
			}

			if( this == from && (!DisableDismountInWarmode || !Warmode) )
			{
				IMount mount = Mount;

				if( mount != null )
				{
					Server.Mobiles.EtherealMount.EthyDismount( this, true );
				}
			}

			if ( this != from && this.Backpack != null )
			{
				if ( !this.flagged)
					from.SendMessage("This person doesn't appear guilty of anything... recently.");
				else if ( !from.InRange(this, 1) || !InLOS( from ) )
					from.SendMessage("You're too far from the thief.");
				else if (this.caught)
					from.SendMessage("Someone else has already caught this thief red-handed.");
				else if ( from.InRange(this, 1) && InLOS( from ))
				{
					from.SendMessage("You grab the thief and look through his posessions!");
					this.SendMessage("You've been caught!");
					this.Backpack.DisplayTo( from );
					this.caught = true;
					int time = Utility.RandomMinMax(3,5);
					this.Paralyze( TimeSpan.FromSeconds( time ) );
					Timer.DelayCall( TimeSpan.FromSeconds( time ), new TimerCallback ( nolongercaught ) );
					from.PlaySound( 0x204 );
					this.PlaySound( 0x204 ); 
				}
			}

			base.OnDoubleClick( from );
		}

		private void nolongercaught()
		{
			if (this.caught)
				this.caught = false;
		}

		public override void DisplayPaperdollTo( Mobile to )
		{
			if ( DesignContext.Check( this ) )
				base.DisplayPaperdollTo( to );
		}

		private static bool m_NoRecursion;

		public override bool CheckEquip( Item item )
		{
			if ( !base.CheckEquip( item ) )
				return false;


			if ( this.AccessLevel < AccessLevel.GameMaster && item.Layer != Layer.Mount && this.HasTrade )
			{
				BounceInfo bounce = item.GetBounce();

				if ( bounce != null )
				{
					if ( bounce.m_Parent is Item )
					{
						Item parent = (Item) bounce.m_Parent;

						if ( parent == this.Backpack || parent.IsChildOf( this.Backpack ) )
							return true;
					}
					else if ( bounce.m_Parent == this )
					{
						return true;
					}
				}

				SendLocalizedMessage( 1004042 ); // You can only equip what you are already carrying while you have a trade pending.
				return false;
			}
			return true;
		}

		public override bool CheckTrade( Mobile to, Item item, SecureTradeContainer cont, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			int msgNum = 0;

			if ( cont == null )
			{
				if ( to.Holding != null )
					msgNum = 1062727; // You cannot trade with someone who is dragging something.
				else if ( this.HasTrade )
					msgNum = 1062781; // You are already trading with someone else!
				else if ( to.HasTrade )
					msgNum = 1062779; // That person is already involved in a trade
			}

			if ( msgNum == 0 )
			{
				if ( cont != null )
				{
					plusItems += cont.TotalItems;
					plusWeight += cont.TotalWeight;
				}

				if ( this.Backpack == null || !this.Backpack.CheckHold( this, item, false, checkItems, plusItems, plusWeight ) )
					msgNum = 1004040; // You would not be able to hold this if the trade failed.
				else if ( to.Backpack == null || !to.Backpack.CheckHold( to, item, false, checkItems, plusItems, plusWeight ) )
					msgNum = 1004039; // The recipient of this trade would not be able to carry this.
				else
					msgNum = CheckContentForTrade( item );
			}

			if ( msgNum != 0 )
			{
				if ( message )
					this.SendLocalizedMessage( msgNum );

				return false;
			}

			return true;
		}

		private static int CheckContentForTrade( Item item )
		{
			if ( item is TrapableContainer && ((TrapableContainer)item).TrapType != TrapType.None )
				return 1004044; // You may not trade trapped items.

			if ( SkillHandlers.StolenItem.IsStolen( item ) )
				return 1004043; // You may not trade recently stolen items.

			if ( item is Container )
			{
				foreach ( Item subItem in item.Items )
				{
					int msg = CheckContentForTrade( subItem );

					if ( msg != 0 )
						return msg;
				}
			}

			return 0;
		}

		public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
		{
			if ( !base.CheckNonlocalDrop( from, item, target ) )
				return false;

			if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			Container pack = this.Backpack;
			if ( from == this && this.HasTrade && ( target == pack || target.IsChildOf( pack ) ) )
			{
				BounceInfo bounce = item.GetBounce();

				if ( bounce != null && bounce.m_Parent is Item )
				{
					Item parent = (Item) bounce.m_Parent;

					if ( parent == pack || parent.IsChildOf( pack ) )
						return true;
				}

				SendLocalizedMessage( 1004041 ); // You can't do that while you have a trade pending.
				return false;
			}

			return true;
		}




		protected override void OnLocationChange( Point3D oldLocation ) 
		{
			if ( !( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, oldLocation ) ) ) && Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) ) )
			{
				Server.Misc.Worlds.EnteredTheLand( this );
				Server.Misc.RegionMusic.MusicRegion( this, this.Region );
			}

			if ( SoulBound && this.Party != null && Server.Misc.Worlds.GetRegionName( this.Map, oldLocation ) != Server.Misc.Worlds.GetRegionName( this.Map, Location ))
			{
					Party p = this.Party as Party;
					p.UpdateSoulboundBuffs( true);

			}

			BaseHouse house = BaseHouse.FindHouseAt( this );
			BaseHouse oldhouse = BaseHouse.FindHouseAt( oldLocation, this.Map, this.Z );

			if ( house != null && oldhouse == null )
			{

				if ( (house.IsFriend( this ) || house.IsOwner( this )) && this.AccessLevel < AccessLevel.GameMaster )
				{
					if ( house.RefreshDecay() )
						SendMessage("You refresh the contents of the house.");
				}
			}

			bool mountAble = true;
			bool speedAble = true;

			if ( Server.Misc.MyServerSettings.NoMountsInCertainRegions() && Server.Mobiles.AnimalTrainer.IsNoMountRegion(Region.Find( this.Location, this.Map ) ) )
			{
				mountAble = false;
				speedAble = false;
			}

			if ( Server.Mobiles.AnimalTrainer.IsBeingFast( this ) && !mountAble )
			{
				if ( this.Mounted )
				{
					Server.Mobiles.AnimalTrainer.DismountPlayer( this );
				}
			}
			if ( !speedAble )
			{
				AnimalFormContext contexts = Server.Spells.Ninjitsu.AnimalForm.GetContext(this as Mobile);
				if ( !Server.Mobiles.AnimalTrainer.AllowMagicSpeed( this, Region.Find( this.Location, this.Map ) ) )
				{
					Item shoes = this.FindItemOnLayer( Layer.Shoes );
					if (this.SoulBound && m_sbmaster && m_sbmasterspeed)
					{
						this.Send(SpeedControl.Disable);
						this.SendMessage( "Your speed is diminished here." );
						m_sbmasterspeed = false;
					}
					else if ( (shoes is BootsofHermes && shoes.Weight < 5.0 && !this.SoulBound) )
					{
						this.Send(SpeedControl.Disable);
						if (shoes != null)
						{
							shoes.Weight = 5.0;
							this.SendMessage( "These boots seem to have their magic diminished here." );
						}
					}
					else if (contexts != null && contexts.SpeedBoost)
						this.Send(SpeedControl.Disable);

					Server.Spells.Jedi.Celerity.RemoveEffect( this );
					Server.Spells.Mystic.WindRunner.RemoveEffect( this );
					Server.Spells.Syth.SythSpeed.RemoveEffect( this );
					Server.Misc.HenchmanFunctions.DismountHenchman( this );
					//0xDC 0xD2
				}
			}
			else if ( speedAble && !mountAble && !Mounted && Alive )
			{
				AnimalFormContext contexts = Server.Spells.Ninjitsu.AnimalForm.GetContext(this as Mobile);
				Item shoes = this.FindItemOnLayer( Layer.Shoes );
				if (this.SoulBound && m_sbmaster && !m_sbmasterspeed)
				{
						this.Send(SpeedControl.MountSpeed);
						m_sbmasterspeed = true;
				}
				else if ( (shoes is BootsofHermes && shoes.Weight > 3.0 && !this.SoulBound) )
				{
					if (shoes != null)
					    shoes.Weight = 3.0;

					this.Send(SpeedControl.MountSpeed);
				}
				else if (contexts != null && contexts.SpeedBoost)
					this.Send(SpeedControl.MountSpeed);
				
			}
			else if ( mountAble && !Mounted && Alive )
			{
				AnimalFormContext contexts = Server.Spells.Ninjitsu.AnimalForm.GetContext(this as Mobile);
				if ( BodyMod == 0 && !(Server.Spells.Ninjitsu.AnimalForm.UnderTransformation( this as Mobile )) )
					Server.Mobiles.AnimalTrainer.GetLastMounted( this );

				Item shoes = this.FindItemOnLayer( Layer.Shoes );
				if (m_sbmaster && !m_sbmasterspeed)
				{
						this.Send(SpeedControl.MountSpeed);
						m_sbmasterspeed = true;
				}
				else if ( (shoes is BootsofHermes && shoes.Weight > 3.0 && !this.SoulBound))
				{
					if (shoes != null)
					    shoes.Weight = 3.0;

					this.Send(SpeedControl.MountSpeed);
				}
				else if (contexts != null && contexts.SpeedBoost)
					this.Send(SpeedControl.MountSpeed);

				if ( this.Mount != null ){ Server.Misc.HenchmanFunctions.MountHenchman( this ); }
			}

			CheckLightLevels( false );

			DesignContext context = m_DesignContext;

			if ( context == null || m_NoRecursion )
				return;

			m_NoRecursion = true;

			HouseFoundation foundation = context.Foundation;

			int newX = this.X, newY = this.Y;
			int newZ = foundation.Z + HouseFoundation.GetLevelZ( context.Level, context.Foundation );

			int startX = foundation.X + foundation.Components.Min.X + 1;
			int startY = foundation.Y + foundation.Components.Min.Y + 1;
			int endX = startX + foundation.Components.Width - 1;
			int endY = startY + foundation.Components.Height - 2;

			if ( newX >= startX && newY >= startY && newX < endX && newY < endY && Map == foundation.Map )
			{
				if ( Z != newZ )
					Location = new Point3D( X, Y, newZ );

				m_NoRecursion = false;
				return;
			}

			Location = new Point3D( foundation.X, foundation.Y, newZ );
			Map = foundation.Map;

			m_NoRecursion = false;
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is BaseCreature && !((BaseCreature)m).Controlled )
				return ( !Alive || !m.Alive || IsDeadBondedPet || m.IsDeadBondedPet ) || ( Hidden && m.AccessLevel > AccessLevel.Player );

			return base.OnMoveOver( m );
		}

		public override bool CheckShove( Mobile shoved )
		{
			if( m_IgnoreMobiles || TransformationSpellHelper.UnderTransformation( shoved, typeof( WraithFormSpell ) ) )
				return true;
			else
				return base.CheckShove( shoved );
		}

		protected override void OnMapChange( Map oldMap )
		{
			/* Begin UltimaLive Mod */
			if (BlockQuery != null)
			{
			m_PreviousMapBlock = BlockQuery.QueryMobile(this, m_PreviousMapBlock);
			}
			/* End UltimaLive Mod */
			
			if ( oldMap != this.Map && Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) ) )
			{
				Server.Misc.Worlds.EnteredTheLand( this );
				Server.Misc.RegionMusic.MusicRegion( this, this.Region );
			}


			if ( SoulBound && this.Party != null )
			{
				Party p = this.Party as Party;
				p.UpdateSoulboundBuffs( true );
			}

			DesignContext context = m_DesignContext;

			if ( context == null || m_NoRecursion )
				return;

			m_NoRecursion = true;

			HouseFoundation foundation = context.Foundation;

			if ( Map != foundation.Map )
				Map = foundation.Map;

			m_NoRecursion = false;
		}

		public override void OnBeneficialAction( Mobile target, bool isCriminal )
		{
			if ( m_SentHonorContext != null )
				m_SentHonorContext.OnSourceBeneficialAction( target );

			if ( !IsBeneficialCriminal(this) && !IsBeneficialCriminal(target) )
				base.OnBeneficialAction( target, isCriminal );
				
		}

		public override void OnDamage( int amount, Mobile from, bool willKill ) //player receives damage?
		{
			int disruptThreshold;

			if ( !Core.AOS )
				disruptThreshold = 0;
			else if ( from != null && from.Player )
				disruptThreshold = (int)((double)HitsMax * 0.15) ;
			else
				disruptThreshold = 25;

			if ( amount > disruptThreshold )
			{
				BandageContext c = BandageContext.GetContext( this );

				if ( c != null )
				{
					if (AdventuresFunctions.IsPuritain((object)this) && (Agility()/2) > Utility.RandomDouble() )
					{}
					else
						c.Slip();
				}
			}

			if( Confidence.IsRegenerating( this ) )
				Confidence.StopRegenerating( this );

			WeightOverloading.FatigueOnDamage( this, amount );

			if ( m_ReceivedHonorContext != null )
				m_ReceivedHonorContext.OnTargetDamaged( from, amount );
			if ( m_SentHonorContext != null )
				m_SentHonorContext.OnSourceDamaged( from, amount );

			if ( willKill && from is PlayerMobile )
				Timer.DelayCall( TimeSpan.FromSeconds( 10 ), new TimerCallback( ((PlayerMobile) from).RecoverAmmo ) );
			
			if ( from != null && ((double)this.Hits / (double)this.HitsMax) <= 0.15 && this.BodyMod == 263 ) // minotaur morph
				MinotaurCloak.UseMinotaurAbility( this );

			if (SoulBound && Hits == HitsMax && (amount >= HitsMax || willKill) && Utility.RandomBool() )
			{
				amount = HitsMax - Utility.RandomMinMax(1,5);
				willKill = false;
				SendMessage("You barely survived that! Now run!");
			}
			
			base.OnDamage( amount, from, willKill );
		}

		public override void Resurrect()
		{
			if (this.SoulBound && !m_soulboundresurrect)
				this.ResetPlayer();

			bool wasAlive = this.Alive;

			base.Resurrect();

			this.Hunger = 10;
			this.Thirst = 10;
			this.Hits = this.HitsMax/2;
			this.Stam = this.StamMax/2;
			this.Mana = this.ManaMax/2;

			MusicName toPlay = MusicList[Utility.Random(MusicList.Length)];
			this.Send(PlayMusic.GetInstance(toPlay));

			switch( Utility.Random( 7 ) )
			{
				case 0: LoggingFunctions.LogStandard( this, "has returned from the realm of the dead" );		break;
				case 1: LoggingFunctions.LogStandard( this, "was brought back to the world of the living" );	break;
				case 2: LoggingFunctions.LogStandard( this, "has been restored to life" );					break;
				case 3: LoggingFunctions.LogStandard( this, "has been brought back from the grave" );		break;
				case 4: LoggingFunctions.LogStandard( this, "has been resurrected to this world" );			break;
				case 5: LoggingFunctions.LogStandard( this, "has returned to life after death" );			break;
				case 6: LoggingFunctions.LogStandard( this, "was resurrected for another chance at life" );	break;
			}

			if ( this.QuestArrow != null ){ this.QuestArrow.Stop(); }

			if ( this.Alive && !wasAlive )
			{
				Item deathRobe = new DeathRobe();
				
				if ( !EquipItem( deathRobe ) )
					deathRobe.Delete();
			}
		}

        public static MusicName[] MusicList = new MusicName[]
        {
            MusicName.Opn_Gen,
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
            MusicName.Opn_Gen13
        };

		public override double RacialSkillBonus
		{
			get
			{
				//if( Core.ML && this.Race == Race.Human ) // WIZARD
				//	return 20.0;

				return 0;
			}
		}

		public override void OnWarmodeChanged()
		{
			if ( !Warmode  )
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 10 ), new TimerCallback( RecoverAmmo ) );
				//if (!( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, oldLocation ) ) ) && Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) ))
				Server.Misc.RegionMusic.MusicRegion( this, this.Region );
			}
			else
				Server.Misc.RegionMusic.WarMusicToggle( this );

			AutoSheatheWeapon.From(this); // WIZARD ADDED THIS


		}

		private bool FindItems_Callback(Item item)
		{
			if (!item.Deleted 
				&& (
					(item.LootType == LootType.Blessed && !this.SoulBound)
						||
					( this.SoulBound && item.LootType == LootType.Ensouled )
					)
				)
			{
				if (this.Backpack != item.ParentEntity)
				{
					return true;
				}
			}
			return false;
		}


		public override bool OnBeforeDeath()
		{
            var region = Region.GetRegion(typeof(MBRegion));
            if (region != null && region.GetPlayers().Contains(this))
            {
                MBRegion reg = region as MBRegion;
                GauntletMaster npc = reg.EventNPC;

                this.Hits = this.HitsMax;
                this.Mana = this.ManaMax;
                this.Stam = this.StamMax;
                this.Poison = null;
                this.Blessed = true;

                MoveToWorld(npc.PlayerResPoint, npc.Map);

                Timer.DelayCall(TimeSpan.FromSeconds(2), delegate { this.Blessed = false; });

                npc.OnEventFinished(false);
                return false;
            }

            //Zombiex and canchew
            if (FindMostRecentDamager(true) is BaseCreature && !this.SoulBound )
            {
				BaseCreature mo = (BaseCreature)FindMostRecentDamager(true);

				if ( mo.CanChew )
					ChewItem( mo );

				if (mo is Zombiex || mo.CanInfect || mo is WanderingConcubine && (Str + Dex + Int) > 125 )
				{
					Zombiex zomb = new Zombiex();
					zomb.NewZombie(this);
				}
            }
            //Zombiex end
			
            else if (FindMostRecentDamager(true) is PlayerMobile )
			{
				PlayerMobile killer = (PlayerMobile)FindMostRecentDamager(true);
				
				if (killer.BodyMod == 84 && Utility.RandomBool() ) // Final: Widow's morphing armor addition
				{
					WidowSpawn spawn = new WidowSpawn();
					spawn.NewSpawn(this, FindMostRecentDamager(true));
				}	
			}
			
			if ( Server.Misc.SeeIfJewelInBag.IHaveAJewel( this ) == true ) // FOR THE JEWEL OF IMMORTALITY
			{
				return false;
			}
			else if ( Server.Misc.SeeIfGemInBag.IHaveAGem( this ) == true ) // FOR THE GEM OF IMMORTALITY
			{
				return false;
			}

			NetState state = NetState;

			if ( state != null )
				state.CancelAllTrades();

			DropHolding();

			if (Backpack != null && !Backpack.Deleted)
			{
				// reach in and grab all the items recursively so they can be saved 
				List<Item> ilist = Backpack.FindItemsByType<Item>(FindItems_Callback);
				for (int i = 0; i < ilist.Count; i++)
				{
					Item poop = (Item)ilist[i];
					if (!(poop is Phylactery) && !(poop is SoulTome))
						Backpack.DropItem(ilist[i]);
					
				}	
				if (this.SoulBound) {
					List<Item> itemsToDelete = new List<Item>(Backpack.Items);
					Phylactery phylactery = this.FindPhylactery();
					Mobile undead = new Skeleton();
					int phylacteryPower = 0;
					if (phylactery != null) {
						undead = phylactery.GetEscapedUndead(false);
						phylacteryPower = phylactery.TotalPower();
					}
					foreach(Item item in itemsToDelete) {
						if (item is Gold || item is DDCopper || item is DDSilver || item is Amber || item is Amethyst || item is Citrine || item is Diamond || item is Emerald || item is Ruby || item is Sapphire || item is StarSapphire || item is Tourmaline ) {
							if (phylacteryPower < 1000 ) {
								// reduce the return by 50%
								item.Amount = (int)Math.Ceiling((item.Amount*0.50));
							}
							undead.AddItem(item);
						} else if (!CanRemainOnHardcoreCorpse(item) && item.LootType != LootType.Ensouled) 
						{
							if (Utility.RandomDouble() > 0.33)
								item.Delete();
							else if (item.LootType == LootType.Blessed)
								item.Delete();
							else 
								undead.AddItem( item );
						}
					}
					undead.MoveToWorld(new Point3D(this.X, this.Y, this.Z), this.Map);
					undead.SayTo( this, "Thisss body is now mmiiine." );	
				}
			}

			RecoverAmmo();

			Mobile mob = this.LastKiller;
			if ( mob != null ){ LoggingFunctions.LogDeaths( this, mob ); }
			
			BaseWeapon.DamageItems((Mobile)this);

			return base.OnBeforeDeath();
		}

		public bool CanRemainOnHardcoreCorpse(Item item) {
			if (item is BaseClothing) {
				BaseClothing clothing = (BaseClothing)item;
				return (clothing.Attributes.IsEmpty && clothing.ClothingAttributes.IsEmpty && clothing.Resistances.IsEmpty && clothing.SkillBonuses.IsEmpty);
			} 
			if (item is Phylactery || item is SoulTome)
				return true;

			return false;
		}

		public override DeathMoveResult GetParentMoveResultFor( Item item )
		{
			DeathMoveResult res = base.GetParentMoveResultFor( item );

			if ( res == DeathMoveResult.MoveToCorpse && item.Movable && this.Young )
				res = DeathMoveResult.MoveToBackpack;

			return res;
		}

		public override DeathMoveResult GetInventoryMoveResultFor( Item item )
		{
			DeathMoveResult res = base.GetInventoryMoveResultFor( item );

			if ( res == DeathMoveResult.MoveToCorpse && item.Movable && this.Young )
				res = DeathMoveResult.MoveToBackpack;

			return res;
		}

		public bool GetEnsouledItems(Item item)
		{
			if (!item.Deleted && (item.LootType == LootType.Ensouled))
			{
				if (this.Backpack != item.ParentEntity)
				{
					return true;
				}
			}
			return false;
		}

		public override void OnDeath( Container c )
		{

			Mobile killer = this.FindMostRecentDamager( true );

			if (this.SoulBound)
			{
				m_SbRes = false;
				
				new SbResTick( this ).Start();
				m_SbResTimer = true;

				if (c.Items != null && c is Corpse ) {
					if ( ((Corpse)c).EquipItems.Count > 0) {
						List<Item> items =  new List<Item>( ((Corpse)c).EquipItems );
						foreach (Item item in items)
						{
							if (!(this.CanRemainOnHardcoreCorpse(item))) {
								item.Delete();	
							}
						} 
					}
				}
			}

            var region = Region.GetRegion(typeof(MBRegion));
            if (region != null && region.GetPlayers().Contains(this))
            {
                return;
            }

			base.OnDeath(c);

			HueMod = -1;
			NameMod = null;
			SavagePaintExpiration = TimeSpan.Zero;

			SetHairMods( -1, -1 );

			PolymorphSpell.StopTimer( this );
			IncognitoSpell.StopTimer( this );
			DisguiseTimers.RemoveTimer( this );

			EndAction( typeof( PolymorphSpell ) );
			EndAction( typeof( IncognitoSpell ) );

			if ( m_PermaFlags.Count > 0 )
			{
				m_PermaFlags.Clear();

				if ( c is Corpse )
					((Corpse)c).Criminal = true;

				if ( SkillHandlers.Stealing.ClassicMode )
					Criminal = true;
			}



			if ( killer is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)killer;

				Mobile master = bc.GetMaster();
				if( master != null )
					killer = master;
			}

			if ( this.Young )
			{
				if ( YoungDeathTeleport() )
					Timer.DelayCall( TimeSpan.FromSeconds( 2.5 ), new TimerCallback( SendYoungDeathNotice ) );
			}


			Server.Guilds.Guild.HandleDeath( this, killer );

			if( m_BuffTable != null )
			{
				List<BuffInfo> list = new List<BuffInfo>();

				foreach( BuffInfo buff in m_BuffTable.Values )
				{
					if( !buff.RetainThroughDeath )
					{
						list.Add( buff );
					}
				}

				for( int i = 0; i < list.Count; i++ )
				{
					RemoveBuff( list[i] );
				}
			}
			//final players dying will affect the curse

			double changechange = 0;
			if (this.BalanceStatus != 0)
				changechange = (double)this.Karma / 750; 
			else if (this.Avatar)
				changechange = (double)this.Karma / 1500; 
			else
				changechange = (double)this.Karma / 10000; 

			m_BalanceEffect += (int)changechange;
			AetherGlobe.ChangeCurse( (int)changechange );	

			if ( GetFlag( PlayerFlag.WellRested ) )
				SetFlag( PlayerFlag.WellRested, false );
				
			if( c.Items.Count > 0 ) //player died, chance of losing durability for their items
			{
				List<Item> list = new List<Item>( c.Items );
				bool reduced = false;
				foreach (Item thing in list)
				{
					if ((thing is BaseClothing || thing is BaseWeapon || thing is BaseArmor || thing is BaseJewel) && Utility.RandomBool() )
					{
						if (!reduced)
							reduced = true;
						BaseWeapon.DamageItem(thing, this, 4);
					}
				}
				if (reduced)
					SendMessage("Your demise damaged your items!");
			}
		
		}

		private List<Mobile> m_PermaFlags;
		private List<Mobile> m_VisList;
		private Hashtable m_AntiMacroTable;
		private TimeSpan m_GameTime;
		private TimeSpan m_ShortTermElapse;
		private TimeSpan m_LongTermElapse;
		private DateTime m_SessionStart;
		private DateTime m_LastEscortTime;
		private DateTime m_LastPetBallTime;
		private DateTime m_NextSmithBulkOrder;
		private DateTime m_NextTailorBulkOrder;
		private DateTime m_SavagePaintExpiration;
		private SkillName m_Learning = (SkillName)(-1);

		public SkillName Learning
		{
			get{ return m_Learning; }
			set{ m_Learning = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan SavagePaintExpiration
		{
			get
			{
				TimeSpan ts = m_SavagePaintExpiration - DateTime.UtcNow;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				m_SavagePaintExpiration = DateTime.UtcNow + value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextSmithBulkOrder
		{
			get
			{
				TimeSpan ts = m_NextSmithBulkOrder - DateTime.UtcNow;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				try{ m_NextSmithBulkOrder = DateTime.UtcNow + value; }
				catch{}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextTailorBulkOrder
		{
			get
			{
				TimeSpan ts = m_NextTailorBulkOrder - DateTime.UtcNow;

				if ( ts < TimeSpan.Zero )
					ts = TimeSpan.Zero;

				return ts;
			}
			set
			{
				try{ m_NextTailorBulkOrder = DateTime.UtcNow + value; }
				catch{}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastEscortTime
		{
			get{ return m_LastEscortTime; }
			set{ m_LastEscortTime = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastPetBallTime
		{
			get{ return m_LastPetBallTime; }
			set{ m_LastPetBallTime = value; }
		}

		public PlayerMobile()
		{
			m_AutoStabled = new List<Mobile>();

			m_VisList = new List<Mobile>();
			m_PermaFlags = new List<Mobile>();
			m_AntiMacroTable = new Hashtable();
			m_RecentlyReported = new List<Mobile>();
			m_BOBFilter = new Engines.BulkOrders.BOBFilter();
			m_SongEffects = new List<SongEffect>();
			m_GameTime = TimeSpan.Zero;
			m_ShortTermElapse = TimeSpan.FromHours( 8.0 );
			m_LongTermElapse = TimeSpan.FromHours( 40.0 );

			m_JusticeProtectors = new List<Mobile>();
			m_GuildRank = Guilds.RankDefinition.Lowest;

			m_ChampionTitles = new ChampionTitleInfo();

			InvalidateMyRunUO();
		}

		public override bool MutateSpeech( List<Mobile> hears, ref string text, ref object context )
		{
			if ( Alive )
				return false;

			if ( Core.ML && Skills[SkillName.SpiritSpeak].Value >= 100.0 )
				return false;

			if ( Core.AOS )
			{
				for ( int i = 0; i < hears.Count; ++i )
				{
					Mobile m = hears[i];

					if ( m != this && m.Skills[SkillName.SpiritSpeak].Value >= 100.0 )
						return false;
				}
			}

			return base.MutateSpeech( hears, ref text, ref context );
		}

		public override void DoSpeech( string text, int[] keywords, MessageType type, int hue )
		{

			bool drunks = false;
			string newspeech = "";
			if (BAC > 0) //is drunk!
				{
					if (text.Length > 0 && Utility.RandomDouble() < ((double)BAC/100))
					{
						drunks = true;
						// lets have fun
						string[] said = text.Split(' ');

						for( int i = 0; i < said.Length; i++ )
						{
							if (Utility.RandomDouble() > 0.77)
							{
								string junk = "";
								switch (Utility.Random(10))
								{
								case 0: newspeech += "ftgnt" + " "; break; //Final twist gets no thanks
								case 1: newspeech += "fcatc" + " "; break; //for coding all this code
								case 2: newspeech += "hgya" + " "; break; //he gets yelled at
								case 3: newspeech += "buls" + " "; break; //by ungrateful little shits
								case 4: newspeech += "bhkat" + " "; break; //but he keeps at it
								case 5: newspeech += "bhas" + " "; break; //because he appreciates some
								case 6: newspeech += "otcphm" + " "; break; //of the cool players he met
								case 7: newspeech += "otlty" + " "; break; //over the last two years
								case 8: newspeech += "sgapar" + " "; break; //so go and play and remember
								case 9: newspeech += "jsbhb" + " "; break; //jetson sucks big hairy balls
								}
							}
							else
								newspeech += said[Utility.Random(said.Length)]+ " ";
						}
						if (Utility.RandomDouble() < ((double)BAC / 75) )
							keywords = new int[0];
					}
				}

			if (THC > 0) //is drunk!
				{
					if (text.Length > 0 && Utility.RandomDouble() < ((double)THC/100))
					{
						drunks = true;
						// lets have fun
						string[] said = text.Split(' ');

						for( int i = 0; i < said.Length; i++ )
						{
							if (Utility.RandomDouble() > 0.77)
							{
								string junk = "";
								switch (Utility.Random(7))
								{
									case 0: newspeech += "man" + " "; break; 
									case 1: newspeech += "dude" + " "; break; 
									case 2: newspeech += "groovy" + " "; break;
									case 3: newspeech += "trippy" + " "; break;
									case 4: newspeech += "enlightened" + " "; break;
									case 5: newspeech += "uh" + " "; break;
									case 6: newspeech += "woah" + " "; break;
								}
							}
							else
								newspeech += said[Utility.Random(i)]+ " ";
						}
						if (Utility.RandomDouble() < ((double)THC / 75) )
							keywords = new int[0];
					}
				}

			if( Guilds.Guild.NewGuildSystem && (type == MessageType.Guild || type == MessageType.Alliance) )
			{
				Guilds.Guild g = this.Guild as Guilds.Guild;
				if( g == null )
				{
					SendLocalizedMessage( 1063142 ); // You are not in a guild!
				}
				else if( type == MessageType.Alliance )
				{
					if( g.Alliance != null && g.Alliance.IsMember( g ) )
					{
						//g.Alliance.AllianceTextMessage( hue, "[Alliance][{0}]: {1}", this.Name, text );
						g.Alliance.AllianceChat( this, text );
						SendToStaffMessage( this, "[Alliance]: {0}", text );

					}
					else
					{
						SendLocalizedMessage( 1071020 ); // You are not in an alliance!
					}
				}
				else	//Type == MessageType.Guild
				{
					m_GuildMessageHue = hue;

					g.GuildChat( this, text );
					SendToStaffMessage( this, "[Guild]: {0}", text );
				}
			}
			else
			{
				if (drunks)
					base.DoSpeech( newspeech, keywords, type, hue );
				else
					base.DoSpeech( text, keywords, type, hue );
			}
		}

		private static void SendToStaffMessage( Mobile from, string text )
		{
			Packet p = null;

			foreach( NetState ns in from.GetClientsInRange( 8 ) )
			{
				Mobile mob = ns.Mobile;

				if( mob != null && mob.AccessLevel >= AccessLevel.GameMaster && mob.AccessLevel > from.AccessLevel )
				{
					if( p == null )
						p = Packet.Acquire( new UnicodeMessage( from.Serial, from.Body, MessageType.Regular, from.SpeechHue, 3, from.Language, from.Name, text ) );

					ns.Send( p );
				}
			}

			Packet.Release( p );
		}

		private static void SendToStaffMessage( Mobile from, string format, params object[] args )
		{
			SendToStaffMessage( from, String.Format( format, args ) );
		}

		public void ChewItem(BaseCreature killer)
		{

			if (Utility.RandomDouble() > 0.75)
			{
				int count = 10;
				Item ILost = null;
				while (count > 0 && ILost == null)
				{
					ILost = HiddenTrap.GetMyItem( this );
					count -= 1;
				}

					if ( ILost != null )
					{ 
							ChewedItem box = new ChewedItem();
							box.ItemID = ILost.ItemID;
							box.Hue = 700;
							box.Chew = killer.Name;

							this.PlaySound( 0x637 ); 
							killer.PublicOverheadMessage( MessageType.Emote, EmoteHue, false, "*Chews on something*" );

							ILost.Delete();
							this.AddToBackpack ( box );	
					}
			}

		}

		public void DamageItemsOnDeath()
		{

			if ( this.AccessLevel > AccessLevel.Player )
				return;

			if ( FindItemOnLayer( Layer.OuterTorso ) != null ) 
			{ DamageItem(FindItemOnLayer( Layer.OuterTorso )); }
			if ( FindItemOnLayer( Layer.OneHanded ) != null ) { DamageItem(FindItemOnLayer( Layer.OneHanded ));}
			if ( FindItemOnLayer( Layer.TwoHanded ) != null ) { DamageItem(FindItemOnLayer( Layer.TwoHanded ));}
			if ( FindItemOnLayer( Layer.Bracelet ) != null ) { DamageItem(FindItemOnLayer( Layer.Bracelet )); }
			if ( FindItemOnLayer( Layer.Ring ) != null ) { DamageItem(FindItemOnLayer( Layer.Ring )); }
			if ( FindItemOnLayer( Layer.Helm ) != null ) { DamageItem(FindItemOnLayer( Layer.Helm )); }
			if ( FindItemOnLayer( Layer.Arms ) != null ) { DamageItem(FindItemOnLayer( Layer.Arms ));}
			if ( FindItemOnLayer( Layer.OuterLegs ) != null ) { DamageItem(FindItemOnLayer( Layer.OuterLegs )); }
			if ( FindItemOnLayer( Layer.Neck ) != null ) { DamageItem(FindItemOnLayer( Layer.Neck )); }
			if ( FindItemOnLayer( Layer.Gloves ) != null ) { DamageItem(FindItemOnLayer( Layer.Gloves ));}
			if ( FindItemOnLayer( Layer.Talisman ) != null && !(FindItemOnLayer( Layer.Talisman ) is Spellbook) && !(FindItemOnLayer( Layer.Talisman ) is BaseInstrument)) { DamageItem(FindItemOnLayer( Layer.Talisman )); } 
			if ( FindItemOnLayer( Layer.Shoes ) != null ) { DamageItem(FindItemOnLayer( Layer.Shoes )); }
			if ( FindItemOnLayer( Layer.Cloak ) != null ) { DamageItem(FindItemOnLayer( Layer.Cloak ));}
			if ( FindItemOnLayer( Layer.FirstValid ) != null ) { DamageItem(FindItemOnLayer( Layer.FirstValid )); }
			if ( FindItemOnLayer( Layer.Waist ) != null ) { DamageItem(FindItemOnLayer( Layer.Waist ));}
			if ( FindItemOnLayer( Layer.InnerLegs ) != null ) { DamageItem(FindItemOnLayer( Layer.InnerLegs ));}
			if ( FindItemOnLayer( Layer.InnerTorso ) != null ) { DamageItem(FindItemOnLayer( Layer.InnerTorso )); }
			if ( FindItemOnLayer( Layer.Pants ) != null ) { DamageItem(FindItemOnLayer( Layer.Pants ));}
			if ( FindItemOnLayer( Layer.Shirt ) != null ) { DamageItem(FindItemOnLayer( Layer.Shirt )); }
			if ( FindItemOnLayer( Layer.Earrings ) != null ) { DamageItem(FindItemOnLayer( Layer.Earrings  )); }

		}

		public void DamageItem(Item thing)
		{
			//arms lore determines 50% chance of damage other 50% is rng
			if (Utility.RandomBool() && Utility.RandomDouble() > ( (0.50 * (Skills[SkillName.ArmsLore].Base / 120)) + ((double)Utility.RandomMinMax(1, 50)/100)) )
			{
				if (thing is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)thing;
					if (armor.HitPoints >= 1)
						armor.HitPoints -= 1;
					else if (armor.MaxHitPoints > 1)
						armor.MaxHitPoints -= 1;
					else 
						armor.Delete();
				}
				else if (thing is BaseJewel )
				{
					BaseJewel armor = (BaseJewel)thing;
					if (armor.HitPoints >= 1)
						armor.HitPoints -= 1;
					else if (armor.MaxHitPoints > 1)
						armor.MaxHitPoints -= 1;
					else 
						armor.Delete();
				}
				else if (thing is BaseWeapon )
				{
					BaseWeapon armor = (BaseWeapon)thing;
					if (armor.HitPoints >= 1)
						armor.HitPoints -= 1;
					else if (armor.MaxHitPoints > 1)
						armor.MaxHitPoints -= 1;
					else 
						armor.Delete();
				}
				else if (thing is BaseClothing )
				{
					BaseClothing armor = (BaseClothing)thing;
					if (armor.HitPoints >= 1)
						armor.HitPoints -= 1;
					else if (armor.MaxHitPoints > 1)
						armor.MaxHitPoints -= 1;
					else 
						armor.Delete();
				}	
			}
		}

		public override void Damage( int amount, Mobile from )
		{
			if ( Spells.Necromancy.EvilOmenSpell.TryEndEffect( this ) )
				amount = (int)(amount * 1.25);

			Mobile oath = Spells.Necromancy.BloodOathSpell.GetBloodOath( from );

				/* Per EA's UO Herald Pub48 (ML):
				 * ((resist spellsx10)/20 + 10=percentage of damage resisted)
				 */

			if ( oath == this )
			{
				amount = (int)(amount * 1.1);

				if( amount > 35 && from is PlayerMobile )  /* capped @ 35, seems no expansion */
				{
					amount = 35;
				}

				if( Core.ML )
				{
					from.Damage( (int)(amount * ( 1 - ((( from.Skills.MagicResist.Value * .5 ) + 10) / 100 ))), this );
				}
				else
				{
					from.Damage( amount, this );
				}
			}

			base.Damage( amount, from );
		}

		#region Poison

		public override ApplyPoisonResult ApplyPoison( Mobile from, Poison poison )
		{
			if ( !Alive )
				return ApplyPoisonResult.Immune;

			if ( Spells.Necromancy.EvilOmenSpell.TryEndEffect( this ) )
				poison = PoisonImpl.IncreaseLevel( poison );

			ApplyPoisonResult result = base.ApplyPoison( from, poison );

			if ( from != null && result == ApplyPoisonResult.Poisoned && PoisonTimer is PoisonImpl.PoisonTimer )
				(PoisonTimer as PoisonImpl.PoisonTimer).From = from;

			return result;
		}

		public override bool CheckPoisonImmunity( Mobile from, Poison poison )
		{
			if ( this.Young )
				return true;

			if (poison == Poison.Lesser && from.Skills[SkillName.Poisoning].Base > 50.0 )
			{
				double val = from.Skills[SkillName.Poisoning].Base - 50.0; 
				if (val > 25 && Utility.RandomDouble() < 0.50)
					return true;
				else if (Utility.RandomDouble() < (0.50 * (val / 25) ) )
					return true;
			}
			if (poison == Poison.Regular && from.Skills[SkillName.Poisoning].Base > 75.0 )
			{
				double val = from.Skills[SkillName.Poisoning].Base - 75.0; 
				if (val > 15 && Utility.RandomDouble() < 0.50)
					return true;
				else if (Utility.RandomDouble() < (0.50 * (val / 15) ) )
					return true;
			}
			if (poison == Poison.Greater && from.Skills[SkillName.Poisoning].Base > 90.0 )
			{
				double val = from.Skills[SkillName.Poisoning].Base - 90.0; 
				if (val > 10 && Utility.RandomDouble() < 0.50)
					return true;
				else if (Utility.RandomDouble() < (0.50 * (val / 10) ) )
					return true;
			}
			if (poison == Poison.Deadly && from.Skills[SkillName.Poisoning].Base > 100.0 )
			{
				double val = from.Skills[SkillName.Poisoning].Base - 100.0; 
				if (val > 15 && Utility.RandomDouble() < 0.50)
					return true;
				else if (Utility.RandomDouble() < (0.50 * (val / 15) ) )
					return true;
			}
			if (poison == Poison.Lethal && from.Skills[SkillName.Poisoning].Base > 115.0 )
			{
				double val = from.Skills[SkillName.Poisoning].Base - 115.0; 
				if (val > 10 && Utility.RandomDouble() < 0.50)
					return true;
				else if (Utility.RandomDouble() < (0.50 * (val / 10) ) )
					return true;
			}

			return base.CheckPoisonImmunity( from, poison );
		}

		public override void OnPoisonImmunity( Mobile from, Poison poison )
		{
			if ( this.Young )
				SendLocalizedMessage( 502808 ); // You would have been poisoned, were you not new to the land of Britannia. Be careful in the future.
			else
				base.OnPoisonImmunity( from, poison );
		}

		#endregion

		public PlayerMobile( Serial s ) : base( s )
		{
			m_VisList = new List<Mobile>();
			m_AntiMacroTable = new Hashtable();
			InvalidateMyRunUO();
		}

		public List<Mobile> VisibilityList
		{
			get{ return m_VisList; }
		}

		public List<Mobile> PermaFlags
		{
			get{ return m_PermaFlags; }
		}

		public override int Luck{ get{ return AosAttributes.GetValue( this, AosAttribute.Luck ); } }

		public override bool IsHarmfulCriminal( Mobile target )
		{
			if ( SkillHandlers.Stealing.ClassicMode && target is PlayerMobile && ((PlayerMobile)target).m_PermaFlags.Count > 0 )
			{
				int noto = Notoriety.Compute( this, target );

				if ( noto == Notoriety.Innocent )
					target.Delta( MobileDelta.Noto );

				return false;
			}

			if ( target is BaseCreature && ((BaseCreature)target).InitialInnocent && !((BaseCreature)target).Controlled )
				return false;

			if ( Core.ML && target is BaseCreature && ((BaseCreature)target).Controlled && this == ((BaseCreature)target).ControlMaster )
				return false;

			return base.IsHarmfulCriminal( target );
		}

		public bool AntiMacroCheck( Skill skill, object obj )
		{
			if ( obj == null || m_AntiMacroTable == null || this.AccessLevel != AccessLevel.Player )
				return true;

			Hashtable tbl = (Hashtable)m_AntiMacroTable[skill];
			if ( tbl == null )
				m_AntiMacroTable[skill] = tbl = new Hashtable();

			CountAndTimeStamp count = (CountAndTimeStamp)tbl[obj];
			if ( count != null )
			{
				if ( count.TimeStamp + SkillCheck.AntiMacroExpire <= DateTime.UtcNow )
				{
					count.Count = 1;
					return true;
				}
				else
				{
					++count.Count;
					if ( count.Count <= SkillCheck.Allowance )
						return true;
					else
						return false;
				}
			}
			else
			{
				tbl[obj] = count = new CountAndTimeStamp();
				count.Count = 1;

				return true;
			}
		}

		public void AdjustReputation(Mobile killed)
		{
			if (AdventuresFunctions.IsPuritain((object)this))
			{
				int effect = killed.Fame/50;
				int harmed = 0;

				if (killed is BaseCreature)
					harmed = ((BaseCreature)killed).midrace;
				
				if (killed is MidlandVendor) //special exceptions for mobs who are low fame but worth more
					effect = 250;

				if (harmed == 1)//humans harmed
				{
					m_midhumans -= effect;
					m_midgargoyles += effect;
					m_midlizards += effect /4;
					m_midpirates -= effect /4;
					m_midorcs += effect / 10;
				}
				if (harmed == 2)//gargoyles harmed
				{
					m_midhumans += effect;
					m_midgargoyles -= effect;
					m_midlizards -= effect /4;
					m_midpirates += effect /4;
					m_midorcs += effect / 10;
				}
				if (harmed == 3)//lizards harmed
				{
					m_midhumans += effect/4;
					m_midgargoyles -= effect/4;
					m_midlizards -= effect;
					m_midpirates += effect;
					m_midorcs += effect / 10;
				}
				if (harmed == 4)//pirates harmed
				{
					m_midhumans -= effect/4;
					m_midgargoyles += effect/4;
					m_midlizards += effect;
					m_midpirates -= effect;
					m_midorcs += effect / 10;
				}
				if (harmed == 5)//orcs harmed
				{
					m_midhumans += effect/10;
					m_midgargoyles += effect/10;
					m_midlizards += effect/10;
					m_midpirates += effect/10;
					m_midorcs -= effect;
				}

			}
		}

		public double Lucidity() //for mana regen
		{

			if ( AccessLevel > AccessLevel.Player )
				return 1;

			bool running = ( Direction.Running != 0 );
			double calc = 0;
			calc = ((double)Server.SkillHandlers.Stealth.GetArmorRating(this) / ( ((double)Str/3) + ((double)Int) ) ); 

			calc += ( (double)Backpack.TotalWeight/3 ) / (((double)this.Str*4) + ((double)this.Dex));

			if (running) // harder to parry/dodge/hit while running
				calc += 0.15;

			if (calc > 1 )
				calc = 0.99;

			calc = 1 - calc;
			return calc;
		}
		
		public void CheckRest()
		{
			if (GetFlag( PlayerFlag.WellRested ) )
			{
				if ( (m_LastLogin + TimeSpan.FromHours(12)) > DateTime.Now )
					SetFlag( PlayerFlag.WellRested, false ); 
			}
		}

		public bool Sorcerer() 
		{
			bool yeah = true;
			
			Item magebook = this.FindItemOnLayer( Layer.Talisman );
			if ( (magebook != null && ( !(magebook is Spellbook) && !(magebook is NecromancerSpellbook) )) || magebook == null )
				yeah = false;

			Item shirt = this.FindItemOnLayer( Layer.InnerTorso );
			if (shirt != null && !(shirt is BaseClothing))
				yeah = false;

			Item glove = this.FindItemOnLayer( Layer.Gloves );
			if (glove != null && !(glove is BaseClothing))
				yeah = false;

			Item pants = this.FindItemOnLayer( Layer.Pants );
			if (pants != null && !(pants is BaseClothing))
				yeah = false;

			Item neck = this.FindItemOnLayer( Layer.Neck );
			if (neck != null && !(neck is BaseClothing) && !(neck is BaseJewel))
				yeah = false;

			Item arms = this.FindItemOnLayer( Layer.Arms );
			if (arms != null && !(arms is BaseClothing))
				yeah = false;

			Item cloak = this.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && !(cloak is BaseClothing))
				yeah = false;

			Item helm = this.FindItemOnLayer( Layer.Helm );
			if (helm != null && !(helm is BaseClothing))
				yeah = false;

			Item twohanded = this.FindItemOnLayer( Layer.TwoHanded );
			if (twohanded != null && (twohanded is BaseWeapon || twohanded is BaseArmor))	
				yeah = false;

			Item oneHanded = this.FindItemOnLayer( Layer.OneHanded );
			if (oneHanded != null && (oneHanded is BaseWeapon || oneHanded is BaseArmor))	
				yeah = false;

			Item firstvalid = this.FindItemOnLayer( Layer.FirstValid );
			if (firstvalid != null && (firstvalid is BaseWeapon || firstvalid is BaseArmor))	
				yeah = false;

			if (Skills[SkillName.Magery].Value < 90)
				yeah = false;

			if (Skills[SkillName.EvalInt].Value < 90)
				yeah = false;
				
			if (Int < 50)
				yeah = false;

			if (yeah && !m_sorc) // new status
			{
					PlaySound( 0x202 );
					FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
					FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
			}
			
			if (m_sorc && !yeah) // removed status, remove all buffs
			{
				if( m_BuffTable != null )
				{
					List<BuffInfo> list = new List<BuffInfo>();

					foreach( BuffInfo buff in m_BuffTable.Values )
					{
						if( !buff.RetainThroughDeath )
						{
							list.Add( buff );
						}
					}

					for( int i = 0; i < list.Count; i++ )
					{
						RemoveBuff( list[i] );
					}
				}
			}

			m_sorc = yeah;

			return yeah;
		}
		
		public bool Troubadour()
		{
			bool yeah = true;
			
			Item instrument = this.FindItemOnLayer( Layer.Talisman );
			if ( (instrument != null && ( !(instrument is BaseInstrument) && !(instrument is SongBook) )) || instrument == null ) 
				yeah = false;

			Item arms = this.FindItemOnLayer( Layer.Arms );
			if (arms != null && !(arms is BaseClothing))
				yeah = false;

			Item glove = this.FindItemOnLayer( Layer.Gloves );
			if (glove != null && !(glove is BaseClothing))
				yeah = false;
				
			Item neck = this.FindItemOnLayer( Layer.Neck );
			if (neck != null && !(neck is BaseClothing) && !(neck is BaseJewel))
				yeah = false;

			Item cloak = this.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && !(cloak is BaseClothing))
				yeah = false;
				
			Item helm = this.FindItemOnLayer( Layer.Helm );
			if (helm != null && !(helm is BaseClothing))
				yeah = false;

			Item twohanded = this.FindItemOnLayer( Layer.TwoHanded );
			if (twohanded != null && (twohanded is BaseWeapon || twohanded is BaseArmor))	
				yeah = false;

			Item oneHanded = this.FindItemOnLayer( Layer.OneHanded );
			if (oneHanded != null && (oneHanded is BaseWeapon || oneHanded is BaseArmor))	
				yeah = false;

			Item firstvalid = this.FindItemOnLayer( Layer.FirstValid );
			if (firstvalid != null && (firstvalid is BaseWeapon || firstvalid is BaseArmor))	
				yeah = false;

			if (Skills[SkillName.Musicianship].Value < 90)
				yeah = false;
				
			if (Dex < 50)
				yeah = false;

			if (yeah && !m_troub)
			{
				FixedParticles( 0x376A, 1, 14, 0x13B5, EffectLayer.Waist );
				PlaySound( 0x511 );
			}
			
			if (m_troub && !yeah) // removed status, remove all buffs
			{
				if( m_BuffTable != null )
				{
					List<BuffInfo> list = new List<BuffInfo>();

					foreach( BuffInfo buff in m_BuffTable.Values )
					{
						if( !buff.RetainThroughDeath )
						{
							list.Add( buff );
						}
					}

					for( int i = 0; i < list.Count; i++ )
					{
						RemoveBuff( list[i] );
					}
				}
			}

			m_troub = yeah;

			return yeah;		
		
		}

		public bool Alchemist() 
		{
	
			bool yeah = true;
			
			Item stone = this.FindItemOnLayer( Layer.Talisman );
			    if ( (stone != null && !(stone is PhilosophersStone)) || stone == null )
				yeah = false;
			

			Item neck = this.FindItemOnLayer( Layer.Neck );
			if (neck != null && !(neck is BaseClothing))
				yeah = false;

			Item arms = this.FindItemOnLayer( Layer.Arms );
			if (arms != null && !(arms is BaseClothing))
				yeah = false;

			Item cloak = this.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && !(cloak is BaseClothing))
				yeah = false;

			Item apron = this.FindItemOnLayer( Layer.MiddleTorso );
			if (apron != null && !(apron is FullApron) && !(apron is HalfApron) )
				yeah = false;

			Item helm = this.FindItemOnLayer( Layer.Helm );
			if (helm != null && helm.ItemID != 0x2FB8 && helm.ItemID != 0x3172 )
				yeah = false;

			Item twohanded = this.FindItemOnLayer( Layer.TwoHanded );
			if (twohanded != null && (twohanded is BaseWeapon || twohanded is BaseArmor))	
				yeah = false;

			Item oneHanded = this.FindItemOnLayer( Layer.OneHanded );
			if (oneHanded != null && (oneHanded is BaseWeapon || oneHanded is BaseArmor))	
				yeah = false;

			Item firstvalid = this.FindItemOnLayer( Layer.FirstValid );
			if (firstvalid != null && (firstvalid is BaseWeapon || firstvalid is BaseArmor))	
				yeah = false;

			if (Skills[SkillName.Alchemy].Value < 90)
				yeah = false;

			if (Skills[SkillName.Forensics].Value < 90)
				yeah = false;
				
			if (Dex < 50)
				yeah = false;
				
			if (yeah && !m_alch)
			{
				FixedParticles( 0x376A, 1, 14, 0x13B5, EffectLayer.Waist );
				PlaySound( 0x511 );
			}
			
			if (m_alch && !yeah) // removed status, remove all buffs
			{
				if( m_BuffTable != null )
				{
					List<BuffInfo> list = new List<BuffInfo>();

					foreach( BuffInfo buff in m_BuffTable.Values )
					{
						if( !buff.RetainThroughDeath )
						{
							list.Add( buff );
						}
					}

					for( int i = 0; i < list.Count; i++ )
					{
						RemoveBuff( list[i] );
					}
				}
			}
			
			m_alch = yeah;
			return yeah;
		}
		
		public double AlchemistBonus()
		{
			double bonus = 1;
			if (Alchemist())
			{
				//total 250% bonus damage
				
				//50 pts for alchemy
				bonus += 0.2 * (Skills[SkillName.Alchemy].Value / 120);
				
				//50 pts for dex
				bonus += 0.2 * ((double)Dex/500);
				
				//50 pts base
				bonus += 0.2;
				
				//100 pts TasteID
				bonus += 1.0 * (Skills[SkillName.TasteID].Value / 120);
			}
			return bonus;
		}
				

		public double Agility() //for dodge and parry
		{
			//how nimble is the player?  Dex greatly influences this and armor influences it to the negative

			if ( AccessLevel > AccessLevel.Player )
				return 1;
			bool running = false;
			//bool running = ( (Direction & Direction.Running) != 0 );  didnt work..... wonder why +++
			if ( m_NextMovementTime > Core.TickCount && Direction.Running != 0)
				running = true;

			double calc = 0;

			double resists = ((double)PoisonResistance + (double)PhysicalResistance + (double)FireResistance + (double)ColdResistance + (double)EnergyResistance) /3;
			calc = ( ( ((double)Server.SkillHandlers.Stealth.GetArmorRating(this)*1.5) + resists ) / (( ((double)Str/3) + ((double)Dex) ) * 2.25) ); 

			calc += 0.50 * (1- ((double)this.Stam / (double)this.StamMax)); // tired player isnt agile
			calc += 0.30 * Encumbrance();

			if (running) // harder to parry/dodge/hit while running
				calc += 0.15;

			if (calc > 1 )
				calc = 0.99;

			calc = 1 - calc;
			return calc;
		}

		public double Encumbrance()
		{
			//how much is the player carrying?  This will affect how much he can run, whether he can dodge, etc
			//strength greatly influences this higher str, more you can carry
			
			if ( AccessLevel > AccessLevel.Player )
				return 0;

			double calc = 0;

			//backpack weight is mostly str, some dex
			calc =  ((double)Backpack.TotalWeight-75) / (double)this.MaxWeight; //are they near their maxweight?

			if (calc < 0 )
				calc = 0;

			//adjust slightly for their current stamina level
			calc -= ((double)this.Stam / (double)this.StamMax)/7;

			if (calc > 1)
				calc = 1;
			if (calc <0 )
				calc = 0;

			return calc;

		}

		public double MentalExhaustion()
		{
			//everytime the function is called on, count goes up.  if it gets too high the player will start failing (doing too much too quickly)
			//higher int increases the cap and impact of this, higher end, higher focus
			
			if ( AccessLevel > AccessLevel.Player )
				return 0;

			m_mentalexhaustcount ++; 

			if ( m_mentalexhaustcount > (int)(50 + (this.Int * 20)))
				m_mentalexhaustcount = (int)(50 + (this.Int * 20));
			
			double calc = 0;

			//lets calculate base using a function of int.
			calc =  (double)m_mentalexhaustcount / (double)(50 + (this.Int * 20));

			if (calc > 1)
				calc = 1;
			if (calc <0 )
				calc = 0;

			//Add chance of count going down
			if (Utility.RandomDouble() < (this.Int / 350));
			    m_mentalexhaustcount --;

			return calc;
		}

		public void AdjustReputation(int gold, int midrace, bool benefit)
		{
			if ( AccessLevel > AccessLevel.Player )
				return;

			if (AdventuresFunctions.IsPuritain((object)this))
			{
				int effect = gold/50;
				int race = 0;

				if (midrace >0)
					race = midrace;

				if (benefit)
				{
					if (race == 1)//humans race
					{
						m_midhumans += effect;
						m_midgargoyles -= effect;
						m_midlizards -= effect /4;
						m_midpirates += effect /4;
						m_midorcs -= effect / 10;
					}
					if (race == 2)//gargoyles race
					{
						m_midhumans -= effect;
						m_midgargoyles += effect;
						m_midlizards += effect /4;
						m_midpirates -= effect /4;
						m_midorcs -= effect / 10;
					}
					if (race == 3)//lizards race
					{
						m_midhumans -= effect/4;
						m_midgargoyles += effect/4;
						m_midlizards += effect;
						m_midpirates -= effect;
						m_midorcs -= effect / 10;
					}
					if (race == 4)//pirates race
					{
						m_midhumans += effect/4;
						m_midgargoyles -= effect/4;
						m_midlizards -= effect;
						m_midpirates += effect;
						m_midorcs -= effect / 10;
					}
					if (race == 5)//orcs race
					{
						m_midhumans -= effect/10;
						m_midgargoyles -= effect/10;
						m_midlizards -= effect/10;
						m_midpirates -= effect/10;
						m_midorcs += effect;
					}
				}
				else
				{
					if (race == 1)//humans race
					{
						m_midhumans -= effect;
						m_midgargoyles += effect;
						m_midlizards += effect /4;
						m_midpirates -= effect /4;
						m_midorcs += effect / 10;
					}
					if (race == 2)//gargoyles race
					{
						m_midhumans += effect;
						m_midgargoyles -= effect;
						m_midlizards -= effect /4;
						m_midpirates += effect /4;
						m_midorcs += effect / 10;
					}
					if (race == 3)//lizards race
					{
						m_midhumans += effect/4;
						m_midgargoyles -= effect/4;
						m_midlizards -= effect;
						m_midpirates += effect;
						m_midorcs += effect / 10;
					}
					if (race == 4)//pirates race
					{
						m_midhumans -= effect/4;
						m_midgargoyles += effect/4;
						m_midlizards += effect;
						m_midpirates -= effect;
						m_midorcs += effect / 10;
					}
					if (race == 5)//orcs race
					{
						m_midhumans += effect/10;
						m_midgargoyles += effect/10;
						m_midlizards += effect/10;
						m_midpirates += effect/10;
						m_midorcs -= effect;
					}

				}

			}
		}

		private void RevertHair()
		{
			SetHairMods( -1, -1 );
		}

		private Engines.BulkOrders.BOBFilter m_BOBFilter;

		public Engines.BulkOrders.BOBFilter BOBFilter
		{
			get{ return m_BOBFilter; }
		}

		public override void Serialize( GenericWriter writer )
		{
			//cleanup our anti-macro table
			foreach ( Hashtable t in m_AntiMacroTable.Values )
			{
				ArrayList remove = new ArrayList();
				foreach ( CountAndTimeStamp time in t.Values )
				{
					if ( time.TimeStamp + SkillCheck.AntiMacroExpire <= DateTime.UtcNow )
						remove.Add( time );
				}

				for (int i=0;i<remove.Count;++i)
					t.Remove( remove[i] );
			}

			CheckKillDecay();

			CheckAtrophies( this );

			base.Serialize( writer );

			writer.Write( (int) 47 ); // 47 added lastlogout

			writer.Write( (DateTime)m_LastLogout);

			writer.Write( (int)m_BlacksmithBOD);
			writer.Write( (int)m_TailorBOD);
			writer.Write( (int)m_THC);
			writer.Write( (int)m_Stealthing);

			writer.Write( (bool) m_avatar);
			writer.Write( (bool) m_IsZen );
			writer.Write( (int) m_FastGain );

			writer.Write( (int)m_midhumanacc);
			writer.Write( (int)m_midgargoyleacc);
			writer.Write( (int)m_midlizardacc);
			writer.Write( (int)m_midpirateacc);
			writer.Write( (int)m_midorcacc);

			writer.Write( (int)m_midrace);
			writer.Write( (int)m_midhumans);
			writer.Write( (int)m_midgargoyles);
			writer.Write( (int)m_midorcs);
			writer.Write( (int)m_midlizards);
			writer.Write( (int)m_midpirates);

			writer.Write( (bool)m_sbmasterspeed );
			writer.Write( (bool)m_sbmaster );
			writer.Write( (string)m_lastdeeds);

			writer.Write( (string)m_lastwords);

			writer.Write( (DateTime) m_lastautores);
			writer.Write(m_LastGauntletLevel);
			
			if (m_OriginalBody == null) {
				m_OriginalBody = this.Body;
			}
			
			writer.Write( (int)m_OriginalBody );

           		writer.Write( (int)m_SoulForce );
			writer.Write( (DateTime) m_soulbounddate);
			writer.Write( m_SoulBound );
			writer.Write( (DateTime) m_PeacedUntil );
			writer.Write( (DateTime) m_AnkhNextUse );
			writer.Write( m_AutoStabled, true );

			if( m_AcquiredRecipes == null )
			{
				writer.Write( (int)0 );
			}
			else
			{
				writer.Write( m_AcquiredRecipes.Count );

				foreach( KeyValuePair<int, bool> kvp in m_AcquiredRecipes )
				{
					writer.Write( kvp.Key );
					writer.Write( kvp.Value );
				}
			}

			writer.WriteDeltaTime( m_LastHonorLoss );

			ChampionTitleInfo.Serialize( writer, m_ChampionTitles );

			writer.Write( m_LastValorLoss );
			writer.Write( m_BalanceEffect );	//This ain't going to be a small #.

			writer.WriteEncodedInt( m_ExtraSlots );
			writer.WriteEncodedInt( m_GuildMessageHue );

			writer.WriteEncodedInt( m_GuildRank.Rank );
			writer.Write( m_LastOnline );

			writer.WriteEncodedInt( (int) m_SolenFriendship );

			QuestSerializer.Serialize( m_Quest, writer );

			if ( m_DoneQuests == null )
			{
				writer.WriteEncodedInt( (int) 0 );
			}
			else
			{
				writer.WriteEncodedInt( (int) m_DoneQuests.Count );

				for ( int i = 0; i < m_DoneQuests.Count; ++i )
				{
					QuestRestartInfo restartInfo = m_DoneQuests[i];

					QuestSerializer.Write( (Type) restartInfo.QuestType, QuestSystem.QuestTypes, writer );
					writer.Write( (DateTime) restartInfo.RestartTime );
				}
			}

			writer.WriteEncodedInt( (int) m_Profession );

			writer.WriteDeltaTime( m_LastCompassionLoss );

			writer.WriteEncodedInt( m_CompassionGains );

			if ( m_CompassionGains > 0 )
				writer.WriteDeltaTime( m_NextCompassionDay );

			m_BOBFilter.Serialize( writer );

			bool useMods = ( m_HairModID != -1 || m_BeardModID != -1 );

			writer.Write( useMods );

			if ( useMods )
			{
				writer.Write( (int) m_HairModID );
				writer.Write( (int) m_HairModHue );
				writer.Write( (int) m_BeardModID );
				writer.Write( (int) m_BeardModHue );
			}

			writer.Write( SavagePaintExpiration );

			writer.Write( (int) m_NpcGuild );
			writer.Write( (DateTime) m_NpcGuildJoinTime );
			writer.Write( (TimeSpan) m_NpcGuildGameTime );

			writer.Write( m_PermaFlags, true );

			writer.Write( NextTailorBulkOrder );

			writer.Write( NextSmithBulkOrder );

			writer.WriteDeltaTime( m_LastJusticeLoss );
			writer.Write( m_JusticeProtectors, true );

			writer.WriteDeltaTime( m_LastSacrificeGain );
			writer.WriteDeltaTime( m_LastSacrificeLoss );
			writer.Write( m_AvailableResurrects );

			writer.Write( (int) m_Flags );

			writer.Write( m_LongTermElapse );
			writer.Write( m_ShortTermElapse );
			writer.Write( this.GameTime );
			
			writer.Write( m_BalanceStatus );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			switch ( version )
			{
				case 47:
					m_LastLogout = reader.ReadDateTime();
					goto case 46;
				case 46:
					m_BlacksmithBOD = reader.ReadInt();
					m_TailorBOD = reader.ReadInt();
					goto case 45;
				case 45:
					m_THC = reader.ReadInt();
					goto case 44;
				case 44:
					m_Stealthing = reader.ReadInt();
					goto case 43;
				case 43:
					m_avatar = reader.ReadBool();
					goto case 42;
				case 42:
					m_IsZen = reader.ReadBool();
					m_FastGain = reader.ReadInt();
					goto case 41;
				case 41:
					m_midhumanacc = reader.ReadInt();
					m_midgargoyleacc = reader.ReadInt();
					m_midlizardacc = reader.ReadInt();
					m_midpirateacc = reader.ReadInt();
					m_midorcacc = reader.ReadInt();
					goto case 40;				
				case 40:
					m_midrace = reader.ReadInt();
					goto case 39;
				case 39:
					m_midhumans = reader.ReadInt();
					m_midgargoyles = reader.ReadInt();
					m_midorcs = reader.ReadInt();
					m_midlizards = reader.ReadInt();
					m_midpirates = reader.ReadInt();
					goto case 38;
				case 38:
					m_sbmasterspeed = reader.ReadBool();
					goto case 37;
				case 37:
					m_sbmaster = reader.ReadBool();
					goto case 36;
				case 36:
					m_lastdeeds = reader.ReadString();
					goto case 35;
				case 35:
					m_lastwords = reader.ReadString();
					goto case 34;
				case 34:
					m_lastautores = reader.ReadDateTime();
					goto case 33;
				case 33:
					m_InGauntlet = false;
					m_LastGauntletLevel = reader.ReadInt();
					goto case 32;
				case 32:
					m_OriginalBody = new Body(reader.ReadInt());
					m_SoulForce = reader.ReadInt();
					goto case 31;
				case 31:
				{
					m_soulbounddate = reader.ReadDateTime();
					goto case 30;
				}
				case 30: 
				{
					m_SoulBound = reader.ReadBool();
					goto case 28;
				}
				case 28:
				{
					m_PeacedUntil = reader.ReadDateTime();

					goto case 27;
				}
				case 27:
				{
					m_AnkhNextUse = reader.ReadDateTime();

					goto case 26;
				}
				case 26:
				{
					m_AutoStabled = reader.ReadStrongMobileList();

					goto case 25;
				}
				case 25:
				{
					int recipeCount = reader.ReadInt();

					if( recipeCount > 0 )
					{
						m_AcquiredRecipes = new Dictionary<int, bool>();

						for( int i = 0; i < recipeCount; i++ )
						{
							int r = reader.ReadInt();
							if( reader.ReadBool() )	//Don't add in recipies which we haven't gotten or have been removed
								m_AcquiredRecipes.Add( r, true );
						}
					}
					goto case 24;
				}
				case 24:
				{
					m_LastHonorLoss = reader.ReadDeltaTime();
					goto case 23;
				}
				case 23:
				{
					m_ChampionTitles = new ChampionTitleInfo( reader );
					goto case 22;
				}
				case 22:
				{
					m_LastValorLoss = reader.ReadDateTime();
					goto case 21;
				}
				case 21:
				{
					m_BalanceEffect = reader.ReadInt();
					goto case 20;
				}
				case 20:
				{
					m_ExtraSlots = reader.ReadEncodedInt();
					m_GuildMessageHue = reader.ReadEncodedInt();

					goto case 19;
				}
				case 19:
				{
					int rank = reader.ReadEncodedInt();
					int maxRank = Guilds.RankDefinition.Ranks.Length -1;
					if( rank > maxRank )
						rank = maxRank;

					m_GuildRank = Guilds.RankDefinition.Ranks[rank];
					m_LastOnline = reader.ReadDateTime();
					goto case 18;
				}
				case 18:
				{
					m_SolenFriendship = (SolenFriendship) reader.ReadEncodedInt();

					goto case 17;
				}
				case 17: // changed how DoneQuests is serialized
				case 16:
				{
					m_Quest = QuestSerializer.DeserializeQuest( reader );

					if ( m_Quest != null )
						m_Quest.From = this;

					int count = reader.ReadEncodedInt();

					if ( count > 0 )
					{
						m_DoneQuests = new List<QuestRestartInfo>();

						for ( int i = 0; i < count; ++i )
						{
							Type questType = QuestSerializer.ReadType( QuestSystem.QuestTypes, reader );
							DateTime restartTime;

							if ( version < 17 )
								restartTime = DateTime.MaxValue;
							else
								restartTime = reader.ReadDateTime();

							m_DoneQuests.Add( new QuestRestartInfo( questType, restartTime ) );
						}
					}

					m_Profession = reader.ReadEncodedInt();
					goto case 15;
				}
				case 15:
				{
					m_LastCompassionLoss = reader.ReadDeltaTime();
					goto case 14;
				}
				case 14:
				{
					m_CompassionGains = reader.ReadEncodedInt();

					if ( m_CompassionGains > 0 )
						m_NextCompassionDay = reader.ReadDeltaTime();

					goto case 13;
				}
				case 13: 
				case 12:
				{
					m_BOBFilter = new Engines.BulkOrders.BOBFilter( reader );
					goto case 11;
				}
				case 11:
				{
					goto case 10;
				}
				case 10:
				{
					if ( reader.ReadBool() )
					{
						m_HairModID = reader.ReadInt();
						m_HairModHue = reader.ReadInt();
						m_BeardModID = reader.ReadInt();
						m_BeardModHue = reader.ReadInt();
					}

					goto case 9;
				}
				case 9:
				{
					SavagePaintExpiration = reader.ReadTimeSpan();

					if ( SavagePaintExpiration > TimeSpan.Zero )
					{
						BodyMod = ( Female ? 184 : 183 );
						HueMod = 0;
					}

					goto case 8;
				}
				case 8:
				{
					m_NpcGuild = (NpcGuild)reader.ReadInt();
					m_NpcGuildJoinTime = reader.ReadDateTime();
					m_NpcGuildGameTime = reader.ReadTimeSpan();
					goto case 7;
				}
				case 7:
				{
					m_PermaFlags = reader.ReadStrongMobileList();
					goto case 6;
				}
				case 6:
				{
					NextTailorBulkOrder = reader.ReadTimeSpan();
					goto case 5;
				}
				case 5:
				{
					NextSmithBulkOrder = reader.ReadTimeSpan();
					goto case 4;
				}
				case 4:
				{
					m_LastJusticeLoss = reader.ReadDeltaTime();
					m_JusticeProtectors = reader.ReadStrongMobileList();
					goto case 3;
				}
				case 3:
				{
					m_LastSacrificeGain = reader.ReadDeltaTime();
					m_LastSacrificeLoss = reader.ReadDeltaTime();
					m_AvailableResurrects = reader.ReadInt();
					goto case 2;
				}
				case 2:
				{
					m_Flags = (PlayerFlag)reader.ReadInt();
					goto case 1;
				}
				case 1:
				{
					m_LongTermElapse = reader.ReadTimeSpan();
					m_ShortTermElapse = reader.ReadTimeSpan();
					m_GameTime = reader.ReadTimeSpan();
					if (version >= 29)
						m_BalanceStatus = reader.ReadInt();
					goto case 0;
				}
				case 0:
				{
					if( version < 26 )
						m_AutoStabled = new List<Mobile>();
					break;

				}
			

			}

			if (m_SongEffects == null) 
				m_SongEffects = new List<SongEffect>();

			if (m_RecentlyReported == null)
				m_RecentlyReported = new List<Mobile>();

			// Professions weren't verified on 1.0 RC0
			if ( !CharacterCreation.VerifyProfession( m_Profession ) )
				m_Profession = 0;

			if ( m_PermaFlags == null )
				m_PermaFlags = new List<Mobile>();

			if ( m_JusticeProtectors == null )
				m_JusticeProtectors = new List<Mobile>();

			if ( m_BOBFilter == null )
				m_BOBFilter = new Engines.BulkOrders.BOBFilter();

			if( m_GuildRank == null )
				m_GuildRank = Guilds.RankDefinition.Member;	//Default to member if going from older verstion to new version (only time it should be null)

			if( m_LastOnline == DateTime.MinValue && Account != null )
				m_LastOnline = ((Account)Account).LastLogin;

			if( m_ChampionTitles == null )
				m_ChampionTitles = new ChampionTitleInfo();

			if ( AccessLevel > AccessLevel.Player )
				m_IgnoreMobiles = true;

			List<Mobile> list = this.Stabled;

			for ( int i = 0; i < list.Count; ++i )
			{
				BaseCreature bc = list[i] as BaseCreature;

				if ( bc != null )
					bc.IsStabled = true;
			}

			CheckAtrophies( this );

			if( Hidden )	//Hiding is the only buff where it has an effect that's serialized.
				AddBuff( new BuffInfo( BuffIcon.HidingAndOrStealth, 1075655 ) );
		}



		public static void CheckAtrophies( Mobile m )
		{
			SacrificeVirtue.CheckAtrophy( m );
			JusticeVirtue.CheckAtrophy( m );
			CompassionVirtue.CheckAtrophy( m );
			ValorVirtue.CheckAtrophy( m );

			if( m is PlayerMobile )
				ChampionTitleInfo.CheckAtrophy( (PlayerMobile)m );
		}

		public void CheckKillDecay()
		{
			if ( m_ShortTermElapse < this.GameTime )
			{
				m_ShortTermElapse += TimeSpan.FromHours( 4 );
				if ( ShortTermMurders > 0 )
					--ShortTermMurders;
			}

			if ( m_LongTermElapse < this.GameTime )
			{
				m_LongTermElapse += TimeSpan.FromHours( 12 );
				if ( Kills > 0 )
					--Kills;
			}
		}

		public void ResetKillTime()
		{
			m_ShortTermElapse = this.GameTime + TimeSpan.FromHours( 4 );
			m_LongTermElapse = this.GameTime + TimeSpan.FromHours( 12 );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime SessionStart
		{
			get{ return m_SessionStart; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan GameTime
		{
			get
			{
				if ( NetState != null )
					return m_GameTime + (DateTime.UtcNow - m_SessionStart);
				else
					return m_GameTime;
			}
		}

		public override bool CanSee( Mobile m )
		{
			if ( m is CharacterStatue )
				((CharacterStatue) m).OnRequestedAnimation( this );

			if ( m is PlayerMobile && ((PlayerMobile)m).m_VisList.Contains( this ) )
				return true;

			return base.CanSee( m );
		}

		public override bool CanSee( Item item )
		{
			if ( m_DesignContext != null && m_DesignContext.Foundation.IsHiddenToCustomizer( item ) )
				return false;

			return base.CanSee( item );
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();
			this.HandleDeletion(true);
		}

		public void HandleDeletion(bool handleHouse) {

			if (handleHouse) {
				BaseHouse.HandleDeletion( this );	
			}
			DisguiseTimers.RemoveTimer( this );
		}

		public override bool NewGuildDisplay { get { return Server.Guilds.Guild.NewGuildSystem; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( this );
			if ( DB == null )
			{
				CharacterDatabase MyDB = new CharacterDatabase();
				MyDB.CharacterOwner = this;
				this.BankBox.DropItem( MyDB );
			}

			base.GetProperties( list );

			string sTitle = "" + GetPlayerInfo.GetSkillTitle( this ) + GetPlayerInfo.GetNPCGuild( this );
			list.Add( Utility.FixHtml( sTitle ) );
			
			if ( this.SoulBound)
			{
				if (!m_sbmaster)
					list.Add ( "this being is SoulBound" );
				else
					list.Add ( "this being is a master SoulBound" );
			}
			
			if ( this.m_soulbounddate != null && this.SoulBound)
				list.Add ( "Among the living since "+ m_soulbounddate);

			if ( AetherGlobe.EvilChamp == this || AetherGlobe.GoodChamp == this)
				list.Add ( "Champion of the Balance");
			else if (this.Avatar && !(this.SoulBound))
			{
				
				if (BalanceStatus > 0)
					list.Add ( "Avatar of the Balance, pledged to order" );
				else if (BalanceStatus < 0)
					list.Add ( "Avatar of the Balance, pledged to chaos" );
				else 
				{
					list.Add ( "Avatar of the Balance, unpledged to chaos or order" );
					list.Add ( "Seek out the Time Lord to choose a path" );
				}
			}

			if (this.SkillsCap == 1000)
				list.Add ( "Disciple of the Moon" );

			if (this.Sorcerer())
				list.Add ( "Sorcerer of the hidden arts." );
				
			if (this.Troubadour())
				list.Add ( "Wandering Troubadour - teller of tales." );
            if (this.Alchemist())
                list.Add("Mad Chemist - For Great Science!!.");
            if (this.BAC > 0)
			{
				string drunk = "a little tipsy";
				
				if (BAC > 50)
					drunk = "completely face-smashed.";
				else if (BAC > 40)
					drunk = "cross-eyed drunk.";
				else if (BAC > 30)
					drunk = "very drunk.";
				else if (BAC > 20)
					drunk = "drunk.";
				else if (BAC > 10)
					drunk = "wobbly.";
					
				list.Add("Currently " + drunk);
			}
			
			if (GetFlag( PlayerFlag.WellRested ) )
			{
				CheckRest();
				if (GetFlag( PlayerFlag.WellRested ) )
					list.Add("Well Rested");
			}

			if ( Core.ML )
			{
				for ( int i = AllFollowers.Count - 1; i >= 0; i-- )
				{
					BaseCreature c = AllFollowers[ i ] as BaseCreature;

					if ( c != null && c.ControlOrder == OrderType.Guard )
					{
						list.Add( 501129 ); // guarded
						break;
					}
				}
			}
		}

		public override void OnSingleClick( Mobile from )
		{

			base.OnSingleClick( from );
		}

/*		public void SoulTouchedAura(int stacks) {
			double soulBuff = (double)((double)stacks*0.25); // 25% per member
		 	double skillBuff = (double)((double)stacks*0.10); // 10% per member
			foreach ( Mobile mobile in this.GetMobilesInRange( 10 ) )
			{
				if (!(mobile is BaseConvo) && (mobile is BaseCreature) && (((BaseCreature)mobile).SecondsSoulTouched == 0)) {
					int newStr = (int)(mobile.RawStr*soulBuff);
					mobile.AddStatMod(new StatMod( StatType.Int, "SoulTouchedInt", newStr, TimeSpan.Zero ));				
					mobile.AddStatMod(new StatMod( StatType.Dex, "SoulTouchedDex", (int)(mobile.RawDex*soulBuff), TimeSpan.Zero ));		
					mobile.AddStatMod(new StatMod( StatType.Str, "SoulTouchedStr", (int)(mobile.RawStr*soulBuff), TimeSpan.Zero ));
					mobile.Hits += newStr;

					List<SkillMod> soulSkillMods = new List<SkillMod>();
					for ( int i = 0; i < mobile.Skills.Length; ++i )
					{
						if ( mobile.Skills[i].Value > 0 ) {
							double newSkillValue = (double)mobile.Skills[i].Value+(double)(mobile.Skills[i].Value*skillBuff);
							soulSkillMods.Add( new DefaultSkillMod( (SkillName)i, true, newSkillValue) );
						}
					}
					((BaseCreature)mobile).SoulSkillMods = soulSkillMods;
					int soulTouchSeconds = 0;
					if (this.LastGauntletLevel > 0) {
						soulTouchSeconds = 10; // dummy value, in a gauntlet it wont reset easily
					} else if (this.SoulBound && this.Party != null) {
						soulTouchSeconds = (int)GetGroupPhylacteryPower()/100;
						if (soulTouchSeconds < 10) {
							soulTouchSeconds = 10;
						}
					}
					((BaseCreature)mobile).SecondsSoulTouched = soulTouchSeconds;
					((BaseCreature)mobile).InitCheckSoulTouchedAuraTimer();
				}
			}
		}
*/
		public int GetGroupPhylacteryPower() {
			int groupPower = 0;
			Party party = (Party)this.Party;
			foreach(PartyMemberInfo member in party.Members) {
				Phylactery memberPhylactery = ((PlayerMobile)member.Mobile).FindPhylactery();
				if (memberPhylactery != null) {
					groupPower += memberPhylactery.TotalPower();
				}
			}
			return groupPower;
		}

		protected override bool OnMove( Direction d )
		{

			Item cloak = this.FindItemOnLayer( Layer.Cloak );
			if (cloak != null && cloak is RewardCloak)
			{
					switch( d & Direction.Mask )
					{
						case Direction.North:
							d = Direction.South;
							break;
						case Direction.Right:
							d = Direction.Left;
							break;
						case Direction.East:
							d = Direction.West;
							break;
						case Direction.Down:
							d = Direction.Up;
							break;
						case Direction.South:
							d = Direction.North;
							break;
						case Direction.Left:
							d = Direction.Right;
							break;
						case Direction.West:
							d = Direction.East;
							break;
						case Direction.Up:
							d = Direction.Down;
							break;
					}
			}

			if (AdventuresFunctions.IsPuritain((object)this)) 
			{

				if ( Mounted && Mount is BaseCreature)
				{
					BaseCreature mt = (BaseCreature)Mount;
					if (mt.Stam == 0)
					{
						SendMessage("Your mount is exhausted!");
						return false;
					}
				}
				
			}

			if (m_PokerGame != null) //Start Edit For Poker
			{
				if (!HasGump(typeof(PokerLeaveGump)))
				{
					SendGump(new PokerLeaveGump(this, m_PokerGame));
					return false;
				}
			} //End Edit For Poker

			if( AccessLevel != AccessLevel.Player )
				return true;

			if( Hidden && DesignContext.Find( this ) == null )	//Hidden & NOT customizing a house
			{
				if( !Mounted && Skills.Stealth.Value >= 25.0 )
				{
					bool running = (d & Direction.Running) != 0;

					if( running )
					{
						if( (AllowedStealthSteps -= 2) <= 0 )
							RevealingAction();
					}
					else if( AllowedStealthSteps-- <= 0 )
					{
						Server.SkillHandlers.Stealth.OnUse( this );
					}
				}
				else
				{
					RevealingAction();
				}
			}

			FlavorText( false );
			AmbientSounds( false );

			return true;
		}

		private bool m_BedrollLogout;

		public bool BedrollLogout
		{
			get{ return m_BedrollLogout; }
			set{ m_BedrollLogout = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override bool Paralyzed
		{
			get
			{
				return base.Paralyzed;
			}
			set
			{
				base.Paralyzed = value;

				if( value )
					AddBuff( new BuffInfo( BuffIcon.Paralyze, 1075827 ) );	//Paralyze/You are frozen and can not move
				else
					RemoveBuff( BuffIcon.Paralyze );
			}
		}


		#region Quests
		private QuestSystem m_Quest;
		private List<QuestRestartInfo> m_DoneQuests;
		private SolenFriendship m_SolenFriendship;

		public QuestSystem Quest
		{
			get{ return m_Quest; }
			set{ m_Quest = value; }
		}

		public List<QuestRestartInfo> DoneQuests
		{
			get{ return m_DoneQuests; }
			set{ m_DoneQuests = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SolenFriendship SolenFriendship
		{
			get{ return m_SolenFriendship; }
			set{ m_SolenFriendship = value; }
		}
		#endregion

		#region MyRunUO Invalidation
		private bool m_ChangedMyRunUO;

		public bool ChangedMyRunUO
		{
			get{ return m_ChangedMyRunUO; }
			set{ m_ChangedMyRunUO = value; }
		}

		public void InvalidateMyRunUO()
		{
			if ( !Deleted && !m_ChangedMyRunUO )
			{
				m_ChangedMyRunUO = true;
				Engines.MyRunUO.MyRunUO.QueueMobileUpdate( this );
			}
		}

		public override void OnKillsChange( int oldValue )
		{
			if ( this.Young && this.Kills > oldValue )
			{
				Account acc = this.Account as Account;

				if ( acc != null )
					acc.RemoveYoungStatus( 0 );
			}

			InvalidateMyRunUO();
		}

		public override void OnGenderChanged( bool oldFemale )
		{
			InvalidateMyRunUO();
		}

		public override void OnGuildChange( Server.Guilds.BaseGuild oldGuild )
		{
			InvalidateMyRunUO();
		}

		public override void OnGuildTitleChange( string oldTitle )
		{
			InvalidateMyRunUO();
		}

		public override void OnKarmaChange( int oldValue )
		{
			InvalidateMyRunUO();
		}

		public override void OnFameChange( int oldValue )
		{
			InvalidateMyRunUO();
		}

		public override void OnSkillChange( SkillName skill, double oldBase )
		{
			if ( this.Young && this.SkillsTotal >= 4500 )
			{
				Account acc = this.Account as Account;

				if ( acc != null )
					acc.RemoveYoungStatus( 1019036 ); // You have successfully obtained a respectable skill level, and have outgrown your status as a young player!
			}

			if (skill != SkillName.Focus)
			    DisruptAfk = true;

			InvalidateMyRunUO();
		}

		public override void OnAccessLevelChanged( AccessLevel oldLevel )
		{
			if ( AccessLevel == AccessLevel.Player )
				IgnoreMobiles = false;
			else
				IgnoreMobiles = true;

			InvalidateMyRunUO();
		}

		public override void OnRawStatChange( StatType stat, int oldValue )
		{
			InvalidateMyRunUO();
		}

		public override void OnDelete()
		{
			if ( m_ReceivedHonorContext != null )
				m_ReceivedHonorContext.Cancel();
			if ( m_SentHonorContext != null )
				m_SentHonorContext.Cancel();

			InvalidateMyRunUO();
		}

		#endregion

		#region Fastwalk Prevention
		private static bool FastwalkPrevention = true; // Is fastwalk prevention enabled?
		private static int FastwalkThreshold = 400; // Fastwalk prevention will become active after 0.4 seconds

		private long m_NextMovementTime;
		private bool m_HasMoved;

		public virtual bool UsesFastwalkPrevention{ get{ return ( AccessLevel < AccessLevel.Counselor ); } }
		public override int ComputeMovementSpeed(Direction dir, bool checkTurning)
		{ 
			if ( checkTurning && (dir & Direction.Mask) != (this.Direction & Direction.Mask) )
				return Mobile.RunMount;	// We are NOT actually moving (just a direction change)

			TransformContext context = TransformationSpellHelper.GetContext( this );

			bool running = ( (dir & Direction.Running) != 0 );

			bool onHorse = ( this.Mount != null );

			AnimalFormContext animalContext = AnimalForm.GetContext( this );

			if (AdventuresFunctions.IsPuritain((object)this)) 
			{
				if (running && !onHorse)
				{
					//lose stamina 40% of moves at MAX encumbrance and lowest agility
					if (Utility.RandomDouble() < (0.50 * (Encumbrance()) + 0.45 * (1- Agility())))
						Stam -= Utility.RandomMinMax(1, 3);

				}
				else if (running && onHorse && Mount is BaseCreature)
				{
					BaseCreature mt = (BaseCreature)Mount;
					//lose stamina 40% of moves at MAX encumbrance and lowest agility
					if (Utility.RandomDouble() < (0.30 * (Encumbrance()) + 0.15 * (1- mt.Agility())))
						mt.Stam -= Utility.RandomMinMax(1, 3);

				}					
				if ( !onHorse && Encumbrance() >= 0.90)
					return Mobile.WalkFoot;
				
			}
			
				if( onHorse || (animalContext != null && animalContext.SpeedBoost) )
					return ( running ? Mobile.RunMount : Mobile.WalkMount );

				return ( running ? Mobile.RunFoot : Mobile.WalkFoot );
			
		}

		public static bool MovementThrottle_Callback( NetState ns )
		{
			PlayerMobile pm = ns.Mobile as PlayerMobile;

			if ( pm != null )
			{
				if ( pm.FindItemOnLayer( Layer.Shoes ) != null )
				{
					Item shoes = pm.FindItemOnLayer( Layer.Shoes );
					if ( (shoes is BootsofHermes && !pm.SoulBound) ){ return true; }
				}
				if ( Spells.Mystic.WindRunner.UnderEffect( pm ) )
				{
					return true;
				}
				if ( pm.SoulBound && pm.sbmaster && pm.sbmasterspeed )
				{
					return true;
				}
				if ( Spells.Syth.SythSpeed.UnderEffect( pm ) )
				{
					return true;
				}
				if ( Spells.Jedi.Celerity.UnderEffect( pm ) )
				{
					return true;
				}
			}

			if ( pm == null || !pm.UsesFastwalkPrevention )
				return true;
				
			if (!pm.m_HasMoved)
			{
				// has not yet moved
				pm.m_NextMovementTime = Core.TickCount;
				pm.m_HasMoved = true;
				return true;
			}

			long ts = pm.m_NextMovementTime - Core.TickCount;
			if ( ts < 0 )
			{
				// been a while since we've last moved
				pm.m_NextMovementTime = Core.TickCount;
				return true;
			}

			return ( ts < FastwalkThreshold );
		}

		#endregion

		#region Enemy of One
		private Type m_EnemyOfOneType;
		private bool m_WaitingForEnemy;

		public Type EnemyOfOneType
		{
			get{ return m_EnemyOfOneType; }
			set
			{
				Type oldType = m_EnemyOfOneType;
				Type newType = value;

				if ( oldType == newType )
					return;

				m_EnemyOfOneType = value;

				DeltaEnemies( oldType, newType );
			}
		}

		public bool WaitingForEnemy
		{
			get{ return m_WaitingForEnemy; }
			set{ m_WaitingForEnemy = value; }
		}

		private void DeltaEnemies( Type oldType, Type newType )
		{
			foreach ( Mobile m in this.GetMobilesInRange( 18 ) )
			{
				Type t = m.GetType();

				if ( t == oldType || t == newType ) {
					NetState ns = this.NetState;

					if ( ns != null ) {
						if ( ns.StygianAbyss ) {
							ns.Send( new MobileMoving( m, Notoriety.Compute( this, m ) ) );
						} else {
							ns.Send( new MobileMovingOld( m, Notoriety.Compute( this, m ) ) );
						}
					}
				}
			}
		}

		#endregion

		#region Hair and beard mods
		private int m_HairModID = -1, m_HairModHue;
		private int m_BeardModID = -1, m_BeardModHue;

		public void SetHairMods( int hairID, int beardID )
		{
			if ( hairID == -1 )
				InternalRestoreHair( true, ref m_HairModID, ref m_HairModHue );
			else if ( hairID != -2 )
				InternalChangeHair( true, hairID, ref m_HairModID, ref m_HairModHue );

			if ( beardID == -1 )
				InternalRestoreHair( false, ref m_BeardModID, ref m_BeardModHue );
			else if ( beardID != -2 )
				InternalChangeHair( false, beardID, ref m_BeardModID, ref m_BeardModHue );
		}

		private void CreateHair( bool hair, int id, int hue )
		{
			if( hair )
			{
				//TODO Verification?
				HairItemID = id;
				HairHue = hue;
			}
			else
			{
				FacialHairItemID = id;
				FacialHairHue = hue;
			}
		}

		private void InternalRestoreHair( bool hair, ref int id, ref int hue )
		{
			if ( id == -1 )
				return;

			if ( hair )
				HairItemID = 0;
			else
				FacialHairItemID = 0;

			//if( id != 0 )
			CreateHair( hair, id, hue );

			id = -1;
			hue = 0;
		}

		private void InternalChangeHair( bool hair, int id, ref int storeID, ref int storeHue )
		{
			if ( storeID == -1 )
			{
				storeID = hair ? HairItemID : FacialHairItemID;
				storeHue = hair ? HairHue : FacialHairHue;
			}
			CreateHair( hair, id, 0 );
		}

		#endregion

		#region Virtues
		private DateTime m_LastSacrificeGain;
		private DateTime m_LastSacrificeLoss;
		private int m_AvailableResurrects;

		public DateTime LastSacrificeGain{ get{ return m_LastSacrificeGain; } set{ m_LastSacrificeGain = value; } }
		public DateTime LastSacrificeLoss{ get{ return m_LastSacrificeLoss; } set{ m_LastSacrificeLoss = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int AvailableResurrects{ get{ return m_AvailableResurrects; } set{ m_AvailableResurrects = value; } }

		private DateTime m_NextJustAward;
		private DateTime m_LastJusticeLoss;
		private List<Mobile> m_JusticeProtectors;

		public DateTime LastJusticeLoss{ get{ return m_LastJusticeLoss; } set{ m_LastJusticeLoss = value; } }
		public List<Mobile> JusticeProtectors { get { return m_JusticeProtectors; } set { m_JusticeProtectors = value; } }

		private DateTime m_LastCompassionLoss;
		private DateTime m_NextCompassionDay;
		private int m_CompassionGains;

		public DateTime LastCompassionLoss{ get{ return m_LastCompassionLoss; } set{ m_LastCompassionLoss = value; } }
		public DateTime NextCompassionDay{ get{ return m_NextCompassionDay; } set{ m_NextCompassionDay = value; } }
		public int CompassionGains{ get{ return m_CompassionGains; } set{ m_CompassionGains = value; } }

		private DateTime m_LastValorLoss;

		public DateTime LastValorLoss { get { return m_LastValorLoss; } set { m_LastValorLoss = value; } }

		private DateTime m_LastHonorLoss;
		private DateTime m_LastHonorUse;
		private bool m_HonorActive;
		private HonorContext m_ReceivedHonorContext;
		private HonorContext m_SentHonorContext;
		public DateTime m_hontime;

		public DateTime LastHonorLoss{ get{ return m_LastHonorLoss; } set{ m_LastHonorLoss = value; } }
		public DateTime LastHonorUse{ get{ return m_LastHonorUse; } set{ m_LastHonorUse = value; } }
		public bool HonorActive{ get{ return m_HonorActive; } set{ m_HonorActive = value; } }
		public HonorContext ReceivedHonorContext{ get{ return m_ReceivedHonorContext; } set{ m_ReceivedHonorContext = value; } }
		public HonorContext SentHonorContext{ get{ return m_SentHonorContext; } set{ m_SentHonorContext = value; } }
		#endregion

		#region Young system
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Young
		{
			get{ return GetFlag( PlayerFlag.Young ); }
			set{ SetFlag( PlayerFlag.Young, value ); InvalidateProperties(); }
		}

		public override string ApplyNameSuffix( string suffix )
		{
			if ( Young )
			{
				if ( suffix.Length == 0 )
					suffix = "(Young)";
				else
					suffix = String.Concat( suffix, " (Young)" );
			}


			return base.ApplyNameSuffix( suffix );
		}

		public override TimeSpan GetLogoutDelay()
		{
			if ( BedrollLogout )
				return TimeSpan.Zero;

			return base.GetLogoutDelay();
		}

		private DateTime m_LastYoungMessage = DateTime.MinValue;

		public bool CheckYoungProtection( Mobile from )
		{
			if ( !this.Young )
				return false;

			if ( Region is BaseRegion && !((BaseRegion)Region).YoungProtected )
				return false;

			if( from is BaseCreature && ((BaseCreature)from).IgnoreYoungProtection )
				return false;

			if ( this.Quest != null && this.Quest.IgnoreYoungProtection( from ) )
				return false;

			if ( DateTime.UtcNow - m_LastYoungMessage > TimeSpan.FromMinutes( 1.0 ) )
			{
				m_LastYoungMessage = DateTime.UtcNow;
				SendLocalizedMessage( 1019067 ); // A monster looks at you menacingly but does not attack.  You would be under attack now if not for your status as a new citizen of Britannia.
			}

			return true;
		}

		private DateTime m_LastYoungHeal = DateTime.MinValue;

		public bool CheckYoungHealTime()
		{
			if ( DateTime.UtcNow - m_LastYoungHeal > TimeSpan.FromMinutes( 5.0 ) )
			{
				m_LastYoungHeal = DateTime.UtcNow;
				return true;
			}

			return false;
		}

		private static Point3D[] m_TrammelDeathDestinations = new Point3D[]
			{
				new Point3D( 1481, 1612, 20 ),
				new Point3D( 2708, 2153,  0 ),
				new Point3D( 2249, 1230,  0 ),
				new Point3D( 5197, 3994, 37 ),
				new Point3D( 1412, 3793,  0 ),
				new Point3D( 3688, 2232, 20 ),
				new Point3D( 2578,  604,  0 ),
				new Point3D( 4397, 1089,  0 ),
				new Point3D( 5741, 3218, -2 ),
				new Point3D( 2996, 3441, 15 ),
				new Point3D(  624, 2225,  0 ),
				new Point3D( 1916, 2814,  0 ),
				new Point3D( 2929,  854,  0 ),
				new Point3D(  545,  967,  0 ),
				new Point3D( 3665, 2587,  0 )
			};

		private static Point3D[] m_IlshenarDeathDestinations = new Point3D[]
			{
				new Point3D( 1216,  468, -13 ),
				new Point3D(  723, 1367, -60 ),
				new Point3D(  745,  725, -28 ),
				new Point3D(  281, 1017,   0 ),
				new Point3D(  986, 1011, -32 ),
				new Point3D( 1175, 1287, -30 ),
				new Point3D( 1533, 1341,  -3 ),
				new Point3D(  529,  217, -44 ),
				new Point3D( 1722,  219,  96 )
			};

		private static Point3D[] m_MalasDeathDestinations = new Point3D[]
			{
				new Point3D( 2079, 1376, -70 ),
				new Point3D(  944,  519, -71 )
			};

		private static Point3D[] m_TokunoDeathDestinations = new Point3D[]
			{
				new Point3D( 1166,  801, 27 ),
				new Point3D(  782, 1228, 25 ),
				new Point3D(  268,  624, 15 )
			};

		public bool YoungDeathTeleport()
		{
			if ( this.Region.IsPartOf( typeof( Jail ) )
				|| this.Region.IsPartOf( "Samurai start location" )
				|| this.Region.IsPartOf( "Ninja start location" )
				|| this.Region.IsPartOf( "Ninja cave" ) )
				return false;

			Point3D loc;
			Map map;

			DungeonRegion dungeon = (DungeonRegion) this.Region.GetRegion( typeof( DungeonRegion ) );
			if ( dungeon != null && dungeon.EntranceLocation != Point3D.Zero )
			{
				loc = dungeon.EntranceLocation;
				map = dungeon.EntranceMap;
			}
			else
			{
				loc = this.Location;
				map = this.Map;
			}

			Point3D[] list;

			list = m_TrammelDeathDestinations;

			Point3D dest = Point3D.Zero;
			int sqDistance = int.MaxValue;

			for ( int i = 0; i < list.Length; i++ )
			{
				Point3D curDest = list[i];

				int width = loc.X - curDest.X;
				int height = loc.Y - curDest.Y;
				int curSqDistance = width * width + height * height;

				if ( curSqDistance < sqDistance )
				{
					dest = curDest;
					sqDistance = curSqDistance;
				}
			}

			this.MoveToWorld( dest, map );
			return true;
		}

		private void SendYoungDeathNotice()
		{
			this.SendGump( new YoungDeathNotice() );
		}

		#endregion

		#region Speech log
		private SpeechLog m_SpeechLog;

		public SpeechLog SpeechLog{ get{ return m_SpeechLog; } }

		public override void OnSpeech( SpeechEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile && e.Mobile == this )
			{
				m_lastwords = e.Speech.ToString();
				
				string speech = e.Speech;
				
				if ( (Insensitive.Contains( speech, "i wish to make" ) || Insensitive.Contains( speech, "i wish to start" ) || Insensitive.Contains( speech, "i wish to have" ) || Insensitive.Contains( speech, "i wish to sacrifice" ) ) && !GetFlag( PlayerFlag.IsAutomated ) )
				{
					AdventuresAutomation.StartTask(this, speech);
				}
				else if (  Insensitive.Contains( speech, "i wish to stop" ) && GetFlag( PlayerFlag.IsAutomated ) )
				{
					AdventuresAutomation.StopAction(this);
					SetFlag( PlayerFlag.IsAutomated, false );
				}
				else if (  Insensitive.Contains( speech, "i wish to strip" ) )
				{
					AdventuresAutomation.UndressItem( this, Layer.OuterTorso );
					AdventuresAutomation.UndressItem( this, Layer.InnerTorso );
					AdventuresAutomation.UndressItem( this, Layer.MiddleTorso );
					AdventuresAutomation.UndressItem( this, Layer.Pants );
					AdventuresAutomation.UndressItem( this, Layer.Shirt );
					AdventuresAutomation.UndressItem( this, Layer.OuterLegs );
					AdventuresAutomation.UndressItem( this, Layer.Waist );
					AdventuresAutomation.UndressItem( this, Layer.Cloak );
				}
			}

			if ( SpeechLog.Enabled && this.NetState != null )
			{
				if ( m_SpeechLog == null )
					m_SpeechLog = new SpeechLog();

				m_SpeechLog.Add( e.Mobile, e.Speech );
			}

		}

		#endregion

		#region Champion Titles
		[CommandProperty( AccessLevel.GameMaster )]
		public bool DisplayChampionTitle
		{
			get { return GetFlag( PlayerFlag.DisplayChampionTitle ); }
			set { SetFlag( PlayerFlag.DisplayChampionTitle, value ); }
		}

		private ChampionTitleInfo m_ChampionTitles;

		[CommandProperty( AccessLevel.GameMaster )]
		public ChampionTitleInfo ChampionTitles { get { return m_ChampionTitles; } set { } }

		private void ToggleChampionTitleDisplay()
		{
			if( !CheckAlive() )
				return;

			if( DisplayChampionTitle )
				SendLocalizedMessage( 1062419, "", 0x23 ); // You have chosen to hide your monster kill title.
			else
				SendLocalizedMessage( 1062418, "", 0x23 ); // You have chosen to display your monster kill title.

			DisplayChampionTitle = !DisplayChampionTitle;
		}

		[PropertyObject]
		public class ChampionTitleInfo
		{
			public static TimeSpan LossDelay = TimeSpan.FromDays( 1.0 );
			public const int LossAmount = 90;

			private class TitleInfo
			{
				private int m_Value;
				private DateTime m_LastDecay;

				public int Value { get { return m_Value; } set { m_Value = value; } }
				public DateTime LastDecay { get { return m_LastDecay; } set { m_LastDecay = value; } }

				public TitleInfo()
				{
				}

				public TitleInfo( GenericReader reader )
				{
					int version = reader.ReadEncodedInt();

					switch( version )
					{
						case 0:
						{
							m_Value = reader.ReadEncodedInt();
							m_LastDecay = reader.ReadDateTime();
							break;
						}
					}
				}

				public static void Serialize( GenericWriter writer, TitleInfo info )
				{
					writer.WriteEncodedInt( (int)0 ); // version

					writer.WriteEncodedInt( info.m_Value );
					writer.Write( info.m_LastDecay );
				}
			}

			private TitleInfo[] m_Values;

			private int m_Harrower;	//Harrower titles do NOT decay

			public int GetValue( ChampionSpawnType type )
			{
				return GetValue( (int)type );
			}

			public void SetValue( ChampionSpawnType type, int value )
			{
				SetValue( (int)type, value );
			}

			public void Award( ChampionSpawnType type, int value )
			{
				Award( (int)type, value );
			}

			public int GetValue( int index )
			{
				if( m_Values == null || index < 0 || index >= m_Values.Length )
					return 0;

				if( m_Values[index] == null )
					m_Values[index] = new TitleInfo();

				return m_Values[index].Value;
			}

			public DateTime GetLastDecay( int index )
			{
				if( m_Values == null || index < 0 || index >= m_Values.Length )
					return DateTime.MinValue;

				if( m_Values[index] == null )
					m_Values[index] = new TitleInfo();

				return m_Values[index].LastDecay;
			}

			public void SetValue( int index, int value )
			{
				if( m_Values == null )
					m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

				if( value < 0 )
					value = 0;

				if( index < 0 || index >= m_Values.Length )
					return;

				if( m_Values[index] == null )
					m_Values[index] = new TitleInfo();

				m_Values[index].Value = value;
			}

			public void Award( int index, int value )
			{
				if( m_Values == null )
					m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

				if( index < 0 || index >= m_Values.Length || value <= 0 )
					return;

				if( m_Values[index] == null )
					m_Values[index] = new TitleInfo();

				m_Values[index].Value += value;
			}

			public void Atrophy( int index, int value )
			{
				if( m_Values == null )
					m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

				if( index < 0 || index >= m_Values.Length || value <= 0 )
					return;

				if( m_Values[index] == null )
					m_Values[index] = new TitleInfo();

				int before = m_Values[index].Value;

				if( (m_Values[index].Value - value) < 0 )
					m_Values[index].Value = 0;
				else
					m_Values[index].Value -= value;

				if( before != m_Values[index].Value )
					m_Values[index].LastDecay = DateTime.UtcNow;
			}

			public override string ToString()
			{
				return "...";
			}

			//[CommandProperty(AccessLevel.GameMaster)]
			//public int Pestilence { get { return GetValue(ChampionSpawnType.Pestilence); } set { SetValue(ChampionSpawnType.Pestilence, value); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int Abyss { get { return GetValue( ChampionSpawnType.Abyss ); } set { SetValue( ChampionSpawnType.Abyss, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int Arachnid { get { return GetValue( ChampionSpawnType.Arachnid ); } set { SetValue( ChampionSpawnType.Arachnid, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int ColdBlood { get { return GetValue( ChampionSpawnType.ColdBlood ); } set { SetValue( ChampionSpawnType.ColdBlood, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int ForestLord { get { return GetValue( ChampionSpawnType.ForestLord ); } set { SetValue( ChampionSpawnType.ForestLord, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int SleepingDragon { get { return GetValue( ChampionSpawnType.SleepingDragon ); } set { SetValue( ChampionSpawnType.SleepingDragon, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int UnholyTerror { get { return GetValue( ChampionSpawnType.UnholyTerror ); } set { SetValue( ChampionSpawnType.UnholyTerror, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int VerminHorde { get { return GetValue( ChampionSpawnType.VerminHorde ); } set { SetValue( ChampionSpawnType.VerminHorde, value ); } }

			[CommandProperty( AccessLevel.GameMaster )]
			public int Harrower { get { return m_Harrower; } set { m_Harrower = value; } }

			public ChampionTitleInfo()
			{
			}

			public ChampionTitleInfo( GenericReader reader )
			{
				int version = reader.ReadEncodedInt();

				switch( version )
				{
					case 0:
					{
						m_Harrower = reader.ReadEncodedInt();

						int length = reader.ReadEncodedInt();
						m_Values = new TitleInfo[length];

						for( int i = 0; i < length; i++ )
						{
							m_Values[i] = new TitleInfo( reader );
						}

						if( m_Values.Length != ChampionSpawnInfo.Table.Length )
						{
							TitleInfo[] oldValues = m_Values;
							m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

							for( int i = 0; i < m_Values.Length && i < oldValues.Length; i++ )
							{
								m_Values[i] = oldValues[i];
							}
						}
						break;
					}
				}
			}

			public static void Serialize( GenericWriter writer, ChampionTitleInfo titles )
			{
				writer.WriteEncodedInt( (int)0 ); // version

				writer.WriteEncodedInt( titles.m_Harrower );

				int length = titles.m_Values.Length;
				writer.WriteEncodedInt( length );

				for( int i = 0; i < length; i++ )
				{
					if( titles.m_Values[i] == null )
						titles.m_Values[i] = new TitleInfo();

					TitleInfo.Serialize( writer, titles.m_Values[i] );
				}
			}

			public static void CheckAtrophy( PlayerMobile pm )
			{
				ChampionTitleInfo t = pm.m_ChampionTitles;
				if( t == null )
					return;

				if( t.m_Values == null )
					t.m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

				for( int i = 0; i < t.m_Values.Length; i++ )
				{
					if( (t.GetLastDecay( i ) + LossDelay) < DateTime.UtcNow )
					{
						t.Atrophy( i, LossAmount );
					}
				}
			}

			public static void AwardHarrowerTitle( PlayerMobile pm )	//Called when killing a harrower.  Will give a minimum of 1 point.
			{
				ChampionTitleInfo t = pm.m_ChampionTitles;
				if( t == null )
					return;

				if( t.m_Values == null )
					t.m_Values = new TitleInfo[ChampionSpawnInfo.Table.Length];

				int count = 1;

				for( int i = 0; i < t.m_Values.Length; i++ )
				{
					if( t.m_Values[i].Value > 900 )
						count++;
				}

				t.m_Harrower = Math.Max( count, t.m_Harrower );	//Harrower titles never decay.
			}
		}

		#endregion

		#region Recipes

		private Dictionary<int, bool> m_AcquiredRecipes;

		public virtual bool HasRecipe( Recipe r )
		{
			if( r == null )
				return false;

			return HasRecipe( r.ID );
		}

		public virtual bool HasRecipe( int recipeID )
		{
			if( m_AcquiredRecipes != null && m_AcquiredRecipes.ContainsKey( recipeID ) )
				return m_AcquiredRecipes[recipeID];

			return false;
		}

		public virtual void AcquireRecipe( Recipe r )
		{
			if( r != null )
				AcquireRecipe( r.ID );
		}

		public virtual void AcquireRecipe( int recipeID )
		{
			if( m_AcquiredRecipes == null )
				m_AcquiredRecipes = new Dictionary<int, bool>();

			m_AcquiredRecipes[recipeID] = true;
		}

		public virtual void ResetRecipes()
		{
			m_AcquiredRecipes = null;
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int KnownRecipes
		{
			get
			{
				if( m_AcquiredRecipes == null )
					return 0;

				return m_AcquiredRecipes.Count;
			}
		}

		#endregion

		#region Buff Icons

		public void ResendBuffs()
		{
			if( !BuffInfo.Enabled || m_BuffTable == null )
				return;

			NetState state = this.NetState;

			if( state != null && state.BuffIcon )
			{
				foreach( BuffInfo info in m_BuffTable.Values )
				{
					state.Send( new AddBuffPacket( this, info ) );
				}
			}
		}

		private Dictionary<BuffIcon, BuffInfo> m_BuffTable;

		public void AddBuff( BuffInfo b )
		{
			if( !BuffInfo.Enabled || b == null )
				return;

			RemoveBuff( b );	//Check & subsequently remove the old one.

			if( m_BuffTable == null )
				m_BuffTable = new Dictionary<BuffIcon, BuffInfo>();

			m_BuffTable.Add( b.ID, b );

			NetState state = this.NetState;

			if( state != null && state.BuffIcon )
			{
				state.Send( new AddBuffPacket( this, b ) );
			}
		}

		public void RemoveBuff( BuffInfo b )
		{
			if( b == null )
				return;

			RemoveBuff( b.ID );
		}

		public void RemoveBuff( BuffIcon b )
		{
			if( m_BuffTable == null || !m_BuffTable.ContainsKey( b ) )
				return;

			BuffInfo info = m_BuffTable[b];

			if( info.Timer != null && info.Timer.Running )
				info.Timer.Stop();

			m_BuffTable.Remove( b );

			NetState state = this.NetState;

			if( state != null && state.BuffIcon )
			{
				state.Send( new RemoveBuffPacket( this, b ) );
			}

			if( m_BuffTable.Count <= 0 )
				m_BuffTable = null;
		}

		#endregion

		public List<Mobile> getNearbyPets(int range) {
			List<Mobile> list = new List<Mobile>();
			if (AllFollowers.Count > 0) {
				foreach ( Mobile mobile in this.GetMobilesInRange( range ) ) {
					for ( int i = AllFollowers.Count - 1; i >= 0; --i )
					{
						Mobile pet = AllFollowers[i] as Mobile;
						if (pet.Serial == mobile.Serial) {
							list.Add(pet);
						}
					}
				}
			}
			return list;
		}

		public void AutoStablePets()
		{
			if ( Core.SE && AllFollowers.Count > 0 )
			{
				for ( int i = m_AllFollowers.Count - 1; i >= 0; --i )
				{
					BaseCreature pet = AllFollowers[i] as BaseCreature;

					if (pet == null || pet.ControlMaster == null || pet.IsHitchStabled)
                        continue;

					if (pet.Summoned)
					{
						if (pet.Map != Map)
						{
							pet.PlaySound( pet.GetAngerSound() );
							Timer.DelayCall( TimeSpan.Zero, new TimerCallback( pet.Delete ) );
						}
						continue;
					}

					if ( (pet is IMount && ((IMount)pet).Rider != null ) || (this.Mounted && this.Mount == pet) )
						continue;

					if ( (pet is PackLlama || pet is PackHorse || pet is Beetle || pet is HordeMinionFamiliar) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) )
						continue;

					pet.ControlTarget = null;
					pet.ControlOrder = OrderType.Stay;
					pet.Internalize();

					pet.SetControlMaster( null );
					pet.SummonMaster = null;

					pet.IsStabled = true;

					//pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully happy

					Stabled.Add( pet );
					m_AutoStabled.Add( pet );
				}
			}
		}

		public void ClaimAutoStabledPets()
		{
			if ( !Core.SE || m_AutoStabled.Count <= 0 )
				return;

			if ( !Alive )
			{
				SendLocalizedMessage( 1076251 ); // Your pet was unable to join you while you are a ghost.  Please re-login once you have ressurected to claim your pets.
				return;
			}

			for ( int i = m_AutoStabled.Count - 1; i >= 0; --i )
			{
				BaseCreature pet = m_AutoStabled[i] as BaseCreature;

				if ( pet == null || pet.Deleted )
				{
					pet.IsStabled = false;

					if ( Stabled.Contains( pet ) )
						Stabled.Remove( pet );

					continue;
				}

				if ( (Followers + pet.ControlSlots) <= FollowersMax )
				{
					pet.SetControlMaster( this );

					if ( pet.Summoned )
						pet.SummonMaster = this;

					pet.ControlTarget = this;
					pet.ControlOrder = OrderType.Follow;

					pet.MoveToWorld( Location, Map );

					pet.IsStabled = false;

					//pet.Loyalty = BaseCreature.MaxLoyalty; // Wonderfully Happy

					if ( Stabled.Contains( pet ) )
						Stabled.Remove( pet );
				}
				else
				{
					SendLocalizedMessage( 1049612, pet.Name ); // ~1_NAME~ remained in the stables because you have too many followers.
				}
			}

			m_AutoStabled.Clear();
		}
	}
}
