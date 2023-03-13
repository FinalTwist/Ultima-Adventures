using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Multis;
using Server.Regions;

namespace Server.Items
{
	public enum AddonFitResult
	{
		Valid,
		Blocked,
		NotInHouse,
		DoorTooClose,
		NoWall,
		DoorsNotClosed
	}

	public interface IAddon
	{
		Item Deed{ get; }

		bool CouldFit( IPoint3D p, Map map );
	}

	public abstract class BaseAddon : Item, IChopable, IAddon
	{
		private List<AddonComponent> m_Components;
		private List<Item> m_Itms;
		
		private Mobile m_owner;
		[CommandProperty( AccessLevel.GameMaster )]
        public Mobile Owner
        {
            get{ return m_owner; }
            set{ m_owner = value; }
        }

		public void AddComponent( AddonComponent c, int x, int y, int z )
		{
			if ( Deleted || this == null)
			{
				//Console.WriteLine("1");
				return;
			}

			/*if (Map == null)
			{
				//Console.WriteLine("2");
				Map = Map.Trammel;
			}

			if (c == null)
				//Console.WriteLine("3");

			if (this == null)
				//Console.WriteLine("4");

			if (m_Components == null)
			{
				m_Components = new List<AddonComponent>();
				//Console.WriteLine("4.5");
			}*/

			m_Components.Add( c );

			c.Addon = this;
			c.Offset = new Point3D( x, y, z );
			c.MoveToWorld( new Point3D( X + x, Y + y, Z + z ), Map );
		}

		public void AddUsableItem( Item c, int x, int y, int z)
		{
			if ( Deleted )
				return;

			m_Itms.Add( c );
			c.MoveToWorld( new Point3D( X + x, Y + y, Z + z ), Map );
			c.Movable = false;
		}

		public BaseAddon() : base( 1 )
		{
			Movable = false;
			Visible = false;
			m_owner = null;

			m_Components = new List<AddonComponent>();
			m_Itms = new List<Item>();
		}

		public virtual bool RetainDeedHue{ get{ return false; } }

		public virtual void OnChop( Mobile from )
		{
			if (this is HeroSarcoNS || this is HeroSarcoWE) //not choppable
				return;

			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsOwner( from ) && house.Addons.Contains( this ) )
			{
				Effects.PlaySound( GetWorldLocation(), Map, 0x3B3 );
				from.SendLocalizedMessage( 500461 ); // You destroy the item.

				int hue = 0;

				if ( RetainDeedHue )
				{
					for ( int i = 0; hue == 0 && i < m_Components.Count; ++i )
					{
						AddonComponent c = m_Components[i];

						if ( c.Hue != 0 )
							hue = c.Hue;
					}
				}

				Delete();

				house.Addons.Remove( this );

				BaseAddonDeed deed = Deed;

				if ( deed != null )
				{
					if ( RetainDeedHue )
						deed.Hue = hue;

					from.AddToBackpack( deed );
				}
			}
			
			else if (house == null) // FINAL - adding for addons outside of homes
			{
				if (m_owner != null && m_owner != from)
				{
					from.SendMessage("Only he who placed this may destroy it.");
					return;
				}
				
				Effects.PlaySound( GetWorldLocation(), Map, 0x3B3 );
				from.SendLocalizedMessage( 500461 ); // You destroy the item.

				int hue = 0;

				if ( RetainDeedHue )
				{
					for ( int i = 0; hue == 0 && i < m_Components.Count; ++i )
					{
						AddonComponent c = m_Components[i];

						if ( c.Hue != 0 )
							hue = c.Hue;
					}
				}

				Delete();

				BaseAddonDeed deed = Deed;

				if ( deed != null )
				{
					if ( RetainDeedHue )
						deed.Hue = hue;

					from.AddToBackpack( deed );
				}
			}		
		}

		public virtual BaseAddonDeed Deed{ get{ return null; } }

		Item IAddon.Deed
		{
			get{ return this.Deed; }
		}

		public List<AddonComponent> Components
		{
			get
			{
				return m_Components;
			}
		}

		public List<Item> Items
		{
			get
			{
				return m_Itms;
			}
		}

		public BaseAddon( Serial serial ) : base( serial )
		{
		}

		public bool CouldFit( IPoint3D p, Map map )
		{
			if (map == null )
				return false;
			
			BaseHouse h = null;
			return ( CouldFit( p, map, null, ref h ) == AddonFitResult.Valid );
		}

		public virtual AddonFitResult CouldFit( IPoint3D p, Map map, Mobile from, ref BaseHouse house )
		{
			
			if (from.AccessLevel >= AccessLevel.GameMaster)
				return AddonFitResult.Valid;
			
			if ( from == null || Deleted  || map == null )
				return AddonFitResult.Blocked;

			Region reg = Region.Find( new Point3D( p ), from.Map );

			if ( (this is HousePlacementTool || this is NewHousePlacement) && !reg.AllowHousing( from, new Point3D( p ) ) )
				return AddonFitResult.Blocked;

			foreach ( AddonComponent c in m_Components )
			{
				if (c == null )
					return AddonFitResult.Blocked;
				
				Point3D p3D = new Point3D( p.X + c.Offset.X, p.Y + c.Offset.Y, p.Z + c.Offset.Z );

				if ( c.NeedsWall )
				{
					Point3D wall = c.WallPosition;

					if ( !IsWall( p3D.X + wall.X, p3D.Y + wall.Y, p3D.Z + wall.Z, map ) )
						return AddonFitResult.NoWall;
				}

				if ( house != null && house.IsOwner( from ) && ( this is StoneOvenEastAddon || this is StoneOvenSouthAddon ) )
					return AddonFitResult.Valid;

				if ( !map.CanFit( p3D.X, p3D.Y, p3D.Z, c.ItemData.Height, false, true, ( c.Z == 0 ) ) )
					return AddonFitResult.Blocked;
			}

			if (house == null)
				return AddonFitResult.Valid;

			else if (house != null)
			{
				ArrayList doors = house.Doors;

				for ( int i = 0; i < doors.Count; ++i )
				{
					BaseDoor door = doors[i] as BaseDoor;

					Point3D doorLoc = door.GetWorldLocation();
					int doorHeight = door.ItemData.CalcHeight;

					foreach ( AddonComponent c in m_Components )
					{
						Point3D addonLoc = new Point3D( p.X + c.Offset.X, p.Y + c.Offset.Y, p.Z + c.Offset.Z );
						int addonHeight = c.ItemData.CalcHeight;
							
						if ( Utility.InRange( doorLoc, addonLoc, 1 ) && (addonLoc.Z == doorLoc.Z || ((addonLoc.Z + addonHeight) > doorLoc.Z && (doorLoc.Z + doorHeight) > addonLoc.Z)) )
							return AddonFitResult.DoorTooClose;
					}
				}
				return AddonFitResult.Valid;
			}
			
				
			
			return AddonFitResult.Blocked;
		}

		public static bool CheckHouse( Mobile from, Point3D p, Map map, int height, ref BaseHouse house )
		{
			house = BaseHouse.FindHouseAt( p, map, height );

			if ( from == null || house == null || !house.IsOwner( from ) )
				return false;

			return true;
		}

		public static bool IsWall( int x, int y, int z, Map map )
		{
			if ( map == null )
				return false;

			StaticTile[] tiles = map.Tiles.GetStaticTiles( x, y, true );

			for ( int i = 0; i < tiles.Length; ++i )
			{
				StaticTile t = tiles[i];
				ItemData id = TileData.ItemTable[t.ID & TileData.MaxItemValue];

				if ( (id.Flags & TileFlag.Wall) != 0 && (z + 16) > t.Z && (t.Z + t.Height) > z )
					return true;
			}

			return false;
		}

		public virtual void OnComponentLoaded( AddonComponent c )
		{
		}

		public virtual void OnComponentUsed( AddonComponent c, Mobile from )
		{
		}

		public override void OnLocationChange( Point3D oldLoc )
		{
			if ( Deleted )
				return;

			foreach ( AddonComponent c in m_Components )
				c.Location = new Point3D( X + c.Offset.X, Y + c.Offset.Y, Z + c.Offset.Z );
			foreach (Item cc in m_Itms)
				cc.Location = new Point3D( X, Y, Z );
		}

		public override void OnMapChange()
		{
			if ( Deleted )
			{
				//Console.WriteLine("6");
				return;
			}

			//if (m_Components == null)
				//Console.WriteLine("7");

			foreach ( AddonComponent c in m_Components )
			{
				//if (Map == null)
					//Console.WriteLine("8");

				//if (c == null)
					//Console.WriteLine("9");

				c.Map = Map;
			}
			foreach ( Item cc in m_Itms )
			{
				//if (Map == null)
					//Console.WriteLine("10");

				//if (cc == null)
					//Console.WriteLine("11");
					
				cc.Map = Map;
			}
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			foreach ( AddonComponent c in m_Components )
					c.Delete();
			if (m_Itms != null)
			{
				foreach ( Item cc in m_Itms )
				{
					if (cc is HousePlacementContainer)
					{
						HousePlacementContainer hp = (HousePlacementContainer)cc;
						if (hp.HaveWood > 0)
						{
							Item boards = new Board((int)((double)hp.HaveWood * ((double)Utility.RandomMinMax(80, 100)/100) ));
							boards.MoveToWorld(cc.Location, cc.Map);
						}
						if (hp.HaveNails > 0)
						{
							Item nails = new Nails((int)((double)hp.HaveNails * ((double)Utility.RandomMinMax(80, 100)/100) ));
							nails.MoveToWorld(cc.Location, cc.Map);
						}
						if (hp.HaveStone > 0)
						{
							Item nails = new Granite((int)((double)hp.HaveStone * ((double)Utility.RandomMinMax(80, 100)/100) ));
							nails.MoveToWorld(cc.Location, cc.Map);
						}
					}

					cc.Delete();
				}
					
			}
		}

		public virtual bool ShareHue{ get{ return true; } }

		[Hue, CommandProperty( AccessLevel.GameMaster )]
		public override int Hue
		{
			get
			{
				return base.Hue;
			}
			set
			{
				if ( base.Hue != value )
				{
					base.Hue = value;

					if ( !Deleted && this.ShareHue && m_Components != null )
					{
						foreach ( AddonComponent c in m_Components )
							c.Hue = value;
					}
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version ++3 owners

			if (m_Itms == null)
				m_Itms = new List<Item>();
				
			writer.Write( (Mobile)m_owner);
			writer.WriteItemList<Item>( m_Itms );//2
			writer.WriteItemList<AddonComponent>( m_Components );//0
			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				{
					m_owner = reader.ReadMobile();
					goto case 2;
				}
				case 2:
				{
					m_Itms = reader.ReadStrongItemList<Item>();
					goto case 1;
				}
				case 1:
				case 0:
				{
					m_Components = reader.ReadStrongItemList<AddonComponent>();
					break;
				}
			}

			if ( version < 1 && Weight == 0 )
				Weight = -1;
		}
	}
}
