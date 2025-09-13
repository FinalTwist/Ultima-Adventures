using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class AssassinGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.AssassinsGuild; } }

		[Constructable]
		public AssassinGuildmaster() : base( "assassin" )
		{
			SetSkill( SkillName.DetectHidden, 75.0, 98.0 );
			SetSkill( SkillName.Hiding, 65.0, 88.0 );
			SetSkill( SkillName.Fencing, 75.0, 98.0 );
			SetSkill( SkillName.Stealth, 85.0, 100.0 );
			SetSkill( SkillName.Poisoning, 85.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBAssassin() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();


			switch ( Utility.RandomMinMax( 0, 2 ) )
			{
				case 0: AddItem( new Server.Items.ClothCowl( 2411 ) ); break;
				case 1: AddItem( new Server.Items.ClothHood( 2411 ) ); break;
				case 2: AddItem( new Server.Items.FancyHood( 2411 ) ); break;
			}


			AddItem( new Server.Items.AssassinRobe() );
			AddItem( new Server.Items.LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.Dagger() );
			AddItem( new Server.Items.ThighBoots( Utility.RandomNeutralHue() ) );
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: m_Giver.Say("Although I am the guildmaster, the guild leader is the of higher rank than I."); break;
						case 1: m_Giver.Say("I handle membership, and sell particular items that aid us in our profession."); break;
						case 2: m_Giver.Say("You may need to speak to the guild leader to find profitable tasks."); break;
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public AssassinGuildmaster( Serial serial ) : base( serial )
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