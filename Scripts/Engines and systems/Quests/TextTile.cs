using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class TextTile : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		private Mobile person;

		private int range;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Range
        {
            get{ return range; }
            set{ range = value; }
        }	

		private int percentage;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Percentage
        {
            get{ return percentage; }
            set{ percentage = value; }
        }	

		private String Text1;
		[CommandProperty( AccessLevel.GameMaster )]
        public String text1
        {
            get{ return Text1; }
            set{ Text1 = value; }
        }	
		
		private String Text2;
		[CommandProperty( AccessLevel.GameMaster )]
        public String text2
        {
            get{ return Text2; }
            set{ Text2 = value; }
        }	
		private String Text3;
		[CommandProperty( AccessLevel.GameMaster )]
        public String text3
        {
            get{ return Text3; }
            set{ Text3 = value; }
        }	

		public override void OnMovement( Mobile from, Point3D oldLocation )
		{
			if( from is PlayerMobile && from.Map != null && this.Map != null)
			{
				if ( Utility.RandomDouble() < ((double)percentage/100)  && Utility.InRange( from.Location, this.Location, range ) && !Utility.InRange( oldLocation, this.Location, range ))
				{
					if (person != null && from == person)
						return;

					if ( Text1 != "" && Text1 != null)
					{
						int number = 1;
						if (Text2 != "" && Text2 != null)
							number += 1;
						if (Text3 != "" && Text3 != null)
							number += 1;
							
						person = from;

						switch( Utility.RandomMinMax( 1, number ) )
						{
							case 1: from.SendMessage(Text1, 821);	break;
							case 2: from.SendMessage(Text2, 821);	break;
							case 3: from.SendMessage(Text3, 821);	break;
						}	
					}
				}
			}
		}

		[Constructable]
		public TextTile( ) : base( 0x181E )
		{
			Movable = false;
			Visible = false;
			Name = "Notice Tile";
			person = null;
			Text1 = "";
			Text2 = "";
			Text3 = "";
			range = 4;
			percentage = 75;
		}

		public TextTile( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 1);
			writer.Write((String) Text1);
			writer.Write((String) Text2);
			writer.Write((String) Text3);
			writer.Write((int) range);
			writer.Write((int) percentage);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Text1 = reader.ReadString();
			Text2 = reader.ReadString();
			Text3 = reader.ReadString();
			if (version >= 1)
			{
				range = reader.ReadInt();
				percentage = reader.ReadInt();
			}

		}
	}	
}