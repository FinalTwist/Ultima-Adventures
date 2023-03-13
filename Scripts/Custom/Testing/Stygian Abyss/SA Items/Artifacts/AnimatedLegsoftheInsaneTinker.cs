using System;
using Server;

namespace Server.Items
{
public class AnimatedLegsoftheInsaneTinker : PlateLegs
	{
	
		public override int BasePhysicalResistance{ get{ return 17; } }
		public override int BaseFireResistance{ get{ return 15; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }
		
		public override int AosStrReq{ get{ return 45; } }
		public override int OldStrReq{ get{ return 45; } }

		[Constructable]
		public AnimatedLegsoftheInsaneTinker() : base( )
		{
			Name = ("Animated Legs of the Insane Tinker");
			Hue = 2310;
			Attributes.BonusDex = 5;
			Attributes.RegenStam = 2;
			Attributes.WeaponDamage = 10;
			Attributes.WeaponSpeed = 10;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public AnimatedLegsoftheInsaneTinker( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); //version
		}
	}
}