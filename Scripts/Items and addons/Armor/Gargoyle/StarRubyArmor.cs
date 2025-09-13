using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class StarRubyPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public StarRubyPlateLegs()
		{
			Name = "Star Ruby Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyPlateLegs( Serial serial ) : base( serial )
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
	public class StarRubyPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public StarRubyPlateGloves()
		{
			Name = "Star Ruby Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyPlateGloves( Serial serial ) : base( serial )
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
	public class StarRubyPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public StarRubyPlateGorget()
		{
			Name = "Star Ruby Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyPlateGorget( Serial serial ) : base( serial )
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
	public class StarRubyPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public StarRubyPlateArms()
		{
			Name = "Star Ruby Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyPlateArms( Serial serial ) : base( serial )
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
	public class StarRubyPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public StarRubyPlateChest()
		{
			Name = "Star Ruby Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyPlateChest( Serial serial ) : base( serial )
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
	public class StarRubyFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public StarRubyFemalePlateChest()
		{
			Name = "Star Ruby Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyFemalePlateChest( Serial serial ) : base( serial )
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
	public class StarRubyShield : HeaterShield /////////////////////////////////////////
	{
		[Constructable]
		public StarRubyShield()
		{
			Name = "Star Ruby Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyShield( Serial serial ) : base( serial )
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
	public class StarRubyPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public StarRubyPlateHelm()
		{
			Name = "Star Ruby Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "star ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateStarRuby("armors") );
		}

		public StarRubyPlateHelm( Serial serial ) : base( serial )
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