using System;
using System.Collections.Generic;
using Server;
using Server.Engines.BulkOrders;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class Tailor : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.TailorsGuild; } }

		[Constructable]
		public Tailor() : base( "the tailor" )
		{
			Job = JobFragment.tailor;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Tailoring, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBTailor() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 

			if ( Region.IsPartOf( "the Enchanted Pass" ) )
				m_SBInfos.Add( new SBGodlySewing() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Sandals : VendorShoeType.Shoes; }
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
						mobile.SendGump(new SpeechGump( "Altering Cloaks And Robes", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Tailor" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Tailor m_Tailor;
			private Mobile m_From;

			public FixEntry( Tailor Tailor, Mobile from ) : base( 6120, 12 )
			{
				m_Tailor = Tailor;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Tailor.BeginRepair( m_From );
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

			int idCost = 200;
			int nCost = 50;
			int nCostH = 100;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				nCostH = nCostH - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCostH ); if ( nCostH < 1 ){ nCostH = 1; }
				idCost = idCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * idCost ); if ( idCost < 1 ){ idCost = 1; }
				SayTo(from, "Since you are begging, do you still want me to tailor your robe or cloak to look normal, it will only cost you " + nCost.ToString() + " gold? Maybe repair a hat for at least " + nCostH.ToString() + " gold per durability? Or maybe identify an item for " + idCost.ToString() + " gold?");
			}
			else { SayTo(from, "If you want me to tailor your robe or cloak to look normal, it will cost you " + nCost.ToString() + " gold. Maybe repair a hat at " + nCostH.ToString() + " gold per durability? Or maybe identify an item for " + idCost.ToString() + " gold?"); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Tailor m_Tailor;

            public RepairTarget(Tailor tailor) : base(12, false, TargetFlags.None)
            {
                m_Tailor = tailor;
            }

            protected override void OnTarget(Mobile from, object targeted)
			{
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if ( ( targeted is MagicRobe || targeted is BaseOuterTorso ) && from.Backpack != null )
                {
                    Item ba = targeted as Item;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if ( ba.ItemID != 0x1F03 && ba.ItemID != 0x1F04 )
                    {
						int nCost = 50;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
						}
						toConsume = nCost;
                    }
                    else
                    {
						m_Tailor.SayTo(from, "That does not need my services.");
                    }

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Tailor.SayTo(from, "Here is your robe.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x248);
						ba.ItemID = 0x1F03;
						ba.Name = Server.Misc.MaterialInfo.GetSpecialMaterialName( ba ) + "robe";
                    }
                    else
                    {
                        m_Tailor.SayTo(from, "It would cost you {0} gold to have that done.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if ( ( targeted is MagicCloak || targeted is BaseCloak ) && from.Backpack != null )
                {
                    Item ba = targeted as Item;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if ( ba.ItemID != 0x1515 && ba.ItemID != 0x1530 )
                    {
						int nCost = 50;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
						}
						toConsume = nCost;
                    }
                    else
                    {
						m_Tailor.SayTo(from, "That does not need my services.");
                    }

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Tailor.SayTo(from, "Here is your cloak.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x248);
						ba.ItemID = 0x1515;
						ba.Name = Server.Misc.MaterialInfo.GetSpecialMaterialName( ba ) + "cloak";
                    }
                    else
                    {
                        m_Tailor.SayTo(from, "It would cost you {0} gold to have that done.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is BaseHat && from.Backpack != null && ((Item)targeted).ItemID != 0x1545 && ((Item)targeted).ItemID != 0x1546 && ((Item)targeted).ItemID != 0x1547 && ((Item)targeted).ItemID != 0x1548 && ((Item)targeted).ItemID != 0x2B6D && ((Item)targeted).ItemID != 0x3164 )
				{
                    BaseHat ba = targeted as BaseHat;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if (ba.HitPoints < ba.MaxHitPoints)
                    {
						int nCost = 100;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
							toConsume = (ba.MaxHitPoints - ba.HitPoints) * nCost;
						}
						else { toConsume = (ba.MaxHitPoints - ba.HitPoints) * nCost; }
                    }
                    else if (ba.HitPoints >= ba.MaxHitPoints)
                    {
						m_Tailor.SayTo(from, "That does not need to be repaired.");
                    }
					else
					{
						m_Tailor.SayTo(from, "I cannot repair that.");
					}

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Tailor.SayTo(from, "Here is your hat.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x248);
							if (ba.MaxHitPoints > 10)
								ba.MaxHitPoints -= Utility.RandomMinMax(7, 10);
							else 
								ba.MaxHitPoints -= 1;
			
                        ba.HitPoints = ba.MaxHitPoints;
                    }
                    else
                    {
                        m_Tailor.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }

				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                else if ( (targeted is BaseLevelHat || targeted is BaseLevelClothing ) && from.Backpack != null && ((Item)targeted).ItemID != 0x1545 && ((Item)targeted).ItemID != 0x1546 && ((Item)targeted).ItemID != 0x1547 && ((Item)targeted).ItemID != 0x1548 && ((Item)targeted).ItemID != 0x2B6D && ((Item)targeted).ItemID != 0x3164 )
                {
                    BaseLevelHat ba = targeted as BaseLevelHat;
                    Container pack = from.Backpack;
                    int toConsume = 0;

                    if (ba.HitPoints < ba.MaxHitPoints)
                    {
                        int nCost = 200;

                        if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
                        {
                            nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
                            toConsume = (ba.MaxHitPoints - ba.HitPoints) * nCost;
                        }
                        else { toConsume = (ba.MaxHitPoints - ba.HitPoints) * nCost; }
                    }
                    else if (ba.HitPoints >= ba.MaxHitPoints)
                    {
                        m_Tailor.SayTo(from, "That does not need to be repaired.");
                    }
                    else
                    {
                        m_Tailor.SayTo(from, "I cannot repair that.");
                    }

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
                        if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Tailor.SayTo(from, "Here is your hat.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x248);
                            if (ba.MaxHitPoints > 10)
                                ba.MaxHitPoints -= Utility.RandomMinMax(7, 10);
                            else 
                                ba.MaxHitPoints -= 1;
            
                        ba.HitPoints = ba.MaxHitPoints;
                    }
                    else
                    {
                        m_Tailor.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else if ( targeted is UnidentifiedItem )
				{
					Container packs = from.Backpack;
					int nCost = 200;
					UnidentifiedItem WhatIsIt = (UnidentifiedItem)targeted;

					if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
					{
						nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
					}
					int toConsume = nCost;

                    if ( WhatIsIt.VendorCanID != "Tailor" )
                    {
                        m_Tailor.SayTo( from, "Sorry, I cannot tell what that is." );
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
						m_Tailor.SayTo(from, "Let me tell you about this item...");
						WhatIsIt.Delete();
                    }
                    else
                    {
                        m_Tailor.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
				}
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				else
				{
					m_Tailor.SayTo(from, "That does not need my services.");
				}
            }
        }

		#region Bulk Orders
		public override Item CreateBulkOrder( Mobile from, bool fromContextMenu )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( pm != null && pm.NextTailorBulkOrder == TimeSpan.Zero && (fromContextMenu || 0.2 > Utility.RandomDouble()) )
			{
				double theirSkill = pm.Skills[SkillName.Tailoring].Base;

				if ( theirSkill >= 70.1 )
					pm.NextTailorBulkOrder = TimeSpan.FromMinutes( 0.01 );
				else if ( theirSkill >= 50.1 )
					pm.NextTailorBulkOrder = TimeSpan.FromMinutes( 0.01 );
				else
					pm.NextTailorBulkOrder = TimeSpan.FromMinutes( 0.01 );

				if ( theirSkill >= 70.1 && ((theirSkill - 40.0) / 300.0) > Utility.RandomDouble() )
					return new LargeTailorBOD();

				return SmallTailorBOD.CreateRandomFor( from );
			}

			return null;
		}

		public override bool IsValidBulkOrder( Item item )
		{
			return ( item is SmallTailorBOD || item is LargeTailorBOD );
		}

		public override bool SupportsBulkOrders( Mobile from )
		{
			return ( from is PlayerMobile && from.Skills[SkillName.Tailoring].Base > 0 );
		}

		public override TimeSpan GetNextBulkOrder( Mobile from )
		{
			if ( from is PlayerMobile )
				return ((PlayerMobile)from).NextTailorBulkOrder;

			return TimeSpan.Zero;
		}

		public override void OnSuccessfulBulkOrderReceive( Mobile from )
		{
			if( Core.SE && from is PlayerMobile )
				((PlayerMobile)from).NextTailorBulkOrder = TimeSpan.Zero;
		}
		#endregion

		public Tailor( Serial serial ) : base( serial )
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
