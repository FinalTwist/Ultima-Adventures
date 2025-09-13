using System;
using Server.Network;

namespace Server.Items
{

	[FlipableAttribute( 0x5528, 0x5529 )] 
	public class CathedralWindow1 : Item
	{
		[Constructable]
		public CathedralWindow1() : base( 0x5528 )
		{
			Name = "a cathedral window";
			Weight = 50;
		}

		public CathedralWindow1( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x552A, 0x552B )] 
	public class CathedralWindow2 : Item
	{
		[Constructable]
		public CathedralWindow2() : base( 0x552A )
		{
			Name = "a cathedral window";
			Weight = 50;
		}

		public CathedralWindow2( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x552C, 0x552D )] 
	public class CathedralWindow3 : Item
	{
		[Constructable]
		public CathedralWindow3() : base( 0x552C )
		{
			Name = "a cathedral window";
			Weight = 50;
		}

		public CathedralWindow3( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x552E, 0x552F )] 
	public class CathedralWindow4 : Item
	{
		[Constructable]
		public CathedralWindow4() : base( 0x552E )
		{
			Name = "a cathedral window";
			Weight = 50;
		}

		public CathedralWindow4( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x5530, 0x5531 )] 
	public class CathedralWindow5 : Item
	{
		[Constructable]
		public CathedralWindow5() : base( 0x5530 )
		{
			Name = "a cathedral window";
			Weight = 50;
		}

		public CathedralWindow5( Serial serial ) : base( serial )
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