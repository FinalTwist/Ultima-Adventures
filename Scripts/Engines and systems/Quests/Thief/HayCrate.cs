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
				bool LookInside = true;

				if ( from.Backpack.FindItemByType( typeof ( ThiefNote ) ) != null )
				{
					Item mail = from.Backpack.FindItemByType( typeof ( ThiefNote ) );
					ThiefNote envelope = (ThiefNote)mail;

					if ( envelope.NoteOwner == from && envelope.NoteItemGot > 0 && HayTown == envelope.NoteDeliverTo && envelope.NoteDeliverType == 2 )
					{
						int modded = Server.Misc.AdventuresFunctions.DiminishingReturns(envelope.Consecutive, 200);
						envelope.Consecutive += 1;
						LoggingFunctions.LogStandard( from, "has stolen " + envelope.NoteItem + "." );
						
						AetherGlobe.QuestEffect( envelope.NoteReward, from, false);

						if (envelope.NoteReward < 5000 && envelope.NoteReward > 0)
							from.AddToBackpack ( new Gold( envelope.NoteReward ) );
						else if (envelope.NoteReward > 0)
							from.AddToBackpack ( new BankCheck( envelope.NoteReward ) );
						
						Titles.AwardFame( from, ((int)(envelope.NoteReward/25)), true );
						Titles.AwardKarma( from, -((int)(envelope.NoteReward/25)), true );
						Server.Items.ThiefNote.SetupNote( envelope, from, envelope.Consecutive );
						from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You collected your reward.");
						from.SendMessage( "You found another secret note for you." );
						from.SendSound( 0x3D );
						from.CloseGump( typeof( Server.Items.ThiefNote.NoteGump ) );
						Server.Items.ThiefNote.ThiefTimeAllowed( from );
						LookInside = false;
						
						Item rngitem = null;

						if (envelope.Consecutive == 50 || envelope.Consecutive  == 100 || envelope.Consecutive == 150 || envelope.Consecutive == 200 || envelope.Consecutive == 250 || envelope.Consecutive == 300 || envelope.Consecutive == 350)
							rngitem = Loot.RandomArty();	

						else if (rngitem == null && Utility.RandomDouble() < (0.03 + ((double)modded / 300)) )
						{
							switch ( Utility.Random( 6 ) ) //
										{					
												case 0: rngitem = Loot.RandomArty(); break;
												case 1: rngitem = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing(); Stealing.ItemMutate( from, from.Luck, rngitem, (int)(modded/10)); break;
												case 2: rngitem = Loot.RandomInstrument(); Stealing.ItemMutate( from, from.Luck, rngitem, (int)(modded/10) ); break;
												case 3: rngitem = Loot.RandomQuiver(); Stealing.ItemMutate( from, from.Luck, rngitem, (int)(modded/10) ); break;
												case 4: rngitem = Loot.RandomWand(); Stealing.ItemMutate( from, from.Luck, rngitem, (int)(modded/10) ); break;
												case 5: rngitem = Loot.RandomJewelry(); Stealing.ItemMutate( from, from.Luck, rngitem, (int)(modded/10) ); break;
										}
							if (rngitem != null)
							{
								from.AddToBackpack( rngitem );
								from.SendMessage( "The guild offers you a special gift.  May it help you stay in the shadows." );
							}						}
					}
				}

				if ( LookInside )
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
