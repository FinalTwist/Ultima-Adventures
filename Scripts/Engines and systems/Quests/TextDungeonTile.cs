using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{
    public class TextDungeonTile : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		private Mobile person;


		private int range;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Range
        {
            get{ return range; }
            set{ range = value; }
        }	

		private int percentage;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Percentage
        {
            get{ return percentage; }
            set{ percentage = value; }
        }	


		public override void OnMovement( Mobile from, Point3D oldLocation )
		{
			if( from is PlayerMobile )
			{
				if ( from != person && Utility.InRange( from.Location, this.Location, range )&& !Utility.InRange( oldLocation, this.Location, range ) )
				{
					person = from;
					string Heat = "";
					
					Region reg = Region.Find( this.Location, this.Map );

					if ( this.Map == Map.Felucca )
					{
						if ( reg.IsPartOf( "the Lodoria Sewers" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Lizardman Cave" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Ratmen Cave" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Crypt" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Wrong" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Volcanic Cave" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Terathan Keep" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Shame" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Ice Fiend Lair" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Frozen Hells" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Hythloth" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Mind Flayer City" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the City of Embers" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Destard" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Despise" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Deceit" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Covetous" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Lodoria Catacombs" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Halls of Undermountain" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Vault of the Black Knight" ) ){ Heat = ""; } // -- IN SERPENT ISLAND
						else if ( reg.IsPartOf( "the Crypts of Dracula" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Castle of Dracula" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Stonegate Castle" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Ancient Elven Mine" ) ){ Heat = ""; }

						else if ( reg.IsPartOf( "Morgaelin's Inferno" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Zealan Tombs" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Temple of Osirus" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Argentrock Castle" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Daemon's Crag" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Hall of the Mountain King" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Depths of Carthax Lake" ) ){ Heat = ""; }

						else if ( reg.IsPartOf( "the Montor Sewers" ) ){ Heat = ""; }

						else if ( reg.IsPartOf( "Mangar's Tower" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Mangar's Chamber" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Kylearan's Tower" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Harkyn's Castle" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Catacombs" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Lower Catacombs" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Sewers" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Cellar" ) ){ Heat = ""; }

						else if ( reg.IsPartOf( "the Sanctum of Saltmarsh" ) ){ Heat = ""; }
					}
					else if ( this.Map == Map.Trammel )
					{
						if ( reg.IsPartOf( "the Ancient Pyramid" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Mausoleum" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Clues" ) ){ Heat = "The path seems well worn, Lots have passed this way. You hear mumbling in the distance"; }
						else if ( reg.IsPartOf( "Dardin's Pit" ) ){ Heat = "Nature has taken back this ancient underground city, In the distance you hear the roars of Ettins and Orcs"; }
						else if ( reg.IsPartOf( "Frostwall Caverns" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Abandon" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Exodus" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Fires of Hell" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Frozen Dungeon" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Mines of Morinia" ) ){ Heat = "Carefully stepping over the acid puddles you wonder if any treasure remains"; }
						else if ( reg.IsPartOf( "the Perinian Depths" ) ){ Heat = "As you enter the cave you sense a dark presence, something watching from the shadows."; }
						else if ( reg.IsPartOf( "the Ratmen Lair" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Dungeon of Time Awaits" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Castle Exodus" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Cave of Banished Mages" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the City of the Dead" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Dragon's Maw" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Cave of the Zuluu" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Tower of Brass" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Caverns of Poseidon" ) ){ Heat = "The dry salt air grates against your skin, an ungodly fishy smell envelopes the area, this is no place for sushi"; }

						else if ( reg.IsPartOf( "the Accursed Maze" ) ){ Heat = "The rotting smell of long dead prisoners and Kobold permeate the halls of this maze. You wonder how long this place has been abandoned"; }
						else if ( reg.IsPartOf( "the Chamber of Bane" ) ){ Heat = "Death fills the air, you feel uneasy as the clanging of chains and cackeling undead draw closer"; }
						else if ( reg.IsPartOf( "Coldhall Depths" ) ){ Heat = "Once the great home of long forgotten Kings, Coldhall Depths serves as a breeding ground for Dark Magics and unholy creatures"; }
						else if ( reg.IsPartOf( "the Dark Sanctum" ) ){ Heat = "As you brush the cobwebs from your face you realise this is not a nice place, possibly the abandoned home of a Dark Wizard?"; }
						else if ( reg.IsPartOf( "the Forgotten Tombs" ) ){ Heat = "Britain's long dead kings rest in these tombs. the musty smell of death engulfs the air, what horrors lay within?"; }
						else if ( reg.IsPartOf( "the Magma Vaults" ) ){ Heat = "You feel the intense heat as you climb down the ladder, hearing the sound of foot steps beyond the door you wonder how anything could live down here"; }
						else if ( reg.IsPartOf( "the Shrouded Grave" ) ){ Heat = "The final reseting place for the people of Grey, it seems like others have been here before, grave robbing perhaps?"; }

						else if ( reg.IsPartOf( "the Ruins of the Black Blade" ) ){ Heat = ""; }

						else if ( reg.IsPartOf( "Steamfire Cave" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Kuldara Sewers" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Crypts of Kuldar" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Vordo's Castle" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Vordo's Dungeon" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Vordo's Castle Grounds" ) ){ Heat = ""; }

					}
					else if ( this.Map == Map.Malas )
					{
						if ( reg.IsPartOf( "the Ancient Prison" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Cave of Fire" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Cave of Souls" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Ankh" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Bane" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Hate" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Scorn" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Torment" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Vile" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Wicked" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Wrath" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Flooded Temple" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Gargoyle Crypts" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Serpent Sanctum" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Tomb of the Fallen Wizard" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Castle of the Black Knight" ) ){ Heat = ""; }
					}
					else if ( this.Map == Map.Tokuno )
					{
						if ( reg.IsPartOf( "the Altar of the Blood God" ) ){ Heat = ""; }
					}
					else if ( this.Map == Map.Ilshenar )
					{
						if ( this.Location.X < 1007 && this.Location.Y < 1279 )
						{
							// DarkMoor
							Heat = "";
						}
						else 
						{
							if ( reg.IsPartOf( "the Glacial Scar" ) ){ Heat = ""; }
							else if ( Server.Misc.Worlds.GetRegionName( this.Map, this.Location ) == "the Underworld" ){ Heat = ""; }
							else if ( reg.IsPartOf( typeof( DungeonRegion ) ) ){ Heat = ""; }
						}
					}
					else if ( this.Map == Map.TerMur )
					{
						if ( reg.IsPartOf( "the Blood Temple" ) ){ Heat = ""; } // -- IN ISLES OF DREAD
						else if ( reg.IsPartOf( "the Tombs" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Corrupt Pass" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Crypt" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Great Pyramid" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Altar of the Dragon King" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Ice Queen Fortress" ) ){ Heat = ""; } // -- IN ISLES OF DREAD
						else if ( reg.IsPartOf( "Dungeon of the Lich King" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon of the Mad Archmage" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Halls of Ogrimar" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Ratmen Mines" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "Dungeon Rock" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Sakkhra Tunnel" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Spider Cave" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Storm Giant Lair" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Cave of the Ancient Wyrm" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Isle of the Lich" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Castle of the Mad Archmage" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Mage Mansion" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Island of the Storm Giant" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Orc Fort" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Hedge Maze" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Pixie Cave" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Forgotten Halls" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Undersea Castle" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Tomb of Kazibal" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Catacombs of Azerok" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Azure Castle" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Scurvy Reef" ) ){ Heat = ""; }

						else if ( reg.IsPartOf( "the Ancient Crash Site" ) ){ Heat = ""; }
						else if ( reg.IsPartOf( "the Ancient Sky Ship" ) ){ Heat = ""; }
					}
					if ( Heat != "" )
					{
						from.PrivateOverheadMessage(MessageType.Regular, 744, false, Heat, from.NetState);
					}
				}
			}
		}

		[Constructable]
		public TextDungeonTile( ) : base( 0x181E )
		{
			Movable = false;
			Visible = false;
			Name = "TextDungeonTile";
			person = null;
			range = 4;
		}

		public TextDungeonTile( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 1);
			writer.Write((int) range);
			writer.Write((int) percentage);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			if (version >= 1)
			{
				range = reader.ReadInt();
				percentage = reader.ReadInt();
			}
		}
	}	
}