using System;
using Server;


namespace Server.Items
{
	public class TotemGorget : LeatherGorget
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061599; } } // Totem Gorget


		public override int BasePhysicalResistance{ get{ return 12; } }


		[Constructable]
		public TotemGorget()
		{
			Name = "Totem Gorget";
			Hue = 0x455;
			Attributes.BonusStr = 20; 
			Attributes.ReflectPhysical = 7;
			Attributes.AttackChance = 7;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TotemGorget( Serial serial ) : base( serial )
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
					break;
				}
			}
		}
	}
}