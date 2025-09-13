using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class EmeraldPlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public EmeraldPlateLegs()
		{
			Name = "Emerald Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldPlateLegs( Serial serial ) : base( serial )
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
	public class EmeraldPlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public EmeraldPlateGloves()
		{
			Name = "Emerald Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldPlateGloves( Serial serial ) : base( serial )
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
	public class EmeraldPlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public EmeraldPlateGorget()
		{
			Name = "Emerald Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldPlateGorget( Serial serial ) : base( serial )
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
	public class EmeraldPlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public EmeraldPlateArms()
		{
			Name = "Emerald Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldPlateArms( Serial serial ) : base( serial )
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
	public class EmeraldPlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public EmeraldPlateChest()
		{
			Name = "Emerald Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldPlateChest( Serial serial ) : base( serial )
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
	public class EmeraldFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public EmeraldFemalePlateChest()
		{
			Name = "Emerald Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldFemalePlateChest( Serial serial ) : base( serial )
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
	public class EmeraldShield : HeaterShield /////////////////////////////////////////
	{
		[Constructable]
		public EmeraldShield()
		{
			Name = "Emerald Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldShield( Serial serial ) : base( serial )
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
	public class EmeraldPlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public EmeraldPlateHelm()
		{
			Name = "Emerald Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "emerald", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateEmerald("armors") );
		}

		public EmeraldPlateHelm( Serial serial ) : base( serial )
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