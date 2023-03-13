using System;
using Server;

namespace Server.Items
{
	public class QuiverOfIce : ElvenQuiver
	{
		public override int LabelNumber{ get{ return 1073110; } } // quiver of ice
	
		[Constructable]
		public QuiverOfIce() : base()
		{
			Hue = 0x4ED;
			WeightReduction = 100;
			DamageIncrease = 50;
			LowerAmmoCost = 75;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public QuiverOfIce( Serial serial ) : base( serial )
		{
		}

		public override void AlterBowDamage( ref int phys, ref int fire, ref int cold, ref int pois, ref int nrgy, ref int chaos, ref int direct )
		{
			fire = pois = nrgy = chaos = direct = phys = 0;
			cold = 100;
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
