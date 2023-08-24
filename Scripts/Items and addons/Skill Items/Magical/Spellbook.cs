using System;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Engines.Craft;
using Server.Network;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
	public enum SpellbookType
	{
		Invalid = -1,
		Regular,
		Necromancer,
		Paladin,
		Ninja,
		Samurai,
		Arcanist,
		Song,
		DeathKnight,
		HolyMan,
		Mystic,
		Syth,
		Jedi
	}

	public class Spellbook : Item, ICraftable, ISlayer
	{
		public static void Initialize()
		{
			EventSink.OpenSpellbookRequest += new OpenSpellbookRequestEventHandler( EventSink_OpenSpellbookRequest );
			EventSink.CastSpellRequest += new CastSpellRequestEventHandler( EventSink_CastSpellRequest );

			CommandSystem.Register( "AllSpells", AccessLevel.GameMaster, new CommandEventHandler( AllSpells_OnCommand ) );
		}

		[Usage( "AllSpells" )]
		[Description( "Completely fills a targeted spellbook with scrolls." )]
		private static void AllSpells_OnCommand( CommandEventArgs e )
		{
			e.Mobile.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( AllSpells_OnTarget ) );
			e.Mobile.SendMessage( "Target the spellbook to fill." );
		}

		private static void AllSpells_OnTarget( Mobile from, object obj )
		{
			if ( obj is Spellbook )
			{
				Spellbook book = (Spellbook)obj;

				if ( book.BookCount == 64 )
					book.Content = ulong.MaxValue;
				else
					book.Content = (1ul << book.BookCount) - 1;

				from.SendMessage( "The spellbook has been filled." );

				CommandLogging.WriteLine( from, "{0} {1} filling spellbook {2}", from.AccessLevel, CommandLogging.Format( from ), CommandLogging.Format( book ) );
			}
			else
			{
				from.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( AllSpells_OnTarget ) );
				from.SendMessage( "That is not a spellbook. Try again." );
			}
		}

		private static void EventSink_OpenSpellbookRequest( OpenSpellbookRequestEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( from ) )
				return; // They are customizing

			SpellbookType type;

			switch ( e.Type )
			{
				default:
				case 1: 	type = SpellbookType.Regular; break;
				case 2: 	type = SpellbookType.Necromancer; break;
				case 3: 	type = SpellbookType.Paladin; break;
				case 4: 	type = SpellbookType.Ninja; break;
				case 5: 	type = SpellbookType.Samurai; break;
				case 6:		type = SpellbookType.Arcanist; break;
				case 7:		type = SpellbookType.Song; break;
				case 8:		type = SpellbookType.DeathKnight; break;
				case 9:		type = SpellbookType.HolyMan; break;
				case 10:	type = SpellbookType.Mystic; break;
				case 11:	type = SpellbookType.Syth; break;
					case 12:	type = SpellbookType.Jedi; break;
			}

			Spellbook book = Spellbook.Find( from, -1, type );

			if ( book != null )
				book.DisplayTo( from );
		}

		private static void EventSink_CastSpellRequest( CastSpellRequestEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !Multis.DesignContext.Check( from ) )
				return; // They are customizing

			Spellbook book = e.Spellbook as Spellbook;
			int spellID = e.SpellID;

			if ( book == null || !book.HasSpell( spellID ) )
				book = Find( from, spellID );

			if ( book != null && book.HasSpell( spellID ) )
			{
				SpecialMove move = SpellRegistry.GetSpecialMove( spellID );

				if ( move != null )
				{
					SpecialMove.SetCurrentMove( from, move );
				}
				else
				{
					Spell spell = SpellRegistry.NewSpell( spellID, from, null );
	
					if ( spell != null )
						spell.Cast();
					else
						from.SendLocalizedMessage( 502345 ); // This spell has been temporarily disabled.
				}
			}
			else
			{
				from.SendLocalizedMessage( 500015 ); // You do not have that spell!
			}
		}

		private static Dictionary<Mobile, List<Spellbook>> m_Table = new Dictionary<Mobile, List<Spellbook>>();

		public static SpellbookType GetTypeForSpell( int spellID )
		{
			if ( spellID >= 0 && spellID < 64 )
				return SpellbookType.Regular;
			else if ( spellID >= 100 && spellID < 117 )
				return SpellbookType.Necromancer;
			else if ( spellID >= 200 && spellID < 210 )
				return SpellbookType.Paladin;
			else if( spellID >= 400 && spellID < 406 )
				return SpellbookType.Samurai;
			else if( spellID >= 500 && spellID < 508 )
				return SpellbookType.Ninja;
			else if ( spellID >= 600 && spellID < 617 )
				return SpellbookType.Arcanist;
			else if ( spellID >= 351 && spellID < 368 )
				return SpellbookType.Song;
			else if ( spellID >= 750 && spellID < 764 )
				return SpellbookType.DeathKnight;
			else if ( spellID >= 770 && spellID < 784 )
				return SpellbookType.HolyMan;
			else if ( spellID >= 250 && spellID < 260 )
				return SpellbookType.Mystic;
			else if ( spellID >= 270 && spellID < 280 )
				return SpellbookType.Syth;
				else if ( spellID >= 280 && spellID < 290 )
				return SpellbookType.Jedi;

			return SpellbookType.Invalid;
		}

		public static Spellbook FindRegular( Mobile from )
		{
			return Find( from, -1, SpellbookType.Regular );
		}

		public static Spellbook FindNecromancer( Mobile from )
		{
			return Find( from, -1, SpellbookType.Necromancer );
		}

		public static Spellbook FindPaladin( Mobile from )
		{
			return Find( from, -1, SpellbookType.Paladin );
		}

		public static Spellbook FindSamurai( Mobile from )
		{
			return Find( from, -1, SpellbookType.Samurai );
		}

		public static Spellbook FindNinja( Mobile from )
		{
			return Find( from, -1, SpellbookType.Ninja );
		}

		public static Spellbook FindArcanist( Mobile from )
		{
			return Find( from, -1, SpellbookType.Arcanist );
		}

		public static Spellbook FindSong( Mobile from )
		{
			return Find( from, -1, SpellbookType.Song );
		}

		public static Spellbook FindDeathKnight( Mobile from )
		{
			return Find( from, -1, SpellbookType.DeathKnight );
		}

		public static Spellbook FindHolyMan( Mobile from )
		{
			return Find( from, -1, SpellbookType.HolyMan );
		}

		public static Spellbook FindMystic( Mobile from )
		{
			return Find( from, -1, SpellbookType.Mystic );
		}

		public static Spellbook FindSyth( Mobile from )
		{
			return Find( from, -1, SpellbookType.Syth );
		}
		public static Spellbook FindJedi( Mobile from )
		{
			return Find( from, -1, SpellbookType.Jedi );
		}

		public static Spellbook Find( Mobile from, int spellID )
		{
			return Find( from, spellID, GetTypeForSpell( spellID ) );
		}

		public static Spellbook Find( Mobile from, int spellID, SpellbookType type )
		{
			if ( from == null )
				return null;

			if ( from.Deleted )
			{
				m_Table.Remove( from );
				return null;
			}

			List<Spellbook> list = null;

			m_Table.TryGetValue( from, out list );

			bool searchAgain = false;

			if ( list == null )
				m_Table[from] = list = FindAllSpellbooks( from );
			else
				searchAgain = true;

			Spellbook book = FindSpellbookInList( list, from, spellID, type );

			if ( book == null && searchAgain )
			{
				m_Table[from] = list = FindAllSpellbooks( from );

				book = FindSpellbookInList( list, from, spellID, type );
			}

			return book;
		}

		public static Spellbook FindSpellbookInList( List<Spellbook> list, Mobile from, int spellID, SpellbookType type )
		{
			Container pack = from.Backpack;

			for ( int i = list.Count - 1; i >= 0; --i )
			{
				if ( i >= list.Count )
					continue;

				Spellbook book = list[i];

				if ( !book.Deleted && (book.Parent == from || (pack != null && book.Parent == pack)) && ValidateSpellbook( book, spellID, type ) )
					return book;

				list.RemoveAt( i );
			}

			return null;
		}

		public static List<Spellbook> FindAllSpellbooks( Mobile from )
		{
			List<Spellbook> list = new List<Spellbook>();

			Item item = from.FindItemOnLayer( Layer.Talisman );

			if ( item is Spellbook )
			{
				list.Add( (Spellbook)item );
			}
		
			Container pack = from.Backpack;

			if ( pack == null )
				return list;

			for ( int i = 0; i < pack.Items.Count; ++i )
			{
				item = pack.Items[i];

				if ( item is Spellbook )
					list.Add( (Spellbook)item );
			}

			return list;
		}

		public static Spellbook FindEquippedSpellbook( Mobile from )
		{
			Item item = from.FindItemOnLayer( Layer.Talisman );
			if ( item is Spellbook )
			{
				return (item as Spellbook);
			}

			return (from.FindItemOnLayer( Layer.Talisman ) as Spellbook);
		}

		public static bool ValidateSpellbook( Spellbook book, int spellID, SpellbookType type )
		{
			return ( book.SpellbookType == type && ( spellID == -1 || book.HasSpell( spellID ) ) );
		}

		public override bool DisplayWeight { get { return false; } }

		private AosAttributes m_AosAttributes;
		private AosSkillBonuses m_AosSkillBonuses;

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		public virtual SpellbookType SpellbookType{ get{ return SpellbookType.Regular; } }
		public virtual int BookOffset{ get{ return 0; } }
		public virtual int BookCount{ get{ return 64; } }

		private ulong m_Content;
		private int m_Count;

		public override bool AllowSecureTrade( Mobile from, Mobile to, Mobile newOwner, bool accepted )
		{

			return base.AllowSecureTrade( from, to, newOwner, accepted );
		}

		public override bool CanEquip( Mobile from )
		{
			 if ( !from.CanBeginAction( typeof( BaseWeapon ) ) )
			{
				return false;
			}

			return base.CanEquip( from );
		}

		public override bool AllowEquipedCast( Mobile from )
		{
			return true;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is SpellScroll && dropped.Amount == 1 )
			{
				SpellScroll scroll = (SpellScroll)dropped;

				SpellbookType type = GetTypeForSpell( scroll.SpellID );

				if ( type != this.SpellbookType )
				{
					return false;
				}
				else if ( HasSpell( scroll.SpellID ) )
				{
					if ( this is SythSpellbook ){ from.SendMessage( "That power is already in that datacron." ); }
					
					else if ( this is JediSpellbook ){ from.SendMessage( "That wisdom is already in that datacron." ); }
				

					return false;
				}
				else
				{
					int val = scroll.SpellID - BookOffset;

					if ( val >= 0 && val < BookCount )
					{
						m_Content |= (ulong)1 << val;
						++m_Count;

						InvalidateProperties();

						scroll.Delete();

						if ( this is SythSpellbook ){ from.SendSound( 0x558 ); }
					    
						else if ( this is JediSpellbook ){ from.SendSound( 0x558 ); }

					
						else { from.Send( new PlaySound( 0x249, GetWorldLocation() ) ); }
						return true;
					}

					return false;
				}
			}
			else
			{
				return false;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public ulong Content
		{
			get
			{
				return m_Content;
			}
			set
			{
				if ( m_Content != value )
				{
					m_Content = value;

					m_Count = 0;

					while ( value > 0 )
					{
						m_Count += (int)(value & 0x1);
						value >>= 1;
					}

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpellCount
		{
			get
			{
				return m_Count;
			}
		}

		[Constructable]
		public Spellbook() : this( (ulong)0 )
		{
		}

		[Constructable]
		public Spellbook( ulong content ) : this( content, 0xEFA )
		{
		}

		public Spellbook( ulong content, int itemID ) : base( itemID )
		{
			m_AosAttributes = new AosAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );

			Weight = 3.0;
			Layer = Layer.Talisman;

			// LootType = LootType.Blessed; // WIZARD WANTS NO BLESSING HERE

			Content = content;
		}

		public override void OnAfterDuped( Item newItem )
		{
			Spellbook book = newItem as Spellbook;

			if ( book == null )
				return;

			book.m_AosAttributes = new AosAttributes( newItem, m_AosAttributes );
			book.m_AosSkillBonuses = new AosSkillBonuses( newItem, m_AosSkillBonuses );
		}

		public override void OnAdded(IEntity parent )
		{
			if ( Core.AOS && parent is Mobile  && !(Server.Misc.AdventuresFunctions.IsPuritain((object)parent)))
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

		public override bool OnEquip( Mobile from )
		{
			if ( this is SongBook ){ if ( from.Skills[SkillName.Musicianship].Base < 30 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in musicianship to equip that!");
				return false;
			}}
			else if ( this is NecromancerSpellbook ){ if ( from.Skills[SkillName.Necromancy].Base < 30 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in necromancy to equip that!");
				return false;
			}}
			else if ( this is BookOfNinjitsu ){ if ( from.Skills[SkillName.Ninjitsu].Base < 30 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in ninjitsu to equip that!");
				return false;
			}}
			else if ( this is BookOfBushido ){ if ( from.Skills[SkillName.Bushido].Base < 30 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in bushido to equip that!");
				return false;
			}}
			else if ( this is BookOfChivalry ){ if ( from.Skills[SkillName.Chivalry].Base < 30 && from.Karma < 0 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in chivalry to equip that!");
				return false;
			}}
			else if ( this is DeathKnightSpellbook ){ if ( from.Skills[SkillName.Chivalry].Base < 30 && from.Karma > 0 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in chivalry to equip that!");
				return false;
			}}
			else if ( this is HolyManSpellbook )
			{
				return false;
			}
			else if ( this is MysticSpellbook )
			{
				return false;
			}
			else if ( this is SythSpellbook )
			{
				return false;
			}
			
			else if ( this is JediSpellbook )
			{
				return false;
			}
			
			else if ( from.Skills[SkillName.Magery].Base < 30 )
			{
				from.SendMessage("Your need at least a natural neophyte skill in magery to equip that!");
				return false;
			}

			return base.OnEquip( from );
		}

		public bool HasSpell( int spellID )
		{
			spellID -= BookOffset;

			return ( spellID >= 0 && spellID < BookCount && (m_Content & ((ulong)1 << spellID)) != 0 );
		}

		public Spellbook( Serial serial ) : base( serial )
		{
		}

		public void DisplayTo( Mobile to )
		{
			// The client must know about the spellbook or it will crash!

			NetState ns = to.NetState;

			if ( ns == null )
				return;

			if ( Parent == null )
			{
				to.Send( this.WorldPacket );
			}
			else if ( Parent is Item )
			{
				// What will happen if the client doesn't know about our parent?
				if ( ns.ContainerGridLines )
					to.Send( new ContainerContentUpdate6017( this ) );
				else
					to.Send( new ContainerContentUpdate( this ) );
			}
			else if ( Parent is Mobile )
			{
				// What will happen if the client doesn't know about our parent?
				to.Send( new EquipUpdate( this ) );
			}

			if ( ns.HighSeas )
				to.Send( new DisplaySpellbookHS( this ) );
			else
				to.Send( new DisplaySpellbook( this ) );

			if ( Core.AOS ) {

				if ( ns.NewSpellbook ) {
					to.Send( new NewSpellbookContent( this, ItemID, BookOffset + 1, m_Content ) );
				} else {
					//to.Send( new SpellbookContent( m_Count, BookOffset + 1, m_Content, this ) );
				}
			}
			else {
				if ( ns.ContainerGridLines ) {
					to.Send( new SpellbookContent6017( m_Count, BookOffset + 1, m_Content, this ) );
				} else {
					to.Send( new SpellbookContent( m_Count, BookOffset + 1, m_Content, this ) );
				}
			}
		}

		private Mobile m_Crafter;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~
				
			m_AosSkillBonuses.GetProperties( list );

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


			
			bool MD = false;
			if (Server.Misc.AdventuresFunctions.IsPuritain((object)this))
				MD = true;

			int prop;

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

			if ( this is SongBook )
			{
				if ( m_Count == 1 ){ list.Add( 1049644, "1 Song" );  } else { list.Add( 1049644, "" + m_Count.ToString() + " Songs" ); }
			}
			else if ( this is MysticSpellbook )
			{
				if ( m_Count == 1 ){ list.Add( 1049644, "1 Ability" );  } else { list.Add( 1049644, "" + m_Count.ToString() + " Abilities" ); }
			}
			else if ( this is SythSpellbook )
			{
				if ( m_Count == 1 ){ list.Add( 1049644, "1 Power" );  } else { list.Add( 1049644, "" + m_Count.ToString() + " Powers" ); }
			}
			
			else if ( this is JediSpellbook )
			{
				if ( m_Count == 1 ){ list.Add( 1049644, "1 Power" );  } else { list.Add( 1049644, "" + m_Count.ToString() + " Powers" ); }
			}
			
			else
			{
				list.Add( 1042886, m_Count.ToString() ); // ~1_NUMBERS_OF_SPELLS~ Spells
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Crafter != null )
				this.LabelTo( from, 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			this.LabelTo( from, 1042886, m_Count.ToString() );
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;

			if ( Parent == from || ( pack != null && Parent == pack ) )
				DisplayTo( from );
			else
				from.SendLocalizedMessage( 500207 ); // The spellbook must be in your backpack (and not in a container within) to open.
		}


		private SlayerName m_Slayer;
		private SlayerName m_Slayer2;
		//Currently though there are no dual slayer spellbooks, OSI has a habit of putting dual slayer stuff in later

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer
		{
			get { return m_Slayer; }
			set { m_Slayer = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer2
		{
			get { return m_Slayer2; }
			set { m_Slayer2 = value; InvalidateProperties(); }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version
			writer.Write( m_Crafter );

			writer.Write( (int)m_Slayer );
			writer.Write( (int)m_Slayer2 );

			m_AosAttributes.Serialize( writer );
			m_AosSkillBonuses.Serialize( writer );

			writer.Write( m_Content );
			writer.Write( m_Count );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_Crafter = reader.ReadMobile();
					goto case 2;
				}
				case 2:
				{
					m_Slayer = (SlayerName)reader.ReadInt();
					m_Slayer2 = (SlayerName)reader.ReadInt();
					goto case 1;
				}
				case 1:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );

					goto case 0;
				}
				case 0:
				{
					m_Content = reader.ReadULong();
					m_Count = reader.ReadInt();

					break;
				}
			}

			bool MD = false;
			if (Server.Misc.AdventuresFunctions.IsPuritain((object)Parent))
				MD = true;

			if ( m_AosAttributes == null &&!MD)
				m_AosAttributes = new AosAttributes( this );

			if ( m_AosSkillBonuses == null &&!MD)
				m_AosSkillBonuses = new AosSkillBonuses( this );

			if ( Core.AOS && Parent is Mobile &&!MD)
				m_AosSkillBonuses.AddTo( (Mobile) Parent );

			int strBonus = m_AosAttributes.BonusStr;
			int dexBonus = m_AosAttributes.BonusDex;
			int intBonus = m_AosAttributes.BonusInt;

			if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) && !MD)
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

		private static int[] m_LegendPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,	// 0 properties : 21/52 : 40%
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,					// 1 property   : 15/52 : 29%
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,									// 2 properties : 10/52 : 19%
				3, 3, 3, 3, 3, 3												// 3 properties :  6/52 : 12%

			};

		private static int[] m_ElderPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,// 0 properties : 15/34 : 44%
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 				// 1 property   : 10/34 : 29%
				2, 2, 2, 2, 2, 2,							// 2 properties :  6/34 : 18%
				3, 3, 3										// 3 properties :  3/34 :  9%
			};

		private static int[] m_GrandPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,	// 0 properties : 10/20 : 50%
				1, 1, 1, 1, 1, 1,				// 1 property   :  6/20 : 30%
				2, 2, 2,						// 2 properties :  3/20 : 15%
				3								// 3 properties :  1/20 :  5%
			};

		private static int[] m_MasterPropertyCounts = new int[]
			{
				0, 0, 0, 0, 0, 0,				// 0 properties : 6/10 : 60%
				1, 1, 1,						// 1 property   : 3/10 : 30%
				2								// 2 properties : 1/10 : 10%
			};

		private static int[] m_AdeptPropertyCounts = new int[]
			{
				0, 0, 0,						// 0 properties : 3/4 : 75%
				1								// 1 property   : 1/4 : 25%
			};

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			int magery = from.Skills.Magery.BaseFixedPoint;

			if ( magery >= 800 )
			{
				int[] propertyCounts;
				int minIntensity;
				int maxIntensity;

				if ( magery >= 1000 )
				{
					if( magery >= 1200 )
						propertyCounts = m_LegendPropertyCounts;
					else if( magery >= 1100 )
						propertyCounts = m_ElderPropertyCounts;
					else
						propertyCounts = m_GrandPropertyCounts;

					minIntensity = 55;
					maxIntensity = 75;
				}
				else if ( magery >= 900 )
				{
					propertyCounts = m_MasterPropertyCounts;
					minIntensity = 25;
					maxIntensity = 45;
				}
				else
				{
					propertyCounts = m_AdeptPropertyCounts;
					minIntensity = 0;
					maxIntensity = 15;
				}

				int propertyCount = propertyCounts[Utility.Random( propertyCounts.Length )];

				BaseRunicTool.ApplyAttributesTo( this, true, 0, propertyCount, minIntensity, maxIntensity );
			}

			if ( makersMark )
				Crafter = from;

			return quality;
		}
	}
}