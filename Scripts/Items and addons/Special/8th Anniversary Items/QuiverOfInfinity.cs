using System;

namespace Server.Items
{
	public class QuiverOfInfinity : BaseQuiver, ITokunoDyable
	{
		public override int LabelNumber { get { return 1075201; } } // Quiver of Infinity

		[Constructable]
		public QuiverOfInfinity() : base()
		{
			Weight = 8.0;
			Name = "Almost Infinite Quiver";
			WeightReduction = 100;
			DamageIncrease = 50;
			LowerAmmoCost = 90; // game actually changes this ingame back to 90 if set to 100 when youfire an arrow
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public QuiverOfInfinity( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 1 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			//if( version < 1 && DamageIncrease == 0 )
			//{
			//	DamageIncrease = 50;
			//}
		}
	}
}
