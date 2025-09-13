/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
    public class Adolph : BaseMutatedReindeer
    {
        [Constructable]
        public Adolph()
        {
            Name = "Adolph";
            Hue = 1810;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment1());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public Adolph(Serial serial)
            : base(serial)
        {
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


    public class Masher : BaseMutatedReindeer
    {
        [Constructable]
        public Masher()
        {
            Name = "Masher";
            Hue = 1811;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment2());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public Masher(Serial serial)
            : base(serial)
        {
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

    public class Slasher : BaseMutatedReindeer
    {
        [Constructable]
        public Slasher()
        {
            Name = "Slasher";
            Hue = 1812;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment3());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }


        public Slasher(Serial serial)
            : base(serial)
        {
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

    public class Smasher : BaseMutatedReindeer
    {
        [Constructable]
        public Smasher()
        {
            Name = "Smasher";
            Hue = 1813;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment4());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public Smasher(Serial serial)
            : base(serial)
        {
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

    public class Nixon : BaseMutatedReindeer
    {
        [Constructable]
        public Nixon()
        {
            Name = "Nixon";
            Hue = 1814;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment5());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public Nixon(Serial serial)
            : base(serial)
        {
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

    public class Gromet : BaseMutatedReindeer
    {
        [Constructable]
        public Gromet()
        {
            Name = "Gromet";
            Hue = 1815;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment6());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public Gromet(Serial serial)
            : base(serial)
        {
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

    public class Stupid : BaseMutatedReindeer
    {
        [Constructable]
        public Stupid()
        {
            Name = "Stupid";
            Hue = 1816;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment7());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public Stupid(Serial serial)
            : base(serial)
        {
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

    public class Slaughter : BaseMutatedReindeer
    {
        [Constructable]
        public Slaughter()
        {
            Name = "Slaughter";
            Hue = 1817;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment8());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public Slaughter(Serial serial)
            : base(serial)
        {
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
