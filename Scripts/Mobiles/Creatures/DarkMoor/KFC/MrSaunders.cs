using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class MrSaunders : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public MrSaunders() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = ( "Saunders" );
			SpeechHue = Utility.RandomDyedHue();
			Title = "the Colonel";
			Hue = Utility.RandomSkinHue();
			Body = 0x190;

			SetStr( 150, 160 );
			SetDex( 101, 125 );
			SetInt( 81, 105 );
            SetHits(500, 700);
			SetDamage( 12, 24 );
            SetDamageType(ResistanceType.Fire, 100);

            SetResistance(ResistanceType.Physical, 65, 70);
            SetResistance(ResistanceType.Fire, 80, 80);
            SetResistance(ResistanceType.Cold, 25, 30);
            SetResistance(ResistanceType.Poison, 45, 55);


            SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 100, 110 );
			SetSkill( SkillName.Swords, 85, 90 );
			SetSkill( SkillName.Tactics, 90, 100 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			Fame = 10000;
			Karma = 10000;

			AddItem( new Boots( 1175 ) );
			AddItem( new FancyShirt( 1153 ) );
			AddItem( new TricorneHat( 1153 ) );
			AddItem( new TattsukeHakama( 1153 ) );
            AddItem(new FullApron(32));
            AddItem(new Cleaver());



			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
            AddItem(new ChickenLeg(4));
            AddItem(new CookedBird(4));
            
        }

		public override bool AlwaysMurderer{ get{ return false; } }

		public MrSaunders( Serial serial ) : base( serial )
		{
		}

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (Utility.RandomMinMax(1, 4) == 1)
            {
                int goo = 0;

                foreach (Item splash in this.GetItemsInRange(10)) { if (splash is MonsterSplatter && splash.Name == "hot oil") { goo++; } }

                if (goo == 0)
                {
                    attacker.PlaySound(0x639);
                    MonsterSplatter.AddSplatter(this.X, this.Y, this.Z, this.Map, this.Location, this, "hot oil", 0x7D1, 0);
                    this.Say("It's finger licking good!!!");
                    //add fire damage?
                }
            }
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
