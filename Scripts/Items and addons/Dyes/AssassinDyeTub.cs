using System;

namespace Server.Items
{
	public class AssassinDyeTub : DyeTub
	{
		[Constructable]
		public AssassinDyeTub()
		{
			Hue = DyedHue = 0x497;
			Redyable = false;
		}

		public override bool AllowLeather
		{
			get{ return true; }
		}

		public override bool AllowDyables
		{
			get{ return true; }
		}

		public override bool AllowArmor
		{
			get{ return true; }
		}

		public override bool AllowWeapons
		{
			get{ return true; }
		}

		public AssassinDyeTub( Serial serial ) : base( serial )
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