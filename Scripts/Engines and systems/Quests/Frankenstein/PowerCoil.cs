using System;
using Server; 
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using Server.Network;
using Server.Misc;

namespace Server.Items
{
	public class PowerCoil : Item
	{
		public override bool HandlesOnSpeech{ get{ return true; } }

		public override void OnSpeech( SpeechEventArgs e )
		{
			if ( !e.Handled )
			{
				Mobile m = e.Mobile;

				string keyword = "rise";

				if ( !m.Player )
					return;

				if ( !m.InRange( GetWorldLocation(), 10 ) )
					return;

				bool isMatch = false;

				if ( e.Speech.ToLower().IndexOf( keyword.ToLower() ) >= 0 )
					isMatch = true;

				if ( !isMatch )
					return;

				e.Handled = true;

				// DO RESURRECT
			}
		}

		[Constructable]
		public PowerCoil() : base( 0x8A7 )
		{
			Name = "power coil";
			Weight = 20.0;
			Light = LightType.Circle300;
		}

		public PowerCoil( Serial serial ) : base( serial )
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
}