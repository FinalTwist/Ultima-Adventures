using System;
using Server;

namespace Server.Items
{
	public class DDRelicLight1 : BaseLight
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		public override int LitItemID{ get { return 0x40BE; } }
		public override int UnlitItemID{ get { return 0x4039; } }

		[Constructable]
		public DDRelicLight1() : base( 0x4039 )
		{
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;

			string sLook = "a rare";
			switch ( Utility.RandomMinMax( 0, 18 ) )
			{
				case 0:	sLook = "a rare";	break;
				case 1:	sLook = "a nice";	break;
				case 2:	sLook = "a pretty";	break;
				case 3:	sLook = "a superb";	break;
				case 4:	sLook = "a delightful";	break;
				case 5:	sLook = "an elegant";	break;
				case 6:	sLook = "an exquisite";	break;
				case 7:	sLook = "a fine";	break;
				case 8:	sLook = "a gorgeous";	break;
				case 9:	sLook = "a lovely";	break;
				case 10:sLook = "a magnificent";	break;
				case 11:sLook = "a marvelous";	break;
				case 12:sLook = "a splendid";	break;
				case 13:sLook = "a wonderful";	break;
				case 14:sLook = "an extraordinary";	break;
				case 15:sLook = "a strange";	break;
				case 16:sLook = "an odd";	break;
				case 17:sLook = "a unique";	break;
				case 18:sLook = "an unusual";	break;
			}

			string sDecon = "decorative";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sDecon = ", decorative";		break;
				case 1:	sDecon = ", ornamental";		break;
				case 2:	sDecon = "";		break;
				case 3:	sDecon = "";		break;
			}

			Name = sLook + sDecon + " candelabra";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicLight1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( RelicGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DDRelicLight2 : BaseLight
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		public override int LitItemID{ get { return 0xB1D; } }
		public override int UnlitItemID{ get { return 0xA27; } }

		[Constructable]
		public DDRelicLight2() : base( 0xA27 )
		{
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;

			string sLook = "a rare";
			switch ( Utility.RandomMinMax( 0, 18 ) )
			{
				case 0:	sLook = "a rare";	break;
				case 1:	sLook = "a nice";	break;
				case 2:	sLook = "a pretty";	break;
				case 3:	sLook = "a superb";	break;
				case 4:	sLook = "a delightful";	break;
				case 5:	sLook = "an elegant";	break;
				case 6:	sLook = "an exquisite";	break;
				case 7:	sLook = "a fine";	break;
				case 8:	sLook = "a gorgeous";	break;
				case 9:	sLook = "a lovely";	break;
				case 10:sLook = "a magnificent";	break;
				case 11:sLook = "a marvelous";	break;
				case 12:sLook = "a splendid";	break;
				case 13:sLook = "a wonderful";	break;
				case 14:sLook = "an extraordinary";	break;
				case 15:sLook = "a strange";	break;
				case 16:sLook = "an odd";	break;
				case 17:sLook = "a unique";	break;
				case 18:sLook = "an unusual";	break;
			}

			string sDecon = "decorative";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sDecon = ", decorative";		break;
				case 1:	sDecon = ", ornamental";		break;
				case 2:	sDecon = "";		break;
				case 3:	sDecon = "";		break;
			}

			Name = sLook + sDecon + " candelabra";
		}

		public DDRelicLight2( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( RelicGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
	public class DDRelicLight3 : BaseLight
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		public override int LitItemID{ get { return 0xB26; } }
		public override int UnlitItemID{ get { return 0xA29; } }

		[Constructable]
		public DDRelicLight3() : base( 0xA29 )
		{
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;

			string sLook = "a rare";
			switch ( Utility.RandomMinMax( 0, 18 ) )
			{
				case 0:	sLook = "a rare";	break;
				case 1:	sLook = "a nice";	break;
				case 2:	sLook = "a pretty";	break;
				case 3:	sLook = "a superb";	break;
				case 4:	sLook = "a delightful";	break;
				case 5:	sLook = "an elegant";	break;
				case 6:	sLook = "an exquisite";	break;
				case 7:	sLook = "a fine";	break;
				case 8:	sLook = "a gorgeous";	break;
				case 9:	sLook = "a lovely";	break;
				case 10:sLook = "a magnificent";	break;
				case 11:sLook = "a marvelous";	break;
				case 12:sLook = "a splendid";	break;
				case 13:sLook = "a wonderful";	break;
				case 14:sLook = "an extraordinary";	break;
				case 15:sLook = "a strange";	break;
				case 16:sLook = "an odd";	break;
				case 17:sLook = "a unique";	break;
				case 18:sLook = "an unusual";	break;
			}

			string sDecon = "decorative";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sDecon = ", decorative";		break;
				case 1:	sDecon = ", ornamental";		break;
				case 2:	sDecon = "";		break;
				case 3:	sDecon = "";		break;
			}

			Name = sLook + sDecon + " candelabra";
		}

		public DDRelicLight3( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( RelicGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
		}
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////
}