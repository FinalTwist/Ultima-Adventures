using System;
using Server;

namespace Server.Items
{
	public class RuneStoneGate : Item
	{
		[Constructable]
		public RuneStoneGate() : base( 0x21B9 )
		{
			Movable = false;
			Name = "runic doorway";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This large stone door is covered in strange runes." );
		}

		public RuneStoneGate(Serial serial) : base(serial)
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