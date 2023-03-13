using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[Flipable( 0x1EB8, 0x1EB9 )]
	public class RunicTinkerTools : BaseRunicTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTinkering.CraftSystem; } }

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
		public RunicTinkerTools( CraftResource resource ) : base( resource, 0x1EB8 )
		{
			Weight = 1.0;
			Hue = CraftResources.GetHue( resource );
		}

		[Constructable]
		public RunicTinkerTools( CraftResource resource, int uses ) : base( resource, uses, 0x1EB8 )
		{
			Weight = 1.0;
			Hue = CraftResources.GetHue( resource );
		}

		public RunicTinkerTools( Serial serial ) : base( serial )
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

	public class VeriteTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public VeriteTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public VeriteTinkerTools( int uses ) : base( CraftResource.Verite )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public VeriteTinkerTools( Serial serial ) : base( serial )
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

	public class AgapiteTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public AgapiteTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public AgapiteTinkerTools( int uses ) : base( CraftResource.Agapite )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public AgapiteTinkerTools( Serial serial ) : base( serial )
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

	public class BronzeTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public BronzeTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public BronzeTinkerTools( int uses ) : base( CraftResource.Bronze )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public BronzeTinkerTools( Serial serial ) : base( serial )
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

	public class CopperTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public CopperTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public CopperTinkerTools( int uses ) : base( CraftResource.Copper )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public CopperTinkerTools( Serial serial ) : base( serial )
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

	public class DullCopperTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public DullCopperTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public DullCopperTinkerTools( int uses ) : base( CraftResource.DullCopper )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public DullCopperTinkerTools( Serial serial ) : base( serial )
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

	public class GoldTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public GoldTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public GoldTinkerTools( int uses ) : base( CraftResource.Gold )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public GoldTinkerTools( Serial serial ) : base( serial )
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

	public class ShadowIronTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public ShadowIronTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public ShadowIronTinkerTools( int uses ) : base( CraftResource.ShadowIron )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public ShadowIronTinkerTools( Serial serial ) : base( serial )
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

	public class ValoriteTinkerTools : RunicTinkerTools
	{

		[Constructable]
		public ValoriteTinkerTools() : this( 50 )
		{
		}		

		[Constructable]
		public ValoriteTinkerTools( int uses ) : base( CraftResource.Valorite )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic tinker tools";
		}
		public ValoriteTinkerTools( Serial serial ) : base( serial )
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