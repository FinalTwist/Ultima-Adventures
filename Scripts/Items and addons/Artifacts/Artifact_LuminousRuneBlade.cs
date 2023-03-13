using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class LuminousRuneBlade : RuneBlade
	{
		public override int LabelNumber{ get{ return 1072922; } } // Luminous Rune Blade


		[Constructable]
		public LuminousRuneBlade()
		{
			Name = "Luminous Rune Blade";


			WeaponAttributes.HitLightning = 40;
			WeaponAttributes.SelfRepair = 5;
			Attributes.NightSight = 1;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 55;


			Hue = this.GetElementalDamageHue();
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


		public LuminousRuneBlade( Serial serial ) : base( serial )
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