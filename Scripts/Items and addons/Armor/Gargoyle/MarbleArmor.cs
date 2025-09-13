using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class MarblePlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public MarblePlateLegs()
		{
			Name = "Marble Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarblePlateLegs( Serial serial ) : base( serial )
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
	public class MarblePlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public MarblePlateGloves()
		{
			Name = "Marble Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarblePlateGloves( Serial serial ) : base( serial )
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
	public class MarblePlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public MarblePlateGorget()
		{
			Name = "Marble Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarblePlateGorget( Serial serial ) : base( serial )
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
	public class MarblePlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public MarblePlateArms()
		{
			Name = "Marble Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarblePlateArms( Serial serial ) : base( serial )
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
	public class MarblePlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public MarblePlateChest()
		{
			Name = "Marble Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarblePlateChest( Serial serial ) : base( serial )
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
	public class MarbleFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public MarbleFemalePlateChest()
		{
			Name = "Marble Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarbleFemalePlateChest( Serial serial ) : base( serial )
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
	public class MarbleShields : HeaterShield /////////////////////////////////////////////////////////
	{
		[Constructable]
		public MarbleShields()
		{
			Name = "Marble Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarbleShields( Serial serial ) : base( serial )
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
	public class MarblePlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public MarblePlateHelm()
		{
			Name = "Marble Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "marble", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateMarble("armors") );
		}

		public MarblePlateHelm( Serial serial ) : base( serial )
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