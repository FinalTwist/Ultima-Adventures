using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public enum SignTypes
	{
		SignNE,
		SignNW,
		SignN,
		SignW,
		SignSE,
		SignE,
		SignS,
		SignSW
	}
	
	public class RoadSign : Item
	{
		[Constructable] 
		public RoadSign ( SignTypes type ) : base( 0x1297 + (int)type )
		{
			Name = "a sign";
			Movable = false;
		}
		
		[Constructable] 
		public RoadSign ( SignTypes type, string name ) : base( 0x1297 + (int)type )
		{
			Name = name;
			Movable = false;
		}
		
		public RoadSign( Serial serial ) : base( serial )
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