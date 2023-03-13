using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Commands;
using Server.Targeting;
using Custom.Jerbal.Jako.Breeding;

namespace Custom.Jerbal.Jako.Gumps
{
    #region Accept Breeder Gump
    public class JakoBreederAcceptGump : Gump
    {

        private int m_BreedID;

        public JakoBreederAcceptGump(Mobile targeter, Mobile owners, Mobile others,int breedID)
            : base(0, 0)
        {
            BaseCreature ownbc = owners as BaseCreature;
            BaseCreature othbc = others as BaseCreature;
            this.Closable = true;
            this.Dragable = true;
            this.Resizable = false;
            m_BreedID = breedID;

            AddPage(0);
            AddImageTiled(90, 107, 260, 226, 2082);
            AddImage(72, 72, 2080);
            AddImage(91, 331, 2083);
            AddHtml(102, 106, 238, 179, String.Format("{0} wishes to breed your pet {1} (Level {2} {3}) with {4} pet {5} (Level {6} {7}). This would cause your pet to be inaccessible for up to {8} days and cost {9} gold. Please note that the owner of the female receives the deed to redeem the child if the mating is successful.", targeter.Name, ownbc.Name, ownbc.Level, ownbc.SexString, (ownbc.ControlMaster == othbc.ControlMaster ? "your" : String.Format("{0}'s", othbc.ControlMaster.Name)), othbc.Name, othbc.Level, othbc.SexString, ownbc.NextMateIn.TotalDays.ToString(), JakoBreeder.GoldPrice(owners, others)), (bool)false, (bool)false);
            AddButton(314, 78, 1151, 1152, (int)Buttons.Close, GumpButtonType.Reply, 0);
            AddButton(104, 289, 92, 92, (int)Buttons.Accept, GumpButtonType.Reply, 0);
            AddImage(155, 289, 93);
            AddImage(272, 289, 94);
            AddLabel(158, 295, 32, @"Accept Proposal");
        }

        public enum Buttons
        {
            RightClicked,
            Close,
            Accept,
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.Accept:JakoBreeder.DoBreeding(from, m_BreedID);break;
                default: JakoBreeder.CancelBreeding(m_BreedID); break;
            }
        }
    }
    #endregion

    #region Contract Gump
    public class JakoBreederContractGump : Gump
    {
        private Mobile m_From, m_Breeder;
        private BaseCreature m_Pet1, m_Pet2;
        private string m_Message;

        public BaseCreature Pet1 { get { return m_Pet1; } set { m_Pet1 = value; } }
        public BaseCreature Pet2 { get { return m_Pet2; } set { m_Pet2 = value; } }
        public string Message { get { return m_Message; } set { m_Message = value; } }

        public JakoBreederContractGump(JakoBreederContractGump gmp) : this(gmp.m_From,gmp.m_Breeder,gmp.Pet1,gmp.Pet2,gmp.Message)
        {
            DisplayGump();

        }

        private JakoBreederContractGump(Mobile from, Mobile breeder, BaseCreature p1, BaseCreature p2, string message)
            : base(0, 0)
        {
            m_From = from;
            from.CloseGump(GetType());
            m_Breeder = breeder;
            m_Pet1 = p1;
            m_Pet2 = p2;
            m_Message = message;
        }
        public JakoBreederContractGump(Mobile from, Mobile breeder)
            : base(0,0)
        {
            m_From = from;
            from.CloseGump(GetType());
            m_Breeder = breeder;
            DisplayGump();
            
        }
        public void DisplayGump()
        {
            Closable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddImage(34, 107, 510);
            AddLabel(85, 135, 198, @"Breeding Contract");
            AddButton(73, 167, 30089, 30086, (int)Buttons.Pet1, GumpButtonType.Reply, 0);
            AddButton(73, 245, 30089, 30086, (int)Buttons.Pet2, GumpButtonType.Reply, 0);

            if (m_Pet1 != null)
            {
                AddLabel(105, 167, 0, m_Pet1.Name);
                AddLabel(65, 189, 0, @"Owner:");
                if ( m_Pet1.ControlMaster != null )
                    AddLabel(115, 189, 0, m_Pet1.ControlMaster.Name);
                AddLabel(65, 211, 0, @"Level:");
                AddLabel(115, 211, 0, String.Format("{0} {1}", m_Pet1.RealLevel.ToString(), m_Pet1.SexString));
            }


            if (m_Pet2 != null)
            {
                AddLabel(105, 245, 0, m_Pet2.Name);
                AddLabel(65, 267, 0, @"Owner:");
                if ( m_Pet2.ControlMaster != null)
                    AddLabel(115, 267, 0, m_Pet2.ControlMaster.Name);
                AddLabel(65, 289, 0, @"Level:");
                AddLabel(115, 289, 0, String.Format("{0} {1}", m_Pet2.RealLevel.ToString(), m_Pet2.SexString));
            }
 

            AddButton(343, 292, 241, 243, (int)Buttons.Cancel, GumpButtonType.Reply, 0);


            if (m_Message == null)
            {
                if (Pet1 != null && Pet2 != null && JakoBreeder.CanBreed(Pet1, Pet2) == null)
                {
                    AddButton(260, 290, 247, 248, (int)Buttons.Okay, GumpButtonType.Reply, 0);
                    AddHtml(261, 127, 157, 150, @"These creatures seem to be in proper breeding conditions.", (bool)false, (bool)false);
                } else
                    AddHtml(261, 127, 157, 150, @"Use the crystals on the page, then select the creature(s) you wish to breed.", (bool)false, (bool)false);

            }
            else
                AddHtml(261, 127, 157, 150, m_Message, (bool)false, (bool)false);
        }

        public enum Buttons
        {
            RightClicked,
            Pet2,
            Pet1,
            Okay,
            Cancel,
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            switch (info.ButtonID)
            {
                case (int)Buttons.Pet2:from.Target = new InternalTarget(this,Buttons.Pet2);break;
                case (int)Buttons.Pet1:from.Target = new InternalTarget(this, Buttons.Pet1);break;
                case (int)Buttons.Okay:
                    {
                        if (JakoBreeder.CanBreed(Pet1) == null && JakoBreeder.CanBreed(Pet2) == null && JakoBreeder.CanBreed(Pet1, Pet2) == null)
                            JakoBreeder.SendMasterOkayGumps(from,Pet1, Pet2);
                        break;
                    }
                default: m_Breeder.SayTo(from, "If not today, perhaps another time then."); break; //Closed or right clicked
            }
        }

        private class InternalTarget : Target
        {
            private JakoBreederContractGump m_Gump;
            private Buttons m_Button;


            public InternalTarget(JakoBreederContractGump gump, Buttons button)
                : base(14, false, TargetFlags.None)
            {
                m_Gump = gump;
                m_Button = button;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!(targeted is Mobile))
                    m_Gump.Message = "Is your vision that bad?";
                else
                    m_Gump.Message = JakoBreeder.CanBreed((Mobile)targeted);
                if (m_Gump.Message == null)
                {
                    switch (m_Button)
                    {
                        case Buttons.Pet1: m_Gump.Pet1 = (BaseCreature)targeted; break;
                        case Buttons.Pet2: m_Gump.Pet2 = (BaseCreature)targeted; break;
                    }
                    if (m_Gump.Pet1 != null && m_Gump.Pet2 != null)
                        m_Gump.Message = JakoBreeder.CanBreed(m_Gump.Pet1, m_Gump.Pet2);
                }
                from.CloseGump(typeof(JakoBreederContractGump));
                from.SendGump(new JakoBreederContractGump(m_Gump));
            }
        }

    }
    #endregion 

    #region Breeder Talk Message
    public class JakoBreederTalkGump : Gump
    {
        Mobile m_Breeder;

        public enum Buttons
        {
            RightClicked,
            Okay,
            Close,
        }

        public JakoBreederTalkGump(PlayerMobile pm, Mobile breeder)
            : this()
        {
            if (pm.HasGump(GetType()))
                pm.CloseGump(GetType());
           m_Breeder = breeder;
            
        }

        public JakoBreederTalkGump() : base( 0, 0 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

            

			AddPage(0);
			AddImage(78, 94, 8000);
			AddImageTiled(99, 130, 302, 215, 8001);
			AddLabel(192, 99, 198, @"Animal Breeding");
            AddHtml(115, 136, 265, 197, @"<font size=2>I'm a breeder by trade.  You see, many creatures of this land are able to become stronger and more powerful.  With time, care, and with proper mating, your pets can become some of the best!

In case you didn't know, in order to breed a pet, I will need two pets of the same type and opposite sexes.  This will take some time depending on the creature, and of course my time is not free.  I will take both the male and female and attempt to breed them.  If successful, the owner of the male can come pick it up at their leasure and I will continue to maintain and care for the female until the birth.

The owner of the female will be given a certificate that contains all of the information about the pet and the parents, who are the only two people who will be able to pick up the child.  The owner of the ticket is charged typical stable prices while I wait for the child to be picked up.

Now that you know how I work, I would love to take a look at some animals for you.</font>", (bool)false, (bool)true);
			AddImage(99, 343, 8003);
			AddButton(148, 355, 12012, 12014, (int)Buttons.Okay, GumpButtonType.Reply, 0);
			AddButton(271, 356, 12006, 12008, (int)Buttons.Close, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            switch(info.ButtonID)
            {
                case (int)Buttons.Okay: from.SendGump(new JakoBreederContractGump(from,m_Breeder));break;
                default: m_Breeder.SayTo(from, "Nice talking with you!"); break;
            }
        }
    }
    #endregion
}