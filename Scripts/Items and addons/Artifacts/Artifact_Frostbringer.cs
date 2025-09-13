using System;
using Server;


namespace Server.Items
{
	public class Frostbringer : Bow
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061111; } } // Frostbringer


		[Constructable]
		public Frostbringer()
		{
			Hue = 0x4F2;
			WeaponAttributes.HitDispel = 50;
			SkillBonuses.SetValues( 0, SkillName.Archery, 15 );
			Attributes.RegenStam = 25;
			Attributes.WeaponDamage = 75;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = fire = pois = nrgy = chaos = direct = 0;
			cold = 100;
		}


		public Frostbringer( Serial serial ) : base( serial )
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