using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Targeting;

namespace Server.Items	
{
	[FlipableAttribute( 0x4142, 0x4144 )]
    public class Mailbox : BaseContainer
    {
        public override int LabelNumber { get { return 1113927; } } // Mailbox
        public override int DefaultGumpID { get { return 0x11A; } }
        public override int DefaultDropSound { get { return 0x42; } }

        [Constructable]
        public Mailbox() : base( 0x4142 )
        {
        }
        
        public override void OnDoubleClick( Mobile from )
        {
        	BaseHouse housefoundation = BaseHouse.FindHouseAt( this );

        	if( !this.IsSecure || housefoundation == null)
        	{
                from.SendMessage("This must be secured in a home to use!");
        	}
            else if ( this.IsSecure && housefoundation.IsFriend( from ) ) 
            {
				if ( ItemID == 0x4141 )
					this.ItemID = 0x4142;
				else if ( ItemID == 0x4143 )
					this.ItemID = 0x4144;

            	base.OnDoubleClick(from);
            }
            else
                from.SendMessage("You cannot access this mailbox!");
        }
        
        public override bool OnDragDrop( Mobile from, Item dropped )
		{
        	Item item = dropped as Item;

        	if( !this.IsSecure )
        	{
                from.SendMessage("This must be secured in a home to use!");
				return false;
        	}
			else if ( dropped is Item )
			{
				if (this.ItemID == 0x4142 )
					this.ItemID = 0x4141;
				if (this.ItemID == 0x4144 )
					this.ItemID = 0x4143;

        		DropItem( dropped );
				from.PlaySound( 0x42 );
				from.SendMessage( "You place the item in the mailbox." );
				return true;
			}
			else 
			{
				from.SendMessage( "You don't need to put that in there." );
				return false;
			}
		}

        public Mailbox(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
}