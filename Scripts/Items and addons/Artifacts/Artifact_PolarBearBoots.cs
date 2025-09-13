using System;
using Server;


namespace Server.Items
{
	public class PolarBearBoots : FurBoots
	{
		[Constructable]
		public PolarBearBoots()
		{
			Hue = 0x47E;
			Name = "Polar Bear Boots";
			Resistances.Cold = 30;
			Attributes.WeaponDamage = 25;
			Attributes.BonusStr = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public PolarBearBoots( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}