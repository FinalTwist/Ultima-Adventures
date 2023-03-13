using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class Fisherman : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.FishermensGuild; } }

		[Constructable]
		public Fisherman() : base( "the fisherman" )
		{
			Job = JobFragment.fisher;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Fishing, 75.0, 98.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBFisherman() );
			m_SBInfos.Add( new SBSailor() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.FishingPole() );
		}

		public Fisherman( Serial serial ) : base( serial )
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