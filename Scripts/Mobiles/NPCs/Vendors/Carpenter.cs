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
	public class Carpenter : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.CarpentersGuild; } }

		[Constructable]
		public Carpenter() : base( "the carpenter" )
		{
			Job = JobFragment.carpenter;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Carpentry, 85.0, 100.0 );
			SetSkill( SkillName.Lumberjacking, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStavesWeapon() );
			m_SBInfos.Add( new SBCarpenter() );
			m_SBInfos.Add( new SBWoodenShields() ); 
			m_SBInfos.Add( new SBBuyArtifacts() ); 
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.HalfApron() );
		}

		///////////////////////////////////////////////////////////////////////////
		private class FixEntry : ContextMenuEntry
		{
			private Carpenter m_Carpenter;
			private Mobile m_From;

			public FixEntry( Carpenter Carpenter, Mobile from ) : base( 6120, 12 )
			{
				m_Carpenter = Carpenter;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Carpenter.BeginRepair( m_From );
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
            private Carpenter m_Carpenter;

            public RepairTarget(Carpenter blacksmith)
                : base(12, false, TargetFlags.None)
            {
                m_Carpenter = blacksmith;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				if ( targeted is Item )
				{
					Item ITEM = (Item)targeted;

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

						if ( WhatIsIt.VendorCanID != "Carpenter" )
						{
							m_Carpenter.SayTo( from, "Sorry, I cannot tell what that is." );
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
							m_Carpenter.SayTo(from, "Let me tell you about this item...");
							WhatIsIt.Delete();
						}
						else
						{
							m_Carpenter.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
					else if ( ( targeted is BaseWeapon && from.Backpack != null) && !( targeted is BaseRanged ) && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ITEM ) )
					{
						BaseWeapon bw = targeted as BaseWeapon;
						Container pack = from.Backpack;
						int toConsume = 0;

						if (bw.HitPoints < bw.MaxHitPoints )
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
							m_Carpenter.SayTo(from, "That does not need to be repaired.");
						}

						if (toConsume == 0)
						{
							m_Carpenter.SayTo(from, "That is not really that damaged.");
							return;
						}

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Carpenter.SayTo(from, "Here is your weapon.");
							from.SendMessage(String.Format("You pay {0} gold.", toConsume));
							Effects.PlaySound(from.Location, from.Map, 0x23D);
if (bw.MaxHitPoints > 10)
								bw.MaxHitPoints -= Utility.RandomMinMax(7, 10);
							else 
								bw.MaxHitPoints -= 1;
							bw.HitPoints = bw.MaxHitPoints;
						}
						else
						{
							m_Carpenter.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else if ( targeted is BaseArmor && from.Backpack != null && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ITEM ) )
					{
						BaseArmor ba = targeted as BaseArmor;
						Container pack = from.Backpack;
						int toConsume = 0;

						if (ba.HitPoints < ba.MaxHitPoints && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ((Item)targeted) ))
						{
							int nCost = 10;

							if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
							{
								nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
								toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost;
							}
							else { toConsume = (ba.MaxHitPoints - ba.HitPoints - 1) * nCost; }
						}
						else if (ba.HitPoints >= ba.MaxHitPoints && Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( ((Item)targeted) ))
						{
							m_Carpenter.SayTo(from, "That does not need to be repaired.");
						}
						else
						{
							m_Carpenter.SayTo(from, "I cannot repair that.");
						}

						if (toConsume == 0)
						{
							m_Carpenter.SayTo(from, "That is not really that damaged.");
							return;
						}

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Carpenter.SayTo(from, "Here is your armor.");
							from.SendMessage(String.Format("You pay {0} gold.", toConsume));
							Effects.PlaySound(from.Location, from.Map, 0x23D);
if (ba.MaxHitPoints > 10)
								ba.MaxHitPoints -= Utility.RandomMinMax(7, 10);
							else 
								ba.MaxHitPoints -= 1;
							ba.HitPoints = ba.MaxHitPoints;
						}
						else
						{
							m_Carpenter.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else
						m_Carpenter.SayTo(from, "I cannot repair that.");
				}
                else
                    m_Carpenter.SayTo(from, "I cannot repair that.");
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public Carpenter( Serial serial ) : base( serial )
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