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
	[TypeAlias( "Server.Mobiles.Bower" )]
	public class Bowyer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.ArchersGuild; } }

		[Constructable]
		public Bowyer() : base( "the bowyer" )
		{
			Job = JobFragment.bowyer;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Fletching, 80.0, 100.0 );
			SetSkill( SkillName.Archery, 80.0, 100.0 );
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
			base.InitOutfit();

			AddItem( new Server.Items.Bow() );
			AddItem( new Server.Items.LeatherGorget() );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBowyer() );
			m_SBInfos.Add( new SBRangedWeapon() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 

			if ( this.Map == Map.Tokuno )
				m_SBInfos.Add( new SBLotsOfArrows() );
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
			private Bowyer m_Bowyer;
			private Mobile m_From;

			public FixEntry( Bowyer Bowyer, Mobile from ) : base( 6120, 12 )
			{
				m_Bowyer = Bowyer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Bowyer.BeginRepair( m_From );
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
            private Bowyer m_Bowyer;

            public RepairTarget(Bowyer bowyer) : base(12, false, TargetFlags.None)
            {
                m_Bowyer = bowyer;
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

                    if ( WhatIsIt.VendorCanID != "Bowyer" )
                    {
                        m_Bowyer.SayTo( from, "Sorry, I cannot tell what that is." );
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
						m_Bowyer.SayTo(from, "Let me tell you about this item...");
						WhatIsIt.Delete();
                    }
                    else
                    {
                        m_Bowyer.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is BaseKilrathi || targeted is BaseGiftStave || targeted is BaseWizardStaff || targeted is LightSword || targeted is DoubleLaserSword )
				{
                    m_Bowyer.SayTo(from, "You would need a tinker to repair that.");
				}
                else if ( ( targeted is BaseWeapon && from.Backpack != null) && targeted is BaseRanged && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ((Item)targeted) ) )
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
						m_Bowyer.SayTo(from, "That does not need to be repaired.");
                    }

					if (toConsume == 0)
					{
						m_Bowyer.SayTo(from, "That is not really that damaged.");
						return;
					}

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Bowyer.SayTo(from, "Here is your weapon.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x55);
                        bw.MaxHitPoints -= 1;
                        bw.HitPoints = bw.MaxHitPoints;
                    }
                    else
                    {
                        m_Bowyer.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
                else
                    m_Bowyer.SayTo(from, "I cannot repair that.");
            }
        }

		public Bowyer( Serial serial ) : base( serial )
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