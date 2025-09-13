using System;
using Server;

namespace Server.Items
{
	public class OrcSoul : SoulCosmetic
	{
		[Constructable]
		public OrcSoul() : this( 1 )
		{
		}

		[Constructable]
		public OrcSoul( int amount ) : base(amount)
		{
			Body = 17;
			BaseSoundID = 0x45A;
		}

		public OrcSoul( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			list.Add( 1060658, "{0}\t{1}", "Orc Soul", "(Cosmetic)" );  // ~1_val~: ~2_val~
		}
	}
}