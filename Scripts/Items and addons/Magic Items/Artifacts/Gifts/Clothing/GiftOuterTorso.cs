using System;

namespace Server.Items
{
	public abstract class BaseGiftOuterTorso : BaseGiftClothing
	{
		public BaseGiftOuterTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseGiftOuterTorso( int itemID, int hue ) : base( itemID, Layer.OuterTorso, hue )
		{
		}

		public BaseGiftOuterTorso( Serial serial ) : base( serial )
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

	[Flipable( 0x230E, 0x230D )]
	public class GiftGildedDress : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftGildedDress() : this( 0 )
		{
		}

		[Constructable]
		public GiftGildedDress( int hue ) : base( 0x230E, hue )
		{
			Weight = 3.0;
		}

        public GiftGildedDress(Serial serial)
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

	[Flipable( 0x1F00, 0x1EFF )]
	public class GiftFancyDress : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftFancyDress() : this( 0 )
		{
		}

		[Constructable]
		public GiftFancyDress( int hue ) : base( 0x1F00, hue )
		{
			Weight = 3.0;
		}

        public GiftFancyDress(Serial serial)
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
	public class GiftRobe : BaseGiftOuterTorso, IArcaneEquip
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

		public void Update()
		{
			if ( IsArcane )
				ItemID = 0x26AE;
			else if ( ItemID == 0x26AE )
				ItemID = 0x1F04;

			if ( IsArcane && CurArcaneCharges == 0 )
				Hue = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( IsArcane )
				list.Add( 1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ); // arcane charges: ~1_val~ / ~2_val~
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( IsArcane )
				LabelTo( from, 1061837, String.Format( "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ) );
		}

		public void Flip()
		{
			if ( ItemID == 0x1F03 )
				ItemID = 0x1F04;
			else if ( ItemID == 0x1F04 )
				ItemID = 0x1F03;
		}
		#endregion

		[Constructable]
		public GiftRobe() : this( 0 )
		{
		}

		[Constructable]
		public GiftRobe( int hue ) : base( 0x1F03, hue )
		{
			Weight = 3.0;
		}

        public GiftRobe(Serial serial)
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

	[Flipable( 0x2684, 0x2683 )]
	public class GiftHoodedShroudOfShadows : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftHoodedShroudOfShadows() : this( 0x455 )
		{
		}

		[Constructable]
		public GiftHoodedShroudOfShadows( int hue ) : base( 0x2684, hue )
		{
			LootType = LootType.Blessed;
			Weight = 3.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

        public GiftHoodedShroudOfShadows(Serial serial)
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

	[Flipable( 0x1f01, 0x1f02 )]
	public class GiftPlainDress : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftPlainDress() : this( 0 )
		{
		}

		[Constructable]
		public GiftPlainDress( int hue ) : base( 0x1F01, hue )
		{
			Weight = 2.0;
		}

        public GiftPlainDress(Serial serial)
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

			if ( Weight == 3.0 )
				Weight = 2.0;
		}
	}

	[Flipable( 0x2799, 0x27E4 )]
	public class GiftKamishimo : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftKamishimo() : this( 0 )
		{
		}

		[Constructable]
		public GiftKamishimo( int hue ) : base( 0x2799, hue )
		{
			Weight = 3.0;
		}

        public GiftKamishimo(Serial serial)
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

	[Flipable( 0x279C, 0x27E7 )]
	public class GiftHakamaShita : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftHakamaShita() : this( 0 )
		{
		}

		[Constructable]
		public GiftHakamaShita( int hue ) : base( 0x279C, hue )
		{
			Weight = 3.0;
		}

        public GiftHakamaShita(Serial serial)
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

	[Flipable( 0x2782, 0x27CD )]
	public class GiftMaleKimono : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftMaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public GiftMaleKimono( int hue ) : base( 0x2782, hue )
		{
			Weight = 3.0;
		}

		public override bool AllowFemaleWearer{ get{ return false; } }

        public GiftMaleKimono(Serial serial)
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

	[Flipable( 0x2783, 0x27CE )]
	public class GiftFemaleKimono : BaseGiftOuterTorso
	{
		[Constructable]
		public GiftFemaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public GiftFemaleKimono( int hue ) : base( 0x2783, hue )
		{
			Weight = 3.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

        public GiftFemaleKimono(Serial serial)
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
}