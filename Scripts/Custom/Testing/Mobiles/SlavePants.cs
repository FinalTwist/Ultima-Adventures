      //////////////////////////////////////////////////
     //////////////////////////////////////////////////
    /////       Created By: 	 Murrer.    //////////
   /////       Scripted By:	     Murrer.   //////////
  //////////////////////////////////////////////////
 //////////////////////////////////////////////////


using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13cb, 0x13d2 )]
	public class SlavePants : BaseArmor
	{
		public override int ArtifactRarity{ get{ return 1; } }
		
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		public override int AosStrReq{ get{ return 0; } }
		public override int OldStrReq{ get{ return 0; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public SlavePants() : base( 0x13CB )
		{
			Name = "Slaves Pants";
			Hue = 1153;
			Weight = 4.0;
		}

		public SlavePants( Serial serial ) : base( serial )
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