using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class SwiftStrike : Wakizashi, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


      [Constructable]
		public SwiftStrike()
		{
          Name = "Swift Strike";
          Hue = 2111;
		  WeaponAttributes.HitLeechHits = 25;
		  WeaponAttributes.HitLeechStam = 25;
		  WeaponAttributes.HitLowerAttack = 15;
		  WeaponAttributes.HitLowerDefend = 15;
		  Attributes.BonusDex = 3;
		  Attributes.BonusStam = 15;
		  Attributes.RegenStam = 3;
		  Attributes.WeaponDamage = 30;
		  Attributes.WeaponSpeed = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public SwiftStrike( Serial serial ) : base( serial )
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
