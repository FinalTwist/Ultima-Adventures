using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a baby wyrm corpse" )]
	public class FireWyrmling : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public FireWyrmling () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fire wyrmling";
			Body = 269;
			Hue = Utility.RandomList( 0x489, 0x4E7, 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC );
			BaseSoundID = 660;

			SetStr( 198, 212 );
			SetDex( 76, 95 );
			SetInt( 109, 116 );

			SetHits( 198, 212 );

			SetDamage( 8, 13 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 70.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 65.1, 70.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 15;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 30.3;

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 4; } }
		public override HideType HideType{ get{ return HideType.Volcanic; } }
		public override int Scales{ get{ return 1; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Yellow; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }

		public override int GetAttackSound(){ return 0x5E8; }	// A
		public override int GetDeathSound(){ return 0x5E9; }	// D
		public override int GetHurtSound(){ return 0x5EA; }		// H

		public FireWyrmling( Serial serial ) : base( serial )
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