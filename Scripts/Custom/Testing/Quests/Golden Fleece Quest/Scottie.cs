// Created by GreyWolf.
// Help from PrplBeast
// Finished Oct. 14, 2007

using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;
using System.Collections.Generic;

namespace Server.Mobiles
{
    [CorpseName("Scottie's corpse")]
	public class Scottie : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Scottie()
		{
            ///////////STR/DEX/INT
            InitStats(31, 41, 51);

            ///////////name
            Name = "Scottie";

            ///////////title
            Title = "";

            ///////////sex. 0x191 is female, 0x190 is male.
            Body = 0x190;

            ///////////skincolor
            Hue = Utility.RandomSkinHue();

            ///////////Random hair and haircolor
            Utility.AssignRandomHair(this);

            ///////////clothing and hues
            AddItem(new Server.Items.Shirt(Utility.RandomBlueHue()));
            AddItem(new Server.Items.LongPants(Utility.RandomBlueHue()));
            AddItem(new Server.Items.Sandals(Utility.RandomBlueHue()));

            ///////////immortal and frozen to-the-spot features below:
            Blessed = true;
            CantWalk = true;

            ///////////Adding a backpack
            Container pack = new Backpack();
            pack.DropItem(new Gold(250, 300));
            pack.Movable = false;
            AddItem(pack);

		}

        public Scottie(Serial serial)
            : base(serial)
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list );
                    list.Add(new ScottieEntry(from, this)); 
	        } 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public class ScottieEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;

            public ScottieEntry(Mobile from, Mobile giver)
                : base(6146, 3)
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
                    ///////////gump name
                    if (!mobile.HasGump(typeof(GoldenThreadQuestGump)))
					{
                        mobile.SendGump(new GoldenThreadQuestGump(mobile));
						
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	        Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
                        Account acct=(Account)from.Account;
                        bool GoldenFleeceRecieved = Convert.ToBoolean(acct.GetTag("GoldenFleeceRecieved"));

			if ( mobile != null)
			{
                ///////////item to be dropped
                if (dropped is GoldenThread)
            
         		{
         			if(dropped.Amount!=10)
         			{
                        { this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "There's not the right amount of thread here!", mobile.NetState); return false; }
                        dropped.Delete();
         			}
                    if (!GoldenFleeceRecieved) //added account tag check
		                {
                            ///////////the reward
                            mobile.AddToBackpack(new Gold(1500));
                            mobile.AddToBackpack(new GoldenFleece());
                            mobile.SendMessage("Thank you for your help!");
                            acct.SetTag("GoldenFleeceRecieved", "true");

                            dropped.Delete(); // added to make it so it would delete the thread on first time completing - GreyWolf.
				
         		        }
				else //what to do if account has already been tagged
         			{
         				mobile.SendMessage("You already did this for me... oh well, suppose I should give you some gold anyway!");
         				mobile.AddToBackpack( new Gold( 500 ) );
         				dropped.Delete();
         			}
         		}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Why on earth would I want to have that?", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
