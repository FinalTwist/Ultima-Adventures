using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
   public class ParagonTarget : Target
   {
      private ParagonPetDeed m_Deed;

      public ParagonTarget(ParagonPetDeed deed) : base(1, false, TargetFlags.None)
      {
         m_Deed = deed;
      }

      protected override void OnTarget(Mobile from, object target)
      {
         if (target != null && target is BaseCreature)
         {
            BaseCreature t = (BaseCreature)target;

            if (t.IsParagon == true)
            {
               from.SendMessage("That pet is already a paragon!");
            }
            else if (t.ControlMaster != from)
            {
               from.SendMessage("That is not your pet!");
            }
            else
            {
               bool bonded = false;
               if (t.IsBonded)
                  bonded = true;

               t.IsParagon = true;
               from.SendMessage("Your pet is now a paragon!");

               m_Deed.Delete(); // Delete the deed 
               t.Tamable = true;
               if (bonded)
                  t.IsBonded = true; // issue where bonded pets got unbonded 
            }

         }
         else
         {
            from.SendMessage("That is not a valid traget.");
         }
      }
   }

   public class ParagonPetDeed : Item // Create the item class which is derived from the base item class 
   {
      [Constructable]
      public ParagonPetDeed() : base(0x14F0)
      {
         Weight = 1.0;
         Name = "a paragon pet deed";
         //LootType = LootType.Blessed; 
         Hue = 1152;
      }

      public ParagonPetDeed(Serial serial) : base(serial)
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
         //LootType = LootType.Blessed; 

         int version = reader.ReadInt();
      }

      public override bool DisplayLootType { get { return false; } }

      public override void OnDoubleClick(Mobile from) // Override double click of the deed to call our target 
      {
         if (!IsChildOf(from.Backpack)) // Make sure its in their pack 
         {
            from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it. 
         }
         else
         {
            from.SendMessage("Choose the pet you wish to make a paragon.");
            from.Target = new ParagonTarget(this); // Call our target 
         }
      }
   }
}