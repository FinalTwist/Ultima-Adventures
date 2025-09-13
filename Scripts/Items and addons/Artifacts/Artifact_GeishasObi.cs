using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class GeishasObi : Obi, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int BasePhysicalResistance{ get{ return 5; } } 
      
		[Constructable]
		public GeishasObi()
		{
          Name = "Geishas Obi";
          Hue = 31;
		  Attributes.BonusInt = 20;
		  Attributes.ReflectPhysical = 20;
		  SkillBonuses.SetValues( 0, SkillName.Ninjitsu, 30 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GeishasObi( Serial serial ) : base( serial )
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
