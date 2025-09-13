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
	public class GanonFirstForm : BaseCreature
	{
        public override bool IgnoreYoungProtection { get { return Core.ML; } }

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public GanonFirstForm(): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x31C;
			Name = "Dark Fiend Ganon";
			Hue = 0;
            BaseSoundID = 609;

            SetStr(255, 847);
            SetDex(145, 428);
            SetInt(26, 362);

            SetHits(1501, 1871);

            SetDamage(20, 25);

            SetDamageType(ResistanceType.Physical, 80);
            SetDamageType(ResistanceType.Poison, 20);

            SetResistance(ResistanceType.Physical, 30, 59);
            SetResistance(ResistanceType.Fire, 30, 55);
            SetResistance(ResistanceType.Cold, 50, 65);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 50, 74);

            SetSkill(SkillName.Wrestling, 84.9, 99.3);
            SetSkill(SkillName.Tactics, 98.4, 110.6);
            SetSkill(SkillName.MagicResist, 74.4, 77.7);

			Fame = 100000;
			Karma = 100000;


		}

        public override bool AlwaysMurderer { get { return true; } }
        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool AreaPeaceImmune { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override bool AutoDispel { get { return true; } }

        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.MortalStrike;
        }

		public GanonFirstForm( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
            GanonSecondForm rm = new GanonSecondForm();
			rm.Team = this.Team;
			rm.Combatant = this.Combatant;
			rm.NoKillAwards = true;

            Hits = HitsMax;
            Stam = StamMax;
            Mana = ManaMax;

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