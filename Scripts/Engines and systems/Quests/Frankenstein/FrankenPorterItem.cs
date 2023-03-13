using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Gumps;
using Server.Regions;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;
using Server.Accounting;

namespace Server.Items
{
	public class FrankenPorterItem : Item
	{
		public int PorterSerial;
		public int PorterOwner;
		public int PorterLevel;
		public int PorterType;
		public string PorterName;
		public int m_Charges;

		[CommandProperty(AccessLevel.Owner)]
		public int Porter_Serial{ get { return PorterSerial; } set { PorterSerial = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Porter_Owner{ get { return PorterOwner; } set { PorterOwner = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Porter_Level{ get { return PorterLevel; } set { PorterLevel = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Porter_Type{ get { return PorterType; } set { PorterType = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Porter_Name { get { return PorterName; } set { PorterName = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get{ return m_Charges; }
			set{ m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public FrankenPorterItem() : base( 0x2624 )
		{
			Name = "a reanimation";
			Weight = 1.0;
			PorterSerial = 0;
			Charges = 5;
			ItemID = 0x2624;
		}

		public FrankenPorterItem( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			ArrayList pets = new ArrayList();

			foreach ( Mobile m in World.Mobiles.Values )
			{
				if ( m is FrankenPorter && PorterType == 0 )
				{
					BaseCreature bc = (BaseCreature)m;
					if ( bc.Controlled && bc.ControlMaster == from )
						pets.Add( bc );
				}
				else if ( m is FrankenFighter && PorterType == 1 )
				{
					BaseCreature bc = (BaseCreature)m;
					if ( bc.Controlled && bc.ControlMaster == from )
						pets.Add( bc );
				}
			}

			int nFollowers = from.Followers;

			if (!IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001);
			}
			else if ( pets.Count > 0 )
			{
				from.SendMessage("You already have a reanimation.");
			}
			else if ( nFollowers > 0 )
			{
				from.SendMessage("You already have too many in your group.");
			}
			else if ( Charges == 0 )
			{
				from.SendMessage("Your reanimation needs more embalming fluid.");
			}
			else if ( PorterOwner != from.Serial )
			{
				from.SendMessage("This is not your reanimation!");
			}
			else
			{
				Map map = from.Map;
				ConsumeCharge( from );
				this.InvalidateProperties();

				BaseCreature friend = new FrankenPorter();
				((FrankenPorter)friend).PorterLevel = this.PorterLevel;

				if ( this.PorterType > 0 ){ friend.Delete(); friend = new FrankenFighter(); ((FrankenFighter)friend).FighterLevel = this.PorterLevel; }

				bool validLocation = false;
				Point3D loc = from.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				friend.ControlMaster = from;
				friend.Controlled = true;
				friend.ControlOrder = OrderType.Come;
				friend.ControlSlots = 5;
				friend.Loyalty = 100;
				friend.Summoned = true;
				friend.SummonMaster = from;

				if ( PorterName != null ){ friend.Name = PorterName; } else { friend.Name = "a reanimation"; }

				from.PlaySound( 0x665 );
				friend.MoveToWorld( loc, map );
				friend.OnAfterSpawn();
				this.LootType = LootType.Blessed;
				this.Visible = false;
				this.PorterSerial = friend.Serial;
			}
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;
		}

		private static string GetOwner( int serial )
		{
			string sOwner = null;

			foreach ( Mobile owner in World.Mobiles.Values )
			if ( owner.Serial == serial )
			{
				sOwner = owner.Name;
			}

			return sOwner;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060584, m_Charges.ToString() );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			string sType = "a reanimation";
			if ( PorterName != "a reanimation" ){ sType = PorterName + " the reanimation"; }

			string sInfo = sType + " - Level " + PorterLevel + "";

			if ( PorterType > 0 ){ sInfo = sInfo + " Protector"; } else { sInfo = sInfo + " Slave"; }

			list.Add( 1070722, sInfo );

			string sOwner = GetOwner( PorterOwner );
			if ( sOwner == null ){ this.Delete(); }
			list.Add( 1049644, "Belongs To " + sOwner + ""); // PARENTHESIS
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( PorterSerial );
            writer.Write( PorterOwner );
            writer.Write( PorterType );
            writer.Write( PorterLevel );
            writer.Write( PorterName );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			PorterSerial = reader.ReadInt();
			PorterOwner = reader.ReadInt();
			PorterType = reader.ReadInt();
			PorterLevel = reader.ReadInt();
			PorterName = reader.ReadString();
			switch ( version )
			{
				case 0:
				{
					m_Charges = (int)reader.ReadInt();

					break;
				}
			}
			LootType = LootType.Regular;
			Visible = true;
		}
	}
}