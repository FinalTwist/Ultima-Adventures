using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
	public delegate void InstrumentPickedCallback( Mobile from, BaseInstrument instrument );

	public enum InstrumentQuality
	{
		Low,
		Regular,
		Exceptional
	}

	public abstract class BaseInstrument : Item, ICraftable, ISlayer
	{
		private int m_WellSound, m_BadlySound;
		private SlayerName m_Slayer, m_Slayer2;
		private InstrumentQuality m_Quality;
		private Mobile m_Crafter;
		private int m_UsesRemaining;

		private int m_MaxHitPoints;
		private int m_HitPoints;

		private AosAttributes m_AosAttributes;
		private AosElementAttributes m_AosResistances;
		private AosSkillBonuses m_AosSkillBonuses;
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SuccessSound
		{
			get{ return m_WellSound; }
			set{ m_WellSound = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int FailureSound
		{
			get{ return m_BadlySound; }
			set{ m_BadlySound = value; }
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
			get{ return m_Slayer2; }
			set{ m_Slayer2 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public InstrumentQuality Quality
		{
			get{ return m_Quality; }
			set{ UnscaleUses(); m_Quality = value; InvalidateProperties(); ScaleUses(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.Player )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); }
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

		public override int PhysicalResistance{ get{ return m_AosResistances.Physical; } }
		public override int FireResistance{ get{ return m_AosResistances.Fire; } }
		public override int ColdResistance{ get{ return m_AosResistances.Cold; } }
		public override int PoisonResistance{ get{ return m_AosResistances.Poison; } }
		public override int EnergyResistance{ get{ return m_AosResistances.Energy; } }

		public virtual int InitMinHits{ get{ return 0; } }
		public virtual int InitMaxHits{ get{ return 0; } }

		public virtual int InitMinUses{ get{ return 150; } }
		public virtual int InitMaxUses{ get{ return 200; } }

		public virtual TimeSpan ChargeReplenishRate { get { return TimeSpan.FromMinutes( 5.0 ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get{ CheckReplenishUses(); return m_UsesRemaining; }
			set{ m_UsesRemaining = value; InvalidateProperties(); }
		}

		private DateTime m_LastReplenished;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime LastReplenished
		{
			get { return m_LastReplenished; }
			set { m_LastReplenished = value; CheckReplenishUses(); }
		}

		private bool m_ReplenishesCharges;
		[CommandProperty( AccessLevel.GameMaster )]
		public bool ReplenishesCharges
		{
			get { return m_ReplenishesCharges; }
			set 
			{
				if( value != m_ReplenishesCharges && value )
					m_LastReplenished = DateTime.UtcNow;

				m_ReplenishesCharges = value; 
			}
		}

		public void CheckReplenishUses()
		{
			CheckReplenishUses( true );
		}

		public void CheckReplenishUses( bool invalidate )
		{
			if( !m_ReplenishesCharges || m_UsesRemaining >= InitMaxUses )
				return;

			if( m_LastReplenished + ChargeReplenishRate < DateTime.UtcNow )
			{
				TimeSpan timeDifference = DateTime.UtcNow - m_LastReplenished;

				m_UsesRemaining = Math.Min( m_UsesRemaining + (int)( timeDifference.Ticks / ChargeReplenishRate.Ticks), InitMaxUses );	//How rude of TimeSpan to not allow timespan division.
				m_LastReplenished = DateTime.UtcNow;

				if( invalidate )
					InvalidateProperties();

			}
		}

		public void ScaleUses()
		{
			UsesRemaining = (UsesRemaining * GetUsesScalar()) / 100;
			//InvalidateProperties();
		}

		public void UnscaleUses()
		{
			UsesRemaining = (UsesRemaining * 100) / GetUsesScalar();
		}

		public int GetUsesScalar()
		{
			if ( m_Quality == InstrumentQuality.Exceptional )
				return 200;

			return 100;
		}

		public void ConsumeUse( Mobile from )
		{
			// TODO: Confirm what must happen here?

			if ( UsesRemaining > 1 )
			{
				--UsesRemaining;
			}
			else
			{
				if ( from != null )
					from.SendLocalizedMessage( 502079 ); // The instrument played its last tune.

				Delete();
			}
		}

		private static Hashtable m_Instruments = new Hashtable();

		public static BaseInstrument GetInstrument( Mobile from )
		{
			BaseInstrument item = m_Instruments[from] as BaseInstrument;

			if ( item == null )
				return null;

			if ( item.Parent != from && !item.IsChildOf( from.Backpack ) )
			{
				m_Instruments.Remove( from );
				return null;
			}

			return item;
		}

		public static int GetBardRange( Mobile bard, SkillName skill )
		{
			return 8 + (int)(bard.Skills[skill].Value / 15);
		}

		public static void PickInstrument( Mobile from, InstrumentPickedCallback callback )
		{
			BaseInstrument instrument = GetInstrument( from );

			if ( instrument != null )
			{
				if ( callback != null )
					callback( from, instrument );
			}
			else
			{
				from.SendLocalizedMessage( 500617 ); // What instrument shall you play?
				from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( OnPickedInstrument ), callback );
			}
		}

		public static void OnPickedInstrument( Mobile from, object targeted, object state )
		{
			BaseInstrument instrument = targeted as BaseInstrument;

			if ( instrument == null )
			{
				from.SendLocalizedMessage( 500619 ); // That is not a musical instrument.
			}
			else
			{
				SetInstrument( from, instrument );

				InstrumentPickedCallback callback = state as InstrumentPickedCallback;

				if ( callback != null )
					callback( from, instrument );
			}
		}

		public static bool IsMageryCreature( BaseCreature bc )
		{
			return ( bc != null && bc.AI == AIType.AI_Mage && bc.Skills[SkillName.Magery].Base > 5.0 );
		}

		public static bool IsFireBreathingCreature( BaseCreature bc )
		{
			if ( bc == null )
				return false;

			return bc.HasBreath;
		}

		public static bool IsPoisonImmune( BaseCreature bc )
		{
			return ( bc != null && bc.PoisonImmune != null );
		}

		public static int GetPoisonLevel( BaseCreature bc )
		{
			if ( bc == null )
				return 0;

			Poison p = bc.HitPoison;

			if ( p == null )
				return 0;

			return p.Level + 1;
		}

		public static double GetBaseDifficulty( Mobile targ )
		{

			if ( !(targ is BaseCreature))
				return 110;
				
			BaseCreature from = (BaseCreature)targ;

			double finalcompute = from.Fame;
			double val = 0;

			//diminishing returns to prevent 125+ min taming required
			double final = 0;

					double karmatotame = 50; // 
					double diminish = 0.85; // 
					double step = 500;

					if (finalcompute < step)
						final = finalcompute / karmatotame;	

					else 
					{	
						while ( finalcompute > 0 )
						{
							if (finalcompute > step)
							{
								finalcompute -= step;
								final += step / karmatotame;
								
								if (final < 105)
									finalcompute *= diminish;
								else
									finalcompute *= (diminish * ((125 - final) / 20)); // makes sure max is 122 skill
							}
							else
							{
								final += (finalcompute/karmatotame) * diminish;
								finalcompute = 0;
							}
						}
					}			

					val = (int)Math.Ceiling(final) + (15-(15*(final/125))); // 15 addition only applies to taming value under 100


			return val * 1.25;
		}

		public double GetDifficultyFor(Mobile from, Mobile targ )
		{
			double val = GetBaseDifficulty( targ );

			if ( m_Quality == InstrumentQuality.Exceptional )
				val -= 5.0; // 10%

			if ( targ is BaseCreature )
			{
				if ( ((BaseCreature)targ).BardImmune )
				{
					val += 25.0; // -30%
				}
			}

			if ( m_Slayer != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer );

				if ( entry != null )
				{
					if ( entry.Slays( targ ) )
						val -= 35.0; // 20%
					else if ( entry.Group.OppositionSuperSlays( targ ) )
						val += 15.0; // -20%
				}
			}

			if ( m_Slayer2 != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer2 );

				if ( entry != null )
				{
					if ( entry.Slays( targ ) )
						val -= 35.0; // 20%
					else if ( entry.Group.OppositionSuperSlays( targ ) )
						val += 15.0; // -20%
				}
			}

			if (from is PlayerMobile && ((PlayerMobile)from).Troubadour())
			{
				if (from.Dex > 500)
					val -= 35;
				else if (from.Dex > 300)
					val -= 20;
				else if (from.Dex > 175)
					val -= 10;
				else if (from.Dex > 90)
					val -= 5;
			}

			return val;
		}

		public static void SetInstrument( Mobile from, BaseInstrument item )
		{
			m_Instruments[from] = item;
		}

		public override void OnAfterDuped( Item newItem )
		{
			BaseInstrument lute = newItem as BaseInstrument;

			if ( lute == null )
				return;

			lute.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			lute.m_AosResistances = new AosElementAttributes( newItem, m_AosResistances );
			lute.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
		}

		public virtual int ArtifactRarity{ get{ return 0; } }

		public BaseInstrument( int itemID, int wellSound, int badlySound ) : base( itemID )
		{
			m_WellSound = wellSound;
			m_BadlySound = badlySound;
			UsesRemaining = Utility.RandomMinMax( InitMinUses, InitMaxUses );

			m_AosAttributes = new AosAttributes( this );
			m_AosResistances = new AosElementAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_Resource = CraftResource.RegularWood;

			Layer = Layer.Talisman;

			m_HitPoints = m_MaxHitPoints = Utility.RandomMinMax( InitMinHits, InitMaxHits );
		}

		public override void OnSingleClick( Mobile from )
		{
			ArrayList attrs = new ArrayList();

			if ( DisplayLootType )
			{
				if ( LootType == LootType.Blessed )
					attrs.Add( new EquipInfoAttribute( 1038021 ) ); // blessed
				else if ( LootType == LootType.Cursed )
					attrs.Add( new EquipInfoAttribute( 1049643 ) ); // cursed
			}

			if ( m_Quality == InstrumentQuality.Exceptional )
				attrs.Add( new EquipInfoAttribute( 1018305 - (int)m_Quality ) );

			if( m_ReplenishesCharges )
				attrs.Add( new EquipInfoAttribute( 1070928 ) ); // Replenish Charges

			// TODO: Must this support item identification?
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

			EquipmentInfo eqInfo = new EquipmentInfo( number, m_Crafter, false, (EquipInfoAttribute[])attrs.ToArray( typeof( EquipInfoAttribute ) ) );

			from.Send( new DisplayEquipmentInfo( this, eqInfo ) );
		}

		public override void OnAdded(IEntity parent )
		{
			if ( Core.AOS && parent is Mobile && !(Server.Misc.AdventuresFunctions.IsPuritain((object)parent)))
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.AddTo( from );

				int strBonus = m_AosAttributes.BonusStr;
				int dexBonus = m_AosAttributes.BonusDex;
				int intBonus = m_AosAttributes.BonusInt;

				if ( strBonus != 0 || dexBonus != 0 || intBonus != 0 )
				{
					string modName = this.Serial.ToString();

					if ( strBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

					if ( dexBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

					if ( intBonus != 0 )
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
				}

				from.CheckStatTimers();
			}
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( Core.AOS && parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				from.RemoveStatMod( modName + "Str" );
				from.RemoveStatMod( modName + "Dex" );
				from.RemoveStatMod( modName + "Int" );

				from.CheckStatTimers();
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			int oldUses = m_UsesRemaining;
			CheckReplenishUses( false );

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~

			if( m_ReplenishesCharges )
				list.Add( 1070928 ); // Replenish Charges

			int oreType;

			switch ( m_Resource )
			{
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

			if ( m_Quality == InstrumentQuality.Exceptional )
			{
				if ( oreType != 0 )
					list.Add( 1053100, "#{0}\t{1}", oreType, "Wood" ); // exceptional ~1_oretype~ ~2_armortype~
				else
					list.Add( 1050040, "Wood" ); // exceptional ~1_ITEMNAME~
			}
			else
			{
				if ( oreType != 0 )
					list.Add( 1053099, "#{0}\t{1}", oreType, "Wood" ); // ~1_oretype~ ~2_armortype~
			}

			bool MD = false;
			if (Server.Misc.AdventuresFunctions.IsPuritain((object)this))
				MD = true;

			m_AosSkillBonuses.GetProperties( list );

			int prop;

			if ( (prop = ArtifactRarity) > 0 )
				list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

			if ( (prop = m_AosAttributes.WeaponDamage) != 0 && !MD)
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 && !MD)
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 && !MD)
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 && !MD)
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 && !MD)
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 && !MD)
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = m_AosAttributes.AttackChance) != 0 && !MD)
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusHits) != 0 && !MD)
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 && !MD)
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 && !MD)
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 && !MD)
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = m_AosAttributes.Luck) != 0 && !MD)
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosAttributes.BonusMana) != 0 && !MD)
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 && !MD)
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 && !MD)
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 && !MD)
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 && !MD)
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 && !MD)
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 && !MD)
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 && !MD)
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 && !MD)
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			base.AddResistanceProperties( list );

			if ( m_HitPoints >= 0 && m_MaxHitPoints > 0 )
				list.Add( 1060639, "{0}\t{1}", m_HitPoints, m_MaxHitPoints ); // durability ~1_val~ / ~2_val~

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

			if( m_UsesRemaining != oldUses )
				Timer.DelayCall( TimeSpan.Zero, new TimerCallback( InvalidateProperties ) );
		}

		public override bool OnEquip( Mobile from )
		{
			if ( from.Skills[SkillName.Musicianship].Base < 30 )
			{
				from.SendMessage("Your need to at least a natural neophyte skill in musicianship to equip that!");
				return false;
			}

			return base.OnEquip( from );
		}

		public BaseInstrument( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version

			writer.Write( m_ReplenishesCharges );
			if( m_ReplenishesCharges )
				writer.Write( m_LastReplenished );

			writer.Write( m_Crafter );

			writer.WriteEncodedInt( (int) m_Quality );
			writer.WriteEncodedInt( (int) m_Slayer );
			writer.WriteEncodedInt( (int) m_Slayer2 );

			writer.WriteEncodedInt( (int)UsesRemaining );

			writer.WriteEncodedInt( (int) m_WellSound );
			writer.WriteEncodedInt( (int) m_BadlySound );

			writer.WriteEncodedInt( (int) m_MaxHitPoints );
			writer.WriteEncodedInt( (int) m_HitPoints );

			writer.WriteEncodedInt( (int) m_Resource );

			m_AosAttributes.Serialize( writer );
			m_AosResistances.Serialize( writer );
			m_AosSkillBonuses.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_ReplenishesCharges = reader.ReadBool();
			if( m_ReplenishesCharges )
				m_LastReplenished = reader.ReadDateTime();

			m_Crafter = reader.ReadMobile();

			m_Quality = (InstrumentQuality)reader.ReadEncodedInt();
			m_Slayer = (SlayerName)reader.ReadEncodedInt();
			m_Slayer2 = (SlayerName)reader.ReadEncodedInt();

			UsesRemaining = reader.ReadEncodedInt();

			m_WellSound = reader.ReadEncodedInt();
			m_BadlySound = reader.ReadEncodedInt();

			m_MaxHitPoints = reader.ReadEncodedInt();
			m_HitPoints = reader.ReadEncodedInt();

			m_Resource = (CraftResource)reader.ReadEncodedInt();

			m_AosAttributes = new AosAttributes( this, reader );
			m_AosResistances = new AosElementAttributes( this, reader );
			m_AosSkillBonuses = new AosSkillBonuses( this, reader );

			CheckReplenishUses();

			if ( Core.AOS && Parent is Mobile && !(Server.Misc.AdventuresFunctions.IsPuritain((object)Parent)))
				m_AosSkillBonuses.AddTo( (Mobile)Parent );

			int strBonus = m_AosAttributes.BonusStr;
			int dexBonus = m_AosAttributes.BonusDex;
			int intBonus = m_AosAttributes.BonusInt;

			if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) && !(Server.Misc.AdventuresFunctions.IsPuritain((object)Parent)))
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
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
			}
			else if ( from.BeginAction( typeof( BaseInstrument ) ) )
			{
				SetInstrument( from, this );
				this.ConsumeUse( from );

				// Delay of 7 second before beign able to play another instrument again
				new InternalTimer( from ).Start();

				if ( CheckMusicianship( from ) )
					PlayInstrumentWell( from );
				else
					PlayInstrumentBadly( from );
			}
			else
			{
				from.SendLocalizedMessage( 500119 ); // You must wait to perform another action
			}
		}

		public static bool CheckMusicianship( Mobile m )
		{
			m.CheckSkill( SkillName.Musicianship, 0.0, 120.0 );

			return ( (m.Skills[SkillName.Musicianship].Value / 100) > Utility.RandomDouble() );
		}

		public void PlayInstrumentWell( Mobile from )
		{
			from.PlaySound( m_WellSound );
		}

		public void PlayInstrumentBadly( Mobile from )
		{
			from.PlaySound( m_BadlySound );
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 6.0 ) )
			{
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_From.EndAction( typeof( BaseInstrument ) );
			}
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (InstrumentQuality)quality;

			if ( makersMark )
				Crafter = from;

			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			return quality;
		}

		#endregion
	}
}