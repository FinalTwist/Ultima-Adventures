using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class LocalMap : MapItem
	{
		public string MapWorld;

		[CommandProperty(AccessLevel.Owner)]
		public string Map_World { get { return MapWorld; } set { MapWorld = value; InvalidateProperties(); } }

		[Constructable]
		public LocalMap()
		{
			SetDisplay( 0, 0, 5119, 4095, 400, 400 );
		}

		public override string DefaultName
		{
			get { return "Small Map"; }
		}

		public override void CraftInit( Mobile from )
		{
            Map map = from.Map;

            double skillValue = from.Skills[SkillName.Cartography].Value;
            int dist = 0;

			MapWorld = Worlds.GetMyWorld( map, from.Location, from.X, from.Y );

			if ( MapWorld == "the Land of Lodoria" ){ dist = 60 + (int)(skillValue * 2); }
			else if ( MapWorld == "the Serpent Island" ){ dist = 50 + (int)(skillValue * 1.5); }
			else if ( MapWorld == "the Savaged Empire" ){ dist = 40 + (int)(skillValue); }
			else if ( MapWorld == "the Isles of Dread" ){ dist = 40 + (int)(skillValue); }
			else if ( MapWorld == "the Land of Ambrosia" ){ dist = 40 + (int)(skillValue); }
			else if ( MapWorld == "the Bottle World of Kuldar" ){ dist = 40 + (int)(skillValue); }
			else if ( MapWorld == "the Island of Umber Veil" ){ dist = 30 + (int)(skillValue); }
			else if ( MapWorld == "the Moon of Luna" ){ dist = 20 + (int)(skillValue); }
			else if ( MapWorld == "the Underworld" ){ dist = 50 + (int)(skillValue * 1.5); }
			else
			{
                dist = 60 + (int)(skillValue * 2);
				MapWorld = "the Land of Sosaria";
			}

			SetDisplay( from.X - dist, from.Y - dist, from.X + dist, from.Y + dist, 200, 200, from.Map, from.X, from.Y );
		}

		public override int LabelNumber{ get{ return 1015230; } } // local map

		public LocalMap( Serial serial ) : base( serial )
		{
		}

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            string mDesc = "for " + MapWorld;
            list.Add(1053099, String.Format("<BASEFONT COLOR=#DDCC22>\t{0}<BASEFONT Color=#FBFBFB>", mDesc));
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( MapWorld );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            MapWorld = reader.ReadString();
		}
	}
}