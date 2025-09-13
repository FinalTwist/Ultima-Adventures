using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class PirateBounty : Item
	{
		public int BountyValue;
		public string BountyWho;

		[CommandProperty(AccessLevel.Owner)]
		public int Bounty_Value { get { return BountyValue; } set { BountyValue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Bounty_Who { get { return BountyWho; } set { BountyWho = value; InvalidateProperties(); } }

		[Constructable]
		public PirateBounty() : base( 0x0DEB )
		{
			Name = "Pirate Bounty";
			Weight = 1.0;
			ItemID = Utility.RandomList( 0x0DEB, 0x0DED );
			BountyWho = NameList.RandomName("male") + " the pirate";
			BountyValue = Utility.RandomMinMax( 1000, 3000 );
				BountyValue = (int)(BountyValue * (MyServerSettings.GetGoldCutRate() * .01));
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "For " + BountyWho + "");
        }
    
		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 4 ) )
			{
				from.CloseGump( typeof( BountyGump ) );
				from.SendGump( new BountyGump( from, this ) );
				from.PlaySound( 0x249 );
			}
		}

		public class BountyGump : Gump
		{
			public BountyGump( Mobile from, PirateBounty bounty ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 100, 155);
				AddImage(300, 100, 155);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 98, 129);
				AddImage(298, 98, 129);
				AddImage(6, 7, 133);
				AddImage(225, 46, 132);
				AddImage(298, 46, 132);
				AddImage(180, 22, 156);
				AddImage(196, 17, 156);
				AddImage(339, 65, 10888);
				AddHtml( 13, 15, 571, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>" + bounty.BountyWho + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 109, 136, 211, 226, @"<BODY><BASEFONT Color=#FFA200><BIG>Bounties are often placed on famous pirates that sail the high seas and create havoc in their pursuit for booty. Giving this bounty contract to a town guard will reward you with the listed gold.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(206, 86, 3823);
				AddHtml( 95, 90, 84, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Bounty:</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 245, 90, 84, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + bounty.BountyValue + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				from.PlaySound( 0x249 );
			}
		}

		public PirateBounty( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( BountyValue );
            writer.Write( BountyWho );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            BountyValue = reader.ReadInt();
			BountyWho = reader.ReadString();
		}
	}
}