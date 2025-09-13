using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;

namespace Server.Items
{
	[Flipable( 0x1C0E, 0x1C0F )]
	public class Coffer : Container
	{
		public override bool DisplaysContent{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

		public int CofferRobbed;
		[CommandProperty(AccessLevel.Owner)]
		public int Coffer_Robbed { get { return CofferRobbed; } set { CofferRobbed = value; InvalidateProperties(); } }

		public string CofferRobber;
		[CommandProperty(AccessLevel.Owner)]
		public string Coffer_Robber { get { return CofferRobber; } set { CofferRobber = value; InvalidateProperties(); } }

		public int CofferGold;
		[CommandProperty(AccessLevel.Owner)]
		public int Coffer_Gold { get { return CofferGold; } set { CofferGold = value; InvalidateProperties(); } }

		public string CofferType;
		[CommandProperty(AccessLevel.Owner)]
		public string Coffer_Type { get { return CofferType; } set { CofferType = value; InvalidateProperties(); } }

		public Mobile CofferSnooperA;
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Coffer_SnooperA { get{ return CofferSnooperA; } set{ CofferSnooperA = value; InvalidateProperties(); } }

		public Mobile CofferSnooperB;
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Coffer_SnooperB { get{ return CofferSnooperB; } set{ CofferSnooperB = value; InvalidateProperties(); } }

		public Mobile CofferSnooperC;
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Coffer_SnooperC { get{ return CofferSnooperC; } set{ CofferSnooperC = value; InvalidateProperties(); } }

		public Mobile CofferSnooperD;
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Coffer_SnooperD { get{ return CofferSnooperD; } set{ CofferSnooperD = value; InvalidateProperties(); } }

		public Mobile CofferSnooperE;
		[CommandProperty( AccessLevel.Owner )]
		public Mobile Coffer_SnooperE { get{ return CofferSnooperE; } set{ CofferSnooperE = value; InvalidateProperties(); } }

		public string CofferTown;
		[CommandProperty(AccessLevel.Owner)]
		public string Coffer_Town { get { return CofferTown; } set { CofferTown = value; InvalidateProperties(); } }

		[Constructable]
		public Coffer() : base( 0x1C0E )
		{
			Name = "coffer";
			Movable = false;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, CofferType);
        }

		public static void ConfigureAllThiefQuestItems()
		{
			foreach ( Item item in World.Items.Values )
			if ( item is Coffer )
			{
				Coffer coffer = (Coffer)item;
				Server.Items.Coffer.SetupCoffer( coffer );
				if ( coffer.CofferTown == null || coffer.CofferTown == "" ){ Server.Items.Coffer.GetNearbyTown( coffer ); }
			}
			else if ( item is HayCrate )
			{
				HayCrate hay = (HayCrate)item;
				if ( hay.HayTown == null || hay.HayTown == "" ){ Server.Items.HayCrate.GetNearbyTown( hay ); }
			}
			else if ( item is HollowStump )
			{
				HollowStump stump = (HollowStump)item;
				if ( stump.StumpTown == null || stump.StumpTown == "" ){ Server.Items.HollowStump.GetNearbyTown( stump ); }
			}
		}

        public static void SetupCoffer( Coffer coffer )
        {
			if ( (coffer.Name).Contains(" coffer") ){ coffer.CofferType = (coffer.Name).Replace(" coffer", ""); }
			coffer.Name = "coffer";

			if ( coffer.CofferTown == null || coffer.CofferTown == "" ){ Server.Items.Coffer.GetNearbyTown( coffer ); }


			coffer.Hue = 0x6A5;
			if ( Utility.RandomMinMax( 0, 13 ) != 13 ){ coffer.Hue = Server.Misc.RandomThings.GetRandomWoodColor(); }

			int money1 = 1;
			int money2 = 1;

			if (Utility.RandomDouble() > 0.95)
			{
				money1 = 1500;
				money2 = 15000;
			}
			else
			{
				money1 = 50;
				money2 = 1000;				
			}

			double w1 = money1 * (MyServerSettings.GetGoldCutRate( null, coffer ) * .01);
			double w2 = money2 * (MyServerSettings.GetGoldCutRate( null, coffer ) * .01);

			money1 = (int)w1;
			money2 = (int)w2;

			coffer.CofferGold = Utility.RandomMinMax( money1, money2 );
			coffer.CofferRobber = "";
			coffer.CofferRobbed = 0;
			coffer.CofferSnooperA = null;
			coffer.CofferSnooperB = null;
			coffer.CofferSnooperC = null;
			coffer.CofferSnooperD = null;
			coffer.CofferSnooperE = null;
		}

        public static bool GetSnooper( Coffer coffer, Mobile from )
        {
			bool AlreadySnooped = false;

			if ( coffer.CofferSnooperA == from ){ AlreadySnooped = true; }
			else if ( coffer.CofferSnooperB == from ){ AlreadySnooped = true; }
			else if ( coffer.CofferSnooperC == from ){ AlreadySnooped = true; }
			else if ( coffer.CofferSnooperD == from ){ AlreadySnooped = true; }
			else if ( coffer.CofferSnooperE == from ){ AlreadySnooped = true; }

			return AlreadySnooped;
		}

        public static void AssignSnooper( Coffer coffer, Mobile from )
        {
			coffer.CofferSnooperE = coffer.CofferSnooperD;
			coffer.CofferSnooperD = coffer.CofferSnooperC;
			coffer.CofferSnooperC = coffer.CofferSnooperB;
			coffer.CofferSnooperB = coffer.CofferSnooperA;
			coffer.CofferSnooperA = from;
		}

		public static void GetNearbyTown( Coffer coffer )
		{
			foreach ( Mobile citizen in coffer.GetMobilesInRange( 20 ) )
			{
				if ( citizen is BaseVendor )
				{
					coffer.CofferTown = Server.Misc.Worlds.GetRegionName( citizen.Map, citizen.Location );
				}
			}
		}

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
			from.SendMessage("This isn't your coffer!");
			return false;
        }

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
        {
			from.SendMessage("This isn't your coffer!");
			return false;
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( Server.Items.Coffer.GetSnooper( this, from ) )
			{
				if ( CofferGold > 0 )
				{
					from.SendMessage("Use your stealing skill on the coffer if you want the " + CofferGold + " gold.");
				}
				else
				{
					from.SendMessage("There is no gold in the coffer.");
				}
			}
			else if ( from.CheckSkill( SkillName.Snooping, 10, 125 ) && from.InRange(this, 3) )
			{
				Server.Items.Coffer.AssignSnooper( this, from );
				if ( CofferGold > 0 )
				{
					from.SendMessage("You can see about " + CofferGold + " gold in the coffer.");
				}
				else
				{
					from.SendMessage("There is no gold in the coffer.");
				}
			}
			else if ( from.Skills[SkillName.Snooping].Base >= 10 )
			{
				from.SendMessage("You can't seem to get a peek inside the coffer.");
				if ( from.Skills[SkillName.Hiding].Value / 2 < Utility.Random( 100 ) )
					from.RevealingAction();
			}
			else
			{
				from.SendMessage("You should probably get better at snooping first.");
			}
		}

		public Coffer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( CofferRobbed );
            writer.Write( CofferRobber );
            writer.Write( CofferGold );
            writer.Write( CofferType );
            writer.Write( CofferSnooperA );
            writer.Write( CofferSnooperB );
            writer.Write( CofferSnooperC );
            writer.Write( CofferSnooperD );
            writer.Write( CofferSnooperE );
            writer.Write( CofferTown );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            CofferRobbed = reader.ReadInt();
            CofferRobber = reader.ReadString();
            CofferGold = reader.ReadInt();
			CofferType = reader.ReadString();
			CofferSnooperA = reader.ReadMobile();
			CofferSnooperB = reader.ReadMobile();
			CofferSnooperC = reader.ReadMobile();
			CofferSnooperD = reader.ReadMobile();
			CofferSnooperE = reader.ReadMobile();
			CofferTown = reader.ReadString();
		} 
	}
}