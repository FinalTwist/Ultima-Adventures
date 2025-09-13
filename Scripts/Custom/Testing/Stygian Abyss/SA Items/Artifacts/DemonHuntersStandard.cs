using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class DemonHuntersStandard : Spear
	{


		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public DemonHuntersStandard() : base( 0xF62 )
		{
			Name = ("Demon Hunter's Standard");
		
			Hue = 1377;	
			
			Attributes.CastSpeed = 1;			
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 50;
			WeaponAttributes.HitLeechStam = 50;
			WeaponAttributes.HitLightning = 40;	
			WeaponAttributes.HitLowerDefend = 30;
			Slayer = SlayerName.DaemonDismissal;
			
			
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public DemonHuntersStandard( Serial serial ) : base( serial )
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