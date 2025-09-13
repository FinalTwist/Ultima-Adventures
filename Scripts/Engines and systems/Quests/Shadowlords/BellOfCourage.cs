using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class BellOfCourage : Item
	{
		[Constructable]
		public BellOfCourage() : base( 0x1C12 )
		{
			Name = "Bell of Courage";
			Weight = 1.0;
		}

		private static int[] m_Sounds = new int[] { 0x505, 0x506, 0x507 };

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				from.PlaySound( m_Sounds[Utility.Random( m_Sounds.Length )] );
				from.SendMessage( "You ring the bell, producing a courageous melody." );
			}
		}

		public BellOfCourage(Serial serial) : base(serial)
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