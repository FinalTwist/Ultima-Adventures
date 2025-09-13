using System;
using System.Collections.Generic;
using Server;
using System.Collections; 
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Furtrader : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TailorsGuild; } }

		[Constructable]
		public Furtrader() : base( "the furtrader" )
		{
			Job = JobFragment.furtrader;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Camping, 55.0, 78.0 );
			SetSkill( SkillName.AnimalLore, 85.0, 100.0 );
			SetSkill( SkillName.Cooking, 45.0, 68.0 );
			SetSkill( SkillName.Tracking, 36.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBFurtrader() );
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
						mobile.SendGump(new SpeechGump( "Bears And Deer", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Furtrader" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Furtrader m_Furtrader;
			private Mobile m_From;

			public FixEntry( Furtrader Furtrader, Mobile from ) : base( 6120, 12 )
			{
				m_Furtrader = Furtrader;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Furtrader.BeginRepair( m_From );
			}
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive && !from.Blessed )
			{
				list.Add( new FixEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

        public void BeginRepair(Mobile from)
        {
            if ( Deleted || !from.Alive )
                return;

			int nCost = 10;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				SayTo(from, "Since you are begging, do you still want to hire me to repair a bearskin or deerskin cap for at least " + nCost.ToString() + " gold per durability?");
			}
			else { SayTo(from, "You want to hire me to repair a bearskin or deerskin cap at " + nCost.ToString() + " gold per durability?"); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Furtrader m_Furtrader;

            public RepairTarget(Furtrader Furtrader) : base(12, false, TargetFlags.None)
            {
                m_Furtrader = Furtrader;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BearMask && from.Backpack != null)
                {
                    BearMask ba = targeted as BearMask;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if (ba.HitPoints < ba.MaxHitPoints)
                    {
						int nCost = 10;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
							toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost;
						}
						else { toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost; }
                    }
                    else if (ba.HitPoints >= ba.MaxHitPoints)
                    {
						m_Furtrader.SayTo(from, "That does not need to be repaired.");
                    }
					else
					{
						m_Furtrader.SayTo(from, "I cannot repair that.");
					}

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Furtrader.SayTo(from, "Here is your bearskin cap.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x248);
                        ba.MaxHitPoints -= 1;
                        ba.HitPoints = ba.MaxHitPoints;
                    }
                    else
                    {
                        m_Furtrader.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
                else if (targeted is DeerMask && from.Backpack != null)
                {
                    DeerMask ba = targeted as DeerMask;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if (ba.HitPoints < ba.MaxHitPoints)
                    {
						int nCost = 10;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
							toConsume = (ba.MaxHitPoints - ba.HitPoints) * nCost;
						}
						else { toConsume = (ba.MaxHitPoints - ba.HitPoints) * nCost; }
                    }
                    else if (ba.HitPoints >= ba.MaxHitPoints)
                    {
						m_Furtrader.SayTo(from, "That does not need to be repaired.");
                    }
					else
					{
						m_Furtrader.SayTo(from, "I cannot repair that.");
					}

					if (toConsume == 0)
					{
						m_Furtrader.SayTo(from, "That is not really that damaged.");
						return;
					}

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Furtrader.SayTo(from, "Here is your deerskin cap.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x248);
                        ba.MaxHitPoints -= 1;
                        ba.HitPoints = ba.MaxHitPoints;
                    }
                    else
                    {
                        m_Furtrader.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
            }
        }

		public Furtrader( Serial serial ) : base( serial )
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