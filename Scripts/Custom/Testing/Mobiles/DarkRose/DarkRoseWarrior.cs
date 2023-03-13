using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class DarkRoseWarrior : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public DarkRoseWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{

			Title = "Warrior of the Dark Rose";
			Hue = Utility.RandomSkinHue();

				Body = 0x190;
				Name = NameList.RandomName( "male" );
			
			SetStr( 286, 400 );
			SetDex( 181, 195 );
			SetInt( 351, 485 );

            SetHits(1500, 3000);
			SetDamage( 10, 15 );

			SetSkill( SkillName.MagicResist, 75.0, 87.5 );
			SetSkill( SkillName.Swords, 85.0, 97.5 );
			SetSkill( SkillName.Tactics, 75.0, 87.5 );
			SetSkill( SkillName.Wrestling, 85.0, 97.5 );

			Fame = 1000;
			Karma = -1000;

            RingmailChest chest = new RingmailChest();
            chest.Hue = 0;
            chest.Movable = false;
            AddItem(chest);

            RingmailArms arms = new RingmailArms();
            arms.Hue = 0;
            arms.Movable = false;
            AddItem(arms);

            RingmailGloves gloves = new RingmailGloves();
            gloves.Hue = 0;
            gloves.Movable = false;
            AddItem(gloves);

            PlateGorget gorget = new PlateGorget();
            gorget.Hue = 0;
            gorget.Movable = false;
            AddItem(gorget);

            NorseHelm helm = new NorseHelm();
            helm.Hue = 0;
            helm.Movable = false;
            AddItem(helm);

            RingmailLegs legs = new RingmailLegs();
            legs.Hue = 0;
            legs.Movable = false;
            AddItem(legs);

            Longsword weapon = new Longsword();
            weapon.Hue = 0;
            weapon.Movable = false;
            AddItem(weapon);

            Buckler shield = new Buckler();
            shield.Hue = 0;
            shield.Movable = false;
            AddItem(shield);

            Boots boots = new Boots();
            boots.Hue = 0;
            boots.Movable = false;
            AddItem(boots);

			

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average);
		}

		public override bool AlwaysMurderer{ get{ return true; } }

        public DarkRoseWarrior(Serial serial)
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