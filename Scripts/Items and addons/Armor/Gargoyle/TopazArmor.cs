using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class TopazPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public TopazPlateLegs()
		{
			Name = "Topaz Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazPlateLegs( Serial serial ) : base( serial )
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
	public class TopazPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public TopazPlateGloves()
		{
			Name = "Topaz Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazPlateGloves( Serial serial ) : base( serial )
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
	public class TopazPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public TopazPlateGorget()
		{
			Name = "Topaz Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazPlateGorget( Serial serial ) : base( serial )
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
	public class TopazPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public TopazPlateArms()
		{
			Name = "Topaz Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazPlateArms( Serial serial ) : base( serial )
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
	public class TopazPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public TopazPlateChest()
		{
			Name = "Topaz Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazPlateChest( Serial serial ) : base( serial )
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
	public class TopazFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public TopazFemalePlateChest()
		{
			Name = "Topaz Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazFemalePlateChest( Serial serial ) : base( serial )
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
	public class TopazPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public TopazPlateHelm()
		{
			Name = "Topaz Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazPlateHelm( Serial serial ) : base( serial )
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
	public class TopazShield : HeaterShield ///////////////////////////////////////////////////////
	{
		[Constructable]
		public TopazShield()
		{
			Name = "Topaz Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateTopaz("armors") );
		}

		public TopazShield( Serial serial ) : base( serial )
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