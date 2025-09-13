using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class FisherGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.FishermensGuild; } }

		[Constructable]
		public FisherGuildmaster() : base( "mariner" )
		{
			Job = JobFragment.fisher;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Fishing, 80.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBSailorGuild() );
		}


		public override void InitOutfit()
		{
			base.InitOutfit();
			AddItem( new Server.Items.FloppyHat( Utility.RandomNeutralHue() ) );
		}


		public FisherGuildmaster( Serial serial ) : base( serial )
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