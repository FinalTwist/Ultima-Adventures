using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26BF, 0x26C9 )]
	public class StandardOfChaos : DoubleBladedStaff //TODO: DoubleBladedSpear
	{

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public StandardOfChaos()
		{
			Name = ("Standard Of Chaos");
		
			Hue = 2209;
			
		WeaponAttributes.HitHarm = 30;	
		WeaponAttributes.HitFireball = 20;	
		WeaponAttributes.HitLightning = 10;
		WeaponAttributes.HitLowerDefend = 40;
		Attributes.WeaponSpeed = 30;
		Attributes.WeaponDamage = -40;
		Attributes.CastSpeed = 1;
		AosElementDamages.Chaos = 100;		
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public StandardOfChaos( Serial serial ) : base( serial )
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