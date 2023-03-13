using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "Zorn's corpse" )]
	public class ZornTheBlacksmith : BaseCreature
	{
		[Constructable]
		public ZornTheBlacksmith() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Zorn";
			Title = "the blacksmith";
			Body = 75;
			BaseSoundID = 609;
			Hue = 0;

			SetStr( 536, 585 );
			SetDex( 126, 145 );
			SetInt( 281, 305 );

			SetHits( 322, 351 );

			SetDamage( 13, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 80.2, 110.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 11500;
			Karma = -11500;

			VirtualArmor = 40;

			PackItem( new RubyPickaxe() );

			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				LootChest MyChest = new LootChest( Server.Misc.IntelligentAction.FameBasedLevel( this ) );
				MyChest.Name = "Zorn's Caddellite Chest";
				MyChest.Hue = 0x5B6;
				PackItem( MyChest );
			}
		}

		public override int GetAttackSound(){ return 0x5F8; }	// A
		public override int GetDeathSound(){ return 0x5F9; }	// D
		public override int GetHurtSound(){ return 0x5FA; }		// H

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls );
		}

		public override int Meat{ get{ return 4; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Hides{ get{ return 18; } }
		public override HideType HideType{ get{ return HideType.Goliath; } }

		public ZornTheBlacksmith( Serial serial ) : base( serial )
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