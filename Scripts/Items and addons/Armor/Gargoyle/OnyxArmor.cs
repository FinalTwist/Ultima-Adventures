using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class OnyxPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxPlateLegs()
		{
			Name = "Onyx Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxPlateLegs( Serial serial ) : base( serial )
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
	public class OnyxPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxPlateGloves()
		{
			Name = "Onyx Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxPlateGloves( Serial serial ) : base( serial )
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
	public class OnyxPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxPlateGorget()
		{
			Name = "Onyx Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxPlateGorget( Serial serial ) : base( serial )
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
	public class OnyxPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxPlateArms()
		{
			Name = "Onyx Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxPlateArms( Serial serial ) : base( serial )
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
	public class OnyxPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxPlateChest()
		{
			Name = "Onyx Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxPlateChest( Serial serial ) : base( serial )
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
	public class OnyxFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public OnyxFemalePlateChest()
		{
			Name = "Onyx Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxFemalePlateChest( Serial serial ) : base( serial )
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
	public class OnyxShield : HeaterShield ////////////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxShield()
		{
			Name = "Onyx Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxShield( Serial serial ) : base( serial )
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
	public class OnyxPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public OnyxPlateHelm()
		{
			Name = "Onyx Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateOnyx("armors") );
		}

		public OnyxPlateHelm( Serial serial ) : base( serial )
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