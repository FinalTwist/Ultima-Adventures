using System;

namespace Server.Items
{
	public class WornHumanDeco : BaseRing
	{
		public override bool DisplayLootType{ get{ return false; } }
		public override bool DisplayWeight{ get{ return false; } }

		[Constructable]
		public WornHumanDeco() : base( 0x4CFA )
		{
			Weight = 0;
			LootType = LootType.Blessed;
			Movable = false;
		}

		public WornHumanDeco( Serial serial ) : base( serial )
		{
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

	[Flipable( 0x2FC5, 0x317B )]
	public class DemonWings : BaseMiddleTorso
	{
		[Constructable]
		public DemonWings() : this( 0 )
		{
		}

		[Constructable]
		public DemonWings( int hue ) : base( 0x2FC5, hue )
		{
			Name = "demon wings";
			Weight = 1.0;
		}

		public DemonWings( Serial serial ) : base( serial )
		{
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

	[Flipable( 0x2B72, 0x3169 )]
	public class DemonHead : BaseHat
	{
		[Constructable]
		public DemonHead() : this( 0 )
		{
		}

		[Constructable]
		public DemonHead( int hue ) : base( 0x2B72, hue )
		{
			Name = "demon head";
			Weight = 1.0;
		}

		public DemonHead( Serial serial ) : base( serial )
		{
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
