using System.Collections.Generic;
using Server.ContextMenus;
using Server.Custom;
using Server.Engines.Craft;
using Server.Mobiles;

namespace Server.Items
{
    public class GogglesofScienceV2 : BaseGiftArmor, ITinkerRepairable, ISpecializationObserver, IDisableableItem
    {
        public override int BasePhysicalResistance { get { return 10; } }
        public override int BaseFireResistance { get { return 10; } }
        public override int BaseColdResistance { get { return 10; } }
        public override int BasePoisonResistance { get { return 10; } }
        public override int BaseEnergyResistance { get { return 10; } }

        public override int InitMinHits { get { return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits { get { return Utility.RandomMinMax(126, 150); } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
        public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.All; } }

        private PlayerMobile _boundTo;

        public bool IsDisabled
        {
            get
            {
                return _boundTo == null || _boundTo.CustomClass != SpecializationType.Alchemist;
            }
        }

        [Constructable]
        public GogglesofScienceV2() : base(0x2FB8)
        {
            Name = "Goggles of Science";
            Hue = 26;
        }

        public GogglesofScienceV2(Serial serial) : base(serial)
        {
        }

        public override bool CanEquip(Mobile from)
        {
            var canEquip = base.CanEquip(from);
            if (!canEquip) return false;
            if (_boundTo == null || _boundTo == from) return true;

            from.SendAsciiMessage("These goggles are fit to another.");

            return false;
        }

        public override bool OnEquip(Mobile from)
        {
            var equipped = base.OnEquip(from);
            if (!equipped) return false;

            var player = from as PlayerMobile;
            if (player == null) return false;

            if (_boundTo == null)
            {
                _boundTo = player;
                m_Points = 2 * (int)player.Skills[SkillName.Alchemy].Base; // 2 points per 1 Alchemy
                InvalidateProperties();

                player.SendAsciiMessage("Your Alchemical experience allows you to adjust your PPE.");
            }

            return true;
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            if (m_Points > 0) { list.Add(new GiftInfoEntry(from, this, GiftAttributeCategory.Melee) { Number = 1011355 }); } // "Customize"
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            if (IsDisabled)
            {
                base.AddNameProperty(list);
                list.Add(1049644, "Required Class: Chemist");
                if (_boundTo == null) list.Add(1070722, "Sized to your character");
                else if (m_Points > 5) list.Add(1070722, "Single Click to Customize");
            }
            else
            {
                base.GetProperties(list);
            }

            if (_boundTo != null) list.Add(1070722, "Perfectly fits " + _boundTo.Name);
        }


        public void SpecializationUpdated(PlayerMobile player, SpecializationType specialization)
        {
            if (IsDisabled) RemoveStatMods(player);
            else AddStatMods(player);

            InvalidateProperties();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.WriteMobile(_boundTo);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            _boundTo = reader.ReadMobile() as PlayerMobile;
        }
    }
}