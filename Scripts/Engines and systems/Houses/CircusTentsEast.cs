using System;
using Server;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Commands;

namespace Server.Items
{
	public class MyCircusTentEastAddon : BaseAddon
	{
		public int TentColor1;
		public int TentColor2;

		[CommandProperty(AccessLevel.Owner)]
		public int Tent_Color1 { get { return TentColor1; } set { TentColor1 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Tent_Color2 { get { return TentColor2; } set { TentColor2 = value; InvalidateProperties(); } }

		public override BaseAddonDeed Deed
		{
			get
			{
				return new MyCircusTentEastAddonDeed( Tent_Color1, Tent_Color2, 1 );
			}
		}

		[Constructable]
		public MyCircusTentEastAddon() : this( 0, 0 )
		{
		}

		[ Constructable ]
		public MyCircusTentEastAddon( int RelHue1, int RelHue2 )
		{
			AddComplexComponent( (BaseAddon) this, 1630, 0, 1, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -1, 0, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -1, 1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -1, 2, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -2, 0, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -2, 1, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -2, -1, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -2, 2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1630, -2, 3, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 0, -1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 0, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 1, 0, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 1, -1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 1, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, -1, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 2, -1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 2, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1631, 3, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 0, 3, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 0, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 1, 2, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 1, 3, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 1, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, -1, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 2, 3, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 2, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1632, 3, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 2, 1, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 3, 0, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 3, 1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 3, 2, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 4, 0, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 4, 1, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 4, -1, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 4, 2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1633, 4, 3, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1635, 2, 2, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1635, 3, 3, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1635, 4, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1636, 2, 0, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1636, 3, -1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1636, 4, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1637, 0, 0, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1637, -1, -1, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1637, -2, -2, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1638, 0, 2, 26, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1638, -1, 3, 23, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1638, -2, 4, 20, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 1639, 1, 1, 34, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 872, 4, 4, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 873, 0, 4, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 873, 1, 4, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 873, -1, 4, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 873, 2, 4, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 873, -2, 4, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 873, 3, 4, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 874, 4, -2, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 874, 4, -1, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 874, 4, 2, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 874, 4, 3, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 875, -3, -3, 0, 0, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 876, -3, 4, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 877, 4, -3, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 880, -3, 0, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 880, -3, 1, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 880, -3, -1, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 880, -3, 2, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 880, -3, -2, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 880, -3, 3, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 881, 0, -3, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 881, 1, -3, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 881, -1, -3, 0, RelHue1, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 881, 2, -3, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 881, -2, -3, 0, RelHue2, -1, "tent", 1);
			AddComplexComponent( (BaseAddon) this, 881, 3, -3, 0, RelHue1, -1, "tent", 1);

			TentColor1 = RelHue1;
			TentColor2 = RelHue2;
		}

		public MyCircusTentEastAddon( Serial serial ) : base( serial )
		{
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

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( TentColor1 );
            writer.Write( TentColor2 );
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            TentColor1 = reader.ReadInt();
            TentColor2 = reader.ReadInt();
		}
	}

	public class MyCircusTentEastAddonDeed : BaseAddonDeed
	{
		public int TentColor1;
		public int TentColor2;
		public int TentFound;

		[CommandProperty(AccessLevel.Owner)]
		public int Tent_Color1 { get { return TentColor1; } set { TentColor1 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Tent_Color2 { get { return TentColor2; } set { TentColor2 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Tent_Found { get { return TentFound; } set { TentFound = value; InvalidateProperties(); } }

		public override BaseAddon Addon
		{
			get
			{
				return new MyCircusTentEastAddon( Tent_Color1, Tent_Color2 );
			}
		}

		[Constructable]
		public MyCircusTentEastAddonDeed() : this( 0, 0, 0 )
		{
		}

		[Constructable]
		public MyCircusTentEastAddonDeed( int RelHue1, int RelHue2, int RelRolled )
		{
			Weight = 50;
			ItemID = 0xA58;

			/// COLOR ////////////////
			if ( TentFound > 0 ){ TentColor1 = Tent_Color1; TentColor2 = Tent_Color2; Hue = Tent_Color1; }
			else if ( RelRolled > 0 ){ TentColor1 = RelHue1; TentColor2 = RelHue2; Hue = RelHue1; }
			else
			{
				Hue = Server.Misc.RandomThings.GetRandomColor(0);
				TentColor1 = Hue;
				TentColor2 = Server.Misc.RandomThings.GetRandomColor(0);
			}

			Name = "Circus Tent (East Door)";
			TentFound = 1;
		}

		public class MyCircusTentMenu : ContextMenuEntry 
		{ 
			private MyCircusTentEastAddonDeed i_Tent; 
			private Mobile m_From; 

			public MyCircusTentMenu( Mobile from, MyCircusTentEastAddonDeed tent ) : base( 5109, 1 ) 
			{ 
				m_From = from; 
				i_Tent = tent; 
			} 

			public override void OnClick() 
			{          
				if( i_Tent.IsChildOf( m_From.Backpack ) ) 
				{
					int color1 = i_Tent.TentColor1;
					int color2 = i_Tent.TentColor2;
					i_Tent.TentColor1 = color2;
					i_Tent.TentColor2 = color1;
					i_Tent.Hue = color2;
					m_From.PlaySound( 0x55 );
					m_From.SendMessage( "You swap the primary and secondary colors." );
				} 
				else 
				{
					m_From.SendMessage( "This must be in your backpack to swap colors." );
				} 
			} 
		} 

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
				list.Add( new MyCircusTentMenu( from, this ) );
		}

		public MyCircusTentEastAddonDeed( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			list.Add( 1070722, "Can Be Dyed Different Colors");
			list.Add( 1049644, "Requires a Minimum 9x9 Plot of Land");
        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( TentColor1 );
            writer.Write( TentColor2 );
            writer.Write( TentFound );
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            TentColor1 = reader.ReadInt();
            TentColor2 = reader.ReadInt();
            TentFound = reader.ReadInt();
		}
	}
}