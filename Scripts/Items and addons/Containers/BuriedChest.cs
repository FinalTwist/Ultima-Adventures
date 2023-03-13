using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Misc;

namespace Server.Items
{
	public class BuriedChest : LockableContainer
	{
		[Constructable]
		public BuriedChest( int level, string who, Mobile digger ) : base( 0xe40 )
		{
			if ( who == "" ){ who = ContainerFunctions.GetOwner( "Chest" ); }
			else
			{
				string[] vAdj = new string[] {"Exotic", "Mysterious", "Marvelous", "Amazing", "Astonishing", "Mystical", "Astounding", "Magnificent", "Phenomenal", "Fantastic", "Incredible", "Extraordinary", "Fabulous", "Wondrous", "Glorious", "Lost", "Fabled", "Legendary", "Mythical", "Missing", "Ancestral", "Ornate", "Wonderful", "Sacred", "Unspeakable", "Unknown", "Forgotten"};
				string sAdj = vAdj[Utility.RandomMinMax( 0, (vAdj.Length-1) )];
				who = "The " + sAdj + " Chest of " + who;
			}

			ContainerFunctions.BuildContainer( this, 0, Utility.RandomList( 1, 2 ), 0, 0 );
			Name = who;
			ContainerFunctions.FillTheContainer( level, this, digger );
			if ( GetPlayerInfo.LuckyPlayer( digger.Luck, digger ) ){ ContainerFunctions.FillTheContainer( level, this, digger ); }

			if ( level > 4 ){ ContainerFunctions.FillTheContainer( level, this, digger ); }
			if ( level > 8 ){ ContainerFunctions.FillTheContainer( level, this, digger ); }

			ContainerFunctions.LockTheContainer( level, this, 1 );

			int xTraCash = Utility.RandomMinMax( (level*300), (level*500) );
			ContainerFunctions.AddGoldToContainer( xTraCash, this, 0, digger );

            if (  Utility.RandomMinMax( 0, 500 ) < ( level ) )
			{
				Item arty = ArtifactBuilder.CreateArtifact( "random" );
				DropItem( arty );
				//BaseContainer.DropItemFix( arty, digger, ItemID, GumpID );
			}
		}

		public BuriedChest( Serial serial ) : base( serial )
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