using System;
using Server;


namespace Server.Items
{
	public class RobinHoodsFeatheredHat : FeatheredHat
	{
		[Constructable]
		public RobinHoodsFeatheredHat()
		{
			Hue = 0x114;
			Name = "Robin Hood's Feathered Hat";
			SkillBonuses.SetValues( 0, SkillName.Archery, 10 );
			Attributes.Luck = 20;
			Attributes.BonusDex = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RobinHoodsFeatheredHat( Serial serial ) : base( serial )
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