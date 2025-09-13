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
	public class MixtureDiseasedSlime : BaseMixture
	{
		[Constructable]
		public MixtureDiseasedSlime() : base( PotionEffect.MixtureDiseasedSlime )
		{
			Name = "slimy diseased mixture";

			SlimeName = "diseased slime";
			SlimeMagery = 0;
			SlimePoisons = 1;
			SlimeHue = 0x7D6;
			SlimePhys = 20;
			SlimeCold = 0;
			SlimeFire = 0;
			SlimePois = 80;
			SlimeEngy = 0;
			SlimeGlow = 0;
			SlimeHate = 0;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "Poison Damage" );
		}

		public MixtureDiseasedSlime( Serial serial ) : base( serial )
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