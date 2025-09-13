using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class RunicSewingKit : BaseRunicTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTailoring.CraftSystem; } }

		public override void AddNameProperty( ObjectPropertyList list )
		{
			string v = " ";

			if ( !CraftResources.IsStandard( Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( Resource );

				if ( num > 0 )
					v = String.Format( "#{0}", num );
				else
					v = CraftResources.GetName( Resource );
			}

			list.Add( 1061119, v ); // ~1_LEATHER_TYPE~ runic sewing kit
		}

		public override void OnSingleClick( Mobile from )
		{
			string v = " ";

			if ( !CraftResources.IsStandard( Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( Resource );

				if ( num > 0 )
					v = String.Format( "#{0}", num );
				else
					v = CraftResources.GetName( Resource );
			}

			LabelTo( from, 1061119, v ); // ~1_LEATHER_TYPE~ runic sewing kit
		}

		[Constructable]
		public RunicSewingKit( CraftResource resource ) : base( resource, 0x4C81 )
		{
			Name = "sewing kit";
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		[Constructable]
		public RunicSewingKit( CraftResource resource, int uses ) : base( resource, uses, 0x4C81 )
		{
			Weight = 2.0;
			Hue = CraftResources.GetHue( resource );
		}

		public RunicSewingKit( Serial serial ) : base( serial )
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
}