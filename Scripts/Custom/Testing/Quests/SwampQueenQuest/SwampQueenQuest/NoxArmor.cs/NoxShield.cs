using System;
using Server;

namespace Server.Items
{
	public class NoxShield : WoodenKiteShield
	{
		public override int LabelNumber{ get{ return 1061101; } }  
		public override int ArtifactRarity{ get{ return 11; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public NoxShield()
		{
			ItemID = 0x1B78;
			Hue = 1272;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 25;
			Attributes.CastSpeed = 1;
		}

		public NoxShield( Serial serial ) : base( serial )
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

			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}