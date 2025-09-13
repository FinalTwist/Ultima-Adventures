using System;
using Server;


namespace Server.Items
{
	public class ColdSnap : PugilistMits
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public ColdSnap()
		{
			
			Name = "Cold Snap";
			Hue = 0x4F2;

			SkillBonuses.SetValues( 0, SkillName.Wrestling, 25 );
			Attributes.BonusStr = 10;
			WeaponAttributes.HitHarm = 100;
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
			cold = 100;


			fire = phys = pois = nrgy = chaos = direct = 0;
		}


		public ColdSnap( Serial serial ) : base( serial )
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