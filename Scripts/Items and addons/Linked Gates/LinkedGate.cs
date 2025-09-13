using System;
using Server;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Reflection;
using Server.Network;
using Server.Misc;
using Server.Regions;
using Server.Gumps;

namespace Server.Lokai
{
	public class LinkedGate : Moongate
	{
		private LinkedGate mateGate;
		public LinkedGate MateGate { get { return mateGate; } set { mateGate = value; } }
		
		private Point3D m_Target;
		private Map m_TargetMap;

		private bool m_WasPlaced = false;

		private Mobile m_owner;
		
		private bool showWarning;
		[CommandProperty( AccessLevel.GameMaster )]
		public bool ShowWarning { get { return showWarning; } set { showWarning = value; } }

		[Constructable]
		public LinkedGate() : base( false )
		{
			Movable = true;
			Visible = true;
			ShowWarning = false;
			Hue = 0x2D1;
			Light = LightType.Circle300;
			m_owner = null;
		}

		public LinkedGate( Serial serial ) : base( serial )
		{
		}

		public override void DelayCallback( Mobile from, int range )
		{
			BeginConfirmation( from );
		}

		public override void BeginConfirmation( Mobile from )
		{
			if ( ShowWarning && ( IsInTown( from.Location, from.Map ) && !IsInTown( MateGate.Location, MateGate.Map )
					|| ( from.Map != Map.Felucca && MateGate.Map == Map.Felucca && ShowFeluccaWarning ) ) )
			{
				from.Send( new PlaySound( 0x20E, from.Location ) );
				from.CloseGump( typeof( MoongateConfirmGump ) );
				from.SendGump( new MoongateConfirmGump( from, this ) );
			}
			else
			{
				EndConfirmation( from );
			}
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
		    if ( this.ParentEntity == null )
		    {
			m_WasPlaced = true;
		    }
		}

		public override void UseGate( Mobile m )
		{
			if ( MateGate == null || MateGate.Deleted  )
			{
				m.SendMessage("This gate isn't currently linked to another gate - was its mate destroyed?");
				//Console.WriteLine( "The Gate at {0} is missing its mate", this.Location.ToString() );
				return;
			}
			
			if ( MateGate.ParentEntity != null ) 
			{
				if (!MateGate.m_WasPlaced)
				{
				    m.SendMessage("This gate's linked moongate wasn't placed yet.");
				}
				else
				{
				    m.SendMessage("This gate's linked moongate was moved and is no longer accessible... ");
				}
				//Console.WriteLine( "The Gate at {0} is missing its mate", this.Location.ToString() );
				return;
			}

			m_Target = MateGate.Location;
			m_TargetMap = MateGate.Map;
			
			ClientFlags flags = m.NetState == null ? ClientFlags.None : m.NetState.Flags;

			if ( m_TargetMap != null && m_TargetMap != Map.Internal )
			{

				string world = Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y );
				string mateworld = Worlds.GetMyWorld( MateGate.Map, MateGate.Location, MateGate.X, MateGate.Y );
				Region reg = Region.Find( this.Location, this.Map );

				if (m is PlayerMobile && m.AccessLevel >= AccessLevel.GameMaster) //power to the gm's
				{
					BaseCreature.TeleportPets( m, m_Target, m_TargetMap );
					m.MoveToWorld( m_Target, m_TargetMap );
					m.PlaySound( 0x1FE );
					OnGateUsed( m );
				}

				if ( !Worlds.RegionAllowedTeleport( this.Map, this.Location, this.X, this.Y ) && world != "the Land of Ambrosia"  )
				{
					m.SendMessage( "This other gate's region destabilizes the link." );
					return;
				}
				else if ( !Worlds.RegionAllowedTeleport( MateGate.Map, MateGate.Location, MateGate.X, MateGate.Y ) && mateworld != "the Land of Ambrosia" )
				{
					m.SendMessage( "This other gate's region destabilizes the link." );
					return;
				}
				else if ( reg.IsPartOf( typeof( DungeonRegion ) ) && this.Map != Map.Trammel )
				{
					m.SendMessage( "This other gate's region destabilizes the link." );
					return;
				}
				else if ( !CharacterDatabase.GetDiscovered( m, mateworld ) )
				{
					m.SendMessage( "You don't quite know where the gate leads so you change your mind." );
					return;
				}
				else if (m.Map != m_TargetMap)
				{
					m.SendMessage( "This moongate leads to another world and lacks the stability required for travel." );
					return;
				}
				else
				{	
				BaseCreature.TeleportPets( m, m_Target, m_TargetMap );

				m.MoveToWorld( m_Target, m_TargetMap );

				m.PlaySound( 0x1FE );

				OnGateUsed( m );
				}
			}
			else
			{
				m.SendMessage( "This moongate does not seem to go anywhere." );
			}
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			if (MateGate != null)
			{

				string mateworld = Worlds.GetMyWorld( MateGate.Map, MateGate.Location, MateGate.X, MateGate.Y );
				list.Add( "This magical gate can teleport you to another land." ); 
				list.Add( "Through it, you glimpse " + mateworld );
				
			}
			else
				list.Add( "You can't see any destination through the gate." ); 
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (m_owner != null && m_owner != from)
				return;
			else if (m_owner == null)
				m_owner = from;

			from.CloseGump(typeof(GateNameGump));
			from.SendGump(new GateNameGump(from, this));
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 4 ); // version

			writer.Write( (Mobile)m_owner);
			writer.Write( m_WasPlaced );
			writer.Write( showWarning );
			writer.Write( mateGate );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 4:
				{
					m_owner = reader.ReadMobile();
					goto case 3;
				}
				case 3:
				{
					m_WasPlaced = reader.ReadBool();
					goto case 2;
				}
				case 2:
				{
					showWarning = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					mateGate = reader.ReadItem() as LinkedGate;
					break;
				}
			}
		}
	}
}
