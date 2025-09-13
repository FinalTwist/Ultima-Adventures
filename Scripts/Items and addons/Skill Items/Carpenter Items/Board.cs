using System;

namespace Server.Items
{
	public class BaseWoodBoard : Item, ICommodity
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get { return m_Resource; }
			set { m_Resource = value; InvalidateProperties(); }
		}

		int ICommodity.DescriptionNumber 
		{ 
			get
			{
				switch ( m_Resource )
				{
					case CraftResource.RegularWood: return 1015101;
					case CraftResource.AshTree: return 1095389;
					case CraftResource.CherryTree: return 1095390;
					case CraftResource.EbonyTree: return 1095391;
					case CraftResource.GoldenOakTree: return 1095392;
					case CraftResource.HickoryTree: return 1095393;
					case CraftResource.MahoganyTree: return 1095394;
					case CraftResource.DriftwoodTree: return 1095410;
					case CraftResource.OakTree: return 1095395;
					case CraftResource.PineTree: return 1095396;
					case CraftResource.GhostTree: return 1095512;
					case CraftResource.RosewoodTree: return 1095397;
					case CraftResource.WalnutTree: return 1095398;
					case CraftResource.PetrifiedTree: return 1095533;
					case CraftResource.ElvenTree: return 1095536;
				}

				return LabelNumber;
			} 
		}

		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public BaseWoodBoard() : this( 1 )
		{
		}

		[Constructable]
		public BaseWoodBoard( int amount ) : this( CraftResource.RegularWood, amount )
		{
		}

		public BaseWoodBoard( Serial serial ) : base( serial )
		{
		}

		[Constructable]
		public BaseWoodBoard( CraftResource resource ) : this( resource, 1 )
		{
		}

		[Constructable]
		public BaseWoodBoard( CraftResource resource, int amount ) : base( 0x1BD7 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 0.1;
			m_Resource = resource;
			Hue = CraftResources.GetHue( resource );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !CraftResources.IsStandard( m_Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( m_Resource ) );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 );

			writer.Write( (int)m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3:
				case 2:
					{
						m_Resource = (CraftResource)reader.ReadInt();
						break;
					}
			}

			if ( Weight != 0.1 )
				Weight = 0.1;

			if ( version <= 1 )
				m_Resource = CraftResource.RegularWood;

			ItemID = 0x1BD7;
		}
	}
	public class Board : BaseWoodBoard
	{
		[Constructable]
		public Board() : this( 1 )
		{
		}

		[Constructable]
		public Board( int amount ) : base( CraftResource.RegularWood, amount )
		{
		}

		public Board( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class AshBoard : BaseWoodBoard
	{
		[Constructable]
		public AshBoard() : this( 1 )
		{
		}

		[Constructable]
		public AshBoard( int amount ) : base( CraftResource.AshTree, amount )
		{
		}

		public AshBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CherryBoard : BaseWoodBoard
	{
		[Constructable]
		public CherryBoard() : this( 1 )
		{
		}

		[Constructable]
		public CherryBoard( int amount ) : base( CraftResource.CherryTree, amount )
		{
		}

		public CherryBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class EbonyBoard : BaseWoodBoard
	{
		[Constructable]
		public EbonyBoard() : this( 1 )
		{
		}

		[Constructable]
		public EbonyBoard( int amount ) : base( CraftResource.EbonyTree, amount )
		{
		}

		public EbonyBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class GoldenOakBoard : BaseWoodBoard
	{
		[Constructable]
		public GoldenOakBoard() : this( 1 )
		{
		}

		[Constructable]
		public GoldenOakBoard( int amount ) : base( CraftResource.GoldenOakTree, amount )
		{
		}

		public GoldenOakBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class HickoryBoard : BaseWoodBoard
	{
		[Constructable]
		public HickoryBoard() : this( 1 )
		{
		}

		[Constructable]
		public HickoryBoard( int amount ) : base( CraftResource.HickoryTree, amount )
		{
		}

		public HickoryBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class MahoganyBoard : BaseWoodBoard
	{
		[Constructable]
		public MahoganyBoard() : this( 1 )
		{
		}

		[Constructable]
		public MahoganyBoard( int amount ) : base( CraftResource.MahoganyTree, amount )
		{
		}

		public MahoganyBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class OakBoard : BaseWoodBoard
	{
		[Constructable]
		public OakBoard() : this( 1 )
		{
		}

		[Constructable]
		public OakBoard( int amount ) : base( CraftResource.OakTree, amount )
		{
		}

		public OakBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class PineBoard : BaseWoodBoard
	{
		[Constructable]
		public PineBoard() : this( 1 )
		{
		}

		[Constructable]
		public PineBoard( int amount ) : base( CraftResource.PineTree, amount )
		{
		}

		public PineBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class RosewoodBoard : BaseWoodBoard
	{
		[Constructable]
		public RosewoodBoard() : this( 1 )
		{
		}

		[Constructable]
		public RosewoodBoard( int amount ) : base( CraftResource.RosewoodTree, amount )
		{
		}

		public RosewoodBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class WalnutBoard : BaseWoodBoard
	{
		[Constructable]
		public WalnutBoard() : this( 1 )
		{
		}

		[Constructable]
		public WalnutBoard( int amount ) : base( CraftResource.WalnutTree, amount )
		{
		}

		public WalnutBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class DriftwoodBoard : BaseWoodBoard
	{
		[Constructable]
		public DriftwoodBoard() : this( 1 )
		{
		}

		[Constructable]
		public DriftwoodBoard( int amount ) : base( CraftResource.DriftwoodTree, amount )
		{
		}

		public DriftwoodBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class GhostBoard : BaseWoodBoard
	{
		[Constructable]
		public GhostBoard() : this( 1 )
		{
		}

		[Constructable]
		public GhostBoard( int amount ) : base( CraftResource.GhostTree, amount )
		{
		}

		public GhostBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class PetrifiedBoard : BaseWoodBoard
	{
		[Constructable]
		public PetrifiedBoard() : this( 1 )
		{
		}

		[Constructable]
		public PetrifiedBoard( int amount ) : base( CraftResource.PetrifiedTree, amount )
		{
		}

		public PetrifiedBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class ElvenBoard : BaseWoodBoard
	{
		[Constructable]
		public ElvenBoard() : this( 1 )
		{
		}

		[Constructable]
		public ElvenBoard( int amount ) : base( CraftResource.ElvenTree, amount )
		{
		}

		public ElvenBoard( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}