using System;
using Server;


namespace Server.Items
{
	public class WildfireBow : ElvenCompositeLongbow
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075044; } } // Wildfire Bow


		[Constructable]
		public WildfireBow() : base()
		{
			Hue = 0x489;
			Name = "Wildfire Bow";
			
			SkillBonuses.SetValues( 0, SkillName.Archery, 10 );
			WeaponAttributes.ResistFireBonus = 25;
			
			Velocity = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = cold = pois = nrgy = chaos = direct = 0;
			fire = 100;
		}


		public WildfireBow( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.WriteEncodedInt( 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );


			int version = reader.ReadEncodedInt();
		}
	}
}
