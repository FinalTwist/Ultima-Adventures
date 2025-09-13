using System;
using Server; 
using Server.Items;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{ 
	public class BoneContainer : Bag
	{ 
		public override int MaxWeight{ get{ return 400; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		[Constructable] 
		public BoneContainer()
		{ 
			Name = "a strange pile of bones";
			Movable = true;
			Weight = 5;
			GumpID = 0x2A73;
			DropSound = 0x48;
			ItemID = 3786 + Utility.Random( 8 );
		} 

		public BoneContainer( Serial serial ) : base( serial )
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