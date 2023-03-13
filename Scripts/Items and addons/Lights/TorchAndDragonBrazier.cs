using System;
using Server;

namespace Server.Items
{
	public class light_dragon_brazier : BaseLight
	{
        public override int LitItemID { get { return 0x194D; } }
        public override int UnlitItemID { get { return 0x194E; } }

		[Constructable]
		public light_dragon_brazier() : base(0x194E)
		{
			Movable = true;
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 3.0;
			Name = "dragon brazier";
		}

		public light_dragon_brazier( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	[Flipable(0xA07, 0xA0C)]
	public class light_wall_torch: Item
	{
		[Constructable]
		public light_wall_torch() : base(0xA07)
		{
			Movable = true;
			Weight = 1.0;
			Light = LightType.Circle225;
			Name = "wall torch";
		}

		public light_wall_torch(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
}