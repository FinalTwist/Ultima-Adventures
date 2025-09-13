using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using System.Collections;

namespace Server.Items
{
    public class DungeonMastersGuide : Item
	{
        [Constructable]
        public DungeonMastersGuide() : base( 0x3046 )
		{
            Name = "Dungeon Masters Guide";
			Weight = 1.0;
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Dungeons & Dragons");
        }

		public override void OnDoubleClick( Mobile e )
		{
			e.CloseGump( typeof( DMGuideGump ) );
			e.SendGump( new DMGuideGump() );
			e.SendSound( 0x55 );
		}

		public class DMGuideGump : Gump
		{
			public DMGuideGump(): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 154);
				AddImage(300, 0, 154);
				AddImage(0, 300, 154);
				AddImage(300, 300, 154);
				AddImage(0, 600, 154);
				AddImage(300, 600, 154);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 302, 129);
				AddImage(298, 300, 129);
				AddImage(2, 598, 129);
				AddImage(298, 598, 129);
				AddImage(398, 7, 146);
				AddImage(267, 652, 147);
				AddImage(6, 680, 148);
				AddImage(19, 307, 161);
				AddImage(7, 7, 145);
				AddImage(19, 356, 161);
				AddImage(20, 656, 157);
				AddImage(21, 277, 158);
				AddImage(554, 248, 161);
				AddImage(554, 320, 161);
				AddImage(555, 610, 157);
				AddImage(555, 219, 158);
				AddImage(200, 12, 156);
				AddImage(176, 10, 156);
				AddImage(392, 22, 156);
				AddImage(414, 21, 156);
				AddImage(167, 9, 156);
				AddHtml( 176, 22, 284, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>DUNGEON MASTERS GUIDE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(493, 102, 12358);
				AddHtml( 170, 68, 249, 73, @"<BODY><BASEFONT Color=#FCFF00><BIG>This book contains a listing of almost all of the dungeons in the many worlds of Ultima.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 99, 156, 418, 645, @"<BODY><BASEFONT Color=#FFA200><BIG>" + Server.Misc.Worlds.GetDungeonListing() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				AddImage(363, 155, 11415);
			}
		}

        public DungeonMastersGuide( Serial serial ) : base( serial )
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