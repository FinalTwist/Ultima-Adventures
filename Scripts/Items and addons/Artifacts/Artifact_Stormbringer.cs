using System;
using Server;


namespace Server.Items
{
	public class Stormbringer : VikingSword
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public Stormbringer()
		{
			Hue = 0x76B;
			Name = "Stormbringer";
			WeaponAttributes.HitLeechHits = 10;
			WeaponAttributes.HitLeechStam = 10;
			Attributes.BonusStr = 10;
			DamageLevel = WeaponDamageLevel.Vanq;
            Slayer = SlayerName.Repond;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Elric's Lost Sword" );
        }


		public Stormbringer( Serial serial ) : base( serial )
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