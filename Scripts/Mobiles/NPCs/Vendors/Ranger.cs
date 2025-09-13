using System;
using Server;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;

namespace Server.Mobiles
{
	public class Ranger : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.RangersGuild; } }

		[Constructable]
		public Ranger() : base( "the ranger" )
		{
			Job = JobFragment.ranger;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Camping, 65.0, 88.0 );
			SetSkill( SkillName.DetectHidden, 65.0, 88.0 );
			SetSkill( SkillName.Hiding, 45.0, 68.0 );
			SetSkill( SkillName.Archery, 65.0, 88.0 );
			SetSkill( SkillName.Tracking, 65.0, 88.0 );
			SetSkill( SkillName.Veterinary, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			Region reg = Region.Find( this.Location, this.Map );
			if ( reg.Name == "Savage Sea Docks" || reg.Name == "Serpent Sail Docks" || reg.Name == "Anchor Rock Docks" || reg.Name == "Kraken Reef Docks" || reg.Name == "the Port" )
			{
				m_SBInfos.Add( new SBSailor() );
			}
			m_SBInfos.Add( new SBRanger() );
			m_SBInfos.Add( new SBLeatherArmor() );
			m_SBInfos.Add( new SBStuddedArmor() ); 
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
						mobile.SendGump(new SpeechGump( "Camping Safely", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Ranger" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public Ranger( Serial serial ) : base( serial )
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