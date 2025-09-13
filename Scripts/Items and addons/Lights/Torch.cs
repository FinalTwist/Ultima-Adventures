using System;
using Server;

namespace Server.Items
{
	public class Torch : BaseEquipableLight
	{
		public override int LitItemID{ get { return 0xA12; } }
		public override int UnlitItemID{ get { return 0xF6B; } }

		public override int LitSound{ get { return 0x54; } }
		public override int UnlitSound{ get { return 0x4BB; } }
		
		[Constructable]
		public Torch() : base( 0xF6B )
		{
			if ( Burnout )
				Duration = TimeSpan.FromMinutes( 30 );
			else
				Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle300;
			Weight = 1.0;
		}

		public override void OnAdded(IEntity parent )
		{
			base.OnAdded( parent );
		}

		public override void Ignite()
		{
			base.Ignite();
		}

		public Torch( Serial serial ) : base( serial )
		{
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}
}