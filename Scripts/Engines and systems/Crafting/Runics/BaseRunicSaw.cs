using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x1034, 0x1035 )]
	public class RunicSaw : BaseRunicTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			int index = CraftResources.GetIndex( Resource );

			if ( index >= 1 && index <= 8 )
				return;

			if ( !CraftResources.IsStandard( Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( Resource ) );
			}
		}

		[Constructable]
		public RunicSaw( CraftResource resource ) : base( resource, 0x1034 )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		[Constructable]
		public RunicSaw( CraftResource resource, int uses ) : base( resource, uses, 0x1034 )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		public RunicSaw( Serial serial ) : base( serial )
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

	public class AshTreeSaw : RunicSaw
	{

		[Constructable]
		public AshTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public AshTreeSaw( int uses ) : base( CraftResource.AshTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public AshTreeSaw( Serial serial ) : base( serial )
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

	public class CherryTreeSaw : RunicSaw
	{

		[Constructable]
		public CherryTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public CherryTreeSaw( int uses ) : base( CraftResource.CherryTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public CherryTreeSaw( Serial serial ) : base( serial )
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

	public class EbonyTreeSaw : RunicSaw
	{

		[Constructable]
		public EbonyTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public EbonyTreeSaw( int uses ) : base( CraftResource.EbonyTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public EbonyTreeSaw( Serial serial ) : base( serial )
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

	public class GoldenOakTreeSaw : RunicSaw
	{

		[Constructable]
		public GoldenOakTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public GoldenOakTreeSaw( int uses ) : base( CraftResource.GoldenOakTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public GoldenOakTreeSaw( Serial serial ) : base( serial )
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

	public class HickoryTreeSaw : RunicSaw
	{

		[Constructable]
		public HickoryTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public HickoryTreeSaw( int uses ) : base( CraftResource.HickoryTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public HickoryTreeSaw( Serial serial ) : base( serial )
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

	public class MahoganyTreeSaw : RunicSaw
	{

		[Constructable]
		public MahoganyTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public MahoganyTreeSaw( int uses ) : base( CraftResource.MahoganyTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public MahoganyTreeSaw( Serial serial ) : base( serial )
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

	public class OakTreeSaw : RunicSaw
	{

		[Constructable]
		public OakTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public OakTreeSaw( int uses ) : base( CraftResource.OakTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public OakTreeSaw( Serial serial ) : base( serial )
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

	public class PineTreeSaw : RunicSaw
	{

		[Constructable]
		public PineTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public PineTreeSaw( int uses ) : base( CraftResource.PineTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public PineTreeSaw( Serial serial ) : base( serial )
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

	public class RosewoodTreeSaw : RunicSaw
	{

		[Constructable]
		public RosewoodTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public RosewoodTreeSaw( int uses ) : base( CraftResource.RosewoodTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public RosewoodTreeSaw( Serial serial ) : base( serial )
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

	public class WalnutTreeSaw : RunicSaw
	{

		[Constructable]
		public WalnutTreeSaw() : this( 50 )
		{
		}		

		[Constructable]
		public WalnutTreeSaw( int uses ) : base( CraftResource.WalnutTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic saw";
		}
		public WalnutTreeSaw( Serial serial ) : base( serial )
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