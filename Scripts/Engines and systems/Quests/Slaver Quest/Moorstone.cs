using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class MoorStone : Item
	{
		private InternalTimer m_MoorTimer;

		[Constructable]
		public MoorStone() : base( 0xF8B )
		{
			Weight = 1.0;
			Hue = 1287;
			Name = "a djem";
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "You feel evil within" );
		}

		public override void OnDoubleClick( Mobile from )
		{
			string world = Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y );

			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( from is PlayerMobile && world == "the Bottle World of Kuldar" && !( Server.Items.CharacterDatabase.GetKeys( from, "VordoKey" ) ) )
			{
				from.SendMessage( "This magical gate doesn't seem to do anything." );
			}
			else if ( Worlds.AllowEscape( from, from.Map, from.Location, from.X, from.Y ) == false && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) != "the Bottle World of Kuldar" )
			{
				from.SendMessage( "This magical gate doesn't seem to do anything." );
			}
			else if ( Worlds.RegionAllowedRecall( from.Map, from.Location, from.X, from.Y ) == false && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) != "the Land of Ambrosia" && Worlds.GetMyWorld( from.Map, from.Location, from.X, from.Y ) != "the Bottle World of Kuldar" )
			{
				from.SendMessage( "This magical gate doesn't seem to do anything." );
			}
			else if ( from.Karma > -5000 )
			{
				from.SendMessage( "The stone bores through your soul and recoils immediately." );
			}
			else
			{
				Effects.PlaySound( from.Location, from.Map, 0x20E );
				m_MoorTimer = new InternalTimer(from);
				m_MoorTimer.Start ();
				this.Delete();
			}
		}

		private class InternalTimer : Timer
		{
			private MoorGate m_MoorGate;
			private Item m_MoonEffect;
			
			public InternalTimer ( Mobile from ) : base (TimeSpan.FromSeconds(60))
			{
				Delay = TimeSpan.FromSeconds(120);
				Priority = TimerPriority.OneSecond;
				Point3D loc = new Point3D(from.X, from.Y, from.Z);
				Map map = from.Map;
				
				m_MoorGate = new MoorGate();
				m_MoorGate.MoveToWorld (new Point3D(from.X, from.Y, from.Z), from.Map);

				CharacterDatabase.SetDiscovered( from, "DarkMoor", false );

				m_MoonEffect = new StonePaversDark();
				m_MoonEffect.Visible = true;
				m_MoonEffect.Weight = -2.0;
				m_MoonEffect.ItemID = 0x4C82;
				m_MoonEffect.Hue = 1;
				m_MoonEffect.Light = LightType.Circle300;
				m_MoonEffect.MoveToWorld (new Point3D(from.X, from.Y, from.Z+5), from.Map);
			}
			
			protected override void OnTick ()
			{
				((Item)m_MoorGate).Delete ();
				((Item)m_MoonEffect).Delete ();
				Stop();
			}
		}

		public MoorStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}