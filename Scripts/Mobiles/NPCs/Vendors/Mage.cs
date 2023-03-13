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
	public class Mage : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		public override NpcGuild NpcGuild{ get{ return NpcGuild.MagesGuild; } }

		[Constructable]
		public Mage() : base( "the mage" )
		{
			Job = JobFragment.mage;
			Karma = Utility.RandomMinMax( 13, -45 );
			SetSkill( SkillName.EvalInt, 65.0, 88.0 );
			SetSkill( SkillName.Inscribe, 60.0, 83.0 );
			SetSkill( SkillName.Magery, 64.0, 100.0 );
			SetSkill( SkillName.Meditation, 60.0, 83.0 );
			SetSkill( SkillName.MagicResist, 65.0, 88.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBMage() ); 
			m_SBInfos.Add( new SBRuneCasting() );
			m_SBInfos.Add( new SBBuyArtifacts() ); 

			if ( Server.Misc.Worlds.GetMyWorld( this.Map, this.Location, this.X, this.Y ) == "the Land of Lodoria" )
			{
				m_SBInfos.Add( new SBElfWizard() );
			}
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( Utility.RandomBlueHue() ) );
			AddItem( new Server.Items.WizardsHat( Utility.RandomBlueHue() ) );
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
						mobile.SendGump(new SpeechGump( "The Mystical Art Of Wizardry", SpeechFunctions.SpeechText( m_Giver.Name, m_Mobile.Name, "Mage" ) ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		private class FixEntry : ContextMenuEntry
		{
			private Mage m_Mage;
			private Mobile m_From;

			public FixEntry( Mage Mage, Mobile from ) : base( 6120, 12 )
			{
				m_Mage = Mage;
				m_From = from;
			}

			public override void OnClick()
			{
				m_Mage.BeginRepair( m_From );
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

			int nCost = 100;

			if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
			{
				nCost = nCost - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * nCost ); if ( nCost < 1 ){ nCost = 1; }
				SayTo(from, "Since you are begging, do you still want me to charge a magic wand with 5 charges, it will only cost you " + nCost.ToString() + " gold per spell circle of the wand?");
			}
			else { SayTo(from, "If you want me to charge a magic wand with 5 charges, it will cost you " + nCost.ToString() + " gold per spell circle of the wand."); }

            from.Target = new RepairTarget(this);
        }

        private class RepairTarget : Target
        {
            private Mage m_Mage;

            public RepairTarget(Mage mage) : base(12, false, TargetFlags.None)
            {
                m_Mage = mage;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseMagicStaff && from.Backpack != null)
                {
                    BaseMagicStaff ba = targeted as BaseMagicStaff;
                    BaseWeapon bw = targeted as BaseWeapon;
                    Container pack = from.Backpack;

                    int toConsume = 0;
					int spellCircle = 0;
					int myCharges = 0;

					if ( bw.IntRequirement == 10 ) { spellCircle = 1; myCharges = 30; }
					else if ( bw.IntRequirement == 15 ) { spellCircle = 2; myCharges = 23; }
					else if ( bw.IntRequirement == 20 ) { spellCircle = 3; myCharges = 18; }
					else if ( bw.IntRequirement == 25 ) { spellCircle = 4; myCharges = 15; }
					else if ( bw.IntRequirement == 30 ) { spellCircle = 5; myCharges = 12; }
					else if ( bw.IntRequirement == 35 ) { spellCircle = 6; myCharges = 9; }
					else if ( bw.IntRequirement == 40 ) { spellCircle = 7; myCharges = 6; }
					else if ( bw.IntRequirement == 45 ) { spellCircle = 8; myCharges = 3; }

					if ( bw.IntRequirement < 1 )
					{
						m_Mage.SayTo(from, "That does not need my services.");
					}
                    else if ( ba.Charges <= myCharges )
                    {
						toConsume = spellCircle * 100;

						if ( BeggingPose(from) > 0 ) // LET US SEE IF THEY ARE BEGGING - WIZARD
						{
							toConsume = toConsume - (int)( ( from.Skills[SkillName.Begging].Value * 0.005 ) * toConsume );
						}
                    }
                    else
                    {
						m_Mage.SayTo(from, "That wand has too many charges already.");
                    }

                    if (toConsume == 0)
                        return;

                    if (pack.ConsumeTotal(typeof(Gold), toConsume))
                    {
						if ( BeggingPose(from) > 0 ){ Titles.AwardKarma( from, -BeggingKarma( from ), true ); } // DO ANY KARMA LOSS
                        m_Mage.SayTo(from, "Your wand is charged.");
                        from.SendMessage(String.Format("You pay {0} gold.", toConsume));
                        Effects.PlaySound(from.Location, from.Map, 0x5C1);
						ba.Charges = ba.Charges + 5;
                    }
                    else
                    {
                        m_Mage.SayTo(from, "It would cost you {0} gold to have that charged.", toConsume);
                        from.SendMessage("You do not have enough gold.");
                    }
                }
				else
				{
					m_Mage.SayTo(from, "That does not need my services.");
				}
            }
        }

		public Mage( Serial serial ) : base( serial )
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