using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a gore fiend corpse" )]
	public class GoreFiend : BaseCreature
	{
		[Constructable]
		public GoreFiend() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gore fiend";
			Body = 305;
			BaseSoundID = 224;

			SetStr( 161, 185 );
			SetDex( 41, 65 );
			SetInt( 46, 70 );

			SetHits( 97, 111 );

			SetDamage( 15, 21 );

			SetDamageType( ResistanceType.Physical, 85 );
			SetDamageType( ResistanceType.Poison, 15 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 15 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 40.1, 55.0 );
			SetSkill( SkillName.Tactics, 45.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 24;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
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

		public override int GetDeathSound()
		{
			return 1218;
		}

		public override bool BleedImmune{ get{ return true; } }

		public GoreFiend( Serial serial ) : base( serial )
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