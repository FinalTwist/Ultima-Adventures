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
	public class ArcherGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.ArchersGuild; } }

		[Constructable]
		public ArcherGuildmaster() : base( "archer" )
		{
			Job = JobFragment.ranger;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Fletching, 80.0, 100.0 );
			SetSkill( SkillName.Archery, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 80.0, 100.0 );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Female ? VendorShoeType.ThighBoots : VendorShoeType.Boots; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			AddItem( new Server.Items.FeatheredHat( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.LeatherArms() );
			AddItem( new Server.Items.LeatherChest() );
			AddItem( new Server.Items.LeatherGloves() );
			AddItem( new Server.Items.LeatherGorget() );
			AddItem( new Server.Items.LeatherLegs() );
			AddItem( new Server.Items.Bow() );
			AddItem( new Server.Items.ThighBoots( Utility.RandomNeutralHue() ) );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBBowyer() );
			SBInfos.Add( new SBRangedWeapon() ); 
			SBInfos.Add( new SBBuyArtifacts() ); 
			SBInfos.Add( new SBArcherGuild() );
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
						mobile.SendGump(new SpeechGump( "When The Bow Breaks", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Bowyer" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private ArcherGuildmaster m_ArcherGuildmaster;
			private Mobile m_From;

			public FixEntry( ArcherGuildmaster ArcherGuildmaster, Mobile from ) : base( 6120, 12 )
			{
				m_ArcherGuildmaster = ArcherGuildmaster;
				m_From = from;
			}

			public override void OnClick()
			{
				m_ArcherGuildmaster.BeginRepair( m_From );
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

			int nCost = 10;
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
            private ArcherGuildmaster m_ArcherGuildmaster;

            public RepairTarget(ArcherGuildmaster bowyer) : base(12, false, TargetFlags.None)
            {
                m_ArcherGuildmaster = bowyer;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				if ( targeted is UnidentifiedItem )
				{
					Container packs = from.Backpack;
					int nCost = 200;
					UnidentifiedItem WhatIsIt = (UnidentifiedItem)targeted;

					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
					}
					int toConsume = nCost;

                    if ( WhatIsIt.VendorCanID != "ArcherGuildmaster" )
                    {
                        m_ArcherGuildmaster.SayTo( from, "Sorry, I cannot tell what that is." );
					}
                    else if (packs.ConsumeTotal(typeof(Gold), toConsume))
                    {
						string MyItemName = "item";
						Container pack = (Container)targeted;
							List<Item> items = new List<Item>();
							foreach (Item item in pack.Items)
							{
								items.Add(item);
							}
							foreach (Item item in items)
							{
								MyItemName = item.Name;
								from.AddToBackpack ( item );
							}
							if ( MyItemName == ""){ MyItemName = "item"; }
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
						m_ArcherGuildmaster.SayTo(from, "Let me tell you about this item...");
						WhatIsIt.Delete();
                    }
                    else
                    {
                        m_ArcherGuildmaster.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if ( ( targeted is BaseWeapon && from.Backpack != null) && ( targeted is BaseRanged ) )
                {
                    BaseWeapon bw = targeted as BaseWeapon;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if ( bw.HitPoints < bw.MaxHitPoints )
                    {
						int nCost = 10;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
							toConsume = (bw.MaxHitPoints - bw.HitPoints - 1) * nCost;
						}
						else { toConsume = (bw.MaxHitPoints - bw.HitPoints - 1) * nCost; }
                    }
					else
                    {
						m_ArcherGuildmaster.SayTo(from, "That does not need to be repaired.");
                    }

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_ArcherGuildmaster.SayTo(from, "Here is your weapon.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x55);
                        bw.MaxHitPoints -= 1;
                        bw.HitPoints = bw.MaxHitPoints;
                    }
                    else
                    {
                        m_ArcherGuildmaster.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
                else
                    m_ArcherGuildmaster.SayTo(from, "I cannot repair that.");
            }
        }

		public ArcherGuildmaster( Serial serial ) : base( serial )
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