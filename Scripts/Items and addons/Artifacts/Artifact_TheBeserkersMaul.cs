using System;
using Server;


namespace Server.Items
{
	public class TheBeserkersMaul : Maul
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061108; } } // The Berserker's Maul


		[Constructable]
		public TheBeserkersMaul()
		{
			Name = "Berserker's Maul";
			Hue = 0x21;
			Attributes.WeaponSpeed = 75;
			Attributes.WeaponDamage = 50;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TheBeserkersMaul( Serial serial ) : base( serial )
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