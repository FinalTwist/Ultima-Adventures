using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class CaddellitePlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public CaddellitePlateLegs()
		{
			Name = "Caddellite Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddellitePlateLegs( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddellitePlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public CaddellitePlateGloves()
		{
			Name = "Caddellite Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddellitePlateGloves( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddellitePlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public CaddellitePlateGorget()
		{
			Name = "Caddellite Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddellitePlateGorget( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddellitePlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public CaddellitePlateArms()
		{
			Name = "Caddellite Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddellitePlateArms( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddellitePlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public CaddellitePlateChest()
		{
			Name = "Caddellite Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddellitePlateChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddelliteFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public CaddelliteFemalePlateChest()
		{
			Name = "Caddellite Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddelliteFemalePlateChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddelliteShield : HeaterShield /////////////////////////////////////////
	{
		[Constructable]
		public CaddelliteShield()
		{
			Name = "Caddellite Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddelliteShield( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	public class CaddellitePlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public CaddellitePlateHelm()
		{
			Name = "Caddellite Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "caddellite", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateCaddellite("armors") );
		}

		public CaddellitePlateHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}