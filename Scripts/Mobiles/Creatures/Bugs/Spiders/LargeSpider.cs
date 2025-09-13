using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class LargeSpider : BaseCreature
	{
		[Constructable]
		public LargeSpider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a huge spider";
			Body = Utility.RandomList( 28, 140 );
			BaseSoundID = 0x388;

			SetStr( 46, 70 );
			SetDex( 46, 65 );
			SetInt( 6, 12 );

			SetHits( 20, 30 );
			SetMana( 0 );

			SetDamage( 2, 8 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Poison, 25, 35 );

			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 35.1, 50.0 );
			SetSkill( SkillName.Wrestling, 50.1, 65.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 6;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 29.1;

			PackItem( new SpidersSilk( 2 ) );

			Item Venom = new VenomSack();
				Venom.Name = "lesser venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Arachnid; } }

		public LargeSpider( Serial serial ) : base( serial )
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