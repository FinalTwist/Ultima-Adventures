using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class RighteousAnger : ElvenMachete
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1075049; } } // Righteous Anger


		[Constructable]
		public RighteousAnger()
		{
			Name = "Righteous Anger";
			Hue = 0x284;


			Attributes.AttackChance = 15;
			Attributes.DefendChance = 5;
			Attributes.WeaponSpeed = 35;
			Attributes.WeaponDamage = 40;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public RighteousAnger( Serial serial ) : base( serial )
		{
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