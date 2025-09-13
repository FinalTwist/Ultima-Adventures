using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class PrincessIllusion : RoyalLongSkirt
	{

		[Constructable]
		public PrincessIllusion()
		{
          Name = "Princess's Illusion";
		  Attributes.BonusStr = 15;
		  Attributes.DefendChance = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public PrincessIllusion( Serial serial ) : base( serial )
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
