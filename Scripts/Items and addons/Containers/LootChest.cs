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
	public class LootChest : LockableContainer
	{
		[Constructable]
		public LootChest( int level ) : base( 0xe40 )
		{
			Name = "chest";
			ContainerFunctions.BuildContainer( this, 0, Utility.RandomList( 1, 2 ), 0, 0 );
			ContainerFunctions.LockTheContainer( level, this, 1 );
			Weight = 51.0 + (double)level;

			if ( Weight > 50 ){ Movable = false; } // DON'T WANT THEM TO MOVE IT UNTIL THEY OPEN IT FIRST
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 50 )
			{
				Movable = true;
				int FillMeUpLevel = (int)(this.Weight - 51);
				this.Weight = 5.0;

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from );

				//BaseContainer.OrderContainer( this, from );
			}

			base.Open( from );
		}

		public LootChest( Serial serial ) : base( serial )
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