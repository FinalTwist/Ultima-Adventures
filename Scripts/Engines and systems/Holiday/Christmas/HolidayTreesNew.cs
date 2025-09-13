using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;

namespace Server.Items
{
	public class HolidayTreeNew1Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeNew1Deed(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {19561, -1, 1, 0}, {147, 0, -1, 0}, {147, 0, -1, 0}// 1	2	3	
			, {19562, 0, 0, 0}, {19563, 1, -1, 0}, {19564, 2, -2, 0}// 4	5	6
		};

		[ Constructable ]
		public HolidayTreeNew1Addon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public HolidayTreeNew1Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HolidayTreeNew2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeNew2Deed(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {19553, -1, 2, 0}, {147, 0, 0, 0}, {19554, 0, 1, 0}// 1	2	4	
			, {19555, 1, 0, 0}, {19556, 2, -1, 0}// 5	6	
		};

		[ Constructable ]
		public HolidayTreeNew2Addon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 35, 0, 29, "", 1);// 3
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public HolidayTreeNew2Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HolidayTreeNew3Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeNew3Deed(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {19557, -1, 2, 0}, {147, 0, 0, 0}, {19558, 0, 1, 0}// 1	2	4	
			, {19559, 1, 0, 0}, {19560, 2, -1, 0}// 5	6	
		};

		[ Constructable ]
		public HolidayTreeNew3Addon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 35, 0, 29, "", 1);// 3
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public HolidayTreeNew3Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HolidayTreeNew4Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeNew4Deed(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {147, -1, -1, 0}, {19568, 1, -1, 0}, {19569, 2, -2, 0}// 1	2	3	
			, {19565, -2, 2, 0}, {19566, -1, 1, 0}, {19567, 0, 0, 0}// 4	5	6	
					};

		[ Constructable ]
		public HolidayTreeNew4Addon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
		}

		public HolidayTreeNew4Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HolidayTreeNew5Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeNew5Deed(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {19575, -2, 3, 0}, {19576, -1, 2, 0}, {147, 0, 0, 0}// 1	2	3	
			, {19577, 0, 1, 0}, {19578, 1, 0, 0}, {19579, 2, -1, 0}// 5	6	7	
			, {19580, 3, -2, 0}// 8	
		};

		[ Constructable ]
		public HolidayTreeNew5Addon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 60, 0, 29, "", 1);// 4
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public HolidayTreeNew5Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HolidayTreeNew6Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HolidayTreeNew6Deed(); } }

        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {19571, -1, 1, 0}, {327, 0, -1, 0}, {19572, 0, 0, 0}// 2	3	5	
			, {19573, 1, -1, 0}, {19574, 2, -2, 0}, {19570, -2, 2, 0}// 6	7	8	
					};

		[ Constructable ]
		public HolidayTreeNew6Addon()
		{
            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );

			AddComplexComponent( (BaseAddon) this, 5703, -1, 0, 57, 0, 29, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 5703, 0, -1, 57, 0, 29, "", 1);// 4
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public HolidayTreeNew6Addon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}





	public class HolidayTreeNew1Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeNew1Addon(); } }

		[Constructable]
		public HolidayTreeNew1Deed()
		{
			Name = "grand holiday tree";
			ItemID = 0x4C7D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeNew1Deed( Serial serial ) : base( serial )
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
	public class HolidayTreeNew2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeNew2Addon(); } }

		[Constructable]
		public HolidayTreeNew2Deed()
		{
			Name = "grand holiday tree";
			ItemID = 0x4C7D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeNew2Deed( Serial serial ) : base( serial )
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
	public class HolidayTreeNew3Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeNew3Addon(); } }

		[Constructable]
		public HolidayTreeNew3Deed()
		{
			Name = "grand holiday tree";
			ItemID = 0x4C7D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeNew3Deed( Serial serial ) : base( serial )
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
	public class HolidayTreeNew4Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeNew4Addon(); } }

		[Constructable]
		public HolidayTreeNew4Deed()
		{
			Name = "grand holiday tree";
			ItemID = 0x4C7D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeNew4Deed( Serial serial ) : base( serial )
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
	public class HolidayTreeNew5Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeNew5Addon(); } }

		[Constructable]
		public HolidayTreeNew5Deed()
		{
			Name = "grand holiday tree";
			ItemID = 0x4C7D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeNew5Deed( Serial serial ) : base( serial )
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
	public class HolidayTreeNew6Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HolidayTreeNew6Addon(); } }

		[Constructable]
		public HolidayTreeNew6Deed()
		{
			Name = "grand holiday tree";
			ItemID = 0x4C7D;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public HolidayTreeNew6Deed( Serial serial ) : base( serial )
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







	public class NewHolidayTree : Item
	{
		[Constructable]
		public NewHolidayTree() : base( 0x4C7D )
		{
			Weight = 1.0;
			Name = "grand holiday tree";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.CloseGump( typeof( NewHolidayTreeGump ) );
			from.SendGump( new NewHolidayTreeGump( this ) );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Select A Tree");
        }

		public NewHolidayTree( Serial serial ) : base( serial )
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

		public class NewHolidayTreeGump : Gump
		{
			private Item m_Tree;

			public NewHolidayTreeGump( Item tree ): base( 25, 25 )
			{
				m_Tree = tree; 

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 152);
				AddImage(300, 0, 152);
				AddImage(600, 0, 152);
				AddImage(0, 300, 152);
				AddImage(300, 300, 152);
				AddImage(600, 300, 152);
				AddImage(0, 420, 152);
				AddImage(300, 420, 152);
				AddImage(600, 420, 152);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 279, 129);
				AddImage(302, 285, 129);
				AddImage(598, 285, 129);
				AddImage(2, 418, 129);
				AddImage(302, 418, 129);
				AddImage(598, 418, 129);
				AddImage(53, 70, 11425);
				AddButton(142, 297, 4005, 4005, 1, GumpButtonType.Reply, 0);
				AddButton(408, 297, 4005, 4005, 2, GumpButtonType.Reply, 0);
				AddButton(706, 296, 4005, 4005, 3, GumpButtonType.Reply, 0);
				AddButton(144, 683, 4005, 4005, 4, GumpButtonType.Reply, 0);
				AddButton(418, 680, 4005, 4005, 5, GumpButtonType.Reply, 0);
				AddButton(713, 676, 4005, 4005, 6, GumpButtonType.Reply, 0);
				AddHtml( 12, 14, 872, 58, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Choose the type of holiday tree you want. You can only make this choice once, and be aware that these trees are quite tall and will appear through house floors so they are best on roofs or on deck areas. Actual sizes are shown.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 

				if ( info.ButtonID == 1 ){ 			from.AddToBackpack( new HolidayTreeNew1Deed() );			m_Tree.Delete(); }
				else if ( info.ButtonID == 2 ){		from.AddToBackpack( new HolidayTreeNew2Deed() );			m_Tree.Delete(); }
				else if ( info.ButtonID == 3 ){		from.AddToBackpack( new HolidayTreeNew3Deed() );			m_Tree.Delete(); }
				else if ( info.ButtonID == 4 ){		from.AddToBackpack( new HolidayTreeNew4Deed() );			m_Tree.Delete(); }
				else if ( info.ButtonID == 5 ){		from.AddToBackpack( new HolidayTreeNew5Deed() );			m_Tree.Delete(); }
				else if ( info.ButtonID == 6 ){		from.AddToBackpack( new HolidayTreeNew6Deed() );			m_Tree.Delete(); }
			}
		}
	}
}