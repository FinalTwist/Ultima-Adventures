using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class ArcanicRobe : Robe, ITokunoDyable
	{
      [Constructable]
		public ArcanicRobe()
		{
			Name = "Arcanic Robe";
			Hue = 1150;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 1;
			Attributes.LowerRegCost = 40;
		 	Attributes.LowerManaCost = 40;
			//Attributes.Luck = 95;
			Attributes.SpellDamage = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ArcanicRobe( Serial serial ) : base( serial )
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
