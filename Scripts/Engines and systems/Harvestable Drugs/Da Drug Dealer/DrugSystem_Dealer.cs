#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Items.Crops;

namespace Server.Mobiles
{
    public class DrugSystem_Dealer : BaseVendor
    {
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
	protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

        #region Vendor Can Say Random Phrases From A [.txt] File

        public bool active;

        public static string path = "Data/The Drug Dealer/Speech.txt";

        private DateTime nextAbilityTime;

        private StreamReader text;

        private string curspeech;

        public override bool InitialInnocent { get { return true; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (!value)
                {
                    CloseStream();
                }

                active = value;
            }
        }

        #endregion Vendor Can Say Random Phrases From A [.txt] File

        public override NpcGuild NpcGuild { get { return NpcGuild.MagesGuild; } }

        [Constructable]
        public DrugSystem_Dealer()
            : base("Smokeweed Merchant")
        {
            Title = " the shady merchant";

        #region Vendor Can Say Random Phrases From A [.txt] File

            SpeechHue = Utility.RandomDyedHue();

        #endregion Vendor Can Say Random Phrases From A [.txt] File

            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");

            }

            Item hair = new Item(Utility.RandomList(0x203B, 0x2049, 0x2048, 0x204A));
            hair.Hue = Utility.RandomNondyedHue();
            hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem(hair);

            if (Utility.RandomBool() && !this.Female)
            {
                Item beard = new Item(Utility.RandomList(0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D));

                beard.Hue = hair.Hue;
                beard.Layer = Layer.FacialHair;
                beard.Movable = false;

                AddItem(beard);
            }

            switch (Utility.Random(1))
            {
                case 0: AddItem(new Dagger()); break;
            

            AddItem(new Server.Items.Shirt(Utility.RandomRedHue()));
            AddItem(new Server.Items.ShortPants(Utility.RandomBlueHue()));
            AddItem(new Server.Items.SkullCap(Utility.RandomBlueHue()));
            AddItem(new Server.Items.Sandals(Utility.RandomBlueHue()));
            }

            SetStr(95, 125);
            SetDex(75, 100);
            SetInt(95, 125);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 100, 100);
            SetDamageType(ResistanceType.Fire, 25, 50);
            SetDamageType(ResistanceType.Cold, 25, 50);
            SetDamageType(ResistanceType.Energy, 25, 50);
            SetDamageType(ResistanceType.Poison, 25, 50);

            SetResistance(ResistanceType.Physical, 100, 100);
            SetResistance(ResistanceType.Fire, 25, 50);
            SetResistance(ResistanceType.Cold, 25, 50);
            SetResistance(ResistanceType.Energy, 25, 50);
            SetResistance(ResistanceType.Poison, 25, 50);

            SetSkill(SkillName.TasteID, 65.0, 88.0);
            SetSkill(SkillName.Alchemy, 85.0, 100.0);
            SetSkill(SkillName.Parry, 100, 120);
            SetSkill(SkillName.Anatomy, 100, 120);
            SetSkill(SkillName.Wrestling, 100, 120);
            SetSkill(SkillName.Healing, 100, 120);
            SetSkill(SkillName.Tactics, 100, 120);
            SetSkill(SkillName.MagicResist, 100, 120);

        #region Vendor Can Say Random Phrases From A [.txt] File

            active = true;
        
        #endregion Vendor Can Say Random Phrases From A [.txt] File

            Karma = -5000;
            Fame = 1000;

            VirtualArmor = 5;
            CantWalk = false;

            PackItem(new SmokeweedBong());

        }   //These Overrides Are Added To Benefit This NPC In Combat

            public override bool BardImmune { get { return !Core.AOS; } }
            public override bool AutoDispel { get { return true; } }
            public override bool AlwaysMurderer { get { return false; } }

            public override bool IsScaredOfScaryThings { get { return false; } }
            public override bool IsScaryToPets { get { return true; } }
            public override bool IsInvulnerable { get { return false; } }
        
        #region Vendor Can Say Random Phrases From A [.txt] File

        public void Emote()
        {
            switch (Utility.Random(85))
            {
                case 1:
                    PlaySound(Female ? 785 : 1056);
                    Say("*cough!*");
                    break;
                case 2:
                    PlaySound(Female ? 818 : 1092);
                    Say("*sniff*");
                    break;
                default:
                    break;
            }
        }

        public void CloseStream()
        {
            if (text != null)
            {
                try { text.Close(); text = null; }
                catch { };
            }
        }

        public void Talk()
        {
            if (text == null) return;

            try
            {
                curspeech = text.ReadLine();

                if (curspeech == null) throw (new ArgumentNullException());

                Say(curspeech);
            }
            catch
            {
                CloseStream();
            }
        }

        public override void OnThink()
        {
            if (DateTime.UtcNow >= nextAbilityTime && Combatant == null && active == true)
            {
                nextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(15, 30));

                if (text == null)
                {
                    try
                    {
                        text = new StreamReader(path, System.Text.Encoding.Default, false);
                    }
                    catch { }
                }

                Talk();

                Emote();
            }
        }

        public override void OnDeath(Container c)
        {
            CloseStream();
            base.OnDeath(c);
        }

        public override void OnDelete()
        {
            CloseStream();
            base.OnDelete();
        }

        #endregion Vendor Can Say Random Phrases From A [.txt] File

        public DrugSystem_Dealer(Serial serial): base(serial)
        {
        }

        #region This References The Vendor Inventory Script File

        public override void InitSBInfo()
        {
            m_SBInfos.Add(new SBDrugSystem());
        }

        #endregion This References The Vendor Inventory Script File

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

        #region Vendor Can Say Random Phrases From A [.txt] File

            writer.Write((bool)active);

         #endregion Vendor Can Say Random Phrases From A [.txt] File
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

        #region Vendor Can Say Random Phrases From A [.txt] File

            active = reader.ReadBool();

        #endregion Vendor Can Say Random Phrases From A [.txt] File

        }
    }
}