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
	public class ThiefGuildmaster : BaseGuildmaster
	{
		static List<Mobile> flaggedtargets = new List<Mobile>();
		public override NpcGuild NpcGuild{ get{ return NpcGuild.ThievesGuild; } }

		[Constructable]
		public ThiefGuildmaster() : base( "thief" )
		{
			Job = JobFragment.thief;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.DetectHidden, 75.0, 98.0 );
			SetSkill( SkillName.Hiding, 65.0, 88.0 );
			SetSkill( SkillName.Lockpicking, 85.0, 100.0 );
			SetSkill( SkillName.Snooping, 90.0, 100.0 );
			SetSkill( SkillName.Stealing, 90.0, 100.0 );
			SetSkill( SkillName.Fencing, 75.0, 98.0 );
			SetSkill( SkillName.Stealth, 85.0, 100.0 );
			SetSkill( SkillName.RemoveTrap, 85.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBThiefGuild() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			int color = Utility.RandomNeutralHue();
			switch ( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: AddItem( new Server.Items.Bandana( color ) ); break;
				case 1: AddItem( new Server.Items.SkullCap( color ) ); break;
				case 2: AddItem( new Server.Items.ClothCowl( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
				case 3: AddItem( new Server.Items.ClothHood( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
				case 4: AddItem( new Server.Items.FancyHood( color ) ); AddItem( new Server.Items.Cloak( color ) ); break;
			}
		}

		public static bool CheckflaggedTargets (Mobile target, bool check)
		{
			if (flaggedtargets == null)
                flaggedtargets = new List<Mobile>();

			if (check)
			{
				flaggedtargets.Add( target );
				return false;
			}
			else 
			{
				for ( int i = 0; i < flaggedtargets.Count; i++ ) // check if mobile is in list
				{			
					Mobile m = (Mobile)flaggedtargets[i];
					if (m == target) //already in the list
						return false;
				}
				
				return true; //not in the list, proceed
			}
		}

		public static void WipeFlaggedList ()
		{
			ThiefGuildmaster.flaggedtargets.Clear();

		}

		public override void SayWelcomeTo( Mobile m )
		{
			SayTo( m, 1008053 ); // Welcome to the guild! Stay to the shadows, friend.
		}

		private class JobEntry : ContextMenuEntry
		{
			private ThiefGuildmaster m_ThiefGuildmaster;
			private Mobile m_From;

			public JobEntry( ThiefGuildmaster ThiefGuildmaster, Mobile from ) : base( 2078, 3 )
			{
				m_ThiefGuildmaster = ThiefGuildmaster;
				m_From = from;
			}

			public override void OnClick()
			{
				m_ThiefGuildmaster.FindMessage( m_From );
			}
		}

        public void FindMessage( Mobile m )
        {
            if ( Deleted || !m.Alive )
                return;

			Item note = Server.Items.ThiefNote.GetMyCurrentJob( m );

			if ( note != null )
			{
				ThiefNote job = (ThiefNote)note;
				m.AddToBackpack( note );
				m.PlaySound( 0x249 );
				SayTo(m, "Hmmm...you already have a job from " + job.NoteItemPerson + ". Here is a copy if you lost it.");
			}
			else
			{
				ThiefNote task = new ThiefNote();
				Server.Items.ThiefNote.SetupNote( task, m );
				m.AddToBackpack( task );
				m.PlaySound( 0x249 );
				SayTo(m, "Here is something I think you can handle.");
            }
        }

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new JobEntry( this, from ) );
				if (from is PlayerMobile && ((PlayerMobile)from).flagged )
					list.Add( new TitheEntry( from ) );

			}

			base.AddCustomContextEntries( from, list );
		}

		private class TitheEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;

			public TitheEntry( Mobile mobile ) : base( 6198, 4 )
			{
				m_Mobile = mobile;

				Enabled = m_Mobile.Alive;
			}

			public override void OnClick()
			{
				if ( !(m_Mobile is PlayerMobile) )
					return;
				else if ( !(((PlayerMobile)m_Mobile).flagged) )
				{
					m_Mobile.SendMessage( "You remain in the shadows, you do not need our help." );
					return;
				}
				else if ( CheckflaggedTargets( m_Mobile, false ) )
				{
					((PlayerMobile)m_Mobile).flagged = false;
					m_Mobile.SendMessage( "The thief will take care to erase any evidence of your missdeeds." );
					Misc.Titles.AwardFame ( m_Mobile, -(Utility.RandomMinMax(200, 400)), true);
					CheckflaggedTargets( m_Mobile, true );
				}
				else
					m_Mobile.SendMessage( "It looks like we cannot help you any more this day." );
			}
		}

		public ThiefGuildmaster( Serial serial ) : base( serial )
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