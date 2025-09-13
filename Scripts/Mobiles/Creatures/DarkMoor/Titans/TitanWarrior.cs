// Created by Script Creator
using System;
using Server.Items;

namespace Server.Mobiles
{
     [CorpseName( "Titan Warrior Corpse" )]
     public class TitanWarrior: BaseCreature
     {
        public override int BreathPhysicalDamage { get { return 100; } }
 
        public override int BreathEffectHue { get { return 0; } }
        public override int BreathEffectSound { get { return 0x65A; } }
        public override int BreathEffectItemID { get { return 0x1365; } } // SMALL BOULDER
        public override bool HasBreath { get { return true; } }
        public override double BreathEffectDelay { get { return 0.1; } }
        public override void BreathDealDamage(Mobile target, int form) { base.BreathDealDamage(target, 7); }
        public override double BreathDamageScalar { get { return 0.35; } }
                     public override bool CanAngerOnTame { get { return true; } }
             public override bool BardImmune{ get{ return true; } }
             public override bool Unprovokable{ get{ return true; } }
             public override bool Uncalmable{ get{ return true; } } 

        [Constructable]
		public TitanWarrior () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
         {
             Name = "Titan Warrior";
             Body = 0x4C;
             Hue = 2001;
             BaseSoundID = 609;
            SetStr(336, 385);
            SetDex(96, 115);
            SetInt(281, 305);

            SetHits(202, 231);
            SetMana(0);

            SetDamage(7, 23);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 125.1, 140.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);



            Fame = 10000;
             Karma = 5000;
             VirtualArmor = 10;
                          ControlSlots = 2;
             MinTameSkill = 20;
             Tamable = true;

}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }



 public TitanWarrior( Serial serial ) : base( serial )
{
}

public override void Serialize( GenericWriter writer )
     {
         base.Serialize( writer );
         writer.Write( (int) 0 );
     }

public override void Deserialize( GenericReader reader )
     {
         base.Deserialize( reader );
         int version = reader.ReadInt();
     }
   }
}
