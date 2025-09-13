using System;
using Server;


namespace Server.Items
{
	public class ArcaneLeggings : LeatherLegs
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061101; } } // Arcane Leggings 


		[Constructable]
		public ArcaneLeggings()
		{
			Name = "Arcane Leggings";
			Hue = 0x556;
			Attributes.NightSight = 1;
			Attributes.DefendChance = 10; 
			Attributes.CastSpeed = 1; 
			Attributes.LowerManaCost = 10;
			Attributes.LowerRegCost = 35; 
			Attributes.SpellDamage = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ArcaneLeggings( Serial serial ) : base( serial )
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


			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}