using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Text;
using System.IO;
using System.Threading;
using Server.Gumps;

namespace Server.Items
{
	[Flipable(0x1E5E, 0x1E5F)]
	public class RulesBoard : Item
	{
		[Constructable]
		public RulesBoard( ) : base( 0x1E5E )
		{
			Weight = 1.0;
			Name = "Laws of the Land";
			Hue = 0x6AA;
		}

		public class RulesGump : Gump
		{
			public RulesGump( Mobile from ): base( 25, 25 )
			{
				string rules = null;
				string path = "Info/Rules.txt";

				if ( File.Exists( path ))
				{
					StreamReader r = new StreamReader( path, System.Text.Encoding.Default, false );
					rules = r.ReadToEnd();
					r.Close();
					rules = rules.ToString();
				}

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 151);
				AddImage(300, 0, 151);
				AddImage(0, 300, 151);
				AddImage(300, 300, 151);
				AddImage(600, 0, 151);
				AddImage(600, 300, 151);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(301, 298, 129);
				AddImage(598, 298, 129);
				AddImage(8, 11, 145);
				AddImage(267, 19, 141);
				AddImage(473, 19, 141);
				AddImage(698, 7, 146);
				AddImage(219, 14, 143);
				AddImage(249, 31, 159);
				AddImage(50, 260, 161);
				AddImage(853, 210, 161);
				AddImage(853, 257, 161);
				AddImage(854, 554, 157);
				AddImage(51, 554, 157);
				AddItem(179, 49, 7775);
				AddHtml( 234, 72, 428, 27, @"<BODY><BASEFONT Color=#FBFBFB><BIG>LAWS OF THE LAND</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 100, 155, 737, 418, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + rules + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( e.InRange( this.GetWorldLocation(), 4 ) )
			{
				e.SendGump( new RulesGump( e ) );
			}
		}

		public RulesBoard(Serial serial) : base(serial)
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