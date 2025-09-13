using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a sakkhra corpse" )]
	public class HaptahArcher : BaseCreature
	{
		public override InhumanSpeech SpeechType
		{
			get
			{
				if ( ControlSlots == 5 )
					return null;

				return InhumanSpeech.Lizardman;
			}
		}

        public override int GetAngerSound()
        {
			if ( ControlSlots == 5 )
				return 0x5E1;

            return 0x1A1;
        }

        public override int GetIdleSound()
        {
			if ( ControlSlots == 5 )
				return 0x5E1;

            return 0x1A2;
        }

		[Constructable]
		public HaptahArcher() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "lizardman" );
			Title = "the haptah";
			Body = 326;
			BaseSoundID = 417;

			SetStr( 136, 165 );
			SetDex( 56, 75 );
			SetInt( 31, 55 );

			SetHits( 82, 99 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 15, 25 );

			SetSkill( SkillName.MagicResist, 40.1, 55.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );
			SetSkill( SkillName.Archery, 50.1, 60.0 );

			Fame = 3500;
			Karma = 3500;

			VirtualArmor = 38;

			MonsterGloves gloves = new MonsterGloves();
			gloves.ThrowType = "Daggers";
			AddItem( gloves );

			ThrowingWeapon toss = new ThrowingWeapon( Utility.RandomMinMax( 10, 30 ) );
			toss.ammo = "Throwing Daggers"; toss.ItemID = 0x10B7; toss.Name = "throwing dagger";
			PackItem( toss );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();

			if ( (Region.Find( this.Location, this.Map )).IsPartOf( "the Sanctum of Saltmarsh" ) && Utility.RandomMinMax( 1, 4 ) > 1 )
				ControlSlots = 5;
		}

		public HaptahArcher( Serial serial ) : base( serial )
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