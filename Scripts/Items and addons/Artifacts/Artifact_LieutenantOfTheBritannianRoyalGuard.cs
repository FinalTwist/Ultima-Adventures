using System;
using Server;


namespace Server.Items
{
	public class LieutenantOfTheBritannianRoyalGuard : BodySash
	{
		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }


		public override bool CanFortify{ get{ return true; } }


		[Constructable]
		public LieutenantOfTheBritannianRoyalGuard()
		{
			Name = "Royal Guard Sash";
			Hue = 0xe8;


			Attributes.BonusInt = 15;
			Attributes.RegenMana = 5;
			Attributes.LowerManaCost = 10;
			Attributes.WeaponDamage = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public LieutenantOfTheBritannianRoyalGuard( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}
