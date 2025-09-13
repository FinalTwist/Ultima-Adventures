using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class FirstMate : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

        [Constructable]
        public FirstMate(): base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "FirstMate";
            Hue = Utility.RandomSkinHue();
            Body = 0x190;
            Name = "Tristan Irons";

            {

                AddItem(new FancyShirt(Utility.RandomNeutralHue()));
                AddItem(new ShortPants());
                AddItem(new ThighBoots());
                AddItem(new TricorneHat());
                AddItem(new Cutlass());

            }

            SetStr(96, 105);
            SetDex(91, 100);
            SetInt(61, 75);

            SetHits(400, 500);

            SetDamage(11, 23);

            SetDamageType(ResistanceType.Physical, 100);
            SetDamageType(ResistanceType.Cold, 25);

            SetResistance(ResistanceType.Physical, 45);
            SetResistance(ResistanceType.Fire, 30);
            SetResistance(ResistanceType.Cold, 60);
            SetResistance(ResistanceType.Poison, 35);
            SetResistance(ResistanceType.Energy, 50);

            SetSkill(SkillName.Fencing, 100);
            SetSkill(SkillName.MagicResist, 100);
            SetSkill(SkillName.Swords, 100);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Wrestling, 25.0, 37.5);

            Fame = 2000;
            Karma = -2000;

            switch (Utility.Random(3))
            {
                case 0: AddItem(new Cutlass()); break;
                case 1: AddItem(new Dagger()); break;
            }
        }

		public override bool AlwaysMurderer{ get{ return true; } }

        public FirstMate(Serial serial): base(serial)
        {
        }

        public override bool OnBeforeDeath()
        {
            CaptainJackSparrow rm = new CaptainJackSparrow();
            rm.Team = this.Team;
            rm.Combatant = this.Combatant;
            rm.NoKillAwards = true;

            Effects.PlaySound(this, Map, GetDeathSound());
            Effects.SendLocationEffect(Location, Map, 0x3709, 30, 10, 0x835, 0);
            rm.MoveToWorld(Location, Map);

            Delete();
            return false;
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