using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a maggoty corpse" )] // TODO: Corpse name?
	public class MoundOfMaggots : BaseCreature
	{
		[Constructable]
		public MoundOfMaggots() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mound of maggots";
			Body = 775;
			BaseSoundID = 898;

			SetStr( 61, 70 );
			SetDex( 61, 70 );
			SetInt( 10 );

			SetMana( 0 );

			SetDamage( 3, 9 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Poison, 100 );

			SetSkill( SkillName.Tactics, 50.0 );
			SetSkill( SkillName.Wrestling, 50.1, 60.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 24;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.Gems );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			if ( Utility.RandomDouble() <= 0.01 )
				switch ( Utility.Random( 20 ))		  
				{
					case 0: c.DropItem( new ArmorEnhancementDeed() ); break;
					case 1: c.DropItem( new AosEnhancementDeed() ); break;
					case 2: c.DropItem( new EnhancementDeed() ); break;
					case 3: c.DropItem( new SkillEnhancementDeed() ); break;
					case 4: c.DropItem( new WeaponEnhancementDeed() ); break;
				};

		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public MoundOfMaggots( Serial serial ) : base( serial )
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