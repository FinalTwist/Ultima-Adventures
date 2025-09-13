using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class BaneBase : BaseAddon
	{
		public int ItemType;

		[CommandProperty(AccessLevel.Owner)]
		public int Item_Type { get { return ItemType; } set { ItemType = value; InvalidateProperties(); } }

		[ Constructable ]
		public BaneBase( string serpent )
		{
			Light = LightType.Circle150;
			string sPed = "an ornately ";
			switch( Utility.RandomMinMax( 0, 10 ) )
			{
				case 0: sPed = "an ornately ";		break;
				case 1: sPed = "a beautifully ";	break;
				case 2: sPed = "an expertly ";		break;
				case 3: sPed = "an artistically ";	break;
				case 4: sPed = "an exquisitely ";	break;
				case 5: sPed = "a decoratively ";	break;
				case 6: sPed = "an ancient ";		break;
				case 7: sPed = "an old ";			break;
				case 8: sPed = "an unusually ";		break;
				case 9: sPed = "a curiously ";		break;
				case 10: sPed = "an oddly ";		break;
			}
			sPed = sPed + "carved pedestal";

			int iThing = 0x573A;
			string sThing = "Scales of Ethicality";
			int iColor = 0x4AB;
			int z = 6;
			ItemType = 1;
			if ( serpent == "logic" ){ iThing = 0xE2E; iColor = 0x430; sThing = "Orb of Logic"; z = 6; ItemType = 2; }
			else if ( serpent == "discipline" ){ iThing = 0x4101; iColor = 0; sThing = "Lantern of Discipline"; z = 6; ItemType = 3; }

			AddComplexComponent( (BaseAddon) this, iThing, 0, 0, z, iColor, 29, sThing, 1);
			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 0, 0, 29, sPed, 1);
			AddComplexComponent( (BaseAddon) this, 13042, 0, 0, 0, 0, -1, "", 1);
		}

		public BaneBase( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } }
		private DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextTalk && Utility.InRange( m.Location, this.Location, 2 ) )
				{
					m.PrivateOverheadMessage(MessageType.Regular, 1150, false, "I could use that item on the pedestal to take it.", m.NetState);
					m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
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

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.SendMessage( "You will have to get closer to take that." );
			}
			else 
			{
				string serpentType = "";

				if ( ItemType == 1 )
				{
					serpentType = "Scales of Ethicality";
						ArrayList books = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is ScalesOfEthicality )
						{
							books.Add( item );
						}
						for ( int i = 0; i < books.Count; ++i )
						{
							Item item = ( Item )books[ i ];
							item.Delete();
						}
					from.AddToBackpack( new ScalesOfEthicality() );
				}
				else if ( ItemType == 2 )
				{
					serpentType = "Orb of Logic";
						ArrayList bells = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is OrbOfLogic )
						{
							bells.Add( item );
						}
						for ( int i = 0; i < bells.Count; ++i )
						{
							Item item = ( Item )bells[ i ];
							item.Delete();
						}
					from.AddToBackpack( new OrbOfLogic() );
				}
				else
				{
					serpentType = "Lantern of Discipline";
						ArrayList candles = new ArrayList();
						foreach ( Item item in World.Items.Values )
						if ( item is LanternOfDiscipline )
						{
							candles.Add( item );
						}
						for ( int i = 0; i < candles.Count; ++i )
						{
							Item item = ( Item )candles[ i ];
							item.Delete();
						}
					from.AddToBackpack( new LanternOfDiscipline() );
				}
				BaneBaseEmpty Pedul = new BaneBaseEmpty();
				Pedul.ItemType = ItemType;
				Pedul.MoveToWorld (new Point3D(this.X, this.Y, this.Z), this.Map);
				from.SendMessage( "You have found the " + serpentType + "!" );
				from.SendSound( 0x3D );
				LoggingFunctions.LogGeneric( from, "has found the " + serpentType + "." );
				this.Delete();
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( ItemType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ItemType = reader.ReadInt();
		}
	}
}