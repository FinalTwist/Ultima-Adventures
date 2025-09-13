using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using System.Collections;
using System.Globalization;

namespace Server.Items
{
	public class MedicalRecord : Item
	{
		[Constructable]
		public MedicalRecord( ) : base( 0x27FB )
		{
			Name = "Medical Record";
			Weight = 1.0;
			Light = LightType.Circle150;
			ItemID = Utility.RandomList( 0x27FB, 0x27FC );
			Hue = Utility.RandomList( 0x859, 0x85B, 0x85C, 0x85E, 0x85F, 0x860, 0x861, 0x862, 0x863, 0x864, 0x865, 0x866, 0x867, 0x86C, 0x86D, 0x871, 0x873 );
			SetupMedicalRecord( this );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "For " + DataPatient + "" );
		}

		public class MedicalRecordGump : Gump
		{
			public MedicalRecordGump( Mobile from, MedicalRecord Data ): base( 100, 100 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 1242);
				AddHtml( 47, 85, 308, 180, @"<BODY><BASEFONT Color=#00FF06>" + SetupMedicalRecord( Data ) + "</BASEFONT></BODY>", (bool)false, (bool)true);
				AddHtml( 47, 31, 308, 20, @"<BODY><BASEFONT Color=#00FF06>Medical Record for " + Data.DataPatient + "</BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 47, 56, 308, 20, @"<BODY><BASEFONT Color=#00FF06>By Dr. Thomas Witman</BASEFONT></BODY>", (bool)false, (bool)false);
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile;
				from.SendSound( 0x54D );
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			e.CloseGump( typeof( MedicalRecordGump ) );
			e.SendGump( new MedicalRecordGump( e, this ) );
			e.SendSound( 0x54D );
		}

		public MedicalRecord(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.Write( DataPatient );
			writer.Write( DataPlanet );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			DataPatient = reader.ReadString();
			DataPlanet = reader.ReadString();
		}

		public string DataPatient;
		[CommandProperty(AccessLevel.Owner)]
		public string Data_Patient { get { return DataPatient; } set { DataPatient = value; InvalidateProperties(); } }

		public string DataPlanet;
		[CommandProperty(AccessLevel.Owner)]
		public string Data_Planet { get { return DataPlanet; } set { DataPlanet = value; InvalidateProperties(); } }

		public static string SetupMedicalRecord( MedicalRecord pad )
		{
			if ( pad.DataPlanet == null || pad.DataPlanet == "" )
			{
				switch ( Utility.RandomMinMax( 0, 3 ) )
				{
					case 0:	pad.DataPlanet = NameList.RandomName( "dark_elf_prefix_female" ) + NameList.RandomName( "dark_elf_suffix_female" );	break;
					case 1:	pad.DataPlanet = NameList.RandomName( "dark_elf_prefix_female" ) + NameList.RandomName( "dark_elf_suffix_male" );	break;
					case 2:	pad.DataPlanet = NameList.RandomName( "dark_elf_prefix_male" ) + NameList.RandomName( "dark_elf_suffix_female" );	break;
					case 3:	pad.DataPlanet = NameList.RandomName( "dark_elf_prefix_male" ) + NameList.RandomName( "dark_elf_suffix_male" );		break;
				}

				switch ( Utility.RandomMinMax( 0, 12 ) )
				{
					case 0:		pad.DataPlanet = pad.DataPlanet + " I";		break;
					case 1:		pad.DataPlanet = pad.DataPlanet + " II";	break;
					case 2:		pad.DataPlanet = pad.DataPlanet + " III";	break;
					case 3:		pad.DataPlanet = pad.DataPlanet + " IV";	break;
					case 4:		pad.DataPlanet = pad.DataPlanet + " V";		break;
					case 5:		pad.DataPlanet = pad.DataPlanet + " VI";	break;
					case 6:		pad.DataPlanet = pad.DataPlanet + " VII";	break;
					case 7:		pad.DataPlanet = pad.DataPlanet + " VIII";	break;
					case 8:		pad.DataPlanet = pad.DataPlanet + " IX";	break;
					case 9:		pad.DataPlanet = pad.DataPlanet + " X";		break;
					case 10:	pad.DataPlanet = pad.DataPlanet + " XI";	break;
					case 11:	pad.DataPlanet = pad.DataPlanet + " XII";	break;
					case 12:	pad.DataPlanet = pad.DataPlanet + " XIII";	break;
				}
			}

			string text = "Entry 1:<br><br>We found a derelict shuttle a few days ago. The life support was off and the crew was dead, except for one. We brought the survivor on board as they were near death and unconscious. I placed them in a stasis chamber and induced a coma while they heal. It seems to be working, but I can tell they have severe head trauma so they may not even remember what happened or even who they are.<br><br>Entry 2:<br><br>The healing process is coming along well. From what the security team could determine, the derelict ship was attacked by the Kilrathi. From what we learned, our patient goes by the name of " + pad.DataPatient + " and they are from the plant " + pad.DataPlanet + ". We didn't learn anything else about them.<br><br>Entry 3:<br><br>The station's fuel reserves are almost gone. Apparently some stranger came on board recently and our engineer gave them too much of our fuel so they can go off to be a space ace. We were orbiting a very primitive world and now that orbit is starting to decay. The captain informed us that we will be pulled to the surface in a matter of hours, so the crew is preparing everything they can for the upcoming impact. My patient is still in a coma, but I can't help to think there is something special about them. I am going to place the stasis chamber in our last medical shuttle and set the auto-pilot to hopefully land on the surface safely.";

			return text;
		}
	}
}