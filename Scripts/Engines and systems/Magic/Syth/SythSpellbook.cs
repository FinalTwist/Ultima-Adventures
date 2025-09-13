using System;
using Server.Network;
using Server.Gumps;
using Server.Spells;
using Server.Misc;
using Server.Items;

namespace Server.Items
{
	public class SythSpellbook : Spellbook
	{
		public Mobile owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public int crystals;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Crystals { get{ return crystals; } set{ crystals = value; } }

		public int page;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Page { get{ return page; } set{ page = value; } }

		public int names;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Names { get{ return names; } set{ names = value; } }

		public int gem;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Gem { get{ return gem; } set{ gem = value; } }

		public int steel;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Steel { get{ return steel; } set{ steel = value; } }

		public override SpellbookType SpellbookType{ get{ return SpellbookType.Syth; } }
		public override int BookOffset{ get{ return 270; } }
		public override int BookCount{ get{ return 11; } }

		[Constructable]
		public SythSpellbook( ulong content, Mobile gifted ) : base( content, 0x4CE0 )
		{
			owner = gifted;
			page = 0;
			crystals = 0;
			names = 0;
			Name = "Datacron of Syth Knowledge";
			Light = LightType.Circle225;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;

			if ( owner != from )
			{
				from.SendMessage( "This device seems strange to you." );
			}
			else if ( Parent == from || ( pack != null && Parent == pack ) )
			{
				from.SendSound( 0x54D );
				from.CloseGump( typeof( SythSpellbookGump ) );
				from.SendGump( new SythSpellbookGump( from, this, 1 ) );
			}
			else
			{
				from.SendMessage( "This datacron must be in your backpack (and not in a container within) to open." );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			bool doSythEffect = false;
			bool doGemColor = false;
			bool doSteelAdd = false;
			int color = Utility.RandomList( 0xB80, 0xB5E, 0xB39, 0xB3A, 0xA9F, 0x99E, 0x997, 0x8D9, 0x8DA, 0x8DB, 0x8DC, 0x8B9 );

			if ( dropped is HellShard )
			{
				if ( crystals >= 50000 )
				{
					from.SendMessage( "That datacron is already fully charged." );
				}
				else if ( ( crystals + dropped.Amount ) < 50000 )
				{
					from.SendMessage( "The datacron has been charged." );
					crystals = crystals + dropped.Amount;
					from.PlaySound( 0x54B );
					dropped.Delete();
				}
				else
				{
					int need = 50000 - crystals;
					from.SendMessage( "The datacron has been charged to maximum capacity, so you did not use all of them." );
					crystals = 50000;
					dropped.Amount = dropped.Amount - need;
					from.PlaySound( 0x54B );
				}
			}
			else if ( dropped is Ruby && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0x48E; } doGemColor = true; }
			else if ( dropped is Amber && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0xB17; } doGemColor = true; }
			else if ( dropped is Amethyst && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0x490; } doGemColor = true; }
			else if ( dropped is Citrine && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0x491; } doGemColor = true; }
			else if ( dropped is Emerald && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0x48F; } doGemColor = true; }
			else if ( dropped is Diamond && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0xB33; } doGemColor = true; }
			else if ( dropped is Sapphire && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0x48D; } doGemColor = true; }
			else if ( dropped is StarSapphire && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0x4AB; } doGemColor = true; }
			else if ( dropped is Tourmaline && dropped.Amount == 1 ){ if ( dropped.Hue > 0 ){ gem = dropped.Hue; } else { gem = 0xAFA; } doGemColor = true; }
			else if ( MaterialInfo.IsMagicTalisman( dropped ) ){ dropped.ItemID = 0x4CDE; dropped.Name = "Syth Exacron"; doSythEffect = true; }
			else if ( dropped.Name != null && (dropped.Name).Contains("Durasteel") && dropped.Hue == 0x7A9 && steel < 1 ){ steel = 1; doSteelAdd = true; }
			else if ( dropped != null && ( dropped is BaseArmor || dropped is BaseClothing || dropped is BaseJewel || dropped is BaseHat ) )
			{
				if ( dropped is BaseHat || dropped is MagicHat )
				{
					if ( dropped.ItemID == 0x4CDA ){ dropped.ItemID = 0x4CDC; dropped.Name = "Syth hood"; }
					else { dropped.ItemID = 0x4CDA; dropped.Name = "Syth cowl"; }
					doSythEffect = true;
				}
				else if ( dropped is BaseShield )
				{
					dropped.ItemID = 0x1BC3;
					dropped.Name = "Syth shield";
					doSythEffect = true;
				}
				else if ( dropped.Layer == Layer.OuterTorso )
				{
					if ( dropped.ItemID == 0x2B69 ){ dropped.ItemID = 0x2FB9; dropped.Name = "Syth shroud"; }
					else if ( dropped.ItemID == 0x2FB9 ){ dropped.ItemID = 0x2B69; dropped.Name = "Syth robe"; }
					else { dropped.ItemID = 0x2B69; dropped.Name = "Syth robe"; }
					doSythEffect = true;
				}
				else if ( dropped.Layer == Layer.Cloak && (
					dropped.ItemID == 0x1515 || 
					dropped.ItemID == 0x1530 || 
					dropped.ItemID == 0x2309 || 
					dropped.ItemID == 0x230A || 
					dropped.ItemID == 0x26AD || 
					dropped.ItemID == 0x2B04 || 
					dropped.ItemID == 0x2B05 || 
					dropped.ItemID == 0x2B76 || 
					dropped.ItemID == 0x316D || 
					dropped.ItemID == 0x5679 ) )
				{
					dropped.ItemID = 0x1515; dropped.Name = "Syth cloak";
					doSythEffect = true;
				}
				else if ( dropped.Layer == Layer.Helm && dropped is BaseArmor )
				{
					dropped.ItemID = 0x2FBB; dropped.Name = "Syth helm";
					doSythEffect = true;
				}
			}

			if ( doSythEffect )
			{
				from.PlaySound( 0x55B );
				from.RevealingAction();
				dropped.Hue = color;
				from.SendMessage( "The datacron transformed the item." );
			}
			else if ( doGemColor )
			{
				from.PlaySound( 0x55B );
				from.RevealingAction();
				dropped.Delete();
				from.SendMessage( "The add the colored gem to the datacron." );
			}
			else if ( doSteelAdd )
			{
				from.PlaySound( 0x55B );
				from.RevealingAction();
				dropped.Delete();
				from.SendMessage( "The add the piece of durasteel to the datacron." );
			}

			base.OnDragDrop( from, dropped );

			InvalidateProperties();
			return false;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "For " + owner.Name + "" ); }
        }

		public SythSpellbook( Serial serial ) : base( serial )
		{
		}

		public static bool HasSpell( Mobile from, int spellID )
		{
			Spellbook book = Spellbook.Find( from, spellID );

			return ( book != null && book.HasSpell( spellID ) );
		}

		public class PowerRow : Gump
		{
			private SythSpellbook mBook;
			public PowerRow( Mobile from, SythSpellbook book ): base( 25, 25 )
			{
				mBook = book;
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 11427);

				int icon = 269;
				int button = 0;
				int r = 57;

				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(r, 0, button, button, icon, GumpButtonType.Reply, 0); AddImage(r, 0, button, 0x22);
					r=r+50;
				}
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				Server.Spells.Syth.SythSpell.CastSpell( from, info.ButtonID );
				from.CloseGump( typeof( PowerRow ) );
				if ( Server.Misc.GetPlayerInfo.isSyth ( from, true ) )
				{
					from.SendGump( new PowerRow( from, mBook ) );
				}
			}
		}

		public class PowerColumn : Gump
		{
			private SythSpellbook mBook;
			public PowerColumn( Mobile from, SythSpellbook book ): base( 25, 25 )
			{
				mBook = book;
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 11427);

				int icon = 269;
				int button = 0;
				int r = 53;

				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
				icon++; if ( HasSpell( from, icon ) )
				{
					button = ( Int32.Parse( Server.Spells.Syth.SythSpell.SpellInfo( icon, 11 ) ) );
					AddButton(2, r, button, button, icon, GumpButtonType.Reply, 0); AddImage(2, r, button, 0x22);
					if ( book.names > 0 ){ AddLabel(57, (r+8), 0x481, @"" + Server.Spells.Syth.SythSpell.SpellInfo( icon, 1 ) + ""); }
					r=r+50;
				}
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				Server.Spells.Syth.SythSpell.CastSpell( from, info.ButtonID );
				from.CloseGump( typeof( PowerColumn ) );
				if ( Server.Misc.GetPlayerInfo.isSyth ( from, true ) )
				{
					from.SendGump( new PowerColumn( from, mBook ) );
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Mobile)owner);
			writer.Write( crystals );
			writer.Write( page );
			writer.Write( names );
			writer.Write( gem );
			writer.Write( steel );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			crystals = reader.ReadInt();
			page = reader.ReadInt();
			names = reader.ReadInt();
			gem = reader.ReadInt();
			steel = reader.ReadInt();
		}
	}
}
