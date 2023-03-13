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
	public class PirateChest : SkullChest
	{
		public string ContainerOwner;

		[CommandProperty(AccessLevel.Owner)]
		public string Container_Owner { get { return ContainerOwner; } set { ContainerOwner = value; InvalidateProperties(); } }

		[Constructable]
		public PirateChest( int level, string digger ) : base()
		{
			Name = "pirate chest";

			ContainerFunctions.LockTheContainer( level, this, 1 );

			if ( digger == "null" ){ digger = "From An Unknown Pirate"; }
			ContainerOwner = digger;

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

		public PirateChest( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, ContainerOwner );
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ContainerOwner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			ContainerOwner = reader.ReadString();
		}
	}
}