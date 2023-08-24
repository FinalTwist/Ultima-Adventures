using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Engines.Craft;
using Server.Targeting;
using Server.Misc;
using AMA = Server.Items.ArmorMeditationAllowance;
using AMT = Server.Items.ArmorMaterialType;
using ABT = Server.Items.ArmorBodyType;
using System.Globalization;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class BaseArmor : Item, IScissorable, ICraftable, IWearableDurability
	{


		// Instance values. These values must are unique to each armor piece.
		private int m_MaxHitPoints;
		private int m_HitPoints;
		private Mobile m_Crafter;
		private ArmorQuality m_Quality;
		private ArmorDurabilityLevel m_Durability;
		private ArmorProtectionLevel m_Protection;
		private CraftResource m_Resource;
		private bool m_Identified, m_PlayerConstructed;
		private int m_PhysicalBonus, m_FireBonus, m_ColdBonus, m_PoisonBonus, m_EnergyBonus;

		private AosAttributes m_AosAttributes;
		private AosArmorAttributes m_AosArmorAttributes;
		private AosSkillBonuses m_AosSkillBonuses;

		// Overridable values. These values are provided to override the defaults which get defined in the individual armor scripts.
		private int m_ArmorBase = -1;
		private int m_StrBonus = -1, m_DexBonus = -1, m_IntBonus = -1;
		private int m_StrReq = -1, m_DexReq = -1, m_IntReq = -1;
		private AMA m_Meditate = (AMA)(-1);


		public virtual bool AllowMaleWearer{ get{ return true; } }
		public virtual bool AllowFemaleWearer{ get{ return true; } }

		public abstract AMT MaterialType{ get; }

		public virtual int RevertArmorBase{ get{ return ArmorBase; } }
		public virtual int ArmorBase{ get{ return 0; } }

		public virtual AMA DefMedAllowance{ get{ return AMA.None; } }
		public virtual AMA AosMedAllowance{ get{ return DefMedAllowance; } }
		public virtual AMA OldMedAllowance{ get{ return DefMedAllowance; } }


		public virtual int AosStrBonus{ get{ return 0; } }
		public virtual int AosDexBonus{ get{ return 0; } }
		public virtual int AosIntBonus{ get{ return 0; } }
		public virtual int AosStrReq{ get{ return 0; } }
		public virtual int AosDexReq{ get{ return 0; } }
		public virtual int AosIntReq{ get{ return 0; } }


		public virtual int OldStrBonus{ get{ return 0; } }
		public virtual int OldDexBonus{ get{ return 0; } }
		public virtual int OldIntBonus{ get{ return 0; } }
		public virtual int OldStrReq{ get{ return 0; } }
		public virtual int OldDexReq{ get{ return 0; } }
		public virtual int OldIntReq{ get{ return 0; } }

		public virtual bool CanFortify{ get{ return true; } }

		private int m_wear;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Wear
		{
			get{ return m_wear;}
			set{m_wear = value;}
		}

		public override void OnAfterDuped( Item newItem )
		{
			BaseArmor armor = newItem as BaseArmor;

			if ( armor == null )
				return;

			armor.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			armor.m_AosArmorAttributes = new AosArmorAttributes( newItem, m_AosArmorAttributes );
			armor.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AMA MeditationAllowance
		{
			get{ return ( m_Meditate == (AMA)(-1) ? Core.AOS ? AosMedAllowance : OldMedAllowance : m_Meditate ); }
			set{ m_Meditate = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BaseArmorRating
		{
			get
			{
				if ( m_ArmorBase == -1 )
					return ArmorBase;
				else
					return m_ArmorBase;
			}
			set
			{ 
				m_ArmorBase = value; Invalidate(); 
			}
		}

		public double BaseArmorRatingScaled
		{
			get
			{
				return ( BaseArmorRating * ArmorScalar );
			}
		}

		public virtual double ArmorRating
		{
			get
			{
				int ar = BaseArmorRating;

				if ( m_Protection != ArmorProtectionLevel.Regular )
					ar += 10 + (5 * (int)m_Protection);

				switch ( m_Resource )
				{
					case CraftResource.DullCopper:		ar += 6; break;
					case CraftResource.ShadowIron:		ar += 12; break;
					case CraftResource.Copper:			ar += 18; break;
					case CraftResource.Bronze:			ar += 24; break;
					case CraftResource.Gold:			ar += 30; break;
					case CraftResource.Agapite:			ar += 36; break;
					case CraftResource.Verite:			ar += 42; break;
					case CraftResource.Valorite:		ar += 48; break;
					case CraftResource.Nepturite:		ar += 48; break;
					case CraftResource.Obsidian:		ar += 48; break;
					case CraftResource.Steel:			ar += 53; break;
					case CraftResource.Brass:			ar += 60; break;
					case CraftResource.Mithril:			ar += 66; break;
					case CraftResource.Xormite:			ar += 66; break;
					case CraftResource.Dwarven:			ar += 108; break;
					case CraftResource.SpinedLeather:	ar += 8; break;
					case CraftResource.HornedLeather:	ar += 12; break;
					case CraftResource.BarbedLeather:	ar += 16; break;
					case CraftResource.NecroticLeather:	ar += 18; break;
					case CraftResource.VolcanicLeather:	ar += 20; break;
					case CraftResource.FrozenLeather:	ar += 24; break;
					case CraftResource.GoliathLeather:	ar += 28; break;
					case CraftResource.DraconicLeather:	ar += 30; break;
					case CraftResource.HellishLeather:	ar += 32; break;
					case CraftResource.DinosaurLeather:	ar += 36; break;
					case CraftResource.AlienLeather:	ar += 36; break;
					case CraftResource.AshTree: 		ar += 6; break;
					case CraftResource.CherryTree: 		ar += 12; break;
					case CraftResource.EbonyTree: 		ar += 18; break;
					case CraftResource.GoldenOakTree: 	ar += 24; break;
					case CraftResource.HickoryTree: 	ar += 30; break;
					case CraftResource.MahoganyTree: 	ar += 32; break;
					case CraftResource.DriftwoodTree: 	ar += 32; break;
					case CraftResource.OakTree: 		ar += 36; break;
					case CraftResource.PineTree: 		ar += 38; break;
					case CraftResource.GhostTree: 		ar += 38; break;
					case CraftResource.RosewoodTree: 	ar += 42; break;
					case CraftResource.WalnutTree: 		ar += 46; break;
					case CraftResource.PetrifiedTree: 	ar += 54; break;
					case CraftResource.ElvenTree: 		ar += 90; break;
					case CraftResource.YellowScales:	ar += 32; break;
					case CraftResource.RedScales:		ar += 36; break;
					case CraftResource.GreenScales:		ar += 38; break;
					case CraftResource.BlackScales:		ar += 42; break;
					case CraftResource.WhiteScales:		ar += 46; break;
					case CraftResource.BlueScales:		ar += 48; break;
				}

				ar += -8 + (8 * (int)m_Quality);
				return ScaleArmorByDurability( ar );
			}
		}

		public double ArmorRatingScaled
		{
			get
			{
				return ( ArmorRating * ArmorScalar );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StrBonus
		{
			get{ return ( m_StrBonus == -1 ? Core.AOS ? AosStrBonus : OldStrBonus : m_StrBonus ); }
			set{ m_StrBonus = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DexBonus
		{
			get{ return ( m_DexBonus == -1 ? Core.AOS ? AosDexBonus : OldDexBonus : m_DexBonus ); }
			set{ m_DexBonus = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int IntBonus
		{
			get{ return ( m_IntBonus == -1 ? Core.AOS ? AosIntBonus : OldIntBonus : m_IntBonus ); }
			set{ m_IntBonus = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StrRequirement
		{
			get{ return ( m_StrReq == -1 ? Core.AOS ? AosStrReq : OldStrReq : m_StrReq ); }
			set{ m_StrReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DexRequirement
		{
			get{ return ( m_DexReq == -1 ? Core.AOS ? AosDexReq : OldDexReq : m_DexReq ); }
			set{ m_DexReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int IntRequirement
		{
			get{ return ( m_IntReq == -1 ? Core.AOS ? AosIntReq : OldIntReq : m_IntReq ); }
			set{ m_IntReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Identified
		{
			get{ return m_Identified; }
			set{ m_Identified = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PlayerConstructed
		{
			get{ return m_PlayerConstructed; }
			set{ m_PlayerConstructed = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get
			{
				return m_Resource;
			}
			set
			{
				if ( m_Resource != value )
				{
					UnscaleDurability();

					m_Resource = value;

					if (!DefTailoring.IsNonColorable(this.GetType()))
					{
						Hue = CraftResources.GetHue(m_Resource);
					}

					Invalidate();
					InvalidateProperties();

					if ( Parent is Mobile )
						((Mobile)Parent).UpdateResistances();

					ScaleDurability();
				}
			}
		}

		public virtual double ArmorScalar
		{
			get
			{
				int pos = (int)BodyPosition;

				if ( pos >= 0 && pos < m_ArmorScalars.Length )
					return m_ArmorScalars[pos];

				return 1.0;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxHitPoints
		{
			get{ return m_MaxHitPoints; }
			set{ m_MaxHitPoints = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitPoints
		{
			get 
			{
				return m_HitPoints;
			}
			set 
			{
				if ( value != m_HitPoints && MaxHitPoints > 0 )
				{
					m_HitPoints = value;

					if ( m_HitPoints < 0 )
						Delete();
					else if ( m_HitPoints > MaxHitPoints )
						m_HitPoints = MaxHitPoints;

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		
		[CommandProperty( AccessLevel.GameMaster )]
		public ArmorQuality Quality
		{
			get{ return m_Quality; }
			set{ UnscaleDurability(); m_Quality = value; Invalidate(); InvalidateProperties(); ScaleDurability(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ArmorDurabilityLevel Durability
		{
			get{ return m_Durability; }
			set{ UnscaleDurability(); m_Durability = value; ScaleDurability(); InvalidateProperties(); }
		}

		public virtual int ArtifactRarity
		{
			get{ return 0; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ArmorProtectionLevel ProtectionLevel
		{
			get
			{
				return m_Protection;
			}
			set
			{
				if ( m_Protection != value )
				{
					m_Protection = value;

					Invalidate();
					InvalidateProperties();

					if ( Parent is Mobile )
						((Mobile)Parent).UpdateResistances();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosArmorAttributes ArmorAttributes
		{
			get{ return m_AosArmorAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		public int ComputeStatReq( StatType type )
		{
			int v;

			if ( type == StatType.Str )
				v = StrRequirement;
			else if ( type == StatType.Dex )
				v = DexRequirement;
			else
				v = IntRequirement;

			return AOS.Scale( v, 100 - GetLowerStatReq() );
		}

		public int ComputeStatBonus( StatType type )
		{
			if ( type == StatType.Str )
				return StrBonus + Attributes.BonusStr;
			else if ( type == StatType.Dex )
				return DexBonus + Attributes.BonusDex;
			else
				return IntBonus + Attributes.BonusInt;
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PhysicalBonus{ get{ return m_PhysicalBonus; } set{ m_PhysicalBonus = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int FireBonus{ get{ return m_FireBonus; } set{ m_FireBonus = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ColdBonus{ get{ return m_ColdBonus; } set{ m_ColdBonus = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PoisonBonus{ get{ return m_PoisonBonus; } set{ m_PoisonBonus = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int EnergyBonus{ get{ return m_EnergyBonus; } set{ m_EnergyBonus = value; InvalidateProperties(); } }

		public virtual int BasePhysicalResistance{ get{ return 0; } }
		public virtual int BaseFireResistance{ get{ return 0; } }
		public virtual int BaseColdResistance{ get{ return 0; } }
		public virtual int BasePoisonResistance{ get{ return 0; } }
		public virtual int BaseEnergyResistance{ get{ return 0; } }

		public override int PhysicalResistance{ get{ return BasePhysicalResistance + GetProtOffset() + GetResourceAttrs().ArmorPhysicalResist + m_PhysicalBonus; } }
		public override int FireResistance{ get{ return BaseFireResistance + GetProtOffset() + GetResourceAttrs().ArmorFireResist + m_FireBonus; } }
		public override int ColdResistance{ get{ return BaseColdResistance + GetProtOffset() + GetResourceAttrs().ArmorColdResist + m_ColdBonus; } }
		public override int PoisonResistance{ get{ return BasePoisonResistance + GetProtOffset() + GetResourceAttrs().ArmorPoisonResist + m_PoisonBonus; } }
		public override int EnergyResistance{ get{ return BaseEnergyResistance + GetProtOffset() + GetResourceAttrs().ArmorEnergyResist + m_EnergyBonus; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public ArmorBodyType BodyPosition
		{
			get
			{
				switch ( this.Layer )
				{
					default:
					case Layer.Neck:		return ArmorBodyType.Gorget;
					case Layer.TwoHanded:	return ArmorBodyType.Shield;
					case Layer.Gloves:		return ArmorBodyType.Gloves;
					case Layer.Helm:		return ArmorBodyType.Helmet;
					case Layer.Arms:		return ArmorBodyType.Arms;

					case Layer.InnerLegs:
					case Layer.OuterLegs:
					case Layer.Pants:		return ArmorBodyType.Legs;

					case Layer.InnerTorso:
					case Layer.OuterTorso:
					case Layer.Shirt:		return ArmorBodyType.Chest;
				}
			}
		}

		public void DistributeBonuses( int amount )
		{
			for ( int i = 0; i < amount; ++i )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: ++m_PhysicalBonus; break;
					case 1: ++m_FireBonus; break;
					case 2: ++m_ColdBonus; break;
					case 3: ++m_PoisonBonus; break;
					case 4: ++m_EnergyBonus; break;
				}
			}

			InvalidateProperties();
		}

		public CraftAttributeInfo GetResourceAttrs()
		{
			CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

			if ( info == null )
				return CraftAttributeInfo.Blank;

			return info.AttributeInfo;
		}

		public int GetProtOffset()
		{
			switch ( m_Protection )
			{
				case ArmorProtectionLevel.Guarding: return 1;
				case ArmorProtectionLevel.Hardening: return 2;
				case ArmorProtectionLevel.Fortification: return 3;
				case ArmorProtectionLevel.Invulnerability: return 4;
			}

			return 0;
		}

		public void UnscaleDurability()
		{
			int scale = 100 + GetDurabilityBonus();

			m_HitPoints = ((m_HitPoints * 100) + (scale - 1)) / scale;
			m_MaxHitPoints = ((m_MaxHitPoints * 100) + (scale - 1)) / scale;
			InvalidateProperties();
		}

		public void ScaleDurability()
		{
			int scale = 100 + GetDurabilityBonus();

			m_HitPoints = ((m_HitPoints * scale) + 99) / 100;
			m_MaxHitPoints = ((m_MaxHitPoints * scale) + 99) / 100;
			InvalidateProperties();
		}

		public int GetDurabilityBonus()
		{
			int bonus = 0;

			if ( m_Quality == ArmorQuality.Exceptional )
				bonus += 20;

			switch ( m_Durability )
			{
				case ArmorDurabilityLevel.Durable: bonus += 20; break;
				case ArmorDurabilityLevel.Substantial: bonus += 50; break;
				case ArmorDurabilityLevel.Massive: bonus += 70; break;
				case ArmorDurabilityLevel.Fortified: bonus += 100; break;
				case ArmorDurabilityLevel.Indestructible: bonus += 120; break;
			}

			if ( Core.AOS )
			{
				bonus += m_AosArmorAttributes.DurabilityBonus;

				CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );
				CraftAttributeInfo attrInfo = null;

				if ( resInfo != null )
					attrInfo = resInfo.AttributeInfo;

				if ( attrInfo != null )
					bonus += attrInfo.ArmorDurability;
			}

			return bonus;
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
				return false;
			}

			CraftSystem system = DefTailoring.CraftSystem;

			CraftItem item = system.CraftItems.SearchFor( GetType() );

			if ( item != null && item.Resources.Count == 1 && item.Resources.GetAt( 0 ).Amount >= 2 )
			{
				try
				{
					Item res = (Item)Activator.CreateInstance( CraftResources.GetInfo( m_Resource ).ResourceTypes[0] );

					ScissorHelper( from, res, m_PlayerConstructed ? (item.Resources.GetAt( 0 ).Amount / 2) : 1 );
					return true;
				}
				catch
				{
				}
			}

			from.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
			return false;
		}

		private static double[] m_ArmorScalars = { 0.07, 0.07, 0.14, 0.15, 0.22, 0.35 };

		public static double[] ArmorScalars
		{
			get
			{
				return m_ArmorScalars;
			}
			set
			{
				m_ArmorScalars = value;
			}
		}

		public static void ValidateMobile( Mobile m )
		{
			for ( int i = m.Items.Count - 1; i >= 0; --i )
			{
				if ( i >= m.Items.Count )
					continue;

				Item item = m.Items[i];

				if ( item is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)item;

					if( armor.RequiredRace != null && m.Race != armor.RequiredRace )
					{
						if( armor.RequiredRace == Race.Elf )
							m.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
						else
							m.SendMessage( "Only {0} may use this.", armor.RequiredRace.PluralName );

						m.AddToBackpack( armor );
					}
					else if ( !armor.AllowMaleWearer && !m.Female && m.AccessLevel < AccessLevel.GameMaster )
					{
						if ( armor.AllowFemaleWearer )
							m.SendLocalizedMessage( 1010388 ); // Only females can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( armor );
					}
					else if ( !armor.AllowFemaleWearer && m.Female && m.AccessLevel < AccessLevel.GameMaster )
					{
						if ( armor.AllowMaleWearer )
							m.SendLocalizedMessage( 1063343 ); // Only males can wear this.
						else
							m.SendMessage( "You may not wear this." );

						m.AddToBackpack( armor );
					}
				}
			}
		}

		public int GetLowerStatReq()
		{
			if ( !Core.AOS )
				return 0;

			int v = m_AosArmorAttributes.LowerStatReq;

			CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

			if ( info != null )
			{
				CraftAttributeInfo attrInfo = info.AttributeInfo;

				if ( attrInfo != null )
					v += attrInfo.ArmorLowerRequirements;
			}

			if ( v > 100 )
				v = 100;

			return v;
		}

		public override void OnAdded(IEntity parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				if ( Core.AOS && !(AdventuresFunctions.IsPuritain((object)parent)) && !(from is PlayerMobile && ((PlayerMobile)from).SoulBound ))
					m_AosSkillBonuses.AddTo( from );

				from.Delta( MobileDelta.Armor ); // Tell them armor rating has changed
			}
		}

		public virtual double ScaleArmorByDurability( double armor )
		{
			int scale = 100;

			if ( m_MaxHitPoints > 0 && m_HitPoints < m_MaxHitPoints )
				scale = 50 + ((50 * m_HitPoints) / m_MaxHitPoints);

			return ( armor * scale ) / 100;
		}

		protected void Invalidate()
		{
			if ( Parent is Mobile )
				((Mobile)Parent).Delta( MobileDelta.Armor ); // Tell them armor rating has changed
		}

		public BaseArmor( Serial serial ) :  base( serial )
		{
		}

		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Attributes			= 0x00000001,
			ArmorAttributes		= 0x00000002,
			PhysicalBonus		= 0x00000004,
			FireBonus			= 0x00000008,
			ColdBonus			= 0x00000010,
			PoisonBonus			= 0x00000020,
			EnergyBonus			= 0x00000040,
			Identified			= 0x00000080,
			MaxHitPoints		= 0x00000100,
			HitPoints			= 0x00000200,
			Crafter				= 0x00000400,
			Quality				= 0x00000800,
			Durability			= 0x00001000,
			Protection			= 0x00002000,
			Resource			= 0x00004000,
			BaseArmor			= 0x00008000,
			StrBonus			= 0x00010000,
			DexBonus			= 0x00020000,
			IntBonus			= 0x00040000,
			StrReq				= 0x00080000,
			DexReq				= 0x00100000,
			IntReq				= 0x00200000,
			MedAllowance		= 0x00400000,
			SkillBonuses		= 0x00800000,
			PlayerConstructed	= 0x01000000
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 8 ); // version added wear
			
			writer.Write( (int) m_wear);

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.Attributes,		!m_AosAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.ArmorAttributes,	!m_AosArmorAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.PhysicalBonus,		m_PhysicalBonus != 0 );
			SetSaveFlag( ref flags, SaveFlag.FireBonus,			m_FireBonus != 0 );
			SetSaveFlag( ref flags, SaveFlag.ColdBonus,			m_ColdBonus != 0 );
			SetSaveFlag( ref flags, SaveFlag.PoisonBonus,		m_PoisonBonus != 0 );
			SetSaveFlag( ref flags, SaveFlag.EnergyBonus,		m_EnergyBonus != 0 );
			SetSaveFlag( ref flags, SaveFlag.Identified,		m_Identified != false );
			SetSaveFlag( ref flags, SaveFlag.MaxHitPoints,		m_MaxHitPoints != 0 );
			SetSaveFlag( ref flags, SaveFlag.HitPoints,			m_HitPoints != 0 );
			SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Quality,			m_Quality != ArmorQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.Durability,		m_Durability != ArmorDurabilityLevel.Regular );
			SetSaveFlag( ref flags, SaveFlag.Protection,		m_Protection != ArmorProtectionLevel.Regular );
			SetSaveFlag( ref flags, SaveFlag.Resource,			m_Resource != DefaultResource );
			SetSaveFlag( ref flags, SaveFlag.BaseArmor,			m_ArmorBase != -1 );
			SetSaveFlag( ref flags, SaveFlag.StrBonus,			m_StrBonus != -1 );
			SetSaveFlag( ref flags, SaveFlag.DexBonus,			m_DexBonus != -1 );
			SetSaveFlag( ref flags, SaveFlag.IntBonus,			m_IntBonus != -1 );
			SetSaveFlag( ref flags, SaveFlag.StrReq,			m_StrReq != -1 );
			SetSaveFlag( ref flags, SaveFlag.DexReq,			m_DexReq != -1 );
			SetSaveFlag( ref flags, SaveFlag.IntReq,			m_IntReq != -1 );
			SetSaveFlag( ref flags, SaveFlag.MedAllowance,		m_Meditate != (AMA)(-1) );
			SetSaveFlag( ref flags, SaveFlag.SkillBonuses,		!m_AosSkillBonuses.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.PlayerConstructed,	m_PlayerConstructed != false );

			writer.WriteEncodedInt( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
				m_AosAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.ArmorAttributes ) )
				m_AosArmorAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.PhysicalBonus ) )
				writer.WriteEncodedInt( (int) m_PhysicalBonus );

			if ( GetSaveFlag( flags, SaveFlag.FireBonus ) )
				writer.WriteEncodedInt( (int) m_FireBonus );

			if ( GetSaveFlag( flags, SaveFlag.ColdBonus ) )
				writer.WriteEncodedInt( (int) m_ColdBonus );

			if ( GetSaveFlag( flags, SaveFlag.PoisonBonus ) )
				writer.WriteEncodedInt( (int) m_PoisonBonus );

			if ( GetSaveFlag( flags, SaveFlag.EnergyBonus ) )
				writer.WriteEncodedInt( (int) m_EnergyBonus );

			if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
				writer.WriteEncodedInt( (int) m_MaxHitPoints );

			if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
				writer.WriteEncodedInt( (int) m_HitPoints );

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				writer.Write( (Mobile) m_Crafter );

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				writer.WriteEncodedInt( (int) m_Quality );

			if ( GetSaveFlag( flags, SaveFlag.Durability ) )
				writer.WriteEncodedInt( (int) m_Durability );

			if ( GetSaveFlag( flags, SaveFlag.Protection ) )
				writer.WriteEncodedInt( (int) m_Protection );

			if ( GetSaveFlag( flags, SaveFlag.Resource ) )
				writer.WriteEncodedInt( (int) m_Resource );

			if ( GetSaveFlag( flags, SaveFlag.BaseArmor ) )
				writer.WriteEncodedInt( (int) m_ArmorBase );

			if ( GetSaveFlag( flags, SaveFlag.StrBonus ) )
				writer.WriteEncodedInt( (int) m_StrBonus );

			if ( GetSaveFlag( flags, SaveFlag.DexBonus ) )
				writer.WriteEncodedInt( (int) m_DexBonus );

			if ( GetSaveFlag( flags, SaveFlag.IntBonus ) )
				writer.WriteEncodedInt( (int) m_IntBonus );

			if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
				writer.WriteEncodedInt( (int) m_StrReq );

			if ( GetSaveFlag( flags, SaveFlag.DexReq ) )
				writer.WriteEncodedInt( (int) m_DexReq );

			if ( GetSaveFlag( flags, SaveFlag.IntReq ) )
				writer.WriteEncodedInt( (int) m_IntReq );

			if ( GetSaveFlag( flags, SaveFlag.MedAllowance ) )
				writer.WriteEncodedInt( (int) m_Meditate );

			if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
				m_AosSkillBonuses.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 8:
				{
					m_wear = reader.ReadInt();
					goto case 7;
				}
				case 7:
				case 6:
				case 5:
				{
					SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Attributes ) )
						m_AosAttributes = new AosAttributes( this, reader );
					else
						m_AosAttributes = new AosAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.ArmorAttributes ) )
						m_AosArmorAttributes = new AosArmorAttributes( this, reader );
					else
						m_AosArmorAttributes = new AosArmorAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.PhysicalBonus ) )
						m_PhysicalBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.FireBonus ) )
						m_FireBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.ColdBonus ) )
						m_ColdBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.PoisonBonus ) )
						m_PoisonBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.EnergyBonus ) )
						m_EnergyBonus = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Identified ) )
						m_Identified = ( version >= 7 || reader.ReadBool() );

					if ( GetSaveFlag( flags, SaveFlag.MaxHitPoints ) )
						m_MaxHitPoints = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.HitPoints ) )
						m_HitPoints = reader.ReadEncodedInt();

					if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
						m_Crafter = reader.ReadMobile();

					if ( GetSaveFlag( flags, SaveFlag.Quality ) )
						m_Quality = (ArmorQuality)reader.ReadEncodedInt();
					else
						m_Quality = ArmorQuality.Regular;

					if ( version == 5 && m_Quality == ArmorQuality.Low )
						m_Quality = ArmorQuality.Regular;

					if ( GetSaveFlag( flags, SaveFlag.Durability ) )
					{
						m_Durability = (ArmorDurabilityLevel)reader.ReadEncodedInt();

						if ( m_Durability > ArmorDurabilityLevel.Indestructible )
							m_Durability = ArmorDurabilityLevel.Durable;
					}

					if ( GetSaveFlag( flags, SaveFlag.Protection ) )
					{
						m_Protection = (ArmorProtectionLevel)reader.ReadEncodedInt();

						if ( m_Protection > ArmorProtectionLevel.Invulnerability )
							m_Protection = ArmorProtectionLevel.Defense;
					}

					if ( GetSaveFlag( flags, SaveFlag.Resource ) )
						m_Resource = (CraftResource)reader.ReadEncodedInt();
					else
						m_Resource = DefaultResource;

					if ( m_Resource == CraftResource.None )
						m_Resource = DefaultResource;

					if ( GetSaveFlag( flags, SaveFlag.BaseArmor ) )
						m_ArmorBase = reader.ReadEncodedInt();
					else
						m_ArmorBase = -1;

					if ( GetSaveFlag( flags, SaveFlag.StrBonus ) )
						m_StrBonus = reader.ReadEncodedInt();
					else
						m_StrBonus = -1;

					if ( GetSaveFlag( flags, SaveFlag.DexBonus ) )
						m_DexBonus = reader.ReadEncodedInt();
					else
						m_DexBonus = -1;

					if ( GetSaveFlag( flags, SaveFlag.IntBonus ) )
						m_IntBonus = reader.ReadEncodedInt();
					else
						m_IntBonus = -1;

					if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
						m_StrReq = reader.ReadEncodedInt();
					else
						m_StrReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.DexReq ) )
						m_DexReq = reader.ReadEncodedInt();
					else
						m_DexReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.IntReq ) )
						m_IntReq = reader.ReadEncodedInt();
					else
						m_IntReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.MedAllowance ) )
						m_Meditate = (AMA)reader.ReadEncodedInt();
					else
						m_Meditate = (AMA)(-1);

					if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
						m_AosSkillBonuses = new AosSkillBonuses( this, reader );

					if ( GetSaveFlag( flags, SaveFlag.PlayerConstructed ) )
						m_PlayerConstructed = true;

					break;
				}
				case 4:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosArmorAttributes = new AosArmorAttributes( this, reader );
					goto case 3;
				}
				case 3:
				{
					m_PhysicalBonus = reader.ReadInt();
					m_FireBonus = reader.ReadInt();
					m_ColdBonus = reader.ReadInt();
					m_PoisonBonus = reader.ReadInt();
					m_EnergyBonus = reader.ReadInt();
					goto case 2;
				}
				case 2:
				case 1:
				{
					m_Identified = reader.ReadBool();
					goto case 0;
				}
				case 0:
				{
					m_ArmorBase = reader.ReadInt();
					m_MaxHitPoints = reader.ReadInt();
					m_HitPoints = reader.ReadInt();
					m_Crafter = reader.ReadMobile();
					m_Quality = (ArmorQuality)reader.ReadInt();
					m_Durability = (ArmorDurabilityLevel)reader.ReadInt();
					m_Protection = (ArmorProtectionLevel)reader.ReadInt();

					AMT mat = (AMT)reader.ReadInt();

					if ( m_ArmorBase == RevertArmorBase )
						m_ArmorBase = -1;

					/*m_BodyPos = (ArmorBodyType)*/reader.ReadInt();

					if ( version < 4 )
					{
						m_AosAttributes = new AosAttributes( this );
						m_AosArmorAttributes = new AosArmorAttributes( this );
					}

					if ( version < 3 && m_Quality == ArmorQuality.Exceptional )
						DistributeBonuses( 6 );

					if ( version >= 2 )
					{
						m_Resource = (CraftResource)reader.ReadInt();
					}
					else
					{
						OreInfo info;

						switch ( reader.ReadInt() )
						{
							default:
							case 0: info = OreInfo.Iron; break;
							case 1: info = OreInfo.DullCopper; break;
							case 2: info = OreInfo.ShadowIron; break;
							case 3: info = OreInfo.Copper; break;
							case 4: info = OreInfo.Bronze; break;
							case 5: info = OreInfo.Gold; break;
							case 6: info = OreInfo.Agapite; break;
							case 7: info = OreInfo.Verite; break;
							case 8: info = OreInfo.Valorite; break;
						}

						m_Resource = CraftResources.GetFromOreInfo( info, mat );
					}

					m_StrBonus = reader.ReadInt();
					m_DexBonus = reader.ReadInt();
					m_IntBonus = reader.ReadInt();
					m_StrReq = reader.ReadInt();
					m_DexReq = reader.ReadInt();
					m_IntReq = reader.ReadInt();

					if ( m_StrBonus == OldStrBonus )
						m_StrBonus = -1;

					if ( m_DexBonus == OldDexBonus )
						m_DexBonus = -1;

					if ( m_IntBonus == OldIntBonus )
						m_IntBonus = -1;

					if ( m_StrReq == OldStrReq )
						m_StrReq = -1;

					if ( m_DexReq == OldDexReq )
						m_DexReq = -1;

					if ( m_IntReq == OldIntReq )
						m_IntReq = -1;

					m_Meditate = (AMA)reader.ReadInt();

					if ( m_Meditate == OldMedAllowance )
						m_Meditate = (AMA)(-1);

					if ( m_Resource == CraftResource.None )
					{
						if ( mat == ArmorMaterialType.Studded || mat == ArmorMaterialType.Leather )
							m_Resource = CraftResource.RegularLeather;
						else if ( mat == ArmorMaterialType.Spined )
							m_Resource = CraftResource.SpinedLeather;
						else if ( mat == ArmorMaterialType.Horned )
							m_Resource = CraftResource.HornedLeather;
						else if ( mat == ArmorMaterialType.Barbed )
							m_Resource = CraftResource.BarbedLeather;
						else if ( mat == ArmorMaterialType.Necrotic )
							m_Resource = CraftResource.NecroticLeather;
						else if ( mat == ArmorMaterialType.Volcanic )
							m_Resource = CraftResource.VolcanicLeather;
						else if ( mat == ArmorMaterialType.Frozen )
							m_Resource = CraftResource.FrozenLeather;
						else if ( mat == ArmorMaterialType.Goliath )
							m_Resource = CraftResource.GoliathLeather;
						else if ( mat == ArmorMaterialType.Draconic )
							m_Resource = CraftResource.DraconicLeather;
						else if ( mat == ArmorMaterialType.Hellish )
							m_Resource = CraftResource.HellishLeather;
						else if ( mat == ArmorMaterialType.Dinosaur )
							m_Resource = CraftResource.DinosaurLeather;
						else if ( mat == ArmorMaterialType.Alien )
							m_Resource = CraftResource.AlienLeather;
						else
							m_Resource = CraftResource.Iron;
					}

					if ( m_MaxHitPoints == 0 && m_HitPoints == 0 )
						m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

					break;
				}
			}

			if ( m_AosSkillBonuses == null )
				m_AosSkillBonuses = new AosSkillBonuses( this );

			if ( Core.AOS && Parent is Mobile && !(AdventuresFunctions.IsPuritain((object)Parent)))
				m_AosSkillBonuses.AddTo( (Mobile)Parent );

			int strBonus = ComputeStatBonus( StatType.Str );
			int dexBonus = ComputeStatBonus( StatType.Dex );
			int intBonus = ComputeStatBonus( StatType.Int );

			if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) && (Parent is PlayerMobile && !((PlayerMobile)Parent).SoulBound) && !(AdventuresFunctions.IsPuritain((object)Parent)))
			{
				Mobile m = (Mobile)Parent;

				string modName = Serial.ToString();

				if ( strBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

				if ( dexBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

				if ( intBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
			}

			if ( Parent is Mobile )
				((Mobile)Parent).CheckStatTimers();

			if ( version < 7 )
				m_PlayerConstructed = true; // we don't know, so, assume it's crafted
		}

		public virtual CraftResource DefaultResource{ get{ return CraftResource.Iron; } }

		public BaseArmor( int itemID ) :  base( itemID )
		{
			m_Quality = ArmorQuality.Regular;
			m_Durability = ArmorDurabilityLevel.Regular;
			m_Crafter = null;

			m_Resource = DefaultResource;
			Hue = CraftResources.GetHue( m_Resource );

			m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			this.Layer = (Layer)ItemData.Quality;

			m_AosAttributes = new AosAttributes( this );
			m_AosArmorAttributes = new AosArmorAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
		}

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{

			return base.AllowSecureTrade( from, to, newOwner, accepted );
		}

		public virtual Race RequiredRace { get { return null; } }

		public override bool CanEquip( Mobile from )
		{

			if( from.AccessLevel < AccessLevel.GameMaster )
			{
				if( RequiredRace != null && from.Race != RequiredRace )
				{
					if( RequiredRace == Race.Elf )
						from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
					else
						from.SendMessage( "Only {0} may use this.", RequiredRace.PluralName );

					return false;
				}
				else if( !AllowMaleWearer && !from.Female )
				{
					if( AllowFemaleWearer )
						from.SendLocalizedMessage( 1010388 ); // Only females can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else if( !AllowFemaleWearer && from.Female )
				{
					if( AllowMaleWearer )
						from.SendLocalizedMessage( 1063343 ); // Only males can wear this.
					else
						from.SendMessage( "You may not wear this." );

					return false;
				}
				else
				{
					int strBonus = ComputeStatBonus( StatType.Str ), strReq = ComputeStatReq( StatType.Str );
					int dexBonus = ComputeStatBonus( StatType.Dex ), dexReq = ComputeStatReq( StatType.Dex );
					int intBonus = ComputeStatBonus( StatType.Int ), intReq = ComputeStatReq( StatType.Int );

					if( from.Dex < dexReq || (from.Dex + dexBonus) < 1 )
					{
						from.SendLocalizedMessage( 502077 ); // You do not have enough dexterity to equip this item.
						return false;
					}
					else if( from.Str < strReq || (from.Str + strBonus) < 1 )
					{
						from.SendLocalizedMessage( 500213 ); // You are not strong enough to equip that.
						return false;
					}
					else if( from.Int < intReq || (from.Int + intBonus) < 1 )
					{
						from.SendMessage( "You are not smart enough to equip that." );
						return false;
					}
				}
			}

			return base.CanEquip( from );
		}

		public override bool CheckPropertyConfliction( Mobile m )
		{
			if ( base.CheckPropertyConfliction( m ) )
				return true;

			if ( Layer == Layer.Pants )
				return ( m.FindItemOnLayer( Layer.InnerLegs ) != null );

			if ( Layer == Layer.Shirt )
				return ( m.FindItemOnLayer( Layer.InnerTorso ) != null );

			return false;
		}

		public override bool OnEquip( Mobile from )
		{
			Server.Misc.MaterialInfo.IsNoHairHat( ItemID, from );

			from.CheckStatTimers();

			bool SB = false;
			if (from is PlayerMobile )
			{
				if (((PlayerMobile)from).SoulBound || AdventuresFunctions.IsPuritain((object)from))
					SB = true;
			}

			bool MD = false;
			if (AdventuresFunctions.IsPuritain((object)from))
				MD = true;

			int strBonus = ComputeStatBonus( StatType.Str );
			int dexBonus = ComputeStatBonus( StatType.Dex );
			int intBonus = ComputeStatBonus( StatType.Int );

			if ( (strBonus != 0 || dexBonus != 0 || intBonus != 0) && !SB && !MD )
			{
				string modName = this.Serial.ToString();

				if ( strBonus != 0 )
					from.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

				if ( dexBonus != 0 )
					from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

				if ( intBonus != 0 )
					from.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
			}

			if ( this.Layer == Layer.Gloves )
			{
				if (	( from.FindItemOnLayer( Layer.OneHanded ) is PugilistGloves ) || 
						( from.FindItemOnLayer( Layer.OneHanded ) is PugilistGlove )  || 
						( from.FindItemOnLayer( Layer.OneHanded ) is ThrowingGloves )  || 
						( from.FindItemOnLayer( Layer.OneHanded ) is LevelPugilistGloves )  || 
						( from.FindItemOnLayer( Layer.OneHanded ) is LevelThrowingGloves )  || 
						( from.FindItemOnLayer( Layer.OneHanded ) is GiftPugilistGloves )  || 
						( from.FindItemOnLayer( Layer.OneHanded ) is GiftThrowingGloves )  || 
						( from.FindItemOnLayer( Layer.OneHanded ) is GlovesOfThePugilist ) )
					{ Item oneHand = from.FindItemOnLayer( Layer.OneHanded ); from.Backpack.DropItem( oneHand ); //BaseContainer.DropItemFix( oneHand, from, from.Backpack.ItemID, from.Backpack.GumpID ); 
					}
				else if ( ( from.FindItemOnLayer( Layer.FirstValid ) is PugilistGloves ) || ( from.FindItemOnLayer( Layer.FirstValid ) is PugilistGlove )  || ( from.FindItemOnLayer( Layer.FirstValid ) is GlovesOfThePugilist ) )
					{ Item firstValid = from.FindItemOnLayer( Layer.FirstValid ); from.Backpack.DropItem( firstValid ); //BaseContainer.DropItemFix( firstValid, from, from.Backpack.ItemID, from.Backpack.GumpID ); 
					}
			}

			if( base.OnEquip( from ) )
			{
				if ( Server.Misc.MaterialInfo.IsCowlHood( this ) )
				{
					from.SendMessage( "You can double click this to change the color." );
				}
			}

			return base.OnEquip( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (from == null && this.Deleted)
				return;
			
			if ( from is PlayerMobile && from.FindItemOnLayer( Layer.Gloves ) == this )
			{
				if ( ((PlayerMobile)from).BalanceEffect != 0 )
				{
					if ( from.CanBeginAction( this ))
					{
						if (from.Karma >= 0)
							from.SendMessage( "Whose Honor will you challenge?" );
						else if (from.Karma <0)
							from.SendMessage( "What pityful human will you humiliate?" );
						
						from.Target = new ChampionChallengeTarget(this); // Call our target	
					}
					else 
						from.SendMessage( "You are still taking off your glove, try again." );
				}
				else
				{
					from.SendMessage( "Only those pledged to the balance may challenge another." );
				}
			}
				
				
			if ( Server.Misc.MaterialInfo.IsCowlHood( this ) )
			{
				if ( from.FindItemOnLayer( Layer.Helm ) != this )
				{
					from.SendMessage( "You must be wearing this to change the color." );
				}
				else
				{
					Target t;

					from.SendMessage( "What worn item do you wish to match the color of?" );
					t = new HatTarget( this );
					from.Target = t;
				}
			}
        }


		private class ChallengeTimer : Timer
		{
			private Mobile m_From;

			public ChallengeTimer( Mobile from ) : base( TimeSpan.FromSeconds( 1.5 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.EndAction( this );
			}
		}


		private class ChampionChallengeTarget : Target
		{
			private Item Gloves;

			public ChampionChallengeTarget( Item gloves ) : base( 1, false, TargetFlags.None )
			{
				Gloves = gloves;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( !(targeted is PlayerMobile) || from == null || targeted == null)
					return;
				
				Mobile champ = from;
				Mobile opponent = (Mobile)targeted;
				
				if ( ((PlayerMobile)from).BalanceStatus != ((PlayerMobile)opponent).BalanceStatus)			
				{
					
					Region reg = Region.Find( opponent.Location, opponent.Map );
					
					if (!(from.InRange(opponent, 7)))
					{
						from.SendMessage("He's too far!");
						return;
					}

					else if (opponent == from)
					{
						from.SendMessage("How ridiculous you feel!  You remind yourself to stop doing that.");
						return;
					}

					else if ( ((PlayerMobile)opponent).BalanceStatus == 0)
					{
						from.SendMessage("This one's not worthy of a challenge.");
						return;						
					}
					
					else if ( (from != AetherGlobe.EvilChamp && from != AetherGlobe.GoodChamp) && ( opponent == AetherGlobe.EvilChamp || opponent == AetherGlobe.GoodChamp )  )
					{
						from.SendMessage("You lack the status to challenge a champion of the balance.");
						return;
					}
					else if ( (from == AetherGlobe.EvilChamp ) && ( opponent != AetherGlobe.GoodChamp )  )
					{
						from.SendMessage("This person is beneath you to challenge.");
						return;
					}
					else if ( (from == AetherGlobe.GoodChamp) && ( opponent != AetherGlobe.EvilChamp  )  )
					{
						from.SendMessage("This person is beneath you to challenge.");
						return;
					}

					else if ( reg.IsPartOf( "SafeRegion" ) )
					{
						from.SendMessage("Not here...");
						return;
					}


					if ( from.BeginAction( this ) )
					{
							new ChallengeTimer( from ).Start();

							from.PlaySound( 0x145 );

							from.Animate( 9, 1, 1, true, false, 0 );
							
							Effects.SendMovingEffect( from, opponent, 0x36E4, 7, 0, false, true, 0x480, 0 ); //change itemid from snowball to glove

							if (Utility.RandomDouble() > 0.25)
							{
								from.SendMessage("You throw a glove at your opponent's face, but miss!");
								opponent.SendMessage("A glove narrowly misses your face!");
								return;
							}
							
							from.SendMessage("Your Glove Connects with his Face!");
							opponent.SendMessage("A glove smacks you in the Face!.");
							
							if (from.Karma >= 0)
								from.SendMessage("You cannot believe such a vile being can walk upright - you dishonor him!");
							else
								from.SendMessage("This one was weak - you completely humiliate him for all to see.");
						
							if ( (AetherGlobe.EvilChamp == champ || AetherGlobe.GoodChamp == champ) && (AetherGlobe.EvilChamp == opponent || AetherGlobe.GoodChamp == opponent))
							{
								if (from == AetherGlobe.EvilChamp )
								{
									AetherGlobe.GoodChamp = null;
									opponent.SendMessage("You have been humiliated by " + from.Name +" !");
									opponent.SendMessage("You are no longer the champion of the Balance.");
								}
								else 
								{
									AetherGlobe.EvilChamp = null;
									opponent.SendMessage("You have been found wanting by " + from.Name +" !");
									opponent.SendMessage("You are no longer the champion of the Balance.");
								}	
								
								int old = ((PlayerMobile)opponent).BalanceEffect;
								((PlayerMobile)opponent).BalanceEffect = (int)((double)(((PlayerMobile)opponent).BalanceEffect)*.75);
								((PlayerMobile)from).BalanceEffect += (int)( (double)(old - ((PlayerMobile)opponent).BalanceEffect) /2 );
							}
								
							else
							{
								if (opponent.Karma >=0)
									opponent.SendMessage("You were humiliated by " + from.Name +" !");
								else
									opponent.SendMessage("You were found wanting by " + from.Name +" !");
								
								((PlayerMobile)from).BalanceEffect += (int)((double)(((PlayerMobile)opponent).BalanceEffect)*.1);
								((PlayerMobile)opponent).BalanceEffect = (int)((double)(((PlayerMobile)opponent).BalanceEffect)*.9);

							}
					}
					else
					{
							from.SendMessage("Your glove snags on a finger, try again.");
					}
				}	
				else if ( ((PlayerMobile)from).BalanceStatus == 0 )
					from.SendMessage("You must pledge to the balance before challenging another.");
				else if ( ((PlayerMobile)from).BalanceStatus == ((PlayerMobile)opponent).BalanceStatus)			
					from.SendMessage("This person fights for the same side as you!");
				
				return;

			}
		}

		private class HatTarget : Target
		{
			private Item m_Hats;

			public HatTarget( Item cowl ) : base( 1, false, TargetFlags.None )
			{
				m_Hats = cowl;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item iColorHat = targeted as Item;

					int color = 0;

					if ( from.FindItemOnLayer( Layer.Helm ) != m_Hats ) { from.SendMessage( "You must be wearing this to change the color." ); }
					else if ( from.FindItemOnLayer( Layer.Waist ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.OuterTorso ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Arms ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.OuterLegs ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Neck ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Gloves ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Shoes ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Cloak ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.FirstValid ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.InnerLegs ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.InnerTorso ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Pants ) == iColorHat ) { color = iColorHat.Hue; }
					else if ( from.FindItemOnLayer( Layer.Shirt ) == iColorHat ) { color = iColorHat.Hue; }
					else
					{
						from.SendMessage( "You can only match colors of certain equipped items." );
					}

					if ( color > 0 )
					{
						m_Hats.Hue = color;
						from.SendMessage( "You change the color to match the item." );
					}
					else
					{
						from.SendMessage( "Items selected must have a distinct color." );
					}
				}
				else
				{
					from.SendMessage( "You can only match color of certain equipped items that have distinct colors." );
				}
			}
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( parent is Mobile )
			{
				Mobile m = (Mobile)parent;

				/*bool SB = false;
				if (m is PlayerMobile )
				{
					if (((PlayerMobile)m).SoulBound)
						SB = true;
				}*/

				string modName = this.Serial.ToString();

				//if (!SB)
				//{
					m.RemoveStatMod( modName + "Str" );
					m.RemoveStatMod( modName + "Dex" );
					m.RemoveStatMod( modName + "Int" );
				//}

				if ( Core.AOS )
					m_AosSkillBonuses.Remove();

				((Mobile)parent).Delta( MobileDelta.Armor ); // Tell them armor rating has changed
				m.CheckStatTimers();
			}

			base.OnRemoved( parent );
		}

		public virtual void AddResists( BaseArmor item, int A, int B, int C, int D, int E)
		{
			if (item == null || !(item is BaseArmor))
				return;

				item.PhysicalBonus = A;
				item.FireBonus = B;
				item.ColdBonus = C;
				item.PoisonBonus = D;
				item.EnergyBonus = E;

		}

		
		public virtual int OnHit( BaseWeapon weapon, int damageTaken )
		{
			double HalfAr = ArmorRating / 2.0;
			int Absorbed = (int)(HalfAr + HalfAr*Utility.RandomDouble());

			damageTaken -= Absorbed;
			if ( damageTaken < 0 ) 
				damageTaken = 0;

			if ( Absorbed < 2 )
				Absorbed = 2;

			int ruin = 75; // 25% chance to lower durability

			if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( (Item)this ) )
				ruin = 50; // Metal armor is just more durable and it gives life back to metal armor types

			if ( ruin < Utility.Random( 100 ) ) // chance to lower durability
			{
				if ( this is ILevelable && !(AdventuresFunctions.IsPuritain((object)this)) )
				{
					LevelItemManager.RepairItems( ((Mobile)Parent) );
				}
				else if ( !AdventuresFunctions.IsPuritain((object)this) && m_AosArmorAttributes.SelfRepair > Utility.Random( 20 ) )
				{
					HitPoints += 1;
				}
				else
				{
					int wear;

					if ( weapon.Type == WeaponType.Bashing )
						wear = Absorbed / 2;
					else
						wear = Utility.Random( 2 );

					if ( wear > 0 && m_MaxHitPoints > 0 )
					{
						if ( m_HitPoints >= wear )
						{
							HitPoints -= wear;
							
							//WearAndTear(Utility.RandomMinMax(0, 1));
							
							wear = 0;
						}
						else
						{
							wear -= HitPoints;
							HitPoints = 0;
						}

						if ( wear > 0 )
						{
							if ( m_MaxHitPoints > wear )
							{
								MaxHitPoints -= wear;
								
								//WearAndTear(Utility.RandomMinMax(1, 3));

								if ( Parent is Mobile )
									((Mobile)Parent).LocalOverheadMessage( MessageType.Regular, 0x3B2, 1061121 ); // Your equipment is severely damaged.
							}
							else
							{
								Delete();
							}
						}
					}
				}
			}

			return damageTaken;
		}
		
		public void WearAndTear( int amount )
		{
			if (Wear > 100 || m_MaxHitPoints == 0)
				return;

			if ( (100 - Wear) < amount )
				amount = 100-Wear;

			double repair = (double)m_HitPoints / (double)m_MaxHitPoints; //0 is damaged, 1 is in good repair, check condition of repair
			repair += 0.50 * ((double)m_HitPoints/255); // bonus for items with high durability e.g. 255 versus 60.  
			
			//at this point a weapon at 90% durability value with 100 hits would equal 1.29, impossible to have wear
			//wep with 20% repair and 50 hits would equal .298
			
			//check if wear actually happens based on the state of repair.
			if ( (Utility.RandomDouble()/2) > repair )

/*
m_Hits
m_MaxHits
		public override int PhysicalResistance{ get{ return m_AosWeaponAttributes.ResistPhysicalBonus; } }
		public override int FireResistance{ get{ return m_AosWeaponAttributes.ResistFireBonus; } }
		public override int ColdResistance{ get{ return m_AosWeaponAttributes.ResistColdBonus; } }
		public override int PoisonResistance{ get{ return m_AosWeaponAttributes.ResistPoisonBonus; } }
		public override int EnergyResistance{ get{ return m_AosWeaponAttributes.ResistEnergyBonus; } }
				public virtual int AosMinDamage{ get{ return 0; } }
		public virtual int AosMaxDamage{ get{ return 0; } }
		public virtual int AosSpeed{ get{ return 0; } }
			private int m_StrReq, m_DexReq, m_IntReq;
		private int m_MinDamage, m_MaxDamage;
				private SlayerName m_Slayer;
		private SlayerName m_Slayer2;
		private WeaponDamageLevel m_DamageLevel;
		private WeaponAccuracyLevel m_AccuracyLevel;
		private WeaponDurabilityLevel m_DurabilityLevel;
		private AosAttributes m_AosAttributes;
		private AosWeaponAttributes m_AosWeaponAttributes;
*/

			Wear += amount;

		}

		private string GetNameString()
		{
			string name = this.Name;

			if ( name == null ) {
			    string Label = MorphingItem.AddSpacesToSentence( (this.GetType()).Name );
			    TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
			    Label = cultInfo.ToTitleCase(Label);
			    return Label;
			}

			return name;
		}

		[Hue, CommandProperty( AccessLevel.GameMaster )]
		public override int Hue
		{
			get{ return base.Hue; }
			set{ base.Hue = value; InvalidateProperties(); }
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
		    #region [Item Name Color]
		    string resourceName = CraftResources.GetName(m_Resource);
		    TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

		    if (string.IsNullOrEmpty(resourceName) || resourceName.ToLower() == "none" || resourceName.ToLower() == "normal" || resourceName.ToLower() == "iron")
				resourceName = "";

		    if (resourceName == "")
				list.Add(1053099, ItemNameHue.UnifiedItemProps.RarityNameMod(this, ((m_Quality == ArmorQuality.Exceptional) ? "Exceptional " : "") + "{0}"), cultInfo.ToTitleCase(GetNameString()));
		    else
				list.Add(1053099, ItemNameHue.UnifiedItemProps.RarityNameMod(this, ((m_Quality == ArmorQuality.Exceptional) ? "Exceptional " : "") + "{0}\t{1}"), resourceName, GetNameString());
		    #endregion
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			if ( base.AllowEquipedCast( from ) )
				return true;

			return ( m_AosAttributes.SpellChanneling != 0 );
		}

		public virtual int GetLuckBonus()
		{
			CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

			if ( resInfo == null )
				return 0;

			CraftAttributeInfo attrInfo = resInfo.AttributeInfo;

			if ( attrInfo == null )
				return 0;

			return attrInfo.ArmorLuck;
		}

		public override void GetProperties( ObjectPropertyList list ) 
		{
			base.GetProperties( list );

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~


			bool md = false;
			if (AdventuresFunctions.IsPuritain((object)this))
				md = true;

			#region [Item Name Color]
			//int value = ItemNameHue.ArmorItemProps.CheckArmor(this);
			#endregion
			
			if( RequiredRace == Race.Elf )
				list.Add( 1075086 ); // Elves Only

			m_AosSkillBonuses.GetProperties( list );

			int prop; // ++ adjust for midland

			if ( (prop = ArtifactRarity) > 0 )
				list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

			if ( (prop = m_AosAttributes.WeaponDamage) != 0 && !md)
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 && !md)
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 && !md)
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 && !md)
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 && !md)
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 && !md)
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = m_AosAttributes.AttackChance) != 0 && !md)
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusHits) != 0 && !md)
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 && !md)
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 && !md)
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = GetLowerStatReq()) != 0 )
				list.Add( 1060435, prop.ToString() ); // lower requirements ~1_val~%

			if ( (prop = (GetLuckBonus() + m_AosAttributes.Luck)) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosArmorAttributes.MageArmor) != 0 )
				list.Add( 1060437 ); // mage armor

			if ( (prop = m_AosAttributes.BonusMana) != 0 && !md)
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 && !md)
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 && !md)
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 && !md)
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 && !md)
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosArmorAttributes.SelfRepair) != 0 && !md)
				list.Add( 1060450, prop.ToString() ); // self repair ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 && !md)
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 && !md)
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 && !md)
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 && !md)
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			base.AddResistanceProperties( list );

			if ( (prop = GetDurabilityBonus()) > 0 )
				list.Add( 1060410, prop.ToString() ); // durability ~1_val~%

			if ( (prop = ComputeStatReq( StatType.Str )) > 0 )
				list.Add( 1061170, prop.ToString() ); // strength requirement ~1_val~

			if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
				list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~

			list.Add("this item is considered armor");
		}

		public override void OnSingleClick( Mobile from )
		{
			List<EquipInfoAttribute> attrs = new List<EquipInfoAttribute>();

			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			if ( m_Quality == ArmorQuality.Exceptional )
				attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );

			if ( m_Identified || from.AccessLevel >= AccessLevel.GameMaster)
			{
				if ( m_Durability != ArmorDurabilityLevel.Regular )
					attrs.Add( new EquipInfoAttribute( 1038000 + (int)m_Durability ) );

				if ( m_Protection > ArmorProtectionLevel.Regular && m_Protection <= ArmorProtectionLevel.Invulnerability )
					attrs.Add( new EquipInfoAttribute( 1038005 + (int)m_Protection ) );
			}
			else if ( m_Durability != ArmorDurabilityLevel.Regular || (m_Protection > ArmorProtectionLevel.Regular && m_Protection <= ArmorProtectionLevel.Invulnerability) )
				attrs.Add( new EquipInfoAttribute( 1038000 ) ); // Unidentified

			int number;

			if ( Name == null )
			{
				number = LabelNumber;
			}
			else
			{
				this.LabelTo( from, Name );
				number = 1041000;
			}

			if ( attrs.Count == 0 && Crafter == null && Name != null )
				return;

			EquipmentInfo eqInfo = new EquipmentInfo( number, m_Crafter, false, attrs.ToArray() );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ArmorQuality)quality;

			if ( makersMark )
				Crafter = from;

			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );
			PlayerConstructed = true;

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			if( Quality == ArmorQuality.Exceptional )
			{
				if ( !( Core.ML && this is BaseShield ))		// Guessed Core.ML removed exceptional resist bonuses from crafted shields
					DistributeBonuses( (tool is BaseRunicTool ? 8 : Core.SE ? 5 : 5) ); // Not sure since when, but right now 15 points are added, not 14.

				if( Core.ML /* && !(this is BaseShield) */ )
				{
					int bonus = (int)(from.Skills.ArmsLore.Value / 15);

					for( int i = 0; i < bonus; i++ )
					{
						switch( Utility.Random( 5 ) )
						{
							case 0: m_PhysicalBonus++;	break;
							case 1: m_FireBonus++;		break;
							case 2: m_ColdBonus++;		break;
							case 3: m_EnergyBonus++;	break;
							case 4: m_PoisonBonus++;	break;
						}
					}

					from.CheckSkill( SkillName.ArmsLore, 0, 100 );
				}
			}

			if ( Core.AOS && tool is BaseRunicTool )
				((BaseRunicTool)tool).ApplyAttributesTo( this );

			return quality;
		}

		#endregion
	}
}
