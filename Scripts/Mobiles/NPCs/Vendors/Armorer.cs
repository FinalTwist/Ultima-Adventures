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
	public class Armorer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.WarriorsGuild; } }

		[Constructable]
		public Armorer() : base( "the armorer" )
		{
			Job = JobFragment.armourer;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.ArmsLore, 64.0, 100.0 );
			SetSkill( SkillName.Blacksmith, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBuyArtifacts() ); 
			m_SBInfos.Add( new SBLeatherArmor() );
			m_SBInfos.Add( new SBStuddedArmor() );
			m_SBInfos.Add( new SBMetalShields() );
			m_SBInfos.Add( new SBPlateArmor() );
			m_SBInfos.Add( new SBHelmetArmor() );
			m_SBInfos.Add( new SBChainmailArmor() );
			m_SBInfos.Add( new SBRingmailArmor() );
			m_SBInfos.Add( new SBGemArmor() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Boots; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.HalfApron( Utility.RandomYellowHue() ) );
			AddItem( new Server.Items.Bandana() );
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
						mobile.SendGump(new SpeechGump( "Keep Armor Shiny", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Armorer" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Armorer m_Armorer;
			private Mobile m_From;

			public FixEntry( Armorer Armorer, Mobile from ) : base( 6120, 12 )
			{
				m_Armorer = Armorer;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Armorer.BeginRepair( m_From );
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
            private Armorer m_Armorer;

            public RepairTarget(Armorer armorer) : base(12, false, TargetFlags.None)
            {
                m_Armorer = armorer;
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

                    if ( WhatIsIt.VendorCanID != "Blacksmith" )
                    {
                        m_Armorer.SayTo( from, "Sorry, I cannot tell what that is." );
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
						m_Armorer.SayTo(from, "Let me tell you about this item...");
						WhatIsIt.Delete();
                    }
                    else
                    {
                        m_Armorer.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if (targeted is BaseArmor && from.Backpack != null)
                {
                    BaseArmor ba = targeted as BaseArmor;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if (ba.HitPoints < ba.MaxHitPoints && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ((Item)targeted) ))
                    {
						int nCost = 10;
						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
							toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost;
						}
						else { toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost; }
                    }
                    else if (ba.HitPoints >= ba.MaxHitPoints && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ((Item)targeted) ))
                    {
						m_Armorer.SayTo(from, "That does not need to be repaired.");
                    }
					else
					{
						m_Armorer.SayTo(from, "I cannot repair that.");
					}

					if (toConsume == 0)
					{
						m_Armorer.SayTo(from, "That is not really that damaged.");
						return;
					}

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Armorer.SayTo(from, "Here is your armor.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x2A);
if (ba.MaxHitPoints > 10)
								ba.MaxHitPoints -= Utility.RandomMinMax(7, 10);
							else 
								ba.MaxHitPoints -= 1;
							ba.HitPoints = ba.MaxHitPoints;
                    }
                    else
                    {
                        m_Armorer.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
                else
                    m_Armorer.SayTo(from, "I cannot repair that.");
            }
        }

		public Armorer( Serial serial ) : base( serial )
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