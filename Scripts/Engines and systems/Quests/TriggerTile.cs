using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class TriggerTile : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				CharacterDatabase.SetBardsTaleQuest( m, this.Name, true );
			}
		}

		[Constructable]
		public TriggerTile( ) : base( 0x181E )
		{
			Movable = false;
			Visible = false;
			Name = "trigger";
		}

		public TriggerTile( Serial serial ) : base( serial )
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