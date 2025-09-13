using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class WarriorGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.WarriorsGuild; } }

		[Constructable]
		public WarriorGuildmaster() : base( "warrior" )
		{
			
			Job = JobFragment.weaponstrainer;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.ArmsLore, 75.0, 98.0 );
			SetSkill( SkillName.Parry, 85.0, 100.0 );
			SetSkill( SkillName.MagicResist, 60.0, 83.0 );
			SetSkill( SkillName.Tactics, 85.0, 100.0 );
			SetSkill( SkillName.Swords, 90.0, 100.0 );
			SetSkill( SkillName.Macing, 60.0, 83.0 );
			SetSkill( SkillName.Fencing, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBWeaponSmith() ); 
			SBInfos.Add( new SBLeatherArmor() );
			SBInfos.Add( new SBStuddedArmor() );
			SBInfos.Add( new SBMetalShields() );
			SBInfos.Add( new SBPlateArmor() );
			SBInfos.Add( new SBHelmetArmor() );
			SBInfos.Add( new SBChainmailArmor() );
			SBInfos.Add( new SBRingmailArmor() );
			SBInfos.Add( new SBAxeWeapon() );
			SBInfos.Add( new SBKnifeWeapon() );
			SBInfos.Add( new SBMaceWeapon() );
			SBInfos.Add( new SBPoleArmWeapon() );
			SBInfos.Add( new SBSpearForkWeapon() );
			SBInfos.Add( new SBStavesWeapon() );
			SBInfos.Add( new SBSwordWeapon() );
			SBInfos.Add( new SBGemArmor() ); 
			SBInfos.Add( new SBBuyArtifacts() );
			SBInfos.Add( new SBWarriorGuild() );
		}

		public override void InitOutfit()
		{
			AddItem( new PlateArms() );
			AddItem( new PlateChest() );
			AddItem( new PlateGloves() );
			AddItem( new PlateGorget() );
			AddItem( new PlateLegs() );

			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new PlateHelm() ); break;
				case 1: AddItem( new NorseHelm() ); break;
				case 2: AddItem( new CloseHelm() ); break;
				case 3: AddItem( new Helmet() ); break;
			}

			AddItem( new Broadsword() );
			AddItem( new MetalShield() );
		}

		public WarriorGuildmaster( Serial serial ) : base( serial )
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