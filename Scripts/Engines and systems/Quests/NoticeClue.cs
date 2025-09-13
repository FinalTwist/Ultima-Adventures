using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class NoticeClue : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		public override void OnMovement( Mobile from, Point3D oldLocation )
		{
			if( from is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && Utility.InRange( from.Location, this.Location, 5 ) )
				{
					from.PrivateOverheadMessage(MessageType.Regular, 1150, false, this.Name, from.NetState);

					if ( this.X == 5764 && this.Y == 2215 )
					{
						from.CloseGump( typeof(Server.Gumps.ClueGump) );
						from.SendGump(new Server.Gumps.ClueGump( "Seems like an odd phrase. Perhaps I should remember the name that some give to a ruby.", "The Bloodstone" ) );
					}
					else if ( this.X == 6268 && this.Y == 2661 )
					{
						from.CloseGump( typeof(Server.Gumps.ClueGump) );
						from.SendGump(new Server.Gumps.ClueGump( "What altars did Harkyn set? What name must be spoken?", "Harkyn's Altars" ) );
					}
					else if ( this.X == 6293 && this.Y == 1649 )
					{
						from.CloseGump( typeof(Server.Gumps.ClueGump) );
						from.SendGump(new Server.Gumps.ClueGump( "The emerald gate? Perhaps a magical gate of green? If I speak the name of the ruby near it, I may be able to enter.", "The Emerald Gate" ) );
					}
					else if ( this.X == 6497 && this.Y == 1440 )
					{
						from.CloseGump( typeof(Server.Gumps.ClueGump) );
						from.SendGump(new Server.Gumps.ClueGump( "The shapes of three, silver they be, can make the golden skull speak? Perhaps these things I must find, but where?", "The Silver Shapes" ) );
					}
					else if ( this.X == 6501 && this.Y == 1773 )
					{
						from.CloseGump( typeof(Server.Gumps.ClueGump) );
						from.SendGump(new Server.Gumps.ClueGump( "Know this, that a man called Tarjan, thought by many to be insane, had through wizardly powers proclaimed himself a god in Skara Brae a hundred years ago. Perhaps that cult in town knows of this.", "The Mad God" ) );
					}
					else if ( this.X == 6988 && this.Y == 164 )
					{
						from.CloseGump( typeof(Server.Gumps.ClueGump) );
						from.SendGump(new Server.Gumps.ClueGump( "You can already feel the magical energy that is sealing this door. Perhaps there is another way to enter this vile place.", "Mangar's Tower Door" ) );
					}

					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public NoticeClue( ) : base( 0x181E )
		{
			Movable = false;
			Visible = false;
			Name = "clue";
		}

		public NoticeClue( Serial serial ) : base( serial )
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