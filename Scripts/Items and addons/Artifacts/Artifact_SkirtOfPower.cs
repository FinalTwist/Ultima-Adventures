using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class SkirtOfPower : Skirt
	{

		[Constructable]
		public SkirtOfPower()
		{
          Name = "Skirt Of POWER";
	      Attributes.LowerManaCost = 25;
		  Attributes.SpellDamage = 50;
		  Attributes.CastSpeed = 1;
		  Attributes.BonusInt = 10;
		  Hue = 0x816;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public SkirtOfPower( Serial serial ) : base( serial )
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
