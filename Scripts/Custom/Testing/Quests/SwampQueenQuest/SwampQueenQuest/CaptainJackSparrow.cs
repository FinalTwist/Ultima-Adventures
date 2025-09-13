using System;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	public class CaptainJackSparrow : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public CaptainJackSparrow(): base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x190;
			Name = "Captain Jack Sparrow";
			Title = "Dread Pirate";
			Hue = 0;

			SetStr( 351, 400 );
			SetDex( 151, 165 );
			SetInt( 76, 100 );

			SetHits( 1200, 1500 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Cold, 25 );

			SetResistance( ResistanceType.Physical, 45 );
			SetResistance( ResistanceType.Fire, 30 );
			SetResistance( ResistanceType.Cold, 60 );
			SetResistance( ResistanceType.Poison, 35 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.Wrestling, 100.0 );
			SetSkill( SkillName.Swords, 120.1, 130.0 );
			SetSkill( SkillName.Anatomy, 120.1, 130.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );

			Fame = 10000;
			Karma = -10000;         

			VirtualArmor = 55;          
         
			Cutlass weapon = new Cutlass();
			weapon.Hue = 0x835;
			weapon.Movable = false;
			AddItem( weapon );

			MetalShield shield = new MetalShield();
			shield.Hue = 0x835;
			shield.Movable = false;
			AddItem( shield );

			TricorneHat hat = new TricorneHat();
			hat.Hue = 0x835;
			AddItem( hat );

			BoneArms arms = new BoneArms();
			arms.Hue = 0x835;
			AddItem( arms );

			BoneGloves gloves = new BoneGloves();
			gloves.Hue = 0x835;
			AddItem( gloves );

			BoneChest tunic = new BoneChest();
			tunic.Hue = 0x835;
			AddItem( tunic );

			BoneLegs legs = new BoneLegs();
			legs.Hue = 0x835;
			AddItem( legs );

			AddItem( new Boots() );
            PackGold(1500, 2000);

            switch (Utility.Random(3))
            {
                case 0: PackItem(new DreadPirateHat()); break;
                case 1: PackItem(new ShipModelOfTheHMSCape()); break;
                case 2: PackItem(new CaptainJackSparrowsCutlass()); break;

            }
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

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 4);
        }
        
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

        public override bool OnBeforeDeath()
        {
            CaptainJackSparrowGate g = new CaptainJackSparrowGate();
            g.MoveToWorld(new Point3D(5205, 1783, 0), Map.Trammel);

            return base.OnBeforeDeath();

        }

		public CaptainJackSparrow( Serial serial ) : base( serial )
		{
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