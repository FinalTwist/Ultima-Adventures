using System;
using Server;


namespace Server.Items
{
	public class QuiverOfElements : BaseQuiver
	{
		public override int LabelNumber{ get{ return 1075040; } } // Quiver of the Elements


		[Constructable]
		public QuiverOfElements() : base()
		{
			Hue = 0xEB;
			
			WeightReduction = 100;
			DamageIncrease = 50;
			LowerAmmoCost = 75;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public QuiverOfElements( Serial serial ) : base( serial )
		{
		}


		public override void AlterBowDamage( ref int phys, ref int fire, ref int cold, ref int pois, ref int nrgy, ref int chaos, ref int direct )
		{
			chaos = direct = phys = 0;
			fire = cold = pois = nrgy = 25;
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
