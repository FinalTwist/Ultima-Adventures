using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class SapphirePlateLegs : PlateLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SapphirePlateLegs()
		{
			Name = "Sapphire Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphirePlateLegs( Serial serial ) : base( serial )
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
	public class SapphirePlateGloves : PlateGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SapphirePlateGloves()
		{
			Name = "Sapphire Gauntlets";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphirePlateGloves( Serial serial ) : base( serial )
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
	public class SapphirePlateGorget : PlateGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SapphirePlateGorget()
		{
			Name = "Sapphire Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphirePlateGorget( Serial serial ) : base( serial )
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
	public class SapphirePlateArms : PlateArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SapphirePlateArms()
		{
			Name = "Sapphire Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphirePlateArms( Serial serial ) : base( serial )
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
	public class SapphirePlateChest : PlateChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SapphirePlateChest()
		{
			Name = "Sapphire Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphirePlateChest( Serial serial ) : base( serial )
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
	public class SapphireFemalePlateChest : FemalePlateChest /////////////////////////////////////////
	{
		[Constructable]
		public SapphireFemalePlateChest()
		{
			Name = "Sapphire Female Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphireFemalePlateChest( Serial serial ) : base( serial )
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
	public class SapphireShield : HeaterShield ////////////////////////////////////////////////////
	{
		[Constructable]
		public SapphireShield()
		{
			Name = "Sapphire Shield";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphireShield( Serial serial ) : base( serial )
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
	public class SapphirePlateHelm : PlateHelm ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SapphirePlateHelm()
		{
			Name = "Sapphire Helm";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
			MorphingItem.MorphMyItem( this, "IGNORED", "IGNORED", "IGNORED", MorphingTemplates.TemplateSapphire("armors") );
		}

		public SapphirePlateHelm( Serial serial ) : base( serial )
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