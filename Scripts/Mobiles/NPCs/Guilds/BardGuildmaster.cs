using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class BardGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.BardsGuild; } }

		[Constructable]
		public BardGuildmaster() : base( "bard" )
		{
			Job = JobFragment.bard;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Archery, 80.0, 100.0 );
			SetSkill( SkillName.Discordance, 80.0, 100.0 );
			SetSkill( SkillName.Musicianship, 80.0, 100.0 );
			SetSkill( SkillName.Peacemaking, 80.0, 100.0 );
			SetSkill( SkillName.Provocation, 80.0, 100.0 );
			SetSkill( SkillName.Swords, 80.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBBardGuild() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
			AddItem( new Server.Items.FeatheredHat( Utility.RandomNeutralHue() ) );
		}

		public BardGuildmaster( Serial serial ) : base( serial )
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