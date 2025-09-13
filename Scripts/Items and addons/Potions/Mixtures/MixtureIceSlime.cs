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
	public class MixtureIceSlime : BaseMixture
	{
		[Constructable]
		public MixtureIceSlime() : base( PotionEffect.MixtureIceSlime )
		{
			Name = "slimy ice mixture";

			SlimeName = "freezing slime";
			SlimeMagery = 0;
			SlimePoisons = 0;
			SlimeHue = 0x480;
			SlimePhys = 20;
			SlimeCold = 80;
			SlimeFire = 0;
			SlimePois = 0;
			SlimeEngy = 0;
			SlimeGlow = 1;
			SlimeHate = 0;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Cold Damage" );
		}

		public MixtureIceSlime( Serial serial ) : base( serial )
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