using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
    public class ECrystalTableComponent : AddonComponent
    {
        [Constructable]
        public ECrystalTableComponent(int itemID)
            : base(itemID)
        {
            Weight = 100.0;
            Movable = false;
        }

        public override int LabelNumber { get { return 1076673; } }
        public ECrystalTableComponent(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 906, 1019045); // I can't reach that.
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class ECrystalTableAddon : BaseAddon
	{
        public override BaseAddonDeed Deed { get { return new ECrystalTableDeed(); } }

		[Constructable]
		public ECrystalTableAddon( bool east )
		{
			if ( east )
			{
                AddComponent(new ECrystalTableComponent(0x3605), 0, 0, 0);
                AddComponent(new ECrystalTableComponent(0x3606), 0, -1, 0);
			}
			else
			{
                AddComponent(new ECrystalTableComponent(0x3608), 0, 0, 0);
                AddComponent(new ECrystalTableComponent(0x3607), 1, 0, 0);
			}
		}

		public ECrystalTableAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

    public class ECrystalTableDeed : BaseAddonDeed
	{
		private bool m_East;

        public override BaseAddon Addon { get { return new ECrystalTableAddon(m_East); } }

        public override int LabelNumber { get { return 1076673; } }

		[Constructable]
		public ECrystalTableDeed()
		{
			ItemID = 0x14EF;
            Hue = 0x495;
            Weight = 1.0;
            LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.CloseGump( typeof( InternalGump ) );
				from.SendGump( new InternalGump( this ) );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private void SendTarget( Mobile m )
		{
			base.OnDoubleClick( m );
		}

		private class InternalGump : Gump
		{
			private ECrystalTableDeed m_Deed;

			public InternalGump( ECrystalTableDeed deed ) : base( 60, 35 )
			{
				m_Deed = deed;

                AddBackground(0, 0, 270, 330, 0x13BE);

                AddImageTiled(10, 10, 250, 20, 0xA40);
                AddImageTiled(10, 40, 250, 250, 0xA40);
                AddImageTiled(10, 300, 250, 20, 0xA40);

                AddAlphaRegion(10, 10, 250, 310);

                AddHtmlLocalized(13, 12, 250, 20, 1076727, 0x7FFF, false, false); //Position
               
				AddButton(15, 48, 0x4B9, 0x4BA, 1, GumpButtonType.Reply, 0); // South
                AddHtmlLocalized(38, 46, 340, 20, 1075386, 0x7FFF, false, false); // South

                AddButton(15, 70, 0x4B9, 0x4BA, 2, GumpButtonType.Reply, 0); // East
                AddHtmlLocalized(38, 67, 340, 20, 1075387, 0x7FFF, false, false); // East

                AddButton(10, 300, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(45, 302, 340, 20, 1060051, 0x7FFF, false, false); // CANCEL
			}

            public void AddBlackAlpha(int x, int y, int width, int height)
            {
                AddImageTiled(x, y, width, height, 2624);
                AddAlphaRegion(x, y, width, height);
            }

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Deed.Deleted || info.ButtonID == 0 )
					return;

				m_Deed.m_East = (info.ButtonID != 1);
				m_Deed.SendTarget( sender.Mobile );
			}
		}

		public ECrystalTableDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}