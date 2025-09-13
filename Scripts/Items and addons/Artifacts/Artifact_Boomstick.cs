using System;
using Server;
using Server.Items;


namespace Server.Items
{
	public class Boomstick : WildStaff
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075032; } } // Boomstick


		[Constructable]
		public Boomstick() : base()
		{
			Name = "Boomstick";
			Hue = 0x25;
			
			Attributes.SpellChanneling = 1;
			Attributes.RegenMana = 3;
			Attributes.CastSpeed = 1;
			Attributes.LowerRegCost = 20;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = fire = cold = pois = nrgy = direct = 0;
			chaos = 100;
		}


		public Boomstick( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.WriteEncodedInt( 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );


			int version = reader.ReadEncodedInt();
		}
	}
}
