using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class AmethystPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public AmethystPlateLegs()
		{
			Name = "Amethyst Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystPlateLegs( Serial serial ) : base( serial )
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
	public class AmethystPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public AmethystPlateGloves()
		{
			Name = "Amethyst Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystPlateGloves( Serial serial ) : base( serial )
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
	public class AmethystPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public AmethystPlateGorget()
		{
			Name = "Amethyst Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystPlateGorget( Serial serial ) : base( serial )
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
	public class AmethystPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public AmethystPlateArms()
		{
			Name = "Amethyst Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystPlateArms( Serial serial ) : base( serial )
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
	public class AmethystPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public AmethystPlateChest()
		{
			Name = "Amethyst Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystPlateChest( Serial serial ) : base( serial )
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
	public class AmethystFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public AmethystFemalePlateChest()
		{
			Name = "Amethyst Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystFemalePlateChest( Serial serial ) : base( serial )
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
	public class AmethystShield : HeaterShield /////////////////////////////////////////
	{
		[Constructable]
		public AmethystShield()
		{
			Name = "Amethyst Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystShield( Serial serial ) : base( serial )
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
	public class AmethystPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public AmethystPlateHelm()
		{
			Name = "Amethyst Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "amethyst", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateAmethyst("armors") );
		}

		public AmethystPlateHelm( Serial serial ) : base( serial )
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