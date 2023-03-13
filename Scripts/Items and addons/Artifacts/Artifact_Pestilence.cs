using System;
using Server.Items;


namespace Server.Items
{
	public class Pestilence: BaseQuiver, ITokunoDyable
	{		
		[Constructable]
		public Pestilence() : base()
        {
			Name = "Pestilence";
			Hue = 1151;
            DamageIncrease = 5;
			Attributes.DefendChance = 5;
			Attributes.AttackChance = 5;
			LowerAmmoCost = 5;
			WeightReduction = 100;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public Pestilence( Serial serial ) : base( serial )
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