using System;
using System.Collections.Generic;
using Server;
using Server.Engines.BulkOrders;
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
	public class Tinker : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public Tinker() : base( "the tinker" )
		{
			Job = JobFragment.tinker;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Lockpicking, 60.0, 83.0 );
			SetSkill( SkillName.RemoveTrap, 75.0, 98.0 );
			SetSkill( SkillName.Tinkering, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBTinker() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Tinker m_Tinker;
			private Mobile m_From;

			public FixEntry( Tinker Tinker, Mobile from ) : base( 6120, 12 )
			{
				m_Tinker = Tinker;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Tinker.BeginRepair( m_From );
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
            if (Deleted || !from.CheckAlive())
                return;

			int nCost = 100;
			int idCost = 200;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				idCost = idCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * idCost ); if ( idCost < 1 ){ idCost = 1; }
				SayTo(from, "Since you are begging, do you still want to hire me to repair something...at least " + nCost.ToString() + " gold per durability? Or maybe identify an item for " + idCost.ToString() + " gold?");
			}
			else { SayTo(from, "You want to hire me to repair what at " + nCost.ToString() + " gold per durability? Or maybe identify an item for " + idCost.ToString() + " gold?"); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Tinker m_Tinker;

            public RepairTarget(Tinker blacksmith) : base(12, false, TargetFlags.None)
            {
                m_Tinker = blacksmith;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				if ( targeted is Item )
				{
					Item ITEM = (Item)targeted;

					if ( ( targeted is BaseWeapon && from.Backpack != null) && ( targeted is LightSword || targeted is DoubleLaserSword || targeted is BaseKilrathi || targeted is BaseGiftStave || targeted is BaseWizardStaff ) )
					{
						BaseWeapon bw = targeted as BaseWeapon;
						Container pack = from.Backpack;
						int toConsume = 0;

						if (bw.HitPoints < bw.MaxHitPoints )
						{
							int nCost = 100;

							if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
							{
								nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
								toConsume = (bw.MaxHitPoints - bw.HitPoints - 1) * nCost;
							}
							else { toConsume = (bw.MaxHitPoints - bw.HitPoints - 1) * nCost; }
						}
						else
						{
							m_Tinker.SayTo(from, "That does not need to be repaired.");
						}

						if (toConsume == 0)
						{
							m_Tinker.SayTo(from, "That is not really that damaged.");
							return;
						}

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Tinker.SayTo(from, "Here is your weapon.");
							from.SendMessage(String.Format("You pay {0} gold.", toConsume));
							Effects.PlaySound(from.Location, from.Map, 0x542);
							if (bw.MaxHitPoints > 10)
								bw.MaxHitPoints -= Utility.RandomMinMax(7, 10);
							else 
								bw.MaxHitPoints -= 1;
							bw.HitPoints = bw.MaxHitPoints;
						}
						else
						{
							m_Tinker.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else
						m_Tinker.SayTo(from, "I cannot repair that.");
				}
                else
                    m_Tinker.SayTo(from, "I cannot repair that.");
            }
        }

		public Tinker( Serial serial ) : base( serial )
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