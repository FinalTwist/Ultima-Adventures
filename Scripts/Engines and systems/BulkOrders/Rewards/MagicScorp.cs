using Server.Targeting;

namespace Server.Items
{
    public class MagicScorp : Item
    {
        [Constructable]
        public MagicScorp() : base(0x10E7)
        {
            Weight = 1.0;
            Hue = 0xAFA;
            Name = "magical scorp";
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1070722, "Changes the Name and Appearance of wooden armor");
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1060640); // The item must be in your backpack to use it.
            }
            else
            {
                from.SendMessage("What do you want to use the scorp on?");
                Target t = new InternalTarget(this);
                from.Target = t;
            }
        }

        private class InternalTarget : Target
        {
            private MagicScorp m_Tool;

            public InternalTarget(MagicScorp scorp) : base(1, false, TargetFlags.None)
            {
                m_Tool = scorp;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from == null || targeted == null || !(targeted is Item))
                    return;

                bool DoEffects = false;
                Item iWear = targeted as Item;

                string NewName = null;

                if (!iWear.IsChildOf(from.Backpack))
                {
                    from.SendMessage("You can only use this scorp on items in your pack.");
                }
                else if (
                    iWear is WoodenPlateHelm || iWear is GiftWoodenPlateHelm || iWear is LevelWoodenPlateHelm
                    || iWear is WoodenPlateGloves || iWear is GiftWoodenPlateGloves || iWear is LevelWoodenPlateGloves
                    || iWear is WoodenPlateChest || iWear is GiftWoodenPlateChest || iWear is LevelWoodenPlateChest
                    || iWear is WoodenPlateLegs || iWear is GiftWoodenPlateLegs || iWear is LevelWoodenPlateLegs
                    || iWear is WoodenPlateArms || iWear is GiftWoodenPlateArms || iWear is LevelWoodenPlateArms
                    || iWear is WoodenPlateGorget || iWear is GiftWoodenPlateGorget || iWear is LevelWoodenPlateGorget
                    )
                {
                    if (iWear.ItemID == 0x1966) { DoEffects = true; iWear.ItemID = 0x2645; NewName = "dragon helm (wood)"; }
                    else if (iWear.ItemID == 0x2645 || iWear.ItemID == 0x2646) { DoEffects = true; iWear.ItemID = 0x2FBB; NewName = "dread helm (wood)"; }
                    else if (iWear.ItemID == 0x2FBB) { DoEffects = true; iWear.ItemID = 0x2B10; NewName = "royal helm (wood)"; }
                    else if (iWear.ItemID == 0x2B10 || iWear.ItemID == 0x2B11) { DoEffects = true; iWear.ItemID = 0x1412; NewName = "platemail helm (wood)"; }
                    else if (iWear.ItemID == 0x1412 || iWear.ItemID == 0x1419) { DoEffects = true; iWear.ItemID = 0x1F0B; NewName = "horned helm (wood)"; }
                    else if (iWear.ItemID == 0x1F0B || iWear.ItemID == 0x1F0C) { DoEffects = true; iWear.ItemID = 0x140E; NewName = "norse helm (wood)"; }
                    else if (iWear.ItemID == 0x140E || iWear.ItemID == 0x140F) { DoEffects = true; iWear.ItemID = 0x140A; NewName = "helmet (wood)"; }
                    else if (iWear.ItemID == 0x140A || iWear.ItemID == 0x140B) { DoEffects = true; iWear.ItemID = 0x1451; NewName = "bone helm (wood)"; }
                    else if (iWear.ItemID == 0x1451 || iWear.ItemID == 0x1456) { DoEffects = true; iWear.ItemID = 0x13BB; NewName = "chainmail coif (wood)"; }
                    else if (iWear.ItemID == 0x13BB || iWear.ItemID == 0x13C0) { DoEffects = true; iWear.ItemID = 0x1408; NewName = "close helm (wood)"; }
                    else if (iWear.ItemID == 0x1408 || iWear.ItemID == 0x1409) { DoEffects = true; iWear.ItemID = 0x140C; NewName = "bascinet (wood)"; }
                    else if (iWear.ItemID == 0x140C || iWear.ItemID == 0x140D) { DoEffects = true; iWear.ItemID = 0x2774; NewName = "chainmail hatsuburi (wood)"; }
                    else if (iWear.ItemID == 0x2774) { DoEffects = true; iWear.ItemID = 0x2781; NewName = "platemail jingasa (wood)"; }
                    else if (iWear.ItemID == 0x2781) { DoEffects = true; iWear.ItemID = 0x2777; NewName = "platemail jingasa (wood)"; }
                    else if (iWear.ItemID == 0x2777) { DoEffects = true; iWear.ItemID = 0x2775; NewName = "platemail hatsuburi (wood)"; }
                    else if (iWear.ItemID == 0x2775 || iWear.ItemID == 0x27C0) { DoEffects = true; iWear.ItemID = 0x2789; NewName = "platemail kabuto (wood)"; }
                    else if (iWear.ItemID == 0x2789 || iWear.ItemID == 0x27D4) { DoEffects = true; iWear.ItemID = 0x2785; NewName = "platemail kabuto (wood)"; }
                    else if (iWear.ItemID == 0x2785 || iWear.ItemID == 0x27D0) { DoEffects = true; iWear.ItemID = 0x2784; NewName = "platemail jingasa (wood)"; }
                    else if (iWear.ItemID == 0x2784) { DoEffects = true; iWear.ItemID = 0x2778; NewName = "platemail kabuto (wood)"; }
                    else if (iWear.ItemID == 0x2778 || iWear.ItemID == 0x27C3) { DoEffects = true; iWear.ItemID = 0x1966; NewName = "wooden helm"; }
                    else if (iWear.ItemID == 0x1968) { DoEffects = true; iWear.ItemID = 0x1450; NewName = "bone gloves (wood)"; }
                    else if (iWear.ItemID == 0x1450 || iWear.ItemID == 0x1455) { DoEffects = true; iWear.ItemID = 0x1414; NewName = "platemail gloves (wood)"; }
                    else if (iWear.ItemID == 0x1414 || iWear.ItemID == 0x1418) { DoEffects = true; iWear.ItemID = 0x2B0C; NewName = "royal bracers (wood)"; }
                    else if (iWear.ItemID == 0x2B0C || iWear.ItemID == 0x2B0D) { DoEffects = true; iWear.ItemID = 0x13eb; NewName = "ringmail gloves (wood)"; }
                    else if (iWear.ItemID == 0x13eb || iWear.ItemID == 0x13f2) { DoEffects = true; iWear.ItemID = 0x2643; NewName = "scalemail gloves (wood)"; }
                    else if (iWear.ItemID == 0x2643 || iWear.ItemID == 0x2644) { DoEffects = true; iWear.ItemID = 0x1968; NewName = "wooden gloves"; }
                    else if (iWear.ItemID == 0x1969) { DoEffects = true; iWear.ItemID = 0x13BF; NewName = "chainmail tunic (wood)"; }
                    else if (iWear.ItemID == 0x13BF || iWear.ItemID == 0x13C4) { DoEffects = true; iWear.ItemID = 0x13EC; NewName = "ringmail tunic (wood)"; }
                    else if (iWear.ItemID == 0x13EC || iWear.ItemID == 0x13ED) { DoEffects = true; iWear.ItemID = 0x1415; NewName = "platemail tunic (wood)"; }
                    else if (iWear.ItemID == 0x1415 || iWear.ItemID == 0x1416) { DoEffects = true; iWear.ItemID = 0x2B08; NewName = "royal tunic (wood)"; }
                    else if (iWear.ItemID == 0x2B08 || iWear.ItemID == 0x2B09) { DoEffects = true; iWear.ItemID = 0x277D; NewName = "platemail do (wood)"; }
                    else if (iWear.ItemID == 0x277D || iWear.ItemID == 0x27C8) { DoEffects = true; iWear.ItemID = 0x2641; NewName = "scalemail tunic (wood)"; }
                    else if (iWear.ItemID == 0x2641 || iWear.ItemID == 0x2642) { DoEffects = true; iWear.ItemID = 0x1969; NewName = "wooden tunic"; }
                    else if (iWear.ItemID == 0x1965) { DoEffects = true; iWear.ItemID = 0x2647; NewName = "scalemail leggings (wood)"; }
                    else if (iWear.ItemID == 0x2647 || iWear.ItemID == 0x2648) { DoEffects = true; iWear.ItemID = 0x2788; NewName = "platemail suneate (wood)"; }
                    else if (iWear.ItemID == 0x2788 || iWear.ItemID == 0x27D3) { DoEffects = true; iWear.ItemID = 0x278D; NewName = "platemail haidate (wood)"; }
                    else if (iWear.ItemID == 0x278D || iWear.ItemID == 0x27D8) { DoEffects = true; iWear.ItemID = 0x13BE; NewName = "chainmail leggings (wood)"; }
                    else if (iWear.ItemID == 0x13BE || iWear.ItemID == 0x13C3) { DoEffects = true; iWear.ItemID = 0x13F0; NewName = "ringmail leggings (wood)"; }
                    else if (iWear.ItemID == 0x13F0 || iWear.ItemID == 0x13F1) { DoEffects = true; iWear.ItemID = 0x1411; NewName = "platemail leggings (wood)"; }
                    else if (iWear.ItemID == 0x1411 || iWear.ItemID == 0x141A) { DoEffects = true; iWear.ItemID = 0x2B06; NewName = "royal leggings (wood)"; }
                    else if (iWear.ItemID == 0x2B06 || iWear.ItemID == 0x2B07) { DoEffects = true; iWear.ItemID = 0x1965; NewName = "wooden leggings"; }
                    else if (iWear.ItemID == 0x1964) { DoEffects = true; iWear.ItemID = 0x13EE; NewName = "ringmail arms (wood)"; }
                    else if (iWear.ItemID == 0x13EE || iWear.ItemID == 0x13EF) { DoEffects = true; iWear.ItemID = 0x1410; NewName = "platemail arms (wood)"; }
                    else if (iWear.ItemID == 0x1410 || iWear.ItemID == 0x1417) { DoEffects = true; iWear.ItemID = 0x2B0A; NewName = "royal arms (wood)"; }
                    else if (iWear.ItemID == 0x2B0A || iWear.ItemID == 0x2B0B) { DoEffects = true; iWear.ItemID = 0x2780; NewName = "platemail kote (wood)"; }
                    else if (iWear.ItemID == 0x2780 || iWear.ItemID == 0x27CB) { DoEffects = true; iWear.ItemID = 0x2657; NewName = "scalemail arms (wood)"; }
                    else if (iWear.ItemID == 0x2657 || iWear.ItemID == 0x2658) { DoEffects = true; iWear.ItemID = 0x1964; NewName = "wooden arms"; }
                    else if (iWear.ItemID == 0x1967) { DoEffects = true; iWear.ItemID = 0x1413; NewName = "platemail gorget (wood)"; }
                    else if (iWear.ItemID == 0x1413 || iWear.ItemID == 0x264B || iWear.ItemID == 0x264C) { DoEffects = true; iWear.ItemID = 0x2B0E; NewName = "royal gorget (wood)"; }
                    else if (iWear.ItemID == 0x2B0E || iWear.ItemID == 0x2B0F) { DoEffects = true; iWear.ItemID = 0x2779; NewName = "platemail mempo (wood)"; }
                    else if (iWear.ItemID == 0x2779 || iWear.ItemID == 0x27C4) { DoEffects = true; iWear.ItemID = 0x1967; NewName = "wooden gorget"; }
                }

                if (DoEffects)
                {
                    iWear.Name = Server.Misc.MaterialInfo.GetSpecialMaterialName(iWear) + NewName;
                    from.PlaySound(0x2A);
                    from.SendMessage("The scorp magical transforms the armor.");
                }
                else
                {
                    from.SendMessage("This scorp is not really meant to do that.");
                }
            }
        }

        public MagicScorp(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}