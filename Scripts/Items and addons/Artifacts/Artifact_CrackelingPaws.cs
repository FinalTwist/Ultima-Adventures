using System;
using Server;


namespace Server.Items
{
	public class CrackelingPaws : ThrowingGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public CrackelingPaws()
		{
			
			Name = "Crackeling Paws";
			Hue = 0x4F2;

			SkillBonuses.SetValues( 0, SkillName.Archery, 25 );
			Attributes.BonusInt = 10;
			WeaponAttributes.HitLightning = 100;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.ResistPhysicalBonus = 3;
			WeaponAttributes.ResistColdBonus = 3;
			WeaponAttributes.ResistEnergyBonus = 3;
			WeaponAttributes.ResistFireBonus = 3;
			WeaponAttributes.ResistPoisonBonus = 3;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			nrgy = 100;


			fire = phys = pois = cold = chaos = direct = 0;
		}


		public CrackelingPaws( Serial serial ) : base( serial )
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