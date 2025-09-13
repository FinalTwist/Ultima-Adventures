using System;
using Server;


namespace Server.Items
{
	public class DivineCountenance : HornedTribalMask
	{
		public override int LabelNumber{ get{ return 1061289; } } // Divine Countenance


		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 25; } }


		[Constructable]
		public DivineCountenance()
		{
			Name = "Divine Countenance";
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


		public DivineCountenance( Serial serial ) : base( serial )
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
					Resistances.Physical = 0;
					Resistances.Fire = 0;
					Resistances.Cold = 0;
					Resistances.Energy = 0;
					break;
				}
			}
		}
	}
}