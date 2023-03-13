using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class SilverPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SilverPlateLegs()
		{
			Name = "Silver Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverPlateLegs( Serial serial ) : base( serial )
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
	public class SilverPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SilverPlateGloves()
		{
			Name = "Silver Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverPlateGloves( Serial serial ) : base( serial )
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
	public class SilverPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SilverPlateGorget()
		{
			Name = "Silver Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverPlateGorget( Serial serial ) : base( serial )
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
	public class SilverPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SilverPlateArms()
		{
			Name = "Silver Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverPlateArms( Serial serial ) : base( serial )
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
	public class SilverPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SilverPlateChest()
		{
			Name = "Silver Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverPlateChest( Serial serial ) : base( serial )
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
	public class SilverFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public SilverFemalePlateChest()
		{
			Name = "Silver Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverFemalePlateChest( Serial serial ) : base( serial )
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
	public class SilverShield : HeaterShield //////////////////////////////////////////////////////
	{
		[Constructable]
		public SilverShield()
		{
			Name = "Silver Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverShield( Serial serial ) : base( serial )
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
	public class SilverPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SilverPlateHelm()
		{
			Name = "Silver Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "silver", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSilver("armors") );
		}

		public SilverPlateHelm( Serial serial ) : base( serial )
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