using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class DemiLich : BaseCreature
	{
		[Constructable]
		public DemiLich() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "evil mage" );
			BaseSoundID = 0x3E9;
			Body = 0x190; 
			Title = "the demilich";

			if ( Utility.RandomMinMax( 0, 1 ) == 1 )
			{
				Name = NameList.RandomName( "evil witch" );
				Body = 0x191; 
				BaseSoundID = 0x4B0;
			}

			Hunger = Server.Misc.RandomThings.GetRandomColor( 0 );
			Thirst = 0;

			string gear = "demilich";
			int Rgear = Server.Misc.RandomThings.GetRandomColor( 0 );
			int Cgear = 0;
			int Magic = 1;
			int Chance = 8;
			int Mgear = 3;

			int bone = 0x48F;
			switch( Utility.RandomMinMax( 0, 8 ) )
			{
				case 0: bone = 0x48D; AddItem( new LightSource() );		break;
				case 1: bone = 0x48E; AddItem( new LightSource() );		break;
				case 2: bone = 0x48F; AddItem( new LightSource() );		break;
				case 3: bone = 0x490; AddItem( new LightSource() );		break;
				case 4: bone = 0x491; AddItem( new LightSource() );		break;
				case 5: bone = 0x47E; AddItem( new LightSource() );		break;
				case 6: bone = 0xB4E; break;
				case 7: bone = 0x430; Title = "the crypt thing";	Chance = 6;		Mgear = 4;	Thirst = bone;	Hunger = bone;	gear = "crypt";		Magic = 3;	break;
				case 8: bone = 0x497; Title = "the dark lich";		Chance = 4;		Mgear = 5;	Thirst = bone;	Hunger = bone;	gear = "dark";		Magic = 5;	break;
			}

			Hue = bone;

			Robe robe = new Robe();
				robe.Name = gear + " robe";
				robe.Hue = Hunger;
				robe.LootType = LootType.Blessed;
				AddItem( robe );

			QuarterStaff staff = new QuarterStaff();
				staff.Name = gear + " staff";
				staff.ItemID = Utility.RandomList( 0xDF0, 0x13F8, 0xE89, 0x2D25 );
				TithingPoints = staff.ItemID;
				staff.Hue = Thirst;
				staff.LootType = LootType.Blessed;
				staff.Attributes.SpellChanneling = 1;
				AddItem( staff );

			Item helm = new WornHumanDeco();
				helm.Name = "skull";
				helm.ItemID = 0x1451;	if ( ( Magic == 3 || Magic == 5 ) && Utility.RandomBool() ){ helm.ItemID = 0x4CDD; } else if ( bone == 0xB4E ){ helm.ItemID = 0x4CDB; }
				helm.Hue = bone;
				helm.Layer = Layer.Helm;
				AddItem( helm );

			Item hands = new WornHumanDeco();
				hands.Name = "bony fingers";
				hands.ItemID = 0x1450;
				hands.Hue = bone;
				hands.Layer = Layer.Gloves;
				AddItem( hands );

			Item feet = new WornHumanDeco();
				feet.Name = "bony feet";
				feet.ItemID = 0x170D;
				feet.Hue = bone;
				feet.Layer = Layer.Shoes;
				AddItem( feet );

			SetStr( 466, 555 );
			SetDex( 146, 165 );
			SetInt( 666, 755 );

			SetHits( 450, 503 );

			SetDamage( 13, 18 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Necromancy, 90, 110.0 );
			SetSkill( SkillName.SpiritSpeak, 90.0, 110.0 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 150.5, 200.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Macing, 60.1, 80.0 );

			Fame = 21000;
			Karma = -21000;

			VirtualArmor = 50;
			PackNecroReg( 24, 80 );

			int[] list = new int[]
				{
					0x1B11, 0x1B12, 0x1B13, 0x1B14, 0x1B15, 0x1B16, 0x1B19, 0x1B1A, // bone parts
					0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
					0x1B17, 0x1B18, 0x1B1B, 0x1B1C, // ribs and spines
					0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
					0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 // bones
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.BeforeMyBirth( this );
			base.OnAfterSpawn();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, defender );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			Mobile killer = this.LastKiller;

			if ( killer is BaseCreature )
				killer = ((BaseCreature)killer).GetMaster();

			if ( killer is PlayerMobile )
			{
				if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Server.Misc.IntelligentAction.FameBasedEvent( this ) )
				{
					LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
					Server.Misc.ContainerFunctions.MakeTomb( MyChest, this, 1 );
					c.DropItem( MyChest );
				}

				if ( GetPlayerInfo.LuckyKiller( killer.Luck, killer ) && Utility.RandomMinMax( 1, 300 ) == 1 )
				{
					DemonPrison shard = new DemonPrison();
					c.DropItem( shard );
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			this.Body = 50;
			Server.Misc.IntelligentAction.BeforeMyDeath( this );
			Server.Misc.IntelligentAction.DropItem( this, this.LastKiller );
			return base.OnBeforeDeath();
		}

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

        public override int GetAngerSound()
        {
            return 0x61E;
        }

        public override int GetDeathSound()
        {
            return 0x61F;
        }

        public override int GetHurtSound()
        {
            return 0x620;
        }

        public override int GetIdleSound()
        {
            return 0x621;
        }

		public DemiLich( Serial serial ) : base( serial )
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