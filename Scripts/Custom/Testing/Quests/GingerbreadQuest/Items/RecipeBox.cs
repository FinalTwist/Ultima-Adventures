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

	public class RecipeBox : Item
	{
		[Constructable]
		public RecipeBox() : this( null )
		{
		}

		[Constructable]
		public RecipeBox ( string name ) : base ( 0x9A8 )
		{
			Name = "Recipe Box";
			LootType = LootType.Blessed;
			Hue = 1742;
		}

        public RecipeBox(Serial serial)
            : base(serial)
        {
        }


        public override void OnDoubleClick(Mobile m)
        {

            Item k = m.Backpack.FindItemByType(typeof(RecipeBox));

            if (k != null)
            {

                Item a = m.Backpack.FindItemByType(typeof(RecipeFragment1));
                Item b = m.Backpack.FindItemByType(typeof(RecipeFragment2));
                Item c = m.Backpack.FindItemByType(typeof(RecipeFragment3));
                Item d = m.Backpack.FindItemByType(typeof(RecipeFragment4));
                Item e = m.Backpack.FindItemByType(typeof(RecipeFragment5));
                Item f = m.Backpack.FindItemByType(typeof(RecipeFragment6));
                Item g = m.Backpack.FindItemByType(typeof(RecipeFragment7));
                Item h = m.Backpack.FindItemByType(typeof(RecipeFragment8));
                Item i = m.Backpack.FindItemByType(typeof(RecipeFragment9));
                Item j = m.Backpack.FindItemByType(typeof(RecipeFragment10));


                if (k == null || k.Amount < 1 || a == null || a.Amount < 1 || b == null || b.Amount < 1 || c == null || c.Amount < 1 || d == null || d.Amount < 1
                    || e == null || e.Amount < 1 || f == null || f.Amount < 1 || g == null || g.Amount < 1 || h == null || h.Amount < 1 || i == null || i.Amount < 1 
                    || j == null || j.Amount < 1)
                {
                    m.SendMessage("You are missing something...");
                }

                else
                {
                    m.AddToBackpack(new SpecialGingerbreadRecipe());
                    a.Delete();
                    b.Delete();
                    c.Delete();
                    d.Delete();
                    e.Delete();
                    f.Delete();
                    g.Delete();
                    h.Delete();
                    i.Delete();
                    j.Delete();
                    m.SendMessage("You Combine the fragments into the Special Gingerbread Recipe");
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