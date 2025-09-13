using System;
using Server.Mobiles;

namespace Server.Items
{
	public class MoonObelisk : Item
	{
		public override int LabelNumber{ get{ return 1016474; } } // an MoonObelisk

		[Constructable]
		public MoonObelisk() : base(0x115F)
		{
			Movable = false;
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile && ((PlayerMobile)m).SkillsCap != 10000)
			{
				if ( Utility.InRange( Location, m.Location, 6 ) )
				{
    					//Point3D shoofrancisaway = new Point3D(2228, 3572, 10);
					m.MoveToWorld(oldLocation, m.Map);
     					m.SendMessage("A strange moon on the obelisk glows and stops you from moving further.");
	  			}
			}
		}

		public MoonObelisk(Serial serial) : base(serial)
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
