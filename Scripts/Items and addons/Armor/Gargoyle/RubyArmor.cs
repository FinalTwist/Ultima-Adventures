using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class RubyPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public RubyPlateLegs()
		{
			Name = "Ruby Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyPlateLegs( Serial serial ) : base( serial )
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
	public class RubyPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public RubyPlateGloves()
		{
			Name = "Ruby Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyPlateGloves( Serial serial ) : base( serial )
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
	public class RubyPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public RubyPlateGorget()
		{
			Name = "Ruby Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyPlateGorget( Serial serial ) : base( serial )
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
	public class RubyPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public RubyPlateArms()
		{
			Name = "Ruby Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyPlateArms( Serial serial ) : base( serial )
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
	public class RubyPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public RubyPlateChest()
		{
			Name = "Ruby Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyPlateChest( Serial serial ) : base( serial )
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
	public class RubyFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public RubyFemalePlateChest()
		{
			Name = "Ruby Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyFemalePlateChest( Serial serial ) : base( serial )
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
	public class RubyShield : HeaterShield /////////////////////////////////////////
	{
		[Constructable]
		public RubyShield()
		{
			Name = "Ruby Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyShield( Serial serial ) : base( serial )
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
	public class RubyPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public RubyPlateHelm()
		{
			Name = "Ruby Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateRuby("armors") );
		}

		public RubyPlateHelm( Serial serial ) : base( serial )
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