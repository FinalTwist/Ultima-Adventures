using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class CircletOfTheSorceress : LeatherCap, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int BaseColdResistance{ get{ return 11; } } 
		public override int BaseEnergyResistance{ get{ return 12; } } 
		public override int BasePhysicalResistance{ get{ return 2; } } 
		public override int BasePoisonResistance{ get{ return 8; } } 
		public override int BaseFireResistance{ get{ return 12; } } 
      
      [Constructable]
		public CircletOfTheSorceress()
		{
			ItemID = Utility.RandomList( 0x2B6F, 0x3166 );
			Resource = CraftResource.None;
			Name = "Circlet Of The Sorceress";
			Hue = 2062;
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 3;
			Attributes.BonusMana = 15;
			Attributes.LowerManaCost = 6;
			Attributes.LowerRegCost = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public CircletOfTheSorceress( Serial serial ) : base( serial )
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
