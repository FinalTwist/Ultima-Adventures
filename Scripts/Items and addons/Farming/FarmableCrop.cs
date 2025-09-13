using System;
using Server;
using Server.Network;
using Server.Regions;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class FarmableCrop : Item
	{
		private bool m_Picked;

		public abstract Item GetCropObject();
		public abstract int GetPickedID();

		public FarmableCrop( int itemID ) : base( itemID )
		{
			Movable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Map map = this.Map;
			Point3D loc = this.Location;

			if ( Parent != null || Movable || IsLockedDown || IsSecure || map == null || map == Map.Internal )
				return;

			if ( !from.InRange( loc, 2 ) || !from.InLOS( this ) )
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else if ( !m_Picked )
				OnPicked( from, loc, map );
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile && m.Alive )
			{
				this.OnDoubleClick( m );
			}
			return true;
		}

		public virtual void OnPicked( Mobile from, Point3D loc, Map map )
		{
			ItemID = GetPickedID();

			Item spawn = GetCropObject();

			if ( spawn != null )
			{
				if ( from.PlaceInBackpack( spawn ) )
				{
					from.SendMessage( "You put it in your backpack." );
				}
				else
				{
					from.SendMessage( "You can't fit it in your backpack!" );
					spawn.MoveToWorld( loc, map );
				}
			}

			m_Picked = true;
		}

		public void Unlink()
		{
			ISpawner se = this.Spawner;

			if ( se != null )
			{
				this.Spawner.Remove( this );
				this.Spawner = null;
			}

		}

		public FarmableCrop( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 ); // version
			writer.Write( m_Picked );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
			switch ( version )
			{
				case 0:
					m_Picked = reader.ReadBool();
					break;
			}
			if ( m_Picked )
			{
				//Unlink();
				//Delete();
			}
		}
	}
}