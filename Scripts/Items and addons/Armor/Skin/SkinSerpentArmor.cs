using System;
using Server;

namespace Server.Items
{
	public class SkinSerpentLegs : LeatherLegs ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinSerpentLegs()
		{
			Name = "Serpent Skin Leggings";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 3;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 1;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 5;
		}

		public SkinSerpentLegs( Serial serial ) : base( serial )
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
	public class SkinSerpentGloves : LeatherGloves ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinSerpentGloves()
		{
			Name = "Serpent Skin Gloves";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 3;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 1;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 5;
		}

		public SkinSerpentGloves( Serial serial ) : base( serial )
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
	public class SkinSerpentGorget : LeatherGorget ///////////////////////////////////////////////////
	{
		[Constructable]
		public SkinSerpentGorget()
		{
			Name = "Serpent Skin Gorget";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 3;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 1;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 5;
		}

		public SkinSerpentGorget( Serial serial ) : base( serial )
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
	public class SkinSerpentArms : LeatherArms ///////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinSerpentArms()
		{
			Name = "Serpent Skin Arms";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 3;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 1;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 5;
		}

		public SkinSerpentArms( Serial serial ) : base( serial )
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
	public class SkinSerpentChest : LeatherChest /////////////////////////////////////////////////////
	{
		[Constructable]
		public SkinSerpentChest()
		{
			Name = "Serpent Skin Tunic";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 3;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 1;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 5;
		}

		public SkinSerpentChest( Serial serial ) : base( serial )
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
	public class SkinSerpentHelm : LeatherCap /////////////////////////////////////////
	{
		[Constructable]
		public SkinSerpentHelm()
		{
			Name = "Serpent Skin Cap";
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );

			ArmorAttributes.DurabilityBonus = 20;
			ArmorAttributes.LowerStatReq = 0;
			ArmorAttributes.MageArmor = 1;

			Attributes.SpellDamage = 0;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 3;
			Attributes.LowerManaCost = 0;
			Attributes.LowerRegCost = 0;
			Attributes.ReflectPhysical = 0;
			Attributes.Luck = 10;
			Attributes.NightSight = 0;
			Attributes.BonusDex = 1;
			Attributes.BonusInt = 0;
			Attributes.BonusStr = 0;
			Attributes.RegenHits = 0;
			Attributes.RegenMana = 0;
			Attributes.RegenStam = 0;

			PhysicalBonus = 0;
			ColdBonus = 0;
			EnergyBonus = 0;
			FireBonus = 0;
			PoisonBonus = 5;
		}

		public SkinSerpentHelm( Serial serial ) : base( serial )
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