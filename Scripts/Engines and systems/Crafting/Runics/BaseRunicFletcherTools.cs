using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x1F2C, 0x1023 )]
	public class RunicFletcherTools : BaseRunicTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBowFletching.CraftSystem; } }

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
		public RunicFletcherTools( CraftResource resource ) : base( resource, 0x1F2C )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		[Constructable]
		public RunicFletcherTools( CraftResource resource, int uses ) : base( resource, uses, 0x1F2C )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		public RunicFletcherTools( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}


	public class AshTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public AshTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public AshTreeFletcherTools( int uses ) : base( CraftResource.AshTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public AshTreeFletcherTools( Serial serial ) : base( serial )
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

	public class CherryTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public CherryTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public CherryTreeFletcherTools( int uses ) : base( CraftResource.CherryTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public CherryTreeFletcherTools( Serial serial ) : base( serial )
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

	public class EbonyTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public EbonyTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public EbonyTreeFletcherTools( int uses ) : base( CraftResource.EbonyTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public EbonyTreeFletcherTools( Serial serial ) : base( serial )
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

	public class GoldenOakTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public GoldenOakTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public GoldenOakTreeFletcherTools( int uses ) : base( CraftResource.GoldenOakTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public GoldenOakTreeFletcherTools( Serial serial ) : base( serial )
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

	public class HickoryTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public HickoryTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public HickoryTreeFletcherTools( int uses ) : base( CraftResource.HickoryTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public HickoryTreeFletcherTools( Serial serial ) : base( serial )
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

	public class MahoganyTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public MahoganyTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public MahoganyTreeFletcherTools( int uses ) : base( CraftResource.MahoganyTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public MahoganyTreeFletcherTools( Serial serial ) : base( serial )
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

	public class OakTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public OakTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public OakTreeFletcherTools( int uses ) : base( CraftResource.OakTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public OakTreeFletcherTools( Serial serial ) : base( serial )
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

	public class PineTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public PineTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public PineTreeFletcherTools( int uses ) : base( CraftResource.PineTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public PineTreeFletcherTools( Serial serial ) : base( serial )
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

	public class RosewoodTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public RosewoodTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public RosewoodTreeFletcherTools( int uses ) : base( CraftResource.RosewoodTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public RosewoodTreeFletcherTools( Serial serial ) : base( serial )
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

	public class WalnutTreeFletcherTools : RunicFletcherTools
	{

		[Constructable]
		public WalnutTreeFletcherTools() : this( 50 )
		{
		}		

		[Constructable]
		public WalnutTreeFletcherTools( int uses ) : base( CraftResource.WalnutTree )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic fletcher tools";
		}
		public WalnutTreeFletcherTools( Serial serial ) : base( serial )
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