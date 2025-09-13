using System;
using Server.Items;

namespace Server.Items
{
	         public class ShogunRobe :    Kamishimo  
	{
                                public override int ArtifactRarity{ get{ return 110; } }
		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		public override int AosStrReq{ get{ return 0; } }
		public override int OldStrReq{ get{ return 0; } }

                               [Constructable]
		public ShogunRobe() : base( 0x2683 )
		{
	                Name = "Shogun Robe";
			Hue = 2959;
			Layer = Layer.OuterTorso;
		
			Attributes.WeaponDamage = 35;
			Attributes.SpellDamage = 30;
			Attributes.DefendChance = 20;
                  Attributes.CastRecovery = 20;
                  Attributes.CastSpeed = 15;
			
		}

		public ShogunRobe( Serial serial ) : base( serial )
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
	