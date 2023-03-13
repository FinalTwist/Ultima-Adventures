using System;
using System.Collections;
using Server;
using Server.Multis;
using Server.Mobiles;

namespace Server.Items
{
	public enum PlankSide{ Port, Starboard }

	public class Plank : Item, ILockable
	{
		private BaseBoat m_Boat;
		private PlankSide m_Side;
		private bool m_Locked;
		private uint m_KeyValue;

		private Timer m_CloseTimer;

		public Plank( BaseBoat boat, PlankSide side, uint keyValue ) : base( 0x3EB1 + (int)side )
		{
			m_Boat = boat;
			m_Side = side;
			m_KeyValue = keyValue;
			m_Locked = true;
				if ( m_KeyValue == 0 ){ m_Locked = false; }
			Movable = false;

			if ( BaseBoat.isCarpet( m_Boat ) )
			{
				Name = "magic carpet";
				ItemID = 0x5431;
			}
		}

		public Plank( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );//version

			writer.Write( m_Boat );
			writer.Write( (int) m_Side );
			writer.Write( m_Locked );
			writer.Write( m_KeyValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Boat = reader.ReadItem() as BaseBoat;
					m_Side = (PlankSide) reader.ReadInt();
					m_Locked = reader.ReadBool();
					m_KeyValue = reader.ReadUInt();

					if ( m_Boat == null )
						Delete();

					break;
				}
			}

			if ( IsOpen )
			{
				m_CloseTimer = new CloseTimer( this );
				m_CloseTimer.Start();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public BaseBoat Boat{ get{ return m_Boat; } set{ m_Boat = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public PlankSide Side{ get{ return m_Side; } set{ m_Side = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Locked{ get{ return m_Locked; } set{ m_Locked = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public uint KeyValue{ get{ return m_KeyValue; } set{ m_KeyValue = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsOpen{ get{ return ( ItemID == 0x5435 || ItemID == 0x5436 || ItemID == 0x5437 || ItemID == 0x5438 || ItemID == 0x3ED5 || ItemID == 0x3ED4 || ItemID == 0x3E84 || ItemID == 0x3E89 ); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Starboard{ get{ return ( m_Side == PlankSide.Starboard ); } }

		public void SetFacing( Direction dir )
		{
			if ( BaseBoat.isCarpet( m_Boat ) )
			{
				if ( IsOpen )
				{
					switch ( dir )
					{
						case Direction.North: ItemID = Starboard ? 0x5437 : 0x5435; break;
						case Direction.East:  ItemID = Starboard ? 0x5438 : 0x5436; break;
						case Direction.South: ItemID = Starboard ? 0x5435 : 0x5437; break;
						case Direction.West:  ItemID = Starboard ? 0x5436 : 0x5438; break;
					}
				}
				else
				{
					switch ( dir )
					{
						case Direction.North: ItemID = Starboard ? 0x5433 : 0x5431; break;
						case Direction.East:  ItemID = Starboard ? 0x5434 : 0x5432; break;
						case Direction.South: ItemID = Starboard ? 0x5431 : 0x5433; break;
						case Direction.West:  ItemID = Starboard ? 0x5432 : 0x5434; break;
					}
				}
			}
			else
			{
			if ( IsOpen )
			{
				switch ( dir )
				{
					case Direction.North: ItemID = Starboard ? 0x3ED4 : 0x3ED5; break;
					case Direction.East:  ItemID = Starboard ? 0x3E84 : 0x3E89; break;
					case Direction.South: ItemID = Starboard ? 0x3ED5 : 0x3ED4; break;
					case Direction.West:  ItemID = Starboard ? 0x3E89 : 0x3E84; break;
				}
			}
			else
			{
				switch ( dir )
				{
					case Direction.North: ItemID = Starboard ? 0x3EB2 : 0x3EB1; break;
					case Direction.East:  ItemID = Starboard ? 0x3E85 : 0x3E8A; break;
					case Direction.South: ItemID = Starboard ? 0x3EB1 : 0x3EB2; break;
					case Direction.West:  ItemID = Starboard ? 0x3E8A : 0x3E85; break;
				}
			}
		}
		}

		public void Open()
		{
			if ( IsOpen || Deleted )
				return;

			if ( m_CloseTimer != null )
				m_CloseTimer.Stop();

			m_CloseTimer = new CloseTimer( this );
			m_CloseTimer.Start();

			switch ( ItemID )
			{
				case 0x3EB1: ItemID = 0x3ED5; break;
				case 0x3E8A: ItemID = 0x3E89; break;
				case 0x3EB2: ItemID = 0x3ED4; break;
				case 0x3E85: ItemID = 0x3E84; break;
				case 0x5431: ItemID = 0x5435; break;
				case 0x5432: ItemID = 0x5436; break;
				case 0x5433: ItemID = 0x5437; break;
				case 0x5434: ItemID = 0x5438; break;
			}

			if ( m_Boat != null )
				m_Boat.Refresh();
		}

		public override bool OnMoveOver( Mobile from )
		{
			if ( IsOpen )
			{
				if ( (from.Direction & Direction.Running) != 0 || (m_Boat != null && !m_Boat.Contains( from )) )
					return true;

				Map map = Map;

				if ( map == null )
					return false;

				int rx = 0, ry = 0;

				if ( ItemID == 0x3ED4 || ItemID == 0x5437 )
					rx = 1;
				else if ( ItemID == 0x3ED5 || ItemID == 0x5435 )
					rx = -1;
				else if ( ItemID == 0x3E84 || ItemID == 0x5438 )
					ry = 1;
				else if ( ItemID == 0x3E89 || ItemID == 0x5436 )
					ry = -1;

				for ( int i = 1; i <= 6; ++i )
				{
					int x = X + (i*rx);
					int y = Y + (i*ry);
					int z;

					for ( int j = -16; j <= 16; ++j )
					{
						z = from.Z + j;

						if ( map.CanFit( x, y, z, 16, false, false ) && !Server.Spells.SpellHelper.CheckMulti( new Point3D( x, y, z ), map )  )
						{
							if ( i == 1 && j >= -2 && j <= 2 )
								return true;

							from.Location = new Point3D( x, y, z );
							BaseCreature.TeleportPets( from, from.Location, from.Map, false );
							return false;
						}
					}

					z = map.GetAverageZ( x, y );

					if ( map.CanFit( x, y, z, 16, false, false ) && !Server.Spells.SpellHelper.CheckMulti( new Point3D( x, y, z ), map )  )
					{
						if ( i == 1 )
							return true;

						from.Location = new Point3D( x, y, z );
						BaseCreature.TeleportPets( from, from.Location, from.Map, false );
						return false;
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool CanClose()
		{
			Map map = Map;

			if ( map == null || Deleted )
				return false;

			foreach ( object o in this.GetObjectsInRange( 0 ) )
			{
				if ( o != this )
					return false;
			}

			return true;
		}

		public void Close()
		{
			if ( !IsOpen || !CanClose() || Deleted )
				return;

			if ( m_CloseTimer != null )
				m_CloseTimer.Stop();

			m_CloseTimer = null;

			switch ( ItemID )
			{
				case 0x3ED5: ItemID = 0x3EB1; break;
				case 0x3E89: ItemID = 0x3E8A; break;
				case 0x3ED4: ItemID = 0x3EB2; break;
				case 0x3E84: ItemID = 0x3E85; break;
				case 0x5435: ItemID = 0x5431; break;
				case 0x5436: ItemID = 0x5432; break;
				case 0x5437: ItemID = 0x5433; break;
				case 0x5438: ItemID = 0x5434; break;
			}

			if ( m_Boat != null )
				m_Boat.Refresh();
		}

		public override void OnDoubleClickDead( Mobile from )
		{
			OnDoubleClick( from );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_Boat == null )
				return;

			if ( from.InRange( GetWorldLocation(), 15 ) )
			{
				if ( m_Boat.Contains( from ) )
				{
					if ( IsOpen )
						Close();
					else
						Open();
				}
				else
				{
					if ( !IsOpen )
					{
						if ( !Locked )
						{
							Open();
						}
						else if ( from.AccessLevel >= AccessLevel.GameMaster )
						{
							from.LocalOverheadMessage( Network.MessageType.Regular, 0x00, 502502 ); // That is locked but your godly powers allow access
							Open();
						}
						else
						{
							from.LocalOverheadMessage( Network.MessageType.Regular, 0x00, 502503 ); // That is locked.
						}
					}
					else if ( !Locked )
					{
						from.Location = new Point3D( this.X, this.Y, this.Z + 3 );
						BaseCreature.TeleportPets( from, from.Location, from.Map, false );
					}
					else if ( from.AccessLevel >= AccessLevel.GameMaster )
					{
						from.LocalOverheadMessage( Network.MessageType.Regular, 0x00, 502502 ); // That is locked but your godly powers allow access
						from.Location = new Point3D( this.X, this.Y, this.Z + 3 );
						BaseCreature.TeleportPets( from, from.Location, from.Map, false );
					}
					else
					{
						from.LocalOverheadMessage( Network.MessageType.Regular, 0x00, 502503 ); // That is locked.
					}
				}
			}
		}

		private class CloseTimer : Timer
		{
			private Plank m_Plank;

			public CloseTimer( Plank plank ) : base( TimeSpan.FromSeconds( 15.0 ), TimeSpan.FromSeconds( 15.0 ) )
			{
				m_Plank = plank;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Plank.Close();
			}
		}
	}
}