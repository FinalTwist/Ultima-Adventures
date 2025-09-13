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
	public class BlackThorn : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public BlackThorn(): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x3DF;
			Name = "Blackthorn";
			Title = "the King of Britain";
			Hue = 0;

			SetStr( 800, 900 );
			SetDex( 1000, 1200 );
			SetInt( 125, 135 );

			SetHits( 10000 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 60, 80);
            SetResistance(ResistanceType.Cold, 60, 80);
            SetResistance(ResistanceType.Poison, 60, 80);
            SetResistance(ResistanceType.Energy, 60, 80);

            SetSkill(SkillName.Wrestling, 90.1, 100.0);
            SetSkill(SkillName.Tactics, 90.2, 110.0);
            SetSkill(SkillName.MagicResist, 120.2, 160.0);
            SetSkill(SkillName.Swords, 120.0);
            SetSkill(SkillName.EvalInt, 120.0);
            SetSkill(SkillName.Focus, 120.0);
            SetSkill(SkillName.Anatomy, 120.0);

			Fame = 100000;
			Karma = -100000;
			VirtualArmor = 50;

			VikingSword weapon = new VikingSword();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

		}

		public override int GetIdleSound()
		{
			return 0x184;
		}

		public override int GetAngerSound()
		{
			return 0x286;
		}

		public override int GetDeathSound()
		{
			return 0x288;
		}

		public override int GetHurtSound()
		{
			return 0x19F;
		}

        public override bool AlwaysMurderer { get { return true; } }
        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool AreaPeaceImmune { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 5; } }

		public BlackThorn( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
            BlackthornSecondForm rm = new BlackthornSecondForm();
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