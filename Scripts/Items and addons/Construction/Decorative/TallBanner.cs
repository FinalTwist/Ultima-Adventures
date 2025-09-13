using System;
using Server.Network;

namespace Server.Items
{

	[FlipableAttribute( 0x54CC, 0x54CD )] 
	public class TallBannerEast : Item
	{
		[Constructable]
		public TallBannerEast() : base( 0x54CC )
		{
			Name = "a tall banner";
			Weight = 50;
		}

		public TallBannerEast( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x54CA, 0x54CB )] 
	public class TallBannerNorth : Item
	{
		[Constructable]
		public TallBannerNorth() : base( 0x54CA )
		{
			Name = "a tall banner";
			Weight = 50;
		}

		public TallBannerNorth( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x54CE, 0x54CF )] 
	public class GuildedTallBannerEast : Item
	{
		[Constructable]
		public GuildedTallBannerEast() : base( 0x54CE )
		{
			Name = "a guilded tall banner";
			Weight = 50;
		}

		public GuildedTallBannerEast( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x54D0, 0x54D1 )] 
	public class GuildedTallBannerNorth : Item
	{
		[Constructable]
		public GuildedTallBannerNorth() : base( 0x54D0 )
		{
			Name = "a guilded tall banner";
			Weight = 50;
		}

		public GuildedTallBannerNorth( Serial serial ) : base( serial )
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