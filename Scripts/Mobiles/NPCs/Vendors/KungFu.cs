using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using System.Collections;
using Server.Multis;
using Server.Guilds;

namespace Server.Mobiles
{
	public class KungFu : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		
		[Constructable]
		public KungFu() : base( "the Monk" )
		{
			Job = JobFragment.monk;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Bushido, 64.0, 85.0 );
			SetSkill( SkillName.Fencing, 64.0, 80.0 );
			SetSkill( SkillName.Macing, 64.0, 80.0 );
			SetSkill( SkillName.Ninjitsu, 60.0, 80.0 );
			SetSkill( SkillName.Parry, 64.0, 80.0 );
			SetSkill( SkillName.Tactics, 64.0, 85.0 );
			SetSkill( SkillName.Swords, 64.0, 85.0 );
		}
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBKungFu() ); 
			m_SBInfos.Add( new SBBuyArtifacts() );
		}
		public override void InitOutfit()
		{
			AddItem( new Sandals() );
			AddItem( new Robe(Utility.RandomYellowHue()) );
		}

		public KungFu( Serial serial ) : base( serial )
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