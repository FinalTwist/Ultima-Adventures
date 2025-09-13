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
	public class NecroMage : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.NecromancersGuild; } }

		[Constructable]
		public NecroMage() : base( "the necromancer" )
		{

			Job = JobFragment.cashual;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.SpiritSpeak, 65.0, 88.0 );
			SetSkill( SkillName.Inscribe, 60.0, 83.0 );
			SetSkill( SkillName.Meditation, 60.0, 83.0 );
			SetSkill( SkillName.MagicResist, 65.0, 88.0 );
			SetSkill( SkillName.Necromancy, 64.0, 100.0 ); // WIZARD CHANGED WRESTLE TO NECRO
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
			SetSkill( SkillName.Forensics, 82.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBMage() );
			m_SBInfos.Add( new SBNecroMage() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( 0x497 ) );
			AddItem( new Server.Items.WizardsHat( 0x497 ) );
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
						mobile.SendGump(new SpeechGump( "Dealing With Deathly Things", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Necromancer" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public NecroMage( Serial serial ) : base( serial )
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