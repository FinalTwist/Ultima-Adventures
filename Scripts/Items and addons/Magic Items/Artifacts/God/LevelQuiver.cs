using System;

namespace Server.Items
{
	
	public class LevelQuiver : BaseLevelQuiver
	{

		[Constructable]
		public LevelQuiver() : base()
		{
			WeightReduction = 0;
			DamageIncrease = 0;
			LowerAmmoCost = 0;
		}

        public LevelQuiver(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}
}