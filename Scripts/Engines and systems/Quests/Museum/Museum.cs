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
	public class Museums : Item
	{
		public string DiscoverName;
		[CommandProperty(AccessLevel.Owner)]
		public string Discover_Name { get { return DiscoverName; } set { DiscoverName = value; InvalidateProperties(); } }

		public Mobile DiscoverOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Discover_Owner { get{ return DiscoverOwner; } set{ DiscoverOwner = value; } }

		public string ThisDescription;
		[CommandProperty(AccessLevel.Owner)]
		public string This_Description { get { return ThisDescription; } set { ThisDescription = value; InvalidateProperties(); } }

		public int ThisValue;
		[CommandProperty(AccessLevel.Owner)]
		public int This_Value { get { return ThisValue; } set { ThisValue = value; InvalidateProperties(); } }

		[Constructable]
		public Museums( ) : base( 0x1444 )
		{
			Weight = 10.0;
			Name = "antique";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, ThisDescription );
			if ( DiscoverOwner.Name == null ){ list.Add( 1070722, "Discovered by " + DiscoverName ); } else { list.Add( 1070722, "Discovered by " + DiscoverOwner.Name ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				int cost = Museums.AntiqueTotalValue( ThisValue, from, false );
				from.SendMessage( "This antique is worth " + cost + " gold to an art collector." );
			}
			else
			{
				if ( ItemID == 0x52FC ){ ItemID = 0x0481; Light = LightType.Circle300; from.SendSound( 0x4A ); }
				else if ( ItemID == 0x0481 ){ ItemID = 0x52FC; Light = LightType.Empty; from.SendSound( 0x4A ); }
				else if ( ItemID == 0x5354 ){ ItemID = 0x5351; Light = LightType.Circle300; from.SendSound( 0x47 ); }
				else if ( ItemID == 0x5351 ){ ItemID = 0x5354; Light = LightType.Empty; from.SendSound( 0x3be ); }
				else if ( ItemID == 0x5355 ){ ItemID = 0x5356; Light = LightType.Circle150; from.SendSound( 0x47 ); }
				else if ( ItemID == 0x5356 ){ ItemID = 0x5355; Light = LightType.Empty; from.SendSound( 0x3be ); }
				else if ( ItemID == 0x535C ){ ItemID = 0x535D; Light = LightType.Circle150; from.SendSound( 0x47 ); }
				else if ( ItemID == 0x535D ){ ItemID = 0x535C; Light = LightType.Empty; from.SendSound( 0x3be ); }
				else if ( ItemID == 0x5363 ){ ItemID = 0x5364; Light = LightType.Circle150; from.SendSound( 0x47 ); }
				else if ( ItemID == 0x5364 ){ ItemID = 0x5363; Light = LightType.Empty; from.SendSound( 0x3be ); }
				else if ( ItemID == 0x5367 ){ ItemID = 0x5368; Light = LightType.Circle150; from.SendSound( 0x47 ); }
				else if ( ItemID == 0x5368 ){ ItemID = 0x5367; Light = LightType.Empty; from.SendSound( 0x3be ); }
				else if ( ItemID == 0x5320 ){ ItemID = 0x5321; Light = LightType.Circle300; from.SendSound( 0x208 ); }
				else if ( ItemID == 0x5321 ){ ItemID = 0x5320; Light = LightType.Empty; from.SendSound( 0x3be ); }
				else if ( ItemID == 0x539F ){ ItemID = 0x53A0; Light = LightType.Circle300; from.SendSound( 0x208 ); }
				else if ( ItemID == 0x53A0 ){ ItemID = 0x539F; Light = LightType.Empty; from.SendSound( 0x3be ); }
			}
		}

		public Museums(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
            writer.Write( DiscoverName );
			writer.Write( (Mobile)DiscoverOwner );
            writer.Write( ThisDescription );
            writer.Write( ThisValue );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
            DiscoverName = reader.ReadString();
			DiscoverOwner = reader.ReadMobile();
            ThisDescription = reader.ReadString();
            ThisValue = reader.ReadInt();
		}

		public static int AntiqueMerchantGold( int cost, Mobile player ){ return (int)((cost*(player.Skills[SkillName.ItemID].Value * 0.01)/4)); }
		public static int AntiqueBeggingGold( int cost, Mobile player ){ if ( BaseVendor.BeggingPose( player ) > 0 ){ return (int)((cost*(player.Skills[SkillName.Begging].Value * 0.01)/4)); } return 0; }
		public static int AntiqueGuildGold( int cost, Mobile player ){ PlayerMobile pm = (PlayerMobile)player; if ( pm.NpcGuild == NpcGuild.MerchantsGuild ){ return (int)(cost*(25 * 0.01)); } return 0; }

		public static int AntiqueTotalValue( int cost, Mobile player, bool karma )
		{
			int gold = cost;
				gold = gold + AntiqueMerchantGold( cost, player );
				gold = gold + AntiqueBeggingGold( cost, player );
				gold = gold + AntiqueGuildGold( cost, player );

			if ( BaseVendor.BeggingPose( player ) > 0 && karma ){ Titles.AwardKarma( player, -BaseVendor.BeggingKarma( player ), false ); }

			return gold;
		}

		public static void GiveAntique( Museums antique, Mobile vendor, Mobile player )
		{
			string say = "Thank you!";
			int cost = Museums.AntiqueTotalValue( antique.ThisValue, player, true );

			switch ( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0:	say = "I have been looking for something like this. Here is " + cost.ToString() + " gold for you.";		break;
				case 1:	say = "I have heard of this item before. Here is " + cost.ToString() + " gold for you.";		break;
				case 2:	say = "I never thought I would see one of these. Here is " + cost.ToString() + " gold for you.";		break;
				case 3:	say = "I have never seen one of these. Here is " + cost.ToString() + " gold for you.";		break;
				case 4:	say = "What a rare item. Here is " + cost.ToString() + " gold for you.";		break;
				case 5:	say = "This is quite rare. Here is " + cost.ToString() + " gold for you.";		break;
				case 6:	say = "This will go nicely in my museum. Here is " + cost.ToString() + " gold for you.";		break;
				case 7:	say = "I have only heard tales about such items. Here is " + cost.ToString() + " gold for you.";		break;
				case 8:	say = "How did you come across this? Here is " + cost.ToString() + " gold for you.";		break;
				case 9:	say = "Where did you find this? Here is " + cost.ToString() + " gold for you.";		break;
			}

			player.SendSound( 0x2E6 );
			player.SendMessage( "You receive " + cost + " gold." );
			player.AddToBackpack ( new Gold( cost ) );
			antique.Delete();
			vendor.PrivateOverheadMessage(MessageType.Regular, 1153, false, say, player.NetState);
		}
	}
}