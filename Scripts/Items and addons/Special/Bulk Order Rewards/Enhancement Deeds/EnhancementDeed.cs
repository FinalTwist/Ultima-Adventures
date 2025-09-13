using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Gumps;

namespace Server.Custom
{
    public enum EnhancementType
    {
        Aos, Armor, Element, Weapon, Skill
    }

    public class EnhancementDeed : Item
    {
        private EnhancementType m_DeedType;
        private AosAttribute m_Attribute;
        private AosArmorAttribute m_ArmorAttribute;
        private AosElementAttribute m_ElementAttribute;
        private AosWeaponAttribute m_WeaponAttribute;
        private SkillName m_BonusSkill;
        private int m_DeedValue;

        public AosAttribute Attribute { get { return m_Attribute; } set { m_Attribute = value; } }
        public AosArmorAttribute ArmorAttribute { get { return m_ArmorAttribute; } set { m_ArmorAttribute = value; } }
        public AosElementAttribute ElementAttribute { get { return m_ElementAttribute; } set { m_ElementAttribute = value; } }
        public AosWeaponAttribute WeaponAttribute { get { return m_WeaponAttribute; } set { m_WeaponAttribute = value; } }
        public SkillName BonusSkill { get { return m_BonusSkill; } set { m_BonusSkill = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DeedValue { get { return m_DeedValue; } set { m_DeedValue = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public EnhancementType DeedType { get { return m_DeedType; } }

        [Constructable]
        public EnhancementDeed()
            : this((EnhancementType)Utility.RandomMinMax(0,4), 0)
        {
        }
        [Constructable]
        public EnhancementDeed(EnhancementType type, int flagAttribute, int deedValue)
            : base(0x14F0)
        {
            m_DeedType = type;
            m_DeedValue = deedValue;

            switch (m_DeedType)
            {
                case EnhancementType.Aos: Attribute = (AosAttribute)flagAttribute; break;
                case EnhancementType.Armor: ArmorAttribute = (AosArmorAttribute)flagAttribute; break;
                case EnhancementType.Element: ElementAttribute = (AosElementAttribute)flagAttribute; break;
                case EnhancementType.Skill: BonusSkill = (SkillName)flagAttribute; break;
                case EnhancementType.Weapon: WeaponAttribute = (AosWeaponAttribute)flagAttribute; break;
            }
        }

        [Constructable]
        public EnhancementDeed(EnhancementType type, int deedValue)
            : base(0x14F0)
        {
            Weight = 0.5;
            Name = "Enhancement knowledge";
            Hue = 544;
            m_DeedType = type;
            int num;
            int[] nums = new int[] { 0x00000001, 0x00000002, 0x00000004, 0x00000008, 0x00000010, 0x00000020, 0x00000040,
                0x00000080, 0x00000100, 0x00000200, 0x00000400, 0x00000800, 0x00001000, 0x00002000, 0x00004000, 0x00008000,
                0x00010000, 0x00020000, 0x00040000, 0x00080000, 0x00100000, 0x00200000, 0x00400000, 0x00800000, 0x01000000 };
            switch (type)
            {
                case EnhancementType.Aos:
                    {
                        num = Enum.GetNames(typeof(AosAttribute)).Length;
                        Attribute = (AosAttribute)nums[Utility.Random(num)];
                        if (deedValue == 0) m_DeedValue = EnhancementUtility.GetValue(Attribute);
                        else m_DeedValue = deedValue;
                        break;
                    }
                case EnhancementType.Armor:
                    {
                        num = Enum.GetNames(typeof(AosArmorAttribute)).Length;
                        ArmorAttribute = (AosArmorAttribute)nums[Utility.Random(num)];
                        if (deedValue == 0) m_DeedValue = EnhancementUtility.GetValue(ArmorAttribute);
                        else m_DeedValue = deedValue;
                        break;
                    }
                case EnhancementType.Element:
                    {
                        num = Enum.GetNames(typeof(AosElementAttribute)).Length;
                        ElementAttribute = (AosElementAttribute)nums[Utility.Random(num)];
                        if (deedValue == 0) m_DeedValue = EnhancementUtility.GetValue(ElementAttribute);
                        else m_DeedValue = deedValue;
                        break;
                    }
                case EnhancementType.Skill:
                    {
                        num = Enum.GetNames(typeof(SkillName)).Length;
                        BonusSkill = (SkillName)Utility.Random(num);
                        if (deedValue == 0) m_DeedValue = EnhancementUtility.GetValue(BonusSkill);
                        else m_DeedValue = deedValue;
                        break;
                    }
                case EnhancementType.Weapon:
                    {
                        num = Enum.GetNames(typeof(AosWeaponAttribute)).Length;
                        WeaponAttribute = (AosWeaponAttribute)nums[Utility.Random(num)];
                        if (deedValue == 0) m_DeedValue = EnhancementUtility.GetValue(WeaponAttribute);
                        else m_DeedValue = deedValue;
                        break;
                    }
            }
        }

        public EnhancementDeed(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            switch (DeedType)
            {
                case EnhancementType.Aos: list.Add("{0} Enhancement Deed: {1} : +{2}", DeedType, Attribute, DeedValue); break;
                case EnhancementType.Armor: list.Add("{0} Enhancement Deed: {1} : +{2}", DeedType, ArmorAttribute, DeedValue); break;
                case EnhancementType.Element: list.Add("{0} Enhancement Deed: {1} : +{2}", DeedType, ElementAttribute, DeedValue); break;
                case EnhancementType.Skill: list.Add("{0} Enhancement Deed: {1} : +{2}", DeedType, BonusSkill, DeedValue); break;
                case EnhancementType.Weapon: list.Add("{0} Enhancement Deed: {1} : +{2}", DeedType, WeaponAttribute, DeedValue); break;
            }
            list.Add("Double-click, then {0}", UseMessage);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
            else
            {
                from.SendMessage(UseMessage); //"Target the ... you wish to enhance."
                from.Target = new EnhanceTarget(from, this);
            }
        }

        public bool Enhance(Mobile from, Spellbook book)
        {
            int attribute;
            int maxvalue;
            if (DeedType == EnhancementType.Aos)
            {
                attribute = (int)book.Attributes[Attribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(Attribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        book.Attributes[Attribute] = maxvalue;
                    }
                    else
                        book.Attributes[Attribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Skill)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (book.SkillBonuses.GetSkill(x) == BonusSkill)
                    {
                        attribute = (int)book.SkillBonuses.GetBonus(x);
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            if (attribute + DeedValue >= maxvalue)
                                book.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                book.SkillBonuses.SetBonus(x, book.SkillBonuses.GetBonus(x) + (double)DeedValue);
                            return true;
                        }
                        return false;
                    }
                }
                for (int x = 0; x < 5; x++)
                {
                    if (book.SkillBonuses.GetBonus(x) == 0.0)
                    {
                        attribute = 0;
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            book.SkillBonuses.SetSkill(x, BonusSkill);
                            if (attribute + DeedValue >= maxvalue)
                                book.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                book.SkillBonuses.SetBonus(x, (double)DeedValue);
                            return true;
                        }
                    }
                }
                return false;
            }
            else return false;
        }

        public bool Enhance(Mobile from, BaseJewel jewel)
        {
            int attribute;
            int maxvalue;

            if (jewel is BootsofHermes)
                return false;

            if (DeedType == EnhancementType.Aos)
            {
                attribute = (int)jewel.Attributes[Attribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(Attribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        jewel.Attributes[Attribute] = maxvalue;
                    }
                    else
                        jewel.Attributes[Attribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Element)
            {
                attribute = (int)jewel.Resistances[ElementAttribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(ElementAttribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        jewel.Resistances[ElementAttribute] = maxvalue;
                    }
                    else
                        jewel.Resistances[ElementAttribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Skill)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (jewel.SkillBonuses.GetSkill(x) == BonusSkill)
                    {
                        attribute = (int)jewel.SkillBonuses.GetBonus(x);
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            if (attribute + DeedValue >= maxvalue)
                                jewel.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                jewel.SkillBonuses.SetBonus(x, jewel.SkillBonuses.GetBonus(x) + (double)DeedValue);
                            return true;
                        }
                        return false;
                    }
                }
                for (int x = 0; x < 5; x++)
                {
                    if (jewel.SkillBonuses.GetBonus(x) == 0.0)
                    {
                        attribute = 0;
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            jewel.SkillBonuses.SetSkill(x, BonusSkill);
                            if (attribute + DeedValue >= maxvalue)
                                jewel.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                jewel.SkillBonuses.SetBonus(x, (double)DeedValue);
                            return true;
                        }
                    }
                }
                return false;
            }
            else return false;
        }

        public bool Enhance(Mobile from, BaseClothing clothing)
        {
            int attribute;
            int maxvalue;
            if (DeedType == EnhancementType.Aos)
            {
                attribute = (int)clothing.Attributes[Attribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(Attribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        clothing.Attributes[Attribute] = maxvalue;
                    }
                    else
                        clothing.Attributes[Attribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Armor)
            {
                attribute = (int)clothing.ClothingAttributes[ArmorAttribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(ArmorAttribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        clothing.ClothingAttributes[ArmorAttribute] = maxvalue;
                    }
                    else
                        clothing.ClothingAttributes[ArmorAttribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Element)
            {
                attribute = (int)clothing.Resistances[ElementAttribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(ElementAttribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        clothing.Resistances[ElementAttribute] = maxvalue;
                    }
                    else
                        clothing.Resistances[ElementAttribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Skill)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (clothing.SkillBonuses.GetSkill(x) == BonusSkill)
                    {
                        attribute = (int)clothing.SkillBonuses.GetBonus(x);
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            if (attribute + DeedValue >= maxvalue)
                                clothing.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                clothing.SkillBonuses.SetBonus(x, clothing.SkillBonuses.GetBonus(x) + (double)DeedValue);
                            return true;
                        }
                        return false;
                    }
                }
                for (int x = 0; x < 5; x++)
                {
                    if (clothing.SkillBonuses.GetBonus(x) == 0.0)
                    {
                        attribute = 0;
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            clothing.SkillBonuses.SetSkill(x, BonusSkill);
                            if (attribute + DeedValue >= maxvalue)
                                clothing.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                clothing.SkillBonuses.SetBonus(x, (double)DeedValue);
                            return true;
                        }
                    }
                }
                return false;
            }
            else return false;
        }

        public bool Enhance(Mobile from, BaseArmor armor)
        {
            int attribute;
            int maxvalue;
            if (DeedType == EnhancementType.Aos)
            {
                attribute = (int)armor.Attributes[Attribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(Attribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        armor.Attributes[Attribute] = maxvalue;
                    }
                    else
                        armor.Attributes[Attribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Armor)
            {
                attribute = (int)armor.ArmorAttributes[ArmorAttribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(ArmorAttribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        armor.ArmorAttributes[ArmorAttribute] = maxvalue;
                    }
                    else
                        armor.ArmorAttributes[ArmorAttribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Skill)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (armor.SkillBonuses.GetSkill(x) == BonusSkill)
                    {
                        attribute = (int)armor.SkillBonuses.GetBonus(x);
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            if (attribute + DeedValue >= maxvalue)
                                armor.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                armor.SkillBonuses.SetBonus(x, armor.SkillBonuses.GetBonus(x) + (double)DeedValue);
                            return true;
                        }
                        return false;
                    }
                }
                for (int x = 0; x < 5; x++)
                {
                    if (armor.SkillBonuses.GetBonus(x) == 0.0)
                    {
                        attribute = 0;
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            armor.SkillBonuses.SetSkill(x, BonusSkill);
                            if (attribute + DeedValue >= maxvalue)
                                armor.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                armor.SkillBonuses.SetBonus(x, (double)DeedValue);
                            return true;
                        }
                    }
                }
                return false;
            }
            else return false;
        }

        public bool Enhance(Mobile from, BaseWeapon weapon)
        {
            int attribute;
            int maxvalue;
            if (DeedType == EnhancementType.Aos)
            {
                attribute = (int)weapon.Attributes[Attribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(Attribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        weapon.Attributes[Attribute] = maxvalue;
                    }
                    else
                        weapon.Attributes[Attribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Weapon)
            {
                attribute = (int)weapon.WeaponAttributes[WeaponAttribute];
                maxvalue = (int)EnhancementUtility.GetMaxValue(WeaponAttribute);
                if (attribute < maxvalue)
                {
                    if (attribute + DeedValue >= maxvalue)
                    {
                        weapon.WeaponAttributes[WeaponAttribute] = maxvalue;
                    }
                    else
                        weapon.WeaponAttributes[WeaponAttribute] += DeedValue;
                    return true;
                }
                else return false;
            }
            else if (DeedType == EnhancementType.Skill)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (weapon.SkillBonuses.GetSkill(x) == BonusSkill)
                    {
                        attribute = (int)weapon.SkillBonuses.GetBonus(x);
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            if (attribute + DeedValue >= maxvalue)
                                weapon.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                weapon.SkillBonuses.SetBonus(x, weapon.SkillBonuses.GetBonus(x) + (double)DeedValue);
                            return true;
                        }
                        return false;
                    }
                }
                for (int x = 0; x < 5; x++)
                {
                    if (weapon.SkillBonuses.GetBonus(x) == 0.0)
                    {
                        attribute = 0;
                        maxvalue = (int)EnhancementUtility.GetMaxValue(BonusSkill);
                        if (attribute < maxvalue)
                        {
                            weapon.SkillBonuses.SetSkill(x, BonusSkill);
                            if (attribute + DeedValue >= maxvalue)
                                weapon.SkillBonuses.SetBonus(x, (double)maxvalue);
                            else
                                weapon.SkillBonuses.SetBonus(x, (double)DeedValue);
                            return true;
                        }
                    }
                }
                return false;
            }
            return false;
        }

        public string UseMessage
        {
            get
            {
                switch (DeedType)
                {
                    case EnhancementType.Weapon: return "target the weapon you wish to enhance.";
                    case EnhancementType.Armor: return "target the clothing, or armor you wish to enhance.";
                    case EnhancementType.Element: return "target the clothing, or jewel you wish to enhance.";
                    case EnhancementType.Aos: return "target the clothing, armor, spellbook, jewel, or weapon you wish to enhance.";
                    case EnhancementType.Skill: return "target the clothing, armor, spellbook, jewel, or weapon you wish to enhance.";
                }
                return "Invalid Deed.";
            }
        }

        private class EnhanceTarget : Target
        {
            private EnhancementDeed m_Deed;

            public EnhanceTarget(Mobile from, EnhancementDeed deed) : base(-1, false, TargetFlags.None) { m_Deed = deed; }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!EnhancementUtility.EDStoneExists())
                {
                    if (from.AccessLevel >= AccessLevel.Administrator)
                    {
                        EDStone eds = new EDStone();
                        eds.MoveToWorld(from.Location, from.Map);
                    }
                    else
                    {
                        from.SendMessage("Please ask an Admin to initialize the Enhancement Deed system.");
                        Console.WriteLine("Please Initialize the Enhancement Deed system by creating an EDStone.");
                        return;
                    }
                }

                if (targeted is BaseWeapon)
                {
                    BaseWeapon weapon = targeted as BaseWeapon;
                    if (weapon.IsChildOf(from.Backpack))
                        if (m_Deed.Enhance(from, weapon))
                        {
                            from.SendMessage("You have successfully enhanced the weapon!");
                            EnhancementUtility.Increment(from, weapon);
                            m_Deed.Delete();
                        }
                        else
                            from.SendMessage("Unable to enhance that weapon!");
                    else
                        from.SendMessage("You must target an item in your pack.");
                }
                else if (targeted is BaseArmor)
                {
                    BaseArmor armor = targeted as BaseArmor;
                    if (armor.IsChildOf(from.Backpack))
                        if (m_Deed.Enhance(from, armor))
                        {
                            from.SendMessage("You have successfully enhanced your armor!");
                            EnhancementUtility.Increment(from, armor);
                            m_Deed.Delete();
                        }
                        else
                            from.SendMessage("Unable to enhance that armor!");
                    else
                        from.SendMessage("You must target an item in your pack.");
                }
                else if (targeted is BaseClothing)
                {
                    BaseClothing clothing = targeted as BaseClothing;
                    if (clothing.IsChildOf(from.Backpack))
                        if (m_Deed.Enhance(from, clothing))
                        {
                            from.SendMessage("You have successfully enhanced your clothing!");
                            EnhancementUtility.Increment(from, clothing);
                            m_Deed.Delete();
                        }
                        else
                            from.SendMessage("Unable to enhance that clothing!");
                    else
                        from.SendMessage("You must target an item in your pack.");
                }
                else if (targeted is BaseJewel)
                {
                    BaseJewel jewel = targeted as BaseJewel;
                    if (jewel.IsChildOf(from.Backpack))
                        if (m_Deed.Enhance(from, jewel))
                        {
                            from.SendMessage("You have successfully enhanced your jewelry!");
                            EnhancementUtility.Increment(from, jewel);
                            m_Deed.Delete();
                        }
                        else
                            from.SendMessage("Unable to enhance that jewelry!");
                    else
                        from.SendMessage("You must target an item in your pack.");
                }
                else if (targeted is Spellbook)
                {
                    Spellbook book = targeted as Spellbook;
                    if (book.IsChildOf(from.Backpack))
                        if (m_Deed.Enhance(from, book))
                        {
                            from.SendMessage("You have successfully enhanced your spell book!");
                            EnhancementUtility.Increment(from, book);
                            m_Deed.Delete();
                        }
                        else
                            from.SendMessage("Unable to enhance that spell book!");
                    else
                        from.SendMessage("You must target an item in your pack.");
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)m_DeedType);
            writer.Write((int)m_Attribute);
            writer.Write((int)m_ArmorAttribute);
            writer.Write((int)m_ElementAttribute);
            writer.Write((int)m_WeaponAttribute);
            writer.Write((int)m_BonusSkill);
            writer.Write((int)m_DeedValue);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 1:
                    {
                        m_DeedType = (EnhancementType)reader.ReadInt();
                        m_Attribute = (AosAttribute)reader.ReadInt();
                        m_ArmorAttribute = (AosArmorAttribute)reader.ReadInt();
                        m_ElementAttribute = (AosElementAttribute)reader.ReadInt();
                        m_WeaponAttribute = (AosWeaponAttribute)reader.ReadInt();
                        m_BonusSkill = (SkillName)reader.ReadInt();
                        m_DeedValue = reader.ReadInt();
                        goto case 0;
                    }
                case 0: break;
            }
        }

    }
}