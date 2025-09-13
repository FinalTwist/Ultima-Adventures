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
	public class Blacksmith : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.BlacksmithsGuild; } }

		[Constructable]
		public Blacksmith() : base( "the blacksmith" )
		{
			Job = JobFragment.blacksmith;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.ArmsLore, 36.0, 68.0 );
			SetSkill( SkillName.Blacksmith, 65.0, 88.0 );
			SetSkill( SkillName.Fencing, 60.0, 83.0 );
			SetSkill( SkillName.Macing, 61.0, 93.0 );
			SetSkill( SkillName.Swords, 60.0, 83.0 );
			SetSkill( SkillName.Tactics, 60.0, 83.0 );
			SetSkill( SkillName.Parry, 61.0, 93.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBlacksmith() ); 
			m_SBInfos.Add( new SBAxeWeapon() );
			m_SBInfos.Add( new SBKnifeWeapon() );
			m_SBInfos.Add( new SBMaceWeapon() );
			m_SBInfos.Add( new SBPoleArmWeapon() );
			m_SBInfos.Add( new SBSpearForkWeapon() );
			m_SBInfos.Add( new SBSwordWeapon() );
			m_SBInfos.Add( new SBMetalShields() );
			m_SBInfos.Add( new SBPlateArmor() );
			m_SBInfos.Add( new SBHelmetArmor() );
			m_SBInfos.Add( new SBChainmailArmor() );
			m_SBInfos.Add( new SBRingmailArmor() );
			m_SBInfos.Add( new SBBuyArtifacts() ); 
			m_SBInfos.Add( new SBGemArmor() ); 

			if ( Region.IsPartOf( "the Enchanted Pass" ) )
				m_SBInfos.Add( new SBGodlySmithing() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Sandals : VendorShoeType.Shoes; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.FullApron() );
			AddItem( new Server.Items.Bandana() );
			AddItem( new Server.Items.SmithHammer() );
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
						mobile.SendGump(new SpeechGump( "Knocking The Dents Out", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Blacksmith" ) ));
					}
				}
            }
        }

		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Blacksmith m_Blacksmith;
			private Mobile m_From;

			public FixEntry( Blacksmith Blacksmith, Mobile from ) : base( 6120, 12 )
			{
				m_Blacksmith = Blacksmith;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Blacksmith.BeginRepair( m_From );
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

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
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
            private Blacksmith m_Blacksmith;

            public RepairTarget(Blacksmith blacksmith)
                : base(12, false, TargetFlags.None)
            {
                m_Blacksmith = blacksmith;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				if ( targeted is Item )
				{
					Item ITEM = (Item)targeted;

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					if ( targeted is UnidentifiedItem )
					{
						Container packs = from.Backpack;
						int nCost = 200;
						UnidentifiedItem WhatIsIt = (UnidentifiedItem)targeted;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
						{
							nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
						}
						int toConsume = nCost;

						if ( WhatIsIt.VendorCanID != "Blacksmith" )
						{
							m_Blacksmith.SayTo( from, "Sorry, I cannot tell what that is." );
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
							m_Blacksmith.SayTo(from, "Let me tell you about this item...");
							WhatIsIt.Delete();
						}
						else
						{
							m_Blacksmith.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					else if ( targeted is BaseKilrathi || targeted is BaseGiftStave || targeted is BaseWizardStaff || targeted is LightSword || targeted is DoubleLaserSword )
					{
						m_Blacksmith.SayTo(from, "You would need a tinker to repair that.");
					}
					else if ( ( targeted is BaseWeapon && from.Backpack != null) && !( targeted is BaseRanged ) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ITEM ) )
					{
						BaseWeapon bw = targeted as BaseWeapon;
						Container pack = from.Backpack;
						int toConsume = 0;

						if (bw.HitPoints < bw.MaxHitPoints )
						{
							int nCost = 100;

							if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING
							{
								nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
								toConsume = (bw.MaxHitPoints - bw.HitPoints - 1) * nCost;
							}
							else { toConsume = (bw.MaxHitPoints - bw.HitPoints - 1) * nCost; }
						}
						else
						{
							m_Blacksmith.SayTo(from, "That does not need to be repaired.");
						}

						if (toConsume == 0)
						{
							m_Blacksmith.SayTo(from, "That is not really that damaged.");
							return;
						}

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Blacksmith.SayTo(from, "Here is your weapon.");
							from.SendMessage(String.Format("You pay {0} gold.", toConsume));
							Effects.PlaySound(from.Location, from.Map, 0x2A);
							if (bw.MaxHitPoints > 10)
								bw.MaxHitPoints -= Utility.RandomMinMax(7, 10);
							else 
								bw.MaxHitPoints -= 1;
							bw.HitPoints = bw.MaxHitPoints;
						}
						else
						{
							m_Blacksmith.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else if ( targeted is BaseArmor && from.Backpack != null && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ITEM ) )
					{
						BaseArmor ba = targeted as BaseArmor;
						Container pack = from.Backpack;
						int toConsume = 0;

						if (ba.HitPoints < ba.MaxHitPoints && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ((Item)targeted) ))
						{
							int nCost = 100;

							if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
							{
								nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
								toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost;
							}
							else { toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost; }
						}
						else if (ba.HitPoints >= ba.MaxHitPoints && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ((Item)targeted) ))
						{
							m_Blacksmith.SayTo(from, "That does not need to be repaired.");
						}
						else
						{
							m_Blacksmith.SayTo(from, "I cannot repair that.");
						}

						if (toConsume == 0)
						{
							m_Blacksmith.SayTo(from, "That is not really that damaged.");
							return;
						}

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Blacksmith.SayTo(from, "Here is your armor.");
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
							m_Blacksmith.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else
						m_Blacksmith.SayTo(from, "I cannot repair that.");
				}
                else
                    m_Blacksmith.SayTo(from, "I cannot repair that.");
            }
        }

		#region Bulk Orders
		public override Item CreateBulkOrder( Mobile from, bool fromContextMenu )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( pm != null && pm.NextSmithBulkOrder == TimeSpan.Zero && (fromContextMenu || 0.2 > Utility.RandomDouble()) )
			{
				double theirSkill = pm.Skills[SkillName.Blacksmith].Base;

				if ( theirSkill >= 70.1 )
					pm.NextSmithBulkOrder = TimeSpan.FromMinutes( 0.01 );
				else if ( theirSkill >= 50.1 )
					pm.NextSmithBulkOrder = TimeSpan.FromMinutes( 0.1 );
				else
					pm.NextSmithBulkOrder = TimeSpan.FromMinutes( 0.5 );

				if ( theirSkill >= 70.1 && ((theirSkill - 40.0) / 300.0) > Utility.RandomDouble() )
					return new LargeSmithBOD();

				return SmallSmithBOD.CreateRandomFor( from );
			}

			return null;
		}

		public override bool IsValidBulkOrder( Item item )
		{
			return ( item is SmallSmithBOD || item is LargeSmithBOD );
		}

		public override bool SupportsBulkOrders( Mobile from )
		{
			return ( from is PlayerMobile && from.Skills[SkillName.Blacksmith].Base > 0 );
		}

		public override TimeSpan GetNextBulkOrder( Mobile from )
		{
			if ( from is PlayerMobile )
				return ((PlayerMobile)from).NextSmithBulkOrder;

			return TimeSpan.Zero;
		}

		public override void OnSuccessfulBulkOrderReceive( Mobile from )
		{
			if( Core.SE && from is PlayerMobile )
				((PlayerMobile)from).NextSmithBulkOrder = TimeSpan.Zero;
		}
		#endregion

		public Blacksmith( Serial serial ) : base( serial )
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