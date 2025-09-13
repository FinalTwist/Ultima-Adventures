using System;
using Server; 
using Server.Misc;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class ComputerDatabase : Item
	{
		[Constructable]
		public ComputerDatabase() : base( 0x3A2 )
		{
			Movable = false;
			Name = "computer terminal";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 4 ) )
			{
				from.CloseGump( typeof( ComputerDatabaseGump ) );
				from.SendGump( new ComputerDatabaseGump( from ) );
				from.SendSound( 0x54D );
			}
		}

		public ComputerDatabase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class ComputerDatabaseGump : Gump
		{
			public ComputerDatabaseGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 30521);
				AddHtml( 46, 41, 672, 102, @"<BODY><BASEFONT Color=#00FF06>This computer is mostly ruined due to corrupt data, but there is a fragment of your medical record still accessible. The medical record in your pack mentions the world you come from, but it doesn't provide details on the appearance of those that come from that planet. Select the colors below that fit your preferred style, where the cancelling entries of skin and hair will give you a random human color. If you like the way you currently look, then simply close the computer terminal.</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 55, 160, 191, 19, @"<BODY><BASEFONT Color=#00FF06>Skin Color</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 275, 160, 191, 19, @"<BODY><BASEFONT Color=#00FF06>Hair Color</BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(55, 190, 17440, 0x406);
				AddItem(55, 243, 17440, 0x6F6);
				AddItem(55, 296, 17440, 0x97F);
				AddItem(55, 349, 17440, 0x99B);
				AddItem(55, 402, 17440, 0x6E4);
				AddItem(55, 455, 17440, 0x870);
				AddItem(55, 508, 17440, 0xB38);
				AddItem(55, 561, 17440, 0xB54);
				AddButton(115, 202, 4020, 4020, 1, GumpButtonType.Reply, 0);
				AddButton(115, 255, 3609, 3609, 2, GumpButtonType.Reply, 0);
				AddButton(115, 308, 3609, 3609, 3, GumpButtonType.Reply, 0);
				AddButton(115, 361, 3609, 3609, 4, GumpButtonType.Reply, 0);
				AddButton(115, 414, 3609, 3609, 5, GumpButtonType.Reply, 0);
				AddButton(115, 467, 3609, 3609, 6, GumpButtonType.Reply, 0);
				AddButton(115, 520, 3609, 3609, 7, GumpButtonType.Reply, 0);
				AddButton(115, 573, 3609, 3609, 8, GumpButtonType.Reply, 0);

				AddItem(275, 190, 17440, 0x456);
				AddItem(275, 243, 17440, 0x829);
				AddItem(275, 296, 17440, 0xAF8);
				AddItem(275, 349, 17440, 0x82C);
				AddItem(275, 402, 17440, 0xB42);
				AddItem(275, 455, 17440, 0x8C1);
				AddItem(275, 508, 17440, 0x7A9);
				AddItem(275, 561, 17440, 0x8D7);
				AddButton(335, 202, 4020, 4020, 9, GumpButtonType.Reply, 0);
				AddButton(335, 255, 3609, 3609, 10, GumpButtonType.Reply, 0);
				AddButton(335, 308, 3609, 3609, 11, GumpButtonType.Reply, 0);
				AddButton(335, 361, 3609, 3609, 12, GumpButtonType.Reply, 0);
				AddButton(335, 414, 3609, 3609, 13, GumpButtonType.Reply, 0);
				AddButton(335, 467, 3609, 3609, 14, GumpButtonType.Reply, 0);
				AddButton(335, 520, 3609, 3609, 15, GumpButtonType.Reply, 0);
				AddButton(335, 573, 3609, 3609, 16, GumpButtonType.Reply, 0);

				AddItem(450, 190, 17440, 0x77F);
				AddItem(450, 243, 17440, 0x870);
				AddItem(450, 296, 17440, 0x6F8);
				AddItem(450, 349, 17440, 0x705);
				AddItem(450, 402, 17440, 0x877);
				AddItem(450, 455, 17440, 0x776);
				AddItem(450, 508, 17440, 0x825);
				AddItem(450, 561, 17440, 0x701);
				AddButton(510, 202, 3609, 3609, 17, GumpButtonType.Reply, 0);
				AddButton(510, 255, 3609, 3609, 18, GumpButtonType.Reply, 0);
				AddButton(510, 308, 3609, 3609, 19, GumpButtonType.Reply, 0);
				AddButton(510, 361, 3609, 3609, 20, GumpButtonType.Reply, 0);
				AddButton(510, 414, 3609, 3609, 21, GumpButtonType.Reply, 0);
				AddButton(510, 467, 3609, 3609, 22, GumpButtonType.Reply, 0);
				AddButton(510, 520, 3609, 3609, 23, GumpButtonType.Reply, 0);
				AddButton(510, 573, 3609, 3609, 24, GumpButtonType.Reply, 0);

				AddItem(625, 190, 17440, 0x406);
				AddItem(625, 243, 17440, 0x97F);
				AddItem(625, 296, 17440, 0x99B);
				AddItem(625, 349, 17440, 0x6E4);
				AddItem(625, 402, 17440, 0x5E0);
				AddItem(625, 455, 17440, 0xB38);
				AddItem(625, 508, 17440, 0xB2B);
				AddItem(625, 561, 17440, 0x497);
				AddButton(685, 202, 3609, 3609, 25, GumpButtonType.Reply, 0);
				AddButton(685, 255, 3609, 3609, 26, GumpButtonType.Reply, 0);
				AddButton(685, 308, 3609, 3609, 27, GumpButtonType.Reply, 0);
				AddButton(685, 361, 3609, 3609, 28, GumpButtonType.Reply, 0);
				AddButton(685, 414, 3609, 3609, 29, GumpButtonType.Reply, 0);
				AddButton(685, 467, 3609, 3609, 30, GumpButtonType.Reply, 0);
				AddButton(685, 520, 3609, 3609, 31, GumpButtonType.Reply, 0);
				AddButton(685, 573, 3609, 3609, 32, GumpButtonType.Reply, 0);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile;
				CharacterDatabase DataBase = Server.Items.CharacterDatabase.GetDB( from );

				int skinColor = 0;
				int hairColor = 0;

				if ( info.ButtonID == 1 ){ 			skinColor = RandomThings.GetRandomSkinColor(); }
				else if ( info.ButtonID == 2 ){ 	skinColor = 0x6F6; }
				else if ( info.ButtonID == 3 ){ 	skinColor = 0x97F; }
				else if ( info.ButtonID == 4 ){ 	skinColor = 0x99B; }
				else if ( info.ButtonID == 5 ){ 	skinColor = 0x6E4; }
				else if ( info.ButtonID == 6 ){ 	skinColor = 0x870; }
				else if ( info.ButtonID == 7 ){ 	skinColor = 0xB38; }
				else if ( info.ButtonID == 8 ){ 	skinColor = 0xB54; }

				else if ( info.ButtonID == 9 ){  	hairColor = RandomThings.GetRandomHairColor(); }
				else if ( info.ButtonID == 10 ){ 	hairColor = 0x829; }
				else if ( info.ButtonID == 11 ){ 	hairColor = 0xAF8; }
				else if ( info.ButtonID == 12 ){ 	hairColor = 0x82C; }
				else if ( info.ButtonID == 13 ){ 	hairColor = 0xB42; }
				else if ( info.ButtonID == 14 ){ 	hairColor = 0x8C1; }
				else if ( info.ButtonID == 15 ){ 	hairColor = 0x7A9; }
				else if ( info.ButtonID == 16 ){ 	hairColor = 0x8D7; }

				else if ( info.ButtonID == 17 ){ 	hairColor = 0x77F; }
				else if ( info.ButtonID == 18 ){ 	hairColor = 0x870; }
				else if ( info.ButtonID == 19 ){ 	hairColor = 0x6F8; }
				else if ( info.ButtonID == 20 ){ 	hairColor = 0x705; }
				else if ( info.ButtonID == 21 ){ 	hairColor = 0x877; }
				else if ( info.ButtonID == 22 ){ 	hairColor = 0x776; }
				else if ( info.ButtonID == 23 ){ 	hairColor = 0x825; }
				else if ( info.ButtonID == 24 ){ 	hairColor = 0x701; }

				else if ( info.ButtonID == 25 ){ 	hairColor = 0x406; }
				else if ( info.ButtonID == 26 ){ 	hairColor = 0x97F; }
				else if ( info.ButtonID == 27 ){ 	hairColor = 0x99B; }
				else if ( info.ButtonID == 28 ){ 	hairColor = 0x6E4; }
				else if ( info.ButtonID == 29 ){ 	hairColor = 0x5E0; }
				else if ( info.ButtonID == 30 ){ 	hairColor = 0xB38; }
				else if ( info.ButtonID == 31 ){ 	hairColor = 0xB2B; }
				else if ( info.ButtonID == 32 ){ 	hairColor = 0x497; }

				if ( skinColor > 0 )
				{
					from.Hue = skinColor;
					DataBase.CharHue = skinColor;
					from.SendGump( new ComputerDatabaseGump( from ) );
					from.SendSound( 0x54B );
				}
				else if ( hairColor > 0 )
				{
					from.HairHue = hairColor;
					from.FacialHairHue = hairColor;
					DataBase.CharHairHue = hairColor;
					from.SendGump( new ComputerDatabaseGump( from ) );
					from.SendSound( 0x54B );
				}
				else
				{
					from.SendSound( 0x54D );
				}
			}
		}
	}
}