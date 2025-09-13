using System;
using Server;
using Server.Network;
using Server.Gumps;

namespace Server.Items
{
    public class PuzzleCube : Item
    {
        [Constructable]
        public PuzzleCube() : base(0x202B)
        {
            Name = "puzzle cube";
            Weight = 1.0;
        }
        public PuzzleCube(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to play with." );
				return;
			}
			else if ( this.ItemID == 0x202A )
			{
				this.ItemID = 0x202B;
				from.PlaySound( 0x04B );
				from.SendMessage( "You scramble the puzzle cube." );
			}
			else if ( from.Int > Utility.RandomMinMax( 50, 150 ) )
			{
				this.ItemID = 0x202A;
				from.PlaySound( 0x04B );
				from.SendMessage( "You solve the puzzle cube." );
			}
			else
			{
				from.PlaySound( 0x04B );
				from.SendMessage( "You are not intelligent enough to solve that." );
			}
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
