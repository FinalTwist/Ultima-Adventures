using System;
using Server;

namespace Server.Items
{
	public class IcySkinLegs : LeatherLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public IcySkinLegs()
		{
			Name = "Icy Skin Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "icy skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 0;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 2;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 2;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 3;

			PhysicalBonus = 8;
			ColdBonus = 10;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public IcySkinLegs( Serial serial ) : base( serial )
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
	public class IcySkinGloves : LeatherGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public IcySkinGloves()
		{
			Name = "Icy Skin Gloves";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "icy skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 0;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 2;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 2;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 3;

			PhysicalBonus = 8;
			ColdBonus = 10;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public IcySkinGloves( Serial serial ) : base( serial )
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
	public class IcySkinGorget : LeatherGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public IcySkinGorget()
		{
			Name = "Icy Skin Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "icy skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 0;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 2;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 2;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 3;

			PhysicalBonus = 8;
			ColdBonus = 10;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public IcySkinGorget( Serial serial ) : base( serial )
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
	public class IcySkinArms : LeatherArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public IcySkinArms()
		{
			Name = "Icy Skin Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "icy skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 0;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 2;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 2;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 3;

			PhysicalBonus = 8;
			ColdBonus = 10;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public IcySkinArms( Serial serial ) : base( serial )
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
	public class IcySkinChest : LeatherChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public IcySkinChest()
		{
			Name = "Icy Skin Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "icy skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 0;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 2;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 2;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 3;

			PhysicalBonus = 8;
			ColdBonus = 10;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public IcySkinChest( Serial serial ) : base( serial )
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
	public class IcySkinHelm : LeatherCap /////////////////////////////////////////
	{
		[Constructable]
		public IcySkinHelm()
		{
			Name = "Icy Skin Cap";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "icy skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 0;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 2;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 2;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 3;

			PhysicalBonus = 8;
			ColdBonus = 10;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public IcySkinHelm( Serial serial ) : base( serial )
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