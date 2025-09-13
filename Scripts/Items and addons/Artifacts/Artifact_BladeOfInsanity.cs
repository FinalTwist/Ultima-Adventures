using System;
using Server;


namespace Server.Items
{
	public class BladeOfInsanity : Katana
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061088; } } // Blade of Insanity


		[Constructable]
		public BladeOfInsanity()
		{
			Hue = 0x76D;
			WeaponAttributes.HitLeechStam = 100;
			Attributes.RegenStam = 2;
			Attributes.WeaponSpeed = 30;
			Attributes.WeaponDamage = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BladeOfInsanity( Serial serial ) : base( serial )
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


			if ( Hue == 0x44F )
				Hue = 0x76D;
		}
	}
}