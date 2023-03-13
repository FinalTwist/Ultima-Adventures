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
	public class LootBag : LockableContainer
	{
		[Constructable]
		public LootBag( int level ) : base( 0xe40 )
		{
			Name = "chest";
			int nContainerLockable = ContainerFunctions.BuildContainer( this, 0, Utility.RandomList( 7, 8 ), 0, 0 );
			Weight = 11.0 + (double)level;

			TrapType = TrapType.None;
			TrapPower = 0;
			TrapLevel = 0;
			Locked = false;
            LockLevel = 0;
			MaxLockLevel = 0;
			RequiredSkill = 0;

			if ( Weight > 10 ){ Movable = false; } // DON'T WANT THEM TO MOVE IT UNTIL THEY OPEN IT FIRST
		}

		public override void Open( Mobile from )
		{
			if ( this.Weight > 10 )
			{
				Movable = true;
				int FillMeUpLevel = (int)(this.Weight - 11);
				this.Weight = 5.0;

				if ( GetPlayerInfo.LuckyPlayer( from.Luck, from ) )
				{
					FillMeUpLevel = FillMeUpLevel + Utility.RandomMinMax( 1, 2 );
				}

				ContainerFunctions.FillTheContainer( FillMeUpLevel, this, from );
			}

			base.Open( from );
		}

		public LootBag( Serial serial ) : base( serial )
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