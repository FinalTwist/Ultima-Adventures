using System;
using Server;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using System.Collections;
using System.Collections.Generic;
using Server.Regions;

namespace Server.Items
{
	public class PowerScroll : SpecialScroll
	{
		public override int Message { get { return 1049469; } } /* Using a scroll increases the maximum amount of a specific skill or your maximum statistics.
																* When used, the effect is not immediately seen without a gain of points with that skill or statistics.
																* You can view your maximum skill values in your skills window.
																* You can view your maximum statistic value in your statistics window. */
		public override int Title 
		{
			get 
			{
				double level = ( Value - 105.0 ) / 5.0;
			
				if ( level >= 0.0 && level <= 3.0 && Value % 5.0 == 0.0 )
					return 1049635 + (int)level;	/* Wonderous Scroll (105 Skill): OR
													* Exalted Scroll (110 Skill): OR
													* Mythical Scroll (115 Skill): OR
													* Legendary Scroll (120 Skill): */
				
				return 0;
			}
		}
		
		public override string DefaultTitle{ get{ return String.Format( "<basefont Color=#FBFBFB>Power Scroll ({0} Skill):</basefont>", Value ); } }
		
		private static SkillName[] m_Skills = new SkillName[]
			{
				SkillName.Blacksmith,
				SkillName.Tailoring,
				SkillName.Fletching,
				SkillName.Swords,
				SkillName.Fencing,
				SkillName.Macing,
				SkillName.Archery,
				SkillName.Wrestling,
				SkillName.Parry,
				SkillName.Tactics,
				SkillName.Anatomy,
				SkillName.Healing,
				SkillName.Hiding,
				SkillName.Magery,
				SkillName.Meditation,
				SkillName.EvalInt,
				SkillName.MagicResist,
				SkillName.AnimalTaming,
				SkillName.AnimalLore,
				SkillName.Veterinary,
				SkillName.Musicianship,
				SkillName.Provocation,
				SkillName.Discordance,
				SkillName.Peacemaking
			};

		private static SkillName[] m_AOSSkills = new SkillName[]
			{
				SkillName.Chivalry,
				SkillName.Focus,
				SkillName.Necromancy,
				SkillName.Stealing,
				SkillName.Stealth,
				SkillName.SpiritSpeak
			};

		private static SkillName[] m_SESkills = new SkillName[]
			{
				SkillName.Ninjitsu,
				SkillName.Bushido
			};
		
		private static List<SkillName> _Skills = new List<SkillName>();

		public static List<SkillName> Skills
		{ 
			get
			{
				if ( _Skills.Count == 0 )
				{
					_Skills.AddRange( m_Skills );
					if (Core.AOS)
					{
						_Skills.AddRange( m_AOSSkills );
						if (Core.SE)
						{
							_Skills.AddRange( m_SESkills );
							if (Core.ML)
								_Skills.Add( SkillName.Spellweaving );
						}
					}
				}
				return _Skills;
			} 
		}

		private static PowerScroll CreateRandom(int min, int max, bool excludeCrafting) 
		{
			min /= 5;
			max /= 5;
			
			SkillName skillName;

			while (true)
			{
				skillName = Skills[Utility.Random( Skills.Count )];
				if (skillName == SkillName.Spellweaving || skillName == SkillName.Mysticism  || skillName == SkillName.Imbuing || skillName == SkillName.Throwing) continue;

				if (excludeCrafting) {
					if (skillName == SkillName.Blacksmith || skillName == SkillName.Tailoring || skillName == SkillName.Fletching) continue;
				}

				break;
			}

			return new PowerScroll( skillName, 100 + (Utility.RandomMinMax( min, max ) * 5));
		}

		public static PowerScroll CreateRandom( int min, int max )
		{
			return CreateRandom(min, max, false);
		}

		public static PowerScroll CreateRandomNoCraft( int min, int max )
		{
			if (min <5 || max <5)
				return null;

			return CreateRandom(min, max, true);
		}

		public PowerScroll() : this( SkillName.Alchemy, 0.0 )
		{
		}
		
		[Constructable]
		public PowerScroll( SkillName skill, double value ) : base( skill, value )
		{
			Hue = 0x481;

            if (Value == 105.0 || skill == Server.SkillName.Blacksmith || skill == Server.SkillName.Tailoring )
				LootType = LootType.Regular;
		}

		public PowerScroll( Serial serial ) : base( serial )
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			double level = ( Value - 105.0 ) / 5.0;
			
			if ( level >= 0.0 && level <= 3.0 && Value % 5.0 == 0.0 )
				list.Add( 1049639 + (int)level, GetNameLocalized() );	/* a wonderous scroll of ~1_type~ (105 Skill) OR
																		* an exalted scroll of ~1_type~ (110 Skill) OR
																		* a mythical scroll of ~1_type~ (115 Skill) OR
																		* a legendary scroll of ~1_type~ (120 Skill) */
			else
				list.Add( "a power scroll of {0} ({1} Skill)", GetName(), Value );
		}

		public override void OnSingleClick( Mobile from )
		{
			double level = ( Value - 105.0 ) / 5.0;
			
			if ( level >= 0.0 && level <= 3.0 && Value % 5.0 == 0.0 )
				base.LabelTo( from, 1049639 + (int)level, GetNameLocalized() );
			else
				base.LabelTo( from, "a power scroll of {0} ({1} Skill)", GetName(), Value );
		}
		
		public override bool CanUse( Mobile from )
		{
			if ( !base.CanUse( from ) )
				return false;
			
			Skill skill = from.Skills[Skill];

			if ( skill == null )
				return false;
			
			if ( skill.Cap >= Value )
			{
				from.SendLocalizedMessage( 1049511, GetNameLocalized() ); // Your ~1_type~ is too high for this power scroll.
				return false;
			}

			if ( MyServerSettings.powerscrolllevel() )
			{
				if ( skill.Cap < (this.Value - 5) )
				{
					string dummyvalue = Convert.ToString(this.Value - 5);
					from.SendMessage( "Your current cap has to be at {0} to use this scroll", dummyvalue );
					return false;
				}
			}

			if ( (
				( Skill == SkillName.Wrestling ) || 
				( Skill == SkillName.Bushido ) || 
				( Skill == SkillName.Swords ) || 
				( Skill == SkillName.Lumberjacking ) || 
				( Skill == SkillName.Mining ) || 
				( Skill == SkillName.Blacksmith ) || 
				( Skill == SkillName.Carpentry ) || 
				( Skill == SkillName.Macing ) || 
				( Skill == SkillName.Tactics ) || 
				( Skill == SkillName.Parry ) || 
				( Skill == SkillName.Fencing )
				) && ( !from.Region.IsPartOf( "Shrine of Strength" ) ) )
			{
				from.SendMessage( "This magic can only be unleashed at the Shrine of Strength." );
				return false;
			}

			if ( (
				( Skill == SkillName.Magery ) || 
				( Skill == SkillName.MagicResist ) || 
				( Skill == SkillName.Meditation ) || 
				( Skill == SkillName.Necromancy ) || 
				( Skill == SkillName.ArmsLore ) || 
				( Skill == SkillName.Cartography ) || 
				( Skill == SkillName.Cooking ) || 
				( Skill == SkillName.EvalInt ) || 
				( Skill == SkillName.Anatomy ) || 
				( Skill == SkillName.Alchemy ) || 
				( Skill == SkillName.Tailoring ) || 
				( Skill == SkillName.Tinkering ) || 
				( Skill == SkillName.Inscribe )
				) && ( !from.Region.IsPartOf( "Shrine of Intelligence" ) ) )
			{
				from.SendMessage( "This magic can only be unleashed at the Shrine of Intelligence." );
				return false;
			}

			if ( (
				( Skill == SkillName.Discordance ) || 
				( Skill == SkillName.Provocation ) || 
				( Skill == SkillName.Musicianship ) || 
				( Skill == SkillName.Archery ) || 
				( Skill == SkillName.Stealing ) || 
				( Skill == SkillName.Stealth ) || 
				( Skill == SkillName.Hiding ) || 
				( Skill == SkillName.RemoveTrap ) || 
				( Skill == SkillName.Snooping ) || 
				( Skill == SkillName.DetectHidden ) || 
				( Skill == SkillName.Ninjitsu ) || 
				( Skill == SkillName.Fletching ) || 
				( Skill == SkillName.Lockpicking )
				) && ( !from.Region.IsPartOf( "Shrine of Dexterity" ) ) )
			{
				from.SendMessage( "This magic can only be unleashed at the Shrine of Dexterity." );
				return false;
			}

			if ( (
				( Skill == SkillName.SpiritSpeak ) || 
				( Skill == SkillName.Chivalry ) || 
				( Skill == SkillName.Peacemaking ) || 
				( Skill == SkillName.Tracking ) || 
				( Skill == SkillName.Veterinary ) || 
				( Skill == SkillName.AnimalLore ) || 
				( Skill == SkillName.AnimalTaming ) || 
				( Skill == SkillName.Poisoning ) || 
				( Skill == SkillName.Focus ) || 
				( Skill == SkillName.Fishing ) || 
				( Skill == SkillName.Healing )
				) && ( !from.Region.IsPartOf( "Shrine of Wisdom" ) ) )
			{
				from.SendMessage( "This magic can only be unleashed at the Shrine of Wisdom." );
				return false;
			}

			return true;
		}

		public override void Use( Mobile from )
		{
			if ( !CanUse( from ) )
				return;
			
			from.SendLocalizedMessage( 1049513, GetNameLocalized() ); // You feel a surge of magic as the scroll enhances your ~1_type~!

			from.Skills[Skill].Cap = Value;

			Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0, 0, 0, 0, 0, 5060, 0 );
			Effects.PlaySound( from.Location, from.Map, 0x243 );

			Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 6, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
			Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 4, from.Y - 6, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );
			Effects.SendMovingParticles( new Entity( Serial.Zero, new Point3D( from.X - 6, from.Y - 4, from.Z + 15 ), from.Map ), from, 0x36D4, 7, 0, false, true, 0x497, 0, 9502, 1, 0, (EffectLayer)255, 0x100 );

			Effects.SendTargetParticles( from, 0x375A, 35, 90, 0x00, 0x00, 9502, (EffectLayer)255, 0x100 );

			Delete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = ( InheritsItem ? 0 : reader.ReadInt() ); //Required for SpecialScroll insertion

            if (Value == 105.0 || Skill == SkillName.Blacksmith || Skill == SkillName.Tailoring)
			{
				LootType = LootType.Regular;
			}
			else
			{
				LootType = LootType.Cursed;
				
			}
		}

		public static Item RandomPowerScroll()
		{
			Item scroll = new DJ_SW_Alchemy();

			int roll = Utility.RandomMinMax( 1, 100 );
			int choice = Utility.RandomMinMax( 0, 47 );

			if ( roll >= 99 ){ choice = Utility.RandomMinMax( 192, 239 ); }
			else if ( roll >= 97 ){ choice = Utility.RandomMinMax( 144, 191 ); }
			else if ( roll >= 94 ){ choice = Utility.RandomMinMax( 96, 143 ); }
			else if ( roll >= 85 ){ choice = Utility.RandomMinMax( 48, 95 ); }

			switch ( choice )
			{
				case 0: scroll = new DJ_SW_Alchemy(); break;
				case 1: scroll = new DJ_SW_Anatomy(); break;
				case 2: scroll = new DJ_SW_AnimalLore(); break;
				case 3: scroll = new DJ_SW_AnimalTaming(); break;
				case 4: scroll = new DJ_SW_Archery(); break;
				case 5: scroll = new DJ_SW_ArmsLore(); break;
				case 6: scroll = new DJ_SW_Blacksmith(); break;
				case 7: scroll = new DJ_SW_Bushido(); break;
				case 8: scroll = new DJ_SW_Carpentry(); break;
				case 9: scroll = new DJ_SW_Cartography(); break;
				case 10: scroll = new DJ_SW_Chivalry(); break;
				case 11: scroll = new DJ_SW_Cooking(); break;
				case 12: scroll = new DJ_SW_DetectHidden(); break;
				case 13: scroll = new DJ_SW_Discordance(); break;
				case 14: scroll = new DJ_SW_EvalInt(); break;
				case 15: scroll = new DJ_SW_Fencing(); break;
				case 16: scroll = new DJ_SW_Fishing(); break;
				case 17: scroll = new DJ_SW_Fletching(); break;
				case 18: scroll = new DJ_SW_Focus(); break;
				case 19: scroll = new DJ_SW_Healing(); break;
				case 20: scroll = new DJ_SW_Hiding(); break;
				case 21: scroll = new DJ_SW_Inscribe(); break;
				case 22: scroll = new DJ_SW_Lockpicking(); break;
				case 23: scroll = new DJ_SW_Lumberjacking(); break;
				case 24: scroll = new DJ_SW_Macing(); break;
				case 25: scroll = new DJ_SW_Magery(); break;
				case 26: scroll = new DJ_SW_MagicResist(); break;
				case 27: scroll = new DJ_SW_Meditation(); break;
				case 28: scroll = new DJ_SW_Mining(); break;
				case 29: scroll = new DJ_SW_Musicianship(); break;
				case 30: scroll = new DJ_SW_Necromancy(); break;
				case 31: scroll = new DJ_SW_Ninjitsu(); break;
				case 32: scroll = new DJ_SW_Parry(); break;
				case 33: scroll = new DJ_SW_Peacemaking(); break;
				case 34: scroll = new DJ_SW_Poisoning(); break;
				case 35: scroll = new DJ_SW_Provocation(); break;
				case 36: scroll = new DJ_SW_RemoveTrap(); break;
				case 37: scroll = new DJ_SW_Snooping(); break;
				case 38: scroll = new DJ_SW_SpiritSpeak(); break;
				case 39: scroll = new DJ_SW_Stealing(); break;
				case 40: scroll = new DJ_SW_Stealth(); break;
				case 41: scroll = new DJ_SW_Swords(); break;
				case 42: scroll = new DJ_SW_Tactics(); break;
				case 43: scroll = new DJ_SW_Tailoring(); break;
				case 44: scroll = new DJ_SW_Tinkering(); break;
				case 45: scroll = new DJ_SW_Tracking(); break;
				case 46: scroll = new DJ_SW_Veterinary(); break;
				case 47: scroll = new DJ_SW_Wrestling(); break;
				case 48: scroll = new DJ_SE_Alchemy(); break;
				case 49: scroll = new DJ_SE_Anatomy(); break;
				case 50: scroll = new DJ_SE_AnimalLore(); break;
				case 51: scroll = new DJ_SE_AnimalTaming(); break;
				case 52: scroll = new DJ_SE_Archery(); break;
				case 53: scroll = new DJ_SE_ArmsLore(); break;
				case 54: scroll = new DJ_SE_Blacksmith(); break;
				case 55: scroll = new DJ_SE_Bushido(); break;
				case 56: scroll = new DJ_SE_Carpentry(); break;
				case 57: scroll = new DJ_SE_Cartography(); break;
				case 58: scroll = new DJ_SE_Chivalry(); break;
				case 59: scroll = new DJ_SE_Cooking(); break;
				case 60: scroll = new DJ_SE_DetectHidden(); break;
				case 61: scroll = new DJ_SE_Discordance(); break;
				case 62: scroll = new DJ_SE_EvalInt(); break;
				case 63: scroll = new DJ_SE_Fencing(); break;
				case 64: scroll = new DJ_SE_Fishing(); break;
				case 65: scroll = new DJ_SE_Fletching(); break;
				case 66: scroll = new DJ_SE_Focus(); break;
				case 67: scroll = new DJ_SE_Healing(); break;
				case 68: scroll = new DJ_SE_Hiding(); break;
				case 69: scroll = new DJ_SE_Inscribe(); break;
				case 70: scroll = new DJ_SE_Lockpicking(); break;
				case 71: scroll = new DJ_SE_Lumberjacking(); break;
				case 72: scroll = new DJ_SE_Macing(); break;
				case 73: scroll = new DJ_SE_Magery(); break;
				case 74: scroll = new DJ_SE_MagicResist(); break;
				case 75: scroll = new DJ_SE_Meditation(); break;
				case 76: scroll = new DJ_SE_Mining(); break;
				case 77: scroll = new DJ_SE_Musicianship(); break;
				case 78: scroll = new DJ_SE_Necromancy(); break;
				case 79: scroll = new DJ_SE_Ninjitsu(); break;
				case 80: scroll = new DJ_SE_Parry(); break;
				case 81: scroll = new DJ_SE_Peacemaking(); break;
				case 82: scroll = new DJ_SE_Poisoning(); break;
				case 83: scroll = new DJ_SE_Provocation(); break;
				case 84: scroll = new DJ_SE_RemoveTrap(); break;
				case 85: scroll = new DJ_SE_Snooping(); break;
				case 86: scroll = new DJ_SE_SpiritSpeak(); break;
				case 87: scroll = new DJ_SE_Stealing(); break;
				case 88: scroll = new DJ_SE_Stealth(); break;
				case 89: scroll = new DJ_SE_Swords(); break;
				case 90: scroll = new DJ_SE_Tactics(); break;
				case 91: scroll = new DJ_SE_Tailoring(); break;
				case 92: scroll = new DJ_SE_Tinkering(); break;
				case 93: scroll = new DJ_SE_Tracking(); break;
				case 94: scroll = new DJ_SE_Veterinary(); break;
				case 95: scroll = new DJ_SE_Wrestling(); break;
				case 96: scroll = new DJ_SM_Alchemy(); break;
				case 97: scroll = new DJ_SM_Anatomy(); break;
				case 98: scroll = new DJ_SM_AnimalLore(); break;
				case 99: scroll = new DJ_SM_AnimalTaming(); break;
				case 100: scroll = new DJ_SM_Archery(); break;
				case 101: scroll = new DJ_SM_ArmsLore(); break;
				case 102: scroll = new DJ_SM_Blacksmith(); break;
				case 103: scroll = new DJ_SM_Bushido(); break;
				case 104: scroll = new DJ_SM_Carpentry(); break;
				case 105: scroll = new DJ_SM_Cartography(); break;
				case 106: scroll = new DJ_SM_Chivalry(); break;
				case 107: scroll = new DJ_SM_Cooking(); break;
				case 108: scroll = new DJ_SM_DetectHidden(); break;
				case 109: scroll = new DJ_SM_Discordance(); break;
				case 110: scroll = new DJ_SM_EvalInt(); break;
				case 111: scroll = new DJ_SM_Fencing(); break;
				case 112: scroll = new DJ_SM_Focus(); break;
				case 113: scroll = new DJ_SM_Fishing(); break;
				case 114: scroll = new DJ_SM_Fletching(); break;
				case 115: scroll = new DJ_SM_Healing(); break;
				case 116: scroll = new DJ_SM_Hiding(); break;
				case 117: scroll = new DJ_SM_Inscribe(); break;
				case 118: scroll = new DJ_SM_Lockpicking(); break;
				case 119: scroll = new DJ_SM_Lumberjacking(); break;
				case 120: scroll = new DJ_SM_Macing(); break;
				case 121: scroll = new DJ_SM_Magery(); break;
				case 122: scroll = new DJ_SM_MagicResist(); break;
				case 123: scroll = new DJ_SM_Meditation(); break;
				case 124: scroll = new DJ_SM_Mining(); break;
				case 125: scroll = new DJ_SM_Musicianship(); break;
				case 126: scroll = new DJ_SM_Necromancy(); break;
				case 127: scroll = new DJ_SM_Ninjitsu(); break;
				case 128: scroll = new DJ_SM_Parry(); break;
				case 129: scroll = new DJ_SM_Peacemaking(); break;
				case 130: scroll = new DJ_SM_Poisoning(); break;
				case 131: scroll = new DJ_SM_Provocation(); break;
				case 132: scroll = new DJ_SM_RemoveTrap(); break;
				case 133: scroll = new DJ_SM_Snooping(); break;
				case 134: scroll = new DJ_SM_SpiritSpeak(); break;
				case 135: scroll = new DJ_SM_Stealing(); break;
				case 136: scroll = new DJ_SM_Stealth(); break;
				case 137: scroll = new DJ_SM_Swords(); break;
				case 138: scroll = new DJ_SM_Tactics(); break;
				case 139: scroll = new DJ_SM_Tailoring(); break;
				case 140: scroll = new DJ_SM_Tinkering(); break;
				case 141: scroll = new DJ_SM_Tracking(); break;
				case 142: scroll = new DJ_SM_Veterinary(); break;
				case 143: scroll = new DJ_SM_Wrestling(); break;
				case 144: scroll = new DJ_SL_Alchemy(); break;
				case 145: scroll = new DJ_SL_Anatomy(); break;
				case 146: scroll = new DJ_SL_AnimalLore(); break;
				case 147: scroll = new DJ_SL_AnimalTaming(); break;
				case 148: scroll = new DJ_SL_Archery(); break;
				case 149: scroll = new DJ_SL_ArmsLore(); break;
				case 150: scroll = new DJ_SL_Blacksmith(); break;
				case 151: scroll = new DJ_SL_Bushido(); break;
				case 152: scroll = new DJ_SL_Carpentry(); break;
				case 153: scroll = new DJ_SL_Cartography(); break;
				case 154: scroll = new DJ_SL_Chivalry(); break;
				case 155: scroll = new DJ_SL_Cooking(); break;
				case 156: scroll = new DJ_SL_DetectHidden(); break;
				case 157: scroll = new DJ_SL_Discordance(); break;
				case 158: scroll = new DJ_SL_EvalInt(); break;
				case 159: scroll = new DJ_SL_Fencing(); break;
				case 160: scroll = new DJ_SL_Fishing(); break;
				case 161: scroll = new DJ_SL_Fletching(); break;
				case 162: scroll = new DJ_SL_Focus(); break;
				case 163: scroll = new DJ_SL_Healing(); break;
				case 164: scroll = new DJ_SL_Hiding(); break;
				case 165: scroll = new DJ_SL_Inscribe(); break;
				case 166: scroll = new DJ_SL_Lockpicking(); break;
				case 167: scroll = new DJ_SL_Lumberjacking(); break;
				case 168: scroll = new DJ_SL_Macing(); break;
				case 169: scroll = new DJ_SL_Magery(); break;
				case 170: scroll = new DJ_SL_MagicResist(); break;
				case 171: scroll = new DJ_SL_Meditation(); break;
				case 172: scroll = new DJ_SL_Mining(); break;
				case 173: scroll = new DJ_SL_Musicianship(); break;
				case 174: scroll = new DJ_SL_Necromancy(); break;
				case 175: scroll = new DJ_SL_Ninjitsu(); break;
				case 176: scroll = new DJ_SL_Parry(); break;
				case 177: scroll = new DJ_SL_Peacemaking(); break;
				case 178: scroll = new DJ_SL_Poisoning(); break;
				case 179: scroll = new DJ_SL_Provocation(); break;
				case 180: scroll = new DJ_SL_RemoveTrap(); break;
				case 181: scroll = new DJ_SL_Snooping(); break;
				case 182: scroll = new DJ_SL_SpiritSpeak(); break;
				case 183: scroll = new DJ_SL_Stealing(); break;
				case 184: scroll = new DJ_SL_Stealth(); break;
				case 185: scroll = new DJ_SL_Swords(); break;
				case 186: scroll = new DJ_SL_Tactics(); break;
				case 187: scroll = new DJ_SL_Tailoring(); break;
				case 188: scroll = new DJ_SL_Tinkering(); break;
				case 189: scroll = new DJ_SL_Tracking(); break;
				case 190: scroll = new DJ_SL_Veterinary(); break;
				case 191: scroll = new DJ_SL_Wrestling(); break;
				case 192: scroll = new DJ_SP_Alchemy(); break;
				case 193: scroll = new DJ_SP_Anatomy(); break;
				case 194: scroll = new DJ_SP_AnimalLore(); break;
				case 195: scroll = new DJ_SP_AnimalTaming(); break;
				case 196: scroll = new DJ_SP_Archery(); break;
				case 197: scroll = new DJ_SP_ArmsLore(); break;
				case 198: scroll = new DJ_SP_Blacksmith(); break;
				case 199: scroll = new DJ_SP_Bushido(); break;
				case 200: scroll = new DJ_SP_Carpentry(); break;
				case 201: scroll = new DJ_SP_Cartography(); break;
				case 202: scroll = new DJ_SP_Chivalry(); break;
				case 203: scroll = new DJ_SP_Cooking(); break;
				case 204: scroll = new DJ_SP_DetectHidden(); break;
				case 205: scroll = new DJ_SP_Discordance(); break;
				case 206: scroll = new DJ_SP_EvalInt(); break;
				case 207: scroll = new DJ_SP_Fencing(); break;
				case 208: scroll = new DJ_SP_Fishing(); break;
				case 209: scroll = new DJ_SP_Fletching(); break;
				case 210: scroll = new DJ_SP_Focus(); break;
				case 211: scroll = new DJ_SP_Healing(); break;
				case 212: scroll = new DJ_SP_Hiding(); break;
				case 213: scroll = new DJ_SP_Inscribe(); break;
				case 214: scroll = new DJ_SP_Lockpicking(); break;
				case 215: scroll = new DJ_SP_Lumberjacking(); break;
				case 216: scroll = new DJ_SP_Macing(); break;
				case 217: scroll = new DJ_SP_Magery(); break;
				case 218: scroll = new DJ_SP_MagicResist(); break;
				case 219: scroll = new DJ_SP_Meditation(); break;
				case 220: scroll = new DJ_SP_Mining(); break;
				case 221: scroll = new DJ_SP_Musicianship(); break;
				case 222: scroll = new DJ_SP_Necromancy(); break;
				case 223: scroll = new DJ_SP_Ninjitsu(); break;
				case 224: scroll = new DJ_SP_Parry(); break;
				case 225: scroll = new DJ_SP_Peacemaking(); break;
				case 226: scroll = new DJ_SP_Poisoning(); break;
				case 227: scroll = new DJ_SP_Provocation(); break;
				case 228: scroll = new DJ_SP_RemoveTrap(); break;
				case 229: scroll = new DJ_SP_Snooping(); break;
				case 230: scroll = new DJ_SP_SpiritSpeak(); break;
				case 231: scroll = new DJ_SP_Stealing(); break;
				case 232: scroll = new DJ_SP_Stealth(); break;
				case 233: scroll = new DJ_SP_Swords(); break;
				case 234: scroll = new DJ_SP_Tactics(); break;
				case 235: scroll = new DJ_SP_Tailoring(); break;
				case 236: scroll = new DJ_SP_Tinkering(); break;
				case 237: scroll = new DJ_SP_Tracking(); break;
				case 238: scroll = new DJ_SP_Veterinary(); break;
				case 239: scroll = new DJ_SP_Wrestling(); break;
			}

			return scroll;
		}
	}
}
