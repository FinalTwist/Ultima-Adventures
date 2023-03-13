using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Seventh;

namespace Server.Mobiles
{
    public class Norlop : BaseCreature
    {


        [Constructable]
        public Norlop()
            : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 1.5, 3.0)
        {
            Body = 0x190;
            BaseSoundID = 0;
            Name = ("Norlop");
            Title = ("The Clumsy Mage");
            Female = false;



            SetStr(100);
            SetDex(150);
            SetInt(100);
           
            SetHits(500);

            SetResistance(ResistanceType.Physical, 50);
            SetResistance(ResistanceType.Fire, 50);
            SetResistance(ResistanceType.Cold, 50);
            SetResistance(ResistanceType.Poison, 50);
            SetResistance(ResistanceType.Energy, 50);
            
            
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Focus, 100.0);
            SetSkill(SkillName.Magery, 100.0);
            SetSkill(SkillName.Meditation, 100.0);

            VirtualArmor = 10;

            Container pack = new Backpack();
            pack.Movable = false;
            pack.Hue = 1266;
            pack.Name = "Norlop's Pack";
            AddItem(pack);

            BlackStaff staff = new BlackStaff();
            staff.Hue = 1153;
            staff.Name = "Beginners Mage Staff";
            staff.LootType = LootType.Blessed;
            staff.Movable = false;
            AddItem(staff);

            WizardsHat hat = new WizardsHat();
            hat.Hue = 1150;
            hat.Movable = false;
            hat.LootType = LootType.Blessed;
            AddItem(hat);

            Robe robe = new Robe();
            robe.Hue = 1165;
            robe.Name = "A Faded Robe";
            robe.LootType = LootType.Blessed;
            robe.Movable = false;
            AddItem(robe);

            Sandals sandals = new Sandals();
            sandals.Hue = 1;
            sandals.Name = "A Worn Looking Pair of Old Sandals";
            sandals.LootType = LootType.Blessed;
            sandals.Movable = false;
            AddItem(sandals);

            Item hair = new Item(0x203C);
            hair.Hue = 1154;
            hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem(hair);
			
			Karma = 1;

          /*  Item beard = new Item(0x204C);
            beard.Hue = 1154;
            beard.Layer = Layer.Beard;
            beard.Movable = false;
            AddItem(beard);*/
        }
        
        public override bool AlwaysMurderer { get { return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 7);
        }


        public void Polymorph(Mobile m)
        {
            if (!m.CanBeginAction(typeof(PolymorphSpell)) || !m.CanBeginAction(typeof(IncognitoSpell)) || m.IsBodyMod)
                return;

      /*      IMount mount = m.Mount;

            if (mount != null)
                mount.Rider = null;

            if (m.Mounted)
                return;*/

            if (m.BeginAction(typeof(PolymorphSpell)))
            {
                switch (Utility.Random(1))
                {
                    case 0: m.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.Head); m.PlaySound(0x208); break;

                }
                
                Item disarm = m.FindItemOnLayer(Layer.Pants);

                if (disarm != null && disarm.Movable)
                    m.AddToBackpack(disarm);

              /*  disarm = m.FindItemOnLayer(Layer.Pants);

                if (disarm != null && disarm.Movable)
                    m.AddToBackpack(disarm);*/

                m.BodyMod = 0;
                m.HueMod = -1;
                m.SendMessage("The Magic Spell Hits Your Pants And They Come Alive");
                new ExpirePolymorphTimer(m).Start();
            }
        }

        private class ExpirePolymorphTimer : Timer
        {
            private Mobile m_Owner;

            public ExpirePolymorphTimer(Mobile owner)
                : base(TimeSpan.FromMinutes(3.0))
            {
                m_Owner = owner;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (!m_Owner.CanBeginAction(typeof(PolymorphSpell)))
                {
                   m_Owner.SendMessage("You Feel A Sudden Breeze!?");
                    m_Owner.BodyMod = 0;
                    m_Owner.HueMod = -1;
                    m_Owner.EndAction(typeof(PolymorphSpell));
                }
            }
        }



        public void SpawnRunningPants(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int newRunningPants = Utility.RandomMinMax(1, 1);

            for (int i = 0; i < newRunningPants; ++i)
            {
                RunningPants runningpants = new RunningPants();

                runningpants.Team = this.Team;
                runningpants.FightMode = FightMode.Aggressor;

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                runningpants.MoveToWorld(loc, map);
                runningpants.Combatant = target;
            }
        }

        public void DoSpecialAbility(Mobile target)
        {
            double chance = Utility.RandomDouble();

            if (chance >= 0.9)
            {
                this.Say("umm what was that spell again ... Clatu Virata no no no ahh yes....NICTO*Cough*!!!");
                Polymorph(target);
            }
             if (chance >= 0.9)
            {
                this.Say("I Missed.. Your Pants Are Alive What Else Could I Do Wrong?");
                SpawnRunningPants(target);
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            DoSpecialAbility(defender);

            defender.Damage(Utility.Random(2, 1), this);
            defender.Stam -= Utility.Random(2, 1);
            defender.Mana -= Utility.Random(2, 1);
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            DoSpecialAbility(attacker);

        }

        public override void OnDamagedBySpell(Mobile caster)
        {
            base.OnDamagedBySpell(caster);

            DoSpecialAbility(caster);

        }
        
        
        
        
        
        public Norlop(Serial serial)
            : base(serial)
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
}
