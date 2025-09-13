using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class RunicSewingKit1 : BaseRunicTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTailoring.CraftSystem; } }

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
		public RunicSewingKit1( CraftResource resource ) : base( resource, 0x4C81 )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		[Constructable]
		public RunicSewingKit1( CraftResource resource, int uses ) : base( resource, uses, 0x4C81 )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		public RunicSewingKit1( Serial serial ) : base( serial )
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

			ItemID = 0x4C81;
		}
	}

	public class SpinedSewingKit : RunicSewingKit1
	{

		[Constructable]
		public SpinedSewingKit() : this( 50 )
		{
		}		

		[Constructable]
		public SpinedSewingKit( int uses ) : base( CraftResource.SpinedLeather )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic sewing kit";

		}
		public SpinedSewingKit( Serial serial ) : base( serial )
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

	public class BarbedSewingKit : RunicSewingKit1
	{

		[Constructable]
		public BarbedSewingKit() : this( 50 )
		{
		}		

		[Constructable]
		public BarbedSewingKit( int uses ) : base( CraftResource.BarbedLeather )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic sewing kit";

		}
		public BarbedSewingKit( Serial serial ) : base( serial )
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

	public class HornedSewingKit : RunicSewingKit1
	{

		[Constructable]
		public HornedSewingKit() : this( 50 )
		{
		}		

		[Constructable]
		public HornedSewingKit( int uses ) : base( CraftResource.HornedLeather )
		{
			Weight = 1.0;
			UsesRemaining = uses;
			Name = "runic sewing kit";

		}
		public HornedSewingKit( Serial serial ) : base( serial )
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