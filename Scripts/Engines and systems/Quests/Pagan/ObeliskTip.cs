using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Items
{
	public class ObeliskTip : Item
	{
		public Mobile ObeliskOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Obelisk_Owner { get{ return ObeliskOwner; } set{ ObeliskOwner = value; } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasAir;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Air { get { return HasAir; } set { HasAir = value; InvalidateProperties(); } }

		public int WonAir;
		[CommandProperty(AccessLevel.Owner)]
		public int Won_Air { get { return WonAir; } set { WonAir = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasFire;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Fire { get { return HasFire; } set { HasFire = value; InvalidateProperties(); } }

		public int WonFire;
		[CommandProperty(AccessLevel.Owner)]
		public int Won_Fire { get { return WonFire; } set { WonFire = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasEarth;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Earth { get { return HasEarth; } set { HasEarth = value; InvalidateProperties(); } }

		public int WonEarth;
		[CommandProperty(AccessLevel.Owner)]
		public int Won_Earth { get { return WonEarth; } set { WonEarth = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		public int HasWater;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Water { get { return HasWater; } set { HasWater = value; InvalidateProperties(); } }

		public int WonWater;
		[CommandProperty(AccessLevel.Owner)]
		public int Won_Water { get { return WonWater; } set { WonWater = value; InvalidateProperties(); } }

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		[Constructable]
		public ObeliskTip() : base( 0x185F )
		{
			Name = "obelisk tip";
			Weight = 1.0;
			Light = LightType.Circle150;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( ObeliskOwner != null ){ list.Add( 1049644, "Belongs to " + ObeliskOwner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( ObeliskOwner != from )
			{
				from.SendMessage( "This blackrock does not belong to you so it vanishes!" );
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

						if ( m == ObeliskOwner )
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
				from.CloseGump( typeof( ObeliskGump ) );
				from.SendGump( new ObeliskGump( this ) );
			}
		}

		public ObeliskTip(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);

			writer.Write( (Mobile)ObeliskOwner);

            writer.Write( HasAir );
            writer.Write( WonAir );
            writer.Write( HasFire );
            writer.Write( WonFire );
            writer.Write( HasEarth );
            writer.Write( WonEarth );
            writer.Write( HasWater );
            writer.Write( WonWater );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			ObeliskOwner = reader.ReadMobile();

			HasAir = reader.ReadInt();
			WonAir = reader.ReadInt();
			HasFire = reader.ReadInt();
			WonFire = reader.ReadInt();
			HasEarth = reader.ReadInt();
			WonEarth = reader.ReadInt();
			HasWater = reader.ReadInt();
			WonWater = reader.ReadInt();
		}

		private class ObeliskGump : Gump
		{
			private ObeliskTip m_Tip;

			public ObeliskGump( ObeliskTip tip ) : base( 25, 25 )
			{
				m_Tip = tip;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 151);
				AddImage(300, 0, 151);
				AddImage(0, 300, 151);
				AddImage(300, 300, 151);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(7, 7, 133);
				AddImage(180, 11, 165);
				AddImage(26, 365, 128);
				AddImage(173, 45, 132);
				AddImage(600, 0, 151);
				AddImage(600, 300, 151);
				AddImage(598, 2, 129);
				AddImage(598, 298, 129);
				AddImage(427, 536, 130);
				AddImage(479, 536, 130);
				AddImage(467, 45, 132);
				AddImage(754, 531, 143);
				AddImage(681, 6, 134);
				AddItem(782, 447, 6248);
				AddItem(100, 100-5, 13042);
				AddItem(100, 175-10, 13042);
				AddItem(100, 250-15, 13042);
				AddItem(100, 325-20, 13042);
				AddItem(100, 400-25, 13042);
				AddHtml( 180, 68, 200, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>THE TITAN OF ETHER</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 360, 105, 523, 336, @"<BODY><BASEFONT Color=#FCFF00><BIG>There are those that seek to become the Titan of Ether, but in order to do so, one must defeat the four Titans of the Underworld. Lithos, Pyros, Hydros, and Stratos all contain an elemental power that can be infused within blackrock and empower another. These Titans cannot simply be slain by normal means, as their opponent must possess a particular piece of blackrock in order to best them. These blackrock fragments are scattered throughout the lands and you will have to search far and wide to find them. If you undertake this great quest, be sure to carry the Obelisk Tip with you at all times. If you have the appropriate piece of blackrock, you can decide to face the Titan. If the Titan is slain, the blackrock will absorb their power. Once all of the Titans' power has been absorbed in the four pieces of blackrock, bring the Obelisk Tip to the Obsidian Fortress and approach the Blackrock Gate to become the Titan of Ether. Titans of Ether are special in that their abilities can total 300 instead of 250.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int rocks = tip.HasAir + tip.HasFire + tip.HasEarth + tip.HasWater + 1;
				int titan = tip.WonAir + tip.WonFire + tip.WonEarth + tip.WonWater;

				string stones = "1 of the Pieces of Blackrock Found!";
				string titans = "No Titans Have Been Defeated Yet!";

				if ( rocks > 4 ){ stones = "All of the Pieces of Blackrock Found!"; }
				else if ( rocks > 1 ){ stones = rocks + " Pieces of Blackrock Found!"; }

				if ( titan > 3 ){ titans = "All of the Titans Have Been Defeated!"; }
				else if ( titan == 1 ){ titans = "1 Titan Has Been Defeated!"; }
				else if ( titan > 1 ){ titans = titan + " Titans Have Been Defeated!"; }

				AddHtml( 454, 463, 280, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + stones + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 454, 502, 280, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + titans + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 160, 107-5, 150, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Obelisk Tip</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 160, 130-5, 150, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Khumash-Gor</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(106, 100-5, 6239);
				AddItem(285, 114-5, 11402);

				AddHtml( 160, 182-10, 150, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Breath of Air</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 160, 205-10, 150, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Stratos</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				if ( tip.HasAir > 0 ){ AddItem(106, 180-10, 6240); }
				if ( tip.WonAir > 0 ){ AddItem(285, 188-10, 11402); }

				AddHtml( 160, 257-15, 150, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tongue of Flame</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 160, 280-15, 150, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Pyros</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				if ( tip.HasFire > 0 ){ AddItem(105, 253-15, 6241); }
				if ( tip.WonFire > 0 ){ AddItem(285, 264-15, 11402); }

				AddHtml( 160, 332-20, 150, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Heart of Earth</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 160, 355-20, 150, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Lithos</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				if ( tip.HasEarth > 0 ){ AddItem(106, 331-20, 6242); }
				if ( tip.WonEarth > 0 ){ AddItem(285, 342-20, 11402); }

				AddHtml( 160, 406-25, 150, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tear of the Seas</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 160, 430-25, 150, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Hydros</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				if ( tip.HasWater > 0 ){ AddItem(106, 401-25, 6243); }
				if ( tip.WonWater > 0 ){ AddItem(285, 415-25, 11402); }
			}
		}
	}
}