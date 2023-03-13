// Clone Character On Logout v1.1.2
// Author: Felladrin
// Started: 2016-01-25
// Updated: 2016-02-05

using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Mobiles;
using Server.Regions;
using Server.Misc;

namespace Felladrin.Automations
{
    public static class CloneCharacterOnLogout
    {
        public static class Config
        {
            public static bool Enabled = true;      // Is this system enabled?
            public static bool CanWander = true;    // Can the clones wander freely around the world or should they be frozen?
            public static bool CanTeach = true;     // Can other player train skills with this clone?
        }

        public static void Initialize()
        {
            if (Config.Enabled)
            {
                EventSink.Logout += OnLogout;
                EventSink.Login += OnLogin;
                CheckFirstRun();
            }
        }

        static void OnLogout(LogoutEventArgs e)
        {
            int totalstats = e.Mobile.RawStr + e.Mobile.RawInt + e.Mobile.RawDex;

            if (e.Mobile.AccessLevel == AccessLevel.Player && Utility.RandomDouble() > 0.33 && totalstats > 175)
                CreateCloneOf(e.Mobile);
        }

        static void OnLogin(LoginEventArgs e)
        {
            if (e.Mobile.AccessLevel == AccessLevel.Player)
                DeleteClonesOf(e.Mobile);
        }

        static void CreateCloneOf(Mobile m)
        {
            var characterClone = new CharacterClone(m);

            foreach (var itemOriginal in m.Items)
                if (itemOriginal.Parent == m && itemOriginal.Layer != Layer.Mount)
                    characterClone.AddItem(new ItemClone(itemOriginal));

            if (m.Mounted)
            {
                var baseMount = m.Mount as BaseMount;
                var etherealMount = m.Mount as EtherealMount;

                if (baseMount != null)
                    new MountClone(baseMount).Rider = characterClone;
                else if (etherealMount != null)
                    new EtherealMountClone(etherealMount).Rider = characterClone;
            }
        }

        static void DeleteClonesOf(Mobile m)
        {
            foreach (var mobile in new List<Mobile>(World.Mobiles.Values))
                if (mobile is CharacterClone && ((CharacterClone)mobile).Original == m)
                    mobile.Delete();
        }

        static void CheckFirstRun()
        {
            foreach (var mobile in World.Mobiles.Values)
                if (mobile is CharacterClone)
                    return;

            foreach (var mobile in new List<Mobile>(World.Mobiles.Values))
                if (mobile is PlayerMobile && mobile.Alive && mobile.AccessLevel == AccessLevel.Player)
                    CreateCloneOf(mobile);
        }

        public class CharacterClone : BaseCreature
        {
            [CommandProperty(AccessLevel.GameMaster)]
            public Mobile Original { get; set; }

            public CharacterClone(Mobile original) : base(Config.CanWander ? AIType.AI_Vendor : AIType.AI_Use_Default, FightMode.None, 10, 1, 0.2, 0.2)
            {
                Original = original;

                foreach (var property in (typeof(Mobile)).GetProperties())
                    if (property.CanRead && property.CanWrite)
                        property.SetValue(this, property.GetValue(Original, null), null);

                for (int i = 0, l = Original.Skills.Length; i < l; ++i)
                    Skills[i].Base = Original.Skills[i].Base;

                Player = false;

                if (Map == Map.Internal)
                    Map = LogoutMap;
				
				Blessed = true;
				
            }

            public override void GetProperties(ObjectPropertyList list)
            {
                if (Original != null)
                    Original.GetProperties(list);
                else
                    base.GetProperties(list);
            }

           // public override bool IsInvulnerable{ get{ return true; } }

			public virtual bool IsInvulnerable{ get{ return true; } }

            public override bool CanRegenHits { get { return true; } }

            public override bool CanTeach { get { return Config.CanTeach; } }

            public CharacterClone(Serial serial) : base(serial) { }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
                writer.Write(Original);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                reader.ReadInt();
                Original = reader.ReadMobile();

                if (Original == null)
                    Delete();

                Region reg = Region.Find( this.Location, this.Map );
                if ( reg.IsPartOf( "the Bank" ) || reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)) )  
                {
                    PlayerMobile pm = (PlayerMobile)Original;
                    if ( pm.LastOnline < (DateTime.Now - TimeSpan.FromDays( 30 ) ) )
                    {
                        int whatever = Utility.RandomMinMax(1, 100);
                        String world = "";
                        if (whatever <= 25) // 25%
                            world = "the Isles of Dread";
                        else if (whatever <= 30) // 5%
                            world = "the Land of Sosaria";
                        else if (whatever <= 45) // 15%
                            world = "the Land of Lodoria";
                        else if (whatever <= 65) // 20%
                            world = "the Savaged Empire";
                        else if (whatever <= 85) // 20%
                            world = "the Serpent Island";
                        else 
                            world = "the Island of Umber Veil";

                        // pick what to infect - 10% city, 60% overland, 30% dungeon
                        whatever = Utility.RandomMinMax(1, 100);

                        Point3D gagme;
                        if (whatever <= 75)
                            gagme = Worlds.GetRandomTown( world, false );
                        else 
                            gagme = Worlds.GetRandomLocation( world, "land" );

                        MoveToWorld( gagme, Worlds.GetMyDefaultMap( world ) );
                    }
                }
            }
        }

        public class ItemClone : Item
        {
            [CommandProperty(AccessLevel.GameMaster)]
            public Item Original { get; set; }

            public ItemClone(Item original)
            {
                Original = original;

                foreach (var property in (typeof(Item)).GetProperties())
                    if (property.CanRead && property.CanWrite)
                        property.SetValue(this, property.GetValue(original, null), null);

                Movable = false;
            }

            public override void GetProperties(ObjectPropertyList list)
            {
                if (Original != null)
                    Original.GetProperties(list);
                else
                    base.GetProperties(list);
            }

            public ItemClone(Serial serial) : base(serial) { }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
                writer.Write(Original);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                reader.ReadInt();
                Original = reader.ReadItem();

                if (Original == null)
                    Delete();
            }
        }

        public class MountClone : BaseMount
        {
            public MountClone(BaseMount original) : base(original.Name,  original.BodyValue, original.ItemID, original.AI, original.FightMode, original.RangePerception, original.RangeFight, original.ActiveSpeed, original.PassiveSpeed)
            {
                foreach (var property in (typeof(BaseMount)).GetProperties())
                    if (property.CanRead && property.CanWrite && property.Name != "Rider")
                        property.SetValue(this, property.GetValue(original, null), null);
            }

            public MountClone(Serial serial) : base(serial) { }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                reader.ReadInt();
            }
        }

        public class EtherealMountClone : EtherealMount
        {
            public EtherealMountClone(EtherealMount original) : base(original.RegularID, original.MountedID)
            {
                foreach (var property in (typeof(EtherealMount)).GetProperties())
                    if (property.CanRead && property.CanWrite && property.Name != "Rider")
                        property.SetValue(this, property.GetValue(original, null), null);
            }

            public EtherealMountClone(Serial serial) : base(serial) { }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                reader.ReadInt();
            }
        }
    }
}
