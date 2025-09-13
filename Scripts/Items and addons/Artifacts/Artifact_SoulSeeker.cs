using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class SoulSeeker : RadiantScimitar
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075046; } } // Soul Seeker


		[Constructable]
		public SoulSeeker()
		{
			Name = "Soul Seeker";
			Hue = 0x38C;


			WeaponAttributes.HitLeechStam = 40;
			WeaponAttributes.HitLeechMana = 40;
			WeaponAttributes.HitLeechHits = 40;
			Attributes.WeaponSpeed = 60;
			Slayer = SlayerName.Repond;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			cold = 100;


			pois = fire = phys = nrgy = chaos = direct = 0;
		}


		public SoulSeeker( Serial serial ) : base( serial )
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