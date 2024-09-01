using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Gumps;
using Server.SkillHandlers;

namespace Server.Items
{
	[Flipable( 0x19FD, 0x19FE )]
	public class HollowStump : Item
	{
		public string StumpTown;
		[CommandProperty(AccessLevel.Owner)]
		public string Stump_Town { get { return StumpTown; } set { StumpTown = value; InvalidateProperties(); } }

		[Constructable]
		public HollowStump() : base( 0x19FD )
		{
			Name = "hollow stump";
			Movable = false;
		}

		public static void GetNearbyTown( HollowStump stump )
		{
			if ( stump.Map == Map.Malas )
			{
				stump.StumpTown = "the City of Furnace";
			}
			else
			{
				foreach ( Mobile citizen in stump.GetMobilesInRange( 200 ) )
				{
					if ( citizen is BaseVendor || citizen is TownGuards || citizen is Citizens )
					{
						if ( citizen.Region.Name != null ){ stump.StumpTown = Server.Misc.Worlds.GetRegionName( citizen.Map, citizen.Location ); }
					}
				}
			}

			if ( stump.StumpTown == null || stump.StumpTown == "" ){ stump.StumpTown = "the City of Britain"; }
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				ThiefNote envelope = from.Backpack.FindItemByType( typeof ( ThiefNote ) ) as ThiefNote;
				if (envelope == null || !ThiefNote.TryGetReward(from, envelope, StumpTown, ThiefNote.HollowStumpType))
				{
					string message = "There is nothing of interest in here.";

					if ( Utility.RandomMinMax( 1, 20 ) == 1 && Stackable == false )
					{
						Server.Misc.ContainerFunctions.GiveRandomItem( from );
						message = "You pull something out of the stump and it falls by your feet.";
					}
					Stackable = true;

					from.SendSound( 0x057 );
					from.SendMessage( message );
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public HollowStump( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( StumpTown );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			StumpTown = reader.ReadString();
		} 
	}
}
