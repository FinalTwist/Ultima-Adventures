using System;
using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Bushido;
using Server.Spells.Ninjitsu;
using Server.Engines.Craft;
using System.Collections.Generic;
using Server.Misc;

namespace Server.Items
{
	public interface ISlayer
	{
		SlayerName Slayer { get; set; }
		SlayerName Slayer2 { get; set; }
	}

	public abstract class BaseWeapon : Item, IWeapon, ICraftable, ISlayer, IDurability
	{
		private string m_EngravedText;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string EngravedText
		{
			get{ return m_EngravedText; }
			set{ m_EngravedText = value; InvalidateProperties(); }
		}

		public static int WeaponMaterialDamage( CraftResource m_Resource )
		{
			int dmg = 0;

			switch ( m_Resource )
			{
				case CraftResource.DullCopper:		dmg = 1; break;
				case CraftResource.ShadowIron:		dmg = 2; break;
				case CraftResource.Copper:			dmg = 3; break;
				case CraftResource.Bronze:			dmg = 4; break;
				case CraftResource.Gold:			dmg = 4; break;
				case CraftResource.Agapite:			dmg = 5; break;
				case CraftResource.Verite:			dmg = 5; break;
				case CraftResource.Valorite:		dmg = 6; break;
				case CraftResource.Nepturite:		dmg = 6; break;
				case CraftResource.Obsidian:		dmg = 6; break;
				case CraftResource.Steel:			dmg = 7; break;
				case CraftResource.Brass:			dmg = 8; break;
				case CraftResource.Mithril:			dmg = 9; break;
				case CraftResource.Xormite:			dmg = 9; break;
				case CraftResource.Dwarven:			dmg = 18; break;
				case CraftResource.SpinedLeather:	dmg = 1; break;
				case CraftResource.HornedLeather:	dmg = 2; break;
				case CraftResource.BarbedLeather:	dmg = 3; break;
				case CraftResource.NecroticLeather:	dmg = 4; break;
				case CraftResource.VolcanicLeather:	dmg = 4; break;
				case CraftResource.FrozenLeather:	dmg = 5; break;
				case CraftResource.GoliathLeather:	dmg = 6; break;
				case CraftResource.DraconicLeather:	dmg = 8; break;
				case CraftResource.HellishLeather:	dmg = 9; break;
				case CraftResource.DinosaurLeather:	dmg = 10; break;
				case CraftResource.AlienLeather:	dmg = 18; break;
				case CraftResource.AshTree: 		dmg = 1; break;
				case CraftResource.CherryTree: 		dmg = 1; break;
				case CraftResource.EbonyTree: 		dmg = 2; break;
				case CraftResource.GoldenOakTree: 	dmg = 2; break;
				case CraftResource.HickoryTree: 	dmg = 3; break;
				case CraftResource.MahoganyTree: 	dmg = 3; break;
				case CraftResource.OakTree: 		dmg = 4; break;
				case CraftResource.PineTree: 		dmg = 4; break;
				case CraftResource.RosewoodTree: 	dmg = 5; break;
				case CraftResource.WalnutTree: 		dmg = 5; break;
				case CraftResource.DriftwoodTree: 	dmg = 7; break;
				case CraftResource.GhostTree: 		dmg = 8; break;
				case CraftResource.PetrifiedTree: 	dmg = 9; break;
				case CraftResource.ElvenTree: 		dmg = 18; break;
			}

			return dmg;
		}



		/* Weapon internals work differently now (Mar 13 2003)
		 * 
		 * The attributes defined below default to -1.
		 * If the value is -1, the corresponding virtual 'Aos/Old' property is used.
		 * If not, the attribute value itself is used. Here's the list:
		 *  - MinDamage
		 *  - MaxDamage
		 *  - Speed
		 *  - HitSound
		 *  - MissSound
		 *  - StrRequirement, DexRequirement, IntRequirement
		 *  - WeaponType
		 *  - WeaponAnimation
		 *  - MaxRange
		 */

		#region Var declarations

		// Instance values. These values are unique to each weapon.
		private WeaponDamageLevel m_DamageLevel;
		private WeaponAccuracyLevel m_AccuracyLevel;
		private WeaponDurabilityLevel m_DurabilityLevel;
		private WeaponQuality m_Quality;
		private Mobile m_Crafter;
		private Poison m_Poison;
		private int m_PoisonCharges;
		private bool m_Identified;
		private int m_Hits;
		private int m_MaxHits;
		private SlayerName m_Slayer;
		private SlayerName m_Slayer2;
		private SkillMod m_SkillMod, m_MageMod;
		private CraftResource m_Resource;
		private bool m_PlayerConstructed;

		private bool m_Cursed; // Is this weapon cursed via Curse Weapon necromancer spell? Temporary; not serialized.
		private bool m_Consecrated; // Is this weapon blessed via Consecrate Weapon paladin ability? Temporary; not serialized.

		private AosAttributes m_AosAttributes;
		private AosWeaponAttributes m_AosWeaponAttributes;
		private AosSkillBonuses m_AosSkillBonuses;
		private AosElementAttributes m_AosElementDamages;

		// Overridable values. These values are provided to override the defaults which get defined in the individual weapon scripts.
		private int m_StrReq, m_DexReq, m_IntReq;
		private int m_MinDamage, m_MaxDamage;
		private int m_HitSound, m_MissSound;
		private float m_Speed;
		private int m_MaxRange;
		private SkillName m_Skill;
		private WeaponType m_Type;
		private WeaponAnimation m_Animation;
		#endregion

		#region Virtual Properties
		public virtual WeaponAbility PrimaryAbility{ get{ return null; } }
		public virtual WeaponAbility SecondaryAbility{ get{ return null; } }
		public virtual WeaponAbility ThirdAbility{ get{ return null; } }
		public virtual WeaponAbility FourthAbility{ get{ return null; } }
		public virtual WeaponAbility FifthAbility{ get{ return null; } }

		public virtual int DefMaxRange{ get{ return 1; } }
		public virtual int DefHitSound{ get{ return 0; } }
		public virtual int DefMissSound{ get{ return 0; } }
		public virtual SkillName DefSkill{ get{ return SkillName.Swords; } }
		public virtual WeaponType DefType{ get{ return WeaponType.Slashing; } }
		public virtual WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		public virtual int AosStrengthReq{ get{ return 0; } }
		public virtual int AosDexterityReq{ get{ return 0; } }
		public virtual int AosIntelligenceReq{ get{ return 0; } }
		public virtual int AosMinDamage{ get{ return 0; } }
		public virtual int AosMaxDamage{ get{ return 0; } }
		public virtual int AosSpeed{ get{ return 0; } }
		public virtual float MlSpeed{ get{ return 0.0f; } }
		public virtual int AosMaxRange{ get{ return DefMaxRange; } }
		public virtual int AosHitSound{ get{ return DefHitSound; } }
		public virtual int AosMissSound{ get{ return DefMissSound; } }
		public virtual SkillName AosSkill{ get{ return DefSkill; } }
		public virtual WeaponType AosType{ get{ return DefType; } }
		public virtual WeaponAnimation AosAnimation{ get{ return DefAnimation; } }

		public virtual int OldStrengthReq{ get{ return 0; } }
		public virtual int OldDexterityReq{ get{ return 0; } }
		public virtual int OldIntelligenceReq{ get{ return 0; } }
		public virtual int OldMinDamage{ get{ return 0; } }
		public virtual int OldMaxDamage{ get{ return 0; } }
		public virtual int OldSpeed{ get{ return 0; } }
		public virtual int OldMaxRange{ get{ return DefMaxRange; } }
		public virtual int OldHitSound{ get{ return DefHitSound; } }
		public virtual int OldMissSound{ get{ return DefMissSound; } }
		public virtual SkillName OldSkill{ get{ return DefSkill; } }
		public virtual WeaponType OldType{ get{ return DefType; } }
		public virtual WeaponAnimation OldAnimation{ get{ return DefAnimation; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		public virtual bool CanFortify{ get{ return true; } }

		public override int PhysicalResistance{ get{ return m_AosWeaponAttributes.ResistPhysicalBonus; } }
		public override int FireResistance{ get{ return m_AosWeaponAttributes.ResistFireBonus; } }
		public override int ColdResistance{ get{ return m_AosWeaponAttributes.ResistColdBonus; } }
		public override int PoisonResistance{ get{ return m_AosWeaponAttributes.ResistPoisonBonus; } }
		public override int EnergyResistance{ get{ return m_AosWeaponAttributes.ResistEnergyBonus; } }

		public virtual SkillName AccuracySkill { get { return SkillName.Tactics; } }

		#endregion

		#region Getters & Setters
		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosWeaponAttributes WeaponAttributes
		{
			get{ return m_AosWeaponAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes AosElementDamages
		{
			get { return m_AosElementDamages; }
			set { }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Cursed
		{
			get{ return m_Cursed; }
			set{ m_Cursed = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Consecrated
		{
			get{ return m_Consecrated; }
			set{ m_Consecrated = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Identified
		{
			get{ return m_Identified; }
			set{ m_Identified = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitPoints
		{
			get{ return m_Hits; }
			set
			{
				if ( m_Hits == value )
					return;

				if ( value > m_MaxHits )
					value = m_MaxHits;

				m_Hits = value;

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxHitPoints
		{
			get{ return m_MaxHits; }
			set{ m_MaxHits = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int PoisonCharges
		{
			get{ return m_PoisonCharges; }
			set{ m_PoisonCharges = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get{ return m_Poison; }
			set{ m_Poison = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WeaponQuality Quality
		{
			get{ return m_Quality; }
			set{ UnscaleDurability(); m_Quality = value; ScaleDurability(); InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer
		{
			get{ return m_Slayer; }
			set{ m_Slayer = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer2
		{
			get { return m_Slayer2; }
			set { m_Slayer2 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ UnscaleDurability(); m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); InvalidateProperties(); ScaleDurability(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WeaponDamageLevel DamageLevel
		{
			get{ return m_DamageLevel; }
			set{ m_DamageLevel = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WeaponDurabilityLevel DurabilityLevel
		{
			get{ return m_DurabilityLevel; }
			set{ UnscaleDurability(); m_DurabilityLevel = value; InvalidateProperties(); ScaleDurability(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PlayerConstructed
		{
			get{ return m_PlayerConstructed; }
			set{ m_PlayerConstructed = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxRange
		{
			get{ return ( m_MaxRange == -1 ? Core.AOS ? AosMaxRange : OldMaxRange : m_MaxRange ); }
			set{ m_MaxRange = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WeaponAnimation Animation
		{
			get{ return ( m_Animation == (WeaponAnimation)(-1) ? Core.AOS ? AosAnimation : OldAnimation : m_Animation ); } 
			set{ m_Animation = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WeaponType Type
		{
			get{ return ( m_Type == (WeaponType)(-1) ? Core.AOS ? AosType : OldType : m_Type ); }
			set{ m_Type = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SkillName Skill
		{
			get{ return ( m_Skill == (SkillName)(-1) ? Core.AOS ? AosSkill : OldSkill : m_Skill ); }
			set{ m_Skill = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int HitSound
		{
			get{ return ( m_HitSound == -1 ? Core.AOS ? AosHitSound : OldHitSound : m_HitSound ); }
			set{ m_HitSound = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MissSound
		{
			get{ return ( m_MissSound == -1 ? Core.AOS ? AosMissSound : OldMissSound : m_MissSound ); }
			set{ m_MissSound = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MinDamage
		{
			get{ return ( m_MinDamage == -1 ? Core.AOS ? ( AosMinDamage + WeaponMaterialDamage( m_Resource ) ) : OldMinDamage : m_MinDamage ); }
			set{ m_MinDamage = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxDamage
		{
			get{ return ( m_MaxDamage == -1 ? Core.AOS ? ( AosMaxDamage + WeaponMaterialDamage( m_Resource ) ) : OldMaxDamage : m_MaxDamage ); }
			set{ m_MaxDamage = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public float Speed
		{
			get
			{
				if ( m_Speed != -1 )
					return m_Speed;

				if ( Core.ML )
					return MlSpeed;
				else if ( Core.AOS )
					return AosSpeed;

				return OldSpeed;
			}
			set{ m_Speed = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int StrRequirement
		{
			get{ return ( m_StrReq == -1 ? Core.AOS ? AosStrengthReq : OldStrengthReq : m_StrReq ); }
			set{ m_StrReq = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DexRequirement
		{
			get{ return ( m_DexReq == -1 ? Core.AOS ? AosDexterityReq : OldDexterityReq : m_DexReq ); }
			set{ m_DexReq = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int IntRequirement
		{
			get{ return ( m_IntReq == -1 ? Core.AOS ? AosIntelligenceReq : OldIntelligenceReq : m_IntReq ); }
			set{ m_IntReq = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WeaponAccuracyLevel AccuracyLevel
		{
			get
			{
				return m_AccuracyLevel;
			}
			set
			{
				if ( m_AccuracyLevel != value )
				{
					m_AccuracyLevel = value;

					if ( UseSkillMod )
					{
						if ( m_AccuracyLevel == WeaponAccuracyLevel.Regular )
						{
							if ( m_SkillMod != null )
								m_SkillMod.Remove();

							m_SkillMod = null;
						}
						else if ( m_SkillMod == null && Parent is Mobile )
						{
							m_SkillMod = new DefaultSkillMod( AccuracySkill, true, (int)m_AccuracyLevel * 5 );
							((Mobile)Parent).AddSkillMod( m_SkillMod );
						}
						else if ( m_SkillMod != null )
						{
							m_SkillMod.Value = (int)m_AccuracyLevel * 5;
						}
					}

					InvalidateProperties();
				}
			}
		}

		private int m_wear;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Wear
		{
			get{ return m_wear;}
			set{m_wear = value;}
		}

		#endregion

		public override void OnAfterDuped( Item newItem )
		{
			BaseWeapon weap = newItem as BaseWeapon;

			if ( weap == null )
				return;

			weap.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			weap.m_AosElementDamages = new AosElementAttributes( newItem, m_AosElementDamages );
			weap.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
			weap.m_AosWeaponAttributes = new AosWeaponAttributes( newItem, m_AosWeaponAttributes );
		}

		public virtual void UnscaleDurability()
		{
			int scale = 50 + GetDurabilityBonus();

			m_Hits = ((m_Hits * 100) + (scale - 1)) / scale;
			m_MaxHits = ((m_MaxHits * 100) + (scale - 1)) / scale;
			InvalidateProperties();
		}

		public virtual void ScaleDurability()
		{
			int scale = 50 + GetDurabilityBonus();

			m_Hits = ((m_Hits * scale) + 99) / 100;
			m_MaxHits = ((m_MaxHits * scale) + 99) / 100;
			InvalidateProperties();
		}

		public int GetDurabilityBonus()
		{
			int bonus = 0;

			if ( m_Quality == WeaponQuality.Exceptional )
				bonus += 20;

			switch ( m_DurabilityLevel )
			{
				case WeaponDurabilityLevel.Durable: bonus += 20; break;
				case WeaponDurabilityLevel.Substantial: bonus += 50; break;
				case WeaponDurabilityLevel.Massive: bonus += 70; break;
				case WeaponDurabilityLevel.Fortified: bonus += 100; break;
				case WeaponDurabilityLevel.Indestructible: bonus += 120; break;
			}

			if ( Core.AOS )
			{
				bonus += m_AosWeaponAttributes.DurabilityBonus;

				CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );
				CraftAttributeInfo attrInfo = null;

				if ( resInfo != null )
					attrInfo = resInfo.AttributeInfo;

				if ( attrInfo != null )
					bonus += attrInfo.WeaponDurability;
			}

			return bonus;
		}

		public int GetLowerStatReq()
		{
			if ( !Core.AOS )
				return 0;

			int v = m_AosWeaponAttributes.LowerStatReq;

			CraftResourceInfo info = CraftResources.GetInfo( m_Resource );

			if ( info != null )
			{
				CraftAttributeInfo attrInfo = info.AttributeInfo;

				if ( attrInfo != null )
					v += attrInfo.WeaponLowerRequirements;
			}

			if ( v > 100 )
				v = 100;

			return v;
		}

		public static void BlockEquip( Mobile m, TimeSpan duration )
		{
			if ( m.BeginAction( typeof( BaseWeapon ) ) )
				new ResetEquipTimer( m, duration ).Start();
		}

		private class ResetEquipTimer : Timer
		{
			private Mobile m_Mobile;

			public ResetEquipTimer( Mobile m, TimeSpan duration ) : base( duration )
			{
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				m_Mobile.EndAction( typeof( BaseWeapon ) );
			}
		}

		public override bool CheckConflictingLayer( Mobile m, Item item, Layer layer )
		{
			if ( base.CheckConflictingLayer( m, item, layer ) )
				return true;

			if ( this.Layer == Layer.TwoHanded && layer == Layer.OneHanded )
			{
				m.SendLocalizedMessage( 500214 ); // You already have something in both hands.
				return true;
			}
			else if ( this.Layer == Layer.OneHanded && layer == Layer.TwoHanded && !(item is BaseShield) && !(Server.Misc.MaterialInfo.IsMagicLight(item)) )
			{
				m.SendLocalizedMessage( 500215 ); // You can only wield one weapon at a time.
				return true;
			}

			return false;
		}

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{

			return base.AllowSecureTrade( from, to, newOwner, accepted );
		}

		public virtual Race RequiredRace { get { return null; } }	//On OSI, there are no weapons with race requirements, this is for custom stuff

		public override bool CanEquip( Mobile from )
		{

			if( RequiredRace != null && from.Race != RequiredRace )
			{
				if( RequiredRace == Race.Elf )
					from.SendLocalizedMessage( 1072203 ); // Only Elves may use this.
				else
					from.SendMessage( "Only {0} may use this.", RequiredRace.PluralName );

				return false;
			}
			else if ( from.Dex < DexRequirement )
			{
				from.SendMessage( "You are not nimble enough to equip that." );
				return false;
			} 
			else if ( from.Str < AOS.Scale( StrRequirement, 100 - GetLowerStatReq() ) )
			{
				from.SendLocalizedMessage( 500213 ); // You are not strong enough to equip that.
				return false;
			}
			else if ( from.Int < IntRequirement )
			{
				from.SendMessage( "You are not smart enough to equip that." );
				return false;
			}
			else if ( !from.CanBeginAction( typeof( BaseWeapon ) ) )
			{
				return false;
			}
			else
			{
				return base.CanEquip( from );
			}
		}

		public virtual bool UseSkillMod{ get{ return !Core.AOS; } }

		public override bool OnEquip( Mobile from )
		{

			bool SB = false;
			if (from is PlayerMobile )
			{
				if (((PlayerMobile)from).SoulBound || AdventuresFunctions.IsPuritain((object)from))
					SB = true;
			}

			int strBonus = m_AosAttributes.BonusStr;
			int dexBonus = m_AosAttributes.BonusDex;
			int intBonus = m_AosAttributes.BonusInt;

			if ( (strBonus != 0 || dexBonus != 0 || intBonus != 0) && !SB && !AdventuresFunctions.IsPuritain((object)from))
			{
				Mobile m = from;

				string modName = this.Serial.ToString();

				if ( strBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

				if ( dexBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

				if ( intBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
			}

			from.NextCombatTime = Core.TickCount + (int)GetDelay(from).TotalMilliseconds;

			if ( UseSkillMod && m_AccuracyLevel != WeaponAccuracyLevel.Regular && !SB && !AdventuresFunctions.IsPuritain((object)from) )
			{
				if ( m_SkillMod != null )
					m_SkillMod.Remove();

				m_SkillMod = new DefaultSkillMod( AccuracySkill, true, (int)m_AccuracyLevel * 5 );
				from.AddSkillMod( m_SkillMod );
			}

			if ( Core.AOS && m_AosWeaponAttributes.MageWeapon != 0 && m_AosWeaponAttributes.MageWeapon != 30 && !SB && !AdventuresFunctions.IsPuritain((object)from))
			{
				if ( m_MageMod != null )
					m_MageMod.Remove();

				m_MageMod = new DefaultSkillMod( SkillName.Magery, true, -30 + m_AosWeaponAttributes.MageWeapon );
				from.AddSkillMod( m_MageMod );
			}

			if (!SB && !AdventuresFunctions.IsPuritain((object)from))
				CustomWeaponAbilities.Check(this,from);

			return true;
		}

		public override void OnAdded(IEntity parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				bool SB = false;
				if (from is PlayerMobile )
				{
					if (((PlayerMobile)from).SoulBound || AdventuresFunctions.IsPuritain((object)from))
						SB = true;
				}

				if ( Core.AOS && !SB && !AdventuresFunctions.IsPuritain((object)from))
					m_AosSkillBonuses.AddTo( from );

				from.CheckStatTimers();
				from.Delta( MobileDelta.WeaponDamage );
			}
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( parent is Mobile )
			{
				Mobile m = (Mobile)parent;
				BaseWeapon weapon = m.Weapon as BaseWeapon;

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

				if ( weapon != null )
					m.NextCombatTime = Core.TickCount + (int)weapon.GetDelay(m).TotalMilliseconds;

				if ( UseSkillMod && m_SkillMod != null )
				{
					m_SkillMod.Remove();
					m_SkillMod = null;
				}

				if ( m_MageMod != null )
				{
					m_MageMod.Remove();
					m_MageMod = null;
				}

				if ( Core.AOS )
					m_AosSkillBonuses.Remove();

				m.CheckStatTimers();

				m.Delta( MobileDelta.WeaponDamage );

				CustomWeaponAbilities.Check(m);
			}
		}

		public virtual SkillName GetUsedSkill( Mobile m, bool checkSkillAttrs )
		{
			SkillName sk;

			if ( checkSkillAttrs && m_AosWeaponAttributes.UseBestSkill != 0 )
			{
				double swrd = m.Skills[SkillName.Swords].Value;
				double fenc = m.Skills[SkillName.Fencing].Value;
				double mcng = m.Skills[SkillName.Macing].Value;
				double val;

				sk = SkillName.Swords;
				val = swrd;

				if ( fenc > val ){ sk = SkillName.Fencing; val = fenc; }
				if ( mcng > val ){ sk = SkillName.Macing; val = mcng; }
			}
			else if ( m_AosWeaponAttributes.MageWeapon != 0 )
			{
				if ( m.Skills[SkillName.Magery].Value > m.Skills[Skill].Value )
					sk = SkillName.Magery;
				else
					sk = Skill;
			}
			else
			{
				sk = Skill;

				if ( sk != SkillName.Wrestling && !m.Player && !m.Body.IsHuman && m.Skills[SkillName.Wrestling].Value > m.Skills[sk].Value )
					sk = SkillName.Wrestling;
			}

			return sk;
		}

		public virtual double GetAttackSkillValue( Mobile attacker, Mobile defender )
		{
			return attacker.Skills[GetUsedSkill( attacker, true )].Value;
		}

		public virtual double GetDefendSkillValue( Mobile attacker, Mobile defender )
		{
			return defender.Skills[GetUsedSkill( defender, true )].Value;
		}

		private static bool CheckAnimal( Mobile m, Type type )
		{
			return AnimalForm.UnderTransformation( m, type );
		}

		public virtual bool CheckHit( Mobile attacker, Mobile defender )
		{
			BaseWeapon atkWeapon = attacker.Weapon as BaseWeapon;
			BaseWeapon defWeapon = defender.Weapon as BaseWeapon;

			Skill atkSkill = attacker.Skills[atkWeapon.Skill];
			Skill defSkill = defender.Skills[defWeapon.Skill];

			double atkValue = atkWeapon.GetAttackSkillValue( attacker, defender );
			double defValue = defWeapon.GetDefendSkillValue( attacker, defender );

			double ourValue, theirValue;

			int bonus = GetHitChanceBonus();
			if ( AdventuresFunctions.IsPuritain((object)attacker))
			{
				bonus = 0;
			}

			if ( Core.AOS )
			{
				if ( atkValue <= -20.0 )
					atkValue = -19.9;

				if ( defValue <= -20.0 )
					defValue = -19.9;

				if ( !AdventuresFunctions.IsPuritain((object)attacker))
					bonus += AosAttributes.GetValue( attacker, AosAttribute.AttackChance );

				if ( AdventuresFunctions.IsPuritain((object)attacker) && attacker is PlayerMobile && ( ((PlayerMobile)attacker).Mounted && !(((PlayerMobile)attacker).Mount is EtherealMount ) ) )
					bonus += 15;			
				
				if ( AdventuresFunctions.IsPuritain((object)attacker) && attacker is PlayerMobile)
					bonus += (int)(15 * ((PlayerMobile)attacker).Agility());

				if ( AdventuresFunctions.IsPuritain((object)attacker) && attacker is BaseCreature)
					bonus += (int)(15 * ((BaseCreature)attacker).Agility());

				if ( Spells.Chivalry.DivineFurySpell.UnderEffect( attacker ) )
					bonus += 10; // attacker gets 10% bonus when they're under divine fury

				if ( CheckAnimal( attacker, typeof( GreyWolf ) ) || CheckAnimal( attacker, typeof( MysticalFox ) ) )
					bonus += 25; // attacker gets 20% bonus when under Wolf or Bake Kitsune form

				if ( HitLower.IsUnderAttackEffect( attacker ) )
					bonus -= 25; // Under Hit Lower Attack effect -> 25% malus

				WeaponAbility ability = WeaponAbility.GetCurrentAbility( attacker );

				if ( ability != null )
					bonus += ability.AccuracyBonus;

				SpecialMove move = SpecialMove.GetCurrentMove( attacker );

				if ( move != null )
					bonus += move.GetAccuracyBonus( attacker );

				// Max Hit Chance Increase = 45%
				if ( bonus > MyServerSettings.HitChanceCap())
					bonus = MyServerSettings.HitChanceCap();

				ourValue = (atkValue + 20.0) * (100 + bonus);

				if ( !AdventuresFunctions.IsPuritain((object)defender))
					bonus = AosAttributes.GetValue( defender, AosAttribute.DefendChance );

				if ( AdventuresFunctions.IsPuritain((object)defender) && defender is PlayerMobile)
				{
					if (!((PlayerMobile)defender).Mounted)
						bonus += (int)(15 * ((PlayerMobile)defender).Agility());
				}

				if ( AdventuresFunctions.IsPuritain((object)defender) && defender is BaseCreature)
				{
					if (!((BaseCreature)defender).Mounted)
						bonus += (int)(15 * ((BaseCreature)defender).Agility());
				}

				if ( Spells.Chivalry.DivineFurySpell.UnderEffect( defender ) )
					bonus -= 20; // defender loses 20% bonus when they're under divine fury

				if ( HitLower.IsUnderDefenseEffect( defender ) )
					bonus -= 25; // Under Hit Lower Defense effect -> 25% malus
					
				int blockBonus = 0;

				if ( Block.GetBonus( defender, ref blockBonus ) )
					bonus += blockBonus;

				int surpriseMalus = 0;

				if ( SurpriseAttack.GetMalus( defender, ref surpriseMalus ) )
					bonus -= surpriseMalus;

				int discordanceEffect = 0;

				// Defender loses -0/-28% if under the effect of Discordance.
				if ( SkillHandlers.Discordance.GetEffect( attacker, ref discordanceEffect ) )
					bonus -= discordanceEffect;

				// Defense Chance Increase = 45%
				if ( bonus > MyServerSettings.DefendChanceCap() )
					bonus = MyServerSettings.DefendChanceCap();

				theirValue = (defValue + 20.0) * (100 + bonus);

				bonus = 0;
			}
			else
			{
				if ( atkValue <= -50.0 )
					atkValue = -49.9;

				if ( defValue <= -50.0 )
					defValue = -49.9;

				ourValue = (atkValue + 50.0);
				theirValue = (defValue + 50.0);
			}

			double chance = ourValue / (theirValue * 2.0);

			chance *= 1.0 + ((double)bonus / 100);

			if ( Core.AOS && chance < 0.02 )
				chance = 0.02;

			return attacker.CheckSkill( atkSkill.SkillName, chance );
		}

		public virtual TimeSpan GetDelay( Mobile m )
		{
			double speed = this.Speed;

			if ( speed == 0 )
				return TimeSpan.FromHours( 1.0 );

			double delayInSeconds;

			if ( Core.SE )
			{
				/*
				 * This is likely true for Core.AOS as well... both guides report the same
				 * formula, and both are wrong.
				 * The old formula left in for AOS for legacy & because we aren't quite 100%
				 * Sure that AOS has THIS formula
				 */
				int weaponSpeedCap = MyServerSettings.WeaponSpeedCap();
				int bonus = AosAttributes.GetValue( m, AosAttribute.WeaponSpeed );

				if ( (m is PlayerMobile && ((PlayerMobile)m).SoulBound) || AdventuresFunctions.IsPuritain((object)m))
					bonus = 0;

				if (bonus > weaponSpeedCap) {
					bonus = weaponSpeedCap;
				}

				if ( Spells.Chivalry.DivineFurySpell.UnderEffect( m ) )
					bonus += 10;

				// Bonus granted by successful use of Honorable Execution.
				bonus += HonorableExecution.GetSwingBonus( m );

				if( DualWield.Registry.Contains( m ) )
					bonus += ((DualWield.DualWieldTimer)DualWield.Registry[m]).BonusSwingSpeed;

				if( Feint.Registry.Contains( m ) )
					bonus -= ((Feint.FeintTimer)Feint.Registry[m]).SwingSpeedReduction;

				TransformContext context = TransformationSpellHelper.GetContext( m );

				int discordanceEffect = 0;

				// Discordance gives a malus of -0/-28% to swing speed.
				if ( SkillHandlers.Discordance.GetEffect( m, ref discordanceEffect ) )
					bonus -= discordanceEffect;

				if (AdventuresFunctions.IsPuritain((object)m) && m is PlayerMobile)
				{
					//double differential = speed - ((speed/2)*((PlayerMobile)m).Agility());
					//differential /= 1;
					//speed -= differential;
					bonus += (int)(50 * ((PlayerMobile)m).Agility());
				}

				if ( bonus > 60 )
					bonus = 60;
				
				double ticks;

				if ( Core.ML )
				{
					int stamTicks = m.Stam / 30;

					ticks = speed * 4;
					ticks = Math.Floor( ( ticks - stamTicks ) * ( 100.0 / ( 100 + bonus ) ) );
				}
				else
				{
					speed = Math.Floor( speed * ( bonus + 100.0 ) / 100.0 );

					if ( speed <= 0 )
						speed = 1;

					ticks = Math.Floor( ( 80000.0 / ( ( m.Stam + 100 ) * speed ) ) - 2 );
				}		
				// Swing speed currently capped at one swing every 1.25 seconds (5 ticks).
				if ( ticks < 5 )
					ticks = 5;

				delayInSeconds = ticks * 0.25;
			}
			else if ( Core.AOS )
			{
				int v = (m.Stam + 100) * (int) speed;

				int weaponSpeedCap = MyServerSettings.WeaponSpeedCap();
				int bonus = AosAttributes.GetValue( m, AosAttribute.WeaponSpeed );

				if (bonus > weaponSpeedCap) {
					bonus = weaponSpeedCap;
				}

				if ( Spells.Chivalry.DivineFurySpell.UnderEffect( m ) )
					bonus += 10;

				int discordanceEffect = 0;

				// Discordance gives a malus of -0/-28% to swing speed.
				if ( SkillHandlers.Discordance.GetEffect( m, ref discordanceEffect ) )
					bonus -= discordanceEffect;

				v += AOS.Scale( v, bonus );

				if ( v <= 0 )
					v = 1;

				delayInSeconds = Math.Floor( 40000.0 / v ) * 0.5;
				double minimumSwingDelay = MyServerSettings.MinimumSwingDelaySeconds();
				// Maximum swing rate capped at one swing per second 
				// OSI dev said that it has and is supposed to be 1.25
				if ( delayInSeconds < minimumSwingDelay )
					delayInSeconds = minimumSwingDelay;
			}
			else
			{
				int v = (m.Stam + 100) * (int) speed;

				if ( v <= 0 )
					v = 1;

				delayInSeconds = 15000.0 / v;
			}

			return TimeSpan.FromSeconds( delayInSeconds );
		}

		public virtual void OnBeforeSwing( Mobile attacker, Mobile defender )
		{
			WeaponAbility a = WeaponAbility.GetCurrentAbility( attacker );

			if( a != null && !a.OnBeforeSwing( attacker, defender ) )
				WeaponAbility.ClearCurrentAbility( attacker );

			SpecialMove move = SpecialMove.GetCurrentMove( attacker );

			if( move != null && !move.OnBeforeSwing( attacker, defender ) )
				SpecialMove.ClearCurrentMove( attacker );

			if( attacker.Hidden && attacker is PlayerMobile && attacker.Skills[SkillName.Hiding].Value > Utility.RandomMinMax( 1, 125 ) )
			{
				PlayerMobile pm = (PlayerMobile)attacker;

				pm.Stealthing = 1;

				if ( attacker.Skills[SkillName.Stealth].Value > Utility.RandomMinMax( 1, 125 ) )
				{
					pm.Stealthing = 2;
				}
			}
		}

		public virtual TimeSpan OnSwing( Mobile attacker, Mobile defender )
		{
			return OnSwing( attacker, defender, 1.0 );
		}

		public virtual TimeSpan OnSwing( Mobile attacker, Mobile defender, double damageBonus )
		{
			bool canSwing = ( !attacker.Paralyzed && !attacker.Frozen );

			if ( canSwing )
			{
				Spell sp = attacker.Spell as Spell;

				canSwing = ( sp == null || !sp.IsCasting || !sp.BlocksMovement );
			}

			if ( canSwing )
			{
				PlayerMobile p = attacker as PlayerMobile;

				canSwing = ( p == null || p.PeacedUntil <= DateTime.UtcNow );
			}

			if (AdventuresFunctions.IsPuritain((object)attacker) && attacker is PlayerMobile)
			{
				if (Utility.RandomDouble() < (0.35 * (((PlayerMobile)attacker).Encumbrance()) + 0.45 * (1- ((PlayerMobile)attacker).Agility())))
					((PlayerMobile)attacker).Stam -= Utility.RandomMinMax(1, 3);
			}

			if ( canSwing && attacker.HarmfulCheck( defender ) )
			{
				attacker.DisruptiveAction();

				if ( attacker.NetState != null )
					attacker.Send( new Swing( 0, attacker, defender ) );

				if ( attacker is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)attacker;
					WeaponAbility ab = bc.GetWeaponAbility();

					if ( ab != null )
					{
						if ( bc.WeaponAbilityChance > Utility.RandomDouble() )
							WeaponAbility.SetCurrentAbility( bc, ab );
						else
							WeaponAbility.ClearCurrentAbility( bc );
					}
				}

				if ( CheckHit( attacker, defender ) )
					OnHit( attacker, defender, damageBonus );
				else
					OnMiss( attacker, defender );
			}

			return GetDelay( attacker );
		}

		#region Sounds
		public virtual int GetHitAttackSound( Mobile attacker, Mobile defender )
		{
			int sound = attacker.GetAttackSound();

			if ( sound == -1 )
				sound = HitSound;

			return sound;
		}

		public virtual int GetHitDefendSound( Mobile attacker, Mobile defender )
		{
			return defender.GetHurtSound();
		}

		public virtual int GetMissAttackSound( Mobile attacker, Mobile defender )
		{
			if ( attacker.GetAttackSound() == -1 )
				return MissSound;
			else
				return -1;
		}

		public virtual int GetMissDefendSound( Mobile attacker, Mobile defender )
		{
			return -1;
		}
		#endregion

		public static bool CheckParry( Mobile defender )
		{
			if ( defender == null )
				return false;

			BaseShield shield = defender.FindItemOnLayer( Layer.TwoHanded ) as BaseShield;

			double parry = defender.Skills[SkillName.Parry].Value;
			double bushidoNonRacial = defender.Skills[SkillName.Bushido].NonRacialValue;
			double bushido = defender.Skills[SkillName.Bushido].Value;

			if ( shield != null )
			{
				double chance = (parry - bushidoNonRacial) / 400.0;	// As per OSI, no negitive effect from the Racial stuffs, ie, 120 parry and '0' bushido with humans

				if ( chance < 0 ) // chance shouldn't go below 0
					chance = 0;				

				// Parry/Bushido over 100 grants a 5% bonus.
				if ( parry >= 100.0 || bushido >= 100.0)
					chance += 0.05;

				// Evasion grants a variable bonus post ML. 50% prior.
				if ( Evasion.IsEvading( defender ) )
					chance *= Evasion.GetParryScalar( defender );

				// Low dexterity lowers the chance.
				if ( defender.Dex < 80 )
					chance = chance * (20 + defender.Dex) / 100;

				if (AdventuresFunctions.IsPuritain((object)defender) && defender is PlayerMobile)
					chance *= ((PlayerMobile)defender).Agility();

				return defender.CheckSkill( SkillName.Parry, chance );
			}
			else if ( !(defender.Weapon is Fists) && !(defender.Weapon is BaseRanged) )
			{
				BaseWeapon weapon = defender.Weapon as BaseWeapon;

				double divisor = (weapon.Layer == Layer.OneHanded) ? 48000.0 : 41140.0;

				double chance = (parry * bushido) / divisor;

				double aosChance = parry / 800.0;

				// Parry or Bushido over 100 grant a 5% bonus.
				if( parry >= 100.0 )
				{
					chance += 0.05;
					aosChance += 0.05;
				}
				else if( bushido >= 100.0 )
				{
					chance += 0.05;
				}

				// Evasion grants a variable bonus post ML. 50% prior.
				if( Evasion.IsEvading( defender ) )
					chance *= Evasion.GetParryScalar( defender );

				// Low dexterity lowers the chance.
				if( defender.Dex < 80 )
					chance = chance * (20 + defender.Dex) / 100;

				if (AdventuresFunctions.IsPuritain((object)defender) && defender is PlayerMobile)
					chance *= ((PlayerMobile)defender).Agility();

				if ( chance > aosChance )
					return defender.CheckSkill( SkillName.Parry, chance );
				else
					return (aosChance > Utility.RandomDouble()); // Only skillcheck if wielding a shield & there's no effect from Bushido
			}

			return false;
		}

		public static bool CheckDodge( Mobile defender, Mobile attacker)
		{
			if ( defender == null || ( !(defender is PlayerMobile || defender is BaseCreature) || !(attacker is PlayerMobile || attacker is BaseCreature) )) // only basecreatures and playermobiles can do this
				return false;

			double chance = 0;
			double defag = 0;
			double attag = 0;

			if (defender is PlayerMobile)
				defag = ((PlayerMobile)defender).Agility();
			if (defender is BaseCreature)
				defag = ((BaseCreature)defender).Agility();	
				
			chance = defag/1.25; // max 0.8%

			if (attacker is BaseCreature)
				attag = ((BaseCreature)attacker).Agility();
			if (attacker is PlayerMobile)
				attag = ((PlayerMobile)attacker).Agility();

				// Evasion grants a variable bonus post ML. 50% prior.
				if( Evasion.IsEvading( defender ) )
					chance *= (Evasion.GetParryScalar( defender )/2);

				// Low stam lowers the chance.
				if( defender.Dex < 50 )
					chance = chance * (20 + defender.Dex) / 100;

			if ( (defender is BaseCreature && ((BaseCreature)defender).midrace == 3) || (defender is PlayerMobile && ((PlayerMobile)defender).midrace == 3))
				chance += 0.1;

			//Compare enemyagility.  Attacker with higher agility can reduce chance of defender dodge
			chance += (defag - attag)/4; // MAX 0.25 bonus if opponenet is NOT agile

			if (defender is PlayerMobile && ((PlayerMobile)defender).Mounted)
				chance /= 2;

			if ( chance > Utility.RandomDouble() )
				return true;

			return false;
		}

		public virtual int AbsorbDamageAOS( Mobile attacker, Mobile defender, int damage )
		{
			bool blocked = false;

			if ( defender.Player || defender.Body.IsHuman )
			{
				blocked = CheckParry( defender );

				if ( blocked )
				{
					defender.FixedEffect( 0x37B9, 10, 16 );
					damage = 0;

					// Successful block removes the Honorable Execution penalty.
					HonorableExecution.RemovePenalty( defender );

					if ( CounterAttack.IsCountering( defender ) )
					{
						BaseWeapon weapon = defender.Weapon as BaseWeapon;

						if ( weapon != null )
						{
							defender.FixedParticles(0x3779, 1, 15, 0x158B, 0x0, 0x3, EffectLayer.Waist);
							weapon.OnSwing( defender, attacker );
						}

						CounterAttack.StopCountering( defender );
					}

					if ( Confidence.IsConfident( defender ) )
					{
						defender.SendLocalizedMessage( 1063117 ); // Your confidence reassures you as you successfully block your opponent's blow.

						double bushido = defender.Skills.Bushido.Value;

						defender.Hits += Utility.RandomMinMax( 1, (int)(bushido / 12) );
						defender.Stam += Utility.RandomMinMax( 1, (int)(bushido / 5) );
					}

					BaseShield shield = defender.FindItemOnLayer( Layer.TwoHanded ) as BaseShield;

					if ( shield != null )
					{
						shield.OnHit( this, damage ); // call OnHit to lose durability
						LevelItemManager.RepairItems( defender );
					}

					if (AdventuresFunctions.IsPuritain((object)defender) && defender.Player && CheckDodge(defender, attacker) && Utility.RandomBool()) //riposte!
					{
						defender.SendMessage("You parry the blow and quickly counter the attack!");
						OnSwing(defender, attacker);
					}


				}

			}

			if ( !blocked )
			{
				double positionChance = Utility.RandomDouble();

				Item hitItem = null;
				//determine which item is hit - also adding a hit order... Weps>Outerclothes>armor>innerclothes>jewelry
			
				int tries = 10;
				while ( hitItem == null && tries > 0)
				{
					//18% odds its a wep that gets hit
					if( positionChance < 0.06 && defender.FindItemOnLayer( Layer.OneHanded ) != null && defender.FindItemOnLayer( Layer.OneHanded ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.OneHanded ));
					else if( positionChance < 0.12 && defender.FindItemOnLayer( Layer.TwoHanded ) != null && defender.FindItemOnLayer( Layer.TwoHanded ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.TwoHanded ));
					else if( positionChance < 0.18 && defender.FindItemOnLayer( Layer.FirstValid ) != null && defender.FindItemOnLayer( Layer.FirstValid ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.FirstValid ));

					//64% odds its a armor/outer clothes that gets hit
					else if( positionChance < 0.26 && defender.FindItemOnLayer( Layer.OuterTorso ) != null && defender.FindItemOnLayer( Layer.OuterTorso ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.OuterTorso ));
					else if( positionChance < 0.33 && defender.FindItemOnLayer( Layer.OuterLegs ) != null && defender.FindItemOnLayer( Layer.OuterLegs ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.OuterLegs ));
					else if( positionChance < 0.40 && defender.FindItemOnLayer( Layer.Waist ) != null && defender.FindItemOnLayer( Layer.Waist ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Waist ));
					else if( positionChance < 0.47 && defender.FindItemOnLayer( Layer.Helm ) != null && defender.FindItemOnLayer( Layer.Helm ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Helm ));
					else if( positionChance < 0.54 && defender.FindItemOnLayer( Layer.Arms ) != null && defender.FindItemOnLayer( Layer.Arms ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Arms ));
					else if( positionChance < 0.61 && defender.FindItemOnLayer( Layer.Neck ) != null && defender.FindItemOnLayer( Layer.Neck ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Neck ));
					else if( positionChance < 0.68 && defender.FindItemOnLayer( Layer.Gloves ) != null && defender.FindItemOnLayer( Layer.Gloves ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Gloves ));
					else if( positionChance < 0.75 && defender.FindItemOnLayer( Layer.Shoes ) != null && defender.FindItemOnLayer( Layer.Shoes ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Shoes ));
					else if( positionChance < 0.82 && defender.FindItemOnLayer( Layer.Cloak ) != null && defender.FindItemOnLayer( Layer.Cloak ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Cloak ));

					//15% odds its inner clothes that gets hit
					else if( positionChance < 0.84 && defender.FindItemOnLayer( Layer.InnerLegs ) != null && defender.FindItemOnLayer( Layer.InnerLegs ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.InnerLegs ));
					else if( positionChance < 0.86 && defender.FindItemOnLayer( Layer.InnerTorso ) != null && defender.FindItemOnLayer( Layer.InnerTorso ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.InnerTorso ));
					else if( positionChance < 0.88 && defender.FindItemOnLayer( Layer.Pants ) != null && defender.FindItemOnLayer( Layer.Pants ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Pants ));
					else if( positionChance < 0.90 && defender.FindItemOnLayer( Layer.Shirt ) != null && defender.FindItemOnLayer( Layer.Shirt ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Shirt ));
					else if( positionChance < 0.92 && defender.FindItemOnLayer( Layer.InnerTorso ) != null && defender.FindItemOnLayer( Layer.InnerTorso ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.InnerTorso ));
					else if( positionChance < 0.94 && defender.FindItemOnLayer( Layer.InnerTorso ) != null && defender.FindItemOnLayer( Layer.InnerTorso ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.InnerTorso ));
					else if( positionChance < 0.96 && defender.FindItemOnLayer( Layer.InnerTorso ) != null && defender.FindItemOnLayer( Layer.InnerTorso ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.InnerTorso ));

					//4% its jewelry 
					else if( positionChance < 0.98 && defender.FindItemOnLayer( Layer.Bracelet ) != null && defender.FindItemOnLayer( Layer.Bracelet ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Bracelet ));
					else if( positionChance < 0.99 && defender.FindItemOnLayer( Layer.Ring ) != null && defender.FindItemOnLayer( Layer.Ring ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Ring ));
					else if( positionChance <= 1.0 && defender.FindItemOnLayer( Layer.Earrings ) != null && defender.FindItemOnLayer( Layer.Earrings ) is Item )
						hitItem = (Item)(defender.FindItemOnLayer( Layer.Earrings ));
						
					//sanity check
					if (!(hitItem is IWearableDurability) && !(hitItem is BaseJewel) && !(hitItem is IDurability) ) //can't reduce durability
						hitItem = null;
						
					tries --;
				}

				if (hitItem is IWearableDurability) //baseclothing or basearmor
				{
					IWearableDurability armor = hitItem as IWearableDurability;

					if ( armor != null )
					{
						armor.OnHit( this, damage ); // call OnHit to lose durability
						LevelItemManager.RepairItems( defender );
					}
				}
				else if (hitItem is BaseJewel) // jewels
				{
					BaseJewel armor = hitItem as BaseJewel;

					if ( armor != null )
					{
						armor.OnHit( this, damage ); // call OnHit to lose durability
						LevelItemManager.RepairItems( defender );
					}
				}
			}

			return damage;
		}

		public static void DamageItems(Mobile from)
		{
			if ( from.AccessLevel > AccessLevel.Player )
				return;

			if ( from.FindItemOnLayer( Layer.OuterTorso ) != null ) { DamageItem(from.FindItemOnLayer( Layer.OuterTorso ), from ); }
			if ( from.FindItemOnLayer( Layer.OneHanded ) != null ) { DamageItem(from.FindItemOnLayer( Layer.OneHanded ), from );}
			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null ) { DamageItem(from.FindItemOnLayer( Layer.TwoHanded ), from );}
			if ( from.FindItemOnLayer( Layer.Bracelet ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Bracelet ), from ); }
			if ( from.FindItemOnLayer( Layer.Ring ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Ring ), from ); }
			if ( from.FindItemOnLayer( Layer.Helm ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Helm ), from ); }
			if ( from.FindItemOnLayer( Layer.Arms ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Arms ), from );}
			if ( from.FindItemOnLayer( Layer.OuterLegs ) != null ) { DamageItem(from.FindItemOnLayer( Layer.OuterLegs ), from ); }
			if ( from.FindItemOnLayer( Layer.Neck ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Neck ), from ); }
			if ( from.FindItemOnLayer( Layer.Gloves ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Gloves ), from ); }
			if ( from.FindItemOnLayer( Layer.Talisman ) != null && !(from.FindItemOnLayer( Layer.Talisman ) is Spellbook) && !(from.FindItemOnLayer( Layer.Talisman ) is BaseInstrument)) { DamageItem(from.FindItemOnLayer( Layer.Talisman ), from ); } 
			if ( from.FindItemOnLayer( Layer.Shoes ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Shoes ), from ); }
			if ( from.FindItemOnLayer( Layer.Cloak ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Cloak ), from );}
			if ( from.FindItemOnLayer( Layer.FirstValid ) != null ) { DamageItem(from.FindItemOnLayer( Layer.FirstValid ), from ); }
			if ( from.FindItemOnLayer( Layer.Waist ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Waist ), from );}
			if ( from.FindItemOnLayer( Layer.InnerLegs ) != null ) { DamageItem(from.FindItemOnLayer( Layer.InnerLegs ), from );}
			if ( from.FindItemOnLayer( Layer.InnerTorso ) != null ) { DamageItem(from.FindItemOnLayer( Layer.InnerTorso ), from ); }
			if ( from.FindItemOnLayer( Layer.Pants ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Pants ), from );}
			if ( from.FindItemOnLayer( Layer.Shirt ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Shirt ), from ); }
			if ( from.FindItemOnLayer( Layer.Earrings ) != null ) { DamageItem(from.FindItemOnLayer( Layer.Earrings ), from ); }
		}

		public static void DamageItem(Item thing, Mobile from)
		{
			DamageItem(thing, from, 1);
		}
		
		public static void DamageItem(Item thing, Mobile from, int max)
		{
			int val = Utility.RandomMinMax(1, max);
			
			//arms lore determines 50% chance of damage other 50% is rng
			if (Utility.RandomBool() && Utility.RandomDouble() > ( (0.50 * (from.Skills[SkillName.ArmsLore].Base / 120)) + ((double)Utility.RandomMinMax(1, 50)/100)) )
			{
				if (thing is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)thing;
					if (armor.HitPoints >= val)
						armor.HitPoints -= val;
					else if (armor.MaxHitPoints > val)
						armor.MaxHitPoints -= val;
					else 
						armor.Delete();
				}
				else if (thing is BaseJewel )
				{
					BaseJewel armor = (BaseJewel)thing;
					if (armor.HitPoints >= val)
						armor.HitPoints -= val;
					else if (armor.MaxHitPoints > val)
						armor.MaxHitPoints -= val;
					else 
						armor.Delete();
				}
				else if (thing is BaseWeapon )
				{
					BaseWeapon armor = (BaseWeapon)thing;
					if (armor.HitPoints >= val)
						armor.HitPoints -= val;
					else if (armor.MaxHitPoints > val)
						armor.MaxHitPoints -= val;
					else 
						armor.Delete();
				}
				else if (thing is BaseClothing )
				{
					BaseClothing armor = (BaseClothing)thing;
					if (armor.HitPoints >= val)
						armor.HitPoints -= val;
					else if (armor.MaxHitPoints > val)
						armor.MaxHitPoints -= val;
					else 
						armor.Delete();
				}	
			}
		}

		public virtual int AbsorbDamage( Mobile attacker, Mobile defender, int damage )
		{
			return AbsorbDamageAOS( attacker, defender, damage );
		}

		public virtual int GetPackInstinctBonus( Mobile attacker, Mobile defender )
		{
			if ( attacker.Player || defender.Player )
				return 0;

			BaseCreature bc = attacker as BaseCreature;

			if ( bc == null || bc.PackInstinct == PackInstinct.None || (!bc.Controlled && !bc.Summoned) )
				return 0;

			Mobile master = bc.ControlMaster;

			if ( master == null )
				master = bc.SummonMaster;

			if ( master == null )
				return 0;

			int inPack = 1;

			foreach ( Mobile m in defender.GetMobilesInRange( 1 ) )
			{
				if ( m != attacker && m is BaseCreature )
				{
					BaseCreature tc = (BaseCreature)m;

					if ( (tc.PackInstinct & bc.PackInstinct) == 0 || (!tc.Controlled && !tc.Summoned) )
						continue;

					Mobile theirMaster = tc.ControlMaster;

					if ( theirMaster == null )
						theirMaster = tc.SummonMaster;

					if ( master == theirMaster && tc.Combatant == defender )
						++inPack;
				}
			}

			if ( inPack >= 5 )
				return 100;
			else if ( inPack >= 4 )
				return 75;
			else if ( inPack >= 3 )
				return 50;
			else if ( inPack >= 2 )
				return 25;

			return 0;
		}

		private static bool m_InDoubleStrike;

		public static bool InDoubleStrike
		{
			get{ return m_InDoubleStrike; }
			set{ m_InDoubleStrike = value; }
		}

		public void OnHit( Mobile attacker, Mobile defender )
		{
			OnHit( attacker, defender, 1.0 );
		}

		public virtual void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			bool dodged = false;

			if ( (defender is PlayerMobile || defender is BaseCreature) && AdventuresFunctions.IsPuritain((object)defender))
				dodged = CheckDodge(defender, attacker);

			double sneakBonus = 0.0;
			if( attacker is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)attacker;

				if ( pm.Stealthing > 0 )
				{
					double sneakAttack = attacker.Skills[SkillName.Hiding].Value;

					if ( pm.Stealthing > 1 )
					{
						sneakAttack = sneakAttack + attacker.Skills[SkillName.Stealth].Value;
					}
					
					double bonusrange = Utility.RandomDouble();
					if (bonusrange < 0.50)
						bonusrange += 0.40;
					if (bonusrange > 0.90)
						bonusrange -= 0.10;
					
                    			sneakBonus = ( (0.015 * sneakAttack) / 1.50) * bonusrange;
					if ( sneakBonus > 1.25 ){ sneakBonus = 1.25; }
					if ( this is BaseRanged ){ sneakBonus = (double)(sneakBonus/2); }

					int tellBonus = (int)(sneakBonus * 100);
					attacker.SendMessage( "You perform a sneak attack for " + tellBonus + "% more damage!" );
					pm.Stealthing = 0;
				}
			}

			if ( MirrorImage.HasClone( defender ) && (defender.Skills.Ninjitsu.Value / 150.0) > Utility.RandomDouble() )
			{
				Clone bc;

				foreach ( Mobile m in defender.GetMobilesInRange( 4 ) )
				{
					bc = m as Clone;

					if ( bc != null && bc.Summoned && bc.SummonMaster == defender )
					{
						attacker.SendLocalizedMessage( 1063141 ); // Your attack has been diverted to a nearby mirror image of your target!
						defender.SendLocalizedMessage( 1063140 ); // You manage to divert the attack onto one of your nearby mirror images.
						defender = m;
						break;
					}
				}
			}

			PlaySwingAnimation( attacker );

			if (!dodged)
			{
				PlayHurtAnimation( defender );
				attacker.PlaySound( GetHitAttackSound( attacker, defender ) );
				defender.PlaySound( GetHitDefendSound( attacker, defender ) );

				if ( AdventuresFunctions.IsPuritain((object)defender) && (( defender is BaseCreature && ((BaseCreature)defender).Mounted  && !(((BaseCreature)defender).Mount is EtherealMount) ) || (defender is PlayerMobile && ((PlayerMobile)defender).Mounted && !(((PlayerMobile)defender).Mount is EtherealMount)) ) ) // hit deflects on mount
				{

					IMount mount = null;
					if (defender is BaseCreature)
						mount = ((BaseCreature)defender).Mount;
					else if (defender is PlayerMobile)
						mount = ((PlayerMobile)defender).Mount;
					
						if ( mount != null && mount is BaseCreature && CheckHit( attacker, (Mobile)mount ))
						{
							if ( ( attacker is BaseCreature && ( (((BaseCreature)attacker).Agility()/2) * Utility.RandomDouble() ) > ( Utility.RandomDouble() * ((BaseCreature)mount).Agility() ) ) || 
								( attacker is PlayerMobile && ( (((PlayerMobile)attacker).Agility()/2) * Utility.RandomDouble() ) > ( Utility.RandomDouble() * ((BaseCreature)mount).Agility() ) ) )
							{
								OnHit( attacker, (Mobile)mount, (Utility.RandomMinMax(1, 5)/10) );
								if (defender is PlayerMobile)
									defender.SendMessage("The hit misses you, but deflects on your mount!");
							}
						}
				}
			}


				if (dodged)
				{
					if (attacker is PlayerMobile)
						attacker.SendMessage ("Your opponent swiftly dodges your attack!");
					if (defender is PlayerMobile)
						defender.SendMessage ("You step aside in one swift move and dodge the attack!");

					if ( (defender is BaseCreature && attacker is BaseCreature) && Utility.RandomDouble() < ((BaseCreature)attacker).Agility() && Utility.RandomBool())
					{
					}
					else if ( (defender is BaseCreature && attacker is PlayerMobile) && Utility.RandomDouble() < ((PlayerMobile)attacker).Agility() && Utility.RandomBool())
					{
						attacker.SendMessage ("but you deftly adjust your swing and hit the opponent!");
					}
					else if ( (defender is PlayerMobile && attacker is BaseCreature) && Utility.RandomDouble() < ((BaseCreature)attacker).Agility() && Utility.RandomBool())
					{
						defender.SendMessage ("But your opponent deftly adjusts and scores a hit!");
					}	
					else if ( (defender is PlayerMobile && attacker is PlayerMobile) && Utility.RandomDouble() < ((PlayerMobile)attacker).Agility() && Utility.RandomBool())
					{
						defender.SendMessage ("But your opponent deftly adjusts and scores a hit!");
						attacker.SendMessage ("but you deftly adjust your swing and hit the opponent!");
					}							
					else 
						return;
				}

			int damage = ComputeDamage( attacker, defender );

			Mobile atcker = attacker;
			
			if ( attacker is BaseCreature && ((BaseCreature)attacker).GetMaster() is PlayerMobile)
				atcker = ((BaseCreature)attacker).GetMaster();

			if ( atcker is PlayerMobile && ((PlayerMobile)atcker).Avatar ) // new effect - balance affects players damage
			{
				double bal = 0;
				double adjust = 0;
				PlayerMobile pm = (PlayerMobile)atcker;
				
				if (AetherGlobe.BalanceLevel > 51000) //balance is evil
				{
					bal = ( (AetherGlobe.BalanceLevel - 50000) / 50000) * 0.17;
					adjust = bal * (double)damage;
					
					if (pm.BalanceStatus > 0 || (pm.BalanceStatus == 0 && pm.Karma > 0) ) // good players
						damage -= (int)adjust;
					if (pm.BalanceStatus < 0 || (pm.BalanceStatus == 0 && pm.Karma > 0) ) // evil
						damage += (int)adjust;
				}
				else if (AetherGlobe.BalanceLevel < 49000) //balance is evil
				{
					bal = ( ( 50000 - AetherGlobe.BalanceLevel ) / 50000) * 0.17;
					adjust = bal * damage;
					
					if (pm.BalanceStatus > 0 || (pm.BalanceStatus == 0 && pm.Karma > 0) ) // good players
						damage += (int)adjust;
					if (pm.BalanceStatus < 0 || (pm.BalanceStatus == 0 && pm.Karma > 0) ) // evil
						damage -= (int)adjust;
				}
	
			}

			#region Damage Multipliers
			/*
			 * The following damage bonuses multiply damage by a factor.
			 * Capped at x3 (300%).
			 */
			//double factor = 1.0;
			int percentageBonus = 0;

			WeaponAbility a = WeaponAbility.GetCurrentAbility( attacker );
			SpecialMove move = SpecialMove.GetCurrentMove( attacker );

			if( a != null )
			{
				//factor *= a.DamageScalar;
				percentageBonus += (int)(a.DamageScalar * 100) - 100;
			}

			if( move != null )
			{
				//factor *= move.GetDamageScalar( attacker, defender );
				percentageBonus += (int)(move.GetDamageScalar( attacker, defender ) * 100) - 100;
			}

			//factor *= damageBonus;
			percentageBonus += (int)(damageBonus * 100) - 100;

			if (AdventuresFunctions.IsPuritain((object)attacker) && ( ( attacker is PlayerMobile && ((PlayerMobile)attacker).Mounted) || (attacker is BaseCreature && ((BaseCreature)attacker).Mounted)) )
				percentageBonus += 15;

			CheckSlayerResult cs = CheckSlayers( attacker, defender );

			if ( cs != CheckSlayerResult.None )
			{
				if ( cs == CheckSlayerResult.Slayer )
					defender.FixedEffect( 0x37B9, 10, 5 );

				//factor *= 2.0;
				percentageBonus += 75;

				if ( Utility.Random( 5 ) == 1 && attacker is PlayerMobile )
				{
					attacker.SendMessage( "This weapon seems to be doing quite well against this enemy." );
				}
			}

			if ( !attacker.Player )
			{
				if ( defender is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)defender;

					if( pm.EnemyOfOneType != null && pm.EnemyOfOneType != attacker.GetType() )
					{
						//factor *= 2.0;
						percentageBonus += 50;
					}
				}
			}
			else if ( !defender.Player )
			{
				if ( attacker is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)attacker;

					if ( pm.WaitingForEnemy )
					{
						pm.EnemyOfOneType = defender.GetType();
						pm.WaitingForEnemy = false;
					}

					if ( pm.EnemyOfOneType == defender.GetType() )
					{
						defender.FixedEffect( 0x37B9, 10, 5, 1160, 0 );
						//factor *= 1.5;
						percentageBonus += 50;
					}
				}
			}

			int packInstinctBonus = GetPackInstinctBonus( attacker, defender );

			if( packInstinctBonus != 0 )
			{
				//factor *= 1.0 + (double)packInstinctBonus / 100.0;
				percentageBonus += packInstinctBonus;
			}

			if( m_InDoubleStrike )
			{
				//factor *= 0.9; // 10% loss when attacking with double-strike
				percentageBonus -= 10;
			}

			TransformContext context = TransformationSpellHelper.GetContext( defender );

			if( (m_Slayer == SlayerName.Silver || m_Slayer2 == SlayerName.Silver) && context != null && context.Spell is NecromancerSpell && context.Type != typeof( HorrificBeastSpell ) )
			{
				//factor *= 1.25; // Every necromancer transformation other than horrific beast takes an additional 25% damage
				percentageBonus += 25;
			}

			if ( attacker is PlayerMobile && !(Core.ML && defender is PlayerMobile ))
			{
				PlayerMobile pmAttacker = (PlayerMobile) attacker;

				if( pmAttacker.HonorActive && pmAttacker.InRange( defender, 1 ) )
				{
					//factor *= 1.25;
					percentageBonus += 25;
				}

				if( pmAttacker.SentHonorContext != null && pmAttacker.SentHonorContext.Target == defender )
				{
					//pmAttacker.SentHonorContext.ApplyPerfectionDamageBonus( ref factor );
					percentageBonus += pmAttacker.SentHonorContext.PerfectionDamageBonus;
				}
			}

			percentageBonus = Math.Min( percentageBonus, 300 );

			damage = damage + (int)( damage * sneakBonus );

			damage = AOS.Scale( damage, 100 + percentageBonus );
			#endregion

			if ( attacker is BaseCreature )
				((BaseCreature)attacker).AlterMeleeDamageTo( defender, ref damage );

			if ( defender is BaseCreature )
				((BaseCreature)defender).AlterMeleeDamageFrom( attacker, ref damage );

			damage = AbsorbDamage( attacker, defender, damage );

			if ( Core.AOS && damage == 0 ) // parried
			{
				if ( a != null && a.Validate( attacker ) /*&& a.CheckMana( attacker, true )*/ ) // Parried special moves have no mana cost 
				{
					a = null;
					WeaponAbility.ClearCurrentAbility( attacker );

					attacker.SendLocalizedMessage( 1061140 ); // Your attack was parried!
				}
			}

			AddBlood( attacker, defender, damage );

			int phys, fire, cold, pois, nrgy, chaos, direct;

			GetDamageTypes( attacker, out phys, out fire, out cold, out pois, out nrgy, out chaos, out direct );

			if ( Core.ML && this is BaseRanged )
			{
				BaseQuiver quiver = attacker.FindItemOnLayer( Layer.Cloak ) as BaseQuiver;

				if ( quiver != null )
					quiver.AlterBowDamage( ref phys, ref fire, ref cold, ref pois, ref nrgy, ref chaos, ref direct );
			}

			if ( m_Consecrated )
			{
				phys = defender.PhysicalResistance;
				fire = defender.FireResistance;
				cold = defender.ColdResistance;
				pois = defender.PoisonResistance;
				nrgy = defender.EnergyResistance;

				int low = phys, type = 0;

				if ( fire < low ){ low = fire; type = 1; }
				if ( cold < low ){ low = cold; type = 2; }
				if ( pois < low ){ low = pois; type = 3; }
				if ( nrgy < low ){ low = nrgy; type = 4; }

				phys = fire = cold = pois = nrgy = chaos = direct = 0;

				if ( type == 0 ) phys = 100;
				else if ( type == 1 ) fire = 100;
				else if ( type == 2 ) cold = 100;
				else if ( type == 3 ) pois = 100;
				else if ( type == 4 ) nrgy = 100;
			}

			int damageGiven = damage;


			if ( ( defender is IMount && ((IMount)defender).Rider != null ) ) // mount getting hit
			{
				if (defender.Hits <= damage)
					Server.Mobiles.BaseMount.Dismount( ((IMount)defender).Rider );
			}


			if ( a != null && !a.OnBeforeDamage( attacker, defender ) )
			{
				WeaponAbility.ClearCurrentAbility( attacker );
				a = null;
			}

			if ( move != null && !move.OnBeforeDamage( attacker, defender ) )
			{
				SpecialMove.ClearCurrentMove( attacker );
				move = null;
			}

			// New stuff for BladeWeaing performing Armor Ignore attack
			WeaponAbility weaponA;
			bool BladeWeaving = Bladeweave.BladeWeaving(attacker, out weaponA);

			bool ignoreArmor = ( a is ArmorIgnore || (move != null && move.IgnoreArmor( attacker )) || (BladeWeaving && weaponA is ArmorIgnore ));

			if (attacker is BaseCreature && ((BaseCreature)attacker).Special == 6) //new creature abilities
			{
				if (Utility.RandomMinMax(5, 15) > Utility.Random(100)) 
				ignoreArmor = true;
			}

			damageGiven = AOS.Damage( defender, attacker, damage, ignoreArmor, phys, fire, cold, pois, nrgy, chaos, direct, false, this is BaseRanged, false );

			double propertyBonus = ( move == null ) ? 1.0 : move.GetPropertyBonus( attacker );

				Phylactery vault = null;
				if (attacker is PlayerMobile && ((PlayerMobile)attacker).SoulBound)
				{
					vault = ((PlayerMobile)attacker).FindPhylactery();
				}

				int lifeLeech = 0;
				int stamLeech = 0;
				int manaLeech = 0;
				int wraithLeech = 0;

				if (vault == null)
				{
					if ( (int)(m_AosWeaponAttributes.HitLeechHits * propertyBonus) > Utility.Random( 125 ) )
						lifeLeech += 15; // HitLeechHits% chance to leech 15% of damage as hit points

					if ( (int)(m_AosWeaponAttributes.HitLeechStam * propertyBonus) > Utility.Random( 125 ) )
						stamLeech += 15; // HitLeechStam% chance to leech 15% of damage as stamina

					if ( (int)(m_AosWeaponAttributes.HitLeechMana * propertyBonus) > Utility.Random( 125 ) )
						manaLeech += 15; // HitLeechMana% chance to leech 15% of damage as mana
				}
				else 
				{
					if ( (int)((vault.VampireEssence + vault.CalculatePhylacteryMods("VampireEssence"))*propertyBonus) >Utility.Random(125))
						lifeLeech += 15;
					if ( (int)((vault.SpringEssence + vault.CalculatePhylacteryMods("SpringEssence"))*propertyBonus) >Utility.Random(125))
						stamLeech += 15;
					if ( (int)((vault.SacredEssence + vault.CalculatePhylacteryMods("SacredEssence"))*propertyBonus) >Utility.Random(125))
						manaLeech += 15;
				}

				if ( m_Cursed )
					lifeLeech += 15; // Additional 25% life leech for cursed weapons (necro spell)

				if (attacker is BaseCreature && ((BaseCreature)attacker).Special == 2 && Utility.RandomDouble() < ( Convert.ToDouble( ((BaseCreature)attacker).Level) / 100 )) // new creature abilities 
				{
					lifeLeech += Utility.RandomMinMax(5, 15);
				} //creature ability 2

				context = TransformationSpellHelper.GetContext( attacker );

				if ( context != null && context.Type == typeof( VampiricEmbraceSpell ) )
					lifeLeech += 10; // Vampiric embrace gives an additional 10% life leech

				if ( context != null && context.Type == typeof( WraithFormSpell ) )
				{
					wraithLeech = (5 + (int)((15 * attacker.Skills.SpiritSpeak.Value) / 100)); // Wraith form gives an additional 5-20% mana leech

					// Mana leeched by the Wraith Form spell is actually stolen, not just leeched.
					defender.Mana -= AOS.Scale( damageGiven, wraithLeech );

					manaLeech += wraithLeech;
				}

				if ( lifeLeech != 0 )
					attacker.Hits += AOS.Scale( damageGiven, lifeLeech );

				if ( stamLeech != 0 )
					attacker.Stam += AOS.Scale( damageGiven, stamLeech );

				if ( manaLeech != 0 )
					attacker.Mana += AOS.Scale( damageGiven, manaLeech );

				if ( lifeLeech != 0 || stamLeech != 0 || manaLeech != 0 )
					attacker.PlaySound( 0x44D );

			if ( m_MaxHits > 0 && ( ( MaxRange <= 1 && ( defender is Slime || defender is Xenomorph || defender is GreenSlime || defender is BlackPudding || defender is LavaPuddle || defender is AcidPuddle || defender is ToxicElemental ) ) || Utility.Random( 15 ) == 0) ) // FINAL changed from 25 to 15 for more damage to items
			{
				
				if ( MaxRange <= 1 && ( defender is Slime || defender is Xenomorph || defender is GreenSlime || defender is BlackPudding || defender is LavaPuddle || defender is AcidPuddle || defender is ToxicElemental ) )
				{
					attacker.LocalOverheadMessage( MessageType.Regular, 0x3B2, 500263 ); // *Acid blood scars your weapon!*
					if (Utility.RandomBool())
						HitPoints -= (Utility.RandomMinMax(0,2));
				}

				if ( !(AdventuresFunctions.IsPuritain((object)this)) && m_AosWeaponAttributes.SelfRepair > Utility.Random( 20 ) )
				{
					HitPoints += 1;
				}
				else
				{
					if ( this is ILevelable && !(AdventuresFunctions.IsPuritain((object)this)))
					{
						LevelItemManager.RepairItems( attacker );
					}
					else if ( m_Hits > 0 )
					{
						--HitPoints;
						//WearAndTear(Utility.RandomMinMax(0, 1));
					}
					else if ( m_MaxHits > 1 )
					{
						--MaxHitPoints;

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

			if ( attacker is VampireBatFamiliar )
			{
				BaseCreature bc = (BaseCreature)attacker;
				Mobile caster = bc.ControlMaster;

				if ( caster == null )
					caster = bc.SummonMaster;

				if ( caster != null && caster.Map == bc.Map && caster.InRange( bc, 2 ) )
					caster.Hits += damage;
				else
					bc.Hits += damage;
			}

			if ( !AdventuresFunctions.IsPuritain((object)attacker) )
			{
				bool SB = false;

				if (attacker is PlayerMobile && ((PlayerMobile)attacker).SoulBound)
					SB = true; // set up for future removals if necessary... 

				int physChance = (int)(m_AosWeaponAttributes.HitPhysicalArea * propertyBonus);
				int fireChance = (int)(m_AosWeaponAttributes.HitFireArea * propertyBonus);
				int coldChance = (int)(m_AosWeaponAttributes.HitColdArea * propertyBonus);
				int poisChance = (int)(m_AosWeaponAttributes.HitPoisonArea * propertyBonus);
				int nrgyChance = (int)(m_AosWeaponAttributes.HitEnergyArea * propertyBonus);

				if ( physChance != 0 && physChance > Utility.Random( 100 ) )
					DoAreaAttack( attacker, defender, 0x10E,   50, 100, 0, 0, 0, 0 );

				if ( fireChance != 0 && fireChance > Utility.Random( 100 ) )
					DoAreaAttack( attacker, defender, 0x11D, 1160, 0, 100, 0, 0, 0 );

				if ( coldChance != 0 && coldChance > Utility.Random( 100 ) )
					DoAreaAttack( attacker, defender, 0x0FC, 2100, 0, 0, 100, 0, 0 );

				if ( poisChance != 0 && poisChance > Utility.Random( 100 ) )
					DoAreaAttack( attacker, defender, 0x205, 1166, 0, 0, 0, 100, 0 );

				if ( nrgyChance != 0 && nrgyChance > Utility.Random( 100 ) )
					DoAreaAttack( attacker, defender, 0x1F1,  120, 0, 0, 0, 0, 100 );

				int maChance = (int)(m_AosWeaponAttributes.HitMagicArrow * propertyBonus);
				int harmChance = (int)(m_AosWeaponAttributes.HitHarm * propertyBonus);
				int fireballChance = (int)(m_AosWeaponAttributes.HitFireball * propertyBonus);
				int lightningChance = (int)(m_AosWeaponAttributes.HitLightning * propertyBonus);
				int dispelChance = (int)(m_AosWeaponAttributes.HitDispel * propertyBonus);

				if ( attacker is BaseCreature && ((BaseCreature)attacker).Special != 0) //new creature abilities
				{
					BaseCreature pet = attacker as BaseCreature;
					if (pet.Special == 3)
						harmChance = Utility.RandomMinMax(5,15);
					if (pet.Special == 4)
						fireballChance = Utility.RandomMinMax(5,15);
					if (pet.Special == 5)
						lightningChance = Utility.RandomMinMax(5,15);
				}

				if ( maChance != 0 && maChance > Utility.Random( 100 ) )
					DoMagicArrow( attacker, defender );

				if ( harmChance != 0 && harmChance > Utility.Random( 100 ) )
					DoHarm( attacker, defender );

				if ( fireballChance != 0 && fireballChance > Utility.Random( 100 ) )
					DoFireball( attacker, defender );

				if ( lightningChance != 0 && lightningChance > Utility.Random( 100 ) )
					DoLightning( attacker, defender );

				if ( dispelChance != 0 && dispelChance > Utility.Random( 100 ) )
					DoDispel( attacker, defender );

				int laChance = (int)(m_AosWeaponAttributes.HitLowerAttack * propertyBonus);
				int ldChance = (int)(m_AosWeaponAttributes.HitLowerDefend * propertyBonus);

				if ( laChance != 0 && laChance > Utility.Random( 100 ) )
					DoLowerAttack( attacker, defender );

				if ( ldChance != 0 && ldChance > Utility.Random( 100 ) )
					DoLowerDefense( attacker, defender );
			}

			if ( attacker is BaseCreature )
				((BaseCreature)attacker).OnGaveMeleeAttack( defender );

			if ( defender is BaseCreature )
				((BaseCreature)defender).OnGotMeleeAttack( attacker );

			if ( a != null )
				a.OnHit( attacker, defender, damage );

			if ( move != null )
				move.OnHit( attacker, defender, damage );

			if ( defender is IHonorTarget && ((IHonorTarget)defender).ReceivedHonorContext != null )
				((IHonorTarget)defender).ReceivedHonorContext.OnTargetHit( attacker );

			if ( !(this is BaseRanged) )
			{
				if ( AnimalForm.UnderTransformation( attacker, typeof( GiantSerpent ) ) )
					defender.ApplyPoison( attacker, Poison.Lesser );

				if ( AnimalForm.UnderTransformation( defender, typeof( BullFrog ) ) )
					attacker.ApplyPoison( defender, Poison.Regular );
			}

			BaseWeapon poisonWeapon = attacker.Weapon as BaseWeapon; // ------- POISON SECTION ------- //
			if ( poisonWeapon != null && attacker is PlayerMobile && defender != null )
			{
				Poison p = poisonWeapon.Poison;

				bool willPoison = true;

				int ClassicPoisons = 0;
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( attacker );
				if ( DB != null )
				{
					ClassicPoisons = DB.ClassicPoisoning;
				}

				if ( p != null )
				{
					// Use poisoning skill to help determine potency 
					int maxLevel = attacker.Skills[SkillName.Poisoning].Fixed / 200;
					if ( maxLevel < 0 ) maxLevel = 0;
					if ( p.Level > maxLevel ) p = Poison.GetPoison( maxLevel );

					if ( poisonWeapon.PoisonCharges < 1 && willPoison == true )
						willPoison = false;

					if ( defender is BaseCreature && willPoison == true )
					{
						BaseCreature bc = (BaseCreature)defender;
						Poison venom = bc.PoisonImmune;
						if ( venom != null && venom.Level >= p.Level )
							willPoison = false;
					}

					if ( Server.Items.WeaponAbility.GetCurrentAbility( attacker ) == WeaponAbility.ShadowInfectiousStrike && willPoison == true && ClassicPoisons == 0 )
						willPoison = false;
					else if ( Server.Items.WeaponAbility.GetCurrentAbility( attacker ) == WeaponAbility.InfectiousStrike && willPoison == true && ClassicPoisons == 0 )
						willPoison = false;
					else if ( ClassicPoisons == 0 )
						willPoison = false;

					if ( defender.Poisoned && willPoison == true )
						willPoison = false;

					if ( willPoison == true )
					{
						if ( !(attacker.CheckSkill( SkillName.Poisoning, 0, 125 ) ) )
							willPoison = false;
					}

					if ( ClassicPoisons > 0 && !( this is BaseKnife || this is BaseSword || this is BaseSpear ) )
					{
						willPoison = false;
					}

					if ( willPoison == true )
					{
						Misc.Titles.AwardKarma( attacker, -20, true );
						--poisonWeapon.PoisonCharges;
						defender.ApplyPoison( attacker, p );

						defender.PlaySound( 0x62D );
						defender.FixedParticles( 0x3728, 244, 25, 9941, 1266, 0, EffectLayer.Waist );

						attacker.SendLocalizedMessage( 1008096, true, defender.Name ); // You have poisoned your target : 
						defender.SendLocalizedMessage( 1008097, false, attacker.Name ); //  : poisoned you!
					}
				}
			}
		}

		public virtual double GetAosDamage( Mobile attacker, int bonus, int dice, int sides )
		{
			int damage = Utility.Dice( dice, sides, bonus ) * 100;
			int damageBonus = 0;

			// Inscription bonus
			int inscribeSkill = attacker.Skills[SkillName.Inscribe].Fixed;

			damageBonus += inscribeSkill / 200;

			if ( inscribeSkill >= 1000 )
				damageBonus += 5;

			if ( attacker.Player )
			{
				// Int bonus
				damageBonus += (attacker.Int / 10);

				int SDICap = MyServerSettings.RealSpellDamageCap();
				// SDI bonus
				if ( !AdventuresFunctions.IsPuritain((object)attacker) && !(attacker is PlayerMobile && ((PlayerMobile)attacker).SoulBound))
				{
					damageBonus += AosAttributes.GetValue( attacker, AosAttribute.SpellDamage );
				}
						if (damageBonus > SDICap) {
						damageBonus = SDICap;
					}
				
				

				TransformContext context = TransformationSpellHelper.GetContext( attacker );
			}

			damage = AOS.Scale( damage, 100 + damageBonus );

			return damage / 100;
		}

		#region Do<AoSEffect>
		public virtual void DoMagicArrow( Mobile attacker, Mobile defender )
		{
			if ( !attacker.CanBeHarmful( defender, false ) )
				return;

			attacker.DoHarmful( defender );

			double damage = GetAosDamage( attacker, 10, 1, 4 );

			attacker.MovingParticles( defender, 0x36E4, 5, 0, false, true, 3006, 4006, 0 );
			attacker.PlaySound( 0x1E5 );

			SpellHelper.Damage( TimeSpan.FromSeconds( 1.0 ), defender, attacker, damage, 0, 100, 0, 0, 0 );
		}

		public virtual void DoHarm( Mobile attacker, Mobile defender )
		{
			if ( !attacker.CanBeHarmful( defender, false ) )
				return;

			attacker.DoHarmful( defender );

			double damage = GetAosDamage( attacker, 17, 1, 5 );

			if ( !defender.InRange( attacker, 2 ) )
				damage *= 0.25; // 1/4 damage at > 2 tile range
			else if ( !defender.InRange( attacker, 1 ) )
				damage *= 0.50; // 1/2 damage at 2 tile range

			defender.FixedParticles( 0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist );
			defender.PlaySound( 0x0FC );

			SpellHelper.Damage( TimeSpan.Zero, defender, attacker, damage, 0, 0, 100, 0, 0 );
		}

		public virtual void DoFireball( Mobile attacker, Mobile defender )
		{
			if ( !attacker.CanBeHarmful( defender, false ) )
				return;

			attacker.DoHarmful( defender );

			double damage = GetAosDamage( attacker, 19, 1, 5 );

			attacker.MovingParticles( defender, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160 );
			attacker.PlaySound( 0x15E );

			SpellHelper.Damage( TimeSpan.FromSeconds( 1.0 ), defender, attacker, damage, 0, 100, 0, 0, 0 );
		}

		public virtual void DoLightning( Mobile attacker, Mobile defender )
		{
			if ( !attacker.CanBeHarmful( defender, false ) )
				return;

			attacker.DoHarmful( defender );

			double damage = GetAosDamage( attacker, 23, 1, 4 );

			defender.BoltEffect( 0 );

			SpellHelper.Damage( TimeSpan.Zero, defender, attacker, damage, 0, 0, 0, 0, 100 );
		}

		public virtual void DoDispel( Mobile attacker, Mobile defender )
		{
			if ( defender is BaseCreature )
			{
				BaseCreature bc = defender as BaseCreature;

				if ( bc.ControlSlots == 666 )
				{
					Effects.SendLocationParticles( EffectItem.Create( defender.Location, defender.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
					Effects.PlaySound( defender, defender.Map, 0x201 );
					defender.Delete();
				}
				else
				{
					bool dispellable = false;

					if ( defender is BaseCreature )
						dispellable = ((BaseCreature)defender).Summoned && !((BaseCreature)defender).IsAnimatedDead;

					if ( !dispellable )
						return;

					if ( !attacker.CanBeHarmful( defender, false ) )
						return;

					attacker.DoHarmful( defender );

					Spells.MagerySpell sp = new Spells.Sixth.DispelSpell( attacker, null );

					if ( sp.CheckResisted( defender ) )
					{
						defender.FixedEffect( 0x3779, 10, 20 );
					}
					else
					{
						Effects.SendLocationParticles( EffectItem.Create( defender.Location, defender.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						Effects.PlaySound( defender, defender.Map, 0x201 );

						defender.Delete();
					}
				}
			}
		}

		public virtual void DoLowerAttack( Mobile from, Mobile defender )
		{
			if ( HitLower.ApplyAttack( defender ) )
			{
				defender.PlaySound( 0x28E );
				Effects.SendTargetEffect( defender, 0x37BE, 1, 4, 0xA, 3 );
			}
		}

		public virtual void DoLowerDefense( Mobile from, Mobile defender )
		{
			if ( HitLower.ApplyDefense( defender ) )
			{
				defender.PlaySound( 0x28E );
				Effects.SendTargetEffect( defender, 0x37BE, 1, 4, 0x23, 3 );
			}
		}

		public virtual void DoAreaAttack( Mobile from, Mobile defender, int sound, int hue, int phys, int fire, int cold, int pois, int nrgy )
		{
			Map map = from.Map;

			if ( map == null )
				return;

			List<Mobile> list = new List<Mobile>();

			foreach ( Mobile m in from.GetMobilesInRange( 5 ) )
			{
				if ( Utility.RandomDouble() > 0.66 && from != m && defender != m && SpellHelper.ValidIndirectTarget( from, m ) && from.CanBeHarmful( m, false ) && ( !Core.ML || from.InLOS( m ) ) )
					list.Add( m );
			}

			if ( list.Count == 0 )
				return;

			Effects.PlaySound( from.Location, map, sound );

			// TODO: What is the damage calculation?

			for ( int i = 0; i < list.Count; ++i )
			{
				Mobile m = list[i];

				double scalar = (8 - from.GetDistanceToSqrt( m )) / 7;

				if ( scalar > 1.0 )
					scalar = 1.0;
				else if ( scalar < 0.0 )
					continue;

				from.DoHarmful( m, true );
				m.FixedEffect( 0x3779, 1, 15, hue, 0 );
				AOS.Damage( m, from, (int)(GetBaseDamage( from ) * scalar), phys, fire, cold, pois, nrgy );
			}
		}
		#endregion

		public virtual CheckSlayerResult CheckSlayers( Mobile attacker, Mobile defender )
		{
			BaseWeapon atkWeapon = attacker.Weapon as BaseWeapon;
			SlayerEntry atkSlayer = SlayerGroup.GetEntryByName( atkWeapon.Slayer );
			SlayerEntry atkSlayer2 = SlayerGroup.GetEntryByName( atkWeapon.Slayer2 );

			if ( atkSlayer != null && atkSlayer.Slays( defender )  || atkSlayer2 != null && atkSlayer2.Slays( defender ) )
				return CheckSlayerResult.Slayer;

			if ( !Core.SE )
			{
				ISlayer defISlayer = Spellbook.FindEquippedSpellbook( defender );

				if( defISlayer == null )
					defISlayer = defender.Weapon as ISlayer;

				if( defISlayer != null )
				{
					SlayerEntry defSlayer = SlayerGroup.GetEntryByName( defISlayer.Slayer );
					SlayerEntry defSlayer2 = SlayerGroup.GetEntryByName( defISlayer.Slayer2 );

					if( defSlayer != null && defSlayer.Group.OppositionSuperSlays( attacker ) || defSlayer2 != null && defSlayer2.Group.OppositionSuperSlays( attacker ) )
						return CheckSlayerResult.Opposition;
				}
			}

			return CheckSlayerResult.None;
		}

		public virtual void AddBlood( Mobile attacker, Mobile defender, int damage )
		{
			if ( damage > 0 && ( (defender is BaseCreature && !((BaseCreature)defender).BleedImmune) || !(defender is BaseCreature) ) )
			{
				new Blood().MoveToWorld( defender.Location, defender.Map );

				int extraBlood = (Core.SE ? Utility.RandomMinMax( 3, 4 ) : Utility.RandomMinMax( 0, 1 ) );

				for( int i = 0; i < extraBlood; i++ )
				{
					new Blood().MoveToWorld( new Point3D(
						defender.X + Utility.RandomMinMax( -1, 1 ),
						defender.Y + Utility.RandomMinMax( -1, 1 ),
						defender.Z ), defender.Map );
				}
			}
		}

		public virtual void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			if( wielder is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)wielder;

				if (bc.Special == 8) //new creature abilities
				{
					phys = 0;
					fire = 0;
					cold = 0;
					pois = 0;
					nrgy = 0;
					chaos = 100;
					direct = 0;
				}
				else
				{
					phys = bc.PhysicalDamage;
					fire = bc.FireDamage;
					cold = bc.ColdDamage;
					pois = bc.PoisonDamage;
					nrgy = bc.EnergyDamage;
					chaos = bc.ChaosDamage;
					direct = bc.DirectDamage;
				}
			}
			else
			{
				fire = m_AosElementDamages.Fire;
				cold = m_AosElementDamages.Cold;
				pois = m_AosElementDamages.Poison;
				nrgy = m_AosElementDamages.Energy;
				chaos = m_AosElementDamages.Chaos;
				direct = m_AosElementDamages.Direct;

				phys = 100 - fire - cold - pois - nrgy - chaos - direct;

				CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

				if( resInfo != null )
				{
					CraftAttributeInfo attrInfo = resInfo.AttributeInfo;

					if( attrInfo != null )
					{
						int left = phys;

						left = ApplyCraftAttributeElementDamage( attrInfo.WeaponColdDamage,		ref cold, left );
						left = ApplyCraftAttributeElementDamage( attrInfo.WeaponEnergyDamage,	ref nrgy, left );
						left = ApplyCraftAttributeElementDamage( attrInfo.WeaponFireDamage,		ref fire, left );
						left = ApplyCraftAttributeElementDamage( attrInfo.WeaponPoisonDamage,	ref pois, left );
						left = ApplyCraftAttributeElementDamage( attrInfo.WeaponChaosDamage,	ref chaos, left );
						left = ApplyCraftAttributeElementDamage( attrInfo.WeaponDirectDamage,	ref direct, left );

						phys = left;
					}
				}
			}
		}

		private int ApplyCraftAttributeElementDamage( int attrDamage, ref int element, int totalRemaining )
		{
			if( totalRemaining <= 0 )
				return 0;

			if ( attrDamage <= 0 )
				return totalRemaining;

			int appliedDamage = attrDamage;

			if ( (appliedDamage + element) > 100 )
				appliedDamage = 100 - element;

			if( appliedDamage > totalRemaining )
				appliedDamage = totalRemaining;

			element += appliedDamage;

			return totalRemaining - appliedDamage;
		}

		public virtual void OnMiss( Mobile attacker, Mobile defender )
		{
			if ( attacker is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)attacker;
				pm.Stealthing = 0;
			}
			if (AdventuresFunctions.IsPuritain((object)defender) &&  defender is PlayerMobile && attacker is BaseCreature )
			{
				PlayerMobile pm = (PlayerMobile)defender;
				if (pm.Mounted && pm.Mount != null && !(pm.Mount is EtherealMount) )
				{
					IMount mount = pm.Mount;

					if ( CheckHit( attacker, (Mobile)mount ) && ( (((BaseCreature)attacker).Agility()/3) * Utility.RandomDouble() ) > ( Utility.RandomDouble() * ((BaseCreature)mount).Agility() ) )
					{
						OnHit( attacker, defender, (Utility.RandomMinMax(1, 5)/10) );
						defender.SendMessage("The hit misses you, but if deflected on your mount!");
					}
					
				}
			}

			PlaySwingAnimation( attacker );
			attacker.PlaySound( GetMissAttackSound( attacker, defender ) );
			defender.PlaySound( GetMissDefendSound( attacker, defender ) );

			WeaponAbility ability = WeaponAbility.GetCurrentAbility( attacker );

			if ( ability != null )
				ability.OnMiss( attacker, defender );

			SpecialMove move = SpecialMove.GetCurrentMove( attacker );

			if ( move != null )
				move.OnMiss( attacker, defender );

			if ( defender is IHonorTarget && ((IHonorTarget)defender).ReceivedHonorContext != null )
				((IHonorTarget)defender).ReceivedHonorContext.OnTargetMissed( attacker );


			if ( attacker is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)attacker;
				if ( bc.AI == AIType.AI_Archer )
				{
					int sound = 0;

					if ( bc.FindItemOnLayer( Layer.OneHanded ) is BaseMeleeWeapon ) { sound = ( ( BaseMeleeWeapon )( bc.FindItemOnLayer( Layer.OneHanded ) ) ).DefMissSound; }
					else if ( bc.FindItemOnLayer( Layer.TwoHanded ) is BaseMeleeWeapon ) { sound = ( ( BaseMeleeWeapon )( bc.FindItemOnLayer( Layer.TwoHanded ) ) ).DefMissSound; }

					if ( sound > 0 ){ bc.PlaySound( sound ); }
				}
			}
		}

		public virtual void GetBaseDamageRange( Mobile attacker, out int min, out int max )
		{
			if ( attacker is BaseCreature )
			{
				BaseCreature c = (BaseCreature)attacker;

				if ( c.DamageMin >= 0 )
				{
					min = c.DamageMin;
					max = c.DamageMax;
					return;
				}

				if ( this is Fists && !attacker.Body.IsHuman )
				{

					min = (attacker.Str / 25) * (int)(c.Skills[SkillName.Wrestling].Value / 125); // FINAL - shouldn't mob attack power be based on wrestling skill?  was /28 before
					max = (attacker.Str / 20) * (int)(c.Skills[SkillName.Wrestling].Value / 125);
					return;
				}
			}
			// final for morph armors
			if (attacker is PlayerMobile)
			{
				if (  ( attacker.BodyMod == 318 || attacker.BodyMod == 84) && this is Fists ) // Final for morphing armor
				{	
					min = 17;
					max = 35;
					return;
				}
				else if ( attacker.BodyMod == 263 && this is Fists)
				{	
					min = 45;
					max = 65;
					return;
				}	
				else if ( ((PlayerMobile)attacker).Troubadour() && this is Fists)
				{	
					min = 10;
					max = 17;
					return;
				}
			}
			
			min = MinDamage;
			max = MaxDamage;
		}

		public virtual double GetBaseDamage( Mobile attacker )
		{
			int min, max;

			GetBaseDamageRange( attacker, out min, out max );

			return Utility.RandomMinMax( min, max );
		}

		public virtual double GetBonus( double value, double scalar, double threshold, double offset )
		{
			double bonus = value * scalar;

			if ( value >= threshold )
				bonus += offset;

			return bonus / 100;
		}

		public virtual int GetHitChanceBonus()
		{
			if ( !Core.AOS )
				return 0;

			int bonus = 0;

			switch ( m_AccuracyLevel )
			{
				case WeaponAccuracyLevel.Accurate:		bonus += 02; break;
				case WeaponAccuracyLevel.Surpassingly:	bonus += 04; break;
				case WeaponAccuracyLevel.Eminently:		bonus += 06; break;
				case WeaponAccuracyLevel.Exceedingly:	bonus += 08; break;
				case WeaponAccuracyLevel.Supremely:		bonus += 10; break;
			}

			return bonus;
		}

		public virtual int GetDamageBonus()
		{
			int bonus = VirtualDamageBonus;

			switch ( m_Quality )
			{
				case WeaponQuality.Low:			bonus -= 20; break;
				case WeaponQuality.Exceptional:	bonus += 20; break;
			}

			switch ( m_DamageLevel )
			{
				case WeaponDamageLevel.Ruin:	bonus += 15; break;
				case WeaponDamageLevel.Might:	bonus += 20; break;
				case WeaponDamageLevel.Force:	bonus += 25; break;
				case WeaponDamageLevel.Power:	bonus += 30; break;
				case WeaponDamageLevel.Vanq:	bonus += 35; break;
			}

			return bonus;
		}

		public virtual void GetStatusDamage( Mobile from, out int min, out int max )
		{
			int baseMin, baseMax;

			GetBaseDamageRange( from, out baseMin, out baseMax );

			if ( Core.AOS )
			{
				min = Math.Max( (int)ScaleDamageAOS( from, baseMin, false ), 1 );
				max = Math.Max( (int)ScaleDamageAOS( from, baseMax, false ), 1 );
			}
			else
			{
				min = Math.Max( (int)ScaleDamageOld( from, baseMin, false ), 1 );
				max = Math.Max( (int)ScaleDamageOld( from, baseMax, false ), 1 );
			}
		}

		public virtual double ScaleDamageAOS( Mobile attacker, double damage, bool checkSkills )
		{
			if ( checkSkills )
			{
				attacker.CheckSkill( SkillName.Tactics, 0.0, attacker.Skills[SkillName.Tactics].Cap ); // Passively check tactics for gain
				attacker.CheckSkill( SkillName.Anatomy, 0.0, attacker.Skills[SkillName.Anatomy].Cap ); // Passively check anatomy for gain

				//if ( Type == WeaponType.Axe )
				//	attacker.CheckSkill( SkillName.Lumberjacking, 0.0, 100.0 ); // Passively check Lumberjacking for gain
			}

			#region Physical bonuses
			/*
			 * These are the bonuses given by the physical characteristics of the mobile.
			 * No caps apply.
			 */
			double	strengthBonus = GetBonus( attacker.Str,										0.300, 100.0,  5.00 );
			double	anatomyBonus = GetBonus( attacker.Skills[SkillName.Anatomy].Value,			0.500, 100.0,  5.00 );
			double	tacticsBonus = GetBonus( attacker.Skills[SkillName.Tactics].Value,			0.625, 100.0,  6.25 );
			double	lumberBonus = GetBonus( attacker.Skills[SkillName.Lumberjacking].Value,		0.200, 100.0, 10.00 );
			double	armsLoreBonus = GetBonus( attacker.Skills[SkillName.ArmsLore].Value,		0.625, 100.0,  6.25 );
			double	miningBonus = GetBonus( attacker.Skills[SkillName.Mining].Value,			0.200, 100.0, 10.00 );
			double	fishingBonus = GetBonus( attacker.Skills[SkillName.Fishing].Value,			0.200, 100.0, 10.00 );
			double	ninjaBonus = GetBonus( attacker.Skills[SkillName.Ninjitsu].Value,			0.625, 100.0,  6.25 );
			double	bushidoBonus = GetBonus( attacker.Skills[SkillName.Bushido].Value,			0.625, 100.0,  6.25 );
			double	necroBonus = GetBonus( attacker.Skills[SkillName.Necromancy].Value,			0.625, 100.0,  6.25 );
			double	wizardBonus = GetBonus( attacker.Skills[SkillName.Magery].Value,			0.625, 100.0,  6.25 );
			double	bowyerBonus = GetBonus( attacker.Skills[SkillName.Fletching].Value,			0.625, 100.0,  6.25 );

			if ( Type != WeaponType.Axe && Type != WeaponType.Slashing && Type != WeaponType.Polearm )
				bushidoBonus = 0.0;

			if ( Type != WeaponType.Axe )
				lumberBonus = 0.0;

			if ( Type != WeaponType.Bashing )
				miningBonus = 0.0;

			if (!( this is Harpoon || this is GiftHarpoon || this is LevelHarpoon ))
				fishingBonus = 0.0;

			if (!( this is WizardWand || this is BaseWizardStaff || this is BaseLevelStave || this is BaseGiftStave || this is GiftScepter || this is LevelScepter || this is Scepter ))
			{
				necroBonus = 0.0;
				wizardBonus = 0.0;
			}

			if (!( this is BaseRanged && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem((this)) ))
				bowyerBonus = 0.0;

			#endregion

			#region Modifiers
			/*
			 * The following are damage modifiers whose effect shows on the status bar.
			 * Capped at 100% total.
			 */
			int damageBonus = AosAttributes.GetValue( attacker, AosAttribute.WeaponDamage );

			// Horrific Beast transformation gives a +25% bonus to damage.
			if( TransformationSpellHelper.UnderTransformation( attacker, typeof( HorrificBeastSpell ) ) )
				damageBonus += 25;

			// Divine Fury gives a +10% bonus to damage.
			if ( Spells.Chivalry.DivineFurySpell.UnderEffect( attacker ) )
				damageBonus += 10;

			int defenseMasteryMalus = 0;

			// Defense Mastery gives a -50%/-80% malus to damage.
			if ( Server.Items.DefenseMastery.GetMalus( attacker, ref defenseMasteryMalus ) )
				damageBonus -= defenseMasteryMalus;

			int discordanceEffect = 0;

			// Discordance gives a -2%/-48% malus to damage.
			if ( SkillHandlers.Discordance.GetEffect( attacker, ref discordanceEffect ) )
				damageBonus -= discordanceEffect * 2;

			int damageBonusCap = MyServerSettings.DamageIncreaseCap();
			if ( damageBonus > damageBonusCap )
				damageBonus = damageBonusCap;
			#endregion

			double totalBonus = strengthBonus + anatomyBonus + tacticsBonus + necroBonus + wizardBonus + bowyerBonus + bushidoBonus + ninjaBonus + armsLoreBonus + lumberBonus + miningBonus + fishingBonus + ((double)(GetDamageBonus() + damageBonus) / 100.0);

			double total = damage + (int)(damage * totalBonus);

			if (attacker is PlayerMobile && ((PlayerMobile)attacker).Avatar)
				total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 200, 10 ) );
			else if (attacker is PlayerMobile)
				total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 150, 10 ) );
			else
				total = (double)(AdventuresFunctions.DiminishingReturns( (int)total, 225, 10 ) );
			
			Mobile atcker = attacker;
			
			if ( attacker is BaseCreature && ((BaseCreature)attacker).GetMaster() is PlayerMobile)
				atcker = ((BaseCreature)attacker).GetMaster();

			return total;

		}

		public virtual int VirtualDamageBonus{ get{ return 0; } }

		public virtual int ComputeDamageAOS( Mobile attacker, Mobile defender )
		{
			return (int)ScaleDamageAOS( attacker, GetBaseDamage( attacker ), true );
		}

		public virtual double ScaleDamageOld( Mobile attacker, double damage, bool checkSkills )
		{
			if ( checkSkills )
			{
				attacker.CheckSkill( SkillName.Tactics, 0.0, attacker.Skills[SkillName.Tactics].Cap ); // Passively check tactics for gain
				attacker.CheckSkill( SkillName.Anatomy, 0.0, attacker.Skills[SkillName.Anatomy].Cap ); // Passively check Anatomy for gain

				//if ( Type == WeaponType.Axe )
				//	attacker.CheckSkill( SkillName.Lumberjacking, 0.0, 100.0 ); // Passively check Lumberjacking for gain
			}

			/* Compute tactics modifier
			 * :   0.0 = 50% loss
			 * :  50.0 = unchanged
			 * : 100.0 = 50% bonus
			 */
			double tacticsBonus = (attacker.Skills[SkillName.Tactics].Value - 50.0) / 100.0;

			/* Compute strength modifier
			 * : 1% bonus for every 5 strength
			 */
			double strBonus = (attacker.Str / 5.0) / 100.0;

			/* Compute anatomy modifier
			 * : 1% bonus for every 5 points of anatomy
			 * : +10% bonus at Grandmaster or higher
			 */
			double anatomyValue = attacker.Skills[SkillName.Anatomy].Value;
			double anatomyBonus = (anatomyValue / 5.0) / 100.0;

			if ( anatomyValue >= 100.0 )
				anatomyBonus += 0.1;

			/* Compute lumberjacking bonus
			 * : 1% bonus for every 5 points of lumberjacking
			 * : +10% bonus at Grandmaster or higher
			 */
			double lumberBonus;

			if ( Type == WeaponType.Axe )
			{
				double lumberValue = attacker.Skills[SkillName.Lumberjacking].Value;

				lumberBonus = (lumberValue / 5.0) / 100.0;

				if ( lumberValue >= 100.0 )
					lumberBonus += 0.1;
			}
			else
			{
				lumberBonus = 0.0;
			}

			/* Compute mining bonus
			 * : 1% bonus for every 5 points of mining
			 * : +10% bonus at Grandmaster or higher
			 */
			double miningBonus;

			if ( Type == WeaponType.Bashing )
			{
				double miningValue = attacker.Skills[SkillName.Mining].Value;

				miningBonus = (miningValue / 5.0) / 100.0;

				if ( miningValue >= 100.0 )
					miningBonus += 0.1;
			}
			else
			{
				miningBonus = 0.0;
			}

			/* Compute fishing bonus
			 * : 1% bonus for every 5 points of fishing
			 * : +10% bonus at Grandmaster or higher
			 */
			double fishingBonus;

			if ( this is Harpoon || this is GiftHarpoon || this is LevelHarpoon )
			{
				double fishingValue = attacker.Skills[SkillName.Fishing].Value;

				fishingBonus = (fishingValue / 5.0) / 100.0;

				if ( fishingValue >= 100.0 )
					fishingBonus += 0.1;
			}
			else
			{
				fishingBonus = 0.0;
			}

			/* Compute ninjitsu modifier
			 * : 1% bonus for every 5 points of ninjitsu
			 * : +10% bonus at Grandmaster or higher
			 */
			double ninjaValue = attacker.Skills[SkillName.Ninjitsu].Value;
			double ninjaBonus = (ninjaValue / 5.0) / 100.0;

			if ( ninjaValue >= 100.0 )
				ninjaBonus += 0.1;

			/* Compute bushido modifier
			 * : 1% bonus for every 5 points of bushido
			 * : +10% bonus at Grandmaster or higher
			 */
			double bushidoValue = attacker.Skills[SkillName.Bushido].Value;
			double bushidoBonus = (bushidoValue / 5.0) / 100.0;
			if ( Type == WeaponType.Axe || Type == WeaponType.Slashing || Type == WeaponType.Polearm )
			{
				if ( bushidoValue >= 100.0 )
					bushidoBonus += 0.1;
			}
			else
			{
				bushidoBonus = 0.0;
			}

			/* Compute necromancer modifier
			 * : 1% bonus for every 5 points of necromancy
			 * : +10% bonus at Grandmaster or higher
			 */
			double necromancyValue = attacker.Skills[SkillName.Necromancy].Value;
			double necromancyBonus = (necromancyValue / 5.0) / 100.0;
			if ( this is WizardWand || this is BaseWizardStaff || this is BaseLevelStave || this is BaseGiftStave || this is GiftScepter || this is LevelScepter || this is Scepter )
			{
				if ( necromancyValue >= 100.0 )
					necromancyBonus += 0.1;
			}
			else
			{
				necromancyBonus = 0.0;
			}

			/* Compute magery modifier
			 * : 1% bonus for every 5 points of magery
			 * : +10% bonus at Grandmaster or higher
			 */
			double mageryValue = attacker.Skills[SkillName.Magery].Value;
			double mageryBonus = (mageryValue / 5.0) / 100.0;
			if ( this is WizardWand || this is BaseWizardStaff || this is BaseLevelStave || this is BaseGiftStave || this is GiftScepter || this is LevelScepter || this is Scepter )
			{
				if ( mageryValue >= 100.0 )
					mageryBonus += 0.1;
			}
			else
			{
				mageryBonus = 0.0;
			}

			/* Compute bowyer modifier
			 * : 1% bonus for every 5 points of fletching
			 * : +10% bonus at Grandmaster or higher
			 */
			double bowyerValue = attacker.Skills[SkillName.Fletching].Value;
			double bowyerBonus = (bowyerValue / 5.0) / 100.0;
			if ( this is BaseRanged && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem((this)) )
			{
				if ( bowyerValue >= 100.0 )
					bowyerBonus += 0.1;
			}
			else
			{
				bowyerBonus = 0.0;
			}

			// New quality bonus:
			double qualityBonus = ((int)m_Quality - 1) * 0.2;

			// Apply bonuses
			damage += (damage * tacticsBonus) + (damage * strBonus) + (damage * necromancyBonus) + (damage * mageryBonus) + (damage * bowyerBonus) +(damage * anatomyBonus) + (damage * lumberBonus) + (damage * bushidoBonus) + (damage * ninjaBonus) + (damage * miningBonus) + (damage * fishingBonus) + (damage * qualityBonus) + ((damage * VirtualDamageBonus) / 100);

			// Old quality bonus:
#if false
			/* Apply quality offset
			 * : Low         : -4
			 * : Regular     :  0
			 * : Exceptional : +4
			 */
			damage += ((int)m_Quality - 1) * 4.0;
#endif

			/* Apply damage level offset
			 * : Regular : 0
			 * : Ruin    : 1
			 * : Might   : 3
			 * : Force   : 5
			 * : Power   : 7
			 * : Vanq    : 9
			 */
			if ( m_DamageLevel != WeaponDamageLevel.Regular )
				damage += (2.0 * (int)m_DamageLevel) - 1.0;

			// Halve the computed damage and return
			damage /= 2.0;

			return ScaleDamageByDurability( (int)damage );
		}

		public virtual int ScaleDamageByDurability( int damage )
		{
			int scale = 100;

			if ( m_MaxHits > 0 && m_Hits < m_MaxHits )
				scale = 50 + ((50 * m_Hits) / m_MaxHits);

			return AOS.Scale( damage, scale );
		}

		public virtual int ComputeDamage( Mobile attacker, Mobile defender )
		{
			if ( Core.AOS )
				return ComputeDamageAOS( attacker, defender );

			return (int)ScaleDamageOld( attacker, GetBaseDamage( attacker ), true );
		}

		public virtual void PlayHurtAnimation( Mobile from )
		{
			int action;
			int frames;

			switch ( from.Body.Type )
			{
				case BodyType.Sea:
				case BodyType.Animal:
				{
					action = 7;
					frames = 5;
					break;
				}
				case BodyType.Monster:
				{
					action = 10;
					frames = 4;
					break;
				}
				case BodyType.Human:
				{
					action = 20;
					frames = 5;
					break;
				}
				default: return;
			}

			if ( from.Mounted )
				return;

			from.Animate( action, frames, 1, true, false, 0 );
		}

		public virtual void PlaySwingAnimation( Mobile from )
		{
			int action;

			switch ( from.Body.Type )
			{
				case BodyType.Sea:
				case BodyType.Animal:
				{
					action = Utility.Random( 5, 2 );
					break;
				}
				case BodyType.Monster:
				{
					switch ( Animation )
					{
						default:
						case WeaponAnimation.Wrestle:
						case WeaponAnimation.Bash1H:
						case WeaponAnimation.Pierce1H:
						case WeaponAnimation.Slash1H:
						case WeaponAnimation.Bash2H:
						case WeaponAnimation.Pierce2H:
						case WeaponAnimation.Slash2H: action = Utility.Random( 4, 3 ); break;
						case WeaponAnimation.ShootBow:  return; // 7
						case WeaponAnimation.ShootXBow: return; // 8
					}

					break;
				}
				case BodyType.Human:
				{
					if ( !from.Mounted )
					{
						action = (int)Animation;
					}
					else
					{
						switch ( Animation )
						{
							default:
							case WeaponAnimation.Wrestle:
							case WeaponAnimation.Bash1H:
							case WeaponAnimation.Pierce1H:
							case WeaponAnimation.Slash1H: action = 26; break;
							case WeaponAnimation.Bash2H:
							case WeaponAnimation.Pierce2H:
							case WeaponAnimation.Slash2H: action = 29; break;
							case WeaponAnimation.ShootBow: action = 27; break;
							case WeaponAnimation.ShootXBow: action = 28; break;
						}
					}

					break;
				}
				default: return;
			}

			from.Animate( action, 7, 1, true, false, 0 );
		}

		#region Serialization/Deserialization
		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf )
				flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 10 ); // version 10 added wear

			writer.Write((int) m_wear);

			SaveFlag flags = SaveFlag.None;

			SetSaveFlag( ref flags, SaveFlag.DamageLevel,		m_DamageLevel != WeaponDamageLevel.Regular );
			SetSaveFlag( ref flags, SaveFlag.AccuracyLevel,		m_AccuracyLevel != WeaponAccuracyLevel.Regular );
			SetSaveFlag( ref flags, SaveFlag.DurabilityLevel,	m_DurabilityLevel != WeaponDurabilityLevel.Regular );
			SetSaveFlag( ref flags, SaveFlag.Quality,			m_Quality != WeaponQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.Hits,				m_Hits != 0 );
			SetSaveFlag( ref flags, SaveFlag.MaxHits,			m_MaxHits != 0 );
			SetSaveFlag( ref flags, SaveFlag.Slayer,			m_Slayer != SlayerName.None );
			SetSaveFlag( ref flags, SaveFlag.Poison,			m_Poison != null );
			SetSaveFlag( ref flags, SaveFlag.PoisonCharges,		m_PoisonCharges != 0 );
			SetSaveFlag( ref flags, SaveFlag.Crafter,			m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Identified,		m_Identified != false );
			SetSaveFlag( ref flags, SaveFlag.StrReq,			m_StrReq != -1 );
			SetSaveFlag( ref flags, SaveFlag.DexReq,			m_DexReq != -1 );
			SetSaveFlag( ref flags, SaveFlag.IntReq,			m_IntReq != -1 );
			SetSaveFlag( ref flags, SaveFlag.MinDamage,			m_MinDamage != -1 );
			SetSaveFlag( ref flags, SaveFlag.MaxDamage,			m_MaxDamage != -1 );
			SetSaveFlag( ref flags, SaveFlag.HitSound,			m_HitSound != -1 );
			SetSaveFlag( ref flags, SaveFlag.MissSound,			m_MissSound != -1 );
			SetSaveFlag( ref flags, SaveFlag.Speed,				m_Speed != -1 );
			SetSaveFlag( ref flags, SaveFlag.MaxRange,			m_MaxRange != -1 );
			SetSaveFlag( ref flags, SaveFlag.Skill,				m_Skill != (SkillName)(-1) );
			SetSaveFlag( ref flags, SaveFlag.Type,				m_Type != (WeaponType)(-1) );
			SetSaveFlag( ref flags, SaveFlag.Animation,			m_Animation != (WeaponAnimation)(-1) );
			SetSaveFlag( ref flags, SaveFlag.Resource,			m_Resource != CraftResource.Iron );
			SetSaveFlag( ref flags, SaveFlag.xAttributes,		!m_AosAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.xWeaponAttributes,	!m_AosWeaponAttributes.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.PlayerConstructed,	m_PlayerConstructed );
			SetSaveFlag( ref flags, SaveFlag.SkillBonuses,		!m_AosSkillBonuses.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.Slayer2,			m_Slayer2 != SlayerName.None );
			SetSaveFlag( ref flags, SaveFlag.ElementalDamages,	!m_AosElementDamages.IsEmpty );
			SetSaveFlag( ref flags, SaveFlag.EngravedText,		!String.IsNullOrEmpty( m_EngravedText ) );

			writer.Write( (int) flags );

			if ( GetSaveFlag( flags, SaveFlag.DamageLevel ) )
				writer.Write( (int) m_DamageLevel );

			if ( GetSaveFlag( flags, SaveFlag.AccuracyLevel ) )
				writer.Write( (int) m_AccuracyLevel );

			if ( GetSaveFlag( flags, SaveFlag.DurabilityLevel ) )
				writer.Write( (int) m_DurabilityLevel );

			if ( GetSaveFlag( flags, SaveFlag.Quality ) )
				writer.Write( (int) m_Quality );

			if ( GetSaveFlag( flags, SaveFlag.Hits ) )
				writer.Write( (int) m_Hits );

			if ( GetSaveFlag( flags, SaveFlag.MaxHits ) )
				writer.Write( (int) m_MaxHits );

			if ( GetSaveFlag( flags, SaveFlag.Slayer ) )
				writer.Write( (int) m_Slayer );

			if ( GetSaveFlag( flags, SaveFlag.Poison ) )
				Poison.Serialize( m_Poison, writer );

			if ( GetSaveFlag( flags, SaveFlag.PoisonCharges ) )
				writer.Write( (int) m_PoisonCharges );

			if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
				writer.Write( (Mobile) m_Crafter );

			if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
				writer.Write( (int) m_StrReq );

			if ( GetSaveFlag( flags, SaveFlag.DexReq ) )
				writer.Write( (int) m_DexReq );

			if ( GetSaveFlag( flags, SaveFlag.IntReq ) )
				writer.Write( (int) m_IntReq );

			if ( GetSaveFlag( flags, SaveFlag.MinDamage ) )
				writer.Write( (int) m_MinDamage );

			if ( GetSaveFlag( flags, SaveFlag.MaxDamage ) )
				writer.Write( (int) m_MaxDamage );

			if ( GetSaveFlag( flags, SaveFlag.HitSound ) )
				writer.Write( (int) m_HitSound );

			if ( GetSaveFlag( flags, SaveFlag.MissSound ) )
				writer.Write( (int) m_MissSound );

			if ( GetSaveFlag( flags, SaveFlag.Speed ) )
				writer.Write( (float) m_Speed );

			if ( GetSaveFlag( flags, SaveFlag.MaxRange ) )
				writer.Write( (int) m_MaxRange );

			if ( GetSaveFlag( flags, SaveFlag.Skill ) )
				writer.Write( (int) m_Skill );

			if ( GetSaveFlag( flags, SaveFlag.Type ) )
				writer.Write( (int) m_Type );

			if ( GetSaveFlag( flags, SaveFlag.Animation ) )
				writer.Write( (int) m_Animation );

			if ( GetSaveFlag( flags, SaveFlag.Resource ) )
				writer.Write( (int) m_Resource );

			if ( GetSaveFlag( flags, SaveFlag.xAttributes ) )
				m_AosAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.xWeaponAttributes ) )
				m_AosWeaponAttributes.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
				m_AosSkillBonuses.Serialize( writer );

			if ( GetSaveFlag( flags, SaveFlag.Slayer2 ) )
				writer.Write( (int)m_Slayer2 );

			if( GetSaveFlag( flags, SaveFlag.ElementalDamages ) )
				m_AosElementDamages.Serialize( writer );

			if( GetSaveFlag( flags, SaveFlag.EngravedText ) )
				writer.Write( (string) m_EngravedText );



		}

		[Flags]
		private enum SaveFlag
		{
			None					= 0x00000000,
			DamageLevel				= 0x00000001,
			AccuracyLevel			= 0x00000002,
			DurabilityLevel			= 0x00000004,
			Quality					= 0x00000008,
			Hits					= 0x00000010,
			MaxHits					= 0x00000020,
			Slayer					= 0x00000040,
			Poison					= 0x00000080,
			PoisonCharges			= 0x00000100,
			Crafter					= 0x00000200,
			Identified				= 0x00000400,
			StrReq					= 0x00000800,
			DexReq					= 0x00001000,
			IntReq					= 0x00002000,
			MinDamage				= 0x00004000,
			MaxDamage				= 0x00008000,
			HitSound				= 0x00010000,
			MissSound				= 0x00020000,
			Speed					= 0x00040000,
			MaxRange				= 0x00080000,
			Skill					= 0x00100000,
			Type					= 0x00200000,
			Animation				= 0x00400000,
			Resource				= 0x00800000,
			xAttributes				= 0x01000000,
			xWeaponAttributes		= 0x02000000,
			PlayerConstructed		= 0x04000000,
			SkillBonuses			= 0x08000000,
			Slayer2					= 0x10000000,
			ElementalDamages		= 0x20000000,
			EngravedText			= 0x40000000
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 10:
					m_wear = reader.ReadInt();
					goto case 5;
				case 9:
				case 8:
				case 7:
				case 6:
				case 5:
				{
					SaveFlag flags = (SaveFlag)reader.ReadInt();

					if ( GetSaveFlag( flags, SaveFlag.DamageLevel ) )
					{
						m_DamageLevel = (WeaponDamageLevel)reader.ReadInt();

						if ( m_DamageLevel > WeaponDamageLevel.Vanq )
							m_DamageLevel = WeaponDamageLevel.Ruin;
					}

					if ( GetSaveFlag( flags, SaveFlag.AccuracyLevel ) )
					{
						m_AccuracyLevel = (WeaponAccuracyLevel)reader.ReadInt();

						if ( m_AccuracyLevel > WeaponAccuracyLevel.Supremely )
							m_AccuracyLevel = WeaponAccuracyLevel.Accurate;
					}

					if ( GetSaveFlag( flags, SaveFlag.DurabilityLevel ) )
					{
						m_DurabilityLevel = (WeaponDurabilityLevel)reader.ReadInt();

						if ( m_DurabilityLevel > WeaponDurabilityLevel.Indestructible )
							m_DurabilityLevel = WeaponDurabilityLevel.Durable;
					}

					if ( GetSaveFlag( flags, SaveFlag.Quality ) )
						m_Quality = (WeaponQuality)reader.ReadInt();
					else
						m_Quality = WeaponQuality.Regular;

					if ( GetSaveFlag( flags, SaveFlag.Hits ) )
						m_Hits = reader.ReadInt();

					if ( GetSaveFlag( flags, SaveFlag.MaxHits ) )
						m_MaxHits = reader.ReadInt();

					if ( GetSaveFlag( flags, SaveFlag.Slayer ) )
						m_Slayer = (SlayerName)reader.ReadInt();

					if ( GetSaveFlag( flags, SaveFlag.Poison ) )
						m_Poison = Poison.Deserialize( reader );

					if ( GetSaveFlag( flags, SaveFlag.PoisonCharges ) )
						m_PoisonCharges = reader.ReadInt();

					if ( GetSaveFlag( flags, SaveFlag.Crafter ) )
						m_Crafter = reader.ReadMobile();

					if ( GetSaveFlag( flags, SaveFlag.Identified ) )
						m_Identified = ( version >= 6 || reader.ReadBool() );

					if ( GetSaveFlag( flags, SaveFlag.StrReq ) )
						m_StrReq = reader.ReadInt();
					else
						m_StrReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.DexReq ) )
						m_DexReq = reader.ReadInt();
					else
						m_DexReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.IntReq ) )
						m_IntReq = reader.ReadInt();
					else
						m_IntReq = -1;

					if ( GetSaveFlag( flags, SaveFlag.MinDamage ) )
						m_MinDamage = reader.ReadInt();
					else
						m_MinDamage = -1;

					if ( GetSaveFlag( flags, SaveFlag.MaxDamage ) )
						m_MaxDamage = reader.ReadInt();
					else
						m_MaxDamage = -1;

					if ( GetSaveFlag( flags, SaveFlag.HitSound ) )
						m_HitSound = reader.ReadInt();
					else
						m_HitSound = -1;

					if ( GetSaveFlag( flags, SaveFlag.MissSound ) )
						m_MissSound = reader.ReadInt();
					else
						m_MissSound = -1;

					if ( GetSaveFlag( flags, SaveFlag.Speed ) )
					{
						if ( version < 9 )
							m_Speed = reader.ReadInt();
						else
							m_Speed = reader.ReadFloat();
					}
					else
						m_Speed = -1;

					if ( GetSaveFlag( flags, SaveFlag.MaxRange ) )
						m_MaxRange = reader.ReadInt();
					else
						m_MaxRange = -1;

					if ( GetSaveFlag( flags, SaveFlag.Skill ) )
						m_Skill = (SkillName)reader.ReadInt();
					else
						m_Skill = (SkillName)(-1);

					if ( GetSaveFlag( flags, SaveFlag.Type ) )
						m_Type = (WeaponType)reader.ReadInt();
					else
						m_Type = (WeaponType)(-1);

					if ( GetSaveFlag( flags, SaveFlag.Animation ) )
						m_Animation = (WeaponAnimation)reader.ReadInt();
					else
						m_Animation = (WeaponAnimation)(-1);

					if ( GetSaveFlag( flags, SaveFlag.Resource ) )
						m_Resource = (CraftResource)reader.ReadInt();
					else
						m_Resource = CraftResource.Iron;

					if ( GetSaveFlag( flags, SaveFlag.xAttributes ) )
						m_AosAttributes = new AosAttributes( this, reader );
					else
						m_AosAttributes = new AosAttributes( this );

					if ( GetSaveFlag( flags, SaveFlag.xWeaponAttributes ) )
						m_AosWeaponAttributes = new AosWeaponAttributes( this, reader );
					else
						m_AosWeaponAttributes = new AosWeaponAttributes( this );

					if ( UseSkillMod && m_AccuracyLevel != WeaponAccuracyLevel.Regular && Parent is Mobile )
					{
						m_SkillMod = new DefaultSkillMod( AccuracySkill, true, (int)m_AccuracyLevel * 5 );
						((Mobile)Parent).AddSkillMod( m_SkillMod );
					}

					if ( version < 7 && m_AosWeaponAttributes.MageWeapon != 0 )
						m_AosWeaponAttributes.MageWeapon = 30 - m_AosWeaponAttributes.MageWeapon;

					if ( Core.AOS && m_AosWeaponAttributes.MageWeapon != 0 && m_AosWeaponAttributes.MageWeapon != 30 && Parent is Mobile )
					{
						m_MageMod = new DefaultSkillMod( SkillName.Magery, true, -30 + m_AosWeaponAttributes.MageWeapon );
						((Mobile)Parent).AddSkillMod( m_MageMod );
					}

					if ( GetSaveFlag( flags, SaveFlag.PlayerConstructed ) )
						m_PlayerConstructed = true;

					if( GetSaveFlag( flags, SaveFlag.SkillBonuses ) )
						m_AosSkillBonuses = new AosSkillBonuses( this, reader );
					else
						m_AosSkillBonuses = new AosSkillBonuses( this );

					if( GetSaveFlag( flags, SaveFlag.Slayer2 ) )
						m_Slayer2 = (SlayerName)reader.ReadInt();

					if( GetSaveFlag( flags, SaveFlag.ElementalDamages ) )
						m_AosElementDamages = new AosElementAttributes( this, reader );
					else
						m_AosElementDamages = new AosElementAttributes( this );

					if( GetSaveFlag( flags, SaveFlag.EngravedText ) )
						m_EngravedText = reader.ReadString();

					break;
				}
				case 4:
				{
					m_Slayer = (SlayerName)reader.ReadInt();

					goto case 3;
				}
				case 3:
				{
					m_StrReq = reader.ReadInt();
					m_DexReq = reader.ReadInt();
					m_IntReq = reader.ReadInt();

					goto case 2;
				}
				case 2:
				{
					m_Identified = reader.ReadBool();

					goto case 1;
				}
				case 1:
				{
					m_MaxRange = reader.ReadInt();

					goto case 0;
				}
				case 0:
				{
					if ( version == 0 )
						m_MaxRange = 1; // default

					if ( version < 5 )
					{
						m_Resource = CraftResource.Iron;
						m_AosAttributes = new AosAttributes( this );
						m_AosWeaponAttributes = new AosWeaponAttributes( this );
						m_AosElementDamages = new AosElementAttributes( this );
						m_AosSkillBonuses = new AosSkillBonuses( this );
					}

					m_MinDamage = reader.ReadInt();
					m_MaxDamage = reader.ReadInt();

					m_Speed = reader.ReadInt();

					m_HitSound = reader.ReadInt();
					m_MissSound = reader.ReadInt();

					m_Skill = (SkillName)reader.ReadInt();
					m_Type = (WeaponType)reader.ReadInt();
					m_Animation = (WeaponAnimation)reader.ReadInt();
					m_DamageLevel = (WeaponDamageLevel)reader.ReadInt();
					m_AccuracyLevel = (WeaponAccuracyLevel)reader.ReadInt();
					m_DurabilityLevel = (WeaponDurabilityLevel)reader.ReadInt();
					m_Quality = (WeaponQuality)reader.ReadInt();

					m_Crafter = reader.ReadMobile();

					m_Poison = Poison.Deserialize( reader );
					m_PoisonCharges = reader.ReadInt();

					if ( m_StrReq == OldStrengthReq )
						m_StrReq = -1;

					if ( m_DexReq == OldDexterityReq )
						m_DexReq = -1;

					if ( m_IntReq == OldIntelligenceReq )
						m_IntReq = -1;

					if ( m_MinDamage == OldMinDamage )
						m_MinDamage = -1;

					if ( m_MaxDamage == OldMaxDamage )
						m_MaxDamage = -1;

					if ( m_HitSound == OldHitSound )
						m_HitSound = -1;

					if ( m_MissSound == OldMissSound )
						m_MissSound = -1;

					if ( m_Speed == OldSpeed )
						m_Speed = -1;

					if ( m_MaxRange == OldMaxRange )
						m_MaxRange = -1;

					if ( m_Skill == OldSkill )
						m_Skill = (SkillName)(-1);

					if ( m_Type == OldType )
						m_Type = (WeaponType)(-1);

					if ( m_Animation == OldAnimation )
						m_Animation = (WeaponAnimation)(-1);

					if ( UseSkillMod && m_AccuracyLevel != WeaponAccuracyLevel.Regular && Parent is Mobile && !(AdventuresFunctions.IsPuritain((object)Parent)))
					{
						m_SkillMod = new DefaultSkillMod( AccuracySkill, true, (int)m_AccuracyLevel * 5);
						((Mobile)Parent).AddSkillMod( m_SkillMod );
					}

					break;
				}
			}

			if ( Core.AOS && Parent is Mobile  && !(AdventuresFunctions.IsPuritain((object)Parent)))
				m_AosSkillBonuses.AddTo( (Mobile)Parent );

			int strBonus = m_AosAttributes.BonusStr;
			int dexBonus = m_AosAttributes.BonusDex;
			int intBonus = m_AosAttributes.BonusInt;

			if ( this.Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) && (Parent is PlayerMobile && !((PlayerMobile)Parent).SoulBound) && !(AdventuresFunctions.IsPuritain((object)Parent)) )
			{
				Mobile m = (Mobile)this.Parent;

				string modName = this.Serial.ToString();

				if ( strBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

				if ( dexBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

				if ( intBonus != 0 )
					m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
			}

			if ( Parent is Mobile )
				((Mobile)Parent).CheckStatTimers();

			if ( m_Hits <= 0 && m_MaxHits <= 0 )
			{
				m_Hits = m_MaxHits = Utility.RandomMinMax( InitMinHits, InitMaxHits );
			}

			if ( version < 6 )
				m_PlayerConstructed = true; // we don't know, so, assume it's crafted
		}
		#endregion

		public BaseWeapon( int itemID ) : base( itemID )
		{
			Layer = (Layer)ItemData.Quality;

			m_Quality = WeaponQuality.Regular;
			m_StrReq = -1;
			m_DexReq = -1;
			m_IntReq = -1;
			m_MinDamage = -1;
			m_MaxDamage = -1;
			m_HitSound = -1;
			m_MissSound = -1;
			m_Speed = -1;
			m_MaxRange = -1;
			m_Skill = (SkillName)(-1);
			m_Type = (WeaponType)(-1);
			m_Animation = (WeaponAnimation)(-1);

			m_Hits = m_MaxHits = Utility.RandomMinMax( InitMinHits, InitMaxHits );

			m_Resource = CraftResource.Iron;

			m_AosAttributes = new AosAttributes( this );
			m_AosWeaponAttributes = new AosWeaponAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_AosElementDamages = new AosElementAttributes( this );
		}

		public BaseWeapon( Serial serial ) : base( serial )
		{
		}

		private string GetNameString()
		{
			string name = this.Name;

			if ( name == null )
				name = String.Format( "#{0}", LabelNumber );

			return name;
		}

		[Hue, CommandProperty( AccessLevel.GameMaster )]
		public override int Hue
		{
			get{ return base.Hue; }
			set{ base.Hue = value; InvalidateProperties(); }
		}

		public int GetElementalDamageHue()
		{
			int phys, fire, cold, pois, nrgy, chaos, direct;
			GetDamageTypes( null, out phys, out fire, out cold, out pois, out nrgy, out chaos, out direct );
			//Order is Cold, Energy, Fire, Poison, Physical left

			int currentMax = 50;
			int hue = 0;

			if( pois >= currentMax )
			{
				hue = 1267 + (pois - 50) / 10;
				currentMax = pois;
			}

			if( fire >= currentMax )
			{
				hue = 1255 + (fire - 50) / 10;
				currentMax = fire;
			}

			if( nrgy >= currentMax )
			{
				hue = 1273 + (nrgy - 50) / 10;
				currentMax = nrgy;
			}

			if( cold >= currentMax )
			{
				hue = 1261 + (cold - 50) / 10;
				currentMax = cold;
			}

			return hue;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			int oreType;

			switch ( m_Resource )
			{
				case CraftResource.DullCopper:		oreType = 1053108; break; // dull copper
				case CraftResource.ShadowIron:		oreType = 1053107; break; // shadow iron
				case CraftResource.Copper:			oreType = 1053106; break; // copper
				case CraftResource.Bronze:			oreType = 1053105; break; // bronze
				case CraftResource.Gold:			oreType = 1053104; break; // golden
				case CraftResource.Agapite:			oreType = 1053103; break; // agapite
				case CraftResource.Verite:			oreType = 1053102; break; // verite
				case CraftResource.Valorite:		oreType = 1053101; break; // valorite
				case CraftResource.Nepturite:		oreType = 1036175; break; // nepturite
				case CraftResource.Obsidian:		oreType = 1036165; break; // obsidian
				case CraftResource.Steel:			oreType = 1036146; break; // steel
				case CraftResource.Brass:			oreType = 1036154; break; // brass
				case CraftResource.Mithril:			oreType = 1036139; break; // mithril
				case CraftResource.Xormite:			oreType = 1034439; break; // xormite
				case CraftResource.Dwarven:			oreType = 1036183; break; // dwarven
				case CraftResource.SpinedLeather:	oreType = 1061118; break; // deep sea
				case CraftResource.HornedLeather:	oreType = 1061117; break; // lizard
				case CraftResource.BarbedLeather:	oreType = 1061116; break; // serpent
				case CraftResource.NecroticLeather:	oreType = 1034413; break; // necrotic
				case CraftResource.VolcanicLeather:	oreType = 1034424; break; // volcanic
				case CraftResource.FrozenLeather:	oreType = 1034435; break; // frozen
				case CraftResource.GoliathLeather:	oreType = 1034380; break; // goliath
				case CraftResource.DraconicLeather:	oreType = 1034391; break; // draconic
				case CraftResource.HellishLeather:	oreType = 1034402; break; // hellish
				case CraftResource.DinosaurLeather:	oreType = 1036161; break; // dinosaur
				case CraftResource.AlienLeather:	oreType = 1034454; break; // alien
				case CraftResource.RedScales:		oreType = 1060814; break; // red
				case CraftResource.YellowScales:	oreType = 1060818; break; // yellow
				case CraftResource.BlackScales:		oreType = 1060820; break; // black
				case CraftResource.GreenScales:		oreType = 1060819; break; // green
				case CraftResource.WhiteScales:		oreType = 1060821; break; // white
				case CraftResource.BlueScales:		oreType = 1060815; break; // blue
				case CraftResource.AshTree: 		oreType = 1095399; break; // ash
				case CraftResource.CherryTree: 		oreType = 1095400; break; // cherry
				case CraftResource.EbonyTree: 		oreType = 1095401; break; // ebony
				case CraftResource.GoldenOakTree: 	oreType = 1095402; break; // gold oak
				case CraftResource.HickoryTree: 	oreType = 1095403; break; // hickory
				case CraftResource.MahoganyTree: 	oreType = 1095404; break; // mahogany
				case CraftResource.DriftwoodTree: 	oreType = 1095510; break; // driftwood
				case CraftResource.OakTree: 		oreType = 1095405; break; // oak
				case CraftResource.PineTree: 		oreType = 1095406; break; // pine
				case CraftResource.GhostTree: 		oreType = 1095513; break; // ghostwood
				case CraftResource.RosewoodTree: 	oreType = 1095407; break; // rosewood
				case CraftResource.WalnutTree: 		oreType = 1095408; break; // walnut
				case CraftResource.PetrifiedTree: 	oreType = 1095534; break; // petrified
				case CraftResource.ElvenTree: 		oreType = 1095537; break; // elven
				default: oreType = 0; break;
			}

			if ( oreType != 0 )
				list.Add( 1053099, "#{0}\t{1}", oreType, GetNameString() ); // ~1_oretype~ ~2_armortype~
			else if ( Name == null )
				list.Add( LabelNumber );
			else
				list.Add( Name );

			if ( !String.IsNullOrEmpty( m_EngravedText ) )
				list.Add( 1062613, m_EngravedText );

			if (Wear >0)
				list.Add("Wear and Tear: " + Wear + "%");

				/* list.Add( 1062613, Utility.FixHtml( m_EngravedText ) ); */
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			if ( base.AllowEquipedCast( from ) )
				return true;

			return ( m_AosAttributes.SpellChanneling != 0 );
		}

		public virtual int ArtifactRarity
		{
			get{ return 0; }
		}

		public virtual int GetLuckBonus()
		{
			CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

			if ( resInfo == null )
				return 0;

			CraftAttributeInfo attrInfo = resInfo.AttributeInfo;

			if ( attrInfo == null )
				return 0;

			return attrInfo.WeaponLuck;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			if ( m_AosSkillBonuses != null )
				m_AosSkillBonuses.GetProperties( list );

			if ( m_Quality == WeaponQuality.Exceptional )
				list.Add( 1060636 ); // exceptional

			if( RequiredRace == Race.Elf )
				list.Add( 1075086 ); // Elves Only

			if ( ArtifactRarity > 0 )
				list.Add( 1061078, ArtifactRarity.ToString() ); // artifact rarity ~1_val~

			if ( this is IUsesRemaining && ((IUsesRemaining)this).ShowUsesRemaining )
				list.Add( 1060584, ((IUsesRemaining)this).UsesRemaining.ToString() ); // uses remaining: ~1_val~

			if ( m_Poison != null && m_PoisonCharges > 0 )
				list.Add( 1062412 + m_Poison.Level, m_PoisonCharges.ToString() );

			if( m_Slayer != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer );
				if( entry != null )
					list.Add( entry.Title );
			}

			if( m_Slayer2 != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer2 );
				if( entry != null )
					list.Add( entry.Title );
			}


			base.AddResistanceProperties( list );

			int prop;

			if ( Core.ML && this is BaseRanged && ( (BaseRanged) this ).Balanced )
				list.Add( 1072792 ); // Balanced

			if ( (prop = m_AosWeaponAttributes.UseBestSkill) != 0 )
				list.Add( 1060400 ); // use best weapon skill

			if ( (prop = (GetDamageBonus() + m_AosAttributes.WeaponDamage)) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = (GetHitChanceBonus() + m_AosAttributes.AttackChance)) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitColdArea) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060416, prop.ToString() ); // hit cold area ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitDispel) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060417, prop.ToString() ); // hit dispel ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitEnergyArea) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060418, prop.ToString() ); // hit energy area ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitFireArea) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060419, prop.ToString() ); // hit fire area ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitFireball) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060420, prop.ToString() ); // hit fireball ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitHarm) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060421, prop.ToString() ); // hit harm ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitLeechHits) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060422, prop.ToString() ); // hit life leech ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitLightning) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060423, prop.ToString() ); // hit lightning ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitLowerAttack) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060424, prop.ToString() ); // hit lower attack ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitLowerDefend) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060425, prop.ToString() ); // hit lower defense ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitMagicArrow) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060426, prop.ToString() ); // hit magic arrow ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitLeechMana) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060427, prop.ToString() ); // hit mana leech ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitPhysicalArea) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060428, prop.ToString() ); // hit physical area ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitPoisonArea) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060429, prop.ToString() ); // hit poison area ~1_val~%

			if ( (prop = m_AosWeaponAttributes.HitLeechStam) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060430, prop.ToString() ); // hit stamina leech ~1_val~%

			if ( Core.ML && this is BaseRanged && ( prop = ( (BaseRanged) this ).Velocity ) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1072793, prop.ToString() ); // Velocity ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.BonusHits) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = GetLowerStatReq()) != 0 )
				list.Add( 1060435, prop.ToString() ); // lower requirements ~1_val~%

			if ( (prop = (GetLuckBonus() + m_AosAttributes.Luck)) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosWeaponAttributes.MageWeapon) != 0 )
				list.Add( 1060438, (30 - prop).ToString() ); // mage weapon -~1_val~ skill

			if ( (prop = m_AosAttributes.BonusMana) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )

			if ( (prop = m_AosAttributes.NightSight) != 0 && !(this is LightSword) && !(this is DoubleLaserSword) && !(this is LevelLaserSword) && !(this is LevelDoubleLaserSword) )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosWeaponAttributes.SelfRepair) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060450, prop.ToString() ); // self repair ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 && !(AdventuresFunctions.IsPuritain((object)this)))
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			int phys, fire, cold, pois, nrgy, chaos, direct;

			GetDamageTypes( null, out phys, out fire, out cold, out pois, out nrgy, out chaos, out direct );

			if ( phys != 0 )
				list.Add( 1060403, phys.ToString() ); // physical damage ~1_val~%

			if ( fire != 0 )
				list.Add( 1060405, fire.ToString() ); // fire damage ~1_val~%

			if ( cold != 0 )
				list.Add( 1060404, cold.ToString() ); // cold damage ~1_val~%

			if ( pois != 0 )
				list.Add( 1060406, pois.ToString() ); // poison damage ~1_val~%

			if ( nrgy != 0 )
				list.Add( 1060407, nrgy.ToString() ); // energy damage ~1_val

			if ( Core.ML && chaos != 0 )
				list.Add( 1072846, chaos.ToString() ); // chaos damage ~1_val~%

			if ( Core.ML && direct != 0 )
				list.Add( 1079978, direct.ToString() ); // Direct Damage: ~1_PERCENT~%

			list.Add( 1061168, "{0}\t{1}", MinDamage.ToString(), MaxDamage.ToString() ); // weapon damage ~1_val~ - ~2_val~

			if ( Core.ML )
				list.Add( 1061167, String.Format( "{0}s", Speed ) ); // weapon speed ~1_val~
			else
				list.Add( 1061167, Speed.ToString() );

			if ( MaxRange > 1 )
				list.Add( 1061169, MaxRange.ToString() ); // range ~1_val~

			int strReq = AOS.Scale( StrRequirement, 100 - GetLowerStatReq() );

			if ( strReq > 0 )
				list.Add( 1061170, strReq.ToString() ); // strength requirement ~1_val~

			if ( Layer == Layer.TwoHanded )
				list.Add( 1061171 ); // two-handed weapon
			else
				list.Add( 1061824 ); // one-handed weapon

			if ( Core.SE || m_AosWeaponAttributes.UseBestSkill == 0 )
			{
				switch ( Skill )
				{
					case SkillName.Swords:  list.Add( 1061172 ); break; // skill required: swordsmanship
					case SkillName.Macing:  list.Add( 1061173 ); break; // skill required: mace fighting
					case SkillName.Fencing: list.Add( 1061174 ); break; // skill required: fencing
					case SkillName.Archery: 
						if ( this is Harpoon || this is LevelHarpoon || this is GiftHarpoon || this is BaseWizardStaff || this is BaseLevelStave || this is BaseGiftStave || this is ThrowingGloves || this is GiftThrowingGloves || this is LevelThrowingGloves ){ list.Add( "skill required: marksmanship" ); }
						else { list.Add( 1061175 ); } 
						break; // skill required: archery
					case SkillName.Wrestling: list.Add( "skill required: wrestling" ); break; // skill required: wrestling
				}
			}

			if ( m_Hits >= 0 && m_MaxHits > 0 )
				list.Add( 1060639, "{0}\t{1}", m_Hits, m_MaxHits ); // durability ~1_val~ / ~2_val~

			//if (Wear > 0 )
			//	list.Add("This item has lost "+ Wear + "% of its effectiveness due to wear and tear.");
		}

		public void WearAndTear( int severity )
		{
			if (Wear > 100 || m_MaxHits == 0)
				return;

			if ( (100 - Wear) < severity )
				severity = 100-Wear;

			double repair = (double)m_Hits / (double)m_MaxHits; //0 is damaged, 1 is in good repair, check condition of repair
			repair += 0.50 * ((double)m_Hits/255); // bonus for items with high durability e.g. 255 versus 60.  
			
			//at this point a weapon at 90% durability value with 100 hits would equal 1.29, impossible to have wear
			//wep with 20% repair and 50 hits would equal .298
			
			//check if wear actually happens based on the state of repair.
			if ( (Utility.RandomDouble()/1.5) > repair )
			/*
			this.m_MaxHits = (int)( this.m_MaxHits - ( (int)( (double)this.m_MaxHits * 0.04 ) * severity ) ); // hitpoints go down for each injury
			
			int whichresist = Utility.RandomMinMax(1, 6); // which resist will be reduced, 1/6 chance of all 5 being reduced 
			
			if (m_AosWeaponAttributes.ResistPhysicalBonus > 0 && (whichresist == 1 || whichresist == 6))
				m_AosWeaponAttributes.ResistPhysicalBonus = (int)( (double)m_AosWeaponAttributes.ResistPhysicalBonus
			if (this.FireResistSeed < 80 && (whichresist == 2 || whichresist == 6))
				this.SetResistance( ResistanceType.Fire, (int)((double)this.FireResistSeed *(0.5 * severity)) );
			if (this.ColdResistSeed < 80 && (whichresist == 3 || whichresist == 6))
				this.SetResistance( ResistanceType.Cold, (int)((double)this.ColdResistSeed *(0.5 * severity)) );
			if (this.PoisonResistSeed < 80 && (whichresist == 4 || whichresist == 6))
				this.SetResistance( ResistanceType.Poison, (int)((double)this.PoisonResistSeed *(0.5 * severity)) );
			if (this.EnergyResistSeed < 80 && (whichresist == 5 || whichresist == 6))
				this.SetResistance( ResistanceType.Energy, (int)((double)this.EnergyResistSeed *(0.5 * severity)) );
			
			int which = Utility.RandomMinMax(1, 4); // which stat will be reduced, 1/4 chance of all 3 reducing
			
			if (which == 1 || which ==4)
				this.RawStr = (int)( this.RawStr - ( (int)( (double)this.RawStr * 0.04 ) * severity ) );
			else if (which == 2 || which ==4)
				this.RawInt = (int)( this.RawInt - ( (int)( (double)this.RawInt * 0.04 ) * severity ) );
			else if (which == 3 || which ==4)
				this.RawDex = (int)( this.RawDex - ( (int)( (double)this.RawDex * 0.04 ) * severity ) );

/*
m_Hits

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
		
		Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
			SkillBonuses.SetValues( 0, SkillName.EvalInt, 10 );
			SkillBonuses.SetValues( 1, SkillName.Magery, 10 );
			SkillBonuses.SetValues( 2, SkillName.MagicResist, 10 );
			SkillBonuses.SetValues( 3, SkillName.Meditation, 10 );
			Attributes.RegenMana = 10;
			Attributes.BonusInt = 10;
			Attributes.SpellChanneling = 1;

*/

			Wear += severity;

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

			if ( m_Quality == WeaponQuality.Exceptional )
				attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );

			if ( m_Identified || from.AccessLevel >= AccessLevel.GameMaster )
			{
				if( m_Slayer != SlayerName.None )
				{
					SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer );
					if( entry != null )
						attrs.Add( new EquipInfoAttribute( entry.Title ) );
				}

				if( m_Slayer2 != SlayerName.None )
				{
					SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer2 );
					if( entry != null )
						attrs.Add( new EquipInfoAttribute( entry.Title ) );
				}

				if ( m_DurabilityLevel != WeaponDurabilityLevel.Regular )
					attrs.Add( new EquipInfoAttribute( 1038000 + (int)m_DurabilityLevel ) );

				if ( m_DamageLevel != WeaponDamageLevel.Regular )
					attrs.Add( new EquipInfoAttribute( 1038015 + (int)m_DamageLevel ) );

				if ( m_AccuracyLevel != WeaponAccuracyLevel.Regular )
					attrs.Add( new EquipInfoAttribute( 1038010 + (int)m_AccuracyLevel ) );
			}
			else if( m_Slayer != SlayerName.None || m_Slayer2 != SlayerName.None || m_DurabilityLevel != WeaponDurabilityLevel.Regular || m_DamageLevel != WeaponDamageLevel.Regular || m_AccuracyLevel != WeaponAccuracyLevel.Regular )
				attrs.Add( new EquipInfoAttribute( 1038000 ) ); // Unidentified

			if ( m_Poison != null && m_PoisonCharges > 0 )
				attrs.Add( new EquipInfoAttribute( 1017383, m_PoisonCharges ) );

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

		private static BaseWeapon m_Fists; // This value holds the default--fist--weapon

		public static BaseWeapon Fists
		{
			get{ return m_Fists; }
			set{ m_Fists = value; }
		}

		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (WeaponQuality)quality;

			if ( makersMark )
				Crafter = from;

			PlayerConstructed = true;

			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			if ( Core.AOS )
			{
				Resource = CraftResources.GetFromType( resourceType );

				CraftContext context = craftSystem.GetContext( from );

				if ( context != null && context.DoNotColor )
					Hue = 0;

				if ( tool is BaseRunicTool )
					((BaseRunicTool)tool).ApplyAttributesTo( this );

				if ( Quality == WeaponQuality.Exceptional )
				{
					if ( Attributes.WeaponDamage > 35 )
						Attributes.WeaponDamage -= 20;
					else
						Attributes.WeaponDamage = 15;

					if( Core.ML )
					{
						Attributes.WeaponDamage += (int)(from.Skills.ArmsLore.Value / 20);

						if ( Attributes.WeaponDamage > MaxWeaponDamage() )
							Attributes.WeaponDamage = MaxWeaponDamage();

						from.CheckSkill( SkillName.ArmsLore, 0, 100 );
					}
				}
			}
			else if ( tool is BaseRunicTool )
			{
				CraftResource thisResource = CraftResources.GetFromType( resourceType );

				if ( thisResource == ((BaseRunicTool)tool).Resource )
				{
					Resource = thisResource;

					CraftContext context = craftSystem.GetContext( from );

					if ( context != null && context.DoNotColor )
						Hue = 0;

					switch ( thisResource )
					{
						case CraftResource.DullCopper:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Durable;
							AccuracyLevel = WeaponAccuracyLevel.Accurate;
							break;
						}
						case CraftResource.ShadowIron:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Durable;
							DamageLevel = WeaponDamageLevel.Ruin;
							break;
						}
						case CraftResource.Copper:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Fortified;
							DamageLevel = WeaponDamageLevel.Ruin;
							AccuracyLevel = WeaponAccuracyLevel.Surpassingly;
							break;
						}
						case CraftResource.Bronze:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Fortified;
							DamageLevel = WeaponDamageLevel.Might;
							AccuracyLevel = WeaponAccuracyLevel.Surpassingly;
							break;
						}
						case CraftResource.Gold:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Force;
							AccuracyLevel = WeaponAccuracyLevel.Eminently;
							break;
						}
						case CraftResource.Agapite:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Power;
							AccuracyLevel = WeaponAccuracyLevel.Eminently;
							break;
						}
						case CraftResource.Verite:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Power;
							AccuracyLevel = WeaponAccuracyLevel.Exceedingly;
							break;
						}
						case CraftResource.Valorite:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.Nepturite:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.Obsidian:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.Steel:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.Mithril:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.Xormite:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.Dwarven:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.AshTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Durable;
							AccuracyLevel = WeaponAccuracyLevel.Accurate;
							break;
						}
						case CraftResource.CherryTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Durable;
							DamageLevel = WeaponDamageLevel.Ruin;
							AccuracyLevel = WeaponAccuracyLevel.Accurate;
							break;
						}
						case CraftResource.EbonyTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Durable;
							DamageLevel = WeaponDamageLevel.Might;
							AccuracyLevel = WeaponAccuracyLevel.Accurate;
							break;
						}
						case CraftResource.GoldenOakTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Durable;
							DamageLevel = WeaponDamageLevel.Might;
							AccuracyLevel = WeaponAccuracyLevel.Surpassingly;
							break;
						}
						case CraftResource.HickoryTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Fortified;
							DamageLevel = WeaponDamageLevel.Force;
							AccuracyLevel = WeaponAccuracyLevel.Surpassingly;
							break;
						}
						case CraftResource.MahoganyTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Fortified;
							DamageLevel = WeaponDamageLevel.Force;
							AccuracyLevel = WeaponAccuracyLevel.Surpassingly;
							break;
						}
						case CraftResource.DriftwoodTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Fortified;
							DamageLevel = WeaponDamageLevel.Force;
							AccuracyLevel = WeaponAccuracyLevel.Surpassingly;
							break;
						}
						case CraftResource.OakTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Power;
							AccuracyLevel = WeaponAccuracyLevel.Eminently;
							break;
						}
						case CraftResource.PineTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Power;
							AccuracyLevel = WeaponAccuracyLevel.Eminently;
							break;
						}
						case CraftResource.GhostTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Power;
							AccuracyLevel = WeaponAccuracyLevel.Eminently;
							break;
						}
						case CraftResource.RosewoodTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Exceedingly;
							break;
						}
						case CraftResource.WalnutTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.PetrifiedTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
						case CraftResource.ElvenTree:
						{
							Identified = true;
							DurabilityLevel = WeaponDurabilityLevel.Indestructible;
							DamageLevel = WeaponDamageLevel.Vanq;
							AccuracyLevel = WeaponAccuracyLevel.Supremely;
							break;
						}
					}
				}
			}

			return quality;
		}
		public static int MaxWeaponDamage() {
			return 45;
		}
		#endregion
	}

	public enum CheckSlayerResult
	{
		None,
		Slayer,
		Opposition
	}
}
