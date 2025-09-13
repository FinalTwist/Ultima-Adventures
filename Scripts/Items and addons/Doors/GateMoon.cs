using System;
using Server;
using Server.Mobiles;
using Server.Network;
using System.Collections; 
using Server.Items;
using Server.Misc;
using Server.Gumps;

namespace Server.Items
{
	public class GateMoon : Item
	{
		[Constructable]
		public GateMoon() : base( 0x1B72 )
		{
			Movable = false;
			Visible = false;
			Name = "moongate";
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				string world = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );

				if ( m is PlayerMobile && world == "the Bottle World of Kuldar" && !( Server.Items.CharacterDatabase.GetKeys( m, "VordoKey" ) ) )
				{
					m.SendMessage( "This magical gate doesn't seem to do anything." );
				}
				else if ( Worlds.AllowEscape( m, m.Map, m.Location, m.X, m.Y ) == false && Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y ) != "the Bottle World of Kuldar" )
				{
					m.SendMessage( "This magical gate doesn't seem to do anything." );
				}
				else if ( Worlds.RegionAllowedRecall( m.Map, m.Location, m.X, m.Y ) == false && Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y ) != "the Land of Ambrosia" && Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y ) != "the Bottle World of Kuldar" )
				{
					m.SendMessage( "This magical gate doesn't seem to do anything." );
				}
				else 
				{
					Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ),( delegate
					{
						foreach ( Mobile pet in World.Mobiles.Values )
						{
							if ( pet is BaseCreature )
							{
								BaseCreature bc = (BaseCreature)pet;
								if ( bc.Controlled && bc.ControlMaster == m )
									pet.Hidden = true;
							}
						}
						m.Hidden = true;
					} ) );

					m.PlaySound( 0x20E );
					m.CloseGump( typeof( MoonGateGump ) );
					m.SendGump( new MoonGateGump( m, false ) );
					m.SendMessage( "Choose a destination." );
				}
			}
			return true;
		}

		public class MoonGateGump : Gump
		{
			public MoonGateGump( Mobile from, bool IsBlackrock ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=false;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(300, 0, 153);
				AddImage(600, 0, 153);
				AddImage(0, 300, 153);
				AddImage(300, 300, 153);
				AddImage(600, 300, 153);
				AddImage(2, 2, 129);
				AddImage(302, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(302, 298, 129);
				AddImage(598, 298, 129);
				AddImage(6, 5, 133);
				AddImage(232, 44, 132);
				AddImage(531, 44, 132);
				AddImage(676, 40, 156);
				AddImage(679, 7, 134);
				AddImage(190, 536, 130);
				AddImage(25, 365, 128);
				AddImage(543, 564, 159);
				AddImage(464, 531, 143);
				AddImage(753, 444, 5608);
				AddImage(566, 353, 147);
				
				string mainTitle = "MOONGATE DESTINATIONS";
					if ( IsBlackrock ){ mainTitle = "BLACKROCK GATE DESTINATIONS"; AddItem(697, 362, 6248); } else { AddItem(709, 371, 19586); } 

				AddHtml( 140, 72, 600, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>" + mainTitle + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				/////////////////////////////////////////////////////////////////////////////

				int GateAmount = 31; // THE AMOUNT OF MOONGATES IN THE GAME - MAX 30
				int GateNumber = 0;
				string sPlace = "";
				int counter = 0;

				while ( GateAmount > 0 )
				{
					GateAmount--;
					GateNumber++;

					sPlace = GetGateName( GateNumber, from );

					if ( sPlace != "" )
					{
						counter++;

						if ( counter == 1 ){
							AddButton(104, 114, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 114, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 2 ){
							AddButton(104, 144, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 144, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 3 ){
							AddButton(104, 174, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 174, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 4 ){
							AddButton(104, 204, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 204, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 5 ){
							AddButton(104, 234, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 234, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 6 ){
							AddButton(104, 264, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 264, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 7 ){
							AddButton(104, 294, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 294, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 8 ){
							AddButton(104, 324, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 324, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 9 ){
							AddButton(104, 354, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 354, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 10 ){
							AddButton(104, 384, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 139, 384, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);


						} else if ( counter == 11 ){
							AddButton(366, 114, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 114, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 12 ){
							AddButton(366, 144, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 144, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 13 ){
							AddButton(366, 174, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 174, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 14 ){
							AddButton(366, 204, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 204, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 15 ){
							AddButton(366, 234, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 234, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 16 ){
							AddButton(366, 264, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 264, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 17 ){
							AddButton(366, 294, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 294, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 18 ){
							AddButton(366, 324, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 324, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 19 ){
							AddButton(366, 354, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 354, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 20 ){
							AddButton(366, 384, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 384, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 21 ){
							AddButton(366, 414, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 414, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 22 ){
							AddButton(366, 444, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 401, 444, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);


						} else if ( counter == 23 ){
							AddButton(632, 114, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 114, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 24 ){
							AddButton(632, 144, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 144, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 25 ){
							AddButton(632, 174, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 174, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 26 ){
							AddButton(632, 204, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 204, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 27 ){
							AddButton(632, 234, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 234, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 28 ){
							AddButton(632, 264, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 264, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 29 ){
							AddButton(632, 294, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 294, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 30 ){
							AddButton(632, 324, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 324, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						} else if ( counter == 31 ){
							AddButton(632, 354, 4005, 4005, GateNumber, GumpButtonType.Reply, 0);
								AddHtml( 667, 354, 205, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sPlace + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
						}
					}
				}
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;
				if (!(from is PlayerMobile))
					return;

				from.CloseGump( typeof( MoonGateGump ) );

				if ( info.ButtonID > 0 )
				{
					bool gate1 = info.ButtonID == 1;
					bool gate2 = info.ButtonID == 2;
					bool gate3 = info.ButtonID == 3;
					bool gate4 = info.ButtonID == 4;
					bool gate5 = info.ButtonID == 5;
					bool gate6 = info.ButtonID == 6;
					bool gate7 = info.ButtonID == 7;
					bool gate8 = info.ButtonID == 8;
					bool gate9 = info.ButtonID == 9;
					bool gate10 = info.ButtonID == 10;
					bool gate11 = info.ButtonID == 11;
					bool gate12 = info.ButtonID == 12;
					bool gate13 = info.ButtonID == 13;
					bool gate14 = info.ButtonID == 14;
					bool gate15 = info.ButtonID == 15;
					bool gate16 = info.ButtonID == 16;
					bool gate17 = info.ButtonID == 17;
					bool gate18 = info.ButtonID == 18;
					bool gate19 = info.ButtonID == 19;
					bool gate20 = info.ButtonID == 20;
					bool gate21 = info.ButtonID == 21;
					bool gate22 = info.ButtonID == 22;
					bool gate23 = info.ButtonID == 23;
					bool gate24 = info.ButtonID == 24;
					bool gate25 = info.ButtonID == 25;
					bool gate26 = info.ButtonID == 26;
					bool gate27 = info.ButtonID == 27;
					bool gate28 = info.ButtonID == 28;
					bool gate29 = info.ButtonID == 29;
					bool gate30 = info.ButtonID == 30;
					bool gate31 = info.ButtonID == 31;

					int gX = 0; int gY = 0; int gZ = 0; Map map = Map.Trammel;

					if (gate1 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 2518; gY = 1529; gZ = 3; map = Map.Trammel;  }
					else if (gate2 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 3723; gY = 2155; gZ = 4; map = Map.Trammel;  }
					else if (gate3 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 1779; gY = 1714; gZ = 6; map = Map.Trammel;  }
					else if (gate4 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 3718; gY = 1136; gZ = 0; map = Map.Trammel;  }
					else if (gate5 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 4970; gY = 1297; gZ = 4; map = Map.Trammel;  }
					else if (gate6 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 3907; gY = 3962; gZ = 5; map = Map.Trammel;  }
					else if (gate7 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 2548; gY = 2685; gZ = 4; map = Map.Trammel;  }
					else if (gate8 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 963; gY = 514; gZ = 4; map = Map.Trammel;  }
					else if (gate9 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 1052; gY = 1570; gZ = 2; map = Map.Trammel;  }
					else if (gate10 && CharacterDatabase.GetDiscovered( from, "the Land of Sosaria" )){gX = 1792; gY = 913; gZ = 27; map = Map.Trammel;  }
					else if (gate11  && CharacterDatabase.GetDiscovered( from, "the Land of Ambrosia" )){gX = 968; gY = 2726; gZ = 4; map = Map.Trammel;  }
					else if (gate12 && CharacterDatabase.GetDiscovered( from, "the Island of Umber Veil" )){gX = 4038; gY = 179; gZ = 2; map = Map.Trammel;  }
					else if (gate13 && CharacterDatabase.GetDiscovered( from, "the Land of Ambrosia" )){gX = 6092; gY = 3595; gZ = 4; map = Map.Trammel;  }
					else if (gate14 && CharacterDatabase.GetDiscovered( from, "the Island of Umber Veil" )){gX = 1249; gY = 3815; gZ = 2; map = Map.Trammel;  }
					else if (gate15 && CharacterDatabase.GetDiscovered( from, "the Isles of Dread" )){gX = 1017; gY = 546; gZ = 3; map = Map.Tokuno;  }
					else if (gate16 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 4199; gY = 2516; gZ = 7; map = Map.Felucca;  }
					else if (gate17 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 2497; gY = 1981; gZ = 5; map = Map.Felucca;  }
					else if (gate18 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 1045; gY = 2258; gZ = 6; map = Map.Felucca;  }
					else if (gate19 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 2350; gY = 3619; gZ = 6; map = Map.Felucca;  }
					else if (gate20 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 4276; gY = 1841; gZ = 16; map = Map.Felucca;  }
					else if (gate21 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 719; gY = 962; gZ = 6; map = Map.Felucca;  }
					else if (gate22 && CharacterDatabase.GetDiscovered( from, "the Land of Lodoria" )){gX = 2876; gY = 733; gZ = 9; map = Map.Felucca;  }
					else if (gate23 && CharacterDatabase.GetDiscovered( from, "the Serpent Island" )){gX = 1163; gY = 411; gZ = 5; map = Map.Malas;  }
					else if (gate24 && CharacterDatabase.GetDiscovered( from, "the Serpent Island" )){gX = 1300; gY = 1372; gZ = 5; map = Map.Malas;  }
					else if (gate25 && CharacterDatabase.GetDiscovered( from, "the Serpent Island" )){gX = 234; gY = 1333; gZ = 3; map = Map.Malas;  }
					else if (gate26 && CharacterDatabase.GetDiscovered( from, "the Savaged Empire" )){gX = 656; gY = 240; gZ = 3; map = Map.TerMur;  }
					else if (gate27 && CharacterDatabase.GetDiscovered( from, "the Savaged Empire" )){gX = 1112; gY = 1710; gZ = 20; map = Map.TerMur;  }
					else if (gate28 && CharacterDatabase.GetDiscovered( from, "the Savaged Empire" )){gX = 303; gY = 1269; gZ = 3; map = Map.TerMur;  }
					else if (gate29 && CharacterDatabase.GetDiscovered( from, "the Bottle World of Kuldar" )){gX = 6603; gY = 1082; gZ = 2; map = Map.Trammel;  }
					else if (gate30 && CharacterDatabase.GetDiscovered( from, "the Bottle World of Kuldar" )){gX = 6377; gY = 302; gZ = 15; map = Map.Felucca;  }
					else if (gate31 && CharacterDatabase.GetDiscovered( from, "DarkMoor" )){gX = 603; gY = 709; gZ = -38; map = Map.Ilshenar;  }

					if ( gX > 0 )
					{
						/*
						IPooledEnumerable eable = map.GetObjectsInRange( from.Location, 2 );
						bool okay = false;

						foreach ( object o in eable )
						{
							if ( o is Item && (o is GateMoon))
							{
								okay = true;
							}
						} */
						
						bool okay = false;

						foreach ( Item item in from.GetItemsInRange( 2 ) )
						{
							if ( item is GateMoon || item is PublicMoongate || item is ObsidianGate )
								okay = true;
						}

						if (okay)
						{
							Point3D loc = new Point3D( gX, gY, gZ );

							if (from is PlayerMobile && from.Karma < -500 && ((PlayerMobile)from).Criminal && gX == 603 && map == Map.Ilshenar)
								((PlayerMobile)from).Criminal = false;

							GateMoonTeleport( from, loc, map );
						}
						else
							from.SendMessage( "There is no MoonGate nearby." ); 
					}
				}
			}
		}

		public static string GetGateName( int gate, Mobile m )
		{
			PlayerMobile pm = (PlayerMobile)m;

			string world = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );

			string sGate = "";

			if ( world == "the Bottle World of Kuldar" && !(CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" )) ){}
			else if ( gate == 1 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" ) ){ sGate = "Sosaria - Central"; }
			else if ( gate == 2 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Clues"; }
			else if ( gate == 3 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Devil Guard"; }
			else if ( gate == 4 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - East"; }
			else if ( gate == 5 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Frozen Isles"; }
			else if ( gate == 6 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Lost Glade"; }
			else if ( gate == 7 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Montor"; }
			else if ( gate == 8 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Moon"; }
			else if ( gate == 9 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - WestGate"; }
			else if ( gate == 10 && CharacterDatabase.GetDiscovered( m, "the Land of Sosaria" )){ sGate = "Sosaria - Yew"; }

			else if ( gate == 11 && CharacterDatabase.GetDiscovered( m, "the Land of Ambrosia" ) ){ sGate = "Isle of Fire"; }
			else if ( gate == 12 && CharacterDatabase.GetDiscovered( m, "the Island of Umber Veil" ) ){ sGate = "Lost Isle"; }

			else if ( gate == 13 && CharacterDatabase.GetDiscovered( m, "the Land of Ambrosia" ) ){ sGate = "Land of Ambrosia"; }
			else if ( gate == 14 && CharacterDatabase.GetDiscovered( m, "the Island of Umber Veil" ) ){ sGate = "Island of Umber Veil"; }
			else if ( gate == 15 && CharacterDatabase.GetDiscovered( m, "the Isles of Dread" ) ){ sGate = "Isles of Dread"; }

			else if ( gate == 16 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - Greensky"; }
			else if ( gate == 17 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - Islegem"; }
			else if ( gate == 18 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - Portshine"; }
			else if ( gate == 19 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - South"; }
			else if ( gate == 20 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - Springvale"; }
			else if ( gate == 21 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - Whisper"; }
			else if ( gate == 22 && CharacterDatabase.GetDiscovered( m, "the Land of Lodoria" ) ){ sGate = "Lodoria - Winterlands"; }

			else if ( gate == 23 && CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ){ sGate = "Serpent Island - North"; }
			else if ( gate == 24 && CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ){ sGate = "Serpent Island - South"; }
			else if ( gate == 25 && CharacterDatabase.GetDiscovered( m, "the Serpent Island" ) ){ sGate = "Serpent Island - West"; }

			else if ( gate == 26 && CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ){ sGate = "Savaged Empire - North"; }
			else if ( gate == 27 && CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ){ sGate = "Savaged Empire - South"; }
			else if ( gate == 28 && CharacterDatabase.GetDiscovered( m, "the Savaged Empire" ) ){ sGate = "Savaged Empire - West"; }

			else if ( gate == 29 && CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" ) ){ sGate = "Bottle World of Kuldar"; }
			else if ( gate == 30 && CharacterDatabase.GetDiscovered( m, "the Bottle World of Kuldar" ) ){ sGate = "Black Knight's Vault"; }
			else if ( gate == 31 && CharacterDatabase.GetDiscovered( m, "DarkMoor" ) ){ sGate = "The Lands of DarkMoor"; }

			return sGate;
		}

		public static void GateMoonTeleport( Mobile m, Point3D loc, Map map )
		{
			BaseCreature.TeleportPets( m, loc, map, false );
			m.MoveToWorld ( loc, map );
			m.PlaySound( 0x1FE );
			foreach ( Mobile pet in World.Mobiles.Values )
			{
				if ( pet is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)pet;
					if ( bc.Controlled && bc.ControlMaster == m )
						pet.Hidden = true;
				}
			}
			m.Hidden = true;
		}

		public GateMoon(Serial serial) : base(serial)
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