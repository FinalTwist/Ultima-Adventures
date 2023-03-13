using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class CarpenterGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.CarpentersGuild; } }

		[Constructable]
		public CarpenterGuildmaster() : base( "carpenter" )
		{
			Job = JobFragment.carpenter;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Carpentry, 85.0, 100.0 );
			SetSkill( SkillName.Lumberjacking, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBStavesWeapon() );
			SBInfos.Add( new SBCarpenterGuild() );
			SBInfos.Add( new SBWoodenShields() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.HalfApron() );
		}

		public CarpenterGuildmaster( Serial serial ) : base( serial )
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