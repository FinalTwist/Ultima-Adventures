using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class RangerGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.RangersGuild; } }

		[Constructable]
		public RangerGuildmaster() : base( "ranger" )
		{
			Job = JobFragment.ranger;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.Camping, 75.0, 98.0 );
			SetSkill( SkillName.Hiding, 75.0, 98.0 );
			SetSkill( SkillName.MagicResist, 75.0, 98.0 );
			SetSkill( SkillName.Tactics, 65.0, 88.0 );
			SetSkill( SkillName.Archery, 90.0, 100.0 );
			SetSkill( SkillName.Tracking, 90.0, 100.0 );
			SetSkill( SkillName.Stealth, 60.0, 83.0 );
			SetSkill( SkillName.Fencing, 36.0, 68.0 );
			SetSkill( SkillName.Herding, 36.0, 68.0 );
			SetSkill( SkillName.Swords, 45.0, 68.0 );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.FeatheredHat( Utility.RandomGreenHue() ) );
			AddItem( new Server.Items.Shirt( Utility.RandomGreenHue() ) );
			AddItem( new Server.Items.LongPants( Utility.RandomGreenHue() ) );
			AddItem( new Server.Items.Bow() );
			AddItem( new Server.Items.ThighBoots( Utility.RandomNeutralHue() ) );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBBowyer() );
			SBInfos.Add( new SBRangedWeapon() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
			SBInfos.Add( new SBLotsOfArrows() );
			SBInfos.Add( new SBProvisioner() ); 
		}

		public RangerGuildmaster( Serial serial ) : base( serial )
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