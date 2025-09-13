using System;

namespace Server.Items
{
	public class BigWillowTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigWillowDeed(); } }

		[Constructable]
		public BigWillowTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54D9, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54DA, 1076786 ), 0, 0, 0 );
		}

		public BigWillowTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big willow tree";
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

	public class BigWillowDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigWillowTreeAddon(); } }

		[Constructable]
		public BigWillowDeed() : base()
		{
			Name = "Big Willow Tree Addon";
		}

		public BigWillowDeed( Serial serial ) : base( serial )
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

	public class BigWillowTree2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigWillow2Deed(); } }

		[Constructable]
		public BigWillowTree2Addon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54DB, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54DC, 1076786 ), 0, 0, 0 );
		}

		public BigWillowTree2Addon( Serial serial ) : base( serial )
		{
			Name = "a big willow tree";
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

	public class BigWillow2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigWillowTree2Addon(); } }

		[Constructable]
		public BigWillow2Deed() : base()
		{
			Name = "Big Willow Tree 2 Addon";
		}

		public BigWillow2Deed( Serial serial ) : base( serial )
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

	public class SmallWillowTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new SmallWillowDeed(); } }

		[Constructable]
		public SmallWillowTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54F2, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54F3, 1076786 ), 0, 0, 0 );
		}

		public SmallWillowTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big willow tree";
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

	public class SmallWillowDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new SmallWillowTreeAddon(); } }

		[Constructable]
		public SmallWillowDeed() : base()
		{
			Name = "Big Willow Tree Addon";
		}

		public SmallWillowDeed( Serial serial ) : base( serial )
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

	public class BigOakTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigOakDeed(); } }

		[Constructable]
		public BigOakTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54DD, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54DE, 1076786 ), 0, 0, 0 );
		}

		public BigOakTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big oak tree";
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

	public class BigOakDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigOakTreeAddon(); } }

		[Constructable]
		public BigOakDeed() : base()
		{
			Name = "Big Oak Tree Addon";
		}

		public BigOakDeed( Serial serial ) : base( serial )
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

	public class SmallOakTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new SmallOakDeed(); } }

		[Constructable]
		public SmallOakTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54F6, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54F7, 1076786 ), 0, 0, 0 );
		}

		public SmallOakTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big oak tree";
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

	public class SmallOakDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new SmallOakTreeAddon(); } }

		[Constructable]
		public SmallOakDeed() : base()
		{
			Name = "Big Oak Tree Addon";
		}

		public SmallOakDeed( Serial serial ) : base( serial )
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

	public class BigOakTree2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigOak2Deed(); } }

		[Constructable]
		public BigOakTree2Addon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54DF, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54E0, 1076786 ), 0, 0, 0 );
		}

		public BigOakTree2Addon( Serial serial ) : base( serial )
		{
			Name = "a big oak tree";
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

	public class BigOak2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigOakTree2Addon(); } }

		[Constructable]
		public BigOak2Deed() : base()
		{
			Name = "Big Oak Tree 2 Addon";
		}

		public BigOak2Deed( Serial serial ) : base( serial )
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

	public class BigOakTree3Addon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigOak3Deed(); } }

		[Constructable]
		public BigOakTree3Addon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54E9, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54EA, 1076786 ), 0, 0, 0 );
		}

		public BigOakTree3Addon( Serial serial ) : base( serial )
		{
			Name = "a big oak tree";
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

	public class BigOak3Deed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigOakTree3Addon(); } }

		[Constructable]
		public BigOak3Deed() : base()
		{
			Name = "Big Oak Tree 3 Addon";
		}

		public BigOak3Deed( Serial serial ) : base( serial )
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

	public class BigPoplarTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigPoplarDeed(); } }

		[Constructable]
		public BigPoplarTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54E1, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54E2, 1076786 ), 0, 0, 0 );
		}

		public BigPoplarTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big poplar tree";
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

	public class BigPoplarDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigPoplarTreeAddon(); } }

		[Constructable]
		public BigPoplarDeed() : base()
		{
			Name = "Big Poplar Tree Addon";
		}

		public BigPoplarDeed( Serial serial ) : base( serial )
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

	public class BigPoplarTree2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigPoplar2Deed(); } }

		[Constructable]
		public BigPoplarTree2Addon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54E3, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54E4, 1076786 ), 0, 0, 0 );
		}

		public BigPoplarTree2Addon( Serial serial ) : base( serial )
		{
			Name = "a big poplar tree";
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

	public class BigPoplar2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigPoplarTree2Addon(); } }

		[Constructable]
		public BigPoplar2Deed() : base()
		{
			Name = "Big Poplar Tree Addon";
		}

		public BigPoplar2Deed( Serial serial ) : base( serial )
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

	public class BigPoplarTree3Addon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigPoplar3Deed(); } }

		[Constructable]
		public BigPoplarTree3Addon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54E5, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54E6, 1076786 ), 0, 0, 0 );
		}

		public BigPoplarTree3Addon( Serial serial ) : base( serial )
		{
			Name = "a big poplar tree";
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

	public class BigPoplar3Deed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigPoplarTree3Addon(); } }

		[Constructable]
		public BigPoplar3Deed() : base()
		{
			Name = "Big Poplar Tree Addon";
		}

		public BigPoplar3Deed( Serial serial ) : base( serial )
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

	public class BigSwampTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigSwampTreeDeed(); } }

		[Constructable]
		public BigSwampTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54EB, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54EC, 1076786 ), 0, 0, 0 );
		}

		public BigSwampTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big swamp tree";
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

	public class BigSwampTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigSwampTreeAddon(); } }

		[Constructable]
		public BigSwampTreeDeed() : base()
		{
			Name = "Big Swamp Tree Addon";
		}

		public BigSwampTreeDeed( Serial serial ) : base( serial )
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

	public class BigSwampTree2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigSwampTree2Deed(); } }

		[Constructable]
		public BigSwampTree2Addon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54EE, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54EF, 1076786 ), 0, 0, 0 );
		}

		public BigSwampTree2Addon( Serial serial ) : base( serial )
		{
			Name = "a big swamp tree";
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

	public class BigSwampTree2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigSwampTree2Addon(); } }

		[Constructable]
		public BigSwampTree2Deed() : base()
		{
			Name = "Big Swamp Tree 2 Addon";
		}

		public BigSwampTree2Deed( Serial serial ) : base( serial )
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

	public class BigTeakTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BigTeakTreeDeed(); } }

		[Constructable]
		public BigTeakTreeAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x54F0, 1076786 ), 0, 0, 0 );
			AddComponent( new LocalizedAddonComponent( 0x54F1, 1076786 ), 0, 0, 0 );
		}

		public BigTeakTreeAddon( Serial serial ) : base( serial )
		{
			Name = "a big swamp tree";
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

	public class BigTeakTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BigTeakTreeAddon(); } }

		[Constructable]
		public BigTeakTreeDeed() : base()
		{
			Name = "Big Swamp Tree 2 Addon";
		}

		public BigTeakTreeDeed( Serial serial ) : base( serial )
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
}
