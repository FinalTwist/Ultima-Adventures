using System;
using Server;


namespace Server.Items
{
	public class DivineGloves : LeatherGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061289; } } // Divine Gloves


		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 16; } }


		[Constructable]
		public DivineGloves()
		{
			Name = "Divine Gloves";
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


		public DivineGloves( Serial serial ) : base( serial )
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