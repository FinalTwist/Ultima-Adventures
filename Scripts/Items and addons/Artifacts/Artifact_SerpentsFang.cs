using System;
using Server;


namespace Server.Items
{
	public class SerpentsFang : Kryss
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061601; } } // Serpent's Fang


		[Constructable]
		public SerpentsFang()
		{
			ItemID = 0x1400;
			Hue = 0x488;
			WeaponAttributes.HitPoisonArea = 100;
			WeaponAttributes.ResistPoisonBonus = 30;
			Attributes.AttackChance = 25;
			Attributes.WeaponDamage = 50;
			Attributes.WeaponSpeed = 30;
			Attributes.BonusDex = 10;
			SkillBonuses.SetValues(0, SkillName.Poisoning, 10);
			AosElementDamages.Physical = 50;
			AosElementDamages.Poison = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = cold = nrgy = chaos = direct = 0;
			phys = 25;
			pois = 75;
		}


		public SerpentsFang( Serial serial ) : base( serial )
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


			if ( ItemID == 0x1401 )
				ItemID = 0x1400;
		}
	}
}