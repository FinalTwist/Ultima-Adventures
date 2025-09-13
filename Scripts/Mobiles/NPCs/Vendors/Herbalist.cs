using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles 
{ 
	public class Herbalist : BaseVendor 
	{ 
		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.AlchemistsGuild; } }

		[Constructable]
		public Herbalist() : base( "the herbalist" ) 
		{ 
			Job = JobFragment.herbalist;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.Alchemy, 80.0, 100.0 );
			SetSkill( SkillName.Cooking, 80.0, 100.0 );
			SetSkill( SkillName.TasteID, 80.0, 100.0 );
		} 

		public override void InitSBInfo() 
		{ 
			m_SBInfos.Add( new SBHerbalist() ); 
			m_SBInfos.Add( new SBMixologist() ); 
		} 

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; } 
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
						mobile.SendGump(new SpeechGump( "Herbs And Spices", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Herbalist" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Herbalist m_Herbalist;
			private Mobile m_From;

			public FixEntry( Herbalist Herbalist, Mobile from ) : base( 6120, 12 )
			{
				m_Herbalist = Herbalist;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Herbalist.BeginRepair( m_From );
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

			int nCost = 20;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				SayTo(from, "Since you are begging, do you still want me to identify unknown reagents, it will only cost you " + nCost.ToString() + " gold?");
			}
			else { SayTo(from, "If you want me to identify unknown reagents, it will cost you " + nCost.ToString() + " gold."); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Herbalist m_Herbalist;

            public RepairTarget(Herbalist mage) : base(12, false, TargetFlags.None)
            {
                m_Herbalist = mage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				int nCost = 20;

				if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
				{
					nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				}

                if (targeted is UnknownReagent && from.Backpack != null)
                {
					UnknownReagent weed = targeted as UnknownReagent;
                    Container pack = from.Backpack;
                    int toConsume = nCost;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));

						m_Herbalist.PlaySound( Utility.Random( 0x3A, 3 ) );
						m_Herbalist.Animate( 34, 5, 1, true, false, 0 );

						int RegCount = weed.RegAmount;
						if ( RegCount < 1 ){ RegCount = 1; }

						int reagentType = Utility.RandomMinMax( 0, 15 );
						string reagentName = "";

						if ( reagentType == 0 ){ from.AddToBackpack( new BlackPearl( RegCount ) ); reagentName = "black pearl"; }
						else if ( reagentType == 1 ){ from.AddToBackpack( new Bloodmoss( RegCount ) ); reagentName = "bloodmoss"; }
						else if ( reagentType == 2 ){ from.AddToBackpack( new Garlic( RegCount ) ); reagentName = "garlic"; }
						else if ( reagentType == 3 ){ from.AddToBackpack( new Ginseng( RegCount ) ); reagentName = "ginseng"; }
						else if ( reagentType == 4 ){ from.AddToBackpack( new MandrakeRoot( RegCount ) ); reagentName = "mandrake root"; }
						else if ( reagentType == 5 ){ from.AddToBackpack( new Nightshade( RegCount ) ); reagentName = "nightshade"; }
						else if ( reagentType == 6 ){ from.AddToBackpack( new SulfurousAsh( RegCount ) ); reagentName = "sulfurous ash"; }
						else if ( reagentType == 7 ){ from.AddToBackpack( new SpidersSilk( RegCount ) ); reagentName = "spiders silk"; }
						else if ( reagentType == 8 ){ from.AddToBackpack( new BatWing( RegCount ) ); reagentName = "bat wing"; }
						else if ( reagentType == 9 ){ from.AddToBackpack( new GraveDust( RegCount ) ); reagentName = "grave dust"; }
						else if ( reagentType == 10 ){ from.AddToBackpack( new DaemonBlood( RegCount ) ); reagentName = "daemon blood"; }
						else if ( reagentType == 11 ){ from.AddToBackpack( new NoxCrystal( RegCount ) ); reagentName = "nox crystal"; }
						else if ( reagentType == 12 ){ from.AddToBackpack( new PigIron( RegCount ) ); reagentName = "pig iron"; }
						else if ( reagentType == 13 ){ from.AddToBackpack( new SackFlour() ); RegCount=1; reagentName = "regular flour"; }
						else if ( reagentType == 14 ){ from.AddToBackpack( new FertileDirt() ); RegCount=1; reagentName = "plain dirt"; }
						else { from.AddToBackpack( new Sand() ); RegCount=1; reagentName = "some sand"; }

						if ( RegCount < 2 ){ m_Herbalist.SayTo(from, "This seems to be " + reagentName + "."); }
						else { m_Herbalist.SayTo(from, "This seems to be " + RegCount + " " + reagentName + "."); }
						weed.Delete();
                    }
                    else
                    {
                        m_Herbalist.SayTo(from, "It would cost you {0} gold to have that identified.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				else
				{
					m_Herbalist.SayTo(from, "That does not need my services.");
				}
            }
        }

		public Herbalist( Serial serial ) : base( serial ) 
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