using Server;
using System;
using System.Collections.Generic;
using System.Collections;
using Server.Items;
using Server.Multis;
using Server.Guilds;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class KeeperOfChivalry : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public KeeperOfChivalry() : base( "the Knight" )
		{
			Job = JobFragment.paladin;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Fencing, 75.0, 85.0 );
			SetSkill( SkillName.Macing, 75.0, 85.0 );
			SetSkill( SkillName.Swords, 75.0, 85.0 );
			SetSkill( SkillName.Chivalry, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBKeeperOfChivalry() );
		}

		public override void InitOutfit()
		{
			AddItem( new PlateArms() );
			AddItem( new PlateChest() );
			AddItem( new PlateGloves() );
			AddItem( new StuddedGorget() );
			AddItem( new PlateLegs() );

			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new PlateHelm() ); break;
				case 1: AddItem( new NorseHelm() ); break;
				case 2: AddItem( new CloseHelm() ); break;
				case 3: AddItem( new Helmet() ); break;
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0: AddItem( new BodySash( 0x482 ) ); break;
				case 1: AddItem( new Doublet( 0x482 ) ); break;
				case 2: AddItem( new Tunic( 0x482 ) ); break;
			}

			AddItem( new Broadsword() );

			Item shield = new MetalKiteShield();

			shield.Hue = Utility.RandomNondyedHue();

			AddItem( shield );

			switch ( Utility.Random( 2 ) )
			{
				case 0: AddItem( new Boots() ); break;
				case 1: AddItem( new ThighBoots() ); break;
			}

			PackGold( 100, 200 );
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
					if ( ! mobile.HasGump( typeof( SpeechGump ) ) )
					{
						mobile.SendGump(new SpeechGump( "The Road To Knighthood", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Knight" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public KeeperOfChivalry( Serial serial ) : base( serial )
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