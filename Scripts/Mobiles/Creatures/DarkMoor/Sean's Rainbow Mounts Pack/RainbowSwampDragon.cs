/*
 created by:
     /\       
____/_ \____  ### ### ### ### #  ### ### # ##  ##  ###
\  ___\ \  /  #   #   # # # # #  # # # # # # # # # #
 \/ /  \/ /   ### ##  ### # # #  ### # # # # # ##  ##
 / /\__/_/\     # #   # # # # #  # # # # # # # # # #
/__\ \_____\  ### ### # # # ###  # # # ### ##  # # ###
    \  /             http://www.wftpradio.net/
     \/       
*/
using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a rainbow corpse" )]
	public class RainbowSwampDragon : BaseMount
	{
		private static int[] m_IDs = new int[]
			{
				0xC8, 0x3E9F,
				0xE2, 0x3EA0,
				0xE4, 0x3EA1,
				0xCC, 0x3EA2
			};

        [Constructable]
		public RainbowSwampDragon() : this( "a rainbow swamp dragon" )
		{
		}

		[Constructable]
		public RainbowSwampDragon( string name ) : base( name, 0xE2, 0x3EA0, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			int random = Utility.Random( 4 );

			Body = 0x31A;
			ItemID = 0x3EBD;
			BaseSoundID = 0x16A;
            Hue = 1289;

            SetStr(2200, 2300);
            SetDex(100, 500);
            SetInt(1000, 2000);

            SetHits(4000, 5000);
            SetMana(2000, 5000);

            SetDamage(30, 50);

            SetDamageType(ResistanceType.Physical, 20, 100);
            SetDamageType(ResistanceType.Energy, 20, 100);
            SetDamageType(ResistanceType.Cold, 20, 100);
            SetDamageType(ResistanceType.Fire, 20, 100);
            SetDamageType(ResistanceType.Poison, 20, 100);

			SetResistance(ResistanceType.Physical, 15, 60 );
            SetResistance(ResistanceType.Energy, 45, 100);
            SetResistance(ResistanceType.Cold, 45, 100);
            SetResistance(ResistanceType.Fire, 45, 100);
            SetResistance(ResistanceType.Poison, 45, 100);

			SetSkill( SkillName.MagicResist, 100.1, 120.0 );
			SetSkill( SkillName.Tactics, 100.3, 120.0 );
			SetSkill( SkillName.Wrestling, 100.3, 120.0 );
            SetSkill( SkillName.Magery, 100.3, 120.0);
            SetSkill( SkillName.EvalInt, 100.3, 120.0);
            SetSkill( SkillName.Anatomy, 100.3, 120.0);
            SetSkill( SkillName.Poisoning, 100.3, 120.0);
            SetSkill( SkillName.Meditation, 100.3, 120.0);

			Fame = 15000;
			Karma = 15000;

            Tamable = true;
            ControlSlots = 3;//set the control slots required here
            MinTameSkill = 0.0;//Set min taming skill here
		}

        public override bool SubdueBeforeTame { get { return true; } }//Add or remove any other things you want the steed to do.
        public override bool CanRummageCorpses { get { return true; } }
        public override bool HasBreath { get { return true; } }
		public override int Meat{ get{ return 3; } }
		public override int Hides{ get{ return 10; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override void GenerateLoot() //Loot options here
		{
			AddLoot(LootPack.Rich, 2);
		}

		public override bool HandlesOnSpeech(Mobile from)
        {
            if (!from.Alive && from.InRange(this.Location, 12))//I put the commands like this so it doesn't spam players.
            {
                this.Say("Double click me");
                this.Say("out of war mode");
                this.Say("to be resurrected!");
            }
            return base.HandlesOnSpeech(from);
        }

        public override void OnDoubleClickDead(Mobile from)//Edit what you want the steed to say when resurrecting below:
        {
            this.Say("There you go, Enjoy life!");
            if (!from.Alive)
            {
                from.Resurrect();
            }
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile == ControlMaster && e.Mobile.InRange(this.Location, 5))//Edit/add what ever color you want below:
            {
                if (e.Speech == "change random")
                {
                    this.Say("As you wish!");
                    this.Hue = Utility.Random(2, 1200);//Edit which colors you want him to cycle through here.
                }
                if (e.Speech == "change red")
                {
                    this.Say("As you wish!");
                    this.Hue = 33;
                }
                if (e.Speech == "change black")
                {
                    this.Say("As you wish!");
                    this.Hue = 1;
                }
                if (e.Speech == "change blue")
                {
                    this.Say("As you wish!");
                    this.Hue = 2;
                }
                if (e.Speech == "change pink")
                {
                    this.Say("As you wish!");
                    this.Hue = 26;
                }
                if (e.Speech == "change orange")
                {
                    this.Say("As you wish!");
                    this.Hue = 45;
                }
                if (e.Speech == "change yellow")
                {
                    this.Say("As you wish!");
                    this.Hue = 55;
                }
                if (e.Speech == "change purple")
                {
                    this.Say("As you wish!");
                    this.Hue = 117;
                }
                if (e.Speech == "change green")
                {
                    this.Say("As you wish!");
                    this.Hue = 66;
                }
                if (e.Speech == "change brown")
                {
                    this.Say("As you wish!");
                    this.Hue = 1044;
                }
                if (e.Speech == "change gray")
                {
                    this.Say("As you wish!");
                    this.Hue = 941;
                }
                if (e.Speech == "change normal")
                {
                    this.Say("As you wish!");
                    this.Hue = 1289;
                }
                base.OnSpeech(e);
            }
        }
		public RainbowSwampDragon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
	}
}
