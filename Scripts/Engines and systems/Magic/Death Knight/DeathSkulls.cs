using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DeathKnightSkull750 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull750() : base( 750, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Saint Kargoth");
            list.Add( 1049644, "Banish");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull750( Serial serial ) : base( serial )
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
	public class DeathKnightSkull751 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull751() : base( 751, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Lord Monduiz Dephaar");
            list.Add( 1049644, "Demonic Touch");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull751( Serial serial ) : base( serial )
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
	public class DeathKnightSkull752 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull752() : base( 752, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Lady Kath of Naelex");
            list.Add( 1049644, "Devil Pact");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull752( Serial serial ) : base( serial )
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
	public class DeathKnightSkull753 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull753() : base( 753, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Prince Myrhal of Rax");
            list.Add( 1049644, "Grim Reaper");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull753( Serial serial ) : base( serial )
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
	public class DeathKnightSkull754 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull754() : base( 754, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sir Maeril of Naelax");
            list.Add( 1049644, "Hag Hand");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull754( Serial serial ) : base( serial )
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
	public class DeathKnightSkull755 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull755() : base( 755, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sir Farian of Lirtham");
            list.Add( 1049644, "Hellfire");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull755( Serial serial ) : base( serial )
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
	public class DeathKnightSkull756 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull756() : base( 756, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Lord Androma of Gara");
            list.Add( 1049644, "Lucifer's Bolt");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull756( Serial serial ) : base( serial )
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
	public class DeathKnightSkull757 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull757() : base( 757, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sir Oslan Knarren");
            list.Add( 1049644, "Orb of Orcus");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull757( Serial serial ) : base( serial )
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
	public class DeathKnightSkull758 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull758() : base( 758, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sir Rezinar of Haxx");
            list.Add( 1049644, "Shield of Hate");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull758( Serial serial ) : base( serial )
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
	public class DeathKnightSkull759 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull759() : base( 759, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Lord Thyrian of Naelax");
            list.Add( 1049644, "Soul Reaper");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull759( Serial serial ) : base( serial )
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
	public class DeathKnightSkull760 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull760() : base( 760, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sir Minar of Darmen");
            list.Add( 1049644, "Strength of Steel");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull760( Serial serial ) : base( serial )
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
	public class DeathKnightSkull761 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull761() : base( 761, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Duke Urkar of Torquann");
            list.Add( 1049644, "Strike");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull761( Serial serial ) : base( serial )
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
	public class DeathKnightSkull762 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull762() : base( 762, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Sir Luren the Boar");
            list.Add( 1049644, "Succubus Skin");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull762( Serial serial ) : base( serial )
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
	public class DeathKnightSkull763 : SpellScroll
	{
		[Constructable]
		public DeathKnightSkull763() : base( 763, 0x1AE0 )
		{
			ItemID = Utility.RandomList( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3 );
			Hue = 0xB9A;
			Name = "Death Knight Skull";
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Lord Khayven of Rax");
            list.Add( 1049644, "Wrath");
        }

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This skull is from a long dead death knight." );
		}

		public DeathKnightSkull763( Serial serial ) : base( serial )
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