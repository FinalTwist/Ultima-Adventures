using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;

namespace Server.Items
{
	public class LiquidIce : BaseLiquid
	{
		[Constructable]
		public LiquidIce() : base( PotionEffect.LiquidIce )
		{
			LiquidGlow = 1;
			Name = "liquid ice";
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Cold Damage" );
		}

		public LiquidIce( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}