using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class DarkRoseKnight : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public DarkRoseKnight() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{

			Title = "Knight of the Dark Rose";
			Hue = Utility.RandomSkinHue();

				Body = 0x190;
				Name = NameList.RandomName( "male" );
			

			SetStr( 306, 450 );
			SetDex( 201, 215 );
			SetInt( 351, 485 );

            SetHits(2500, 4000);
			SetDamage( 15, 25 );

			SetSkill( SkillName.MagicResist, 85.0, 97.5 );
			SetSkill( SkillName.Swords, 99.0, 117.5 );
			SetSkill( SkillName.Tactics, 95.0, 107.5 );
			SetSkill( SkillName.Wrestling, 95.0, 107.5 );

			Fame = 1000;
			Karma = -1000;

            PlateChest chest = new PlateChest();
            chest.Hue = 2023;
            chest.Movable = false;
            AddItem(chest);

            PlateArms arms = new PlateArms();
            arms.Hue = 2023;
            arms.Movable = false;
            AddItem(arms);

            PlateGloves gloves = new PlateGloves();
            gloves.Hue = 2023;
            gloves.Movable = false;
            AddItem(gloves);

            PlateGorget gorget = new PlateGorget();
            gorget.Hue = 2023;
            gorget.Movable = false;
            AddItem(gorget);

            PlateHelm helm = new PlateHelm();
            helm.Hue = 2023;
            helm.Movable = false;
            AddItem(helm);

            PlateLegs legs = new PlateLegs();
            legs.Hue = 2023;
            legs.Movable = false;
            AddItem(legs);

            DarkRosePetals cloak = new DarkRosePetals();
            cloak.Hue = 2949;
            cloak.Movable = false;
            AddItem(cloak);

            LesserDarkRoseThorn weapon = new LesserDarkRoseThorn();
            weapon.Hue = 2949;
            weapon.Movable = false;
            AddItem(weapon);

            DarkRoseShield shield = new DarkRoseShield();
            shield.Hue = 2949;
            shield.Movable = false;
            AddItem(shield);

            new Nightmare().Rider = this;
			

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
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
                      switch (Utility.Random(15))
            {
                case 0: PackItem(new DarkRoseShield()); break;
                case 1: PackItem(new LesserDarkRoseThorn()); break;
                case 2: PackItem(new DarkRosePetals()); break;
            }

            return base.OnBeforeDeath();
        }
        public DarkRoseKnight(Serial serial)
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