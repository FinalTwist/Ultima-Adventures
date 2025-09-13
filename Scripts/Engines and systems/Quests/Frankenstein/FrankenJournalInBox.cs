using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class FrankenJournalInBox : Item
	{
		[Constructable]
		public FrankenJournalInBox() : base( 0x1A97 )
		{
			Weight = 1.0;
			Hue = 0xB51;
			Name = "Frankenstein's Journal";
			Movable = false;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Double Click To Take It" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				bool HasBook = false;

				ArrayList targets = new ArrayList();
				foreach ( Item item in World.Items.Values )
				{
					if ( item is FrankenJournal )
					{
						if ( ((FrankenJournal)item).JournalOwner == from )
						{
							targets.Add( item );
							HasBook = true;
						}
					}
				}
				for ( int i = 0; i < targets.Count; ++i )
				{
					Item item = ( Item )targets[ i ];

					if ( item is FrankenJournal )
					{
						from.AddToBackpack( item );
						from.SendMessage( "You don't need this journal as there is already one in your pack." );
					}
				}

				if ( !HasBook )
				{
					FrankenJournal journal = new FrankenJournal();
					journal.JournalOwner = from;
					from.AddToBackpack( journal );
					from.SendMessage( "You take possession of Frankenstein's Journal!" );
					from.SendSound( 0x3D );
					LoggingFunctions.LogGeneric( from, "has found Frankenstein's Journal." );
					this.Delete();
				}
			}
		}

		public FrankenJournalInBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}