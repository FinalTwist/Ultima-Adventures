//Proudly Created by Darck... *once his first form is defeated he turns into his second form with a full hp restore* 
//Please if this is distributed please leave the credit for this base please..
using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	public class GanonSecondForm : BaseCreature
	{
        public override bool IgnoreYoungProtection { get { return Core.ML; } }

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public GanonSecondForm(): base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x13E;
			Name = "Ganon";
			Title = "the Dark Master";
			Hue = 0;

            SetStr(500);
            SetDex(100);
            SetInt(1000);

            SetHits(30000);
            SetMana(5000);

            SetDamage(17, 21);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 20);
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 30);
            SetResistance(ResistanceType.Fire, 30);
            SetResistance(ResistanceType.Cold, 30);
            SetResistance(ResistanceType.Poison, 30);
            SetResistance(ResistanceType.Energy, 30);

            SetSkill(SkillName.Necromancy, 120, 120.0);
            SetSkill(SkillName.SpiritSpeak, 120.0, 120.0);

            SetSkill(SkillName.DetectHidden, 80.0);
            SetSkill(SkillName.EvalInt, 100.0);
            SetSkill(SkillName.Magery, 100.0);
            SetSkill(SkillName.Meditation, 120.0);
            SetSkill(SkillName.MagicResist, 150.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 120.0);

            Fame = 100000;
            Karma = 100000;

            VirtualArmor = 64;


		}

        public override bool AlwaysMurderer { get { return true; } }
        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool AreaPeaceImmune { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 5; } }

		public GanonSecondForm( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
            GanonFinalForm rm = new GanonFinalForm();
			rm.Team = this.Team;
			rm.Combatant = this.Combatant;
			rm.NoKillAwards = true;

            Hits = HitsMax;
            Stam = StamMax;
            Mana = ManaMax;

            Say(1049499); // Behold my true form!

			if ( rm.Backpack == null )
			{
				Backpack pack = new Backpack();
				pack.Movable = false;
				rm.AddItem( pack );
			}

			for ( int i = 0; i < 2; i++ )
			{
				LootPack.FilthyRich.Generate( this, rm.Backpack, true, LootPack.GetLuckChanceForKiller( this ) );
				LootPack.FilthyRich.Generate( this, rm.Backpack, false, LootPack.GetLuckChanceForKiller( this ) );
			}

			Effects.PlaySound(this, Map, GetDeathSound());
			Effects.SendLocationEffect( Location, Map, 0x3709, 30, 10, 0x835, 0 );
			rm.MoveToWorld( Location, Map );

			Delete();
			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}