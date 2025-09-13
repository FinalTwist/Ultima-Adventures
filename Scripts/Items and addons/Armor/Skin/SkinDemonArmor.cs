using System;
using Server;

namespace Server.Items
{
	public class SkinDemonLegs : LeatherLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinDemonLegs()
		{
			Name = "Demon Skin Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 0;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 3;
			Attributes.RegenStam = 0;

			PhysicalBonus = 1;
			ColdBonus = 0;
			EnergyBonus = 1;
			FireBonus = 2;
			PoisonBonus = 0;
		}

		public SkinDemonLegs( Serial serial ) : base( serial )
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
	public class SkinDemonGloves : LeatherGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinDemonGloves()
		{
			Name = "Demon Skin Gloves";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 0;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 3;
			Attributes.RegenStam = 0;

			PhysicalBonus = 1;
			ColdBonus = 0;
			EnergyBonus = 1;
			FireBonus = 2;
			PoisonBonus = 0;
		}

		public SkinDemonGloves( Serial serial ) : base( serial )
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
	public class SkinDemonGorget : LeatherGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinDemonGorget()
		{
			Name = "Demon Skin Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 0;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 3;
			Attributes.RegenStam = 0;

			PhysicalBonus = 1;
			ColdBonus = 0;
			EnergyBonus = 1;
			FireBonus = 2;
			PoisonBonus = 0;
		}

		public SkinDemonGorget( Serial serial ) : base( serial )
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
	public class SkinDemonArms : LeatherArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinDemonArms()
		{
			Name = "Demon Skin Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 0;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 3;
			Attributes.RegenStam = 0;

			PhysicalBonus = 1;
			ColdBonus = 0;
			EnergyBonus = 1;
			FireBonus = 2;
			PoisonBonus = 0;
		}

		public SkinDemonArms( Serial serial ) : base( serial )
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
	public class SkinDemonChest : LeatherChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinDemonChest()
		{
			Name = "Demon Skin Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 0;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 3;
			Attributes.RegenStam = 0;

			PhysicalBonus = 1;
			ColdBonus = 0;
			EnergyBonus = 1;
			FireBonus = 2;
			PoisonBonus = 0;
		}

		public SkinDemonChest( Serial serial ) : base( serial )
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
	public class SkinDemonHelm : LeatherCap /////////////////////////////////////////
	{
		[Constructable]
		public SkinDemonHelm()
		{
			Name = "Demon Skin Cap";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "demon skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 50;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 2;
			Attributes.ReflectPhysical = 2;
			Attributes.Luck = 0;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 3;
			Attributes.RegenStam = 0;

			PhysicalBonus = 1;
			ColdBonus = 0;
			EnergyBonus = 1;
			FireBonus = 2;
			PoisonBonus = 0;
		}

		public SkinDemonHelm( Serial serial ) : base( serial )
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