using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class SpinelPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelPlateLegs()
		{
			Name = "Spinel Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelPlateLegs( Serial serial ) : base( serial )
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
	public class SpinelPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelPlateGloves()
		{
			Name = "Spinel Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelPlateGloves( Serial serial ) : base( serial )
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
	public class SpinelPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelPlateGorget()
		{
			Name = "Spinel Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelPlateGorget( Serial serial ) : base( serial )
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
	public class SpinelPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelPlateArms()
		{
			Name = "Spinel Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelPlateArms( Serial serial ) : base( serial )
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
	public class SpinelPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelPlateChest()
		{
			Name = "Spinel Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelPlateChest( Serial serial ) : base( serial )
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
	public class SpinelFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public SpinelFemalePlateChest()
		{
			Name = "Spinel Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelFemalePlateChest( Serial serial ) : base( serial )
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
	public class SpinelShield : HeaterShield //////////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelShield()
		{
			Name = "Spinel Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelShield( Serial serial ) : base( serial )
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
	public class SpinelPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SpinelPlateHelm()
		{
			Name = "Spinel Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "spinel", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSpinel("armors") );
		}

		public SpinelPlateHelm( Serial serial ) : base( serial )
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