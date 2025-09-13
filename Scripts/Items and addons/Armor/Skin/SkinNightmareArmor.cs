using System;
using Server;

namespace Server.Items
{
	public class SkinNightmareLegs : LeatherLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinNightmareLegs()
		{
			Name = "Nightmare Skin Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 1;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 3;
			PoisonBonus = 0;
		}

		public SkinNightmareLegs( Serial serial ) : base( serial )
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
	public class SkinNightmareGloves : LeatherGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinNightmareGloves()
		{
			Name = "Nightmare Skin Gloves";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 1;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 3;
			PoisonBonus = 0;
		}

		public SkinNightmareGloves( Serial serial ) : base( serial )
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
	public class SkinNightmareGorget : LeatherGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinNightmareGorget()
		{
			Name = "Nightmare Skin Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 1;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 3;
			PoisonBonus = 0;
		}

		public SkinNightmareGorget( Serial serial ) : base( serial )
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
	public class SkinNightmareArms : LeatherArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinNightmareArms()
		{
			Name = "Nightmare Skin Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 1;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 3;
			PoisonBonus = 0;
		}

		public SkinNightmareArms( Serial serial ) : base( serial )
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
	public class SkinNightmareChest : LeatherChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinNightmareChest()
		{
			Name = "Nightmare Skin Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 1;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 3;
			PoisonBonus = 0;
		}

		public SkinNightmareChest( Serial serial ) : base( serial )
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
	public class SkinNightmareHelm : LeatherCap /////////////////////////////////////////
	{
		[Constructable]
		public SkinNightmareHelm()
		{
			Name = "Nightmare Skin Cap";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 10;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 0;
			Attributes.DefendChance = 0;
			Attributes.LowerManaCost = 2;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 20;
			Attributes.NightSight = 1;
			Attributes.BonusDex = 0;
			Attributes.BonusInt = 1;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 3;
			PoisonBonus = 0;
		}

		public SkinNightmareHelm( Serial serial ) : base( serial )
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