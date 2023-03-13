using System;

namespace Server.Items
{
	public abstract class BaseLevelShoes : BaseLevelClothing
	{
		public BaseLevelShoes( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseLevelShoes( int itemID, int hue ) : base( itemID, Layer.Shoes, hue )
		{
		}

		public BaseLevelShoes( Serial serial ) : base( serial )
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
	public class LevelFurBoots : BaseLevelShoes
	{
		[Constructable]
		public LevelFurBoots() : this( 0 )
		{
		}

		[Constructable]
		public LevelFurBoots( int hue ) : base( 0x2307, hue )
		{
			Weight = 3.0;
		}

        public LevelFurBoots(Serial serial)
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
	public class LevelBoots : BaseLevelShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public LevelBoots() : this( 0 )
		{
		}

		[Constructable]
		public LevelBoots( int hue ) : base( 0x170B, hue )
		{
			Weight = 3.0;
		}

        public LevelBoots(Serial serial)
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
	public class LevelThighBoots : BaseLevelShoes, IArcaneEquip
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
		public LevelThighBoots() : this( 0 )
		{
		}

		[Constructable]
		public LevelThighBoots( int hue ) : base( 0x1711, hue )
		{
			Weight = 4.0;
		}

        public LevelThighBoots(Serial serial)
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
	public class LevelShoes : BaseLevelShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public LevelShoes() : this( 0 )
		{
		}

		[Constructable]
		public LevelShoes( int hue ) : base( 0x170F, hue )
		{
			Weight = 2.0;
		}

        public LevelShoes(Serial serial)
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
	public class LevelSandals : BaseLevelShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public LevelSandals() : this( 0 )
		{
		}

		[Constructable]
		public LevelSandals( int hue ) : base( 0x170D, hue )
		{
			Weight = 1.0;
		}

        public LevelSandals(Serial serial)
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
	public class LevelNinjaTabi : BaseLevelShoes
	{
		[Constructable]
		public LevelNinjaTabi() : this( 0 )
		{
		}

		[Constructable]
		public LevelNinjaTabi( int hue ) : base( 0x2797, hue )
		{
			Weight = 2.0;
		}

        public LevelNinjaTabi(Serial serial)
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
	public class LevelSamuraiTabi : BaseLevelShoes
	{
		[Constructable]
		public LevelSamuraiTabi() : this( 0 )
		{
		}

		[Constructable]
		public LevelSamuraiTabi( int hue ) : base( 0x2796, hue )
		{
			Weight = 2.0;
		}

        public LevelSamuraiTabi(Serial serial)
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
	public class LevelWaraji : BaseLevelShoes
	{
		[Constructable]
		public LevelWaraji() : this( 0 )
		{
		}

		[Constructable]
		public LevelWaraji( int hue ) : base( 0x2796, hue )
		{
			Weight = 2.0;
		}

        public LevelWaraji(Serial serial)
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
	public class LevelElvenBoots : BaseLevelShoes
	{
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public LevelElvenBoots() : this( 0 )
		{
		}

		[Constructable]
		public LevelElvenBoots( int hue ) : base( 0x2FC4, hue )
		{
			Name = "fancy boots";
			Weight = 2.0;
		}

        public LevelElvenBoots(Serial serial)
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
