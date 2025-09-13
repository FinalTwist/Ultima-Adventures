using System;
using Server;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x2A69, 0x2A6D )]
	public class CreepyPortraitComponent : AddonComponent
	{
		public override int LabelNumber { get { return 1074481; } } // Creepy portrait
		public override bool HandlesOnMovement { get { return true; } }

		public CreepyPortraitComponent() : base( 0x2A69 )
		{
		}

		public CreepyPortraitComponent( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Utility.InRange( Location, from.Location, 2 ) )
				Effects.PlaySound( Location, Map, Utility.RandomMinMax( 0x565, 0x566 ) );
			else
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
		}

		public override void OnMovement( Mobile m, Point3D old )
		{
			if ( m.Alive && m.Player && ( m.AccessLevel == AccessLevel.Player || !m.Hidden ) )
			{
				if ( !Utility.InRange( old, Location, 2 ) && Utility.InRange( m.Location, Location, 2 ) )
				{
					if ( ItemID == 0x2A69 || ItemID == 0x2A6D )
						Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), TimeSpan.FromSeconds( 0.5 ), 3, new TimerCallback( Up ) );
				}
				else if ( Utility.InRange( old, Location, 2 ) && !Utility.InRange( m.Location, Location, 2 ) )
				{
					if ( ItemID == 0x2A6C || ItemID == 0x2A70 )
						Timer.DelayCall( TimeSpan.FromSeconds( 0.5 ), TimeSpan.FromSeconds( 0.5 ), 3, new TimerCallback( Down ) );
				}
			}
		}

		private void Up()
		{
			ItemID += 1;
		}

		private void Down()
		{
			ItemID -= 1;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class CreepyPortraitAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new CreepyPortraitDeed(); } }

		[Constructable]
		public CreepyPortraitAddon() : base()
		{
			AddComponent( new CreepyPortraitComponent(), 0, 0, 0 );
		}

		public CreepyPortraitAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class CreepyPortraitDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new CreepyPortraitAddon(); } }

		[Constructable]
		public CreepyPortraitDeed() : base()
		{
			Name = "box containing a creepy portrait";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
		}

		public CreepyPortraitDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			if ( ItemID != 0x3420 && ItemID != 0x3425 ){ ItemID = 0x3425; }
		}
	}
}
