using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Items
{
	public class VortexCube : Item
	{
		public Mobile CubeOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Cube_Owner { get{ return CubeOwner; } set{ CubeOwner = value; } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasConvexLense;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_ConvexLense { get { return HasConvexLense; } set { HasConvexLense = value; InvalidateProperties(); } }

		public int HasConcaveLense;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_ConcaveLense { get { return HasConcaveLense; } set { HasConcaveLense = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasKeyLaw;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_KeyLaw { get { return HasKeyLaw; } set { HasKeyLaw = value; InvalidateProperties(); } }

			public string TextKeyLaw;
			[CommandProperty(AccessLevel.Owner)]
			public string Text_KeyLaw { get { return TextKeyLaw; } set { TextKeyLaw = value; InvalidateProperties(); } }

			public string LocationKeyLaw;
			[CommandProperty(AccessLevel.Owner)]
			public string Location_KeyLaw { get { return LocationKeyLaw; } set { LocationKeyLaw = value; InvalidateProperties(); } }

		public int HasKeyChaos;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_KeyChaos { get { return HasKeyChaos; } set { HasKeyChaos = value; InvalidateProperties(); } }

			public string TextKeyChaos;
			[CommandProperty(AccessLevel.Owner)]
			public string Text_KeyChaos { get { return TextKeyChaos; } set { TextKeyChaos = value; InvalidateProperties(); } }

			public string LocationKeyChaos;
			[CommandProperty(AccessLevel.Owner)]
			public string Location_KeyChaos { get { return LocationKeyChaos; } set { LocationKeyChaos = value; InvalidateProperties(); } }

		public int HasKeyBalance;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_KeyBalance { get { return HasKeyBalance; } set { HasKeyBalance = value; InvalidateProperties(); } }

			public string TextKeyBalance;
			[CommandProperty(AccessLevel.Owner)]
			public string Text_KeyBalance { get { return TextKeyBalance; } set { TextKeyBalance = value; InvalidateProperties(); } }

			public string LocationKeyBalance;
			[CommandProperty(AccessLevel.Owner)]
			public string Location_KeyBalance { get { return LocationKeyBalance; } set { LocationKeyBalance = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasCrystalRed;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_CrystalRed { get { return HasCrystalRed; } set { HasCrystalRed = value; InvalidateProperties(); } }

		public int HasCrystalBlue;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_CrystalBlue { get { return HasCrystalBlue; } set { HasCrystalBlue = value; InvalidateProperties(); } }

		public int HasCrystalGreen;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_CrystalGreen { get { return HasCrystalGreen; } set { HasCrystalGreen = value; InvalidateProperties(); } }

		public int HasCrystalYellow;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_CrystalYellow { get { return HasCrystalYellow; } set { HasCrystalYellow = value; InvalidateProperties(); } }

		public int HasCrystalWhite;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_CrystalWhite { get { return HasCrystalWhite; } set { HasCrystalWhite = value; InvalidateProperties(); } }

		public int HasCrystalPurple;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_CrystalPurple { get { return HasCrystalPurple; } set { HasCrystalPurple = value; InvalidateProperties(); } }

		public string TextCrystal;
		[CommandProperty(AccessLevel.Owner)]
		public string Text_Crystal { get { return TextCrystal; } set { TextCrystal = value; InvalidateProperties(); } }

		public string LocationCrystal;
		[CommandProperty(AccessLevel.Owner)]
		public string Location_Crystal { get { return LocationCrystal; } set { LocationCrystal = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		[Constructable]
		public VortexCube() : base( 0x05D5 )
		{
			Name = "vortex cube";
			Weight = 1.0;
			Light = LightType.Circle150;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( CubeOwner != null ){ list.Add( 1049644, "Belongs to " + CubeOwner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( CubeOwner != from )
			{
				from.SendMessage( "This Codex does not belong to you so it vanishes!" );
				bool remove = true;
				foreach ( Account a in Accounts.GetAccounts() )
				{
					if (a == null)
						break;

					int index = 0;

					for (int i = 0; i < a.Length; ++i)
					{
						Mobile m = a[i];

						if (m == null)
							continue;

						if ( m == CubeOwner )
						{
							m.AddToBackpack( this );
							remove = false;
						}

						++index;
					}
				}
				if ( remove )
				{
					this.Delete();
				}
			}
			else
			{
				from.SendSound( 0x5AA );
				from.CloseGump( typeof( VortexGump ) );
				from.SendGump( new VortexGump( this ) );
			}
		}

		public VortexCube(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);

			writer.Write( (Mobile)CubeOwner);

            writer.Write( HasConvexLense );
            writer.Write( HasConcaveLense );

            writer.Write( HasKeyLaw );
            writer.Write( TextKeyLaw );
            writer.Write( LocationKeyLaw );
            writer.Write( HasKeyChaos );
            writer.Write( TextKeyChaos );
            writer.Write( LocationKeyChaos );
            writer.Write( HasKeyBalance );
            writer.Write( TextKeyBalance );
            writer.Write( LocationKeyBalance );

            writer.Write( HasCrystalRed );
            writer.Write( HasCrystalBlue );
            writer.Write( HasCrystalGreen );
            writer.Write( HasCrystalYellow );
            writer.Write( HasCrystalWhite );
            writer.Write( HasCrystalPurple );

            writer.Write( TextCrystal );
            writer.Write( LocationCrystal );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			CubeOwner = reader.ReadMobile();

			HasConvexLense = reader.ReadInt();
			HasConcaveLense = reader.ReadInt();

			HasKeyLaw = reader.ReadInt();
			TextKeyLaw = reader.ReadString();
			LocationKeyLaw = reader.ReadString();
			HasKeyChaos = reader.ReadInt();
			TextKeyChaos = reader.ReadString();
			LocationKeyChaos = reader.ReadString();
			HasKeyBalance = reader.ReadInt();
			TextKeyBalance = reader.ReadString();
			LocationKeyBalance = reader.ReadString();

			HasCrystalRed = reader.ReadInt();
			HasCrystalBlue = reader.ReadInt();
			HasCrystalGreen = reader.ReadInt();
			HasCrystalYellow = reader.ReadInt();
			HasCrystalWhite = reader.ReadInt();
			HasCrystalPurple = reader.ReadInt();

			TextCrystal = reader.ReadString();
			LocationCrystal = reader.ReadString();
		}

		private class VortexGump : Gump
		{
			private VortexCube m_Cube;

			public VortexGump( VortexCube cube ) : base( 25, 25 )
			{
				m_Cube = cube;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(300, 0, 153);
				AddImage(600, 0, 153);
				AddImage(0, 300, 153);
				AddImage(300, 300, 153);
				AddImage(600, 300, 153);
				AddImage(900, 0, 153);
				AddImage(900, 300, 153);
				AddImage(0, 600, 153);
				AddImage(300, 600, 153);
				AddImage(600, 600, 153);
				AddImage(900, 600, 153);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(600, 2, 129);
				AddImage(898, 2, 129);
				AddImage(2, 302, 129);
				AddImage(300, 302, 129);
				AddImage(600, 302, 129);
				AddImage(898, 302, 129);
				AddImage(2, 598, 129);
				AddImage(300, 598, 129);
				AddImage(600, 598, 129);
				AddImage(898, 598, 129);
				AddImage(6, 7, 145);
				AddImage(8, 653, 142);
				AddImage(328, 862, 140);
				AddImage(594, 862, 140);
				AddImage(850, 680, 144);
				AddImage(166, 8, 140);
				AddImage(464, 8, 140);
				AddImage(996, 8, 146);
				AddImage(993, 22, 156);
				AddImage(1027, 36, 162);
				AddImage(1006, 18, 156);
				AddImage(699, 8, 140);
				AddImage(1004, 10, 143);

				// BOOK AND ANKH
				AddItem(107, 170, 2076);
				AddItem(1060, 129, 4);
				AddItem(1082, 129, 5);

				AddHtml( 176, 32, 307, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>CODEX OF ULTIMATE WISDOM</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 177, 73, 857, 165, @"<BODY><BASEFONT Color=#FCFF00><BIG>Those that wield the Codex of Ultimate Wisdom, can use the knowledge within to become more intelligent (+25) and a grandmaster in two skills of their choice (+100 in 2 chosen skills). The Codex lies within the Ethereal Void and can only be drawn out from within the Chamber of the Codex. To do this, you must obtain the 3 Keys of Infinity in order to enter the chamber. To see into the Void, where the Codex lies, you will need the Convex and Concave Lenses. Finally, this Cube has the power to draw things out from the Void. In order to do that, you will need to find the 6 void crystals to power the cube. If you manage to find all of these items, you can enter the Chamber of the Codex and approach the Void. The Codex will then be yours to do with what you wish, but it will be yours alone to use. Make sure to bring this cube with you when doing this quest.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				// PEDESTALS
				AddItem(85, 246, 4643);
				AddItem(85, 346, 4643);
				AddItem(85, 446, 4643);

				AddHtml( 140, 252, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>The Vortex Cube</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 140, 279, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(80, 245, 1493);

				AddHtml( 140, 355, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>The Concave Lense</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				if ( m_Cube.HasConcaveLense > 0 )
				{
					AddHtml( 140, 382, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddItem(80, 343, 1517);
				}
				else
				{
					AddHtml( 140, 382, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Naxatilor " + GargoyleLocation( "Naxatilor" ) + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				AddHtml( 140, 458, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>The Convex Lense</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				if ( m_Cube.HasConvexLense > 0 )
				{
					AddHtml( 140, 485, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddItem(80, 443, 1518);
				}
				else
				{
					AddHtml( 140, 485, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Lor-wis-lem " + GargoyleLocation( "Lor-wis-lem" ) + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				// PEDESTAL
				AddItem(84, 552, 13042);

				if ( m_Cube.HasKeyLaw > 0 )
				{
					AddItem(89, 551, 13519); // KEY OF LAW
					AddHtml( 140, 538, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The Key of Law has been found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else
				{
					AddHtml( 140, 538, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The Key of Law " + m_Cube.TextKeyLaw + " " + m_Cube.LocationKeyLaw + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				if ( m_Cube.HasKeyBalance > 0 )
				{
					AddItem(98, 542, 13516); // KEY OF BALANCE
					AddHtml( 140, 568, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The Key of Balance has been found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else
				{
					AddHtml( 140, 568, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The Key of Balance " + m_Cube.TextKeyBalance + " " + m_Cube.LocationKeyBalance + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				if ( m_Cube.HasKeyChaos > 0 )
				{
					AddItem(109, 550, 13520); // KEY OF CHAOS
					AddHtml( 140, 598, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The Key of Chaos has been found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else
				{
					AddHtml( 140, 598, 1016, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The Key of Chaos " + m_Cube.TextKeyChaos + " " + m_Cube.LocationKeyChaos + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

				AddHtml( 99, 656, 1016, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>The Void Crystals are scattered throughout the land. The Vortex Cube can draw you toward the dungeons they may be in.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( ( m_Cube.HasCrystalRed + m_Cube.HasCrystalBlue + m_Cube.HasCrystalGreen + m_Cube.HasCrystalYellow + m_Cube.HasCrystalWhite + m_Cube.HasCrystalPurple ) > 5 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>All of the Void Crystals have been found!</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Cube.HasCrystalRed == 0 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The red Void Crystal " + m_Cube.TextCrystal + " " + m_Cube.LocationCrystal + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Cube.HasCrystalBlue == 0 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The blue Void Crystal " + m_Cube.TextCrystal + " " + m_Cube.LocationCrystal + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Cube.HasCrystalGreen == 0 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The green Void Crystal " + m_Cube.TextCrystal + " " + m_Cube.LocationCrystal + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Cube.HasCrystalYellow == 0 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The yellow Void Crystal " + m_Cube.TextCrystal + " " + m_Cube.LocationCrystal + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Cube.HasCrystalWhite == 0 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The white Void Crystal " + m_Cube.TextCrystal + " " + m_Cube.LocationCrystal + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( m_Cube.HasCrystalPurple == 0 )
				{
					AddHtml( 116, 715, 976, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>The purple Void Crystal " + m_Cube.TextCrystal + " " + m_Cube.LocationCrystal + ".</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}

				// PEDESTALS
				AddItem(382, 788, 4643);
				AddItem(462, 788, 4643);
				AddItem(542, 788, 4643);
				AddItem(622, 788, 4643);
				AddItem(702, 788, 4643);
				AddItem(782, 788, 4643);

				if ( m_Cube.HasCrystalRed > 0 ){ AddItem(379, 782, 6284); }
				if ( m_Cube.HasCrystalBlue > 0 ){ AddItem(459, 782, 6286); }
				if ( m_Cube.HasCrystalGreen > 0 ){ AddItem(539, 782, 6288); }
				if ( m_Cube.HasCrystalYellow > 0 ){ AddItem(619, 782, 6290); }
				if ( m_Cube.HasCrystalWhite > 0 ){ AddItem(699, 782, 6496); }
				if ( m_Cube.HasCrystalPurple > 0 ){ AddItem(779, 782, 6498); }
			}
		}

		public static string GargoyleLocation( string gargoyle )
		{
			string where = "the gargoyle's whereabouts are currently unknown";

			foreach ( Mobile mob in World.Mobiles.Values )
			if ( mob is CodexGargoyleA && gargoyle == "Naxatilor" )
			{
				where = "the gargoyle is said to be within " + Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location );
			}
			else if ( mob is CodexGargoyleB && gargoyle == "Lor-wis-lem" )
			{
				where = "the gargoyle is said to be within " + Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location );
			}

			return where;
		}
	}
}