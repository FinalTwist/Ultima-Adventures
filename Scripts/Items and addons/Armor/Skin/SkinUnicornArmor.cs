using System;
using Server;

namespace Server.Items
{
	public class SkinUnicornLegs : LeatherLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinUnicornLegs()
		{
			Name = "Unicorn Skin Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 3;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public SkinUnicornLegs( Serial serial ) : base( serial )
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
	public class SkinUnicornGloves : LeatherGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinUnicornGloves()
		{
			Name = "Unicorn Skin Gloves";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 3;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public SkinUnicornGloves( Serial serial ) : base( serial )
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
	public class SkinUnicornGorget : LeatherGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinUnicornGorget()
		{
			Name = "Unicorn Skin Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 3;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public SkinUnicornGorget( Serial serial ) : base( serial )
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
	public class SkinUnicornArms : LeatherArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinUnicornArms()
		{
			Name = "Unicorn Skin Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 3;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public SkinUnicornArms( Serial serial ) : base( serial )
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
	public class SkinUnicornChest : LeatherChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinUnicornChest()
		{
			Name = "Unicorn Skin Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 3;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public SkinUnicornChest( Serial serial ) : base( serial )
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
	public class SkinUnicornHelm : LeatherCap /////////////////////////////////////////
	{
		[Constructable]
		public SkinUnicornHelm()
		{
			Name = "Unicorn Skin Cap";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 3;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 2;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 0;
		}

		public SkinUnicornHelm( Serial serial ) : base( serial )
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