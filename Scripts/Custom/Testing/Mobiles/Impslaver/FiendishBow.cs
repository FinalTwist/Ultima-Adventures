using System;
using Server;

namespace Server.Items
{
	public class FiendishBow : CompositeBow
	{
	       public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
               public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
		
                [Constructable]
		public FiendishBow()
		{
			Attributes.WeaponDamage =  70;
			Attributes.WeaponSpeed =  35;
			Attributes.SpellChanneling = 1;
			Attributes.Luck = 450;
			WeaponAttributes.HitLightning = 52;
			WeaponAttributes.ResistFireBonus = 5;
			WeaponAttributes.ResistPhysicalBonus = 20;
			WeaponAttributes.ResistColdBonus = 20;
			WeaponAttributes.SelfRepair = 20;
			Hue = 189;
			Name = "A Fiendish Bow";
		}

		

		public FiendishBow( Serial serial ) : base( serial )
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
