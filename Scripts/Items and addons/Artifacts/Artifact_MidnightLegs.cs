using System;
using Server;


namespace Server.Items
{
	public class MidnightLegs : BoneLegs
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061093; } } // Midnight Legs


		public override int BasePhysicalResistance{ get{ return 21; } }


		[Constructable]
		public MidnightLegs()
		{
			Name = "Midnight Leggings";
			Hue = 0x455;
			Attributes.SpellDamage = 35;
			Attributes.RegenMana = 5;
			ArmorAttributes.MageArmor = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public MidnightLegs( Serial serial ) : base( serial )
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
				PhysicalBonus = 0;
		}
	}
}