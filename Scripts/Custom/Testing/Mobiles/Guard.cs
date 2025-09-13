using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Spells;

namespace Server.Mobiles
{
    public enum GuardType
    {
        Pikeman,
        Swordsman,
        Archer,
        Cavalry,
        Wizard,
        Medic
    }

    [CorpseName("a fallen Imperial guard")]
    public class Guard : BaseCreature
    {
        private GuardType m_Type;

        [CommandProperty(AccessLevel.GameMaster)]
        public GuardType Type { get { return m_Type; } }

        [Constructable]
        public Guard(GuardType type)
            : this(type, AIType.AI_Melee)
        {

        }

        [Constructable]
        public Guard(GuardType type, AIType ai)
            : base(ai, FightMode.Closest, 20, 1, 0.2, 0.4)
        {
            m_Type = type;

                        if (m_Type == GuardType.Wizard)
                ChangeAIType(AIType.AI_Mage);

            if (m_Type == GuardType.Archer)
                ChangeAIType(AIType.AI_Archer);

            if (m_Type == GuardType.Medic)
                ChangeAIType(AIType.AI_Healer);

            Title = GetTitle(type);
            Hue = Utility.RandomSkinHue();
            Karma = 12000;

            if (0.50 >= Utility.RandomDouble())
            {
                Name = NameList.RandomName("female") + ",";
                Female = true;
                Body = 0x191;
            }
            else
            {
                Name = NameList.RandomName("male") + ",";
                Body = 0x190;
                FacialHairItemID = Utility.RandomList(0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D);
            }

            SetStatsAndSkills(type);

            SetDamage(7, 13);
            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 40, 50);
            SetResistance(ResistanceType.Cold, 30, 45);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 50, 60);

            HairItemID = Utility.RandomList(0x203B, 0x203C, 0x203D, 0x2048);
            HairHue = FacialHairHue = Utility.RandomHairHue();

            Backpack pack = new Backpack();
            pack.AddItem(new Bandage(Utility.RandomMinMax(100, 200)));

            this.AddItem(pack);

            AddEquipment(type);

            if (type == GuardType.Cavalry)
            {
                Horse horse = new Horse();
                horse.Body = 0xE4;
                horse.Controlled = true;
                horse.ControlMaster = this;
                horse.ControlOrder = OrderType.Come;
                horse.RawName = "an Imperial War Horse";
                horse.Hue = 1410;
                horse.ItemID = 16033;
                horse.Rider = this;

                horse.RawStr += Utility.RandomMinMax(45, 60);
                horse.RawDex += Utility.RandomMinMax(25, 30);
                horse.RawInt += Utility.RandomMinMax(15, 20);

                horse.SetSkill(SkillName.Wrestling, horse.Skills.Wrestling.Value + Utility.RandomMinMax(15, 20));
                horse.SetSkill(SkillName.Tactics, horse.Skills.Tactics.Value + Utility.RandomMinMax(20, 25));
                horse.SetSkill(SkillName.MagicResist, horse.Skills.MagicResist.Value + Utility.RandomMinMax(30, 40));

            }
        }

        public Guard(Serial serial)
            : base(serial)
        {
        }    

        #region Stats
        private string GetTitle(GuardType type)
        {
            string title;

            switch (type)
            {
                default:
                case GuardType.Archer: title = "the Imperial Archer"; break;
                case GuardType.Cavalry: title = "Imperial Cavalry"; break;
                case GuardType.Pikeman: title = "the Imperial Hoplite"; break;
                case GuardType.Swordsman: title = "the Imperial Knight"; break;
                case GuardType.Wizard: title = "the Imperial Wizard"; break;
                case GuardType.Medic: title = "the Imperial Medic"; break;
            }

            return title;
        }

        private void SetStatsAndSkills(GuardType type)
        {
            switch (type)
            {
                default:
                case GuardType.Cavalry:
                case GuardType.Pikeman:
                case GuardType.Swordsman:
                    {
                        SetStr(150, 175);
                        SetDex(80, 100);
                        SetInt(65, 80);

                        SetHits(475, 700);

                        SetSkill(SkillName.Tactics, 85, 100);
                        SetSkill(SkillName.Healing, 65, 85);
                        SetSkill(SkillName.Anatomy, 100, 110);
                        SetSkill(SkillName.Wrestling, 95, 110);
                        SetSkill(SkillName.Swords, 95, 105);
                        SetSkill(SkillName.Fencing, 95, 105);
                        SetSkill(SkillName.MagicResist, 95, 105);

                    } break;

                case GuardType.Archer:
                    {
                        SetStr(100, 130);
                        SetDex(125, 140);
                        SetInt(75, 90);

                        SetHits(300, 400);

                        SetSkill(SkillName.Tactics, 85, 100);
                        SetSkill(SkillName.Healing, 65, 85);
                        SetSkill(SkillName.Anatomy, 100, 105);
                        SetSkill(SkillName.Wrestling, 75, 80);
                        SetSkill(SkillName.Archery, 95, 105);
                        SetSkill(SkillName.MagicResist, 95, 105);

                        this.RangeFight = 6;
                    } break;

                case GuardType.Wizard:
                    {
                        SetStr(75, 80);
                        SetDex(100, 125);
                        SetInt(175, 200);

                        SetHits(200, 250);
                        SetMana(200, 300);

                        SetSkill(SkillName.Tactics, 70, 85);
                        SetSkill(SkillName.Wrestling, 70, 85);
                        SetSkill(SkillName.Magery, 100, 130);
                        SetSkill(SkillName.EvalInt, 95, 105);
                        SetSkill(SkillName.Focus, 60, 70);
                        SetSkill(SkillName.Macing, 90, 100);
                        SetSkill(SkillName.MagicResist, 95, 105);
                        SetSkill(SkillName.Meditation, 90, 100);

                    } break;

                case GuardType.Medic:
                    {
                        SetStr(75, 80);
                        SetDex(100, 125);
                        SetInt(175, 200);

                        SetHits(200, 250);
                        SetMana(200, 300);

                        SetSkill(SkillName.Tactics, 40, 55);
                        SetSkill(SkillName.Wrestling, 90, 105);
                        SetSkill(SkillName.Magery, 100, 105);
                        SetSkill(SkillName.EvalInt, 55, 65);
                        SetSkill(SkillName.Focus, 60, 70);
                        SetSkill(SkillName.Meditation, 90, 100);
                        SetSkill(SkillName.MagicResist, 95, 105);

                    } break;
            }
        }

        private void AddEquipment(GuardType type)
        {
            AddItem(new Boots());
            AddItem(new Cloak(1155));
            AddItem(new BodySash(1155));

            switch (type)
            {
                default:
                case GuardType.Archer:
                    {
                        AddItem(new LeatherLegs());
                        AddItem(new StuddedChest());
                        AddItem(new LeatherGloves());
                        AddItem(new LeatherArms());

                        Bow bow = new Bow();
                        bow.Quality = WeaponQuality.Exceptional;

                        AddItem(bow);
                        AddToBackpack(new Arrow(200));
                    } break;
                case GuardType.Cavalry:
                    {
                        AddItem(new PlateLegs());
                        AddItem(new RingmailChest());
                        AddItem(new FancyShirt());
                        AddItem(new PlateGorget());
                        AddItem(new RingmailGloves());

                        BaseWeapon weapon;

                        if (Utility.RandomBool())
                            weapon = new Halberd();
                        else
                            weapon = new Bardiche();

                        weapon.Quality = WeaponQuality.Exceptional;
                        weapon.Resource = CraftResource.Gold;
                        weapon.Speed += 5;

                        AddItem(weapon);
                    } break;

                case GuardType.Pikeman:
                    {
                        AddItem(new RingmailLegs());
                        AddItem(new RingmailChest());
                        AddItem(new RingmailArms());
                        AddItem(new RingmailGloves());
                        AddItem(new PlateGorget());

                        if (Utility.RandomBool())
                            AddItem(new CloseHelm());
                        else
                            AddItem(new NorseHelm());

                        AddItem(new Pike());
                    }
                    break;

                case GuardType.Swordsman:
                    {
                        AddItem(new ChainLegs());
                        AddItem(new ChainChest());
                        AddItem(new RingmailArms());
                        AddItem(new RingmailGloves());
                        AddItem(new PlateGorget());

                        switch (Utility.Random(3))
                        {
                            case 0: AddItem(new CloseHelm()); break;
                            case 1: AddItem(new NorseHelm()); break;
                            case 2: AddItem(new PlateHelm()); break;
                        }

                        BaseWeapon weapon;

                        switch (Utility.Random(4))
                        {
                            default:
                            case 0: weapon = new Broadsword(); break;
                            case 1: weapon = new Longsword(); break;
                            case 2: weapon = new Katana(); break;
                            case 3: weapon = new Axe(); break;
                        }

                        weapon.Quality = WeaponQuality.Exceptional;
                        weapon.Resource = CraftResource.Gold;
                        weapon.Layer = Layer.OneHanded;

                        AddItem(weapon);

                        if (Utility.RandomBool())
                            AddItem(new HeaterShield());
                        else
                            AddItem(new MetalKiteShield());
                    } 
                    break;

                case GuardType.Wizard:
                    {
                        AddItem(new WizardsHat(Utility.RandomGreenHue()));
                        AddItem(new Robe(Utility.RandomGreenHue()));      
   
                        GnarledStaff staff = new GnarledStaff();
                        staff.Attributes.SpellChanneling = 1;
                        staff.Attributes.SpellDamage = Utility.RandomMinMax(4, 8);
                        AddItem(staff);
                    }
                    break;

                case GuardType.Medic:
                    {
                        AddItem(new Bandana(Utility.RandomGreenHue()));
                        AddItem(new Robe(Utility.RandomGreenHue()));

                    } 
                    break;
            }
        }
        #endregion

        public override bool BardImmune { get { return true; } }

        public override bool IsScaryToPets
        {
            get
            {
                return true;
            }
        }

        public override bool IsEnemy(Mobile m)
        {
            int noto = Server.Misc.NotorietyHandlers.MobileNotoriety(this, m);

            if (noto == Notoriety.Criminal || noto == Notoriety.Murderer || noto == Notoriety.Enemy)
                return true;

            return base.IsEnemy(m);
        }

        private DateTime _nextCallHelp;

        [CommandProperty(AccessLevel.Counselor, AccessLevel.Administrator)]
        public TimeSpan NextCallout
        {
            get
            {
                TimeSpan time = _nextCallHelp - DateTime.UtcNow;

                if (time < TimeSpan.Zero)
                    time = TimeSpan.Zero;

                return time;
            }
            set
            {
                try { _nextCallHelp = DateTime.UtcNow + value; }
                catch { }
            }
        }

        public override void OnThink()
        {
            //Adjust to control speed of recognition
            if (Utility.RandomDouble() > 0.66)
            {
                if (!SpellHelper.CheckCombat(this))
                {
                  foreach (Mobile mobile in this.GetMobilesInRange(16))
                  {
                    int noto = Server.Misc.NotorietyHandlers.MobileNotoriety(this, mobile);
                    bool isEnemy = (noto == Notoriety.Criminal || noto == Notoriety.Murderer || noto == Notoriety.Enemy);     
                 
                        if (this.Combatant == null && isEnemy && this.CanSee(mobile))
                        {
                               this.DoHarmful(mobile);
                        }                    
                    }  
                }
            }

            if (NextCallout == TimeSpan.Zero)
            {
                int toRescue = 0;

                if(Hits < (HitsMax * 0.33) && Combatant != null)
                {
                    switch(Utility.RandomMinMax( 1, 5 ))
                    {
                        case 1: Say("I am in dire need of assistance."); break;
                        case 2: Say("I don't think I'm going to make it."); break;
                        case 3: Say("I could use a hand over here."); break;
                        case 4: Say("I can't handle this alone."); break;
                        case 5: Say("This isn't going to end well for me."); break;
                        default: break;
                    }

                    NextCallout = TimeSpan.FromSeconds(Utility.RandomMinMax( 20, 40));

                    foreach (Mobile m in this.GetMobilesInRange(16))
                    {
                        if(m is Guard && m.Hits > (m.HitsMax * 0.33) && m != this)
                        {
                            if (Combatant != null && Combatant != m.Combatant)
                            {
                                if (toRescue == 0)
                                {                                
                                    switch (Utility.RandomMinMax(1, 4))
                                    {
                                        case 1: m.Say("I'm on the way."); break;
                                        case 2: m.Say("Hold tight {0} I've got you.", Name); break;
                                        case 3: m.Say("You're going to make it out of this."); break;
                                        case 4: m.Say("Just hang on {0} I'm coming.", Name); break;
                                        default: break;
                                    }
                                }

                                m.Combatant = Combatant;
                                m.DoHarmful(Combatant);

                                toRescue++;

                                if (toRescue > 1)
                                    return;
                            }
                        }
                    }
                }
            }

            base.OnThink();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write((int)m_Type);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Type = (GuardType)reader.ReadInt();
        }
    }
}
