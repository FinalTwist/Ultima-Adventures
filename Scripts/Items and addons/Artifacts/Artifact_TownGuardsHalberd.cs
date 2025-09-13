using System;
using Server.Network;
using Server.Items;
using Server.Targeting;


namespace Server.Items
{
	public class TownGuardsHalberd : Halberd, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		[Constructable]
		public TownGuardsHalberd()
		{
			Name = "Guardsman Halberd";
			Hue = 1407;
			WeaponAttributes.HitLightning = 100;
			WeaponAttributes.HitLowerDefend = 40;
			Attributes.WeaponDamage = 50;
			Attributes.WeaponSpeed = 25;
			Slayer = SlayerName.Repond ;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public TownGuardsHalberd( Serial serial ) : base( serial )
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
