using System;

namespace Server.Items
{
	public class BoneCouchComponent : AddonComponent
	{
		public override int LabelNumber { get { return 1074477; } } // Bone couch

		public BoneCouchComponent( int itemID ) : base( itemID )
		{
		}

		public BoneCouchComponent( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			bool allow = base.OnMoveOver( m );

			if ( allow && m.Alive && m.Player && ( m.AccessLevel == AccessLevel.Player || !m.Hidden ) )
				Effects.PlaySound( Location, Map, Utility.RandomMinMax( 0x547, 0x54A ) );

			return allow;
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

	[FlipableAddon( Direction.South, Direction.East )]
	public class BoneCouchAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BoneCouchDeed(); } }

		[Constructable]
		public BoneCouchAddon() : base()
		{
			Direction = Direction.South;

			AddComponent( new BoneCouchComponent( 0x2A5A ), 0, 0, 0 );
			AddComponent( new BoneCouchComponent( 0x2A5B ), -1, 0, 0 );
		}

		public BoneCouchAddon( Serial serial ) : base( serial )
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

		public virtual void Flip( Mobile from, Direction direction )
		{
			switch ( direction )
			{
				case Direction.East:
					AddComponent( new BoneCouchComponent( 0x2A80 ), 0, 0, 0 );
					AddComponent( new BoneCouchComponent( 0x2A7F ), 0, 1, 0 );
					break;
				case Direction.South:
					AddComponent( new BoneCouchComponent( 0x2A5A ), 0, 0, 0 );
					AddComponent( new BoneCouchComponent( 0x2A5B ), -1, 0, 0 );
					break;
			}
		}
	}

	public class BoneCouchDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BoneCouchAddon(); } }

		[Constructable]
		public BoneCouchDeed() : base()
		{
			Name = "box containing a couch of bones";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
		}

		public BoneCouchDeed( Serial serial ) : base( serial )
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
