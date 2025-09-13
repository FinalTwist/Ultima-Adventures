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
	public class MixtureSlime : BaseMixture
	{
		[Constructable]
		public MixtureSlime() : base( PotionEffect.MixtureSlime )
		{
			Name = "slimy mixture";

			SlimeName = "slime";
			SlimeMagery = 0;
			SlimePoisons = 0;
			SlimeHue = 0x8AB;
			SlimePhys = 100;
			SlimeCold = 0;
			SlimeFire = 0;
			SlimePois = 0;
			SlimeEngy = 0;
			SlimeGlow = 0;
			SlimeHate = 0;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Physical Damage" );
		}

		public MixtureSlime( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}