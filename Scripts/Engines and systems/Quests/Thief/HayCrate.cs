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
	public class HayCrate : Item
	{
		public string HayTown;
		[CommandProperty(AccessLevel.Owner)]
		public string Hay_Town { get { return HayTown; } set { HayTown = value; InvalidateProperties(); } }

		[Constructable]
		public HayCrate() : base( 0x0701 )
		{
			Name = "hay";
			Movable = false;
		}

		public static void GetNearbyTown( HayCrate hay )
		{
			if ( hay.Map == Map.Malas )
			{
				hay.HayTown = "the City of Furnace";
			}
			else
			{
				foreach ( Mobile citizen in hay.GetMobilesInRange( 200 ) )
				{
					if ( citizen is BaseVendor || citizen is BlueGuard || citizen is Citizens )
					{
						if ( citizen.Region.Name != null ){ hay.HayTown = Server.Misc.Worlds.GetRegionName( citizen.Map, citizen.Location ); }
					}
				}
			}

			if ( hay.HayTown == null || hay.HayTown == "" ){ hay.HayTown = "the City of Britain"; }
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				ThiefNote envelope = from.Backpack.FindItemByType( typeof ( ThiefNote ) ) as ThiefNote;
				if (envelope == null || !ThiefNote.TryGetReward(from, envelope, HayTown, ThiefNote.HayCrateType))
				{
					string message = "There is nothing of interest in here.";

					if ( Utility.RandomMinMax( 1, 20 ) == 1 && Stackable == false )
					{
						Server.Misc.ContainerFunctions.GiveRandomItem( from );
						message = "You pull something out of the hay and it falls by your feet.";
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

		public HayCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( HayTown );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			HayTown = reader.ReadString();
		} 
	}
}
