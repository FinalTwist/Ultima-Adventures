using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class MinerGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.MinersGuild; } }

		[Constructable]
		public MinerGuildmaster() : base( "miner" )
		{
			Job = JobFragment.miner;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.ItemID, 60.0, 83.0 );
			SetSkill( SkillName.Mining, 90.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBMinerGuild() );
		}

		public MinerGuildmaster( Serial serial ) : base( serial )
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