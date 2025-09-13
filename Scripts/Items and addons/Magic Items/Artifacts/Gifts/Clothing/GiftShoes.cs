using System;

namespace Server.Items
{
	public abstract class BaseGiftShoes : BaseGiftClothing
	{
		public BaseGiftShoes( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftShoes( int itemID, int hue ) : base( itemID, Layer.Shoes, hue )
		{
		}

		public BaseGiftShoes( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2: break; // empty, resource removed
				case 1:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case 0:
				{
					m_Resource = DefaultResource;
					break;
				}
			}
		}
	}

	[Flipable( 0x2307, 0x2308 )]
	public class GiftFurBoots : BaseGiftShoes
	{
		[Constructable]
		public GiftFurBoots() : this( 0 )
		{
		}

		[Constructable]
		public GiftFurBoots( int hue ) : base( 0x2307, hue )
		{
			Weight = 3.0;
		}

        public GiftFurBoots(Serial serial)
            : base(serial)
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

	[FlipableAttribute( 0x170b, 0x170c )]
	public class GiftBoots : BaseGiftShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public GiftBoots() : this( 0 )
		{
		}

		[Constructable]
		public GiftBoots( int hue ) : base( 0x170B, hue )
		{
			Weight = 3.0;
		}

        public GiftBoots(Serial serial)
            : base(serial)
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

	[Flipable]
	public class GiftThighBoots : BaseGiftShoes, IArcaneEquip
	{
		#region Arcane Impl
		private int m_MaxArcaneCharges, m_CurArcaneCharges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxArcaneCharges
		{
			get{ return m_MaxArcaneCharges; }
			set{ m_MaxArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurArcaneCharges
		{
			get{ return m_CurArcaneCharges; }
			set{ m_CurArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsArcane
		{
			get{ return ( m_MaxArcaneCharges > 0 && m_CurArcaneCharges >= 0 ); }
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( IsArcane )
				LabelTo( from, 1061837, String.Format( "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ) );
		}

		public void Update()
		{
			if ( IsArcane )
				ItemID = 0x26AF;
			else if ( ItemID == 0x26AF )
				ItemID = 0x1711;

			if ( IsArcane && CurArcaneCharges == 0 )
				Hue = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( IsArcane )
				list.Add( 1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ); // arcane charges: ~1_val~ / ~2_val~
		}

		public void Flip()
		{
			if ( ItemID == 0x1711 )
				ItemID = 0x1712;
			else if ( ItemID == 0x1712 )
				ItemID = 0x1711;
		}
		#endregion

		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public GiftThighBoots() : this( 0 )
		{
		}

		[Constructable]
		public GiftThighBoots( int hue ) : base( 0x1711, hue )
		{
			Weight = 4.0;
		}

        public GiftThighBoots(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			if ( IsArcane )
			{
				writer.Write( true );
				writer.Write( (int) m_CurArcaneCharges );
				writer.Write( (int) m_MaxArcaneCharges );
			}
			else
			{
				writer.Write( false );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					if ( reader.ReadBool() )
					{
						m_CurArcaneCharges = reader.ReadInt();
						m_MaxArcaneCharges = reader.ReadInt();

						if ( Hue == 2118 )
							Hue = ArcaneGem.DefaultArcaneHue;
					}

					break;
				}
			}
		}
	}

	[FlipableAttribute( 0x170f, 0x1710 )]
	public class GiftShoes : BaseGiftShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public GiftShoes() : this( 0 )
		{
		}

		[Constructable]
		public GiftShoes( int hue ) : base( 0x170F, hue )
		{
			Weight = 2.0;
		}

        public GiftShoes(Serial serial)
            : base(serial)
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

	[FlipableAttribute( 0x170d, 0x170e )]
	public class GiftSandals : BaseGiftShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public GiftSandals() : this( 0 )
		{
		}

		[Constructable]
		public GiftSandals( int hue ) : base( 0x170D, hue )
		{
			Weight = 1.0;
		}

        public GiftSandals(Serial serial)
            : base(serial)
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
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

	[Flipable( 0x2797, 0x27E2 )]
	public class GiftNinjaTabi : BaseGiftShoes
	{
		[Constructable]
		public GiftNinjaTabi() : this( 0 )
		{
		}

		[Constructable]
		public GiftNinjaTabi( int hue ) : base( 0x2797, hue )
		{
			Weight = 2.0;
		}

        public GiftNinjaTabi(Serial serial)
            : base(serial)
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

	[Flipable( 0x2796, 0x27E1 )]
	public class GiftSamuraiTabi : BaseGiftShoes
	{
		[Constructable]
		public GiftSamuraiTabi() : this( 0 )
		{
		}

		[Constructable]
		public GiftSamuraiTabi( int hue ) : base( 0x2796, hue )
		{
			Weight = 2.0;
		}

        public GiftSamuraiTabi(Serial serial)
            : base(serial)
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

	[Flipable( 0x2796, 0x27E1 )]
	public class GiftWaraji : BaseGiftShoes
	{
		[Constructable]
		public GiftWaraji() : this( 0 )
		{
		}

		[Constructable]
		public GiftWaraji( int hue ) : base( 0x2796, hue )
		{
			Weight = 2.0;
		}

        public GiftWaraji(Serial serial)
            : base(serial)
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

	[FlipableAttribute( 0x2FC4, 0x317A )]
	public class GiftElvenBoots : BaseGiftShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public GiftElvenBoots() : this( 0 )
		{
		}

		[Constructable]
		public GiftElvenBoots( int hue ) : base( 0x2FC4, hue )
		{
			Name = "fancy boots";
			Weight = 2.0;
		}

        public GiftElvenBoots(Serial serial)
            : base(serial)
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
