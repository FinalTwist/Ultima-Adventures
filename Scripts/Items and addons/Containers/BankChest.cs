using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable(0x436, 0x437)]
    public class BankChest : Item
	{
        [Constructable]
        public BankChest() : base(0x436)
		{
            Name = "Bank Vault";
        }

        public override void OnDoubleClick(Mobile from)
		{

			if ( from is PlayerMobile )
			{
				int mX = 0;
				int mY = 0;
				int mZ = 0;
				Map mWorld = null;
				string mZone = "";
				string sPublicDoor = "";
				PlayerMobile pc = (PlayerMobile)from;

				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );

				sPublicDoor = DB.CharacterPublicDoor;

				if ( sPublicDoor != null )
				{
					string[] sPublicDoors = sPublicDoor.Split('#');
					int nEntry = 1;
					foreach (string exits in sPublicDoors)
					{
						if ( nEntry == 1 ){ mX = Convert.ToInt32(exits); }
						else if ( nEntry == 2 ){ mY = Convert.ToInt32(exits); }
						else if ( nEntry == 3 ){ mZ = Convert.ToInt32(exits); }
						else if ( nEntry == 4 ){ try { mWorld = Map.Parse( exits ); } catch{} if ( mWorld == null ){ mWorld = Map.Trammel; } }
						else if ( nEntry == 5 ){ mZone = exits; }
						nEntry++;
					}
				}
				Point3D loc = new Point3D( mX, mY, mZ );
				Region reg = Region.Find( loc, mWorld );
				if (reg.IsPartOf( "Doom Gauntlet" ))
				{
					from.SendMessage( "You can't access the bank from the Doom Gauntlet." ); 
					return;
				}
			}

			if ( from.InRange( this.GetWorldLocation(), 4 ) )
			{
				BankBox box = from.BankBox;
				if (box != null)
				{
					box.Open();
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
        }

        public BankChest( Serial serial ) : base( serial )
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