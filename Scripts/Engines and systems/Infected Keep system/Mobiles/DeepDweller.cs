using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dweller corpse" )]
	public class DeepDweller : BaseUndead
	{
		[Constructable]
		public DeepDweller () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dweller of the deep" ;
            Title = "*Infected*";
			Body = 172;
			BaseSoundID = 357;
			Hue = 768;

			AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
			CanInfect = true;

			SetStr( 986, 1185 );
			SetDex( 177, 255 );
			SetInt( 500, 750 );

			SetHits( 1592, 1711 );

			SetDamage( 35, 50 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 99.5, 120.0 );
			SetSkill( SkillName.Meditation, 25.1, 50.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -27000;

			VirtualArmor = 90;

			PackItem( new Longsword() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override void OnDeath( Container c )
		{
            base.OnDeath(c);
            if (Utility.RandomDouble() > 0.90)
                c.DropItem(new ZombieKillerPotion());
            if (Utility.RandomDouble() >= 0.95)
                c.DropItem(new SlayerDeed());

            switch (Utility.Random(400))
            {
                case 0: c.DropItem(new CombativeNorseHelm()); break;
                case 1: c.DropItem(new CombativePlateArms()); break;
                case 2: c.DropItem(new CombativePlateChest()); break;
                case 3: c.DropItem(new CombativePlateGloves()); break;
                case 4: c.DropItem(new CombativePlateGorget()); break;
                case 5: c.DropItem(new CombativePlateLegs()); break;
                case 6: c.DropItem(new CombativePlateSkirt()); break;
                case 7: c.DropItem(new FemaleCombativePlateChest()); break;
            }
			if (Utility.RandomMinMax(1, 50) == 69)
				c.DropItem(new InfectionPotion());


		}

		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public DeepDweller( Serial serial ) : base( serial )
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

						AIFullSpeedActive = true;
			AIFullSpeedPassive = false; // Force full speed
		}
	}
}