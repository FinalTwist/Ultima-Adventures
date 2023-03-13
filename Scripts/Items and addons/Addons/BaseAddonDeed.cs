using System;
using System.Collections.Generic;
using Server;
using Server.Multis;
using Server.Targeting;

using Server.Commands;
using Server.Commands.Generic;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x14F0, 0x14EF )]
	public abstract class BaseAddonDeed : Item
	{
		public abstract BaseAddon Addon{ get; }

		public BaseAddonDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
		}

		public BaseAddonDeed( Serial serial ) : base( serial )
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

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				from.Target = new InternalTarget( this );
			else
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
		}

		private class InternalTarget : Target
		{
			private BaseAddonDeed m_Deed;

			public InternalTarget( BaseAddonDeed deed ) : base( -1, true, TargetFlags.None )
			{
				m_Deed = deed;

				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				IPoint3D p = targeted as IPoint3D;
				Point3D p2 = new Point3D( p);
				Point3D fromloc = new Point3D( from);
				
				Map map = from.Map;

				if ( p == null || map == null || m_Deed.Deleted )
					return;

				if ( m_Deed.IsChildOf( from.Backpack ) )
				{
					BaseAddon addon = m_Deed.Addon;

					Server.Spells.SpellHelper.GetSurfaceTop( ref p );

					BaseHouse house = BaseHouse.FindHouseAt( p2, map, p2.Z );

					AddonFitResult res = addon.CouldFit( p, map, from, ref house );

					if ( !(Utility.InRange( p2, fromloc, 10 )) )
					{
						from.SendMessage( 0,  "This is too far away." );
						return;
					}

					if ( res == AddonFitResult.Valid )
					{
						addon.MoveToWorld( new Point3D( p ), map );
						if (house == null)
							m_Deed.Delete();

						if (addon is BaseHarvestPatchAddon || addon is BaseFruitTreeAddon)
							addon.Owner = from;
						
					}
					else if ( res == AddonFitResult.Blocked )
						from.SendLocalizedMessage( 500269 ); // You cannot build that there.
					else if ( res == AddonFitResult.DoorTooClose )
						from.SendLocalizedMessage( 500271 ); // You cannot build near the door.
					else if ( res == AddonFitResult.NoWall )
						from.SendLocalizedMessage( 500268 ); // This object needs to be mounted on something.
					
					if ( res == AddonFitResult.Valid )
					{
						if (house != null)
						{
							m_Deed.Delete();
							house.Addons.Add( addon );
						}						
					}
					else
					{
						addon.Delete();
					}
				}
				else
				{
					from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				}
			}
		}
	}
}