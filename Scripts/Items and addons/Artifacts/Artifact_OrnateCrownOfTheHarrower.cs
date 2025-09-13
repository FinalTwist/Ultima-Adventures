using System;
using Server;


namespace Server.Items
{
	public class OrnateCrownOfTheHarrower : BoneHelm
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061095; } } // Ornate Crown of the Harrower


		public override int BasePoisonResistance{ get{ return 17; } }


		[Constructable]
		public OrnateCrownOfTheHarrower()
		{
			Name = "Ornate Crown Of The Harrower";
			Hue = 0x4F6;
			Attributes.RegenHits = 5;
			Attributes.RegenStam = 5;
			Attributes.WeaponDamage = 35;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public OrnateCrownOfTheHarrower( Serial serial ) : base( serial )
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
				if ( Hue == 0x55A )
					Hue = 0x4F6;


				PoisonBonus = 0;
			}
		}
	}
}