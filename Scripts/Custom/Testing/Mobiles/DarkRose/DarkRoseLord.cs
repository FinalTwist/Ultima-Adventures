using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class DarkRoseLord : BaseCreature
	{
        private bool m_Stunning;

        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }

        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public DarkRoseLord() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{

            Title = "Lord of the Dark Rose";
			Hue = Utility.RandomSkinHue();

				Body = 0x190;
				Name = NameList.RandomName( "male" );
			

			SetStr( 426, 570 );
			SetDex( 221, 245 );
			SetInt( 371, 485 );

            SetHits(5000, 8500);
            SetMana(500, 650);
			SetDamage( 20, 35 );

			SetSkill( SkillName.MagicResist, 85.0, 97.5 );
			SetSkill( SkillName.Swords, 109.0, 119.5 );
			SetSkill( SkillName.Tactics, 105.0, 117.5 );
			SetSkill( SkillName.Wrestling, 95.0, 107.5 );
            SetSkill(SkillName.Healing, 100.0);

			Fame = 15000;
			Karma = -15000;

            DarkRoseChest chest = new DarkRoseChest();
            chest.Hue = 2949;
            chest.Movable = false;
            AddItem(chest);

            DarkRoseArms arms = new DarkRoseArms();
            arms.Hue = 2949;
            arms.Movable = false;
            AddItem(arms);

            DarkRoseGloves gloves = new DarkRoseGloves();
            gloves.Hue = 2949;
            gloves.Movable = false;
            AddItem(gloves);

            DarkRoseGorget gorget = new DarkRoseGorget();
            gorget.Hue = 2949;
            gorget.Movable = false;
            AddItem(gorget);

            DarkRoseLegs legs = new DarkRoseLegs();
            legs.Hue = 2949;
            legs.Movable = false;
            AddItem(legs);

            DarkRosePetals cloak = new DarkRosePetals();
            cloak.Hue = 2949;
            cloak.Movable = false;
            AddItem(cloak);

            GreaterDarkRoseThorn weapon = new GreaterDarkRoseThorn();
            weapon.Hue = 2949;
            weapon.Movable = false;
            AddItem(weapon);

            HornedTribalMask clothes = new DarkRoseHelm();
            clothes.Hue = 2949;
            clothes.Movable = false;
            AddItem(clothes);

            DarkRoseShield shield = new DarkRoseShield();
            shield.Hue = 2949;
            shield.Movable = false;
            AddItem(shield);



            new HellSteed().Rider = this;
			

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

        public override bool OnBeforeDeath()
        {
            IMount mount = this.Mount;
            if (mount != null)
            {
                mount.Rider = null;

                if (mount is Mobile) ((Mobile)mount).Delete();
            }
            switch (Utility.Random(50))
            {
                case 0: PackItem(new DarkRoseArms()); break;
                case 1: PackItem(new DarkRoseLegs()); break;
                case 2: PackItem(new DarkRoseGorget()); break;
                case 3: PackItem(new DarkRoseHelm()); break;
                case 4: PackItem(new DarkRoseChest()); break;
                case 5: PackItem(new DarkRoseGloves()); break;
                case 6: PackItem(new DarkRoseShield()); break;
                case 7: PackItem(new DarkRosePetals()); break;
                case 8: PackItem(new GreaterDarkRoseThorn()); break;
            }

            return base.OnBeforeDeath();
        }

        public DarkRoseLord(Serial serial)
            : base(serial)
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