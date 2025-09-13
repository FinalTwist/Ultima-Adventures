using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class PearlDiver : BaseCreature
	{
        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }

        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public PearlDiver() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{

			Title = "A PearlDiver";
			Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
            }
			

			SetStr( 250, 320 );
			SetDex( 201, 215 );
			SetInt( 201, 235 );

            SetHits(1200, 1800);
			SetDamage( 15, 25 );

			SetSkill( SkillName.MagicResist, 85.0, 97.5 );
			SetSkill( SkillName.Swords, 99.0, 117.5 );
			SetSkill( SkillName.Tactics, 95.0, 107.5 );
			SetSkill( SkillName.Fencing, 95.0, 107.5 );

			Fame = 1000;
			Karma = 1000;
            VirtualArmor = 50;

            AddItem(new Server.Items.Shirt());
            AddItem(new Server.Items.ShortPants());

            PearlKnife weapon = new PearlKnife();
            weapon.Hue = 1153;
            weapon.Movable = false;
            AddItem(weapon);


			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

                public override bool OnBeforeDeath()
        {
 
                      switch (Utility.Random(15))
            {
                case 0: PackItem(new WhitePearl()); break;

            }

            return base.OnBeforeDeath();
        }
            public PearlDiver(Serial serial): base(serial)
		{
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