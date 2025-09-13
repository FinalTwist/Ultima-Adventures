using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class ReaperOil : Item
	{
		[Constructable]
		public ReaperOil() : this( 1 )
		{
		}

		[Constructable]
		public ReaperOil( int amount ) : base( 0xF7D )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Hue = 0x840;
			Name = "reaper oil";
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Softens Wood for Molding" );
		}

		public ReaperOil( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}