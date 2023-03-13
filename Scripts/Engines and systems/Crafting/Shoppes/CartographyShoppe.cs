using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0x3CFD, 0x3CFE )]
	public class CartographyShoppe : BaseShoppe
	{
		[Constructable]
		public CartographyShoppe()
		{
			Name = "Cartography Work Shoppe";
			ItemID = Utility.RandomList( 0x3CFD, 0x3CFE );
			ShoppeName = Name;
			ShelfTitle = "CARTOGRAPHY WORK SHOPPE";
			ShelfItem = 0x3CFD;
			ShelfSkill = 12;
			ShelfGuild = NpcGuild.CartographersGuild;
			ShelfTools = "Mapmaker Pens";
			ShelfResources = "Blank Scrolls";
			ShelfSound = 0x249;
		}

		public static string MakeThisTask()
		{
			string task = null;

			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0:	task = Server.Misc.RandomThings.MadeUpCity();													break;
				case 1:	task = Server.Misc.RandomThings.MadeUpDungeon();												break;
				case 2:	task = "the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom();	break;
			}

			switch ( Utility.RandomMinMax( 0, 10 ) )
			{
				case 0:		task = "Redraw this map of " + task;				break;
				case 1:		task = "Decipher this map of " + task;				break;
				case 2:		task = "Clean up this map of " + task;				break;
				case 3:		task = "Verify this map of " + task;				break;
				case 4:		task = "Copy this map of " + task;					break;
				case 5:		task = "Make an atlas for these maps of " + task;	break;
				case 6:		task = "Draw a trail map to " + task;				break;
				case 7:		task = "Decode this treasure map for " + task;		break;
				case 8:		task = "Take this map of " + task + " and make it a larger scale";		break;
				case 9:		task = "Take this map of " + task + " and make it a smaller scale";		break;
				case 10:	task = "Encode this treasure map for " + task;		break;
			}

			return task;
		}

		public CartographyShoppe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}