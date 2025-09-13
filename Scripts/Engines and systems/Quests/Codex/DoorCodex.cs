using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Misc;

namespace Server.Items
{
	[Flipable(0x679, 0x67B)]
	public class DoorCodex : Item
	{
		[Constructable]
		public DoorCodex() : base( 0x679 )
		{
			Name = "chamber door";
			Weight = 1.0;
			Hue = 0xB4F;
		}

		public DoorCodex( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.Y < this.Y && m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoTeleport( m );
			}
			else if ( m.Y > this.Y && m.InRange( this.GetWorldLocation(), 2 ) && m.Backpack.FindItemByType( typeof ( VortexCube ) ) != null )
			{
				Item square = m.Backpack.FindItemByType( typeof ( VortexCube ) );

				if ( square is VortexCube )
				{
					VortexCube cube = (VortexCube)square;
					int quest = cube.HasConvexLense + cube.HasConcaveLense + cube.HasKeyLaw + cube.HasKeyChaos + cube.HasKeyBalance + cube.HasCrystalRed + cube.HasCrystalBlue + cube.HasCrystalGreen + cube.HasCrystalYellow + cube.HasCrystalWhite + cube.HasCrystalPurple;

					if ( cube.CubeOwner == m && quest > 10 )
					{
						DoTeleport( m );
					}
					else
					{
						m.SendMessage( "This door seems to be magically sealed." );
					}
				}
			}
			else if ( m.Y > this.Y && m.InRange( this.GetWorldLocation(), 2 ) && m.Backpack.FindItemByType( typeof ( CodexWisdom ) ) != null )
			{
				Item book = m.Backpack.FindItemByType( typeof ( CodexWisdom ) );
				Item tome = m.FindItemOnLayer( Layer.Talisman );

				if ( book is CodexWisdom )
				{
					CodexWisdom codex = (CodexWisdom)book;

					if ( codex.CodexOwner == m )
					{
						DoTeleport( m );
					}
					else
					{
						m.SendMessage( "This door seems to be magically sealed." );
					}
				}
				else if ( tome is CodexWisdom )
				{
					CodexWisdom codex = (CodexWisdom)tome;

					if ( codex.CodexOwner == m )
					{
						DoTeleport( m );
					}
					else
					{
						m.SendMessage( "This door seems to be magically sealed." );
					}
				}
			}
			else if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendMessage( "This door seems to be magically sealed." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) && GetPlayerInfo.EvilPlayer( m ) )
			{
				DoTeleport( m );
			}
			else if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendMessage( "This door seems to be magically sealed." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public virtual void DoTeleport( Mobile m )
		{
			Point3D p = new Point3D(this.X, (this.Y+1), -42);

			if ( m.Y > this.Y )
			{
				p = new Point3D(this.X, (this.Y-1), -42);
			}

			m.PlaySound( 236 );

			Server.Mobiles.BaseCreature.TeleportPets( m, p, m.Map );

			m.MoveToWorld( p, m.Map );
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