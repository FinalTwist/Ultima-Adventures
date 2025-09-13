using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class MasherBasher : Tetsubo
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public MasherBasher()
		{
          Name = "Masher Basher";
          Hue = 0x835;
		  WeaponAttributes.HitColdArea = 30;
		  WeaponAttributes.HitEnergyArea = 30;
		  WeaponAttributes.HitFireArea = 30;
		  WeaponAttributes.HitPhysicalArea = 30;
		  WeaponAttributes.HitPoisonArea = 30;
		  Attributes.AttackChance = 20;
		  Attributes.WeaponDamage = 50;
		  Attributes.WeaponSpeed = 30;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public MasherBasher( Serial serial ) : base( serial )
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
