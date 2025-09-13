using System;
using Server;


namespace Server.Items
{
	public class BoneCrusher : WarMace
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061596; } } // Bone Crusher


		[Constructable]
		public BoneCrusher()
		{
			ItemID = 0x1406;
			Hue = 0x60C;
			WeaponAttributes.HitLowerDefend = 50;
			Attributes.BonusStr = 10;
			Attributes.WeaponDamage = 75;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BoneCrusher( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();


			if ( Hue == 0x604 )
				Hue = 0x60C;


			if ( ItemID == 0x1407 )
				ItemID = 0x1406;
		}
	}
}