using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Engines.Craft;
using Server.Items;

namespace Server.Items
{
    class FlashPowder : Item, ICraftable, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
    
        public override int LabelNumber
        {
            get
            {
                return 1063574;
            }
        }

        [Constructable]
        public FlashPowder(int amount)
            : base(0x423A)
        {
            Stackable = true;
            Weight = 1.0;
            Amount = amount;
        }
      
        [Constructable]
        public FlashPowder()
            : this(1)
        {
        }
        public FlashPowder(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); //version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 1))
            {
                PlayEffect(from);
                Consume();
            }
        }
     

        public static void PlayEffect(Mobile from)
        {
            from.RevealingAction();

            Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
            Effects.PlaySound(from.Location, from.Map, 0x2B2);

            if (from.Paralyzed)
                from.Paralyzed = false;
            
        }

        protected override void OnAmountChange(int oldValue)
        {
            int newValue = this.Amount;

            UpdateTotal(this, TotalType.Items, newValue - oldValue);
        }

        public override int GetTotal(TotalType type)
        {
            int total = base.GetTotal(type);

            if (type == TotalType.Items)
            {
                return Amount - 1;          // RunUO seems to treat TotalItems as a 0-based count for some reason
            }

            return total;
        }

        public int OnCraft(int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue)
        {
            if (craftSystem is DefTinkering)
            {
                return 1;
            }

            return -1;
        }
    }
}
