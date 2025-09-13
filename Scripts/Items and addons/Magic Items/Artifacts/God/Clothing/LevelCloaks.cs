using System;

namespace Server.Items
{
	public abstract class BaseLevelCloak : BaseLevelClothing
	{
		public BaseLevelCloak( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseLevelCloak( int itemID, int hue ) : base( itemID, Layer.Cloak, hue )
		{
		}

		public BaseLevelCloak( Serial serial ) : base( serial )
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
	public class LevelCloak : BaseLevelCloak, IArcaneEquip
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
				ItemID = 0x26AD;
			else if ( ItemID == 0x26AD )
				ItemID = 0x1515;

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
			if ( ItemID == 0x1515 )
				ItemID = 0x1530;
			else if ( ItemID == 0x1530 )
				ItemID = 0x1515;
		}
		#endregion

		[Constructable]
		public LevelCloak() : this( 0 )
		{
		}

		[Constructable]
		public LevelCloak( int hue ) : base( 0x1515, hue )
		{
			Weight = 5.0;
		}

        public LevelCloak(Serial serial)
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

			if ( Weight == 4.0 )
				Weight = 5.0;
		}
	}

	[Flipable( 0x230A, 0x2309 )]
	public class LevelFurCape : BaseLevelCloak
	{
		[Constructable]
		public LevelFurCape() : this( 0 )
		{
		}

		[Constructable]
		public LevelFurCape( int hue ) : base( 0x230A, hue )
		{
			Weight = 4.0;
		}

        public LevelFurCape(Serial serial)
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