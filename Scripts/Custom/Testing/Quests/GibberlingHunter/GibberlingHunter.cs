using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a corpse")]
    public class GibberHunter : BaseCreature
    {
        [Constructable]
        public GibberHunter()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = NameList.RandomName("male");
            Title = "the hunter";
            Body = 400;

            ///////////immortal and frozen to-the-spot features below:
            Blessed = true;
            CantWalk = true;
            ///////////STR/DEX/INT
            InitStats(31, 41, 51);
            /*
            SetStr(76, 100);
            SetDex(56, 75);
            SetInt(11, 14);

            SetHits(100);
            SetDex(25);
            SetInt(100);

            SetDamage(11, 15);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 20);
            SetResistance(ResistanceType.Cold, 20);
            SetResistance(ResistanceType.Poison, 20);

            SetSkill(SkillName.Magery, 100.0);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Archery, 100.0);
            SetSkill(SkillName.Anatomy, 100.0);
            SetSkill(SkillName.Focus, 100.0);*/

            Utility.AssignRandomHair(this);
            AddItem(new Shirt());
            AddItem(new LongPants());

            Bow bow = new Bow();
            bow.Attributes.SpellChanneling = 1;
            bow.Attributes.CastSpeed = -1;
            AddItem(bow);

            Fame = 450;
            Karma = 0;

            VirtualArmor = 24;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            AddItem(new Gold(2500));
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {

            Mobile m = from;

            PlayerMobile mobile = m as PlayerMobile;

            if (mobile != null)
            {
                if (dropped is GibberHead)
                {
                    dropped.Delete();
                    //mobile.AddToBackpack(new SoulThiefsDagger( ));
                    mobile.AddToBackpack(new GibberToken());
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Thank you. as i said here is a reward.", mobile.NetState);
                }
                else
                {
                    this.PrivateOverheadMessage(MessageType.Regular, 1153, false, "I have no need for this...", mobile.NetState); return true;
                }


            }

            return false;
        }

        public class TheHunterEntry : ContextMenuEntry
        {
            private Mobile m_Mobile; private Mobile m_Giver;

            public TheHunterEntry(Mobile from, Mobile giver)
                : base(6146, 3)
            {
                m_Mobile = from; m_Giver = giver;
            }


            public override void OnClick()
            {

                if (!(m_Mobile is PlayerMobile)) return;
                PlayerMobile mobile = (PlayerMobile)m_Mobile;
                {

                    if (!mobile.HasGump(typeof(HunterQuestGump)))
                    {
                        if (GibberToken.GibberTokenExistsOn(mobile))
                        {
                            mobile.SendMessage("You have already completed this task.");
                        }
                        else
                        {
                            mobile.SendGump(new HunterQuestGump());
                        }
                    }
                }
            }


        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new TheHunterEntry(from, this));
        }

        public GibberHunter(Serial serial)
            : base(serial)
		{
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

    }

}
