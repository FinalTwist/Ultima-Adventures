using System;
using System.Collections;

namespace Server.Items
{
	public abstract class BaseRunicTool : BaseTool
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); InvalidateProperties(); }
		}

		public BaseRunicTool( CraftResource resource, int itemID ) : base( itemID )
		{
			m_Resource = resource;
		}

		public BaseRunicTool( CraftResource resource, int uses, int itemID ) : base( uses, itemID )
		{
			m_Resource = resource;
		}

		public BaseRunicTool( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}
		}

		private static bool m_IsRunicTool;
		private static int m_LuckChance;

		private static int Scale( int min, int max, int low, int high )
		{
			int percent;

			if ( m_IsRunicTool )
			{
				percent = Utility.RandomMinMax( min, max );
			}
			else
			{
				// Behold, the worst system ever!
				int v = Utility.RandomMinMax( 0, 10000 );

				v = (int) Math.Sqrt( v );
				v = 100 - v;

				if ( LootPack.CheckLuck( m_LuckChance ) )
					v += 10;

				if ( v < min )
					v = min;
				else if ( v > max )
					v = max;

				percent = v;
			}

			int scaledBy = Math.Abs( high - low ) + 1;

			if ( scaledBy != 0 )
				scaledBy = 10000 / scaledBy;

			percent *= (10000 + scaledBy);

			return low + (((high - low) * percent) / 1000001);
		}

		private static void ApplyAttribute( AosAttributes attrs, int min, int max, AosAttribute attr, int low, int high )
		{
			ApplyAttribute( attrs, min, max, attr, low, high, 1 );
		}

		private static void ApplyAttribute( AosAttributes attrs, int min, int max, AosAttribute attr, int low, int high, int scale )
		{
			if ( attr == AosAttribute.CastSpeed )
				attrs[attr] += Scale( min, max, low / scale, high / scale ) * scale;
			else
				attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;

			if ( attr == AosAttribute.SpellChanneling )
				attrs[AosAttribute.CastSpeed] -= 1;
		}

		private static void ApplyAttribute( AosArmorAttributes attrs, int min, int max, AosArmorAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyAttribute( AosArmorAttributes attrs, int min, int max, AosArmorAttribute attr, int low, int high, int scale )
		{
			attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;
		}

		private static void ApplyAttribute( AosWeaponAttributes attrs, int min, int max, AosWeaponAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyAttribute( AosWeaponAttributes attrs, int min, int max, AosWeaponAttribute attr, int low, int high, int scale )
		{
			attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;
		}

		private static void ApplyAttribute( AosElementAttributes attrs, int min, int max, AosElementAttribute attr, int low, int high )
		{
			attrs[attr] = Scale( min, max, low, high );
		}

		private static void ApplyAttribute( AosElementAttributes attrs, int min, int max, AosElementAttribute attr, int low, int high, int scale )
		{
			attrs[attr] = Scale( min, max, low / scale, high / scale ) * scale;
		}

		private static SkillName[] m_PossibleBonusSkills = new SkillName[]
			{
				SkillName.Alchemy,
				SkillName.Anatomy,
				SkillName.AnimalLore,
				SkillName.AnimalTaming,
				SkillName.Archery,
				SkillName.ArmsLore,
				SkillName.Begging,
				SkillName.Blacksmith,
				SkillName.Bushido,
				SkillName.Camping,
				SkillName.Carpentry,
				SkillName.Cartography,
				SkillName.Chivalry,
				SkillName.Cooking,
				SkillName.DetectHidden,
				SkillName.Discordance,
				SkillName.EvalInt,
				SkillName.Fencing,
				SkillName.Fishing,
				SkillName.Fletching,
				SkillName.Focus,
				SkillName.Forensics,
				SkillName.Healing,
				SkillName.Herding,
				SkillName.Hiding,
				SkillName.Inscribe,
				SkillName.ItemID,
				SkillName.Lockpicking,
				SkillName.Lumberjacking,
				SkillName.Macing,
				SkillName.Magery,
				SkillName.MagicResist,
				SkillName.Meditation,
				SkillName.Mining,
				SkillName.Musicianship,
				SkillName.Necromancy,
				SkillName.Ninjitsu,
				SkillName.Parry,
				SkillName.Peacemaking,
				SkillName.Poisoning,
				SkillName.Provocation,
				SkillName.RemoveTrap,
				SkillName.Snooping,
				SkillName.SpiritSpeak,
				SkillName.Stealing,
				SkillName.Stealth,
				SkillName.Swords,
				SkillName.Tactics,
				SkillName.Tailoring,
				SkillName.TasteID,
				SkillName.Tinkering,
				SkillName.Tracking,
				SkillName.Veterinary,
				SkillName.Wrestling
			};

		private static SkillName[] m_PossibleFightSkills = new SkillName[]
			{
				SkillName.Archery,
				SkillName.Bushido,
				SkillName.Chivalry,
				SkillName.Fencing,
				SkillName.Focus,
				SkillName.Healing,
				SkillName.Macing,
				SkillName.Parry,
				SkillName.Swords,
				SkillName.Tactics,
				SkillName.Wrestling
			};

		private static SkillName[] m_PossibleShieldSkills = new SkillName[]
			{
				SkillName.Fencing,
				SkillName.Macing,
				SkillName.Parry,
				SkillName.Swords,
				SkillName.Tactics
			};

		private static SkillName[] m_PossibleWepFencingSkills = new SkillName[]
			{
				SkillName.Fencing,
				SkillName.Tactics
			};

		private static SkillName[] m_PossibleWepMacingSkills = new SkillName[]
			{
				SkillName.Macing,
				SkillName.Tactics
			};
		private static SkillName[] m_PossibleWepSwordsSkills = new SkillName[]
			{
				SkillName.Swords,
				SkillName.Tactics
			};

		private static SkillName[] m_PossibleWepArcherySkills = new SkillName[]
			{
				SkillName.Archery,
				SkillName.Tactics
			};

		private static SkillName[] m_PossibleWepWrestlingSkills = new SkillName[]
			{
				SkillName.Wrestling,
				SkillName.Tactics
			};

		private static SkillName[] m_PossibleSpellbookSkills = new SkillName[]
			{
				SkillName.Magery,
				SkillName.Meditation,
				SkillName.EvalInt,
				SkillName.MagicResist
			};

		private static SkillName[] m_PossibleBardSkills = new SkillName[]
			{
				SkillName.Discordance,
				SkillName.Musicianship,
				SkillName.Peacemaking,
				SkillName.Provocation
			};

		private static void ApplySkillBonus( AosSkillBonuses attrs, int min, int max, int index, int low, int high )
		{
			SkillName[] possibleSkills = m_PossibleBonusSkills;

			// WIZARD WANTED MAGIC WEAPONS TO HAVE A SKILL BONUS /////
			if ( attrs.Owner is BaseShield ){ possibleSkills = m_PossibleShieldSkills; }
			else if ( attrs.Owner is BaseArmor ){ possibleSkills = m_PossibleFightSkills; }
			else if ( attrs.Owner is BaseWeapon )
			{
				BaseWeapon bm = (BaseWeapon)attrs.Owner;
				if ( bm.Skill == SkillName.Swords ){ possibleSkills = m_PossibleWepSwordsSkills; }
				else if ( bm.Skill == SkillName.Archery ){ possibleSkills = m_PossibleWepArcherySkills; }
				else if ( bm.Skill == SkillName.Fencing ){ possibleSkills = m_PossibleWepFencingSkills; }
				else if ( bm.Skill == SkillName.Macing ){ possibleSkills = m_PossibleWepMacingSkills; }
				else if ( bm.Skill == SkillName.Wrestling ){ possibleSkills = m_PossibleWepWrestlingSkills; }
			}
			else if ( attrs.Owner is Spellbook ){ possibleSkills = m_PossibleSpellbookSkills; }
			else if ( attrs.Owner is BaseInstrument ){ possibleSkills = m_PossibleBardSkills; }

			int count = possibleSkills.Length;

			SkillName sk, check;
			double bonus;
			bool found;

			do
			{
				found = false;
				sk = possibleSkills[Utility.Random( count )];

				for ( int i = 0; !found && i < 5; ++i )
					found = ( attrs.GetValues( i, out check, out bonus ) && check == sk );
			} while ( found );

			attrs.SetValues( index, sk, Scale( min, max, low, high ) );
		}

		private static void ApplyResistance( BaseArmor ar, int min, int max, ResistanceType res, int low, int high )
		{
			switch ( res )
			{
				case ResistanceType.Physical: ar.PhysicalBonus += Scale( min, max, low, high ); break;
				case ResistanceType.Fire: ar.FireBonus += Scale( min, max, low, high ); break;
				case ResistanceType.Cold: ar.ColdBonus += Scale( min, max, low, high ); break;
				case ResistanceType.Poison: ar.PoisonBonus += Scale( min, max, low, high ); break;
				case ResistanceType.Energy: ar.EnergyBonus += Scale( min, max, low, high ); break;
			}
		}

		private const int MaxProperties = 32;
		private static BitArray m_Props = new BitArray( MaxProperties );
		private static int[] m_Possible = new int[MaxProperties];

		public static int GetUniqueRandom( int count )
		{
			int avail = 0;

			for ( int i = 0; i < count; ++i )
			{
				if ( !m_Props[i] )
					m_Possible[avail++] = i;
			}

			if ( avail == 0 )
				return -1;

			int v = m_Possible[Utility.Random( avail )];

			m_Props.Set( v, true );

			return v;
		}

		public void ApplyAttributesTo( BaseWeapon weapon )
		{
			CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

			if ( resInfo == null )
				return;

			CraftAttributeInfo attrs = resInfo.AttributeInfo;

			if ( attrs == null )
				return;

			int attributeCount = Utility.RandomMinMax( attrs.RunicMinAttributes, attrs.RunicMaxAttributes );
			int min = attrs.RunicMinIntensity;
			int max = attrs.RunicMaxIntensity;

			ApplyAttributesTo( weapon, true, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseWeapon weapon, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( weapon, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseWeapon weapon, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = weapon.Attributes;
			AosWeaponAttributes secondary = weapon.WeaponAttributes;
			AosSkillBonuses skills = weapon.SkillBonuses;

			m_Props.SetAll( false );

			if ( weapon is BaseRanged )
				m_Props.Set( 2, true ); // ranged weapons cannot be ubws or mageweapon

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 26 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case 0:
					{
						switch ( Utility.Random( 5 ) )
						{
							case 0: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitPhysicalArea,2, 50, 2 ); break;
							case 1: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitFireArea,	2, 50, 2 ); break;
							case 2: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitColdArea,	2, 50, 2 ); break;
							case 3: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitPoisonArea,	2, 50, 2 ); break;
							case 4: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitEnergyArea,	2, 50, 2 ); break;
						}

						break;
					}
					case 1:
					{
						switch ( Utility.Random( 4 ) )
						{
							case 0: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitMagicArrow,	2, 50, 2 ); break;
							case 1: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitHarm,		2, 50, 2 ); break;
							case 2: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitFireball,	2, 50, 2 ); break;
							case 3: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitLightning,	2, 50, 2 ); break;
						}

						break;
					}
					case 2:
					{
						switch ( Utility.Random( 2 ) )
						{
							case 0: ApplyAttribute( secondary, min, max, AosWeaponAttribute.UseBestSkill,	1, 1 ); break;
							case 1: ApplyAttribute( secondary, min, max, AosWeaponAttribute.MageWeapon,		1, 10 ); break;
						}

						break;
					}
					case  3: ApplyAttribute( primary,	min, max, AosAttribute.WeaponDamage,				1, 50 ); break;
					case  4: ApplyAttribute( primary,	min, max, AosAttribute.DefendChance,				1, 15 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.CastSpeed,					1, 1 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.AttackChance,				1, 15 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.Luck,						1, 100 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.WeaponSpeed,					5, 30, 5 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.SpellChanneling,				1, 1 ); break;
					case 10: ApplyAttribute( secondary, min, max, AosWeaponAttribute.HitDispel,				2, 50, 2 ); break;
					case 11: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.HitLeechHits,			2, 50, 2 ); break;
					case 12: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.HitLowerAttack,		2, 50, 2 ); break;
					case 13: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.HitLowerDefend,		2, 50, 2 ); break;
					case 14: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.HitLeechMana,			2, 50, 2 ); break;
					case 15: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.HitLeechStam,			2, 50, 2 ); break;
					case 16: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.LowerStatReq,			10, 100, 10 ); break;
					case 17: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.ResistPhysicalBonus,	1, 15 ); break;
					case 18: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.ResistFireBonus,		1, 15 ); break;
					case 19: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.ResistColdBonus,		1, 15 ); break;
					case 20: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.ResistPoisonBonus,		1, 15 ); break;
					case 21: ApplyAttribute( secondary,	min, max, AosWeaponAttribute.ResistEnergyBonus,		1, 15 ); break;
					case 22: ApplyAttribute( secondary, min, max, AosWeaponAttribute.DurabilityBonus,		10, 100, 10 ); break;
					case 23: weapon.Slayer = GetRandomSlayer(); break;
					case 24: GetElementalDamages( weapon ); break;
					case 25: ApplySkillBonus( skills,	min, max, 0,										1, 15 ); break;
				}
			}
		}

		public static void GetElementalDamages( BaseWeapon weapon )
		{
			GetElementalDamages( weapon, true );
		}

		public static void GetElementalDamages( BaseWeapon weapon, bool randomizeOrder )
		{
			int fire, phys, cold, nrgy, pois, chaos, direct;

			weapon.GetDamageTypes( null, out phys, out fire, out cold, out pois, out nrgy, out chaos, out direct );

			int totalDamage = phys;

			AosElementAttribute[] attrs = new AosElementAttribute[]
			{
				AosElementAttribute.Cold,
				AosElementAttribute.Energy,
				AosElementAttribute.Fire,
				AosElementAttribute.Poison
			};

			if( randomizeOrder )
			{
				for( int i = 0; i < attrs.Length; i++ )
				{
					int rand = Utility.Random( attrs.Length );
					AosElementAttribute temp = attrs[i];

					attrs[i] = attrs[rand];
					attrs[rand] = temp;
				}
			}


			/*
			totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Cold,		totalDamage );
			totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Energy,	totalDamage );
			totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Fire,		totalDamage );
			totalDamage = AssignElementalDamage( weapon, AosElementAttribute.Poison,	totalDamage );

			weapon.AosElementDamages[AosElementAttribute.Physical] = 100 - totalDamage;
			 * */

			for( int i = 0; i < attrs.Length; i++ )
				totalDamage = AssignElementalDamage( weapon, attrs[i], totalDamage );


			//Order is Cold, Energy, Fire, Poison -> Physical left
			//Cannot be looped, AoselementAttribute is 'out of order'

			weapon.Hue = weapon.GetElementalDamageHue();
		}

		private static int AssignElementalDamage( BaseWeapon weapon, AosElementAttribute attr, int totalDamage )
		{
			if( totalDamage <= 0 )
				return 0;

			int random = Utility.Random( (int)(totalDamage/10) + 1 ) * 10;
			weapon.AosElementDamages[attr] = random;

			return (totalDamage - random);
		}

		public static SlayerName GetRandomSlayer()
		{
			/* OLD CODE
			SlayerGroup[] groups = SlayerGroup.Groups;

			if ( groups.Length == 0 )
				return SlayerName.None;

			SlayerGroup group = groups[Utility.Random( groups.Length )]; // WIZARD GOT RID OF THE -1 SO YOU GET FEY SLAYERS
			SlayerEntry entry;

			if ( 10 > Utility.Random( 100 ) ) // 10% chance to do super slayer
			{
				entry = group.Super;
			}
			else
			{
				SlayerEntry[] entries = group.Entries;

				if ( entries.Length == 0 )
					return SlayerName.None;

				entry = entries[Utility.Random( entries.Length )];
			}

			return entry.Name;
			*/

			return Server.Items.SlayerDeed.GetDeedSlayer( Utility.RandomMinMax( 1, 34 ) );
		}

		public void ApplyAttributesTo( BaseArmor armor )
		{
			CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

			if ( resInfo == null )
				return;

			CraftAttributeInfo attrs = resInfo.AttributeInfo;

			if ( attrs == null )
				return;

			int attributeCount = Utility.RandomMinMax( attrs.RunicMinAttributes, attrs.RunicMaxAttributes );
			int min = attrs.RunicMinIntensity;
			int max = attrs.RunicMaxIntensity;

			ApplyAttributesTo( armor, true, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseArmor armor, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( armor, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseArmor armor, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = armor.Attributes;
			AosArmorAttributes secondary = armor.ArmorAttributes;
			AosSkillBonuses skills = armor.SkillBonuses;

			m_Props.SetAll( false );

			bool isShield = ( armor is BaseShield );
			int baseCount = ( isShield ? 7 : 20 );
			int baseOffset = ( isShield ? 0 : 4 );

			if ( !isShield && armor.MeditationAllowance == ArmorMeditationAllowance.All )
				m_Props.Set( 3, true ); // remove mage armor from possible properties
			if ( armor.Resource >= CraftResource.RegularLeather && armor.Resource <= CraftResource.BarbedLeather )
			{
				m_Props.Set( 0, true ); // remove lower requirements from possible properties for leather armor
				m_Props.Set( 2, true ); // remove durability bonus from possible properties
			}
			if ( armor.RequiredRace == Race.Elf )
				m_Props.Set( 7, true ); // elves inherently have night sight and elf only armor doesn't get night sight as a mod

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( baseCount );

				if ( random == -1 )
					break;

				random += baseOffset;

				switch ( random )
				{
						/* Begin Shields */
					case  0: ApplyAttribute( primary,	min, max, AosAttribute.SpellChanneling,			1, 1 ); break;
					case  1: ApplyAttribute( primary,	min, max, AosAttribute.DefendChance,			1, 15 ); break;
					case  2:
						if ( Utility.RandomMinMax( 1, 2 ) == 1 ) {
							ApplyAttribute( primary,    min, max, AosAttribute.ReflectPhysical,         1, 15 );
							} else {
							ApplyAttribute( primary,    min, max, AosAttribute.AttackChance,            1, 15 );
							}
						break;
					case  3: ApplyAttribute( primary,	min, max, AosAttribute.CastSpeed,				1, 1 ); break;
						/* Begin Armor */
					case  4:
						if ( Utility.RandomMinMax( 1, 2 ) == 1 ) {
							ApplySkillBonus( skills,	min, max, 0,									1, 15 );
							} else {
							ApplyAttribute( secondary,	min, max, AosArmorAttribute.LowerStatReq,		10, 100, 10 );
							}
						break;
					case  5:
						if ( Utility.RandomMinMax( 1, 2 ) == 1 ) {
							ApplySkillBonus( skills,	min, max, 0,									1, 15 );
							} else {
							ApplyAttribute( secondary,	min, max, AosArmorAttribute.SelfRepair,			1, 5 );
							}
						break;
					case  6: ApplyAttribute( secondary,	min, max, AosArmorAttribute.DurabilityBonus,	10, 100, 10 ); break;
						/* End Shields */
					case  7: ApplyAttribute( secondary,	min, max, AosArmorAttribute.MageArmor,			1, 1 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.RegenHits,				1, 2 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.RegenStam,				1, 3 ); break;
					case 10: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 2 ); break;
					case 11: ApplyAttribute( primary,	min, max, AosAttribute.NightSight,				1, 1 ); break;
					case 12: ApplyAttribute( primary,	min, max, AosAttribute.BonusHits,				1, 5 ); break;
					case 13: ApplyAttribute( primary,	min, max, AosAttribute.BonusStam,				1, 8 ); break;
					case 14: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				1, 8 ); break;
					case 15: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case 16: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 17: ApplyAttribute( primary,	min, max, AosAttribute.Luck,					1, 100 ); break;
					case 18: ApplyAttribute( primary,	min, max, AosAttribute.ReflectPhysical,			1, 15 ); break;
					case 19: ApplyResistance( armor,	min, max, ResistanceType.Physical,				1, 15 ); break;
					case 20: ApplyResistance( armor,	min, max, ResistanceType.Fire,					1, 15 ); break;
					case 21: ApplyResistance( armor,	min, max, ResistanceType.Cold,					1, 15 ); break;
					case 22: ApplyResistance( armor,	min, max, ResistanceType.Poison,				1, 15 ); break;
					case 23: ApplyResistance( armor,	min, max, ResistanceType.Energy,				1, 15 ); break;
					/* End Armor */
				}
			}
		}

		public static void ApplyAttributesTo( BaseHat hat, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( hat, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseHat hat, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = hat.Attributes;
			AosArmorAttributes secondary = hat.ClothingAttributes;
			AosElementAttributes resists = hat.Resistances;

			m_Props.SetAll( false );

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 19 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case  0: ApplyAttribute( primary,	min, max, AosAttribute.ReflectPhysical,			1, 15 ); break;
					case  1: ApplyAttribute( primary,	min, max, AosAttribute.RegenHits,				1, 2 ); break;
					case  2: ApplyAttribute( primary,	min, max, AosAttribute.RegenStam,				1, 3 ); break;
					case  3: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 2 ); break;
					case  4: ApplyAttribute( primary,	min, max, AosAttribute.NightSight,				1, 1 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.BonusHits,				1, 5 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.BonusStam,				1, 8 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				1, 8 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 10: ApplyAttribute( primary,	min, max, AosAttribute.Luck,					1, 100 ); break;
					case 11: ApplyAttribute( secondary,	min, max, AosArmorAttribute.LowerStatReq,		10, 100, 10 ); break;
					case 12: ApplyAttribute( secondary,	min, max, AosArmorAttribute.SelfRepair,			1, 5 ); break;
					case 13: ApplyAttribute( secondary,	min, max, AosArmorAttribute.DurabilityBonus,	10, 100, 10 ); break;
					case 14: ApplyAttribute( resists,	min, max, AosElementAttribute.Physical,			1, 15 ); break;
					case 15: ApplyAttribute( resists,	min, max, AosElementAttribute.Fire,				1, 15 ); break;
					case 16: ApplyAttribute( resists,	min, max, AosElementAttribute.Cold,				1, 15 ); break;
					case 17: ApplyAttribute( resists,	min, max, AosElementAttribute.Poison,			1, 15 ); break;
					case 18: ApplyAttribute( resists,	min, max, AosElementAttribute.Energy,			1, 15 ); break;
				}
			}
		}

		public void ApplyAttributesTo( BaseClothing cloth )
		{
			CraftResourceInfo resInfo = CraftResources.GetInfo( m_Resource );

			if ( resInfo == null )
				return;

			CraftAttributeInfo attrs = resInfo.AttributeInfo;

			if ( attrs == null )
				return;

			int attributeCount = Utility.RandomMinMax( attrs.RunicMinAttributes, attrs.RunicMaxAttributes );
			int min = attrs.RunicMinIntensity;
			int max = attrs.RunicMaxIntensity;

			ApplyAttributesTo( cloth, true, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseClothing cloth, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( cloth, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseClothing cloth, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = cloth.Attributes;
			AosArmorAttributes secondary = cloth.ClothingAttributes;
			AosElementAttributes resists = cloth.Resistances;

			m_Props.SetAll( false );

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 19 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case  0: ApplyAttribute( primary,	min, max, AosAttribute.ReflectPhysical,			1, 15 ); break;
					case  1: ApplyAttribute( primary,	min, max, AosAttribute.RegenHits,				1, 2 ); break;
					case  2: ApplyAttribute( primary,	min, max, AosAttribute.RegenStam,				1, 3 ); break;
					case  3: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 2 ); break;
					case  4: ApplyAttribute( primary,	min, max, AosAttribute.NightSight,				1, 1 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.BonusHits,				1, 5 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.BonusStam,				1, 8 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				1, 8 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 10: ApplyAttribute( primary,	min, max, AosAttribute.Luck,					1, 100 ); break;
					case 11: ApplyAttribute( secondary,	min, max, AosArmorAttribute.LowerStatReq,		10, 100, 10 ); break;
					case 12: ApplyAttribute( secondary,	min, max, AosArmorAttribute.SelfRepair,			1, 5 ); break;
					case 13: ApplyAttribute( secondary,	min, max, AosArmorAttribute.DurabilityBonus,	10, 100, 10 ); break;
					case 14: ApplyAttribute( resists,	min, max, AosElementAttribute.Physical,			1, 15 ); break;
					case 15: ApplyAttribute( resists,	min, max, AosElementAttribute.Fire,				1, 15 ); break;
					case 16: ApplyAttribute( resists,	min, max, AosElementAttribute.Cold,				1, 15 ); break;
					case 17: ApplyAttribute( resists,	min, max, AosElementAttribute.Poison,			1, 15 ); break;
					case 18: ApplyAttribute( resists,	min, max, AosElementAttribute.Energy,			1, 15 ); break;
				}
			}
		}

		public static void ApplyAttributesTo( BaseJewel jewelry, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( jewelry, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseJewel jewelry, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = jewelry.Attributes;
			AosElementAttributes resists = jewelry.Resistances;
			AosSkillBonuses skills = jewelry.SkillBonuses;

			m_Props.SetAll( false );

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 32 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case  0: ApplyAttribute( resists,	min, max, AosElementAttribute.Physical,			1, 15 ); break;
					case  1: ApplyAttribute( resists,	min, max, AosElementAttribute.Fire,				1, 15 ); break;
					case  2: ApplyAttribute( resists,	min, max, AosElementAttribute.Cold,				1, 15 ); break;
					case  3: ApplyAttribute( resists,	min, max, AosElementAttribute.Poison,			1, 15 ); break;
					case  4: ApplyAttribute( resists,	min, max, AosElementAttribute.Energy,			1, 15 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.WeaponDamage,			1, 25 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.DefendChance,			1, 15 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.AttackChance,			1, 15 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.BonusStr,				1, 8 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.BonusDex,				1, 8 ); break;
					case 10: ApplyAttribute( primary,	min, max, AosAttribute.BonusInt,				1, 8 ); break;
					case 11: ApplyAttribute( primary,	min, max, AosAttribute.EnhancePotions,			5, 25, 5 ); break;
					case 12: ApplyAttribute( primary,	min, max, AosAttribute.CastSpeed,				1, 1 ); break;
					case 13: ApplyAttribute( primary,	min, max, AosAttribute.CastRecovery,			1, 3 ); break;
					case 14: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case 15: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 16: ApplyAttribute( primary,	min, max, AosAttribute.Luck,					1, 100 ); break;
					case 17: ApplyAttribute( primary,	min, max, AosAttribute.SpellDamage,				1, 12 ); break;
					case 18: ApplyAttribute( primary,	min, max, AosAttribute.NightSight,				1, 1 ); break;
					case 19: ApplyAttribute( primary,	min, max, AosAttribute.BonusHits,				5, 20 ); break;
					case 20: ApplyAttribute( primary,	min, max, AosAttribute.BonusStam,				5, 20 ); break;
					case 21: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				5, 20 ); break;
					case 22: ApplyAttribute( primary,	min, max, AosAttribute.ReflectPhysical,			5, 15 ); break;
					case 23: ApplyAttribute( primary,	min, max, AosAttribute.RegenHits,				1, 5 ); break;
					case 24: ApplyAttribute( primary,	min, max, AosAttribute.RegenStam,				1, 5 ); break;
					case 25: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 5 ); break;
					case 26: ApplyAttribute( primary,	min, max, AosAttribute.WeaponSpeed,				1, 2 ); break;
					case 27: ApplySkillBonus( skills,	min, max, 0,									1, 15 ); break;
					case 28: ApplySkillBonus( skills,	min, max, 1,									1, 15 ); break;
					case 29: ApplySkillBonus( skills,	min, max, 2,									1, 15 ); break;
					case 30: ApplySkillBonus( skills,	min, max, 3,									1, 15 ); break;
					case 31: ApplySkillBonus( skills,	min, max, 4,									1, 15 ); break;
				}
			}
		}

		public static void ApplyAttributesTo( BaseQuiver quiver, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( quiver, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseQuiver quiver, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = quiver.Attributes;

			m_Props.SetAll( false );

			int dmgIncrease = 0;
				if ( LootPack.CheckLuck( m_LuckChance ) || max >= Utility.RandomMinMax( 1, 50 ) ){ dmgIncrease = 5 + Utility.RandomMinMax( min, max ); }
				if ( dmgIncrease > 50 ){ dmgIncrease = 50; }
			int lowAmmo = 0;
				if ( LootPack.CheckLuck( m_LuckChance ) || max >= Utility.RandomMinMax( 1, 50 ) ){ lowAmmo = 5 + Utility.RandomMinMax( min, max ); }
				if ( lowAmmo > 75 ){ lowAmmo = 75; }
			int weightReduce = 50;
				if ( LootPack.CheckLuck( m_LuckChance ) || max >= Utility.RandomMinMax( 1, 50 ) ){ weightReduce = weightReduce + Utility.RandomMinMax( min, max ); }
				if ( weightReduce > 100 ){ weightReduce = 100; }

			quiver.DamageIncrease = dmgIncrease;
			quiver.LowerAmmoCost = lowAmmo;
			quiver.WeightReduction = weightReduce;

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 22 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case  0: ApplyAttribute( primary,	min, max, AosAttribute.WeaponDamage,			1, 25 ); break;
					case  1: ApplyAttribute( primary,	min, max, AosAttribute.DefendChance,			1, 15 ); break;
					case  2: ApplyAttribute( primary,	min, max, AosAttribute.AttackChance,			1, 15 ); break;
					case  3: ApplyAttribute( primary,	min, max, AosAttribute.BonusStr,				1, 8 ); break;
					case  4: ApplyAttribute( primary,	min, max, AosAttribute.BonusDex,				1, 8 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.BonusInt,				1, 8 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.EnhancePotions,			5, 25, 5 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.CastSpeed,				1, 1 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.CastRecovery,			1, 3 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case 10: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 11: ApplyAttribute( primary,	min, max, AosAttribute.Luck,					1, 100 ); break;
					case 12: ApplyAttribute( primary,	min, max, AosAttribute.SpellDamage,				1, 12 ); break;
					case 13: ApplyAttribute( primary,	min, max, AosAttribute.NightSight,				1, 1 ); break;
					case 14: ApplyAttribute( primary,	min, max, AosAttribute.BonusHits,				5, 20 ); break;
					case 15: ApplyAttribute( primary,	min, max, AosAttribute.BonusStam,				5, 20 ); break;
					case 16: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				5, 20 ); break;
					case 17: ApplyAttribute( primary,	min, max, AosAttribute.ReflectPhysical,			5, 15 ); break;
					case 18: ApplyAttribute( primary,	min, max, AosAttribute.RegenHits,				1, 5 ); break;
					case 19: ApplyAttribute( primary,	min, max, AosAttribute.RegenStam,				1, 5 ); break;
					case 20: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 5 ); break;
					case 21: ApplyAttribute( primary,	min, max, AosAttribute.WeaponSpeed,				1, 2 ); break;
				}
			}
		}

		public static void ApplyAttributesTo( BaseInstrument lute, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( lute, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( BaseInstrument lute, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = lute.Attributes;
			AosElementAttributes resists = lute.Resistances;
			AosSkillBonuses skills = lute.SkillBonuses;

			m_Props.SetAll( false );

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 32 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case  0: ApplyAttribute( resists,	min, max, AosElementAttribute.Physical,			1, 15 ); break;
					case  1: ApplyAttribute( resists,	min, max, AosElementAttribute.Fire,				1, 15 ); break;
					case  2: ApplyAttribute( resists,	min, max, AosElementAttribute.Cold,				1, 15 ); break;
					case  3: ApplyAttribute( resists,	min, max, AosElementAttribute.Poison,			1, 15 ); break;
					case  4: ApplyAttribute( resists,	min, max, AosElementAttribute.Energy,			1, 15 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.WeaponDamage,			1, 25 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.DefendChance,			1, 15 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.AttackChance,			1, 15 ); break;
					case  8: ApplyAttribute( primary,	min, max, AosAttribute.BonusStr,				1, 8 ); break;
					case  9: ApplyAttribute( primary,	min, max, AosAttribute.BonusDex,				1, 8 ); break;
					case 10: ApplyAttribute( primary,	min, max, AosAttribute.BonusInt,				1, 8 ); break;
					case 11: ApplyAttribute( primary,	min, max, AosAttribute.EnhancePotions,			5, 25, 5 ); break;
					case 12: ApplyAttribute( primary,	min, max, AosAttribute.CastSpeed,				1, 1 ); break;
					case 13: ApplyAttribute( primary,	min, max, AosAttribute.CastRecovery,			1, 3 ); break;
					case 14: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case 15: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 16: ApplyAttribute( primary,	min, max, AosAttribute.Luck,					1, 100 ); break;
					case 17: ApplyAttribute( primary,	min, max, AosAttribute.SpellDamage,				1, 12 ); break;
					case 18: ApplyAttribute( primary,	min, max, AosAttribute.NightSight,				1, 1 ); break;
					case 19: ApplyAttribute( primary,	min, max, AosAttribute.BonusHits,				5, 20 ); break;
					case 20: ApplyAttribute( primary,	min, max, AosAttribute.BonusStam,				5, 20 ); break;
					case 21: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				5, 20 ); break;
					case 22: ApplyAttribute( primary,	min, max, AosAttribute.ReflectPhysical,			5, 15 ); break;
					case 23: ApplyAttribute( primary,	min, max, AosAttribute.RegenHits,				1, 5 ); break;
					case 24: ApplyAttribute( primary,	min, max, AosAttribute.RegenStam,				1, 5 ); break;
					case 25: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 5 ); break;
					case 26: ApplyAttribute( primary,	min, max, AosAttribute.WeaponSpeed,				1, 2 ); break;
					case 27: ApplySkillBonus( skills,	min, max, 0,									1, 15 ); break;
					case 28: ApplySkillBonus( skills,	min, max, 1,									1, 15 ); break;
					case 29: ApplySkillBonus( skills,	min, max, 2,									1, 15 ); break;
					case 30: ApplySkillBonus( skills,	min, max, 3,									1, 15 ); break;
					case 31: ApplySkillBonus( skills,	min, max, 4,									1, 15 ); break;
				}
			}
		}

		public static void ApplyAttributesTo( Spellbook spellbook, int attributeCount, int min, int max )
		{
			ApplyAttributesTo( spellbook, false, 0, attributeCount, min, max );
		}

		public static void ApplyAttributesTo( Spellbook spellbook, bool isRunicTool, int luckChance, int attributeCount, int min, int max )
		{
			m_IsRunicTool = isRunicTool;
			m_LuckChance = luckChance;

			AosAttributes primary = spellbook.Attributes;
			AosSkillBonuses skills = spellbook.SkillBonuses;

			m_Props.SetAll( false );

			for ( int i = 0; i < attributeCount; ++i )
			{
				int random = GetUniqueRandom( 16 );

				if ( random == -1 )
					break;

				switch ( random )
				{
					case  0:
					case  1:
					case  2:
					case  3:
					{
						ApplyAttribute( primary, min, max, AosAttribute.BonusInt, 1, 8 );

						for ( int j = 0; j < 4; ++j )
							m_Props.Set( j, true );

						break;
					}
					case  4: ApplyAttribute( primary,	min, max, AosAttribute.BonusMana,				1, 8 ); break;
					case  5: ApplyAttribute( primary,	min, max, AosAttribute.CastSpeed,				1, 1 ); break;
					case  6: ApplyAttribute( primary,	min, max, AosAttribute.CastRecovery,			1, 3 ); break;
					case  7: ApplyAttribute( primary,	min, max, AosAttribute.SpellDamage,				1, 12 ); break;
					case  8: ApplySkillBonus( skills,	min, max, 0,									1, 15 ); break;
					case  9: ApplySkillBonus( skills,	min, max, 1,									1, 15 ); break;
					case 10: ApplySkillBonus( skills,	min, max, 2,									1, 15 ); break;
					case 11: ApplySkillBonus( skills,	min, max, 3,									1, 15 ); break;
					case 12: ApplyAttribute( primary,	min, max, AosAttribute.LowerRegCost,			1, 20 ); break;
					case 13: ApplyAttribute( primary,	min, max, AosAttribute.LowerManaCost,			1, 8 ); break;
					case 14: ApplyAttribute( primary,	min, max, AosAttribute.RegenMana,				1, 2 ); break;
					case 15: spellbook.Slayer = GetRandomSlayer(); break;
				}
			}
		}
	}
}