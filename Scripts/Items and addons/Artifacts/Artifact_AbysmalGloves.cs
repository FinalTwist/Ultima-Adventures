using System;
using Server;
namespace Server.Items
{
	public class AbysmalGloves : LeatherGloves, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public AbysmalGloves()
		{
			Hue = 1172;
			Name = "Abysmal Gloves";
			ColdBonus = 3;
			EnergyBonus = 9;
			PhysicalBonus = 7;
			PoisonBonus = 7;
			FireBonus = 10;
			ArmorAttributes.SelfRepair = 5;
			Attributes.BonusInt = 5;
			Attributes.LowerManaCost = 5;
			Attributes.LowerRegCost = 10;
			Attributes.SpellDamage = 35;
			Attributes.RegenMana = 5;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public AbysmalGloves( Serial serial ) : base( serial )
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
