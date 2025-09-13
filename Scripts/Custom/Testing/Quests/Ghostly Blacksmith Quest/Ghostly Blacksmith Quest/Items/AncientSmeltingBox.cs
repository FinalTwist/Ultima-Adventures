/* Created by Hammerhand */
using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;


namespace Server.Items
{

	public class AncientSmeltingBox : Item
	{
		[Constructable]
		public AncientSmeltingBox() : this( null )
		{
		}

		[Constructable]
		public AncientSmeltingBox ( string name ) : base ( 0x9A8 )
		{
			Name = "Ancient Smelting Box";
			LootType = LootType.Blessed;
			Hue = 1259;
		}

        public AncientSmeltingBox(Serial serial)
            : base(serial)
        {
        }


        public override void OnDoubleClick(Mobile m)
        {

            Item e = m.Backpack.FindItemByType(typeof(AncientSmeltingBox));

            if (e != null)
            {

                Item a = m.Backpack.FindItemByType(typeof(SosarianOre));
                Item b = m.Backpack.FindItemByType(typeof(EnergizerCrystal));
                Item c = m.Backpack.FindItemByType(typeof(StarMetalFragments));
                Item d = m.Backpack.FindItemByType(typeof(SpecialCharcoal));


                if (e == null || e.Amount < 1 || a == null || a.Amount < 1 || b == null || b.Amount < 1 || c == null || c.Amount < 1 || d == null || d.Amount < 1)
                {
                    m.SendMessage("You are missing something...");
                }

                else
                {
                    m.AddToBackpack(new EnergizedSosarianIngots());
                    a.Delete();
                    b.Delete();
                    c.Delete();
                    d.Delete();
                    m.SendMessage("You Combine the items into the Energized Sosarian Ingots");
                    this.Delete();
                }

            }
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
    }
}