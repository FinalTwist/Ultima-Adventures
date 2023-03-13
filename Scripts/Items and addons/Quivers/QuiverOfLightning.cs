using System;
using Server;

namespace Server.Items
{
	public class QuiverOfLightning : ElvenQuiver
	{		
		public override int LabelNumber{ get{ return 1073112; } } // Quiver of Lightning
		
		[Constructable]
		public QuiverOfLightning() : base()
		{
			Hue = 0x4F9;
			WeightReduction = 100;
			DamageIncrease = 50;
			LowerAmmoCost = 75;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public QuiverOfLightning( Serial serial ) : base( serial )
		{
		}

		public override void AlterBowDamage( ref int phys, ref int fire, ref int cold, ref int pois, ref int nrgy, ref int chaos, ref int direct )
		{
			fire = cold = pois = chaos = direct = phys = 0;
			nrgy = 100;
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
