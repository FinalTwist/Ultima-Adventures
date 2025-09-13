using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class ShardOfFalsehood : Item
	{
		[Constructable]
		public ShardOfFalsehood() : base( 0x3155 )
		{
			Name = "Shard of Falsehood";
			Weight = 1.0;
			Hue = 0x48D;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				from.SendMessage( "You feel the lies emanating from this shard." );
			}
		}

		public ShardOfFalsehood(Serial serial) : base(serial)
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
		}
	}
}