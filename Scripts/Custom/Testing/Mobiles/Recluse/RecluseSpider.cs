// Created by David aka EvilPounder
// Server: Lords of UO

using System;
using Server.Items;

namespace Server.Mobiles

{
    [CorpseName("EWWW It's Dead")]
    public class RecluseSpider : DreadSpider
    {
        [Constructable]
        public RecluseSpider() : base()
        {
            Name = "-Recluse Spider-";
            Hue = 1931;
            Body = 173; // Uncomment these lines and input values
                        //BaseSoundID = ; // To use your own custom body and sound.
            SetStr(1230);
            SetDex(620);
            SetInt(772);
            SetHits(2000, 3000);
            SetDamage(13, 20);
            SetDamageType(ResistanceType.Physical, 55);
            SetDamageType(ResistanceType.Cold, 0);
            SetDamageType(ResistanceType.Fire, 0);
            SetDamageType(ResistanceType.Energy, 35);
            SetDamageType(ResistanceType.Poison, 100);

            SetResistance(ResistanceType.Physical, 30);
            SetResistance(ResistanceType.Cold, 10);
            SetResistance(ResistanceType.Fire, 10);
            SetResistance(ResistanceType.Energy, 30);
            SetResistance(ResistanceType.Poison, 100);
            Fame = -1000;
            Karma = -1000;
            VirtualArmor = 40;

            switch (Utility.Random(100))
            {
                case 0: AddItem(new FangoftheRecluse()); break;
                case 1: AddItem(new UberShot()); break;
            }
        }

        public override void GenerateLoot()
        {
            PackGold(300, 950);
            AddLoot(LootPack.FilthyRich, 4);
            AddLoot(LootPack.Gems, Utility.Random(1, 5));
        }

        public override bool AutoDispel { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override Poison HitPoison { get { return Poison.Lethal; } }
        public override bool AlwaysMurderer { get { return true; } }

        public RecluseSpider(Serial serial) : base(serial)
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
