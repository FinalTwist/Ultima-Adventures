using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.ContextMenus;
using Server.Misc;

namespace Server.Mobiles
{
	public class DruidTree : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.DruidsGuild; } }

		[Constructable]
		public DruidTree() : base( "the druid" )
		{
			Job = JobFragment.ranger;
			Karma = Utility.RandomMinMax( 13, -45 );
			Name = NameList.RandomName( "trees" );
			Body = 312;
			Hue = 0;
			BaseSoundID = 442;
			CantWalk = true;
			Direction = Direction.East;

			SetSkill( SkillName.Herding, 80.0, 100.0 );
			SetSkill( SkillName.Camping, 80.0, 100.0 );
			SetSkill( SkillName.Cooking, 80.0, 100.0 );
			SetSkill( SkillName.Alchemy, 80.0, 100.0 );
			SetSkill( SkillName.AnimalLore, 85.0, 100.0 );
			SetSkill( SkillName.AnimalTaming, 90.0, 100.0 );
			SetSkill( SkillName.Veterinary, 90.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBDruidTree() );
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
						mobile.SendGump(new SpeechGump( "The Protectors Of The Forest", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Druid" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public virtual bool CheckResurrect( Mobile m )
		{
			return true;
		}

		private DateTime m_NextResurrect;
		private static TimeSpan ResurrectDelay = TimeSpan.FromSeconds( 2.0 );

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !m.Frozen && DateTime.UtcNow >= m_NextResurrect && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) )
			{
				if ( m.IsDeadBondedPet )
				{
					m_NextResurrect = DateTime.UtcNow + ResurrectDelay;

					if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
					{
						Say("I sense a spirt of an animal...somewhere.");
					}
					else
					{
						BaseCreature bc = m as BaseCreature;
					
						bc.PlaySound( 0x214 );
						bc.FixedEffect( 0x376A, 10, 16 );

						bc.ResurrectPet();

						Say("Rise my friend. I wish I could save every unfortunate animal.");
					}
				}
			}
		}

		public DruidTree( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
		}
	}
}