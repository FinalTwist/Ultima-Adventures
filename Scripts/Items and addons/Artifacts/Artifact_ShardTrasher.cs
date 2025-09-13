using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	public class ShardThrasher : DiamondMace
	{
		public override int LabelNumber{ get{ return 1072918; } } // Shard Thrasher


		[Constructable]
		public ShardThrasher()
		{
			Hue = 0x4F2;
			Name = "Shard Thrasher";


			WeaponAttributes.HitPhysicalArea = 30;
			Attributes.BonusStam = 8;
			Attributes.AttackChance = 10;
			Attributes.WeaponSpeed = 35;
			Attributes.WeaponDamage = 40;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ShardThrasher( Serial serial ) : base( serial )
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