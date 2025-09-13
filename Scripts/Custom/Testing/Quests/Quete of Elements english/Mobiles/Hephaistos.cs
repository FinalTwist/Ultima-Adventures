using System.Collections.Generic;
using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;


namespace Server.Mobiles
{
    [CorpseName("Corpse of Hephaistos")]
    public class Hephaistos : BaseCreature
    {
        public virtual bool IsInvulnerable { get { return true; } }

        [Constructable]
        public Hephaistos()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Hephaistos";
            Title = "God of Fire";
            Body = 15;
            BaseSoundID = 838;
            CantWalk = true;


           
            Blessed = true;

           
        }
        public Hephaistos(Serial serial)
            : base(serial)
        {
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new HephaistosEntry(from, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public class HephaistosEntry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public HephaistosEntry(Mobile from, Mobile giver)
                : base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            base.HandlesOnSpeech(from);
            return true;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {

            bool isMatch = false;

            Mobile from = e.Mobile;

            string keyword = this.Name + " wind";


            if (keyword != null && e.Speech.ToLower().IndexOf(keyword.ToLower()) >= 0)
            {
                isMatch = true;

                if (!isMatch)
                    return;

               
                    from.SendGump(new ElementQuestGump1(from));
                    from.AddToBackpack(new ElementFeu());
                e.Handled = true;
            }
            
            base.OnSpeech(e);
        }

       
            }
     
        }
   


                    
                   
