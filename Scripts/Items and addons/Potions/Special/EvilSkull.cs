using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class EvilSkull : Item
	{
		[Constructable]
		public EvilSkull() : base( 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Name = "evil skull";
			Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				Target t;
				int number;

				if ( from.Mana < from.ManaMax )
				{
					from.SendMessage( "The skull crumbles into dust, magically restoring your mana." );
					from.Mana = from.ManaMax;
					from.PlaySound( 0x1FA );
					Misc.Titles.AwardKarma( from, -100, true );
				}
				else
				{
					from.SendMessage( "The skull crumbles into dust." );
				}

				this.Delete();
			}
		}

		public EvilSkull(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}