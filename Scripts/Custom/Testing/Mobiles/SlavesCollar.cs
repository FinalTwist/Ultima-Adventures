      //////////////////////////////////////////////////
     //////////////////////////////////////////////////
    /////       Created By: 	 Murrer.    //////////
   /////       Scripted By:	     Murrer.   //////////
  //////////////////////////////////////////////////
 //////////////////////////////////////////////////


using System;
using Server;

namespace Server.Items
{
	public class SlaveCollar : PlateGorget
	{
		
		public override int ArtifactRarity{ get{return 1; } }
            public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

        public override int AosStrReq{ get{ return 0; } }
		public override int OldStrReq{ get{ return 0; } }

		[Constructable]
		public SlaveCollar()
		{
			Weight = 4.0; 
            	Name = "Slaves Collar"; 
            	Hue = 1153;

			
			


			

		}

		public SlaveCollar( Serial serial ) : base( serial )
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
