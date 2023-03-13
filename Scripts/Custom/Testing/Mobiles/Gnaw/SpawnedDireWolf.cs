using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an dire wolf corpse" )]
	public class SpawnedDireWolf : DireWolf
	{
		[Constructable]
		public SpawnedDireWolf()
		{
			Container pack = this.Backpack;

			if ( pack != null )
			{
				pack.Delete();
			}

			NoKillAwards = true;
		}

		public SpawnedDireWolf( Serial serial ) : base( serial )
		{
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			c.Delete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			NoKillAwards = true;
		}
	}
}
