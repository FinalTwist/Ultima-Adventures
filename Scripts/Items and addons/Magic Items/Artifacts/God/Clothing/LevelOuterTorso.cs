using System;

namespace Server.Items
{
	public abstract class BaseLevelOuterTorso : BaseLevelClothing
	{
		public BaseLevelOuterTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseLevelOuterTorso( int itemID, int hue ) : base( itemID, Layer.OuterTorso, hue )
		{
		}

		public BaseLevelOuterTorso( Serial serial ) : base( serial )
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
	public class LevelGildedDress : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelGildedDress() : this( 0 )
		{
		}

		[Constructable]
		public LevelGildedDress( int hue ) : base( 0x230E, hue )
		{
			Weight = 3.0;
		}

        public LevelGildedDress(Serial serial)
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
	public class LevelFancyDress : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelFancyDress() : this( 0 )
		{
		}

		[Constructable]
		public LevelFancyDress( int hue ) : base( 0x1F00, hue )
		{
			Weight = 3.0;
		}

        public LevelFancyDress(Serial serial)
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
	public class LevelRobe : BaseLevelOuterTorso, IArcaneEquip
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
		public LevelRobe() : this( 0 )
		{
		}

		[Constructable]
		public LevelRobe( int hue ) : base( 0x1F03, hue )
		{
			Weight = 3.0;
		}

        public LevelRobe(Serial serial)
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
	public class LevelHoodedShroudOfShadows : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelHoodedShroudOfShadows() : this( 0x455 )
		{
		}

		[Constructable]
		public LevelHoodedShroudOfShadows( int hue ) : base( 0x2684, hue )
		{
			LootType = LootType.Blessed;
			Weight = 3.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

        public LevelHoodedShroudOfShadows(Serial serial)
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
	public class LevelPlainDress : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelPlainDress() : this( 0 )
		{
		}

		[Constructable]
		public LevelPlainDress( int hue ) : base( 0x1F01, hue )
		{
			Weight = 2.0;
		}

        public LevelPlainDress(Serial serial)
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
	public class LevelKamishimo : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelKamishimo() : this( 0 )
		{
		}

		[Constructable]
		public LevelKamishimo( int hue ) : base( 0x2799, hue )
		{
			Weight = 3.0;
		}

        public LevelKamishimo(Serial serial)
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
	public class LevelHakamaShita : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelHakamaShita() : this( 0 )
		{
		}

		[Constructable]
		public LevelHakamaShita( int hue ) : base( 0x279C, hue )
		{
			Weight = 3.0;
		}

        public LevelHakamaShita(Serial serial)
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
	public class LevelMaleKimono : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelMaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public LevelMaleKimono( int hue ) : base( 0x2782, hue )
		{
			Weight = 3.0;
		}

		public override bool AllowFemaleWearer{ get{ return false; } }

        public LevelMaleKimono(Serial serial)
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
	public class LevelFemaleKimono : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelFemaleKimono() : this( 0 )
		{
		}

		[Constructable]
		public LevelFemaleKimono( int hue ) : base( 0x2783, hue )
		{
			Weight = 3.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

        public LevelFemaleKimono(Serial serial)
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

	[Flipable( 0x2FB9, 0x3173 )]
	public class LevelMaleElvenRobe : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelMaleElvenRobe() : this( 0 )
		{
		}

		[Constructable]
		public LevelMaleElvenRobe( int hue ) : base( 0x2FB9, hue )
		{
			Name = "robe";
			Weight = 2.0;
		}

		public override bool AllowFemaleWearer{ get{ return false; } }

        public LevelMaleElvenRobe(Serial serial)
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

	[Flipable( 0x2FBA, 0x3174 )]
	public class LevelFemaleElvenRobe : BaseLevelOuterTorso
	{
		[Constructable]
		public LevelFemaleElvenRobe() : this( 0 )
		{
		}

		[Constructable]
		public LevelFemaleElvenRobe( int hue ) : base( 0x2FBA, hue )
		{
			Name = "female robe";
			Weight = 2.0;
		}

		public override bool AllowMaleWearer{ get{ return false; } }

        public LevelFemaleElvenRobe(Serial serial)
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