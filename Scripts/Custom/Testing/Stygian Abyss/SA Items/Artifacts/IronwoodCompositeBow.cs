using System; 
using Server; 
using Server.Mobiles;

namespace Server.Items
{
	public class IronwoodCompositeBow : CompositeBow
	{

		[Constructable]
		public IronwoodCompositeBow() : base()
		{
				
		Name = ("Ironwood Composite Bow");
		
			Hue = 1410;
			
			Slayer = SlayerName.Fey;
			WeaponAttributes.HitFireball = 40;
			WeaponAttributes.HitLowerDefend = 30;	
			Attributes.BonusDex = 5;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 45;
			Velocity = 30;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public IronwoodCompositeBow( Serial serial ) : base( serial )
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