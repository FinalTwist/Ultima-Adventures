using System;

namespace Server.Items
{
	[Flipable( 0x2A58, 0x2A59 )]
	public class BoneThroneComponent : AddonComponent
	{
		public override int LabelNumber { get { return 1074476; } } // Bone throne

		public BoneThroneComponent() : base( 0x2A58 )
		{
		}

		public BoneThroneComponent( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			bool allow = base.OnMoveOver( m );

			if ( allow && m.Alive && m.Player && ( m.AccessLevel == AccessLevel.Player || !m.Hidden ) )
				Effects.PlaySound( Location, Map, Utility.RandomMinMax( 0x54B, 0x54D ) );

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

	public class BoneThroneAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BoneThroneDeed(); } }

		[Constructable]
		public BoneThroneAddon() : base()
		{
			AddComponent( new BoneThroneComponent(), 0, 0, 0 );
		}

		public BoneThroneAddon( Serial serial ) : base( serial )
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

	public class BoneThroneDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BoneThroneAddon(); } }

		[Constructable]
		public BoneThroneDeed() : base()
		{
			Name = "box containing a throne of bones";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
		}

		public BoneThroneDeed( Serial serial ) : base( serial )
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
