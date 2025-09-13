using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
	public class MapWorld : Item, ICraftable
	{
		public int WorldMap;
		
		[CommandProperty(AccessLevel.Owner)]
		public int World_Map { get { return WorldMap; } set { WorldMap = value; InvalidateProperties(); } }

		[Constructable]
		public MapWorld( ) : base( 0x4CC2 )
		{
			Weight = 1.0;
			Name = "World Map";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			CraftInit( from );
			return 1;
		}

		public virtual void CraftInit( Mobile from )
		{
            Map map = from.Map;

            if (map == Map.Trammel && from.X>5124 && from.Y>3041 && from.X<6147 && from.Y<4092)
				WorldMap = 7; // AMBROSIA

            else if (map == Map.Trammel && from.X>859 && from.Y>3181 && from.X<2133 && from.Y<4092)
				WorldMap = 8; // UMBER VEIL

            else if (map == Map.Malas && from.X<1870)
				WorldMap = 4; // SERPENT ISLAND

            else if (map == Map.Tokuno)
				WorldMap = 5; // ISLES OF DREAD

            else if (map == Map.TerMur && from.X>132 && from.Y>4 && from.X<1165 && from.Y<1798)
				WorldMap = 6; // SAVAGE EMPIRE

            else if (map == Map.Trammel && from.X>6125 && from.Y>824 && from.X<7175 && from.Y<2746)
				WorldMap = 9; // BOTTLE

            else if (map == Map.Trammel && from.X<5121 && from.Y<3128)
				WorldMap = 1; // SOSARIA

            else if (map == Map.Felucca && from.X>6442 && from.Y>3051 && from.X<7007 && from.Y<3478)
				WorldMap = 2; // LODORIA

            else if (map == Map.Felucca && from.X<5420 && from.Y<4096)
				WorldMap = 2; // LODORIA

            else if (map == Map.Ilshenar && from.X<1581 && from.Y<1599)
				WorldMap = 10; // Underworld

            else
				WorldMap = 11;
		}

		public class MapAmbrosiaGump : Gump
		{
			public MapAmbrosiaGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(471, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(471, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(14, 29, 11413);
			}
		}

		public class MapBottleGump : Gump
		{
			public MapBottleGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(235, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(234, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(14, 29, 1101);
			}
		}

		public class MapDreadGump : Gump
		{
			public MapDreadGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(504, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(503, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(277, 506, 9397);
				AddImage(333, 506, 9397);
				AddImage(386, 506, 9397);
				AddImage(432, 506, 9397);
				AddImage(488, 506, 9397);
				AddImage(276, 0, 9391);
				AddImage(385, 0, 9391);
				AddImage(330, 0, 9391);
				AddImage(439, 0, 9391);
				AddImage(493, 0, 9391);
				AddImage(14, 29, 5597);
			}
		}

		public class MapLodoriaGump : Gump
		{
			public MapLodoriaGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(652, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(652, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(487, 0, 9391);
				AddImage(596, 0, 9391);
				AddImage(541, 0, 9391);
				AddImage(652, 0, 9391);
				AddImage(476, 506, 9397);
				AddImage(532, 506, 9397);
				AddImage(585, 506, 9397);
				AddImage(636, 506, 9397);
				AddImage(14, 29, 10869);
			}
		}

		public class MapSerpentGump : Gump
		{
			public MapSerpentGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(453, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(452, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(487, 0, 9391);
				AddImage(476, 506, 9397);
				AddImage(14, 29, 5596);
			}
		}

		public class MapSosariaGump : Gump
		{
			public MapSosariaGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(880, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(880, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(487, 0, 9391);
				AddImage(596, 0, 9391);
				AddImage(541, 0, 9391);
				AddImage(652, 0, 9391);
				AddImage(709, 0, 9391);
				AddImage(765, 0, 9391);
				AddImage(806, 0, 9391);
				AddImage(862, 0, 9391);
				AddImage(476, 506, 9397);
				AddImage(532, 506, 9397);
				AddImage(585, 506, 9397);
				AddImage(636, 506, 9397);
				AddImage(690, 506, 9397);
				AddImage(743, 506, 9397);
				AddImage(785, 506, 9397);
				AddImage(842, 506, 9397);
				AddImage(14, 29, 10870);
			}
		}

		public class MapSavageGump : Gump
		{
			public MapSavageGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(252, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(251, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(14, 29, 5598);
			}
		}

		public class MapUmberGump : Gump
		{
			public MapUmberGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(873, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(874, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(276, 506, 9397);
				AddImage(332, 506, 9397);
				AddImage(385, 506, 9397);
				AddImage(442, 506, 9397);
				AddImage(498, 506, 9397);
				AddImage(551, 506, 9397);
				AddImage(605, 506, 9397);
				AddImage(661, 506, 9397);
				AddImage(714, 506, 9397);
				AddImage(770, 506, 9397);
				AddImage(826, 506, 9397);
				AddImage(879, 506, 9397);
				AddImage(276, 0, 9391);
				AddImage(385, 0, 9391);
				AddImage(330, 0, 9391);
				AddImage(441, 0, 9391);
				AddImage(550, 0, 9391);
				AddImage(495, 0, 9391);
				AddImage(607, 0, 9391);
				AddImage(716, 0, 9391);
				AddImage(661, 0, 9391);
				AddImage(769, 0, 9391);
				AddImage(878, 0, 9391);
				AddImage(823, 0, 9391);
				AddImage(14, 29, 11414);
			}
		}

		public class MapUnderworldGump : Gump
		{
			public MapUnderworldGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(512, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(512, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(276, 506, 9397);
				AddImage(332, 506, 9397);
				AddImage(385, 506, 9397);
				AddImage(442, 506, 9397);
				AddImage(498, 506, 9397);
				AddImage(276, 0, 9391);
				AddImage(385, 0, 9391);
				AddImage(330, 0, 9391);
				AddImage(441, 0, 9391);
				AddImage(495, 0, 9391);
				AddImage(14, 29, 1126);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( MapSosariaGump ) );
				e.CloseGump( typeof( MapLodoriaGump ) );
				e.CloseGump( typeof( MapSerpentGump ) );
				e.CloseGump( typeof( MapDreadGump ) );
				e.CloseGump( typeof( MapDreadGump ) );
				e.CloseGump( typeof( MapSavageGump ) );
				e.CloseGump( typeof( MapAmbrosiaGump ) );
				e.CloseGump( typeof( MapUmberGump ) );
				e.CloseGump( typeof( MapBottleGump ) );
				e.CloseGump( typeof( MapUnderworldGump ) );

				e.PlaySound( 0x249 );
				if (WorldMap == 1) 
					e.SendGump( new MapSosariaGump( e ) );
				else if (WorldMap == 2) 
					e.SendGump( new MapLodoriaGump( e ) );
				else if (WorldMap == 4) 
					e.SendGump( new MapSerpentGump( e ) );
				else if (WorldMap == 5) 
					e.SendGump( new MapDreadGump( e ) );
				else if (WorldMap == 6) 
					e.SendGump( new MapSavageGump( e ) );
				else if (WorldMap == 7) 
					e.SendGump( new MapAmbrosiaGump( e ) );
				else if (WorldMap == 8) 
					e.SendGump( new MapUmberGump( e ) );
				else if (WorldMap == 9) 
					e.SendGump( new MapBottleGump( e ) );
				else if (WorldMap == 10) 
					e.SendGump( new MapUnderworldGump( e ) );
				else
					e.SendMessage("This map seems to be a bunch of scribbles.");
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public MapWorld( Serial serial ) : base( serial )
		{
		}

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            string mDesc = "";

            if (WorldMap == 1) 
                mDesc = "for Sosaria";
            else if (WorldMap == 2) 
                mDesc = "for Lodoria";
            else if (WorldMap == 4) 
                mDesc = "for the Serpent Island";
            else if (WorldMap == 5) 
                mDesc = "for the Isles of Dread";
            else if (WorldMap == 6) 
                mDesc = "for the Savaged Empire";
            else if (WorldMap == 7) 
                mDesc = "for Ambrosia";
            else if (WorldMap == 8) 
                mDesc = "for Umber Veil";
            else if (WorldMap == 9) 
                mDesc = "for Kuldar";
            else if (WorldMap == 10) 
                mDesc = "for the Underworld";
            else
                mDesc = "for Nowhere Particular";

            list.Add(1053099, String.Format("<BASEFONT COLOR=#DDCC22>\t{0}<BASEFONT Color=#FBFBFB>", mDesc));
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( WorldMap );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            WorldMap = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
}
namespace Server.Items
{
	public class WorldMapLodor : Item
	{
		[Constructable]
		public WorldMapLodor( ) : base( 0x4CC2 )
		{
			Name = "World Map of Lodoria";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(652, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(652, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(487, 0, 9391);
				AddImage(596, 0, 9391);
				AddImage(541, 0, 9391);
				AddImage(652, 0, 9391);
				AddImage(476, 506, 9397);
				AddImage(532, 506, 9397);
				AddImage(585, 506, 9397);
				AddImage(636, 506, 9397);
				AddImage(14, 29, 10869);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapLodor(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapSosaria : Item
	{
		[Constructable]
		public WorldMapSosaria( ) : base( 0x4CC2 )
		{
			Name = "World Map of Sosaria";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(880, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(880, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(487, 0, 9391);
				AddImage(596, 0, 9391);
				AddImage(541, 0, 9391);
				AddImage(652, 0, 9391);
				AddImage(709, 0, 9391);
				AddImage(765, 0, 9391);
				AddImage(806, 0, 9391);
				AddImage(862, 0, 9391);
				AddImage(476, 506, 9397);
				AddImage(532, 506, 9397);
				AddImage(585, 506, 9397);
				AddImage(636, 506, 9397);
				AddImage(690, 506, 9397);
				AddImage(743, 506, 9397);
				AddImage(785, 506, 9397);
				AddImage(842, 506, 9397);
				AddImage(14, 29, 10870);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapSosaria(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapBottle : Item
	{
		[Constructable]
		public WorldMapBottle( ) : base( 0x4CC2 )
		{
			Name = "World Map of Kuldar";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(235, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(234, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(14, 29, 1101);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapBottle(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapSerpent : Item
	{
		[Constructable]
		public WorldMapSerpent( ) : base( 0x4CC2 )
		{
			Name = "World Map of the Serpent Island";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(453, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(452, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(487, 0, 9391);
				AddImage(476, 506, 9397);
				AddImage(14, 29, 5596);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapSerpent(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapUmber : Item
	{
		[Constructable]
		public WorldMapUmber( ) : base( 0x4CC2 )
		{
			Name = "World Map of Umber Veil";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(873, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(874, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(276, 506, 9397);
				AddImage(332, 506, 9397);
				AddImage(385, 506, 9397);
				AddImage(442, 506, 9397);
				AddImage(498, 506, 9397);
				AddImage(551, 506, 9397);
				AddImage(605, 506, 9397);
				AddImage(661, 506, 9397);
				AddImage(714, 506, 9397);
				AddImage(770, 506, 9397);
				AddImage(826, 506, 9397);
				AddImage(879, 506, 9397);
				AddImage(276, 0, 9391);
				AddImage(385, 0, 9391);
				AddImage(330, 0, 9391);
				AddImage(441, 0, 9391);
				AddImage(550, 0, 9391);
				AddImage(495, 0, 9391);
				AddImage(607, 0, 9391);
				AddImage(716, 0, 9391);
				AddImage(661, 0, 9391);
				AddImage(769, 0, 9391);
				AddImage(878, 0, 9391);
				AddImage(823, 0, 9391);
				AddImage(14, 29, 11414);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapUmber(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapAmbrosia : Item
	{
		[Constructable]
		public WorldMapAmbrosia( ) : base( 0x4CC2 )
		{
			Name = "World Map of Ambrosia";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(471, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(471, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(276, 0, 9391);
				AddImage(333, 0, 9391);
				AddImage(389, 0, 9391);
				AddImage(430, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(273, 506, 9397);
				AddImage(327, 506, 9397);
				AddImage(380, 506, 9397);
				AddImage(422, 506, 9397);
				AddImage(14, 29, 11413);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapAmbrosia(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapIslesOfDread : Item
	{
		[Constructable]
		public WorldMapIslesOfDread( ) : base( 0x4CC2 )
		{
			Name = "World Map of the Isles of Dread";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(504, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(503, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(277, 506, 9397);
				AddImage(333, 506, 9397);
				AddImage(386, 506, 9397);
				AddImage(432, 506, 9397);
				AddImage(488, 506, 9397);
				AddImage(276, 0, 9391);
				AddImage(385, 0, 9391);
				AddImage(330, 0, 9391);
				AddImage(439, 0, 9391);
				AddImage(493, 0, 9391);
				AddImage(14, 29, 5597);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapIslesOfDread(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapSavage : Item
	{
		[Constructable]
		public WorldMapSavage( ) : base( 0x4CC2 )
		{
			Name = "World Map of the Savaged Empire";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(252, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(251, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(14, 29, 5598);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapSavage(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public class WorldMapUnderworld : Item
	{
		[Constructable]
		public WorldMapUnderworld( ) : base( 0x4CC2 )
		{
			Name = "Whole Map of the Underworld";
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
			Hue = 0xB63;
		}

		public class WorldMapGump : Gump
		{
			public WorldMapGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 9390);
				AddImage(512, 0, 9392);
				AddImage(2, 506, 9396);
				AddImage(512, 506, 9398);
				AddImage(111, 0, 9391);
				AddImage(220, 0, 9391);
				AddImage(165, 0, 9391);
				AddImage(113, 506, 9397);
				AddImage(169, 506, 9397);
				AddImage(222, 506, 9397);
				AddImage(276, 506, 9397);
				AddImage(332, 506, 9397);
				AddImage(385, 506, 9397);
				AddImage(442, 506, 9397);
				AddImage(498, 506, 9397);
				AddImage(276, 0, 9391);
				AddImage(385, 0, 9391);
				AddImage(330, 0, 9391);
				AddImage(441, 0, 9391);
				AddImage(495, 0, 9391);
				AddImage(14, 29, 1126);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.CloseGump( typeof( WorldMapGump ) );
				e.SendGump( new WorldMapGump( e ) );
				e.PlaySound( 0x249 );
			}
			else
			{
				e.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public WorldMapUnderworld(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			if ( ItemID != 0x4CC2 && ItemID != 0x4CC3 ){ ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 ); }
			Hue = 0xB63;
		}
	}
}