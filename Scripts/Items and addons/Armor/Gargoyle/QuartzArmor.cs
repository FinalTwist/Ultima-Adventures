using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class QuartzPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzPlateLegs()
		{
			Name = "Quartz Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzPlateLegs( Serial serial ) : base( serial )
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
	public class QuartzPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzPlateGloves()
		{
			Name = "Quartz Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzPlateGloves( Serial serial ) : base( serial )
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
	public class QuartzPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzPlateGorget()
		{
			Name = "Quartz Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzPlateGorget( Serial serial ) : base( serial )
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
	public class QuartzPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzPlateArms()
		{
			Name = "Quartz Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzPlateArms( Serial serial ) : base( serial )
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
	public class QuartzPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzPlateChest()
		{
			Name = "Quartz Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzPlateChest( Serial serial ) : base( serial )
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
	public class QuartzFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public QuartzFemalePlateChest()
		{
			Name = "Quartz Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzFemalePlateChest( Serial serial ) : base( serial )
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
	public class QuartzShield : HeaterShield //////////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzShield()
		{
			Name = "Quartz Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzShield( Serial serial ) : base( serial )
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
	public class QuartzPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public QuartzPlateHelm()
		{
			Name = "Quartz Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateQuartz("armors") );
		}

		public QuartzPlateHelm( Serial serial ) : base( serial )
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