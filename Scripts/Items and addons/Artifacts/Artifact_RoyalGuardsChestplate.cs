using System;
using Server;


namespace Server.Items
{
	public class RoyalGuardsChestplate : PlateChest, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 15; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 10; } }


		[Constructable]
		public RoyalGuardsChestplate()
		{
			Name = "Royal Guard's Chest Plate";
			Hue = 0x47E;
			Attributes.BonusHits = 10;
			Attributes.BonusMana = 10;
			Attributes.BonusStam = 10;
			Attributes.RegenHits = 5;
			Attributes.ReflectPhysical = 25;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RoyalGuardsChestplate( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}