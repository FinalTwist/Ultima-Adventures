using System;
using Server;

namespace Server.Items
{
	public class TheMostKnowledgePerson : BaseOuterTorso
	{
		public override int LabelNumber{ get{ return 1094893; } } // The Most Knowledge Person [Replica]

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override bool CanFortify{ get{ return true; } }

		public override bool CanBeBlessed{ get{ return true; } }

		[Constructable]
		public TheMostKnowledgePerson() : base( 0x2684 )
		{
			Hue = 0x117;
			StrRequirement = 0;

			Attributes.BonusHits = 3 + Utility.RandomMinMax( 0, 2 );
		}

		public TheMostKnowledgePerson( Serial serial ) : base( serial )
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
