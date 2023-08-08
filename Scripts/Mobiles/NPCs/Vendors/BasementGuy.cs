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
using Server.Targeting;
using Server.Network;

namespace Server.Mobiles
{
	public class BasementGuy : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MerchantsGuild; } }

		[Constructable]
		public BasementGuy() : base( "the Basement Guy" )
		{
			Job = JobFragment.shopkeep;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Camping, 65.0, 88.0 );
			SetSkill( SkillName.ItemID, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBasement() ); 

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
						mobile.SendGump(new SpeechGump( "The Right Survival Gear", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "BasementGuy" ) ));
					}
				}
            }
        }

		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private BasementGuy m_BasementGuy;
			private Mobile m_From;

			public FixEntry( BasementGuy BasementGuy, Mobile from ) : base( 6120, 12 )
			{
				m_BasementGuy = BasementGuy;
				m_From = from;
			}

			public override void OnClick()
			{
				m_BasementGuy.BeginRepair( m_From );
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

			int idCost = 250;
			int nCost = 50;
			int nCostH = 100;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				nCostH = nCostH - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCostH ); if ( nCostH < 1 ){ nCostH = 1; }
				idCost = idCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * idCost ); if ( idCost < 1 ){ idCost = 1; }
				SayTo(from, "Since you are begging, which unusual item shall I examine, for " + idCost.ToString() + " gold?");
			}
			else { SayTo(from, "Which unusual item shall I examine, for " + idCost.ToString() + " gold?"); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private BasementGuy m_BasementGuy;

            public RepairTarget(BasementGuy BasementGuy) : base(12, false, TargetFlags.None)
            {
                m_BasementGuy = BasementGuy;
            }

            protected override void OnTarget(Mobile from, object targeted)
			{
				if ( targeted is Item )
				{
					Item examine = (Item)targeted;

					if ( Server.Misc.RelicItems.IsRelicItem( examine ) == true )
					{
						Container packs = from.Backpack;
						int nCost = 25;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
						}
						int toConsume = nCost;

						if ( packs.ConsumeTotal(typeof(Gold), toConsume) )
						{
							string toSay = Server.Misc.RelicItems.IdentifyRelicValue( m_BasementGuy, from, examine );
							if ( toSay != "" )
							{
								from.SendMessage(String.Format("You pay {0} gold.", toConsume));
								m_BasementGuy.SayTo(from, toSay );
							}
							else
							{
								m_BasementGuy.SayTo(from, "I cannot put a value on that.");
							}
	
						}
						else
						{
							m_BasementGuy.SayTo(from, "It would cost you {0} gold for me to examine that.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else
					{
						m_BasementGuy.SayTo(from, "I cannot put a value on that.");
					}
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else
				{
					m_BasementGuy.SayTo(from, "I cannot put a value on that.");
				}
            }
        }

		public BasementGuy( Serial serial ) : base( serial )
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