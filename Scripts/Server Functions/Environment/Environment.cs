using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class ComputerBeeps : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomMinMax( 1, 2 ) == 1 )
					{
						int sound = Utility.RandomList( 0x548, 0x549, 0x55F );
						m.PlaySound( sound );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public ComputerBeeps( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "lightning crackle";
		}

		public ComputerBeeps( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class ComputerConsole : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						m.PlaySound( 0x54C );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));
				}
			}
		}

		[Constructable]
		public ComputerConsole( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "lightning crackle";
		}

		public ComputerConsole( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class Hologram : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomMinMax( 1, 2 ) == 1 )
					{
						int sound = Utility.RandomList( 0x0F5, 0x0F6, 0x0F7 );
						m.PlaySound( sound );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public Hologram( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "hologram hum";
		}

		public Hologram( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class LightningCracksFar : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 15 ) )
				{
					if ( Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						Point3D bolt = new Point3D( ( this.X ), ( this.Y ), ( this.Z+5 ) );
						int sound = Utility.RandomList( 0x028, 0x029 );
						Effects.SendLocationEffect( bolt, m.Map, 0x2A4E, 30, 10, 0, 0 );
						m.PlaySound( sound );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));
				}
			}
		}

		[Constructable]
		public LightningCracksFar( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "lightning crackle";
		}

		public LightningCracksFar( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class LightningCracks : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						Point3D bolt = new Point3D( ( this.X ), ( this.Y ), ( this.Z+5 ) );
						int sound = Utility.RandomList( 0x028, 0x029 );
						Effects.SendLocationEffect( bolt, m.Map, 0x2A4E, 30, 10, 0, 0 );
						m.PlaySound( sound );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));
				}
			}
		}

		[Constructable]
		public LightningCracks( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "lightning crackle";
		}

		public LightningCracks( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class SoundWindBlowing : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomMinMax( 1, 4 ) == 1 )
					{
						int sound = Utility.RandomList( 0x014, 0x015, 0x016, 0x565, 0x566, 0x567, 0x654 );
						m.PlaySound( sound );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 60 ));
				}
			}
		}

		[Constructable]
		public SoundWindBlowing( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "sound of wind";
		}

		public SoundWindBlowing( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class Electrical : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		private DateTime m_NextSound;
		public DateTime NextSound{ get{ return m_NextSound; } set{ m_NextSound = value; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( DateTime.UtcNow >= m_NextSound && Utility.InRange( m.Location, this.Location, 10 ) )
				{
					if ( Utility.RandomMinMax( 1, 2 ) == 1 )
					{
						m.PlaySound( 0x5C3 );
					}
					m_NextSound = (DateTime.UtcNow + TimeSpan.FromSeconds( 30 ));
				}
			}
		}

		[Constructable]
		public Electrical( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "lightning crackle";
		}

		public Electrical( Serial serial ) : base( serial )
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

namespace Server.Items
{
    public class RavendarkStorm : Item
	{
		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if( m is PlayerMobile )
			{
				if ( Utility.InRange( m.Location, this.Location, 10 ) )
				{
					Point3D bolt = new Point3D( ( this.X ), ( this.Y ), ( this.Z+5 ) );
					int sound = Utility.RandomList( 0x028, 0x029 );
					Effects.SendLocationEffect( bolt, m.Map, 0x2A4E, 30, 10, 0, 0 );
					m.PlaySound( sound );
					this.Delete();
				}
			}
		}

		[Constructable]
		public RavendarkStorm( ) : base( 0x215D )
		{
			Movable = false;
			Visible = false;
			Name = "storm";
		}

		public RavendarkStorm( Serial serial ) : base( serial )
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