#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using Server;
using Server.Network;
using Server.Scripts;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items.Crops
{

#region DrugSystem_Engine | DrugSystem_Helper | DrugSystem_GrowTimer

    public class DrugSystem_Engine : Item, IChopable
    {
        public virtual bool CanGrowFarm { get { return true; } }
        public virtual bool CanGrowHouseTiles { get { return true; } }
        public virtual bool CanGrowDirt { get { return true; } }
        public virtual bool CanGrowGround { get { return false; } }
        public virtual bool CanGrowSwamp { get { return false; } }
        public virtual bool CanGrowSand { get { return false; } }
        public virtual bool PlayerCanDestroy { get { return false; } }
        public virtual bool CanGrowGarden { get { return true; } }

        public virtual TimeSpan SowerPickTime { get { return TimeSpan.FromDays(7); } }

        private bool i_bumpZ = false;

        public bool BumpZ { get { return i_bumpZ; } set { i_bumpZ = value; } }

        public DrugSystem_Engine(int itemID): base(itemID)
        {
        }

        public virtual void OnChop(Mobile from)
        {
        }

        public DrugSystem_Engine(Serial serial): base(serial)
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

    public class DrugSystem_Helper
    {
        public static bool CanWorkMounted { get { return false; } }
        public static int[] FarmTiles = new int[]
		{
			0x009, 0x00A,
			0x00C, 0x00E,
			0x013, 0x015,
			0x150, 0x155,
			0x15A, 0x15C
		};

        public static int[] HouseTiles = new int[]
		{
			0x31F4, 0x31F5,
			0x31F6, 0x31F7,
			0x515, 0x516,
			0x517, 0x518,
			0x31F4, 0x31F9,
			0x31FA, 0x31FB
		};

        public static int[] DirtTiles = new int[]
		{
			0x071, 0x07C,
			0x165, 0x174,
			0x1DC, 0x1EF,
			0x306, 0x31F,
			0x08D, 0x0A7,
			0x2E5, 0x305,
			0x777, 0x791,
			0x98C, 0x9BF,
		};

        public static int[] GroundTiles = new int[]
		{
			0x003, 0x006,
			0x033, 0x03E,
			0x078, 0x08C,
			0x0AC, 0x0DB,
			0x108, 0x10B,
			0x14C, 0x174,
			0x1A4, 0x1A7,
			0x1B1, 0x1B2,
			0x26E, 0x281,
			0x292, 0x295,
			0x355, 0x37E,
			0x3CB, 0x3CE,
			0x547, 0x5A6,
			0x5E3, 0x618,
			0x66B, 0x66E,
			0x6A1, 0x6C2,
			0x6DE, 0x6E1,
			0x73F, 0x742,
		};

        public static int[] SwampTiles = new int[]
		{
			0x3DC1, 0x3DC2,
			0x3DD9, 0x3EF0,
		};

        public static int[] SandTiles = new int[]
		{
			0x016, 0x019,
			0x033, 0x03E,
			0x1A8, 0x1AB,
			0x282, 0x291,
			0x335, 0x35C,
			0x3B7, 0x3CA,
			0x5A7, 0x5BA,
			0x64B, 0x66A,
			0x66F, 0x672,
			0x7D5, 0x7D8,
		};

        public static bool CheckCanGrow(DrugSystem_Engine crop, Map map, int x, int y)
        {
            if (crop.CanGrowFarm && ValidateFarmLand(map, x, y)) return true;
            if (crop.CanGrowHouseTiles && ValidateHouseTiles(map, x, y)) return true;
            if (crop.CanGrowDirt && ValidateDirt(map, x, y)) return true;
            if (crop.CanGrowGround && ValidateGround(map, x, y)) return true;
            if (crop.CanGrowSand && ValidateSand(map, x, y)) return true;
            if (crop.CanGrowSwamp && ValidateSwamp(map, x, y)) return true;
            if (crop.CanGrowGarden) { crop.BumpZ = ValidateGardenPlot(map, x, y); return crop.BumpZ; }
            return false;
        }

        public static bool ValidateGardenPlot(Map map, int x, int y)
        {
            bool ground = false;
            IPooledEnumerable eable = map.GetItemsInBounds(new Rectangle2D(x, y, 1, 1));
            foreach (Item item in eable)
            {
                if (item.ItemID == 0x32C9 || item.ItemID == 0x32CA) ground = true;
            }
            eable.Free();
            if (!ground)
            {
                 StaticTile[] tile = map.Tiles.GetStaticTiles(x, y);
                for (int i = 0; i < tile.Length; ++i)
                {
                    if ((tile[i].ID & 0x3FFF) == 0x32C9) ground = true;
                }
            }
            return ground;
        }

        public static bool ValidateFarmLand(Map map, int x, int y)
        {
            int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
            bool ground = false;
            for (int i = 0; !ground && i < FarmTiles.Length; i += 2)
                ground = (tileID >= FarmTiles[i] && tileID <= FarmTiles[i + 1]);
            return ground;
        }

        public static bool ValidateHouseTiles(Map map, int x, int y)
        {
            int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
            bool ground = false;
            for (int i = 0; !ground && i < HouseTiles.Length; i += 2)
                ground = (tileID >= HouseTiles[i] && tileID <= HouseTiles[i + 1]);
            return ground;
        }

        public static bool ValidateDirt(Map map, int x, int y)
        {
            int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
            bool ground = false;
            for (int i = 0; !ground && i < DirtTiles.Length; i += 2)
                ground = (tileID >= DirtTiles[i] && tileID <= DirtTiles[i + 1]);
            return ground;
        }

        public static bool ValidateGround(Map map, int x, int y)
        {
            int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
            bool ground = false;
            for (int i = 0; !ground && i < GroundTiles.Length; i += 2)
                ground = (tileID >= GroundTiles[i] && tileID <= GroundTiles[i + 1]);
            return ground;
        }

        public static bool ValidateSwamp(Map map, int x, int y)
        {
            int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
            bool ground = false;
            for (int i = 0; !ground && i < SwampTiles.Length; i += 2)
                ground = (tileID >= SwampTiles[i] && tileID <= SwampTiles[i + 1]);
            return ground;
        }

        public static bool ValidateSand(Map map, int x, int y)
        {
            int tileID = map.Tiles.GetLandTile(x, y).ID & 0x3FFF;
            bool ground = false;
            for (int i = 0; !ground && i < SandTiles.Length; i += 2)
                ground = (tileID >= SandTiles[i] && tileID <= SandTiles[i + 1]);
            return ground;

        }
        public class DrugSystem_GrowTimer : Timer
        {
            private Item i_seedling;
            private Type t_crop;
            private Mobile m_sower;
            private int cnt;
            private double alchemyValue;
            private double rnd;

            public DrugSystem_GrowTimer(Item seedling, Type croptype, Mobile sower): base(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(12))
            {
                Priority = TimerPriority.OneSecond;
                i_seedling = seedling;
                t_crop = croptype;
                m_sower = sower;
                cnt = 0;
                rnd = Utility.RandomDouble();
                alchemyValue = sower.Skills[SkillName.Alchemy].Value / 50;
            }

            protected override void OnTick()
            {
                if (cnt++ / 20 > rnd)
                {
                    if ((i_seedling != null) && (!i_seedling.Deleted))
                    {
                        object[] args = { m_sower };
                        Item newitem = Activator.CreateInstance(t_crop, args) as Item;
                        if (newitem == null || Utility.RandomDouble() > alchemyValue)
                        {
                            newitem = new DrugSystem_DeadCrops(m_sower);
                        }
                        newitem.Location = i_seedling.Location;
                        newitem.Map = i_seedling.Map;
                        i_seedling.Delete();
                    }
                    Stop();
                }
            }
        }

        public static ArrayList CheckCrop(Point3D pnt, Map map, int range)
        {
            ArrayList crops = new ArrayList();
            IPooledEnumerable eable = map.GetItemsInRange(pnt, range);
            foreach (Item crop in eable)
            {
                if ((crop != null) && (crop is DrugSystem_Engine)) crops.Add((DrugSystem_Engine)crop);
            }
            eable.Free();
            return crops;
        }
    }

#endregion DrugSystem_Engine | DrugSystem_Helper | DrugSystem_GrowTimer

#region DrugSystem_DeadCrops | DrugSystem_DeadCrop

    public class DrugSystem_DeadCrops : DrugSystem_Engine
    {
        private static DateTime planted;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime t_planted { get { return planted; } set { planted = value; } }
        private static Mobile m_sower;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Sower { get { return m_sower; } set { m_sower = value; } }

        [Constructable]
        public DrugSystem_DeadCrops(Mobile sower): base(0x1B9C)
        {
            Name = "Dead Harvest";
            Movable = false;
            m_sower = sower;
            planted = DateTime.UtcNow;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Mounted && !DrugSystem_Helper.CanWorkMounted)
            {
                from.SendMessage("You Cannot Pull Up A Weed While Mounted.");
                return;
            }
            if (from.InRange(this.GetWorldLocation(), 1))
            {
                if ((from == m_sower) || (DateTime.UtcNow >= planted.AddDays(3)))
                {
                    from.Direction = from.GetDirectionTo(this);
                    from.Animate(from.Mounted ? 29 : 32, 5, 1, true, false, 0);
                    from.SendMessage("You Gather Up The Dead Crops.");

                    DrugSystem_DeadCrop fruit = new DrugSystem_DeadCrop();
                    from.AddToBackpack(fruit); //was DrugSystem_DeadCrop
                    this.Delete();
                }
                else from.SendMessage("This Crop Is Still Too Tough To Pull.");
            }
            else
            {
                from.SendMessage("You Are Too Far Away To Harvest Anything.");
            }
        }

        public DrugSystem_DeadCrops(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(m_sower);
            writer.Write(planted);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_sower = reader.ReadMobile();
            planted = reader.ReadDateTime();

        }
    }

    public class DrugSystem_DeadCrop : Item
    {
        [Constructable]
        public DrugSystem_DeadCrop(): this(1)
        {
        }

        [Constructable]
        public DrugSystem_DeadCrop(int amount): base(0x1B9C)
        {
            Name = "Bad Harvest";
            Weight = 0.1;
            Hue = 837;
            Movable = true;
        }

        public DrugSystem_DeadCrop(Serial serial)
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

#endregion DrugSystem_DeadCrops | DrugSystem_DeadCrop

#region DrugSystem_Effect | DrugSystem_StonedTimer

    public abstract class DrugSystem_Effect : Item
    {
        public float Highness;

        public DrugSystem_Effect(int itemID): base(itemID)
        {
        }

        public DrugSystem_Effect(Serial serial): base(serial)
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

    public class DrugSystem_StonedTimer : Timer
    {
        private Mobile m_Drunk;
        private float Highness;

        public DrugSystem_StonedTimer(Mobile from, float High)
            : base(TimeSpan.FromSeconds(Utility.RandomMinMax(1,5)), TimeSpan.FromSeconds(Utility.RandomMinMax(30,45)))
        {

            m_Drunk = from;
            Highness = High;
            //Increase int decrease dex
            m_Drunk.Stam = (int)((double)m_Drunk.Stam * 0.75);
            m_Drunk.Int += 10;
            m_Drunk.Dex -= 10;
            Priority = TimerPriority.OneSecond;
            if (m_Drunk is PlayerMobile)
                ((PlayerMobile)m_Drunk).High = true;
        }

        protected override void OnTick()
        {
            if (m_Drunk.Deleted || m_Drunk.Map == Map.Internal)
            {
                Stop();

            }
            else if (!m_Drunk.Alive)
            {
                Stop();
            }

            if (m_Drunk is PlayerMobile && Utility.RandomDouble() < (((PlayerMobile)m_Drunk).THC / 100) )
            {

            m_Drunk.Direction = (Direction)Utility.Random(8);
            m_Drunk.Stam = (int)((double)m_Drunk.Stam * 0.90);

            if (Highness % 10 == 0)
            {
                m_Drunk.SendMessage("You Are Stoned!");
                m_Drunk.PlaySound(0x420);   // coughing

                int enlight = Utility.Random(5);
                switch (enlight) //Randomly generated enlightning things to say
                {
                    case 0:
                        {
                           m_Drunk.PlaySound(m_Drunk.Female ? 813 : 1087);
                           m_Drunk.Say("*pukes*");
                           if (!m_Drunk.Mounted)
                               m_Drunk.Animate(32, 5, 1, true, false, 0);
                           Point3D p = new Point3D(m_Drunk.Location);
                           switch (m_Drunk.Direction)
                           {
                               case Direction.North:
                                   p.Y--; break;
                               case Direction.South:
                                   p.Y++; break;
                               case Direction.East:
                                   p.X++; break;
                               case Direction.West:
                                   p.X--; break;
                               case Direction.Right:
                                   p.X++; p.Y--; break;
                               case Direction.Down:
                                   p.X++; p.Y++; break;
                               case Direction.Left:
                                   p.X--; p.Y++; break;
                               case Direction.Up:
                                   p.X--; p.Y--; break;
                               default:
                                   break;
                           }
                           p.Z = m_Drunk.Map.GetAverageZ(p.X, p.Y);

                           bool canFit = Server.Spells.SpellHelper.AdjustField(ref p, m_Drunk.Map, 12, false);

                           if (canFit)
                           {
                               Puke puke = new Puke();
                               puke.Map = m_Drunk.Map;
                               puke.Location = p;
                           }
                           /*else
                               pm.SendMessage( "your puke won't fit!" ); /* Debug testing */
                           break;

                           }
                    case 1:
                        {
                            m_Drunk.Say("So I Was Like Dude, And He Was Like Sweet, And Then... Ahhh Geez, I Forgot");
                            m_Drunk.PlaySound(m_Drunk.Female ? 794 : 1066);
                            m_Drunk.Say("*Laughs*");
                            break;
                        }

                    case 2:
                        {
                            m_Drunk.Say("What If You Were Like A Polar Bear?! Then You Like Start Dancin Like A Pixie!...");
                            m_Drunk.Say("*Breaks down laughing*");
                            m_Drunk.PlaySound(m_Drunk.Female ? 794 : 1066);
                            Thread.Sleep(3000);
                            m_Drunk.PlaySound(m_Drunk.Female ? 794 : 1066);
                            Thread.Sleep(3000);
                            m_Drunk.PlaySound(m_Drunk.Female ? 794 : 1066);
                            break;
                        }

                    case 3:
                        {
                            m_Drunk.Say("Dude The Universe Is Like In My Dreams... And Everything Around Me Is Not Real.. It's All An Illusion, Cause Reality Is Just A Perspective, Dude...");
                            break;
                        }

                    case 4:
                        {
                            m_Drunk.Say("The Room Is Spinning Dude, Like I See... No Wait There Are Two Of You...Whoa!?");
                            m_Drunk.PlaySound(m_Drunk.Female ? 792 : 1064);
                            m_Drunk.Emote("*Farts*");
                            m_Drunk.Say("Dude That Was Awesome! I Gotta Cut Back On Those Bean Burittos Man");
                            m_Drunk.PlaySound(m_Drunk.Female ? 791 : 1063);
                            m_Drunk.Emote("*Faints*");

                            if (!m_Drunk.Mounted)
                                m_Drunk.Animate(22, 5, 1, true, false, 0);
                            break;
                        }
                  }
            }
            }
            if (m_Drunk is PlayerMobile)
                ((PlayerMobile)m_Drunk).THC -= Utility.RandomMinMax(1,4);
            else
                Highness--;

            if (Highness <= 0 || (m_Drunk is PlayerMobile && ((PlayerMobile)m_Drunk).THC <= 0))
            {

                m_Drunk.SendMessage("Your initial Buzz Has Been Killed, You Are No Longer Stoned!");
                m_Drunk.Int -= 10;//set int back to normal 
                m_Drunk.Dex += 10;

                if (m_Drunk is PlayerMobile)
                    ((PlayerMobile)m_Drunk).High = false;
				
				if (Utility.RandomDouble() > 0.99 && Utility.RandomBool()) //very rare
				{
					m_Drunk.Int -= 1;
					m_Drunk.SendMessage("*You Feel As If You Just Lost A Brain Cell*");
				}
                if (m_Drunk is PlayerMobile)
                    ((PlayerMobile)m_Drunk).THC = 0;
                Stop();
            }
        }
    }

#endregion DrugSystem_Effect | DrugSystem_StonedTimer

}

