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
	public class Garth : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.BlacksmithsGuild; } }

		[Constructable]
		public Garth() : base( "the arms dealer" )
		{
			Name = "Garth";
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			Body = 400; 
			Female = false;
			Hue = 0x83EA;

			SetSkill( SkillName.ArmsLore, 64.0, 100.0 );
			SetSkill( SkillName.Blacksmith, 65.0, 88.0 );
			SetSkill( SkillName.Fencing, 60.0, 83.0 );
			SetSkill( SkillName.Macing, 61.0, 93.0 );
			SetSkill( SkillName.Swords, 60.0, 83.0 );
			SetSkill( SkillName.Tactics, 60.0, 83.0 );
			SetSkill( SkillName.Parry, 61.0, 93.0 );

			FacialHairItemID = 0x204B; // SHORT BEARD
			HairItemID = 0x203D; // PONY TAIL
			FacialHairHue = 0x44E;
			HairHue = 0x44E;
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBWeaponSmith() ); 
			m_SBInfos.Add( new SBBlacksmith() ); 
			m_SBInfos.Add( new SBStuddedArmor() );
			m_SBInfos.Add( new SBLeatherArmor() );
			m_SBInfos.Add( new SBMetalShields() );
			m_SBInfos.Add( new SBPlateArmor() );
			m_SBInfos.Add( new SBHelmetArmor() );
			m_SBInfos.Add( new SBChainmailArmor() );
			m_SBInfos.Add( new SBRingmailArmor() );
			m_SBInfos.Add( new SBBuyArtifacts() ); 
			m_SBInfos.Add( new SBGemArmor() ); 
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Boots; }
		}

		public override void InitOutfit()
		{
			this.FacialHairItemID = 0x204B; // SHORT BEARD
			this.HairItemID = 0x203D; // PONY TAIL
			this.FacialHairHue = 0x44E;
			this.HairHue = 0x44E;

			AddItem( new Server.Items.FullApron( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.LongPants( Utility.RandomNeutralHue() ) );
			AddItem( new Server.Items.SmithHammer() );
			AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
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
			private Garth m_Garth;
			private Mobile m_From;

			public FixEntry( Garth Garth, Mobile from ) : base( 6120, 12 )
			{
				m_Garth = Garth;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Garth.BeginRepair( m_From );
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

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				SayTo(from, "Since you are begging, do you still want to hire me to repair something...at least " + nCost.ToString() + " gold per durability?");
			}
			else { SayTo(from, "You want to hire me to repair what at " + nCost.ToString() + " gold per durability?"); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Garth m_Garth;

            public RepairTarget(Garth Garth)
                : base(12, false, TargetFlags.None)
            {
                m_Garth = Garth;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				if ( targeted is Item )
				{
					Item ITEM = (Item)targeted;

					if ( ( targeted is BaseWeapon && from.Backpack != null) && !( targeted is BaseRanged ) && Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( ITEM ) )
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
							m_Garth.SayTo(from, "That does not need to be repaired.");
						}

						if (toConsume == 0)
							return;

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Garth.SayTo(from, "Here is your weapon.");
							from.SendMessage(String.Format("You pay {0} gold.", toConsume));
							Effects.PlaySound(from.Location, from.Map, 0x2A);
							bw.MaxHitPoints -= 1;
							bw.HitPoints = bw.MaxHitPoints;
						}
						else
						{
							m_Garth.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
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
							m_Garth.SayTo(from, "That does not need to be repaired.");
						}
						else
						{
							m_Garth.SayTo(from, "I cannot repair that.");
						}

						if (toConsume == 0)
							return;

						if (pack.ConsumeTotal(typeof(Gold), toConsume))
						{
							if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
							m_Garth.SayTo(from, "Here is your armor.");
							from.SendMessage(String.Format("You pay {0} gold.", toConsume));
							Effects.PlaySound(from.Location, from.Map, 0x2A);
							ba.MaxHitPoints -= 1;
							ba.HitPoints = ba.MaxHitPoints;
						}
						else
						{
							m_Garth.SayTo(from, "It would cost you {0} gold to have that repaired.", toConsume);
							from.SendMessage("You do not have enough gold.");
						}
					}
					else
						m_Garth.SayTo(from, "I cannot repair that.");
				}
				else
					m_Garth.SayTo(from, "I cannot repair that.");

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
					pm.NextSmithBulkOrder = TimeSpan.FromMinutes( 0.01 );
				else
					pm.NextSmithBulkOrder = TimeSpan.FromMinutes( 0.01 );

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

		public Garth( Serial serial ) : base( serial )
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