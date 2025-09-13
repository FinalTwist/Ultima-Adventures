using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a skittering hopper corpse" )]
	public class SkitteringHopper : BaseCreature
	{
		[Constructable]
		public SkitteringHopper() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a skittering hopper";
			Body = 155;
			BaseSoundID = 959;

			SetStr( 41, 65 );
			SetDex( 91, 115 );
			SetInt( 26, 50 );

			SetHits( 31, 45 );

			SetDamage( 3, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 30.1, 45.0 );
			SetSkill( SkillName.Tactics, 45.1, 70.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -12.9;

			VirtualArmor = 12;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			if ( Utility.RandomDouble() <= 0.02 )
				switch ( Utility.Random( 40 ))		  
				{
					case 0: c.DropItem( new ArmorEnhancementDeed() ); break;
					case 1: c.DropItem( new AosEnhancementDeed() ); break;
					case 2: c.DropItem( new EnhancementDeed() ); break;
					case 3: c.DropItem( new SkillEnhancementDeed() ); break;
					case 4: c.DropItem( new WeaponEnhancementDeed() ); break;
				};

		}

		public override int TreasureMapLevel{ get{ return 1; } }

		public SkitteringHopper( Serial serial ) : base( serial )
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