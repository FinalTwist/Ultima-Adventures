using System;
using System.Collections;
using Server;
using Server.Items;
using System.Collections.Generic;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using System.Reflection;
using System.Text.RegularExpressions;
using Server.Engines.Soulbound;
using Server.Network;
using System.ComponentModel;

namespace Server.Items
{
	public enum EssenceType : byte
	{
		Power = 0,
		Regular = 1,
		Channeling = 2,
		Luck = 3
	}
	[TypeAlias( "Scripts.Engines.Soul.Phylactery" )]
	public class Phylactery : Item
	{
		private int m_SoulForceSpent;
		private int m_BaseSoulForceEssenceCost;
		private int m_BaseSoulForcePowerCost;
		private int m_BaseSoulForceLuckCost;
		private int m_FireEssenceMax;
		private ResistanceMod m_FireMod;
		private int m_ColdEssenceMax;
		private ResistanceMod m_ColdMod;
		private int m_PhysicalEssenceMax;
		private ResistanceMod m_EnergyMod;
		private int m_EnergyEssenceMax;
		private ResistanceMod m_PoisonMod;
		private int m_ScorpionEssenceMax;
		private int m_CurrentSkillCap;
		private int m_SageEssenceMax;
		private int m_ThornEssenceMax;
		private int m_DemonicEssenceMax;
		private int m_WaterEssenceMax;
		private int m_PlantEssenceMax;

		private int m_VampireEssenceMax;
		private int m_SpringEssenceMax;
		private int m_SacredEssenceMax;
		private int m_BearEssenceMax;
		private int m_EagleEssenceMax;
		private int m_EarthEssenceMax;
		private ResistanceMod m_PhysicalMod;
		private bool m_Temporary;
		private int m_ColdEssence; 

		[CommandProperty( AccessLevel.GameMaster )]
		public int SoulForceSpent
		{
			get{ return m_SoulForceSpent; }
			set{ m_SoulForceSpent = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int BaseSoulForceEssenceCost
		{
			get{ return m_BaseSoulForceEssenceCost; }
			set{ m_BaseSoulForceEssenceCost = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int BaseSoulForcePowerCost
		{
			get{ return m_BaseSoulForcePowerCost; }
			set{ m_BaseSoulForcePowerCost = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int BaseSoulForceLuckCost
		{
			get{ return m_BaseSoulForceLuckCost; }
			set{ m_BaseSoulForceLuckCost = value; }
		}

		public bool Temporary {
			get{ return m_Temporary; }
			set{ m_Temporary = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ResistanceMod FireMod
		{
			get{ return m_FireMod; }
			set{ m_FireMod = value; }
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public ResistanceMod ColdMod
		{
			get{ return m_ColdMod; }
			set{ m_ColdMod = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ResistanceMod EnergyMod
		{
			get{ return m_EnergyMod; }
			set{ m_EnergyMod = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ResistanceMod PoisonMod
		{
			get{ return m_PoisonMod; }
			set{ m_PoisonMod = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ResistanceMod PhysicalMod
		{
			get{ return m_PhysicalMod; }
			set{ m_PhysicalMod = value; }
		}
		[Description("Cold Essence")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int ColdEssence{ get{ return m_ColdEssence; } set{ m_ColdEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ColdEssenceMax{ get{ return m_ColdEssenceMax; } set{ m_ColdEssenceMax = value; InvalidateProperties(); } }

		private int m_PhysicalEssence;

		[Description("Physical Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int PhysicalEssence{ get{ return m_PhysicalEssence; } set{ m_PhysicalEssence = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PhysicalEssenceMax{ get{ return m_PhysicalEssenceMax; } set{ m_PhysicalEssenceMax = value; } }

		private int m_EnergyEssence;

		[Description("Energy Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int EnergyEssence{ get{ return m_EnergyEssenceMax; } set{ m_EnergyEssenceMax = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int EnergyEssenceMax{ get{ return m_EnergyEssence; } set{ m_EnergyEssence = value; InvalidateProperties(); } }

		private int m_ScorpionEssence;

		[Description("Scorpion Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int ScorpionEssence{ get{ return m_ScorpionEssenceMax; } set{ m_ScorpionEssenceMax = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ScorpionEssenceMax{ get{ return m_ScorpionEssence; } set{ m_ScorpionEssence = value; InvalidateProperties(); } }

		private int m_OwlEssence;

		[Description("Owl Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int OwlEssence{ get{ return m_OwlEssence; } set{ m_OwlEssence = value; InvalidateProperties(); } }

		private int m_OwlEssenceMax;

		[CommandProperty( AccessLevel.GameMaster )]
		public int OwlEssenceMax{ get{ return m_OwlEssenceMax; } set{ m_OwlEssenceMax = value; InvalidateProperties(); } }

		private int m_SageEssence;

		[Description("Sage Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int SageEssence{ get{ return m_SageEssence; } set{ m_SageEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int SageEssenceMax{ get{ return m_SageEssenceMax; } set{ m_SageEssenceMax = value; InvalidateProperties(); } }

		private int m_TitanEssenceMax;
		private int m_TitanEssence;

		[Description("Titan Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int TitanEssence{ get{ return m_TitanEssence; } set{ m_TitanEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int TitanEssenceMax{ get{ return m_TitanEssenceMax; } set{ m_TitanEssenceMax = value; InvalidateProperties(); } }

		private int m_GazerEssenceMax;
		private int m_GazerEssence;

		[Description("Gazer Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int GazerEssence{ get{ return m_GazerEssence; } set{ m_GazerEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int GazerEssenceMax{ get{ return m_GazerEssenceMax; } set{ m_GazerEssenceMax = value; InvalidateProperties(); } }

		private int m_HorseEssenceMax;
		private int m_HorseEssence;

		[Description("Horse Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int HorseEssence{ get{ return m_HorseEssence; } set{ m_HorseEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int HorseEssenceMax{ get{ return m_HorseEssenceMax; } set{ m_HorseEssenceMax = value; InvalidateProperties(); } }

		private int m_SnakeEssenceMax;
		private int m_SnakeEssence;

		[Description("Snake Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int SnakeEssence{ get{ return m_SnakeEssence; } set{ m_SnakeEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int SnakeEssenceMax{ get{ return m_SnakeEssenceMax; } set{ m_SnakeEssenceMax = value; InvalidateProperties(); } }

		private int m_ImpEssenceMax;
		private int m_ImpEssence;

		[Description("Imp Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int ImpEssence{ get{ return m_ImpEssence; } set{ m_ImpEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ImpEssenceMax{ get{ return m_ImpEssenceMax; } set{ m_ImpEssenceMax = value; InvalidateProperties(); } }

		private int m_LuckyEssenceMax;
		private int m_LuckyEssence;

		[Description("Lucky Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int LuckyEssence{ get{ return m_LuckyEssence; } set{ m_LuckyEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int LuckyEssenceMax{ get{ return m_LuckyEssenceMax; } set{ m_LuckyEssenceMax = value; InvalidateProperties(); } }

		private int m_PixieEssenceMax;
		private int m_PixieEssence;

		[Description("Pixie Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int PixieEssence{ get{ return m_PixieEssence; } set{ m_PixieEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PixieEssenceMax{ get{ return m_PixieEssenceMax; } set{ m_PixieEssenceMax = value; InvalidateProperties(); } }

		private int m_PlanarEssenceMax;
		private int m_PlanarEssence;

		[Description("Planar Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int PlanarEssence{ get{ return m_PlanarEssence; } set{ m_PlanarEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PlanarEssenceMax{ get{ return m_PlanarEssenceMax; } set{ m_PlanarEssenceMax = value; InvalidateProperties(); } }

		private int m_CelestialEssenceMax;
		private int m_CelestialEssence;

		[Description("Celestial Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int CelestialEssence{ get{ return m_CelestialEssence; } set{ m_CelestialEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int CelestialEssenceMax{ get{ return m_CelestialEssenceMax; } set{ m_CelestialEssenceMax = value; InvalidateProperties(); } }

		private int m_ThornEssence;

		[Description("Thorn Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int ThornEssence{ get{ return m_ThornEssence; } set{ m_ThornEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int ThornEssenceMax{ get{ return m_ThornEssenceMax; } set{ m_ThornEssenceMax = value; InvalidateProperties(); } }

		private int m_DemonicEssence;

		[Description("Demonic Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int DemonicEssence{ get{ return m_DemonicEssence; } set{ m_DemonicEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int DemonicEssenceMax{ get{ return m_DemonicEssenceMax; } set{ m_DemonicEssenceMax = value; InvalidateProperties(); } }

		private int m_WaterEssence;

		[Description("Water Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int WaterEssence{ get{ return m_WaterEssence; } set{ m_WaterEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int WaterEssenceMax{ get{ return m_WaterEssenceMax; } set{ m_WaterEssenceMax = value; InvalidateProperties(); } }

		private int m_BearEssence;

		[Description("Bear Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int BearEssence{ get{ return m_BearEssence; } set{ m_BearEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int BearEssenceMax{ get{ return m_BearEssenceMax; } set{ m_BearEssenceMax = value; InvalidateProperties(); } }

		private int m_EagleEssence;

		[Description("Eagle Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int EagleEssence{ get{ return m_EagleEssence; } set{ m_EagleEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int EagleEssenceMax{ get{ return m_EagleEssenceMax; } set{ m_EagleEssenceMax = value; InvalidateProperties(); } }

		private int m_EarthEssence;

		[Description("Earth Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int EarthEssence{ get{ return m_EarthEssence; } set{ m_EarthEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int EarthEssenceMax{ get{ return m_EarthEssenceMax; } set{ m_EarthEssenceMax = value; InvalidateProperties(); } }

		private int m_FireEssence;

		[Description("Fire Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int FireEssence{ get{ return m_FireEssence; } set{ m_FireEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int FireEssenceMax{ get{ return m_FireEssenceMax; } set{ m_FireEssenceMax = value; InvalidateProperties(); } }

		private int m_PowerLevel;
		private int m_PowerLevelMax;

		[CommandProperty( AccessLevel.GameMaster )]
		public int PowerLevel{ get{ return m_PowerLevel; } set{ m_PowerLevel = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PowerLevelMax{ get{ return m_PowerLevelMax; } set{ m_PowerLevelMax = value; InvalidateProperties(); } }

		private int m_PlantEssence;

		[Description("Plant Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int PlantEssence{ get{ return m_PlantEssence; } set{ m_PlantEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int PlantEssenceMax{ get{ return m_PlantEssenceMax; } set{ m_PlantEssenceMax = value; InvalidateProperties(); } }

		private int m_VampireEssence;
		[Description("Vammpire Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int VampireEssence{ get{ return m_VampireEssence; } set{ m_VampireEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int VampireEssenceMax{ get{ return m_VampireEssenceMax; } set{ m_VampireEssenceMax = value; InvalidateProperties(); } }

		private int m_SpringEssence;
		[Description("Spring Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int SpringEssence{ get{ return m_SpringEssence; } set{ m_SpringEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpringEssenceMax{ get{ return m_SpringEssenceMax; } set{ m_SpringEssenceMax = value; InvalidateProperties(); } }

		private int m_SacredEssence;
		[Description("Sacred Essence"),Category("Appearance")]
		[CommandProperty( AccessLevel.GameMaster )]
		public int SacredEssence{ get{ return m_SacredEssence; } set{ m_SacredEssence = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int SacredEssenceMax{ get{ return m_SacredEssenceMax; } set{ m_SacredEssenceMax = value; InvalidateProperties(); } }


		private AosAttributes m_AosAttributes;

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		private List<PhylacteryMod> m_PhylacteryMods;

		public List<PhylacteryMod> PhylacteryMods {
			get { return m_PhylacteryMods; }
			set { m_PhylacteryMods = value; }
		}

		private PlayerMobile m_Owner;
		public PlayerMobile Owner {
			get { return m_Owner; }
			set { m_Owner = value; }
		}

		[Constructable]
		public Phylactery() : base( 0x09A8 )
		{
			LootType = LootType.Ensouled; 
			Init();
		}

		private void Init() {
			SoulForceSpent = 0;
			BaseSoulForcePowerCost = MyServerSettings.SoulForceMin() + 2000;
			BaseSoulForceEssenceCost = MyServerSettings.SoulForceMin();
			BaseSoulForceLuckCost = MyServerSettings.SoulForceMin() + 500;
			m_AosAttributes = new AosAttributes( this ); 
			PhylacteryMods = new List<PhylacteryMod>();
			Temporary = false;
			Hue = 2222;
			Weight = 1.0;	
			
			Movable = true;
			PowerLevel = 1;
			PowerLevelMax = 10;
			// physical resist
			EarthEssence = 0;
			// fire resist
			FireEssence = 0;
			// cold resist
			ColdEssence = 0;
			// hit chance and bonus dex
			EagleEssence = 0;
			// hit regen and bonus hit
			PlantEssence = 0;
			//hit life leech
			VampireEssence = 0;
			// hit stam leech
			SpringEssence = 0;
			// hit mana leech
			SacredEssence = 0;
			// energy resist
			EnergyEssence = 0;
			// lrc
			SageEssence = 0;
			// bonus str and weapon damage
			TitanEssence = 0;
			// bonus int
			GazerEssence = 0;
			// poison resist
			ScorpionEssence = 0;
			// defend chance and bonus stam
			BearEssence = 0;
			// mana regen and bonus mana
			WaterEssence = 0;
			// ReflectPhysical
			ThornEssence = 0;
			// knowledge cap?
			OwlEssence = 0;
			// spell damage
			DemonicEssence = 0;
			// regen stamina
			HorseEssence = 0;
			// weapon speed
			SnakeEssence = 0;
			// luck
			LuckyEssence = 0;
			// spell channelling
			PixieEssence = 0;
			// cast recovery
			PlanarEssence = 0;
			// cast speed
			CelestialEssence = 0;

			// enhance potions
			ImpEssence = 0;

			// attributes
			// imp essence
			Attributes.EnhancePotions = 0;
			// planar essence
			Attributes.CastRecovery = 0;
			// celestial essence
			Attributes.CastSpeed = 0;
			// eagle essence
			Attributes.AttackChance = 0;
			// eagle essence / 1.8
			Attributes.BonusDex = 0;
			// plant essence
			Attributes.RegenHits = 0;
			// plant essence / 1.4
			Attributes.BonusHits = 0;
			// gazer essence
			Attributes.LowerManaCost = 0;

			// sage essence
			Attributes.LowerRegCost = 0;

			// lucky essence
			Attributes.Luck = 0;

			// water essence
			Attributes.RegenMana = 0;
			// water essence / 1.4
			Attributes.BonusMana = 0;

			// combine with owl essence once skill caps figured out
			Attributes.NightSight = 0;
			// thorn essence
			Attributes.ReflectPhysical = 0;
			// horse essence
			Attributes.RegenStam = 0;
			// faery essence
			Attributes.SpellChanneling = 0;
			// demon essence
			Attributes.SpellDamage = 0;
			// demon essence / 4
			Attributes.BonusInt = 0;

			// titan essence
			Attributes.WeaponDamage = 0;
			// titan essence / 4
			Attributes.BonusStr = 0;
			// snake essence
			Attributes.WeaponSpeed = 0;
			// bear essence
			Attributes.DefendChance = 0;
			// bear essence / 1.8
			Attributes.BonusStam = 0;

			SetAttributeCaps();
		}

		public override int LabelNumber{ get{ return 1061183; } } // a phylactery

		public int PhylacteryPowerModifier(int value) {
			// for things that are low (e.g cast speed) double its scale with power
			double powerModifier = (value < 5) ? 5.0 : 10.0;
			double modifier = (double)PowerLevel/10.0;
			double calculated = (double)value*modifier;	
			return (int)calculated;
		}

		public int GetRawPoints(Mobile from) {
			// 1218 for a?
			int rawPoints = MyServerSettings.RegenManaCap() + MyServerSettings.RegenStamCap();
			rawPoints += MyServerSettings.RegenHitsCap() + MyServerSettings.SpellDamage();
			// stam regen
			rawPoints += MyServerSettings.LowerManaCostCap(); // 40
			rawPoints += MyServerSettings.LowerReagentCostCap();
			rawPoints += MyServerSettings.CastRecoveryCap() + MyServerSettings.CastSpeedCap();
			rawPoints += MyServerSettings.HitChanceCap() + MyServerSettings.DefendChanceCap();
			rawPoints += MyServerSettings.ReflectDamageCap() + MyServerSettings.WeaponSpeedCap();
			rawPoints += from.GetMaxResistance(ResistanceType.Poison) + from.GetMaxResistance(ResistanceType.Fire);
			rawPoints += from.GetMaxResistance(ResistanceType.Cold) + from.GetMaxResistance(ResistanceType.Energy);
			rawPoints += from.GetMaxResistance(ResistanceType.Physical);
			rawPoints += MyServerSettings.PhylacterySkillStatGainCap();
			rawPoints += MyServerSettings.DamageIncreaseCap() + 1; // static value of pixieValue (spell channelling);
			rawPoints += 50;

			return rawPoints;
		}

		public void ValidateSoulForceCosts() {
			if (BaseSoulForceEssenceCost < MyServerSettings.SoulForceMin()) {
				BaseSoulForceEssenceCost = MyServerSettings.SoulForceMin() ;
			} else if (BaseSoulForceEssenceCost > MyServerSettings.SoulForceCap()) {
				BaseSoulForceEssenceCost = MyServerSettings.SoulForceCap();
			}
			if (BaseSoulForceLuckCost < MyServerSettings.SoulForceMin() + 500) {
				BaseSoulForceLuckCost = MyServerSettings.SoulForceMin() + 500;
			} else if (BaseSoulForceLuckCost > MyServerSettings.SoulForceCap()) {
				BaseSoulForceLuckCost = MyServerSettings.SoulForceCap();
			}
			if (BaseSoulForcePowerCost < MyServerSettings.SoulForceMin() + 2000) {
				BaseSoulForcePowerCost = MyServerSettings.SoulForceMin() + 2000;
			} else if (BaseSoulForcePowerCost > MyServerSettings.SoulForceCap()) {
				BaseSoulForcePowerCost = MyServerSettings.SoulForceCap();
			}
		}

		public void UpdateSoulForceCost(string propertyName, int soulForceCost, bool increase) {
			PropertyInfo property = typeof(Phylactery).GetProperty(propertyName);
			if (property != null) {
				int value = (int)property.GetValue(this);
				if (increase) {
					value += soulForceCost;
				} else {
					value -= soulForceCost;
				}
				property.SetValue(this, value);
				this.ValidateSoulForceCosts();
			}
		}

		public int SoulForceFraction(Mobile from, string propertyName) {
			double percentage = 1.001250;
			int soulForceFraction = 0;
			PropertyInfo property = typeof(Phylactery).GetProperty(propertyName);
			if (property != null) {
				int newSoulForceAmount = 0;
				int soulForceCost = (int)property.GetValue(this);
				switch (propertyName) {
					case "BaseSoulForcePowerCost":
						if (PowerLevel < PowerLevelMax) {
							percentage = 1.30;
	 					} 
					break;
					case "BaseSoulForceEssenceCost":
						int totalPower = (this.TotalPower() - LuckyEssence);
						if (totalPower < this.GetRawPoints(from)) {
							double multiplier = 10.0;
							if (totalPower > 600) {
								multiplier = 6.5;
							} else if (totalPower > 1000) {
							 	multiplier = 4.0;
							} 
							percentage += (double)(totalPower/((MyServerSettings.FameCap()/this.GetRawPoints(from))*multiplier))/10000;
						}
					break;
					case "BaseSoulForceLuckCost":
						if (LuckyEssence < LuckyEssenceMax) {
							percentage = 1.0015;
							if (LuckyEssence > 500) {
								// modifier += ((LuckyEssenceMax - LuckyEssence)*0.02);
								percentage = 1.025;
							}
						}
					break;
				}
				soulForceFraction = (int)(Math.Ceiling(soulForceCost*percentage) - soulForceCost);
			}
			return soulForceFraction;
		}

		public int SoulForceRequiredForGain(Mobile from, string propertyName) {
			int soulForceCost = 0;
			PropertyInfo property = typeof(Phylactery).GetProperty(propertyName);
			if (property != null) {
				soulForceCost = (int)property.GetValue(this);
				int soulForceFraction = this.SoulForceFraction(from, propertyName);
				soulForceCost += soulForceFraction;
			}
			return soulForceCost;			
		}

		public double CalculateSkillGainModifier() {
			return (double)OwlEssence/5;
		}

		// x = ((y * 2.5)/5)
		public double CalculateStatGainModifier(double baseStatGainModifier) {
			return (double)(baseStatGainModifier - (((double)OwlEssence*3.6)/5));
		}


		public int CalculateExtraGold(int baseGoldAmount) {
			int goldFind = 0;
			if (ImpEssence > 0) {
				goldFind = ImpEssence + Utility.RandomMinMax(10,50);
			}
			return (int)Math.Ceiling((double)(baseGoldAmount/100)*goldFind);
		}

		public void RefreshItem(Mobile from) {
			if (PowerLevel > PowerLevelMax) {
				m_PowerLevel = m_PowerLevelMax; 
			}
			SetAttributeCaps();
			SetResistanceCaps(from);
			InvalidateProperties();
		}

		public void SetAttributeCaps() {
			WaterEssenceMax = PhylacteryPowerModifier(MyServerSettings.RegenManaCap());
			HorseEssenceMax = PhylacteryPowerModifier(MyServerSettings.RegenStamCap());
			PlantEssenceMax =  PhylacteryPowerModifier(MyServerSettings.RegenHitsCap());
			DemonicEssenceMax =  PhylacteryPowerModifier(MyServerSettings.SpellDamage());
			EagleEssenceMax = PhylacteryPowerModifier(MyServerSettings.HitChanceCap()*2);
			BearEssenceMax = PhylacteryPowerModifier(MyServerSettings.DefendChanceCap());
			ThornEssenceMax = PhylacteryPowerModifier(MyServerSettings.ReflectDamageCap());
			SageEssenceMax = PhylacteryPowerModifier(MyServerSettings.LowerReagentCostCap());
			TitanEssenceMax = PhylacteryPowerModifier( MyServerSettings.DamageIncreaseCap() / 3);
			GazerEssenceMax = PhylacteryPowerModifier(MyServerSettings.LowerManaCostCap());
			SnakeEssenceMax = PhylacteryPowerModifier(MyServerSettings.WeaponSpeedCap());
			LuckyEssenceMax = PhylacteryPowerModifier(MyServerSettings.LuckCap() / 20);
			ImpEssenceMax = PhylacteryPowerModifier(50);
			PlanarEssenceMax = PhylacteryPowerModifier(MyServerSettings.CastRecoveryCap());
			CelestialEssenceMax = PhylacteryPowerModifier(MyServerSettings.CastSpeedCap());
			OwlEssenceMax = PhylacteryPowerModifier(MyServerSettings.PhylacterySkillStatGainCap());
			VampireEssenceMax = PhylacteryPowerModifier(50); // hit life leech
			SpringEssenceMax = PhylacteryPowerModifier(50); // hit stam leech
			SacredEssenceMax = PhylacteryPowerModifier(50); // hit mana leech
			PixieEssenceMax = PhylacteryPowerModifier(1);
		}

		public void SetResistanceCaps(Mobile from) 
		{
			ScorpionEssenceMax = PhylacteryPowerModifier(from.GetMaxResistance(ResistanceType.Poison));
			FireEssenceMax = PhylacteryPowerModifier(from.GetMaxResistance(ResistanceType.Fire));
			ColdEssenceMax = PhylacteryPowerModifier(from.GetMaxResistance(ResistanceType.Cold));
			EnergyEssenceMax = PhylacteryPowerModifier(from.GetMaxResistance(ResistanceType.Energy));
			EarthEssenceMax = PhylacteryPowerModifier(from.GetMaxResistance(ResistanceType.Physical));
		}

		public List<PropertyInfo> GetEssenceProperties() {
			Regex regex = new Regex(@"Essence$");
			PropertyInfo[] properties = typeof(Phylactery).GetProperties();
			List<PropertyInfo> essenceProperties = new List<PropertyInfo>();
			for(int index = 0; index < properties.Length; index++) {
				PropertyInfo property = properties[index];
				Match match = regex.Match(property.Name);
				MethodInfo setMethod = property.GetSetMethod();
				if (match.Success && setMethod != null) 
				{
					essenceProperties.Add(property);
				}
			}
			return essenceProperties;
		}

		public PropertyInfo GetMaxEssenceProperty(string essenceName) {
			return typeof(Phylactery).GetProperty(essenceName + "Max");
		}

		public void MergePhylactery(PlayerMobile player, Phylactery target) {
			if ( target.Serial != this.Serial) {
				int targetPower = target.PowerLevel;
				int combinedPower = targetPower + PowerLevel;
				if (combinedPower > PowerLevelMax ) {
					player.SendMessage("The power of this item is too great and would destroy your phylactery."); // 
				} else {
					// set this first, important
					PowerLevel = combinedPower;
					this.RefreshItem(player);
					List<PropertyInfo> properties = GetEssenceProperties();
					foreach(PropertyInfo property in properties) {
						if (property.PropertyType == typeof(int)) 
						{
							PropertyInfo maxProperty = GetMaxEssenceProperty(property.Name);
							if (maxProperty != null) {
								if (maxProperty.PropertyType == typeof(int)) {
								int maxValue = (int)maxProperty.GetValue(this);
								int newValue = (int)property.GetValue(target);
								newValue += (int)property.GetValue(this);
								if (newValue > maxValue)
									newValue = maxValue;
								property.SetValue(this, newValue);
								}
							}
						}
					}
					target.Delete();	
				}
				this.UpdateOwnerSoul(player);
				player.SendLocalizedMessage( 1061209 ); //You merge another phylactery into your own
			}
		
		}

		public int CalculatePhylacteryMods(string propertyName) {
			int newOffset = 0;
			if (PhylacteryMods != null) {
				foreach(PhylacteryMod mod in PhylacteryMods) {
					if (propertyName == mod.EssenceProperty) {
						PropertyInfo property = typeof(Phylactery).GetProperty(propertyName);
						if (property != null) {
							newOffset = mod.Offset;
						}
					}			
				}
			}
			return newOffset;
		}

		public int TotalPower() {
			int currentPower = 0;
			List<PropertyInfo> properties = GetEssenceProperties();
			foreach(PropertyInfo property in properties) {
				if (property.PropertyType == typeof(int)) 
				{
					currentPower += (int)property.GetValue(this);
				}
			}
			return currentPower;
		}

		public bool HasMaxPower() {
			return (PowerLevel == PowerLevelMax);
		}

		public void ResetPhylactery() {
			Init();
		}

		public Mobile GetEscapedUndead(bool random) {
			int totalPower = this.TotalPower();
			if (random) {
				totalPower = Utility.RandomMinMax((int)(totalPower/4), totalPower);
			} 
			if (totalPower < 200) {
				Wraith wraith = new Wraith();
				wraith.Body = 181;
				return wraith;
			} else if (totalPower < 400) {
				return new SkeletalKnight();
			} else if (totalPower < 600) {
				return new SkeletalMage();
			} else if (totalPower < 700) {
				return new RevenantLion();
			} else if (totalPower < 800) {
				return new RottingMinotaur();
			} else if (totalPower < 900) {
				return new Lich(); 
			} else if (totalPower < 1000) {
				return new Nazghoul();
			} else if (totalPower < 1200) {
				return new LichLord();
			} else {
				return new AncientLich();
			}
		}

		public void ResetOwnerResistances(PlayerMobile player) {
			if (FireMod != null) {
				player.RemoveResistanceMod( FireMod );	
			} 
			if (PoisonMod != null) {
				player.RemoveResistanceMod( PoisonMod );	
			}
			if (ColdMod != null) {
				player.RemoveResistanceMod( ColdMod );	
			}
			if (EnergyMod != null) {
					player.RemoveResistanceMod( EnergyMod );	
			}
			if (PhysicalMod != null) {
				player.RemoveResistanceMod( PhysicalMod );	
			}
		}

		public void ResetOwnerStats(PlayerMobile player) {
			//player.RemoveStatMod( this.Serial.ToString() + "Str" );
			//player.RemoveStatMod( this.Serial.ToString() + "Dex" );
			//player.RemoveStatMod( this.Serial.ToString() + "Int" );

			for( int i = 0; i < player.StatMods.Count; ++i )
			{
				StatMod check = player.StatMods[i];

					player.StatMods.RemoveAt( i );
					player.CheckStatTimers();
					player.InvalidateProperties();

			}
		
		}	

		public void UpdateOwnerSoul(PlayerMobile player) {
			if ( Core.AOS ) {
				if (!player.SoulBound) {
					return;
				}
				Owner = player;
				ResetOwnerResistances(player);
				ResetOwnerStats(player);
				int fireEssence = this.FireEssence + CalculatePhylacteryMods("FireEssence");
				if (fireEssence > FireEssenceMax) {
					fireEssence = FireEssenceMax;
				}
				FireMod = new ResistanceMod( ResistanceType.Fire, fireEssence*3);
				player.AddResistanceMod( FireMod );
				int scorpionEssence = this.ScorpionEssence + CalculatePhylacteryMods("ScorpionEssence");
				if (scorpionEssence > ScorpionEssenceMax) {
					scorpionEssence = ScorpionEssenceMax;
				}
				PoisonMod = new ResistanceMod( ResistanceType.Poison, scorpionEssence*3 );
				player.AddResistanceMod( PoisonMod );
				int coldEssence = this.ColdEssence + CalculatePhylacteryMods("ColdEssence");
				if (coldEssence > ColdEssenceMax) {
					coldEssence = ColdEssenceMax;
				}			
				ColdMod = new ResistanceMod( ResistanceType.Cold, coldEssence*3);
				player.AddResistanceMod( ColdMod );	
				int energyEssence = this.EnergyEssence + CalculatePhylacteryMods("EnergyEssence");
				if (energyEssence > EnergyEssenceMax) {
					energyEssence = EnergyEssenceMax;
				}			
				EnergyMod = new ResistanceMod( ResistanceType.Energy, energyEssence*3);
				player.AddResistanceMod( EnergyMod );	
				int earthEssence = this.EarthEssence + CalculatePhylacteryMods("EarthEssence");
				if (earthEssence > EarthEssenceMax) {
					earthEssence = EarthEssenceMax;
				}	
				PhysicalMod = new ResistanceMod( ResistanceType.Physical, earthEssence*3);
				player.AddResistanceMod( PhysicalMod );
				
				int defendChance = this.BearEssence + CalculatePhylacteryMods("BearEssence");
				if (defendChance > MyServerSettings.DefendChanceCap()) {
					defendChance = MyServerSettings.DefendChanceCap();
				}
				Attributes.DefendChance = defendChance;
				Attributes.BonusStam = (int)((this.BearEssence+CalculatePhylacteryMods("BearEssence"))*1.11); 
				
				int spellDamage = (this.DemonicEssence + CalculatePhylacteryMods("DemonicEssence"));
				if (spellDamage > MyServerSettings.SpellDamage()) {
					spellDamage = MyServerSettings.SpellDamage();
				}
				Attributes.SpellDamage = spellDamage;

				int regenHits = (this.PlantEssence + CalculatePhylacteryMods("PlantEssence"));
				if (regenHits > MyServerSettings.RegenHitsCap()) {
					regenHits = MyServerSettings.RegenHitsCap();
				}
				Attributes.RegenHits = regenHits;

				int leechHits = (this.VampireEssence + CalculatePhylacteryMods("VampireEssence"));
				if (leechHits > 50) {
					leechHits = 50;
				}
				//Attributes.RegenHits = leechHits;  figure this out

				int leechMana = (this.SacredEssence + CalculatePhylacteryMods("SacredEssence"));
				if (leechMana > 50) {
					leechMana = 50;
				}
				//Attributes.RegenHits = leechHits;  figure this out

				int leechStam = (this.SpringEssence + CalculatePhylacteryMods("SpringEssence"));
				if (leechStam > 50) {
					leechStam = 50;
				}
				//Attributes.RegenHits = leechHits;  figure this out

				Attributes.BonusHits = (int)((this.PlantEssence+CalculatePhylacteryMods("PlantEssence"))*1.42);
				int regenMana = (this.WaterEssence + CalculatePhylacteryMods("WaterEssence"));
				if (regenMana > MyServerSettings.RegenManaCap()) {
					regenMana = MyServerSettings.RegenManaCap();
				}
				Attributes.RegenMana = regenMana;
				Attributes.BonusMana = (int)((this.WaterEssence+CalculatePhylacteryMods("WaterEssence"))*1.42);
				int reflectPhysical = (this.ThornEssence + CalculatePhylacteryMods("ThornEssence"));
				if (reflectPhysical > MyServerSettings.ReflectDamageCap()) {
					reflectPhysical = MyServerSettings.ReflectDamageCap();
				}
				Attributes.ReflectPhysical = reflectPhysical;
				int attackChance = (this.EagleEssence/2 + CalculatePhylacteryMods("EagleEssence"));
				if (attackChance > MyServerSettings.HitChanceCap()) {
					attackChance = MyServerSettings.HitChanceCap();
				}
				Attributes.AttackChance = attackChance;
				int strBonus = (int)((this.TitanEssence+CalculatePhylacteryMods("TitanEssence")));
				if ( strBonus > 0) {
					player.AddStatMod( new StatMod( StatType.Str, this.Serial.ToString() + "Str", strBonus, TimeSpan.Zero ) );
				}
				int dexBonus = (int)((this.EagleEssence+CalculatePhylacteryMods("EagleEssence")));
				if ( dexBonus > 0 ) {
					player.AddStatMod( new StatMod( StatType.Dex,  this.Serial.ToString() + "Dex", dexBonus, TimeSpan.Zero ) );
				}
				int intBonus = (int)((this.GazerEssence+CalculatePhylacteryMods("GazerEssence")));
				if ( intBonus > 0 ) {
					player.AddStatMod( new StatMod( StatType.Int,  this.Serial.ToString() + "Int", intBonus, TimeSpan.Zero ) );
				}
				
				int weaponDamage = ( TitanEssence + ( CalculatePhylacteryMods("TitanEssence")));
				if (weaponDamage > MyServerSettings.DamageIncreaseCap()) {
					weaponDamage = MyServerSettings.DamageIncreaseCap();
				}
				Attributes.WeaponDamage = weaponDamage;
				int regenStam = (HorseEssence + CalculatePhylacteryMods("HorseEssence"));
				if (regenStam > MyServerSettings.RegenStamCap()) {
					regenStam = MyServerSettings.RegenStamCap();
				}
				Attributes.RegenStam = regenStam;
				int weaponSpeed = (SnakeEssence + CalculatePhylacteryMods("SnakeEssence"));
				if (weaponSpeed > MyServerSettings.WeaponSpeedCap()) {
					weaponSpeed = MyServerSettings.WeaponSpeedCap();
				}
				Attributes.WeaponSpeed = weaponSpeed;
				int spellChanneling = (PixieEssence + CalculatePhylacteryMods("PixieEssence"));
				if (spellChanneling > 1) {
					spellChanneling = 1;
				}
				Attributes.SpellChanneling = spellChanneling;
				int lowerManaCost = (GazerEssence + CalculatePhylacteryMods("GazerEssence"));
				if (lowerManaCost > MyServerSettings.LowerManaCostCap()) {
					lowerManaCost = MyServerSettings.LowerManaCostCap();
				}
				Attributes.LowerManaCost = lowerManaCost;
				int lowerRegCost = (SageEssence + CalculatePhylacteryMods("SageEssence"));
				if (lowerRegCost > MyServerSettings.LowerReagentCostCap()) {
					lowerRegCost = MyServerSettings.LowerReagentCostCap();
				}
				Attributes.LowerRegCost = lowerRegCost;
				int luck = (this.LuckyEssence + CalculatePhylacteryMods("LuckyEssence")) *20;
				if (luck > MyServerSettings.LuckCap()) {
					luck = MyServerSettings.LuckCap();
				}
				Attributes.Luck = luck;
				int enhancePotions = this.ImpEssence + CalculatePhylacteryMods("ImpEssence");
				if (enhancePotions > 50) {
					enhancePotions = 50;
				}
				Attributes.EnhancePotions = enhancePotions;
				if (OwlEssence == MyServerSettings.PhylacterySkillStatGainCap()) {
					Attributes.NightSight = 1;
				}
				int castSpeed = (this.CelestialEssence + CalculatePhylacteryMods("CelestialEssence"));
				if (castSpeed > MyServerSettings.CastSpeedCap()) {
					castSpeed = MyServerSettings.CastSpeedCap();
				}
				Attributes.CastSpeed = castSpeed;
				int castRecovery = (this.PlanarEssence + CalculatePhylacteryMods("PlanarEssence"));
				if (castRecovery > MyServerSettings.CastRecoveryCap()) {
					castRecovery = MyServerSettings.CastRecoveryCap();
				}
				Attributes.CastRecovery = castRecovery;
				RefreshItem(player);	
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			// base.GetProperties( list );

			LootType = LootType.Ensouled; 

			list.Add( "your soul's vault" ); // a phylactery

			list.Add( 1060658, "{0}\t{1}", "Level", PowerLevel );  // ~1_val~: ~2_val~
			if ((ImpEssence  + CalculatePhylacteryMods("ImpEssence")) > 0) {
				list.Add( 1060658, "{0}\t{1}", "Gold find", (ImpEssence+10) + "-" + (ImpEssence + 50) + "%" );  // ~1_val~: ~2_val~			
				list.Add( 1060411, Attributes.EnhancePotions.ToString() ); // enhance potions ~1_val~%
			}
			if (OwlEssence > 0) {
				list.Add( 1060658, "{0}\t{1}", "Skill gain increase", ( this.CalculateSkillGainModifier() * 100).ToString() + "%" );  // ~1_val~: ~2_val~
				list.Add( 1060658, "{0}\t{1}", "Stat increase rating", this.CalculateStatGainModifier(15.0).ToString() + " minutes" );  // ~1_val~: ~2_val~
			}

			if (PhylacteryMods != null && PhylacteryMods.Count > 0) {
				foreach(PhylacteryMod mod in PhylacteryMods) {
					list.Add( 1060658, "{0}\t{1}", "Party " + mod.EssenceProperty.Replace("Essence", "") + " buff", CalculatePhylacteryMods(mod.EssenceProperty).ToString());  // ~1_val~: ~2_val~	
				}
			}
			
			if ((EarthEssence + CalculatePhylacteryMods("EarthEssence")) > 0) {
				list.Add( 1060448, (EarthEssence + CalculatePhylacteryMods("EarthEssence")*3).ToString()); // physical resist ~1_val~%
			}
			if ((FireEssence + CalculatePhylacteryMods("FireEssence")) > 0) {
				list.Add( 1060447, (FireEssence + CalculatePhylacteryMods("FireEssence")*3).ToString()); // fire resist ~1_val~%
			}
			if ((ColdEssence + CalculatePhylacteryMods("ColdEssence")) > 0) {
				list.Add( 1060445, (ColdEssence + CalculatePhylacteryMods("ColdEssence")*3).ToString()); // cold resist ~1_val~%
			}
			if ((ScorpionEssence + CalculatePhylacteryMods("ScorpionEssence")) > 0) {
				list.Add( 1060449, (ScorpionEssence + CalculatePhylacteryMods("ScorpionEssence")*3).ToString()); // poison resist ~1_val~%
			}
			if ((EnergyEssence + CalculatePhylacteryMods("EnergyEssence")) > 0) {
				list.Add( 1060446, (EnergyEssence + CalculatePhylacteryMods("EnergyEssence")*3).ToString()); // energy resist ~1_val~%
			}
			if ((TitanEssence + CalculatePhylacteryMods("TitanEssence")) > 0) {
				list.Add( 1060401, Attributes.WeaponDamage.ToString()); // damage increase ~1_val~%
				list.Add( 1060485,((int)(TitanEssence + CalculatePhylacteryMods("TitanEssence"))).ToString() ); // strength bonus ~1_val~
			}
			if ((BearEssence + CalculatePhylacteryMods("BearEssence")) > 0) {
				list.Add( 1060408, Attributes.DefendChance.ToString() ); // defense chance increase ~1_val~%	
				list.Add( 1060484, Attributes.BonusStam.ToString() ); // stamina increase ~1_val~
			}
			if ((EagleEssence + CalculatePhylacteryMods("EagleEssence")) > 0) {
				list.Add( 1060415, Attributes.AttackChance.ToString() ); // hit chance increase ~1_val~%
				list.Add( 1060409, ((int)(EagleEssence + CalculatePhylacteryMods("EagleEssence"))).ToString() ); // dexterity bonus ~1_val~
			}
			if ((PlanarEssence + CalculatePhylacteryMods("PlanarEssence")) > 0) {
				list.Add( 1060412, Attributes.CastRecovery.ToString() ); // faster cast recovery ~1_val~
			}
			if ((CelestialEssence + CalculatePhylacteryMods("CelestialEssence")) > 0) {
				list.Add( 1060413, Attributes.CastSpeed.ToString() ); // faster casting ~1_val~	
			}
			if ((PlantEssence + CalculatePhylacteryMods("PlantEssence")) > 0) {
				list.Add( 1060444, Attributes.RegenHits.ToString() ); // hit point regeneration ~1_val~
				list.Add( 1060431, Attributes.BonusHits.ToString() ); // hit point increase ~1_val~	
			}
			if ((VampireEssence + CalculatePhylacteryMods("VampireEssence")) > 0) {
				list.Add( 1060422, VampireEssence.ToString() ); // hit life leech ~1_val~%
				//list.Add( 1060444, Attributes.RegenHits.ToString() ); // hit point regeneration ~1_val~
				//list.Add( 1060431, Attributes.BonusHits.ToString() ); // hit point increase ~1_val~	
			}
			if ((SpringEssence + CalculatePhylacteryMods("SpringEssence")) > 0) {
				list.Add( 1060430, SpringEssence.ToString() ); // hit stamina leech ~1_val~%
				//list.Add( 1060444, Attributes.RegenHits.ToString() ); // hit point regeneration ~1_val~
				//list.Add( 1060431, Attributes.BonusHits.ToString() ); // hit point increase ~1_val~	
			}
			if ((SacredEssence + CalculatePhylacteryMods("SacredEssence")) > 0) {
				list.Add( 1060427, SacredEssence.ToString() ); // hit mana leech ~1_val~%
				//list.Add( 1060444, Attributes.RegenHits.ToString() ); // hit point regeneration ~1_val~
				//list.Add( 1060431, Attributes.BonusHits.ToString() ); // hit point increase ~1_val~	
			}
			if ((GazerEssence + CalculatePhylacteryMods("GazerEssence")) > 0) {
				list.Add( 1060433, Attributes.LowerManaCost.ToString() ); // lower mana cost ~1_val~%
				list.Add( 1060432,((int)((GazerEssence + CalculatePhylacteryMods("GazerEssence")))).ToString() ); // intelligence bonus ~1_val~
			}
			if ((SageEssence + CalculatePhylacteryMods("SageEssence")) > 0) {
				list.Add( 1060434, Attributes.LowerRegCost.ToString() ); // lower reagent cost ~1_val~%
			}
			if ((LuckyEssence + CalculatePhylacteryMods("LuckyEssence")) > 0) {
				list.Add( 1060436, Attributes.Luck.ToString() ); // luck ~1_va				
			}
			if ((WaterEssence + CalculatePhylacteryMods("WaterEssence")) > 0) {
				list.Add( 1060440, Attributes.RegenMana.ToString() ); // mana regeneration ~1_val~
				list.Add( 1060439, Attributes.BonusMana.ToString() ); // mana increase ~1_val~	
			}
			if (OwlEssence == MyServerSettings.PhylacterySkillStatGainCap()) {
				list.Add( 1060441, Attributes.NightSight.ToString() ); // night sight
			}
			if ((ThornEssence + CalculatePhylacteryMods("ThornEssence")) > 0) {
				list.Add( 1060442, Attributes.ReflectPhysical.ToString() ); // reflect physical damage ~1_val~%	
			}
			if ((HorseEssence + CalculatePhylacteryMods("HorseEssence")) > 0) {
				list.Add( 1060443, Attributes.RegenStam.ToString() ); // stamina regeneration ~1_val~	
			}			
			if ((PixieEssence + CalculatePhylacteryMods("PixieEssence")) > 0) {
				list.Add( 1060482, Attributes.SpellChanneling.ToString()); // spell channeling
			}
			if ((DemonicEssence + CalculatePhylacteryMods("DemonicEssence")) > 0) {
				list.Add( 1060483, Attributes.SpellDamage.ToString() ); // spell damage increase ~1_val~%	
			}
			if ((SnakeEssence + CalculatePhylacteryMods("SnakeEssence")) > 0) {
				list.Add( 1060486, Attributes.WeaponSpeed.ToString() ); // swing speed increase ~1_val~%	
			}
		}
		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if (this.Temporary) {
						from.SendMessage("You cannot use this phylactery in this way, it is temporary");
					} else {
						from.SendLocalizedMessage( 1061190 ); // What would you like to use your phylactery on?
						from.Target = new PhylacteryTarget( this );
					}
			}
			else {
				from.SendLocalizedMessage( 1045158 ); //  You must have the item in your backpack to target it.
			}
		}

		public void AddRandomEssences(int amount) {
			List<PropertyInfo> properties = GetEssenceProperties();
			for (int iteration = 0; iteration < amount; iteration = iteration + 1) {
				Random random = new Random();
				int index = random.Next(properties.Count);
				PropertyInfo property = properties[index];
				int currentValue = (int)property.GetValue(this);
				property.SetValue(this, (currentValue+1));
			}
		}

		public void RemoveRandomEssences(double percentage, PlayerMobile player) {				
			double remainingEssences = (double)(this.TotalPower()*percentage);
			int originalPower = this.TotalPower();
			int differential = (int)(originalPower-(int)remainingEssences);
			List<PropertyInfo> properties = GetEssenceProperties();
			int luckRemoved = 0;
			int standardRemoved = 0;
			int originalLuck = LuckyEssence;
			for (int iteration = 0; iteration < differential; iteration = iteration + 1) {
				PropertyInfo property = properties[Utility.Random(properties.Count)];
				int currentValue = (int)property.GetValue(this);
				if (currentValue > 0) {
					int newValue = currentValue - 1;
					if (newValue >= 0) {
						property.SetValue(this, newValue);
					}
					AttributeCollection attributes = TypeDescriptor.GetProperties(this)[property.Name].Attributes;
					DescriptionAttribute attribute = (DescriptionAttribute)attributes[typeof(DescriptionAttribute)];
					player.SendMessage(attribute.Description + " has decreased in your phylactery. It is now " + newValue);
					string soulForceCostProperty = "BaseSoulForceEssenceCost";
					if (property.Name == "LuckyEssence") {
						soulForceCostProperty = "BaseSoulForceLuckCost";
					} 
					int soulForceFraction = this.SoulForceFraction(player, soulForceCostProperty);
					this.UpdateSoulForceCost(soulForceCostProperty, soulForceFraction, false);
				} else {
					// essence was empty, so keep loop alive
					--iteration;
				}
			}
		}

		public void BeginCombine( Mobile from )
		{
			from.Target = new PhylacteryTarget( this );
		}

		public void EndCombine( Mobile from, object o )
		{
			if ( o is Item && ((Item)o).IsChildOf( from.Backpack ) )
			{
				Item item = (Item)o;
				if (o is Phylactery) {
					MergePhylactery((PlayerMobile)from, (Phylactery)item);
				} else if (o is SoulShard) {
					SoulShard shard = (SoulShard)o;
					int numCharges = shard.Charges;
					int totalPower = TotalPower();
					if (numCharges < shard.MaxCharges) {
						double differential = 1.00001-(((double)shard.MaxCharges - (double)numCharges)/3000);
						RemoveRandomEssences(differential,(PlayerMobile)from);	
						shard.Charges = shard.MaxCharges;
						this.UpdateOwnerSoul((PlayerMobile)from);
					}
				} else if (o is PhylacteryComponent) {
					// RequiredForGain = CalculateRequiredForGain(from, ((PhylacteryComponent)o).ComponentType);
 					// if (item.Amount >= RequiredForGain) {
						int essenceValue = 0;
						string propertyName = ((PhylacteryComponent)o).BoundEssence;
						if (propertyName.Length > 0) {
							PropertyInfo property = typeof(Phylactery).GetProperty(propertyName);
							PropertyInfo maxProperty = typeof(Phylactery).GetProperty(propertyName + "Max");
							if (property != null && maxProperty != null) {
								int max = (int)maxProperty.GetValue(this);
								int current = (int)property.GetValue(this);
								int newValue = current + item.Amount;
								if (current == max) {
									if (o is LichBlood || (o.GetType() != typeof(LichBlood) && PowerLevel == PowerLevelMax)) {
										from.SendLocalizedMessage( 1061187 ); // Your phylactery has already reached its maximum amount of this essence
									} else {
										from.SendLocalizedMessage( 1061188 ); // Your phylactery has reached its maximum amount of this essence, but untapped power remains
									}
								} else {
									if (newValue > max) {
										item.Amount = newValue - max;
									} else {
										item.Delete();
									}
									property.SetValue(this, newValue);
									if (o is LichBlood) {
										from.SendLocalizedMessage( 1061186 ); // The power of your phylactery has grown, it can now hold more essence
									} else {
										from.SendLocalizedMessage( 1061189 ); // You infuse the essence with your phylactery
									}
									PlayerMobile player = (PlayerMobile)from;
									
									this.UpdateOwnerSoul(player);
									player.FixedParticles( 0x374A, 10, 30, 5013, Server.Items.CharacterDatabase.GetMySpellHue( player, 0 ), 2, EffectLayer.Waist );
									player.PlaySound( 0x0FC );
								}	
							}
						}
					// } else {
						// from.SendLocalizedMessage( 1061205 ); // You do not have enough essence to gain power
					// }	
				} else {
					from.SendLocalizedMessage( 1061185 ); // This is not a compatible item for the phylactery
				}
			} 
			else
			{
				from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
			}
		}

		public Phylactery( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version 1, added temporary phylactery
			writer.Write(m_SoulForceSpent);
			writer.Write(m_BaseSoulForceEssenceCost);
			writer.Write(m_BaseSoulForcePowerCost);
			writer.Write(m_BaseSoulForceLuckCost);
			writer.Write(m_FireEssenceMax);
			writer.Write(m_ColdEssenceMax);
			writer.Write(m_PhysicalEssenceMax);
			writer.Write(m_EnergyEssenceMax);
			writer.Write(m_ScorpionEssenceMax);
			writer.Write(m_CurrentSkillCap);
			writer.Write(m_SageEssenceMax);
			writer.Write(m_ThornEssenceMax);
			writer.Write(m_DemonicEssenceMax);
			writer.Write(m_WaterEssenceMax);
			writer.Write(m_PlantEssenceMax);
			writer.Write(m_BearEssenceMax);
			writer.Write(m_EagleEssenceMax);
			writer.Write(m_EarthEssenceMax);
			writer.Write(m_ColdEssence);
			writer.Write(m_PhysicalEssence);
			writer.Write(m_EnergyEssence);
			writer.Write(m_ScorpionEssence);
			writer.Write(m_OwlEssence);
			writer.Write(m_OwlEssenceMax);
			writer.Write(m_SageEssence);
			writer.Write(m_ThornEssence);
			writer.Write(m_DemonicEssence);
			writer.Write(m_WaterEssence);
			writer.Write(m_BearEssence);
			writer.Write(m_EagleEssence);
			writer.Write(m_EarthEssence);
			writer.Write(m_FireEssence);
			writer.Write(m_PowerLevel);
			writer.Write(m_PowerLevelMax);
			writer.Write(m_PlantEssence);
			writer.Write(m_TitanEssence);
			writer.Write(m_TitanEssenceMax);
			writer.Write(m_GazerEssence);
			writer.Write(m_GazerEssenceMax);
			writer.Write(m_HorseEssence);
			writer.Write(m_HorseEssenceMax);
			writer.Write(m_SnakeEssence);
			writer.Write(m_SnakeEssenceMax);
			writer.Write(m_LuckyEssence);
			writer.Write(m_LuckyEssenceMax);
			writer.Write(m_PixieEssence);
			writer.Write(m_PixieEssenceMax);
			writer.Write(m_ImpEssence);
			writer.Write(m_ImpEssenceMax);
			writer.Write(m_PlanarEssence);
			writer.Write(m_PlanarEssenceMax);
			writer.Write(m_CelestialEssence);
			writer.Write(m_CelestialEssenceMax);
			m_AosAttributes.Serialize( writer );
			writer.Write(m_Temporary);
			writer.Write(m_VampireEssence);
			writer.Write(m_VampireEssenceMax);
			writer.Write(m_SpringEssence);
			writer.Write(m_SpringEssenceMax);
			writer.Write(m_SacredEssence);
			writer.Write(m_SacredEssenceMax);

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_SoulForceSpent = reader.ReadInt();
			m_BaseSoulForceEssenceCost = reader.ReadInt();
			m_BaseSoulForcePowerCost = reader.ReadInt();
			m_BaseSoulForceLuckCost = reader.ReadInt();
			m_FireEssenceMax = reader.ReadInt();
			m_ColdEssenceMax = reader.ReadInt();
			m_PhysicalEssenceMax = reader.ReadInt();
			m_EnergyEssenceMax = reader.ReadInt();
			m_ScorpionEssenceMax = reader.ReadInt();
			m_CurrentSkillCap = reader.ReadInt();
			m_SageEssenceMax = reader.ReadInt();
			m_ThornEssenceMax = reader.ReadInt();
			m_DemonicEssenceMax = reader.ReadInt();
			m_WaterEssenceMax = reader.ReadInt();
			m_PlantEssenceMax = reader.ReadInt();
			m_BearEssenceMax = reader.ReadInt();
			m_EagleEssenceMax = reader.ReadInt();
			m_EarthEssenceMax = reader.ReadInt();
			m_ColdEssence = reader.ReadInt();
			m_PhysicalEssence = reader.ReadInt();
			m_EnergyEssence = reader.ReadInt();
			m_ScorpionEssence = reader.ReadInt();
			m_OwlEssence = reader.ReadInt();
			m_OwlEssenceMax = reader.ReadInt();
			m_SageEssence = reader.ReadInt();
			m_ThornEssence = reader.ReadInt();
			m_DemonicEssence = reader.ReadInt();
			m_WaterEssence = reader.ReadInt();
			m_BearEssence = reader.ReadInt();
			m_EagleEssence = reader.ReadInt();
			m_EarthEssence = reader.ReadInt();
			m_FireEssence = reader.ReadInt();
			m_PowerLevel = reader.ReadInt();
			m_PowerLevelMax = reader.ReadInt();
			m_PlantEssence = reader.ReadInt();
			m_TitanEssence = reader.ReadInt();
			m_TitanEssenceMax = reader.ReadInt();
			m_GazerEssence = reader.ReadInt();
			m_GazerEssenceMax = reader.ReadInt();
			m_HorseEssence = reader.ReadInt();
			m_HorseEssenceMax = reader.ReadInt();
			m_SnakeEssence = reader.ReadInt();
			m_SnakeEssenceMax = reader.ReadInt();
			m_LuckyEssence = reader.ReadInt();
			m_LuckyEssenceMax = reader.ReadInt();
			m_PixieEssence = reader.ReadInt();
			m_PixieEssenceMax = reader.ReadInt();
			m_ImpEssence = reader.ReadInt();
			m_ImpEssenceMax = reader.ReadInt();
			m_PlanarEssence = reader.ReadInt();
			m_PlanarEssenceMax = reader.ReadInt();
			m_CelestialEssence = reader.ReadInt();
			m_CelestialEssenceMax = reader.ReadInt();
			m_AosAttributes = new AosAttributes( this, reader );
			m_PhylacteryMods = new List<PhylacteryMod>();
			if (version > 0) {
				m_Temporary = reader.ReadBool();
			} else {
				m_Temporary = false;
			}
			if (version >= 2)
			{
				m_VampireEssence = reader.ReadInt();
				m_VampireEssenceMax = reader.ReadInt();
				m_SpringEssence = reader.ReadInt();
				m_SpringEssenceMax = reader.ReadInt();
				m_SacredEssence = reader.ReadInt();
				m_SacredEssenceMax = reader.ReadInt();
			}

			if (LootType != LootType.Ensouled)
				LootType = LootType.Ensouled;
		}

		public virtual bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{
			return false;
		}
	}
	public class PhylacteryMod
	{
		private PlayerMobile m_Player;
		private string m_EssenceProperty;
		private int m_Offset;

		public PlayerMobile Player
		{
			get { return m_Player; }
			set { m_Player = value; }
		}

		public string EssenceProperty
		{
			get { return m_EssenceProperty; }
			set
			{
				if( m_EssenceProperty != value )
				{
					m_EssenceProperty = value;
				}
			}
		}

		public int Offset
		{
			get { return m_Offset; }
			set
			{
				if( m_Offset != value )
				{
					m_Offset = value;
				}
			}
		}

		public PhylacteryMod(string essenceProperty, int offset )
		{
			m_EssenceProperty = essenceProperty;
			m_Offset = offset;
		}
	}
}