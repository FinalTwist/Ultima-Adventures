using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class JadePlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public JadePlateLegs()
		{
			Name = "Jade Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadePlateLegs( Serial serial ) : base( serial )
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
	public class JadePlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public JadePlateGloves()
		{
			Name = "Jade Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadePlateGloves( Serial serial ) : base( serial )
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
	public class JadePlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public JadePlateGorget()
		{
			Name = "Jade Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadePlateGorget( Serial serial ) : base( serial )
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
	public class JadePlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public JadePlateArms()
		{
			Name = "Jade Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadePlateArms( Serial serial ) : base( serial )
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
	public class JadePlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public JadePlateChest()
		{
			Name = "Jade Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadePlateChest( Serial serial ) : base( serial )
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
	public class JadeFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public JadeFemalePlateChest()
		{
			Name = "Jade Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadeFemalePlateChest( Serial serial ) : base( serial )
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
	public class JadeShield : HeaterShield ////////////////////////////////////////////////////////
	{
		[Constructable]
		public JadeShield()
		{
			Name = "Jade Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadeShield( Serial serial ) : base( serial )
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
	public class JadePlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public JadePlateHelm()
		{
			Name = "Jade Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "jade", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateJade("armors") );
		}

		public JadePlateHelm( Serial serial ) : base( serial )
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