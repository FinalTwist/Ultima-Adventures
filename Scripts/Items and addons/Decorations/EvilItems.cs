using System;
using System.Collections.Generic;
using Server;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class EvilItems : Item
	{
		[Constructable]
		public EvilItems(): base()
		{
			Weight = 5.0;
			Movable = true;

			switch ( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0:	ItemID = 16382; Name = "stuffed nightmare"; Hue = 0x386; break;
				case 1:	ItemID = 2852; Name = "gothic lamp post"; Light = LightType.Circle225; break;
				case 2:	ItemID = Utility.RandomList( 0xC10, 0xC11 ); Name = "broken chair"; Hue = 0; break;
				case 3:	ItemID = Utility.RandomList( 0xC19, 0xC1A ); Name = "broken chair"; Hue = 0; break;
				case 4:	ItemID = Utility.RandomList( 0xC17, 0xC18 ); Name = "covered chair"; Hue = 0; break;
			}
		}

		public EvilItems( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0xC12, 0xC13 )]
	public class BrokenArmoire : MapleArmoire
	{
		[Constructable]
		public BrokenArmoire() : base( 0xC12 )
		{
			Name = "broken armoire";
			Weight = 10.0;
		}

		public BrokenArmoire( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0xC14, 0xC15 )]
	public class BrokenBookcase : EmptyBookcase
	{
		[Constructable]
		public BrokenBookcase() : base( 0xC14 )
		{
			Name = "ruined bookcase";
			Weight = 10.0;
		}

		public BrokenBookcase( Serial serial ) : base( serial )
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

	[Furniture]
	[Flipable( 0xC24, 0xC25 )]
	public class BrokenDrawer : Drawer
	{
		[Constructable]
		public BrokenDrawer() : base( 0xC24 )
		{
			Name = "broken dresser";
			Weight = 10.0;
		}

		public BrokenDrawer( Serial serial ) : base( serial )
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