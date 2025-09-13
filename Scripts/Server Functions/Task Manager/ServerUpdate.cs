using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Spells;
using System.Collections.Generic;
using Server.Misc;
using System.Collections;
using System.Text;
using System.IO;
using Server.Regions;
using Server.Targeting;
using Server.Commands;
using Server.Commands.Generic;
using Server.Accounting;
using System.IO;

namespace Server.Misc
{
    class ServerUpdate
    {
		public static void UpdateMaterialColors()
		{
			if ( !Directory.Exists( "Info" ) )
				Directory.CreateDirectory( "Info" );

			if( !File.Exists( "Info/colors.set" ) )
			{
				LoggingFunctions.CreateFile( "Info/colors.set" );

				ArrayList list = new ArrayList();

				foreach ( Item item in World.Items.Values )
				{
					if ( IsColorUpdating( item ) )
						list.Add( item );
				}
				for ( int i = 0; i < list.Count; ++i )
				{
					Server.Misc.MaterialInfo.ConvertMaterial( (Item)list[i] );
				}
			}
		}

		public static bool IsColorUpdating( Item item )
		{
			if ( item is BaseArmor || 
					item is BaseWeapon || 
					item is BaseIngot || 
					item is BaseGranite || 
					item is BaseOre || 
					item is BaseHides || 
					item is BaseLeather || 
					item is BaseLog || 
					item is BaseWoodBoard || 
					item is Container || 
					item is HardScales || 
					item is RareMetals || 
					item is CaddelliteOre || 
					item is TopazIngot || 
					item is StarRubyIngot || 
					item is SpinelIngot || 
					item is SapphireIngot || 
					item is RubyIngot || 
					item is QuartzIngot || 
					item is OnyxIngot || 
					item is MarbleIngot || 
					item is JadeIngot || 
					item is IceIngot || 
					item is GarnetIngot || 
					item is EmeraldIngot || 
					item is CaddelliteIngot || 
					item is AmethystIngot || 
					item is ShinySilverIngot || 
					item is UnicornSkin || 
					item is TrollSkin || 
					item is SerpentSkin || 
					item is NightmareSkin || 
					item is DragonSkin || 
					item is DemonSkin )
				return true;

			return false;
		}
	}
}