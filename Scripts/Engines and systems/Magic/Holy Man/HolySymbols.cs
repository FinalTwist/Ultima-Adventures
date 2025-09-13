using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HolyManSymbol770 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol770() : base( 770, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Patriarch Morden");
            list.Add( 1049644, "Banish");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol770( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol771 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol771() : base( 771, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Archbishop Halyrn");
            list.Add( 1049644, "Dampen Spirit");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol771( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol772 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol772() : base( 772, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Bishop Leantre");
            list.Add( 1049644, "Enchant");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol772( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol773 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol773() : base( 773, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Deacon Wilems");
            list.Add( 1049644, "Hammer of Faith");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol773( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol774 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol774() : base( 774, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Drumat the Apostle");
            list.Add( 1049644, "Heavenly Light");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol774( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol775 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol775() : base( 775, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Vincent the Priest");
            list.Add( 1049644, "Nourish");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol775( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol776 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol776() : base( 776, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Abigayl the Preacher");
            list.Add( 1049644, "Purge");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol776( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol777 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol777() : base( 777, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Cardinal Greggs");
            list.Add( 1049644, "Rebirth");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol777( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol778 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol778() : base( 778, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Father Michal");
            list.Add( 1049644, "Sacred Boon");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol778( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol779 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol779() : base( 779, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sister Tiana");
            list.Add( 1049644, "Sanctify");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol779( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol780 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol780() : base( 780, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Brother Kurklan");
            list.Add( 1049644, "Seance");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol780( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol781 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol781() : base( 781, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Edwin the Pope");
            list.Add( 1049644, "Smite");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol781( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol782 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol782() : base( 782, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Xephyn the Monk");
            list.Add( 1049644, "Touch of Life");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol782( Serial serial ) : base( serial )
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
	///////////////////////////////////////////////////////////////////////////////////////////////
	public class HolyManSymbol783 : SpellScroll
	{
		[Constructable]
		public HolyManSymbol783() : base( 783, 0xE5B )
		{
			Hue = 0xB89;
			Name = "holy symbol";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Chancellor Davis");
            list.Add( 1049644, "Trial by Fire");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This symbol once belonged to a great holy man." );
		}

		public HolyManSymbol783( Serial serial ) : base( serial )
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