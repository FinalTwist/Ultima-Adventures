using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class GuildCraftingProcess
    {
        private int BaseCost = 100;
        private bool AttrCountAffectsCost = true;
        public int MaxAttrCount = 15;

        public Mobile Owner = null;
        public Item ItemToUpgrade = null;
        public int CurrentAttributeCount = 0;

        public GuildCraftingProcess(Mobile from, Item target)
        {
            Owner = from;
            ItemToUpgrade = target;
        }

        public void BeginProcess()
        {
            CurrentAttributeCount = 0;

            if (!(ItemToUpgrade is BaseShield || ItemToUpgrade is BaseClothing || ItemToUpgrade is BaseArmor || ItemToUpgrade is BaseWeapon || ItemToUpgrade is BaseJewel || ItemToUpgrade is Spellbook))
            {
                Owner.SendMessage("This cannot be enhanced.");
            }
            else
            {
                int MaxedAttributes = 0;

                foreach (AttributeHandler handler in AttributeHandler.Definitions)
                {
                    int attr = handler.Upgrade(ItemToUpgrade, true);
                    
                    if (attr > 0)
                        CurrentAttributeCount++;

                    if (attr >= handler.MaxValue)
                        MaxedAttributes++;
                }

                if (CurrentAttributeCount > MaxAttrCount || MaxedAttributes >= MaxAttrCount )
                    Owner.SendMessage("This piece of equipment cannot be enhanced any further.");
                else
                    Owner.SendGump(new EnhancementGump(this));
            }
        }

        public void BeginUpgrade(AttributeHandler handler)
        {
            if (GetCostToUpgrade(handler) < 1 )
			{
				Owner.SendMessage("This piece of equipment cannot be enhanced with that any further.");
			}
            else if (SpendGold(GetCostToUpgrade(handler)))
            {
                handler.Upgrade(ItemToUpgrade, false);
                BeginProcess();
            }
        }

        private bool SpendGold(int amount)
        {
            bool bought = (Owner.AccessLevel >= AccessLevel.GameMaster);
            bool fromBank = false;

            Container cont = Owner.Backpack;
            if (!bought && cont != null)
            {
                if (cont.ConsumeTotal(typeof(Gold), amount))
                    bought = true;
                else
                {
                    cont = Owner.FindBankNoCreate();
                    if (cont != null && cont.ConsumeTotal(typeof(Gold), amount))
                    {
                        bought = true;
                        fromBank = true;
                    }
                    else
                    {
                        Owner.SendLocalizedMessage(500192);
                    }
                }
            }

            if (bought)
            {
                if (Owner.AccessLevel >= AccessLevel.GameMaster)
                    Owner.SendMessage("{0} gold would have been withdrawn from your bank if you were not an admin.", amount);
                else if (fromBank)
                    Owner.SendMessage("The total of your purchase is {0} gold, which has been withdrawn from your bank account.", amount);
                else
                    Owner.SendMessage("The total of your purchase is {0} gold.", amount);
            }

			PlayerMobile pc = (PlayerMobile)Owner;
			if ( pc.NpcGuild == NpcGuild.TailorsGuild ){ Owner.PlaySound( 0x248 ); }
			else if ( pc.NpcGuild == NpcGuild.CarpentersGuild ){ Owner.PlaySound( 0x23D ); }
			else if ( pc.NpcGuild == NpcGuild.ArchersGuild ){ Owner.PlaySound( 0x55 ); }
			else if ( pc.NpcGuild == NpcGuild.TinkersGuild ){ Owner.PlaySound( 0x542 ); }
			else if ( pc.NpcGuild == NpcGuild.BlacksmithsGuild ){ Owner.PlaySound( 0x541 ); }

            return bought;
        }

		public bool IsCraftedByEnhancer( Item item, Mobile from )
		{
            Mobile crafter = null;

            if (item is BaseClothing) crafter = ((BaseClothing)item).Crafter;
            else if (item is BaseArmor) crafter = ((BaseArmor)item).Crafter;
            else if (item is BaseWeapon) crafter = ((BaseWeapon)item).Crafter;
            else if (item is BaseJewel) crafter = ((BaseJewel)item).Crafter;

            return crafter != null && crafter == from;
		}

        public int GetCostToUpgrade(AttributeHandler handler)
        {
            int attrMultiplier = 1;
			int gold = BaseCost;
				if ( IsCraftedByEnhancer( ItemToUpgrade, Owner ) ){ gold = (int)( gold / 2 ); }

            if (AttrCountAffectsCost)
            {
                foreach (AttributeHandler h in AttributeHandler.Definitions)
                    if (h.Name != handler.Name && h.Upgrade(ItemToUpgrade, true) > 0)
                        attrMultiplier++;
            }

            int cost = 0;

            int max = handler.MaxValue;
			int inc = handler.IncrementValue;
            int lvl = handler.Upgrade(ItemToUpgrade, true);

			if ( lvl < max )
			{
				cost = ((lvl+1)*handler.Cost)*gold;
			}

            cost = (int)(cost * attrMultiplier);

            return cost;
        }
    }
}