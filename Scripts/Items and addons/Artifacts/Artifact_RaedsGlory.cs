using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class RaedsGlory : WarCleaver
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075036; } } // Raed's Glory


		[Constructable]
		public RaedsGlory()
		{
			Name = "Raed's Glory";
			ItemID = 0x2D23;
			Hue = 0x1E6;


			Attributes.BonusMana = 8;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 20;


			WeaponAttributes.HitLeechHits = 40;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RaedsGlory( Serial serial ) : base( serial )
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