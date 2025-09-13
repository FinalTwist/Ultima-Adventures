using System;
using Server.Network;
using Server.Items;


namespace Server.Items
{
	[FlipableAttribute( 0x26CE, 0x26CF )]
	public class GlassSword : VikingSword
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public GlassSword()
		{
			Name = "Sword of Shattered Hopes";
			ItemID = 0x26CE;
			Weight = 5.0;
			Hue = 91;
			WeaponAttributes.HitDispel = 25;
			Attributes.WeaponSpeed = 30;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.ResistFireBonus = 15;
			MinDamage = 11;
			MaxDamage = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1049644, "Glass Sword");
        }


		public GlassSword( Serial serial ) : base( serial )
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