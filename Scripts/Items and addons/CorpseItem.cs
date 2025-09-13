using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class CorpseItem : Item 
	{ 
		[Constructable] 
		public CorpseItem() : base( Utility.Random( 0xECA, 9 ) ) 
		{ 
			Name = "bones of a lost adventurer"; 
			Movable = true;
			Weight = Utility.RandomMinMax(60,100); 
		} 
  
		public CorpseItem( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize(GenericWriter writer) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
			writer.Write( (string) Name );
			writer.Write( (int) Weight);
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
			Name = reader.ReadString();
			Weight = reader.ReadInt();
		}

	}
}