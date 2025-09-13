using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseGranite : Item
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}
			
			Stackable = true;
			Name = "granite";
			ItemID = 0x2158;
		}

		public override double DefaultWeight
		{
			get { return Core.ML ? 1.0 : 10.0; } // Pub 57
		}

		public BaseGranite( CraftResource resource ) : base( 0x2158 )
		{
			Hue = CraftResources.GetHue( resource );
			Stackable = true;

			m_Resource = resource;
			Name = "granite";
		}

		public BaseGranite( Serial serial ) : base( serial )
		{
		}

		public override int LabelNumber{ get{ return 1044607; } } // high quality granite

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
	}

	public class Granite : BaseGranite
	{
		[Constructable]
		public Granite() : base( CraftResource.Iron )
		{
		}

		public Granite( Serial serial ) : base( serial )
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

	public class DullCopperGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ) ); } }

		[Constructable]
		public DullCopperGranite() : base( CraftResource.DullCopper )
		{
		}

		public DullCopperGranite( Serial serial ) : base( serial )
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

	public class ShadowIronGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ) ); } }

		[Constructable]
		public ShadowIronGranite() : base( CraftResource.ShadowIron )
		{
		}

		public ShadowIronGranite( Serial serial ) : base( serial )
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

	public class CopperGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "copper", "classic", 0 ) ); } }

		[Constructable]
		public CopperGranite() : base( CraftResource.Copper )
		{
		}

		public CopperGranite( Serial serial ) : base( serial )
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

	public class BronzeGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ) ); } }

		[Constructable]
		public BronzeGranite() : base( CraftResource.Bronze )
		{
		}

		public BronzeGranite( Serial serial ) : base( serial )
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

	public class GoldGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "gold", "classic", 0 ) ); } }

		[Constructable]
		public GoldGranite() : base( CraftResource.Gold )
		{
		}

		public GoldGranite( Serial serial ) : base( serial )
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

	public class AgapiteGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ) ); } }

		[Constructable]
		public AgapiteGranite() : base( CraftResource.Agapite )
		{
		}

		public AgapiteGranite( Serial serial ) : base( serial )
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

	public class VeriteGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "verite", "classic", 0 ) ); } }

		[Constructable]
		public VeriteGranite() : base( CraftResource.Verite )
		{
		}

		public VeriteGranite( Serial serial ) : base( serial )
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

	public class ValoriteGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ) ); } }

		[Constructable]
		public ValoriteGranite() : base( CraftResource.Valorite )
		{
		}

		public ValoriteGranite( Serial serial ) : base( serial )
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

	public class ObsidianGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ) ); } }

		[Constructable]
		public ObsidianGranite() : base( CraftResource.Obsidian )
		{
		}

		public ObsidianGranite( Serial serial ) : base( serial )
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

	public class MithrilGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ) ); } }

		[Constructable]
		public MithrilGranite() : base( CraftResource.Mithril )
		{
		}

		public MithrilGranite( Serial serial ) : base( serial )
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

	public class DwarvenGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ) ); } }

		[Constructable]
		public DwarvenGranite() : base( CraftResource.Dwarven )
		{
		}

		public DwarvenGranite( Serial serial ) : base( serial )
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

	public class XormiteGranite : BaseGranite
	{
		public override int Hue{ get { return ( Server.Misc.MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ) ); } }

		[Constructable]
		public XormiteGranite() : base( CraftResource.Xormite )
		{
		}

		public XormiteGranite( Serial serial ) : base( serial )
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

	public class NepturiteGranite : BaseGranite
	{
		[Constructable]
		public NepturiteGranite() : base( CraftResource.Nepturite )
		{
		}

		public NepturiteGranite( Serial serial ) : base( serial )
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