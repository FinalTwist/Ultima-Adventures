using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;
using Server.Spells;

namespace Server.Items
{
	public class DockingLantern : Item, ISecurable
	{
        public SecureLevel m_Level;

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level { get { return m_Level; } set { m_Level = value; } }

		[Constructable]
		public DockingLantern() : base( 0x40FF )
		{
			Name = "docking lantern";
			Weight = 2.0;
			Light = LightType.Circle300;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Helps One To Launch or Dock Ships");
        }

		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            SetSecureLevelEntry.AddTo(from, this, list);
        }      

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );		
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
		}

		public bool CheckAccess(Mobile m)
		{
			BaseHouse house = BaseHouse.FindHouseAt(this);

			if (house != null && (house.Public ? house.IsBanned(m) : !house.HasAccess(m)))
				return false;

			return (house != null && house.HasSecureAccess(m, m_Level));
		}

		public DockingLantern( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write((int)m_Level);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Level = (SecureLevel)reader.ReadInt();
		}
	}
}

namespace Server.Misc
{
    class DockSearch
    {
		public static bool NearDock( Mobile m )
		{
			bool IsNearDock = false;

			int DockRange = 30;
			int KeepSearching = 0;

			Region reg = Region.Find( m.Location, m.Map );

			if ( Server.Misc.Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y ) == "the Land of Ambrosia" )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "the Island of Poseidon" ) )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "the Buccaneer's Den" ) )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "Savage Sea Docks" ) )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "Serpent Sail Docks" ) )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "Anchor Rock Docks" ) )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "Kraken Reef Docks" ) )
			{
				IsNearDock = true;
			}
			else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Underworld" )
			{
				IsNearDock = true;
			}
			else if ( m.Skills[SkillName.Fishing].Base >= 100 ) // GM FISHERMAN CAN DOCK AND LAUNCH ANYWHERE
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "the Island of the Black Knight" ) )
			{
				IsNearDock = true;
			}
			else if ( reg.IsPartOf( "the Island of Stonegate" ) )
			{
				IsNearDock = true;
			}
			else
			{
				foreach ( Item lantern in m.GetItemsInRange( DockRange ) )
				{
					if ( KeepSearching != 1 )
					{
						if ( lantern is DockingLantern )
						{
							IsNearDock = true;
							DockingLantern light = (DockingLantern)lantern;
							BaseHouse house = BaseHouse.FindHouseAt(lantern);
							//if ( lantern.Movable != false ){ IsNearDock = false; }
							if (house != null && (house.Public ? house.IsBanned(m) : !house.HasAccess(m))){ IsNearDock = false; }
							if (house != null && !house.HasSecureAccess(m, light.m_Level)){ IsNearDock = false; }
							if ( IsNearDock == true ){ KeepSearching = 1; }
						}
					}
				}
			}

			return IsNearDock;
		}
	}
}