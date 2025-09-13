using System;
using Server;


namespace Server.Items
{
	public class StaffOfTheMagi : BlackStaff
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061600; } } // Staff of the Magi


		[Constructable]
		public StaffOfTheMagi()
		{
			Hue = 0x481;
			WeaponAttributes.MageWeapon = 30;
			Attributes.SpellChanneling = 1;
			Attributes.CastSpeed = 1;
			Attributes.SpellDamage = 30;
			Attributes.WeaponDamage = 100;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = fire = cold = pois = chaos = direct = 0;
			nrgy = 100;
		}


		public StaffOfTheMagi( Serial serial ) : base( serial )
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


			if ( WeaponAttributes.MageWeapon == 0 )
				WeaponAttributes.MageWeapon = 30;


			if ( ItemID == 0xDF1 )
				ItemID = 0xDF0;
		}
	}
}