using System;
using Server;


namespace Server.Items
{
	public class GauntletsOfNobility : RingmailGloves
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061092; } } // Gauntlets of Nobility


		public override int BasePhysicalResistance{ get{ return 18; } }
		public override int BasePoisonResistance{ get{ return 20; } }


		[Constructable]
		public GauntletsOfNobility()
		{
			Name = "Gauntlets of Nobility";
			Hue = 0x4FE;
			Attributes.BonusStr = 5;
			Attributes.RegenMana = 15;
			Attributes.WeaponDamage = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GauntletsOfNobility( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();


			if ( version < 1 )
			{
				if ( Hue == 0x562 )
					Hue = 0x4FE;


				PhysicalBonus = 0;
				PoisonBonus = 0;
			}
		}
	}
}