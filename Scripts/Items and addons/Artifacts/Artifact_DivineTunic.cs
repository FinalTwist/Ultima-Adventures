using System;
using Server;


namespace Server.Items
{
	public class DivineTunic : LeatherChest
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061289; } } // Divine Tunic


		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 24; } }


		[Constructable]
		public DivineTunic()
		{
			Name = "Divine Tunic";
			Hue = 0x482;
			Attributes.BonusInt = 7;
			Attributes.RegenMana = 3; 
			Attributes.ReflectPhysical = 10;
			Attributes.LowerManaCost = 30; 
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public DivineTunic( Serial serial ) : base( serial )
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


			switch ( version )
			{
				case 0:
				{
					PhysicalBonus = 0;
					FireBonus = 0;
					ColdBonus = 0;
					EnergyBonus = 0;
					break;
				}
			}
		}
	}
}