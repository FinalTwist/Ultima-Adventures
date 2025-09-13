using System;
using Server.Items;


namespace Server.Items
{
	public class BlightGrippedLongbow : ElvenCompositeLongbow
	{
		public override int LabelNumber{ get{ return 1072907; } } // Blight Gripped Longbow


		[Constructable]
		public BlightGrippedLongbow()
		{
			Name = "Blight Gripped Longbow";
			Hue = 0x8A4;


			WeaponAttributes.HitPoisonArea = 20;
			Attributes.RegenStam = 3;
			Attributes.NightSight = 1;
			Attributes.WeaponSpeed = 20;
			Attributes.WeaponDamage = 35;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BlightGrippedLongbow( Serial serial ) : base( serial )
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