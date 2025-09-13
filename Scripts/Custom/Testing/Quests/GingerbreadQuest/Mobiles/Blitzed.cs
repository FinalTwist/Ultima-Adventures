/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
    public class MobileDeleteTime : Timer
    {
        private Item mob;

        public MobileDeleteTime(Item m)
            : base(TimeSpan.FromSeconds(15))
        {
            mob = m;
            Priority = TimerPriority.OneSecond;
        }

        protected override void OnTick()
        {
            if (mob == null || mob.Deleted)
            {
                Stop();
                return;
            }

            mob.Delete();
        }
    }
    public class Blitzed : BaseMutatedReindeer
    {
        [Constructable]
        public Blitzed()
        {
            Name = "Blitzed";
            Hue = 1818;
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            if (m_Spawning)
            {
                PackItem(new RecipeFragment9());
            }
        }
        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }
        public Blitzed(Serial serial)
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
        public override bool OnBeforeDeath()
        {
            // spawn the item
            Item item = (Item)Activator.CreateInstance(typeof(Moongate));
            Moongate moon = (Moongate)item;

            moon.TargetMap = Map.Trammel; //or map
            moon.Target = new Point3D(1422, 1697, 0); // Set map X,Y,Z location here

            // Map map = Map.Trammel; 

            Point3D pnt = GetSpawnLocation();

            moon.MoveToWorld(pnt, this.Map);

            Timer m_timer = new MobileDeleteTime(item);
            m_timer.Start();
            return base.OnBeforeDeath(); 
        }

        //from champspawn.cs
        public Point3D GetSpawnLocation()
        {
            int m_SpawnRange = 2;
            Map map = Map;

            if (map == null)
                return Location;

            // Try 20 times to find a spawnable location.
            for (int i = 0; i < 20; i++)
            {
                int x = Location.X + (Utility.Random((m_SpawnRange * 2) + 1) - m_SpawnRange);
                int y = Location.Y + (Utility.Random((m_SpawnRange * 2) + 1) - m_SpawnRange);
                int z = Map.GetAverageZ(x, y);

                if (Map.CanSpawnMobile(new Point2D(x, y), z))
                    return new Point3D(x, y, z);
            }

            return Location;
	}
  }
}