using System;
using Server;


namespace Server.Items
{
	public class TheDragonSlayer : Lance
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061248; } } // The Dragon Slayer


		[Constructable]
		public TheDragonSlayer()
		{
			Name = "Slayer of Dragons";
			Hue = 0x530;
			Slayer = SlayerName.DragonSlaying;
			Attributes.Luck = 110;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.ResistFireBonus = 20;
			WeaponAttributes.UseBestSkill = 1;
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


		public TheDragonSlayer( Serial serial ) : base( serial )
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


			if ( Slayer == SlayerName.None )
				Slayer = SlayerName.DragonSlaying;
		}
	}
}