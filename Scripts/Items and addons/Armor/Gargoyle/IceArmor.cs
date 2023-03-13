using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class IcePlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public IcePlateLegs()
		{
			Name = "Ice Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IcePlateLegs( Serial serial ) : base( serial )
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
	public class IcePlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public IcePlateGloves()
		{
			Name = "Ice Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IcePlateGloves( Serial serial ) : base( serial )
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
	public class IcePlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public IcePlateGorget()
		{
			Name = "Ice Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IcePlateGorget( Serial serial ) : base( serial )
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
	public class IcePlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public IcePlateArms()
		{
			Name = "Ice Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IcePlateArms( Serial serial ) : base( serial )
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
	public class IcePlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public IcePlateChest()
		{
			Name = "Ice Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IcePlateChest( Serial serial ) : base( serial )
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
	public class IceFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public IceFemalePlateChest()
		{
			Name = "Ice Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IceFemalePlateChest( Serial serial ) : base( serial )
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
	public class IceShield : HeaterShield /////////////////////////////////////////////////////////
	{
		[Constructable]
		public IceShield()
		{
			Name = "Ice Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IceShield( Serial serial ) : base( serial )
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
	public class IcePlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public IcePlateHelm()
		{
			Name = "Ice Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ice", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateIce("armors") );
		}

		public IcePlateHelm( Serial serial ) : base( serial )
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