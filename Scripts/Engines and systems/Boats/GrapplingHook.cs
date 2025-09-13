using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Multis;
using Server.Mobiles;

namespace Server.Items
{
	public class GrapplingHook : Item
	{
		[Constructable]
		public GrapplingHook() : base( 0x4F40 )
		{
			Weight = 3.0;
			Name = "grappling hook";
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "Target a crew member to board their ship." );
				t = new HookTarget();
				from.Target = t;
			}
		}

		private class HookTarget : Target
		{
			public HookTarget() : base( 20, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseCreature )
				{
					BaseCreature pirate = targeted as BaseCreature;
					Point3D loc = Server.Multis.BaseBoat.GetPirateShip( pirate );

					if ( loc.X > 0 && loc.Y > 0 ){ DoTeleport( from, loc ); }
					else { from.SendMessage( "You cannot use the hook on this." ); }
				}
				else
				{
					from.SendMessage( "You cannot use the hook on this." );
				}
			}
		}

		public static void DoTeleport( Mobile m, Point3D p )
		{
			m.PlaySound( Utility.RandomList( 0x5D2,0x5D3 ) );
			Server.Mobiles.BaseCreature.TeleportPets( m, p, m.Map );
			m.MoveToWorld( p, m.Map );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Used to board boats and galleons");
        }

		public GrapplingHook(Serial serial) : base(serial)
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