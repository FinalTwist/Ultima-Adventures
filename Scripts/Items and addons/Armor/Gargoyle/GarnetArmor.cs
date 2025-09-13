using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class GarnetPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetPlateLegs()
		{
			Name = "Garnet Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetPlateLegs( Serial serial ) : base( serial )
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
	public class GarnetPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetPlateGloves()
		{
			Name = "Garnet Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetPlateGloves( Serial serial ) : base( serial )
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
	public class GarnetPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetPlateGorget()
		{
			Name = "Garnet Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetPlateGorget( Serial serial ) : base( serial )
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
	public class GarnetPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetPlateArms()
		{
			Name = "Garnet Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetPlateArms( Serial serial ) : base( serial )
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
	public class GarnetPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetPlateChest()
		{
			Name = "Garnet Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetPlateChest( Serial serial ) : base( serial )
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
	public class GarnetFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public GarnetFemalePlateChest()
		{
			Name = "Garnet Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetFemalePlateChest( Serial serial ) : base( serial )
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
	public class GarnetShield : HeaterShield //////////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetShield()
		{
			Name = "Garnet Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetShield( Serial serial ) : base( serial )
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
	public class GarnetPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public GarnetPlateHelm()
		{
			Name = "Garnet Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateGarnet("armors") );
		}

		public GarnetPlateHelm( Serial serial ) : base( serial )
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