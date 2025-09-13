using System;
using Server;


namespace Server.Items
{
	public class LeggingsOfFire : ChainLegs
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061099; } } // Leggings of Fire


		public override int BasePhysicalResistance{ get{ return 27; } }
		public override int BaseFireResistance{ get{ return 40; } }


		[Constructable]
		public LeggingsOfFire()
		{
			Name = "Leggings of Fire";
			Hue = 0x54F;
			ArmorAttributes.SelfRepair = 5;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public LeggingsOfFire( Serial serial ) : base( serial )
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


			if ( version < 1 )
			{
				if ( Hue == 0x54E )
					Hue = 0x54F;


				if ( Attributes.NightSight == 0 )
					Attributes.NightSight = 1;


				PhysicalBonus = 0;
				FireBonus = 0;
			}
		}
	}
}