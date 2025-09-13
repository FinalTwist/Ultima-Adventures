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
	public class BoatBuild : Item
	{
		[Constructable]
		public BoatBuild() : base( 0x14F1 )
		{
			Weight = 2.0;
			Name = "Schematics of a Small Ship";
			ItemID = Utility.RandomList( 0x14F1, 0x14F2 );

			if ( Weight > 1.0 )
			{
				Weight = 1.0;
				HaveWood = 0;
				HaveCloth = 0;
				HaveIngots = 0;
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			int needWood = 1500 - HaveWood;
				if ( needWood < 0 ){ needWood = 0; }
			int needIngot = 700 - HaveIngots;
				if ( needIngot < 0 ){ needIngot = 0; }
			int needCloth = 300 - HaveCloth;
				if ( needCloth < 0 ){ needCloth = 0; }

			int boatDone = needWood + needIngot + needCloth;

			if ( boatDone > 0 )
			{
				list.Add( 1070722, "Drop The Materials Needed On This Parchment" );
				list.Add( 1049644, "Need " + needWood.ToString() + " Wood, " + needCloth.ToString() + " Cloth, " + needIngot.ToString() + " Ingots" );
			}
			else
			{
				list.Add( 1070722, "Double Click To Build" );
			}
        }

		public override void OnDoubleClick( Mobile from )
		{
			int needWood = 1500 - HaveWood;
			int needIngot = 700 - HaveIngots;
			int needCloth = 300 - HaveCloth;

			int boatDone = needWood + needIngot + needCloth;

			if ( boatDone > 0 )
			{
				from.SendMessage( "You need to gather more materials before you can build this!" );
			}
			else
			{
				int builder = 0;

				foreach ( Mobile m in this.GetMobilesInRange( 20 ) )
				{
					if ( m is Shipwright )
						++builder;
				}

				if ( builder < 1 )
				{
					from.SendMessage( "You need to be near a shipwright to build that!" );
				}
				else
				{
					from.SendMessage( "You build yourself a small ship." );
					from.PlaySound( 0x23D );
					from.AddToBackpack ( new Multis.SmallBoatDeed() );
					this.Delete();
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
			Container pack = from.Backpack;
			int iAmount = 0;
			string sEnd = ".";

			int needWood = 1500 - HaveWood;
			int needIngot = 700 - HaveIngots;
			int needCloth = 300 - HaveCloth;

			if ( from != null )
			{
				iAmount = dropped.Amount;

				if ( dropped is BaseIngot && needIngot > 0 )
				{
					if ( iAmount > 1 ){ sEnd = "s."; }

					HaveIngots = HaveIngots + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " ingot" + sEnd );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
				else if ( ( dropped is BaseWoodBoard || dropped is BaseLog ) && needWood > 0 )
				{
					HaveWood = HaveWood + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " wood." );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
				else if ( ( dropped is UncutCloth || dropped is Cloth ) && needCloth > 0 )
				{
					HaveCloth = HaveCloth + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " cloth." );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
				else if ( dropped is BoltOfCloth && needCloth > 0 )
				{
					HaveCloth = HaveCloth + ( iAmount * 50 );
					from.SendMessage( "You added " + iAmount.ToString() + " cloth" );
					dropped.Delete();
					this.InvalidateProperties();
					return true;
				}
			}

			return false;
		}

		public BoatBuild( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version

			writer.Write( HaveWood );
			writer.Write( HaveCloth );
			writer.Write( HaveIngots );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			HaveWood = reader.ReadInt();
			HaveCloth = reader.ReadInt();
			HaveIngots = reader.ReadInt();

			Name = "Schematics of a Small Ship";
			if ( ItemID != 0x14F1 && ItemID != 0x14F2 ){ ItemID = Utility.RandomList( 0x14F1, 0x14F2 ); }
		}

		public int HaveWood;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveWood { get{ return HaveWood; } set{ HaveWood = value; } }

		public int HaveIngots;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveIngots { get{ return HaveIngots; } set{ HaveIngots = value; } }

		public int HaveCloth;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveCloth { get{ return HaveCloth; } set{ HaveCloth = value; } }
	}
}